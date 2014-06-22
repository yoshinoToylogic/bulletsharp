#include "StdAfx.h"

#ifndef DISABLE_COLLISION_ALGORITHMS

#include "CollisionObject.h"
#include "CollisionObjectWrapper.h"
#include "PersistentManifold.h"
#include "SphereSphereCollisionAlgorithm.h"

#define Native static_cast<btSphereSphereCollisionAlgorithm::CreateFunc*>(_native)

SphereSphereCollisionAlgorithm::CreateFunc::CreateFunc()
	: CollisionAlgorithmCreateFunc(new btSphereSphereCollisionAlgorithm::CreateFunc())
{
}


SphereSphereCollisionAlgorithm::SphereSphereCollisionAlgorithm(PersistentManifold^ mf,
	CollisionAlgorithmConstructionInfo^ ci, CollisionObjectWrapper^ col0Wrap, CollisionObjectWrapper^ col1Wrap)
	: ActivatingCollisionAlgorithm(new btSphereSphereCollisionAlgorithm((btPersistentManifold*)GetUnmanagedNullable(mf),
		*ci->_native, col0Wrap->_native, col1Wrap->_native))
{
}

SphereSphereCollisionAlgorithm::SphereSphereCollisionAlgorithm(CollisionAlgorithmConstructionInfo^ ci)
	: ActivatingCollisionAlgorithm(new btSphereSphereCollisionAlgorithm(*ci->_native))
{
}

#endif
