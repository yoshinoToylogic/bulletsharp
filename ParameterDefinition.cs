namespace BulletSharpGen
{
    class ParameterDefinition
    {
        public string Name { get; private set; }
        public TypeRefDefinition Type { get; private set; }
        public bool IsOptional { get; set; }
        public string ManagedName { get; private set; }

        public ParameterDefinition(string name, TypeRefDefinition type, bool isOptional = false)
        {
            Name = name;
            Type = type;
            IsOptional = isOptional;

            // Remove underscores from managed name
            if (!name.StartsWith("__unnamed"))
            {
                // one_two_three -> oneTwoThree
                while (name.Contains("_"))
                {
                    int pos = name.IndexOf('_');
                    if (pos == 0)
                    {
                        name = name.Substring(1);
                    }
                    else if (pos >= name.Length - 1)
                    {
                        name = name.Substring(0, pos);
                        break;
                    }
                    else
                    {
                        name = name.Substring(0, pos) + name.Substring(pos + 1, 1).ToUpper() + name.Substring(pos + 2);
                    }
                }
            }
            ManagedName = name;
        }

        public override string ToString()
        {
            return Type.ToString() + ' ' + Name;
        }
    }
}
