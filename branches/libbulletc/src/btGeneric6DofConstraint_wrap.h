#include "main.h"

extern "C"
{
	EXPORT btGeneric6DofConstraint* btGeneric6DofConstraint_new(btRigidBody* rbA, btRigidBody* rbB, btScalar* frameInA, btScalar* frameInB, bool useLinearReferenceFrameA);
	EXPORT btGeneric6DofConstraint* btGeneric6DofConstraint_new2(btRigidBody* rbA, btScalar* frameInA, bool useLinearReferenceFrameB);
	EXPORT void btGeneric6DofConstraint_setLimits(btGeneric6DofConstraint* obj, int axis, float lo, float hi);
	EXPORT bool btGeneric6DofConstraint_isLimited(btGeneric6DofConstraint* obj, int limitIndex);
	EXPORT void btGeneric6DofConstraint_setAngularLowerLimit(btGeneric6DofConstraint* obj, btScalar* angularLower);
	EXPORT void btGeneric6DofConstraint_setAngularUpperLimit(btGeneric6DofConstraint* obj, btScalar* angularUpper);
	EXPORT void btGeneric6DofConstraint_setLinearLowerLimit(btGeneric6DofConstraint* obj, btScalar* linearLower);
	EXPORT void btGeneric6DofConstraint_setLinearUpperLimit(btGeneric6DofConstraint* obj, btScalar* linearUpper);
}
