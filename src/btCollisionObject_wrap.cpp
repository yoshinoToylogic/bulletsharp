#include "btCollisionObject_wrap.h"

btCollisionObject* btCollisionObject_new()
{
    return new btCollisionObject();
}

void btCollisionObject_delete(btCollisionObject* obj)
{
    delete obj;
}

void btCollisionObject_activate(btCollisionObject* obj, bool forceActivate)
{
    obj->activate(forceActivate);
}

bool btCollisionObject_checkCollideWith(btCollisionObject* obj, btCollisionObject* collisionObject)
{
    return obj->checkCollideWith(collisionObject);
}

void btCollisionObject_forceActivationState(btCollisionObject* obj, int newState)
{
    obj->forceActivationState(newState);
}

int btCollisionObject_getActivationState(btCollisionObject* obj)
{
    return obj->getActivationState();
}

void btCollisionObject_getAnisotropicFriction(btCollisionObject* obj, btScalar* friction)
{
	VECTOR3_DEF(friction);
	VECTOR3_USE(friction) = obj->getAnisotropicFriction();
	VECTOR3_DEF_OUT(friction);
}

btBroadphaseProxy* btCollisionObject_getBroadphaseHandle(btCollisionObject* obj)
{
    return obj->getBroadphaseHandle();
}

float btCollisionObject_getCcdMotionThreshold(btCollisionObject* obj)
{
    return obj->getCcdMotionThreshold();
}

float btCollisionObject_getCcdSquareMotionThreshold(btCollisionObject* obj)
{
    return obj->getCcdSquareMotionThreshold();
}

float btCollisionObject_getCcdSweptSphereRadius(btCollisionObject* obj)
{
    return obj->getCcdSweptSphereRadius();
}

int btCollisionObject_getCollisionFlags(btCollisionObject* obj)
{
    return obj->getCollisionFlags();
}

btCollisionShape* btCollisionObject_getCollisionShape(btCollisionObject* obj)
{
    return obj->getCollisionShape();
}

int btCollisionObject_getCompanionId(btCollisionObject* obj)
{
    return obj->getCompanionId();
}

float btCollisionObject_getContactProcessingThreshold(btCollisionObject* obj)
{
    return obj->getContactProcessingThreshold();
}

float btCollisionObject_getDeactivationTime(btCollisionObject* obj)
{
    return obj->getDeactivationTime();
}

float btCollisionObject_getFriction(btCollisionObject* obj)
{
    return obj->getFriction();
}

float btCollisionObject_getHitFraction(btCollisionObject* obj)
{
    return obj->getHitFraction();
}

void btCollisionObject_getInterpolationAngularVelocity(btCollisionObject* obj, btScalar* velocity)
{
	VECTOR3_DEF(velocity);
	VECTOR3_USE(velocity) = obj->getInterpolationAngularVelocity();
	VECTOR3_DEF_OUT(velocity);
}

void btCollisionObject_getInterpolationLinearVelocity(btCollisionObject* obj, btScalar* velocity)
{
	VECTOR3_DEF(velocity);
	VECTOR3_USE(velocity) = obj->getInterpolationLinearVelocity();
	VECTOR3_DEF_OUT(velocity);
}

void btCollisionObject_getInterpolationWorldTransform(btCollisionObject* obj, btScalar* transform)
{
	btTransfromToMatrix(&obj->getInterpolationWorldTransform(), transform);
}

int btCollisionObject_getIslandTag(btCollisionObject* obj)
{
    return obj->getIslandTag();
}

float btCollisionObject_getRestitution(btCollisionObject* obj)
{
    return obj->getRestitution();
}

void* btCollisionObject_getUserPointer(btCollisionObject* obj)
{
    return obj->getUserPointer();
}

void btCollisionObject_getWorldTransform(btCollisionObject* obj, btScalar* transform)
{
	btTransfromToMatrix(&obj->getWorldTransform(), transform);
}

bool btCollisionObject_hasAnisotropicFriction(btCollisionObject* obj)
{
    return obj->hasAnisotropicFriction();
}

