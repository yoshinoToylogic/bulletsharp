using System.ComponentModel;

namespace BulletSharp
{

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DynamicsWorldExtensions
    {
        public unsafe static void GetGravity(this DynamicsWorld obj, out OpenTK.Vector3 value)
        {
            fixed (OpenTK.Vector3* valuePtr = &value)
            {
                obj.GetGravity(out *(BulletSharp.Math.Vector3*)valuePtr);
            }
        }

        public static OpenTK.Vector3 GetGravity(this DynamicsWorld obj)
        {
            OpenTK.Vector3 value;
            GetGravity(obj, out value);
            return value;
        }

        public unsafe static void SetGravity(this DynamicsWorld obj, ref OpenTK.Vector3 value)
        {
            fixed (OpenTK.Vector3* valuePtr = &value)
            {
                obj.SetGravity(ref *(BulletSharp.Math.Vector3*)valuePtr);
            }
        }

        public static void SetGravity(this DynamicsWorld obj, OpenTK.Vector3 value)
        {
            SetGravity(obj, ref value);
        }
    }
}
