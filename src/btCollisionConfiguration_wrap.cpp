#include "btCollisionConfiguration_wrap.h"

void btCollisionConfiguration_delete(btCollisionConfiguration* obj)
{
	delete obj;
}

btCollisionAlgorithmCreateFunc* btCollisionConfiguration_getCollisionAlgorithmCreateFunc2(btCollisionConfiguration* obj, int proxyType0, int proxyType1)
{
	return obj->getCollisionAlgorithmCreateFunc(proxyType0, proxyType1);
}

btPoolAllocator* btCollisionConfiguration_getCollisionAlgorithmPool(btCollisionConfiguration* obj)
{
	return obj->getCollisionAlgorithmPool();
}

btPoolAllocator* btCollisionConfiguration_getPersistentManifoldPool(btCollisionConfiguration* obj)
{
	return obj->getPersistentManifoldPool();
}

btStackAlloc* btCollisionConfiguration_getStackAllocator(btCollisionConfiguration* obj)
{
	return obj->getStackAllocator();
}
