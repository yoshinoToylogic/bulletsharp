#pragma once

// Fully implemented as of 25 May 2010

#pragma managed(push, off)
#include <BulletCollision/CollisionShapes/btMultimaterialTriangleMeshShape.h>
#pragma managed(pop)

#include "BvhTriangleMeshShape.h"

namespace BulletSharp
{
	value class BulletMaterial;
	ref class StridingMeshInterface;

	public ref class MultimaterialTriangleMeshShape : BvhTriangleMeshShape
	{
	internal:
		MultimaterialTriangleMeshShape(btMultimaterialTriangleMeshShape* meshShape);

	public:
		MultimaterialTriangleMeshShape();
		MultimaterialTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression, bool buildBvh);
		MultimaterialTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression);
		MultimaterialTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
			Vector3 bvhAabbMin, Vector3 bvhAabbMax, bool buildBvh);
		MultimaterialTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
			Vector3 bvhAabbMin, Vector3 bvhAabbMax);

		BulletMaterial GetMaterialProperties(int partID, int triIndex);

	internal:
		property btMultimaterialTriangleMeshShape* UnmanagedPointer
		{
			btMultimaterialTriangleMeshShape* get() new;
		}
	};
};
