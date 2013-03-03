using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class DefaultCollisionConstructionInfo : IDisposable
    {
        internal IntPtr _native;

        public DefaultCollisionConstructionInfo()
        {
            _native = btDefaultCollisionConstructionInfo_new();
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
                btDefaultCollisionConstructionInfo_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~DefaultCollisionConstructionInfo()
        {
            Dispose(false);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDefaultCollisionConstructionInfo_new();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDefaultCollisionConstructionInfo_delete(IntPtr obj);
    }

    public class DefaultCollisionConfiguration : CollisionConfiguration
    {
        internal DefaultCollisionConfiguration(IntPtr native)
            : base(native)
        {
        }

        public DefaultCollisionConfiguration(DefaultCollisionConstructionInfo constructionInfo)
            : base(btDefaultCollisionConfiguration_new(constructionInfo._native))
        {
        }

        public DefaultCollisionConfiguration()
            : base(btDefaultCollisionConfiguration_new2())
        {
        }
        /*
        public VoronoiSimplexSolver SimplexSolver
        {
            get { new VoronoiSimplexSolver(btDefaultCollisionConfiguration_getSimplexSolver(_native)); }
        }
        */
        public void SetConvexConvexMultipointIterations(int numPerturbationIterations, int minimumPointsPerturbationThreshold)
        {
            btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(_native, numPerturbationIterations, minimumPointsPerturbationThreshold);
        }

        public void SetConvexConvexMultipointIterations(int numPerturbationIterations)
        {
            btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(_native, numPerturbationIterations);
        }

        public void SetConvexConvexMultipointIterations()
        {
            btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(_native);
        }

        public void SetPlaneConvexMultipointIterations(int numPerturbationIterations, int minimumPointsPerturbationThreshold)
        {
            btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(_native, numPerturbationIterations, minimumPointsPerturbationThreshold);
        }

        public void SetPlaneConvexMultipointIterations(int numPerturbationIterations)
        {
            btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(_native, numPerturbationIterations);
        }

        public void SetPlaneConvexMultipointIterations()
        {
            btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(_native);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDefaultCollisionConfiguration_new(IntPtr constructionInfo);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDefaultCollisionConfiguration_new2();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDefaultCollisionConfiguration_getSimplexSolver(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(IntPtr obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(IntPtr obj, int numPerturbationIterations);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(IntPtr obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(IntPtr obj, int numPerturbationIterations);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(IntPtr obj);
    }
}
