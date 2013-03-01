#include "main.h"

extern "C"
{
    EXPORT btCollisionObject* btCollisionObject_new();
	EXPORT void btCollisionObject_delete(btCollisionObject* obj);
    EXPORT void btCollisionObject_activate(btCollisionObject* obj, bool forceActivate);
    EXPORT bool btCollisionObject_checkCollideWith(btCollisionObject* obj, btCollisionObject* collisionObject);
    EXPORT void btCollisionObject_forceActivationState(btCollisionObject* obj, int newState);
    EXPORT int btCollisionObject_getActivationState(btCollisionObject* obj);
    EXPORT void btCollisionObject_getAnisotropicFriction(btCollisionObject* obj, btScalar* friction);
    EXPORT btBroadphaseProxy* btCollisionObject_getBroadphaseHandle(btCollisionObject* obj); 
    EXPORT float btCollisionObject_getCcdMotionThreshold(btCollisionObject* obj);
    EXPORT float btCollisionObject_getCcdSquareMotionThreshold(btCollisionObject* obj);
    EXPORT float btCollisionObject_getCcdSweptSphereRadius(btCollisionObject* obj);
    EXPORT int btCollisionObject_getCollisionFlags(btCollisionObject* obj);
    EXPORT btCollisionShape* btCollisionObject_getCollisionShape(btCollisionObject* obj);
    EXPORT int btCollisionObject_getCompanionId(btCollisionObject* obj);
    EXPORT float btCollisionObject_getContactProcessingThreshold(btCollisionObject* obj);
    EXPORT float btCollisionObject_getDeactivationTime(btCollisionObject* obj);
    EXPORT float btCollisionObject_getFriction(btCollisionObject* obj);
    EXPORT float btCollisionObject_getHitFraction(btCollisionObject* obj);
    EXPORT void btCollisionObject_getInterpolationAngularVelocity(btCollisionObject* obj, btScalar* velocity);
    EXPORT void btCollisionObject_getInterpolationLinearVelocity(btCollisionObject* obj, btScalar* velocity);
    EXPORT void btCollisionObject_getInterpolationWorldTransform(btCollisionObject* obj, btScalar* transform);
    EXPORT int btCollisionObject_getIslandTag(btCollisionObject* obj);
    EXPORT float btCollisionObject_getRestitution(btCollisionObject* obj);
    EXPORT void* btCollisionObject_getUserPointer(btCollisionObject* obj);
    EXPORT void btCollisionObject_getWorldTransform(btCollisionObject* obj, btScalar* transform);
    EXPORT bool btCollisionObject_hasAnisotropicFriction(btCollisionObject* obj);
    EXPORT bool btCollisionObject_hasContactResponse(btCollisionObject* obj);
    EXPORT bool btCollisionObject_isActive(btCollisionObject* obj);
    EXPORT bool btCollisionObject_isKinematicObject(btCollisionObject* obj);
    EXPORT bool btCollisionObject_isStaticObject(btCollisionObject* obj);
    EXPORT bool btCollisionObject_isStaticOrKinematicObject(btCollisionObject* obj);
    EXPORT bool btCollisionObject_mergesSimulationIslands(btCollisionObject* obj);
    EXPORT void btCollisionObject_setActivationState(btCollisionObject* obj, int newState);
    EXPORT void btCollisionObject_setAnisotropicFriction(btCollisionObject* obj, btScalar* anisotropicFriction);
    EXPORT void btCollisionObject_setBroadphaseHandle(btCollisionObject* obj, btBroadphaseProxy* handle);
    EXPORT void btCollisionObject_setCcdMotionThreshold(btCollisionObject* obj, float ccdMotionThreshold);
    EXPORT void btCollisionObject_setCcdSweptSphereRadius(btCollisionObject* obj, float radius);
    EXPORT void btCollisionObject_setCollisionFlags(btCollisionObject* obj, int flags);
    EXPORT void btCollisionObject_setCollisionShape(btCollisionObject* obj, btCollisionShape* collisionShape);
    EXPORT void btCollisionObject_setCompanionId(btCollisionObject* obj, int id);
    EXPORT void btCollisionObject_setContactProcessingThreshold(btCollisionObject* obj, float contactProcessingThreshold);
    EXPORT void btCollisionObject_setDeactivationTime(btCollisionObject* obj, float time);
    EXPORT void btCollisionObject_setFriction(btCollisionObject* obj, float frict);
    EXPORT void btCollisionObject_setHitFraction(btCollisionObject* obj, float hitFraction);
    EXPORT void btCollisionObject_setInterpolationAngularVelocity(btCollisionObject* obj, btScalar* angvel);
    EXPORT void btCollisionObject_setInterpolationLinearVelocity(btCollisionObject* obj, btScalar* linvel);
    EXPORT void btCollisionObject_setInterpolationWorldTransform(btCollisionObject* obj, btScalar* trans);
    EXPORT void btCollisionObject_setIslandTag(btCollisionObject* obj, int tag);
    EXPORT void btCollisionObject_setRestitution(btCollisionObject* obj, float rest);
    EXPORT void btCollisionObject_setUserPointer(btCollisionObject* obj, void* userPointer);
    EXPORT void btCollisionObject_setWorldTransform(btCollisionObject* obj, btScalar* worldTrans);
}
