#include "btGeneric6DofConstraint_wrap.h"

btGeneric6DofConstraint* btGeneric6DofConstraint_new(btRigidBody* rbA, btRigidBody* rbB, btScalar* frameInA, btScalar* frameInB, bool useLinearReferenceFrameA)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return new btGeneric6DofConstraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB), useLinearReferenceFrameA);
}

btGeneric6DofConstraint* btGeneric6DofConstraint_new2(btRigidBody* rbA, btScalar* frameInA, bool useLinearReferenceFrameA)
{
	TRANSFORM_CONV(frameInA);
	return new btGeneric6DofConstraint(*rbA, TRANSFORM_USE(frameInA), useLinearReferenceFrameA);
}

void btGeneric6DofConstraint_setLimit(btGeneric6DofConstraint* obj, int axis, float lo, float hi)
{
	obj->setLimit(axis, lo, hi);
}

bool btGeneric6DofConstraint_isLimited(btGeneric6DofConstraint* obj, int limitIndex)
{
	return obj->isLimited(limitIndex);
}

void btGeneric6DofConstraint_setAngularLowerLimit(btGeneric6DofConstraint* obj, btScalar* angularLower)
{
	VECTOR3_CONV(angularLower);
	obj->setAngularLowerLimit(VECTOR3_USE(angularLower));
}

void btGeneric6DofConstraint_setAngularUpperLimit(btGeneric6DofConstraint* obj, btScalar* angularUpper)
{
	VECTOR3_CONV(angularUpper);
	obj->setAngularUpperLimit(VECTOR3_USE(angularUpper));
}

void btGeneric6DofConstraint_setLinearLowerLimit(btGeneric6DofConstraint* obj, btScalar* linearLower)
{
	VECTOR3_CONV(linearLower);
	obj->setLinearLowerLimit(VECTOR3_USE(linearLower));
}

void btGeneric6DofConstraint_setLinearUpperLimit(btGeneric6DofConstraint* obj, btScalar* linearUpper)
{
	VECTOR3_CONV(linearUpper);
	obj->setLinearUpperLimit(VECTOR3_USE(linearUpper));
}
