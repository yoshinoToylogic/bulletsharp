#include "main.h"

extern "C"
{
    EXPORT int btAlignedCollisionObjectArray_size(btCollisionObjectArray* obj);
	EXPORT btCollisionObject* btAlignedCollisionObjectArray_at(btCollisionObjectArray* obj, int n);
}
