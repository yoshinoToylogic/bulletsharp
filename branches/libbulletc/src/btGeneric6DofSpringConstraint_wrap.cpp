#include <BulletDynamics/ConstraintSolver/btGeneric6DofSpringConstraint.h>

#include "conversion.h"
#include "btGeneric6DofSpringConstraint_wrap.h"

btGeneric6DofSpringConstraint* btGeneric6DofSpringConstraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* frameInA, const btScalar* frameInB, bool useLinearReferenceFrameA)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return new btGeneric6DofSpringConstraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB), useLinearReferenceFrameA);
}

btGeneric6DofSpringConstraint* btGeneric6DofSpringConstraint_new2(btRigidBody* rbB, const btScalar* frameInB, bool useLinearReferenceFrameB)
{
	TRANSFORM_CONV(frameInB);
	return new btGeneric6DofSpringConstraint(*rbB, TRANSFORM_USE(frameInB), useLinearReferenceFrameB);
}

void btGeneric6DofSpringConstraint_enableSpring(btGeneric6DofSpringConstraint* obj, int index, bool onOff)
{
	obj->enableSpring(index, onOff);
}

void btGeneric6DofSpringConstraint_setDamping(btGeneric6DofSpringConstraint* obj, int index, btScalar damping)
{
	obj->setDamping(index, damping);
}

void btGeneric6DofSpringConstraint_setEquilibriumPoint(btGeneric6DofSpringConstraint* obj)
{
	obj->setEquilibriumPoint();
}

void btGeneric6DofSpringConstraint_setEquilibriumPoint2(btGeneric6DofSpringConstraint* obj, int index)
{
	obj->setEquilibriumPoint(index);
}

void btGeneric6DofSpringConstraint_setEquilibriumPoint3(btGeneric6DofSpringConstraint* obj, int index, btScalar val)
{
	obj->setEquilibriumPoint(index, val);
}

void btGeneric6DofSpringConstraint_setStiffness(btGeneric6DofSpringConstraint* obj, int index, btScalar stiffness)
{
	obj->setStiffness(index, stiffness);
}
