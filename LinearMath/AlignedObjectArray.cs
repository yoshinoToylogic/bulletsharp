using System;

namespace BulletSharp
{
    public class AlignedObjectArray
    {
        protected IntPtr _native;

        internal AlignedObjectArray(IntPtr native)
        {
            _native = native;
        }
    }
}
