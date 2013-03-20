#include "btCollisionWorld_wrap.h"

btCollisionWorld::LocalShapeInfo* btCollisionWorld_LocalShapeInfo_new()
{
	return new btCollisionWorld::LocalShapeInfo();
}

void btCollisionWorld_LocalShapeInfo_delete(btCollisionWorld::LocalShapeInfo* obj)
{
	delete obj;
}

btCollisionWorld::LocalRayResult* btCollisionWorld_LocalRayResult_new(btCollisionObject* collisionObject, btCollisionWorld::LocalShapeInfo* localShapeInfo, btScalar* hitNormalLocal, btScalar hitFraction)
{
	VECTOR3_CONV(hitNormalLocal);
	return new btCollisionWorld::LocalRayResult(collisionObject, localShapeInfo, VECTOR3_USE(hitNormalLocal), hitFraction);
}

void btCollisionWorld_LocalRayResult_delete(btCollisionWorld::LocalRayResult* obj)
{
	delete obj;
}

btScalar btCollisionWorld_RayResultCallback_addSingleResult(btCollisionWorld::RayResultCallback* obj, btCollisionWorld::LocalRayResult* rayResult, bool normalInWorldSpace)
{
	return obj->addSingleResult(*rayResult, normalInWorldSpace);
}

btScalar btCollisionWorld_RayResultCallback_getClosestHitFraction(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_closestHitFraction;
}

short int btCollisionWorld_RayResultCallback_getCollisionFilterGroup(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_collisionFilterGroup;
}

short int btCollisionWorld_RayResultCallback_getCollisionFilterMask(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_collisionFilterMask;
}

const btCollisionObject* btCollisionWorld_RayResultCallback_getCollisionObject(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_collisionObject;
}

unsigned int btCollisionWorld_RayResultCallback_getFlags(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_flags;
}

bool btCollisionWorld_RayResultCallback_hasHit(btCollisionWorld::RayResultCallback* obj)
{
	return obj->hasHit();
}

bool btCollisionWorld_RayResultCallback_needsCollision(btCollisionWorld::RayResultCallback* obj, btBroadphaseProxy* proxy0)
{
	return obj->needsCollision(proxy0);
}

void btCollisionWorld_RayResultCallback_setClosestHitFraction(btCollisionWorld::RayResultCallback* obj, btScalar closestHitFraction)
{
	obj->m_closestHitFraction = closestHitFraction;
}

void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(btCollisionWorld::RayResultCallback* obj, short int collisionFilterGroup)
{
	obj->m_collisionFilterGroup = collisionFilterGroup;
}

void btCollisionWorld_RayResultCallback_setCollisionFilterMask(btCollisionWorld::RayResultCallback* obj, short int collisionFilterMask)
{
	obj->m_collisionFilterMask = collisionFilterMask;
}

void btCollisionWorld_RayResultCallback_setCollisionObject(btCollisionWorld::RayResultCallback* obj, btCollisionObject* collisionObject)
{
	obj->m_collisionObject = collisionObject;
}

void btCollisionWorld_RayResultCallback_setFlags(btCollisionWorld::RayResultCallback* obj, unsigned int flags)
{
	obj->m_flags = flags;
}

void btCollisionWorld_RayResultCallback_delete(btCollisionWorld::RayResultCallback* obj)
{
	ALIGNED_FREE(obj);
}


btCollisionWorld::ClosestRayResultCallback* btCollisionWorld_ClosestRayResultCallback_new(btScalar* rayFromWorld, btScalar* rayToWorld)
{
	VECTOR3_CONV(rayFromWorld);
	VECTOR3_CONV(rayToWorld);
	return ALIGNED_NEW(btCollisionWorld::ClosestRayResultCallback) (VECTOR3_USE(rayFromWorld), VECTOR3_USE(rayToWorld));
}

void btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitNormalWorld)
{
	VECTOR3_OUT(&obj->m_hitNormalWorld, hitNormalWorld);
}

void btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitPointWorld)
{
	VECTOR3_OUT(&obj->m_hitPointWorld, hitPointWorld);
}

void btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayFromWorld)
{
	VECTOR3_OUT(&obj->m_rayFromWorld, rayFromWorld);
}

