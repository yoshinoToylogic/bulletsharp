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

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDiscreteDynamicsWorld_new(IntPtr dispatcher, IntPtr broadphase, IntPtr solver, IntPtr collisionconfig);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDiscreteDynamicsWorld_applyGravity(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern void btDiscreteDynamicsWorld_debugDrawConstraint(IntPtr obj, IntPtr constraint);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern bool btDiscreteDynamicsWorld_getApplySpeculativeContactRestitution(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDiscreteDynamicsWorld_getCollisionWorld(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDiscreteDynamicsWorld_getSimulationIslandManager(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern bool btDiscreteDynamicsWorld_getSynchronizeAllMotionStates(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern void btDiscreteDynamicsWorld_setApplySpeculativeContactRestitution(IntPtr obj, bool enable);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern void btDiscreteDynamicsWorld_setNumTasks(IntPtr obj, int numTasks);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern void btDiscreteDynamicsWorld_setSynchronizeAllMotionStates(IntPtr obj, bool synchronizeAll);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern void btDiscreteDynamicsWorld_synchronizeSingleMotionState(IntPtr obj, IntPtr body);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
	    static extern void btDiscreteDynamicsWorld_updateVehicles(IntPtr obj, float timeStep);
    }
}
