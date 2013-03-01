using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class CollisionConfiguration : IDisposable
    {
        internal IntPtr _native;

        internal CollisionConfiguration(IntPtr native)
        {
            _native = native;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_native != IntPtr.Zero)
            {
                btCollisionConfiguration_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~CollisionConfiguration()
        {
            Dispose(false);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionConfiguration_delete(IntPtr obj);
    }
}
