#include "main.h"

extern "C"
{
    EXPORT void btCollisionConfiguration_delete(btCollisionConfiguration* obj);
	EXPORT btPoolAllocator* btCollisionConfiguration_getPersistentManifoldPool(btCollisionConfiguration* obj);
	EXPORT btPoolAllocator* btCollisionConfiguration_getCollisionAlgorithmPool(btCollisionConfiguration* obj);
	EXPORT btStackAlloc* btCollisionConfiguration_getStackAllocator(btCollisionConfiguration* obj);
	EXPORT btCollisionAlgorithmCreateFunc* btCollisionConfiguration_getCollisionAlgorithmCreateFunc(btCollisionConfiguration* obj, int proxyType0, int proxyType1);
}
