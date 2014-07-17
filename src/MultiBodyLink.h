#pragma once

namespace BulletSharp
{
	ref class MultiBodyLinkCollider;

	public ref class MultiBodyLink
	{
	internal:
		btMultibodyLink* _native;
		MultiBodyLink(btMultibodyLink* native);

	public:
		void UpdateCache();

		property Vector3 AppliedForce
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property Vector3 AppliedTorque
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property Vector3 AxisBottom
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property Vector3 AxisTop
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property Quaternion CachedRotParentToThis
		{
			Quaternion get();
			void set(Quaternion value);
		}

		property Vector3 CachedRVector
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property MultiBodyLinkCollider^ Collider
		{
			MultiBodyLinkCollider^ get();
			void set(MultiBodyLinkCollider^ value);
		}

		property Vector3 DVector
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property Vector3 EVector
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property int Flags
		{
			int get();
			void set(int value);
		}

		property Vector3 Inertia
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property bool IsRevolute
		{
			bool get();
			void set(bool value);
		}

		property btScalar JointPos
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar JointTorque
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar Mass
		{
			btScalar get();
			void set(btScalar value);
		}

		property int Parent
		{
			int get();
			void set(int value);
		}

		property Quaternion ZeroRotParentToThis
		{
			Quaternion get();
			void set(Quaternion value);
		}
	};
};
