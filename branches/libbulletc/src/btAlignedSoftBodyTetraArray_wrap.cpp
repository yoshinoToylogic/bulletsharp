#include <BulletSoftBody/btSoftBody.h>

#include "conversion.h"
#include "btAlignedSoftBodyTetraArray_wrap.h"

btSoftBody::Tetra* btAlignedSoftBodyTetraArray_at(btSoftBody::tTetraArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedSoftBodyTetraArray_push_back(btSoftBody::tTetraArray* obj, btSoftBody::Tetra* val)
{
	obj->push_back(*val);
}

void btAlignedSoftBodyTetraArray_resizeNoInitialize(btSoftBody::tTetraArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyTetraArray_size(btSoftBody::tTetraArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyTetraArray_delete(btSoftBody::tTetraArray* obj)
{
	delete obj;
}
