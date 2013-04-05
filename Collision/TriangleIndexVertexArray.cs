using System;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;

namespace BulletSharp
{
	public class IndexedMesh
	{
		internal IntPtr _native;

		internal IndexedMesh(IntPtr native)
		{
			_native = native;
		}

		public IndexedMesh()
		{
			_native = btIndexedMesh_new();
		}

        public unsafe UnmanagedMemoryStream GetTriangleStream()
        {
            int length = 3 * btIndexedMesh_getNumVertices(_native) * btIndexedMesh_getVertexStride(_native);
            return new UnmanagedMemoryStream((byte*)btIndexedMesh_getTriangleIndexBase(_native).ToPointer(), length, length, FileAccess.ReadWrite);
        }

        public PhyScalarType IndexType
		{
			get { return btIndexedMesh_getIndexType(_native); }
			set { btIndexedMesh_setIndexType(_native, value); }
		}

		public int NumTriangles
		{
			get { return btIndexedMesh_getNumTriangles(_native); }
			set { btIndexedMesh_setNumTriangles(_native, value); }
		}

		public int NumVertices
		{
			get { return btIndexedMesh_getNumVertices(_native); }
			set { btIndexedMesh_setNumVertices(_native, value); }
		}

		public IntPtr TriangleIndexBase
		{
			get { return btIndexedMesh_getTriangleIndexBase(_native); }
			set { btIndexedMesh_setTriangleIndexBase(_native, value); }
		}

		public int TriangleIndexStride
		{
			get { return btIndexedMesh_getTriangleIndexStride(_native); }
			set { btIndexedMesh_setTriangleIndexStride(_native, value); }
		}

        public IntPtr VertexBase
		{
			get { return btIndexedMesh_getVertexBase(_native); }
			set { btIndexedMesh_setVertexBase(_native, value); }
		}

		public int VertexStride
		{
			get { return btIndexedMesh_getVertexStride(_native); }
			set { btIndexedMesh_setVertexStride(_native, value); }
		}

        public PhyScalarType VertexType
		{
			get { return btIndexedMesh_getVertexType(_native); }
			set { btIndexedMesh_setVertexType(_native, value); }
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
				btIndexedMesh_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~IndexedMesh()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btIndexedMesh_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern PhyScalarType btIndexedMesh_getIndexType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btIndexedMesh_getNumTriangles(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btIndexedMesh_getNumVertices(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btIndexedMesh_getTriangleIndexBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btIndexedMesh_getTriangleIndexStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btIndexedMesh_getVertexBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btIndexedMesh_getVertexStride(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern PhyScalarType btIndexedMesh_getVertexType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btIndexedMesh_setIndexType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btIndexedMesh_setNumTriangles(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btIndexedMesh_setNumVertices(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btIndexedMesh_setTriangleIndexBase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btIndexedMesh_setTriangleIndexStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btIndexedMesh_setVertexBase(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btIndexedMesh_setVertexStride(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btIndexedMesh_setVertexType(IntPtr obj, PhyScalarType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btIndexedMesh_delete(IntPtr obj);
	}

	public class TriangleIndexVertexArray : StridingMeshInterface
	{
		internal TriangleIndexVertexArray(IntPtr native)
			: base(native)
		{
		}

		public TriangleIndexVertexArray()
			: base(btTriangleIndexVertexArray_new())
		{
		}
        /*
		public TriangleIndexVertexArray(int numTriangles, int triangleIndexBase, int triangleIndexStride, int numVertices, float vertexBase, int vertexStride)
			: base(btTriangleIndexVertexArray_new2(numTriangles, triangleIndexBase._native, triangleIndexStride, numVertices, vertexBase._native, vertexStride))
		{
		}
        */
        public void AddIndexedMesh(IndexedMesh mesh, PhyScalarType indexType)
		{
			btTriangleIndexVertexArray_addIndexedMesh(_native, mesh._native, indexType);
		}

		public void AddIndexedMesh(IndexedMesh mesh)
		{
			btTriangleIndexVertexArray_addIndexedMesh2(_native, mesh._native);
		}
        /*
		public IndexedMeshArray IndexedMeshArray
		{
			get { return btTriangleIndexVertexArray_getIndexedMeshArray(_native); }
		}
        */
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleIndexVertexArray_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleIndexVertexArray_new2(int numTriangles, IntPtr triangleIndexBase, int triangleIndexStride, int numVertices, IntPtr vertexBase, int vertexStride);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTriangleIndexVertexArray_addIndexedMesh(IntPtr obj, IntPtr mesh, PhyScalarType indexType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTriangleIndexVertexArray_addIndexedMesh2(IntPtr obj, IntPtr mesh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTriangleIndexVertexArray_getIndexedMeshArray(IntPtr obj);
	}
}