void btCollisionWorld_ClosestRayResultCallback_getRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayToWorld)
{
	VECTOR3_OUT(&obj->m_rayToWorld, rayToWorld);
}

void btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitNormalWorld)
{
	VECTOR3_IN(hitNormalWorld, &obj->m_hitNormalWorld);
}

void btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* hitPointWorld)
{
	VECTOR3_IN(hitPointWorld, &obj->m_hitPointWorld);
}

void btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayFromWorld)
{
	VECTOR3_IN(rayFromWorld, &obj->m_rayFromWorld);
}

void btCollisionWorld_ClosestRayResultCallback_setRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* rayToWorld)
{
	VECTOR3_IN(rayToWorld, &obj->m_rayToWorld);
}

btCollisionWorld::AllHitsRayResultCallback* btCollisionWorld_AllHitsRayResultCallback_new(btScalar* rayFromWorld, btScalar* rayToWorld)
{
	VECTOR3_CONV(rayFromWorld);
	VECTOR3_CONV(rayToWorld);
	return ALIGNED_NEW(btCollisionWorld::AllHitsRayResultCallback) (VECTOR3_USE(rayFromWorld), VECTOR3_USE(rayToWorld));
}

btCollisionWorld::LocalConvexResult* btCollisionWorld_LocalConvexResult_new(btCollisionObject* hitCollisionObject, btCollisionWorld::LocalShapeInfo* localShapeInfo, btScalar* hitNormalLocal, btScalar* hitPointLocal, btScalar hitFraction)
{
	VECTOR3_CONV(hitNormalLocal);
	VECTOR3_CONV(hitPointLocal);
	return new btCollisionWorld::LocalConvexResult(hitCollisionObject, localShapeInfo, VECTOR3_USE(hitNormalLocal), VECTOR3_USE(hitPointLocal), hitFraction);
}

void btCollisionWorld_LocalConvexResult_delete(btCollisionWorld::LocalConvexResult* obj)
{
	delete obj;
}

bool btCollisionWorld_ConvexResultCallback_hasHit(btCollisionWorld::ConvexResultCallback* obj)
{
	return obj->hasHit();
}

bool btCollisionWorld_ConvexResultCallback_needsCollision(btCollisionWorld::ConvexResultCallback* obj, btBroadphaseProxy* proxy0)
{
	return obj->needsCollision(proxy0);
}

btScalar btCollisionWorld_ConvexResultCallback_addSingleResult(btCollisionWorld::ConvexResultCallback* obj, btCollisionWorld::LocalConvexResult* convexResult, bool normalInWorldSpace)
{
	return obj->addSingleResult(*convexResult, normalInWorldSpace);
}

void btCollisionWorld_ConvexResultCallback_delete(btCollisionWorld::ConvexResultCallback* obj)
{
	delete obj;
}

btCollisionWorld::ClosestConvexResultCallback* btCollisionWorld_ClosestConvexResultCallback_new(btScalar* convexFromWorld, btScalar* convexToWorld)
{
	VECTOR3_CONV(convexFromWorld);
	VECTOR3_CONV(convexToWorld);
	return ALIGNED_NEW(btCollisionWorld::ClosestConvexResultCallback) (VECTOR3_USE(convexFromWorld), VECTOR3_USE(convexToWorld));
}

bool btCollisionWorld_ContactResultCallback_needsCollision(btCollisionWorld::ContactResultCallback* obj, btBroadphaseProxy* proxy0)
{
	return obj->needsCollision(proxy0);
}

btScalar btCollisionWorld_ContactResultCallback_addSingleResult(btCollisionWorld::ContactResultCallback* obj, btManifoldPoint* cp, btCollisionObjectWrapper* colObj0Wrap, int partId0, int index0, btCollisionObjectWrapper* colObj1Wrap, int partId1, int index1)
{
	return obj->addSingleResult(*cp, colObj0Wrap, partId0, index0, colObj1Wrap, partId1, index1);
}

void btCollisionWorld_ContactResultCallback_delete(btCollisionWorld::ContactResultCallback* obj)
{
	delete obj;
}

