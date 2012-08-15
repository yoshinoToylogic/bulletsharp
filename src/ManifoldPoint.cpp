#include "StdAfx.h"

#include "CollisionObjectWrapper.h"
#include "ManifoldPoint.h"

ContactAdded^ ManifoldPoint::ContactAddedCallback::get()
{
	return ManifoldPoint::gContactAddedCallback;
}
void ManifoldPoint::ContactAddedCallback::set(ContactAdded^ value)
{
	ManifoldPoint::gContactAddedCallback = value;
	::gContactAddedCallback = (value != nullptr) ? ContactAddedCallbackWrapper::CustomMaterialCombinerCallback : 0;
}

ManifoldPoint::ManifoldPoint(btManifoldPoint* point)
{
	if (point)
		_unmanaged = point;
}

ManifoldPoint::ManifoldPoint()
{
	_unmanaged = new btManifoldPoint();
}

ManifoldPoint::ManifoldPoint(Vector3 pointA, Vector3 pointB, Vector3 normal, btScalar distance)
{
	VECTOR3_DEF(pointA);
	VECTOR3_DEF(pointB);
	VECTOR3_DEF(normal);

	_unmanaged = new btManifoldPoint(VECTOR3_USE(pointA), VECTOR3_USE(pointB), VECTOR3_USE(normal), distance);

	VECTOR3_DEL(pointA);
	VECTOR3_DEL(pointB);
	VECTOR3_DEL(normal);
}

btScalar ManifoldPoint::AppliedImpulse::get()
{
	return _unmanaged->getAppliedImpulse();
}
void ManifoldPoint::AppliedImpulse::set(btScalar value)
{
	_unmanaged->m_appliedImpulse = value;
}

btScalar ManifoldPoint::CombinedFriction::get()
{
	return _unmanaged->m_combinedFriction;
}
void ManifoldPoint::CombinedFriction::set(btScalar value)
{
	_unmanaged->m_combinedFriction = value;
}

btScalar ManifoldPoint::CombinedRestitution::get()
{
	return _unmanaged->m_combinedRestitution;
}
void ManifoldPoint::CombinedRestitution::set(btScalar value)
{
	_unmanaged->m_combinedRestitution = value;
}

btScalar ManifoldPoint::ContactCfm1::get()
{
	return _unmanaged->m_contactCFM1;
}
void ManifoldPoint::ContactCfm1::set(btScalar value)
{
	_unmanaged->m_contactCFM1 = value;
}

btScalar ManifoldPoint::ContactCfm2::get()
{
	return _unmanaged->m_contactCFM2;
}
void ManifoldPoint::ContactCfm2::set(btScalar value)
{
	_unmanaged->m_contactCFM2 = value;
}

btScalar ManifoldPoint::ContactMotion1::get()
{
	return _unmanaged->m_contactMotion1;
}
void ManifoldPoint::ContactMotion1::set(btScalar value)
{
	_unmanaged->m_contactMotion1 = value;
}

btScalar ManifoldPoint::ContactMotion2::get()
{
	return _unmanaged->m_contactMotion2;
}
void ManifoldPoint::ContactMotion2::set(btScalar value)
{
	_unmanaged->m_contactMotion2 = value;
}

btScalar ManifoldPoint::Distance::get()
{
	return _unmanaged->getDistance();
}
void ManifoldPoint::Distance::set(btScalar value)
{
	_unmanaged->setDistance(value);
}

btScalar ManifoldPoint::Distance1::get()
{
	return _unmanaged->m_distance1;
}
void ManifoldPoint::Distance1::set(btScalar value)
{
	_unmanaged->m_distance1 = value;
}

int ManifoldPoint::Index0::get()
{
	return _unmanaged->m_index0;
}
void ManifoldPoint::Index0::set(int value)
{
	_unmanaged->m_index0 = value;
}

int ManifoldPoint::Index1::get()
{
	return _unmanaged->m_index1;
}
void ManifoldPoint::Index1::set(int value)
{
	_unmanaged->m_index1 = value;
}

Vector3 ManifoldPoint::LateralFrictionDir1::get()
{
	return Math::BtVector3ToVector3(&_unmanaged->m_lateralFrictionDir1);
}
void ManifoldPoint::LateralFrictionDir1::set(Vector3 value)
{
	return Math::Vector3ToBtVector3(value, &_unmanaged->m_lateralFrictionDir1);
}

