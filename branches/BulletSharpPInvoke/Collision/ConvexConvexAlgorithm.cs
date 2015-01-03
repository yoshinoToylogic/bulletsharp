using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class ConvexConvexAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			public CreateFunc(VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver pdSolver)
				: base(btConvexConvexAlgorithm_CreateFunc_new(simplexSolver._native, pdSolver._native))
			{
			}

			public int MinimumPointsPerturbationThreshold
			{
				get { return btConvexConvexAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(_native); }
				set { btConvexConvexAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(_native, value); }
			}

			public int NumPerturbationIterations
			{
				get { return btConvexConvexAlgorithm_CreateFunc_getNumPerturbationIterations(_native); }
				set { btConvexConvexAlgorithm_CreateFunc_setNumPerturbationIterations(_native, value); }
			}

			public ConvexPenetrationDepthSolver PdSolver
			{
                get { return new ConvexPenetrationDepthSolver(btConvexConvexAlgorithm_CreateFunc_getPdSolver(_native)); }
				set { btConvexConvexAlgorithm_CreateFunc_setPdSolver(_native, value._native); }
			}

			public VoronoiSimplexSolver SimplexSolver
			{
                get { return new VoronoiSimplexSolver(btConvexConvexAlgorithm_CreateFunc_getSimplexSolver(_native), true); }
				set { btConvexConvexAlgorithm_CreateFunc_setSimplexSolver(_native, value._native); }
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btConvexConvexAlgorithm_CreateFunc_new(IntPtr simplexSolver, IntPtr pdSolver);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btConvexConvexAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btConvexConvexAlgorithm_CreateFunc_getNumPerturbationIterations(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btConvexConvexAlgorithm_CreateFunc_getPdSolver(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btConvexConvexAlgorithm_CreateFunc_getSimplexSolver(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexConvexAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexConvexAlgorithm_CreateFunc_setNumPerturbationIterations(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexConvexAlgorithm_CreateFunc_setPdSolver(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvexConvexAlgorithm_CreateFunc_setSimplexSolver(IntPtr obj, IntPtr value);
		}

		public ConvexConvexAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold)
			: base(btConvexConvexAlgorithm_new(mf._native, ci._native, body0Wrap._native, body1Wrap._native, simplexSolver._native, pdSolver._native, numPerturbationIterations, minimumPointsPerturbationThreshold))
		{
		}

		public void SetLowLevelOfDetail(bool useLowLevel)
		{
			btConvexConvexAlgorithm_setLowLevelOfDetail(_native, useLowLevel);
		}

		public PersistentManifold Manifold
		{
			get { return new PersistentManifold(btConvexConvexAlgorithm_getManifold(_native)); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexConvexAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr simplexSolver, IntPtr pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexConvexAlgorithm_getManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexConvexAlgorithm_setLowLevelOfDetail(IntPtr obj, bool useLowLevel);
	}
}
