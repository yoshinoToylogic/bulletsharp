#include "conversion.h"
#include "btGearConstraint_wrap.h"

btGearConstraint* btGearConstraint_new(btRigidBody* rbA, btRigidBody* rbB, btScalar* axisInA, btScalar* axisInB, btScalar ratio)
{
	VECTOR3_CONV(axisInA);
	VECTOR3_CONV(axisInB);
	return new btGearConstraint(*rbA, *rbB, VECTOR3_USE(axisInA), VECTOR3_USE(axisInB), ratio);
}

btGearConstraint* btGearConstraint_new2(btRigidBody* rbA, btRigidBody* rbB, btScalar* axisInA, btScalar* axisInB)
{
	VECTOR3_CONV(axisInA);
	VECTOR3_CONV(axisInB);
	return new btGearConstraint(*rbA, *rbB, VECTOR3_USE(axisInA), VECTOR3_USE(axisInB));
}
