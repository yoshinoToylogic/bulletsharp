#include "StdAfx.h"

#ifndef DISABLE_CONSTRAINTS

#include "Hinge2Constraint.h"
#include "RigidBody.h"

#define Native static_cast<btHinge2Constraint*>(_native)

Hinge2Constraint::Hinge2Constraint(btHinge2Constraint* native)
	: Generic6DofSpringConstraint(native)
{
}

Hinge2Constraint::Hinge2Constraint(RigidBody^ rigidBodyA, RigidBody^ rigidBodyB, Vector3 anchor,
	Vector3 axis1, Vector3 axis2)
	: Generic6DofSpringConstraint(0)
{
	VECTOR3_DEF(anchor);
	VECTOR3_DEF(axis1);
	VECTOR3_DEF(axis2);
	UnmanagedPointer = new btHinge2Constraint(*(btRigidBody*)rigidBodyA->_native, *(btRigidBody*)rigidBodyB->_native,
		VECTOR3_USE(anchor), VECTOR3_USE(axis1), VECTOR3_USE(axis2));
	VECTOR3_DEL(anchor);
	VECTOR3_DEL(axis1);
	VECTOR3_DEL(axis2);
}

void Hinge2Constraint::SetLowerLimit(btScalar ang1min)
{
	Native->setLowerLimit(ang1min);
}

void Hinge2Constraint::SetUpperLimit(btScalar ang1max)
{
	Native->setUpperLimit(ang1max);
}

Vector3 Hinge2Constraint::Anchor::get()
{
	return Math::BtVector3ToVector3(&Native->getAnchor());
}

Vector3 Hinge2Constraint::Anchor2::get()
{
	return Math::BtVector3ToVector3(&Native->getAnchor2());
}

btScalar Hinge2Constraint::Angle1::get()
{
	return Native->getAngle1();
}

btScalar Hinge2Constraint::Angle2::get()
{
	return Native->getAngle2();
}

Vector3 Hinge2Constraint::Axis1::get()
{
	return Math::BtVector3ToVector3(&Native->getAxis1());
}

Vector3 Hinge2Constraint::Axis2::get()
{
	return Math::BtVector3ToVector3(&Native->getAxis2());
}

#endif
