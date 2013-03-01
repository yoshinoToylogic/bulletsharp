using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class Dispatcher : IDisposable
    {
        internal IntPtr _native;

        internal Dispatcher(IntPtr native)
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
                btDispatcher_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~Dispatcher()
        {
            Dispose(false);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDispatcher_delete(IntPtr obj);
    }
}
