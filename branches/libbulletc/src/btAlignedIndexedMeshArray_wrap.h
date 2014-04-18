#include "main.h"

extern "C"
{
	EXPORT btIndexedMesh* btAlignedIndexedMeshArray_at(btAlignedIndexedMeshArray* obj, int n);
	EXPORT void btAlignedIndexedMeshArray_push_back(btAlignedIndexedMeshArray* obj, btIndexedMesh* val);
	EXPORT void btAlignedIndexedMeshArray_resizeNoInitialize(btAlignedIndexedMeshArray* obj, int newSize);
	EXPORT int btAlignedIndexedMeshArray_size(btAlignedIndexedMeshArray* obj);
	EXPORT void btAlignedIndexedMeshArray_delete(btAlignedIndexedMeshArray* obj);
}
