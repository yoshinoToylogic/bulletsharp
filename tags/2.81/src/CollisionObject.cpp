#include "StdAfx.h"

#include "BroadphaseProxy.h"
#include "CollisionObject.h"
#include "CollisionShape.h"
#include "RigidBody.h"
#ifndef DISABLE_SERIALIZE
#include "Serializer.h"
#endif
#ifndef DISABLE_SOFTBODY
#include "SoftBody.h"
#endif

CollisionObject::CollisionObject(btCollisionObject* collisionObject)
{
	UnmanagedPointer = collisionObject;
}

CollisionObject::CollisionObject()
{
	UnmanagedPointer = new btCollisionObject();
}

CollisionObject::~CollisionObject()
{
	this->!CollisionObject();
}

CollisionObject::!CollisionObject()
{
	if (this->IsDisposed)
		return;
	
	OnDisposing(this, nullptr);

	if (_doesNotOwnObject == false)
	{
		void* userObj = _unmanaged->getUserPointer();
		if (userObj)
			VoidPtrToGCHandle(userObj).Free();
		delete _unmanaged;
	}
	_unmanaged = NULL;
	
	OnDisposed(this, nullptr);
}

bool CollisionObject::IsDisposed::get()
{
	return (_unmanaged == NULL);
}

void CollisionObject::Activate()
{
	_unmanaged->activate();
}

void CollisionObject::Activate(bool forceActivation)
{
	_unmanaged->activate(forceActivation);
}

bool CollisionObject::CheckCollideWith(CollisionObject^ collisionObject)
{
	return _unmanaged->checkCollideWith(collisionObject->_unmanaged);
}

void CollisionObject::ForceActivationState(BulletSharp::ActivationState newState)
{
	_unmanaged->forceActivationState((int)newState);
}

void CollisionObject::GetWorldTransform([Out] Matrix% outTransform)
{
	BtTransformToMatrixFast(_unmanaged->getWorldTransform(), outTransform);
}

bool CollisionObject::HasAnisotropicFriction()
{
	return _unmanaged->hasAnisotropicFriction();
}
bool CollisionObject::HasAnisotropicFriction(AnisotropicFrictionFlags frictionMode)
{
	return _unmanaged->hasAnisotropicFriction((int)frictionMode);
}

void CollisionObject::SetAnisotropicFriction(Vector3 anisotropicFriction, AnisotropicFrictionFlags frictionMode)
{
	VECTOR3_DEF(anisotropicFriction);
	_unmanaged->setAnisotropicFriction(VECTOR3_USE(anisotropicFriction), (int)frictionMode);
	VECTOR3_DEL(anisotropicFriction);
}
void CollisionObject::SetAnisotropicFriction(Vector3 anisotropicFriction)
{
	VECTOR3_DEF(anisotropicFriction);
	_unmanaged->setAnisotropicFriction(VECTOR3_USE(anisotropicFriction));
	VECTOR3_DEL(anisotropicFriction);
}

#ifndef DISABLE_SERIALIZE
int CollisionObject::CalculateSerializeBufferSize()
{
	return _unmanaged->calculateSerializeBufferSize();
}

String^ CollisionObject::Serialize(IntPtr dataBuffer, BulletSharp::Serializer^ serializer)
{
	const char* name = _unmanaged->serialize(dataBuffer.ToPointer(), serializer->UnmanagedPointer);
	return gcnew String(name);
}

void CollisionObject::SerializeSingleObject(BulletSharp::Serializer^ serializer)
{
	_unmanaged->serializeSingleObject(serializer->UnmanagedPointer);
}
#endif

