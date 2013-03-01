using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public enum ActivationState
    {
        Undefined = 0,
        ActiveTag = 1,
        IslandSleeping = 2,
        WantsDeactivation = 3,
        DisableDeactivation = 4,
        DisableSimulation = 5
    }

    [Flags]
    public enum CollisionFlags
    {
        StaticObject = 1,
        KinematicObject = 2,
        NoContactResponse = 4,
        CustomMaterialCallback = 8,
        CharacterObject = 16,
        DisableVisualizeObject = 32,
        DisableSpuCollisionProcessing = 64
    }

    public class CollisionObject : IDisposable
    {
        internal IntPtr _native;

        internal static CollisionObject GetManaged(IntPtr obj)
        {
            if (obj == IntPtr.Zero)
            {
                return null;
            }

            IntPtr userPtr = btCollisionObject_getUserPointer(obj);
            if (userPtr != IntPtr.Zero)
            {
                return GCHandle.FromIntPtr(userPtr).Target as CollisionObject;
            }

            return new CollisionObject(obj);
        }

        internal CollisionObject(IntPtr obj)
        {
            _native = obj;
            if (btCollisionObject_getUserPointer(_native) == IntPtr.Zero)
            {
                GCHandle handle = GCHandle.Alloc(this);
                btCollisionObject_setUserPointer(_native, GCHandle.ToIntPtr(handle));
            }
        }

        public CollisionObject()
        {
            _native = btCollisionObject_new();
        }

        public void Activate(bool forceActivate)
        {
            btCollisionObject_activate(_native, forceActivate);
        }

        public bool checkCollideWith(CollisionObject collisionObject)
        {
            return btCollisionObject_checkCollideWith(_native, collisionObject._native);
        }

        public void ForceActivationState(ActivationState newState)
        {
            btCollisionObject_forceActivationState(_native, newState);
        }

        public ActivationState ActivationState
        {
            get { return btCollisionObject_getActivationState(_native); }
            set { btCollisionObject_setActivationState(_native, value); }
        }

        public Vector3 AnisotropicFriction
        {
            get
            {
                Vector3 friction;
                btCollisionObject_getAnisotropicFriction(_native, out friction);
                return friction;
            }
            set { btCollisionObject_setAnisotropicFriction(_native, ref value); }
        }

        // TODO: FIXME: create btBroadPhaseProxy object
        private IntPtr BroadPhaseHandle
        {
            get { return btCollisionObject_getBroadphaseHandle(_native); }
            set { btCollisionObject_setBroadphaseHandle(_native, value); }
        }

        public float CcdMotionThreshold
        {
            get { return btCollisionObject_getCcdMotionThreshold(_native); }
            set { btCollisionObject_setCcdMotionThreshold(_native, value); }
        }

        public float CcdSquareMotionThreshold
        {
            get { return btCollisionObject_getCcdSquareMotionThreshold(_native); }
        }

        public float CcdSweptSphereRadius
        {
            get { return btCollisionObject_getCcdSweptSphereRadius(_native); }
            set { btCollisionObject_setCcdSweptSphereRadius(_native, value); }
        }

        public CollisionFlags CollisionFlags
        {
            get { return btCollisionObject_getCollisionFlags(_native); }
            set { btCollisionObject_setCollisionFlags(_native, value); }
        }

        public CollisionShape CollisionShape
        {
            get { return CollisionShape.GetManaged(btCollisionObject_getCollisionShape(_native)); }
            set { CollisionObject_setCollisionShape(_native, value._native); }
        }

        public int CompanionId
        {
            get { return btCollisionObject_getCompanionId(_native); }
            set { btCollisionObject_setCompanionId(_native, value); }
        }

        public float ContactProcessingThreshold
        {
            get { return btCollisionObject_getContactProcessingThreshold(_native); }
            set { btCollisionObject_setContactProcessingThreshold(_native, value); }
        }

        public float DeactivationTime
        {
            get { return btCollisionObject_getDeactivationTime(_native); }
            set { btCollisionObject_setDeactivationTime(_native, value); }
        }

        public float Friction
        {
            get { return btCollisionObject_getFriction(_native); }
            set { btCollisionObject_setFriction(_native, value); }
        }

        public bool HasAnisotropicFriction
        {
            get { return btCollisionObject_hasAnisotropicFriction(_native); }
        }

        public bool HasContactResponse
        {
            get { return btCollisionObject_hasContactResponse(_native); }
        }

        public float HitFraction
        {
            get { return btCollisionObject_getHitFraction(_native); }
            set { btCollisionObject_setHitFraction(_native, value); }
        }

        public Vector3 InterpolationAngularVelocity
        {
            get
            {
                Vector3 velocity;
                btCollisionObject_getInterpolationAngularVelocity(_native, out velocity);
                return velocity;
            }
        }

        public Vector3 InterpolationLinearVelocity
        {
            get
            {
                Vector3 velocity;
                btCollisionObject_getInterpolationLinearVelocity(_native, out velocity);
                return velocity;
            }
        }

        public Matrix InterpolationWorldTransform
        {
            get
            {
                Matrix transform;
                btCollisionObject_getInterpolationWorldTransform(_native, out transform);
                return transform;
            }
            set { btCollisionObject_setInterpolationWorldTransform(_native, ref value); }
        }

        public int IslandTag
        {
            get { return btCollisionObject_getIslandTag(_native); }
            set { btCollisionObject_setIslandTag(_native, value); }
        }

        public bool IsActive
        {
            get { return btCollisionObject_isActive(_native); }
        }

        public bool IsKinematicObject
        {
            get { return btCollisionObject_isKinematicObject(_native); }
        }

        public bool IsStaticObject
        {
            get { return btCollisionObject_isStaticObject(_native); }
        }

        public bool IsStaticOrKinematicObject
        {
            get { return btCollisionObject_isStaticOrKinematicObject(_native); }
        }

        public bool MergesSimulationIslands
        {
            get { return btCollisionObject_mergesSimulationIslands(_native); }
        }

        public float Restitution
        {
            get { return btCollisionObject_getRestitution(_native); }
            set { btCollisionObject_setRestitution(_native, value); }
        }

        public void GetWorldTransform(out Matrix transform)
        {
            btCollisionObject_getWorldTransform(_native, out transform);
        }

        public Matrix WorldTransform
        {
            get
            {
                Matrix transform;
                btCollisionObject_getWorldTransform(_native, out transform);
                return transform;
            }
            set { btCollisionObject_setWorldTransform(_native, ref value); }
        }
        /*
        public void setInterpolationAngularVelocity(Vector3 angvel)
        {
            CollisionObject_setInterpolationAngularVelocity(_native, angvel.Handle);
        }

        public void setInterpolationLinearVelocity(Vector3 linvel)
        {
            CollisionObject_setInterpolationLinearVelocity(_native, linvel.Handle);
        }

        public void setInterpolationWorldTransform(Matrix trans)
        {
            CollisionObject_setInterpolationWorldTransform(_native, trans.Handle);
        }
        */

        public object UserObject { get; set; }

        #region Native Invokes
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionObject_new();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_activate(IntPtr obj, bool forceActivate);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionObject_checkCollideWith(IntPtr obj, IntPtr collisionObject);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_forceActivationState(IntPtr obj, ActivationState newState);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern ActivationState btCollisionObject_getActivationState(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_getAnisotropicFriction(IntPtr obj, [Out] out Vector3 friction);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionObject_getBroadphaseHandle(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionObject_getCcdMotionThreshold(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionObject_getCcdSquareMotionThreshold(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionObject_getCcdSweptSphereRadius(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern CollisionFlags btCollisionObject_getCollisionFlags(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionObject_getCollisionShape(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btCollisionObject_getCompanionId(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionObject_getContactProcessingThreshold(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionObject_getDeactivationTime(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionObject_getFriction(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionObject_getHitFraction(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_getInterpolationAngularVelocity(IntPtr obj, [Out] out Vector3 velocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_getInterpolationLinearVelocity(IntPtr obj, [Out] out Vector3 velocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_getInterpolationWorldTransform(IntPtr obj, [Out] out Matrix transform);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btCollisionObject_getIslandTag(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btCollisionObject_getRestitution(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCollisionObject_getUserPointer(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_getWorldTransform(IntPtr obj, [Out] out Matrix transform);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionObject_hasAnisotropicFriction(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionObject_hasContactResponse(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionObject_isActive(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionObject_isKinematicObject(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionObject_isStaticObject(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionObject_isStaticOrKinematicObject(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btCollisionObject_mergesSimulationIslands(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setActivationState(IntPtr obj, ActivationState newState);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setAnisotropicFriction(IntPtr obj, [In] ref Vector3 anisotropicFriction);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setBroadphaseHandle(IntPtr obj, IntPtr handle);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setCcdMotionThreshold(IntPtr obj, float ccdMotionThreshold);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setCcdSweptSphereRadius(IntPtr obj, float radius);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setCollisionFlags(IntPtr obj, CollisionFlags flags);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void CollisionObject_setCollisionShape(IntPtr obj, IntPtr collisionShape);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setCompanionId(IntPtr obj, int id);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setContactProcessingThreshold(IntPtr obj, float contactProcessingThreshold);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setDeactivationTime(IntPtr obj, float time);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setFriction(IntPtr obj, float frict);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setHitFraction(IntPtr obj, float hitFraction);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setInterpolationAngularVelocity(IntPtr obj, [In] ref Vector3 velocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setInterpolationLinearVelocity(IntPtr obj, [In] ref Vector3 velocity);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setInterpolationWorldTransform(IntPtr obj, [In] ref Matrix transform);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setIslandTag(IntPtr obj, int tag);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setRestitution(IntPtr obj, float rest);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setUserPointer(IntPtr obj, IntPtr userPointer);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_setWorldTransform(IntPtr obj, [In] ref Matrix worldTrans);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCollisionObject_delete(IntPtr obj);
        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_native != IntPtr.Zero)
            {
                btCollisionObject_delete(_native);
                _native = IntPtr.Zero;
            }
        }

        ~CollisionObject()
        {
            Dispose(false);
        }
    }
}
