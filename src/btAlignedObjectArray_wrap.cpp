#include <BulletSoftBody/btSoftBody.h>

#include "conversion.h"
#include "btAlignedObjectArray_wrap.h"

btBroadphasePair* btAlignedBroadphasePairArray_at(btAlignedBroadphasePairArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedBroadphasePairArray_push_back(btAlignedBroadphasePairArray* obj, btBroadphasePair* val)
{
	obj->push_back(*val);
}

void btAlignedBroadphasePairArray_resizeNoInitialize(btAlignedBroadphasePairArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedBroadphasePairArray_size(btAlignedBroadphasePairArray* obj)
{
	return obj->size();
}

void btAlignedBroadphasePairArray_delete(btAlignedBroadphasePairArray* obj)
{
	delete obj;
}


btCollisionObject* btAlignedCollisionObjectArray_at(btAlignedCollisionObjectArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedCollisionObjectArray_push_back(btAlignedCollisionObjectArray* obj, btCollisionObject* val)
{
	obj->push_back(val);
}

void btAlignedCollisionObjectArray_resizeNoInitialize(btAlignedCollisionObjectArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedCollisionObjectArray_size(btAlignedCollisionObjectArray* obj)
{
	return obj->size();
}

void btAlignedCollisionObjectArray_delete(btAlignedCollisionObjectArray* obj)
{
	delete obj;
}


EXPORT btAlignedCollisionShapeArray* btAlignedCollisionShapeArray_new()
{
	return new btAlignedCollisionShapeArray();
}

void btAlignedCollisionShapeArray_push_back(btAlignedCollisionShapeArray* obj, btCollisionShape* val)
{
	obj->push_back(val);
}

btCollisionShape* btAlignedCollisionShapeArray_at(btAlignedCollisionShapeArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedCollisionShapeArray_resizeNoInitialize(btAlignedCollisionShapeArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedCollisionShapeArray_size(btAlignedCollisionShapeArray* obj)
{
	return obj->size();
}

void btAlignedCollisionShapeArray_delete(btAlignedCollisionShapeArray* obj)
{
	delete obj;
}


btSoftBody* btAlignedSoftBodyArray_at(btSoftBody::tSoftBodyArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedSoftBodyArray_push_back(btSoftBody::tSoftBodyArray* obj, btSoftBody* val)
{
	obj->push_back(val);
}

void btAlignedSoftBodyArray_resizeNoInitialize(btSoftBody::tSoftBodyArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyArray_size(btSoftBody::tSoftBodyArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyArray_delete(btSoftBody::tSoftBodyArray* obj)
{
	delete obj;
}


btIndexedMesh* btAlignedIndexedMeshArray_at(btAlignedIndexedMeshArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedIndexedMeshArray_push_back(btAlignedIndexedMeshArray* obj, btIndexedMesh* val)
{
	obj->push_back(*val);
}

void btAlignedIndexedMeshArray_resizeNoInitialize(btAlignedIndexedMeshArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedIndexedMeshArray_size(btAlignedIndexedMeshArray* obj)
{
	return obj->size();
}

void btAlignedIndexedMeshArray_delete(btAlignedIndexedMeshArray* obj)
{
	delete obj;
}


EXPORT btManifoldArray* btManifoldArray_new()
{
	return new btManifoldArray();
}

btPersistentManifold* btManifoldArray_at(btManifoldArray* obj, int n)
{
	return obj->at(n);
}

void btManifoldArray_push_back(btManifoldArray* obj, btPersistentManifold* val)
{
	obj->push_back(val);
}

void btManifoldArray_resizeNoInitialize(btManifoldArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btManifoldArray_size(btManifoldArray* obj)
{
	return obj->size();
}

void btManifoldArray_delete(btManifoldArray* obj)
{
	delete obj;
}


btSoftBody::Cluster* btAlignedSoftBodyClusterArray_at(btSoftBody::tClusterArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedSoftBodyClusterArray_push_back(btSoftBody::tClusterArray* obj, btSoftBody::Cluster* val)
{
	obj->push_back(val);
}

void btAlignedSoftBodyClusterArray_resizeNoInitialize(btSoftBody::tClusterArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyClusterArray_size(btSoftBody::tClusterArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyClusterArray_delete(btSoftBody::tClusterArray* obj)
{
	delete obj;
}


btSoftBody::Face* btAlignedSoftBodyFaceArray_at(btSoftBody::tFaceArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedSoftBodyFaceArray_push_back(btSoftBody::tFaceArray* obj, btSoftBody::Face* val)
{
	obj->push_back(*val);
}

void btAlignedSoftBodyFaceArray_resizeNoInitialize(btSoftBody::tFaceArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyFaceArray_size(btSoftBody::tFaceArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyFaceArray_delete(btSoftBody::tFaceArray* obj)
{
	delete obj;
}


btSoftBody::Link* btAlignedSoftBodyLinkArray_at(btSoftBody::tLinkArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedSoftBodyLinkArray_push_back(btSoftBody::tLinkArray* obj, btSoftBody::Link* val)
{
	obj->push_back(*val);
}

void btAlignedSoftBodyLinkArray_resizeNoInitialize(btSoftBody::tLinkArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyLinkArray_size(btSoftBody::tLinkArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyLinkArray_delete(btSoftBody::tLinkArray* obj)
{
	delete obj;
}


btSoftBody::Material* btAlignedSoftBodyMaterialArray_at(btSoftBody::tMaterialArray* obj, int n)
{
	return obj->at(n);
}

void btAlignedSoftBodyMaterialArray_push_back(btSoftBody::tMaterialArray* obj, btSoftBody::Material* val)
{
	obj->push_back(val);
}

void btAlignedSoftBodyMaterialArray_resizeNoInitialize(btSoftBody::tMaterialArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyMaterialArray_size(btSoftBody::tMaterialArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyMaterialArray_delete(btSoftBody::tMaterialArray* obj)
{
	delete obj;
}


btSoftBody::Node* btAlignedSoftBodyNodeArray_at(btSoftBody::tNodeArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedSoftBodyNodeArray_push_back(btSoftBody::tNodeArray* obj, btSoftBody::Node* val)
{
	obj->push_back(*val);
}

void btAlignedSoftBodyNodeArray_resizeNoInitialize(btSoftBody::tNodeArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyNodeArray_size(btSoftBody::tNodeArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyNodeArray_delete(btSoftBody::tNodeArray* obj)
{
	delete obj;
}


btSoftBody::Tetra* btAlignedSoftBodyTetraArray_at(btSoftBody::tTetraArray* obj, int n)
{
	return &obj->at(n);
}

void btAlignedSoftBodyTetraArray_push_back(btSoftBody::tTetraArray* obj, btSoftBody::Tetra* val)
{
	obj->push_back(*val);
}

void btAlignedSoftBodyTetraArray_resizeNoInitialize(btSoftBody::tTetraArray* obj, int newSize)
{
	return obj->resizeNoInitialize(newSize);
}

int btAlignedSoftBodyTetraArray_size(btSoftBody::tTetraArray* obj)
{
	return obj->size();
}

void btAlignedSoftBodyTetraArray_delete(btSoftBody::tTetraArray* obj)
{
	delete obj;
}
