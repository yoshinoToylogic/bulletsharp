using OpenTK;
using System.ComponentModel;

namespace BulletSharp
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DynamicsWorldExtensions
    {
        public unsafe static void GetGravity(this DynamicsWorld native, out OpenTK.Vector3 gravity)
        {
            fixed (OpenTK.Vector3* value = &gravity)
            {
                native.GetGravity(out *(BulletSharp.Math.Vector3*)value);
            }
        }

        public unsafe static void SetGravity(this DynamicsWorld native, ref OpenTK.Vector3 gravity)
        {
            fixed (OpenTK.Vector3* value = &gravity)
            {
                native.SetGravity(ref *(BulletSharp.Math.Vector3*)value);
            }
        }
    }
}
