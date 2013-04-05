using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class fDrawFlags
	{
		internal IntPtr _native;

		internal fDrawFlags(IntPtr native)
		{
			_native = native;
		}

		public fDrawFlags()
		{
			_native = fDrawFlags_new();
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
				fDrawFlags_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~fDrawFlags()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr fDrawFlags_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void fDrawFlags_delete(IntPtr obj);
	}

	public class SoftBodyHelpers
	{
		internal IntPtr _native;

		internal SoftBodyHelpers(IntPtr native)
		{
			_native = native;
		}

		public SoftBodyHelpers()
		{
			_native = btSoftBodyHelpers_new();
		}

		public float CalculateUV(int resx, int resy, int ix, int iy, int id)
		{
			return btSoftBodyHelpers_CalculateUV(resx, resy, ix, iy, id);
		}

		public SoftBody CreateEllipsoid(SoftBodyWorldInfo worldInfo, Vector3 center, Vector3 radius, int res)
		{
            return new SoftBody(btSoftBodyHelpers_CreateEllipsoid(worldInfo._native, ref center, ref radius, res));
		}

		public SoftBody CreateFromConvexHull(SoftBodyWorldInfo worldInfo, Vector3 vertices, int nvertices, bool randomizeConstraints)
		{
			return new SoftBody(btSoftBodyHelpers_CreateFromConvexHull(worldInfo._native, ref vertices, nvertices, randomizeConstraints));
		}

		public SoftBody CreateFromConvexHull(SoftBodyWorldInfo worldInfo, Vector3 vertices, int nvertices)
		{
			return new SoftBody(btSoftBodyHelpers_CreateFromConvexHull2(worldInfo._native, ref vertices, nvertices));
		}

        public SoftBody CreateFromTetGenData(SoftBodyWorldInfo worldInfo, string ele, string face, string node, bool bfacelinks, bool btetralinks, bool bfacesfromtetras)
		{
			return new SoftBody(btSoftBodyHelpers_CreateFromTetGenData(worldInfo._native, ele, face, node, bfacelinks, btetralinks, bfacesfromtetras));
		}
        /*
		public SoftBody CreateFromTriMesh(SoftBodyWorldInfo worldInfo, float vertices, int triangles, int ntriangles, bool randomizeConstraints)
		{
			return new SoftBody(btSoftBodyHelpers_CreateFromTriMesh(worldInfo._native, vertices._native, triangles._native, ntriangles, randomizeConstraints));
		}

		public SoftBody CreateFromTriMesh(SoftBodyWorldInfo worldInfo, float vertices, int triangles, int ntriangles)
		{
			return new SoftBody(btSoftBodyHelpers_CreateFromTriMesh2(worldInfo._native, vertices._native, triangles._native, ntriangles));
		}
        */
		public SoftBody CreatePatch(SoftBodyWorldInfo worldInfo, Vector3 corner00, Vector3 corner10, Vector3 corner01, Vector3 corner11, int resx, int resy, int fixeds, bool gendiags)
		{
			return new SoftBody(btSoftBodyHelpers_CreatePatch(worldInfo._native, ref corner00, ref corner10, ref corner01, ref corner11, resx, resy, fixeds, gendiags));
		}
        /*
		public SoftBody CreatePatchUV(SoftBodyWorldInfo worldInfo, Vector3 corner00, Vector3 corner10, Vector3 corner01, Vector3 corner11, int resx, int resy, int fixeds, bool gendiags, float tex_coords)
		{
			return new SoftBody(btSoftBodyHelpers_CreatePatchUV(worldInfo._native, ref corner00, ref corner10, ref corner01, ref corner11, resx, resy, fixeds, gendiags, tex_coords._native));
		}
        */
		public SoftBody CreatePatchUV(SoftBodyWorldInfo worldInfo, Vector3 corner00, Vector3 corner10, Vector3 corner01, Vector3 corner11, int resx, int resy, int fixeds, bool gendiags)
		{
			return new SoftBody(btSoftBodyHelpers_CreatePatchUV2(worldInfo._native, ref corner00, ref corner10, ref corner01, ref corner11, resx, resy, fixeds, gendiags));
		}

		public SoftBody CreateRope(SoftBodyWorldInfo worldInfo, Vector3 from, Vector3 to, int res, int fixeds)
		{
			return new SoftBody(btSoftBodyHelpers_CreateRope(worldInfo._native, ref from, ref to, res, fixeds));
		}

		public void Draw(SoftBody psb, IDebugDraw idraw, int drawflags)
		{
			btSoftBodyHelpers_Draw(psb._native, DebugDraw.GetUnmanaged(idraw), drawflags);
		}

		public void Draw(SoftBody psb, IDebugDraw idraw)
		{
			btSoftBodyHelpers_Draw2(psb._native, DebugDraw.GetUnmanaged(idraw));
		}

		public void DrawClusterTree(SoftBody psb, IDebugDraw idraw, int mindepth, int maxdepth)
		{
			btSoftBodyHelpers_DrawClusterTree(psb._native, DebugDraw.GetUnmanaged(idraw), mindepth, maxdepth);
		}

		public void DrawClusterTree(SoftBody psb, IDebugDraw idraw, int mindepth)
		{
			btSoftBodyHelpers_DrawClusterTree2(psb._native, DebugDraw.GetUnmanaged(idraw), mindepth);
		}

		public void DrawClusterTree(SoftBody psb, IDebugDraw idraw)
		{
			btSoftBodyHelpers_DrawClusterTree3(psb._native, DebugDraw.GetUnmanaged(idraw));
		}

		public void DrawFaceTree(SoftBody psb, IDebugDraw idraw, int mindepth, int maxdepth)
		{
			btSoftBodyHelpers_DrawFaceTree(psb._native, DebugDraw.GetUnmanaged(idraw), mindepth, maxdepth);
		}

		public void DrawFaceTree(SoftBody psb, IDebugDraw idraw, int mindepth)
		{
			btSoftBodyHelpers_DrawFaceTree2(psb._native, DebugDraw.GetUnmanaged(idraw), mindepth);
		}

		public void DrawFaceTree(SoftBody psb, IDebugDraw idraw)
		{
			btSoftBodyHelpers_DrawFaceTree3(psb._native, DebugDraw.GetUnmanaged(idraw));
		}

		public void DrawFrame(SoftBody psb, IDebugDraw idraw)
		{
			btSoftBodyHelpers_DrawFrame(psb._native, DebugDraw.GetUnmanaged(idraw));
		}

		public void DrawInfos(SoftBody psb, IDebugDraw idraw, bool masses, bool areas, bool stress)
		{
			btSoftBodyHelpers_DrawInfos(psb._native, DebugDraw.GetUnmanaged(idraw), masses, areas, stress);
		}

		public void DrawNodeTree(SoftBody psb, IDebugDraw idraw, int mindepth, int maxdepth)
		{
			btSoftBodyHelpers_DrawNodeTree(psb._native, DebugDraw.GetUnmanaged(idraw), mindepth, maxdepth);
		}

		public void DrawNodeTree(SoftBody psb, IDebugDraw idraw, int mindepth)
		{
			btSoftBodyHelpers_DrawNodeTree2(psb._native, DebugDraw.GetUnmanaged(idraw), mindepth);
		}

		public void DrawNodeTree(SoftBody psb, IDebugDraw idraw)
		{
			btSoftBodyHelpers_DrawNodeTree3(psb._native, DebugDraw.GetUnmanaged(idraw));
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
				btSoftBodyHelpers_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~SoftBodyHelpers()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBodyHelpers_CalculateUV(int resx, int resy, int ix, int iy, int id);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateEllipsoid(IntPtr worldInfo, [In] ref Vector3 center, [In] ref Vector3 radius, int res);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateFromConvexHull(IntPtr worldInfo, [In] ref Vector3 vertices, int nvertices, bool randomizeConstraints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateFromConvexHull2(IntPtr worldInfo, [In] ref Vector3 vertices, int nvertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBodyHelpers_CreateFromTetGenData(IntPtr worldInfo, string ele, string face, string node, bool bfacelinks, bool btetralinks, bool bfacesfromtetras);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateFromTriMesh(IntPtr worldInfo, IntPtr vertices, IntPtr triangles, int ntriangles, bool randomizeConstraints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateFromTriMesh2(IntPtr worldInfo, IntPtr vertices, IntPtr triangles, int ntriangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreatePatch(IntPtr worldInfo, [In] ref Vector3 corner00, [In] ref Vector3 corner10, [In] ref Vector3 corner01, [In] ref Vector3 corner11, int resx, int resy, int fixeds, bool gendiags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreatePatchUV(IntPtr worldInfo, [In] ref Vector3 corner00, [In] ref Vector3 corner10, [In] ref Vector3 corner01, [In] ref Vector3 corner11, int resx, int resy, int fixeds, bool gendiags, IntPtr tex_coords);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreatePatchUV2(IntPtr worldInfo, [In] ref Vector3 corner00, [In] ref Vector3 corner10, [In] ref Vector3 corner01, [In] ref Vector3 corner11, int resx, int resy, int fixeds, bool gendiags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateRope(IntPtr worldInfo, [In] ref Vector3 from, [In] ref Vector3 to, int res, int fixeds);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_Draw(IntPtr psb, IntPtr idraw, int drawflags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_Draw2(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawClusterTree(IntPtr psb, IntPtr idraw, int mindepth, int maxdepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawClusterTree2(IntPtr psb, IntPtr idraw, int mindepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawClusterTree3(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawFaceTree(IntPtr psb, IntPtr idraw, int mindepth, int maxdepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawFaceTree2(IntPtr psb, IntPtr idraw, int mindepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawFaceTree3(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawFrame(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawInfos(IntPtr psb, IntPtr idraw, bool masses, bool areas, bool stress);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawNodeTree(IntPtr psb, IntPtr idraw, int mindepth, int maxdepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawNodeTree2(IntPtr psb, IntPtr idraw, int mindepth);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_DrawNodeTree3(IntPtr psb, IntPtr idraw);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodyHelpers_delete(IntPtr obj);
	}
}
