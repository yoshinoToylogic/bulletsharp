﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulletSharpGen
{
    class HeaderDefinition
    {
        public string Name { get; set; }
        public string Filename { get; set; }
        public List<ClassDefinition> Classes { get; set; }
        public List<HeaderDefinition> Includes { get; set; }
        public List<EnumDefinition> Enums { get; set; }

        string _managedName;
        public string ManagedName
        {
            get { return _managedName ?? Name; }
            set { _managedName = value; }
        }

        public IEnumerable<ClassDefinition> AllSubClasses
        {
            get
            {
                List<ClassDefinition> subClasses = new List<ClassDefinition>();
                foreach (ClassDefinition cl in Classes)
                {
                    subClasses.AddRange(cl.AllSubClasses);
                    subClasses.Add(cl);
                }
                return subClasses;
            }
        }

        public bool IsExcluded
        {
            get { return Classes.All(x => x.IsExcluded); }
        }

        public HeaderDefinition(string filename)
        {
            Name = Path.GetFileNameWithoutExtension(filename);
            Filename = filename;
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
