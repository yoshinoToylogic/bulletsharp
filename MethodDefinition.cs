using System.Collections.Generic;

namespace BulletSharpGen
{
    class MethodDefinition
    {
        public string Name { get; private set; }
        public ClassDefinition Parent { get; private set; }
        public TypeRefDefinition ReturnType { get; set; }
        public string[] Parameters { get; private set; }
        public TypeRefDefinition[] ParameterTypes { get; private set; }
        public bool IsStatic { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsConstructor { get; set; }

        public MethodDefinition(string name, ClassDefinition parent, int numArgs)
        {
            Name = name;
            Parent = parent;
            Parameters = new string[numArgs];
            ParameterTypes = new TypeRefDefinition[numArgs];

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

            // TODO: check that parameters match also

            return true;
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
