#include "btCollisionConfiguration_wrap.h"

void btCollisionConfiguration_delete(btCollisionConfiguration* obj)
{
    delete obj;
}

btPoolAllocator* btCollisionConfiguration_getPersistentManifoldPool(btCollisionConfiguration* obj)
{
	return obj->getPersistentManifoldPool();
}

btPoolAllocator* btCollisionConfiguration_getCollisionAlgorithmPool(btCollisionConfiguration* obj)
{
	return obj->getCollisionAlgorithmPool();
}

btStackAlloc* btCollisionConfiguration_getStackAllocator(btCollisionConfiguration* obj)
{
	return obj->getStackAllocator();
}

btCollisionAlgorithmCreateFunc* btCollisionConfiguration_getCollisionAlgorithmCreateFunc(btCollisionConfiguration* obj, int proxyType0, int proxyType1)
{
	return obj->getCollisionAlgorithmCreateFunc(proxyType0, proxyType1);
}
