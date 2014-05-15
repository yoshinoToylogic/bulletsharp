using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class WheelInfoConstructionInfoExtensions
	{
		public unsafe static void GetChassisConnectionCS(this WheelInfoConstructionInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ChassisConnectionCS;
			}
		}

		public static OpenTK.Vector3 GetChassisConnectionCS(this WheelInfoConstructionInfo obj)
		{
			OpenTK.Vector3 value;
			GetChassisConnectionCS(obj, out value);
			return value;
		}

		public unsafe static void GetWheelAxleCS(this WheelInfoConstructionInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.WheelAxleCS;
			}
		}

		public static OpenTK.Vector3 GetWheelAxleCS(this WheelInfoConstructionInfo obj)
		{
			OpenTK.Vector3 value;
			GetWheelAxleCS(obj, out value);
			return value;
		}

		public unsafe static void GetWheelDirectionCS(this WheelInfoConstructionInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.WheelDirectionCS;
			}
		}

		public static OpenTK.Vector3 GetWheelDirectionCS(this WheelInfoConstructionInfo obj)
		{
			OpenTK.Vector3 value;
			GetWheelDirectionCS(obj, out value);
			return value;
		}

		public unsafe static void SetChassisConnectionCS(this WheelInfoConstructionInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ChassisConnectionCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetChassisConnectionCS(this WheelInfoConstructionInfo obj, OpenTK.Vector3 value)
		{
			SetChassisConnectionCS(obj, ref value);
		}

		public unsafe static void SetWheelAxleCS(this WheelInfoConstructionInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.WheelAxleCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetWheelAxleCS(this WheelInfoConstructionInfo obj, OpenTK.Vector3 value)
		{
			SetWheelAxleCS(obj, ref value);
		}

		public unsafe static void SetWheelDirectionCS(this WheelInfoConstructionInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.WheelDirectionCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetWheelDirectionCS(this WheelInfoConstructionInfo obj, OpenTK.Vector3 value)
		{
			SetWheelDirectionCS(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class WheelInfoExtensions
	{
		public unsafe static void GetChassisConnectionPointCS(this WheelInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ChassisConnectionPointCS;
			}
		}

		public static OpenTK.Vector3 GetChassisConnectionPointCS(this WheelInfo obj)
		{
			OpenTK.Vector3 value;
			GetChassisConnectionPointCS(obj, out value);
			return value;
		}

		public unsafe static void GetWheelAxleCS(this WheelInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.WheelAxleCS;
			}
		}

		public static OpenTK.Vector3 GetWheelAxleCS(this WheelInfo obj)
		{
			OpenTK.Vector3 value;
			GetWheelAxleCS(obj, out value);
			return value;
		}

		public unsafe static void GetWheelDirectionCS(this WheelInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.WheelDirectionCS;
			}
		}

		public static OpenTK.Vector3 GetWheelDirectionCS(this WheelInfo obj)
		{
			OpenTK.Vector3 value;
			GetWheelDirectionCS(obj, out value);
			return value;
		}

		public unsafe static void GetWorldTransform(this WheelInfo obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.WorldTransform;
			}
		}

		public static OpenTK.Matrix4 GetWorldTransform(this WheelInfo obj)
		{
			OpenTK.Matrix4 value;
			GetWorldTransform(obj, out value);
			return value;
		}

		public unsafe static void SetChassisConnectionPointCS(this WheelInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ChassisConnectionPointCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetChassisConnectionPointCS(this WheelInfo obj, OpenTK.Vector3 value)
		{
			SetChassisConnectionPointCS(obj, ref value);
		}

		public unsafe static void SetWheelAxleCS(this WheelInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.WheelAxleCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetWheelAxleCS(this WheelInfo obj, OpenTK.Vector3 value)
		{
			SetWheelAxleCS(obj, ref value);
		}

		public unsafe static void SetWheelDirectionCS(this WheelInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.WheelDirectionCS = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetWheelDirectionCS(this WheelInfo obj, OpenTK.Vector3 value)
		{
			SetWheelDirectionCS(obj, ref value);
		}

		public unsafe static void SetWorldTransform(this WheelInfo obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.WorldTransform = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetWorldTransform(this WheelInfo obj, OpenTK.Matrix4 value)
		{
			SetWorldTransform(obj, ref value);
		}
	}
}
