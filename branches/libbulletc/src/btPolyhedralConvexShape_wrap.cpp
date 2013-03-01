#include "btPolyhedralConvexShape_wrap.h"

int btPolyhedralConvexShape_getNumEdges(btPolyhedralConvexShape* obj)
{
	return obj->getNumEdges();
}

int btPolyhedralConvexShape_getNumPlanes(btPolyhedralConvexShape* obj)
{
	return obj->getNumPlanes();
}

int btPolyhedralConvexShape_getNumVertices(btPolyhedralConvexShape* obj)
{
	return obj->getNumVertices();
}

void btPolyhedralConvexShape_getVertex(btPolyhedralConvexShape* obj, int i, btScalar* vertex)
{
	VECTOR3_DEF(vertex);
	obj->getVertex(i, VECTOR3_USE(vertex));
	VECTOR3_DEF_OUT(vertex);
}

bool btPolyhedralConvexShape_isInside(btPolyhedralConvexShape* obj, btScalar* pt, float tolerance)
{
	VECTOR3_CONV(pt);
	return obj->isInside(VECTOR3_USE(pt), tolerance);
}
