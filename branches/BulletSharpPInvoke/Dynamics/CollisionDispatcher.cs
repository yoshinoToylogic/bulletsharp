using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class CollisionDispatcher : Dispatcher
    {
        internal CollisionDispatcher(IntPtr native)
            : base(native)
        {
        }

        public CollisionDispatcher(CollisionConfiguration config)
            : base(btCollisionDispatcher_new(config._native))
        {
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionDispatcher_new(IntPtr config);
    }
}
