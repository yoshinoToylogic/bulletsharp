using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class ConstraintRow
	{
		internal IntPtr _native;

		internal ConstraintRow(IntPtr native)
		{
			_native = native;
		}

		public ConstraintRow()
		{
			_native = btConstraintRow_new();
		}

		public float AccumImpulse
		{
			get { return btConstraintRow_getAccumImpulse(_native); }
			set { btConstraintRow_setAccumImpulse(_native, value); }
		}

		public float JacDiagInv
		{
			get { return btConstraintRow_getJacDiagInv(_native); }
			set { btConstraintRow_setJacDiagInv(_native, value); }
		}

		public float LowerLimit
		{
			get { return btConstraintRow_getLowerLimit(_native); }
			set { btConstraintRow_setLowerLimit(_native, value); }
		}
        /*
		public float Normal
		{
			get { return btConstraintRow_getNormal(_native); }
			set { btConstraintRow_setNormal(_native, value._native); }
		}
        */
		public float Rhs
		{
			get { return btConstraintRow_getRhs(_native); }
			set { btConstraintRow_setRhs(_native, value); }
		}

		public float UpperLimit
		{
			get { return btConstraintRow_getUpperLimit(_native); }
			set { btConstraintRow_setUpperLimit(_native, value); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btConstraintRow_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ConstraintRow()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConstraintRow_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConstraintRow_getAccumImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConstraintRow_getJacDiagInv(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConstraintRow_getLowerLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConstraintRow_getNormal(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConstraintRow_getRhs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConstraintRow_getUpperLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintRow_setAccumImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintRow_setJacDiagInv(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintRow_setLowerLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintRow_setNormal(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintRow_setRhs(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintRow_setUpperLimit(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintRow_delete(IntPtr obj);
	}

	public class ManifoldPoint
	{
		internal IntPtr _native;
        bool _preventDelete;

        internal ManifoldPoint(IntPtr native, bool preventDelete = false)
		{
			_native = native;
            _preventDelete = preventDelete;
		}

		public ManifoldPoint()
		{
			_native = btManifoldPoint_new();
		}

		public ManifoldPoint(Vector3 pointA, Vector3 pointB, Vector3 normal, float distance)
		{
			_native = btManifoldPoint_new2(ref pointA, ref pointB, ref normal, distance);
		}

		public float AppliedImpulse
		{
			get { return btManifoldPoint_getAppliedImpulse(_native); }
			set { btManifoldPoint_setAppliedImpulse(_native, value); }
		}

		public float AppliedImpulseLateral1
		{
			get { return btManifoldPoint_getAppliedImpulseLateral1(_native); }
			set { btManifoldPoint_setAppliedImpulseLateral1(_native, value); }
		}

		public float AppliedImpulseLateral2
		{
			get { return btManifoldPoint_getAppliedImpulseLateral2(_native); }
			set { btManifoldPoint_setAppliedImpulseLateral2(_native, value); }
		}

		public float CombinedFriction
		{
			get { return btManifoldPoint_getCombinedFriction(_native); }
			set { btManifoldPoint_setCombinedFriction(_native, value); }
		}

		public float CombinedRestitution
		{
			get { return btManifoldPoint_getCombinedRestitution(_native); }
			set { btManifoldPoint_setCombinedRestitution(_native, value); }
		}

		public float CombinedRollingFriction
		{
			get { return btManifoldPoint_getCombinedRollingFriction(_native); }
			set { btManifoldPoint_setCombinedRollingFriction(_native, value); }
		}

		public float ContactCFM1
		{
			get { return btManifoldPoint_getContactCFM1(_native); }
			set { btManifoldPoint_setContactCFM1(_native, value); }
		}

		public float ContactCFM2
		{
			get { return btManifoldPoint_getContactCFM2(_native); }
			set { btManifoldPoint_setContactCFM2(_native, value); }
		}

		public float ContactMotion1
		{
			get { return btManifoldPoint_getContactMotion1(_native); }
			set { btManifoldPoint_setContactMotion1(_native, value); }
		}

		public float ContactMotion2
		{
			get { return btManifoldPoint_getContactMotion2(_native); }
			set { btManifoldPoint_setContactMotion2(_native, value); }
		}

		public float Distance
		{
			get { return btManifoldPoint_getDistance(_native); }
			set { btManifoldPoint_setDistance(_native, value); }
		}

		public float Distance1
		{
			get { return btManifoldPoint_getDistance1(_native); }
			set { btManifoldPoint_setDistance1(_native, value); }
		}

		public int Index0
		{
			get { return btManifoldPoint_getIndex0(_native); }
			set { btManifoldPoint_setIndex0(_native, value); }
		}

		public int Index1
		{
			get { return btManifoldPoint_getIndex1(_native); }
			set { btManifoldPoint_setIndex1(_native, value); }
		}

        public Vector3 LateralFrictionDir1
		{
            get
            {
                Vector3 value;
                btManifoldPoint_getLateralFrictionDir1(_native, out value);
                return value;
            }
			set { btManifoldPoint_setLateralFrictionDir1(_native, ref value); }
		}

		public Vector3 LateralFrictionDir2
		{
            get
            {
                Vector3 value;
                btManifoldPoint_getLateralFrictionDir2(_native, out value);
                return value;
            }
			set { btManifoldPoint_setLateralFrictionDir2(_native, ref value); }
		}

		public bool LateralFrictionInitialized
		{
			get { return btManifoldPoint_getLateralFrictionInitialized(_native); }
			set { btManifoldPoint_setLateralFrictionInitialized(_native, value); }
		}

		public int LifeTime
		{
			get { return btManifoldPoint_getLifeTime(_native); }
			set { btManifoldPoint_setLifeTime(_native, value); }
		}

        public Vector3 LocalPointA
		{
            get
            {
                Vector3 value;
                btManifoldPoint_getLocalPointA(_native, out value);
                return value;
            }
			set { btManifoldPoint_setLocalPointA(_native, ref value); }
		}

		public Vector3 LocalPointB
		{
            get
            {
                Vector3 value;
                btManifoldPoint_getLocalPointB(_native, out value);
                return value;
            }
			set { btManifoldPoint_setLocalPointB(_native, ref value); }
		}

        public Vector3 NormalWorldOnB
		{
            get
            {
                Vector3 value;
                btManifoldPoint_getNormalWorldOnB(_native, out value);
                return value;
            }
			set { btManifoldPoint_setNormalWorldOnB(_native, ref value); }
		}

		public int PartId0
		{
			get { return btManifoldPoint_getPartId0(_native); }
			set { btManifoldPoint_setPartId0(_native, value); }
		}

		public int PartId1
		{
			get { return btManifoldPoint_getPartId1(_native); }
			set { btManifoldPoint_setPartId1(_native, value); }
		}

		public Vector3 PositionWorldOnA
		{
            get
            {
                Vector3 value;
                btManifoldPoint_getPositionWorldOnA(_native, out value);
                return value;
            }
			set { btManifoldPoint_setPositionWorldOnA(_native, ref value); }
		}

        public Vector3 PositionWorldOnB
		{
            get
            {
                Vector3 value;
                btManifoldPoint_getPositionWorldOnB(_native, out value);
                return value;
            }
			set { btManifoldPoint_setPositionWorldOnB(_native, ref value); }
		}

		public IntPtr UserPersistentData
		{
			get { return btManifoldPoint_getUserPersistentData(_native); }
			set { btManifoldPoint_setUserPersistentData(_native, value); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
                if (!_preventDelete)
                {
                    btManifoldPoint_delete(_native);
                }
				_native = IntPtr.Zero;
			}
		}

		~ManifoldPoint()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldPoint_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btManifoldPoint_new2([In] ref Vector3 pointA, [In] ref Vector3 pointB, [In] ref Vector3 normal, float distance);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getAppliedImpulseLateral1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getAppliedImpulseLateral2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getCombinedFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getCombinedRestitution(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getCombinedRollingFriction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getContactCFM1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getContactCFM2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getContactMotion1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getContactMotion2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btManifoldPoint_getDistance1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btManifoldPoint_getIndex0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btManifoldPoint_getIndex1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_getLateralFrictionDir1(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_getLateralFrictionDir2(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btManifoldPoint_getLateralFrictionInitialized(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btManifoldPoint_getLifeTime(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_getLocalPointA(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_getLocalPointB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_getNormalWorldOnB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btManifoldPoint_getPartId0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btManifoldPoint_getPartId1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_getPositionWorldOnA(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btManifoldPoint_getPositionWorldOnB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btManifoldPoint_getUserPersistentData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setAppliedImpulse(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setAppliedImpulseLateral1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setAppliedImpulseLateral2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setCombinedFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setCombinedRestitution(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setCombinedRollingFriction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setContactCFM1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setContactCFM2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setContactMotion1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setContactMotion2(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setDistance(IntPtr obj, float dist);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setDistance1(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setIndex0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setIndex1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_setLateralFrictionDir1(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_setLateralFrictionDir2(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setLateralFrictionInitialized(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setLifeTime(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_setLocalPointA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_setLocalPointB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_setNormalWorldOnB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setPartId0(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setPartId1(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btManifoldPoint_setPositionWorldOnA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setPositionWorldOnB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_setUserPersistentData(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btManifoldPoint_delete(IntPtr obj);
	}
}
