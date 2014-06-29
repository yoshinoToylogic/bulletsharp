using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class TriangleCallback : IDisposable
	{
		internal IntPtr _native;

		internal TriangleCallback(IntPtr native)
		{
			_native = native;
		}

        public void ProcessTriangle(ref Vector3 triangle, int partId, int triangleIndex)
        {
            btTriangleCallback_processTriangle(_native, ref triangle, partId, triangleIndex);
        }

		public void ProcessTriangle(Vector3 triangle, int partId, int triangleIndex)
		{
			btTriangleCallback_processTriangle(_native, ref triangle, partId, triangleIndex);
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
				btTriangleCallback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~TriangleCallback()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleCallback_processTriangle(IntPtr obj, [In] ref Vector3 triangle, int partId, int triangleIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleCallback_delete(IntPtr obj);
	}

	public class InternalTriangleIndexCallback : IDisposable
	{
		internal IntPtr _native;

		internal InternalTriangleIndexCallback(IntPtr native)
		{
			_native = native;
		}

        public void InternalProcessTriangleIndex(ref Vector3 triangle, int partId, int triangleIndex)
        {
            btInternalTriangleIndexCallback_internalProcessTriangleIndex(_native, ref triangle, partId, triangleIndex);
        }

		public void InternalProcessTriangleIndex(Vector3 triangle, int partId, int triangleIndex)
		{
			btInternalTriangleIndexCallback_internalProcessTriangleIndex(_native, ref triangle, partId, triangleIndex);
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
				btInternalTriangleIndexCallback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~InternalTriangleIndexCallback()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btInternalTriangleIndexCallback_internalProcessTriangleIndex(IntPtr obj, [In] ref Vector3 triangle, int partId, int triangleIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btInternalTriangleIndexCallback_delete(IntPtr obj);
	}
}
