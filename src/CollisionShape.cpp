#include "StdAfx.h"

#include "BoxShape.h"
#include "CapsuleShape.h"
#include "CollisionShape.h"
#include "CompoundShape.h"
#include "ConeShape.h"
#include "ConvexHullShape.h"
#include "ConvexShape.h"
#include "ConvexTriangleMeshShape.h"
#include "CylinderShape.h"
#include "ConvexShape.h"
#include "EmptyShape.h"
//#include "HfFluidBuoyantConvexShape.h"
//#include "HfFluidShape.h"
#include "MultiSphereShape.h"
#include "SphereShape.h"
#include "StaticPlaneShape.h"
#include "StringConv.h"
#include "TriangleMeshShape.h"
#include "UniformScalingShape.h"
#ifndef DISABLE_GIMPACT
#include "GImpactShape.h"
#endif
#include "CollisionShape.h"
#ifndef DISABLE_SERIALIZE
#include "Serializer.h"
#endif
#ifndef DISABLE_UNCOMMON
#include "Box2dShape.h"
#include "Convex2DShape.h"
#include "ConvexPointCloudShape.h"
#include "HeightfieldTerrainShape.h"
#include "MinkowskiSumShape.h"
#include "MultimaterialTriangleMeshShape.h"
#include "StringConv.h"
#include "TriangleShape.h"
#endif

CollisionShape::CollisionShape(btCollisionShape* collisionShape, bool doesNotOwnObject)
{
	_doesNotOwnObject = doesNotOwnObject;

	if (collisionShape)
		UnmanagedPointer = collisionShape;
}

CollisionShape::CollisionShape(btCollisionShape* collisionShape)
{
	if (collisionShape)
		UnmanagedPointer = collisionShape;
}

CollisionShape::~CollisionShape()
{
	this->!CollisionShape();
}

CollisionShape::!CollisionShape()
{
	if (this->IsDisposed)
		return;
	
	OnDisposing(this, nullptr);

	if (_doesNotOwnObject == false)
	{
		void* userObj = _collisionShape->getUserPointer();
		if (userObj)
			VoidPtrToGCHandle(userObj).Free();
		delete _collisionShape;
	}
	_collisionShape = NULL;

	OnDisposed(this, nullptr);
}

bool CollisionShape::Equals(Object^ obj)
{
	CollisionShape^ p = dynamic_cast<CollisionShape^>(obj);
	if (p == nullptr)
		return false;
	return (_collisionShape == p->UnmanagedPointer);
}

int CollisionShape::GetHashCode()
{
	return (int)_collisionShape;
}

bool CollisionShape::IsDisposed::get()
{
	return ( _collisionShape == NULL );
}

void CollisionShape::CalculateLocalInertia(btScalar mass, [Out] Vector3% inertia)
{
	btVector3* inertiaTemp = new btVector3;
	_collisionShape->calculateLocalInertia(mass, *inertiaTemp);
	Math::BtVector3ToVector3(inertiaTemp, inertia);
	delete inertiaTemp;
}

Vector3 CollisionShape::CalculateLocalInertia(btScalar mass)
{
	btVector3* inertiaTemp = new btVector3;
	_collisionShape->calculateLocalInertia(mass, *inertiaTemp);
	Vector3 inertia = Math::BtVector3ToVector3(inertiaTemp);
	delete inertiaTemp;
	return inertia;
}

void CollisionShape::CalculateTemporalAabb(Matrix curTrans,
	Vector3 linvel,	Vector3 angvel, btScalar timeStep,
	Vector3% temporalAabbMin, Vector3% temporalAabbMax)
{
	btTransform* curTransTemp = Math::MatrixToBtTransform(curTrans);
	btVector3* temporalAabbMinTemp = new btVector3;
	btVector3* temporalAabbMaxTemp = new btVector3;
	btVector3* linvelTemp = Math::Vector3ToBtVector3(linvel);
	btVector3* angvelTemp = Math::Vector3ToBtVector3(angvel);

	_collisionShape->calculateTemporalAabb(*curTransTemp, *linvelTemp, *angvelTemp,
		timeStep, *temporalAabbMinTemp,	*temporalAabbMaxTemp
	);

	temporalAabbMin = Math::BtVector3ToVector3(temporalAabbMaxTemp);
	temporalAabbMax = Math::BtVector3ToVector3(temporalAabbMaxTemp);

	delete curTransTemp;
	delete temporalAabbMinTemp;
	delete temporalAabbMaxTemp;
	delete linvelTemp;
	delete angvelTemp;
}

