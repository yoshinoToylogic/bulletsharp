#include "conversion.h"
#include "btAlignedBroadphasePairArray_wrap.h"

int btAlignedBroadphasePairArray_size(btAlignedBroadphasePairArray* obj)
{
	return obj->size();
}

btBroadphasePair* btAlignedBroadphasePairArray_at(btAlignedBroadphasePairArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedBroadphasePairArray_delete(btAlignedBroadphasePairArray* obj)
{
	delete obj;
}