Vector3 ManifoldPoint::LateralFrictionDir2::get()
{
	return Math::BtVector3ToVector3(&_unmanaged->m_lateralFrictionDir2);
}
void ManifoldPoint::LateralFrictionDir2::set(Vector3 value)
{
	return Math::Vector3ToBtVector3(value, &_unmanaged->m_lateralFrictionDir2);
}

bool ManifoldPoint::LateralFrictionInitialized::get()
{
	return _unmanaged->m_lateralFrictionInitialized;
}
void ManifoldPoint::LateralFrictionInitialized::set(bool value)
{
	_unmanaged->m_lateralFrictionInitialized = value;
}

int ManifoldPoint::LifeTime::get()
{
	return _unmanaged->getLifeTime();
}
void ManifoldPoint::LifeTime::set(int value)
{
	_unmanaged->m_lifeTime = value;
}

Vector3 ManifoldPoint::LocalPointA::get()
{
	return Math::BtVector3ToVector3(&_unmanaged->m_localPointA);
}
void ManifoldPoint::LocalPointA::set(Vector3 value)
{
	return Math::Vector3ToBtVector3(value, &_unmanaged->m_localPointA);
}

Vector3 ManifoldPoint::LocalPointB::get()
{
	return Math::BtVector3ToVector3(&_unmanaged->m_localPointB);
}
void ManifoldPoint::LocalPointB::set(Vector3 value)
{
	return Math::Vector3ToBtVector3(value, &_unmanaged->m_localPointB);
}

Vector3 ManifoldPoint::NormalWorldOnB::get()
{
	return Math::BtVector3ToVector3(&_unmanaged->m_normalWorldOnB);
}
void ManifoldPoint::NormalWorldOnB::set(Vector3 value)
{
	return Math::Vector3ToBtVector3(value, &_unmanaged->m_normalWorldOnB);
}

int ManifoldPoint::PartId0::get()
{
	return _unmanaged->m_partId0;
}
void ManifoldPoint::PartId0::set(int value)
{
	_unmanaged->m_partId0 = value;
}

int ManifoldPoint::PartId1::get()
{
	return _unmanaged->m_partId1;
}
void ManifoldPoint::PartId1::set(int value)
{
	_unmanaged->m_partId1 = value;
}

Vector3 ManifoldPoint::PositionWorldOnA::get()
{
	return Math::BtVector3ToVector3(&_unmanaged->getPositionWorldOnA());
}
void ManifoldPoint::PositionWorldOnA::set(Vector3 value)
{
	return Math::Vector3ToBtVector3(value, &_unmanaged->m_positionWorldOnA);
}

Vector3 ManifoldPoint::PositionWorldOnB::get()
{
	return Math::BtVector3ToVector3(&_unmanaged->getPositionWorldOnB());
}
void ManifoldPoint::PositionWorldOnB::set(Vector3 value)
{
	return Math::Vector3ToBtVector3(value, &_unmanaged->m_positionWorldOnB);
}

Object^ ManifoldPoint::UserPersistentObject::get()
{
	return _userPersistentObject;
}
void ManifoldPoint::UserPersistentObject::set(Object^ value)
{
	_userPersistentObject = value;
}

btManifoldPoint* ManifoldPoint::UnmanagedPointer::get()
{
	return _unmanaged;
}
void ManifoldPoint::UnmanagedPointer::set(btManifoldPoint* value)
{
	_unmanaged = value;

	if (_unmanaged->m_userPersistentData == 0)
	{
		GCHandle handle = GCHandle::Alloc(this);
		void* obj = GCHandleToVoidPtr(handle);
		_unmanaged->m_userPersistentData = obj;
	}
}

bool ContactAddedCallbackWrapper::CustomMaterialCombinerCallback(btManifoldPoint& cp, const btCollisionObjectWrapper* colObj0Wrap,
	int partId0, int index0, const btCollisionObjectWrapper* colObj1Wrap, int partId1, int index1)
{
	return ManifoldPoint::gContactAddedCallback->Invoke(gcnew ManifoldPoint(&cp),
		gcnew CollisionObjectWrapper((btCollisionObjectWrapper*)colObj0Wrap), partId0, index0,
		gcnew CollisionObjectWrapper((btCollisionObjectWrapper*)colObj1Wrap), partId1, index1);
}
