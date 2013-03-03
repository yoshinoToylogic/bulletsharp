﻿using System;
using System.Collections.Generic;
using System.IO;

namespace BulletSharpGen
{
    class CWriter
    {
        Dictionary<string, HeaderDefinition> headerDefinitions = new Dictionary<string, HeaderDefinition>();
        StreamWriter headerWriter, sourceWriter;
        bool hasWhiteSpace;
        bool hasSourceWhiteSpace;
        string namespaceName;

        public CWriter(Dictionary<string, HeaderDefinition> headerDefinitions, string namespaceName)
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

        void EnsureWhiteSpace()
        {
            if (!hasWhiteSpace)
            {
                headerWriter.WriteLine();
                hasWhiteSpace = true;
            }
        }

        void EnsureSourceWhiteSpace()
        {
            if (!hasSourceWhiteSpace)
            {
                sourceWriter.WriteLine();
                hasSourceWhiteSpace = true;
            }
        }

        string GetFullClassName(ClassDefinition cl)
        {
            string name = cl.Name;
            if (cl.Parent != null)
            {
                return GetFullClassName(cl.Parent) + "_" + name;
            }
            return name;
        }

        void OutputMethod(MethodDefinition method, ref int constructorIndex, int numOptionalParams = 0)
        {
            EnsureSourceWhiteSpace();

            OutputTabs(1);
            headerWriter.Write("EXPORT ");

            // Return type
            if (method.IsConstructor)
            {
                Write(method.Name);
                Write("* ");
            }
            else
            {
                if (method.ReturnType.IsBasic)
                {
                    Write(method.ReturnType.Name);
                }
                else if (method.ReturnType.Referenced != null)
                {
                    Write(method.ReturnType.Referenced.Name);
                    Write('*');
                }
                else
                {
                    // Return structs to an additional out parameter, not immediately
                    Write("void");
                }
                Write(' ');
            }

            // Name
            Write(GetFullClassName(method.Parent));
            Write("_");
            if (method.IsConstructor)
            {
                Write("new");
                if (constructorIndex != 0)
                {
                    Write((constructorIndex + 1).ToString());
                }
                constructorIndex++;
            }
            else
            {
                Write(method.Name);
            }
            Write('(');

            // Parameters
            int numParameters = method.Parameters.Length - numOptionalParams;

            // The first parameter is the instance pointer (if not constructor or static)
            if (!method.IsConstructor && !method.IsStatic)
            {
                Write(method.Parent.Name);
                Write("* obj");

                if (numParameters != 0)
                {
                    Write(", ");
                }
            }

            bool hasOptionalParam = false;
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                //Write(param.Type.ManagedTypeRefName);
                if (param.Type.Referenced != null && !param.Type.IsBasic)
                {
                    Write(param.Type.Referenced.Name);
                    Write("*");
                }
                else
                {
                    Write(param.Type.Name);
                }
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
            hasWhiteSpace = false;

            // Method definition
            sourceWriter.WriteLine('{');
            OutputTabs(1, true);
            if (method.IsConstructor)
            {
                sourceWriter.Write("return new ");
            }
            else
            {
                if (method.ReturnType.IsBasic && method.ReturnType.Name != "void")
                {
                    sourceWriter.Write("return ");
                }
                if (method.IsStatic)
                {
                    sourceWriter.Write(method.Parent.Name);
                    sourceWriter.Write("::");
                }
                else
                {
                    sourceWriter.Write("obj->");
                }
            }
            sourceWriter.Write(method.Name);
            sourceWriter.Write('(');
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                sourceWriter.Write(param.Name);

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
            //sourceWriter.WriteLine();
            hasSourceWhiteSpace = false;

            // If there are optional parameters, then output all possible combinations of calls
            if (hasOptionalParam)
            {
                OutputMethod(method, ref constructorIndex, numOptionalParams + 1);
            }
        }

        void OutputClass(ClassDefinition c)
        {
            if (c.IsTypedef)
            {
                return;
            }

            EnsureWhiteSpace();

            // Write child classes
            if (c.Classes.Count != 0)
            {
                foreach (ClassDefinition cl in c.Classes)
                {
                    OutputClass(cl);
                }
            }

            // TODO: Write constructor from unmanaged pointer only if the class is ever instantiated in this way.

            // TODO: write destructor & finalizer

            // Write constructors
            int constructorCount = 0;
            int constructorIndex = 0;
            foreach (MethodDefinition method in c.Methods)
            {
                if (method.IsConstructor)
                {
                    OutputMethod(method, ref constructorIndex);
                    constructorCount++;
                }
            }

            // Write default constructor
            if (constructorCount == 0 && !c.IsAbstract)
            {
                MethodDefinition constructor = new MethodDefinition(c.Name, c, 0);
                constructor.IsConstructor = true;
                OutputMethod(constructor, ref constructorIndex, constructorCount);
                constructorCount++;
            }

            // Write methods
            if (c.Methods.Count - constructorCount != 0)
            {
                //EnsureWhiteSpace();

                foreach (MethodDefinition method in c.Methods)
                {
                    if (!method.IsConstructor)
                    {
                        int dummy = 0;
                        OutputMethod(method, ref dummy, 0);
                    }
                }
            }

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
                string headerFilename = header.Name + "_wrap.h";
                FileStream headerFile = new FileStream(outDirectory + "\\" + headerFilename, FileMode.Create, FileAccess.Write);
                headerWriter = new StreamWriter(headerFile);
                headerWriter.WriteLine("#include \"main.h\"");
                headerWriter.WriteLine();

                FileStream sourceFile = new FileStream(outDirectory + "\\" + header.Name + "_wrap.cpp", FileMode.Create, FileAccess.Write);
                sourceWriter = new StreamWriter(sourceFile);
                sourceWriter.Write("#include \"");
                sourceWriter.Write(headerFilename);
                sourceWriter.WriteLine("\"");

                // Write namespace
                headerWriter.Write("extern \"C\"");
                headerWriter.WriteLine();
                headerWriter.WriteLine("{");
                hasWhiteSpace = true;

                // Write classes
                foreach (ClassDefinition c in header.Classes)
                {
                    OutputClass(c);
                }

                headerWriter.WriteLine('}');
                headerWriter.Dispose();
                headerFile.Dispose();
                sourceWriter.Dispose();
                sourceFile.Dispose();
            }

            Console.WriteLine("Write complete");
        }
    }
}
