using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class StorageResultExtensions
	{
		public unsafe static void GetClosestPointInB(this StorageResult obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ClosestPointInB;
			}
		}

		public static OpenTK.Vector3 GetClosestPointInB(this StorageResult obj)
		{
			OpenTK.Vector3 value;
			GetClosestPointInB(obj, out value);
			return value;
		}

		public unsafe static void GetNormalOnSurfaceB(this StorageResult obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.NormalOnSurfaceB;
			}
		}

		public static OpenTK.Vector3 GetNormalOnSurfaceB(this StorageResult obj)
		{
			OpenTK.Vector3 value;
			GetNormalOnSurfaceB(obj, out value);
			return value;
		}

		public unsafe static void SetClosestPointInB(this StorageResult obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ClosestPointInB = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetClosestPointInB(this StorageResult obj, OpenTK.Vector3 value)
		{
			SetClosestPointInB(obj, ref value);
		}

		public unsafe static void SetNormalOnSurfaceB(this StorageResult obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.NormalOnSurfaceB = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetNormalOnSurfaceB(this StorageResult obj, OpenTK.Vector3 value)
		{
			SetNormalOnSurfaceB(obj, ref value);
		}
	}
}
