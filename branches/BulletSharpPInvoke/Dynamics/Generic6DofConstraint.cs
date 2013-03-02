using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class Generic6DofConstraint : TypedConstraint
    {
        public Generic6DofConstraint(RigidBody rbA, RigidBody rbB, Matrix frameInA, Matrix frameInB, bool useLinearReferenceFrameA)
            : base(btGeneric6DofConstraint_new(rbA._native, rbB._native, ref frameInA, ref frameInB, useLinearReferenceFrameA))
        {
        }

        public Generic6DofConstraint(RigidBody rbA, Matrix frameInA, bool useLinearReferenceFrameB)
            : base(btGeneric6DofConstraint_new2(rbA._native, ref frameInA, useLinearReferenceFrameB))
        {
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btGeneric6DofConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, bool useLinearReferenceFrameA);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btGeneric6DofConstraint_new2(IntPtr rbA, [In] ref Matrix frameInA, bool useLinearReferenceFrameB);
    }
}
