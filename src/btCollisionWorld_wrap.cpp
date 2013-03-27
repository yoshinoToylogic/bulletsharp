#include "btCollisionWorld_wrap.h"

btCollisionWorld_ContactResultCallbackWrapper::btCollisionWorld_ContactResultCallbackWrapper(pAddSingleResult addSingleResultCallback, pNeedsCollision needsCollisionCallback)
{
	_addSingleResultCallback = addSingleResultCallback;
	_needsCollisionCallback = needsCollisionCallback;
}

bool btCollisionWorld_ContactResultCallbackWrapper::needsCollision(btBroadphaseProxy* proxy0) const
{
	return _needsCollisionCallback(proxy0);
}

btScalar btCollisionWorld_ContactResultCallbackWrapper::addSingleResult(btManifoldPoint& cp,
	const btCollisionObjectWrapper* colObj0Wrap, int partId0, int index0,
	const btCollisionObjectWrapper* colObj1Wrap, int partId1, int index1)
{
	return _addSingleResultCallback(cp, colObj0Wrap, partId0, index0, colObj1Wrap, partId1, index1);
}

bool btCollisionWorld_ContactResultCallbackWrapper::baseNeedsCollision(btBroadphaseProxy* proxy0) const
{
	return btCollisionWorld::ContactResultCallback::needsCollision(proxy0);
}


btCollisionWorld::LocalShapeInfo* btCollisionWorld_LocalShapeInfo_new()
{
	return new btCollisionWorld::LocalShapeInfo();
}

int btCollisionWorld_LocalShapeInfo_getShapePart(btCollisionWorld::LocalShapeInfo* obj)
{
	return obj->m_shapePart;
}

int btCollisionWorld_LocalShapeInfo_getTriangleIndex(btCollisionWorld::LocalShapeInfo* obj)
{
	return obj->m_triangleIndex;
}

void btCollisionWorld_LocalShapeInfo_setShapePart(btCollisionWorld::LocalShapeInfo* obj, int value)
{
	obj->m_shapePart = value;
}

void btCollisionWorld_LocalShapeInfo_setTriangleIndex(btCollisionWorld::LocalShapeInfo* obj, int value)
{
	obj->m_triangleIndex = value;
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

const btCollisionObject* btCollisionWorld_LocalRayResult_getCollisionObject(btCollisionWorld::LocalRayResult* obj)
{
	return obj->m_collisionObject;
}

btScalar btCollisionWorld_LocalRayResult_getHitFraction(btCollisionWorld::LocalRayResult* obj)
{
	return obj->m_hitFraction;
}

void btCollisionWorld_LocalRayResult_getHitNormalLocal(btCollisionWorld::LocalRayResult* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hitNormalLocal, value);
}

btCollisionWorld::LocalShapeInfo* btCollisionWorld_LocalRayResult_getLocalShapeInfo(btCollisionWorld::LocalRayResult* obj)
{
	return obj->m_localShapeInfo;
}

void btCollisionWorld_LocalRayResult_setCollisionObject(btCollisionWorld::LocalRayResult* obj, btCollisionObject* value)
{
	obj->m_collisionObject = value;
}

void btCollisionWorld_LocalRayResult_setHitFraction(btCollisionWorld::LocalRayResult* obj, btScalar value)
{
	obj->m_hitFraction = value;
}

void btCollisionWorld_LocalRayResult_setHitNormalLocal(btCollisionWorld::LocalRayResult* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hitNormalLocal);
}

