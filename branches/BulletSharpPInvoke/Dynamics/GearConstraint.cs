using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class GearConstraint : TypedConstraint
	{
		internal GearConstraint(IntPtr native)
			: base(native)
		{
		}

        public GearConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 axisInA, ref Vector3 axisInB, float ratio)
			: base(btGearConstraint_new(rbA._native, rbB._native, ref axisInA, ref axisInB, ratio))
		{
		}

		public GearConstraint(RigidBody rbA, RigidBody rbB, Vector3 axisInA, Vector3 axisInB)
			: base(btGearConstraint_new2(rbA._native, rbB._native, ref axisInA, ref axisInB))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btGearConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 axisInA, [In] ref Vector3 axisInB, float ratio);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btGearConstraint_new2(IntPtr rbA, IntPtr rbB, [In] ref Vector3 axisInA, [In] ref Vector3 axisInB);
	}
}
