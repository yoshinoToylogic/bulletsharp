#include "StdAfx.h"

#ifndef DISABLE_GIMPACT

#include "BoxCollision.h"
#include "GImpactShape.h"
#include "StridingMeshInterface.h"
#include "TriangleShapeEx.h"

#define Unmanaged static_cast<btTetrahedronShapeEx*>(_unmanaged)

TetrahedronShapeEx::TetrahedronShapeEx(btTetrahedronShapeEx* shape)
: BU_Simplex1to4(shape)
{
}

TetrahedronShapeEx::TetrahedronShapeEx()
: BU_Simplex1to4(new btTetrahedronShapeEx())
{
}

void TetrahedronShapeEx::SetVertices(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
{
	VECTOR3_DEF(v0);
	VECTOR3_DEF(v1);
	VECTOR3_DEF(v2);
	VECTOR3_DEF(v3);

	Unmanaged->setVertices(VECTOR3_USE(v0), VECTOR3_USE(v1), VECTOR3_USE(v2), VECTOR3_USE(v3));

	VECTOR3_DEL(v0);
	VECTOR3_DEL(v1);
	VECTOR3_DEL(v2);
	VECTOR3_DEL(v3);
}


#undef Unmanaged
#define Unmanaged static_cast<btGImpactShapeInterface*>(_unmanaged)

GImpactShapeInterface::GImpactShapeInterface(btGImpactShapeInterface* shapeInterface)
: ConcaveShape(shapeInterface)
{
}

void GImpactShapeInterface::GetBulletTetrahedron(int prim_index, [Out] TetrahedronShapeEx^% tetrahedron)
{
	btTetrahedronShapeEx* tetrahedronTemp = new btTetrahedronShapeEx;
	Unmanaged->getBulletTetrahedron(prim_index, *tetrahedronTemp);
	tetrahedron = gcnew TetrahedronShapeEx(tetrahedronTemp);
}

void GImpactShapeInterface::GetBulletTriangle(int prim_index, [Out] TriangleShapeEx^% triangle)
{
	btTriangleShapeEx* triangleTemp = new btTriangleShapeEx;
	Unmanaged->getBulletTriangle(prim_index, *triangleTemp);
	triangle = gcnew TriangleShapeEx(triangleTemp);
}

CollisionShape^ GImpactShapeInterface::GetChildShape(int index)
{
	return CollisionShape::GetManaged(Unmanaged->getChildShape(index));
}

#pragma managed(push, off)
btTransform* GImpactShapeInterface_GetChildTransform(btGImpactShapeInterface* shape, int index)
{
	btTransform* transform = new btTransform;
	*transform = shape->getChildTransform(index);
	return transform;
}
#pragma managed(pop)
Matrix GImpactShapeInterface::GetChildTransform(int index)
{
	btTransform* transformTemp = GImpactShapeInterface_GetChildTransform(Unmanaged, index);
	Matrix transform = Math::BtTransformToMatrix(transformTemp);
	delete transformTemp;
	return transform;
}

void GImpactShapeInterface::GetPrimitiveTriangle(int prim_index, [Out] PrimitiveTriangle^% triangle)
{
	btPrimitiveTriangle* triangleTemp = new btPrimitiveTriangle();
	Unmanaged->getPrimitiveTriangle(prim_index, *triangleTemp);
	triangle = gcnew PrimitiveTriangle(triangleTemp);
}

void GImpactShapeInterface::LockChildShapes()
{
	Unmanaged->lockChildShapes();
}

void GImpactShapeInterface::PostUpdate()
{
	Unmanaged->postUpdate();
}

void GImpactShapeInterface::RayTest(Vector3 rayFrom, Vector3 rayTo, CollisionWorld::RayResultCallback^ resultCallback)
{
	VECTOR3_DEF(rayFrom);
	VECTOR3_DEF(rayTo);

	Unmanaged->rayTest(VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), *resultCallback->_unmanaged);

	VECTOR3_DEL(rayFrom);
	VECTOR3_DEL(rayTo);
}

void GImpactShapeInterface::SetChildTransform(int index, Matrix transform)
{
	btTransform* transformTemp = Math::MatrixToBtTransform(transform);
	Unmanaged->setChildTransform(index, *transformTemp);
	delete transformTemp;
}

void GImpactShapeInterface::UnlockChildShapes()
{
	Unmanaged->unlockChildShapes();
}

