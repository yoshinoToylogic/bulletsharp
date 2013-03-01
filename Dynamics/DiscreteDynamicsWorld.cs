using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class DiscreteDynamicsWorld : DynamicsWorld, IDisposable
    {
        public DiscreteDynamicsWorld(IntPtr native)
            : base(native)
        {
        }

        public DiscreteDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache, ConstraintSolver solver, CollisionConfiguration collisionConfiguration)
            : base(btDiscreteDynamicsWorld_new(
            dispatcher != null ? dispatcher._native : IntPtr.Zero,
            pairCache != null ? pairCache._native : IntPtr.Zero,
            solver != null ? solver._native : IntPtr.Zero,
            collisionConfiguration != null ? collisionConfiguration._native : IntPtr.Zero))
        {
            _collisionConfiguration = collisionConfiguration;
            _dispatcher = dispatcher;
            _broadphase = pairCache;
        }

        public void removeRigidBody(RigidBody body)
        {
            BtDynamicsWorld_removeRigidBody(_native, body._native);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDiscreteDynamicsWorld_new(IntPtr dispatcher, IntPtr broadphase, IntPtr solver, IntPtr collisionconfig);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void BtDynamicsWorld_removeRigidBody(IntPtr obj, IntPtr body);
    }
}
