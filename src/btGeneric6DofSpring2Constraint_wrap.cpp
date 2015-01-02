#include <BulletDynamics/ConstraintSolver/btGeneric6DofSpring2Constraint.h>

#include "conversion.h"
#include "btGeneric6DofSpring2Constraint_wrap.h"

btRotationalLimitMotor2* btRotationalLimitMotor2_new()
{
	return new btRotationalLimitMotor2();
}

btRotationalLimitMotor2* btRotationalLimitMotor2_new2(const btRotationalLimitMotor2* limot)
{
	return new btRotationalLimitMotor2(*limot);
}

btScalar btRotationalLimitMotor2_getBounce(btRotationalLimitMotor2* obj)
{
	return obj->m_bounce;
}

int btRotationalLimitMotor2_getCurrentLimit(btRotationalLimitMotor2* obj)
{
	return obj->m_currentLimit;
}

btScalar btRotationalLimitMotor2_getCurrentLimitError(btRotationalLimitMotor2* obj)
{
	return obj->m_currentLimitError;
}

btScalar btRotationalLimitMotor2_getCurrentLimitErrorHi(btRotationalLimitMotor2* obj)
{
	return obj->m_currentLimitErrorHi;
}

btScalar btRotationalLimitMotor2_getCurrentPosition(btRotationalLimitMotor2* obj)
{
	return obj->m_currentPosition;
}

bool btRotationalLimitMotor2_getEnableMotor(btRotationalLimitMotor2* obj)
{
	return obj->m_enableMotor;
}

bool btRotationalLimitMotor2_getEnableSpring(btRotationalLimitMotor2* obj)
{
	return obj->m_enableSpring;
}

btScalar btRotationalLimitMotor2_getEquilibriumPoint(btRotationalLimitMotor2* obj)
{
	return obj->m_equilibriumPoint;
}

btScalar btRotationalLimitMotor2_getHiLimit(btRotationalLimitMotor2* obj)
{
	return obj->m_hiLimit;
}

btScalar btRotationalLimitMotor2_getLoLimit(btRotationalLimitMotor2* obj)
{
	return obj->m_loLimit;
}

btScalar btRotationalLimitMotor2_getMaxMotorForce(btRotationalLimitMotor2* obj)
{
	return obj->m_maxMotorForce;
}

btScalar btRotationalLimitMotor2_getMotorCFM(btRotationalLimitMotor2* obj)
{
	return obj->m_motorCFM;
}

btScalar btRotationalLimitMotor2_getMotorERP(btRotationalLimitMotor2* obj)
{
	return obj->m_motorERP;
}

bool btRotationalLimitMotor2_getServoMotor(btRotationalLimitMotor2* obj)
{
	return obj->m_servoMotor;
}

btScalar btRotationalLimitMotor2_getServoTarget(btRotationalLimitMotor2* obj)
{
	return obj->m_servoTarget;
}

btScalar btRotationalLimitMotor2_getSpringDamping(btRotationalLimitMotor2* obj)
{
	return obj->m_springDamping;
}

btScalar btRotationalLimitMotor2_getSpringStiffness(btRotationalLimitMotor2* obj)
{
	return obj->m_springStiffness;
}

btScalar btRotationalLimitMotor2_getStopCFM(btRotationalLimitMotor2* obj)
{
	return obj->m_stopCFM;
}

btScalar btRotationalLimitMotor2_getStopERP(btRotationalLimitMotor2* obj)
{
	return obj->m_stopERP;
}

btScalar btRotationalLimitMotor2_getTargetVelocity(btRotationalLimitMotor2* obj)
{
	return obj->m_targetVelocity;
}

bool btRotationalLimitMotor2_isLimited(btRotationalLimitMotor2* obj)
{
	return obj->isLimited();
}

void btRotationalLimitMotor2_setBounce(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_bounce = value;
}

void btRotationalLimitMotor2_setCurrentLimit(btRotationalLimitMotor2* obj, int value)
{
	obj->m_currentLimit = value;
}

void btRotationalLimitMotor2_setCurrentLimitError(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_currentLimitError = value;
}

void btRotationalLimitMotor2_setCurrentLimitErrorHi(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_currentLimitErrorHi = value;
}

void btRotationalLimitMotor2_setCurrentPosition(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_currentPosition = value;
}

void btRotationalLimitMotor2_setEnableMotor(btRotationalLimitMotor2* obj, bool value)
{
	obj->m_enableMotor = value;
}

