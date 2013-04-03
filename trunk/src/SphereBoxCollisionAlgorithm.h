#pragma once

#include "ActivatingCollisionAlgorithm.h"
#include "CollisionCreateFunc.h"

namespace BulletSharp
{
	ref class CollisionObjectWrapper;

	public ref class SphereBoxCollisionAlgorithm : ActivatingCollisionAlgorithm
	{
	public:
		ref class CreateFunc : CollisionAlgorithmCreateFunc
		{
		public:
			CreateFunc();
		};

		SphereBoxCollisionAlgorithm(PersistentManifold^ mf, CollisionAlgorithmConstructionInfo^ ci,
			CollisionObjectWrapper^ body0Wrap, CollisionObjectWrapper^ body1Wrap, bool isSwapped);

		bool GetSphereDistance(CollisionObjectWrapper^ boxObjWrap, Vector3 v3PointOnBox, Vector3 normal,
			btScalar% penetrationDepth, Vector3 v3SphereCenter, btScalar fRadius, btScalar maxContactDistance);
		btScalar GetSpherePenetration(Vector3 boxHalfExtent, Vector3 sphereRelPos, Vector3 closestPoint,
			Vector3 normal);
	};
};
