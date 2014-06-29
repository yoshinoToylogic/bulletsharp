using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
    /*
	public class SphereTriangleDetector : DiscreteCollisionDetectorInterface
	{
		internal SphereTriangleDetector(IntPtr native)
			: base(native)
		{
		}

		public SphereTriangleDetector(SphereShape sphere, TriangleShape triangle, float contactBreakingThreshold)
			: base(SphereTriangleDetector_new(sphere._native, triangle._native, contactBreakingThreshold))
		{
		}

		public bool Collide(Vector3 sphereCenter, Vector3 point, Vector3 resultNormal, float depth, float timeOfImpact, float contactBreakingThreshold)
		{
			return SphereTriangleDetector_collide(_native, ref sphereCenter, ref point, ref resultNormal, depth._native, timeOfImpact._native, contactBreakingThreshold);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr SphereTriangleDetector_new(IntPtr sphere, IntPtr triangle, float contactBreakingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool SphereTriangleDetector_collide(IntPtr obj, [In] ref Vector3 sphereCenter, [In] ref Vector3 point, [In] ref Vector3 resultNormal, IntPtr depth, IntPtr timeOfImpact, float contactBreakingThreshold);
	}
    */
}
