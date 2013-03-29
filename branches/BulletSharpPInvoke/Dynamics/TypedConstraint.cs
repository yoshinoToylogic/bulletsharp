using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public enum TypedConstraintType
    {
        Point2Point = 3,
        Hinge,
        ConeTwist,
        D6,
        Slider,
        Contact,
        D6Spring,
        Gear,
        Max
    }

    public enum ConstraintParam
    {
        Erp = 1,
        StopErp,
        Cfm,
        StopCfm
    }

    public class TypedConstraint : IDisposable
    {
        internal IntPtr _native;

        internal static TypedConstraint GetManaged(IntPtr native)
        {
            IntPtr handlePtr = btTypedConstraint_getUserConstraintPtr(native);
            if (handlePtr.ToInt64() != -1)
            {
                GCHandle handle = GCHandle.FromIntPtr(handlePtr);
                return handle.Target as TypedConstraint;
            }

            throw new NotImplementedException();
        }

        internal TypedConstraint(IntPtr native)
        {
            _native = native;

            if (btTypedConstraint_getUserConstraintPtr(_native).ToInt64() != -1)
            {
                throw new InvalidOperationException();
            }

            GCHandle handle = GCHandle.Alloc(this);
            btTypedConstraint_setUserConstraintPtr(_native, GCHandle.ToIntPtr(handle));
        }

        public int CalculateSerializeBufferSize()
        {
            return btTypedConstraint_calculateSerializeBufferSize(_native);
        }

        public void EnableFeedback(bool needsFeedback)
        {
            btTypedConstraint_enableFeedback(_native, needsFeedback);
        }

        /*
        public void GetInfo1(ref ConstraintInfo1 info)
        {
            btTypedConstraint_getInfo1(_native, info._native);
        }

        public void GetInfo2(ref ConstraintInfo2 info)
        {
            btTypedConstraint_getInfo2(_native, info._native);
        }
        */

        public float GetParam(ConstraintParam num, int axis)
        {
            return btTypedConstraint_getParam(_native, num, axis);
        }

        public float GetParam(ConstraintParam num)
        {
            return btTypedConstraint_getParam2(_native, num);
        }

        public void SetParam(ConstraintParam num, float value, int axis)
        {
            btTypedConstraint_setParam(_native, num, value, axis);
        }

        public void SetParam(ConstraintParam num, float value)
        {
            btTypedConstraint_setParam2(_native, num, value);
        }

        public float AppliedImpulse
        {
            get { return btTypedConstraint_getAppliedImpulse(_native); }
        }

        public float BreakingImpulseThreshold
        {
            get { return btTypedConstraint_getBreakingImpulseThreshold(_native); }
            set { btTypedConstraint_setBreakingImpulseThreshold(_native, value); }
        }

        public TypedConstraintType ConstraintType
        {
            get { return btTypedConstraint_getConstraintType(_native); }
        }

        public float DebugDrawSize
        {
            get { return btTypedConstraint_getDbgDrawSize(_native); }
            set { btTypedConstraint_setDbgDrawSize(_native, value); }
        }

        public RigidBody FixedBody
        {
            get { return CollisionObject.GetManaged(btTypedConstraint_getFixedBody(_native)) as RigidBody; }
        }

        public bool IsEnabled
        {
            get { return btTypedConstraint_isEnabled(_native); }
            set { btTypedConstraint_setEnabled(_native, value); }
        }

        /*
        public JointFeedback JointFeedback
        {
            get { return JointFeedback.GetManaged(btTypedConstraint_getJointFeedback(_native)); }
            set { btTypedConstraint_setJointFeedback(value._native); }
        }
        */

        public bool NeedsFeedback
        {
            get { return btTypedConstraint_needsFeedback(_native); }
            set { btTypedConstraint_enableFeedback(_native, value); }
        }

        public int OverrideNumSolverIterations
        {
            get { return btTypedConstraint_getOverrideNumSolverIterations(_native); }
            set { btTypedConstraint_setOverrideNumSolverIterations(_native, value); }
        }

        public RigidBody RigidBodyA
        {
            get { return CollisionObject.GetManaged(btTypedConstraint_getRigidBodyA(_native)) as RigidBody; }
        }

        public RigidBody RigidBodyB
        {
            get { return CollisionObject.GetManaged(btTypedConstraint_getRigidBodyB(_native)) as RigidBody; }
        }

        public int Uid
        {
            get { return btTypedConstraint_getUid(_native); }
        }

        public int UserConstraintId
        {
            get { return btTypedConstraint_getUserConstraintId(_native); }
            set { btTypedConstraint_setUserConstraintId(_native, value); }
        }

        public TypedConstraintType UseConstraintType
        {
            get { return btTypedConstraint_getUserConstraintType(_native); }
            set { btTypedConstraint_setUserConstraintType(_native, value); }
        }

        public Object Userobject { get; set; }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_delete(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btTypedConstraint_calculateSerializeBufferSize(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_enableFeedback(IntPtr obj, bool needsFeedback);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btTypedConstraint_getAppliedImpulse(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btTypedConstraint_getBreakingImpulseThreshold(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern TypedConstraintType btTypedConstraint_getConstraintType(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btTypedConstraint_getDbgDrawSize(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btTypedConstraint_getFixedBody(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_getInfo1(IntPtr obj, IntPtr info);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_getInfo2(IntPtr obj, IntPtr info);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btTypedConstraint_getJointFeedback(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btTypedConstraint_getOverrideNumSolverIterations(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btTypedConstraint_getParam(IntPtr obj, ConstraintParam num, int axis);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btTypedConstraint_getParam2(IntPtr obj, ConstraintParam num);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btTypedConstraint_getRigidBodyA(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btTypedConstraint_getRigidBodyB(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btTypedConstraint_getUid(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btTypedConstraint_getUserConstraintId(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btTypedConstraint_getUserConstraintPtr(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern TypedConstraintType btTypedConstraint_getUserConstraintType(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btTypedConstraint_isEnabled(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btTypedConstraint_needsFeedback(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setBreakingImpulseThreshold(IntPtr obj, float threshold);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setDbgDrawSize(IntPtr obj, float dbgDrawSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setEnabled(IntPtr obj, bool enabled);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setJointFeedback(IntPtr obj, IntPtr jointFeedback);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setOverrideNumSolverIterations(IntPtr obj, int overideNumIterations);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setParam(IntPtr obj, ConstraintParam num, float value, int axis);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setParam2(IntPtr obj, ConstraintParam num, float value);
        //[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        //static extern string btTypedConstraint_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setUserConstraintId(IntPtr obj, int uid);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setUserConstraintPtr(IntPtr obj, IntPtr ptr);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setUserConstraintType(IntPtr obj, TypedConstraintType userConstraintType);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_native != IntPtr.Zero)
            {
                IntPtr handlePtr = btTypedConstraint_getUserConstraintPtr(_native);
                if (handlePtr.ToInt64() != -1)
                {
                    GCHandle.FromIntPtr(handlePtr).Free();
                    btTypedConstraint_delete(_native);
                }
                _native = IntPtr.Zero;
            }
        }

        ~TypedConstraint()
        {
            Dispose(false);
        }
    }
}