CollisionObject^ CollisionObject::GetManaged(btCollisionObject* collisionObject)
{
	if (collisionObject == 0)
		return nullptr;

	void* userObj = collisionObject->getUserPointer();
	if (userObj)
		return static_cast<CollisionObject^>(VoidPtrToGCHandle(userObj).Target);

	// If we reach here, then collisionObject was created from within unmanaged code,
	// mark the wrapper object we create here so we don't try to destroy it later.
	btRigidBody* rigidBody = static_cast<btRigidBody*>(collisionObject);
	if (rigidBody)
	{
		RigidBody^ body = gcnew RigidBody(rigidBody);
		body->_doesNotOwnObject = true;
		return body;
	}

#ifndef DISABLE_SOFTBODY
	btSoftBody* softBody = static_cast<btSoftBody*>(collisionObject);
	if (softBody)
	{
		SoftBody::SoftBody^ body = gcnew SoftBody::SoftBody(softBody);
		body->_doesNotOwnObject = true;
		return body;
	}
#endif

	CollisionObject^ colObject = gcnew CollisionObject(collisionObject);
	colObject->_doesNotOwnObject = true;
	return colObject;
}

BulletSharp::ActivationState CollisionObject::ActivationState::get()
{
	return (BulletSharp::ActivationState)_unmanaged->getActivationState();
}
void CollisionObject::ActivationState::set(BulletSharp::ActivationState value)
{
	_unmanaged->setActivationState((int)value);
}

Vector3 CollisionObject::AnisotropicFriction::get()
{
	return Math::BtVector3ToVector3(&_unmanaged->getAnisotropicFriction());
}
void CollisionObject::AnisotropicFriction::set(Vector3 value)
{
	VECTOR3_DEF(value);
	_unmanaged->setAnisotropicFriction(VECTOR3_USE(value));
	VECTOR3_DEL(value);
}

BroadphaseProxy^ CollisionObject::BroadphaseHandle::get()
{
	btBroadphaseProxy* broadphaseHandle = _unmanaged->getBroadphaseHandle();
	ReturnCachedObject(BroadphaseProxy, _broadphaseHandle, broadphaseHandle);
}

btScalar CollisionObject::CcdMotionThreshold::get()
{
	return _unmanaged->getCcdMotionThreshold();
}
void CollisionObject::CcdMotionThreshold::set(btScalar value)
{
	_unmanaged->setCcdMotionThreshold(value);
}

btScalar CollisionObject::CcdSquareMotionThreshold::get()
{
	return _unmanaged->getCcdSquareMotionThreshold();
}

btScalar CollisionObject::CcdSweptSphereRadius::get()
{
	return _unmanaged->getCcdSweptSphereRadius();
}
void CollisionObject::CcdSweptSphereRadius::set(btScalar value)
{
	_unmanaged->setCcdSweptSphereRadius(value);
}

CollisionFlags CollisionObject::CollisionFlags::get()
{
	return (BulletSharp::CollisionFlags)_unmanaged->getCollisionFlags();
}
void CollisionObject::CollisionFlags::set(BulletSharp::CollisionFlags value)
{
	_unmanaged->setCollisionFlags((int)value);
}

CollisionShape^ CollisionObject::CollisionShape::get()
{
	return BulletSharp::CollisionShape::GetManaged(_unmanaged->getCollisionShape());
}
void CollisionObject::CollisionShape::set(BulletSharp::CollisionShape^ value)
{
	_unmanaged->setCollisionShape(value->_unmanaged);
}

int CollisionObject::CompanionId::get()
{
	return _unmanaged->getCompanionId();
}
void CollisionObject::CompanionId::set(int value)
{
	_unmanaged->setCompanionId(value);
}

btScalar CollisionObject::ContactProcessingThreshold::get()
{
	return _unmanaged->getContactProcessingThreshold();
}
void CollisionObject::ContactProcessingThreshold::set(btScalar value)
{
	_unmanaged->setContactProcessingThreshold(value);
}

btScalar CollisionObject::DeactivationTime::get()
{
	return _unmanaged->getDeactivationTime();
}
void CollisionObject::DeactivationTime::set(btScalar value)
{
	_unmanaged->setDeactivationTime(value);
}

