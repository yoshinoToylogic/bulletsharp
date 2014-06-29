#include "conversion.h"
#include "btTriangleCallback_wrap.h"

void btTriangleCallback_processTriangle(btTriangleCallback* obj, btScalar* triangle, int partId, int triangleIndex)
{
	VECTOR3_CONV(triangle);
	obj->processTriangle(&VECTOR3_USE(triangle), partId, triangleIndex);
}

void btTriangleCallback_delete(btTriangleCallback* obj)
{
	delete obj;
}

void btInternalTriangleIndexCallback_internalProcessTriangleIndex(btInternalTriangleIndexCallback* obj, btScalar* triangle, int partId, int triangleIndex)
{
	VECTOR3_CONV(triangle);
	obj->internalProcessTriangleIndex(&VECTOR3_USE(triangle), partId, triangleIndex);
}

void btInternalTriangleIndexCallback_delete(btInternalTriangleIndexCallback* obj)
{
	delete obj;
}
