using BulletSharp.Math;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class DbvtProxy : BroadphaseProxy
	{
		internal DbvtProxy(IntPtr native)
			: base(native)
		{
		}

		public DbvtProxy(Vector3 aabbMin, Vector3 aabbMax, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask)
			: base(btDbvtProxy_new(ref aabbMin, ref aabbMax, userPtr, collisionFilterGroup, collisionFilterMask))
		{
		}
        /*
		public DbvtNode Leaf
		{
			get { return btDbvtProxy_getLeaf(_native); }
			set { btDbvtProxy_setLeaf(_native, value._native); }
		}

		public DbvtProxy Links
		{
			get { return btDbvtProxy_getLinks(_native); }
			set { btDbvtProxy_setLinks(_native, value._native); }
		}
        */
		public int Stage
		{
			get { return btDbvtProxy_getStage(_native); }
			set { btDbvtProxy_setStage(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtProxy_new([In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtProxy_getLeaf(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtProxy_getLinks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtProxy_getStage(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtProxy_setLeaf(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtProxy_setLinks(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtProxy_setStage(IntPtr obj, int value);
	}

	public class DbvtBroadphase : BroadphaseInterface
	{
		internal DbvtBroadphase(IntPtr native)
			: base(native)
		{
		}

		public DbvtBroadphase(OverlappingPairCache paircache)
			: base(btDbvtBroadphase_new(paircache._native))
		{
            _pairCache = paircache;
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

		public int Cid
		{
			get { return btDbvtBroadphase_getCid(_native); }
			set { btDbvtBroadphase_setCid(_native, value); }
		}

		public int Cupdates
		{
			get { return btDbvtBroadphase_getCupdates(_native); }
			set { btDbvtBroadphase_setCupdates(_native, value); }
		}

		public bool Deferedcollide
		{
			get { return btDbvtBroadphase_getDeferedcollide(_native); }
			set { btDbvtBroadphase_setDeferedcollide(_native, value); }
		}

		public int Dupdates
		{
			get { return btDbvtBroadphase_getDupdates(_native); }
			set { btDbvtBroadphase_setDupdates(_native, value); }
		}

		public int Fixedleft
		{
			get { return btDbvtBroadphase_getFixedleft(_native); }
			set { btDbvtBroadphase_setFixedleft(_native, value); }
		}

		public int Fupdates
		{
			get { return btDbvtBroadphase_getFupdates(_native); }
			set { btDbvtBroadphase_setFupdates(_native, value); }
		}

		public int Gid
		{
			get { return btDbvtBroadphase_getGid(_native); }
			set { btDbvtBroadphase_setGid(_native, value); }
		}

		public bool Needcleanup
		{
			get { return btDbvtBroadphase_getNeedcleanup(_native); }
			set { btDbvtBroadphase_setNeedcleanup(_native, value); }
		}

		public int Newpairs
		{
			get { return btDbvtBroadphase_getNewpairs(_native); }
			set { btDbvtBroadphase_setNewpairs(_native, value); }
		}

		public OverlappingPairCache Paircache
		{
            get { return new OverlappingPairCache(btDbvtBroadphase_getPaircache(_native), true); }
			set { btDbvtBroadphase_setPaircache(_native, value._native); }
		}

		public int Pid
		{
			get { return btDbvtBroadphase_getPid(_native); }
			set { btDbvtBroadphase_setPid(_native, value); }
		}

		public float Prediction
		{
			get { return btDbvtBroadphase_getPrediction(_native); }
			set { btDbvtBroadphase_setPrediction(_native, value); }
		}

		public bool Releasepaircache
		{
			get { return btDbvtBroadphase_getReleasepaircache(_native); }
			set { btDbvtBroadphase_setReleasepaircache(_native, value); }
		}
        /*
		public Dbvt Sets
		{
			get { return btDbvtBroadphase_getSets(_native); }
			set { btDbvtBroadphase_setSets(_native, value._native); }
		}
        */
		public int StageCurrent
		{
			get { return btDbvtBroadphase_getStageCurrent(_native); }
			set { btDbvtBroadphase_setStageCurrent(_native, value); }
		}
        /*
		public DbvtProxy StageRoots
		{
			get { return btDbvtBroadphase_getStageRoots(_native); }
			set { btDbvtBroadphase_setStageRoots(_native, value._native); }
		}
        */
		public uint UpdatesCall
		{
			get { return btDbvtBroadphase_getUpdates_call(_native); }
			set { btDbvtBroadphase_setUpdates_call(_native, value); }
		}

		public uint UpdatesDone
		{
			get { return btDbvtBroadphase_getUpdates_done(_native); }
			set { btDbvtBroadphase_setUpdates_done(_native, value); }
		}

		public float UpdatesRatio
		{
			get { return btDbvtBroadphase_getUpdates_ratio(_native); }
			set { btDbvtBroadphase_setUpdates_ratio(_native, value); }
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
		static extern int btDbvtBroadphase_getCid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getCupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btDbvtBroadphase_getDeferedcollide(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getDupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getFixedleft(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getFupdates(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getGid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btDbvtBroadphase_getNeedcleanup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getNewpairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtBroadphase_getPaircache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getPid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDbvtBroadphase_getPrediction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btDbvtBroadphase_getReleasepaircache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtBroadphase_getSets(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btDbvtBroadphase_getStageCurrent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDbvtBroadphase_getStageRoots(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern uint btDbvtBroadphase_getUpdates_call(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern uint btDbvtBroadphase_getUpdates_done(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDbvtBroadphase_getUpdates_ratio(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btDbvtBroadphase_getVelocityPrediction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_optimize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_performDeferredRemoval(IntPtr obj, IntPtr dispatcher);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setAabbForceUpdate(IntPtr obj, IntPtr absproxy, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax, IntPtr __unnamed3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setCid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setCupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setDeferedcollide(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setDupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setFixedleft(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setFupdates(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setGid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setNeedcleanup(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setNewpairs(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setPaircache(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setPid(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setPrediction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setReleasepaircache(IntPtr obj, bool value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setSets(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setStageCurrent(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setStageRoots(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setUpdates_call(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setUpdates_done(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setUpdates_ratio(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDbvtBroadphase_setVelocityPrediction(IntPtr obj, float prediction);
	}
}
