using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
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
        /*
        public void RayTest(Vector3 rayFromWorld, Vector3 rayToWorld, RayResultCallback resultCallback)
        {
            btCollisionWorld_rayTest(_native, rayFromWorld, rayToWorld, resultCallback.Handle);
        }
        */
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
        static extern void btCollisionWorld_rayTest(IntPtr obj, IntPtr rayFromWorld, IntPtr rayToWorld, IntPtr resultCallback);
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