btCollisionWorld* btCollisionWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* broadphasePairCache, btCollisionConfiguration* collisionConfiguration)
{
	return new btCollisionWorld(dispatcher, broadphasePairCache, collisionConfiguration);
}

void btCollisionWorld_addCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject, short collisionFilterGroup, short collisionFilterMask)
{
	obj->addCollisionObject(collisionObject, collisionFilterGroup, collisionFilterMask);
}

void btCollisionWorld_addCollisionObject2(btCollisionWorld* obj, btCollisionObject* collisionObject, short collisionFilterGroup)
{
	obj->addCollisionObject(collisionObject, collisionFilterGroup);
}

void btCollisionWorld_addCollisionObject3(btCollisionWorld* obj, btCollisionObject* collisionObject)
{
	obj->addCollisionObject(collisionObject);
}

void btCollisionWorld_computeOverlappingPairs(btCollisionWorld* obj)
{
	obj->computeOverlappingPairs();
}

void btCollisionWorld_contactPairTest(btCollisionWorld* obj, btCollisionObject* colObjA, btCollisionObject* colObjB, btCollisionWorld::ContactResultCallback* resultCallback)
{
	obj->contactPairTest(colObjA, colObjB, *resultCallback);
}

void btCollisionWorld_contactTest(btCollisionWorld* obj, btCollisionObject* colObj, btCollisionWorld::ContactResultCallback* resultCallback)
{
	obj->contactTest(colObj, *resultCallback);
}

void btCollisionWorld_convexSweepTest(btCollisionWorld* obj, btConvexShape* castShape, btScalar* from, btScalar* to, btCollisionWorld::ConvexResultCallback* resultCallback, btScalar allowedCcdPenetration)
{
	TRANSFORM_CONV(from);
	TRANSFORM_CONV(to);
	obj->convexSweepTest(castShape, TRANSFORM_USE(from), TRANSFORM_USE(to), *resultCallback, allowedCcdPenetration);
}

void btCollisionWorld_convexSweepTest2(btCollisionWorld* obj, btConvexShape* castShape, btScalar* from, btScalar* to, btCollisionWorld::ConvexResultCallback* resultCallback)
{
	TRANSFORM_CONV(from);
	TRANSFORM_CONV(to);
	obj->convexSweepTest(castShape, TRANSFORM_USE(from), TRANSFORM_USE(to), *resultCallback);
}

void btCollisionWorld_debugDrawObject(btCollisionWorld* obj, btScalar* worldTransform, btCollisionShape* shape, btScalar* color)
{
	TRANSFORM_CONV(worldTransform);
	VECTOR3_CONV(color);
	obj->debugDrawObject(TRANSFORM_USE(worldTransform), shape, VECTOR3_USE(color));
}

void btCollisionWorld_debugDrawWorld(btCollisionWorld* obj)
{
	obj->debugDrawWorld();
}

btBroadphaseInterface* btCollisionWorld_getBroadphase(btCollisionWorld* obj)
{
	return obj->getBroadphase();
}

btCollisionObjectArray* btCollisionWorld_getCollisionObjectArray(btCollisionWorld* obj)
{
	return &obj->getCollisionObjectArray();
}

btIDebugDraw* btCollisionWorld_getDebugDrawer(btCollisionWorld* obj)
{
	return obj->getDebugDrawer();
}

btDispatcher* btCollisionWorld_getDispatcher(btCollisionWorld* obj)
{
	return obj->getDispatcher();
}

btDispatcherInfo* btCollisionWorld_getDispatchInfo(btCollisionWorld* obj)
{
	return &obj->getDispatchInfo();
}

bool btCollisionWorld_getForceUpdateAllAabbs(btCollisionWorld* obj)
{
	return obj->getForceUpdateAllAabbs();
}

int btCollisionWorld_getNumCollisionObjects(btCollisionWorld* obj)
{
	return obj->getNumCollisionObjects();
}

btOverlappingPairCache* btCollisionWorld_getPairCache(btCollisionWorld* obj)
{
	return obj->getPairCache();
}

