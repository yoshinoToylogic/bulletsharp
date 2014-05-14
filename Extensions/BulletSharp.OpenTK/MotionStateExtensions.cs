using OpenTK;
using BulletSharp.Math;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace BulletSharp
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class MotionStateExtensions
    {
        public unsafe static void SetWorldTransform(this MotionState native, ref Matrix4 transform)
        {
            fixed (Matrix4* value = &transform)
            {
                native.SetWorldTransform(ref *(Matrix*)value);
            }
        }
    }
}
