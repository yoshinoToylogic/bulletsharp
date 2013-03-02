#include "main.h"

extern "C"
{
	EXPORT void btCollisionWorld_RayResultCallback_delete(btCollisionWorld::RayResultCallback* obj);
	EXPORT btScalar btCollisionWorld_RayResultCallback_addSingleResult(btCollisionWorld::RayResultCallback* obj, btCollisionWorld::LocalRayResult* rayResult, bool normalInWorldSpace);
	EXPORT btScalar btCollisionWorld_RayResultCallback_getClosestHitFraction(btCollisionWorld::RayResultCallback* obj);
	EXPORT short int btCollisionWorld_RayResultCallback_getCollisionFilterGroup(btCollisionWorld::RayResultCallback* obj);
	EXPORT short int btCollisionWorld_RayResultCallback_getCollisionFilterMask(btCollisionWorld::RayResultCallback* obj);
	EXPORT const btCollisionObject* btCollisionWorld_RayResultCallback_getCollisionObject(btCollisionWorld::RayResultCallback* obj);
	EXPORT unsigned int btCollisionWorld_RayResultCallback_getFlags(btCollisionWorld::RayResultCallback* obj);
	EXPORT bool btCollisionWorld_RayResultCallback_hasHit(btCollisionWorld::RayResultCallback* obj);
	EXPORT bool btCollisionWorld_RayResultCallback_needsCollision(btCollisionWorld::RayResultCallback* obj, btBroadphaseProxy* proxy0);
	EXPORT void btCollisionWorld_RayResultCallback_setClosestHitFraction(btCollisionWorld::RayResultCallback* obj, btScalar closestHitFraction);
	EXPORT void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(btCollisionWorld::RayResultCallback* obj, short int collisionFilterGroup);
	EXPORT void btCollisionWorld_RayResultCallback_setCollisionFilterMask(btCollisionWorld::RayResultCallback* obj, short int collisionFilterMask);
	EXPORT void btCollisionWorld_RayResultCallback_setCollisionObject(btCollisionWorld::RayResultCallback* obj, btCollisionObject* collisionObject);
	EXPORT void btCollisionWorld_RayResultCallback_setFlags(btCollisionWorld::RayResultCallback* obj, unsigned int flags);

	EXPORT btCollisionWorld::ClosestRayResultCallback* btCollisionWorld_ClosestRayResultCallback_new(btScalar* rayFromWorld, btScalar* rayToWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitNormalWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitPointWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayFromWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayToWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitNormalWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitPointWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayFromWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayToWorld);

	EXPORT void btCollisionWorld_delete(btCollisionWorld* obj);
	EXPORT void btCollisionWorld_addCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject, int collisionFilterGroup, int collisionFilterMask);
	EXPORT btBroadphaseInterface* btCollisionWorld_getBroadphase(btCollisionWorld* obj);
	EXPORT btAlignedObjectArray<btCollisionObject*>* btCollisionWorld_getCollisionObjectArray(btCollisionWorld* obj);
	EXPORT btOverlappingPairCache* btCollisionWorld_getPairCache(btCollisionWorld* obj);
	EXPORT btDispatcher* btCollisionWorld_getDispatcher(btCollisionWorld* obj);
	EXPORT int btCollisionWorld_getNumCollisionObjects(btCollisionWorld* obj);
	EXPORT void btCollisionWorld_rayTest(btCollisionWorld* obj, btScalar* rayFromWorld, btScalar* rayToWorld, btCollisionWorld::RayResultCallback* resultCallback);
	EXPORT void btCollisionWorld_removeCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject);
}
