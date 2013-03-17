using System;
namespace BulletSharpGen
{
    class PropertyDefinition
    {
        public string Name { get; private set; }
        public string VerblessName { get; private set; }
        public MethodDefinition Getter { get; private set; }
        public MethodDefinition Setter { get; set; }
        public FieldDefinition Field { get; private set; }
        public ClassDefinition Parent { get; private set; }
        public TypeRefDefinition Type { get; private set; }

        // Property from getter method
        public PropertyDefinition(MethodDefinition getter)
        {
            Getter = getter;
            Name = getter.Name;
            Parent = getter.Parent;
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
            Field = field;
            Name = field.ManagedName;
            Parent = field.Parent;
            Parent.Properties.Add(this);
            Type = field.Type;

            // Capitalize
            Name = Name.Substring(0, 1).ToUpper() + Name.Substring(1);
        }
    }
}
