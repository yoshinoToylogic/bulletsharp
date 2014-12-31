﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    class ExtensionsWriter : WrapperWriter
    {
        Dictionary<string, string> _extensionClassesInternal = new Dictionary<string, string>();
        Dictionary<string, string> _extensionClassesExternal = new Dictionary<string, string>();
        Dictionary<string, string> _returnTypeConversion = new Dictionary<string, string>();

        List<KeyValuePair<string, string>> _extensionMethods = new List<KeyValuePair<string, string>>();

        public ExtensionsWriter(IEnumerable<HeaderDefinition> headerDefinitions, string namespaceName)
            : base(headerDefinitions, namespaceName)
        {
            _extensionClassesInternal.Add("Matrix3x3", "BulletSharp.Math.Matrix");
            _extensionClassesInternal.Add("Quaternion", "BulletSharp.Math.Quaternion");
            _extensionClassesInternal.Add("Transform", "BulletSharp.Math.Matrix");
            _extensionClassesInternal.Add("Vector3", "BulletSharp.Math.Vector3");

            _extensionClassesExternal.Add("Matrix3x3", "OpenTK.Matrix4");
            _extensionClassesExternal.Add("Quaternion", "OpenTK.Quaternion");
            _extensionClassesExternal.Add("Transform", "OpenTK.Matrix4");
            _extensionClassesExternal.Add("Vector3", "OpenTK.Vector3");

            _returnTypeConversion.Add("Matrix3x3", ".ToOpenTK()");
            _returnTypeConversion.Add("Quaternion", ".ToOpenTK()");
            _returnTypeConversion.Add("Transform", ".ToOpenTK()");
            _returnTypeConversion.Add("Vector3", ".ToOpenTK()");
        }

        bool MethodNeedsExtensions(MethodDefinition method)
        {
            // Extension constructors & static extension methods not supported
            if (method.IsConstructor || method.IsStatic)
            {
                return false;
            }

            foreach (var param in method.Parameters)
            {
                if (_extensionClassesInternal.ContainsKey(param.Type.ManagedName))
                {
                    return true;
                }
            }
            return false;
        }

        bool ClassNeedsExtensions(ClassDefinition c)
        {
            foreach (var prop in c.Properties)
            {
                if (_extensionClassesInternal.ContainsKey(prop.Type.ManagedName))
                {
                    return true;
                }
            }

            foreach (var method in c.Methods)
            {
                if (MethodNeedsExtensions(method))
                {
                    return true;
                }
            }
            return false;
        }

        public void Output()
        {
            string outDirectory = NamespaceName + "_extensions";

            foreach (HeaderDefinition header in headerDefinitions)
            {
                bool headerNeedsExtensions = false;
                foreach (var c in header.Classes)
                {
                    if (ClassNeedsExtensions(c))
                    {
                        headerNeedsExtensions = true;
                        break;
                    }
                }

                if (!headerNeedsExtensions)
                {
                    continue;
                }

                Directory.CreateDirectory(outDirectory);

                // C# extensions file
                var csFile = new FileStream(outDirectory + "\\" + header.ManagedName + "Extensions.cs", FileMode.Create, FileAccess.Write);
                csWriter = new StreamWriter(csFile);
                csWriter.WriteLine("using System.ComponentModel;");
                csWriter.WriteLine();
                csWriter.Write("namespace ");
                csWriter.WriteLine(NamespaceName);
                csWriter.WriteLine('{');
                hasCSWhiteSpace = true;

                foreach (var c in header.Classes)
                {
                    WriteClass(c);
                }

                csWriter.WriteLine("}");

                csWriter.Dispose();
                csFile.Dispose();
            }
        }

        private void WriteClass(ClassDefinition c)
        {
            foreach (var child in c.Classes)
            {
                WriteClass(child);
            }

            if (!ClassNeedsExtensions(c))
            {
                return;
            }

            _extensionMethods.Clear();

            EnsureWhiteSpace(WriteTo.CS);
            WriteTabs(1, WriteTo.CS);
            csWriter.WriteLine("[EditorBrowsable(EditorBrowsableState.Never)]");
            WriteTabs(1, WriteTo.CS);
            csWriter.Write("public static class ");
            csWriter.Write(c.ManagedName);
            csWriter.WriteLine("Extensions");
            WriteTabs(1, WriteTo.CS);
            csWriter.WriteLine('{');

            foreach (var prop in c.Properties)
            {
                if (_extensionClassesInternal.ContainsKey(prop.Type.ManagedName))
                {
                    // Getter with out parameter
                    bufferBuilder.Clear();
                    WriteTabs(2, WriteTo.Buffer);
                    Write("public unsafe static void Get", WriteTo.Buffer);
                    Write(prop.Name, WriteTo.Buffer);
                    Write("(this ", WriteTo.Buffer);
                    Write(c.ManagedName, WriteTo.Buffer);
                    Write(" obj, out ", WriteTo.Buffer);
                    Write(_extensionClassesExternal[prop.Type.ManagedName], WriteTo.Buffer);
                    WriteLine(" value)", WriteTo.Buffer);
                    WriteTabs(2, WriteTo.Buffer);
                    WriteLine('{', WriteTo.Buffer);

                    WriteTabs(3, WriteTo.Buffer);
                    Write("fixed (", WriteTo.Buffer);
                    Write(_extensionClassesExternal[prop.Type.ManagedName], WriteTo.Buffer);
                    WriteLine("* valuePtr = &value)", WriteTo.Buffer);
                    WriteTabs(3, WriteTo.Buffer);
                    WriteLine('{', WriteTo.Buffer);
                    WriteTabs(4, WriteTo.Buffer);
                    Write("*(", WriteTo.Buffer);
                    Write(_extensionClassesInternal[prop.Type.ManagedName], WriteTo.Buffer);
                    Write("*)valuePtr = obj.", WriteTo.Buffer);
                    Write(prop.Name, WriteTo.Buffer);
                    WriteLine(';', WriteTo.Buffer);
                    WriteTabs(3, WriteTo.Buffer);
                    WriteLine('}', WriteTo.Buffer);

                    WriteTabs(2, WriteTo.Buffer);
                    WriteLine('}', WriteTo.Buffer);

                    _extensionMethods.Add(new KeyValuePair<string, string>("Get" + prop.Name, bufferBuilder.ToString()));

                    // Getter with return value
                    bufferBuilder.Clear();
                    WriteTabs(2, WriteTo.Buffer);
                    Write("public static ", WriteTo.Buffer);
                    Write(_extensionClassesExternal[prop.Type.ManagedName], WriteTo.Buffer);
                    Write(" Get", WriteTo.Buffer);
                    Write(prop.Name, WriteTo.Buffer);
                    Write("(this ", WriteTo.Buffer);
                    Write(c.ManagedName, WriteTo.Buffer);
                    WriteLine(" obj)", WriteTo.Buffer);
                    WriteTabs(2, WriteTo.Buffer);
                    WriteLine('{', WriteTo.Buffer);

                    WriteTabs(3, WriteTo.Buffer);
                    Write(_extensionClassesExternal[prop.Type.ManagedName], WriteTo.Buffer);
                    WriteLine(" value;", WriteTo.Buffer);
                    WriteTabs(3, WriteTo.Buffer);
                    Write("Get", WriteTo.Buffer);
                    Write(prop.Name, WriteTo.Buffer);
                    WriteLine("(obj, out value);", WriteTo.Buffer);
                    WriteTabs(3, WriteTo.Buffer);
                    WriteLine("return value;", WriteTo.Buffer);

                    WriteTabs(2, WriteTo.Buffer);
                    WriteLine('}', WriteTo.Buffer);

                    _extensionMethods.Add(new KeyValuePair<string, string>("Get" + prop.Name, bufferBuilder.ToString()));

                    if (prop.Setter == null)
                    {
                        continue;
                    }

                    // Setter with ref parameter
                    bufferBuilder.Clear();
                    WriteTabs(2, WriteTo.Buffer);
                    Write("public unsafe static void Set", WriteTo.Buffer);
                    Write(prop.Name, WriteTo.Buffer);
                    Write("(this ", WriteTo.Buffer);
                    Write(c.ManagedName, WriteTo.Buffer);
                    Write(" obj, ref ", WriteTo.Buffer);
                    Write(_extensionClassesExternal[prop.Type.ManagedName], WriteTo.Buffer);
                    WriteLine(" value)", WriteTo.Buffer);
                    WriteTabs(2, WriteTo.Buffer);
                    WriteLine('{', WriteTo.Buffer);

                    WriteTabs(3, WriteTo.Buffer);
                    Write("fixed (", WriteTo.Buffer);
                    Write(_extensionClassesExternal[prop.Type.ManagedName], WriteTo.Buffer);
                    WriteLine("* valuePtr = &value)", WriteTo.Buffer);
                    WriteTabs(3, WriteTo.Buffer);
                    WriteLine('{', WriteTo.Buffer);
                    WriteTabs(4, WriteTo.Buffer);
                    Write("obj.", WriteTo.Buffer);
                    Write(prop.Name, WriteTo.Buffer);
                    Write(" = *(", WriteTo.Buffer);
                    Write(_extensionClassesInternal[prop.Type.ManagedName], WriteTo.Buffer);
                    WriteLine("*)valuePtr;", WriteTo.Buffer);
                    WriteTabs(3, WriteTo.Buffer);
                    WriteLine('}', WriteTo.Buffer);

                    WriteTabs(2, WriteTo.Buffer);
                    WriteLine('}', WriteTo.Buffer);

                    _extensionMethods.Add(new KeyValuePair<string, string>("Set" + prop.Name, bufferBuilder.ToString()));

                    // Setter with non-ref parameter
                    bufferBuilder.Clear();
                    WriteTabs(2, WriteTo.Buffer);
                    Write("public static void Set", WriteTo.Buffer);
                    Write(prop.Name, WriteTo.Buffer);
                    Write("(this ", WriteTo.Buffer);
                    Write(c.ManagedName, WriteTo.Buffer);
                    Write(" obj, ", WriteTo.Buffer);
                    Write(_extensionClassesExternal[prop.Type.ManagedName], WriteTo.Buffer);
                    WriteLine(" value)", WriteTo.Buffer);
                    WriteTabs(2, WriteTo.Buffer);
                    WriteLine('{', WriteTo.Buffer);

                    WriteTabs(3, WriteTo.Buffer);
                    Write("Set", WriteTo.Buffer);
                    Write(prop.Name, WriteTo.Buffer);
                    WriteLine("(obj, ref value);", WriteTo.Buffer);

                    WriteTabs(2, WriteTo.Buffer);
                    WriteLine('}', WriteTo.Buffer);

                    _extensionMethods.Add(new KeyValuePair<string, string>("Set" + prop.Name, bufferBuilder.ToString()));
                }
            }

            foreach (var method in c.Methods)
            {
                if (!MethodNeedsExtensions(method))
                {
                    continue;
                }

                WriteMethod(method);
            }

            foreach (KeyValuePair<string, string> method in _extensionMethods.OrderBy(key => key.Key))
            {
                EnsureWhiteSpace(WriteTo.CS);
                csWriter.Write(method.Value);
                hasCSWhiteSpace = false;
            }

            WriteTabs(1, WriteTo.CS);
            csWriter.WriteLine('}');
            hasCSWhiteSpace = false;
        }

        private void WriteMethod(MethodDefinition method, int numOptionalParams = 0)
        {
            bool convertReturnType = _extensionClassesInternal.ContainsKey(method.ReturnType.ManagedName);

            bufferBuilder.Clear();
            WriteTabs(2, WriteTo.Buffer);
            Write("public unsafe static ", WriteTo.Buffer);
            if (convertReturnType)
            {
                Write(_extensionClassesExternal[method.ReturnType.ManagedName], WriteTo.Buffer);
            }
            else
            {
                Write(method.ReturnType.ManagedName, WriteTo.Buffer);
            }
            Write(' ', WriteTo.Buffer);
            Write(method.ManagedName, WriteTo.Buffer);
            Write("(this ", WriteTo.Buffer);
            Write(method.Parent.ManagedName, WriteTo.Buffer);
            Write(" obj", WriteTo.Buffer);

            List<ParameterDefinition> extendedParams = new List<ParameterDefinition>();
            int numParameters = method.Parameters.Length - numOptionalParams;
            for (int i = 0; i < numParameters; i++)
            {
                Write(", ", WriteTo.Buffer);

                var param = method.Parameters[i];
                if (_extensionClassesInternal.ContainsKey(param.Type.ManagedName))
                {
                    Write("ref ", WriteTo.Buffer);
                    Write(_extensionClassesExternal[param.Type.ManagedName], WriteTo.Buffer);
                    Write(' ', WriteTo.Buffer);
                    Write(param.Name, WriteTo.Buffer);
                    extendedParams.Add(param);
                }
                else
                {
                    Write(param.Type.ManagedName, WriteTo.Buffer);
                    Write(' ', WriteTo.Buffer);
                    Write(param.Name, WriteTo.Buffer);
                }
            }
            WriteLine(')', WriteTo.Buffer);

            WriteTabs(2, WriteTo.Buffer);
            WriteLine('{', WriteTo.Buffer);

            // Fix parameter pointers
            int tabs = 3;
            foreach (var param in extendedParams)
            {
                WriteTabs(tabs, WriteTo.Buffer);
                Write("fixed (", WriteTo.Buffer);
                Write(_extensionClassesExternal[param.Type.ManagedName], WriteTo.Buffer);
                Write("* ", WriteTo.Buffer);
                Write(param.Name, WriteTo.Buffer);
                Write("Ptr = &", WriteTo.Buffer);
                Write(param.Name, WriteTo.Buffer);
                WriteLine(')', WriteTo.Buffer);
                WriteTabs(tabs, WriteTo.Buffer);
                WriteLine('{', WriteTo.Buffer);
                tabs++;
            }

            WriteTabs(tabs, WriteTo.Buffer);
            if (method.ReturnType.Name != "void")
            {
                Write("return ", WriteTo.Buffer);
            }
            Write("obj.", WriteTo.Buffer);
            Write(method.ManagedName, WriteTo.Buffer);
            Write('(', WriteTo.Buffer);
            bool hasOptionalParam = false;
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                if (_extensionClassesInternal.ContainsKey(param.Type.ManagedName))
                {
                    Write("ref *(", WriteTo.Buffer);
                    Write(_extensionClassesInternal[param.Type.ManagedName], WriteTo.Buffer);
                    Write("*)", WriteTo.Buffer);
                    Write(param.Name, WriteTo.Buffer);
                    Write("Ptr", WriteTo.Buffer);
                }
                else
                {
                    Write(param.Name, WriteTo.Buffer);
                }

                if (param.IsOptional)
                {
                    hasOptionalParam = true;
                }

                if (i != numParameters - 1)
                {
                    Write(", ", WriteTo.Buffer);
                }
            }
            Write(')', WriteTo.Buffer);
            if (convertReturnType)
            {
                Write(_returnTypeConversion[method.ReturnType.ManagedName], WriteTo.Buffer);
            }
            WriteLine(';', WriteTo.Buffer);

            // Close fixed blocks
            while (tabs != 2)
            {
                tabs--;
                WriteTabs(tabs, WriteTo.Buffer);
                WriteLine('}', WriteTo.Buffer);
            }

            _extensionMethods.Add(new KeyValuePair<string, string>(method.Name, bufferBuilder.ToString()));

            if (hasOptionalParam)
            {
                WriteMethod(method, numOptionalParams + 1);
            }
        }
    }
}
