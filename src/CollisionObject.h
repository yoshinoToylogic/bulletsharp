#pragma once

#include "IDisposable.h"

namespace BulletSharp
{
	ref class BroadphaseProxy;
	ref class CollisionShape;
	ref class Serializer;

	public ref class CollisionObject : BulletSharp::IDisposable
	{
	public:
		virtual event EventHandler^ OnDisposing;
		virtual event EventHandler^ OnDisposed;

	internal:
		btCollisionObject* _native;

	private:
		bool _doesNotOwnObject;
		
		BroadphaseProxy^ _broadphaseHandle;
		Object^ _userObject;

	internal:
		CollisionObject(btCollisionObject* collisionObject);

	public:
		!CollisionObject();
	protected:
		~CollisionObject();

	public:
		CollisionObject();

		property bool IsDisposed
		{
			virtual bool get();
		}

		void Activate();
		void Activate(bool forceActivation);
		bool CheckCollideWith(CollisionObject^ collisionObject);
		void ForceActivationState(BulletSharp::ActivationState newState);
		void GetWorldTransform([Out] Matrix% transform);
		bool HasAnisotropicFriction();
		bool HasAnisotropicFriction(AnisotropicFrictionFlags frictionMode);
		void SetAnisotropicFriction(Vector3 anisotropicFriction, AnisotropicFrictionFlags frictionMode);
		void SetAnisotropicFriction(Vector3 anisotropicFriction);

#ifndef DISABLE_SERIALIZE
		int CalculateSerializeBufferSize();
		String^ Serialize(IntPtr dataBuffer, Serializer^ serializer);
		void SerializeSingleObject(Serializer^ serializer);
#endif

	internal:
		static CollisionObject^ GetManaged(btCollisionObject* collisionObject);

	public:
		property BulletSharp::ActivationState ActivationState
		{
			BulletSharp::ActivationState get();
			void set(BulletSharp::ActivationState value);
		}

		property Vector3 AnisotropicFriction
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property BroadphaseProxy^ BroadphaseHandle
		{
			BroadphaseProxy^ get();
		}

		property btScalar CcdMotionThreshold
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar CcdSquareMotionThreshold
		{
			btScalar get();
		}

		property btScalar CcdSweptSphereRadius
		{
			btScalar get();
			void set(btScalar value);
		}

		property CollisionFlags CollisionFlags
		{
			BulletSharp::CollisionFlags get();
			void set(BulletSharp::CollisionFlags value);
		}

		property BulletSharp::CollisionShape^ CollisionShape
		{
			BulletSharp::CollisionShape^ get();
			void set(BulletSharp::CollisionShape^ value);
		}

		property int CompanionId
		{
			int get();
			void set(int value);
		}

		property btScalar ContactProcessingThreshold
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar DeactivationTime
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar Friction
		{
			btScalar get();
			void set(btScalar value);
		}

		property bool HasContactResponse
		{
			bool get();
		}

		property btScalar HitFraction
		{
			btScalar get();
			void set(btScalar value);
		}

		property Vector3 InterpolationAngularVelocity
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property Vector3 InterpolationLinearVelocity
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property Matrix InterpolationWorldTransform
		{
			Matrix get();
			void set(Matrix value);
		}

		property bool IsActive
		{
			bool get();
		}

		property bool IsKinematicObject
		{
			bool get();
		}

		property int IslandTag
		{
			int get();
			void set(int value);
		}

		property bool IsStaticObject
		{
			bool get();
		}

		property bool IsStaticOrKinematicObject
		{
			bool get();
		}

		property bool MergesSimulationIslands
		{
			bool get();
		}

		property btScalar Restitution
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar RollingFriction
		{
			btScalar get();
			void set(btScalar value);
		}

		// UserPointer implemented as UserObject
		property Object^ UserObject
		{
			Object^ get();
			void set(Object^ value);
		}

		property Matrix WorldTransform
		{
			Matrix get();
			void set(Matrix value);
		}

	internal:
		property btCollisionObject* UnmanagedPointer
		{
			virtual btCollisionObject* get();
			void set(btCollisionObject* value);
		}
	};
};
