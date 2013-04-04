using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class DiscreteCollisionDetectorInterface
	{
		public class Result
		{
			internal IntPtr _native;

			internal Result(IntPtr native)
			{
				_native = native;
			}

			public void AddContactPoint(Vector3 normalOnBInWorld, Vector3 pointInWorld, float depth)
			{
				btDiscreteCollisionDetectorInterface_Result_addContactPoint(_native, ref normalOnBInWorld, ref pointInWorld, depth);
			}

			public void SetShapeIdentifiersA(int partId0, int index0)
			{
				btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersA(_native, partId0, index0);
			}

			public void SetShapeIdentifiersB(int partId1, int index1)
			{
				btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersB(_native, partId1, index1);
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
					btDiscreteCollisionDetectorInterface_Result_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~Result()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDiscreteCollisionDetectorInterface_Result_addContactPoint(IntPtr obj, [In] ref Vector3 normalOnBInWorld, [In] ref Vector3 pointInWorld, float depth);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersA(IntPtr obj, int partId0, int index0);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersB(IntPtr obj, int partId1, int index1);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDiscreteCollisionDetectorInterface_Result_delete(IntPtr obj);
		}

		public class ClosestPointInput
		{
			internal IntPtr _native;

			internal ClosestPointInput(IntPtr native)
			{
				_native = native;
			}

			public ClosestPointInput()
			{
				_native = btDiscreteCollisionDetectorInterface_ClosestPointInput_new();
			}

			public float MaximumDistanceSquared
			{
				get { return btDiscreteCollisionDetectorInterface_ClosestPointInput_getMaximumDistanceSquared(_native); }
				set { btDiscreteCollisionDetectorInterface_ClosestPointInput_setMaximumDistanceSquared(_native, value); }
			}
            /*
			public StackAlloc StackAlloc
			{
				get { return btDiscreteCollisionDetectorInterface_ClosestPointInput_getStackAlloc(_native); }
				set { btDiscreteCollisionDetectorInterface_ClosestPointInput_setStackAlloc(_native, value._native); }
			}
            */
			public Matrix TransformA
			{
                get
                {
                    Matrix value;
                    btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformA(_native, out value);
                    return value;
                }
				set { btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformA(_native, ref value); }
			}

            public Matrix TransformB
			{
                get
                {
                    Matrix value;
                    btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformB(_native, out value);
                    return value;
                }
				set { btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformB(_native, ref value); }
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
					btDiscreteCollisionDetectorInterface_ClosestPointInput_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~ClosestPointInput()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDiscreteCollisionDetectorInterface_ClosestPointInput_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btDiscreteCollisionDetectorInterface_ClosestPointInput_getMaximumDistanceSquared(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btDiscreteCollisionDetectorInterface_ClosestPointInput_getStackAlloc(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformA(IntPtr obj, [Out] out Matrix value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformB(IntPtr obj, [Out] out Matrix value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_setMaximumDistanceSquared(IntPtr obj, float value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_setStackAlloc(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformA(IntPtr obj, [In] ref Matrix value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformB(IntPtr obj, [In] ref Matrix value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btDiscreteCollisionDetectorInterface_ClosestPointInput_delete(IntPtr obj);
		}

		internal IntPtr _native;

		internal DiscreteCollisionDetectorInterface(IntPtr native)
		{
			_native = native;
		}

		public void GetClosestPoints(ClosestPointInput input, Result output, IDebugDraw debugDraw, bool swapResults)
		{
			btDiscreteCollisionDetectorInterface_getClosestPoints(_native, input._native, output._native, DebugDraw.GetUnmanaged(debugDraw), swapResults);
		}

		public void GetClosestPoints(ClosestPointInput input, Result output, IDebugDraw debugDraw)
		{
			btDiscreteCollisionDetectorInterface_getClosestPoints2(_native, input._native, output._native, DebugDraw.GetUnmanaged(debugDraw));
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
				btDiscreteCollisionDetectorInterface_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~DiscreteCollisionDetectorInterface()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteCollisionDetectorInterface_getClosestPoints(IntPtr obj, IntPtr input, IntPtr output, IntPtr debugDraw, bool swapResults);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteCollisionDetectorInterface_getClosestPoints2(IntPtr obj, IntPtr input, IntPtr output, IntPtr debugDraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btDiscreteCollisionDetectorInterface_delete(IntPtr obj);
	}

    public class StorageResult : DiscreteCollisionDetectorInterface.Result
	{
		internal StorageResult(IntPtr native)
			: base(native)
		{
		}

		public StorageResult()
			: base(btStorageResult_new())
		{
		}

		public Vector3 ClosestPointInB
		{
            get
            {
                Vector3 value;
                btStorageResult_getClosestPointInB(_native, out value);
                return value;
            }
			set { btStorageResult_setClosestPointInB(_native, ref value); }
		}

		public float Distance
		{
			get { return btStorageResult_getDistance(_native); }
			set { btStorageResult_setDistance(_native, value); }
		}

		public Vector3 NormalOnSurfaceB
		{
            get
            {
                Vector3 value;
                btStorageResult_getNormalOnSurfaceB(_native, out value);
                return value;
            }
			set { btStorageResult_setNormalOnSurfaceB(_native, ref value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btStorageResult_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStorageResult_getClosestPointInB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btStorageResult_getDistance(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btStorageResult_getNormalOnSurfaceB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStorageResult_setClosestPointInB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btStorageResult_setDistance(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btStorageResult_setNormalOnSurfaceB(IntPtr obj, [In] ref Vector3 value);
	}
}
