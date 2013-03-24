using System;
using System.Collections;

namespace BulletSharp
{
    public class AlignedObjectArrayEnumerator : IEnumerator
    {
        int _i;
        int _count;
        AlignedObjectArray _array;

        public AlignedObjectArrayEnumerator(AlignedObjectArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public CollisionObject Current
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

    public abstract class AlignedObjectArray
    {
        protected IntPtr _native;

        internal AlignedObjectArray(IntPtr native)
        {
            _native = native;
        }

        public abstract int Count { get; }

        public abstract CollisionObject this[int index] { get; set; }
    }
}
