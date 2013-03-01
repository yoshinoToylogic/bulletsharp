using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class DbvtBroadphase : BroadphaseInterface
    {
        public DbvtBroadphase()
            : base(btDbvtBroadphase_new())
        {
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr btDbvtBroadphase_new();
    }
}
