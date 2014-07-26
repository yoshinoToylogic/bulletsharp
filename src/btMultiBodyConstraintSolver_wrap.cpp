#include <BulletCollision/BroadphaseCollision/btDispatcher.h>
#include <BulletCollision/NarrowPhaseCollision/btPersistentManifold.h>
#include <BulletDynamics/Featherstone/btMultiBodyConstraintSolver.h>
#include <LinearMath/btIDebugDraw.h>

#include "btMultiBodyConstraintSolver_wrap.h"

btMultiBodyConstraintSolver* btMultiBodyConstraintSolver_new()
{
	return new btMultiBodyConstraintSolver();
}

void btMultiBodyConstraintSolver_solveMultiBodyGroup(btMultiBodyConstraintSolver* obj, btCollisionObject** bodies, int numBodies, btPersistentManifold** manifold, int numManifolds, btTypedConstraint** constraints, int numConstraints, btMultiBodyConstraint** multiBodyConstraints, int numMultiBodyConstraints, const btContactSolverInfo* info, btIDebugDraw* debugDrawer, btDispatcher* dispatcher)
{
	obj->solveMultiBodyGroup(bodies, numBodies, manifold, numManifolds, constraints, numConstraints, multiBodyConstraints, numMultiBodyConstraints, *info, debugDrawer, dispatcher);
}