void btCollisionWorld_objectQuerySingle(btConvexShape* castShape, btScalar* rayFromTrans, btScalar* rayToTrans, btCollisionObject* collisionObject, btCollisionShape* collisionShape, btScalar* colObjWorldTransform, btCollisionWorld::ConvexResultCallback* resultCallback, btScalar allowedPenetration)
{
	TRANSFORM_CONV(rayFromTrans);
	TRANSFORM_CONV(rayToTrans);
	TRANSFORM_CONV(colObjWorldTransform);
	btCollisionWorld::objectQuerySingle(castShape, TRANSFORM_USE(rayFromTrans), TRANSFORM_USE(rayToTrans), collisionObject, collisionShape, TRANSFORM_USE(colObjWorldTransform), *resultCallback, allowedPenetration);
}

void btCollisionWorld_objectQuerySingleInternal(btConvexShape* castShape, btScalar* convexFromTrans, btScalar* convexToTrans, btCollisionObjectWrapper* colObjWrap, btCollisionWorld::ConvexResultCallback* resultCallback, btScalar allowedPenetration)
{
	TRANSFORM_CONV(convexFromTrans);
	TRANSFORM_CONV(convexToTrans);
	btCollisionWorld::objectQuerySingleInternal(castShape, TRANSFORM_USE(convexFromTrans), TRANSFORM_USE(convexToTrans), colObjWrap, *resultCallback, allowedPenetration);
}

void btCollisionWorld_performDiscreteCollisionDetection(btCollisionWorld* obj)
{
	obj->performDiscreteCollisionDetection();
}

void btCollisionWorld_rayTest(btCollisionWorld* obj, btScalar* rayFromWorld, btScalar* rayToWorld, btCollisionWorld::RayResultCallback* resultCallback)
{
	VECTOR3_CONV(rayFromWorld);
	VECTOR3_CONV(rayToWorld);
	obj->rayTest(VECTOR3_USE(rayFromWorld), VECTOR3_USE(rayToWorld), *resultCallback);
}

void btCollisionWorld_rayTestSingle(btScalar* rayFromTrans, btScalar* rayToTrans, btCollisionObject* collisionObject, btCollisionShape* collisionShape, btScalar* colObjWorldTransform, btCollisionWorld::RayResultCallback* resultCallback)
{
	TRANSFORM_CONV(rayFromTrans);
	TRANSFORM_CONV(rayToTrans);
	TRANSFORM_CONV(colObjWorldTransform);
	btCollisionWorld::rayTestSingle(TRANSFORM_USE(rayFromTrans), TRANSFORM_USE(rayToTrans), collisionObject, collisionShape, TRANSFORM_USE(colObjWorldTransform), *resultCallback);
}

void btCollisionWorld_rayTestSingleInternal(btScalar* rayFromTrans, btScalar* rayToTrans, btCollisionObjectWrapper* collisionObjectWrap, btCollisionWorld::RayResultCallback* resultCallback)
{
	TRANSFORM_CONV(rayFromTrans);
	TRANSFORM_CONV(rayToTrans);
	btCollisionWorld::rayTestSingleInternal(TRANSFORM_USE(rayFromTrans), TRANSFORM_USE(rayToTrans), collisionObjectWrap, *resultCallback);
}

void btCollisionWorld_removeCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject)
{
	obj->removeCollisionObject(collisionObject);
}

void btCollisionWorld_serialize(btCollisionWorld* obj, btSerializer* serializer)
{
	obj->serialize(serializer);
}

void btCollisionWorld_setBroadphase(btCollisionWorld* obj, btBroadphaseInterface* pairCache)
{
	obj->setBroadphase(pairCache);
}

void btCollisionWorld_setDebugDrawer(btCollisionWorld* obj, btIDebugDraw* debugDrawer)
{
	obj->setDebugDrawer(debugDrawer);
}

void btCollisionWorld_setForceUpdateAllAabbs(btCollisionWorld* obj, bool forceUpdateAllAabbs)
{
	obj->setForceUpdateAllAabbs(forceUpdateAllAabbs);
}

void btCollisionWorld_updateAabbs(btCollisionWorld* obj)
{
	obj->updateAabbs();
}

void btCollisionWorld_updateSingleAabb(btCollisionWorld* obj, btCollisionObject* colObj)
{
	obj->updateSingleAabb(colObj);
}

void btCollisionWorld_delete(btCollisionWorld* obj)
{
	delete obj;
}
