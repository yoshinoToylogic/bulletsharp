#include "main.h"

extern "C"
{
	EXPORT btRigidBody::btRigidBodyConstructionInfo* btRigidBodyConstructionInfo_new(btScalar mass, btMotionState* motionState, btCollisionShape* collisionShape, btScalar* localInertia);
	EXPORT btRigidBody::btRigidBodyConstructionInfo* btRigidBodyConstructionInfo_new2(btScalar mass, btMotionState* motionState, btCollisionShape* collisionShape);
	EXPORT void btRigidBodyConstructionInfo_delete(btRigidBody::btRigidBodyConstructionInfo* obj);
    
	EXPORT btRigidBody* btRigidBody_new(btRigidBody::btRigidBodyConstructionInfo* constructionInfo);
    EXPORT void btRigidBody_applyCentralForce(btRigidBody* obj, btScalar* force);
    EXPORT void btRigidBody_applyCentralImpulse(btRigidBody* obj, btScalar* impulse);
    EXPORT void btRigidBody_applyForce(btRigidBody* obj, btScalar* force, btScalar* rel_pos);
    EXPORT void btRigidBody_applyImpulse(btRigidBody* obj, btScalar* impulse, btScalar* rel_pos);
    EXPORT void btRigidBody_applyTorque(btRigidBody* obj, btScalar* torque);
    EXPORT void btRigidBody_applyTorqueImpulse(btRigidBody* obj, btScalar* torque);
    EXPORT void btRigidBody_clearForces(btRigidBody* obj);
    EXPORT float btRigidBody_computeAngularImpulseDenominator(btRigidBody* obj, btScalar* axis);
    EXPORT float btRigidBody_computeImpulseDenominator(btRigidBody* obj, btScalar* pos, btScalar* normal);
    EXPORT float btRigidBody_getAngularDamping(btRigidBody* obj);
    EXPORT void btRigidBody_getAngularFactor(btRigidBody* obj, btScalar* factor);
    EXPORT float btRigidBody_getAngularSleepingThreshold(btRigidBody* obj);
    EXPORT void btRigidBody_getAngularVelocity(btRigidBody* obj, btScalar* velocity);
    EXPORT float btRigidBody_getLinearSleepingThreshold(btRigidBody* obj);
    EXPORT void btRigidBody_getCenterOfMassPosition(btRigidBody* obj, btScalar* position);
    EXPORT void btRigidBody_getCenterOfMassTransform(btRigidBody* obj, btScalar* transform);
    EXPORT btTypedConstraint* btRigidBody_getConstraintRef(btRigidBody* obj, int index);
    EXPORT void btRigidBody_getGravity(btRigidBody* obj, btScalar* gravity);
    EXPORT void btRigidBody_getInvInertiaDiagLocal(btRigidBody* obj, btScalar* inertia);
    EXPORT void btRigidBody_getInvInertiaTensorWorld(btRigidBody* obj, btScalar* inertiaTensor);
    EXPORT float btRigidBody_getInvMass(btRigidBody* obj);
    EXPORT float btRigidBody_getLinearDamping(btRigidBody* obj);
	EXPORT void btRigidBody_getLinearFactor(btRigidBody* obj, btScalar* linearFactor);
    EXPORT void btRigidBody_getLinearVelocity(btRigidBody* obj, btScalar* linearVelocity);
    EXPORT btMotionState* btRigidBody_getMotionState(btRigidBody* obj);
    EXPORT int btRigidBody_getNumConstraintRefs(btRigidBody* obj);
    EXPORT void btRigidBody_getTotalForce(btRigidBody* obj, btScalar* force);
    EXPORT void btRigidBody_getTotalTorque(btRigidBody* obj, btScalar* torque);
    EXPORT void btRigidBody_getVelocityInLocalPoint(btRigidBody* obj, btScalar* rel_pos, btScalar* velocity);
    EXPORT bool btRigidBody_isInWorld(btRigidBody* obj);
    EXPORT void btRigidBody_setAngularFactor(btRigidBody* obj, float angFac);
    EXPORT void btRigidBody_setAngularVelocity(btRigidBody* obj, btScalar* ang_vel);
    EXPORT void btRigidBody_setInvInertiaDiagLocal(btRigidBody* obj, btScalar* diagInvInertia);
	EXPORT void btRigidBody_setLinearFactor(btRigidBody* obj, btScalar* linearFactor);
    EXPORT void btRigidBody_setLinearVelocity(btRigidBody* obj, btScalar* linearVelocity);
    EXPORT void btRigidBody_setMotionState(btRigidBody* obj, btMotionState* motionState);
    EXPORT void btRigidBody_setNewBroadphaseProxy(btRigidBody* obj, btBroadphaseProxy* broadphaseProxy);
    EXPORT void btRigidBody_setSleepingThresholds(btRigidBody* obj, float linear, float angular);
    EXPORT void btRigidBody_translate(btRigidBody* obj, btScalar* v);
    EXPORT void btRigidBody_updateDeactivation(btRigidBody* obj, float timeStep);
    EXPORT void btRigidBody_delete(btRigidBody* handle);
}