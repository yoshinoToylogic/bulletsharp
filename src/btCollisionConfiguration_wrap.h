#include "main.h"

extern "C"
{
	EXPORT void btCollisionConfiguration_delete(btCollisionConfiguration* obj);
	EXPORT btCollisionAlgorithmCreateFunc* btCollisionConfiguration_getCollisionAlgorithmCreateFunc2(btCollisionConfiguration* obj, int proxyType0, int proxyType1);
	EXPORT btPoolAllocator* btCollisionConfiguration_getCollisionAlgorithmPool(btCollisionConfiguration* obj);
	EXPORT btPoolAllocator* btCollisionConfiguration_getPersistentManifoldPool(btCollisionConfiguration* obj);
	EXPORT btStackAlloc* btCollisionConfiguration_getStackAllocator(btCollisionConfiguration* obj);
}
