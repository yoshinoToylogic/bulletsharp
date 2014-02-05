#include "conversion.h"
#include "btAlignedManifoldArray_wrap.h"

EXPORT btManifoldArray* btManifoldArray_new()
{
	return new btManifoldArray();
}

int btManifoldArray_size(btManifoldArray* obj)
{
	return obj->size();
}

btPersistentManifold* btManifoldArray_at(btManifoldArray* obj, int n)
{
	return obj->at(n);
}
