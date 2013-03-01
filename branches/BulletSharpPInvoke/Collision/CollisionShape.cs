using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class CollisionShape : IDisposable
    {
        internal IntPtr _native;

        internal static CollisionShape GetManaged(IntPtr obj)
        {
            if (obj == IntPtr.Zero)
            {
                return null;
            }

            IntPtr userPtr = btCollisionShape_getUserPointer(obj);
            if (userPtr != IntPtr.Zero)
            {
                return GCHandle.FromIntPtr(userPtr).Target as CollisionShape;
            }

            switch (btCollisionShape_getShapeType(obj))
            {
                case BroadphaseNativeType.BoxShape:
                    return new BoxShape(obj);
            }

            return new CollisionShape(obj);
        }

        internal CollisionShape(IntPtr obj)
        {
            _native = obj;
            if (btCollisionShape_getUserPointer(_native) == IntPtr.Zero)
            {
                GCHandle handle = GCHandle.Alloc(this);
                btCollisionShape_setUserPointer(_native, GCHandle.ToIntPtr(handle));
            }
        }

        public CollisionShape()
        {
            _native = CreateCollisionShape();
        }

        public Vector3 CalculateLocalInertia(float mass)
        {
            Vector3 inertia;
            btCollisionShape_calculateLocalInertia(_native, mass, out inertia);
            return inertia;
        }

        public void CalculateLocalInertia(float mass, out Vector3 inertia)
        {
            btCollisionShape_calculateLocalInertia(_native, mass, out inertia);
        }

        public float getAngularMotionDisk()
        {
            return btCollisionShape_getAngularMotionDisk(_native);
        }

        public Vector3 LocalScaling
        {
            get
            {
                Vector3 scaling;
                btCollisionShape_getLocalScaling(_native, out scaling);
                return scaling;
            }
            set { btCollisionShape_setLocalScaling(_native, ref value); }
        }

        public float getContactBreakingThreshold()
        {
            return btCollisionShape_getContactBreakingThreshold(_native);
        }

        public float Margin
        {
            get { return btCollisionShape_getMargin(_native); }
            set { btCollisionShape_setMargin(_native, value); }
        }

        public BroadphaseNativeType ShapeType
        {
            get { return btCollisionShape_getShapeType(_native); }
        }

        public bool IsCompound
        {
            get { return btCollisionShape_isCompound(_native); }
        }

        public bool IsConcave
        {
            get { return btCollisionShape_isConcave(_native); }
        }

        public bool IsConvex
        {
            get { return btCollisionShape_isConvex(_native); }
        }

        public bool IsInfinite
        {
            get { return btCollisionShape_isInfinite(_native); }
        }

        public bool IsPolyhedral
        {
            get { return btCollisionShape_isPolyhedral(_native); }
        }

        #region Native Invokes
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr CreateCollisionShape();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionShape_calculateLocalInertia(IntPtr obj, float mass, [Out] out Vector3 inertia);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionShape_getAngularMotionDisk(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionShape_getLocalScaling(IntPtr obj, [Out] out Vector3 scaling);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionShape_getContactBreakingThreshold(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionShape_getMargin(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern BroadphaseNativeType btCollisionShape_getShapeType(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionShape_getUserPointer(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionShape_isCompound(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionShape_isConcave(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionShape_isConvex(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionShape_isInfinite(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionShape_isPolyhedral(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionShape_setLocalScaling(IntPtr obj, [In] ref Vector3 scaling);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionShape_setMargin(IntPtr obj, float margin);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionShape_setUserPointer(IntPtr obj, IntPtr ptr);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionShape_delete(IntPtr obj);

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_native != IntPtr.Zero)
            {
                btCollisionShape_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~CollisionShape()
        {
            Dispose(false);
        }
    }
}
