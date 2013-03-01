#include "btCollisionWorld_wrap.h"

void btCollisionWorld_delete(btCollisionWorld* obj)
{
	delete obj;
}

void btCollisionWorld_addCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject, int collisionFilterGroup, int collisionFilterMask)
{
	obj->addCollisionObject(collisionObject, collisionFilterGroup, collisionFilterMask);
}

btBroadphaseInterface* btCollisionWorld_getBroadphase(btCollisionWorld* obj)
{
	return obj->getBroadphase();
}

btAlignedObjectArray<btCollisionObject*>* btCollisionWorld_getCollisionObjectArray(btCollisionWorld* obj)
{
	return &obj->getCollisionObjectArray();
}

btOverlappingPairCache* btCollisionWorld_getPairCache(btCollisionWorld* obj)
{
	return obj->getPairCache();
}

btDispatcher* btCollisionWorld_getDispatcher(btCollisionWorld* obj)
{
	return obj->getDispatcher();
}

int btCollisionWorld_getNumCollisionObjects(btCollisionWorld* obj)
{
	return obj->getNumCollisionObjects();
}

void btCollisionWorld_rayTest(btCollisionWorld* obj, btScalar* rayFromWorld, btScalar* rayToWorld, btCollisionWorld::RayResultCallback* resultCallback)
{
	VECTOR3_CONV(rayFromWorld);
	VECTOR3_CONV(rayToWorld);
	obj->rayTest(VECTOR3_USE(rayFromWorld), VECTOR3_USE(rayToWorld), *resultCallback);
}

void btCollisionWorld_removeCollisionObject(btCollisionWorld* obj, btCollisionObject* collisionObject)
{
	obj->removeCollisionObject(collisionObject);
}
