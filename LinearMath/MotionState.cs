using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MotionState : IDisposable
	{
		internal IntPtr _native;

		internal MotionState(IntPtr native)
		{
			_native = native;
		}

		public void GetWorldTransform(out Matrix worldTrans)
		{
			btMotionState_getWorldTransform(_native, out worldTrans);
		}

		public void SetWorldTransform(ref Matrix worldTrans)
		{
			btMotionState_setWorldTransform(_native, ref worldTrans);
		}

        public Matrix WorldTransform
        {
            get
            {
                Matrix transform;
                btMotionState_getWorldTransform(_native, out transform);
                return transform;
            }
            set { btMotionState_setWorldTransform(_native, ref value); }
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
				btMotionState_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~MotionState()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMotionState_getWorldTransform(IntPtr obj, [Out] out Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMotionState_setWorldTransform(IntPtr obj, [In] ref Matrix worldTrans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMotionState_delete(IntPtr obj);
	}
}
