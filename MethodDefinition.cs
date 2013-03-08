using System.Collections.Generic;

namespace BulletSharpGen
{
    class MethodDefinition
    {
        public string Name { get; private set; }
        public ClassDefinition Parent { get; private set; }
        public TypeRefDefinition ReturnType { get; set; }
        public ParameterDefinition[] Parameters { get; private set; }
        public bool IsStatic { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsConstructor { get; set; }
        public FieldDefinition Field { get; set; } // get/set method target

        public string ManagedName
        {
            get
            {
                return Name.Substring(0, 1).ToUpper() + Name.Substring(1);
            }
        }

        public MethodDefinition(string name, ClassDefinition parent, int numArgs)
        {
            Name = name;
            Parent = parent;
            Parameters = new ParameterDefinition[numArgs];

            parent.Methods.Add(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            MethodDefinition m = obj as MethodDefinition;
            if ((System.Object)m == null)
            {
                return false;
            }

            if (!m.Name.Equals(Name) || m.Parameters.Length != Parameters.Length)
            {
                return false;
            }

            if (!m.ReturnType.Equals(ReturnType))
            {
                return false;
            }

            for (int i = 0; i < Parameters.Length; i++)
            {
                // Parameter names can vary, but types must match
                if (!m.Parameters[i].Type.Equals(Parameters[i].Type))
                {
                    return false;
                }
            }

            return m.IsStatic == IsStatic;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
