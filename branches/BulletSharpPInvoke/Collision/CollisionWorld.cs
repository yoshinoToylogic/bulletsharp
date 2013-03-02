using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class RayResultCallback : IDisposable
    {
        internal IntPtr _native;

        public float AddSingleResult(IntPtr rayResult, bool normalInWorldSpace)
        {
            return btCollisionWorld_RayResultCallback_addSingleResult(_native, rayResult, normalInWorldSpace);
        }

        public bool NeedsCollision(BroadphaseProxy proxy0)
        {
            return btCollisionWorld_RayResultCallback_needsCollision(_native, proxy0._native);
        }

        public float ClosestHitFraction
        {
            get { return btCollisionWorld_RayResultCallback_getClosestHitFraction(_native); }
            set { btCollisionWorld_RayResultCallback_setClosestHitFraction(_native, value); }
        }

        public short CollisionFilterGroup
        {
            get { return btCollisionWorld_RayResultCallback_getCollisionFilterGroup(_native); }
            set { btCollisionWorld_RayResultCallback_setCollisionFilterGroup(_native, value); }
        }

        public short CollisionFilterMask
        {
            get { return btCollisionWorld_RayResultCallback_getCollisionFilterMask(_native); }
            set { btCollisionWorld_RayResultCallback_setCollisionFilterMask(_native, value); }
        }

        public CollisionObject CollisionObject
        {
            get { return CollisionObject.GetManaged(btCollisionWorld_RayResultCallback_getCollisionObject(_native)); }
            set { btCollisionWorld_RayResultCallback_setCollisionObject(_native, value._native); }
        }

        public uint Flags
        {
            get { return btCollisionWorld_RayResultCallback_getFlags(_native); }
            set { btCollisionWorld_RayResultCallback_setFlags(_native, value); }
        }

        public bool HasHit
        {
            get { return btCollisionWorld_RayResultCallback_hasHit(_native); }
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
                btCollisionWorld_RayResultCallback_delete(_native);
                _native = IntPtr.Zero;

            }
        }

        ~RayResultCallback()
        {
            Dispose(false);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_RayResultCallback_delete(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionWorld_RayResultCallback_addSingleResult(IntPtr obj, IntPtr rayResult, bool normalInWorldSpace);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern float btCollisionWorld_RayResultCallback_getClosestHitFraction(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern short btCollisionWorld_RayResultCallback_getCollisionFilterGroup(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern short btCollisionWorld_RayResultCallback_getCollisionFilterMask(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_RayResultCallback_getCollisionObject(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern uint btCollisionWorld_RayResultCallback_getFlags(IntPtr obj);
	    [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionWorld_RayResultCallback_hasHit(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern bool btCollisionWorld_RayResultCallback_needsCollision(IntPtr obj, IntPtr proxy0);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern void btCollisionWorld_RayResultCallback_setClosestHitFraction(IntPtr obj, float closestHitFraction);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(IntPtr obj, short collisionFilterGroup);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern void btCollisionWorld_RayResultCallback_setCollisionFilterMask(IntPtr obj, short collisionFilterMask);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_RayResultCallback_setCollisionObject(IntPtr obj, IntPtr collisionObject);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_RayResultCallback_setFlags(IntPtr obj, uint flags);
    }

    public class ClosestRayResultCallback : RayResultCallback
    {
        public ClosestRayResultCallback(ref Vector3 rayFromWorld, ref Vector3 rayToWorld)
        {
            _native = btCollisionWorld_ClosestRayResultCallback_new(ref rayFromWorld, ref rayToWorld);
        }

        public ClosestRayResultCallback(Vector3 rayFromWorld, Vector3 rayToWorld)
        {
            _native = btCollisionWorld_ClosestRayResultCallback_new(ref rayFromWorld, ref rayToWorld);
        }

        public Vector3 HitNormalWorld
        {
            get
            {
                Vector3 hitNormalWorld;
                btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(_native, out hitNormalWorld);
                return hitNormalWorld;
            }
            set { btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(_native, ref value); }
        }

        public Vector3 HitPointWorld
        {
            get
            {
                Vector3 hitPointWorld;
                btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(_native, out hitPointWorld);
                return hitPointWorld;
            }
            set { btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(_native, ref value); }
        }

        public Vector3 RayFromWorld
        {
            get
            {
                Vector3 rayFromWorld;
                btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(_native, out rayFromWorld);
                return rayFromWorld;
            }
            set { btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(_native, ref value); }
        }

        public Vector3 RayToWorld
        {
            get
            {
                Vector3 rayToWorld;
                btCollisionWorld_ClosestRayResultCallback_getRayToWorld(_native, out rayToWorld);
                return rayToWorld;
            }
            set { btCollisionWorld_ClosestRayResultCallback_setRayToWorld(_native, ref value); }
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_ClosestRayResultCallback_new([In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(IntPtr obj, [Out] out Vector3 rayFromWorld);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ClosestRayResultCallback_getRayToWorld(IntPtr obj, [Out] out Vector3 rayToWorld);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(IntPtr obj, [Out] out Vector3 hitNormalWorld);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(IntPtr obj, [Out] out Vector3 hitPointWorld);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(IntPtr obj, [In] ref Vector3 rayFromWorld);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ClosestRayResultCallback_setRayToWorld(IntPtr obj, [In] ref Vector3 rayToWorld);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(IntPtr obj, [In] ref Vector3 hitNormalWorld);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(IntPtr obj, [In] ref Vector3 hitPointWorld);
    }

    public class CollisionWorld : IDisposable
    {
        protected IntPtr _native;
        AlignedCollisionObjectArray _collisionObjectArray;

        protected CollisionConfiguration _collisionConfiguration;
        protected Dispatcher _dispatcher;
        protected BroadphaseInterface _broadphase;

        public CollisionWorld()
        {
            _native = btCollisionWorld_new();
        }

        public CollisionWorld(IntPtr native)
        {
            _native = native;
        }

        public void addCollisionObject(CollisionObject collisionObject, int collisionFilterGroup, int collisionFilterMask)
        {
            btCollisionWorld_addCollisionObject(_native, collisionObject._native, collisionFilterGroup,
                                                          collisionFilterMask);
        }

        public void DebugDrawWorld()
        {
        }

        public void RayTest(ref Vector3 rayFromWorld, ref Vector3 rayToWorld, RayResultCallback resultCallback)
        {
            btCollisionWorld_rayTest(_native, ref rayFromWorld, ref rayToWorld, resultCallback._native);
        }

        public void RemoveCollisionObject(CollisionObject obj)
        {
            btCollisionWorld_removeCollisionObject(_native, obj._native);

        }

        public BroadphaseInterface Broadphase
        {
            get { return _broadphase; }
        }

        public AlignedCollisionObjectArray CollisionObjectArray
        {
            get
            {
                if (_collisionObjectArray == null)
                {
                    _collisionObjectArray = new AlignedCollisionObjectArray(btCollisionWorld_getCollisionObjectArray(_native));
                }
                return _collisionObjectArray;
            }
        }

        public IDebugDraw DebugDrawer
        {
            get { return null; }
            set { }
        }

        public Dispatcher Dispatcher
        {
            get { return _dispatcher; }
        }

        public int NumCollisionObjects
        {
            get { return btCollisionWorld_getNumCollisionObjects(_native); }
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_new();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_addCollisionObject(IntPtr obj, IntPtr collisionObject, int collisionFilterGroup, int collisionFilterMask);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_getBroadphase(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_getCollisionObjectArray(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_getPairCache(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_getDispatcher(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btCollisionWorld_getNumCollisionObjects(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_rayTest(IntPtr obj, [In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld, IntPtr resultCallback);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_removeCollisionObject(IntPtr obj, IntPtr collisionObject);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_delete(IntPtr obj);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_native != IntPtr.Zero)
            {
                btCollisionWorld_delete(_native);
                _native = IntPtr.Zero;

            }
        }

        ~CollisionWorld()
        {
            Dispose(false);
        }
    }
}