void btRotationalLimitMotor2_setEnableSpring(btRotationalLimitMotor2* obj, bool value)
{
	obj->m_enableSpring = value;
}

void btRotationalLimitMotor2_setEquilibriumPoint(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_equilibriumPoint = value;
}

void btRotationalLimitMotor2_setHiLimit(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_hiLimit = value;
}

void btRotationalLimitMotor2_setLoLimit(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_loLimit = value;
}

void btRotationalLimitMotor2_setMaxMotorForce(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_maxMotorForce = value;
}

void btRotationalLimitMotor2_setMotorCFM(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_motorCFM = value;
}

void btRotationalLimitMotor2_setMotorERP(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_motorERP = value;
}

void btRotationalLimitMotor2_setServoMotor(btRotationalLimitMotor2* obj, bool value)
{
	obj->m_servoMotor = value;
}

void btRotationalLimitMotor2_setServoTarget(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_servoTarget = value;
}

void btRotationalLimitMotor2_setSpringDamping(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_springDamping = value;
}

void btRotationalLimitMotor2_setSpringStiffness(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_springStiffness = value;
}

void btRotationalLimitMotor2_setStopCFM(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_stopCFM = value;
}

void btRotationalLimitMotor2_setStopERP(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_stopERP = value;
}

void btRotationalLimitMotor2_setTargetVelocity(btRotationalLimitMotor2* obj, btScalar value)
{
	obj->m_targetVelocity = value;
}

void btRotationalLimitMotor2_testLimitValue(btRotationalLimitMotor2* obj, btScalar test_value)
{
	obj->testLimitValue(test_value);
}

void btRotationalLimitMotor2_delete(btRotationalLimitMotor2* obj)
{
	delete obj;
}


btTranslationalLimitMotor2* btTranslationalLimitMotor2_new()
{
	return new btTranslationalLimitMotor2();
}

btTranslationalLimitMotor2* btTranslationalLimitMotor2_new2(const btTranslationalLimitMotor2* other)
{
	return new btTranslationalLimitMotor2(*other);
}

void btTranslationalLimitMotor2_getBounce(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_bounce, value);
}

int* btTranslationalLimitMotor2_getCurrentLimit(btTranslationalLimitMotor2* obj)
{
	return obj->m_currentLimit;
}

void btTranslationalLimitMotor2_getCurrentLimitError(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_currentLimitError, value);
}

void btTranslationalLimitMotor2_getCurrentLimitErrorHi(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_currentLimitErrorHi, value);
}

void btTranslationalLimitMotor2_getCurrentLinearDiff(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_currentLinearDiff, value);
}

bool* btTranslationalLimitMotor2_getEnableMotor(btTranslationalLimitMotor2* obj)
{
	return obj->m_enableMotor;
}

bool* btTranslationalLimitMotor2_getEnableSpring(btTranslationalLimitMotor2* obj)
{
	return obj->m_enableSpring;
}

void btTranslationalLimitMotor2_getEquilibriumPoint(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_equilibriumPoint, value);
}

void btTranslationalLimitMotor2_getLowerLimit(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_lowerLimit, value);
}

void btTranslationalLimitMotor2_getMaxMotorForce(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_maxMotorForce, value);
}

void btTranslationalLimitMotor2_getMotorCFM(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_motorCFM, value);
}

void btTranslationalLimitMotor2_getMotorERP(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_motorERP, value);
}

bool* btTranslationalLimitMotor2_getServoMotor(btTranslationalLimitMotor2* obj)
{
	return obj->m_servoMotor;
}

void btTranslationalLimitMotor2_getServoTarget(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_servoTarget, value);
}

void btTranslationalLimitMotor2_getSpringDamping(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_springDamping, value);
}

void btTranslationalLimitMotor2_getSpringStiffness(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_springStiffness, value);
}

void btTranslationalLimitMotor2_getStopCFM(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_stopCFM, value);
}

void btTranslationalLimitMotor2_getStopERP(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_stopERP, value);
}

void btTranslationalLimitMotor2_getTargetVelocity(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_targetVelocity, value);
}

void btTranslationalLimitMotor2_getUpperLimit(btTranslationalLimitMotor2* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_upperLimit, value);
}

bool btTranslationalLimitMotor2_isLimited(btTranslationalLimitMotor2* obj, int limitIndex)
{
	return obj->isLimited(limitIndex);
}

void btTranslationalLimitMotor2_setBounce(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_bounce);
}

