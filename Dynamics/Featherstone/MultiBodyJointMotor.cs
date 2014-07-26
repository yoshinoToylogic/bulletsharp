using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class MultiBodyJointMotor : MultiBodyConstraint
	{
		internal MultiBodyJointMotor(IntPtr native)
			: base(native)
		{
		}

		public MultiBodyJointMotor(MultiBody body, int link, float desiredVelocity, float maxMotorImpulse)
			: base(btMultiBodyJointMotor_new(body._native, link, desiredVelocity, maxMotorImpulse))
		{
            _multiBodyA = body;
            _multiBodyB = body;
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyJointMotor_new(IntPtr body, int link, float desiredVelocity, float maxMotorImpulse);
	}
}
