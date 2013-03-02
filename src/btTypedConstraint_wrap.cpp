#include "btTypedConstraint_wrap.h"

btTypedConstraint::btConstraintInfo1* btTypedConstraint_btConstraintInfo1_new(int numConstraintRows, int nub)
{
	return new btTypedConstraint::btConstraintInfo1();
}

void btTypedConstraint_BtConstraintInfo1_delete(btTypedConstraint::btConstraintInfo1* obj)
{
	delete obj;
}

btTypedConstraint::btConstraintInfo2* btTypedConstraint_btConstraintInfo2_new()
{
	return new btTypedConstraint::btConstraintInfo2();
}

void btTypedConstraint_BtConstraintInfo2_delete(btTypedConstraint::btConstraintInfo2* obj)
{
	delete obj;
}


void btTypedConstraint_delete(btTypedConstraint* obj)
{
	delete obj;
}

int btTypedConstraint_calculateSerializeBufferSize(btTypedConstraint* obj)
{
	return obj->calculateSerializeBufferSize();
}

void btTypedConstraint_enableFeedback(btTypedConstraint* obj, bool needsFeedback)
{
	obj->enableFeedback(needsFeedback);
}

btScalar btTypedConstraint_getAppliedImpulse(btTypedConstraint* obj)
{
	return obj->getAppliedImpulse();
}

btScalar btTypedConstraint_getBreakingImpulseThreshold(btTypedConstraint* obj)
{
	return obj->getBreakingImpulseThreshold();
}

int btTypedConstraint_getConstraintType(btTypedConstraint* obj)
{
	return obj->getConstraintType();
}

btScalar btTypedConstraint_getDbgDrawSize(btTypedConstraint* obj)
{
	return obj->getDbgDrawSize();
}

btRigidBody* btTypedConstraint_getFixedBody(btTypedConstraint* obj)
{
	return &obj->getFixedBody();
}

void btTypedConstraint_getInfo1(btTypedConstraint* obj, btTypedConstraint::btConstraintInfo1* info)
{
	obj->getInfo1(info);
}

void btTypedConstraint_getInfo2(btTypedConstraint* obj, btTypedConstraint::btConstraintInfo2* info)
{
	obj->getInfo2(info);
}

btJointFeedback* btTypedConstraint_getJointFeedback(btTypedConstraint* obj)
{
	return obj->getJointFeedback();
}

int btTypedConstraint_getOverrideNumSolverIterations(btTypedConstraint* obj)
{
	return obj->getOverrideNumSolverIterations();
}

btScalar btTypedConstraint_getParam(btTypedConstraint* obj, int num, int axis)
{
	return obj->getParam(num, axis);
}

btScalar btTypedConstraint_getParam2(btTypedConstraint* obj, int num)
{
	return obj->getParam(num);
}

btRigidBody* btTypedConstraint_getRigidBodyA(btTypedConstraint* obj)
{
	return &obj->getRigidBodyA();
}

btRigidBody* btTypedConstraint_getRigidBodyB(btTypedConstraint* obj)
{
	return &obj->getRigidBodyB();
}

int btTypedConstraint_getUid(btTypedConstraint* obj)
{
	return obj->getUid();
}

int btTypedConstraint_getUserConstraintId(btTypedConstraint* obj)
{
	return obj->getUserConstraintId();
}

void* btTypedConstraint_getUserConstraintPtr(btTypedConstraint* obj)
{
	return obj->getUserConstraintPtr();
}

int btTypedConstraint_getUserConstraintType(btTypedConstraint* obj)
{
	return obj->getUserConstraintType();
}

bool btTypedConstraint_isEnabled(btTypedConstraint* obj)
{
	return obj->isEnabled();
}

bool btTypedConstraint_needsFeedback(btTypedConstraint* obj)
{
	return obj->needsFeedback();
}

void btTypedConstraint_setBreakingImpulseThreshold(btTypedConstraint* obj, btScalar threshold)
{
	obj->setBreakingImpulseThreshold(threshold);
}

void btTypedConstraint_setDbgDrawSize(btTypedConstraint* obj, btScalar dbgDrawSize)
{
	obj->setDbgDrawSize(dbgDrawSize);
}

void btTypedConstraint_setEnabled(btTypedConstraint* obj, bool enabled)
{
	obj->setEnabled(enabled);
}

void btTypedConstraint_setJointFeedback(btTypedConstraint* obj, btJointFeedback* jointFeedback)
{
	obj->setJointFeedback(jointFeedback);
}

void btTypedConstraint_setOverrideNumSolverIterations(btTypedConstraint* obj, int overideNumIterations)
{
	obj->setOverrideNumSolverIterations(overideNumIterations);
}

void btTypedConstraint_setParam(btTypedConstraint* obj, int num, btScalar value, int axis)
{
	obj->setParam(num, value, axis);
}

void btTypedConstraint_setParam2(btTypedConstraint* obj, int num, btScalar value)
{
	obj->setParam(num, value);
}

const char* btTypedConstraint_serialize(btTypedConstraint* obj, void* dataBuffer, btSerializer* serializer)
{
	return obj->serialize(dataBuffer, serializer);
}

void btTypedConstraint_setUserConstraintId(btTypedConstraint* obj, int uid)
{
	obj->setUserConstraintId(uid);
}

void btTypedConstraint_setUserConstraintPtr(btTypedConstraint* obj, void* ptr)
{
	obj->setUserConstraintPtr(ptr);
}

void btTypedConstraint_setUserConstraintType(btTypedConstraint* obj, int userConstraintType)
{
	obj->setUserConstraintType(userConstraintType);
}
