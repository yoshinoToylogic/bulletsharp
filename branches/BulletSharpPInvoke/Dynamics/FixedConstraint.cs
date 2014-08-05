using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class FixedConstraint : TypedConstraint
	{
        private RigidBody _rigidBodyA;
        private RigidBody _rigidBodyB;

		internal FixedConstraint(IntPtr native)
			: base(native)
		{
		}

        public FixedConstraint(RigidBody rbA, RigidBody rbB, ref Matrix frameInA, ref Matrix frameInB)
            : base(btFixedConstraint_new(rbA._native, rbB._native, ref frameInA, ref frameInB))
        {
            _rigidBodyA = rbA;
            _rigidBodyB = rbB;
        }

		public FixedConstraint(RigidBody rbA, RigidBody rbB, Matrix frameInA, Matrix frameInB)
			: this(rbA, rbB, ref frameInA, ref frameInB)
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btFixedConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB);
	}
}
