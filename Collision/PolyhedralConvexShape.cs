using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class PolyhedralConvexShape : ConvexShape
    {
        ConvexPolyhedron _convexPolyhedron;

        internal PolyhedralConvexShape(IntPtr native)
            : base(native)
        {
        }

        public void GetEdge(int i, out Vector3 pa, out Vector3 pb)
        {
            btPolyhedralConvexShape_getEdge(_native, i, out pa, out pb);
        }

        public void GetPlane(out Vector3 planeNormal, out Vector3 planeSupport, int i)
        {
            btPolyhedralConvexShape_getPlane(_native, out planeNormal, out planeSupport, i);
        }

        public void GetVertex(int i, out Vector3 vertex)
        {
            btPolyhedralConvexShape_getVertex(_native, i, out vertex);
        }

        public void InitializePolyhedralFeatures(int shiftVerticesByMargin)
        {
            btPolyhedralConvexShape_initializePolyhedralFeatures(_native, shiftVerticesByMargin);
        }

        public void InitializePolyhedralFeatures()
        {
            btPolyhedralConvexShape_initializePolyhedralFeatures2(_native);
        }

        public bool IsInside(Vector3 pt, float tolerance)
        {
            return btPolyhedralConvexShape_isInside(_native, ref pt, tolerance);
        }

        public ConvexPolyhedron ConvexPolyhedron
        {
            get
            {
                if (_convexPolyhedron == null)
                {
                    IntPtr ptr = btPolyhedralConvexShape_getConvexPolyhedron(_native);
                    if (ptr == IntPtr.Zero)
                    {
                        return null;
                    }
                    _convexPolyhedron = new ConvexPolyhedron();
                }
                return _convexPolyhedron;
            }
        }

        public int NumEdges
        {
            get { return btPolyhedralConvexShape_getNumEdges(_native); }
        }

        public int NumPlanes
        {
            get { return btPolyhedralConvexShape_getNumPlanes(_native); }
        }

        public int NumVertices
        {
            get { return btPolyhedralConvexShape_getNumVertices(_native); }
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btPolyhedralConvexShape_getConvexPolyhedron(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btPolyhedralConvexShape_getEdge(IntPtr obj, int i, [Out] out Vector3 pa, [Out] out Vector3 pb);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btPolyhedralConvexShape_getNumEdges(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btPolyhedralConvexShape_getNumPlanes(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btPolyhedralConvexShape_getNumVertices(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btPolyhedralConvexShape_getPlane(IntPtr obj, [Out] out Vector3 planeNormal, [Out] out Vector3 planeSupport, int i);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btPolyhedralConvexShape_getVertex(IntPtr obj, int i, [Out] out Vector3 vertex);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btPolyhedralConvexShape_initializePolyhedralFeatures(IntPtr obj, int shiftVerticesByMargin);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btPolyhedralConvexShape_initializePolyhedralFeatures2(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btPolyhedralConvexShape_isInside(IntPtr obj, [In] ref Vector3 pt, float tolerance);
    }
}
