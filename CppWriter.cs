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
        StreamWriter writer;
        bool hasWhiteSpace;
        string namespaceName;

        public CppWriter(Dictionary<string, HeaderDefinition> headerDefinitions, string namespaceName)
        {
            this.headerDefinitions = headerDefinitions;
            this.namespaceName = namespaceName;
        }

        void OutputTabs(int n)
        {
            for (int i = 0; i < n; i++)
            {
                writer.Write("\t");
            }
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
                    writer.WriteLine("internal:");
                }
                else if (required == RefAccessSpecifier.Private)
                {
                    writer.WriteLine("private:");
                }
                else if (required == RefAccessSpecifier.Public)
                {
                    writer.WriteLine("public:");
                }
                current = required;
            }
        }

        void EnsureWhiteSpace()
        {
            if (!hasWhiteSpace)
            {
                writer.WriteLine();
                hasWhiteSpace = true;
            }
        }

        void OutputMethod(MethodDefinition method, int level, int numOptionalParams = 0)
        {
            OutputTabs(level + 1);
            if (method.IsStatic)
            {
                writer.Write("static ");
            }
            if (method.IsConstructor)
            {
                writer.Write(method.Parent.ManagedName);
            }
            else
            {
                var returnType = method.ReturnType;
                writer.Write(returnType.ManagedTypeRefName);
                writer.Write(' ');
                string name = method.Name;
                name = name.Substring(0, 1).ToUpper() + name.Substring(1);
                writer.Write(name);
            }
            writer.Write('(');

            // Write parameters
            int numParameters = method.Parameters.Length - numOptionalParams;
            bool hasOptionalParam = false;
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                writer.Write(param.Type.ManagedTypeRefName);
                writer.Write(' ');
                writer.Write(param.Name);

                if (param.IsOptional)
                {
                    hasOptionalParam = true;
                }

                if (i != numParameters - 1)
                {
                    writer.Write(", ");
                }
            }
            writer.WriteLine(");");
            hasWhiteSpace = false;

            // If there are optional parameters, then output all possible combinations of calls
            if (hasOptionalParam)
            {
                OutputMethod(method, level, numOptionalParams + 1);
            }
        }

        void OutputClass(ClassDefinition c, int level)
        {
            // TODO: Write forward references

            hasWhiteSpace = true;

            // Write access modifier
            OutputTabs(level);
            if (level == 1)
            {
                writer.Write("public ");
            }

            // Write class definition
            writer.Write("ref class ");
            writer.Write(c.ManagedName);
            if (c.BaseClass != null)
            {
                writer.Write(" : ");
                writer.Write(c.BaseClass.ManagedName);
            }
            writer.WriteLine();
            OutputTabs(level);
            writer.WriteLine("{");

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

                    if (cl != c.Classes[c.Classes.Count - 1])
                    {
                        writer.WriteLine();
                    }
                }
                currentAccess = RefAccessSpecifier.Public;
            }

            // Write the unmanaged pointer to the base class
            if (c.BaseClass == null)
            {
                if (c.Classes.Count != 0)
                {
                    writer.WriteLine();
                }
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

                OutputTabs(level + 1);
                writer.Write(c.Name);
                writer.WriteLine("* _native;");
            }

            // TODO: Write constructor from unmanaged pointer only if the class is ever instantiated in this way.

            // Write unmanaged constructor
            EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

            OutputTabs(level + 1);
            writer.Write(c.ManagedName);
            writer.Write('(');
            writer.Write(c.Name);
            writer.WriteLine("* native);");
            hasWhiteSpace = false;

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
                writer.Write("property ");
                writer.Write(prop.Type.ManagedTypeRefName);
                writer.Write(" ");
                writer.WriteLine(prop.Name);
                OutputTabs(level + 1);
                writer.WriteLine("{");
                
                // Getter
                OutputTabs(level + 2);
                writer.Write(prop.Type.ManagedTypeRefName);
                writer.WriteLine(" get();");
                
                // Setter
                if (prop.Setter != null || prop.Field != null)
                {
                    OutputTabs(level + 2);
                    writer.Write("void set(");
                    writer.Write(prop.Type.ManagedTypeRefName);
                    writer.WriteLine(" value);");
                }

                OutputTabs(level + 1);
                writer.WriteLine("}");

                hasWhiteSpace = false;
            }

            OutputTabs(level);
            writer.WriteLine("};");
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
                FileStream f = new FileStream(outDirectory + "\\" + header.Name + ".h", FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(f);
                //s.WriteLine("#include \"StdAfx.h\"");
                writer.WriteLine("#pragma once");
                writer.WriteLine();

                // Write includes
                if (header.Includes.Count != 0)
                {
                    foreach (HeaderDefinition include in header.Includes)
                    {
                        writer.Write("#include \"");
                        writer.Write(include.Name);
                        writer.WriteLine(".h\"");
                    }
                    writer.WriteLine();
                }

                // Write classes
                writer.Write("namespace ");
                writer.WriteLine(namespaceName);
                writer.WriteLine("{");

                // Find forward references
                List<ClassDefinition> forwardRefs = new List<ClassDefinition>();
                foreach (ClassDefinition c in header.Classes)
                {
                    FindForwardReferences(forwardRefs, c);
                }
                forwardRefs.Sort((r1, r2) => r1.ManagedName.CompareTo(r2.ManagedName));

                // Write forward references
                foreach (ClassDefinition c in forwardRefs)
                {
                    OutputTabs(1);
                    writer.Write("ref class ");
                    writer.Write(c.ManagedName);
                    writer.WriteLine(";");
                }

                foreach (ClassDefinition c in header.Classes)
                {
                    EnsureWhiteSpace();
                    OutputClass(c, 1);

                    if (c != header.Classes[header.Classes.Count - 1])
                    {
                        writer.WriteLine();
                    }
                }

                writer.WriteLine("};");
                writer.Dispose();
                f.Dispose();
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
