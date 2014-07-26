using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MultiBody : IDisposable
	{
		internal IntPtr _native;
        MultiBodyLink[] _links;

		internal MultiBody(IntPtr native)
		{
			_native = native;
		}

		public MultiBody(int nLinks, float mass, Vector3 inertia, bool fixedBase, bool canSleep)
		{
			_native = btMultiBody_new(nLinks, mass, ref inertia, fixedBase, canSleep);
		}

		public void AddBaseForce(Vector3 f)
		{
			btMultiBody_addBaseForce(_native, ref f);
		}

		public void AddBaseTorque(Vector3 t)
		{
			btMultiBody_addBaseTorque(_native, ref t);
		}

		public void AddJointTorque(int i, float q)
		{
			btMultiBody_addJointTorque(_native, i, q);
		}

		public void AddLinkForce(int i, Vector3 f)
		{
			btMultiBody_addLinkForce(_native, i, ref f);
		}

		public void AddLinkTorque(int i, Vector3 t)
		{
			btMultiBody_addLinkTorque(_native, i, ref t);
		}
        /*
		public void ApplyDeltaVee(float deltaVee, float multiplier)
		{
			btMultiBody_applyDeltaVee(_native, deltaVee._native, multiplier);
		}

		public void ApplyDeltaVee(float deltaVee)
		{
			btMultiBody_applyDeltaVee2(_native, deltaVee._native);
		}

		public void CalcAccelerationDeltas(float force, float output, AlignedObjectArray scratchR, AlignedObjectArray scratchV)
		{
			btMultiBody_calcAccelerationDeltas(_native, force._native, output._native, scratchR._native, scratchV._native);
		}
        */
		public void CheckMotionAndSleepIfRequired(float timestep)
		{
			btMultiBody_checkMotionAndSleepIfRequired(_native, timestep);
		}

		public void ClearForcesAndTorques()
		{
			btMultiBody_clearForcesAndTorques(_native);
		}

		public void ClearVelocities()
		{
			btMultiBody_clearVelocities(_native);
		}
        /*
		public void FillContactJacobian(int link, Vector3 contactPoint, Vector3 normal, float jac, AlignedObjectArray scratchR, AlignedObjectArray scratchV, AlignedObjectArray scratchM)
		{
			btMultiBody_fillContactJacobian(_native, link, ref contactPoint, ref normal, jac._native, scratchR._native, scratchV._native, scratchM._native);
		}
        */
		public float GetJointPos(int i)
		{
			return btMultiBody_getJointPos(_native, i);
		}

		public float GetJointTorque(int i)
		{
			return btMultiBody_getJointTorque(_native, i);
		}

		public float GetJointVel(int i)
		{
			return btMultiBody_getJointVel(_native, i);
		}

		public MultiBodyLink GetLink(int index)
		{
            if (_links == null) {
		        _links = new MultiBodyLink[NumLinks];
	        }
	        if (_links[index] == null) {
                _links[index] = new MultiBodyLink(btMultiBody_getLink(_native, index));
	        }
	        return _links[index];
		}

		public Vector3 GetLinkForce(int i)
		{
            Vector3 value;
            btMultiBody_getLinkForce(_native, i, out value);
            return value;
		}

		public Vector3 GetLinkInertia(int i)
		{
            Vector3 value;
            btMultiBody_getLinkInertia(_native, i, out value);
            return value;
		}

		public float GetLinkMass(int i)
		{
			return btMultiBody_getLinkMass(_native, i);
		}

		public Vector3 GetLinkTorque(int i)
		{
            Vector3 value;
			btMultiBody_getLinkTorque(_native, i, out value);
            return value;
		}

		public int GetParent(int linkNum)
		{
			return btMultiBody_getParent(_native, linkNum);
		}

		public Quaternion GetParentToLocalRot(int i)
		{
            Quaternion value;
			btMultiBody_getParentToLocalRot(_native, i, out value);
            return value;
		}

		public Vector3 GetRVector(int i)
		{
            Vector3 value;
            btMultiBody_getRVector(_native, i, out value);
            return value;
		}

		public void GoToSleep()
		{
			btMultiBody_goToSleep(_native);
		}

		public Vector3 LocalDirToWorld(int i, Vector3 vec)
		{
            Vector3 value;
			btMultiBody_localDirToWorld(_native, i, ref vec, out value);
            return value;
		}

		public Vector3 LocalPosToWorld(int i, Vector3 vec)
		{
            Vector3 value;
            btMultiBody_localPosToWorld(_native, i, ref vec, out value);
            return value;
		}

		public void SetCanSleep(bool canSleep)
		{
			btMultiBody_setCanSleep(_native, canSleep);
		}

		public void SetJointPos(int i, float q)
		{
			btMultiBody_setJointPos(_native, i, q);
		}

		public void SetJointVel(int i, float qdot)
		{
			btMultiBody_setJointVel(_native, i, qdot);
		}

		public void SetupPrismatic(int i, float mass, Vector3 inertia, int parent, Quaternion rotParentToThis, Vector3 jointAxis, Vector3 rVectorWhenQZero)
		{
			btMultiBody_setupPrismatic(_native, i, mass, ref inertia, parent, ref rotParentToThis, ref jointAxis, ref rVectorWhenQZero);
		}

		public void SetupPrismatic(int i, float mass, Vector3 inertia, int parent, Quaternion rotParentToThis, Vector3 jointAxis, Vector3 rVectorWhenQZero, bool disableParentCollision)
		{
			btMultiBody_setupPrismatic2(_native, i, mass, ref inertia, parent, ref rotParentToThis, ref jointAxis, ref rVectorWhenQZero, disableParentCollision);
		}

		public void SetupRevolute(int i, float mass, Vector3 inertia, int parent, Quaternion zeroRotParentToThis, Vector3 jointAxis, Vector3 parentAxisPosition, Vector3 myAxisPosition)
		{
			btMultiBody_setupRevolute(_native, i, mass, ref inertia, parent, ref zeroRotParentToThis, ref jointAxis, ref parentAxisPosition, ref myAxisPosition);
		}

		public void SetupRevolute(int i, float mass, Vector3 inertia, int parent, Quaternion zeroRotParentToThis, Vector3 jointAxis, Vector3 parentAxisPosition, Vector3 myAxisPosition, bool disableParentCollision)
		{
			btMultiBody_setupRevolute2(_native, i, mass, ref inertia, parent, ref zeroRotParentToThis, ref jointAxis, ref parentAxisPosition, ref myAxisPosition, disableParentCollision);
		}

		public void StepPositions(float dt)
		{
			btMultiBody_stepPositions(_native, dt);
		}

		public void StepVelocities(float dt, AlignedObjectArray scratchR, AlignedObjectArray scratchV, AlignedObjectArray scratchM)
		{
			btMultiBody_stepVelocities(_native, dt, scratchR._native, scratchV._native, scratchM._native);
		}

		public void WakeUp()
		{
			btMultiBody_wakeUp(_native);
		}

		public Vector3 WorldDirToLocal(int i, Vector3 vec)
		{
            Vector3 value;
			btMultiBody_worldDirToLocal(_native, i, ref vec, out value);
            return value;
		}

		public Vector3 WorldPosToLocal(int i, Vector3 vec)
		{
            Vector3 value;
            btMultiBody_worldPosToLocal(_native, i, ref vec, out value);
            return value;
		}

		public float AngularDamping
		{
			get { return btMultiBody_getAngularDamping(_native); }
		}

		public Vector3 AngularMomentum
		{
			get
			{
				Vector3 value;
				btMultiBody_getAngularMomentum(_native, out value);
				return value;
			}
		}

		public MultiBodyLinkCollider BaseCollider
		{
            get { return CollisionObject.GetManaged(btMultiBody_getBaseCollider(_native)) as MultiBodyLinkCollider; }
			set { btMultiBody_setBaseCollider(_native, value._native); }
		}

		public Vector3 BaseForce
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseForce(_native, out value);
				return value;
			}
		}

		public Vector3 BaseInertia
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseInertia(_native, out value);
				return value;
			}
			set { btMultiBody_setBaseInertia(_native, ref value); }
		}

		public float BaseMass
		{
			get { return btMultiBody_getBaseMass(_native); }
			set { btMultiBody_setBaseMass(_native, value); }
		}

		public Vector3 BaseOmega
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseOmega(_native, out value);
				return value;
			}
			set { btMultiBody_setBaseOmega(_native, ref value); }
		}

		public Vector3 BasePosition
		{
			get
			{
				Vector3 value;
				btMultiBody_getBasePos(_native, out value);
				return value;
			}
			set { btMultiBody_setBasePos(_native, ref value); }
		}

		public Vector3 BaseTorque
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseTorque(_native, out value);
				return value;
			}
		}

		public Vector3 BaseVelocity
		{
			get
			{
				Vector3 value;
				btMultiBody_getBaseVel(_native, out value);
				return value;
			}
			set { btMultiBody_setBaseVel(_native, ref value); }
		}

		public int CompanionId
		{
			get { return btMultiBody_getCompanionId(_native); }
			set { btMultiBody_setCompanionId(_native, value); }
		}

		public bool HasFixedBase
		{
			get { return btMultiBody_hasFixedBase(_native); }
		}

		public bool HasSelfCollision
		{
			get { return btMultiBody_hasSelfCollision(_native); }
			set { btMultiBody_setHasSelfCollision(_native, value); }
		}

		public bool IsAwake
		{
			get { return btMultiBody_isAwake(_native); }
		}

		public float KineticEnergy
		{
			get { return btMultiBody_getKineticEnergy(_native); }
		}

		public float LinearDamping
		{
			get { return btMultiBody_getLinearDamping(_native); }
			set { btMultiBody_setLinearDamping(_native, value); }
		}

		public float MaxAppliedImpulse
		{
			get { return btMultiBody_getMaxAppliedImpulse(_native); }
			set { btMultiBody_setMaxAppliedImpulse(_native, value); }
		}

		public int NumLinks
		{
			get { return btMultiBody_getNumLinks(_native); }
            set
            {
                btMultiBody_setNumLinks(_native, value);
                if (_links != null)
                {
                    Array.Resize(ref _links, value);
                }
            }
		}

		public bool UseGyroTerm
		{
			get { return btMultiBody_getUseGyroTerm(_native); }
			set { btMultiBody_setUseGyroTerm(_native, value); }
		}
        /*
		public float VelocityVector
		{
			get { return btMultiBody_getVelocityVector(_native); }
		}
        */
		public Quaternion WorldToBaseRot
		{
			get
			{
				Quaternion value;
				btMultiBody_getWorldToBaseRot(_native, out value);
				return value;
			}
			set { btMultiBody_setWorldToBaseRot(_native, ref value); }
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
				btMultiBody_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~MultiBody()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_new(int n_links, float mass, [In] ref Vector3 inertia, bool fixed_base_, bool can_sleep_);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addBaseForce(IntPtr obj, [In] ref Vector3 f);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addBaseTorque(IntPtr obj, [In] ref Vector3 t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addJointTorque(IntPtr obj, int i, float Q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addLinkForce(IntPtr obj, int i, [In] ref Vector3 f);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_addLinkTorque(IntPtr obj, int i, [In] ref Vector3 t);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_applyDeltaVee(IntPtr obj, IntPtr delta_vee, float multiplier);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_applyDeltaVee2(IntPtr obj, IntPtr delta_vee);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_calcAccelerationDeltas(IntPtr obj, IntPtr force, IntPtr output, IntPtr scratch_r, IntPtr scratch_v);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_checkMotionAndSleepIfRequired(IntPtr obj, float timestep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_clearForcesAndTorques(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_clearVelocities(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_fillContactJacobian(IntPtr obj, int link, [In] ref Vector3 contact_point, [In] ref Vector3 normal, IntPtr jac, IntPtr scratch_r, IntPtr scratch_v, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getAngularDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getAngularMomentum(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_getBaseCollider(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getBaseForce(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getBaseInertia(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getBaseMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getBaseOmega(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getBasePos(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getBaseTorque(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getBaseVel(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBody_getCompanionId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getJointPos(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getJointTorque(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getJointVel(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getKineticEnergy(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getLinearDamping(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_getLink(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getLinkForce(IntPtr obj, int i, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getLinkInertia(IntPtr obj, int i, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getLinkMass(IntPtr obj, int i);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_getLinkTorque(IntPtr obj, int i, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultiBody_getMaxAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBody_getNumLinks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultiBody_getParent(IntPtr obj, int link_num);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getParentToLocalRot(IntPtr obj, int i, [Out] out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_getRVector(IntPtr obj, int i, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btMultiBody_getUseGyroTerm(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBody_getVelocityVector(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btMultiBody_getWorldToBaseRot(IntPtr obj, [Out] out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_goToSleep(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btMultiBody_hasFixedBase(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btMultiBody_hasSelfCollision(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btMultiBody_isAwake(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_localDirToWorld(IntPtr obj, int i, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_localPosToWorld(IntPtr obj, int i, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseCollider(IntPtr obj, IntPtr collider);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseInertia(IntPtr obj, [In] ref Vector3 inertia);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseMass(IntPtr obj, float mass);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseOmega(IntPtr obj, [In] ref Vector3 omega);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBasePos(IntPtr obj, [In] ref Vector3 pos);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setBaseVel(IntPtr obj, [In] ref Vector3 vel);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setCanSleep(IntPtr obj, bool canSleep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setCompanionId(IntPtr obj, int id);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setHasSelfCollision(IntPtr obj, bool hasSelfCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setJointPos(IntPtr obj, int i, float q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setJointVel(IntPtr obj, int i, float qdot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setLinearDamping(IntPtr obj, float damp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setMaxAppliedImpulse(IntPtr obj, float maxImp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setNumLinks(IntPtr obj, int numLinks);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupPrismatic(IntPtr obj, int i, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rot_parent_to_this, [In] ref Vector3 joint_axis, [In] ref Vector3 r_vector_when_q_zero);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupPrismatic2(IntPtr obj, int i, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion rot_parent_to_this, [In] ref Vector3 joint_axis, [In] ref Vector3 r_vector_when_q_zero, bool disableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupRevolute(IntPtr obj, int i, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion zero_rot_parent_to_this, [In] ref Vector3 joint_axis, [In] ref Vector3 parent_axis_position, [In] ref Vector3 my_axis_position);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setupRevolute2(IntPtr obj, int i, float mass, [In] ref Vector3 inertia, int parent, [In] ref Quaternion zero_rot_parent_to_this, [In] ref Vector3 joint_axis, [In] ref Vector3 parent_axis_position, [In] ref Vector3 my_axis_position, bool disableParentCollision);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setUseGyroTerm(IntPtr obj, bool useGyro);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_setWorldToBaseRot(IntPtr obj, [In] ref Quaternion rot);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_stepPositions(IntPtr obj, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_stepVelocities(IntPtr obj, float dt, IntPtr scratch_r, IntPtr scratch_v, IntPtr scratch_m);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_wakeUp(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_worldDirToLocal(IntPtr obj, int i, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btMultiBody_worldPosToLocal(IntPtr obj, int i, [In] ref Vector3 vec, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBody_delete(IntPtr obj);
	}
}
