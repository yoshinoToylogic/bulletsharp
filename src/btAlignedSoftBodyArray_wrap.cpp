#include <BulletSoftBody/btSoftBody.h>

#include "conversion.h"
#include "btAlignedSoftBodyArray_wrap.h"

btSoftBody::Cluster* btAlignedSoftBodyArray_at(btSoftBody::tClusterArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedSoftBodyArray_push_back(btSoftBody::tClusterArray* obj, btSoftBody::Cluster* val)
{
	obj->push_back(val);
}

void btAlignedSoftBodyArray_resizeNoInitialize(btSoftBody::tClusterArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyArray_size(btSoftBody::tClusterArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyArray_delete(btSoftBody::tClusterArray* obj)
{
	delete obj;
}
