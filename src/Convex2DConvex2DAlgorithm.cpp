#include "StdAfx.h"

#ifndef DISABLE_UNCOMMON
#ifndef DISABLE_COLLISION_ALGORITHMS

#include "CollisionObject.h"
#include "CollisionObjectWrapper.h"
#include "Convex2DConvex2DAlgorithm.h"
#include "ConvexPenetrationDepthSolver.h"
#include "PersistentManifold.h"
#include "SimplexSolverInterface.h"

Convex2DConvex2DAlgorithm::CreateFunc::CreateFunc(SimplexSolverInterface^ simplexSolver,
	ConvexPenetrationDepthSolver^ pdSolver)
	: CollisionAlgorithmCreateFunc(new btConvex2dConvex2dAlgorithm::CreateFunc(simplexSolver->_native,
		pdSolver->_native))
{
	_simplexSolver = simplexSolver;
	_pdSolver = pdSolver;
}


#define Native static_cast<btConvex2dConvex2dAlgorithm*>(_native)

Convex2DConvex2DAlgorithm::Convex2DConvex2DAlgorithm(PersistentManifold^ mf, CollisionAlgorithmConstructionInfo^ ci,
	CollisionObjectWrapper^ body0Wrap, CollisionObjectWrapper^ body1Wrap, SimplexSolverInterface^ simplexSolver,
	ConvexPenetrationDepthSolver^ pdSolver, int numPerturbationIterations, int minimumPointsPerturbationThreshold)
	: ActivatingCollisionAlgorithm(new btConvex2dConvex2dAlgorithm((btPersistentManifold*)GetUnmanagedNullable(mf),
		*ci->_native, body0Wrap->_native, body1Wrap->_native, simplexSolver->_native,
		pdSolver->_native, numPerturbationIterations, minimumPointsPerturbationThreshold))
{
}

void Convex2DConvex2DAlgorithm::SetLowLevelOfDetail(bool useLowLevel)
{
	Native->setLowLevelOfDetail(useLowLevel);
}

PersistentManifold^ Convex2DConvex2DAlgorithm::Manifold::get()
{
	return gcnew PersistentManifold((btPersistentManifold*)Native->getManifold());
}

#endif
#endif
