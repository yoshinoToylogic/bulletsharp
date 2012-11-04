using System;
using System.Collections.Generic;
using System.IO;
using ClangSharp;

namespace BulletSharpGen
{
    class CppReader
    {
        string src;
        Index index;
        List<string> headerQueue = new List<string>();
        List<string> clangOptions = new List<string>();
        Dictionary<string, string> excludedClasses = new Dictionary<string, string>();
        Dictionary<string, string> excludedMethods = new Dictionary<string, string>();
        AccessSpecifier currentMemberAccess;
        HeaderDefinition currentHeader;
        ClassDefinition currentClass;
        MethodDefinition currentMethod;
        EnumDefinition currentEnum;
        TranslationUnit currentTU;

        public Dictionary<string, ClassDefinition> ClassDefinitions = new Dictionary<string, ClassDefinition>();
        public Dictionary<string, HeaderDefinition> HeaderDefinitions = new Dictionary<string, HeaderDefinition>();

        public CppReader(string sourceDirectory)
        {
            src = Path.GetFullPath(sourceDirectory);
            src = src.Replace('\\', '/');

            string[] commonHeaders;
            List<string> excludedHeaders = new List<string>();

            // Exclude C API
            excludedHeaders.Add(src + "Bullet-C-Api.h");

            // Include directory
            clangOptions.Add("-I");
            clangOptions.Add(src);

            // Specify C++ headers, not C ones
            clangOptions.Add("-x");
            clangOptions.Add("c++-header");

            // Exclude serialization classes
            excludedClasses.Add("btCylinderShapeData", null);
            excludedClasses.Add("btTypedConstraintData", null);

            // Exclude irrelevant methods
            excludedMethods.Add("operator new", null);
            excludedMethods.Add("operator delete", null);
            excludedMethods.Add("operator new[]", null);
            excludedMethods.Add("operator delete[]", null);
            excludedMethods.Add("operator+=", null);
            excludedMethods.Add("operator-=", null);
            excludedMethods.Add("operator*=", null);
            excludedMethods.Add("operator/=", null);
            excludedMethods.Add("operator==", null);
            excludedMethods.Add("operator!=", null);
            excludedMethods.Add("operator()", null);

            // Enumerate all header files in the source tree
            var headerFiles = Directory.EnumerateFiles(src, "*.h", SearchOption.AllDirectories);
            foreach (string header in headerFiles)
            {
                string headerCanonical = header.Replace('\\', '/');
                if (!excludedHeaders.Contains(headerCanonical))
                {
                    headerQueue.Add(headerCanonical);
                }
            }

            // Parse the common headers
            index = new Index();
            commonHeaders = new[] { src + "btBulletCollisionCommon.h", src + "btBulletDynamicsCommon.h" };
            foreach (string commonHeader in commonHeaders)
            {
                if (!headerQueue.Contains(commonHeader))
                {
                    Console.WriteLine("Could not find " + commonHeader);
                    return;
                }
                readHeader(commonHeader);
            }
            index.Dispose();

            Console.WriteLine("Read complete - headers: " + HeaderDefinitions.Count + ", classes: " + ClassDefinitions.Count);
        }

        Cursor.ChildVisitResult HeaderVisitor(Cursor cursor, Cursor parent)
        {
            string filename = cursor.Extent.Start.File.Name.Replace('\\', '/');
            if (!filename.StartsWith(src, StringComparison.OrdinalIgnoreCase))
            {
                return Cursor.ChildVisitResult.Continue;
            }

            // Have we visited this header already?
            if (HeaderDefinitions.ContainsKey(filename))
            {
                currentHeader = HeaderDefinitions[filename];
            }
            else
            {
                // No, define a new one
                currentHeader = new HeaderDefinition(filename);
                HeaderDefinitions.Add(filename, currentHeader);
                headerQueue.Remove(filename);
            }

            if ((cursor.Kind == CursorKind.ClassDecl || cursor.Kind == CursorKind.StructDecl) && cursor.IsDefinition)
            {
                ParseClassCursor(cursor);
                return Cursor.ChildVisitResult.Continue;
            }
            else if (cursor.Kind == CursorKind.TypedefDecl)
            {
                //var underlying = GetTypeRef(cursor.TypedefDeclUnderlyingType);
                ParseClassCursor(cursor);
            }
            else if (cursor.Kind == CursorKind.EnumDecl)
            {
                currentEnum = new EnumDefinition(cursor.Spelling);
                currentHeader.Enums.Add(currentEnum);
                cursor.VisitChildren(EnumVisitor);
                currentEnum = null;
            }
            return Cursor.ChildVisitResult.Continue;
        }

        Cursor.ChildVisitResult EnumVisitor(Cursor cursor, Cursor parent)
        {
            if (cursor.Kind == CursorKind.EnumConstantDecl)
            {
                currentEnum.EnumConstants.Add(cursor.Spelling);
                currentEnum.EnumConstantValues.Add(cursor.Spelling);
            }
            else if (cursor.Kind == CursorKind.IntegerLiteral)
            {
            }
            else if (cursor.Kind == CursorKind.ParenExpr)
            {
                return Cursor.ChildVisitResult.Continue;
            }
            return Cursor.ChildVisitResult.Recurse;
        }

