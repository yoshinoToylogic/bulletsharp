#include "main.h"

extern "C"
{
	EXPORT btSphereSphereCollisionAlgorithm_CreateFunc* CreateFunc_new();
	EXPORT btSphereSphereCollisionAlgorithm* btSphereSphereCollisionAlgorithm_new(btPersistentManifold* mf, const btCollisionAlgorithmConstructionInfo* ci, btCollisionObjectWrapper* col0Wrap, btCollisionObjectWrapper* col1Wrap);
	EXPORT btSphereSphereCollisionAlgorithm* btSphereSphereCollisionAlgorithm_new2(btCollisionAlgorithmConstructionInfo* ci);
}
