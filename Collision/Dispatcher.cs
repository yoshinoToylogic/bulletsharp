using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class DispatcherInfo
	{
		internal IntPtr _native;

        internal DispatcherInfo(IntPtr native)
        {
            _native = native;
        }

		public DispatcherInfo()
		{
			_native = btDispatcherInfo_new();
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
				btDispatcherInfo_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~DispatcherInfo()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcherInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcherInfo_delete(IntPtr obj);
	}

	public class Dispatcher
	{
		internal IntPtr _native;

        internal Dispatcher(IntPtr native)
        {
            _native = native;
        }

		public IntPtr AllocateCollisionAlgorithm(int size)
		{
			return btDispatcher_allocateCollisionAlgorithm(_native, size);
		}
        /*
		public void ClearManifold(PersistentManifold manifold)
		{
			btDispatcher_clearManifold(_native, manifold._native);
		}
        */
		public void DispatchAllCollisionPairs(OverlappingPairCache pairCache, DispatcherInfo dispatchInfo, Dispatcher dispatcher)
		{
			btDispatcher_dispatchAllCollisionPairs(_native, pairCache._native, dispatchInfo._native, dispatcher._native);
		}
        /*
		public CollisionAlgorithm FindAlgorithm(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, PersistentManifold sharedManifold)
		{
			return btDispatcher_findAlgorithm(_native, body0Wrap._native, body1Wrap._native, sharedManifold._native);
		}

		public CollisionAlgorithm FindAlgorithm(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
		{
			return btDispatcher_findAlgorithm2(_native, body0Wrap._native, body1Wrap._native);
		}
        */
		public void FreeCollisionAlgorithm(IntPtr ptr)
		{
			btDispatcher_freeCollisionAlgorithm(_native, ptr);
		}
        /*
		public PersistentManifold GetManifoldByIndexInternal(int index)
		{
			return btDispatcher_getManifoldByIndexInternal(_native, index);
		}

		public PersistentManifold GetNewManifold(CollisionObject b0, CollisionObject b1)
		{
			return btDispatcher_getNewManifold(_native, b0._native, b1._native);
		}
        */
		public bool NeedsCollision(CollisionObject body0, CollisionObject body1)
		{
			return btDispatcher_needsCollision(_native, body0._native, body1._native);
		}

		public bool NeedsResponse(CollisionObject body0, CollisionObject body1)
		{
			return btDispatcher_needsResponse(_native, body0._native, body1._native);
		}
        /*
		public void ReleaseManifold(PersistentManifold manifold)
		{
			btDispatcher_releaseManifold(_native, manifold._native);
		}

		public PersistentManifold InternalManifoldPointer
		{
			get { return btDispatcher_getInternalManifoldPointer(_native); }
		}

		public btPoolAllocator InternalManifoldPool
		{
			get { return btDispatcher_getInternalManifoldPool(_native); }
		}
        */
		public int NumManifolds
		{
			get { return btDispatcher_getNumManifolds(_native); }
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
				btDispatcher_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~Dispatcher()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_allocateCollisionAlgorithm(IntPtr obj, int size);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcher_clearManifold(IntPtr obj, IntPtr manifold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcher_dispatchAllCollisionPairs(IntPtr obj, IntPtr pairCache, IntPtr dispatchInfo, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_findAlgorithm(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr sharedManifold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_findAlgorithm2(IntPtr obj, IntPtr body0Wrap, IntPtr body1Wrap);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcher_freeCollisionAlgorithm(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_getInternalManifoldPointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_getInternalManifoldPool(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_getManifoldByIndexInternal(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDispatcher_getNewManifold(IntPtr obj, IntPtr b0, IntPtr b1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDispatcher_getNumManifolds(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btDispatcher_needsCollision(IntPtr obj, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btDispatcher_needsResponse(IntPtr obj, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcher_releaseManifold(IntPtr obj, IntPtr manifold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDispatcher_delete(IntPtr obj);
	}
}
