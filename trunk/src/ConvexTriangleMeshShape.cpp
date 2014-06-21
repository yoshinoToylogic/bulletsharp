#include "StdAfx.h"

#include "ConvexTriangleMeshShape.h"
#include "StridingMeshInterface.h"

#define Native static_cast<btConvexTriangleMeshShape*>(_native)

ConvexTriangleMeshShape::ConvexTriangleMeshShape(btConvexTriangleMeshShape* meshShape)
: PolyhedralConvexAabbCachingShape(meshShape)
{
}

ConvexTriangleMeshShape::ConvexTriangleMeshShape(StridingMeshInterface^ meshInterface, bool calcAabb)
: PolyhedralConvexAabbCachingShape(new btConvexTriangleMeshShape(meshInterface->_native, calcAabb))
{
}

ConvexTriangleMeshShape::ConvexTriangleMeshShape(StridingMeshInterface^ meshInterface)
: PolyhedralConvexAabbCachingShape(new btConvexTriangleMeshShape(meshInterface->_native))
{
}

void ConvexTriangleMeshShape::CalculatePrincipalAxisTransform(Matrix% principal, [Out] Vector3% inertia, [Out] btScalar% volume)
{
	btTransform* principalTemp = Math::MatrixToBtTransform(principal);
	VECTOR3_DEF(inertia);
	btScalar volumeTemp;

	Native->calculatePrincipalAxisTransform(*principalTemp, VECTOR3_USE(inertia), volumeTemp);

	Math::BtTransformToMatrix(principalTemp, principal);
	ALIGNED_FREE(principalTemp);
	Math::BtVector3ToVector3(VECTOR3_PTR(inertia), inertia);
	VECTOR3_DEL(inertia);
	volume = volumeTemp;
}

StridingMeshInterface^ ConvexTriangleMeshShape::MeshInterface::get()
{
	btStridingMeshInterface* stridingMeshInterface = Native->getMeshInterface();
	ReturnCachedObject(StridingMeshInterface, _stridingMeshInterface, stridingMeshInterface);
}