void btCollisionWorld_LocalRayResult_setLocalShapeInfo(btCollisionWorld::LocalRayResult* obj, btCollisionWorld::LocalShapeInfo* value)
{
	obj->m_localShapeInfo = value;
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

short btCollisionWorld_RayResultCallback_getCollisionFilterGroup(btCollisionWorld::RayResultCallback* obj)
{
	return obj->m_collisionFilterGroup;
}

short btCollisionWorld_RayResultCallback_getCollisionFilterMask(btCollisionWorld::RayResultCallback* obj)
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

void btCollisionWorld_RayResultCallback_setClosestHitFraction(btCollisionWorld::RayResultCallback* obj, btScalar value)
{
	obj->m_closestHitFraction = value;
}

void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(btCollisionWorld::RayResultCallback* obj, short value)
{
	obj->m_collisionFilterGroup = value;
}

void btCollisionWorld_RayResultCallback_setCollisionFilterMask(btCollisionWorld::RayResultCallback* obj, short value)
{
	obj->m_collisionFilterMask = value;
}

void btCollisionWorld_RayResultCallback_setCollisionObject(btCollisionWorld::RayResultCallback* obj, btCollisionObject* value)
{
	obj->m_collisionObject = value;
}

void btCollisionWorld_RayResultCallback_setFlags(btCollisionWorld::RayResultCallback* obj, unsigned int value)
{
	obj->m_flags = value;
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

void btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hitNormalWorld, value);
}

void btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hitPointWorld, value);
}

void btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_rayFromWorld, value);
}

void btCollisionWorld_ClosestRayResultCallback_getRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_rayToWorld, value);
}

void btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hitNormalWorld);
}

void btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hitPointWorld);
}

void btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_rayFromWorld);
}

void btCollisionWorld_ClosestRayResultCallback_setRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_rayToWorld);
}

btCollisionWorld::AllHitsRayResultCallback* btCollisionWorld_AllHitsRayResultCallback_new(btScalar* rayFromWorld, btScalar* rayToWorld)
{
	VECTOR3_CONV(rayFromWorld);
	VECTOR3_CONV(rayToWorld);
	return new btCollisionWorld::AllHitsRayResultCallback(VECTOR3_USE(rayFromWorld), VECTOR3_USE(rayToWorld));
}

btAlignedObjectArray<const btCollisionObject*>* btCollisionWorld_AllHitsRayResultCallback_getCollisionObjects(btCollisionWorld::AllHitsRayResultCallback* obj)
{
	return &obj->m_collisionObjects;
}

btAlignedObjectArray<btScalar>* btCollisionWorld_AllHitsRayResultCallback_getHitFractions(btCollisionWorld::AllHitsRayResultCallback* obj)
{
	return &obj->m_hitFractions;
}

btAlignedObjectArray<btVector3>* btCollisionWorld_AllHitsRayResultCallback_getHitNormalWorld(btCollisionWorld::AllHitsRayResultCallback* obj)
{
	return &obj->m_hitNormalWorld;
}

btAlignedObjectArray<btVector3>* btCollisionWorld_AllHitsRayResultCallback_getHitPointWorld(btCollisionWorld::AllHitsRayResultCallback* obj)
{
	return &obj->m_hitPointWorld;
}

void btCollisionWorld_AllHitsRayResultCallback_getRayFromWorld(btCollisionWorld::AllHitsRayResultCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_rayFromWorld, value);
}

void btCollisionWorld_AllHitsRayResultCallback_getRayToWorld(btCollisionWorld::AllHitsRayResultCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_rayToWorld, value);
}

btAlignedObjectArray<btVector3>* btCollisionWorld_AllHitsRayResultCallback_setHitNormalWorld(btCollisionWorld::AllHitsRayResultCallback* obj)
{
	return &obj->m_hitNormalWorld;
}

btAlignedObjectArray<btVector3>* btCollisionWorld_AllHitsRayResultCallback_setHitPointWorld(btCollisionWorld::AllHitsRayResultCallback* obj)
{
	return &obj->m_hitPointWorld;
}

void btCollisionWorld_AllHitsRayResultCallback_setRayFromWorld(btCollisionWorld::AllHitsRayResultCallback* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_rayFromWorld);
}

void btCollisionWorld_AllHitsRayResultCallback_setRayToWorld(btCollisionWorld::AllHitsRayResultCallback* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_rayToWorld);
}

