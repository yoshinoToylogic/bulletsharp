using System;
using System.Collections.Generic;
using System.IO;

namespace BulletSharpGen
{
    enum RefAccessSpecifier
    {
        Public,
        Private,
        Internal
    }

    class CppWriter
    {
        Dictionary<string, HeaderDefinition> headerDefinitions = new Dictionary<string, HeaderDefinition>();
        StreamWriter headerWriter, sourceWriter;
        bool hasWhiteSpace;
        string namespaceName;

        public CppWriter(Dictionary<string, HeaderDefinition> headerDefinitions, string namespaceName)
        {
            this.headerDefinitions = headerDefinitions;
            this.namespaceName = namespaceName;
        }

        void OutputTabs(int n, bool source = false)
        {
            for (int i = 0; i < n; i++)
            {
                (source ? sourceWriter : headerWriter).Write("\t");
            }
        }

        /// <summary>Writes to both the header and the source file</summary>
        void Write(string s)
        {
            headerWriter.Write(s);
            sourceWriter.Write(s);
        }

        /// <summary>Writes to both the header and the source file</summary>
        void Write(char s)
        {
            headerWriter.Write(s);
            sourceWriter.Write(s);
        }

        void EnsureAccess(int level, ref RefAccessSpecifier current, RefAccessSpecifier required, bool withWhiteSpace = true)
        {
            if (current != required)
            {
                if (withWhiteSpace)
                {
                    EnsureWhiteSpace();
                }

                OutputTabs(level);
                if (required == RefAccessSpecifier.Internal)
                {
                    headerWriter.WriteLine("internal:");
                }
                else if (required == RefAccessSpecifier.Private)
                {
                    headerWriter.WriteLine("private:");
                }
                else if (required == RefAccessSpecifier.Public)
                {
                    headerWriter.WriteLine("public:");
                }
                current = required;
            }
        }

        void EnsureWhiteSpace()
        {
            if (!hasWhiteSpace)
            {
                headerWriter.WriteLine();
                hasWhiteSpace = true;
            }
        }

        void OutputMethod(MethodDefinition method, int level, int numOptionalParams = 0)
        {
            OutputTabs(level + 1);

            // "static"
            if (method.IsStatic)
            {
                headerWriter.Write("static ");
            }

            // Return type
            if (!method.IsConstructor)
            {
                var returnType = method.ReturnType;
                Write(returnType.ManagedTypeRefName);
                Write(' ');
            }

            // Name
            sourceWriter.Write(method.Parent.ManagedName);
            sourceWriter.Write("::");
            if (method.IsConstructor)
            {
                Write(method.Parent.ManagedName);
            }
            else
            {
                Write(method.ManagedName);
            }
            Write('(');

            // Parameters
            int numParameters = method.Parameters.Length - numOptionalParams;
            bool hasOptionalParam = false;
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                Write(param.Type.ManagedTypeRefName);
                Write(' ');
                Write(param.Name);

                if (param.IsOptional)
                {
                    hasOptionalParam = true;
                }

                if (i != numParameters - 1)
                {
                    Write(", ");
                }
            }
            headerWriter.WriteLine(");");
            sourceWriter.WriteLine(')');

            // Method definition
            sourceWriter.WriteLine('{');
            OutputTabs(1, true);
            sourceWriter.Write("_native->");
            sourceWriter.Write(method.Name);
            sourceWriter.Write('(');
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                sourceWriter.Write(param.Name);
                if (!param.Type.IsBasic)
                {
                    sourceWriter.Write("->_native");
                }

                if (param.IsOptional)
                {
                    hasOptionalParam = true;
                }

                if (i != numParameters - 1)
                {
                    sourceWriter.Write(", ");
                }
            }
            sourceWriter.WriteLine(");");
            sourceWriter.WriteLine('}');
            sourceWriter.WriteLine();
            hasWhiteSpace = false;

