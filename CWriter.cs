using System;
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

        void OutputMethod(MethodDefinition method, ref int overloadIndex, int numOptionalParams = 0)
        {
            EnsureSourceWhiteSpace();

            OutputTabs(1);
            headerWriter.Write("EXPORT ");

            // Return type
            if (method.IsConstructor)
            {
                Write(method.Parent.FullName);
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
                    Write(method.ReturnType.Referenced.FullName);
                    Write('*');
                }
                else
                {
                    // Return structures to an additional out parameter, not immediately
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
            }
            else
            {
                Write(method.Name);
            }
            if (overloadIndex != 0)
            {
                Write((overloadIndex + 1).ToString());
            }
            overloadIndex++;
            Write('(');

            // Parameters
            int numParameters = method.Parameters.Length - numOptionalParams;

            // The first parameter is the instance pointer (if not constructor or static)
            if (!method.IsConstructor && !method.IsStatic)
            {
                Write(method.Parent.FullName);
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
                if (param.Type.Referenced != null && !param.Type.IsBasic)
                {
                    Write(param.Type.Referenced.FullName);
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
            if (method.Name == "delete")
            {
                sourceWriter.WriteLine("delete obj;");
            }
            else
            {
                if (method.IsConstructor)
                {
                    sourceWriter.Write("return new ");
                    sourceWriter.Write(method.Parent.FullName);
                }
                else
                {
                    //if (method.ReturnType.IsBasic && method.ReturnType.Name != "void")
                    if (!(method.ReturnType.IsBasic && method.ReturnType.Name == "void"))
                    {
                        if (method.ReturnType.IsBasic || method.ReturnType.Referenced != null)
                        {
                            sourceWriter.Write("return ");

                            if (method.ReturnType.IsReference)
                            {
                                sourceWriter.Write('&');
                            }
                        }
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
                    sourceWriter.Write(method.Name);
                }
                sourceWriter.Write('(');
                for (int i = 0; i < numParameters; i++)
                {
                    var param = method.Parameters[i];
                    if (param.Type.IsReference)
                    {
                        sourceWriter.Write('*');
                    }
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
            }
            sourceWriter.WriteLine('}');
            //sourceWriter.WriteLine();
            hasSourceWhiteSpace = false;

            // If there are optional parameters, then output all possible combinations of calls
            if (hasOptionalParam)
            {
                OutputMethod(method, ref overloadIndex, numOptionalParams + 1);
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

            // Write methods
            int overloadIndex = 0;

            // Write constructors
            int constructorCount = 0;
            if (!c.IsAbstract)
            {
                foreach (MethodDefinition method in c.Methods)
                {
                    if (method.IsConstructor)
                    {
                        OutputMethod(method, ref overloadIndex);
                        constructorCount++;
                    }
                }

                // Write default constructor
                if (constructorCount == 0 && !c.IsAbstract)
                {
                    MethodDefinition constructor = new MethodDefinition(c.Name, c, 0);
                    constructor.IsConstructor = true;
                    OutputMethod(constructor, ref overloadIndex, constructorCount);
                    constructorCount++;
                }
                overloadIndex = 0;
            }

            // Write delete method
            if (c.BaseClass == null)
            {
                MethodDefinition del = new MethodDefinition("delete", c, 0);
                del.ReturnType = new TypeRefDefinition("void");
                del.ReturnType.IsBasic = true;
                OutputMethod(del, ref overloadIndex, 0);
                c.Methods.Remove(del);
                overloadIndex = 0;
            }

            // Write methods
            if (c.Methods.Count - constructorCount != 0)
            {
                //EnsureWhiteSpace();

                MethodDefinition previousMethod = null;
                foreach (MethodDefinition method in c.Methods)
                {
                    if (previousMethod != null && previousMethod.Name != method.Name)
                    {
                        overloadIndex = 0;
                    }

                    if (!method.IsConstructor)
                    {
                        OutputMethod(method, ref overloadIndex, 0);
                    }
                    previousMethod = method;
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
