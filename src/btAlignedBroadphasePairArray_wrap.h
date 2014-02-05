#include "main.h"

extern "C"
{
	EXPORT int btAlignedBroadphasePairArray_size(btAlignedBroadphasePairArray* obj);
	EXPORT btBroadphasePair* btAlignedBroadphasePairArray_at(btAlignedBroadphasePairArray* obj, int n);
}
