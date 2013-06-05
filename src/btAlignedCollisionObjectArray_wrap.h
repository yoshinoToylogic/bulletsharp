#include "main.h"

extern "C"
{
	EXPORT int btAlignedConstCollisionObjectArray_size(btCollisionObjectArray* obj);
	EXPORT btCollisionObject* btAlignedConstCollisionObjectArray_at(btCollisionObjectArray* obj, int n);
}
