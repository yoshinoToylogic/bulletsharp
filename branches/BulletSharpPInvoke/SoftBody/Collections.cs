﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp.SoftBody
{
    public class NodePtrArrayEnumerator : ArrayEnumerator, IEnumerator<Node>
    {
        IList<Node> _array;

        public NodePtrArrayEnumerator(IList<Node> array)
        {
            _array = array;
            _count = array.Count;
        }

        public Node Current
        {
            get { return _array[_i]; }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return _array[_i]; }
        }
    }

    public class NodePtrArray : FixedSizeArray, IList<Node>
    {
        internal NodePtrArray(IntPtr native, int count)
            : base(native, count)
        {
        }

        public void Add(Node item)
        {
            throw new InvalidOperationException();
        }

        public int IndexOf(Node item)
        {
            throw new NotImplementedException();
        }

        public Node this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return new Node(btSoftBodyNodePtrArray_at(_native, index), true);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Contains(Node item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Node[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return new NodePtrArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new NodePtrArrayEnumerator(this);
        }

        public void Insert(int index, Node item)
        {
            throw new InvalidOperationException();
        }

        public bool Remove(Node item)
        {
            throw new NotImplementedException();
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btSoftBodyNodePtrArray_at(IntPtr obj, int n);
    }
}
