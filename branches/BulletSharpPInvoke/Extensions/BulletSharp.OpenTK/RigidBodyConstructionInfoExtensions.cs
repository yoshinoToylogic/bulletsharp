using BulletSharp.Math;
using OpenTK;
using System.ComponentModel;

namespace BulletSharp
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class RigidBodyConstructionInfoExtensions
    {
        public unsafe static void SetLocalInertia(this RigidBodyConstructionInfo native, ref OpenTK.Vector3 localInertia)
        {
            fixed (OpenTK.Vector3* value = &localInertia)
            {
                native.LocalInertia = *(BulletSharp.Math.Vector3*)value;
            }
        }
    }
}
