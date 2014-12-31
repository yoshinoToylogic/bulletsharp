using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class Convex2DConvex2DAlgorithm : ActivatingCollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
            VoronoiSimplexSolver _simplexSolver;
            ConvexPenetrationDepthSolver _pdSolver;

			public CreateFunc(VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver pdSolver)
				: base(btConvex2dConvex2dAlgorithm_CreateFunc_new(simplexSolver._native, pdSolver._native))
			{
                _simplexSolver = simplexSolver;
                _pdSolver = pdSolver;
			}

			public int MinimumPointsPerturbationThreshold
			{
				get { return btConvex2dConvex2dAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(_native); }
				set { btConvex2dConvex2dAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(_native, value); }
			}

			public int NumPerturbationIterations
			{
				get { return btConvex2dConvex2dAlgorithm_CreateFunc_getNumPerturbationIterations(_native); }
				set { btConvex2dConvex2dAlgorithm_CreateFunc_setNumPerturbationIterations(_native, value); }
			}

			public ConvexPenetrationDepthSolver PdSolver
			{
                get { return _pdSolver; }
                set
                {
                    _pdSolver = value;
                    btConvex2dConvex2dAlgorithm_CreateFunc_setPdSolver(_native, value._native);
                }
			}

			public VoronoiSimplexSolver SimplexSolver
			{
                get { return _simplexSolver; }
                set
                {
                    _simplexSolver = value;
                    btConvex2dConvex2dAlgorithm_CreateFunc_setSimplexSolver(_native, value._native);
                }
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btConvex2dConvex2dAlgorithm_CreateFunc_new(IntPtr simplexSolver, IntPtr pdSolver);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btConvex2dConvex2dAlgorithm_CreateFunc_getMinimumPointsPerturbationThreshold(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btConvex2dConvex2dAlgorithm_CreateFunc_getNumPerturbationIterations(IntPtr obj);
			//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			//static extern IntPtr btConvex2dConvex2dAlgorithm_CreateFunc_getPdSolver(IntPtr obj);
			//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			//static extern IntPtr btConvex2dConvex2dAlgorithm_CreateFunc_getSimplexSolver(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setMinimumPointsPerturbationThreshold(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setNumPerturbationIterations(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setPdSolver(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btConvex2dConvex2dAlgorithm_CreateFunc_setSimplexSolver(IntPtr obj, IntPtr value);
		}

		public Convex2DConvex2DAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold)
			: base(btConvex2dConvex2dAlgorithm_new(mf._native, ci._native, body0Wrap._native, body1Wrap._native, simplexSolver._native, pdSolver._native, numPerturbationIterations, minimumPointsPerturbationThreshold))
		{
		}

		public void SetLowLevelOfDetail(bool useLowLevel)
		{
			btConvex2dConvex2dAlgorithm_setLowLevelOfDetail(_native, useLowLevel);
		}

		public PersistentManifold Manifold
		{
            get { return new PersistentManifold(btConvex2dConvex2dAlgorithm_getManifold(_native), true); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvex2dConvex2dAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr body0Wrap, IntPtr body1Wrap, IntPtr simplexSolver, IntPtr pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvex2dConvex2dAlgorithm_getManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvex2dConvex2dAlgorithm_setLowLevelOfDetail(IntPtr obj, bool useLowLevel);
	}
}
