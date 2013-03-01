#include "btRigidBody_wrap.h"

btRigidBody::btRigidBodyConstructionInfo* btRigidBodyConstructionInfo_new(btScalar mass, btMotionState* motionState, btCollisionShape* collisionShape, btScalar* localInertia)
{
	VECTOR3_CONV(localInertia);
	return ALIGNED_NEW(btRigidBody::btRigidBodyConstructionInfo) (mass, motionState, collisionShape, VECTOR3_USE(localInertia));
}

btRigidBody::btRigidBodyConstructionInfo* btRigidBodyConstructionInfo_new2(btScalar mass, btMotionState* motionState, btCollisionShape* collisionShape)
{
    return ALIGNED_NEW(btRigidBody::btRigidBodyConstructionInfo) (mass, motionState, collisionShape);
}

void btRigidBodyConstructionInfo_delete(btRigidBody::btRigidBodyConstructionInfo* obj)
{
	ALIGNED_FREE(obj);
}


btRigidBody* btRigidBody_new(btRigidBody::btRigidBodyConstructionInfo* constructionInfo)
{
    return new btRigidBody(*constructionInfo);
}

void btRigidBody_applyCentralForce(btRigidBody* obj, btScalar* force)
{
	VECTOR3_CONV(force);
    obj->applyCentralForce(VECTOR3_USE(force));
}

void btRigidBody_applyCentralImpulse(btRigidBody* obj, btScalar* impulse)
{
	VECTOR3_CONV(impulse);
    obj->applyCentralImpulse(VECTOR3_USE(impulse));
}

void btRigidBody_applyForce(btRigidBody* obj, btScalar* force, btScalar* rel_pos)
{
	VECTOR3_CONV(force);
	VECTOR3_CONV(rel_pos);
    obj->applyForce(VECTOR3_USE(force), VECTOR3_USE(rel_pos));
}

void btRigidBody_applyImpulse(btRigidBody* obj, btScalar* impulse, btScalar* rel_pos)
{
	VECTOR3_CONV(impulse);
	VECTOR3_CONV(rel_pos);
    obj->applyImpulse(VECTOR3_USE(impulse), VECTOR3_USE(rel_pos));
}

void btRigidBody_applyTorque(btRigidBody* obj, btScalar* torque)
{
	VECTOR3_CONV(torque);
    obj->applyTorque(VECTOR3_USE(torque));
}

void btRigidBody_applyTorqueImpulse(btRigidBody* obj, btScalar* torque)
{
	VECTOR3_CONV(torque);
    obj->applyTorqueImpulse(VECTOR3_USE(torque));
}

void btRigidBody_clearForces(btRigidBody* obj)
{
    obj->clearForces();
}

float btRigidBody_computeAngularImpulseDenominator(btRigidBody* obj, btScalar* axis)
{
	VECTOR3_CONV(axis);
    return obj->computeAngularImpulseDenominator(VECTOR3_USE(axis));
}

float btRigidBody_computeImpulseDenominator(btRigidBody* obj, btScalar* pos, btScalar* normal)
{
	VECTOR3_CONV(pos);
	VECTOR3_CONV(normal);
    return obj->computeImpulseDenominator(VECTOR3_USE(pos), VECTOR3_USE(normal));
}

float btRigidBody_getAngularDamping(btRigidBody* obj)
{
    return obj->getAngularDamping();
}

void btRigidBody_getAngularFactor(btRigidBody* obj, btScalar* factor)
{
    VECTOR3_OUT(&obj->getAngularFactor(), factor);
}

float btRigidBody_getAngularSleepingThreshold(btRigidBody* obj)
{
    return obj->getAngularSleepingThreshold();
}

void btRigidBody_getAngularVelocity(btRigidBody* obj, btScalar* velocity) //btVector3
{
	VECTOR3_OUT(&obj->getAngularVelocity(), velocity);
}

float btRigidBody_getLinearSleepingThreshold(btRigidBody* obj)
{
    return obj->getLinearSleepingThreshold();
}

void btRigidBody_getCenterOfMassPosition(btRigidBody* obj, btScalar* position)
{
	VECTOR3_OUT(&obj->getCenterOfMassPosition(), position);
}

void btRigidBody_getCenterOfMassTransform(btRigidBody* obj, btScalar* transform)
{
	TRANSFORM_DEF(transform) = obj->getCenterOfMassTransform();
	TRANSFORM_DEF_OUT(transform);
}

