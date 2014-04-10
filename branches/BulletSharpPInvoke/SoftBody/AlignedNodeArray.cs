using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp.SoftBody
{
    public class AlignedNodeArrayDebugView
    {
        private AlignedNodeArray _array;

        public AlignedNodeArrayDebugView(AlignedNodeArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Node[] Items
        {
            get
            {
                Node[] array = new Node[_array.Count];
                for (int i = 0; i < _array.Count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedNodeArrayEnumerator : IEnumerator<Node>
    {
        int _i;
        int _count;
        AlignedNodeArray _array;

        public AlignedNodeArrayEnumerator(AlignedNodeArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public Node Current
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

    [Serializable, DebuggerTypeProxy(typeof(AlignedNodeArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedNodeArray : AlignedObjectArray, IList<Node>, IDisposable
    {
        bool _preventDelete;

        internal AlignedNodeArray(IntPtr native, bool preventDelete = false)
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
                    btAlignedNodeArray_delete(_native);
                }
                _native = IntPtr.Zero;
            }
        }

        ~AlignedNodeArray()
        {
            Dispose(false);
        }

        public int IndexOf(Node item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Node item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Node this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)

                    throw new ArgumentOutOfRangeException("index");

                return new Node(btAlignedNodeArray_at(_native, index));
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Node item)
        {
            btAlignedNodeArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedNodeArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(Node item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Node[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedNodeArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Node item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return new AlignedNodeArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedNodeArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr btAlignedNodeArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedNodeArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedNodeArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern int btAlignedNodeArray_size(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedNodeArray_delete(IntPtr obj);
    }
}
