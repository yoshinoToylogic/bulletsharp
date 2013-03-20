#include "main.h"

extern "C"
{
	EXPORT btRigidBody::btRigidBodyConstructionInfo* btRigidBody_btRigidBodyConstructionInfo_new(btScalar mass, btMotionState* motionState, btCollisionShape* collisionShape, btScalar* localInertia);
	EXPORT btRigidBody::btRigidBodyConstructionInfo* btRigidBody_btRigidBodyConstructionInfo_new2(btScalar mass, btMotionState* motionState, btCollisionShape* collisionShape);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getAdditionalAngularDampingFactor(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getAdditionalAngularDampingThresholdSqr(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT bool btRigidBody_btRigidBodyConstructionInfo_getAdditionalDamping(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getAdditionalDampingFactor(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getAdditionalLinearDampingThresholdSqr(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getAngularDamping(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getAngularSleepingThreshold(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btCollisionShape* btRigidBody_btRigidBodyConstructionInfo_getCollisionShape(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getFriction(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getLinearDamping(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getLinearSleepingThreshold(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_getLocalInertia(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getMass(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btMotionState* btRigidBody_btRigidBodyConstructionInfo_getMotionState(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getRestitution(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btScalar btRigidBody_btRigidBodyConstructionInfo_getRollingFriction(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_getStartWorldTransform(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setAdditionalAngularDampingFactor(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setAdditionalAngularDampingThresholdSqr(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setAdditionalDamping(btRigidBody::btRigidBodyConstructionInfo* obj, bool value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setAdditionalDampingFactor(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setAdditionalLinearDampingThresholdSqr(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setAngularDamping(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setAngularSleepingThreshold(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setCollisionShape(btRigidBody::btRigidBodyConstructionInfo* obj, btCollisionShape* value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setFriction(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setLinearDamping(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setLinearSleepingThreshold(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setLocalInertia(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar* value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setMass(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setMotionState(btRigidBody::btRigidBodyConstructionInfo* obj, btMotionState* value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setRestitution(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setRollingFriction(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_setStartWorldTransform(btRigidBody::btRigidBodyConstructionInfo* obj, btScalar* value);
	EXPORT void btRigidBody_btRigidBodyConstructionInfo_delete(btRigidBody::btRigidBodyConstructionInfo* obj);
	EXPORT btRigidBody* btRigidBody_new(btRigidBody::btRigidBodyConstructionInfo* constructionInfo);
	EXPORT btRigidBody* btRigidBody_new2(btScalar mass, btMotionState* motionState, btCollisionShape* collisionShape, btScalar* localInertia);
	EXPORT btRigidBody* btRigidBody_new3(btScalar mass, btMotionState* motionState, btCollisionShape* collisionShape);
	EXPORT void btRigidBody_addConstraintRef(btRigidBody* obj, btTypedConstraint* c);
	EXPORT void btRigidBody_applyCentralForce(btRigidBody* obj, btScalar* force);
	EXPORT void btRigidBody_applyCentralImpulse(btRigidBody* obj, btScalar* impulse);
	EXPORT void btRigidBody_applyDamping(btRigidBody* obj, btScalar timeStep);
	EXPORT void btRigidBody_applyForce(btRigidBody* obj, btScalar* force, btScalar* rel_pos);
	EXPORT void btRigidBody_applyGravity(btRigidBody* obj);
	EXPORT void btRigidBody_applyImpulse(btRigidBody* obj, btScalar* impulse, btScalar* rel_pos);
	EXPORT void btRigidBody_applyTorque(btRigidBody* obj, btScalar* torque);
	EXPORT void btRigidBody_applyTorqueImpulse(btRigidBody* obj, btScalar* torque);
	EXPORT bool btRigidBody_checkCollideWithOverride(btRigidBody* obj, btCollisionObject* co);
	EXPORT void btRigidBody_clearForces(btRigidBody* obj);
	EXPORT btScalar btRigidBody_computeAngularImpulseDenominator(btRigidBody* obj, btScalar* axis);
	EXPORT void btRigidBody_computeGyroscopicForce(btRigidBody* obj, btScalar maxGyroscopicForce);
	EXPORT btScalar btRigidBody_computeImpulseDenominator(btRigidBody* obj, btScalar* pos, btScalar* normal);
	EXPORT void btRigidBody_getAabb(btRigidBody* obj, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT btScalar btRigidBody_getAngularDamping(btRigidBody* obj);
	EXPORT void btRigidBody_getAngularFactor(btRigidBody* obj, btScalar* value);
	EXPORT btScalar btRigidBody_getAngularSleepingThreshold(btRigidBody* obj);
	EXPORT void btRigidBody_getAngularVelocity(btRigidBody* obj, btScalar* value);
	EXPORT btBroadphaseProxy* btRigidBody_getBroadphaseProxy(btRigidBody* obj);
	EXPORT void btRigidBody_getCenterOfMassPosition(btRigidBody* obj, btScalar* value);
	EXPORT void btRigidBody_getCenterOfMassTransform(btRigidBody* obj, btScalar* value);
	EXPORT btTypedConstraint* btRigidBody_getConstraintRef(btRigidBody* obj, int index);
	EXPORT int btRigidBody_getContactSolverType(btRigidBody* obj);
	EXPORT int btRigidBody_getFlags(btRigidBody* obj);
	EXPORT int btRigidBody_getFrictionSolverType(btRigidBody* obj);
	EXPORT void btRigidBody_getGravity(btRigidBody* obj, btScalar* value);
	EXPORT void btRigidBody_getInvInertiaDiagLocal(btRigidBody* obj, btScalar* value);
	EXPORT void btRigidBody_getInvInertiaTensorWorld(btRigidBody* obj, btScalar* value);
	EXPORT btScalar btRigidBody_getInvMass(btRigidBody* obj);
	EXPORT btScalar btRigidBody_getLinearDamping(btRigidBody* obj);
	EXPORT void btRigidBody_getLinearFactor(btRigidBody* obj, btScalar* value);
	EXPORT btScalar btRigidBody_getLinearSleepingThreshold(btRigidBody* obj);
	EXPORT void btRigidBody_getLinearVelocity(btRigidBody* obj, btScalar* value);
	EXPORT btMotionState* btRigidBody_getMotionState(btRigidBody* obj);
	EXPORT int btRigidBody_getNumConstraintRefs(btRigidBody* obj);
	EXPORT void btRigidBody_getOrientation(btRigidBody* obj, btScalar* value);
	EXPORT void btRigidBody_getTotalForce(btRigidBody* obj, btScalar* value);
	EXPORT void btRigidBody_getTotalTorque(btRigidBody* obj, btScalar* value);
	EXPORT void btRigidBody_getVelocityInLocalPoint(btRigidBody* obj, btScalar* rel_pos, btScalar* value);
	EXPORT void btRigidBody_integrateVelocities(btRigidBody* obj, btScalar step);
	EXPORT bool btRigidBody_isInWorld(btRigidBody* obj);
	EXPORT void btRigidBody_predictIntegratedTransform(btRigidBody* obj, btScalar step, btScalar* predictedTransform);
	EXPORT void btRigidBody_proceedToTransform(btRigidBody* obj, btScalar* newTrans);
	EXPORT void btRigidBody_removeConstraintRef(btRigidBody* obj, btTypedConstraint* c);
	EXPORT void btRigidBody_saveKinematicState(btRigidBody* obj, btScalar step);
	EXPORT void btRigidBody_setAngularFactor(btRigidBody* obj, btScalar* angFac);
	EXPORT void btRigidBody_setAngularFactor2(btRigidBody* obj, btScalar angFac);
	EXPORT void btRigidBody_setAngularVelocity(btRigidBody* obj, btScalar* ang_vel);
	EXPORT void btRigidBody_setCenterOfMassTransform(btRigidBody* obj, btScalar* xform);
	EXPORT void btRigidBody_setContactSolverType(btRigidBody* obj, int value);
	EXPORT void btRigidBody_setDamping(btRigidBody* obj, btScalar lin_damping, btScalar ang_damping);
	EXPORT void btRigidBody_setFlags(btRigidBody* obj, int flags);
	EXPORT void btRigidBody_setFrictionSolverType(btRigidBody* obj, int value);
	EXPORT void btRigidBody_setGravity(btRigidBody* obj, btScalar* acceleration);
	EXPORT void btRigidBody_setInvInertiaDiagLocal(btRigidBody* obj, btScalar* diagInvInertia);
	EXPORT void btRigidBody_setLinearFactor(btRigidBody* obj, btScalar* linearFactor);
	EXPORT void btRigidBody_setLinearVelocity(btRigidBody* obj, btScalar* lin_vel);
	EXPORT void btRigidBody_setMassProps(btRigidBody* obj, btScalar mass, btScalar* inertia);
	EXPORT void btRigidBody_setMotionState(btRigidBody* obj, btMotionState* motionState);
	EXPORT void btRigidBody_setNewBroadphaseProxy(btRigidBody* obj, btBroadphaseProxy* broadphaseProxy);
	EXPORT void btRigidBody_setSleepingThresholds(btRigidBody* obj, btScalar linear, btScalar angular);
	EXPORT void btRigidBody_translate(btRigidBody* obj, btScalar* v);
	EXPORT btRigidBody* btRigidBody_upcast(btCollisionObject* colObj);
	EXPORT void btRigidBody_updateDeactivation(btRigidBody* obj, btScalar timeStep);
	EXPORT void btRigidBody_updateInertiaTensor(btRigidBody* obj);
	EXPORT bool btRigidBody_wantsSleeping(btRigidBody* obj);
}