btTypedConstraint* btRigidBody_getConstraintRef(btRigidBody* obj, int index) //btTypedConstraint
{
    return obj->getConstraintRef(index);
}

void btRigidBody_getGravity(btRigidBody* obj, btScalar* gravity)
{
    VECTOR3_OUT(&obj->getGravity(), gravity);
}

void btRigidBody_getInvInertiaDiagLocal(btRigidBody* obj, btScalar* inertia)
{
    VECTOR3_OUT(&obj->getInvInertiaDiagLocal(), inertia);
}

void btRigidBody_getInvInertiaTensorWorld(btRigidBody* obj, btScalar* inertiaTensor)
{
	MATRIX3X3_DEF(inertiaTensor) = obj->getInvInertiaTensorWorld();
	MATRIX3X3_DEF_OUT(inertiaTensor);
}

float btRigidBody_getInvMass(btRigidBody* obj)
{
    return obj->getInvMass();
}

float btRigidBody_getLinearDamping(btRigidBody* obj)
{
    return obj->getLinearDamping();
}

void btRigidBody_getLinearFactor(btRigidBody* obj, btScalar* linearFactor)
{
	VECTOR3_OUT(&obj->getLinearFactor(), linearFactor);
}

void btRigidBody_getLinearVelocity(btRigidBody* obj, btScalar* linearVelocity)
{
	VECTOR3_OUT(&obj->getLinearVelocity(), linearVelocity);
}

btMotionState* btRigidBody_getMotionState(btRigidBody* obj)
{
    return obj->getMotionState();
}

int btRigidBody_getNumConstraintRefs(btRigidBody* obj)
{
    return obj->getNumConstraintRefs();
}

void btRigidBody_getTotalForce(btRigidBody* obj, btScalar* force)
{
	VECTOR3_OUT(&obj->getTotalForce(), force);
}

void btRigidBody_getTotalTorque(btRigidBody* obj, btScalar* torque)
{
	VECTOR3_OUT(&obj->getTotalTorque(), torque);
}

void btRigidBody_getVelocityInLocalPoint(btRigidBody* obj, btScalar* rel_pos, btScalar* velocity)
{
	VECTOR3_CONV(rel_pos);
	VECTOR3_OUT(&obj->getVelocityInLocalPoint(VECTOR3_USE(rel_pos)), velocity);
}

bool btRigidBody_isInWorld(btRigidBody* obj)
{
    return obj->isInWorld();
}

void btRigidBody_setAngularFactor(btRigidBody* obj, float angFac)
{
    obj->setAngularFactor(angFac);
}

void btRigidBody_setAngularVelocity(btRigidBody* obj, btScalar* ang_vel)
{
	VECTOR3_CONV(ang_vel);
    obj->setAngularVelocity(VECTOR3_USE(ang_vel));
}

void btRigidBody_setInvInertiaDiagLocal(btRigidBody* obj, btScalar* diagInvInertia)
{
	VECTOR3_CONV(diagInvInertia);
    obj->setInvInertiaDiagLocal(VECTOR3_USE(diagInvInertia));
}

void btRigidBody_setLinearFactor(btRigidBody* obj, btScalar* linearFactor)
{
	VECTOR3_CONV(linearFactor);
    obj->setLinearFactor(VECTOR3_USE(linearFactor));
}

void btRigidBody_setLinearVelocity(btRigidBody* obj, btScalar* linearVelocity)
{
	VECTOR3_CONV(linearVelocity);
    obj->setLinearVelocity(VECTOR3_USE(linearVelocity));
}

void btRigidBody_setMotionState(btRigidBody* obj, btMotionState* motionState)
{
    obj->setMotionState(motionState);
}

void btRigidBody_setNewBroadphaseProxy(btRigidBody* obj, btBroadphaseProxy* broadphaseProxy)
{
    obj->setNewBroadphaseProxy(broadphaseProxy);
}

void btRigidBody_setSleepingThresholds(btRigidBody* obj, float linear, float angular)
{
    obj->setSleepingThresholds(linear, angular);
}

void btRigidBody_translate(btRigidBody* obj, btScalar* v)
{
	ATTRIBUTE_ALIGNED16(btVector3) vTemp;
	Vector3TobtVector3(v, &vTemp);
    obj->translate(vTemp);
}

void btRigidBody_updateDeactivation(btRigidBody* obj, float timeStep)
{
    obj->updateDeactivation(timeStep);
}

void btRigidBody_delete(btRigidBody* obj)
{
    delete obj;
}
