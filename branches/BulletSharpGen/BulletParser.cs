using System;
using System.Collections.Generic;
using System.Text;

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

            // Exclude constructors of abstract classes
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                if (!c.IsAbstract)
                {
                    continue;
                }

                int i;
                for (i = 0; i < c.Methods.Count; )
                {
                    var method = c.Methods[i];
                    if (method.IsConstructor)
                    {
                        c.Methods.Remove(method);
                        continue;
                    }
                    i++;
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
                        (method.Name.StartsWith("get", StringComparison.InvariantCultureIgnoreCase) ||
                        method.Name.StartsWith("has", StringComparison.InvariantCultureIgnoreCase) ||
                        method.Name.StartsWith("is", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        // function returns the result
                        new PropertyDefinition(method);
                        c.Methods.Remove(method);
                        i--;
                    }
                    else if (method.Parameters.Length == 1 &&
                        method.Name.StartsWith("get", StringComparison.InvariantCultureIgnoreCase) &&
                        method.ReturnType.IsBasic && method.ReturnType.ManagedName.Equals("void"))
                    {
                        // function writes the result to a given pointer
                        new PropertyDefinition(method);
                        c.Methods.Remove(method);
                        i--;
                    }
                }
                for (i = 0; i < c.Methods.Count; i++)
                {
                    var method = c.Methods[i];
                    if (method.Parameters.Length == 1 &&
                        method.Name.StartsWith("set", StringComparison.InvariantCultureIgnoreCase))
                    {
                        string name = method.Name.Substring(3);
                        // Find the property with the matching getter
                        foreach (PropertyDefinition prop in c.Properties)
                        {
                            if (prop.Name.Equals(name))
                            {
                                prop.Setter = method;
                                method.Property = prop;
                                c.Methods.Remove(method);
                                i--;
                                break;
                            }
                        }
                    }
                }
            }

            // Turn fields into properties
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                foreach (FieldDefinition field in c.Fields)
                {
                    ResolveTypeRef(field.Type);
                    var prop = new PropertyDefinition(field);
                    c.Methods.Remove(prop.Getter);
                    c.Methods.Remove(prop.Setter);
                }
            }

            // Get managed header/class/method names
            foreach (HeaderDefinition header in HeaderDefinitions.Values)
            {
                string name = header.Name;
                if (name.StartsWith("bt"))
                {
                    if (name.Equals("btSparseSDF"))
                    {
                        header.ManagedName = "SparseSdf";
                        continue;
                    }
                    else if (name.Equals("btCompoundFromGimpact"))
                    {
                        header.ManagedName = "CompoundFromGImpact";
                        continue;
                    }
                    else if (name.Equals("btNNCGConstraintSolver"))
                    {
                        header.ManagedName = "NncgConstraintSolver";
                        continue;
                    }
                    header.ManagedName = name.Substring(2);
                }
                else if (name.StartsWith("hacd"))
                {
                    if (name.Equals("hacdHACD"))
                    {
                        header.ManagedName = "Hacd";
                    }
                    else
                    {
                        header.ManagedName = "Hacd" + name.Substring(4);
                    }
                }
            }

            // Apply some transformations
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                string name = c.Name;
                if (name.StartsWith("bt"))
                {
                    if (name.Equals("btNNCGConstraintSolver"))
                    {
                        name = "NncgConstraintSolver";
                    }
                    else if (name.Equals("btScalar"))
                    {
                    }
                    else
                    {
                        name = name.Substring(2);
                    }
                }
                c.ManagedName = name;
            }

            // Sort methods and properties alphabetically
            foreach (ClassDefinition c in classDefinitions.Values)
            {
                c.Methods.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
                c.Properties.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
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

                if (typeRef.SpecializedTemplateType != null)
                {
                    ResolveTypeRef(typeRef.SpecializedTemplateType);
                }
            }
        }

        static string GetTabs(int n)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                builder.Append('\t');
            }
            return builder.ToString();
        }

        public static string GetTypeName(TypeRefDefinition type)
        {
            switch (type.ManagedName)
            {
                case "Quaternion":
                case "Transform":
                case "Vector3":
                    return "btScalar";
                default:
                    return type.FullName;
            }
        }

        public static string GetTypeNameCS(TypeRefDefinition type)
        {
            switch (type.ManagedName)
            {
                case "Matrix3x3":
                case "Transform":
                    return "Matrix";
                default:
                    return type.ManagedName;
            }
        }

        public static string GetTypeRefName(TypeRefDefinition type)
        {
            if (!string.IsNullOrEmpty(type.Name) && type.Name.Equals("btAlignedObjectArray"))
            {
                if (type.SpecializedTemplateType != null)
                {
                    return "Aligned" + type.SpecializedTemplateType.ManagedName + "Array^";
                }
            }

            switch (type.ManagedName)
            {
                case "Matrix3x3":
                    return "Matrix";
                case "Transform":
                    return "Matrix";
                case "Quaternion":
                case "Vector3":
                    return type.ManagedName;
            }
            
            if (type.ManagedName.Equals("float") && "btScalar".Equals(type.Name))
            {
                return "btScalar";
            }
            return type.ManagedTypeRefName;
        }

        // Does the type require additional lines of code to marshal?
        public static bool TypeRequiresMarshal(TypeRefDefinition type)
        {
            switch (type.ManagedName)
            {
                case "Matrix3x3":
                case "Quaternion":
                case "Transform":
                case "Vector3":
                    return true;
                default:
                    return false;
            }
        }

        public static string GetTypeMarshalPrologue(ParameterDefinition parameter)
        {
            switch (parameter.Type.ManagedName)
            {
                case "Quaternion":
                    return "QUATERNION_CONV(" + parameter.Name + ");";
                case "Matrix3x3":
                    return "MATRIX3X3_CONV(" + parameter.Name + ");";
                case "Transform":
                    return "TRANSFORM_CONV(" + parameter.Name + ");";
                case "Vector3":
                    return "VECTOR3_CONV(" + parameter.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalPrologueCppCli(ParameterDefinition parameter)
        {
            switch (parameter.Type.ManagedName)
            {
                case "Matrix3x3":
                    return "MATRIX3X3_CONV(" + parameter.Name + ");";
                case "Quaternion":
                    return "QUATERNION_CONV(" + parameter.Name + ");";
                case "Transform":
                    return "TRANSFORM_CONV(" + parameter.Name + ");";
                case "Vector3":
                    return "VECTOR3_DEF(" + parameter.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalEpilogueCppCli(ParameterDefinition parameter)
        {
            switch (parameter.Type.ManagedName)
            {
                case "Quaternion":
                    return "QUATERNION_DEL(" + parameter.Name + ");";
                case "Matrix3x3":
                    return "MATRIX3X3_DEL(" + parameter.Name + ");";
                case "Transform":
                    return "TRANSFORM_DEL(" + parameter.Name + ");";
                case "Vector3":
                    return "VECTOR3_DEL(" + parameter.Name + ");";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshal(ParameterDefinition parameter)
        {
            switch (parameter.Type.ManagedName)
            {
                case "IDebugDraw":
                    return "DebugDraw::GetUnmanaged(" + parameter.Name + ")";
                case "Quaternion":
                    return "QUATERNION_USE(" + parameter.Name + ")";
                case "Transform":
                    return "TRANSFORM_USE(" + parameter.Name + ")";
                case "Matrix3x3":
                    return "MATRIX3X3_USE(" + parameter.Name + ")";
                case "Vector3":
                    return "VECTOR3_USE(" + parameter.Name + ")";
                default:
                    return null;
            }
        }

        public static string GetTypeMarshalConstructorStart(MethodDefinition getter)
        {
            switch (getter.ReturnType.ManagedName)
            {
                case "CollisionShape":
                    return "CollisionShape::GetManaged(";
                case "IDebugDraw":
                    return "DebugDraw::GetUnmanaged(";
                case "OverlappingPairCache":
                    return "OverlappingPairCache::GetManaged(";
                case "Quaternion":
                    return "Math::BtQuatToQuaternion(&";
                case "Transform":
                    return "Math::BtTransformToMatrix(&";
                case "Vector3":
                    return "Math::BtVector3ToVector3(&";
                default:
                    return string.Empty;
            }
        }

        public static string GetTypeMarshalConstructorEnd(MethodDefinition getter)
        {
            switch (getter.ReturnType.ManagedName)
            {
                case "CollisionShape":
                case "IDebugDraw":
                case "Quaternion":
                case "Transform":
                case "Vector3":
                    return ")";
                default:
                    return string.Empty;
            }
        }

        public static string GetTypeMarshalFieldSet(FieldDefinition field, ParameterDefinition parameter)
        {
            switch (field.Type.ManagedName)
            {
                case "Quaternion":
                    return "Math::QuaternionToBtQuat(" + parameter.Name + ", &_native->" + field.Name + ')';
                case "Transform":
                    return "Math::MatrixToBtTransform(" + parameter.Name + ", &_native->" + field.Name + ')';
                case "Vector3":
                    return "Math::Vector3ToBtVector3(" + parameter.Name + ", &_native->" + field.Name + ')';
                default:
                    return null;
            }
        }

        public static string GetTypeDllImport(TypeRefDefinition type)
        {
            if (type.Referenced != null && !type.IsBasic)
            {
                switch (type.ManagedName)
                {
                    case "Matrix3x3":
                    case "Quaternion":
                    case "Transform":
                    case "Vector3":
                        return "[In] ref " + GetTypeNameCS(type);
                    default:
                        return "IntPtr";
                }
            }

            return type.ManagedName;
        }

        public static string GetTypeCSMarshal(ParameterDefinition parameter)
        {
            var type = parameter.Type;

            if (type.Referenced != null && !type.IsBasic)
            {
                switch (type.ManagedName)
                {
                    case "Matrix3x3":
                    case "Quaternion":
                    case "Transform":
                    case "Vector3":
                        return "ref " + parameter.Name;
                    case "IDebugDraw":
                        return "DebugDraw.GetUnmanaged(" + parameter.Name + ')';
                }
            }

            string output = parameter.Name;
            if (!type.IsBasic)
            {
                return parameter.Name + "._native";
            }
            return parameter.Name;
        }

        public static string GetTypeGetterCSMarshal(PropertyDefinition prop, int level)
        {
            StringBuilder output = new StringBuilder();
            TypeRefDefinition type = prop.Type;

            if (type.Referenced != null && !type.IsBasic)
            {
                switch (type.ManagedName)
                {
                    case "Transform":
                    case "Vector3":
                        output.AppendLine(GetTabs(level + 2) + "get");
                        output.AppendLine(GetTabs(level + 2) + "{");
                        output.AppendLine(GetTabs(level + 3) + GetTypeNameCS(type) + " value;");
                        output.AppendLine(GetTabs(level + 3) + prop.Parent.FullNameCS + '_' + prop.Getter.Name + "(_native, out value);");
                        output.AppendLine(GetTabs(level + 3) + "return value;");
                        output.AppendLine(GetTabs(level + 2) + '}');
                        return output.ToString();
                }
            }

            output.AppendLine(GetTabs(level + 2) + "get { return " + prop.Parent.FullNameCS + '_' + prop.Getter.Name + "(_native); }");
            return output.ToString();
        }

        public static string GetTypeSetterCSMarshal(TypeRefDefinition type)
        {
            if (type.Referenced != null && !type.IsBasic)
            {
                switch (type.ManagedName)
                {
                    case "Transform":
                    case "Vector3":
                        return "ref value";
                }
            }

            if (!type.IsBasic)
            {
                return "value._native";
            }
            return "value";
        }

        public static bool IsExcludedClass(ClassDefinition cl)
        {
            // Exclude all "FloatData/DoubleData" serialization classes
            return cl.Name.EndsWith("Data") && !cl.ManagedName.Equals("ContactSolverInfoData");
        }
    }
}