void GImpactShapeInterface::UpdateBound()
{
	Unmanaged->updateBound();
}

#ifndef DISABLE_BVH
GImpactQuantizedBvh^ GImpactShapeInterface::BoxSet::get()
{
	return gcnew GImpactQuantizedBvh(Unmanaged->getBoxSet());
}
#endif

bool GImpactShapeInterface::ChildrenHasTransform::get()
{
	return Unmanaged->childrenHasTransform();
}

BulletSharp::GImpactShapeType GImpactShapeInterface::GImpactShapeType::get()
{
	return (BulletSharp::GImpactShapeType)Unmanaged->getGImpactShapeType();
}

bool GImpactShapeInterface::HasBoxSet::get()
{
	return Unmanaged->hasBoxSet();
}

#pragma managed(push, off)
void GImpactShapeInterface_GetLocalBox(btGImpactShapeInterface* shape, btAABB* aabb)
{
	*aabb = shape->getLocalBox();
}
#pragma managed(pop)
Aabb^ GImpactShapeInterface::LocalBox::get()
{
	btAABB* boxTemp = new btAABB();
	GImpactShapeInterface_GetLocalBox(Unmanaged, boxTemp);
	return gcnew Aabb(boxTemp);
}

bool GImpactShapeInterface::NeedsRetrieveTetrahedrons::get()
{
	return Unmanaged->needsRetrieveTetrahedrons();
}

bool GImpactShapeInterface::NeedsRetrieveTriangles::get()
{
	return Unmanaged->needsRetrieveTriangles();
}

int GImpactShapeInterface::NumChildShapes::get()
{
	return Unmanaged->getNumChildShapes();
}

#ifndef DISABLE_BVH
PrimitiveManagerBase^ GImpactShapeInterface::PrimitiveManager::get()
{
	return gcnew PrimitiveManagerBase((btPrimitiveManagerBase*)Unmanaged->getPrimitiveManager());
}
#endif


#ifndef DISABLE_BVH
GImpactCompoundShape::CompoundPrimitiveManager::CompoundPrimitiveManager(btGImpactCompoundShape::CompoundPrimitiveManager* compound)
: PrimitiveManagerBase(compound)
{
}

GImpactCompoundShape::CompoundPrimitiveManager::CompoundPrimitiveManager(CompoundPrimitiveManager^ compound)
: PrimitiveManagerBase(new btGImpactCompoundShape::CompoundPrimitiveManager(*compound->UnmanagedPointer))
{
}

GImpactCompoundShape::CompoundPrimitiveManager::CompoundPrimitiveManager(GImpactCompoundShape^ compoundShape)
: PrimitiveManagerBase(new btGImpactCompoundShape::CompoundPrimitiveManager(compoundShape->UnmanagedPointer))
{
}

GImpactCompoundShape::CompoundPrimitiveManager::CompoundPrimitiveManager()
: PrimitiveManagerBase(new btGImpactCompoundShape::CompoundPrimitiveManager())
{
}

GImpactCompoundShape^ GImpactCompoundShape::CompoundPrimitiveManager::CompoundShape::get()
{
	return gcnew GImpactCompoundShape(UnmanagedPointer->m_compoundShape);
}

btGImpactCompoundShape::CompoundPrimitiveManager* GImpactCompoundShape::CompoundPrimitiveManager::UnmanagedPointer::get()
{
	return (btGImpactCompoundShape::CompoundPrimitiveManager*)PrimitiveManagerBase::UnmanagedPointer;
}
#endif


#undef Unmanaged
#define Unmanaged (static_cast<btGImpactCompoundShape*>(_unmanaged))

GImpactCompoundShape::GImpactCompoundShape(bool childrenHasTransform)
: GImpactShapeInterface(new btGImpactCompoundShape(childrenHasTransform))
{
}

GImpactCompoundShape::GImpactCompoundShape()
: GImpactShapeInterface(new btGImpactCompoundShape())
{
}

void GImpactCompoundShape::AddChildShape(CollisionShape^ shape)
{
	Unmanaged->addChildShape(shape->_unmanaged);
}

void GImpactCompoundShape::AddChildShape(Matrix localTransform, CollisionShape^ shape)
{
	btTransform* localTransformTemp = Math::MatrixToBtTransform(localTransform);
	Unmanaged->addChildShape(*localTransformTemp, shape->_unmanaged);
	delete localTransformTemp;
}

