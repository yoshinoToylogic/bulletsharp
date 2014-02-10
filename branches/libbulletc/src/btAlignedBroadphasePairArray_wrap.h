#include "main.h"

extern "C"
{
	EXPORT int btAlignedBroadphasePairArray_size(btAlignedBroadphasePairArray* obj);
	EXPORT btBroadphasePair* btAlignedBroadphasePairArray_at(btAlignedBroadphasePairArray* obj, int n);
	EXPORT void btAlignedBroadphasePairArray_resizeNoInitialize(btAlignedBroadphasePairArray* obj, int newSize);
	EXPORT void btAlignedBroadphasePairArray_delete(btAlignedCollisionObjectArray* obj);
}
