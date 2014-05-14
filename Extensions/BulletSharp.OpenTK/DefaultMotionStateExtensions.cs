using OpenTK;
using BulletSharp.Math;
using System.ComponentModel;

namespace BulletSharp
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DefaultMotionStateExtensions
    {
        public unsafe static void SetGraphicsWorldTransform(this DefaultMotionState native, ref Matrix4 transform)
        {
            fixed (Matrix4* value = &transform)
            {
                native.GraphicsWorldTrans = *(Matrix*)value;
            }
        }

        public unsafe static void SetStartWorldTransform(this DefaultMotionState native, ref Matrix4 transform)
        {
            fixed (Matrix4* value = &transform)
            {
                native.StartWorldTrans = *(Matrix*)value;
            }
        }
    }
}