#ifndef DISABLE_BVH
GImpactCompoundShape::CompoundPrimitiveManager^ GImpactCompoundShape::GImpactCompoundPrimitiveManager::get()
{
	return gcnew CompoundPrimitiveManager(Unmanaged->getCompoundPrimitiveManager());
}
#endif


#ifndef DISABLE_BVH
GImpactMeshShapePart::TrimeshPrimitiveManager::TrimeshPrimitiveManager(btGImpactMeshShapePart::TrimeshPrimitiveManager* manager)
: PrimitiveManagerBase(manager)
{
}

GImpactMeshShapePart::TrimeshPrimitiveManager::TrimeshPrimitiveManager()
: PrimitiveManagerBase(new btGImpactMeshShapePart::TrimeshPrimitiveManager())
{
}

GImpactMeshShapePart::TrimeshPrimitiveManager::TrimeshPrimitiveManager(TrimeshPrimitiveManager^ manager)
: PrimitiveManagerBase(new btGImpactMeshShapePart::TrimeshPrimitiveManager(*manager->UnmanagedPointer))
{
}

GImpactMeshShapePart::TrimeshPrimitiveManager::TrimeshPrimitiveManager(StridingMeshInterface^ meshInterface, int part)
: PrimitiveManagerBase(new btGImpactMeshShapePart::TrimeshPrimitiveManager(meshInterface->UnmanagedPointer, part))
{
}

void GImpactMeshShapePart::TrimeshPrimitiveManager::GetBulletTriangle(int prim_index, [Out] TriangleShapeEx^% triangle)
{
	btTriangleShapeEx* triangleTemp = new btTriangleShapeEx;
	UnmanagedPointer->get_bullet_triangle(prim_index, *triangleTemp);
	triangle = gcnew TriangleShapeEx(triangleTemp);
}

void GImpactMeshShapePart::TrimeshPrimitiveManager::GetIndices(int face_index, int% i0, int% i1, int% i2)
{
	int i0Temp;
	int i1Temp;
	int i2Temp;

	UnmanagedPointer->get_indices(face_index, i0Temp, i1Temp, i2Temp);

	i0 = i0Temp;
	i1 = i1Temp;
	i2 = i2Temp;
}

void GImpactMeshShapePart::TrimeshPrimitiveManager::Lock()
{
	UnmanagedPointer->lock();
}

void GImpactMeshShapePart::TrimeshPrimitiveManager::Unlock()
{
	UnmanagedPointer->unlock();
}

