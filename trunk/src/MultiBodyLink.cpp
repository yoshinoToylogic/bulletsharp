#include "StdAfx.h"

#ifndef DISABLE_FEATHERSTONE

#include "MultiBodyLink.h"
#include "MultiBodyLinkCollider.h"

MultiBodyLink::MultiBodyLink(btMultibodyLink* native)
{
	_native = native;
}

void MultiBodyLink::UpdateCache()
{
	_native->updateCache();
}

Vector3 MultiBodyLink::AppliedForce::get()
{
	return Math::BtVector3ToVector3(&_native->applied_force);
}
void MultiBodyLink::AppliedForce::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->applied_force);
}

Vector3 MultiBodyLink::AppliedTorque::get()
{
	return Math::BtVector3ToVector3(&_native->applied_torque);
}
void MultiBodyLink::AppliedTorque::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->applied_torque);
}

Vector3 MultiBodyLink::AxisBottom::get()
{
	return Math::BtVector3ToVector3(&_native->axis_bottom);
}
void MultiBodyLink::AxisBottom::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->axis_bottom);
}

Vector3 MultiBodyLink::AxisTop::get()
{
	return Math::BtVector3ToVector3(&_native->axis_top);
}
void MultiBodyLink::AxisTop::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->axis_top);
}

Quaternion MultiBodyLink::CachedRotParentToThis::get()
{
	return Math::BtQuatToQuaternion(&_native->cached_rot_parent_to_this);
}
void MultiBodyLink::CachedRotParentToThis::set(Quaternion value)
{
	Math::QuaternionToBtQuat(value, &_native->cached_rot_parent_to_this);
}

Vector3 MultiBodyLink::CachedRVector::get()
{
	return Math::BtVector3ToVector3(&_native->cached_r_vector);
}
void MultiBodyLink::CachedRVector::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->cached_r_vector);
}

MultiBodyLinkCollider^ MultiBodyLink::Collider::get()
{
	return (MultiBodyLinkCollider^)CollisionObject::GetManaged(_native->m_collider);
}
void MultiBodyLink::Collider::set(MultiBodyLinkCollider^ value)
{
	_native->m_collider = (btMultiBodyLinkCollider*)value->_native;
}

Vector3 MultiBodyLink::DVector::get()
{
	return Math::BtVector3ToVector3(&_native->d_vector);
}
void MultiBodyLink::DVector::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->d_vector);
}

Vector3 MultiBodyLink::EVector::get()
{
	return Math::BtVector3ToVector3(&_native->e_vector);
}
void MultiBodyLink::EVector::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->e_vector);
}

int MultiBodyLink::Flags::get()
{
	return _native->m_flags;
}
void MultiBodyLink::Flags::set(int value)
{
	_native->m_flags = value;
}

Vector3 MultiBodyLink::Inertia::get()
{
	return Math::BtVector3ToVector3(&_native->inertia);
}
void MultiBodyLink::Inertia::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->inertia);
}

bool MultiBodyLink::IsRevolute::get()
{
	return _native->is_revolute;
}
void MultiBodyLink::IsRevolute::set(bool value)
{
	_native->is_revolute = value;
}

btScalar MultiBodyLink::JointPos::get()
{
	return _native->joint_pos;
}
void MultiBodyLink::JointPos::set(btScalar value)
{
	_native->joint_pos = value;
}

btScalar MultiBodyLink::JointTorque::get()
{
	return _native->joint_torque;
}
void MultiBodyLink::JointTorque::set(btScalar value)
{
	_native->joint_torque = value;
}

btScalar MultiBodyLink::Mass::get()
{
	return _native->mass;
}
void MultiBodyLink::Mass::set(btScalar value)
{
	_native->mass = value;
}

int MultiBodyLink::Parent::get()
{
	return _native->parent;
}
void MultiBodyLink::Parent::set(int value)
{
	_native->parent = value;
}

Quaternion MultiBodyLink::ZeroRotParentToThis::get()
{
	return Math::BtQuatToQuaternion(&_native->zero_rot_parent_to_this);
}
void MultiBodyLink::ZeroRotParentToThis::set(Quaternion value)
{
	Math::QuaternionToBtQuat(value, &_native->zero_rot_parent_to_this);
}

#endif
