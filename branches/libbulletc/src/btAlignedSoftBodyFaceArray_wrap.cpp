#include <BulletSoftBody/btSoftBody.h>

#include "conversion.h"
#include "btAlignedSoftBodyFaceArray_wrap.h"

btSoftBody::Face* btAlignedSoftBodyFaceArray_at(btSoftBody::tFaceArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedSoftBodyFaceArray_push_back(btSoftBody::tFaceArray* obj, btSoftBody::Face* val)
{
	obj->push_back(*val);
}

void btAlignedSoftBodyFaceArray_resizeNoInitialize(btSoftBody::tFaceArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyFaceArray_size(btSoftBody::tFaceArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyFaceArray_delete(btSoftBody::tFaceArray* obj)
{
	delete obj;
}
