using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class GearConstraint : TypedConstraint
	{
        private RigidBody _rigidBodyA;
        private RigidBody _rigidBodyB;

		internal GearConstraint(IntPtr native)
			: base(native)
		{
		}

        public GearConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 axisInA, ref Vector3 axisInB)
            : base(btGearConstraint_new(rbA._native, rbB._native, ref axisInA, ref axisInB))
        {
            _rigidBodyA = rbA;
            _rigidBodyB = rbB;
        }

		public GearConstraint(RigidBody rbA, RigidBody rbB, Vector3 axisInA, Vector3 axisInB)
			: this(rbA, rbB, ref axisInA, ref axisInB)
		{
		}

        public GearConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 axisInA, ref Vector3 axisInB, float ratio)
            : base(btGearConstraint_new2(rbA._native, rbB._native, ref axisInA, ref axisInB, ratio))
        {
            _rigidBodyA = rbA;
            _rigidBodyB = rbB;
        }

		public GearConstraint(RigidBody rbA, RigidBody rbB, Vector3 axisInA, Vector3 axisInB, float ratio)
			: this(rbA, rbB, ref axisInA, ref axisInB, ratio)
		{
		}

		public Vector3 AxisA
		{
			get
			{
				Vector3 value;
				btGearConstraint_getAxisA(_native, out value);
				return value;
			}
			set { btGearConstraint_setAxisA(_native, ref value); }
		}

		public Vector3 AxisB
		{
			get
			{
				Vector3 value;
				btGearConstraint_getAxisB(_native, out value);
				return value;
			}
			set { btGearConstraint_setAxisB(_native, ref value); }
		}

		public float Ratio
		{
			get { return btGearConstraint_getRatio(_native); }
			set { btGearConstraint_setRatio(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGearConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 axisInA, [In] ref Vector3 axisInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGearConstraint_new2(IntPtr rbA, IntPtr rbB, [In] ref Vector3 axisInA, [In] ref Vector3 axisInB, float ratio);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGearConstraint_getAxisA(IntPtr obj, [Out] out Vector3 axisA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGearConstraint_getAxisB(IntPtr obj, [Out] out Vector3 axisB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btGearConstraint_getRatio(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGearConstraint_setAxisA(IntPtr obj, [In] ref Vector3 axisA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btGearConstraint_setAxisB(IntPtr obj, [In] ref Vector3 axisB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGearConstraint_setRatio(IntPtr obj, float ratio);
	}
}