void btTranslationalLimitMotor2_setCurrentLimitError(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_currentLimitError);
}

void btTranslationalLimitMotor2_setCurrentLimitErrorHi(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_currentLimitErrorHi);
}

void btTranslationalLimitMotor2_setCurrentLinearDiff(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_currentLinearDiff);
}

void btTranslationalLimitMotor2_setEquilibriumPoint(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_equilibriumPoint);
}

void btTranslationalLimitMotor2_setLowerLimit(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_lowerLimit);
}

void btTranslationalLimitMotor2_setMaxMotorForce(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_maxMotorForce);
}

void btTranslationalLimitMotor2_setMotorCFM(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_motorCFM);
}

void btTranslationalLimitMotor2_setMotorERP(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_motorERP);
}

void btTranslationalLimitMotor2_setServoTarget(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_servoTarget);
}

void btTranslationalLimitMotor2_setSpringDamping(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_springDamping);
}

void btTranslationalLimitMotor2_setSpringStiffness(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_springStiffness);
}

void btTranslationalLimitMotor2_setStopCFM(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_stopCFM);
}

void btTranslationalLimitMotor2_setStopERP(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_stopERP);
}

void btTranslationalLimitMotor2_setTargetVelocity(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_targetVelocity);
}

void btTranslationalLimitMotor2_setUpperLimit(btTranslationalLimitMotor2* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->m_upperLimit);
}

void btTranslationalLimitMotor2_testLimitValue(btTranslationalLimitMotor2* obj, int limitIndex, btScalar test_value)
{
	obj->testLimitValue(limitIndex, test_value);
}

void btTranslationalLimitMotor2_delete(btTranslationalLimitMotor2* obj)
{
	delete obj;
}


btGeneric6DofSpring2Constraint* btGeneric6DofSpring2Constraint_new(btRigidBody* rbA, btRigidBody* rbB, const btScalar* frameInA, const btScalar* frameInB)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return new btGeneric6DofSpring2Constraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB));
}

btGeneric6DofSpring2Constraint* btGeneric6DofSpring2Constraint_new2(btRigidBody* rbA, btRigidBody* rbB, const btScalar* frameInA, const btScalar* frameInB, RotateOrder rotOrder)
{
	TRANSFORM_CONV(frameInA);
	TRANSFORM_CONV(frameInB);
	return new btGeneric6DofSpring2Constraint(*rbA, *rbB, TRANSFORM_USE(frameInA), TRANSFORM_USE(frameInB), rotOrder);
}

btGeneric6DofSpring2Constraint* btGeneric6DofSpring2Constraint_new3(btRigidBody* rbB, const btScalar* frameInB)
{
	TRANSFORM_CONV(frameInB);
	return new btGeneric6DofSpring2Constraint(*rbB, TRANSFORM_USE(frameInB));
}

btGeneric6DofSpring2Constraint* btGeneric6DofSpring2Constraint_new4(btRigidBody* rbB, const btScalar* frameInB, RotateOrder rotOrder)
{
	TRANSFORM_CONV(frameInB);
	return new btGeneric6DofSpring2Constraint(*rbB, TRANSFORM_USE(frameInB), rotOrder);
}

void btGeneric6DofSpring2Constraint_calculateTransforms(btGeneric6DofSpring2Constraint* obj, const btScalar* transA, const btScalar* transB)
{
	TRANSFORM_CONV(transA);
	TRANSFORM_CONV(transB);
	obj->calculateTransforms(TRANSFORM_USE(transA), TRANSFORM_USE(transB));
}

void btGeneric6DofSpring2Constraint_calculateTransforms2(btGeneric6DofSpring2Constraint* obj)
{
	obj->calculateTransforms();
}

void btGeneric6DofSpring2Constraint_enableMotor(btGeneric6DofSpring2Constraint* obj, int index, bool onOff)
{
	obj->enableMotor(index, onOff);
}

void btGeneric6DofSpring2Constraint_enableSpring(btGeneric6DofSpring2Constraint* obj, int index, bool onOff)
{
	obj->enableSpring(index, onOff);
}

btScalar btGeneric6DofSpring2Constraint_getAngle(btGeneric6DofSpring2Constraint* obj, int axis_index)
{
	return obj->getAngle(axis_index);
}

