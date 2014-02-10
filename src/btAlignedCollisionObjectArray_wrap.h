#include "main.h"

extern "C"
{
	EXPORT int btAlignedCollisionObjectArray_size(btAlignedCollisionObjectArray* obj);
	EXPORT btCollisionObject* btAlignedCollisionObjectArray_at(btAlignedCollisionObjectArray* obj, int n);
	EXPORT void btAlignedCollisionObjectArray_resizeNoInitialize(btAlignedCollisionObjectArray* obj, int newSize);
	EXPORT void btAlignedCollisionObjectArray_delete(btAlignedCollisionObjectArray* obj);
}
