using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp.SoftBody
{
    public class AlignedTetraArrayDebugView
    {
        private AlignedTetraArray _array;

        public AlignedTetraArrayDebugView(AlignedTetraArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Tetra[] Items
        {
            get
            {
                Tetra[] array = new Tetra[_array.Count];
                for (int i = 0; i < _array.Count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedTetraArrayEnumerator : IEnumerator<Tetra>
    {
        int _i;
        int _count;
        AlignedTetraArray _array;

        public AlignedTetraArrayEnumerator(AlignedTetraArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public Tetra Current
        {
            get { return _array[_i]; }
        }

        public void Dispose()
        {
        }

        object System.Collections.IEnumerator.Current
        {
            get { return _array[_i]; }
        }

        public bool MoveNext()
        {
            _i++;
            return _i != _count;
        }

        public void Reset()
        {
            _i = 0;
        }
    }

    [Serializable, DebuggerTypeProxy(typeof(AlignedTetraArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedTetraArray : AlignedObjectArray, IList<Tetra>, IDisposable
    {
        bool _preventDelete;

        internal AlignedTetraArray(IntPtr native, bool preventDelete = false)
            : base(native)
        {
            _preventDelete = preventDelete;
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
                if (!_preventDelete)
                {
                    btAlignedTetraArray_delete(_native);
                }
                _native = IntPtr.Zero;
            }
        }

        ~AlignedTetraArray()
        {
            Dispose(false);
        }

        public int IndexOf(Tetra item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Tetra item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Tetra this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)

                    throw new ArgumentOutOfRangeException("index");

                return new Tetra(btAlignedTetraArray_at(_native, index), true);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Tetra item)
        {
            btAlignedTetraArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedTetraArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(Tetra item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Tetra[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedTetraArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Tetra item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Tetra> GetEnumerator()
        {
            return new AlignedTetraArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedTetraArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr btAlignedTetraArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedTetraArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedTetraArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern int btAlignedTetraArray_size(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedTetraArray_delete(IntPtr obj);
    }
}
