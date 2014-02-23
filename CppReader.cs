﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            // WorldImporter include directory
            clangOptions.Add("-I");
            clangOptions.Add(src + "../Extras/Serialize/BulletWorldImporter");

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
                if (header.Contains("GpuSoftBodySolvers") || header.Contains("vectormath"))
                {
                    continue;
                }

                string headerCanonical = header.Replace('\\', '/');
                if (!excludedHeaders.Contains(headerCanonical))
                {
                    headerQueue.Add(headerCanonical);
                }
            }

            Console.Write("Reading headers");

            index = new Index();

            // Parse the common headers
            commonHeaders = new[] { src + "btBulletCollisionCommon.h", src + "btBulletDynamicsCommon.h" };
            foreach (string commonHeader in commonHeaders)
            {
                if (!headerQueue.Contains(commonHeader))
                {
                    Console.WriteLine("Could not find " + commonHeader);
                    return;
                }
                ReadHeader(commonHeader);
            }

            while (headerQueue.Count != 0)
            {
                ReadHeader(headerQueue[0]);
            }

            ReadHeader(src + "..\\Extras\\Serialize\\BulletFileLoader\\btBulletFile.h");
            ReadHeader(src + "..\\Extras\\Serialize\\BulletWorldImporter\\btBulletWorldImporter.h");
            ReadHeader(src + "..\\Extras\\Serialize\\BulletWorldImporter\\btWorldImporter.h");
            ReadHeader(src + "..\\Extras\\Serialize\\BulletXmlWorldImporter\\btBulletXmlWorldImporter.h");
            ReadHeader(src + "..\\Extras\\HACD\\hacdHACD.h");

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
                string relativeFilename = filename.Substring(src.Length);
                currentHeader = new HeaderDefinition(relativeFilename);
                HeaderDefinitions.Add(filename, currentHeader);
                headerQueue.Remove(filename);
            }

            if ((cursor.Kind == CursorKind.ClassDecl || cursor.Kind == CursorKind.StructDecl ||
                cursor.Kind == CursorKind.ClassTemplate || cursor.Kind == CursorKind.TypedefDecl) && cursor.IsDefinition)
            {
                ParseClassCursor(cursor);
            }
            else if (cursor.Kind == CursorKind.EnumDecl)
            {
                currentEnum = new EnumDefinition(cursor.Spelling);
                currentHeader.Enums.Add(currentEnum);
                cursor.VisitChildren(EnumVisitor);
                currentEnum = null;
            }
            else if (cursor.Kind == CursorKind.Namespace)
            {
                return Cursor.ChildVisitResult.Recurse;
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

            AccessSpecifier parentMemberAccess = currentMemberAccess;
            if (currentClass != null)
            {
                currentClass = new ClassDefinition(className, currentClass);
            }
            else
            {
                currentClass = new ClassDefinition(className, currentHeader);
            }
            ClassDefinitions.Add(currentClass.FullNameCS, currentClass);
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

            if (currentClass.Parent != null)
            {
                currentClass.Parent.Classes.Add(currentClass);
            }
            else
            {
                currentHeader.Classes.Add(currentClass);
            }

            if (cursor.Kind == CursorKind.TypedefDecl)
            {
                currentClass.IsTypedef = true;
                if (cursor.TypedefDeclUnderlyingType.Canonical.TypeKind != ClangSharp.Type.Kind.FunctionProto)
                {
                    currentClass.TypedefUnderlyingType = new TypeRefDefinition(cursor.TypedefDeclUnderlyingType);
                }
            }
            else
            {
                cursor.VisitChildren(ClassVisitor);
            }

            // Restore parent state
            currentClass = currentClass.Parent;
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

                    IList<Token> tokens = currentTU.Tokenize(cursor.Extent).ToList();
                    
                    // Check if the return type is const (no clang_isConst available, so look at tokens)
                    if (tokens.Count > 0 && tokens[0].Spelling.Equals("const"))
                    {
                        currentMethod.ReturnType.IsConst = true;
                    }

                    // Check if the method is abstract (no clang_isAbstract available, so look at tokens)
                    if (tokens.Count > 3)
                    {
                        if (tokens[tokens.Count - 3].Spelling.Equals("=") &&
                            tokens[tokens.Count - 2].Spelling.Equals("0") &&
                            tokens[tokens.Count - 1].Spelling.Equals(";"))
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
                            parameterName = "__unnamed" + i;
                        }
                        currentParameter = new ParameterDefinition(parameterName, new TypeRefDefinition(arg.Type));
                        currentMethod.Parameters[i] = currentParameter;
                        arg.VisitChildren(MethodTemplateTypeVisitor);
                        currentParameter = null;

                        // Check if it's a const or optional parameter
                        IEnumerable<Token> argTokens = currentTU.Tokenize(arg.Extent);
                        foreach (Token token in argTokens)
                        {
                            if (token.Spelling.Equals("const"))
                            {
                                currentMethod.Parameters[i].Type.IsConst = true;
                            }
                            else if (token.Spelling.Equals("="))
                            {
                                currentMethod.Parameters[i].IsOptional = true;
                            }
                        }
                    }

                    currentMethod = null;
                }
                else if (cursor.Kind == CursorKind.FieldDecl)
                {
                    currentField = new FieldDefinition(cursor.Spelling,
                        new TypeRefDefinition(cursor.Type), currentClass);
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

        void ReadHeader(string headerFile)
        {
            Console.Write('.');

            var unsavedFiles = new UnsavedFile[] { };
            if (headerFile.EndsWith("PosixThreadSupport.h"))
            {
                List<string> clangOptionsPThread = new List<string>(clangOptions);
                clangOptionsPThread.Add("-DUSE_PTHREADS");
                currentTU = index.CreateTranslationUnit(headerFile, clangOptionsPThread.ToArray(), unsavedFiles, TranslationUnitFlags.SkipFunctionBodies);
            }
            else
            {
                currentTU = index.CreateTranslationUnit(headerFile, clangOptions.ToArray(), unsavedFiles, TranslationUnitFlags.SkipFunctionBodies);
            }
            var cur = currentTU.Cursor;
            cur.VisitChildren(HeaderVisitor);
            currentTU.Dispose();
            currentTU = null;
            headerQueue.Remove(headerFile);
        }
    }
}
