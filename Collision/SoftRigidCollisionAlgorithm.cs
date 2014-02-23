using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class SoftRigidCollisionAlgorithm : CollisionAlgorithm
	{
		public class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native)
			{
			}

			public CreateFunc()
				: base(btSoftRigidCollisionAlgorithm_CreateFunc_new())
			{
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btSoftRigidCollisionAlgorithm_CreateFunc_new();
		}

		internal SoftRigidCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public SoftRigidCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci, CollisionObjectWrapper col0, CollisionObjectWrapper col1Wrap, bool isSwapped)
			: base(btSoftRigidCollisionAlgorithm_new(mf._native, ci._native, col0._native, col1Wrap._native, isSwapped))
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftRigidCollisionAlgorithm_new(IntPtr mf, IntPtr ci, IntPtr col0, IntPtr col1Wrap, bool isSwapped);
	}
}
