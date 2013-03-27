#include "btGeometryUtil_wrap.h"

bool btGeometryUtil_areVerticesBehindPlane(btScalar* planeNormal, btAlignedObjectArray<btVector3>* vertices, btScalar margin)
{
	VECTOR3_CONV(planeNormal);
	return btGeometryUtil::areVerticesBehindPlane(VECTOR3_USE(planeNormal), *vertices, margin);
}

void btGeometryUtil_getPlaneEquationsFromVertices(btAlignedObjectArray<btVector3>* vertices, btAlignedObjectArray<btVector3>* planeEquationsOut)
{
	btGeometryUtil::getPlaneEquationsFromVertices(*vertices, *planeEquationsOut);
}

void btGeometryUtil_getVerticesFromPlaneEquations(btAlignedObjectArray<btVector3>* planeEquations, btAlignedObjectArray<btVector3>* verticesOut)
{
	btGeometryUtil::getVerticesFromPlaneEquations(*planeEquations, *verticesOut);
}
/*
bool btGeometryUtil_isInside(btAlignedObjectArray<btVector3>* vertices, btVector3* planeNormal, btScalar margin)
{
	VECTOR3_CONV(planeNormal);
	return btGeometryUtil::isInside(*vertices, VECTOR3_USE(planeNormal), margin);
}
*/
bool btGeometryUtil_isPointInsidePlanes(btAlignedObjectArray<btVector3>* planeEquations, btScalar* point, btScalar margin)
{
	VECTOR3_CONV(point);
	return btGeometryUtil::isPointInsidePlanes(*planeEquations, VECTOR3_USE(point), margin);
}
