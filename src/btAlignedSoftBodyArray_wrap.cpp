#include <BulletSoftBody/btSoftBody.h>

#include "conversion.h"
#include "btAlignedSoftBodyArray_wrap.h"

btSoftBody* btAlignedSoftBodyArray_at(btSoftBody::tSoftBodyArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedSoftBodyArray_push_back(btSoftBody::tSoftBodyArray* obj, btSoftBody* val)
{
	obj->push_back(val);
}

void btAlignedSoftBodyArray_resizeNoInitialize(btSoftBody::tSoftBodyArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyArray_size(btSoftBody::tSoftBodyArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyArray_delete(btSoftBody::tSoftBodyArray* obj)
{
	delete obj;
}
