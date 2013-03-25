using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class UIntArrayEnumerator : IEnumerator<uint>
    {
        int _i;
        int _count;
        IList<uint> _array;

        public UIntArrayEnumerator(IList<uint> array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public uint Current
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

    public class Vector3ArrayEnumerator : IEnumerator<Vector3>
    {
        int _i;
        int _count;
        IList<Vector3> _array;

        public Vector3ArrayEnumerator(IList<Vector3> array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public Vector3 Current
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

    public class UIntArray : IList<uint>
    {
        internal IntPtr _native;

        int _count;

        internal UIntArray(IntPtr native, int count)
        {
            _native = native;
            _count = count;
        }

        public int IndexOf(uint item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, uint item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public uint this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)

                    throw new ArgumentOutOfRangeException("index");

                return uint_array_at(_native, index);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(uint item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(uint item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(uint[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(uint item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<uint> GetEnumerator()
        {
            return new UIntArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new UIntArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern uint uint_array_at(IntPtr obj, int n);
    }

    public class Vector3Array : IList<Vector3>
    {
        internal IntPtr _native;

        int _count;

        internal Vector3Array(IntPtr native, int count)
        {
            _native = native;
            _count = count;
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
                btVector3_array_at(_native, index, out value);
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
            get { return _count; }
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
        protected static extern IntPtr btVector3_array_at(IntPtr obj, int n, [Out] out Vector3 value);
    }
}