void btGeneric6DofSpring2Constraint_getAngularLowerLimit(btGeneric6DofSpring2Constraint* obj, btScalar* angularLower)
{
	VECTOR3_CONV(angularLower);
	obj->getAngularLowerLimit(VECTOR3_USE(angularLower));
	VECTOR3_DEF_OUT(angularLower);
}

void btGeneric6DofSpring2Constraint_getAngularLowerLimitReversed(btGeneric6DofSpring2Constraint* obj, btScalar* angularLower)
{
	VECTOR3_CONV(angularLower);
	obj->getAngularLowerLimitReversed(VECTOR3_USE(angularLower));
	VECTOR3_DEF_OUT(angularLower);
}

void btGeneric6DofSpring2Constraint_getAngularUpperLimit(btGeneric6DofSpring2Constraint* obj, btScalar* angularUpper)
{
	VECTOR3_CONV(angularUpper);
	obj->getAngularUpperLimit(VECTOR3_USE(angularUpper));
	VECTOR3_DEF_OUT(angularUpper);
}

void btGeneric6DofSpring2Constraint_getAngularUpperLimitReversed(btGeneric6DofSpring2Constraint* obj, btScalar* angularUpper)
{
	VECTOR3_CONV(angularUpper);
	obj->getAngularUpperLimitReversed(VECTOR3_USE(angularUpper));
	VECTOR3_DEF_OUT(angularUpper);
}

void btGeneric6DofSpring2Constraint_getAxis(btGeneric6DofSpring2Constraint* obj, int axis_index, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getAxis(axis_index), value);
}

void btGeneric6DofSpring2Constraint_getCalculatedTransformA(btGeneric6DofSpring2Constraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getCalculatedTransformA(), value);
}

void btGeneric6DofSpring2Constraint_getCalculatedTransformB(btGeneric6DofSpring2Constraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getCalculatedTransformB(), value);
}

void btGeneric6DofSpring2Constraint_getFrameOffsetA(btGeneric6DofSpring2Constraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getFrameOffsetA(), value);
}

void btGeneric6DofSpring2Constraint_getFrameOffsetB(btGeneric6DofSpring2Constraint* obj, btScalar* value)
{
	TRANSFORM_OUT(&obj->getFrameOffsetB(), value);
}

void btGeneric6DofSpring2Constraint_getLinearLowerLimit(btGeneric6DofSpring2Constraint* obj, btScalar* linearLower)
{
	VECTOR3_CONV(linearLower);
	obj->getLinearLowerLimit(VECTOR3_USE(linearLower));
	VECTOR3_DEF_OUT(linearLower);
}

void btGeneric6DofSpring2Constraint_getLinearUpperLimit(btGeneric6DofSpring2Constraint* obj, btScalar* linearUpper)
{
	VECTOR3_CONV(linearUpper);
	obj->getLinearUpperLimit(VECTOR3_USE(linearUpper));
	VECTOR3_DEF_OUT(linearUpper);
}

btScalar btGeneric6DofSpring2Constraint_getRelativePivotPosition(btGeneric6DofSpring2Constraint* obj, int axis_index)
{
	return obj->getRelativePivotPosition(axis_index);
}

btRotationalLimitMotor2* btGeneric6DofSpring2Constraint_getRotationalLimitMotor(btGeneric6DofSpring2Constraint* obj, int index)
{
	return obj->getRotationalLimitMotor(index);
}

RotateOrder btGeneric6DofSpring2Constraint_getRotationOrder(btGeneric6DofSpring2Constraint* obj)
{
	return obj->getRotationOrder();
}

btTranslationalLimitMotor2* btGeneric6DofSpring2Constraint_getTranslationalLimitMotor(btGeneric6DofSpring2Constraint* obj)
{
	return obj->getTranslationalLimitMotor();
}

bool btGeneric6DofSpring2Constraint_isLimited(btGeneric6DofSpring2Constraint* obj, int limitIndex)
{
	return obj->isLimited(limitIndex);
}

void btGeneric6DofSpring2Constraint_setAngularLowerLimit(btGeneric6DofSpring2Constraint* obj, const btScalar* angularLower)
{
	VECTOR3_CONV(angularLower);
	obj->setAngularLowerLimit(VECTOR3_USE(angularLower));
}

void btGeneric6DofSpring2Constraint_setAngularLowerLimitReversed(btGeneric6DofSpring2Constraint* obj, const btScalar* angularLower)
{
	VECTOR3_CONV(angularLower);
	obj->setAngularLowerLimitReversed(VECTOR3_USE(angularLower));
}

