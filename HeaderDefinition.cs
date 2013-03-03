using System.Collections.Generic;
using System.IO;

namespace BulletSharpGen
{
    class HeaderDefinition
    {
        public string Name { get; set; }
        public List<ClassDefinition> Classes { get; set; }
        public List<HeaderDefinition> Includes { get; set; }
        public List<EnumDefinition> Enums { get; set; }

        string managedName;
        public string ManagedName
        {
            get { return managedName != null ? managedName : Name; }
            set { managedName = value; }
        }

        public HeaderDefinition(string filename)
        {
            Name = Path.GetFileNameWithoutExtension(filename);
            Classes = new List<ClassDefinition>();
            Includes = new List<HeaderDefinition>();
            Enums = new List<EnumDefinition>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
