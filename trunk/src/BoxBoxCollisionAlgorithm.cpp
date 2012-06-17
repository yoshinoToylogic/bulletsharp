#include "StdAfx.h"

#ifndef DISABLE_COLLISION_ALGORITHMS

#include "BoxBoxCollisionAlgorithm.h"
#include "CollisionObject.h"
#include "CollisionObjectWrapper.h"
#include "PersistentManifold.h"

BoxBoxCollisionAlgorithm::CreateFunc::CreateFunc()
: CollisionAlgorithmCreateFunc(new btBoxBoxCollisionAlgorithm::CreateFunc())
{
}

BoxBoxCollisionAlgorithm::BoxBoxCollisionAlgorithm(CollisionAlgorithmConstructionInfo^ ci)
: ActivatingCollisionAlgorithm(new btBoxBoxCollisionAlgorithm(*ci->UnmanagedPointer))
{
}

BoxBoxCollisionAlgorithm::BoxBoxCollisionAlgorithm(PersistentManifold^ mf, CollisionAlgorithmConstructionInfo^ ci,
	CollisionObjectWrapper^ body0Wrap, CollisionObjectWrapper^ body1Wrap)
: ActivatingCollisionAlgorithm(new btBoxBoxCollisionAlgorithm((mf != nullptr) ? mf->UnmanagedPointer : 0, *ci->UnmanagedPointer,
	body0Wrap->_unmanaged, body1Wrap->_unmanaged))
{
}

#endif
