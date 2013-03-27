#include "main.h"

typedef bool (__stdcall *pNeedsCollision)(btBroadphaseProxy* proxy0);
typedef bool (__stdcall *pAddSingleResult)(btManifoldPoint& cp,
		const btCollisionObjectWrapper* colObj0, int partId0, int index0,
		const btCollisionObjectWrapper* colObj1, int partId1, int index1);

class btCollisionWorld_ContactResultCallbackWrapper : public btCollisionWorld::ContactResultCallback
{
private:
	pNeedsCollision _needsCollisionCallback;
	pAddSingleResult _addSingleResultCallback;

public:
	btCollisionWorld_ContactResultCallbackWrapper(pAddSingleResult addSingleResultCallback, pNeedsCollision needsCollisionCallback);

	virtual bool needsCollision(btBroadphaseProxy* proxy0) const;
	virtual btScalar addSingleResult(btManifoldPoint& cp,
		const btCollisionObjectWrapper* colObj0, int partId0, int index0,
		const btCollisionObjectWrapper* colObj1, int partId1, int index1);

	virtual bool baseNeedsCollision(btBroadphaseProxy* proxy0) const;
};

extern "C"
{
	EXPORT btCollisionWorld::LocalShapeInfo* btCollisionWorld_LocalShapeInfo_new();
	EXPORT int btCollisionWorld_LocalShapeInfo_getShapePart(btCollisionWorld::LocalShapeInfo* obj);
	EXPORT int btCollisionWorld_LocalShapeInfo_getTriangleIndex(btCollisionWorld::LocalShapeInfo* obj);
	EXPORT void btCollisionWorld_LocalShapeInfo_setShapePart(btCollisionWorld::LocalShapeInfo* obj, int value);
	EXPORT void btCollisionWorld_LocalShapeInfo_setTriangleIndex(btCollisionWorld::LocalShapeInfo* obj, int value);
	EXPORT void btCollisionWorld_LocalShapeInfo_delete(btCollisionWorld::LocalShapeInfo* obj);

	EXPORT btCollisionWorld::LocalRayResult* btCollisionWorld_LocalRayResult_new(btCollisionObject* collisionObject, btCollisionWorld::LocalShapeInfo* localShapeInfo, btScalar* hitNormalLocal, btScalar hitFraction);
	EXPORT const btCollisionObject* btCollisionWorld_LocalRayResult_getCollisionObject(btCollisionWorld::LocalRayResult* obj);
	EXPORT btScalar btCollisionWorld_LocalRayResult_getHitFraction(btCollisionWorld::LocalRayResult* obj);
	EXPORT void btCollisionWorld_LocalRayResult_getHitNormalLocal(btCollisionWorld::LocalRayResult* obj, btScalar* value);
	EXPORT btCollisionWorld::LocalShapeInfo* btCollisionWorld_LocalRayResult_getLocalShapeInfo(btCollisionWorld::LocalRayResult* obj);
	EXPORT void btCollisionWorld_LocalRayResult_setCollisionObject(btCollisionWorld::LocalRayResult* obj, btCollisionObject* value);
	EXPORT void btCollisionWorld_LocalRayResult_setHitFraction(btCollisionWorld::LocalRayResult* obj, btScalar value);
	EXPORT void btCollisionWorld_LocalRayResult_setHitNormalLocal(btCollisionWorld::LocalRayResult* obj, btScalar* value);
	EXPORT void btCollisionWorld_LocalRayResult_setLocalShapeInfo(btCollisionWorld::LocalRayResult* obj, btCollisionWorld::LocalShapeInfo* value);
	EXPORT void btCollisionWorld_LocalRayResult_delete(btCollisionWorld::LocalRayResult* obj);

	EXPORT btScalar btCollisionWorld_RayResultCallback_addSingleResult(btCollisionWorld::RayResultCallback* obj, btCollisionWorld::LocalRayResult* rayResult, bool normalInWorldSpace);
	EXPORT btScalar btCollisionWorld_RayResultCallback_getClosestHitFraction(btCollisionWorld::RayResultCallback* obj);
	EXPORT short btCollisionWorld_RayResultCallback_getCollisionFilterGroup(btCollisionWorld::RayResultCallback* obj);
	EXPORT short btCollisionWorld_RayResultCallback_getCollisionFilterMask(btCollisionWorld::RayResultCallback* obj);
	EXPORT const btCollisionObject* btCollisionWorld_RayResultCallback_getCollisionObject(btCollisionWorld::RayResultCallback* obj);
	EXPORT unsigned int btCollisionWorld_RayResultCallback_getFlags(btCollisionWorld::RayResultCallback* obj);
	EXPORT bool btCollisionWorld_RayResultCallback_hasHit(btCollisionWorld::RayResultCallback* obj);
	EXPORT bool btCollisionWorld_RayResultCallback_needsCollision(btCollisionWorld::RayResultCallback* obj, btBroadphaseProxy* proxy0);
	EXPORT void btCollisionWorld_RayResultCallback_setClosestHitFraction(btCollisionWorld::RayResultCallback* obj, btScalar value);
	EXPORT void btCollisionWorld_RayResultCallback_setCollisionFilterGroup(btCollisionWorld::RayResultCallback* obj, short value);
	EXPORT void btCollisionWorld_RayResultCallback_setCollisionFilterMask(btCollisionWorld::RayResultCallback* obj, short value);
	EXPORT void btCollisionWorld_RayResultCallback_setCollisionObject(btCollisionWorld::RayResultCallback* obj, btCollisionObject* value);
	EXPORT void btCollisionWorld_RayResultCallback_setFlags(btCollisionWorld::RayResultCallback* obj, unsigned int value);
	EXPORT void btCollisionWorld_RayResultCallback_delete(btCollisionWorld::RayResultCallback* obj);

	EXPORT btCollisionWorld::ClosestRayResultCallback* btCollisionWorld_ClosestRayResultCallback_new(btScalar* rayFromWorld, btScalar* rayToWorld);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_getRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setHitNormalWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setHitPointWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setRayFromWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestRayResultCallback_setRayToWorld(btCollisionWorld::ClosestRayResultCallback* obj, btScalar* value);

	EXPORT btCollisionWorld::AllHitsRayResultCallback* btCollisionWorld_AllHitsRayResultCallback_new(btScalar* rayFromWorld, btScalar* rayToWorld);
	EXPORT btAlignedObjectArray<const btCollisionObject*>* btCollisionWorld_AllHitsRayResultCallback_getCollisionObjects(btCollisionWorld::AllHitsRayResultCallback* obj);
	EXPORT btAlignedObjectArray<btScalar>* btCollisionWorld_AllHitsRayResultCallback_getHitFractions(btCollisionWorld::AllHitsRayResultCallback* obj);
	EXPORT btAlignedObjectArray<btVector3>* btCollisionWorld_AllHitsRayResultCallback_getHitNormalWorld(btCollisionWorld::AllHitsRayResultCallback* obj);
	EXPORT btAlignedObjectArray<btVector3>* btCollisionWorld_AllHitsRayResultCallback_getHitPointWorld(btCollisionWorld::AllHitsRayResultCallback* obj);
	EXPORT void btCollisionWorld_AllHitsRayResultCallback_getRayFromWorld(btCollisionWorld::AllHitsRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_AllHitsRayResultCallback_getRayToWorld(btCollisionWorld::AllHitsRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_AllHitsRayResultCallback_setHitNormalWorld(btCollisionWorld::AllHitsRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_AllHitsRayResultCallback_setHitPointWorld(btCollisionWorld::AllHitsRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_AllHitsRayResultCallback_setRayFromWorld(btCollisionWorld::AllHitsRayResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_AllHitsRayResultCallback_setRayToWorld(btCollisionWorld::AllHitsRayResultCallback* obj, btScalar* value);

	EXPORT btCollisionWorld::LocalConvexResult* btCollisionWorld_LocalConvexResult_new(btCollisionObject* hitCollisionObject, btCollisionWorld::LocalShapeInfo* localShapeInfo, btScalar* hitNormalLocal, btScalar* hitPointLocal, btScalar hitFraction);
	EXPORT const btCollisionObject* btCollisionWorld_LocalConvexResult_getHitCollisionObject(btCollisionWorld::LocalConvexResult* obj);
	EXPORT btScalar btCollisionWorld_LocalConvexResult_getHitFraction(btCollisionWorld::LocalConvexResult* obj);
	EXPORT void btCollisionWorld_LocalConvexResult_getHitNormalLocal(btCollisionWorld::LocalConvexResult* obj, btScalar* value);
	EXPORT void btCollisionWorld_LocalConvexResult_getHitPointLocal(btCollisionWorld::LocalConvexResult* obj, btScalar* value);
	EXPORT btCollisionWorld::LocalShapeInfo* btCollisionWorld_LocalConvexResult_getLocalShapeInfo(btCollisionWorld::LocalConvexResult* obj);
	EXPORT void btCollisionWorld_LocalConvexResult_setHitCollisionObject(btCollisionWorld::LocalConvexResult* obj, btCollisionObject* value);
	EXPORT void btCollisionWorld_LocalConvexResult_setHitFraction(btCollisionWorld::LocalConvexResult* obj, btScalar value);
	EXPORT void btCollisionWorld_LocalConvexResult_setHitNormalLocal(btCollisionWorld::LocalConvexResult* obj, btScalar* value);
	EXPORT void btCollisionWorld_LocalConvexResult_setHitPointLocal(btCollisionWorld::LocalConvexResult* obj, btScalar* value);
	EXPORT void btCollisionWorld_LocalConvexResult_setLocalShapeInfo(btCollisionWorld::LocalConvexResult* obj, btCollisionWorld::LocalShapeInfo* value);
	EXPORT void btCollisionWorld_LocalConvexResult_delete(btCollisionWorld::LocalConvexResult* obj);

	EXPORT btScalar btCollisionWorld_ConvexResultCallback_addSingleResult(btCollisionWorld::ConvexResultCallback* obj, btCollisionWorld::LocalConvexResult* convexResult, bool normalInWorldSpace);
	EXPORT btScalar btCollisionWorld_ConvexResultCallback_getClosestHitFraction(btCollisionWorld::ConvexResultCallback* obj);
	EXPORT short btCollisionWorld_ConvexResultCallback_getCollisionFilterGroup(btCollisionWorld::ConvexResultCallback* obj);
	EXPORT short btCollisionWorld_ConvexResultCallback_getCollisionFilterMask(btCollisionWorld::ConvexResultCallback* obj);
	EXPORT bool btCollisionWorld_ConvexResultCallback_hasHit(btCollisionWorld::ConvexResultCallback* obj);
	EXPORT bool btCollisionWorld_ConvexResultCallback_needsCollision(btCollisionWorld::ConvexResultCallback* obj, btBroadphaseProxy* proxy0);
	EXPORT void btCollisionWorld_ConvexResultCallback_setClosestHitFraction(btCollisionWorld::ConvexResultCallback* obj, btScalar value);
	EXPORT void btCollisionWorld_ConvexResultCallback_setCollisionFilterGroup(btCollisionWorld::ConvexResultCallback* obj, short value);
	EXPORT void btCollisionWorld_ConvexResultCallback_setCollisionFilterMask(btCollisionWorld::ConvexResultCallback* obj, short value);
	EXPORT void btCollisionWorld_ConvexResultCallback_delete(btCollisionWorld::ConvexResultCallback* obj);

	EXPORT btCollisionWorld::ClosestConvexResultCallback* btCollisionWorld_ClosestConvexResultCallback_new(btScalar* convexFromWorld, btScalar* convexToWorld);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_getConvexFromWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_getConvexToWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT const btCollisionObject* btCollisionWorld_ClosestConvexResultCallback_getHitCollisionObject(btCollisionWorld::ClosestConvexResultCallback* obj);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_getHitNormalWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_getHitPointWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_setConvexFromWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_setConvexToWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_setHitCollisionObject(btCollisionWorld::ClosestConvexResultCallback* obj, btCollisionObject* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_setHitNormalWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value);
	EXPORT void btCollisionWorld_ClosestConvexResultCallback_setHitPointWorld(btCollisionWorld::ClosestConvexResultCallback* obj, btScalar* value);

	EXPORT btScalar btCollisionWorld_ContactResultCallback_addSingleResult(btCollisionWorld::ContactResultCallback* obj, btManifoldPoint* cp, btCollisionObjectWrapper* colObj0Wrap, int partId0, int index0, btCollisionObjectWrapper* colObj1Wrap, int partId1, int index1);
	EXPORT short btCollisionWorld_ContactResultCallback_getCollisionFilterGroup(btCollisionWorld::ContactResultCallback* obj);
	EXPORT short btCollisionWorld_ContactResultCallback_getCollisionFilterMask(btCollisionWorld::ContactResultCallback* obj);
	EXPORT bool btCollisionWorld_ContactResultCallback_needsCollision(btCollisionWorld::ContactResultCallback* obj, btBroadphaseProxy* proxy0);
	EXPORT void btCollisionWorld_ContactResultCallback_setCollisionFilterGroup(btCollisionWorld::ContactResultCallback* obj, short value);
	EXPORT void btCollisionWorld_ContactResultCallback_setCollisionFilterMask(btCollisionWorld::ContactResultCallback* obj, short value);
	EXPORT void btCollisionWorld_ContactResultCallback_delete(btCollisionWorld::ContactResultCallback* obj);

	EXPORT btCollisionWorld_ContactResultCallbackWrapper* btCollisionWorld_ContactResultCallbackWrapper_new(pAddSingleResult addSingleResultCallback, pNeedsCollision needsCollisionCallback);
	EXPORT bool btCollisionWorld_ContactResultCallbackWrapper_needsCollision(btCollisionWorld_ContactResultCallbackWrapper* obj, btBroadphaseProxy* proxy0);

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
