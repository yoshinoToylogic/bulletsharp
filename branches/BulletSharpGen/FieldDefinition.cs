namespace BulletSharpGen
{
    class FieldDefinition
    {
        public string Name { get; set; }
        public string ManagedName { get; set; }
        public TypeRefDefinition Type { get; set; }
        public ClassDefinition Parent { get; set; }

        public FieldDefinition(string name, ClassDefinition parent)
        {
            Parent = parent;
            Name = name;
            if (name.StartsWith("m_"))
            {
                name = name.Substring(2);
            }
            ManagedName = name;

            parent.Fields.Add(this);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
