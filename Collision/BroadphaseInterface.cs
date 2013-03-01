using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class BroadphaseInterface : IDisposable
    {
        internal IntPtr _native;

        internal BroadphaseInterface(IntPtr native)
        {
            _native = native;
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr btBroadphaseInterface_delete(IntPtr obj);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_native != IntPtr.Zero)
            {
                btBroadphaseInterface_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~BroadphaseInterface()
        {
            Dispose(false);
        }
    }
}
