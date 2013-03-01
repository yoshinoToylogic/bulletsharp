using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class TypedConstraint
    {
        internal IntPtr _native;

        public TypedConstraint(IntPtr native)
        {
            _native = native;
        }
        /*
        public void getInfo2(ref btConstraintInfo2 b)
        {
            BulletAPI_BtTypedConstraint_getInfo2(_native, b._native);
        }
        */
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void BulletAPI_BtTypedConstraint_getInfo2(IntPtr obj, IntPtr b);
    }
}
