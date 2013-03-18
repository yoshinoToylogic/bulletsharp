using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class CollisionConfiguration
	{
		internal IntPtr _native;

        internal CollisionConfiguration(IntPtr native)
        {
            _native = native;
        }
        /*
		public CollisionAlgorithmCreateFunc GetCollisionAlgorithmCreateFunc(int proxyType0, int proxyType1)
		{
			return btCollisionConfiguration_getCollisionAlgorithmCreateFunc(_native, proxyType0, proxyType1);
		}

		public btPoolAllocator CollisionAlgorithmPool
		{
			get { return btCollisionConfiguration_getCollisionAlgorithmPool(_native); }
		}

		public btPoolAllocator PersistentManifoldPool
		{
			get { return btCollisionConfiguration_getPersistentManifoldPool(_native); }
		}

		public StackAlloc StackAllocator
		{
			get { return btCollisionConfiguration_getStackAllocator(_native); }
		}
        */
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
		static extern IntPtr btCollisionConfiguration_getCollisionAlgorithmCreateFunc(IntPtr obj, int proxyType0, int proxyType1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionConfiguration_getCollisionAlgorithmPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionConfiguration_getPersistentManifoldPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionConfiguration_getStackAllocator(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionConfiguration_delete(IntPtr obj);
	}
}
