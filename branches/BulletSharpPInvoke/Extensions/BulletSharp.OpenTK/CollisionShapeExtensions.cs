using OpenTK;
using System.ComponentModel;

namespace BulletSharp
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class CollisionShapeExtensions
    {
        public unsafe static void CalculateLocalInertia(this CollisionShape native, float mass, out OpenTK.Vector3 localInertia)
        {
            fixed (OpenTK.Vector3* value = &localInertia)
            {
                native.CalculateLocalInertia(mass, out *(BulletSharp.Math.Vector3*)value);
            }
        }
    }
}
