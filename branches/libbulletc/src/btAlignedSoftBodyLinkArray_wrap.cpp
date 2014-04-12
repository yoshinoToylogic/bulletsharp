#include <BulletSoftBody/btSoftBody.h>

#include "conversion.h"
#include "btAlignedSoftBodyLinkArray_wrap.h"

btSoftBody::Link* btAlignedSoftBodyLinkArray_at(btSoftBody::tLinkArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedSoftBodyLinkArray_push_back(btSoftBody::tLinkArray* obj, btSoftBody::Link* val)
{
	obj->push_back(*val);
}

void btAlignedSoftBodyLinkArray_resizeNoInitialize(btSoftBody::tLinkArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyLinkArray_size(btSoftBody::tLinkArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyLinkArray_delete(btSoftBody::tLinkArray* obj)
{
	delete obj;
}
