#include "StdAfx.h"

#include "DataStream.h"
#include "TriangleIndexVertexArray.h"

IndexedMesh::IndexedMesh(btIndexedMesh* indexedMesh)
{
	_indexedMesh = indexedMesh;
}

IndexedMesh::IndexedMesh()
{
	_indexedMesh = new btIndexedMesh();
}

void IndexedMesh::Allocate(int numVerts, int vertexStride, int numIndices, int indexStride)
{
	AllocateVerts(numVerts, vertexStride);
	AllocateIndices(numIndices, indexStride);
}

void IndexedMesh::AllocateVerts(int num, int stride)
{
	_indexedMesh->m_numVertices = num;
	_indexedMesh->m_vertexStride = stride;
	//_indexedMesh->m_indexType = PHY_INTEGER;

	_indexedMesh->m_vertexBase = (const unsigned char *)new btVector3[num];
}

void IndexedMesh::AllocateIndices(int num, int stride)
{
	_indexedMesh->m_numTriangles = num;
	_indexedMesh->m_triangleIndexStride = stride;
	//_IndexedMesh->m_vertexType = PHY_FLOAT;

	_indexedMesh->m_triangleIndexBase = (const unsigned char *)new int[num*3];
}

BulletSharp::DataStream^ IndexedMesh::LockVerts()
{
	return gcnew DataStream((void*)_indexedMesh->m_vertexBase,
		_indexedMesh->m_numVertices * _indexedMesh->m_vertexStride, true, true, false);
}

BulletSharp::DataStream^ IndexedMesh::LockIndices()
{
	return gcnew DataStream((void*)_indexedMesh->m_triangleIndexBase,
		_indexedMesh->m_numTriangles * _indexedMesh->m_triangleIndexStride, true, true, false);
}

PhyScalarType IndexedMesh::IndexType::get()
{
	return (PhyScalarType)_indexedMesh->m_indexType;
}
void IndexedMesh::IndexType::set(PhyScalarType value)
{
	_indexedMesh->m_indexType = (PHY_ScalarType)value;
}

int IndexedMesh::NumTriangles::get()
{
	return _indexedMesh->m_numTriangles;
}

int IndexedMesh::NumVertices::get()
{
	return _indexedMesh->m_numVertices;
}

PhyScalarType IndexedMesh::VertexType::get()
{
	return (PhyScalarType)_indexedMesh->m_vertexType;
}
void IndexedMesh::VertexType::set(PhyScalarType value)
{
	_indexedMesh->m_vertexType = (PHY_ScalarType)value;
}

btIndexedMesh* IndexedMesh::UnmanagedPointer::get()
{
	return _indexedMesh;
}
void IndexedMesh::UnmanagedPointer::set(btIndexedMesh* value)
{
	_indexedMesh = value;
}


TriangleIndexVertexArray::TriangleIndexVertexArray(btTriangleIndexVertexArray* vertexArray)
: StridingMeshInterface(vertexArray)
{
}

TriangleIndexVertexArray::TriangleIndexVertexArray()
: StridingMeshInterface(new btTriangleIndexVertexArray())
{
}

TriangleIndexVertexArray::TriangleIndexVertexArray(int numTriangles, IntPtr triangleIndexBase, int triangleIndexStride,
	int numVertices, IntPtr vertexBase, int vertexStride)
: StridingMeshInterface(new btTriangleIndexVertexArray(numTriangles, (int*)triangleIndexBase.ToPointer(), triangleIndexStride,
	numVertices, (btScalar*)vertexBase.ToPointer(), vertexStride))
{
}

TriangleIndexVertexArray::TriangleIndexVertexArray(array<int>^ indices, array<Vector3>^ vertices)
: StridingMeshInterface(0)
{
	pin_ptr<int> indicesBase = &indices[0];
	btVector3* verticesBase = new btVector3[vertices->Length];
	
	for(int i=0; i<vertices->Length; i++)
		Math::Vector3ToBtVector3(vertices[i], &verticesBase[i]);

	UnmanagedPointer = new btTriangleIndexVertexArray(indices->Length, indicesBase, sizeof(int),
		vertices->Length, *verticesBase, sizeof(btVector3));

	delete[] verticesBase;
}

void TriangleIndexVertexArray::AddIndexedMesh(IndexedMesh^ mesh)
{
	UnmanagedPointer->addIndexedMesh(*mesh->UnmanagedPointer);
}

void TriangleIndexVertexArray::AddIndexedMesh(IndexedMesh^ mesh, PhyScalarType indexType)
{
	UnmanagedPointer->addIndexedMesh(*mesh->UnmanagedPointer, static_cast<PHY_ScalarType>(indexType));
}

BtIndexedMeshArray^ TriangleIndexVertexArray::IndexedMeshArray::get()
{
	return gcnew BtIndexedMeshArray(&UnmanagedPointer->getIndexedMeshArray());
}

btTriangleIndexVertexArray* TriangleIndexVertexArray::UnmanagedPointer::get()
{
	return (btTriangleIndexVertexArray*)StridingMeshInterface::UnmanagedPointer;
}