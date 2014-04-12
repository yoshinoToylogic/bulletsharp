#include "main.h"

extern "C"
{
	EXPORT btSoftBody_Node* btAlignedSoftBodyNodeArray_at(btAlignedSoftBodyNodeArray* obj, int n);
	EXPORT void btAlignedSoftBodyNodeArray_push_back(btAlignedSoftBodyNodeArray* obj, btSoftBody_Node* val);
	EXPORT void btAlignedSoftBodyNodeArray_resizeNoInitialize(btAlignedSoftBodyNodeArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyNodeArray_size(btAlignedSoftBodyNodeArray* obj);
	EXPORT void btAlignedSoftBodyNodeArray_delete(btAlignedSoftBodyNodeArray* obj);
}