btScalar CollisionObject::Friction::get()
{
	return _unmanaged->getFriction();
}
void CollisionObject::Friction::set(btScalar value)
{
	_unmanaged->setFriction(value);
}

bool CollisionObject::HasContactResponse::get()
{
	return _unmanaged->hasContactResponse();
}

btScalar CollisionObject::HitFraction::get()
{
	return _unmanaged->getHitFraction();
}
void CollisionObject::HitFraction::set(btScalar value)
{
	_unmanaged->setHitFraction(value);
}

Vector3 CollisionObject::InterpolationAngularVelocity::get()
{
	return Math::BtVector3ToVector3(&_unmanaged->getInterpolationAngularVelocity());
}
void CollisionObject::InterpolationAngularVelocity::set(Vector3 value)
{
	VECTOR3_DEF(value);
	_unmanaged->setInterpolationAngularVelocity(VECTOR3_USE(value));
	VECTOR3_DEL(value);
}

Vector3 CollisionObject::InterpolationLinearVelocity::get()
{
	return Math::BtVector3ToVector3(&_unmanaged->getInterpolationLinearVelocity());
}
void CollisionObject::InterpolationLinearVelocity::set(Vector3 value)
{
	VECTOR3_DEF(value);
	_unmanaged->setInterpolationLinearVelocity(VECTOR3_USE(value));
	VECTOR3_DEL(value);
}

Matrix CollisionObject::InterpolationWorldTransform::get()
{
	return Math::BtTransformToMatrix(&_unmanaged->getInterpolationWorldTransform());
}
void CollisionObject::InterpolationWorldTransform::set(Matrix value)
{
	btTransform* valueTemp = Math::MatrixToBtTransform(value);
	_unmanaged->setInterpolationWorldTransform(*valueTemp);
	delete valueTemp;
}

bool CollisionObject::IsActive::get()
{
	return _unmanaged->isActive();
}

bool CollisionObject::IsKinematicObject::get()
{
	return _unmanaged->isKinematicObject();
}

int CollisionObject::IslandTag::get()
{
	return _unmanaged->getIslandTag();
}
void CollisionObject::IslandTag::set(int value)
{
	_unmanaged->setIslandTag(value);
}

bool CollisionObject::IsStaticObject::get()
{
	return _unmanaged->isStaticObject();
}

bool CollisionObject::IsStaticOrKinematicObject::get()
{
	return _unmanaged->isStaticOrKinematicObject();
}

bool CollisionObject::MergesSimulationIslands::get()
{
	return _unmanaged->mergesSimulationIslands();
}

btScalar CollisionObject::Restitution::get()
{
	return _unmanaged->getRestitution();
}
void CollisionObject::Restitution::set(btScalar value)
{
	_unmanaged->setRestitution(value);
}

btScalar CollisionObject::RollingFriction::get()
{
	return _unmanaged->getRollingFriction();
}
void CollisionObject::RollingFriction::set(btScalar value)
{
	_unmanaged->setRollingFriction(value);
}

Object^ CollisionObject::UserObject::get()
{
	return _userObject;
}
void CollisionObject::UserObject::set(Object^ value)
{
	_userObject = value;
}

Matrix CollisionObject::WorldTransform::get()
{
	return Math::BtTransformToMatrix(&_unmanaged->getWorldTransform());
}
void CollisionObject::WorldTransform::set(Matrix value)
{
	btTransform* valueTemp = Math::MatrixToBtTransform(value);
	_unmanaged->setWorldTransform(*valueTemp);
	delete valueTemp;
}

btCollisionObject* CollisionObject::UnmanagedPointer::get()
{
	return _unmanaged;
}
void CollisionObject::UnmanagedPointer::set(btCollisionObject* value)
{
	_unmanaged = value;

	if (_unmanaged->getUserPointer() == 0)
	{
		GCHandle handle = GCHandle::Alloc(this);
		void* obj = GCHandleToVoidPtr(handle);
		_unmanaged->setUserPointer(obj);
	}
}