using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class DefaultCollisionConfiguration : CollisionConfiguration
    {
        internal DefaultCollisionConfiguration(IntPtr native)
            : base(native)
        {
        }

        public DefaultCollisionConfiguration()
            : base(btDefaultCollisionConfiguration_new2())
        {
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDefaultCollisionConfiguration_new(IntPtr constructionInfo);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDefaultCollisionConfiguration_new2();
    }
}
