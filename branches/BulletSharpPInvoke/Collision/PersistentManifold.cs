using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public delegate void ContactDestroyedEventHandler(Object userPersistantData);
    public delegate void ContactProcessedEventHandler(ManifoldPoint cp, CollisionObject body0, CollisionObject body1);

	public class PersistentManifold //: TypedObject
	{
        internal IntPtr _native;

        static ContactDestroyedEventHandler _contactDestroyed;
        static ContactProcessedEventHandler _contactProcessed;
        static ContactDestroyedUnmanagedDelegate _contactDestroyedUnmanaged;
        static ContactProcessedUnmanagedDelegate _contactProcessedUnmanaged;
        static IntPtr _contactDestroyedUnmanagedPtr;
        static IntPtr _contactProcessedUnmanagedPtr;

        private delegate bool ContactDestroyedUnmanagedDelegate(IntPtr userPersistantData);
        private delegate bool ContactProcessedUnmanagedDelegate(IntPtr cp, IntPtr body0, IntPtr body1);

        static bool ContactDestroyedUnmanaged(IntPtr userPersistentData)
        {
            _contactDestroyed.Invoke(userPersistentData);
	        return false;
        }

        static bool ContactProcessedUnmanaged(IntPtr cp, IntPtr body0, IntPtr body1)
        {
            _contactProcessed.Invoke(new ManifoldPoint(cp, true), CollisionObject.GetManaged(body0), CollisionObject.GetManaged(body1));
            return false;
        }

        public static event ContactDestroyedEventHandler ContactDestroyed
        {
            add
            {
                if (_contactDestroyedUnmanaged == null)
                {
                    _contactDestroyedUnmanaged = new ContactDestroyedUnmanagedDelegate(ContactDestroyedUnmanaged);
                    _contactDestroyedUnmanagedPtr = Marshal.GetFunctionPointerForDelegate(_contactDestroyedUnmanaged);
                }
                setGContactDestroyedCallback(_contactDestroyedUnmanagedPtr);
                _contactDestroyed += value;
            }
            remove
            {
                _contactDestroyed -= value;
                if (_contactDestroyed.GetInvocationList().Length == 0)
                {
                    setGContactDestroyedCallback(IntPtr.Zero);
                }
            }
        }

        public static event ContactProcessedEventHandler ContactProcessed
        {
            add
            {
                if (_contactProcessedUnmanaged == null)
                {
                    _contactProcessedUnmanaged = new ContactProcessedUnmanagedDelegate(ContactProcessedUnmanaged);
                    _contactProcessedUnmanagedPtr = Marshal.GetFunctionPointerForDelegate(_contactProcessedUnmanaged);
                }
                setGContactProcessedCallback(_contactProcessedUnmanagedPtr);
                _contactProcessed += value;
            }
            remove
            {
                _contactProcessed -= value;
                if (_contactProcessed.GetInvocationList().Length == 0)
                {
                    setGContactProcessedCallback(IntPtr.Zero);
                }
            }
        }

		internal PersistentManifold(IntPtr native)
		{
            _native = native;
		}

		public PersistentManifold()
		{
            _native = btPersistentManifold_new();
		}

		public PersistentManifold(CollisionObject body0, CollisionObject body1, int __unnamed2, float contactBreakingThreshold, float contactProcessingThreshold)
		{
            _native = btPersistentManifold_new2(body0._native, body1._native, __unnamed2, contactBreakingThreshold, contactProcessingThreshold);
		}

		public int AddManifoldPoint(ManifoldPoint newPoint, bool isPredictive)
		{
			return btPersistentManifold_addManifoldPoint(_native, newPoint._native, isPredictive);
		}

		public int AddManifoldPoint(ManifoldPoint newPoint)
		{
			return btPersistentManifold_addManifoldPoint2(_native, newPoint._native);
		}

		public void ClearManifold()
		{
			btPersistentManifold_clearManifold(_native);
		}

		public void ClearUserCache(ManifoldPoint pt)
		{
			btPersistentManifold_clearUserCache(_native, pt._native);
		}

		public int GetCacheEntry(ManifoldPoint newPoint)
		{
			return btPersistentManifold_getCacheEntry(_native, newPoint._native);
		}

		public ManifoldPoint GetContactPoint(int index)
		{
			return new ManifoldPoint(btPersistentManifold_getContactPoint(_native, index), true);
		}

		public void RefreshContactPoints(Matrix trA, Matrix trB)
		{
			btPersistentManifold_refreshContactPoints(_native, ref trA, ref trB);
		}

		public void RemoveContactPoint(int index)
		{
			btPersistentManifold_removeContactPoint(_native, index);
		}

		public void ReplaceContactPoint(ManifoldPoint newPoint, int insertIndex)
		{
			btPersistentManifold_replaceContactPoint(_native, newPoint._native, insertIndex);
		}

		public void SetBodies(CollisionObject body0, CollisionObject body1)
		{
			btPersistentManifold_setBodies(_native, body0._native, body1._native);
		}

		public bool ValidContactDistance(ManifoldPoint pt)
		{
			return btPersistentManifold_validContactDistance(_native, pt._native);
		}

		public CollisionObject Body0
		{
			get { return CollisionObject.GetManaged(btPersistentManifold_getBody0(_native)); }
		}

		public CollisionObject Body1
		{
			get { return CollisionObject.GetManaged(btPersistentManifold_getBody1(_native)); }
		}

		public int CompanionIdA
		{
			get { return btPersistentManifold_getCompanionIdA(_native); }
			set { btPersistentManifold_setCompanionIdA(_native, value); }
		}

		public int CompanionIdB
		{
			get { return btPersistentManifold_getCompanionIdB(_native); }
			set { btPersistentManifold_setCompanionIdB(_native, value); }
		}

		public float ContactBreakingThreshold
		{
			get { return btPersistentManifold_getContactBreakingThreshold(_native); }
			set { btPersistentManifold_setContactBreakingThreshold(_native, value); }
		}

		public float ContactProcessingThreshold
		{
			get { return btPersistentManifold_getContactProcessingThreshold(_native); }
			set { btPersistentManifold_setContactProcessingThreshold(_native, value); }
		}

		public int Index1a
		{
			get { return btPersistentManifold_getIndex1a(_native); }
			set { btPersistentManifold_setIndex1a(_native, value); }
		}

		public int NumContacts
		{
			get { return btPersistentManifold_getNumContacts(_native); }
			set { btPersistentManifold_setNumContacts(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPersistentManifold_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPersistentManifold_new2(IntPtr body0, IntPtr body1, int __unnamed2, float contactBreakingThreshold, float contactProcessingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPersistentManifold_addManifoldPoint(IntPtr obj, IntPtr newPoint, bool isPredictive);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPersistentManifold_addManifoldPoint2(IntPtr obj, IntPtr newPoint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_clearManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_clearUserCache(IntPtr obj, IntPtr pt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPersistentManifold_getBody0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPersistentManifold_getBody1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPersistentManifold_getCacheEntry(IntPtr obj, IntPtr newPoint);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPersistentManifold_getCompanionIdA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPersistentManifold_getCompanionIdB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btPersistentManifold_getContactBreakingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btPersistentManifold_getContactPoint(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btPersistentManifold_getContactProcessingThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPersistentManifold_getIndex1a(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btPersistentManifold_getNumContacts(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_refreshContactPoints(IntPtr obj, [In] ref Matrix trA, [In] ref Matrix trB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_removeContactPoint(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_replaceContactPoint(IntPtr obj, IntPtr newPoint, int insertIndex);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_setBodies(IntPtr obj, IntPtr body0, IntPtr body1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_setCompanionIdA(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_setCompanionIdB(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_setContactBreakingThreshold(IntPtr obj, float contactBreakingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_setContactProcessingThreshold(IntPtr obj, float contactProcessingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_setIndex1a(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btPersistentManifold_setNumContacts(IntPtr obj, int cachedPoints);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btPersistentManifold_validContactDistance(IntPtr obj, IntPtr pt);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr getGContactDestroyedCallback();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr getGContactProcessedCallback();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void setGContactDestroyedCallback(IntPtr value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void setGContactProcessedCallback(IntPtr value);
	}
}
