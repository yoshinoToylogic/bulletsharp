using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class OverlapCallback
	{
		internal IntPtr _native;

		internal OverlapCallback(IntPtr native)
		{
			_native = native;
		}

		public bool ProcessOverlap(BroadphasePair pair)
		{
			return btOverlapCallback_processOverlap(_native, pair._native);
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
				btOverlapCallback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~OverlapCallback()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btOverlapCallback_processOverlap(IntPtr obj, IntPtr pair);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlapCallback_delete(IntPtr obj);
	}

	public class OverlapFilterCallback
	{
		internal IntPtr _native;

		internal OverlapFilterCallback(IntPtr native)
		{
			_native = native;
		}

		public bool NeedBroadphaseCollision(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return btOverlapFilterCallback_needBroadphaseCollision(_native, proxy0._native, proxy1._native);
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
				btOverlapFilterCallback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~OverlapFilterCallback()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btOverlapFilterCallback_needBroadphaseCollision(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlapFilterCallback_delete(IntPtr obj);
	}

	public class OverlappingPairCache : OverlappingPairCallback
	{
        OverlappingPairCallback _ghostPairCallback;

		internal OverlappingPairCache(IntPtr native, bool preventDelete = false)
            : base(native, preventDelete)
		{
		}

		public void CleanOverlappingPair(BroadphasePair pair, Dispatcher dispatcher)
		{
			btOverlappingPairCache_cleanOverlappingPair(_native, pair._native, dispatcher._native);
		}

		public void CleanProxyFromPairs(BroadphaseProxy proxy, Dispatcher dispatcher)
		{
			btOverlappingPairCache_cleanProxyFromPairs(_native, proxy._native, dispatcher._native);
		}

		public BroadphasePair FindPair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
            return new BroadphasePair(btOverlappingPairCache_findPair(_native, proxy0._native, proxy1._native));
		}

		public void ProcessAllOverlappingPairs(OverlapCallback __unnamed0, Dispatcher dispatcher)
		{
			btOverlappingPairCache_processAllOverlappingPairs(_native, __unnamed0._native, dispatcher._native);
		}

		public void SetInternalGhostPairCallback(OverlappingPairCallback ghostPairCallback)
		{
            _ghostPairCallback = ghostPairCallback;
			btOverlappingPairCache_setInternalGhostPairCallback(_native, ghostPairCallback._native);
		}

		public void SetOverlapFilterCallback(OverlapFilterCallback callback)
		{
			btOverlappingPairCache_setOverlapFilterCallback(_native, callback._native);
		}

		public void SortOverlappingPairs(Dispatcher dispatcher)
		{
			btOverlappingPairCache_sortOverlappingPairs(_native, dispatcher._native);
		}

		public bool HasDeferredRemoval
		{
			get { return btOverlappingPairCache_hasDeferredRemoval(_native); }
		}

		public int NumOverlappingPairs
		{
			get { return btOverlappingPairCache_getNumOverlappingPairs(_native); }
		}
        
		public AlignedBroadphasePairArray OverlappingPairArray
		{
            get { return new AlignedBroadphasePairArray(btOverlappingPairCache_getOverlappingPairArray(_native)); }
		}
        

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlappingPairCache_cleanOverlappingPair(IntPtr obj, IntPtr pair, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlappingPairCache_cleanProxyFromPairs(IntPtr obj, IntPtr proxy, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btOverlappingPairCache_findPair(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btOverlappingPairCache_getNumOverlappingPairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btOverlappingPairCache_getOverlappingPairArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btOverlappingPairCache_getOverlappingPairArrayPtr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btOverlappingPairCache_hasDeferredRemoval(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlappingPairCache_processAllOverlappingPairs(IntPtr obj, IntPtr __unnamed0, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlappingPairCache_setInternalGhostPairCallback(IntPtr obj, IntPtr ghostPairCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlappingPairCache_setOverlapFilterCallback(IntPtr obj, IntPtr callback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btOverlappingPairCache_sortOverlappingPairs(IntPtr obj, IntPtr dispatcher);
	}

	public class HashedOverlappingPairCache : OverlappingPairCache
	{
		internal HashedOverlappingPairCache(IntPtr native, bool preventDelete = false)
			: base(native, preventDelete)
		{
		}

		public HashedOverlappingPairCache()
			: base(btHashedOverlappingPairCache_new())
		{
		}

		public int GetCount()
		{
			return btHashedOverlappingPairCache_GetCount(_native);
		}

		public bool NeedsBroadphaseCollision(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return btHashedOverlappingPairCache_needsBroadphaseCollision(_native, proxy0._native, proxy1._native);
		}

		public OverlapFilterCallback OverlapFilterCallback
		{
			get { return new OverlapFilterCallback(btHashedOverlappingPairCache_getOverlapFilterCallback(_native)); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHashedOverlappingPairCache_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btHashedOverlappingPairCache_GetCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btHashedOverlappingPairCache_getOverlapFilterCallback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btHashedOverlappingPairCache_needsBroadphaseCollision(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
	}

	public class SortedOverlappingPairCache : OverlappingPairCache
	{
		internal SortedOverlappingPairCache(IntPtr native)
			: base(native)
		{
		}

		public SortedOverlappingPairCache()
			: base(btSortedOverlappingPairCache_new())
		{
		}

		public bool NeedsBroadphaseCollision(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return btSortedOverlappingPairCache_needsBroadphaseCollision(_native, proxy0._native, proxy1._native);
		}

		public OverlapFilterCallback OverlapFilterCallback
		{
            get { return new OverlapFilterCallback(btSortedOverlappingPairCache_getOverlapFilterCallback(_native)); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSortedOverlappingPairCache_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSortedOverlappingPairCache_getOverlapFilterCallback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btSortedOverlappingPairCache_needsBroadphaseCollision(IntPtr obj, IntPtr proxy0, IntPtr proxy1);
	}

	public class NullPairCache : OverlappingPairCache
	{
		internal NullPairCache(IntPtr native)
			: base(native)
		{
		}

		public NullPairCache()
			: base(btNullPairCache_new())
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btNullPairCache_new();
	}
}
