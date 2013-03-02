#include "main.h"

extern "C"
{
	EXPORT const btConvexPolyhedron* btPolyhedralConvexShape_getConvexPolyhedron(btPolyhedralConvexShape* obj);
	EXPORT void btPolyhedralConvexShape_getEdge(btPolyhedralConvexShape* obj, int i, btScalar* pa, btScalar* pb);
	EXPORT int btPolyhedralConvexShape_getNumEdges(btPolyhedralConvexShape* obj);
	EXPORT int btPolyhedralConvexShape_getNumPlanes(btPolyhedralConvexShape* obj);
	EXPORT int btPolyhedralConvexShape_getNumVertices(btPolyhedralConvexShape* obj);
	EXPORT void btPolyhedralConvexShape_getPlane(btPolyhedralConvexShape* obj, btScalar* planeNormal, btScalar* planeSupport, int i);
	EXPORT void btPolyhedralConvexShape_getVertex(btPolyhedralConvexShape* obj, int i, btScalar* vertex);
	EXPORT bool btPolyhedralConvexShape_initializePolyhedralFeatures(btPolyhedralConvexShape* obj, int shiftVerticesByMargin);
	EXPORT bool btPolyhedralConvexShape_initializePolyhedralFeatures2(btPolyhedralConvexShape* obj);
	EXPORT bool btPolyhedralConvexShape_isInside(btCollisionShape* obj, btScalar* pt, float tolerance);
}
