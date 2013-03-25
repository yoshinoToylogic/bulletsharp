#include "main.h"

#include <LinearMath/btGeometryUtil.h>

extern "C"
{
	EXPORT btGeometryUtil* btGeometryUtil_new();
	EXPORT bool btGeometryUtil_areVerticesBehindPlane(btVector3* planeNormal, btAlignedObjectArray<btVector3>* vertices, btScalar margin);
	EXPORT void btGeometryUtil_getPlaneEquationsFromVertices(btAlignedObjectArray<btVector3>* vertices, btAlignedObjectArray<btVector3>* planeEquationsOut);
	EXPORT void btGeometryUtil_getVerticesFromPlaneEquations(btAlignedObjectArray<btVector3>* planeEquations, btAlignedObjectArray<btVector3>* verticesOut);
	//EXPORT bool btGeometryUtil_isInside(btAlignedObjectArray<btVector3>* vertices, btVector3* planeNormal, btScalar margin);
	EXPORT bool btGeometryUtil_isPointInsidePlanes(btAlignedObjectArray<btVector3>* planeEquations, btVector3* point, btScalar margin);
	EXPORT void btGeometryUtil_delete(btGeometryUtil* obj);
}
