#include <BulletSoftBody/btSoftBody.h>

#include "conversion.h"
#include "btAlignedSoftBodyNodeArray_wrap.h"

btSoftBody::Node* btAlignedSoftBodyNodeArray_at(btSoftBody::tNodeArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedSoftBodyNodeArray_push_back(btSoftBody::tNodeArray* obj, btSoftBody::Node* val)
{
	obj->push_back(*val);
}

void btAlignedSoftBodyNodeArray_resizeNoInitialize(btSoftBody::tNodeArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyNodeArray_size(btSoftBody::tNodeArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyNodeArray_delete(btSoftBody::tNodeArray* obj)
{
	delete obj;
}
