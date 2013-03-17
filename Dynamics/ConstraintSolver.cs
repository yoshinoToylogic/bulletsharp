using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class ConstraintSolver
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
				btConstraintSolver_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ConstraintSolver()
		{
			Dispose(false);
		}
        /*
		public void AllSolved(ContactSolverInfo __unnamed0, IDebugDraw __unnamed1, StackAlloc __unnamed2)
		{
			btConstraintSolver_allSolved(_native, __unnamed0._native, __unnamed1._native, __unnamed2._native);
		}
        */
		public void PrepareSolve(int __unnamed0, int __unnamed1)
		{
			btConstraintSolver_prepareSolve(_native, __unnamed0, __unnamed1);
		}

		public void Reset()
		{
			btConstraintSolver_reset(_native);
		}
        /*
		public float SolveGroup(CollisionObject bodies, int numBodies, PersistentManifold manifold, int numManifolds, TypedConstraint constraints, int numConstraints, ContactSolverInfo info, IDebugDraw debugDrawer, StackAlloc stackAlloc, Dispatcher dispatcher)
		{
			return btConstraintSolver_solveGroup(_native, bodies._native, numBodies, manifold._native, numManifolds, constraints._native, numConstraints, info._native, debugDrawer._native, stackAlloc._native, dispatcher._native);
		}
        */
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintSolver_delete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintSolver_allSolved(IntPtr obj, IntPtr __unnamed0, IntPtr __unnamed1, IntPtr __unnamed2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintSolver_prepareSolve(IntPtr obj, int __unnamed0, int __unnamed1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConstraintSolver_reset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConstraintSolver_solveGroup(IntPtr obj, IntPtr bodies, int numBodies, IntPtr manifold, int numManifolds, IntPtr constraints, int numConstraints, IntPtr info, IntPtr debugDrawer, IntPtr stackAlloc, IntPtr dispatcher);
	}
}
