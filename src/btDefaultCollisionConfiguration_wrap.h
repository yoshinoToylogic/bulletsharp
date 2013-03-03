#include "main.h"

extern "C"
{
	EXPORT btDefaultCollisionConstructionInfo* btDefaultCollisionConstructionInfo_new();
	EXPORT void btDefaultCollisionConstructionInfo_delete(btDefaultCollisionConstructionInfo* obj);

	EXPORT btDefaultCollisionConfiguration* btDefaultCollisionConfiguration_new(btDefaultCollisionConstructionInfo* constructionInfo);
	EXPORT btDefaultCollisionConfiguration* btDefaultCollisionConfiguration_new2();
	EXPORT btVoronoiSimplexSolver* btDefaultCollisionConfiguration_getSimplexSolver(btDefaultCollisionConfiguration* obj);
	EXPORT void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(btDefaultCollisionConfiguration* obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
	EXPORT void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations2(btDefaultCollisionConfiguration* obj, int numPerturbationIterations);
	EXPORT void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations3(btDefaultCollisionConfiguration* obj);
	EXPORT void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(btDefaultCollisionConfiguration* obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold);
	EXPORT void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations2(btDefaultCollisionConfiguration* obj, int numPerturbationIterations);
	EXPORT void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations3(btDefaultCollisionConfiguration* obj);
}
