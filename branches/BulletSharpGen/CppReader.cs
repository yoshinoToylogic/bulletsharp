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
        Dictionary<string, string> excludedMethods = new Dictionary<string, string>();
        AccessSpecifier currentMemberAccess;
        HeaderDefinition currentHeader;
        ClassDefinition currentClass;
        MethodDefinition currentMethod;
        ParameterDefinition currentParameter;
        EnumDefinition currentEnum;
        FieldDefinition currentField;
        bool currentFieldHasSpecializedParameter;
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

            if ((cursor.Kind == CursorKind.ClassDecl || cursor.Kind == CursorKind.StructDecl ||
                cursor.Kind == CursorKind.ClassTemplate || cursor.Kind == CursorKind.TypedefDecl) && cursor.IsDefinition)
            {
                ParseClassCursor(cursor);
                return Cursor.ChildVisitResult.Continue;
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
            if (ClassDefinitions.ContainsKey(className))
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
            else if (cursor.Kind == CursorKind.StructDecl)
            {
                currentClass.IsStruct = true;
                currentMemberAccess = AccessSpecifier.Public; // default struct access specifier
            }
            else if (cursor.Kind == CursorKind.ClassTemplate)
            {
                currentClass.IsTemplate = true;
                if (cursor.TemplateCursorKind != CursorKind.ClassDecl)
                {
                    currentMemberAccess = AccessSpecifier.Private; // default class access specifier
                }
                else
                {
                    currentMemberAccess = AccessSpecifier.Public; // default struct access specifier
                }
            }

            if (parentClass != null)
            {
                parentClass.Classes.Add(currentClass);
            }
            else
            {
                currentHeader.Classes.Add(currentClass);
            }

            if (cursor.Kind == CursorKind.TypedefDecl)
            {
                currentClass.IsTypedef = true;
                if (cursor.TypedefDeclUnderlyingType.Canonical.Kind != TypeKind.FunctionProto)
                {
                    currentClass.TypedefUnderlyingType = new TypeRefDefinition(cursor.TypedefDeclUnderlyingType);
                }
            }
            else
            {
                cursor.VisitChildren(ClassVisitor);
            }

            // Restore parent state
            currentClass = parentClass;
            currentMemberAccess = parentMemberAccess;
        }

        Cursor.ChildVisitResult MethodTemplateTypeVisitor(Cursor cursor, Cursor parent)
        {
            if (cursor.Kind == CursorKind.TypeRef)
            {
                if (cursor.Referenced.Kind == CursorKind.TemplateTypeParameter)
                {
                    if (currentParameter != null)
                    {
                        currentParameter.Type.HasTemplateTypeParameter = true;
                    }
                    else
                    {
                        currentMethod.ReturnType.HasTemplateTypeParameter = true;
                    }
                    return Cursor.ChildVisitResult.Break;
                }
            }
            else if (cursor.Kind == CursorKind.TemplateRef)
            {
                return Cursor.ChildVisitResult.Recurse;
            }
            return Cursor.ChildVisitResult.Continue;
        }

        Cursor.ChildVisitResult FieldTemplateTypeVisitor(Cursor cursor, Cursor parent)
        {
            if (cursor.Kind == CursorKind.TypeRef)
            {
                if (cursor.Referenced.Kind == CursorKind.TemplateTypeParameter)
                {
                    currentField.Type.HasTemplateTypeParameter = true;
                }
                else if (currentFieldHasSpecializedParameter)
                {
                    currentField.Type.SpecializedTemplateType = new TypeRefDefinition(cursor.Definition.Spelling);
                }
                return Cursor.ChildVisitResult.Break;
            }
            else if (cursor.Kind == CursorKind.TemplateRef)
            {
                if (!parent.Type.Declaration.SpecializedCursorTemplate.IsInvalid)
                {
                    currentFieldHasSpecializedParameter = true;
                    return Cursor.ChildVisitResult.Continue;
                }
                else if (parent.Type.Declaration.Kind == CursorKind.ClassTemplate)
                {
                    currentField.Type.HasTemplateTypeParameter = true;
                    return Cursor.ChildVisitResult.Break;
                }
            }
            return Cursor.ChildVisitResult.Continue;
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
                if ((cursor.Kind == CursorKind.ClassDecl || cursor.Kind == CursorKind.StructDecl ||
                    cursor.Kind == CursorKind.ClassTemplate || cursor.Kind == CursorKind.TypedefDecl) && cursor.IsDefinition)
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

                    // Check if the return type is a template
                    cursor.VisitChildren(MethodTemplateTypeVisitor);

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
                        currentParameter = new ParameterDefinition(parameterName, new TypeRefDefinition(arg.Type));
                        currentMethod.Parameters[i] = currentParameter;
                        arg.VisitChildren(MethodTemplateTypeVisitor);
                        currentParameter = null;

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
                    currentField = new FieldDefinition(cursor.Spelling, currentClass);
                    currentField.Type = new TypeRefDefinition(cursor.Type);
                    currentFieldHasSpecializedParameter = false;
                    cursor.VisitChildren(FieldTemplateTypeVisitor);
                    currentField = null;
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
