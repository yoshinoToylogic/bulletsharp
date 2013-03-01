#include "btAlignedCollisionObjectArray_wrap.h"

int btAlignedCollisionObjectArray_size(btCollisionObjectArray* obj)
{
	return obj->size();
}

btCollisionObject* btAlignedCollisionObjectArray_at(btCollisionObjectArray* obj, int n)
{
	return obj->at(n);
}
