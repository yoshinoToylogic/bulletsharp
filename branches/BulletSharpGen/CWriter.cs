using System;
using System.Collections.Generic;
using System.IO;

namespace BulletSharpGen
{
    class CWriter : WrapperWriter
    {
        public CWriter(Dictionary<string, HeaderDefinition> headerDefinitions, string namespaceName)
            : base(headerDefinitions, namespaceName)
        {
        }

        static string GetFullClassName(ClassDefinition cl)
        {
            string name = cl.Name;
            if (cl.Parent != null)
            {
                return GetFullClassName(cl.Parent) + "_" + name;
            }
            return name;
        }

        void OutputMethod(MethodDefinition method, int level, ref int overloadIndex, int numOptionalParams = 0)
        {
            EnsureWhiteSpace(WriteTo.Source);

            OutputTabs(1);
            Write("EXPORT ", WriteTo.Header);
            OutputTabs(level + 1, WriteTo.CS);
            WriteLine("[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]", WriteTo.CS);
            OutputTabs(level + 1, WriteTo.CS);
            Write("static extern ", WriteTo.CS);

            // Return type
            if (method.IsConstructor)
            {
                Write(method.Parent.FullName, WriteTo.Header | WriteTo.Source);
                Write("* ", WriteTo.Header | WriteTo.Source);
                Write("IntPtr ", WriteTo.CS);
            }
            else
            {
                if (method.ReturnType.IsBasic)
                {
                    Write(method.ReturnType.Name);
                }
                else if (method.ReturnType.Referenced != null)
                {
                    Write(method.ReturnType.Referenced.FullName, WriteTo.Header | WriteTo.Source);
                    Write('*', WriteTo.Header | WriteTo.Source);
                    Write("IntPtr", WriteTo.CS);
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
                Write(method.Parent.FullName, WriteTo.Header | WriteTo.Source);
                Write("* obj", WriteTo.Header | WriteTo.Source);
                Write("IntPtr obj", WriteTo.CS);

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
                    Write(param.Type.Referenced.FullName, WriteTo.Header | WriteTo.Source);
                    Write('*', WriteTo.Header | WriteTo.Source);
                    Write("IntPtr", WriteTo.CS);
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
            WriteLine(");", WriteTo.Header | WriteTo.CS);
            WriteLine(')', WriteTo.Source);
            hasHeaderWhiteSpace = false;

            // Method definition
            WriteLine('{', WriteTo.Source);
            OutputTabs(1, WriteTo.Source);
            if (method.Name == "delete")
            {
                WriteLine("delete obj;", WriteTo.Source);
            }
            else
            {
                if (method.IsConstructor)
                {
                    Write("return new ", WriteTo.Source);
                    Write(method.Parent.FullName, WriteTo.Source);
                }
                else
                {
                    //if (method.ReturnType.IsBasic && method.ReturnType.Name != "void")
                    if (!(method.ReturnType.IsBasic && method.ReturnType.Name == "void"))
                    {
                        if (method.ReturnType.IsBasic || method.ReturnType.Referenced != null)
                        {
                            Write("return ", WriteTo.Source);

                            if (method.ReturnType.IsReference)
                            {
                                Write('&', WriteTo.Source);
                            }
                        }
                    }

                    if (method.IsStatic)
                    {
                        Write(method.Parent.Name, WriteTo.Source);
                        Write("::", WriteTo.Source);
                    }
                    else
                    {
                        Write("obj->", WriteTo.Source);
                    }
                    Write(method.Name, WriteTo.Source);
                }
                Write('(', WriteTo.Source);
                for (int i = 0; i < numParameters; i++)
                {
                    var param = method.Parameters[i];
                    if (param.Type.IsReference)
                    {
                        Write('*', WriteTo.Source);
                    }
                    Write(param.Name, WriteTo.Source);

                    if (param.IsOptional)
                    {
                        hasOptionalParam = true;
                    }

                    if (i != numParameters - 1)
                    {
                        Write(", ", WriteTo.Source);
                    }
                }
                WriteLine(");", WriteTo.Source);
            }
            WriteLine('}', WriteTo.Source);
            //WriteLine(WriteTo.Source);
            hasSourceWhiteSpace = false;

            // If there are optional parameters, then output all possible combinations of calls
            if (hasOptionalParam)
            {
                OutputMethod(method, level, ref overloadIndex, numOptionalParams + 1);
            }
        }

        void OutputClass(ClassDefinition c, int level)
        {
            if (c.IsTypedef)
            {
                return;
            }

            EnsureWhiteSpace(WriteTo.Header | WriteTo.CS);

            // Write class definition
            OutputTabs(level, WriteTo.CS);
            Write("public class ", WriteTo.CS);
            Write(c.ManagedName, WriteTo.CS);
            if (c.BaseClass != null)
            {
                Write(" : ", WriteTo.CS);
                Write(c.BaseClass.ManagedName, WriteTo.CS);
            }
            WriteLine(WriteTo.CS);
            OutputTabs(level, WriteTo.CS);
            WriteLine("{", WriteTo.CS);
            hasCSWhiteSpace = true;

            // Write child classes
            if (c.Classes.Count != 0)
            {
                foreach (ClassDefinition cl in c.Classes)
                {
                    OutputClass(cl, level + 1);
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
                        OutputMethod(method, level, ref overloadIndex);
                        constructorCount++;
                    }
                }

                // Write default constructor
                if (constructorCount == 0 && !c.IsAbstract)
                {
                    MethodDefinition constructor = new MethodDefinition(c.Name, c, 0);
                    constructor.IsConstructor = true;
                    OutputMethod(constructor, level, ref overloadIndex, constructorCount);
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
                OutputMethod(del, level, ref overloadIndex, 0);
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
                        OutputMethod(method, level, ref overloadIndex, 0);
                    }
                    previousMethod = method;
                }
            }

            OutputTabs(level, WriteTo.CS);
            WriteLine("}", WriteTo.CS);
            hasCSWhiteSpace = false;
            hasHeaderWhiteSpace = false;
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

                // Header file
                string headerFilename = header.Name + "_wrap.h";
                FileStream headerFile = new FileStream(outDirectory + "\\" + headerFilename, FileMode.Create, FileAccess.Write);
                headerWriter = new StreamWriter(headerFile);
                headerWriter.WriteLine("#include \"main.h\"");
                headerWriter.WriteLine();
                headerWriter.Write("extern \"C\"");
                headerWriter.WriteLine();
                headerWriter.WriteLine("{");
                hasHeaderWhiteSpace = true;

                // C++ source file
                FileStream sourceFile = new FileStream(outDirectory + "\\" + header.Name + "_wrap.cpp", FileMode.Create, FileAccess.Write);
                sourceWriter = new StreamWriter(sourceFile);
                sourceWriter.Write("#include \"");
                sourceWriter.Write(headerFilename);
                sourceWriter.WriteLine("\"");

                // C# source file
                FileStream csFile = new FileStream(outDirectory + "\\" + header.ManagedName + ".cs", FileMode.Create, FileAccess.Write);
                csWriter = new StreamWriter(csFile);
                csWriter.WriteLine("using System;");
                csWriter.WriteLine("using System.Runtime.InteropServices;");
                csWriter.WriteLine("using System.Security;");
                csWriter.WriteLine();
                csWriter.Write("namespace ");
                csWriter.WriteLine(NamespaceName);
                csWriter.WriteLine("{");
                hasCSWhiteSpace = true;

                // Write classes
                foreach (ClassDefinition c in header.Classes)
                {
                    OutputClass(c, 1);
                }
                headerWriter.WriteLine('}');
                csWriter.WriteLine("}");

                headerWriter.Dispose();
                headerFile.Dispose();
                sourceWriter.Dispose();
                sourceFile.Dispose();
                csWriter.Dispose();
                csFile.Dispose();
            }

            Console.WriteLine("Write complete");
        }
    }
}