btCollisionWorld::LocalConvexResult* btCollisionWorld_LocalConvexResult_new(btCollisionObject* hitCollisionObject, btCollisionWorld::LocalShapeInfo* localShapeInfo, btScalar* hitNormalLocal, btScalar* hitPointLocal, btScalar hitFraction)
{
	VECTOR3_CONV(hitNormalLocal);
	VECTOR3_CONV(hitPointLocal);
	return new btCollisionWorld::LocalConvexResult(hitCollisionObject, localShapeInfo, VECTOR3_USE(hitNormalLocal), VECTOR3_USE(hitPointLocal), hitFraction);
}

const btCollisionObject* btCollisionWorld_LocalConvexResult_getHitCollisionObject(btCollisionWorld::LocalConvexResult* obj)
{
	return obj->m_hitCollisionObject;
}

btScalar btCollisionWorld_LocalConvexResult_getHitFraction(btCollisionWorld::LocalConvexResult* obj)
{
	return obj->m_hitFraction;
}

void btCollisionWorld_LocalConvexResult_getHitNormalLocal(btCollisionWorld::LocalConvexResult* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hitNormalLocal, value);
}

void btCollisionWorld_LocalConvexResult_getHitPointLocal(btCollisionWorld::LocalConvexResult* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hitPointLocal, value);
}

btCollisionWorld::LocalShapeInfo* btCollisionWorld_LocalConvexResult_getLocalShapeInfo(btCollisionWorld::LocalConvexResult* obj)
{
	return obj->m_localShapeInfo;
}

void btCollisionWorld_LocalConvexResult_setHitCollisionObject(btCollisionWorld::LocalConvexResult* obj, btCollisionObject* value)
{
	obj->m_hitCollisionObject = value;
}

void btCollisionWorld_LocalConvexResult_setHitFraction(btCollisionWorld::LocalConvexResult* obj, btScalar value)
{
	obj->m_hitFraction = value;
}

void btCollisionWorld_LocalConvexResult_setHitNormalLocal(btCollisionWorld::LocalConvexResult* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hitNormalLocal);
}

void btCollisionWorld_LocalConvexResult_setHitPointLocal(btCollisionWorld::LocalConvexResult* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hitPointLocal);
}

void btCollisionWorld_LocalConvexResult_setLocalShapeInfo(btCollisionWorld::LocalConvexResult* obj, btCollisionWorld::LocalShapeInfo* value)
{
	obj->m_localShapeInfo = value;
}

void btCollisionWorld_LocalConvexResult_delete(btCollisionWorld::LocalConvexResult* obj)
{
	delete obj;
}

btScalar btCollisionWorld_ConvexResultCallback_addSingleResult(btCollisionWorld::ConvexResultCallback* obj, btCollisionWorld::LocalConvexResult* convexResult, bool normalInWorldSpace)
{
	return obj->addSingleResult(*convexResult, normalInWorldSpace);
}

btScalar btCollisionWorld_ConvexResultCallback_getClosestHitFraction(btCollisionWorld::ConvexResultCallback* obj)
{
	return obj->m_closestHitFraction;
}

short btCollisionWorld_ConvexResultCallback_getCollisionFilterGroup(btCollisionWorld::ConvexResultCallback* obj)
{
	return obj->m_collisionFilterGroup;
}

short btCollisionWorld_ConvexResultCallback_getCollisionFilterMask(btCollisionWorld::ConvexResultCallback* obj)
{
	return obj->m_collisionFilterMask;
}

bool btCollisionWorld_ConvexResultCallback_hasHit(btCollisionWorld::ConvexResultCallback* obj)
{
	return obj->hasHit();
}

bool btCollisionWorld_ConvexResultCallback_needsCollision(btCollisionWorld::ConvexResultCallback* obj, btBroadphaseProxy* proxy0)
{
	return obj->needsCollision(proxy0);
}

void btCollisionWorld_ConvexResultCallback_setClosestHitFraction(btCollisionWorld::ConvexResultCallback* obj, btScalar value)
{
	obj->m_closestHitFraction = value;
}

