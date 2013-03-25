using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class ConvexHullShape : PolyhedralConvexShape
	{
		internal ConvexHullShape(IntPtr native)
			: base(native)
		{
		}

		public ConvexHullShape(float[] points, int numPoints, int stride)
			: base(btConvexHullShape_new(points, numPoints, stride))
		{
		}

        public ConvexHullShape(Vector3[] points, int numPoints)
            : base(btConvexHullShape_new(points, numPoints, Marshal.SizeOf(typeof(Vector3))))
		{
		}

		public ConvexHullShape(Vector3[] points)
            : base(btConvexHullShape_new(points, points.Length, Marshal.SizeOf(typeof(Vector3))))
		{
		}

		public ConvexHullShape()
			: base(btConvexHullShape_new4())
		{
		}

		public void AddPoint(Vector3 point, bool recalculateLocalAabb)
		{
			btConvexHullShape_addPoint(_native, ref point, recalculateLocalAabb);
		}

		public void AddPoint(Vector3 point)
		{
			btConvexHullShape_addPoint2(_native, ref point);
		}

		public void GetScaledPoint(int i)
		{
			btConvexHullShape_getScaledPoint(_native, i);
		}
        /*
		public void Project(Matrix trans, Vector3 dir, float minProj, float maxProj, Vector3 witnesPtMin, Vector3 witnesPtMax)
		{
			btConvexHullShape_project(_native, ref trans, dir._native, minProj._native, maxProj._native, ref witnesPtMin, ref witnesPtMax);
		}
        */
		public int NumPoints
		{
			get { return btConvexHullShape_getNumPoints(_native); }
		}
        /*
		public Vector3 Points
		{
			get { return btConvexHullShape_getPoints(_native); }
		}

		public Vector3 UnscaledPoints
		{
			get { return btConvexHullShape_getUnscaledPoints(_native); }
		}
        */
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btConvexHullShape_new(float[] points, int numPoints, int stride);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btConvexHullShape_new(Vector3[] points, int numPoints, int stride);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btConvexHullShape_new2(Vector3[] points, int numPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexHullShape_new3(Vector3[] points);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexHullShape_new4();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexHullShape_addPoint(IntPtr obj, [In] ref Vector3 point, bool recalculateLocalAabb);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btConvexHullShape_addPoint2(IntPtr obj, [In] ref Vector3 point);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btConvexHullShape_getNumPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexHullShape_getPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexHullShape_getScaledPoint(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConvexHullShape_getUnscaledPoints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConvexHullShape_project(IntPtr obj, IntPtr trans, IntPtr dir, IntPtr minProj, IntPtr maxProj, IntPtr witnesPtMin, IntPtr witnesPtMax);
	}
}