#pragma once

#include "TriangleMeshShape.h"

namespace BulletSharp
{
	ref class OptimizedBvh;
	ref class StridingMeshInterface;
	ref class TriangleCallback;
	ref class TriangleInfoMap;

	public ref class BvhTriangleMeshShape : TriangleMeshShape
	{
	private:
#ifndef DISABLE_BVH
		OptimizedBvh^ _optimizedBvh;
#endif
		TriangleInfoMap^ _triangleInfoMap;

	internal:
		BvhTriangleMeshShape(btBvhTriangleMeshShape* native);

	public:
		BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
			bool buildBvh);
		BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression);
		BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
			Vector3% bvhAabbMin, Vector3% bvhAabbMax, bool buildBvh);
		BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
			Vector3 bvhAabbMin, Vector3 bvhAabbMax, bool buildBvh);
		BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
			Vector3% bvhAabbMin, Vector3% bvhAabbMax);
		BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
			Vector3 bvhAabbMin, Vector3 bvhAabbMax);

		void BuildOptimizedBvh();
		void PartialRefitTree(Vector3% aabbMin, Vector3% aabbMax);
		void PartialRefitTree(Vector3 aabbMin, Vector3 aabbMax);
		void PerformConvexcast(TriangleCallback^ callback, Vector3% boxSource, Vector3% boxTarget,
			Vector3% boxMin, Vector3% boxMax);
		void PerformConvexcast(TriangleCallback^ callback, Vector3 boxSource, Vector3 boxTarget,
			Vector3 boxMin, Vector3 boxMax);
		void PerformRaycast(TriangleCallback^ callback, Vector3% raySource, Vector3% rayTarget);
		void PerformRaycast(TriangleCallback^ callback, Vector3 raySource, Vector3 rayTarget);
		void RefitTree(Vector3% aabbMin, Vector3% aabbMax);
		void RefitTree(Vector3 aabbMin, Vector3 aabbMax);
#ifndef DISABLE_SERIALIZE
		void SerializeSingleBvh(Serializer^ serializer);
		void SerializeSingleTriangleInfoMap(Serializer^ serializer);
#endif
#ifndef DISABLE_BVH
		void SetOptimizedBvh(OptimizedBvh^ bvh, Vector3 localScaling);
#endif

#ifndef DISABLE_BVH
		property BulletSharp::OptimizedBvh^ OptimizedBvh
		{
			BulletSharp::OptimizedBvh^ get();
			void set(BulletSharp::OptimizedBvh^ value);
		}
#endif

		property bool OwnsBvh
		{
			bool get();
		}

		property BulletSharp::TriangleInfoMap^ TriangleInfoMap
		{
			BulletSharp::TriangleInfoMap^ get();
			void set(BulletSharp::TriangleInfoMap^ triangleInfoMap);
		}

		property bool UsesQuantizedAabbCompression
		{
			bool get();
		}
	};
};