        void ParseClassCursor(Cursor cursor)
        {
            string className = cursor.Spelling;
            if (ClassDefinitions.ContainsKey(className) || excludedClasses.ContainsKey(className))
            {
                return;
            }

            ClassDefinition parentClass = currentClass;
            AccessSpecifier parentMemberAccess = currentMemberAccess;
            currentClass = new ClassDefinition(className, currentHeader);
            ClassDefinitions.Add(className, currentClass);
            if (cursor.Kind == CursorKind.ClassDecl)
            {
                currentMemberAccess = AccessSpecifier.Private; // default class access specifier
            }
            else
            {
                currentClass.IsStruct = true;
                currentMemberAccess = AccessSpecifier.Public; // default struct access specifier
            }
            if (parentClass != null)
            {
                parentClass.Classes.Add(currentClass);
            }
            else
            {
                currentHeader.Classes.Add(currentClass);
            }
            cursor.VisitChildren(ClassVisitor);

            // Restore parent state
            currentClass = parentClass;
            currentMemberAccess = parentMemberAccess;
        }

        Cursor.ChildVisitResult ClassVisitor(Cursor cursor, Cursor parent)
        {
            if (cursor.Kind == CursorKind.CxxAccessSpecifier)
            {
                currentMemberAccess = cursor.AccessSpecifier;
            }
            else if (cursor.Kind == CursorKind.CxxBaseSpecifier)
            {
                currentClass.BaseClass = new TypeRefDefinition(cursor.Definition.Spelling);
            }
            else if (currentMemberAccess == AccessSpecifier.Public)
            {
                if ((cursor.Kind == CursorKind.ClassDecl || cursor.Kind == CursorKind.StructDecl) && cursor.IsDefinition)
                {
                    ParseClassCursor(cursor);
                }
                else if (cursor.Kind == CursorKind.CxxMethod || cursor.Kind == CursorKind.Constructor)
                {
                    string methodName = cursor.Spelling;
                    if (excludedMethods.ContainsKey(methodName))
                    {
                        return Cursor.ChildVisitResult.Continue;
                    }

                    currentMethod = new MethodDefinition(methodName, currentClass, cursor.NumArguments);
                    currentMethod.ReturnType = new TypeRefDefinition(cursor.ResultType);
                    currentMethod.IsStatic = cursor.IsStaticCxxMethod;
                    currentMethod.IsConstructor = cursor.Kind == CursorKind.Constructor;

                    // Check if the method is abstract (no clang_isAbstract available, so look at tokens)
                    IList<Token> tokens = currentTU.Tokenize(cursor.Extent);
                    if (tokens.Count > 3)
                    {
                        if (tokens[tokens.Count - 3].Spelling == "=" &&
                            tokens[tokens.Count - 2].Spelling == "0" &&
                            tokens[tokens.Count - 1].Spelling == ";")
                        {
                            currentMethod.IsAbstract = true;
                            currentClass.IsAbstract = true;
                        }
                    }

                    // Parse arguments
                    for (uint i = 0; i < cursor.NumArguments; i++)
                    {
                        Cursor arg = cursor.GetArgument(i);

                        string parameterName = arg.Spelling;
                        if (parameterName == "")
                        {
                            parameterName = "__unnamed";
                        }
                        currentMethod.Parameters[i] = new ParameterDefinition(parameterName, new TypeRefDefinition(arg.Type));

                        // Check if it's an optional parameter
                        IList<Token> argTokens = currentTU.Tokenize(arg.Extent);
                        foreach (Token token in argTokens)
                        {
                            if (token.Spelling == "=")
                            {
                                currentMethod.Parameters[i].IsOptional = true;
                            }
                        }
                    }

                    currentMethod = null;
                }
                else if (cursor.Kind == CursorKind.FieldDecl)
                {
                    var field = new FieldDefinition(cursor.Spelling, currentClass);
                    field.Type = new TypeRefDefinition(cursor.Type);
                }
                else if (cursor.Kind == CursorKind.EnumDecl)
                {
                    currentEnum = new EnumDefinition(cursor.Spelling);
                    currentHeader.Enums.Add(currentEnum);
                    cursor.VisitChildren(EnumVisitor);
                    currentEnum = null;
                }
                else
                {
                    //Console.WriteLine(cursor.Spelling);
                }
            }
            return Cursor.ChildVisitResult.Continue;
        }

        void readHeader(string headerFile)
        {
            var unsavedFiles = new UnsavedFile[] { };
            currentTU = index.CreateTranslationUnit(headerFile, clangOptions.ToArray(), unsavedFiles, TranslationUnitFlags.SkipFunctionBodies);
            var cur = currentTU.Cursor;
            cur.VisitChildren(HeaderVisitor);
            currentTU.Dispose();
            currentTU = null;
            headerQueue.Remove(headerFile);
        }
    }
}
