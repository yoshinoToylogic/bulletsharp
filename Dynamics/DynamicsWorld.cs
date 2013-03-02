using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class DynamicsWorld : CollisionWorld
    {
        public DynamicsWorld(IntPtr native)
            : base(native)
        {
        }

        public DynamicsWorld(Dispatcher dispatcher, BroadphaseInterface broadphase, ConstraintSolver solver, CollisionConfiguration collisionconfig)
            : base(btDynamicsWorld_new(dispatcher._native, broadphase._native, solver._native, collisionconfig._native))
        {
        }

        public void AddRigidBody(RigidBody body)
        {
            btDynamicsWorld_addRigidBody(_native, body._native);
        }

        public void AddConstraint(TypedConstraint pconstraint)
        {
            AddConstraint(pconstraint, false);
        }

        public void AddConstraint(TypedConstraint pconstraint, bool disableLinkedCollisions)
        {
            btDynamicsWorld_addConstraint(_native, pconstraint._native, disableLinkedCollisions);
        }

        public TypedConstraint GetConstraint(int index)
        {
            return new TypedConstraint(btDynamicsWorld_getConstraint(_native, index));
        }

        public void RemoveRigidBody(RigidBody body)
        {
            BtDynamicsWorld_removeRigidBody(_native, body._native);
        }

        public void RemoveConstraint(TypedConstraint pconstraint)
        {
            btDynamicsWorld_removeConstraint(_native, pconstraint._native);
        }

        public void SetGravity(ref Vector3 v)
        {
            btDynamicsWorld_setGravity(_native, ref v);
        }

        public int StepSimulation(float timeStep, int maxSubSteps, float fixedTimeStep)
        {
            return btDynamicsWorld_stepSimulation(_native, timeStep, maxSubSteps, fixedTimeStep);
        }
        public int StepSimulation(float timeStep, int maxSubSteps)
        {
            return btDynamicsWorld_stepSimulation(_native, timeStep, maxSubSteps, 1.0f / 60.0f);
        }
        public int StepSimulation(float timeStep)
        {
            return btDynamicsWorld_stepSimulation(_native, timeStep, 1, 1.0f / 60.0f);
        }

        public Vector3 Gravity
        {
            get
            {
                Vector3 gravity;
                btDynamicsWorld_getGravity(_native, out gravity);
                return gravity;
            }
            set { btDynamicsWorld_setGravity(_native, ref value); }
        }

        public int NumConstraints
        {
            get { return btDynamicsWorld_getNumConstraints(_native); }
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDynamicsWorld_new(IntPtr dispatcher, IntPtr broadphase, IntPtr solver, IntPtr collisionconfig);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDynamicsWorld_addConstraint(IntPtr dynamicsWorld, IntPtr constraint, bool disableLinkedCollisions);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDynamicsWorld_addRigidBody(IntPtr obj, IntPtr body);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDynamicsWorld_getConstraint(IntPtr obj, int index);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDynamicsWorld_getGravity(IntPtr dynamicsWorld, [Out] out Vector3 v);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btDynamicsWorld_getNumConstraints(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void BtDynamicsWorld_removeRigidBody(IntPtr obj, IntPtr body);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDynamicsWorld_setGravity(IntPtr dynamicsWorld, [In] ref Vector3 v);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btDynamicsWorld_stepSimulation(IntPtr dynamicsWorld, float timeStep, int maxSubSteps,
                                                             float fixedTimeStep);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDynamicsWorld_removeConstraint(IntPtr dynamicsWorld, IntPtr constraint);
    }
}
