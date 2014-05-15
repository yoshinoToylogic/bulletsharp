﻿using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class BroadphaseAabbCallback
	{
		internal IntPtr _native;

		internal BroadphaseAabbCallback(IntPtr native)
		{
			_native = native;
		}

		public bool Process(BroadphaseProxy proxy)
		{
			return btBroadphaseAabbCallback_process(_native, proxy._native);
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
				btBroadphaseAabbCallback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~BroadphaseAabbCallback()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btBroadphaseAabbCallback_process(IntPtr obj, IntPtr proxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseAabbCallback_delete(IntPtr obj);
	}

	public class BroadphaseRayCallback : BroadphaseAabbCallback
	{
		internal BroadphaseRayCallback(IntPtr native)
			: base(native)
		{
		}

		public BroadphaseRayCallback()
			: base(btBroadphaseRayCallback_new())
		{
		}

		public float Lambda_max
		{
			get { return btBroadphaseRayCallback_getLambda_max(_native); }
			set { btBroadphaseRayCallback_setLambda_max(_native, value); }
		}

		public Vector3 RayDirectionInverse
		{
            get
            {
                Vector3 value;
                btBroadphaseRayCallback_getRayDirectionInverse(_native, out value);
                return value;
            }
			set { btBroadphaseRayCallback_setRayDirectionInverse(_native, ref value); }
		}
        /*
		public uint Signs
		{
			get { return btBroadphaseRayCallback_getSigns(_native); }
			set { btBroadphaseRayCallback_setSigns(_native, value._native); }
		}
        */
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphaseRayCallback_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btBroadphaseRayCallback_getLambda_max(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseRayCallback_getRayDirectionInverse(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphaseRayCallback_getSigns(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseRayCallback_setLambda_max(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseRayCallback_setRayDirectionInverse(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseRayCallback_setSigns(IntPtr obj, IntPtr value);
	}

	public class BroadphaseInterface
	{
		internal IntPtr _native;

        protected OverlappingPairCache _pairCache;
        internal List<CollisionWorld> _worldRefs = new List<CollisionWorld>(1);
        internal bool _worldDeferredCleanup;

        internal static BroadphaseInterface GetManaged(IntPtr obj)
        {
            if (obj == IntPtr.Zero)
            {
                return null;
            }

            return new BroadphaseInterface(obj);
        }

		internal BroadphaseInterface(IntPtr native)
		{
			_native = native;
		}

        public void AabbTest(ref Vector3 aabbMin, ref Vector3 aabbMax, BroadphaseAabbCallback callback)
        {
            btBroadphaseInterface_aabbTest(_native, ref aabbMin, ref aabbMax, callback._native);
        }

		public void AabbTest(Vector3 aabbMin, Vector3 aabbMax, BroadphaseAabbCallback callback)
		{
			btBroadphaseInterface_aabbTest(_native, ref aabbMin, ref aabbMax, callback._native);
		}

		public void CalculateOverlappingPairs(Dispatcher dispatcher)
		{
			btBroadphaseInterface_calculateOverlappingPairs(_native, dispatcher._native);
		}

        public BroadphaseProxy CreateProxy(ref Vector3 aabbMin, ref Vector3 aabbMax, int shapeType, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
        {
            return new BroadphaseProxy(btBroadphaseInterface_createProxy(_native, ref aabbMin, ref aabbMax, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher._native, multiSapProxy));
        }

		public BroadphaseProxy CreateProxy(Vector3 aabbMin, Vector3 aabbMax, int shapeType, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
		{
			return new BroadphaseProxy(btBroadphaseInterface_createProxy(_native, ref aabbMin, ref aabbMax, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher._native, multiSapProxy));
		}

		public void DestroyProxy(BroadphaseProxy proxy, Dispatcher dispatcher)
		{
			btBroadphaseInterface_destroyProxy(_native, proxy._native, dispatcher._native);
		}

        public void GetAabb(BroadphaseProxy proxy, out Vector3 aabbMin, out Vector3 aabbMax)
		{
            btBroadphaseInterface_getAabb(_native, proxy._native, out aabbMin, out aabbMax);
		}

        public void GetBroadphaseAabb(out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btBroadphaseInterface_getBroadphaseAabb(_native, out aabbMin, out aabbMax);
		}

		public void PrintStats()
		{
			btBroadphaseInterface_printStats(_native);
		}

        public void RayTest(ref Vector3 rayFrom, ref Vector3 rayTo, BroadphaseRayCallback rayCallback, ref Vector3 aabbMin, ref Vector3 aabbMax)
        {
            btBroadphaseInterface_rayTest(_native, ref rayFrom, ref rayTo, rayCallback._native, ref aabbMin, ref aabbMax);
        }

		public void RayTest(Vector3 rayFrom, Vector3 rayTo, BroadphaseRayCallback rayCallback, Vector3 aabbMin, Vector3 aabbMax)
		{
			btBroadphaseInterface_rayTest(_native, ref rayFrom, ref rayTo, rayCallback._native, ref aabbMin, ref aabbMax);
		}

        public void RayTest(ref Vector3 rayFrom, ref Vector3 rayTo, BroadphaseRayCallback rayCallback)
        {
            btBroadphaseInterface_rayTest3(_native, ref rayFrom, ref rayTo, rayCallback._native);
        }

		public void RayTest(Vector3 rayFrom, Vector3 rayTo, BroadphaseRayCallback rayCallback)
		{
			btBroadphaseInterface_rayTest3(_native, ref rayFrom, ref rayTo, rayCallback._native);
		}

		public void ResetPool(Dispatcher dispatcher)
		{
			btBroadphaseInterface_resetPool(_native, dispatcher._native);
		}

        public void SetAabb(BroadphaseProxy proxy, ref Vector3 aabbMin, ref Vector3 aabbMax, Dispatcher dispatcher)
        {
            btBroadphaseInterface_setAabb(_native, proxy._native, ref aabbMin, ref aabbMax, dispatcher._native);
        }

		public void SetAabb(BroadphaseProxy proxy, Vector3 aabbMin, Vector3 aabbMax, Dispatcher dispatcher)
		{
            SetAabb(proxy, ref aabbMin, ref aabbMax, dispatcher);
		}

		public OverlappingPairCache OverlappingPairCache
		{
            get
            {
                if (_pairCache == null)
                {
                    _pairCache = new OverlappingPairCache(btBroadphaseInterface_getOverlappingPairCache(_native), true);
                }
                return _pairCache;
            }
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
                if (_worldRefs.Count == 0)
                {
                    btBroadphaseInterface_delete(_native);
                    _native = IntPtr.Zero;
                }
                else
                {
                    // Can't delete broadphase, because it is referenced by a world,
                    // tell the world to clean up the broadphase later.
                    _worldDeferredCleanup = true;
                }
			}
		}

		~BroadphaseInterface()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_aabbTest(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr callback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_calculateOverlappingPairs(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphaseInterface_createProxy(IntPtr obj, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, int shapeType, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, IntPtr dispatcher, IntPtr multiSapProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_destroyProxy(IntPtr obj, IntPtr proxy, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btBroadphaseInterface_getAabb(IntPtr obj, IntPtr proxy, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btBroadphaseInterface_getBroadphaseAabb(IntPtr obj, [Out] out Vector3 aabbMin, [Out] out Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphaseInterface_getOverlappingPairCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_printStats(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr rayCallback, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_rayTest2(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr rayCallback, [In] ref Vector3 aabbMin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_rayTest3(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr rayCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_resetPool(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_setAabb(IntPtr obj, IntPtr proxy, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseInterface_delete(IntPtr obj);
	}
}
