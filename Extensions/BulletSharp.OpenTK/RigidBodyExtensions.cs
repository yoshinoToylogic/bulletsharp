using OpenTK;
using System.ComponentModel;

namespace BulletSharp
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class RigidBodyExtensions
    {
        public unsafe static void Translate(this RigidBody native, ref OpenTK.Vector3 v)
        {
            fixed (OpenTK.Vector3* value = &v)
            {
                native.Translate(*(BulletSharp.Math.Vector3*)value);
            }
        }
    }
}
