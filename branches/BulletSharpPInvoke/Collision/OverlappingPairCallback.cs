using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class OverlappingPairCallback
	{
		internal IntPtr _native;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btOverlappingPairCallback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~OverlappingPairCallback()
		{
			Dispose(false);
		}
        /*
		public BroadphasePair AddOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return btOverlappingPairCallback_addOverlappingPair(_native, proxy0._native, proxy1._native);
		}
        */
		public void RemoveOverlappingPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1, Dispatcher dispatcher)
		{
			btOverlappingPairCallback_removeOverlappingPair(_native, proxy0._native, proxy1._native, dispatcher._native);
		}

		public void RemoveOverlappingPairsContainingProxy(BroadphaseProxy proxy0, Dispatcher dispatcher)
		{
			btOverlappingPairCallback_removeOverlappingPairsContainingProxy(_native, proxy0._native, dispatcher._native);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlappingPairCallback_delete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btOverlappingPairCallback_addOverlappingPair(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btOverlappingPairCallback_removeOverlappingPair(IntPtr obj, IntPtr proxy0, IntPtr proxy1, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlappingPairCallback_removeOverlappingPairsContainingProxy(IntPtr obj, IntPtr proxy0, IntPtr dispatcher);
	}
}