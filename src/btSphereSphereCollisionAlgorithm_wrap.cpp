#include <BulletCollision/CollisionDispatch/btSphereSphereCollisionAlgorithm.h>

#include "btSphereSphereCollisionAlgorithm_wrap.h"

btSphereSphereCollisionAlgorithm::CreateFunc* CreateFunc_new()
{
	return new btSphereSphereCollisionAlgorithm::CreateFunc();
}

btSphereSphereCollisionAlgorithm* btSphereSphereCollisionAlgorithm_new(btPersistentManifold* mf, const btCollisionAlgorithmConstructionInfo* ci, btCollisionObjectWrapper* col0Wrap, btCollisionObjectWrapper* col1Wrap)
{
	return new btSphereSphereCollisionAlgorithm(mf, *ci, col0Wrap, col1Wrap);
}

btSphereSphereCollisionAlgorithm* btSphereSphereCollisionAlgorithm_new2(btCollisionAlgorithmConstructionInfo* ci)
{
	return new btSphereSphereCollisionAlgorithm(*ci);
}
