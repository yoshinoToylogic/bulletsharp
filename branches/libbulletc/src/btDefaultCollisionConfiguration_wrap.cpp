#include "btDefaultCollisionConfiguration_wrap.h"

btDefaultCollisionConstructionInfo* btDefaultCollisionConstructionInfo_new()
{
	return new btDefaultCollisionConstructionInfo();
}

void btDefaultCollisionConstructionInfo_delete(btDefaultCollisionConstructionInfo* obj)
{
	delete obj;
}

btDefaultCollisionConfiguration* btDefaultCollisionConfiguration_new(btDefaultCollisionConstructionInfo* constructionInfo)
{
	return new btDefaultCollisionConfiguration(*constructionInfo);
}

btDefaultCollisionConfiguration* btDefaultCollisionConfiguration_new2()
{
	return new btDefaultCollisionConfiguration();
}

btVoronoiSimplexSolver* btDefaultCollisionConfiguration_getSimplexSolver(btDefaultCollisionConfiguration* obj)
{
	return obj->getSimplexSolver();
}

void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations(btDefaultCollisionConfiguration* obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold)
{
	obj->setConvexConvexMultipointIterations(numPerturbationIterations, minimumPointsPerturbationThreshold);
}

void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations2(btDefaultCollisionConfiguration* obj, int numPerturbationIterations)
{
	obj->setConvexConvexMultipointIterations(numPerturbationIterations);
}

void btDefaultCollisionConfiguration_setConvexConvexMultipointIterations3(btDefaultCollisionConfiguration* obj)
{
	obj->setConvexConvexMultipointIterations();
}

void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations(btDefaultCollisionConfiguration* obj, int numPerturbationIterations, int minimumPointsPerturbationThreshold)
{
	obj->setPlaneConvexMultipointIterations(numPerturbationIterations, minimumPointsPerturbationThreshold);
}

void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations2(btDefaultCollisionConfiguration* obj, int numPerturbationIterations)
{
	obj->setPlaneConvexMultipointIterations(numPerturbationIterations);
}

void btDefaultCollisionConfiguration_setPlaneConvexMultipointIterations3(btDefaultCollisionConfiguration* obj)
{
	obj->setPlaneConvexMultipointIterations();
}
