using System;
using System.Collections.Generic;

namespace BulletSharpGen
{
    class BulletParser
    {
        Dictionary<string, ClassDefinition> classDefinitions = new Dictionary<string, ClassDefinition>();
        public Dictionary<string, HeaderDefinition> HeaderDefinitions = new Dictionary<string, HeaderDefinition>();

        public BulletParser(Dictionary<string, ClassDefinition> classDefinitions, Dictionary<string, HeaderDefinition> headerDefinitions)
        {
            this.classDefinitions = classDefinitions;
            this.HeaderDefinitions = headerDefinitions;

            // Resolve references (match TypeRefDefinitions to ClassDefinitions)
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                // Resolve base class type
                if (c.BaseClass != null)
                {
                    ResolveTypeRef(c.BaseClass);
                    if (c.BaseClass.Target == null)
                    {
                        Console.WriteLine("Base class " + c.BaseClass.Name + " not found!");
                    }
                    else
                    {
                        if (c.Header != c.BaseClass.Target.Header)
                        {
                            c.Header.Includes.Add(c.BaseClass.Target.Header);
                        }
                    }
                }

                // Resolve typedef
                if (c.TypedefUnderlyingType != null)
                {
                    ResolveTypeRef(c.TypedefUnderlyingType);
                }

                // Resolve method return type and parameter types
                foreach (MethodDefinition method in c.Methods)
                {
                    ResolveTypeRef(method.ReturnType);
                    foreach (ParameterDefinition param in method.Parameters)
                    {
                        ResolveTypeRef(param.Type);
                    }
                }

                // Resolve field types
                foreach (FieldDefinition field in c.Fields)
                {
                    ResolveTypeRef(field.Type);
                }
            }

            // Exclude all overridden methods
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                int i;
                for (i = 0; i < c.Methods.Count; )
                {
                    var method = c.Methods[i];
                    // Check if the method already exists in base classes
                    var baseClass = c.BaseClass;
                    while (baseClass != null && baseClass.Target != null)
                    {
                        foreach (MethodDefinition m in baseClass.Target.Methods)
                        {
                            if (method.Equals(m))
                            {
                                c.Methods.Remove(method);
                                method = null;
                                break;
                            }
                        }
                        if (method == null)
                        {
                            break;
                        }
                        baseClass = baseClass.Target.BaseClass;
                    }
                    if (method != null)
                    {
                        i++;
                    }
                }
            }

            /*
            // Turn fields into getters/setters
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                foreach (FieldDefinition field in c.Fields)
                {
                    ResolveTypeRef(field.Type);

                    // Resolve native and managed name
                    string name = field.Name;
                    string managedName = name;
                    if (managedName.StartsWith("m_"))
                    {
                        managedName = managedName.Substring(2);
                    }
                    managedName = name.Substring(0, 1).ToUpper() + name.Substring(1);

                    MethodDefinition getter = new MethodDefinition("get" + name, field.Parent, 0);
                    getter.ReturnType = field.Type;
                    getter.Field = field;
                }
            }*/
            /*
            // Turn fields into properties
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                foreach (FieldDefinition field in c.Fields)
                {
                    ResolveTypeRef(field.Type);
                    string name = field.Name;
                    if (name.StartsWith("m_"))
                    {
                        name = name.Substring(2);
                    }
                    field.ManagedName = name;
                    new PropertyDefinition(field);
                }
            }
            */

            // Exclude duplicate methods (e.g. with const/non-const return value)
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                for (int i = 0; i < c.Methods.Count; i++)
                {
                    for (int j = 0; j < c.Methods.Count; j++)
                    {
                        if (i != j && c.Methods[i].Equals(c.Methods[j]))
                        {
                            if (c.Methods[i].ReturnType.IsConst)
                            {
                                c.Methods.Remove(c.Methods[i]);
                            }
                            else
                            {
                                c.Methods.Remove(c.Methods[j]);
                            }
                            i--;
                            break;
                        }
                    }
                }
            }

            // Turn getters/setters into properties
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                int i;
                for (i = 0; i < c.Methods.Count; i++)
                {
                    var method = c.Methods[i];
                    if (method.Parameters.Length == 0 &&
                        (method.Name.StartsWith("get") ||
                        method.Name.StartsWith("has") ||
                        method.Name.StartsWith("is")))
                    {
                        var prop = new PropertyDefinition(method);
                        c.Properties.Add(prop);
                        //c.Methods.Remove(method);
                        //i--;
                    }
                }
                for (i = 0; i < c.Methods.Count; i++)
                {
                    var method = c.Methods[i];
                    if (method.Parameters.Length == 1 && method.Name.StartsWith("set"))
                    {
                        string name = method.Name.Substring(3);
                        // Find the property with the matching getter
                        foreach (PropertyDefinition prop in c.Properties)
                        {
                            if (prop.VerblessName.Equals(name))
                            {
                                prop.Setter = method;
                                method.Property = prop;
                                //c.Methods.Remove(method);
                                //i--;
                                break;
                            }
                        }
                    }
                }
            }

            // Get managed header/class/method names
            foreach (HeaderDefinition header in HeaderDefinitions.Values)
            {
                string name = header.Name;
                if (name.StartsWith("bt"))
                {
                    header.ManagedName = name.Substring(2);
                }
            }

            // Apply some transformations
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                string name = c.Name;
                if (name.StartsWith("bt") && name != "btScalar")
                {
                    name = name.Substring(2);
                }
                c.ManagedName = name;

                foreach (MethodDefinition method in c.Methods)
                {
                }
            }

            // Sort methods and properties alphabetically
            foreach (HeaderDefinition header in HeaderDefinitions.Values)
            {
                foreach (ClassDefinition c in header.Classes)
                {
                    c.Methods.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
                    c.Properties.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
                }
            }
            /*
            // Apply transformations
            ClassDefinition transformClass;
            if (classDefinitions.TryGetValue("btScalar", out transformClass))
            {
                classDefinitions.Remove("btScalar");
                ClassDefinition replacementClass = new ClassDefinition("btScalar", transformClass.Header);
                //replacementClass.IsBasic = true;
                classDefinitions.Add("btScalar", replacementClass);
            }
            */
            Console.WriteLine("Parsing complete");
        }

        void ResolveTypeRef(TypeRefDefinition typeRef)
        {
            if (!typeRef.IsBasic && !typeRef.HasTemplateTypeParameter)
            {
                if (typeRef.IsPointer || typeRef.IsReference || typeRef.IsConstantArray)
                {
                    ResolveTypeRef(typeRef.Referenced);
                }
                else if (!classDefinitions.ContainsKey(typeRef.Name))
                {
                    Console.WriteLine("Class " + typeRef.Name + " not found!");
                }
                else
                {
                    typeRef.Target = classDefinitions[typeRef.Name];
                }
            }
        }

        public static string GetTypeMarshalPrologue(ParameterDefinition parameter)
        {
            switch (parameter.Type.ManagedName)
            {
                case "Vector3":
                    return "VECTOR3_CONV(" + parameter.Name + ");";
                case "Transform":
                    return "TRANSFORM_CONV(" + parameter.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshal(ParameterDefinition parameter)
        {
            switch (parameter.Type.ManagedName)
            {
                case "Vector3":
                    return "VECTOR3_USE(" + parameter.Name + ")";
                case "Transform":
                    return "TRANSFORM_USE(" + parameter.Name + ")";
                default:
                    return null;
            }
        }
    }
}
