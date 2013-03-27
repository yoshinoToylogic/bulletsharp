#include "main.h"

#include <BulletCollision/CollisionDispatch/btGhostObject.h>

extern "C"
{
	EXPORT btGhostObject* btGhostObject_new();
	EXPORT void btGhostObject_addOverlappingObjectInternal(btGhostObject* obj, btBroadphaseProxy* otherProxy, btBroadphaseProxy* thisProxy);
	EXPORT void btGhostObject_addOverlappingObjectInternal2(btGhostObject* obj, btBroadphaseProxy* otherProxy);
	EXPORT void btGhostObject_convexSweepTest(btGhostObject* obj, btConvexShape* castShape, btScalar* convexFromWorld, btScalar* convexToWorld, btCollisionWorld::ConvexResultCallback* resultCallback, btScalar allowedCcdPenetration);
	EXPORT void btGhostObject_convexSweepTest2(btGhostObject* obj, btConvexShape* castShape, btScalar* convexFromWorld, btScalar* convexToWorld, btCollisionWorld::ConvexResultCallback* resultCallback);
	EXPORT int btGhostObject_getNumOverlappingObjects(btGhostObject* obj);
	EXPORT btCollisionObject* btGhostObject_getOverlappingObject(btGhostObject* obj, int index);
	EXPORT btAlignedObjectArray<btCollisionObject*>* btGhostObject_getOverlappingPairs(btGhostObject* obj);
	EXPORT void btGhostObject_getOverlappingPairs2(btGhostObject* obj);
	EXPORT void btGhostObject_rayTest(btGhostObject* obj, btScalar* rayFromWorld, btScalar* rayToWorld, btCollisionWorld::RayResultCallback* resultCallback);
	EXPORT void btGhostObject_removeOverlappingObjectInternal(btGhostObject* obj, btBroadphaseProxy* otherProxy, btDispatcher* dispatcher, btBroadphaseProxy* thisProxy);
	EXPORT void btGhostObject_removeOverlappingObjectInternal2(btGhostObject* obj, btBroadphaseProxy* otherProxy, btDispatcher* dispatcher);
	EXPORT btGhostObject* btGhostObject_upcast(btCollisionObject* colObj);

	EXPORT btPairCachingGhostObject* btPairCachingGhostObject_new();
	EXPORT btHashedOverlappingPairCache* btPairCachingGhostObject_getOverlappingPairCache(btPairCachingGhostObject* obj);

	EXPORT btGhostPairCallback* btGhostPairCallback_new();
}
