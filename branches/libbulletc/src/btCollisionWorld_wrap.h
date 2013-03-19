#include "main.h"

extern "C"
{
	EXPORT btCollisionWorld::LocalShapeInfo* btCollisionWorld_LocalShapeInfo_new();
	EXPORT void btCollisionWorld_LocalShapeInfo_delete(btCollisionWorld::LocalShapeInfo* obj);

	EXPORT btCollisionWorld::LocalRayResult* btCollisionWorld_LocalRayResult_new(btCollisionObject* collisionObject, btCollisionWorld::LocalShapeInfo* localShapeInfo, btScalar* hitNormalLocal, btScalar hitFraction);
	EXPORT void btCollisionWorld_LocalRayResult_delete(btCollisionWorld::LocalRayResult* obj);

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
	EXPORT void btCollisionWorld_RayResultCallback_delete(btCollisionWorld::RayResultCallback* obj);

	EXPORT btCollisionWorld::ClosestRayResultCallback* btCollisionWorld_ClosestRayResultCallback_new(btScalar* rayFromWorld, btScalar* rayToWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitNormalWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitPointWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayFromWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayToWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitNormalWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitPointWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayFromWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayToWorld);

	EXPORT btCollisionWorld::AllHitsRayResultCallback* btCollisionWorld_AllHitsRayResultCallback_new(btScalar* rayFromWorld, btScalar* rayToWorld);

	EXPORT btCollisionWorld::LocalConvexResult* btCollisionWorld_LocalConvexResult_new(btCollisionObject* hitCollisionObject, btCollisionWorld::LocalShapeInfo* localShapeInfo, btScalar* hitNormalLocal, btScalar* hitPointLocal, btScalar hitFraction);
	EXPORT void btCollisionWorld_LocalConvexResult_delete(btCollisionWorld::LocalConvexResult* obj);

	EXPORT bool btCollisionWorld_ConvexResultCallback_hasHit(btCollisionWorld::ConvexResultCallback* obj);
	EXPORT bool btCollisionWorld_ConvexResultCallback_needsCollision(btCollisionWorld::ConvexResultCallback* obj, btBroadphaseProxy* proxy0);
	EXPORT btScalar btCollisionWorld_ConvexResultCallback_addSingleResult(btCollisionWorld::ConvexResultCallback* obj, btCollisionWorld::LocalConvexResult* convexResult, bool normalInWorldSpace);
	EXPORT void btCollisionWorld_ConvexResultCallback_delete(btCollisionWorld::ConvexResultCallback* obj);

	EXPORT btCollisionWorld::ClosestConvexResultCallback* btCollisionWorld_ClosestConvexResultCallback_new(btScalar* convexFromWorld, btScalar* convexToWorld);

	EXPORT bool btCollisionWorld_ContactResultCallback_needsCollision(btCollisionWorld::ContactResultCallback* obj, btBroadphaseProxy* proxy0);
	EXPORT btScalar btCollisionWorld_ContactResultCallback_addSingleResult(btCollisionWorld::ContactResultCallback* obj, btManifoldPoint* cp, btCollisionObjectWrapper* colObj0Wrap, int partId0, int index0, btCollisionObjectWrapper* colObj1Wrap, int partId1, int index1);
	EXPORT void btCollisionWorld_ContactResultCallback_delete(btCollisionWorld::ContactResultCallback* obj);
	EXPORT btCollisionWorld* btCollisionWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* broadphasePairCache, btCollisionConfiguration* collisionConfiguration);
	EXPORT void btCollisionWorld_addCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject, short collisionFilterGroup, short collisionFilterMask);
	EXPORT void btCollisionWorld_addCollisionObject2(btCollisionWorld* obj, btCollisionObject* collisionObject, short collisionFilterGroup);
	EXPORT void btCollisionWorld_addCollisionObject3(btCollisionWorld* obj, btCollisionObject* collisionObject);
	EXPORT void btCollisionWorld_computeOverlappingPairs(btCollisionWorld* obj);
	EXPORT void btCollisionWorld_contactPairTest(btCollisionWorld* obj, btCollisionObject* colObjA, btCollisionObject* colObjB, btCollisionWorld::ContactResultCallback* resultCallback);
	EXPORT void btCollisionWorld_contactTest(btCollisionWorld* obj, btCollisionObject* colObj, btCollisionWorld::ContactResultCallback* resultCallback);
	EXPORT void btCollisionWorld_convexSweepTest(btCollisionWorld* obj, btConvexShape* castShape, btScalar* from, btScalar* to, btCollisionWorld::ConvexResultCallback* resultCallback, btScalar allowedCcdPenetration);
	EXPORT void btCollisionWorld_convexSweepTest2(btCollisionWorld* obj, btConvexShape* castShape, btScalar* from, btScalar* to, btCollisionWorld::ConvexResultCallback* resultCallback);
	EXPORT void btCollisionWorld_debugDrawObject(btCollisionWorld* obj, btScalar* worldTransform, btCollisionShape* shape, btScalar* color);
	EXPORT void btCollisionWorld_debugDrawWorld(btCollisionWorld* obj);
	EXPORT btBroadphaseInterface* btCollisionWorld_getBroadphase(btCollisionWorld* obj);
	EXPORT btCollisionObjectArray* btCollisionWorld_getCollisionObjectArray(btCollisionWorld* obj);
	EXPORT btIDebugDraw* btCollisionWorld_getDebugDrawer(btCollisionWorld* obj);
	EXPORT btDispatcher* btCollisionWorld_getDispatcher(btCollisionWorld* obj);
	EXPORT btDispatcherInfo* btCollisionWorld_getDispatchInfo(btCollisionWorld* obj);
	EXPORT bool btCollisionWorld_getForceUpdateAllAabbs(btCollisionWorld* obj);
	EXPORT int btCollisionWorld_getNumCollisionObjects(btCollisionWorld* obj);
	EXPORT btOverlappingPairCache* btCollisionWorld_getPairCache(btCollisionWorld* obj);
	EXPORT void btCollisionWorld_objectQuerySingle(btConvexShape* castShape, btScalar* rayFromTrans, btScalar* rayToTrans, btCollisionObject* collisionObject, btCollisionShape* collisionShape, btScalar* colObjWorldTransform, btCollisionWorld::ConvexResultCallback* resultCallback, btScalar allowedPenetration);
	EXPORT void btCollisionWorld_objectQuerySingleInternal(btConvexShape* castShape, btScalar* convexFromTrans, btScalar* convexToTrans, btCollisionObjectWrapper* colObjWrap, btCollisionWorld::ConvexResultCallback* resultCallback, btScalar allowedPenetration);
	EXPORT void btCollisionWorld_performDiscreteCollisionDetection(btCollisionWorld* obj);
	EXPORT void btCollisionWorld_rayTest(btCollisionWorld* obj, btScalar* rayFromWorld, btScalar* rayToWorld, btCollisionWorld::RayResultCallback* resultCallback);
	EXPORT void btCollisionWorld_rayTestSingle(btScalar* rayFromTrans, btScalar* rayToTrans, btCollisionObject* collisionObject, btCollisionShape* collisionShape, btScalar* colObjWorldTransform, btCollisionWorld::RayResultCallback* resultCallback);
	EXPORT void btCollisionWorld_rayTestSingleInternal(btScalar* rayFromTrans, btScalar* rayToTrans, btCollisionObjectWrapper* collisionObjectWrap, btCollisionWorld::RayResultCallback* resultCallback);
	EXPORT void btCollisionWorld_removeCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject);
	EXPORT void btCollisionWorld_serialize(btCollisionWorld* obj, btSerializer* serializer);
	EXPORT void btCollisionWorld_setBroadphase(btCollisionWorld* obj, btBroadphaseInterface* pairCache);
	EXPORT void btCollisionWorld_setDebugDrawer(btCollisionWorld* obj, btIDebugDraw* debugDrawer);
	EXPORT void btCollisionWorld_setForceUpdateAllAabbs(btCollisionWorld* obj, bool forceUpdateAllAabbs);
	EXPORT void btCollisionWorld_updateAabbs(btCollisionWorld* obj);
	EXPORT void btCollisionWorld_updateSingleAabb(btCollisionWorld* obj, btCollisionObject* colObj);
	EXPORT void btCollisionWorld_delete(btCollisionWorld* obj);
}
