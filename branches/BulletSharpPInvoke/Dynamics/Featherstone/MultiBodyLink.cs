using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MultiBodyLink
	{
		internal IntPtr _native;

		internal MultiBodyLink(IntPtr native)
		{
			_native = native;
		}

		public void UpdateCache()
		{
			btMultibodyLink_updateCache(_native);
		}

		public Vector3 AppliedForce
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getApplied_force(_native, out value);
				return value;
			}
			set { btMultibodyLink_setApplied_force(_native, ref value); }
		}

		public Vector3 AppliedTorque
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getApplied_torque(_native, out value);
				return value;
			}
			set { btMultibodyLink_setApplied_torque(_native, ref value); }
		}

		public Vector3 AxisBottom
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getAxis_bottom(_native, out value);
				return value;
			}
			set { btMultibodyLink_setAxis_bottom(_native, ref value); }
		}

		public Vector3 AxisTop
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getAxis_top(_native, out value);
				return value;
			}
			set { btMultibodyLink_setAxis_top(_native, ref value); }
		}

		public Quaternion CachedRotParentToThis
		{
			get
			{
				Quaternion value;
				btMultibodyLink_getCached_rot_parent_to_this(_native, out value);
				return value;
			}
			set { btMultibodyLink_setCached_rot_parent_to_this(_native, ref value); }
		}

		public Vector3 CachedRVector
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getCached_r_vector(_native, out value);
				return value;
			}
			set { btMultibodyLink_setCached_r_vector(_native, ref value); }
		}

		public MultiBodyLinkCollider Collider
		{
            get { return CollisionObject.GetManaged(btMultibodyLink_getCollider(_native)) as MultiBodyLinkCollider; }
			set { btMultibodyLink_setCollider(_native, value._native); }
		}

		public Vector3 DVector
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getD_vector(_native, out value);
				return value;
			}
			set { btMultibodyLink_setD_vector(_native, ref value); }
		}

		public Vector3 EVector
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getE_vector(_native, out value);
				return value;
			}
			set { btMultibodyLink_setE_vector(_native, ref value); }
		}

		public int Flags
		{
			get { return btMultibodyLink_getFlags(_native); }
			set { btMultibodyLink_setFlags(_native, value); }
		}

		public Vector3 Inertia
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getInertia(_native, out value);
				return value;
			}
			set { btMultibodyLink_setInertia(_native, ref value); }
		}

		public bool IsRevolute
		{
			get { return btMultibodyLink_getIs_revolute(_native); }
			set { btMultibodyLink_setIs_revolute(_native, value); }
		}

		public float JointPos
		{
			get { return btMultibodyLink_getJoint_pos(_native); }
			set { btMultibodyLink_setJoint_pos(_native, value); }
		}

		public float JointTorque
		{
			get { return btMultibodyLink_getJoint_torque(_native); }
			set { btMultibodyLink_setJoint_torque(_native, value); }
		}

		public float Mass
		{
			get { return btMultibodyLink_getMass(_native); }
			set { btMultibodyLink_setMass(_native, value); }
		}

		public int Parent
		{
			get { return btMultibodyLink_getParent(_native); }
			set { btMultibodyLink_setParent(_native, value); }
		}

		public Quaternion ZeroRotParentToThis
		{
			get
			{
				Quaternion value;
				btMultibodyLink_getZero_rot_parent_to_this(_native, out value);
				return value;
			}
			set { btMultibodyLink_setZero_rot_parent_to_this(_native, ref value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultibodyLink_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultibodyLink_getApplied_force(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultibodyLink_getApplied_torque(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultibodyLink_getAxis_bottom(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultibodyLink_getAxis_top(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultibodyLink_getCached_r_vector(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultibodyLink_getCached_rot_parent_to_this(IntPtr obj, [Out] out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultibodyLink_getCollider(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultibodyLink_getD_vector(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getE_vector(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultibodyLink_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getInertia(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btMultibodyLink_getIs_revolute(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultibodyLink_getJoint_pos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultibodyLink_getJoint_torque(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultibodyLink_getMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultibodyLink_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getZero_rot_parent_to_this(IntPtr obj, [Out] out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setApplied_force(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setApplied_torque(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setAxis_bottom(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setAxis_top(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setCached_r_vector(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setCached_rot_parent_to_this(IntPtr obj, [In] ref Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setCollider(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setD_vector(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setE_vector(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setFlags(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setInertia(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setIs_revolute(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setJoint_pos(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setJoint_torque(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setMass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setParent(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setZero_rot_parent_to_this(IntPtr obj, [In] ref Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_updateCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_delete(IntPtr obj);
	}
}