bool btCollisionObject_hasContactResponse(btCollisionObject* obj)
{
    return obj->hasContactResponse();
}

bool btCollisionObject_isActive(btCollisionObject* obj)
{
    return obj->isActive();
}

bool btCollisionObject_isKinematicObject(btCollisionObject* obj)
{
    return obj->isKinematicObject();
}

bool btCollisionObject_isStaticObject(btCollisionObject* obj)
{
    return obj->isStaticObject();
}

bool btCollisionObject_isStaticOrKinematicObject(btCollisionObject* obj)
{
    return obj->isStaticOrKinematicObject();
}

bool btCollisionObject_mergesSimulationIslands(btCollisionObject* obj)
{
    return obj->mergesSimulationIslands();
}

void btCollisionObject_setActivationState(btCollisionObject* obj, int newState)
{
    obj->setActivationState(newState);
}

void btCollisionObject_setAnisotropicFriction(btCollisionObject* obj, btScalar* anisotropicFriction)
{
	VECTOR3_CONV(anisotropicFriction);
    obj->setAnisotropicFriction(VECTOR3_USE(anisotropicFriction));
}

void btCollisionObject_setBroadphaseHandle(btCollisionObject* obj, btBroadphaseProxy* handle)
{
    obj->setBroadphaseHandle(handle);
}

void btCollisionObject_setCcdMotionThreshold(btCollisionObject* obj, float ccdMotionThreshold)
{
    obj->setCcdMotionThreshold(ccdMotionThreshold);
}

void btCollisionObject_setCcdSweptSphereRadius(btCollisionObject* obj, float radius)
{
    obj->setCcdSweptSphereRadius(radius);
}

void btCollisionObject_setCollisionFlags(btCollisionObject* obj, int flags)
{
    obj->setCollisionFlags(flags);
}

void btCollisionObject_setCollisionShape(btCollisionObject* obj, btCollisionShape* collisionShape)
{
    obj->setCollisionShape(collisionShape);
}

void btCollisionObject_setCompanionId(btCollisionObject* obj, int id)
{
    obj->setCompanionId(id);
}

void btCollisionObject_setContactProcessingThreshold(btCollisionObject* obj, float contactProcessingThreshold)
{
    obj->setContactProcessingThreshold(contactProcessingThreshold);
}

void btCollisionObject_setDeactivationTime(btCollisionObject* obj, float time)
{
    obj->setDeactivationTime(time);
}

void btCollisionObject_setFriction(btCollisionObject* obj, float frict)
{
    obj->setFriction(frict);
}

void btCollisionObject_setHitFraction(btCollisionObject* obj, float hitFraction)
{
    obj->setHitFraction(hitFraction);
}

void btCollisionObject_setInterpolationAngularVelocity(btCollisionObject* obj, btScalar* angvel)
{
	VECTOR3_CONV(angvel);
    obj->setInterpolationAngularVelocity(VECTOR3_USE(angvel));
}

void btCollisionObject_setInterpolationLinearVelocity(btCollisionObject* obj, btScalar* linvel)
{
	VECTOR3_CONV(linvel);
    obj->setInterpolationLinearVelocity(VECTOR3_USE(linvel));
}

void btCollisionObject_setInterpolationWorldTransform(btCollisionObject* obj, btScalar* trans)
{
	TRANSFORM_CONV(trans);
    obj->setInterpolationWorldTransform(TRANSFORM_USE(trans));
}

void btCollisionObject_setIslandTag(btCollisionObject* obj, int tag)
{
    obj->setIslandTag(tag);
}

void btCollisionObject_setRestitution(btCollisionObject* obj, float rest)
{
    obj->setRestitution(rest);
}

void btCollisionObject_setUserPointer(btCollisionObject* obj, void* userPointer)
{
    obj->setUserPointer(userPointer);
}

void btCollisionObject_setWorldTransform(btCollisionObject* obj, btScalar* worldTrans)
{
	TRANSFORM_CONV(worldTrans);
    obj->setWorldTransform(TRANSFORM_USE(worldTrans));
}
