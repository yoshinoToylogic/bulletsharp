﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    class PInvokeWriter : WrapperWriter
    {
        bool hasClassSeparatingWhitespace;

        public PInvokeWriter(Dictionary<string, HeaderDefinition> headerDefinitions, string namespaceName)
            : base(headerDefinitions, namespaceName)
        {
        }

        void WriteType(TypeRefDefinition type, bool writeToCs)
        {
            if (type.IsBasic)
            {
                Write(type.Name.Replace("::", "_"), WriteTo.Header);
                Write(type.Name, WriteTo.Source);
            }
            else if (type.HasTemplateTypeParameter)
            {
                
            }
            else if (type.Referenced != null)
            {
                if (type.IsConst || type.Referenced.IsConst)
                {
                    Write("const ", WriteTo.Header | WriteTo.Source);
                }
                string typeName = BulletParser.GetTypeName(type.Referenced) ?? string.Empty;
                Write(typeName + '*', WriteTo.Source);
                Write(typeName.Replace("::", "_") + '*', WriteTo.Header);
            }
            else
            {
                string typeName = BulletParser.GetTypeName(type);
                Write(typeName + '*', WriteTo.Source);
                Write(typeName.Replace("::", "_") + '*', WriteTo.Header);
            }

            if (writeToCs)
            {
                WriteTypeCS(type);
            }
        }

        void WriteTypeCS(TypeRefDefinition type)
        {
            if (type.IsBasic)
            {
                Write(type.ManagedNameCS, WriteTo.CS);
            }
            else if (type.HasTemplateTypeParameter)
            {
                Write(type.ManagedNameCS, WriteTo.CS);
            }
            else if (type.Referenced != null)
            {
                if (type.IsPointer && type.Referenced.ManagedNameCS.Equals("void")) // void*
                {
                    Write("IntPtr", WriteTo.CS);
                }
                else
                {
                    Write(BulletParser.GetTypeNameCS(type), WriteTo.CS);
                }
            }
            else
            {
                Write(BulletParser.GetTypeNameCS(type), WriteTo.CS);
            }
        }

        void OutputDeleteMethod(MethodDefinition method, int level)
        {
            // public void Dispose()
            WriteLine("Dispose()", WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('{', WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine("Dispose(true);", WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine("GC.SuppressFinalize(this);", WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);

            // protected virtual void Dispose(bool disposing)
            WriteLine(WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine("protected virtual void Dispose(bool disposing)", WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine("{", WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine("if (_native != IntPtr.Zero)", WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine('{', WriteTo.CS);
            WriteTabs(level + 3, WriteTo.CS);
            Write(method.Parent.FullNameCS, WriteTo.CS);
            WriteLine("_delete(_native);", WriteTo.CS);
            WriteTabs(level + 3, WriteTo.CS);
            WriteLine("_native = IntPtr.Zero;", WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine('}', WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);

            // C# Destructor
            WriteLine(WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            Write('~', WriteTo.CS);
            Write(method.Parent.ManagedName, WriteTo.CS);
            WriteLine("()", WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('{', WriteTo.CS);
            WriteTabs(level + 2, WriteTo.CS);
            WriteLine("Dispose(false);", WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);
        }

        void OutputMethodDeclaration(MethodDefinition method, int numParameters, int level, int overloadIndex, MethodDefinition returnParamMethod = null)
        {
            // Skip methods wrapped by C# properties
            WriteTo cs = (method.Property == null) ? WriteTo.CS : WriteTo.None;

            WriteTabs(1);
            Write("EXPORT ", WriteTo.Header);

            WriteTabs(level + 1, cs);
            Write("public ", cs);

            // DllImport clause
            WriteTabs(level + 1, WriteTo.Buffer);
            WriteLine("[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]", WriteTo.Buffer);
            WriteTabs(level + 1, WriteTo.Buffer);
            Write("static extern ", WriteTo.Buffer);

            // Return type
            if (method.IsConstructor)
            {
                Write(method.Parent.FullNameCS, WriteTo.Header);
                Write(method.Parent.FullName, WriteTo.Source);
                Write("* ", WriteTo.Header | WriteTo.Source);
                Write("IntPtr ", WriteTo.Buffer);
            }
            else
            {
                var returnType = (returnParamMethod != null) ? returnParamMethod.ReturnType : method.ReturnType;

                if (method.IsStatic)
                {
                    Write("static ", cs);
                }
                WriteType(returnType, cs != 0);
                Write(' ', WriteTo.Header | WriteTo.Source | cs);
                if (method.ReturnType.IsBasic)
                {
                    Write(method.ReturnType.ManagedNameCS, WriteTo.Buffer);
                }
                else if (method.ReturnType.HasTemplateTypeParameter)
                {
                    Write(method.ReturnType.ManagedNameCS, WriteTo.Buffer);
                }
                else if (method.ReturnType.Referenced != null)
                {
                    Write("IntPtr", WriteTo.Buffer);
                }
                else
                {
                    // Return structures to an additional out parameter, not immediately
                    Write("void", WriteTo.Buffer);
                }
                Write(' ', WriteTo.Buffer);
            }

            // Name
            Write(method.Parent.FullNameCS, WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
            Write('_', WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
            if (method.IsConstructor)
            {
                Write("new", WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
                Write(method.Parent.ManagedName, WriteTo.CS);
            }
            else
            {
                Write(method.Name, WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
                if (method.Name.Equals("delete"))
                {
                    OutputDeleteMethod(method, level);
                }
                else
                {
                    Write(method.ManagedName, cs);
                }
            }

            // Index number for overloaded methods
            if (overloadIndex != 0)
            {
                Write((overloadIndex + 1).ToString(), WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
            }


            // Parameters
            if (!method.Name.Equals("delete"))
            {
                Write('(', cs);
            }
            Write('(', WriteTo.Header | WriteTo.Source | WriteTo.Buffer);

            // The first parameter is the instance pointer (if not constructor or static method)
            if (!method.IsConstructor && !method.IsStatic)
            {
                Write(method.Parent.FullNameCS, WriteTo.Header);
                Write(method.Parent.FullName, WriteTo.Source);
                Write("* obj", WriteTo.Header | WriteTo.Source);
                Write("IntPtr obj", WriteTo.Buffer);

                if (numParameters != 0)
                {
                    Write(", ", WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
                }
            }

            for (int i = 0; i < numParameters; i++)
            {
                bool isFinalParameter = (i == numParameters - 1);
                bool isCsFinalParameter = (returnParamMethod != null) ? isFinalParameter : false;
                var param = method.Parameters[i];

                // Parameter type
                if (method.Field != null && method.Field.Type.Referenced == null &&
                    BulletParser.MarshalStructByValue(method.Field.Type) && method.Property.Setter.Name == method.Name)
                {
                    Write("const ", WriteTo.Header | WriteTo.Source);
                }
                if (!isCsFinalParameter)
                {
                    if (param.Type.Referenced != null && !(param.Type.IsConst || param.Type.Referenced.IsConst) &&
                        BulletParser.MarshalStructByValue(param.Type))
                    {
                        Write("out ", cs);
                    }
                }
                WriteType(param.Type, cs != 0 && !isCsFinalParameter);
                Write(BulletParser.GetTypeDllImport(param.Type), WriteTo.Buffer);

                // Parameter name
                if (!isCsFinalParameter)
                {
                    Write(' ', cs);
                    Write(param.ManagedName, cs);
                }
                Write(' ', WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
                Write(param.Name, WriteTo.Header | WriteTo.Source | WriteTo.Buffer);

                if (!isFinalParameter)
                {
                    Write(", ", WriteTo.Header | WriteTo.Source | WriteTo.Buffer);
                }
                if (!(isFinalParameter || (returnParamMethod != null && i == numParameters - 2)))
                {
                    Write(", ", WriteTo.CS);
                }
            }
            WriteLine(");", WriteTo.Header | WriteTo.Buffer);
            if (!method.Name.Equals("delete"))
            {
                WriteLine(')', cs);
            }
            WriteLine(')', WriteTo.Source);
        }

        void OutputMethodDefinition(MethodDefinition method, int numParameters, int overloadIndex, int level, MethodDefinition returnParamMethod)
        {
            // Skip methods wrapped by C# properties
            WriteTo cs = (method.Property == null) ? WriteTo.CS : WriteTo.None;

            // Field marshalling
            if (method.Field != null && method.Field.Type.Referenced == null && BulletParser.MarshalStructByValue(method.Field.Type))
            {
                WriteTabs(1, WriteTo.Source);
                if (method.Property.Getter.Name == method.Name)
                {
                    WriteLine(BulletParser.GetFieldGetterMarshal(method.Parameters[0], method.Field), WriteTo.Source);
                }
                else
                {
                    WriteLine(BulletParser.GetFieldSetterMarshal(method.Parameters[0], method.Field), WriteTo.Source);
                }
                return;
            }

            bool needTypeMarshalEpilogue = false;
            if (!(method.Property != null && BulletParser.MarshalStructByValue(method.Property.Type) && method.Property.Getter.Name == method.Name))
            {
                // Type marshalling prologue
                for (int i = 0; i < numParameters; i++)
                {
                    var param = method.Parameters[i];
                    string prologue = BulletParser.GetTypeMarshalPrologue(param, method);
                    if (!string.IsNullOrEmpty(prologue))
                    {
                        WriteTabs(1, WriteTo.Source);
                        WriteLine(prologue, WriteTo.Source);
                    }

                    // Do we need a type marshalling epilogue?
                    if (!needTypeMarshalEpilogue)
                    {
                        string epilogue = BulletParser.GetTypeMarshalEpilogue(param);
                        if (!string.IsNullOrEmpty(epilogue))
                        {
                            needTypeMarshalEpilogue = true;
                        }
                    }
                }
            }

            WriteTabs(1, WriteTo.Source);

            if (returnParamMethod != null)
            {
                // Temporary variable
                Write(returnParamMethod.ReturnType.ManagedName, cs);
                Write(' ', cs);
                Write(method.Parameters[numParameters - 1].ManagedName, cs);
                WriteLine(';', cs);
                WriteTabs(level + 2, cs);

                Write(BulletParser.GetReturnValueMarshalStart(returnParamMethod.ReturnType), WriteTo.Source);
            }

            if (method.IsConstructor)
            {
                Write("return new ", WriteTo.Source);
                Write(method.Parent.FullName, WriteTo.Source);
                if (method.Parent.BaseClass == null)
                {
                    Write("_native = ", WriteTo.CS);
                }
                Write(method.Parent.FullNameCS, WriteTo.CS);
                Write("_new", WriteTo.CS);
            }
            else
            {
                if (!method.IsVoid)
                {
                    if (needTypeMarshalEpilogue)
                    {
                        // Store return value in a temporary variable
                        Write(BulletParser.GetTypeRefName(method.ReturnType), WriteTo.Source);
                        Write(" ret = ", WriteTo.Source);
                    }
                    else
                    {
                        // Return immediately
                        Write("return ", WriteTo.Source);
                    }

                    Write("return ", cs);

                    if (!method.ReturnType.IsBasic && !method.ReturnType.IsPointer && !method.ReturnType.IsConstantArray)
                    {
                        Write('&', WriteTo.Source);
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
                if (method.Field == null)
                {
                    Write(method.Name, WriteTo.Source);
                }

                Write(method.Parent.FullNameCS, cs);
                Write('_', cs);
                Write(method.Name, cs);
            }
            if (overloadIndex != 0)
            {
                Write((overloadIndex + 1).ToString(), cs);
            }

            // Call parameters
            Write('(', cs);
            if (!method.IsConstructor && !method.IsStatic)
            {
                Write("_native", cs);
                if (numParameters != 0)
                {
                    Write(", ", cs);
                }
            }
            if (method.Field != null)
            {
                Write(method.Field.Name, WriteTo.Source);
            }
            else
            {
                Write('(', WriteTo.Source);
            }
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                bool isFinalParameter = (i == numParameters - 1);
                
                Write(BulletParser.GetTypeCSMarshal(param), cs);

                if (method.Field == null)
                {
                    string marshal = BulletParser.GetTypeMarshal(param);
                    if (string.IsNullOrEmpty(marshal))
                    {
                        if (!param.Type.IsBasic && !param.Type.IsPointer && !param.Type.IsConstantArray)
                        {
                            Write('*', WriteTo.Source);
                        }
                        Write(param.Name, WriteTo.Source);
                    }
                    else
                    {
                        Write(marshal, WriteTo.Source);
                    }
                }

                if (!isFinalParameter)
                {
                    Write(", ", WriteTo.Source | cs);
                }
            }

            if (method.IsConstructor && method.Parent.BaseClass != null)
            {
                WriteLine("))", cs);
                WriteTabs(level + 1, cs);
                WriteLine('{', cs);
            }
            else
            {
                WriteLine(");", cs);
            }

            if (method.Field != null)
            {
                if (method.Property.Setter != null && method.Name.Equals(method.Property.Setter.Name))
                {
                    Write(" = value", WriteTo.Source);
                }
            }
            else
            {
                if (returnParamMethod != null)
                {
                    Write(BulletParser.GetReturnValueMarshalEnd(method.Parameters[numParameters - 1]), WriteTo.Source);
                }
                else
                {
                    Write(")", WriteTo.Source);
                }
            }
            WriteLine(';', WriteTo.Source);

            if (returnParamMethod != null)
            {
                WriteTabs(level + 2, cs);
                Write("return ", cs);
                Write(method.Parameters[numParameters - 1].ManagedName, cs);
                WriteLine(';', cs);
            }

            // Write type marshalling epilogue
            if (needTypeMarshalEpilogue)
            {
                for (int i = 0; i < numParameters; i++)
                {
                    var param = method.Parameters[i];
                    string epilogue = BulletParser.GetTypeMarshalEpilogue(param);
                    if (!string.IsNullOrEmpty(epilogue))
                    {
                        WriteTabs(1, WriteTo.Source);
                        WriteLine(epilogue, WriteTo.Source);
                    }
                }

                if (!method.IsVoid)
                {
                    WriteTabs(1, WriteTo.Source);
                    WriteLine("return ret;", WriteTo.Source);
                }
            }
        }

        void OutputMethod(MethodDefinition method, int level, ref int overloadIndex, int numOptionalParams = 0, MethodDefinition returnParamMethod = null)
        {
            // Can't return whole structures, so append an output parameter
            // referencing the struct that will hold the return value.
            // convert "v method(param)" to "void method(param, &v)"
            if (!method.IsConstructor && !method.ReturnType.IsBasic &&
                !method.ReturnType.IsPointer && BulletParser.MarshalStructByValue(method.ReturnType))
            {
                var method2 = method.Copy();
                var paras = method2.Parameters;
                Array.Resize<ParameterDefinition>(ref paras, paras.Length + 1);
                string paramName;
                if (method.Property != null && method.Property.Setter != null)
                {
                    // Borrow out parameter name from setter
                    paramName = method.Property.Setter.Parameters[0].Name;
                }
                else
                {
                    paramName = "value";
                }
                var valueType = new TypeRefDefinition()
                {
                    IsBasic = false,
                    IsPointer = true,
                    Referenced = method2.ReturnType
                };
                paras[paras.Length - 1] = new ParameterDefinition(paramName, valueType);
                method2.Parameters = paras;
                method2.ReturnType = new TypeRefDefinition();
                OutputMethod(method2, level, ref overloadIndex, numOptionalParams, method);
                return;
            }
            
            EnsureWhiteSpace(WriteTo.Source | WriteTo.CS);

            // Skip methods wrapped by C# properties
            WriteTo propertyTo = WriteTo.Header | WriteTo.Source | ((method.Property == null) ? WriteTo.CS : 0);

            int numOptionalParamsTotal = method.NumOptionalParameters;
            int numParameters = method.Parameters.Length - numOptionalParamsTotal + numOptionalParams;

            OutputMethodDeclaration(method, numParameters, level, overloadIndex, returnParamMethod);

            if (method.Name.Equals("delete"))
            {
                WriteLine('{', WriteTo.Source);
                WriteTabs(1, WriteTo.Source);
                WriteLine("delete obj;", WriteTo.Source);
                WriteLine('}', WriteTo.Source);
                hasSourceWhiteSpace = false;
                hasCSWhiteSpace = false;
                return;
            }

            // Constructor base call
            if (method.IsConstructor && method.Parent.BaseClass != null)
            {
                WriteTabs(level + 2, WriteTo.CS & propertyTo);
                Write(": base(", WriteTo.CS & propertyTo);
            }
            else
            {
                WriteTabs(level + 1, WriteTo.CS & propertyTo);
                WriteLine('{', WriteTo.CS & propertyTo);
                WriteTabs(level + 2, WriteTo.CS & propertyTo);
            }

            // Method body
            WriteLine('{', WriteTo.Source);

            OutputMethodDefinition(method, numParameters, overloadIndex, level, returnParamMethod);

            WriteTabs(level + 1, WriteTo.CS & propertyTo);
            WriteLine('}', WriteTo.Source | WriteTo.CS & propertyTo);
            hasSourceWhiteSpace = false;
            if (method.Property == null)
            {
                hasCSWhiteSpace = false;
            }

            // If there are optional parameters, then output all possible combinations of calls
            overloadIndex++;
            if (numOptionalParams < numOptionalParamsTotal)
            {
                OutputMethod(method, level, ref overloadIndex, numOptionalParams + 1, returnParamMethod);
            }
        }

        void OutputProperty(PropertyDefinition prop, int level)
        {
            EnsureWhiteSpace(WriteTo.CS);

            WriteTabs(level + 1, WriteTo.CS);
            Write("public ", WriteTo.CS);
            WriteTypeCS(prop.Type);
            Write(' ', WriteTo.CS);
            WriteLine(prop.Name, WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('{', WriteTo.CS);

            Write(BulletParser.GetTypeGetterCSMarshal(prop, level), WriteTo.CS);

            if (prop.Setter != null)
            {
                WriteTabs(level + 2, WriteTo.CS);
                Write("set { ", WriteTo.CS);
                Write(prop.Parent.FullNameCS, WriteTo.CS);
                Write('_', WriteTo.CS);
                Write(prop.Setter.Name, WriteTo.CS);
                Write("(_native, ", WriteTo.CS);
                Write(BulletParser.GetTypeSetterCSMarshal(prop.Type), WriteTo.CS);
                WriteLine("); }", WriteTo.CS);
            }

            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);

            hasCSWhiteSpace = false;
        }

        void OutputClass(ClassDefinition c, int level)
        {
            if (BulletParser.IsExcludedClass(c) || c.IsTypedef || c.IsPureEnum)
            {
                return;
            }

            EnsureWhiteSpace(WriteTo.Source | WriteTo.CS);

            // Write class definition
            WriteTabs(level, WriteTo.CS);
            Write("public class ", WriteTo.CS);
            Write(c.ManagedName, WriteTo.CS);
            if (c.BaseClass != null)
            {
                Write(" : ", WriteTo.CS);
                WriteLine(c.BaseClass.ManagedNameCS, WriteTo.CS);
            }
            else
            {
                WriteLine(" : IDisposable", WriteTo.CS);
            }
            WriteTabs(level, WriteTo.CS);
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

            if (!hasClassSeparatingWhitespace)
            {
                headerWriter.WriteLine();
                sourceWriter.WriteLine();
                hasClassSeparatingWhitespace = true;
            }

            // Write native pointer
            if (c.BaseClass == null)
            {
                EnsureWhiteSpace(WriteTo.CS);
                WriteTabs(level + 1, WriteTo.CS);
                WriteLine("internal IntPtr _native;", WriteTo.CS);
                hasCSWhiteSpace = false;
            }

            // Write methods
            int overloadIndex = 0;
            bufferBuilder.Clear();

            // Write C# internal constructor
            EnsureWhiteSpace(WriteTo.CS);
            WriteTabs(level + 1, WriteTo.CS);
            Write("internal ", WriteTo.CS);
            Write(c.ManagedName, WriteTo.CS);
            WriteLine("(IntPtr native)", WriteTo.CS);
            if (c.BaseClass != null)
            {
                WriteTabs(level + 2, WriteTo.CS);
                WriteLine(": base(native)", WriteTo.CS);
            }
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('{', WriteTo.CS);
            if (c.BaseClass == null)
            {
                WriteTabs(level + 2, WriteTo.CS);
                WriteLine("_native = native;", WriteTo.CS);
            }
            WriteTabs(level + 1, WriteTo.CS);
            WriteLine('}', WriteTo.CS);
            hasCSWhiteSpace = false;

            // Write constructors
            bool hasConstructors = false;
            if (!c.IsAbstract)
            {
                foreach (MethodDefinition method in c.Methods)
                {
                    if (method.IsConstructor)
                    {
                        OutputMethod(method, level, ref overloadIndex);
                        hasConstructors = true;
                    }
                }

                // Write default constructor
                if (!hasConstructors && !c.IsAbstract)
                {
                    var constructor = new MethodDefinition(c.Name, c, 0);
                    constructor.IsConstructor = true;
                    OutputMethod(constructor, level, ref overloadIndex);
                }
                overloadIndex = 0;
            }

            // Write methods
            MethodDefinition previousMethod = null;
            foreach (MethodDefinition method in c.Methods.OrderBy(m => m.Name))
            {
                if (method.IsConstructor)
                {
                    continue;
                }

                if (previousMethod != null && previousMethod.Name != method.Name)
                {
                    overloadIndex = 0;
                }

                OutputMethod(method, level, ref overloadIndex);
                previousMethod = method;
            }
            overloadIndex = 0;

            // Write properties
            foreach (PropertyDefinition prop in c.Properties)
            {
                OutputProperty(prop, level);
            }

            // Write delete method
            if (c.BaseClass == null)
            {
                var del = new MethodDefinition("delete", c, 0);
                del.ReturnType = new TypeRefDefinition();
                OutputMethod(del, level, ref overloadIndex);
                c.Methods.Remove(del);
                overloadIndex = 0;
            }

            // Write DllImport clauses
            EnsureWhiteSpace(WriteTo.CS);
            Write(bufferBuilder.ToString(), WriteTo.CS);

            WriteTabs(level, WriteTo.CS);
            WriteLine("}", WriteTo.CS);
            hasCSWhiteSpace = false;
            hasClassSeparatingWhitespace = false;
        }

        public void Output()
        {
            string outDirectoryPInvoke = NamespaceName + "_pinvoke";
            string outDirectoryC = NamespaceName + "_c";

            Directory.CreateDirectory(outDirectoryPInvoke);
            Directory.CreateDirectory(outDirectoryC);

            // C++ header file (includes all other headers)
            string includeFilename = NamespaceName + ".h";
            var includeFile = new FileStream(outDirectoryC + "\\" + includeFilename, FileMode.Create, FileAccess.Write);
            var includeWriter = new StreamWriter(includeFile);

            foreach (var header in headerDefinitions.Values.OrderBy(p => p.Name))
            {
                if (header.Classes.Count == 0)
                {
                    continue;
                }

                // C++ header file
                string headerFilename = header.Name + "_wrap.h";
                var headerFile = new FileStream(outDirectoryC + "\\" + headerFilename, FileMode.Create, FileAccess.Write);
                headerWriter = new StreamWriter(headerFile);
                headerWriter.WriteLine("#include \"main.h\"");
                headerWriter.WriteLine();
                headerWriter.Write("extern \"C\"");
                headerWriter.WriteLine();
                headerWriter.WriteLine("{");

                // C++ source file
                var sourceFile = new FileStream(outDirectoryC + "\\" + header.Name + "_wrap.cpp", FileMode.Create, FileAccess.Write);
                sourceWriter = new StreamWriter(sourceFile);
                sourceWriter.Write("#include \"");
                sourceWriter.Write(headerFilename);
                sourceWriter.WriteLine("\"");

                // C# source file
                var csFile = new FileStream(outDirectoryPInvoke + "\\" + header.ManagedName + ".cs", FileMode.Create, FileAccess.Write);
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
                hasClassSeparatingWhitespace = true;
                foreach (ClassDefinition c in header.Classes)
                {
                    OutputClass(c, 1);
                }
                headerWriter.WriteLine('}');
                csWriter.WriteLine('}');

                // Include header
                includeWriter.Write("#include \"");
                includeWriter.Write(headerFilename);
                includeWriter.WriteLine("\"");

                headerWriter.Dispose();
                headerFile.Dispose();
                sourceWriter.Dispose();
                sourceFile.Dispose();
                csWriter.Dispose();
                csFile.Dispose();
            }

            includeWriter.Dispose();
            includeFile.Dispose();

            Console.WriteLine("Write complete");
        }
    }
}
