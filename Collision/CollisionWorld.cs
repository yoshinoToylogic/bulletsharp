using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
    public class AllHitsRayResultCallback : RayResultCallback
    {
        public AllHitsRayResultCallback(Vector3 rayFromWorld, Vector3 rayToWorld)
        {
            RayFromWorld = rayFromWorld;
            RayToWorld = rayToWorld;

            CollisionObjects = new List<CollisionObject>();
            HitFractions = new List<float>();
            HitNormalWorld = new List<Vector3>();
            HitPointWorld = new List<Vector3>();
        }

        public override float AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace)
        {
            CollisionObject = rayResult.CollisionObject;
            CollisionObjects.Add(rayResult.CollisionObject);
            if (normalInWorldSpace)
            {
                HitNormalWorld.Add(rayResult.HitNormalLocal);
            }
            else
            {
                // need to transform normal into worldspace
                HitNormalWorld.Add(Vector3.TransformCoordinate(rayResult.HitNormalLocal, CollisionObject.WorldTransform.Basis));
            }
            HitPointWorld.Add(Vector3.Lerp(RayFromWorld, RayToWorld, rayResult.HitFraction));
            HitFractions.Add(rayResult.HitFraction);
            return ClosestHitFraction;
        }

        public List<CollisionObject> CollisionObjects { get; set; }
        public List<float> HitFractions { get; set; }
        public List<Vector3> HitNormalWorld { get; set; }
        public List<Vector3> HitPointWorld { get; set; }
        public Vector3 RayFromWorld { get; set; }
        public Vector3 RayToWorld { get; set; }
    }

	public class ClosestConvexResultCallback : ConvexResultCallback
	{
		public ClosestConvexResultCallback(ref Vector3 convexFromWorld, ref Vector3 convexToWorld)
		{
            ConvexFromWorld = convexFromWorld;
            ConvexToWorld = convexToWorld;
		}

		public ClosestConvexResultCallback(Vector3 convexFromWorld, Vector3 convexToWorld)
		{
            ConvexFromWorld = convexFromWorld;
            ConvexToWorld = convexToWorld;
		}

        public override float AddSingleResult(LocalConvexResult convexResult, bool normalInWorldSpace)
        {
            //caller already does the filter on the m_closestHitFraction
            Debug.Assert(convexResult.HitFraction <= ClosestHitFraction);

            ClosestHitFraction = convexResult.HitFraction;
            HitCollisionObject = convexResult.HitCollisionObject;
            if (normalInWorldSpace)
            {
                HitNormalWorld = convexResult.HitNormalLocal;
            }
            else
            {
                // need to transform normal into worldspace
                HitNormalWorld = Vector3.TransformCoordinate(convexResult.HitNormalLocal, HitCollisionObject.WorldTransform.Basis);
            }
            HitPointWorld = convexResult.HitPointLocal;
            return convexResult.HitFraction;
        }

        public Vector3 ConvexFromWorld { get; set; }
        public Vector3 ConvexToWorld { get; set; }
        public CollisionObject HitCollisionObject { get; set; }
        public Vector3 HitNormalWorld { get; set; }
        public Vector3 HitPointWorld { get; set; }
	}

    public class ClosestRayResultCallback : RayResultCallback
    {
        public ClosestRayResultCallback(ref Vector3 rayFromWorld, ref Vector3 rayToWorld)
        {
            RayFromWorld = rayFromWorld;
            RayToWorld = rayToWorld;
        }

        public ClosestRayResultCallback(Vector3 rayFromWorld, Vector3 rayToWorld)
        {
            RayFromWorld = rayFromWorld;
            RayToWorld = rayToWorld;
        }

        public override float AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace)
        {
            //caller already does the filter on the m_closestHitFraction
            Debug.Assert(rayResult.HitFraction <= ClosestHitFraction);

            ClosestHitFraction = rayResult.HitFraction;
            CollisionObject = rayResult.CollisionObject;
            if (normalInWorldSpace)
            {
                HitNormalWorld = rayResult.HitNormalLocal;
            }
            else
            {
                // need to transform normal into worldspace
                HitNormalWorld = Vector3.TransformCoordinate(rayResult.HitNormalLocal, CollisionObject.WorldTransform.Basis);
            }
            HitPointWorld = Vector3.Lerp(RayFromWorld, RayToWorld, rayResult.HitFraction);
            return rayResult.HitFraction;
        }

        public Vector3 RayFromWorld { get; set; } //used to calculate hitPointWorld from hitFraction
        public Vector3 RayToWorld { get; set; }

        public Vector3 HitNormalWorld { get; set; }
        public Vector3 HitPointWorld { get; set; }
    }

    public abstract class ContactResultCallback : IDisposable
    {
        internal IntPtr _native;

        [UnmanagedFunctionPointer(Native.Conv)]
        delegate float AddSingleResultUnmanagedDelegate(IntPtr cp, IntPtr colObj0Wrap, int partId0, int index0, IntPtr colObj1Wrap, int partId1, int index1);
        [UnmanagedFunctionPointer(Native.Conv)]
        delegate bool NeedsCollisionUnmanagedDelegate(IntPtr proxy0);

        AddSingleResultUnmanagedDelegate _addSingleResult;
        NeedsCollisionUnmanagedDelegate _needsCollision;

        public ContactResultCallback()
        {
            _addSingleResult = AddSingleResultUnmanaged;
            _needsCollision = NeedsCollisionUnmanaged;
            _native = btCollisionWorld_ContactResultCallbackWrapper_new(
                Marshal.GetFunctionPointerForDelegate(_addSingleResult),
                Marshal.GetFunctionPointerForDelegate(_needsCollision));
        }

        float AddSingleResultUnmanaged(IntPtr cp, IntPtr colObj0Wrap, int partId0, int index0, IntPtr colObj1Wrap, int partId1, int index1)
        {
            return AddSingleResult(new ManifoldPoint(cp, true),
                new CollisionObjectWrapper(colObj0Wrap), partId0, index0,
                new CollisionObjectWrapper(colObj1Wrap), partId1, index1);
        }

        public abstract float AddSingleResult(ManifoldPoint cp, CollisionObjectWrapper colObj0Wrap, int partId0, int index0, CollisionObjectWrapper colObj1Wrap, int partId1, int index1);

        bool NeedsCollisionUnmanaged(IntPtr proxy0)
        {
            return NeedsCollision(BroadphaseProxy.GetManaged(proxy0));
        }

        public virtual bool NeedsCollision(BroadphaseProxy proxy0)
        {
            return btCollisionWorld_ContactResultCallbackWrapper_needsCollision(_native, proxy0._native);
        }

        public short CollisionFilterGroup
        {
            get { return btCollisionWorld_ContactResultCallback_getCollisionFilterGroup(_native); }
            set { btCollisionWorld_ContactResultCallback_setCollisionFilterGroup(_native, value); }
        }

        public short CollisionFilterMask
        {
            get { return btCollisionWorld_ContactResultCallback_getCollisionFilterMask(_native); }
            set { btCollisionWorld_ContactResultCallback_setCollisionFilterMask(_native, value); }
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
                btCollisionWorld_ContactResultCallback_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~ContactResultCallback()
        {
            Dispose(false);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern short btCollisionWorld_ContactResultCallback_getCollisionFilterGroup(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern short btCollisionWorld_ContactResultCallback_getCollisionFilterMask(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ContactResultCallback_setCollisionFilterGroup(IntPtr obj, short value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ContactResultCallback_setCollisionFilterMask(IntPtr obj, short value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ContactResultCallback_delete(IntPtr obj);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_ContactResultCallbackWrapper_new(IntPtr addSingleResult, IntPtr needsCollision);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool btCollisionWorld_ContactResultCallbackWrapper_needsCollision(IntPtr obj, IntPtr proxy0);
    }

    public abstract class ConvexResultCallback : IDisposable
    {
        internal IntPtr _native;

        [UnmanagedFunctionPointer(Native.Conv)]
        delegate float AddSingleResultUnmanagedDelegate(IntPtr convexResult, bool normalInWorldSpace);
        [UnmanagedFunctionPointer(Native.Conv)]
        delegate bool NeedsCollisionUnmanagedDelegate(IntPtr proxy0);

        AddSingleResultUnmanagedDelegate _addSingleResult;
        NeedsCollisionUnmanagedDelegate _needsCollision;

        public ConvexResultCallback()
        {
            _addSingleResult = AddSingleResultUnmanaged;
            _needsCollision = NeedsCollisionUnmanaged;
            _native = btCollisionWorld_ConvexResultCallbackWrapper_new(
                Marshal.GetFunctionPointerForDelegate(_addSingleResult),
                Marshal.GetFunctionPointerForDelegate(_needsCollision));
        }

        float AddSingleResultUnmanaged(IntPtr rayResult, bool normalInWorldSpace)
        {
            return AddSingleResult(new LocalConvexResult(rayResult, true), normalInWorldSpace);
        }

        public abstract float AddSingleResult(LocalConvexResult rayResult, bool normalInWorldSpace);

        bool NeedsCollisionUnmanaged(IntPtr proxy0)
        {
            return NeedsCollision(BroadphaseProxy.GetManaged(proxy0));
        }

        public virtual bool NeedsCollision(BroadphaseProxy proxy0)
        {
            return btCollisionWorld_ConvexResultCallbackWrapper_needsCollision(_native, proxy0._native);
        }

        public float ClosestHitFraction
        {
            get { return btCollisionWorld_ConvexResultCallback_getClosestHitFraction(_native); }
            set { btCollisionWorld_ConvexResultCallback_setClosestHitFraction(_native, value); }
        }

        public CollisionFilterGroups CollisionFilterGroup
        {
            get { return (CollisionFilterGroups)btCollisionWorld_ConvexResultCallback_getCollisionFilterGroup(_native); }
            set { btCollisionWorld_ConvexResultCallback_setCollisionFilterGroup(_native, (short)value); }
        }

        public CollisionFilterGroups CollisionFilterMask
        {
            get { return (CollisionFilterGroups)btCollisionWorld_ConvexResultCallback_getCollisionFilterMask(_native); }
            set { btCollisionWorld_ConvexResultCallback_setCollisionFilterMask(_native, (short)value); }
        }

        public bool HasHit
        {
            get { return btCollisionWorld_ConvexResultCallback_hasHit(_native); }
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
                btCollisionWorld_ConvexResultCallback_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~ConvexResultCallback()
        {
            Dispose(false);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionWorld_ConvexResultCallback_getClosestHitFraction(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern short btCollisionWorld_ConvexResultCallback_getCollisionFilterGroup(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern short btCollisionWorld_ConvexResultCallback_getCollisionFilterMask(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool btCollisionWorld_ConvexResultCallback_hasHit(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ConvexResultCallback_setClosestHitFraction(IntPtr obj, float value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ConvexResultCallback_setCollisionFilterGroup(IntPtr obj, short value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ConvexResultCallback_setCollisionFilterMask(IntPtr obj, short value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_ConvexResultCallback_delete(IntPtr obj);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_ConvexResultCallbackWrapper_new(IntPtr addSingleResult, IntPtr needsCollision);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool btCollisionWorld_ConvexResultCallbackWrapper_needsCollision(IntPtr obj, IntPtr proxy0);
    }

    public class LocalConvexResult : IDisposable
    {
        internal IntPtr _native;
        private readonly bool _preventDelete;
        private LocalShapeInfo _localShapeInfo;

        internal LocalConvexResult(IntPtr native, bool preventDelete)
        {
            _native = native;
            _preventDelete = preventDelete;
        }

        public LocalConvexResult(CollisionObject hitCollisionObject, LocalShapeInfo localShapeInfo, Vector3 hitNormalLocal, Vector3 hitPointLocal, float hitFraction)
        {
            _native = btCollisionWorld_LocalConvexResult_new(hitCollisionObject._native, localShapeInfo._native, ref hitNormalLocal, ref hitPointLocal, hitFraction);
            _localShapeInfo = localShapeInfo;
        }

        public CollisionObject HitCollisionObject
        {
            get { return CollisionObject.GetManaged(btCollisionWorld_LocalConvexResult_getHitCollisionObject(_native)); }
            set { btCollisionWorld_LocalConvexResult_setHitCollisionObject(_native, (value != null) ? value._native : IntPtr.Zero); }
        }

        public float HitFraction
        {
            get { return btCollisionWorld_LocalConvexResult_getHitFraction(_native); }
            set { btCollisionWorld_LocalConvexResult_setHitFraction(_native, value); }
        }

        public Vector3 HitNormalLocal
        {
            get
            {
                Vector3 value;
                btCollisionWorld_LocalConvexResult_getHitNormalLocal(_native, out value);
                return value;
            }
            set { btCollisionWorld_LocalConvexResult_setHitNormalLocal(_native, ref value); }
        }

        public Vector3 HitPointLocal
        {
            get
            {
                Vector3 value;
                btCollisionWorld_LocalConvexResult_getHitPointLocal(_native, out value);
                return value;
            }
            set { btCollisionWorld_LocalConvexResult_setHitPointLocal(_native, ref value); }
        }

        public LocalShapeInfo LocalShapeInfo
        {
            get { return _localShapeInfo; }
            set
            {
                _localShapeInfo = value;
                btCollisionWorld_LocalConvexResult_setLocalShapeInfo(_native, (value != null) ? value._native : IntPtr.Zero);
            }
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
                    btCollisionWorld_LocalConvexResult_delete(_native);
                }
                _native = IntPtr.Zero;
            }
        }

        ~LocalConvexResult()
        {
            Dispose(false);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_LocalConvexResult_new(IntPtr hitCollisionObject, IntPtr localShapeInfo, [In] ref Vector3 hitNormalLocal, [In] ref Vector3 hitPointLocal, float hitFraction);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_LocalConvexResult_getHitCollisionObject(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionWorld_LocalConvexResult_getHitFraction(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalConvexResult_getHitNormalLocal(IntPtr obj, [Out] out Vector3 value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalConvexResult_getHitPointLocal(IntPtr obj, [Out] out Vector3 value);
        //[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        //static extern IntPtr btCollisionWorld_LocalConvexResult_getLocalShapeInfo(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalConvexResult_setHitCollisionObject(IntPtr obj, IntPtr value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalConvexResult_setHitFraction(IntPtr obj, float value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalConvexResult_setHitNormalLocal(IntPtr obj, [In] ref Vector3 value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalConvexResult_setHitPointLocal(IntPtr obj, [In] ref Vector3 value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalConvexResult_setLocalShapeInfo(IntPtr obj, IntPtr value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalConvexResult_delete(IntPtr obj);
    }

    public class LocalRayResult : IDisposable
    {
        internal IntPtr _native;
        private readonly bool _preventDelete;
        private LocalShapeInfo _localShapeInfo;

        internal LocalRayResult(IntPtr native, bool preventDelete)
        {
            _native = native;
            _preventDelete = preventDelete;
        }

        public LocalRayResult(CollisionObject collisionObject, LocalShapeInfo localShapeInfo, Vector3 hitNormalLocal, float hitFraction)
        {
            _native = btCollisionWorld_LocalRayResult_new(collisionObject._native, localShapeInfo._native, ref hitNormalLocal, hitFraction);
            _localShapeInfo = localShapeInfo;
        }

        public CollisionObject CollisionObject
        {
            get { return CollisionObject.GetManaged(btCollisionWorld_LocalRayResult_getCollisionObject(_native)); }
            set { btCollisionWorld_LocalRayResult_setCollisionObject(_native, (value != null) ? value._native : IntPtr.Zero); }
        }

        public float HitFraction
        {
            get { return btCollisionWorld_LocalRayResult_getHitFraction(_native); }
            set { btCollisionWorld_LocalRayResult_setHitFraction(_native, value); }
        }

        public Vector3 HitNormalLocal
        {
            get
            {
                Vector3 value;
                btCollisionWorld_LocalRayResult_getHitNormalLocal(_native, out value);
                return value;
            }
            set { btCollisionWorld_LocalRayResult_setHitNormalLocal(_native, ref value); }
        }

        public LocalShapeInfo LocalShapeInfo
        {
            get { return _localShapeInfo; }
            set
            {
                _localShapeInfo = value;
                btCollisionWorld_LocalRayResult_setLocalShapeInfo(_native, (value != null) ? value._native : IntPtr.Zero);
            }
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
                    btCollisionWorld_LocalRayResult_delete(_native);
                }
                _native = IntPtr.Zero;
            }
        }

        ~LocalRayResult()
        {
            Dispose(false);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_LocalRayResult_new(IntPtr collisionObject, IntPtr localShapeInfo, [In] ref Vector3 hitNormalLocal, float hitFraction);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_LocalRayResult_getCollisionObject(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionWorld_LocalRayResult_getHitFraction(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalRayResult_getHitNormalLocal(IntPtr obj, [Out] out Vector3 value);
        //[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        //static extern IntPtr btCollisionWorld_LocalRayResult_getLocalShapeInfo(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalRayResult_setCollisionObject(IntPtr obj, IntPtr value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalRayResult_setHitFraction(IntPtr obj, float value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalRayResult_setHitNormalLocal(IntPtr obj, [In] ref Vector3 value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalRayResult_setLocalShapeInfo(IntPtr obj, IntPtr value);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_LocalRayResult_delete(IntPtr obj);
    }

	public class LocalShapeInfo : IDisposable
	{
		internal IntPtr _native;

		public LocalShapeInfo()
		{
			_native = btCollisionWorld_LocalShapeInfo_new();
		}

		public int ShapePart
		{
			get { return btCollisionWorld_LocalShapeInfo_getShapePart(_native); }
			set { btCollisionWorld_LocalShapeInfo_setShapePart(_native, value); }
		}

		public int TriangleIndex
		{
			get { return btCollisionWorld_LocalShapeInfo_getTriangleIndex(_native); }
			set { btCollisionWorld_LocalShapeInfo_setTriangleIndex(_native, value); }
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
				btCollisionWorld_LocalShapeInfo_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~LocalShapeInfo()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_LocalShapeInfo_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionWorld_LocalShapeInfo_getShapePart(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionWorld_LocalShapeInfo_getTriangleIndex(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_LocalShapeInfo_setShapePart(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_LocalShapeInfo_setTriangleIndex(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_LocalShapeInfo_delete(IntPtr obj);
	}

	public abstract class RayResultCallback : IDisposable
	{
		internal IntPtr _native;

        [UnmanagedFunctionPointer(Native.Conv)]
        delegate float AddSingleResultUnmanagedDelegate(IntPtr rayResult, bool normalInWorldSpace);
        [UnmanagedFunctionPointer(Native.Conv)]
		delegate bool NeedsCollisionUnmanagedDelegate(IntPtr proxy0);

		AddSingleResultUnmanagedDelegate _addSingleResult;
		NeedsCollisionUnmanagedDelegate _needsCollision;

		public RayResultCallback()
		{
			_addSingleResult = AddSingleResultUnmanaged;
			_needsCollision = NeedsCollisionUnmanaged;
			_native = btCollisionWorld_RayResultCallbackWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_addSingleResult),
				Marshal.GetFunctionPointerForDelegate(_needsCollision));
		}

        float AddSingleResultUnmanaged(IntPtr rayResult, bool normalInWorldSpace)
		{
			return AddSingleResult(new LocalRayResult(rayResult, true), normalInWorldSpace);
		}

        public abstract float AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace);

		bool NeedsCollisionUnmanaged(IntPtr proxy0)
		{
			return NeedsCollision(BroadphaseProxy.GetManaged(proxy0));
		}

		public virtual bool NeedsCollision(BroadphaseProxy proxy0)
		{
			return btCollisionWorld_RayResultCallbackWrapper_needsCollision(_native, proxy0._native);
		}

		public float ClosestHitFraction
		{
			get { return btCollisionWorld_RayResultCallback_getClosestHitFraction(_native); }
			set { btCollisionWorld_RayResultCallback_setClosestHitFraction(_native, value); }
		}

		public short CollisionFilterGroup
		{
			get { return btCollisionWorld_RayResultCallback_getCollisionFilterGroup(_native); }
			set { btCollisionWorld_RayResultCallback_setCollisionFilterGroup(_native, value); }
		}

		public short CollisionFilterMask
		{
			get { return btCollisionWorld_RayResultCallback_getCollisionFilterMask(_native); }
			set { btCollisionWorld_RayResultCallback_setCollisionFilterMask(_native, value); }
		}

		public CollisionObject CollisionObject
		{
			get { return CollisionObject.GetManaged(btCollisionWorld_RayResultCallback_getCollisionObject(_native)); }
			set { btCollisionWorld_RayResultCallback_setCollisionObject(_native, value._native); }
		}

		public uint Flags
		{
			get { return btCollisionWorld_RayResultCallback_getFlags(_native); }
			set { btCollisionWorld_RayResultCallback_setFlags(_native, value); }
		}

		public bool HasHit
		{
			get { return btCollisionWorld_RayResultCallback_hasHit(_native); }
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
				btCollisionWorld_RayResultCallback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~RayResultCallback()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btCollisionWorld_RayResultCallback_getClosestHitFraction(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern short btCollisionWorld_RayResultCallback_getCollisionFilterGroup(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern short btCollisionWorld_RayResultCallback_getCollisionFilterMask(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_RayResultCallback_getCollisionObject(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern uint btCollisionWorld_RayResultCallback_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionWorld_RayResultCallback_hasHit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_RayResultCallback_setClosestHitFraction(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(IntPtr obj, short value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_RayResultCallback_setCollisionFilterMask(IntPtr obj, short value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_RayResultCallback_setCollisionObject(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_RayResultCallback_setFlags(IntPtr obj, uint value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_RayResultCallback_delete(IntPtr obj);

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionWorld_RayResultCallbackWrapper_new(IntPtr addSingleResult, IntPtr needsCollision);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool btCollisionWorld_RayResultCallbackWrapper_needsCollision(IntPtr obj, IntPtr proxy0);
	}

	public class CollisionWorld : IDisposable
	{
		internal IntPtr _native;

		protected AlignedCollisionObjectArray _collisionObjectArray;
		protected Dispatcher _dispatcher;
		protected BroadphaseInterface _broadphase;
        private DispatcherInfo _dispatchInfo;
        internal IDebugDraw _debugDrawer;

		internal CollisionWorld(IntPtr native)
		{
            if (native == IntPtr.Zero)
            {
                return;
            }
			_native = native;
            _collisionObjectArray = new AlignedCollisionObjectArray(btCollisionWorld_getCollisionObjectArray(native), this);
		}

		public CollisionWorld(Dispatcher dispatcher, BroadphaseInterface broadphasePairCache, CollisionConfiguration collisionConfiguration)
            : this(btCollisionWorld_new(dispatcher._native, broadphasePairCache._native, collisionConfiguration._native))
		{
			_dispatcher = dispatcher;
			Broadphase = broadphasePairCache;
		}

		public void AddCollisionObject(CollisionObject collisionObject)
		{
            _collisionObjectArray.Add(collisionObject);
		}

        public void AddCollisionObject(CollisionObject collisionObject, CollisionFilterGroups collisionFilterGroup, CollisionFilterGroups collisionFilterMask)
		{
            _collisionObjectArray.Add(collisionObject, (short)collisionFilterGroup, (short)collisionFilterMask);
		}

        public void AddCollisionObject(CollisionObject collisionObject, short collisionFilterGroup, short collisionFilterMask)
        {
            _collisionObjectArray.Add(collisionObject, collisionFilterGroup, collisionFilterMask);
        }

		public void ComputeOverlappingPairs()
		{
			btCollisionWorld_computeOverlappingPairs(_native);
		}

		public void ContactPairTest(CollisionObject colObjA, CollisionObject colObjB, ContactResultCallback resultCallback)
		{
			btCollisionWorld_contactPairTest(_native, colObjA._native, colObjB._native, resultCallback._native);
		}

		public void ContactTest(CollisionObject colObj, ContactResultCallback resultCallback)
		{
			btCollisionWorld_contactTest(_native, colObj._native, resultCallback._native);
		}

        public void ConvexSweepTest(ConvexShape castShape, ref Matrix from, ref Matrix to, ConvexResultCallback resultCallback)
        {
            btCollisionWorld_convexSweepTest(_native, castShape._native, ref from, ref to, resultCallback._native);
        }

		public void ConvexSweepTest(ConvexShape castShape, Matrix from, Matrix to, ConvexResultCallback resultCallback)
		{
			btCollisionWorld_convexSweepTest(_native, castShape._native, ref from, ref to, resultCallback._native);
		}

        public void ConvexSweepTest(ConvexShape castShape, ref Matrix from, ref Matrix to, ConvexResultCallback resultCallback, float allowedCcdPenetration)
        {
            btCollisionWorld_convexSweepTest2(_native, castShape._native, ref from, ref to, resultCallback._native, allowedCcdPenetration);
        }

		public void ConvexSweepTest(ConvexShape castShape, Matrix from, Matrix to, ConvexResultCallback resultCallback, float allowedCcdPenetration)
		{
			btCollisionWorld_convexSweepTest2(_native, castShape._native, ref from, ref to, resultCallback._native, allowedCcdPenetration);
		}

        public void DebugDrawObject(ref Matrix worldTransform, CollisionShape shape, ref Vector3 color)
        {
            btCollisionWorld_debugDrawObject(_native, ref worldTransform, shape._native, ref color);
        }

		public void DebugDrawObject(Matrix worldTransform, CollisionShape shape, Vector3 color)
		{
			btCollisionWorld_debugDrawObject(_native, ref worldTransform, shape._native, ref color);
		}

		public void DebugDrawWorld()
		{
			btCollisionWorld_debugDrawWorld(_native);
		}

        public static void ObjectQuerySingle(ConvexShape castShape, ref Matrix rayFromTrans, ref Matrix rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape, ref Matrix colObjWorldTransform, ConvexResultCallback resultCallback, float allowedPenetration)
        {
            btCollisionWorld_objectQuerySingle(castShape._native, ref rayFromTrans, ref rayToTrans, collisionObject._native, collisionShape._native, ref colObjWorldTransform, resultCallback._native, allowedPenetration);
        }

		public static void ObjectQuerySingle(ConvexShape castShape, Matrix rayFromTrans, Matrix rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape, Matrix colObjWorldTransform, ConvexResultCallback resultCallback, float allowedPenetration)
		{
			btCollisionWorld_objectQuerySingle(castShape._native, ref rayFromTrans, ref rayToTrans, collisionObject._native, collisionShape._native, ref colObjWorldTransform, resultCallback._native, allowedPenetration);
		}

        public static void ObjectQuerySingleInternal(ConvexShape castShape, ref Matrix convexFromTrans, ref Matrix convexToTrans, CollisionObjectWrapper colObjWrap, ConvexResultCallback resultCallback, float allowedPenetration)
        {
            btCollisionWorld_objectQuerySingleInternal(castShape._native, ref convexFromTrans, ref convexToTrans, colObjWrap._native, resultCallback._native, allowedPenetration);
        }

		public static void ObjectQuerySingleInternal(ConvexShape castShape, Matrix convexFromTrans, Matrix convexToTrans, CollisionObjectWrapper colObjWrap, ConvexResultCallback resultCallback, float allowedPenetration)
		{
			btCollisionWorld_objectQuerySingleInternal(castShape._native, ref convexFromTrans, ref convexToTrans, colObjWrap._native, resultCallback._native, allowedPenetration);
		}

		public void PerformDiscreteCollisionDetection()
		{
			btCollisionWorld_performDiscreteCollisionDetection(_native);
		}

		public void RayTest(ref Vector3 rayFromWorld, ref Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTest(_native, ref rayFromWorld, ref rayToWorld, resultCallback._native);
		}

		public void RayTest(Vector3 rayFromWorld, Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTest(_native, ref rayFromWorld, ref rayToWorld, resultCallback._native);
		}

        public static void RayTestSingle(ref Matrix rayFromTrans, ref Matrix rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape, ref Matrix colObjWorldTransform, RayResultCallback resultCallback)
        {
            btCollisionWorld_rayTestSingle(ref rayFromTrans, ref rayToTrans, collisionObject._native, collisionShape._native, ref colObjWorldTransform, resultCallback._native);
        }

		public static void RayTestSingle(Matrix rayFromTrans, Matrix rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape, Matrix colObjWorldTransform, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTestSingle(ref rayFromTrans, ref rayToTrans, collisionObject._native, collisionShape._native, ref colObjWorldTransform, resultCallback._native);
		}

        public static void RayTestSingleInternal(ref Matrix rayFromTrans, ref Matrix rayToTrans, CollisionObjectWrapper collisionObjectWrap, RayResultCallback resultCallback)
        {
            btCollisionWorld_rayTestSingleInternal(ref rayFromTrans, ref rayToTrans, collisionObjectWrap._native, resultCallback._native);
        }

		public static void RayTestSingleInternal(Matrix rayFromTrans, Matrix rayToTrans, CollisionObjectWrapper collisionObjectWrap, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTestSingleInternal(ref rayFromTrans, ref rayToTrans, collisionObjectWrap._native, resultCallback._native);
		}

		public void RemoveCollisionObject(CollisionObject collisionObject)
		{
            _collisionObjectArray.Remove(collisionObject);
		}

        protected void SerializeCollisionObjects(Serializer serializer)
        {
            foreach (CollisionObject colObj in CollisionObjectArray)
            {
                if (colObj.InternalType == CollisionObjectTypes.CollisionObject)
                {
                    colObj.SerializeSingleObject(serializer);
                }
            }

	        // keep track of shapes already serialized
            Dictionary<CollisionShape, int> serializedShapes = new Dictionary<CollisionShape, int>();

            foreach (CollisionObject colObj in CollisionObjectArray)
            {
                CollisionShape shape = colObj.CollisionShape;
                if (!serializedShapes.ContainsKey(shape))
                {
                    serializedShapes.Add(shape, 0);
                    shape.SerializeSingleShape(serializer);
                }
            }
        }

		public virtual void Serialize(Serializer serializer)
		{
            serializer.StartSerialization();
            SerializeCollisionObjects(serializer);
            serializer.FinishSerialization();
		}

		public void UpdateAabbs()
		{
			btCollisionWorld_updateAabbs(_native);
		}

		public void UpdateSingleAabb(CollisionObject colObj)
		{
			btCollisionWorld_updateSingleAabb(_native, colObj._native);
		}

		public BroadphaseInterface Broadphase
		{
			get { return _broadphase; }
			set
			{
                if (_broadphase != null)
                {
                    _broadphase._worldRefs.Remove(this);
                }
				_broadphase = value;
                _broadphase._worldRefs.Add(this);
				btCollisionWorld_setBroadphase(_native, value._native);
			}
		}

		public AlignedCollisionObjectArray CollisionObjectArray
		{
			get
			{
				return _collisionObjectArray;
			}
		}

		public IDebugDraw DebugDrawer
		{
            get { return _debugDrawer; }
            set
            {
                if (_debugDrawer != null)
                {
                    if (_debugDrawer == value) {
                        return;
                    }

                    // Clear IDebugDraw wrapper
                    if (!(_debugDrawer is DebugDraw)) {
                        //btIDebugDrawer_delete(btCollisionWorld_getDebugDrawer(_native));
                    }
                }

                _debugDrawer = value;
                if (value == null) {
                    btCollisionWorld_setDebugDrawer(_native, IntPtr.Zero);
                    return;
                }

                DebugDraw cast = value as DebugDraw;
                if (cast != null) {
                    btCollisionWorld_setDebugDrawer(_native, cast._native);
                } else {
                    // Create IDebugDraw wrapper, remember to delete it
                    IntPtr wrapper = DebugDraw.CreateWrapper(value, false);
                    btCollisionWorld_setDebugDrawer(_native, wrapper);
                }
            }
		}

		public Dispatcher Dispatcher
		{
			get { return _dispatcher; }
            internal set
            {
                _dispatcher = value;
                _dispatcher._worldRefs.Add(this);
            }
		}

		public DispatcherInfo DispatchInfo
		{
            get
            {
                if (_dispatchInfo == null)
                {
                    _dispatchInfo = new DispatcherInfo(btCollisionWorld_getDispatchInfo(_native), true);
                }
                return _dispatchInfo;
            }
		}

		public bool ForceUpdateAllAabbs
		{
			get { return btCollisionWorld_getForceUpdateAllAabbs(_native); }
			set { btCollisionWorld_setForceUpdateAllAabbs(_native, value); }
		}

		public int NumCollisionObjects
		{
			get { return btCollisionWorld_getNumCollisionObjects(_native); }
		}

		public OverlappingPairCache PairCache
		{
            get { return Broadphase.OverlappingPairCache; }
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
				btCollisionWorld_delete(_native);
				_native = IntPtr.Zero;

                _broadphase._worldRefs.Remove(this);
                if (_broadphase._worldDeferredCleanup && _broadphase._worldRefs.Count == 0)
                {
                    _broadphase.Dispose();
                }

                _dispatcher._worldRefs.Remove(this);
                if (_dispatcher._worldDeferredCleanup && _dispatcher._worldRefs.Count == 0)
                {
                    _dispatcher.Dispose();
                }
			}
		}

		~CollisionWorld()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_new(IntPtr dispatcher, IntPtr broadphasePairCache, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_computeOverlappingPairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_contactPairTest(IntPtr obj, IntPtr colObjA, IntPtr colObjB, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_contactTest(IntPtr obj, IntPtr colObj, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_convexSweepTest(IntPtr obj, IntPtr castShape, [In] ref Matrix from, [In] ref Matrix to, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_convexSweepTest2(IntPtr obj, IntPtr castShape, [In] ref Matrix from, [In] ref Matrix to, IntPtr resultCallback, float allowedCcdPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_debugDrawObject(IntPtr obj, [In] ref Matrix worldTransform, IntPtr shape, [In] ref Vector3 color);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_debugDrawWorld(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getBroadphase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		internal static extern IntPtr btCollisionWorld_getCollisionObjectArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getDebugDrawer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getDispatcher(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getDispatchInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btCollisionWorld_getForceUpdateAllAabbs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionWorld_getNumCollisionObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getPairCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_objectQuerySingle(IntPtr castShape, [In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObject, IntPtr collisionShape, [In] ref Matrix colObjWorldTransform, IntPtr resultCallback, float allowedPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_objectQuerySingleInternal(IntPtr castShape, [In] ref Matrix convexFromTrans, [In] ref Matrix convexToTrans, IntPtr colObjWrap, IntPtr resultCallback, float allowedPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_performDiscreteCollisionDetection(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_rayTest(IntPtr obj, [In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_rayTestSingle([In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObject, IntPtr collisionShape, [In] ref Matrix colObjWorldTransform, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_rayTestSingleInternal([In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObjectWrap, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_setBroadphase(IntPtr obj, IntPtr pairCache);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_setDebugDrawer(IntPtr obj, IntPtr debugDrawer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_setForceUpdateAllAabbs(IntPtr obj, bool forceUpdateAllAabbs);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_updateAabbs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_updateSingleAabb(IntPtr obj, IntPtr colObj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_delete(IntPtr obj);
	}
}