void CollisionShape::GetAabb(Matrix t, Vector3% aabbMin, Vector3% aabbMax)
{
	btTransform* tTemp = Math::MatrixToBtTransform(t);
	btVector3* aabbMinTemp = new btVector3;
	btVector3* aabbMaxTemp = new btVector3;
	
	_collisionShape->getAabb(*tTemp, *aabbMinTemp, *aabbMaxTemp);

	aabbMin = Math::BtVector3ToVector3(aabbMinTemp);
	aabbMax = Math::BtVector3ToVector3(aabbMaxTemp);

	delete tTemp;
	delete aabbMinTemp;
	delete aabbMaxTemp;
}

void CollisionShape::GetBoundingSphere(Vector3% center, btScalar% radius)
{
	btVector3* centerTemp = new btVector3;
	btScalar radiusTemp;
	
	_collisionShape->getBoundingSphere(*centerTemp, radiusTemp);
	
	center = Math::BtVector3ToVector3(centerTemp);
	radius = radiusTemp;
}

btScalar CollisionShape::GetContactBreakingThreshold(btScalar defaultContactThreshold)
{
	return _collisionShape->getContactBreakingThreshold(defaultContactThreshold);
}

#ifndef DISABLE_SERIALIZE
int CollisionShape::CalculateSerializeBufferSize()
{
	return UnmanagedPointer->calculateSerializeBufferSize();
}

String^ CollisionShape::Serialize(IntPtr dataBuffer, BulletSharp::Serializer^ serializer)
{
	const char* name = UnmanagedPointer->serialize(dataBuffer.ToPointer(), serializer->UnmanagedPointer);
	return gcnew String(name);
}

void CollisionShape::SerializeSingleShape(BulletSharp::Serializer^ serializer)
{
	UnmanagedPointer->serializeSingleShape(serializer->UnmanagedPointer);
}
#endif

CollisionShape^ CollisionShape::GetManaged(btCollisionShape* collisionShape)
{
	if (collisionShape == 0)
		return nullptr;

	void* userObj = collisionShape->getUserPointer();
	if (userObj)
		return static_cast<CollisionShape^>(VoidPtrToGCHandle(userObj).Target);

	return CollisionShape::UpcastDetect(collisionShape);
}

CollisionShape^ CollisionShape::UpcastDetect(btCollisionShape* collisionShape)
{
	BroadphaseNativeType type = (BroadphaseNativeType)collisionShape->getShapeType();

	switch(type)
	{
	case BroadphaseNativeType::BoxShape:
		return gcnew BoxShape((btBoxShape*) collisionShape);
	case BroadphaseNativeType::CapsuleShape:
		return gcnew CapsuleShape((btCapsuleShape*) collisionShape);
	case BroadphaseNativeType::CompoundShape:
		return gcnew CompoundShape((btCompoundShape*) collisionShape);
	case BroadphaseNativeType::ConeShape:
		return gcnew ConeShape((btConeShape*) collisionShape);
	case BroadphaseNativeType::ConvexHullShape:
		return gcnew ConvexHullShape((btConvexHullShape*) collisionShape);
	case BroadphaseNativeType::ConvexShape:
		return gcnew ConvexShape((btConvexShape*) collisionShape);
	case BroadphaseNativeType::ConvexTriangleMeshShape:
		return gcnew ConvexTriangleMeshShape((btConvexTriangleMeshShape*) collisionShape);
	//case BroadphaseNativeType::CustomConcaveShape:
	//	return gcnew CustomConcaveShape((btCustomConcaveShape*) collisionShape);
	//case BroadphaseNativeType::CustomPolyhedralShape:
	//	return gcnew CustomPolyhedralShape((btCustomPolyhedralShape*) collisionShape);
	case BroadphaseNativeType::CylinderShape:
		return gcnew CylinderShape((btCylinderShape*) collisionShape);
	case BroadphaseNativeType::EmptyShape:
		return gcnew EmptyShape((btEmptyShape*) collisionShape);
	//case BroadphaseNativeType::FastConcaveMesh:
	//	return gcnew FastConcaveMesh((btFastConcaveMesh*) collisionShape);
	//case BroadphaseNativeType::HfFluidBuoyantConvexShape:
	//	return gcnew HfFluidBuoyantConvexShape((btHfFluidBuoyantConvexShape*) collisionShape);
	//case BroadphaseNativeType::HfFluidShape:
	//	return gcnew HfFluidShape((btHfFluidShape*) collisionShape);
	//case BroadphaseNativeType::MinkowskiDifferenceShape:
	//	return gcnew MinkowskiDifferenceShape((btMinkowskiDifferenceShape*) collisionShape);
	case BroadphaseNativeType::MultiSphereShape:
		return gcnew MultiSphereShape((btMultiSphereShape*) collisionShape);
	//case BroadphaseNativeType::ScaledTriangleMeshShape:
	//	return gcnew ScaledTriangleMeshShape((btScaledTriangleMeshShape*) collisionShape);
	case BroadphaseNativeType::SphereShape:
		return gcnew SphereShape((btSphereShape*) collisionShape);
	case BroadphaseNativeType::StaticPlane:
		return gcnew StaticPlaneShape((btStaticPlaneShape*) collisionShape);
	//case BroadphaseNativeType::TetrahedralShape:
	//	return gcnew TetrahedralShape((btTetrahedralShape*) collisionShape);
	case BroadphaseNativeType::UniformScalingShape:
		return gcnew UniformScalingShape((btUniformScalingShape*) collisionShape);

	case BroadphaseNativeType::GImpactShape:
	{
#ifndef DISABLE_GIMPACT
		return gcnew GImpactMeshShape((btGImpactMeshShape*) collisionShape);
#else
		return this;
#endif
	}

#ifndef DISABLE_UNCOMMON
	case BroadphaseNativeType::Box2dShape:
		return gcnew Box2dShape((btBox2dShape*) collisionShape);
	case BroadphaseNativeType::Convex2DShape:
		return gcnew Convex2DShape((btConvex2dShape*) collisionShape);
	case BroadphaseNativeType::ConvexPointCloudShape:
		return gcnew ConvexPointCloudShape((btConvexPointCloudShape*) collisionShape);
	case BroadphaseNativeType::MinkowskiSumShape:
		return gcnew MinkowskiSumShape((btMinkowskiSumShape*) collisionShape);
	case BroadphaseNativeType::MultiMaterialTriangleMesh:
		return gcnew MultimaterialTriangleMeshShape((btMultimaterialTriangleMeshShape*) collisionShape);
	case BroadphaseNativeType::TerrainShape:
		return gcnew HeightfieldTerrainShape((btHeightfieldTerrainShape*) collisionShape);
	case BroadphaseNativeType::TriangleMeshShape:
		return gcnew TriangleMeshShape((btTriangleMeshShape*) collisionShape);
	case BroadphaseNativeType::TriangleShape:
		return gcnew TriangleShape((btTriangleShape*) collisionShape);
#endif

	default:
		return gcnew CollisionShape(collisionShape, true);
	}
}

