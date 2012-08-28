#include "StdAfx.h"

#ifndef DISABLE_COLLISION_ALGORITHMS

#include "Box2dBox2dCollisionAlgorithm.h"
#include "CollisionObject.h"
#include "CollisionObjectWrapper.h"
#include "PersistentManifold.h"

Box2dBox2dCollisionAlgorithm::CreateFunc::CreateFunc()
: CollisionAlgorithmCreateFunc(new btBox2dBox2dCollisionAlgorithm::CreateFunc())
{
}

Box2dBox2dCollisionAlgorithm::Box2dBox2dCollisionAlgorithm(CollisionAlgorithmConstructionInfo^ ci)
: ActivatingCollisionAlgorithm(new btBox2dBox2dCollisionAlgorithm(*ci->_unmanaged))
{
}

Box2dBox2dCollisionAlgorithm::Box2dBox2dCollisionAlgorithm(PersistentManifold^ mf, CollisionAlgorithmConstructionInfo^ ci,
	CollisionObjectWrapper^ body0Wrap, CollisionObjectWrapper^ body1Wrap)
: ActivatingCollisionAlgorithm(new btBox2dBox2dCollisionAlgorithm((btPersistentManifold*)GetUnmanagedNullable(mf),
	*ci->_unmanaged, body0Wrap->_unmanaged, body1Wrap->_unmanaged))
{
}

#endif
