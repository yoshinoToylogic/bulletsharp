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
            Name = name;
            Parent = parent;

            parent.Fields.Add(this);
        }
    }
}
