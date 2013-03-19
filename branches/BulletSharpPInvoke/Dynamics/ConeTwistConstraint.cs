using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class ConeTwistConstraint : TypedConstraint
	{
		internal ConeTwistConstraint(IntPtr native)
			: base(native)
		{
		}

        public ConeTwistConstraint(RigidBody rbA, RigidBody rbB, Matrix rbAFrame, Matrix rbBFrame)
			: base(btConeTwistConstraint_new(rbA._native, rbB._native, ref rbAFrame, ref rbBFrame))
		{
		}

        public ConeTwistConstraint(RigidBody rbA, Matrix rbAFrame)
			: base(btConeTwistConstraint_new2(rbA._native, ref rbAFrame))
		{
		}

		public void CalcAngleInfo()
		{
			btConeTwistConstraint_calcAngleInfo(_native);
		}

        public void CalcAngleInfo2(Matrix transA, Matrix transB, Matrix invInertiaWorldA, Matrix invInertiaWorldB)
		{
			btConeTwistConstraint_calcAngleInfo2(_native, ref transA, ref transB, ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public void EnableMotor(bool b)
		{
			btConeTwistConstraint_enableMotor(_native, b);
		}
        /*
        public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix transA, Matrix transB, Matrix invInertiaWorldA, Matrix invInertiaWorldB)
		{
			btConeTwistConstraint_getInfo2NonVirtual(_native, info._native, ref transA, ref transB, ref invInertiaWorldA, ref invInertiaWorldB);
		}
        */
		public void GetPointForAngle(float fAngleInRadians, float fLength)
		{
			btConeTwistConstraint_GetPointForAngle(_native, fAngleInRadians, fLength);
		}

		public void SetAngularOnly(bool angularOnly)
		{
			btConeTwistConstraint_setAngularOnly(_native, angularOnly);
		}

		public void SetDamping(float damping)
		{
			btConeTwistConstraint_setDamping(_native, damping);
		}

        public void SetFrames(Matrix frameA, Matrix frameB)
		{
			btConeTwistConstraint_setFrames(_native, ref frameA, ref frameB);
		}

		public void SetLimit(float _swingSpan1, float _swingSpan2, float _twistSpan, float _softness, float _biasFactor, float _relaxationFactor)
		{
			btConeTwistConstraint_setLimit(_native, _swingSpan1, _swingSpan2, _twistSpan, _softness, _biasFactor, _relaxationFactor);
		}

		public void SetLimit(float _swingSpan1, float _swingSpan2, float _twistSpan, float _softness, float _biasFactor)
		{
			btConeTwistConstraint_setLimit2(_native, _swingSpan1, _swingSpan2, _twistSpan, _softness, _biasFactor);
		}

		public void SetLimit(float _swingSpan1, float _swingSpan2, float _twistSpan, float _softness)
		{
			btConeTwistConstraint_setLimit3(_native, _swingSpan1, _swingSpan2, _twistSpan, _softness);
		}

		public void SetLimit(float _swingSpan1, float _swingSpan2, float _twistSpan)
		{
			btConeTwistConstraint_setLimit4(_native, _swingSpan1, _swingSpan2, _twistSpan);
		}

		public void SetLimit(int limitIndex, float limitValue)
		{
			btConeTwistConstraint_setLimit5(_native, limitIndex, limitValue);
		}

		public void SetMaxMotorImpulse(float maxMotorImpulse)
		{
			btConeTwistConstraint_setMaxMotorImpulse(_native, maxMotorImpulse);
		}

		public void SetMaxMotorImpulseNormalized(float maxMotorImpulse)
		{
			btConeTwistConstraint_setMaxMotorImpulseNormalized(_native, maxMotorImpulse);
		}

		public void SetMotorTarget(Quaternion q)
		{
			btConeTwistConstraint_setMotorTarget(_native, ref q);
		}

		public void SetMotorTargetInConstraintSpace(Quaternion q)
		{
			btConeTwistConstraint_setMotorTargetInConstraintSpace(_native, ref q);
		}

		public void UpdateRHS(float timeStep)
		{
			btConeTwistConstraint_updateRHS(_native, timeStep);
		}

		public Matrix AFrame
		{
            get
            {
                Matrix value;
                btConeTwistConstraint_getAFrame(_native, out value);
                return value;
            }
		}

        public Matrix BFrame
        {
            get
            {
                Matrix value;
                btConeTwistConstraint_getBFrame(_native, out value);
                return value;
            }
        }

		public float FixThresh
		{
			get { return btConeTwistConstraint_getFixThresh(_native); }
			set { btConeTwistConstraint_setFixThresh(_native, value); }
		}

        public Matrix FrameOffsetA
        {
            get
            {
                Matrix value;
                btConeTwistConstraint_getFrameOffsetA(_native, out value);
                return value;
            }
        }

        public Matrix FrameOffsetB
        {
            get
            {
                Matrix value;
                btConeTwistConstraint_getFrameOffsetB(_native, out value);
                return value;
            }
        }
        /*
		public void Info1NonVirtual
		{
			get { return btConeTwistConstraint_getInfo1NonVirtual(_native); }
		}
        */
		public bool IsPastSwingLimit
		{
			get { return btConeTwistConstraint_isPastSwingLimit(_native); }
		}

		public int SolveSwingLimit
		{
			get { return btConeTwistConstraint_getSolveSwingLimit(_native); }
		}

		public int SolveTwistLimit
		{
			get { return btConeTwistConstraint_getSolveTwistLimit(_native); }
		}

		public float SwingSpan1
		{
			get { return btConeTwistConstraint_getSwingSpan1(_native); }
		}

		public float SwingSpan2
		{
			get { return btConeTwistConstraint_getSwingSpan2(_native); }
		}

		public float TwistAngle
		{
			get { return btConeTwistConstraint_getTwistAngle(_native); }
		}

		public float TwistLimitSign
		{
			get { return btConeTwistConstraint_getTwistLimitSign(_native); }
		}

		public float TwistSpan
		{
			get { return btConeTwistConstraint_getTwistSpan(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btConeTwistConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix rbAFrame, [In] ref Matrix rbBFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btConeTwistConstraint_new2(IntPtr rbA, [In] ref Matrix rbAFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_calcAngleInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConeTwistConstraint_calcAngleInfo2(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Matrix invInertiaWorldA, [In] ref Matrix invInertiaWorldB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_enableMotor(IntPtr obj, bool b);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_getAFrame(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConeTwistConstraint_getBFrame(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getFixThresh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConeTwistConstraint_getFrameOffsetA(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConeTwistConstraint_getFrameOffsetB(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_getInfo1NonVirtual(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConeTwistConstraint_getInfo2NonVirtual(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Matrix invInertiaWorldA, [In] ref Matrix invInertiaWorldB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_GetPointForAngle(IntPtr obj, float fAngleInRadians, float fLength);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btConeTwistConstraint_getSolveSwingLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btConeTwistConstraint_getSolveTwistLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getSwingSpan1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getSwingSpan2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getTwistAngle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getTwistLimitSign(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getTwistSpan(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btConeTwistConstraint_isPastSwingLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setAngularOnly(IntPtr obj, bool angularOnly);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setDamping(IntPtr obj, float damping);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setFixThresh(IntPtr obj, float fixThresh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConeTwistConstraint_setFrames(IntPtr obj, [In] ref Matrix frameA, [In] ref Matrix frameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setLimit(IntPtr obj, float _swingSpan1, float _swingSpan2, float _twistSpan, float _softness, float _biasFactor, float _relaxationFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setLimit2(IntPtr obj, float _swingSpan1, float _swingSpan2, float _twistSpan, float _softness, float _biasFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setLimit3(IntPtr obj, float _swingSpan1, float _swingSpan2, float _twistSpan, float _softness);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setLimit4(IntPtr obj, float _swingSpan1, float _swingSpan2, float _twistSpan);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setLimit5(IntPtr obj, int limitIndex, float limitValue);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setMaxMotorImpulse(IntPtr obj, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setMaxMotorImpulseNormalized(IntPtr obj, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setMotorTarget(IntPtr obj, [In] ref Quaternion q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConeTwistConstraint_setMotorTargetInConstraintSpace(IntPtr obj, [In] ref Quaternion q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_updateRHS(IntPtr obj, float timeStep);
	}
}
