#include "StdAfx.h"

#ifndef DISABLE_CONSTRAINTS

#include "Point2PointConstraint.h"
#include "RigidBody.h"

ConstraintSetting::ConstraintSetting()
{
	_setting = new btConstraintSetting();
}

ConstraintSetting::ConstraintSetting(btConstraintSetting* setting)
{
	_setting = setting;
}

btScalar ConstraintSetting::Damping::get()
{
	return _setting->m_damping;
}
void ConstraintSetting::Damping::set(btScalar value)
{
	_setting->m_impulseClamp = value;
}

btScalar ConstraintSetting::ImpulseClamp::get()
{
	return _setting->m_damping;
}
void ConstraintSetting::ImpulseClamp::set(btScalar value)
{
	_setting->m_impulseClamp = value;
}

btScalar ConstraintSetting::Tau::get()
{
	return _setting->m_tau;
}
void ConstraintSetting::Tau::set(btScalar value)
{
	_setting->m_tau = value;
}

btConstraintSetting* ConstraintSetting::UnmanagedPointer::get()
{
	return _setting;
}

void ConstraintSetting::UnmanagedPointer::set(btConstraintSetting* setting)
{
	_setting = setting;
}


Point2PointConstraint::Point2PointConstraint(btPoint2PointConstraint* constraint)
: TypedConstraint(constraint)
{
}

Point2PointConstraint::Point2PointConstraint(RigidBody^ rigidBodyA, RigidBody^ rigidBodyB, Vector3 pivotInA, Vector3 pivotInB)
: TypedConstraint(0)
{
	VECTOR3_DEF(pivotInA);
	VECTOR3_DEF(pivotInB);

	UnmanagedPointer = new btPoint2PointConstraint(
		*(btRigidBody*)rigidBodyA->_unmanaged, *(btRigidBody*)rigidBodyB->_unmanaged,
		VECTOR3_USE(pivotInA), VECTOR3_USE(pivotInB));

	VECTOR3_DEL(pivotInA);
	VECTOR3_DEL(pivotInB);
}

Point2PointConstraint::Point2PointConstraint(RigidBody^ rigidBodyA, Vector3 pivotInA)
: TypedConstraint(0)
{
	VECTOR3_DEF(pivotInA);
	UnmanagedPointer = new btPoint2PointConstraint(*(btRigidBody*)rigidBodyA->_unmanaged, VECTOR3_USE(pivotInA));
	VECTOR3_DEL(pivotInA);
}

btScalar Point2PointConstraint::GetParam(int num, int axis)
{
	return UnmanagedPointer->getParam(num, axis);
}

btScalar Point2PointConstraint::GetParam(int num)
{
	return UnmanagedPointer->getParam(num);
}

void Point2PointConstraint::SetParam(int num, btScalar value, int axis)
{
	UnmanagedPointer->setParam(num, value, axis);
}

void Point2PointConstraint::SetParam(int num, btScalar value)
{
	UnmanagedPointer->setParam(num, value);
}

Vector3 Point2PointConstraint::PivotInA::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getPivotInA());
}

void Point2PointConstraint::PivotInA::set(Vector3 value)
{
	VECTOR3_DEF(value);
	UnmanagedPointer->setPivotA(VECTOR3_USE(value));
	VECTOR3_DEL(value);
}

Vector3 Point2PointConstraint::PivotInB::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getPivotInB());
}

void Point2PointConstraint::PivotInB::set(Vector3 value)
{
	VECTOR3_DEF(value);
	UnmanagedPointer->setPivotB(VECTOR3_USE(value));
	VECTOR3_DEL(value);
}

ConstraintSetting^ Point2PointConstraint::Setting::get()
{
	return gcnew ConstraintSetting(&UnmanagedPointer->m_setting);
}

void Point2PointConstraint::Setting::set(ConstraintSetting^ setting)
{
	UnmanagedPointer->m_setting = *setting->UnmanagedPointer;
}

void Point2PointConstraint::UpdateRHS(btScalar timeStep)
{
	UnmanagedPointer->updateRHS(timeStep);
}

btPoint2PointConstraint* Point2PointConstraint::UnmanagedPointer::get()
{
	return (btPoint2PointConstraint*)TypedConstraint::UnmanagedPointer;
}

#endif
