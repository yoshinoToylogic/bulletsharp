#include "StdAfx.h"

#include "BvhTriangleMeshShape.h"
#include "StridingMeshInterface.h"
#include "TriangleCallback.h"
#include "TriangleInfoMap.h"
#ifndef DISABLE_BVH
#include "OptimizedBvh.h"
#endif
#ifndef DISABLE_SERIALIZE
#include "Serializer.h"
#endif

#define Native static_cast<btBvhTriangleMeshShape*>(_native)

BvhTriangleMeshShape::BvhTriangleMeshShape(btBvhTriangleMeshShape* native)
	: TriangleMeshShape(native)
{
}

BvhTriangleMeshShape::BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
	bool buildBvh)
	: TriangleMeshShape(new btBvhTriangleMeshShape(meshInterface->_native, useQuantizedAabbCompression,
		buildBvh))
{
	_meshInterface = meshInterface;
}

BvhTriangleMeshShape::BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression)
	: TriangleMeshShape(new btBvhTriangleMeshShape(meshInterface->_native, useQuantizedAabbCompression))
{
	_meshInterface = meshInterface;
}

BvhTriangleMeshShape::BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
	Vector3% bvhAabbMin, Vector3% bvhAabbMax, bool buildBvh)
	: TriangleMeshShape(0)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	UnmanagedPointer = new btBvhTriangleMeshShape(meshInterface->_native, useQuantizedAabbCompression,
		VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax), buildBvh);
	VECTOR3_DEL(bvhAabbMin);
	VECTOR3_DEL(bvhAabbMax);

	_meshInterface = meshInterface;
}

BvhTriangleMeshShape::BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
	Vector3 bvhAabbMin, Vector3 bvhAabbMax, bool buildBvh)
	: TriangleMeshShape(0)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	UnmanagedPointer = new btBvhTriangleMeshShape(meshInterface->_native, useQuantizedAabbCompression,
		VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax), buildBvh);
	VECTOR3_DEL(bvhAabbMin);
	VECTOR3_DEL(bvhAabbMax);

	_meshInterface = meshInterface;
}

BvhTriangleMeshShape::BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
	Vector3% bvhAabbMin, Vector3% bvhAabbMax)
	: TriangleMeshShape(0)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	UnmanagedPointer = new btBvhTriangleMeshShape(meshInterface->_native, useQuantizedAabbCompression,
		VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax));
	VECTOR3_DEL(bvhAabbMin);
	VECTOR3_DEL(bvhAabbMax);

	_meshInterface = meshInterface;
}

BvhTriangleMeshShape::BvhTriangleMeshShape(StridingMeshInterface^ meshInterface, bool useQuantizedAabbCompression,
	Vector3 bvhAabbMin, Vector3 bvhAabbMax)
	: TriangleMeshShape(0)
{
	VECTOR3_CONV(bvhAabbMin);
	VECTOR3_CONV(bvhAabbMax);
	UnmanagedPointer = new btBvhTriangleMeshShape(meshInterface->_native, useQuantizedAabbCompression,
		VECTOR3_USE(bvhAabbMin), VECTOR3_USE(bvhAabbMax));
	VECTOR3_DEL(bvhAabbMin);
	VECTOR3_DEL(bvhAabbMax);

	_meshInterface = meshInterface;
}

void BvhTriangleMeshShape::BuildOptimizedBvh()
{
	Native->buildOptimizedBvh();
}

