#include "conversion.h"
#include "btAlignedCollisionObjectArray_wrap.h"

int btAlignedConstCollisionObjectArray_size(btCollisionObjectArray* obj)
{
	return obj->size();
}

btCollisionObject* btAlignedConstCollisionObjectArray_at(btCollisionObjectArray* obj, int n)
{
	return obj->at(n);
}
