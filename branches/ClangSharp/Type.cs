using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClangSharp;

namespace ClangSharp {
    public class Type {

        internal Interop.Type Native { get; private set; }

        internal Type(Interop.Type native) {
            Native = native;
        }

        public TypeKind Kind
        {
            get { return Native.kind; }
        }

        public Type ArrayElementType
        {
            get { return new Type(Interop.clang_getArrayElementType(Native)); }
        }

        public Type Canonical {
            get { return new Type(Interop.clang_getCanonicalType(Native)); }
        }

        public Type Pointee {
            get { return new Type(Interop.clang_getPointeeType(Native)); }
        }

        public Type Result {
            get { return new Type(Interop.clang_getResultType(Native)); }
        }

        public Cursor Declaration {
            get { return new Cursor(Interop.clang_getTypeDeclaration(Native)); }
        }

        public bool IsPodType
        {
            get { return Interop.clang_isPODType(Native); }
        }

        public static string TypeKindSpelling(TypeKind kind) {
            return Interop.clang_getTypeKindSpelling(kind).ManagedString;
        }

        public bool Equals(Type other) {
            return Interop.clang_equalTypes(Native, other.Native) != 0;
        }

        public override bool Equals(object obj) {
            return obj is Type && Equals((Type)obj);
        }

        public override int GetHashCode() {
            return Native.GetHashCode();
        }
    }
}
