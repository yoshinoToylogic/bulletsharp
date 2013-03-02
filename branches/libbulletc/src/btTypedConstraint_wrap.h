#include "main.h"

extern "C"
{
	EXPORT btTypedConstraint::btConstraintInfo1* btTypedConstraint_btConstraintInfo1_new();
	EXPORT void btTypedConstraint_BtConstraintInfo1_delete(btTypedConstraint* obj);
	EXPORT btTypedConstraint::btConstraintInfo2* btTypedConstraint_btConstraintInfo2_new();
	EXPORT void btTypedConstraint_BtConstraintInfo2_delete(btTypedConstraint* obj);

	EXPORT void btTypedConstraint_delete(btTypedConstraint* obj);
	EXPORT int btTypedConstraint_calculateSerializeBufferSize(btTypedConstraint* obj);
	EXPORT void btTypedConstraint_enableFeedback(btTypedConstraint* obj, bool needsFeedback);
	EXPORT btScalar btTypedConstraint_getAppliedImpulse(btTypedConstraint* obj);
	EXPORT btScalar btTypedConstraint_getBreakingImpulseThreshold(btTypedConstraint* obj);
	EXPORT int btTypedConstraint_getConstraintType(btTypedConstraint* obj);
	EXPORT btScalar btTypedConstraint_getDbgDrawSize(btTypedConstraint* obj);
	EXPORT btRigidBody* btTypedConstraint_getFixedBody(btTypedConstraint* obj);
	EXPORT void btTypedConstraint_getInfo1(btTypedConstraint* obj, btTypedConstraint::btConstraintInfo1* info);
	EXPORT void btTypedConstraint_getInfo2(btTypedConstraint* obj, btTypedConstraint::btConstraintInfo2* info);
	EXPORT btJointFeedback* btTypedConstraint_getJointFeedback(btTypedConstraint* obj);
	EXPORT int btTypedConstraint_getOverrideNumSolverIterations(btTypedConstraint* obj);
	EXPORT btScalar btTypedConstraint_getParam(btTypedConstraint* obj, int num, int axis);
	EXPORT btScalar btTypedConstraint_getParam2(btTypedConstraint* obj, int num);
	EXPORT btRigidBody* btTypedConstraint_getRigidBodyA(btTypedConstraint* obj);
	EXPORT btRigidBody* btTypedConstraint_getRigidBodyB(btTypedConstraint* obj);
	EXPORT int btTypedConstraint_getUid(btTypedConstraint* obj);
	EXPORT int btTypedConstraint_getUserConstraintId(btTypedConstraint* obj);
	EXPORT void* btTypedConstraint_getUserConstraintPtr(btTypedConstraint* obj);
	EXPORT int btTypedConstraint_getUserConstraintType(btTypedConstraint* obj);
	EXPORT bool btTypedConstraint_isEnabled(btTypedConstraint* obj);
	EXPORT bool btTypedConstraint_needsFeedback(btTypedConstraint* obj);
	EXPORT void btTypedConstraint_setBreakingImpulseThreshold(btTypedConstraint* obj, btScalar threshold);
	EXPORT void btTypedConstraint_setDbgDrawSize(btTypedConstraint* obj, btScalar dbgDrawSize);
	EXPORT void btTypedConstraint_setEnabled(btTypedConstraint* obj, bool enabled);
	EXPORT void btTypedConstraint_setJointFeedback(btTypedConstraint* obj, btJointFeedback* jointFeedback);
	EXPORT void btTypedConstraint_setOverrideNumSolverIterations(btTypedConstraint* obj, int overideNumIterations);
	EXPORT void btTypedConstraint_setParam(btTypedConstraint* obj, int num, btScalar value, int axis);
	EXPORT void btTypedConstraint_setParam2(btTypedConstraint* obj, int num, btScalar value);
	EXPORT const char* btTypedConstraint_serialize(btTypedConstraint* obj, void* dataBuffer, btSerializer* serializer);
	EXPORT void btTypedConstraint_setUserConstraintId(btTypedConstraint* obj, int uid);
	EXPORT void btTypedConstraint_setUserConstraintPtr(btTypedConstraint* obj, void* ptr);
	EXPORT void btTypedConstraint_setUserConstraintType(btTypedConstraint* obj, int userConstraintType);
}
