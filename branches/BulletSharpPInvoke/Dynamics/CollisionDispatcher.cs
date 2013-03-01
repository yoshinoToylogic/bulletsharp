using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public enum DispatcherFlags
    {
        StaticStaticReported = 1,
        UseRelativeContactBreakingThreshold = 2,
        DisableContactPoolDynamicAllocation = 4
    }

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

        public DispatcherFlags DispatcherFlags
        {
            get { return btCollisionDispatcher_getDispatcherFlags(_native); }
            set { btCollisionDispatcher_setDispatcherFlags(_native, value); }
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionDispatcher_new(IntPtr config);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern DispatcherFlags btCollisionDispatcher_getDispatcherFlags(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionDispatcher_setDispatcherFlags(IntPtr obj, DispatcherFlags flags);
    }
}
