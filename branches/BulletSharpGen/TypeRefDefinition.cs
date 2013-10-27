using System;
using ClangSharp;

namespace BulletSharpGen
{
    class TypeRefDefinition
    {
        public string Name { get; private set; }
        public bool IsBasic { get; set; }
        public bool IsPointer { get; set; }
        public bool IsReference { get; set; }
        public bool IsConstantArray { get; set; }
        public bool IsConst { get; set; }
        public TypeRefDefinition Referenced { get; set; }
        public bool HasTemplateTypeParameter { get; set; }
        public TypeRefDefinition SpecializedTemplateType { get; set; }

        public ClassDefinition Target { get; set; }

        public string FullName
        {
            get
            {
                if (Target != null)
                {
                    return Target.FullName;
                }
                return Name;
            }
        }

        public string ManagedName
        {
            get
            {
                if (IsBasic)
                {
                    if (Name.Equals("unsigned char"))
                    {
                        return "byte";
                    }
                    if (Name.Equals("unsigned short"))
                    {
                        return "ushort";
                    }
                    else if (Name.Equals("unsigned int"))
                    {
                        return "uint";
                    }
                    else if (Name.Equals("unsigned long"))
                    {
                        return "ulong";
                    }
                    if (Referenced != null)
                    {
                        return Referenced.ManagedName;
                    }
                    return Name;
                }
                if (HasTemplateTypeParameter)
                {
                    return "T";
                }
                if (IsPointer || IsReference || IsConstantArray)
                {
                    return Referenced.ManagedName;
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
                if (IsPointer || IsReference || IsConstantArray)
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
            switch (type.TypeKind)
            {
                case ClangSharp.Type.Kind.Void:
                case ClangSharp.Type.Kind.Bool:
                case ClangSharp.Type.Kind.CharS:
                case ClangSharp.Type.Kind.Double:
                case ClangSharp.Type.Kind.Float:
                case ClangSharp.Type.Kind.Int:
                case ClangSharp.Type.Kind.UChar:
                case ClangSharp.Type.Kind.UInt:
                    Name = type.Spelling;
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.Long:
                    Name = "long";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.LongLong:
                    Name = "long long";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.Short:
                    Name = "short";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.ULong:
                    Name = "unsigned long";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.UShort:
                    Name = "unsigned short";
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.Typedef:
                    Name = type.Declaration.Spelling;
                    Referenced = new TypeRefDefinition(type.Canonical);
                    IsBasic = Referenced.IsBasic;
                    break;
                case ClangSharp.Type.Kind.Pointer:
                    Referenced = new TypeRefDefinition(type.Pointee);
                    IsPointer = true;
                    break;
                case ClangSharp.Type.Kind.LValueReference:
                    Referenced = new TypeRefDefinition(type.Pointee);
                    IsReference = true;
                    break;
                case ClangSharp.Type.Kind.ConstantArray:
                    Referenced = new TypeRefDefinition(type.ArrayElementType);
                    IsConstantArray = true;
                    break;
                case ClangSharp.Type.Kind.FunctionProto:
                    // ??
                    break;
                case ClangSharp.Type.Kind.Enum:
                    Name = type.Canonical.Declaration.Spelling;
                    IsBasic = true;
                    break;
                case ClangSharp.Type.Kind.Record:
                    Name = type.Canonical.Declaration.Spelling;
                    break;
                case ClangSharp.Type.Kind.Unexposed:
                    if (type.Canonical.Declaration.IsInvalid)
                    {
                        Name = "[unexposed type]";
                    }
                    else
                    {
                        Name = type.Canonical.Declaration.Spelling;
                    }
                    break;
                case ClangSharp.Type.Kind.DependentSizedArray:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public TypeRefDefinition(string name)
        {
            Name = name;
        }

        public TypeRefDefinition()
        {
            Name = "void";
            IsBasic = true;
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

            if (t.IsBasic != IsBasic ||
                t.IsConstantArray != IsConstantArray ||
                t.IsPointer != IsPointer ||
                t.IsReference != IsReference)
            {
                return false;
            }

            if (IsPointer || IsReference || IsConstantArray)
            {
                return t.Referenced.Equals(Referenced);
            }

            return t.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return ManagedName;
        }
    }
}
