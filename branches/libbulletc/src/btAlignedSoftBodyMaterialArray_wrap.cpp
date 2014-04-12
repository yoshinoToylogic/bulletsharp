#include <BulletSoftBody/btSoftBody.h>

#include "conversion.h"
#include "btAlignedSoftBodyMaterialArray_wrap.h"

btSoftBody::Material* btAlignedSoftBodyMaterialArray_at(btSoftBody::tMaterialArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedSoftBodyMaterialArray_push_back(btSoftBody::tMaterialArray* obj, btSoftBody::Material* val)
{
	obj->push_back(val);
}

void btAlignedSoftBodyMaterialArray_resizeNoInitialize(btSoftBody::tMaterialArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyMaterialArray_size(btSoftBody::tMaterialArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyMaterialArray_delete(btSoftBody::tMaterialArray* obj)
{
	delete obj;
}
