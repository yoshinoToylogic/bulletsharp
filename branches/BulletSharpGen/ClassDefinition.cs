using System.Collections.Generic;

namespace BulletSharpGen
{
    class ClassDefinition
    {
        public string Name { get; private set; }
        public List<ClassDefinition> Classes { get; private set; }
        public TypeRefDefinition BaseClass { get; set; }
        public ClassDefinition Parent { get; private set; }
        public HeaderDefinition Header { get; private set; }
        public List<MethodDefinition> Methods { get; private set; }
        public List<PropertyDefinition> Properties { get; private set; }
        public List<FieldDefinition> Fields { get; private set; }
        public bool IsAbstract { get; set; }
        public bool IsStruct { get; set; }
        public bool IsTemplate { get; set; }

        // For function prototypes IsTypeDef == true, but TypedefUnderlyingType == null
        public bool IsTypedef { get; set; }
        public TypeRefDefinition TypedefUnderlyingType { get; set; }

        // Pure enum = enum wrapped in a struct
        public EnumDefinition Enum { get; set; }
        public bool IsPureEnum
        {
            get { return Enum != null && Methods.Count == 0; }
        }

        public string ManagedName { get; set; }

        public string FullName
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.FullName + "::" + Name;
                }
                return Name;
            }
        }

        public string FullNameCS
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.FullNameCS + '_' + Name;
                }
                return Name;
            }
        }

        public string FullNameManaged
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.FullNameManaged + "::" + ManagedName;
                }
                return ManagedName;
            }
        }

        public ClassDefinition(string name, ClassDefinition parent)
            : this(name, parent.Header)
        {
            Parent = parent;
        }

        public ClassDefinition(string name, HeaderDefinition header)
        {
            Name = name;
            Header = header;

            Classes = new List<ClassDefinition>();
            Methods = new List<MethodDefinition>();
            Properties = new List<PropertyDefinition>();
            Fields = new List<FieldDefinition>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
