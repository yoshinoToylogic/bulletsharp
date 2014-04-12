#include "main.h"

extern "C"
{
	EXPORT btSoftBody_Face* btAlignedSoftBodyFaceArray_at(btAlignedSoftBodyFaceArray* obj, int n);
	EXPORT void btAlignedSoftBodyFaceArray_push_back(btAlignedSoftBodyFaceArray* obj, btSoftBody_Face* val);
	EXPORT void btAlignedSoftBodyFaceArray_resizeNoInitialize(btAlignedSoftBodyFaceArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyFaceArray_size(btAlignedSoftBodyFaceArray* obj);
	EXPORT void btAlignedSoftBodyFaceArray_delete(btAlignedSoftBodyFaceArray* obj);
}
