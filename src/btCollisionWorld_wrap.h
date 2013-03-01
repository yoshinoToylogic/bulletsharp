#include "main.h"

extern "C"
{
	EXPORT void btCollisionWorld_delete(btCollisionWorld* obj);
    EXPORT void btCollisionWorld_addCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject, int collisionFilterGroup, int collisionFilterMask);
    EXPORT btBroadphaseInterface* btCollisionWorld_getBroadphase(btCollisionWorld* obj);
	EXPORT btAlignedObjectArray<btCollisionObject*>* btCollisionWorld_getCollisionObjectArray(btCollisionWorld* obj);
    EXPORT btOverlappingPairCache* btCollisionWorld_getPairCache(btCollisionWorld* obj);
    EXPORT btDispatcher* btCollisionWorld_getDispatcher(btCollisionWorld* obj);
    EXPORT int btCollisionWorld_getNumCollisionObjects(btCollisionWorld* obj);
    EXPORT void btCollisionWorld_rayTest(btCollisionWorld* obj, btScalar* rayFromWorld, btScalar* rayToWorld, btScalar* resultCallback);
    EXPORT void btCollisionWorld_removeCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject);
}
