using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp.SoftBody
{
    public class AlignedLinkArrayDebugView
    {
        private AlignedLinkArray _array;

        public AlignedLinkArrayDebugView(AlignedLinkArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Link[] Items
        {
            get
            {
                Link[] array = new Link[_array.Count];
                for (int i = 0; i < _array.Count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedLinkArrayEnumerator : IEnumerator<Link>
    {
        int _i;
        int _count;
        AlignedLinkArray _array;

        public AlignedLinkArrayEnumerator(AlignedLinkArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public Link Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedLinkArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedLinkArray : IList<Link>
    {
        private IntPtr _native;

        internal AlignedLinkArray(IntPtr native)
        {
            _native = native;
        }

        public int IndexOf(Link item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Link item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Link this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)

                    throw new ArgumentOutOfRangeException("index");

                return new Link(btAlignedSoftBodyLinkArray_at(_native, index), true);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Link item)
        {
            btAlignedSoftBodyLinkArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedSoftBodyLinkArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(Link item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Link[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedSoftBodyLinkArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Link item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Link> GetEnumerator()
        {
            return new AlignedLinkArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedLinkArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr btAlignedSoftBodyLinkArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedSoftBodyLinkArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedSoftBodyLinkArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern int btAlignedSoftBodyLinkArray_size(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedSoftBodyLinkArray_delete(IntPtr obj);
    }
}