void btCollisionWorld_ConvexResultCallback_setCollisionFilterGroup(btCollisionWorld::ConvexResultCallback* obj, short value)
{
	obj->m_collisionFilterGroup = value;
}

void btCollisionWorld_ConvexResultCallback_setCollisionFilterMask(btCollisionWorld::ConvexResultCallback* obj, short value)
{
	obj->m_collisionFilterMask = value;
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

void btCollisionWorld_ClosestConvexResultCallback_getConvexFromWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_convexFromWorld, value);
}

void btCollisionWorld_ClosestConvexResultCallback_getConvexToWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_convexToWorld, value);
}

const btCollisionObject* btCollisionWorld_ClosestConvexResultCallback_getHitCollisionObject(btCollisionWorld::ClosestConvexResultCallback* obj)
{
	return obj->m_hitCollisionObject;
}

void btCollisionWorld_ClosestConvexResultCallback_getHitNormalWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hitNormalWorld, value);
}

void btCollisionWorld_ClosestConvexResultCallback_getHitPointWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_hitPointWorld, value);
}

void btCollisionWorld_ClosestConvexResultCallback_setConvexFromWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_convexFromWorld);
}

void btCollisionWorld_ClosestConvexResultCallback_setConvexToWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_convexToWorld);
}

void btCollisionWorld_ClosestConvexResultCallback_setHitCollisionObject(btCollisionWorld::ClosestConvexResultCallback* obj, btCollisionObject* value)
{
	obj->m_hitCollisionObject = value;
}

void btCollisionWorld_ClosestConvexResultCallback_setHitNormalWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hitNormalWorld);
}

void btCollisionWorld_ClosestConvexResultCallback_setHitPointWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_hitPointWorld);
}

btScalar btCollisionWorld_ContactResultCallback_addSingleResult(btCollisionWorld::ContactResultCallback* obj, btManifoldPoint* cp, btCollisionObjectWrapper* colObj0Wrap, int partId0, int index0, btCollisionObjectWrapper* colObj1Wrap, int partId1, int index1)
{
	return obj->addSingleResult(*cp, colObj0Wrap, partId0, index0, colObj1Wrap, partId1, index1);
}

short btCollisionWorld_ContactResultCallback_getCollisionFilterGroup(btCollisionWorld::ContactResultCallback* obj)
{
	return obj->m_collisionFilterGroup;
}

short btCollisionWorld_ContactResultCallback_getCollisionFilterMask(btCollisionWorld::ContactResultCallback* obj)
{
	return obj->m_collisionFilterMask;
}

bool btCollisionWorld_ContactResultCallback_needsCollision(btCollisionWorld::ContactResultCallback* obj, btBroadphaseProxy* proxy0)
{
	return obj->needsCollision(proxy0);
}

void btCollisionWorld_ContactResultCallback_setCollisionFilterGroup(btCollisionWorld::ContactResultCallback* obj, short value)
{
	obj->m_collisionFilterGroup = value;
}

void btCollisionWorld_ContactResultCallback_setCollisionFilterMask(btCollisionWorld::ContactResultCallback* obj, short value)
{
	obj->m_collisionFilterMask = value;
}

void btCollisionWorld_ContactResultCallback_delete(btCollisionWorld::ContactResultCallback* obj)
{
	delete obj;
}

btCollisionWorld_ContactResultCallbackWrapper* btCollisionWorld_ContactResultCallbackWrapper_new(pAddSingleResult addSingleResultCallback, pNeedsCollision needsCollisionCallback)
{
	return new btCollisionWorld_ContactResultCallbackWrapper(addSingleResultCallback, needsCollisionCallback);
}

bool btCollisionWorld_ContactResultCallbackWrapper_needsCollision(btCollisionWorld_ContactResultCallbackWrapper* obj, btBroadphaseProxy* proxy0)
{
	return obj->baseNeedsCollision(proxy0);
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
