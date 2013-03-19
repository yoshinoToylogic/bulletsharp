using System;
namespace BulletSharpGen
{
    class PropertyDefinition
    {
        public string Name { get; private set; }
        public string VerblessName { get; private set; }
        public MethodDefinition Getter { get; private set; }
        public MethodDefinition Setter { get; set; }
        public ClassDefinition Parent { get; private set; }
        public TypeRefDefinition Type { get; private set; }

        // Property from getter method
        public PropertyDefinition(MethodDefinition getter)
        {
            Getter = getter;
            Name = getter.Name;
            Parent = getter.Parent;
            Parent.Properties.Add(this);
            getter.Property = this;
            Type = getter.ReturnType;

            if (Name.StartsWith("get"))
            {
                Name = Name.Substring(3);
                VerblessName = Name;
            }
            else if (Name.StartsWith("is"))
            {
                VerblessName = Name.Substring(2);
                Name = "Is" + VerblessName;
            }
            else if (Name.StartsWith("has"))
            {
                VerblessName = Name.Substring(3);
                Name = "Has" + VerblessName;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        // Property from field
        public PropertyDefinition(FieldDefinition field)
        {
            Name = field.ManagedName;
            VerblessName = Name;
            Parent = field.Parent;
            Parent.Properties.Add(this);
            Type = field.Type;

            // Capitalize
            Name = Name.Substring(0, 1).ToUpper() + Name.Substring(1);

            // Generate getter/setter methods for C API
            string getterName, setterName;
            if (Name.StartsWith("has"))
            {
                getterName = Name;
                setterName = "set" + Name.Substring(3);
            }
            else if (Name.StartsWith("is"))
            {
                getterName = Name;
                setterName = "set" + Name.Substring(2);
            }
            else
            {
                getterName = "get" + Name;
                setterName = "set" + Name;
            }

            Getter = new MethodDefinition(getterName, Parent, 0);
            Getter.ReturnType = field.Type;
            Getter.Field = field;
            Getter.Property = this;

            Setter = new MethodDefinition(setterName, Parent, 1);
            Setter.ReturnType = new TypeRefDefinition();
            Setter.Parameters[0] = new ParameterDefinition("value", field.Type);
            Setter.Field = field;
            Setter.Property = this;
        }
    }
}