btScalar CollisionShape::AngularMotionDisc::get()
{
	return _collisionShape->getAngularMotionDisc();
}

bool CollisionShape::IsCompound::get()
{
	return _collisionShape->isCompound();
}

bool CollisionShape::IsConcave::get()
{
	return _collisionShape->isConcave();
}

bool CollisionShape::IsConvex::get()
{
	return _collisionShape->isConvex();
}

bool CollisionShape::IsConvex2d::get()
{
	return _collisionShape->isConvex2d();
}

bool CollisionShape::IsInfinite::get()
{
	return _collisionShape->isInfinite();
}

bool CollisionShape::IsPolyhedral::get()
{
	return _collisionShape->isPolyhedral();
}

bool CollisionShape::IsSoftBody::get()
{
	return _collisionShape->isSoftBody();
}

Vector3 CollisionShape::LocalScaling::get()
{
	return Math::BtVector3ToVector3(&_collisionShape->getLocalScaling());
}
void CollisionShape::LocalScaling::set(Vector3 value)
{
	btVector3* valueTemp = Math::Vector3ToBtVector3(value);
	_collisionShape->setLocalScaling(*valueTemp);
	delete valueTemp;
}

btScalar CollisionShape::Margin::get()
{
	return _collisionShape->getMargin();
}
void CollisionShape::Margin::set(btScalar margin)
{
	_collisionShape->setMargin(margin);
}

String^ CollisionShape::Name::get()
{
	return StringConv::UnmanagedToManaged(_collisionShape->getName());
}

BroadphaseNativeType CollisionShape::ShapeType::get()
{
	return (BroadphaseNativeType)_collisionShape->getShapeType();
}

Object^ CollisionShape::UserObject::get()
{
	return _userObject;
}

void CollisionShape::UserObject::set(Object^ value)
{
	_userObject = value;
}

btCollisionShape* CollisionShape::UnmanagedPointer::get()
{
	return _collisionShape;
}
void CollisionShape::UnmanagedPointer::set(btCollisionShape* value)
{
	_collisionShape = value;

	if (_collisionShape->getUserPointer() == 0)
	{
		GCHandle handle = GCHandle::Alloc(this);
		void* obj = GCHandleToVoidPtr(handle);
		_collisionShape->setUserPointer(obj);
	}
}
