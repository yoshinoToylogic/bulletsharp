using BulletSharp.Math;
using OpenTK;
using System.ComponentModel;

namespace BulletSharp
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class RigidBodyConstructionInfoExtensions
    {
        public unsafe static void SetLocalInertia(this RigidBodyConstructionInfo obj, ref OpenTK.Vector3 localInertia)
        {
            fixed (OpenTK.Vector3* value = &localInertia)
            {
                obj.LocalInertia = *(BulletSharp.Math.Vector3*)value;
            }
        }

        public unsafe static void SetStartWorldTransform(this RigidBodyConstructionInfo obj, ref OpenTK.Matrix4 startWorldTransform)
        {
            fixed (OpenTK.Matrix4* value = &startWorldTransform)
            {
                obj.StartWorldTransform = *(BulletSharp.Math.Matrix*)value;
            }
        }

        public static void SetStartWorldTransform(this RigidBodyConstructionInfo obj, OpenTK.Matrix4 startWorldTransform)
        {
            SetStartWorldTransform(obj, ref startWorldTransform);
        }
    }
}
