using System;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;

namespace BulletSharp.SoftBody
{
    public enum DrawFlags
    {
        None = 0x00,
        Nodex = 0x01,
        Links = 0x02,
        Faces = 0x04,
        Tetras = 0x08,
        Normals = 0x10,
        Contacts = 0x20,
        Anchors = 0x40,
        Notes = 0x80,
        Clusters = 0x100,
        NodeTree = 0x200,
        FaceTree = 0x400,
        ClusterTree = 0x800,
        Joints = 0x1000,
        Std = Links | Faces | Tetras | Anchors | Notes | Joints,
        StdTetra = Std - Faces - Tetras
    }

	public class SoftBodyHelpers
	{
		public static float CalculateUV(int resx, int resy, int ix, int iy, int id)
		{
			return btSoftBodyHelpers_CalculateUV(resx, resy, ix, iy, id);
		}

		public static SoftBody CreateEllipsoid(SoftBodyWorldInfo worldInfo, Vector3 center, Vector3 radius, int res)
		{
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateEllipsoid(worldInfo._native, ref center, ref radius, res));
            body.WorldInfo = worldInfo;
            return body;
		}

		public static SoftBody CreateFromConvexHull(SoftBodyWorldInfo worldInfo, Vector3[] vertices, int numVertices, bool randomizeConstraints)
		{
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateFromConvexHull(worldInfo._native, vertices, numVertices, randomizeConstraints));
            body.WorldInfo = worldInfo;
            return body;
		}

        public static SoftBody CreateFromConvexHull(SoftBodyWorldInfo worldInfo, Vector3[] vertices, bool randomizeConstraints)
        {
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateFromConvexHull(worldInfo._native, vertices, vertices.Length, randomizeConstraints));
            body.WorldInfo = worldInfo;
            return body;
        }

		public static SoftBody CreateFromConvexHull(SoftBodyWorldInfo worldInfo, Vector3[] vertices, int numVertices)
		{
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateFromConvexHull2(worldInfo._native, vertices, numVertices));
            body.WorldInfo = worldInfo;
            return body;
		}

        public static SoftBody CreateFromConvexHull(SoftBodyWorldInfo worldInfo, Vector3[] vertices)
        {
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateFromConvexHull2(worldInfo._native, vertices, vertices.Length));
            body.WorldInfo = worldInfo;
            return body;
        }

        public static SoftBody CreateFromTetGenData(SoftBodyWorldInfo worldInfo, string ele, string face, string node, bool faceLinks, bool tetraLinks, bool facesFromTetras)
		{
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateFromTetGenData(worldInfo._native, ele, face, node, faceLinks, tetraLinks, facesFromTetras));
            body.WorldInfo = worldInfo;
            return body;
		}

        public static SoftBody CreateFromTetGenFile(SoftBodyWorldInfo worldInfo, string elementFilename, string faceFilename, string nodeFilename, bool faceLinks, bool tetraLinks, bool facesFromTetras)
        {
            string ele = (elementFilename != null) ? File.ReadAllText(elementFilename) : null;
            string face = (faceFilename != null) ? File.ReadAllText(faceFilename) : null;
            string node = (nodeFilename != null) ? File.ReadAllText(nodeFilename) : null;

            return CreateFromTetGenData(worldInfo, ele, face, node, faceLinks, tetraLinks, facesFromTetras);
        }
    
		public static SoftBody CreateFromTriMesh(SoftBodyWorldInfo worldInfo, float[] vertices, int[] triangles, bool randomizeConstraints)
		{
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateFromTriMesh(worldInfo._native, vertices, triangles, triangles.Length / 3, randomizeConstraints));
            body.WorldInfo = worldInfo;
            return body;
		}

		public static SoftBody CreateFromTriMesh(SoftBodyWorldInfo worldInfo, float[] vertices, int[] triangles)
		{
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateFromTriMesh2(worldInfo._native, vertices, triangles, triangles.Length / 3));
            body.WorldInfo = worldInfo;
            return body;
		}

		public static SoftBody CreatePatch(SoftBodyWorldInfo worldInfo, Vector3 corner00, Vector3 corner10, Vector3 corner01, Vector3 corner11, int resx, int resy, int fixeds, bool gendiags)
		{
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreatePatch(worldInfo._native, ref corner00, ref corner10, ref corner01, ref corner11, resx, resy, fixeds, gendiags));
            body.WorldInfo = worldInfo;
            return body;
		}
        
		public static SoftBody CreatePatchUV(SoftBodyWorldInfo worldInfo, Vector3 corner00, Vector3 corner10, Vector3 corner01, Vector3 corner11, int resx, int resy, int fixeds, bool gendiags, float[] texCoords)
		{
			SoftBody body = new SoftBody(btSoftBodyHelpers_CreatePatchUV(worldInfo._native, ref corner00, ref corner10, ref corner01, ref corner11, resx, resy, fixeds, gendiags, texCoords));
            body.WorldInfo = worldInfo;
            return body;
		}
        
        public static SoftBody CreatePatchUV(SoftBodyWorldInfo worldInfo, Vector3 corner00, Vector3 corner10, Vector3 corner01, Vector3 corner11, int resx, int resy, int fixeds, bool gendiags)
		{
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreatePatchUV2(worldInfo._native, ref corner00, ref corner10, ref corner01, ref corner11, resx, resy, fixeds, gendiags));
            body.WorldInfo = worldInfo;
            return body;
		}

		public static SoftBody CreateRope(SoftBodyWorldInfo worldInfo, Vector3 from, Vector3 to, int res, int fixeds)
		{
            SoftBody body = new SoftBody(btSoftBodyHelpers_CreateRope(worldInfo._native, ref from, ref to, res, fixeds));
            body.WorldInfo = worldInfo;
            return body;
		}

		public static void Draw(SoftBody psb, IDebugDraw iDraw, int drawFlags)
		{
			btSoftBodyHelpers_Draw(psb._native, DebugDraw.GetUnmanaged(iDraw), drawFlags);
		}

		public static void Draw(SoftBody psb, IDebugDraw iDraw)
		{
			btSoftBodyHelpers_Draw2(psb._native, DebugDraw.GetUnmanaged(iDraw));
		}

		public static void DrawClusterTree(SoftBody psb, IDebugDraw iDraw, int minDepth, int maxDepth)
		{
			btSoftBodyHelpers_DrawClusterTree(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth, maxDepth);
		}

		public static void DrawClusterTree(SoftBody psb, IDebugDraw iDraw, int minDepth)
		{
			btSoftBodyHelpers_DrawClusterTree2(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth);
		}

		public static void DrawClusterTree(SoftBody psb, IDebugDraw iDraw)
		{
			btSoftBodyHelpers_DrawClusterTree3(psb._native, DebugDraw.GetUnmanaged(iDraw));
		}

		public static void DrawFaceTree(SoftBody psb, IDebugDraw iDraw, int minDepth, int maxDepth)
		{
			btSoftBodyHelpers_DrawFaceTree(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth, maxDepth);
		}

		public static void DrawFaceTree(SoftBody psb, IDebugDraw iDraw, int minDepth)
		{
			btSoftBodyHelpers_DrawFaceTree2(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth);
		}

		public static void DrawFaceTree(SoftBody psb, IDebugDraw iDraw)
		{
			btSoftBodyHelpers_DrawFaceTree3(psb._native, DebugDraw.GetUnmanaged(iDraw));
		}

		public static void DrawFrame(SoftBody psb, IDebugDraw iDraw)
		{
			btSoftBodyHelpers_DrawFrame(psb._native, DebugDraw.GetUnmanaged(iDraw));
		}

		public static void DrawInfos(SoftBody psb, IDebugDraw iDraw, bool masses, bool areas, bool stress)
		{
			btSoftBodyHelpers_DrawInfos(psb._native, DebugDraw.GetUnmanaged(iDraw), masses, areas, stress);
		}

		public static void DrawNodeTree(SoftBody psb, IDebugDraw iDraw, int minDepth, int maxDepth)
		{
			btSoftBodyHelpers_DrawNodeTree(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth, maxDepth);
		}

		public static void DrawNodeTree(SoftBody psb, IDebugDraw iDraw, int minDepth)
		{
			btSoftBodyHelpers_DrawNodeTree2(psb._native, DebugDraw.GetUnmanaged(iDraw), minDepth);
		}

		public static void DrawNodeTree(SoftBody psb, IDebugDraw iDraw)
		{
			btSoftBodyHelpers_DrawNodeTree3(psb._native, DebugDraw.GetUnmanaged(iDraw));
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btSoftBodyHelpers_CalculateUV(int resx, int resy, int ix, int iy, int id);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateEllipsoid(IntPtr worldInfo, [In] ref Vector3 center, [In] ref Vector3 radius, int res);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateFromConvexHull(IntPtr worldInfo, [In] Vector3[] vertices, int nvertices, bool randomizeConstraints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateFromConvexHull2(IntPtr worldInfo, [In] Vector3[] vertices, int nvertices);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBodyHelpers_CreateFromTetGenData(IntPtr worldInfo, string ele, string face, string node, bool bfacelinks, bool btetralinks, bool bfacesfromtetras);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateFromTriMesh(IntPtr worldInfo, [In] float[] vertices, [In] int[] triangles, int ntriangles, bool randomizeConstraints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreateFromTriMesh2(IntPtr worldInfo, [In] float[] vertices, [In] int[] triangles, int ntriangles);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreatePatch(IntPtr worldInfo, [In] ref Vector3 corner00, [In] ref Vector3 corner10, [In] ref Vector3 corner01, [In] ref Vector3 corner11, int resx, int resy, int fixeds, bool gendiags);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSoftBodyHelpers_CreatePatchUV(IntPtr worldInfo, [In] ref Vector3 corner00, [In] ref Vector3 corner10, [In] ref Vector3 corner01, [In] ref Vector3 corner11, int resx, int resy, int fixeds, bool gendiags, float[] tex_coords);
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
	}
}