IntPtr GImpactMeshShapePart::TrimeshPrimitiveManager::IndexBase::get()
{
	return IntPtr((void*)UnmanagedPointer->indexbase);
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::IndexBase::set(IntPtr value)
{
	UnmanagedPointer->indexbase = (unsigned char*)value.ToPointer();
}

int GImpactMeshShapePart::TrimeshPrimitiveManager::IndexStride::get()
{
	return UnmanagedPointer->indexstride;
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::IndexStride::set(int value)
{
	UnmanagedPointer->indexstride = value;
}

PhyScalarType GImpactMeshShapePart::TrimeshPrimitiveManager::IndicesType::get()
{
	return (PhyScalarType)UnmanagedPointer->indicestype;
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::IndicesType::set(PhyScalarType value)
{
	UnmanagedPointer->indicestype = (PHY_ScalarType)value;
}

int GImpactMeshShapePart::TrimeshPrimitiveManager::LockCount::get()
{
	return UnmanagedPointer->m_lock_count;
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::LockCount::set(int value)
{
	UnmanagedPointer->m_lock_count = value;
}

btScalar GImpactMeshShapePart::TrimeshPrimitiveManager::Margin::get()
{
	return UnmanagedPointer->m_margin;
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::Margin::set(btScalar value)
{
	UnmanagedPointer->m_margin = value;
}

StridingMeshInterface^ GImpactMeshShapePart::TrimeshPrimitiveManager::MeshInterface::get()
{
	return StridingMeshInterface::GetManaged(UnmanagedPointer->m_meshInterface);
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::MeshInterface::set(StridingMeshInterface^ value)
{
	UnmanagedPointer->m_meshInterface = value->UnmanagedPointer;
}

int GImpactMeshShapePart::TrimeshPrimitiveManager::NumFaces::get()
{
	return UnmanagedPointer->numfaces;
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::NumFaces::set(int value)
{
	UnmanagedPointer->numfaces = value;
}

int GImpactMeshShapePart::TrimeshPrimitiveManager::NumVerts::get()
{
	return UnmanagedPointer->numverts;
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::NumVerts::set(int value)
{
	UnmanagedPointer->numverts = value;
}

int GImpactMeshShapePart::TrimeshPrimitiveManager::Part::get()
{
	return UnmanagedPointer->m_part;
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::Part::set(int value)
{
	UnmanagedPointer->m_part = value;
}

Vector3 GImpactMeshShapePart::TrimeshPrimitiveManager::Scale::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->m_scale);
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::Scale::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &UnmanagedPointer->m_scale);
}

int GImpactMeshShapePart::TrimeshPrimitiveManager::Stride::get()
{
	return UnmanagedPointer->stride;
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::Stride::set(int value)
{
	UnmanagedPointer->stride = value;
}

PhyScalarType GImpactMeshShapePart::TrimeshPrimitiveManager::Type::get()
{
	return (PhyScalarType)UnmanagedPointer->type;
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::Type::set(PhyScalarType value)
{
	UnmanagedPointer->type = (PHY_ScalarType)value;
}

IntPtr GImpactMeshShapePart::TrimeshPrimitiveManager::VertexBase::get()
{
	return IntPtr((void*)UnmanagedPointer->vertexbase);
}
void GImpactMeshShapePart::TrimeshPrimitiveManager::VertexBase::set(IntPtr value)
{
	UnmanagedPointer->vertexbase = (unsigned char*)value.ToPointer();
}

btGImpactMeshShapePart::TrimeshPrimitiveManager* GImpactMeshShapePart::TrimeshPrimitiveManager::UnmanagedPointer::get()
{
	return (btGImpactMeshShapePart::TrimeshPrimitiveManager*)PrimitiveManagerBase::UnmanagedPointer;
}
#endif


#undef Unmanaged
#define Unmanaged static_cast<btGImpactMeshShapePart*>(_unmanaged)

GImpactMeshShapePart::GImpactMeshShapePart(btGImpactMeshShapePart* shape)
: GImpactShapeInterface(shape)
{
}

GImpactMeshShapePart::GImpactMeshShapePart()
: GImpactShapeInterface(new btGImpactMeshShapePart())
{
}

GImpactMeshShapePart::GImpactMeshShapePart(StridingMeshInterface^ meshInterface, int part)
: GImpactShapeInterface(new btGImpactMeshShapePart(meshInterface->UnmanagedPointer, part))
{
}

void GImpactMeshShapePart::GetVertex(int vertex_index, [Out] Vector3% vertex)
{
	btVector3* vertexTemp = new btVector3;
	Unmanaged->getVertex(vertex_index, *vertexTemp);
	Math::BtVector3ToVector3(vertexTemp, vertex);
	delete vertexTemp;
}

int GImpactMeshShapePart::Part::get()
{
	return Unmanaged->getPart();
}

int GImpactMeshShapePart::VertexCount::get()
{
	return Unmanaged->getVertexCount();
}

#ifndef DISABLE_BVH
GImpactMeshShapePart::TrimeshPrimitiveManager^ GImpactMeshShapePart::GImpactTrimeshPrimitiveManager::get()
{
	return gcnew TrimeshPrimitiveManager(UnmanagedPointer->getTrimeshPrimitiveManager());
}
#endif


#undef Unmanaged
#define Unmanaged static_cast<btGImpactMeshShape*>(_unmanaged)

GImpactMeshShape::GImpactMeshShape(btGImpactMeshShape* shape)
: GImpactShapeInterface(shape)
{
}

GImpactMeshShape::GImpactMeshShape(StridingMeshInterface^ meshInterface)
: GImpactShapeInterface(new btGImpactMeshShape(meshInterface->UnmanagedPointer))
{
}

GImpactMeshShapePart^ GImpactMeshShape::GetMeshPart(int index)
{
	return gcnew GImpactMeshShapePart(Unmanaged->getMeshPart(index));
}

StridingMeshInterface^ GImpactMeshShape::MeshInterface::get()
{
	return gcnew StridingMeshInterface(Unmanaged->getMeshInterface());
}

int GImpactMeshShape::MeshPartCount::get()
{
	return Unmanaged->getMeshPartCount();
}

#endif