            // If there are optional parameters, then output all possible combinations of calls
            if (hasOptionalParam)
            {
                OutputMethod(method, level, numOptionalParams + 1);
            }
        }

        void OutputClass(ClassDefinition c, int level)
        {
            if (c.IsTypedef)
            {
                return;
            }

            EnsureWhiteSpace();

            // Write access modifier
            OutputTabs(level);
            if (level == 1)
            {
                headerWriter.Write("public ");
            }

            // Write class definition
            headerWriter.Write("ref class ");
            headerWriter.Write(c.ManagedName);
            if (c.BaseClass != null)
            {
                headerWriter.Write(" : ");
                headerWriter.Write(c.BaseClass.ManagedName);
            }
            headerWriter.WriteLine();
            OutputTabs(level);
            headerWriter.WriteLine("{");
            hasWhiteSpace = true;

            // Default access for ref class
            var currentAccess = RefAccessSpecifier.Private;

            // Write child classes
            if (c.Classes.Count != 0)
            {
                foreach (ClassDefinition cl in c.Classes)
                {
                    if (level != 0)
                    {
                        EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);
                    }
                    OutputClass(cl, level + 1);
                }
                currentAccess = RefAccessSpecifier.Public;
            }

            // Write the unmanaged pointer to the base class
            if (c.BaseClass == null)
            {
                if (c.Classes.Count != 0)
                {
                    headerWriter.WriteLine();
                }
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

                OutputTabs(level + 1);
                headerWriter.Write(c.Name);
                headerWriter.WriteLine("* _native;");
            }

            // TODO: Write constructor from unmanaged pointer only if the class is ever instantiated in this way.

            // Write unmanaged constructor
            EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

            OutputTabs(level + 1);
            sourceWriter.Write(c.ManagedName);
            sourceWriter.Write("::");
            Write(c.ManagedName);
            Write('(');
            Write(c.Name);
            Write("* native)");
            headerWriter.WriteLine(';');
            hasWhiteSpace = false;
            sourceWriter.WriteLine();
            sourceWriter.WriteLine('{');
            OutputTabs(1, true);
            sourceWriter.WriteLine("_native = native;");
            sourceWriter.WriteLine('}');
            sourceWriter.WriteLine();

            // TODO: write destructor & finalizer

            // Write constructors
            int constructorCount = 0;
            foreach (MethodDefinition method in c.Methods)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);

                if (method.IsConstructor)
                {
                    OutputMethod(method, level);
                    constructorCount++;
                }
            }

            // Write default constructor
            if (constructorCount == 0 && !c.IsAbstract)
            {
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Public);

                MethodDefinition constructor = new MethodDefinition(c.Name, c, 0);
                constructor.IsConstructor = true;
                OutputMethod(constructor, level);
                constructorCount++;
            }

            // Write methods
            if (c.Methods.Count - constructorCount != 0)
            {
                EnsureWhiteSpace();

                foreach (MethodDefinition method in c.Methods)
                {
                    if (!method.IsConstructor)
                    {
                        OutputMethod(method, level);
                    }
                }
            }

            // Write properties
            foreach (PropertyDefinition prop in c.Properties)
            {
                EnsureWhiteSpace();

                OutputTabs(level + 1);
                headerWriter.Write("property ");
                headerWriter.Write(prop.Type.ManagedTypeRefName);
                headerWriter.Write(" ");
                headerWriter.WriteLine(prop.Name);
                OutputTabs(level + 1);
                headerWriter.WriteLine("{");
                
                // Getter
                OutputTabs(level + 2);
                headerWriter.Write(prop.Type.ManagedTypeRefName);
                headerWriter.WriteLine(" get();");
                
                // Setter
                if (prop.Setter != null)
                {
                    OutputTabs(level + 2);
                    headerWriter.Write("void set(");
                    headerWriter.Write(prop.Type.ManagedTypeRefName);
                    headerWriter.WriteLine(" value);");
                }

                OutputTabs(level + 1);
                headerWriter.WriteLine("}");

                hasWhiteSpace = false;
            }

            OutputTabs(level);
            headerWriter.WriteLine("};");
            hasWhiteSpace = false;
        }

        public void Output()
        {
            string outDirectory = "src";

            foreach (HeaderDefinition header in headerDefinitions.Values)
            {
                if (header.Classes.Count == 0)
                {
                    continue;
                }

                Directory.CreateDirectory(outDirectory);
                FileStream headerFile = new FileStream(outDirectory + "\\" + header.Name + ".h", FileMode.Create, FileAccess.Write);
                headerWriter = new StreamWriter(headerFile);
                headerWriter.WriteLine("#pragma once");
                headerWriter.WriteLine();

                FileStream sourceFile = new FileStream(outDirectory + "\\" + header.Name + ".cpp", FileMode.Create, FileAccess.Write);
                sourceWriter = new StreamWriter(sourceFile);
                sourceWriter.WriteLine("#include \"StdAfx.h\"");
                sourceWriter.WriteLine();

                // Write includes
                if (header.Includes.Count != 0)
                {
                    foreach (HeaderDefinition include in header.Includes)
                    {
                        headerWriter.Write("#include \"");
                        headerWriter.Write(include.ManagedName);
                        headerWriter.WriteLine(".h\"");
                    }
                    headerWriter.WriteLine();
                }

                // Write namespace
                headerWriter.Write("namespace ");
                headerWriter.WriteLine(namespaceName);
                headerWriter.WriteLine("{");
                hasWhiteSpace = true;

                // Find forward references
                List<ClassDefinition> forwardRefs = new List<ClassDefinition>();
                foreach (ClassDefinition c in header.Classes)
                {
                    FindForwardReferences(forwardRefs, c);
                }
                forwardRefs.Sort((r1, r2) => r1.ManagedName.CompareTo(r2.ManagedName));

                // Write forward references
                List<HeaderDefinition> forwardRefHeaders = new List<HeaderDefinition>();
                foreach (ClassDefinition c in forwardRefs)
                {
                    OutputTabs(1);
                    headerWriter.Write("ref class ");
                    headerWriter.Write(c.ManagedName);
                    headerWriter.WriteLine(";");
                    if (!forwardRefHeaders.Contains(c.Header))
                    {
                        forwardRefHeaders.Add(c.Header);
                    }
                    hasWhiteSpace = false;
                }
                forwardRefHeaders.Add(header);
                forwardRefHeaders.Sort((r1, r2) => r1.ManagedName.CompareTo(r2.ManagedName));

                // Write statements to include forward referenced types
                if (forwardRefHeaders.Count != 0)
                {
                    foreach (HeaderDefinition h in forwardRefHeaders)
                    {
                        sourceWriter.Write("#include \"");
                        sourceWriter.Write(h.ManagedName);
                        sourceWriter.WriteLine(".h\"");
                    }
                    sourceWriter.WriteLine();
                }

                // Write classes
                foreach (ClassDefinition c in header.Classes)
                {
                    OutputClass(c, 1);
                }

                headerWriter.WriteLine("};");
                headerWriter.Dispose();
                headerFile.Dispose();
                sourceWriter.Dispose();
                sourceFile.Dispose();
            }

            Console.WriteLine("Write complete");
        }

        static void AddForwardReference(List<ClassDefinition> forwardRefs, TypeRefDefinition type, HeaderDefinition header)
        {
            if (type.Target != null)
            {
                if (!forwardRefs.Contains(type.Target))
                {
                    if (type.Target.Header != header)
                    {
                        // Forward ref to class in another header
                        forwardRefs.Add(type.Target);
                    }
                }
            }
        }

        static void FindForwardReferences(List<ClassDefinition> forwardRefs, ClassDefinition c)
        {
            foreach (PropertyDefinition prop in c.Properties)
            {
                AddForwardReference(forwardRefs, prop.Type, c.Header);
            }

            foreach (MethodDefinition method in c.Methods)
            {
                AddForwardReference(forwardRefs, method.ReturnType, c.Header);

                foreach (ParameterDefinition param in method.Parameters)
                {
                    AddForwardReference(forwardRefs, param.Type, c.Header);
                }
            }

            foreach (ClassDefinition cl in c.Classes)
            {
                FindForwardReferences(forwardRefs, cl);
            }
        }
    }
}
