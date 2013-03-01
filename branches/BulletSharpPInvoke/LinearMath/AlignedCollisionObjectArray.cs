using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class AlignedCollisionObjectArray : AlignedObjectArray, IList<CollisionObject>, IDisposable
    {
        internal AlignedCollisionObjectArray(IntPtr native)
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

        ~AlignedCollisionObjectArray()
        {
            Dispose(false);
        }

        public int IndexOf(CollisionObject item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, CollisionObject item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public CollisionObject this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
		            throw new ArgumentOutOfRangeException("index");

                return CollisionObject.GetManaged(btAlignedCollisionObjectArray_at(_native, index));
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(CollisionObject item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(CollisionObject item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(CollisionObject[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedCollisionObjectArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CollisionObject item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<CollisionObject> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern int btAlignedCollisionObjectArray_size(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr btAlignedCollisionObjectArray_at(IntPtr obj, int n);
    }
}
