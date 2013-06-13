using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class TetrahedronShapeEx : BU_Simplex1to4
	{
		internal TetrahedronShapeEx(IntPtr native)
			: base(native)
		{
		}

		public TetrahedronShapeEx()
			: base(btTetrahedronShapeEx_new())
		{
		}

		public void SetVertices(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
		{
			btTetrahedronShapeEx_setVertices(_native, ref v0, ref v1, ref v2, ref v3);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTetrahedronShapeEx_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTetrahedronShapeEx_setVertices(IntPtr obj, [In] ref Vector3 v0, [In] ref Vector3 v1, [In] ref Vector3 v2, [In] ref Vector3 v3);
	}

	public class GImpactShapeInterface : ConcaveShape
	{
		internal GImpactShapeInterface(IntPtr native)
			: base(native)
		{
		}

		public bool ChildrenHasTransform()
		{
			return btGImpactShapeInterface_childrenHasTransform(_native);
		}

		public void GetBulletTetrahedron(int prim_index, TetrahedronShapeEx tetrahedron)
		{
			btGImpactShapeInterface_getBulletTetrahedron(_native, prim_index, tetrahedron._native);
		}
        /*
		public void GetBulletTriangle(int prim_index, TriangleShapeEx triangle)
		{
			btGImpactShapeInterface_getBulletTriangle(_native, prim_index, triangle._native);
		}
        */
		public void GetChildAabb(int child_index, Matrix t, Vector3 aabbMin, Vector3 aabbMax)
		{
			btGImpactShapeInterface_getChildAabb(_native, child_index, ref t, ref aabbMin, ref aabbMax);
		}

		public CollisionShape GetChildShape(int index)
		{
			return CollisionShape.GetManaged(btGImpactShapeInterface_getChildShape(_native, index));
		}

		public void GetChildTransform(int index)
		{
			btGImpactShapeInterface_getChildTransform(_native, index);
		}
        /*
		public void GetPrimitiveTriangle(int index, PrimitiveTriangle triangle)
		{
			btGImpactShapeInterface_getPrimitiveTriangle(_native, index, triangle._native);
		}
        */
		public void LockChildShapes()
		{
			btGImpactShapeInterface_lockChildShapes(_native);
		}

		public bool NeedsRetrieveTetrahedrons()
		{
			return btGImpactShapeInterface_needsRetrieveTetrahedrons(_native);
		}

		public bool NeedsRetrieveTriangles()
		{
			return btGImpactShapeInterface_needsRetrieveTriangles(_native);
		}

		public void PostUpdate()
		{
			btGImpactShapeInterface_postUpdate(_native);
		}
        /*
		public void ProcessAllTrianglesRay(TriangleCallback __unnamed0, Vector3 __unnamed1, Vector3 __unnamed2)
		{
			btGImpactShapeInterface_processAllTrianglesRay(_native, __unnamed0._native, ref __unnamed1, ref __unnamed2);
		}
        */
		public void RayTest(Vector3 rayFrom, Vector3 rayTo, RayResultCallback resultCallback)
		{
			btGImpactShapeInterface_rayTest(_native, ref rayFrom, ref rayTo, resultCallback._native);
		}

		public void SetChildTransform(int index, Matrix transform)
		{
			btGImpactShapeInterface_setChildTransform(_native, index, ref transform);
		}

		public void UnlockChildShapes()
		{
			btGImpactShapeInterface_unlockChildShapes(_native);
		}

		public void UpdateBound()
		{
			btGImpactShapeInterface_updateBound(_native);
		}
        /*
		public GImpactBoxSet BoxSet
		{
			get { return btGImpactShapeInterface_getBoxSet(_native); }
		}

		public eGIMPACT_SHAPE_TYPE GImpactShapeType
		{
			get { return btGImpactShapeInterface_getGImpactShapeType(_native); }
		}
        */
		public bool HasBoxSet
		{
			get { return btGImpactShapeInterface_hasBoxSet(_native); }
		}

		public AABB LocalBox
		{
            get { return new AABB(btGImpactShapeInterface_getLocalBox(_native)); }
		}

		public int NumChildShapes
		{
			get { return btGImpactShapeInterface_getNumChildShapes(_native); }
		}

		public PrimitiveManagerBase PrimitiveManager
		{
            get { return new PrimitiveManagerBase(btGImpactShapeInterface_getPrimitiveManager(_native)); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btGImpactShapeInterface_childrenHasTransform(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactShapeInterface_getBoxSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_getBulletTetrahedron(IntPtr obj, int prim_index, IntPtr tetrahedron);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_getBulletTriangle(IntPtr obj, int prim_index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_getChildAabb(IntPtr obj, int child_index, [In] ref Matrix t, [In] ref Vector3 aabbMin, [In] ref Vector3 aabbMax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactShapeInterface_getChildShape(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_getChildTransform(IntPtr obj, int index);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern eGIMPACT_SHAPE_TYPE btGImpactShapeInterface_getGImpactShapeType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactShapeInterface_getLocalBox(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactShapeInterface_getNumChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactShapeInterface_getPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_getPrimitiveTriangle(IntPtr obj, int index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btGImpactShapeInterface_hasBoxSet(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_lockChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btGImpactShapeInterface_needsRetrieveTetrahedrons(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btGImpactShapeInterface_needsRetrieveTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_postUpdate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_processAllTrianglesRay(IntPtr obj, IntPtr __unnamed0, [In] ref Vector3 __unnamed1, [In] ref Vector3 __unnamed2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_rayTest(IntPtr obj, [In] ref Vector3 rayFrom, [In] ref Vector3 rayTo, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_setChildTransform(IntPtr obj, int index, [In] ref Matrix transform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_unlockChildShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactShapeInterface_updateBound(IntPtr obj);
	}

	public class CompoundPrimitiveManager : PrimitiveManagerBase
	{
		internal CompoundPrimitiveManager(IntPtr native)
			: base(native)
		{
		}

		public CompoundPrimitiveManager(CompoundPrimitiveManager compound)
			: base(btGImpactCompoundShape_CompoundPrimitiveManager_new(compound._native))
		{
		}

		public CompoundPrimitiveManager(GImpactCompoundShape compoundShape)
			: base(btGImpactCompoundShape_CompoundPrimitiveManager_new2(compoundShape._native))
		{
		}

		public CompoundPrimitiveManager()
			: base(btGImpactCompoundShape_CompoundPrimitiveManager_new3())
		{
		}

		public GImpactCompoundShape CompoundShape
		{
			get { return new GImpactCompoundShape(btGImpactCompoundShape_CompoundPrimitiveManager_getCompoundShape(_native)); }
			set { btGImpactCompoundShape_CompoundPrimitiveManager_setCompoundShape(_native, value._native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCompoundShape_CompoundPrimitiveManager_new(IntPtr compound);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCompoundShape_CompoundPrimitiveManager_new2(IntPtr compoundShape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCompoundShape_CompoundPrimitiveManager_new3();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCompoundShape_CompoundPrimitiveManager_getCompoundShape(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCompoundShape_CompoundPrimitiveManager_setCompoundShape(IntPtr obj, IntPtr value);
	}

	public class GImpactCompoundShape : GImpactShapeInterface
	{
		internal GImpactCompoundShape(IntPtr native)
			: base(native)
		{
		}

		public GImpactCompoundShape(bool children_has_transform)
			: base(btGImpactCompoundShape_new(children_has_transform))
		{
		}

		public GImpactCompoundShape()
			: base(btGImpactCompoundShape_new2())
		{
		}

		public void AddChildShape(Matrix localTransform, CollisionShape shape)
		{
			btGImpactCompoundShape_addChildShape(_native, ref localTransform, shape._native);
		}

		public void AddChildShape(CollisionShape shape)
		{
			btGImpactCompoundShape_addChildShape2(_native, shape._native);
		}

		public CompoundPrimitiveManager CompoundPrimitiveManager
		{
            get { return new CompoundPrimitiveManager(btGImpactCompoundShape_getCompoundPrimitiveManager(_native)); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCompoundShape_new(bool children_has_transform);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCompoundShape_new2();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCompoundShape_addChildShape(IntPtr obj, [In] ref Matrix localTransform, IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactCompoundShape_addChildShape2(IntPtr obj, IntPtr shape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactCompoundShape_getCompoundPrimitiveManager(IntPtr obj);
	}

	public class TrimeshPrimitiveManager : PrimitiveManagerBase
	{
		internal TrimeshPrimitiveManager(IntPtr native)
			: base(native)
		{
		}

		public TrimeshPrimitiveManager(StridingMeshInterface meshInterface, int part)
			: base(btGImpactMeshShapePart_TrimeshPrimitiveManager_new(meshInterface._native, part))
		{
		}

		public TrimeshPrimitiveManager(TrimeshPrimitiveManager manager)
			: base(btGImpactMeshShapePart_TrimeshPrimitiveManager_new2(manager._native))
		{
		}

		public TrimeshPrimitiveManager()
			: base(btGImpactMeshShapePart_TrimeshPrimitiveManager_new3())
		{
		}
		/*
		public void Get_bullet_triangle(int prim_index, TriangleShapeEx triangle)
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_get_bullet_triangle(_native, prim_index, triangle._native);
		}

		public void Get_indices(int face_index, uint i0, uint i1, uint i2)
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_get_indices(_native, face_index, i0._native, i1._native, i2._native);
		}
        */
		public void Get_vertex(uint vertex_index, Vector3 vertex)
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex(_native, vertex_index, ref vertex);
		}

		public void Lock()
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_lock(_native);
		}

		public void Unlock()
		{
			btGImpactMeshShapePart_TrimeshPrimitiveManager_unlock(_native);
		}

		public int _vertex_count
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex_count(_native); }
		}

		public IntPtr Indexbase
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexbase(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexbase(_native, value); }
		}

		public int Indexstride
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexstride(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexstride(_native, value); }
		}

		public PhyScalarType Indicestype
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndicestype(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndicestype(_native, value); }
		}

		public int Lock_count
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getLock_count(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setLock_count(_native, value); }
		}

		public float Margin
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getMargin(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setMargin(_native, value); }
		}

		public StridingMeshInterface MeshInterface
		{
            get { return new StridingMeshInterface(btGImpactMeshShapePart_TrimeshPrimitiveManager_getMeshInterface(_native)); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setMeshInterface(_native, value._native); }
		}

		public int Numfaces
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumfaces(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumfaces(_native, value); }
		}

		public int Numverts
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumverts(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumverts(_native, value); }
		}

		public int Part
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getPart(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setPart(_native, value); }
		}
        /*
		public void Scale
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getScale(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setScale(_native, value._native); }
		}
        */
		public int Stride
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getStride(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setStride(_native, value); }
		}

		public PhyScalarType Type
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getType(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setType(_native, value); }
		}

        public IntPtr Vertexbase
		{
			get { return btGImpactMeshShapePart_TrimeshPrimitiveManager_getVertexbase(_native); }
			set { btGImpactMeshShapePart_TrimeshPrimitiveManager_setVertexbase(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new(IntPtr meshInterface, int part);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new2(IntPtr manager);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_new3();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_bullet_triangle(IntPtr obj, int prim_index, IntPtr triangle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_indices(IntPtr obj, int face_index, IntPtr i0, IntPtr i1, IntPtr i2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex(IntPtr obj, uint vertex_index, [In] ref Vector3 vertex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexbase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexstride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern PhyScalarType btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndicestype(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getLock_count(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btGImpactMeshShapePart_TrimeshPrimitiveManager_getMargin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getMeshInterface(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumfaces(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumverts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getPart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_getScale(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_TrimeshPrimitiveManager_getStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern PhyScalarType btGImpactMeshShapePart_TrimeshPrimitiveManager_getType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_TrimeshPrimitiveManager_getVertexbase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_lock(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexbase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexstride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndicestype(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setLock_count(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setMargin(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setMeshInterface(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumfaces(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumverts(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setPart(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setScale(IntPtr obj, Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_setVertexbase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_TrimeshPrimitiveManager_unlock(IntPtr obj);
	}

    public class GImpactMeshShapePart : GImpactShapeInterface
    {
		internal GImpactMeshShapePart(IntPtr native)
			: base(native)
		{
		}

		public GImpactMeshShapePart()
			: base(btGImpactMeshShapePart_new())
		{
		}

		public GImpactMeshShapePart(StridingMeshInterface meshInterface, int part)
			: base(btGImpactMeshShapePart_new2(meshInterface._native, part))
		{
		}

		public void GetVertex(int vertex_index, Vector3 vertex)
		{
			btGImpactMeshShapePart_getVertex(_native, vertex_index, ref vertex);
		}

		public int Part
		{
			get { return btGImpactMeshShapePart_getPart(_native); }
		}

		public TrimeshPrimitiveManager TrimeshPrimitiveManager
		{
            get { return new TrimeshPrimitiveManager(btGImpactMeshShapePart_getTrimeshPrimitiveManager(_native)); }
		}

		public int VertexCount
		{
			get { return btGImpactMeshShapePart_getVertexCount(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_new2(IntPtr meshInterface, int part);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_getPart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShapePart_getTrimeshPrimitiveManager(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGImpactMeshShapePart_getVertex(IntPtr obj, int vertex_index, [In] ref Vector3 vertex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShapePart_getVertexCount(IntPtr obj);
	}

	public class GImpactMeshShape : GImpactShapeInterface
	{
        StridingMeshInterface _meshInterface;
        bool _disposeMeshInterface;

		internal GImpactMeshShape(IntPtr native)
			: base(native)
		{
		}

		public GImpactMeshShape(StridingMeshInterface meshInterface)
			: base(btGImpactMeshShape_new(meshInterface._native))
		{
            _meshInterface = meshInterface;
		}

		public GImpactMeshShapePart GetMeshPart(int index)
		{
			return new GImpactMeshShapePart(btGImpactMeshShape_getMeshPart(_native, index));
		}

		public StridingMeshInterface MeshInterface
		{
            get
            {
                if (_meshInterface == null)
                {
                    _meshInterface = new StridingMeshInterface(btGImpactMeshShape_getMeshInterface(_native));
                    _disposeMeshInterface = true;
                }
                return _meshInterface;
            }
		}

		public int MeshPartCount
		{
			get { return btGImpactMeshShape_getMeshPartCount(_native); }
		}

        protected override void Dispose(bool disposing)
        {
            if (disposing && _disposeMeshInterface)
            {
                _meshInterface.Dispose();
                _disposeMeshInterface = false;
            }
            base.Dispose(disposing);
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShape_new(IntPtr meshInterface);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShape_getMeshInterface(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGImpactMeshShape_getMeshPart(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btGImpactMeshShape_getMeshPartCount(IntPtr obj);
	}
}