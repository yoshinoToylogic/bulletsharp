#include "main.h"

extern "C"
{
	EXPORT int btAlignedCollisionObjectArray_size(btAlignedCollisionObjectArray* obj);
	EXPORT btCollisionObject* btAlignedCollisionObjectArray_at(btAlignedCollisionObjectArray* obj, int n);
}
