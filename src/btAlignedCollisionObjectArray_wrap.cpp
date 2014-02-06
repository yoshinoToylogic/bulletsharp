#include "conversion.h"
#include "btAlignedCollisionObjectArray_wrap.h"

int btAlignedCollisionObjectArray_size(btAlignedCollisionObjectArray* obj)
{
	return obj->size();
}

btCollisionObject* btAlignedCollisionObjectArray_at(btAlignedCollisionObjectArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedCollisionObjectArray_delete(btAlignedCollisionObjectArray* obj)
{
	delete obj;
}