void btGeneric6DofSpring2Constraint_setAngularUpperLimit(btGeneric6DofSpring2Constraint* obj, const btScalar* angularUpper)
{
	VECTOR3_CONV(angularUpper);
	obj->setAngularUpperLimit(VECTOR3_USE(angularUpper));
}

void btGeneric6DofSpring2Constraint_setAngularUpperLimitReversed(btGeneric6DofSpring2Constraint* obj, const btScalar* angularUpper)
{
	VECTOR3_CONV(angularUpper);
	obj->setAngularUpperLimitReversed(VECTOR3_USE(angularUpper));
}

void btGeneric6DofSpring2Constraint_setAxis(btGeneric6DofSpring2Constraint* obj, const btScalar* axis1, const btScalar* axis2)
{
	VECTOR3_CONV(axis1);
	VECTOR3_CONV(axis2);
	obj->setAxis(VECTOR3_USE(axis1), VECTOR3_USE(axis2));
}

void btGeneric6DofSpring2Constraint_setBounce(btGeneric6DofSpring2Constraint* obj, int index, btScalar bounce)
{
	obj->setBounce(index, bounce);
}

void btGeneric6DofSpring2Constraint_setDamping(btGeneric6DofSpring2Constraint* obj, int index, btScalar damping)
{
	obj->setDamping(index, damping);
}

void btGeneric6DofSpring2Constraint_setEquilibriumPoint(btGeneric6DofSpring2Constraint* obj)
{
	obj->setEquilibriumPoint();
}

void btGeneric6DofSpring2Constraint_setEquilibriumPoint2(btGeneric6DofSpring2Constraint* obj, int index, btScalar val)
{
	obj->setEquilibriumPoint(index, val);
}

void btGeneric6DofSpring2Constraint_setEquilibriumPoint3(btGeneric6DofSpring2Constraint* obj, int index)
{
	obj->setEquilibriumPoint(index);
}

void btGeneric6DofSpring2Constraint_setFrames(btGeneric6DofSpring2Constraint* obj, const btScalar* frameA, const btScalar* frameB)
{
	TRANSFORM_CONV(frameA);
	TRANSFORM_CONV(frameB);
	obj->setFrames(TRANSFORM_USE(frameA), TRANSFORM_USE(frameB));
}

void btGeneric6DofSpring2Constraint_setLimit(btGeneric6DofSpring2Constraint* obj, int axis, btScalar lo, btScalar hi)
{
	obj->setLimit(axis, lo, hi);
}

void btGeneric6DofSpring2Constraint_setLimitReversed(btGeneric6DofSpring2Constraint* obj, int axis, btScalar lo, btScalar hi)
{
	obj->setLimitReversed(axis, lo, hi);
}

void btGeneric6DofSpring2Constraint_setLinearLowerLimit(btGeneric6DofSpring2Constraint* obj, const btScalar* linearLower)
{
	VECTOR3_CONV(linearLower);
	obj->setLinearLowerLimit(VECTOR3_USE(linearLower));
}

void btGeneric6DofSpring2Constraint_setLinearUpperLimit(btGeneric6DofSpring2Constraint* obj, const btScalar* linearUpper)
{
	VECTOR3_CONV(linearUpper);
	obj->setLinearUpperLimit(VECTOR3_USE(linearUpper));
}

void btGeneric6DofSpring2Constraint_setMaxMotorForce(btGeneric6DofSpring2Constraint* obj, int index, btScalar force)
{
	obj->setMaxMotorForce(index, force);
}

void btGeneric6DofSpring2Constraint_setRotationOrder(btGeneric6DofSpring2Constraint* obj, RotateOrder order)
{
	obj->setRotationOrder(order);
}

void btGeneric6DofSpring2Constraint_setServo(btGeneric6DofSpring2Constraint* obj, int index, bool onOff)
{
	obj->setServo(index, onOff);
}

void btGeneric6DofSpring2Constraint_setServoTarget(btGeneric6DofSpring2Constraint* obj, int index, btScalar target)
{
	obj->setServoTarget(index, target);
}

void btGeneric6DofSpring2Constraint_setStiffness(btGeneric6DofSpring2Constraint* obj, int index, btScalar stiffness)
{
	obj->setStiffness(index, stiffness);
}

void btGeneric6DofSpring2Constraint_setTargetVelocity(btGeneric6DofSpring2Constraint* obj, int index, btScalar velocity)
{
	obj->setTargetVelocity(index, velocity);
}
