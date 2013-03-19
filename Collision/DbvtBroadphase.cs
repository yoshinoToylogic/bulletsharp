using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class DbvtProxy : BroadphaseProxy
	{
		public DbvtProxy(Vector3 aabbMin, Vector3 aabbMax, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask)
		{
			_native = btDbvtProxy_new(ref aabbMin, ref aabbMax, userPtr, collisionFilterGroup, collisionFilterMask);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDbvtProxy_new([In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask);
	}

	public class DbvtBroadphase : BroadphaseInterface
	{
		public DbvtBroadphase(OverlappingPairCache paircache)
            : base(btDbvtBroadphase_new(paircache._native))
		{
		}

		public DbvtBroadphase()
            : base(btDbvtBroadphase_new2())
		{
		}

		public void Benchmark(BroadphaseInterface __unnamed0)
		{
			btDbvtBroadphase_benchmark(__unnamed0._native);
		}

		public void Collide(Dispatcher dispatcher)
		{
			btDbvtBroadphase_collide(_native, dispatcher._native);
		}

		public void Optimize()
		{
			btDbvtBroadphase_optimize(_native);
		}

		public void PerformDeferredRemoval(Dispatcher dispatcher)
		{
			btDbvtBroadphase_performDeferredRemoval(_native, dispatcher._native);
		}

		public void SetAabbForceUpdate(BroadphaseProxy absproxy, Vector3 aabbMin, Vector3 aabbMax, Dispatcher __unnamed3)
		{
			btDbvtBroadphase_setAabbForceUpdate(_native, absproxy._native, ref aabbMin, ref aabbMax, __unnamed3._native);
		}

		public float VelocityPrediction
		{
			get { return btDbvtBroadphase_getVelocityPrediction(_native); }
			set { btDbvtBroadphase_setVelocityPrediction(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtBroadphase_new(IntPtr paircache);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtBroadphase_new2();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_benchmark(IntPtr __unnamed0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_collide(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDbvtBroadphase_getVelocityPrediction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_optimize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_performDeferredRemoval(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDbvtBroadphase_setAabbForceUpdate(IntPtr obj, IntPtr absproxy, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr __unnamed3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setVelocityPrediction(IntPtr obj, float prediction);
	}
}
