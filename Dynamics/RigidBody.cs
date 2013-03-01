using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class RigidBody : CollisionObject
    {
        MotionState _motionState;

        public RigidBody(IntPtr obj)
            : base(obj)
        {
        }

        public RigidBody(RigidBodyConstructionInfo constructionInfo)
            : base(btRigidBody_new(constructionInfo._native))
        {
            _motionState = constructionInfo._motionState;
        }

        public void ApplyCentralForce(Vector3 force)
        {
            btRigidBody_applyCentralForce(_native, ref force);
        }

        public void ApplyCentralImpulse(Vector3 force)
        {
            btRigidBody_applyCentralImpulse(_native, ref force);
        }

        public void ApplyForce(Vector3 force, Vector3 rel_pos)
        {
            btRigidBody_applyForce(_native, ref force, ref rel_pos);
        }

        public void ApplyImpulse(Vector3 force, Vector3 rel_pos)
        {
            btRigidBody_applyImpulse(_native, ref force, ref rel_pos);
        }

        public void ApplyTorque(Vector3 torque)
        {
            btRigidBody_applyTorque(_native, ref torque);
        }

        public void ApplyTorqueImpulse(Vector3 torque)
        {
            btRigidBody_applyTorqueImpulse(_native, ref torque);
        }

        public void ClearForces()
        {
            btRigidBody_clearForces(_native);
        }

        public float ComputeAngularImpulseDenominator(Vector3 axis)
        {
            return btRigidBody_computeAngularImpulseDenominator(_native, ref axis);
        }

        public float ComputeImpulseDenominator(Vector3 pos, Vector3 normal)
        {
            return btRigidBody_computeImpulseDenominator(_native, ref pos, ref normal);
        }

        public float AngularDamping
        {
            get { return btRigidBody_getAngularDamping(_native); }
        }

        public Vector3 AngularFactor
        {
            get
            {
                Vector3 factor;
                btRigidBody_getAngularFactor(_native, out factor);
                return factor;
            }
            set { btRigidBody_setAngularFactor(_native, ref value); }
        }

        public float AngularSleepingThreshold
        {
            get { return btRigidBody_getAngularSleepingThreshold(_native); }
        }

        public Vector3 AngularVelocity
        {
            get
            {
                Vector3 velocity;
                btRigidBody_getAngularVelocity(_native, out velocity);
                return velocity;
            }
            set { btRigidBody_setAngularVelocity(_native, ref value); }
        }

        public Vector3 CenterOfMassPosition
        {
            get
            {
                Vector3 position;
                btRigidBody_getCenterOfMassPosition(_native, out position);
                return position;
            }
        }

        public Matrix CenterOfMassTransform
        {
            get
            {
                Matrix transform;
                btRigidBody_getCenterOfMassTransform(_native, out transform);
                return transform;
            }
        }

        public TypedConstraint GetConstraintRef(int index)
        {
            return new TypedConstraint(btRigidBody_getConstraintRef(_native, index));
        }

        public Vector3 Gravity
        {
            get
            {
                Vector3 gravity;
                btRigidBody_getGravity(_native, out gravity);
                return gravity;
            }
        }

        public Vector3 InvInertiaDiagLocal
        {
            get
            {
                Vector3 inertia;
                btRigidBody_getInvInertiaDiagLocal(_native, out inertia);
                return inertia;
            }
            set { btRigidBody_setInvInertiaDiagLocal(_native, ref value); }
        }

        public Matrix InvInertiaTensorWorld
        {
            get
            {
                Matrix inertiaTensor;
                btRigidBody_getInvInertiaTensorWorld(_native, out inertiaTensor);
                return inertiaTensor;
            }
        }

        public float InverseMass
        {
            get { return btRigidBody_getInvMass(_native); }
        }

        public float LinearDamping
        {
            get { return btRigidBody_getLinearDamping(_native); }
        }

        public Vector3 LinearFactor
        {
            get
            {
                Vector3 linearFactor;
                btRigidBody_getLinearFactor(_native, out linearFactor);
                return linearFactor;
            }
            set { btRigidBody_setLinearFactor(_native, ref value); }
        }

        public Vector3 LinearVelocity
        {
            get
            {
                Vector3 linearVelocity;
                btRigidBody_getLinearVelocity(_native, out linearVelocity);
                return linearVelocity;
            }
            set { btRigidBody_setLinearVelocity(_native, ref value); }
        }

        public MotionState MotionState
        {
            get { return _motionState; }
            set
            {
                btRigidBody_setMotionState(_native, value._native);
                _motionState = value;
            }
        }

        public int getNumConstraintRefs()
        {
            return btRigidBody_getNumConstraintRefs(_native);
        }

        public Vector3 TotalForce
        {
            get
            {
                Vector3 totalForce;
                btRigidBody_getTotalForce(_native, out totalForce);
                return totalForce;
            }
        }

        public Vector3 GetVelocityInLocalPoint(Vector3 rel_pos)
        {
            Vector3 velocity;
            btRigidBody_getVelocityInLocalPoint(_native, ref rel_pos, out velocity);
            return velocity;
        }

        public bool IsInWorld
        {
            get { return btRigidBody_isInWorld(_native); }
        }

        public void SetSleepingThresholds(float linear, float angular)
        {
            btRigidBody_setSleepingThresholds(_native, linear, angular);
        }

        public void Translate(Vector3 v)
        {
            btRigidBody_translate(_native, ref v);
        }

        public void updateDeactivation(float timeStep)
        {
            btRigidBody_updateDeactivation(_native, timeStep);
        }

        #region Native Invokes

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btRigidBody_new(IntPtr constructionInfo);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_applyCentralForce(IntPtr obj, [In] ref Vector3 force);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_applyCentralImpulse(IntPtr obj, [In] ref Vector3 impulse);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_applyForce(IntPtr obj, [In] ref Vector3 force, [In] ref Vector3 rel_pos);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_applyImpulse(IntPtr obj, [In] ref Vector3 impulse, [In] ref Vector3 rel_pos);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_applyTorque(IntPtr obj, [In] ref Vector3 torque);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_applyTorqueImpulse(IntPtr obj, [In] ref Vector3 torque);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_clearForces(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btRigidBody_computeAngularImpulseDenominator(IntPtr obj, [In] ref Vector3 axis);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btRigidBody_computeImpulseDenominator(IntPtr obj, [In] ref Vector3 pos, [In] ref Vector3 normal);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btRigidBody_getAngularDamping(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_getAngularFactor(IntPtr obj, [Out] out Vector3 factor);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btRigidBody_getAngularSleepingThreshold(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_getAngularVelocity(IntPtr obj, [Out] out Vector3 velocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btRigidBody_getLinearSleepingThreshold(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_getCenterOfMassPosition(IntPtr obj, [Out] out Vector3 position);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_getCenterOfMassTransform(IntPtr obj, [Out] out Matrix transform);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btRigidBody_getConstraintRef(IntPtr obj, int index);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_getGravity(IntPtr obj, [Out] out Vector3 gravity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_getInvInertiaDiagLocal(IntPtr obj, [Out] out Vector3 inertia);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_getInvInertiaTensorWorld(IntPtr obj, [Out] out Matrix inertiaTensor);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btRigidBody_getInvMass(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btRigidBody_getLinearDamping(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_getLinearFactor(IntPtr obj, [Out] out Vector3 linearFactor);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_getLinearVelocity(IntPtr obj, [Out] out Vector3 linearVelocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btRigidBody_getMotionState(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btRigidBody_getNumConstraintRefs(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btRigidBody_getTotalForce(IntPtr obj, [Out] out Vector3 force);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btRigidBody_getTotalTorque(IntPtr obj, [Out] out Vector3 torque);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btRigidBody_getVelocityInLocalPoint(IntPtr obj, [In] ref Vector3 rel_pos, [Out] out Vector3 velocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btRigidBody_isInWorld(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_setAngularFactor(IntPtr obj, [In] ref Vector3 angFac);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_setAngularVelocity(IntPtr obj, [In] ref Vector3 ang_vel);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_setInvInertiaDiagLocal(IntPtr obj, [In] ref Vector3 diagInvInertia);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_setLinearFactor(IntPtr obj, [In] ref Vector3 linearFactor);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_setLinearVelocity(IntPtr obj, [In] ref Vector3 linearVelocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_setMotionState(IntPtr obj, IntPtr motionState);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_setNewBroadphaseProxy(IntPtr obj, IntPtr broadphaseProxy);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_setSleepingThresholds(IntPtr obj, float linear, float angular);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_translate(IntPtr obj, [In] ref Vector3 v);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btRigidBody_updateDeactivation(IntPtr obj, float timeStep);

        #endregion
    }
}
