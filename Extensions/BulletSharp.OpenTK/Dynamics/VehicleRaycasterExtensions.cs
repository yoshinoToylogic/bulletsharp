using System;
using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class VehicleRaycasterExtensions
	{
		public unsafe static IntPtr CastRay(this VehicleRaycaster obj, ref OpenTK.Vector3 from, ref OpenTK.Vector3 to, VehicleRaycasterResult result)
		{
			fixed (OpenTK.Vector3* fromPtr = &from)
			{
				fixed (OpenTK.Vector3* toPtr = &to)
				{
					return obj.CastRay(ref *(BulletSharp.Math.Vector3*)fromPtr, ref *(BulletSharp.Math.Vector3*)toPtr, result);
				}
			}
		}
	}
}
