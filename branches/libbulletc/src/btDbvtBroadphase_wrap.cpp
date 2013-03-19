#include "btDbvtBroadphase_wrap.h"

btDbvtProxy* btDbvtProxy_new(btScalar* aabbMin, btScalar* aabbMax, void* userPtr, short collisionFilterGroup, short collisionFilterMask)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	return new btDbvtProxy(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax), userPtr, collisionFilterGroup, collisionFilterMask);
}

btDbvtBroadphase* btDbvtBroadphase_new(btOverlappingPairCache* paircache)
{
	return new btDbvtBroadphase(paircache);
}

btDbvtBroadphase* btDbvtBroadphase_new2()
{
	return new btDbvtBroadphase();
}

void btDbvtBroadphase_benchmark(btBroadphaseInterface* __unnamed0)
{
	btDbvtBroadphase::benchmark(__unnamed0);
}

void btDbvtBroadphase_collide(btDbvtBroadphase* obj, btDispatcher* dispatcher)
{
	obj->collide(dispatcher);
}

btScalar btDbvtBroadphase_getVelocityPrediction(btDbvtBroadphase* obj)
{
	return obj->getVelocityPrediction();
}

void btDbvtBroadphase_optimize(btDbvtBroadphase* obj)
{
	obj->optimize();
}

void btDbvtBroadphase_performDeferredRemoval(btDbvtBroadphase* obj, btDispatcher* dispatcher)
{
	obj->performDeferredRemoval(dispatcher);
}

void btDbvtBroadphase_setAabbForceUpdate(btDbvtBroadphase* obj, btBroadphaseProxy* absproxy, btScalar* aabbMin, btScalar* aabbMax, btDispatcher* __unnamed3)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->setAabbForceUpdate(absproxy, VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax), __unnamed3);
}

void btDbvtBroadphase_setVelocityPrediction(btDbvtBroadphase* obj, btScalar prediction)
{
	obj->setVelocityPrediction(prediction);
}
