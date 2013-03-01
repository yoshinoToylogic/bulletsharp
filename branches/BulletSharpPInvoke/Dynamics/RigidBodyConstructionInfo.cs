using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class RigidBodyConstructionInfo : IDisposable
    {
        internal IntPtr _native;
        internal MotionState _motionState;

        public RigidBodyConstructionInfo(float mass, MotionState motionState, CollisionShape collisionShape, Vector3 localInertia)
        {
            _native = btRigidBodyConstructionInfo_new(mass, motionState._native, collisionShape._native, ref localInertia);
            _motionState = motionState;
        }

        public RigidBodyConstructionInfo(float mass, MotionState motionState, CollisionShape collisionShape)
        {
            _native = btRigidBodyConstructionInfo_new2(mass, motionState._native, collisionShape._native);
            _motionState = motionState;
        }

        public RigidBodyConstructionInfo(IntPtr native)
        {
            _native = native;
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btRigidBodyConstructionInfo_new(float mass, IntPtr motionState, IntPtr collisionShape, [In] ref Vector3 localInertia);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btRigidBodyConstructionInfo_new2(float mass, IntPtr motionState, IntPtr collisionShape);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBodyConstructionInfo_delete(IntPtr obj);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_native != IntPtr.Zero)
            {
                btRigidBodyConstructionInfo_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~RigidBodyConstructionInfo()
        {
            Dispose(false);
        }

    }
}
