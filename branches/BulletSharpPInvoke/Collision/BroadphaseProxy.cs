using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public enum BroadphaseNativeType
    {
        // polyhedral convex shapes
        BoxShape,
        TriangleShape,
        TetrahedralShape,
        ConvexTriangleMeshShape,
        ConvexHullShape,
        CONVEX_POINT_CLOUD_SHAPE_PROXYTYPE,
        CUSTOM_POLYHEDRAL_SHAPE_TYPE,
        //implicit convex shapes
        IMPLICIT_CONVEX_SHAPES_START_HERE,
        SphereShape,
        MultiSphereShape,
        CapsuleShape,
        ConeShape,
        ConvexShape,
        CylinderShape,
        UniformScalingShape,
        MINKOWSKI_SUM_SHAPE_PROXYTYPE,
        MinkowskiDifferenceShape,
        Box2DShape,
        Convex2DShape,
        CUSTOM_CONVEX_SHAPE_TYPE,
        //concave shapes
        CONCAVE_SHAPES_START_HERE,
        //keep all the convex shapetype below here, for the check IsConvexShape in broadphase proxy!
        TriangleMeshShape,
        SCALED_TRIANGLE_MESH_SHAPE_PROXYTYPE,
        ///used for demo integration FAST/Swift collision library and Bullet
        FAST_CONCAVE_MESH_PROXYTYPE,
        //terrain
        TERRAIN_SHAPE_PROXYTYPE,
        ///Used for GIMPACT Trimesh integration
        GImpactShape,
        ///Multimaterial mesh
        MULTIMATERIAL_TRIANGLE_MESH_PROXYTYPE,

        EmptyShape,
        StaticPlane,
        CUSTOM_CONCAVE_SHAPE_TYPE,
        CONCAVE_SHAPES_END_HERE,

        CompoundShape,

        SoftBodyShape,
        HFFLUID_SHAPE_PROXYTYPE,
        HFFLUID_BUOYANT_CONVEX_SHAPE_PROXYTYPE,
        INVALID_SHAPE_PROXYTYPE,

        MAX_BROADPHASE_COLLISION_TYPES
    };

	public class BroadphaseProxy
	{
		internal IntPtr _native;

        internal BroadphaseProxy(IntPtr native)
        {
            _native = native;
        }

		public BroadphaseProxy()
		{
			_native = btBroadphaseProxy_new();
		}
        /*
		public BroadphaseProxy(Vector3 aabbMin, Vector3 aabbMax, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, IntPtr multiSapParentProxy)
		{
			_native = btBroadphaseProxy_new2(aabbMin._native, aabbMax._native, userPtr._native, collisionFilterGroup, collisionFilterMask, multiSapParentProxy._native);
		}

		public BroadphaseProxy(Vector3 aabbMin, Vector3 aabbMax, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask)
		{
			_native = btBroadphaseProxy_new3(aabbMin._native, aabbMax._native, userPtr._native, collisionFilterGroup, collisionFilterMask);
		}
        */
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btBroadphaseProxy_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~BroadphaseProxy()
		{
			Dispose(false);
		}

		public bool IsCompound(int proxyType)
		{
			return btBroadphaseProxy_isCompound(proxyType);
		}

		public bool IsConcave(int proxyType)
		{
			return btBroadphaseProxy_isConcave(proxyType);
		}

		public bool IsConvex(int proxyType)
		{
			return btBroadphaseProxy_isConvex(proxyType);
		}

		public bool IsConvex2d(int proxyType)
		{
			return btBroadphaseProxy_isConvex2d(proxyType);
		}

		public bool IsInfinite(int proxyType)
		{
			return btBroadphaseProxy_isInfinite(proxyType);
		}

		public bool IsNonMoving(int proxyType)
		{
			return btBroadphaseProxy_isNonMoving(proxyType);
		}

		public bool IsPolyhedral(int proxyType)
		{
			return btBroadphaseProxy_isPolyhedral(proxyType);
		}

		public bool IsSoftBody(int proxyType)
		{
			return btBroadphaseProxy_isSoftBody(proxyType);
		}

		public int Uid
		{
			get { return btBroadphaseProxy_getUid(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphaseProxy_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphaseProxy_new2(IntPtr aabbMin, IntPtr aabbMax, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, IntPtr multiSapParentProxy);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphaseProxy_new3(IntPtr aabbMin, IntPtr aabbMax, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphaseProxy_delete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btBroadphaseProxy_getUid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btBroadphaseProxy_isCompound(int proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btBroadphaseProxy_isConcave(int proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btBroadphaseProxy_isConvex(int proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btBroadphaseProxy_isConvex2d(int proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btBroadphaseProxy_isInfinite(int proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btBroadphaseProxy_isNonMoving(int proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btBroadphaseProxy_isPolyhedral(int proxyType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btBroadphaseProxy_isSoftBody(int proxyType);
	}

	public class BroadphasePair
	{
		internal IntPtr _native;

		public BroadphasePair()
		{
			_native = btBroadphasePair_new();
		}

		public BroadphasePair(BroadphasePair other)
		{
			_native = btBroadphasePair_new2(other._native);
		}

		public BroadphasePair(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			_native = btBroadphasePair_new3(proxy0._native, proxy1._native);
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
				btBroadphasePair_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~BroadphasePair()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphasePair_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphasePair_new2(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphasePair_new3(IntPtr proxy0, IntPtr proxy1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphasePair_delete(IntPtr obj);
	}

	public class BroadphasePairSortPredicate
	{
		internal IntPtr _native;

		public BroadphasePairSortPredicate()
		{
			_native = btBroadphasePairSortPredicate_new();
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
				btBroadphasePairSortPredicate_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~BroadphasePairSortPredicate()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBroadphasePairSortPredicate_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBroadphasePairSortPredicate_delete(IntPtr obj);
	}
}
