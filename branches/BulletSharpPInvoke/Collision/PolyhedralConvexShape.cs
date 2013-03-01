using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class PolyhedralConvexShape : CollisionShape
    {
        internal PolyhedralConvexShape(IntPtr native)
            : base(native)
        {
        }

        public Vector3 GetVertex(int i)
        {
            Vector3 vertex;
            btPolyhedralConvexShape_getVertex(_native, i, out vertex);
            return vertex;
        }

        public bool IsInside(Vector3 pt, float tolerance)
        {
            return btPolyhedralConvexShape_isInside(_native, ref pt, tolerance);
        }

        public int NumEdges
        {
            get { return btPolyhedralConvexShape_getNumEdges(_native); }
        }

        public int NumPlanes
        {
            get { return btPolyhedralConvexShape_getNumPlanes(_native); }
        }

        public int NumVerticies
        {
            get { return btPolyhedralConvexShape_getNumVertices(_native); }
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btPolyhedralConvexShape_getNumEdges(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btPolyhedralConvexShape_getNumPlanes(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btPolyhedralConvexShape_getNumVertices(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btPolyhedralConvexShape_getVertex(IntPtr obj, int i, [Out] out Vector3 vertex);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btPolyhedralConvexShape_isInside(IntPtr obj, [In] ref Vector3 pt, float tolerance);
    }
}
