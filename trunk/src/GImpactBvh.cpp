#include "StdAfx.h"

#ifndef DISABLE_BVH

#include "AlignedObjectArray.h"
#include "BoxCollision.h"
#include "GImpactBvh.h"

GImpactBvh::GImpactBvh(btGImpactBvh* bvh)
{
	_bvh = bvh;
}

GImpactBvh::GImpactBvh()
{
	_bvh = new btGImpactBvh();
}

bool GImpactBvh::BoxQuery(Aabb^ box, [Out] IntArray^% collided_results)
{
	return _bvh->boxQuery(*box->UnmanagedPointer, *collided_results->UnmanagedPointer);
}

bool GImpactBvh::BoxQuery(Aabb^ box, Matrix transform, [Out] IntArray^% collided_results)
{
	btTransform* transformTemp = Math::MatrixToBtTransform(transform);
	bool ret = _bvh->boxQueryTrans(*box->UnmanagedPointer, *transformTemp, *collided_results->UnmanagedPointer);
	delete transformTemp;
	return ret;
}

void GImpactBvh::BuildSet()
{
	_bvh->buildSet();
}

int GImpactBvh::GetEscapeNodeIndex(int nodeIndex)
{
	return _bvh->getEscapeNodeIndex(nodeIndex);
}

int GImpactBvh::GetLeftNode(int nodeIndex)
{
	return _bvh->getLeftNode(nodeIndex);
}

void GImpactBvh::GetNodeBound(int nodeIndex, [Out] Aabb^% bound)
{
	btAABB* boundTemp = new btAABB;
	_bvh->getNodeBound(nodeIndex, *boundTemp);
	bound = gcnew Aabb(boundTemp);
}

int GImpactBvh::GetNodeData(int nodeIndex)
{
	return _bvh->getNodeData(nodeIndex);
}

int GImpactBvh::GetRightNode(int nodeIndex)
{
	return _bvh->getRightNode(nodeIndex);
}

bool GImpactBvh::IsLeafNode(int nodeIndex)
{
	return _bvh->isLeafNode(nodeIndex);
}

bool GImpactBvh::RayQuery(Vector3 ray_dir, Vector3 ray_origin, [Out] IntArray^% collided_results)
{
	btVector3* ray_dirTemp = Math::Vector3ToBtVector3(ray_dir);
	btVector3* ray_originTemp = Math::Vector3ToBtVector3(ray_origin);
	btAlignedObjectArray<int>* collided_resultsTemp = new btAlignedObjectArray<int>();

	bool ret = _bvh->rayQuery(*ray_dirTemp, *ray_originTemp, *collided_resultsTemp);
	collided_results = gcnew IntArray(collided_resultsTemp);

	delete ray_dirTemp;
	delete ray_originTemp;

	return ret;
}

void GImpactBvh::SetNodeBound(int nodeIndex, Aabb^ bound)
{
	_bvh->setNodeBound(nodeIndex, *bound->UnmanagedPointer);
}

void GImpactBvh::Update()
{
	_bvh->update();
}

#pragma managed(push, off)
void GImpactBvh_GlobalBox(btGImpactBvh* bvh, btAABB* aabb)
{
	*aabb = bvh->getGlobalBox();
}
#pragma managed(pop)
Aabb^ GImpactBvh::GlobalBox::get()
{
	btAABB* aabb = new btAABB;
	GImpactBvh_GlobalBox(_bvh, aabb);
	return gcnew Aabb(aabb);
}

bool GImpactBvh::HasHierarchy::get()
{
	return _bvh->hasHierarchy();
}

bool GImpactBvh::IsTrimesh::get()
{
	return _bvh->isTrimesh();
}

int GImpactBvh::NodeCount::get()
{
	return _bvh->getNodeCount();
}

#endif
