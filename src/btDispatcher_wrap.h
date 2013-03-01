#include "main.h"

extern "C"
{
	EXPORT void btDispatcher_delete(btDispatcher* obj);
	/*
	EXPORT btCollisionAlgorithm* findAlgorithm(const btCollisionObjectWrapper* body0Wrap,const btCollisionObjectWrapper* body1Wrap,btPersistentManifold* sharedManifold=0);
	EXPORT btPersistentManifold* getNewManifold(const btCollisionObject* b0,const btCollisionObject* b1);
	EXPORT void releaseManifold(btPersistentManifold* manifold);
	EXPORT void clearManifold(btPersistentManifold* manifold);
	EXPORT bool needsCollision(const btCollisionObject* body0,const btCollisionObject* body1);
	EXPORT bool needsResponse(const btCollisionObject* body0,const btCollisionObject* body1);
	EXPORT void dispatchAllCollisionPairs(btOverlappingPairCache* pairCache,const btDispatcherInfo& dispatchInfo,btDispatcher* dispatcher);
	EXPORT int getNumManifolds();
	EXPORT btPersistentManifold* getManifoldByIndexInternal(int index);
	EXPORT btPersistentManifold** getInternalManifoldPointer();
	EXPORT btPoolAllocator* getInternalManifoldPool();
	EXPORT void* allocateCollisionAlgorithm(int size);
	EXPORT void freeCollisionAlgorithm(void* ptr);
	*/
}