void BvhTriangleMeshShape::PartialRefitTree(Vector3% aabbMin, Vector3% aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	Native->partialRefitTree(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
	VECTOR3_DEL(aabbMin);
	VECTOR3_DEL(aabbMax);
}

void BvhTriangleMeshShape::PartialRefitTree(Vector3 aabbMin, Vector3 aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	Native->partialRefitTree(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
	VECTOR3_DEL(aabbMin);
	VECTOR3_DEL(aabbMax);
}

void BvhTriangleMeshShape::PerformConvexcast(TriangleCallback^ callback, Vector3% boxSource,
	Vector3% boxTarget, Vector3% boxMin, Vector3% boxMax)
{
	VECTOR3_CONV(boxSource);
	VECTOR3_CONV(boxTarget);
	VECTOR3_CONV(boxMin);
	VECTOR3_CONV(boxMax);
	Native->performConvexcast(callback->_native, VECTOR3_USE(boxSource), VECTOR3_USE(boxTarget),
		VECTOR3_USE(boxMin), VECTOR3_USE(boxMax));
	VECTOR3_DEL(boxSource);
	VECTOR3_DEL(boxTarget);
	VECTOR3_DEL(boxMin);
	VECTOR3_DEL(boxMax);
}

void BvhTriangleMeshShape::PerformConvexcast(TriangleCallback^ callback, Vector3 boxSource,
	Vector3 boxTarget, Vector3 boxMin, Vector3 boxMax)
{
	VECTOR3_CONV(boxSource);
	VECTOR3_CONV(boxTarget);
	VECTOR3_CONV(boxMin);
	VECTOR3_CONV(boxMax);
	Native->performConvexcast(callback->_native, VECTOR3_USE(boxSource), VECTOR3_USE(boxTarget),
		VECTOR3_USE(boxMin), VECTOR3_USE(boxMax));
	VECTOR3_DEL(boxSource);
	VECTOR3_DEL(boxTarget);
	VECTOR3_DEL(boxMin);
	VECTOR3_DEL(boxMax);
}

void BvhTriangleMeshShape::PerformRaycast(TriangleCallback^ callback, Vector3% raySource,
	Vector3% rayTarget)
{
	VECTOR3_CONV(raySource);
	VECTOR3_CONV(rayTarget);
	Native->performRaycast(callback->_native, VECTOR3_USE(raySource), VECTOR3_USE(rayTarget));
	VECTOR3_DEL(raySource);
	VECTOR3_DEL(rayTarget);
}

void BvhTriangleMeshShape::PerformRaycast(TriangleCallback^ callback, Vector3 raySource,
	Vector3 rayTarget)
{
	VECTOR3_CONV(raySource);
	VECTOR3_CONV(rayTarget);
	Native->performRaycast(callback->_native, VECTOR3_USE(raySource), VECTOR3_USE(rayTarget));
	VECTOR3_DEL(raySource);
	VECTOR3_DEL(rayTarget);
}

void BvhTriangleMeshShape::RefitTree(Vector3% aabbMin, Vector3% aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	Native->refitTree(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
	VECTOR3_DEL(aabbMin);
	VECTOR3_DEL(aabbMax);
}

void BvhTriangleMeshShape::RefitTree(Vector3 aabbMin, Vector3 aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	Native->refitTree(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
	VECTOR3_DEL(aabbMin);
	VECTOR3_DEL(aabbMax);
}

#ifndef DISABLE_SERIALIZE
void BvhTriangleMeshShape::SerializeSingleBvh(BulletSharp::Serializer^ serializer)
{
	Native->serializeSingleBvh(serializer->_native);
}

void BvhTriangleMeshShape::SerializeSingleTriangleInfoMap(BulletSharp::Serializer^ serializer)
{
	Native->serializeSingleTriangleInfoMap(serializer->_native);
}
#endif

#ifndef DISABLE_BVH
void BvhTriangleMeshShape::SetOptimizedBvh(BulletSharp::OptimizedBvh^ bvh, Vector3 localScaling)
{
	VECTOR3_CONV(localScaling);
	Native->setOptimizedBvh((btOptimizedBvh*)bvh->_native, VECTOR3_USE(localScaling));
	VECTOR3_DEL(localScaling);
}

#pragma managed(push, off)
void BvhTriangleMeshShape_SetOptimizedBvh(btBvhTriangleMeshShape* shape, btOptimizedBvh* bvh)
{
	shape->setOptimizedBvh(bvh);
}
#pragma managed(pop)

OptimizedBvh^ BvhTriangleMeshShape::OptimizedBvh::get()
{
	btOptimizedBvh* optimizedBvh = Native->getOptimizedBvh();
	ReturnCachedObjectCast(BulletSharp::OptimizedBvh, _optimizedBvh, optimizedBvh)
}
void BvhTriangleMeshShape::OptimizedBvh::set(BulletSharp::OptimizedBvh^ value)
{
	BvhTriangleMeshShape_SetOptimizedBvh(Native, (btOptimizedBvh*)value->_native);
}
#endif

bool BvhTriangleMeshShape::OwnsBvh::get()
{
	return Native->getOwnsBvh();
}

BulletSharp::TriangleInfoMap^ BvhTriangleMeshShape::TriangleInfoMap::get()
{
	if (_triangleInfoMap == nullptr)
	{
		_triangleInfoMap = gcnew BulletSharp::TriangleInfoMap(Native->getTriangleInfoMap(), true);
	}
	return _triangleInfoMap;
}
void BvhTriangleMeshShape::TriangleInfoMap::set(BulletSharp::TriangleInfoMap^ triangleInfoMap)
{
	_triangleInfoMap = triangleInfoMap;
	Native->setTriangleInfoMap(triangleInfoMap->_native);
}

bool BvhTriangleMeshShape::UsesQuantizedAabbCompression::get()
{
	return Native->usesQuantizedAabbCompression();
}
