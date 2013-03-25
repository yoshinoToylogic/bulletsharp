using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class AlignedVector3Array : AlignedObjectArray, IList<Vector3>, IDisposable
    {
        internal AlignedVector3Array(IntPtr native)
            : base(native)
        {
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
                //btAlignedObjectArray_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~AlignedVector3Array()
        {
            Dispose(false);
        }

        public int IndexOf(Vector3 item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Vector3 item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Vector3 this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)

                    throw new ArgumentOutOfRangeException("index");

                Vector3 value;
                btAlignedVector3Array_at(_native, index, out value);
                return value;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Vector3 item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Vector3 item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Vector3[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedVector3Array_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Vector3 item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Vector3> GetEnumerator()
        {
            return new Vector3ArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Vector3ArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern int btAlignedVector3Array_size(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr btAlignedVector3Array_at(IntPtr obj, int n, [Out] out Vector3 value);
    }
}
