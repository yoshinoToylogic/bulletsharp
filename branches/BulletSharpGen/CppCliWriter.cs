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

    class CppCliWriter
    {
        Dictionary<string, HeaderDefinition> headerDefinitions = new Dictionary<string, HeaderDefinition>();
        StreamWriter headerWriter, sourceWriter;
        bool hasHeaderWhiteSpace;
        bool hasSourceWhiteSpace;
        string namespaceName;

        int _headerLineLength;
        int _sourceLineLength;
        
        const int TabWidth = 4;
        const int LineBreakWidth = 80;

        public CppCliWriter(Dictionary<string, HeaderDefinition> headerDefinitions, string namespaceName)
        {
            this.headerDefinitions = headerDefinitions;
            this.namespaceName = namespaceName;
        }

        void WriteTabs(int n, bool source = false)
        {
            for (int i = 0; i < n; i++)
            {
                if (source)
                {
                    sourceWriter.Write('\t');
                    _sourceLineLength += TabWidth;
                }
                else
                {
                    headerWriter.Write('\t');
                    _headerLineLength += TabWidth;
                }
            }
        }

        void HeaderWrite(string s)
        {
            headerWriter.Write(s);
            _headerLineLength += s.Length;
        }

        void HeaderWriteLine(string s = "")
        {
            HeaderWrite(s);
            headerWriter.WriteLine();
            _headerLineLength = 0;
        }

        void SourceWrite(string s)
        {
            sourceWriter.Write(s);
            _sourceLineLength += s.Length;
        }

        void SourceWrite(char c)
        {
            sourceWriter.Write(c);
            _sourceLineLength += 1;
        }

        void SourceWriteLine(string s = "")
        {
            sourceWriter.WriteLine(s);
            _sourceLineLength = 0;
        }

        void SourceWriteLine(char c)
        {
            sourceWriter.WriteLine(c);
            _sourceLineLength = 0;
        }

        void Write(string s)
        {
            HeaderWrite(s);
            SourceWrite(s);
        }

        void Write(char c, bool source = true)
        {
            headerWriter.Write(c);
            _headerLineLength += 1;
            if (source)
            {
                sourceWriter.Write(c);
                _sourceLineLength += 1;
            }
        }

        void WriteLine(string s)
        {
            HeaderWriteLine(s);
            SourceWriteLine(s);
        }

        void EnsureAccess(int level, ref RefAccessSpecifier current, RefAccessSpecifier required, bool withWhiteSpace = true)
        {
            if (current != required)
            {
                if (withWhiteSpace)
                {
                    EnsureHeaderWhiteSpace();
                }

                WriteTabs(level);
                if (required == RefAccessSpecifier.Internal)
                {
                    HeaderWriteLine("internal:");
                }
                else if (required == RefAccessSpecifier.Private)
                {
                    HeaderWriteLine("private:");
                }
                else if (required == RefAccessSpecifier.Public)
                {
                    HeaderWriteLine("public:");
                }
                current = required;
            }
        }

        void EnsureHeaderWhiteSpace()
        {
            if (!hasHeaderWhiteSpace)
            {
                HeaderWriteLine();
                hasHeaderWhiteSpace = true;
            }
        }

        void EnsureSourceWhiteSpace()
        {
            if (!hasSourceWhiteSpace)
            {
                SourceWriteLine();
                hasSourceWhiteSpace = true;
            }
        }

        void OutputMethodMarshal(MethodDefinition method, int numParameters)
        {
            if (method.IsConstructor)
            {
                SourceWrite(method.Parent.FullName);
            }
            else
            {
                SourceWrite(method.Name);
            }
            SourceWrite('(');
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                string marshal = BulletParser.GetTypeMarshal(param);
                if (!string.IsNullOrEmpty(marshal))
                {
                    SourceWrite(marshal);
                }
                else if (param.Type.IsBasic)
                {
                    SourceWrite(param.Name);
                }
                else
                {
                    if (param.Type.IsPointer || param.Type.IsReference)
                    {
                        if (param.Type.IsReference)
                        {
                            // Dereference
                            SourceWrite('*');
                        }

                        if (param.Type.Referenced.Target != null &&
                            param.Type.Referenced.Target.BaseClass != null)
                        {
                            // Cast native pointer from base class
                            SourceWrite('(');
                            SourceWrite(param.Type.Referenced.FullName);
                            SourceWrite("*)");
                        }
                    }
                    SourceWrite(param.Name);
                    if (param.Type.IsPointer && param.Type.ManagedName.Equals("void"))
                    {
                        SourceWrite(".ToPointer()");
                    }
                    else
                    {
                        SourceWrite("->_native");
                    }
                }

                // Any more parameters?
                if (i != numParameters - 1)
                {
                    if (_sourceLineLength >= LineBreakWidth)
                    {
                        SourceWriteLine(",");
                        WriteTabs(2, true);
                    }
                    else
                    {
                        SourceWrite(", ");
                    }
                }
            }
            SourceWrite(')');
        }

        void OutputMethod(MethodDefinition method, int level, int numOptionalParams = 0)
        {
            // No whitespace between get/set methods
            if (!(method.Property != null &&
                method.Equals(method.Property.Setter)))
            {
                EnsureSourceWhiteSpace();
                hasHeaderWhiteSpace = false;
            }

            WriteTabs(level + 1);

            // "static"
            if (method.IsStatic)
            {
                HeaderWrite("static ");
            }

            // Return type
            if (!method.IsConstructor)
            {
                var returnType = method.ReturnType;
                Write(BulletParser.GetTypeRefName(returnType));
                Write(' ');
            }

            // Name
            SourceWrite(method.Parent.FullNameManaged);
            SourceWrite("::");
            if (method.IsConstructor)
            {
                Write(method.Parent.ManagedName);
            }
            else
            {
                if (method.Property != null)
                {
                    SourceWrite(method.Property.Name);
                    SourceWrite("::");
                    if (method.Property.Getter.Equals(method))
                    {
                        Write("get");
                    }
                    else
                    {
                        Write("set");
                    }
                }
                else
                {
                    Write(method.ManagedName);
                }
            }
            Write('(');

            // Parameters
            int numParameters = method.Parameters.Length - numOptionalParams;
            bool hasOptionalParam = false;
            for (int i = 0; i < numParameters; i++)
            {
                var param = method.Parameters[i];
                Write(BulletParser.GetTypeRefName(param.Type));
                Write(' ');
                Write(param.Name);

                if (param.IsOptional)
                {
                    hasOptionalParam = true;
                }

                if (i != numParameters - 1)
                {
                    if (_headerLineLength >= LineBreakWidth)
                    {
                        HeaderWriteLine(",");
                        WriteTabs(level + 2);
                    }
                    else
                    {
                        HeaderWrite(", ");
                    }

                    if (_sourceLineLength >= LineBreakWidth)
                    {
                        SourceWriteLine(",");
                        WriteTabs(1, true);
                    }
                    else
                    {
                        SourceWrite(", ");
                    }
                }
            }
            HeaderWriteLine(");");
            SourceWriteLine(')');


            // Constructor chaining
            bool doConstructorChaining = false;
            if (method.IsConstructor && method.Parent.BaseClass != null)
            {
                // If there is no need for marshalling code, we can chain constructors
                doConstructorChaining = true;
                foreach (var param in method.Parameters)
                {
                    if (BulletParser.TypeRequiresMarshal(param.Type))
                    {
                        doConstructorChaining = false;
                        break;
                    }
                }

                WriteTabs(1, true);
                SourceWrite(": ");
                SourceWrite(method.Parent.BaseClass.ManagedName);
                SourceWrite('(');

                if (doConstructorChaining)
                {
                    SourceWrite("new ");
                    OutputMethodMarshal(method, numParameters);
                }
                else
                {
                    SourceWrite('0');
                }

                SourceWriteLine(')');
            }

            // Method definition
            SourceWriteLine('{');

            if (!doConstructorChaining)
            {
                // Type marshalling prologue
                if (method.Field == null)
                {
                    for (int i = 0; i < numParameters; i++)
                    {
                        var param = method.Parameters[i];
                        string prologue = BulletParser.GetTypeMarshalPrologueCppCli(param);
                        if (!string.IsNullOrEmpty(prologue))
                        {
                            WriteTabs(1, true);
                            SourceWriteLine(prologue);
                        }
                    }
                }

                WriteTabs(1, true);
                if (method.IsConstructor)
                {
                    SourceWrite("_native = new ");
                }
                else
                {
                    if (!(method.ReturnType.IsBasic && method.ReturnType.Name.Equals("void")))
                    {
                        //if (method.ReturnType.IsBasic || method.ReturnType.Referenced != null)
                        SourceWrite("return ");
                        SourceWrite(BulletParser.GetTypeMarshalConstructorStart(method));
                    }
                }


                if (method.Field != null)
                {
                    if (method.Equals(method.Property.Getter))
                    {
                        SourceWrite("_native->");
                        SourceWrite(method.Field.Name);
                    }

                    var setter = method.Property.Setter;
                    if (setter != null && method.Equals(setter))
                    {
                        var param = method.Parameters[0];
                        var fieldSet = BulletParser.GetTypeMarshalFieldSet(method.Field, param);
                        if (!string.IsNullOrEmpty(fieldSet))
                        {
                            SourceWrite(fieldSet);
                        }
                        else
                        {
                            SourceWrite("_native->");
                            SourceWrite(method.Field.Name);
                            SourceWrite(" = ");
                            SourceWrite(param.Name);
                            if (!param.Type.IsBasic)
                            {
                                SourceWrite("->_native");
                            }
                        }
                    }
                }
                else
                {
                    if (!method.IsConstructor)
                    {
                        if (method.IsStatic)
                        {
                            SourceWrite(method.Parent.FullName);
                            SourceWrite("::");
                        }
                        else
                        {
                            SourceWrite("_native->");
                        }
                    }
                    OutputMethodMarshal(method, numParameters);
                }
                if (!method.IsConstructor && !(method.ReturnType.IsBasic && method.ReturnType.Name.Equals("void")))
                {
                    SourceWrite(BulletParser.GetTypeMarshalConstructorEnd(method));
                }
                SourceWriteLine(';');

                // Type marshalling epilogue
                if (method.Field == null)
                {
                    for (int i = 0; i < numParameters; i++)
                    {
                        var param = method.Parameters[i];
                        string epilogue = BulletParser.GetTypeMarshalEpilogueCppCli(param);
                        if (!string.IsNullOrEmpty(epilogue))
                        {
                            WriteTabs(1, true);
                            SourceWriteLine(epilogue);
                        }
                    }
                }
            }
            SourceWriteLine('}');
            hasSourceWhiteSpace = false;

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

            EnsureHeaderWhiteSpace();

            // Write access modifier
            WriteTabs(level);
            if (level == 1)
            {
                HeaderWrite("public ");
            }

            // Write class definition
            HeaderWrite("ref class ");
            HeaderWrite(c.ManagedName);
            if (c.IsAbstract)
            {
                HeaderWrite(" abstract");
            }
            if (c.BaseClass != null)
            {
                HeaderWrite(" : ");
                HeaderWrite(c.BaseClass.ManagedName);
            }
            HeaderWriteLine();
            WriteTabs(level);
            headerWriter.WriteLine("{");
            hasHeaderWhiteSpace = true;

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
                    HeaderWriteLine();
                }
                EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

                WriteTabs(level + 1);
                HeaderWrite(c.FullName);
                headerWriter.WriteLine("* _native;");
            }

            // TODO: Write constructor from unmanaged pointer only if the class is ever instantiated in this way.

            // Write unmanaged constructor
            EnsureAccess(level, ref currentAccess, RefAccessSpecifier.Internal);

            WriteTabs(level + 1);
            SourceWrite(c.FullNameManaged);
            SourceWrite("::");
            Write(c.ManagedName);
            Write('(');
            Write(c.FullName);
            Write("* native)");
            headerWriter.WriteLine(';');
            hasHeaderWhiteSpace = false;
            SourceWriteLine();
            if (c.BaseClass != null)
            {
                WriteTabs(1, true);
                SourceWrite(": ");
                SourceWrite(c.BaseClass.ManagedName);
                SourceWriteLine("(native)");
            }
            SourceWriteLine('{');
            if (c.BaseClass == null)
            {
                WriteTabs(1, true);
                SourceWriteLine("_native = native;");
            }
            SourceWriteLine('}');

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
                EnsureHeaderWhiteSpace();

                foreach (MethodDefinition method in c.Methods)
                {
                    if (!method.IsConstructor && method.Field == null)
                    {
                        OutputMethod(method, level);
                    }
                }
            }

            // Write properties (includes unmanaged fields and getters/setters)
            foreach (PropertyDefinition prop in c.Properties)
            {
                EnsureHeaderWhiteSpace();

                string typeRefName = BulletParser.GetTypeRefName(prop.Type);

                /*
                // If property name matches type name, resolve ambiguity
                if (prop.Name.Equals(prop.Type.ManagedName))
                {
                    typeRefName = namespaceName + "::" + typeRefName;
                }
                */

                WriteTabs(level + 1);
                HeaderWrite("property ");
                HeaderWrite(typeRefName);
                HeaderWrite(" ");
                headerWriter.WriteLine(prop.Name);
                WriteTabs(level + 1);
                headerWriter.WriteLine("{");
                
                // Getter/Setter
                OutputMethod(prop.Getter, level + 1);
                if (prop.Setter != null)
                {
                    OutputMethod(prop.Setter, level + 1);
                }

                WriteTabs(level + 1);
                headerWriter.WriteLine("}");

                hasHeaderWhiteSpace = false;
            }

            WriteTabs(level);
            headerWriter.WriteLine("};");
            hasHeaderWhiteSpace = false;
        }

        public void Output()
        {
            string outDirectory = namespaceName + "_cppcli";

            foreach (HeaderDefinition header in headerDefinitions.Values)
            {
                if (header.Classes.Count == 0)
                {
                    continue;
                }

                Directory.CreateDirectory(outDirectory);
                var headerFile = new FileStream(outDirectory + "\\" + header.ManagedName + ".h", FileMode.Create, FileAccess.Write);
                headerWriter = new StreamWriter(headerFile);
                headerWriter.WriteLine("#pragma once");
                HeaderWriteLine();

                var sourceFile = new FileStream(outDirectory + "\\" + header.ManagedName + ".cpp", FileMode.Create, FileAccess.Write);
                sourceWriter = new StreamWriter(sourceFile);
                SourceWriteLine("#include \"StdAfx.h\"");
                SourceWriteLine();

                // Write includes
                if (header.Includes.Count != 0)
                {
                    foreach (HeaderDefinition include in header.Includes)
                    {
                        HeaderWrite("#include \"");
                        HeaderWrite(include.ManagedName);
                        headerWriter.WriteLine(".h\"");
                    }
                    HeaderWriteLine();
                }

                // Write namespace
                HeaderWrite("namespace ");
                headerWriter.WriteLine(namespaceName);
                headerWriter.WriteLine("{");
                hasHeaderWhiteSpace = true;

                // Find forward references
                var forwardRefs = new List<ClassDefinition>();
                foreach (ClassDefinition c in header.Classes)
                {
                    FindForwardReferences(forwardRefs, c);
                }

                // Remove redundant forward references (header file already included)
                forwardRefs.RemoveAll(fr => header.Includes.Contains(fr.Header));
                forwardRefs.Sort((r1, r2) => r1.ManagedName.CompareTo(r2.ManagedName));

                // Write forward references
                var forwardRefHeaders = new List<HeaderDefinition>();
                foreach (ClassDefinition c in forwardRefs)
                {
                    WriteTabs(1);
                    HeaderWrite("ref class ");
                    HeaderWrite(c.ManagedName);
                    headerWriter.WriteLine(";");
                    if (!forwardRefHeaders.Contains(c.Header))
                    {
                        forwardRefHeaders.Add(c.Header);
                    }
                    hasHeaderWhiteSpace = false;
                }
                forwardRefHeaders.Add(header);
                forwardRefHeaders.Sort((r1, r2) => r1.ManagedName.CompareTo(r2.ManagedName));

                // Write statements to include forward referenced types
                if (forwardRefHeaders.Count != 0)
                {
                    foreach (HeaderDefinition h in forwardRefHeaders)
                    {
                        SourceWrite("#include \"");
                        SourceWrite(h.ManagedName);
                        SourceWriteLine(".h\"");
                    }
                    SourceWriteLine();
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

        // These do no need forward references
        public List<string> PrecompiledHeaderReferences = new List<string>(new[] { "Vector3", "Quaternion", "Transform" });

        void AddForwardReference(List<ClassDefinition> forwardRefs, TypeRefDefinition type, HeaderDefinition header)
        {
            if (type.IsBasic)
            {
                return;
            }

            if (type.IsPointer || type.IsReference)
            {
                AddForwardReference(forwardRefs, type.Referenced, header);
                return;
            }

            if (type.Target == null)
            {
                return;
            }
            if (forwardRefs.Contains(type.Target) || PrecompiledHeaderReferences.Contains(type.Target.ManagedName))
            {
                return;
            }

            // Forward ref to class in another header
            if (type.Target.Header != header)
            {
                forwardRefs.Add(type.Target);
            }
        }

        void FindForwardReferences(List<ClassDefinition> forwardRefs, ClassDefinition c)
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
