using System;
using ClangSharp;

namespace BulletSharpGen
{
    class TypeRefDefinition
    {
        public string Name { get; private set; }
        public bool IsPointer { get; set; }
        public bool IsReference { get; set; }
        public bool IsBasic { get; set; }
        public bool IsConstantArray { get; set; }
        public bool HasTemplateTypeParameter { get; set; }
        public TypeRefDefinition SpecializedTemplateType { get; set; }

        public ClassDefinition Target { get; set; }
        public string ManagedName
        {
            get
            {
                if (IsBasic)
                {
                    return Name;
                }
                if (HasTemplateTypeParameter)
                {
                    return "T";
                }
                if (Target == null)
                {
                    Console.WriteLine("Unresolved reference to " + Name);
                    return Name;
                }
                if (SpecializedTemplateType != null)
                {
                    return Target.ManagedName + "<" + SpecializedTemplateType.ManagedName + ">";
                }
                return Target.ManagedName;
            }
        }
        public string ManagedTypeRefName
        {
            get
            {
                if (IsPointer || IsReference)
                {
                    if (IsBasic)
                    {
                        return ManagedName + '*';
                    }
                    else
                    {
                        return ManagedName + '^';
                    }
                }
                return ManagedName;
            }
        }

        public TypeRefDefinition(ClangSharp.Type type)
        {
            switch (type.Kind)
            {
                case TypeKind.Void:
                    Name = "void";
                    IsBasic = true;
                    break;
                case TypeKind.Bool:
                    Name = "bool";
                    IsBasic = true;
                    break;
                case TypeKind.Double:
                    Name = "double";
                    IsBasic = true;
                    break;
                case TypeKind.Float:
                    Name = "float";
                    IsBasic = true;
                    break;
                case TypeKind.Int:
                    Name = "int";
                    IsBasic = true;
                    break;
                case TypeKind.Long:
                    Name = "long";
                    IsBasic = true;
                    break;
                case TypeKind.Short:
                    Name = "short";
                    IsBasic = true;
                    break;
                case TypeKind.UInt:
                    Name = "unsigned int";
                    IsBasic = true;
                    break;
                case TypeKind.ULong:
                    Name = "unsigned long";
                    IsBasic = true;
                    break;
                case TypeKind.Char_S:
                    Name = "char";
                    IsBasic = true;
                    break;
                case TypeKind.UChar:
                    Name = "unsigned char";
                    IsBasic = true;
                    break;
                case TypeKind.UShort:
                    Name = "unsigned short";
                    IsBasic = true;
                    break;
                case TypeKind.Typedef:
                    Name = type.Declaration.Spelling;
                    break;
                case TypeKind.Pointer:
                    var pp = new TypeRefDefinition(type.Pointee);
                    Name = pp.Name;
                    IsBasic = pp.IsBasic;
                    IsPointer = true;
                    break;
                case TypeKind.LValueReference:
                    var rp = new TypeRefDefinition(type.Pointee);
                    Name = rp.Name;
                    IsBasic = rp.IsBasic;
                    IsReference = true;
                    break;
                case TypeKind.Enum:
                case TypeKind.Record:
                case TypeKind.Unexposed:
                    Name = type.Declaration.Spelling;
                    break;
                case TypeKind.ConstantArray:
                    var ca = new TypeRefDefinition(type.ArrayElementType);
                    Name = ca.Name;
                    IsBasic = ca.IsBasic;
                    IsConstantArray = true;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public TypeRefDefinition(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            TypeRefDefinition t = obj as TypeRefDefinition;
            if ((System.Object)t == null)
            {
                return false;
            }

            return t.Name.Equals(Name) &&
                t.IsConstantArray == IsConstantArray &&
                t.IsPointer == IsPointer &&
                t.IsReference == IsReference;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
