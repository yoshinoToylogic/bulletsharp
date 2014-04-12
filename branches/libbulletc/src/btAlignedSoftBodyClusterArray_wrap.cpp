#include <BulletSoftBody/btSoftBody.h>

#include "conversion.h"
#include "btAlignedSoftBodyClusterArray_wrap.h"

btSoftBody::Cluster* btAlignedSoftBodyClusterArray_at(btSoftBody::tClusterArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedSoftBodyClusterArray_push_back(btSoftBody::tClusterArray* obj, btSoftBody::Cluster* val)
{
	obj->push_back(val);
}

void btAlignedSoftBodyClusterArray_resizeNoInitialize(btSoftBody::tClusterArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyClusterArray_size(btSoftBody::tClusterArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyClusterArray_delete(btSoftBody::tClusterArray* obj)
{
	delete obj;
}
