using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp.SoftBody
{
	public class SparseSdf
	{
		internal IntPtr _native;

		internal SparseSdf(IntPtr native)
		{
			_native = native;
		}

		public SparseSdf()
		{
			_native = btSparseSdf_new();
		}

        public void GarbageCollect(int lifetime)
        {
            throw new NotImplementedException();
        }

        public void GarbageCollect()
        {
            throw new NotImplementedException();
        }

        public void Initialize(int hashSize)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public int RemoveReferences(CollisionShape pcs)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
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
				btSparseSdf_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~SparseSdf()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSparseSdf_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSparseSdf_delete(IntPtr obj);
	}
}
