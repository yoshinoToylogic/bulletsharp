#include "conversion.h"
#include "btAlignedIndexedMeshArray_wrap.h"

btIndexedMesh* btAlignedIndexedMeshArray_at(btAlignedIndexedMeshArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedIndexedMeshArray_push_back(btAlignedIndexedMeshArray* obj, btIndexedMesh* val)
{
	obj->push_back(*val);
}

void btAlignedIndexedMeshArray_resizeNoInitialize(btAlignedIndexedMeshArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedIndexedMeshArray_size(btAlignedIndexedMeshArray* obj)
{
	return obj->size();
}

void btAlignedIndexedMeshArray_delete(btAlignedIndexedMeshArray* obj)
{
	delete obj;
}
