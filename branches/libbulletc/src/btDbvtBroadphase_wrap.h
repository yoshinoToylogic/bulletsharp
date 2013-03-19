#include "main.h"

extern "C"
{
	EXPORT btDbvtProxy* btDbvtProxy_new(btScalar* aabbMin, btScalar* aabbMax, void* userPtr, short collisionFilterGroup, short collisionFilterMask);

	EXPORT btDbvtBroadphase* btDbvtBroadphase_new(btOverlappingPairCache* paircache);
	EXPORT btDbvtBroadphase* btDbvtBroadphase_new2();
	EXPORT void btDbvtBroadphase_benchmark(btBroadphaseInterface* __unnamed0);
	EXPORT void btDbvtBroadphase_collide(btDbvtBroadphase* obj, btDispatcher* dispatcher);
	EXPORT btScalar btDbvtBroadphase_getVelocityPrediction(btDbvtBroadphase* obj);
	EXPORT void btDbvtBroadphase_optimize(btDbvtBroadphase* obj);
	EXPORT void btDbvtBroadphase_performDeferredRemoval(btDbvtBroadphase* obj, btDispatcher* dispatcher);
	EXPORT void btDbvtBroadphase_setAabbForceUpdate(btDbvtBroadphase* obj, btBroadphaseProxy* absproxy, btScalar* aabbMin, btScalar* aabbMax, btDispatcher* __unnamed3);
	EXPORT void btDbvtBroadphase_setVelocityPrediction(btDbvtBroadphase* obj, btScalar prediction);
}
