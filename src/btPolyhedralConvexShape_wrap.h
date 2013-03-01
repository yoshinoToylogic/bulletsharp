#include "main.h"

extern "C"
{
	EXPORT int btPolyhedralConvexShape_getNumEdges(btPolyhedralConvexShape* obj);
	EXPORT int btPolyhedralConvexShape_getNumPlanes(btPolyhedralConvexShape* obj);
	EXPORT int btPolyhedralConvexShape_getNumVertices(btPolyhedralConvexShape* obj);
    EXPORT void btPolyhedralConvexShape_getVertex(btPolyhedralConvexShape* obj, int i, btScalar* vertex);
    EXPORT bool btPolyhedralConvexShape_isInside(btCollisionShape* obj, btScalar* pt, float tolerance);
}
