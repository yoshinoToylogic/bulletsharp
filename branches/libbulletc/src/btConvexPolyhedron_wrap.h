#include "main.h"

#include <BulletCollision/CollisionShapes/btConvexPolyhedron.h>

extern "C"
{
	EXPORT btFace* btFace_new();
	EXPORT btAlignedObjectArray<int>* btFace_getIndices(btFace* obj);
	EXPORT btScalar* btFace_getPlane(btFace* obj);
	EXPORT void btFace_delete(btFace* obj);

	EXPORT btConvexPolyhedron* btConvexPolyhedron_new();
	EXPORT void btConvexPolyhedron_getExtents(btConvexPolyhedron* obj, btScalar* value);
	EXPORT btAlignedObjectArray<btFace>* btConvexPolyhedron_getFaces(btConvexPolyhedron* obj);
	EXPORT void btConvexPolyhedron_getLocalCenter(btConvexPolyhedron* obj, btScalar* value);
	EXPORT void btConvexPolyhedron_getMC(btConvexPolyhedron* obj, btScalar* value);
	EXPORT void btConvexPolyhedron_getME(btConvexPolyhedron* obj, btScalar* value);
	EXPORT btScalar btConvexPolyhedron_getRadius(btConvexPolyhedron* obj);
	EXPORT btAlignedObjectArray<btVector3>* btConvexPolyhedron_getUniqueEdges(btConvexPolyhedron* obj);
	EXPORT btAlignedObjectArray<btVector3>* btConvexPolyhedron_getVertices(btConvexPolyhedron* obj);
	EXPORT void btConvexPolyhedron_initialize(btConvexPolyhedron* obj);
	EXPORT void btConvexPolyhedron_project(btConvexPolyhedron* obj, btScalar* trans, btScalar* dir, btScalar* minProj, btScalar* maxProj, btScalar* witnesPtMin, btScalar* witnesPtMax);
	EXPORT void btConvexPolyhedron_setExtents(btConvexPolyhedron* obj, btScalar* value);
	EXPORT void btConvexPolyhedron_setLocalCenter(btConvexPolyhedron* obj, btScalar* value);
	EXPORT void btConvexPolyhedron_setMC(btConvexPolyhedron* obj, btScalar* value);
	EXPORT void btConvexPolyhedron_setME(btConvexPolyhedron* obj, btScalar* value);
	EXPORT void btConvexPolyhedron_setRadius(btConvexPolyhedron* obj, btScalar value);
	EXPORT bool btConvexPolyhedron_testContainment(btConvexPolyhedron* obj);
	EXPORT void btConvexPolyhedron_delete(btConvexPolyhedron* obj);
}