using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class GImpactQuantizedBvhNode : IDisposable
	{
		internal IntPtr _native;

		internal GImpactQuantizedBvhNode(IntPtr native)
		{
			_native = native;
		}

		public GImpactQuantizedBvhNode()
		{
			_native = BT_QUANTIZED_BVH_NODE_new();
		}

		public bool TestQuantizedBoxOverlapp(ushort[] quantizedMin, ushort[] quantizedMax)
		{
			return BT_QUANTIZED_BVH_NODE_testQuantizedBoxOverlapp(_native, quantizedMin, quantizedMax);
		}

		public int DataIndex
		{
			get { return BT_QUANTIZED_BVH_NODE_getDataIndex(_native); }
			set { BT_QUANTIZED_BVH_NODE_setDataIndex(_native, value); }
		}

		public int EscapeIndex
		{
			get { return BT_QUANTIZED_BVH_NODE_getEscapeIndex(_native); }
			set { BT_QUANTIZED_BVH_NODE_setEscapeIndex(_native, value); }
		}

		public int EscapeIndexOrDataIndex
		{
			get { return BT_QUANTIZED_BVH_NODE_getEscapeIndexOrDataIndex(_native); }
			set { BT_QUANTIZED_BVH_NODE_setEscapeIndexOrDataIndex(_native, value); }
		}

		public bool IsLeafNode
		{
			get { return BT_QUANTIZED_BVH_NODE_isLeafNode(_native); }
		}
        /*
		public UShortArray QuantizedAabbMax
		{
			get { return BT_QUANTIZED_BVH_NODE_getQuantizedAabbMax(_native); }
		}

		public UShortArray QuantizedAabbMin
		{
			get { return BT_QUANTIZED_BVH_NODE_getQuantizedAabbMin(_native); }
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
				BT_QUANTIZED_BVH_NODE_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~GImpactQuantizedBvhNode()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr BT_QUANTIZED_BVH_NODE_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int BT_QUANTIZED_BVH_NODE_getDataIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int BT_QUANTIZED_BVH_NODE_getEscapeIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int BT_QUANTIZED_BVH_NODE_getEscapeIndexOrDataIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr BT_QUANTIZED_BVH_NODE_getQuantizedAabbMax(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr BT_QUANTIZED_BVH_NODE_getQuantizedAabbMin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool BT_QUANTIZED_BVH_NODE_isLeafNode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_QUANTIZED_BVH_NODE_setDataIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_QUANTIZED_BVH_NODE_setEscapeIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_QUANTIZED_BVH_NODE_setEscapeIndexOrDataIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool BT_QUANTIZED_BVH_NODE_testQuantizedBoxOverlapp(IntPtr obj, ushort[] quantizedMin, ushort[] quantizedMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_QUANTIZED_BVH_NODE_delete(IntPtr obj);
	}

	public class GimGImpactQuantizedBvhNodeArray : AlignedObjectArray
	{
		internal GimGImpactQuantizedBvhNodeArray(IntPtr native)
			: base(native)
		{
		}

		public GimGImpactQuantizedBvhNodeArray()
			: base(GIM_QUANTIZED_BVH_NODE_ARRAY_new())
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr GIM_QUANTIZED_BVH_NODE_ARRAY_new();
	}

	public class QuantizedBvhTree : IDisposable
	{
		internal IntPtr _native;

		internal QuantizedBvhTree(IntPtr native)
		{
			_native = native;
		}

		public QuantizedBvhTree()
		{
			_native = btQuantizedBvhTree_new();
		}

		public void BuildTree(GimBvhDataArray primitiveBoxes)
		{
			btQuantizedBvhTree_build_tree(_native, primitiveBoxes._native);
		}

		public void ClearNodes()
		{
			btQuantizedBvhTree_clearNodes(_native);
		}
        /*
		public GImpactQuantizedBvhNode GetNodePointer()
		{
			return btQuantizedBvhTree_get_node_pointer(_native);
		}

		public GImpactQuantizedBvhNode GetNodePointer(int index)
		{
			return btQuantizedBvhTree_get_node_pointer2(_native, index);
		}
        */
		public int GetEscapeNodeIndex(int nodeindex)
		{
			return btQuantizedBvhTree_getEscapeNodeIndex(_native, nodeindex);
		}

		public int GetLeftNode(int nodeindex)
		{
			return btQuantizedBvhTree_getLeftNode(_native, nodeindex);
		}

		public void GetNodeBound(int nodeindex, Aabb bound)
		{
			btQuantizedBvhTree_getNodeBound(_native, nodeindex, bound._native);
		}

		public int GetNodeData(int nodeindex)
		{
			return btQuantizedBvhTree_getNodeData(_native, nodeindex);
		}

		public int GetRightNode(int nodeindex)
		{
			return btQuantizedBvhTree_getRightNode(_native, nodeindex);
		}

		public bool IsLeafNode(int nodeindex)
		{
			return btQuantizedBvhTree_isLeafNode(_native, nodeindex);
		}

		public void QuantizePoint(ushort[] quantizedpoint, Vector3 point)
		{
			btQuantizedBvhTree_quantizePoint(_native, quantizedpoint, ref point);
		}

		public void SetNodeBound(int nodeindex, Aabb bound)
		{
			btQuantizedBvhTree_setNodeBound(_native, nodeindex, bound._native);
		}

        public bool TestQuantizedBoxOverlapp(int nodeIndex, ushort[] quantizedMin, ushort[] quantizedMax)
		{
			return btQuantizedBvhTree_testQuantizedBoxOverlapp(_native, nodeIndex, quantizedMin, quantizedMax);
		}

		public int NodeCount
		{
			get { return btQuantizedBvhTree_getNodeCount(_native); }
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
				btQuantizedBvhTree_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~QuantizedBvhTree()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvhTree_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvhTree_build_tree(IntPtr obj, IntPtr primitive_boxes);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvhTree_clearNodes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvhTree_get_node_pointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btQuantizedBvhTree_get_node_pointer2(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btQuantizedBvhTree_getEscapeNodeIndex(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btQuantizedBvhTree_getLeftNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvhTree_getNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btQuantizedBvhTree_getNodeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btQuantizedBvhTree_getNodeData(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btQuantizedBvhTree_getRightNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btQuantizedBvhTree_isLeafNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvhTree_quantizePoint(IntPtr obj, ushort[] quantizedpoint, [In] ref Vector3 point);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvhTree_setNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btQuantizedBvhTree_testQuantizedBoxOverlapp(IntPtr obj, int node_index, ushort[] quantizedMin, ushort[] quantizedMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btQuantizedBvhTree_delete(IntPtr obj);
	}

	public class GImpactQuantizedBvh : IDisposable
	{
		internal IntPtr _native;

        private PrimitiveManagerBase _primitiveManager;

		internal GImpactQuantizedBvh(IntPtr native)
		{
			_native = native;
		}

		public GImpactQuantizedBvh()
		{
			_native = btGImpactQuantizedBvh_new();
		}

		public GImpactQuantizedBvh(PrimitiveManagerBase primitiveManager)
		{
		    _primitiveManager = primitiveManager;
			_native = btGImpactQuantizedBvh_new2(primitiveManager._native);
		}

		public bool BoxQuery(Aabb box, AlignedObjectArray collidedResults)
		{
			return btGImpactQuantizedBvh_boxQuery(_native, box._native, collidedResults._native);
		}

		public bool BoxQueryTrans(Aabb box, Matrix transform, AlignedObjectArray collidedResults)
		{
			return btGImpactQuantizedBvh_boxQueryTrans(_native, box._native, ref transform, collidedResults._native);
		}

		public void BuildSet()
		{
			btGImpactQuantizedBvh_buildSet(_native);
		}

		public static void FindCollision(GImpactQuantizedBvh boxset1, Matrix trans1, GImpactQuantizedBvh boxset2, Matrix trans2, PairSet collisionPairs)
		{
			btGImpactQuantizedBvh_find_collision(boxset1._native, ref trans1, boxset2._native, ref trans2, collisionPairs._native);
		}
        /*
		public GImpactQuantizedBvhNode GetNodePointer()
		{
			return btGImpactQuantizedBvh_get_node_pointer(_native);
		}

		public GImpactQuantizedBvhNode GetNodePointer(int index)
		{
			return btGImpactQuantizedBvh_get_node_pointer2(_native, index);
		}
        */
		public int GetEscapeNodeIndex(int nodeindex)
		{
			return btGImpactQuantizedBvh_getEscapeNodeIndex(_native, nodeindex);
		}

		public int GetLeftNode(int nodeindex)
		{
			return btGImpactQuantizedBvh_getLeftNode(_native, nodeindex);
		}

		public void GetNodeBound(int nodeindex, Aabb bound)
		{
			btGImpactQuantizedBvh_getNodeBound(_native, nodeindex, bound._native);
		}

		public int GetNodeData(int nodeindex)
		{
			return btGImpactQuantizedBvh_getNodeData(_native, nodeindex);
		}

		public void GetNodeTriangle(int nodeindex, PrimitiveTriangle triangle)
		{
			btGImpactQuantizedBvh_getNodeTriangle(_native, nodeindex, triangle._native);
		}

		public int GetRightNode(int nodeindex)
		{
			return btGImpactQuantizedBvh_getRightNode(_native, nodeindex);
		}

		public bool IsLeafNode(int nodeindex)
		{
			return btGImpactQuantizedBvh_isLeafNode(_native, nodeindex);
		}

		public bool RayQuery(Vector3 rayDir, Vector3 rayOrigin, AlignedObjectArray collidedResults)
		{
			return btGImpactQuantizedBvh_rayQuery(_native, ref rayDir, ref rayOrigin, collidedResults._native);
		}

		public void SetNodeBound(int nodeindex, Aabb bound)
		{
			btGImpactQuantizedBvh_setNodeBound(_native, nodeindex, bound._native);
		}

		public void Update()
		{
			btGImpactQuantizedBvh_update(_native);
		}

		public Aabb GlobalBox
		{
            get { return new Aabb(btGImpactQuantizedBvh_getGlobalBox(_native), true); }
		}

		public bool HasHierarchy
		{
			get { return btGImpactQuantizedBvh_hasHierarchy(_native); }
		}

		public bool IsTrimesh
		{
			get { return btGImpactQuantizedBvh_isTrimesh(_native); }
		}

		public int NodeCount
		{
			get { return btGImpactQuantizedBvh_getNodeCount(_native); }
		}

		public PrimitiveManagerBase PrimitiveManager
		{
            get { return _primitiveManager; }
		    set
		    {
		        _primitiveManager = value;
		        btGImpactQuantizedBvh_setPrimitiveManager(_native, value._native);
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
				btGImpactQuantizedBvh_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~GImpactQuantizedBvh()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactQuantizedBvh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactQuantizedBvh_new2(IntPtr primitive_manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btGImpactQuantizedBvh_boxQuery(IntPtr obj, IntPtr box, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btGImpactQuantizedBvh_boxQueryTrans(IntPtr obj, IntPtr box, [In] ref Matrix transform, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactQuantizedBvh_buildSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactQuantizedBvh_find_collision(IntPtr boxset1, [In] ref Matrix trans1, IntPtr boxset2, [In] ref Matrix trans2, IntPtr collision_pairs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactQuantizedBvh_get_node_pointer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactQuantizedBvh_get_node_pointer2(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactQuantizedBvh_getEscapeNodeIndex(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactQuantizedBvh_getGlobalBox(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactQuantizedBvh_getLeftNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactQuantizedBvh_getNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactQuantizedBvh_getNodeCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactQuantizedBvh_getNodeData(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactQuantizedBvh_getNodeTriangle(IntPtr obj, int nodeindex, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactQuantizedBvh_getPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactQuantizedBvh_getRightNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btGImpactQuantizedBvh_hasHierarchy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btGImpactQuantizedBvh_isLeafNode(IntPtr obj, int nodeindex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btGImpactQuantizedBvh_isTrimesh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btGImpactQuantizedBvh_rayQuery(IntPtr obj, [In] ref Vector3 ray_dir, [In] ref Vector3 ray_origin, IntPtr collided_results);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactQuantizedBvh_setNodeBound(IntPtr obj, int nodeindex, IntPtr bound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactQuantizedBvh_setPrimitiveManager(IntPtr obj, IntPtr primitive_manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactQuantizedBvh_update(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactQuantizedBvh_delete(IntPtr obj);
	}
}
