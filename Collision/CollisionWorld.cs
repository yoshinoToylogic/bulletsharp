using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class CollisionWorld
	{
		public class LocalShapeInfo
		{
			internal IntPtr _native;

			public LocalShapeInfo()
			{
				_native = btCollisionWorld_LocalShapeInfo_new();
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
			static extern void btCollisionWorld_LocalShapeInfo_delete(IntPtr obj);
		}

		public class LocalRayResult
		{
			internal IntPtr _native;

			public LocalRayResult(CollisionObject collisionObject, LocalShapeInfo localShapeInfo, Vector3 hitNormalLocal, float hitFraction)
			{
				_native = btCollisionWorld_LocalRayResult_new(collisionObject._native, localShapeInfo._native, ref hitNormalLocal, hitFraction);
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
					btCollisionWorld_LocalRayResult_delete(_native);
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
			static extern void btCollisionWorld_LocalRayResult_delete(IntPtr obj);
		}

		public class RayResultCallback
		{
			internal IntPtr _native;

			public float AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace)
			{
				return btCollisionWorld_RayResultCallback_addSingleResult(_native, rayResult._native, normalInWorldSpace);
			}

            public bool NeedsCollision(BroadphaseProxy proxy0)
            {
                return btCollisionWorld_RayResultCallback_needsCollision(_native, proxy0._native);
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
            static extern float btCollisionWorld_RayResultCallback_addSingleResult(IntPtr obj, IntPtr rayResult, bool normalInWorldSpace);
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
            static extern bool btCollisionWorld_RayResultCallback_hasHit(IntPtr obj);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern bool btCollisionWorld_RayResultCallback_needsCollision(IntPtr obj, IntPtr proxy0);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_RayResultCallback_setClosestHitFraction(IntPtr obj, float closestHitFraction);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(IntPtr obj, short collisionFilterGroup);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_RayResultCallback_setCollisionFilterMask(IntPtr obj, short collisionFilterMask);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_RayResultCallback_setCollisionObject(IntPtr obj, IntPtr collisionObject);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_RayResultCallback_setFlags(IntPtr obj, uint flags);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btCollisionWorld_RayResultCallback_delete(IntPtr obj);
		}

		public class ClosestRayResultCallback : RayResultCallback
		{
            public ClosestRayResultCallback(ref Vector3 rayFromWorld, ref Vector3 rayToWorld)
            {
                _native = btCollisionWorld_ClosestRayResultCallback_new(ref rayFromWorld, ref rayToWorld);
            }

			public ClosestRayResultCallback(Vector3 rayFromWorld, Vector3 rayToWorld)
			{
				_native = btCollisionWorld_ClosestRayResultCallback_new(ref rayFromWorld, ref rayToWorld);
			}

            public Vector3 HitNormalWorld
            {
                get
                {
                    Vector3 hitNormalWorld;
                    btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(_native, out hitNormalWorld);
                    return hitNormalWorld;
                }
                set { btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(_native, ref value); }
            }

            public Vector3 HitPointWorld
            {
                get
                {
                    Vector3 hitPointWorld;
                    btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(_native, out hitPointWorld);
                    return hitPointWorld;
                }
                set { btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(_native, ref value); }
            }

            public Vector3 RayFromWorld
            {
                get
                {
                    Vector3 rayFromWorld;
                    btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(_native, out rayFromWorld);
                    return rayFromWorld;
                }
                set { btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(_native, ref value); }
            }

            public Vector3 RayToWorld
            {
                get
                {
                    Vector3 rayToWorld;
                    btCollisionWorld_ClosestRayResultCallback_getRayToWorld(_native, out rayToWorld);
                    return rayToWorld;
                }
                set { btCollisionWorld_ClosestRayResultCallback_setRayToWorld(_native, ref value); }
            }

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern IntPtr btCollisionWorld_ClosestRayResultCallback_new([In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(IntPtr obj, [Out] out Vector3 rayFromWorld);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_ClosestRayResultCallback_getRayToWorld(IntPtr obj, [Out] out Vector3 rayToWorld);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(IntPtr obj, [Out] out Vector3 hitNormalWorld);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(IntPtr obj, [Out] out Vector3 hitPointWorld);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(IntPtr obj, [In] ref Vector3 rayFromWorld);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_ClosestRayResultCallback_setRayToWorld(IntPtr obj, [In] ref Vector3 rayToWorld);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(IntPtr obj, [In] ref Vector3 hitNormalWorld);
            [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern void btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(IntPtr obj, [In] ref Vector3 hitPointWorld);
		}

		public class AllHitsRayResultCallback : RayResultCallback
		{
			public AllHitsRayResultCallback(Vector3 rayFromWorld, Vector3 rayToWorld)
			{
				_native = btCollisionWorld_AllHitsRayResultCallback_new(ref rayFromWorld, ref rayToWorld);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern IntPtr btCollisionWorld_AllHitsRayResultCallback_new([In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld);
		}

		public class LocalConvexResult
		{
			internal IntPtr _native;

			public LocalConvexResult(CollisionObject hitCollisionObject, LocalShapeInfo localShapeInfo, Vector3 hitNormalLocal, Vector3 hitPointLocal, float hitFraction)
			{
				_native = btCollisionWorld_LocalConvexResult_new(hitCollisionObject._native, localShapeInfo._native, ref hitNormalLocal, ref hitPointLocal, hitFraction);
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
					btCollisionWorld_LocalConvexResult_delete(_native);
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
			static extern void btCollisionWorld_LocalConvexResult_delete(IntPtr obj);
		}

		public class ConvexResultCallback
		{
			internal IntPtr _native;

			public bool NeedsCollision(BroadphaseProxy proxy0)
			{
				return btCollisionWorld_ConvexResultCallback_needsCollision(_native, proxy0._native);
			}

			public float AddSingleResult(LocalConvexResult convexResult, bool normalInWorldSpace)
			{
				return btCollisionWorld_ConvexResultCallback_addSingleResult(_native, convexResult._native, normalInWorldSpace);
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
			static extern bool btCollisionWorld_ConvexResultCallback_hasHit(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern bool btCollisionWorld_ConvexResultCallback_needsCollision(IntPtr obj, IntPtr proxy0);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btCollisionWorld_ConvexResultCallback_addSingleResult(IntPtr obj, IntPtr convexResult, bool normalInWorldSpace);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btCollisionWorld_ConvexResultCallback_delete(IntPtr obj);
		}

		public class ClosestConvexResultCallback : ConvexResultCallback
		{
			public ClosestConvexResultCallback(Vector3 convexFromWorld, Vector3 convexToWorld)
			{
				_native = btCollisionWorld_ClosestConvexResultCallback_new(ref convexFromWorld, ref convexToWorld);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
            static extern IntPtr btCollisionWorld_ClosestConvexResultCallback_new([In] ref Vector3 convexFromWorld, [In] ref Vector3 convexToWorld);
		}

		public class ContactResultCallback
		{
			internal IntPtr _native;

			public bool NeedsCollision(BroadphaseProxy proxy0)
			{
				return btCollisionWorld_ContactResultCallback_needsCollision(_native, proxy0._native);
			}
            /*
			public float AddSingleResult(ManifoldPoint cp, CollisionObjectWrapper colObj0Wrap, int partId0, int index0, CollisionObjectWrapper colObj1Wrap, int partId1, int index1)
			{
				return btCollisionWorld_ContactResultCallback_addSingleResult(_native, cp._native, colObj0Wrap._native, partId0, index0, colObj1Wrap._native, partId1, index1);
			}
            */
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
			static extern bool btCollisionWorld_ContactResultCallback_needsCollision(IntPtr obj, IntPtr proxy0);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btCollisionWorld_ContactResultCallback_addSingleResult(IntPtr obj, IntPtr cp, IntPtr colObj0Wrap, int partId0, int index0, IntPtr colObj1Wrap, int partId1, int index1);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btCollisionWorld_ContactResultCallback_delete(IntPtr obj);
		}

		internal IntPtr _native;
        AlignedCollisionObjectArray _collisionObjectArray;

        protected CollisionConfiguration _collisionConfiguration;
        protected Dispatcher _dispatcher;
        protected BroadphaseInterface _broadphase;

        internal CollisionWorld(IntPtr native)
        {
            _native = native;
        }

		public CollisionWorld(Dispatcher dispatcher, BroadphaseInterface broadphasePairCache, CollisionConfiguration collisionConfiguration)
		{
			_native = btCollisionWorld_new(dispatcher._native, broadphasePairCache._native, collisionConfiguration._native);
            _dispatcher = dispatcher;
            _broadphase = broadphasePairCache;
            _collisionConfiguration = collisionConfiguration;
		}

		public void AddCollisionObject(CollisionObject collisionObject, short collisionFilterGroup, short collisionFilterMask)
		{
			btCollisionWorld_addCollisionObject(_native, collisionObject._native, collisionFilterGroup, collisionFilterMask);
		}

		public void AddCollisionObject(CollisionObject collisionObject, short collisionFilterGroup)
		{
			btCollisionWorld_addCollisionObject2(_native, collisionObject._native, collisionFilterGroup);
		}

		public void AddCollisionObject(CollisionObject collisionObject)
		{
			btCollisionWorld_addCollisionObject3(_native, collisionObject._native);
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

        public void ConvexSweepTest(ConvexShape castShape, Matrix from, Matrix to, ConvexResultCallback resultCallback, float allowedCcdPenetration)
		{
			btCollisionWorld_convexSweepTest(_native, castShape._native, ref from, ref to, resultCallback._native, allowedCcdPenetration);
		}

        public void ConvexSweepTest(ConvexShape castShape, Matrix from, Matrix to, ConvexResultCallback resultCallback)
		{
            btCollisionWorld_convexSweepTest2(_native, castShape._native, ref from, ref to, resultCallback._native);
		}

		public void DebugDrawObject(Matrix worldTransform, CollisionShape shape, Vector3 color)
		{
			btCollisionWorld_debugDrawObject(_native, ref worldTransform, shape._native, ref color);
		}

		public void DebugDrawWorld()
		{
			btCollisionWorld_debugDrawWorld(_native);
		}

        public void ObjectQuerySingle(ConvexShape castShape, Matrix rayFromTrans, Matrix rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape, Matrix colObjWorldTransform, ConvexResultCallback resultCallback, float allowedPenetration)
		{
			btCollisionWorld_objectQuerySingle(castShape._native, ref rayFromTrans, ref rayToTrans, collisionObject._native, collisionShape._native, ref colObjWorldTransform, resultCallback._native, allowedPenetration);
		}
        /*
        public void ObjectQuerySingleInternal(ConvexShape castShape, Matrix convexFromTrans, Matrix convexToTrans, CollisionObjectWrapper colObjWrap, ConvexResultCallback resultCallback, float allowedPenetration)
		{
			btCollisionWorld_objectQuerySingleInternal(castShape._native, ref convexFromTrans, ref convexToTrans, colObjWrap._native, resultCallback._native, allowedPenetration);
		}
        */
		public void PerformDiscreteCollisionDetection()
		{
			btCollisionWorld_performDiscreteCollisionDetection(_native);
		}

        public void RayTest(ref Vector3 rayFromWorld, ref Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTest(_native, ref rayFromWorld, ref rayToWorld, resultCallback._native);
		}

		public void RayTestSingle(Matrix rayFromTrans, Matrix rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape, Matrix colObjWorldTransform, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTestSingle(ref rayFromTrans, ref rayToTrans, collisionObject._native, collisionShape._native, ref colObjWorldTransform, resultCallback._native);
		}
        /*
		public void RayTestSingleInternal(Transform rayFromTrans, Transform rayToTrans, CollisionObjectWrapper collisionObjectWrap, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTestSingleInternal(rayFromTrans._native, rayToTrans._native, collisionObjectWrap._native, resultCallback._native);
		}
        */
		public void RemoveCollisionObject(CollisionObject collisionObject)
		{
			btCollisionWorld_removeCollisionObject(_native, collisionObject._native);
		}
        /*
		public void Serialize(Serializer serializer)
		{
			btCollisionWorld_serialize(_native, serializer._native);
		}
        */
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
			set { btCollisionWorld_setBroadphase(_native, value._native); }
		}

        public AlignedCollisionObjectArray CollisionObjectArray
        {
            get
            {
                if (_collisionObjectArray == null)
                {
                    _collisionObjectArray = new AlignedCollisionObjectArray(btCollisionWorld_getCollisionObjectArray(_native));
                }
                return _collisionObjectArray;
            }
        }

		public IDebugDraw DebugDrawer
		{
            get { return null; }
			//set { btCollisionWorld_setDebugDrawer(_native, value._native); }
            set { }
		}

        public Dispatcher Dispatcher
        {
            get { return _dispatcher; }
        }

		public DispatcherInfo DispatchInfo
		{
            get { return new DispatcherInfo(btCollisionWorld_getDispatchInfo(_native)); }
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
            get { return new OverlappingPairCache(btCollisionWorld_getPairCache(_native)); }
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
			}
		}

		~CollisionWorld()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_new(IntPtr dispatcher, IntPtr broadphasePairCache, IntPtr collisionConfiguration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_addCollisionObject(IntPtr obj, IntPtr collisionObject, short collisionFilterGroup, short collisionFilterMask);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_addCollisionObject2(IntPtr obj, IntPtr collisionObject, short collisionFilterGroup);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_addCollisionObject3(IntPtr obj, IntPtr collisionObject);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_computeOverlappingPairs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_contactPairTest(IntPtr obj, IntPtr colObjA, IntPtr colObjB, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_contactTest(IntPtr obj, IntPtr colObj, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_convexSweepTest(IntPtr obj, IntPtr castShape, [In] ref Matrix from, [In] ref Matrix to, IntPtr resultCallback, float allowedCcdPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_convexSweepTest2(IntPtr obj, IntPtr castShape, [In] ref Matrix from, [In] ref Matrix to, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_debugDrawObject(IntPtr obj, [In] ref Matrix worldTransform, IntPtr shape, [In] ref Vector3 color);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_debugDrawWorld(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getBroadphase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getCollisionObjectArray(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getDebugDrawer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getDispatcher(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getDispatchInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btCollisionWorld_getForceUpdateAllAabbs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btCollisionWorld_getNumCollisionObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btCollisionWorld_getPairCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_objectQuerySingle(IntPtr castShape, [In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObject, IntPtr collisionShape, [In] ref Matrix colObjWorldTransform, IntPtr resultCallback, float allowedPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_objectQuerySingleInternal(IntPtr castShape, IntPtr convexFromTrans, IntPtr convexToTrans, IntPtr colObjWrap, IntPtr resultCallback, float allowedPenetration);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_performDiscreteCollisionDetection(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_rayTest(IntPtr obj, [In] ref Vector3 rayFromWorld, [In] ref Vector3 rayToWorld, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_rayTestSingle([In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObject, IntPtr collisionShape, [In] ref Matrix colObjWorldTransform, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionWorld_rayTestSingleInternal([In] ref Matrix rayFromTrans, [In] ref Matrix rayToTrans, IntPtr collisionObjectWrap, IntPtr resultCallback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_removeCollisionObject(IntPtr obj, IntPtr collisionObject);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCollisionWorld_serialize(IntPtr obj, IntPtr serializer);
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
