using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp.SoftBody
{
	public class SoftRigidDynamicsWorld : DiscreteDynamicsWorld
	{
        private AlignedSoftBodyArray _softBodyArray;
        private SoftBodyWorldInfo _worldInfo;

		public SoftRigidDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache, ConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration)
			: base(btSoftRigidDynamicsWorld_new(dispatcher._native, pairCache._native, constraintSolver._native, collisionConfiguration._native))
		{
            _dispatcher = dispatcher;
            _broadphase = pairCache;
            _constraintSolver = constraintSolver;
		}
        /*
		public SoftRigidDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache, ConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration, SoftBodySolver softBodySolver)
			: base(btSoftRigidDynamicsWorld_new2(dispatcher._native, pairCache._native, constraintSolver._native, collisionConfiguration._native, softBodySolver._native))
		{
            _dispatcher = dispatcher;
            _broadphase = pairCache;
            _constraintSolver = constraintSolver;
		}
        */
		public void AddSoftBody(SoftBody body)
		{
            _collisionObjectArray.Add(body);
		}

        public void AddSoftBody(SoftBody body, CollisionFilterGroups collisionFilterGroup, CollisionFilterGroups collisionFilterMask)
		{
            _collisionObjectArray.Add(body, (short)collisionFilterGroup, (short)collisionFilterMask);
		}

        public void AddSoftBody(SoftBody body, short collisionFilterGroup, short collisionFilterMask)
        {
            _collisionObjectArray.Add(body, collisionFilterGroup, collisionFilterMask);
        }

		public void RemoveSoftBody(SoftBody body)
		{
            _collisionObjectArray.Remove(body);
		}

		public int DrawFlags
		{
			get { return btSoftRigidDynamicsWorld_getDrawFlags(_native); }
			set { btSoftRigidDynamicsWorld_setDrawFlags(_native, value); }
		}

		public AlignedSoftBodyArray SoftBodyArray
		{
            get
            {
                if (_softBodyArray == null)
                {
                    _softBodyArray = new AlignedSoftBodyArray(btSoftRigidDynamicsWorld_getSoftBodyArray(_native));
                }
                return _softBodyArray;
            }
		}

		public SoftBodyWorldInfo WorldInfo
		{
            get
            {
                if (_worldInfo == null)
                {
                    _worldInfo = new SoftBodyWorldInfo(btSoftRigidDynamicsWorld_getWorldInfo(_native), true);
                }
                return _worldInfo;
            }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftRigidDynamicsWorld_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftRigidDynamicsWorld_new2(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration, IntPtr softBodySolver);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSoftRigidDynamicsWorld_getDrawFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftRigidDynamicsWorld_getSoftBodyArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftRigidDynamicsWorld_getWorldInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftRigidDynamicsWorld_setDrawFlags(IntPtr obj, int f);
	}
}
