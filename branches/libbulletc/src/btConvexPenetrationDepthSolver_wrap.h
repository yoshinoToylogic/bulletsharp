#include "main.h"

#include <BulletCollision/NarrowPhaseCollision/btConvexPenetrationDepthSolver.h>

extern "C"
{
	EXPORT bool btConvexPenetrationDepthSolver_calcPenDepth(btConvexPenetrationDepthSolver* obj, btVoronoiSimplexSolver* simplexSolver, btConvexShape* convexA, btConvexShape* convexB, btScalar* transA, btScalar* transB, btScalar* v, btScalar* pa, btScalar* pb, btIDebugDraw* debugDraw, btStackAlloc* stackAlloc);
	EXPORT void btConvexPenetrationDepthSolver_delete(btConvexPenetrationDepthSolver* obj);
}
