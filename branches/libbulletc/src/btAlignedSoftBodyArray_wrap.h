#include "main.h"

extern "C"
{
	EXPORT btSoftBody* btAlignedSoftBodyArray_at(btAlignedSoftBodyArray* obj, int n);
	EXPORT void btAlignedSoftBodyArray_push_back(btAlignedSoftBodyArray* obj, btSoftBody* val);
	EXPORT void btAlignedSoftBodyArray_resizeNoInitialize(btAlignedSoftBodyArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyArray_size(btAlignedSoftBodyArray* obj);
	EXPORT void btAlignedSoftBodyArray_delete(btAlignedSoftBodyArray* obj);
}
