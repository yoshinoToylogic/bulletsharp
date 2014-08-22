using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class HingeConstraint : TypedConstraint
	{
        private RigidBody _rigidBodyA;
        private RigidBody _rigidBodyB;

        public HingeConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 pivotInA, ref Vector3 pivotInB, ref Vector3 axisInA, ref Vector3 axisInB)
            : base(btHingeConstraint_new(rbA._native, rbB._native, ref pivotInA, ref pivotInB, ref axisInA, ref axisInB))
        {
            _rigidBodyA = rbA;
            _rigidBodyB = rbB;
        }

		public HingeConstraint(RigidBody rbA, RigidBody rbB, Vector3 pivotInA, Vector3 pivotInB, Vector3 axisInA, Vector3 axisInB)
			: this(rbA, rbB, ref pivotInA, ref pivotInB, ref axisInA, ref axisInB)
		{
		}

        public HingeConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 pivotInA, ref Vector3 pivotInB, ref Vector3 axisInA, ref Vector3 axisInB, bool useReferenceFrameA)
            : base(btHingeConstraint_new2(rbA._native, rbB._native, ref pivotInA, ref pivotInB, ref axisInA, ref axisInB, useReferenceFrameA))
        {
            _rigidBodyA = rbA;
            _rigidBodyB = rbB;
        }

		public HingeConstraint(RigidBody rbA, RigidBody rbB, Vector3 pivotInA, Vector3 pivotInB, Vector3 axisInA, Vector3 axisInB, bool useReferenceFrameA)
			: this(rbA, rbB, ref pivotInA, ref pivotInB, ref axisInA, ref axisInB, useReferenceFrameA)
		{
		}

        public HingeConstraint(RigidBody rbA, ref Vector3 pivotInA, ref Vector3 axisInA)
            : base(btHingeConstraint_new3(rbA._native, ref pivotInA, ref axisInA))
        {
            _rigidBodyA = rbA;
        }

		public HingeConstraint(RigidBody rbA, Vector3 pivotInA, Vector3 axisInA)
			: this(rbA, ref pivotInA, ref axisInA)
		{
		}

        public HingeConstraint(RigidBody rbA, ref Vector3 pivotInA, ref Vector3 axisInA, bool useReferenceFrameA)
            : base(btHingeConstraint_new4(rbA._native, ref pivotInA, ref axisInA, useReferenceFrameA))
        {
            _rigidBodyA = rbA;
        }

		public HingeConstraint(RigidBody rbA, Vector3 pivotInA, Vector3 axisInA, bool useReferenceFrameA)
			: this(rbA, ref pivotInA, ref axisInA, useReferenceFrameA)
		{
		}

        public HingeConstraint(RigidBody rbA, RigidBody rbB, ref Matrix rbAFrame, ref Matrix rbBFrame)
            : base(btHingeConstraint_new5(rbA._native, rbB._native, ref rbAFrame, ref rbBFrame))
        {
            _rigidBodyA = rbA;
            _rigidBodyB = rbB;
        }

		public HingeConstraint(RigidBody rbA, RigidBody rbB, Matrix rbAFrame, Matrix rbBFrame)
			: this(rbA, rbB, ref rbAFrame, ref rbBFrame)
		{
		}

        public HingeConstraint(RigidBody rbA, RigidBody rbB, ref Matrix rbAFrame, ref Matrix rbBFrame, bool useReferenceFrameA)
            : base(btHingeConstraint_new6(rbA._native, rbB._native, ref rbAFrame, ref rbBFrame, useReferenceFrameA))
        {
            _rigidBodyA = rbA;
            _rigidBodyB = rbB;
        }

		public HingeConstraint(RigidBody rbA, RigidBody rbB, Matrix rbAFrame, Matrix rbBFrame, bool useReferenceFrameA)
			: this(rbA, rbB, ref rbAFrame, ref rbBFrame, useReferenceFrameA)
		{
		}

        public HingeConstraint(RigidBody rbA, ref Matrix rbAFrame)
            : base(btHingeConstraint_new7(rbA._native, ref rbAFrame))
        {
            _rigidBodyA = rbA;
        }

		public HingeConstraint(RigidBody rbA, Matrix rbAFrame)
			: this(rbA, ref rbAFrame)
		{
		}

        public HingeConstraint(RigidBody rbA, ref Matrix rbAFrame, bool useReferenceFrameA)
            : base(btHingeConstraint_new8(rbA._native, ref rbAFrame, useReferenceFrameA))
        {
            _rigidBodyA = rbA;
        }

		public HingeConstraint(RigidBody rbA, Matrix rbAFrame, bool useReferenceFrameA)
			: this(rbA, ref rbAFrame, useReferenceFrameA)
		{
		}

		public void EnableAngularMotor(bool enableMotor, float targetVelocity, float maxMotorImpulse)
		{
			btHingeConstraint_enableAngularMotor(_native, enableMotor, targetVelocity, maxMotorImpulse);
		}

        public float GetHingeAngle(ref Matrix transA, ref Matrix transB)
        {
            return btHingeConstraint_getHingeAngle(_native, ref transA, ref transB);
        }

		public float GetHingeAngle(Matrix transA, Matrix transB)
		{
			return btHingeConstraint_getHingeAngle(_native, ref transA, ref transB);
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			btHingeConstraint_getInfo1NonVirtual(_native, info._native);
		}

		public void GetInfo2Internal(ConstraintInfo2 info, Matrix transA, Matrix transB, Vector3 angVelA, Vector3 angVelB)
		{
			btHingeConstraint_getInfo2Internal(_native, info._native, ref transA, ref transB, ref angVelA, ref angVelB);
		}

		public void GetInfo2InternalUsingFrameOffset(ConstraintInfo2 info, Matrix transA, Matrix transB, Vector3 angVelA, Vector3 angVelB)
		{
			btHingeConstraint_getInfo2InternalUsingFrameOffset(_native, info._native, ref transA, ref transB, ref angVelA, ref angVelB);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix transA, Matrix transB, Vector3 angVelA, Vector3 angVelB)
		{
			btHingeConstraint_getInfo2NonVirtual(_native, info._native, ref transA, ref transB, ref angVelA, ref angVelB);
		}

        public void SetAxis(ref Vector3 axisInA)
        {
            btHingeConstraint_setAxis(_native, ref axisInA);
        }

		public void SetAxis(Vector3 axisInA)
		{
			btHingeConstraint_setAxis(_native, ref axisInA);
		}

        public void SetFrames(ref Matrix frameA, ref Matrix frameB)
        {
            btHingeConstraint_setFrames(_native, ref frameA, ref frameB);
        }

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			btHingeConstraint_setFrames(_native, ref frameA, ref frameB);
		}

		public void SetLimit(float low, float high)
		{
			btHingeConstraint_setLimit(_native, low, high);
		}

		public void SetLimit(float low, float high, float softness)
		{
			btHingeConstraint_setLimit2(_native, low, high, softness);
		}

		public void SetLimit(float low, float high, float softness, float biasFactor)
		{
			btHingeConstraint_setLimit3(_native, low, high, softness, biasFactor);
		}

		public void SetLimit(float low, float high, float softness, float biasFactor, float relaxationFactor)
		{
			btHingeConstraint_setLimit4(_native, low, high, softness, biasFactor, relaxationFactor);
		}

		public void SetMotorTarget(float targetAngle, float dt)
		{
			btHingeConstraint_setMotorTarget(_native, targetAngle, dt);
		}

        public void SetMotorTarget(ref Quaternion qAinB, float dt)
        {
            btHingeConstraint_setMotorTarget2(_native, ref qAinB, dt);
        }

		public void SetMotorTarget(Quaternion qAinB, float dt)
		{
			btHingeConstraint_setMotorTarget2(_native, ref qAinB, dt);
		}

        public void TestLimit(ref Matrix transA, ref Matrix transB)
        {
            btHingeConstraint_testLimit(_native, ref transA, ref transB);
        }

		public void TestLimit(Matrix transA, Matrix transB)
		{
			btHingeConstraint_testLimit(_native, ref transA, ref transB);
		}

		public void UpdateRHS(float timeStep)
		{
			btHingeConstraint_updateRHS(_native, timeStep);
		}

		public Matrix AFrame
		{
			get
			{
				Matrix value;
				btHingeConstraint_getAFrame(_native, out value);
				return value;
			}
		}

		public bool AngularOnly
		{
			get { return btHingeConstraint_getAngularOnly(_native); }
			set { btHingeConstraint_setAngularOnly(_native, value); }
		}

		public Matrix BFrame
		{
			get
			{
				Matrix value;
				btHingeConstraint_getBFrame(_native, out value);
				return value;
			}
		}

		public bool EnableMotor
		{
			get { return btHingeConstraint_getEnableAngularMotor(_native); }
		    set { btHingeConstraint_enableMotor(_native, value); }
		}

		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				btHingeConstraint_getFrameOffsetA(_native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				btHingeConstraint_getFrameOffsetB(_native, out value);
				return value;
			}
		}

		public float HingeAngle
		{
			get { return btHingeConstraint_getHingeAngle2(_native); }
		}

		public float LimitSign
		{
			get { return btHingeConstraint_getLimitSign(_native); }
		}

		public float LowerLimit
		{
			get { return btHingeConstraint_getLowerLimit(_native); }
		}

		public float MaxMotorImpulse
		{
			get { return btHingeConstraint_getMaxMotorImpulse(_native); }
			set { btHingeConstraint_setMaxMotorImpulse(_native, value); }
		}

		public float MotorTargetVelocity
		{
			get { return btHingeConstraint_getMotorTargetVelosity(_native); }
		}

		public int SolveLimit
		{
			get { return btHingeConstraint_getSolveLimit(_native); }
		}

		public float UpperLimit
		{
			get { return btHingeConstraint_getUpperLimit(_native); }
		}

		public bool UseFrameOffset
		{
			get { return btHingeConstraint_getUseFrameOffset(_native); }
			set { btHingeConstraint_setUseFrameOffset(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHingeConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Vector3 axisInA, [In] ref Vector3 axisInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHingeConstraint_new2(IntPtr rbA, IntPtr rbB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Vector3 axisInA, [In] ref Vector3 axisInB, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHingeConstraint_new3(IntPtr rbA, [In] ref Vector3 pivotInA, [In] ref Vector3 axisInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHingeConstraint_new4(IntPtr rbA, [In] ref Vector3 pivotInA, [In] ref Vector3 axisInA, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHingeConstraint_new5(IntPtr rbA, IntPtr rbB, [In] ref Matrix rbAFrame, [In] ref Matrix rbBFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHingeConstraint_new6(IntPtr rbA, IntPtr rbB, [In] ref Matrix rbAFrame, [In] ref Matrix rbBFrame, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHingeConstraint_new7(IntPtr rbA, [In] ref Matrix rbAFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHingeConstraint_new8(IntPtr rbA, [In] ref Matrix rbAFrame, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_enableAngularMotor(IntPtr obj, bool enableMotor, float targetVelocity, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_enableMotor(IntPtr obj, bool enableMotor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_getAFrame(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool btHingeConstraint_getAngularOnly(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_getBFrame(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool btHingeConstraint_getEnableAngularMotor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_getFrameOffsetA(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_getFrameOffsetB(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btHingeConstraint_getHingeAngle(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btHingeConstraint_getHingeAngle2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_getInfo1NonVirtual(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_getInfo2Internal(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_getInfo2InternalUsingFrameOffset(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_getInfo2NonVirtual(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Vector3 angVelA, [In] ref Vector3 angVelB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btHingeConstraint_getLimitSign(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btHingeConstraint_getLowerLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btHingeConstraint_getMaxMotorImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btHingeConstraint_getMotorTargetVelosity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btHingeConstraint_getSolveLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btHingeConstraint_getUpperLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
		static extern bool btHingeConstraint_getUseFrameOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setAngularOnly(IntPtr obj, bool angularOnly);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setAxis(IntPtr obj, [In] ref Vector3 axisInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setFrames(IntPtr obj, [In] ref Matrix frameA, [In] ref Matrix frameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setLimit(IntPtr obj, float low, float high);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setLimit2(IntPtr obj, float low, float high, float _softness);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setLimit3(IntPtr obj, float low, float high, float _softness, float _biasFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setLimit4(IntPtr obj, float low, float high, float _softness, float _biasFactor, float _relaxationFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setMaxMotorImpulse(IntPtr obj, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setMotorTarget(IntPtr obj, float targetAngle, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setMotorTarget2(IntPtr obj, [In] ref Quaternion qAinB, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_setUseFrameOffset(IntPtr obj, bool frameOffsetOnOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_testLimit(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btHingeConstraint_updateRHS(IntPtr obj, float timeStep);
	}
}
