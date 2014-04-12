#include "main.h"

extern "C"
{
	EXPORT btSoftBody_Material* btAlignedSoftBodyMaterialArray_at(btAlignedSoftBodyMaterialArray* obj, int n);
	EXPORT void btAlignedSoftBodyMaterialArray_push_back(btAlignedSoftBodyMaterialArray* obj, btSoftBody_Material* val);
	EXPORT void btAlignedSoftBodyMaterialArray_resizeNoInitialize(btAlignedSoftBodyMaterialArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyMaterialArray_size(btAlignedSoftBodyMaterialArray* obj);
	EXPORT void btAlignedSoftBodyMaterialArray_delete(btAlignedSoftBodyMaterialArray* obj);
}
