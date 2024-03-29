#include "StdAfx.h"

#ifndef DISABLE_VEHICLE

#include "RigidBody.h"
#include "VehicleRaycaster.h"

VehicleRaycasterResult::~VehicleRaycasterResult()
{
	this->!VehicleRaycasterResult();
}

VehicleRaycasterResult::!VehicleRaycasterResult()
{
	ALIGNED_FREE(_native);
	_native = NULL;
}

VehicleRaycasterResult::VehicleRaycasterResult()
{
	_native = ALIGNED_NEW(btVehicleRaycaster::btVehicleRaycasterResult) ();
}

btScalar VehicleRaycasterResult::DistFraction::get()
{
	return _native->m_distFraction;
}
void VehicleRaycasterResult::DistFraction::set(btScalar value)
{
	_native->m_distFraction = value;
}

Vector3 VehicleRaycasterResult::HitNormalInWorld::get()
{
	return Math::BtVector3ToVector3(&_native->m_hitNormalInWorld);
}
void VehicleRaycasterResult::HitNormalInWorld::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->m_hitNormalInWorld);
}

Vector3 VehicleRaycasterResult::HitPointInWorld::get()
{
	return Math::BtVector3ToVector3(&_native->m_hitPointInWorld);
}
void VehicleRaycasterResult::HitPointInWorld::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->m_hitPointInWorld);
}


VehicleRaycaster::VehicleRaycaster(btVehicleRaycaster* native)
{
	_native = native;
}

VehicleRaycaster::~VehicleRaycaster()
{
	this->!VehicleRaycaster();
}

VehicleRaycaster::!VehicleRaycaster()
{
	delete _native;
	_native = NULL;
}

Object^ VehicleRaycaster::CastRay(Vector3 from, Vector3 to, VehicleRaycasterResult^ result)
{
	VECTOR3_CONV(from);
	VECTOR3_CONV(to);
	void* ret = _native->castRay(VECTOR3_USE(from), VECTOR3_USE(to), *result->_native);
	VECTOR3_DEL(from);
	VECTOR3_DEL(to);
	return CollisionObject::GetManaged((btRigidBody*)ret);
}

#endif
