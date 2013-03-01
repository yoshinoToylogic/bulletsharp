#include "StdAfx.h"

#include "DiscreteCollisionDetectorInterface.h"
#include "StackAlloc.h"
#ifndef DISABLE_DEBUGDRAW
#include "DebugDraw.h"
#endif

DiscreteCollisionDetectorInterface::ClosestPointInput::ClosestPointInput()
{
	_input = new btDiscreteCollisionDetectorInterface::ClosestPointInput();
}

btScalar DiscreteCollisionDetectorInterface::ClosestPointInput::MaximumDistanceSquared::get()
{
	return UnmanagedPointer->m_maximumDistanceSquared;
}
void DiscreteCollisionDetectorInterface::ClosestPointInput::MaximumDistanceSquared::set(btScalar value)
{
	UnmanagedPointer->m_maximumDistanceSquared = value;
}

StackAlloc^ DiscreteCollisionDetectorInterface::ClosestPointInput::StackAlloc::get()
{
	if (UnmanagedPointer->m_stackAlloc == nullptr)
		return nullptr;
	return gcnew BulletSharp::StackAlloc(UnmanagedPointer->m_stackAlloc);
}
void DiscreteCollisionDetectorInterface::ClosestPointInput::StackAlloc::set(BulletSharp::StackAlloc^ value)
{
	UnmanagedPointer->m_stackAlloc = value->UnmanagedPointer;
}

Matrix DiscreteCollisionDetectorInterface::ClosestPointInput::TransformA::get()
{
	return Math::BtTransformToMatrix(&UnmanagedPointer->m_transformA);
}
void DiscreteCollisionDetectorInterface::ClosestPointInput::TransformA::set(Matrix value)
{
	Math::MatrixToBtTransform(value, &UnmanagedPointer->m_transformA);
}

Matrix DiscreteCollisionDetectorInterface::ClosestPointInput::TransformB::get()
{
	return Math::BtTransformToMatrix(&UnmanagedPointer->m_transformB);
}
void DiscreteCollisionDetectorInterface::ClosestPointInput::TransformB::set(Matrix value)
{
	Math::MatrixToBtTransform(value, &UnmanagedPointer->m_transformB);
}

btDiscreteCollisionDetectorInterface::ClosestPointInput*
DiscreteCollisionDetectorInterface::ClosestPointInput::UnmanagedPointer::get()
{
	return _input;
}
void DiscreteCollisionDetectorInterface::ClosestPointInput::UnmanagedPointer::set(
	btDiscreteCollisionDetectorInterface::ClosestPointInput* value)
{
	_input = value;
}

DiscreteCollisionDetectorInterface::Result::Result(
	btDiscreteCollisionDetectorInterface::Result* result)
{
	_unmanaged = result;
}

DiscreteCollisionDetectorInterface::Result::~Result()
{
	this->DiscreteCollisionDetectorInterface::Result::!Result();
}

DiscreteCollisionDetectorInterface::Result::!Result()
{
	if (this->IsDisposed)
		return;

	OnDisposing(this, nullptr);

	delete _unmanaged;
	_unmanaged = NULL;

	OnDisposed(this, nullptr);
}

bool DiscreteCollisionDetectorInterface::Result::IsDisposed::get()
{
	return (_unmanaged == NULL);
}

void DiscreteCollisionDetectorInterface::Result::AddContactPoint(Vector3 normalOnBInWorld, Vector3 pointInWorld, btScalar depth)
{
	VECTOR3_DEF(normalOnBInWorld);
	VECTOR3_DEF(pointInWorld);
	
	_unmanaged->addContactPoint(VECTOR3_USE(normalOnBInWorld), VECTOR3_USE(pointInWorld), depth);
	
	VECTOR3_DEL(normalOnBInWorld);
	VECTOR3_DEL(pointInWorld);
}

void DiscreteCollisionDetectorInterface::Result::SetShapeIdentifiersA(int partId0, int index0)
{
	_unmanaged->setShapeIdentifiersA(partId0, index0);
}

void DiscreteCollisionDetectorInterface::Result::SetShapeIdentifiersB(int partId1, int index1)
{
	_unmanaged->setShapeIdentifiersB(partId1, index1);
}


DiscreteCollisionDetectorInterface::DiscreteCollisionDetectorInterface(
	btDiscreteCollisionDetectorInterface* detectorInterface)
{
	_detectorInterface = detectorInterface;
}

DiscreteCollisionDetectorInterface::~DiscreteCollisionDetectorInterface()
{
	this->!DiscreteCollisionDetectorInterface();
}

DiscreteCollisionDetectorInterface::!DiscreteCollisionDetectorInterface()
{
	if (this->IsDisposed)
		return;

	OnDisposing(this, nullptr);

	_detectorInterface = NULL;

	OnDisposed(this, nullptr);
}

#ifndef DISABLE_DEBUGDRAW
void DiscreteCollisionDetectorInterface::GetClosestPoints(
	ClosestPointInput^ input, Result^ output, IDebugDraw^ debugDraw)
{
	UnmanagedPointer->getClosestPoints(*input->UnmanagedPointer, *output->_unmanaged,
		DebugDraw::GetUnmanaged(debugDraw));
}

void DiscreteCollisionDetectorInterface::GetClosestPoints(
	ClosestPointInput^ input, Result^ output, IDebugDraw^ debugDraw, bool swapResults)
{
	UnmanagedPointer->getClosestPoints(*input->UnmanagedPointer, *output->_unmanaged,
		DebugDraw::GetUnmanaged(debugDraw), swapResults);
}
#else
void DiscreteCollisionDetectorInterface::GetClosestPoints(
	ClosestPointInput^ input, Result^ output)
{
	UnmanagedPointer->getClosestPoints(*input->UnmanagedPointer, *output->UnmanagedPointer, 0);
}

void DiscreteCollisionDetectorInterface::GetClosestPoints(
	ClosestPointInput^ input, Result^ output, bool swapResults)
{
	UnmanagedPointer->getClosestPoints(*input->UnmanagedPointer, *output->UnmanagedPointer, 0, swapResults);
}
#endif

bool DiscreteCollisionDetectorInterface::IsDisposed::get()
{
	return (_detectorInterface == NULL);
}

btDiscreteCollisionDetectorInterface* DiscreteCollisionDetectorInterface::UnmanagedPointer::get()
{
	return _detectorInterface;
}
void DiscreteCollisionDetectorInterface::UnmanagedPointer::set(btDiscreteCollisionDetectorInterface* value)
{
	_detectorInterface = value;
}


#define Unmanaged static_cast<btStorageResultWrapper*>(_unmanaged)

StorageResult::StorageResult(btStorageResultWrapper* result)
: DiscreteCollisionDetectorInterface::Result(result)
{
}

StorageResult::StorageResult()
: DiscreteCollisionDetectorInterface::Result(0)
{
//	_unmanaged = new btStorageResultWrapper(this);
}

void StorageResult::SetShapeIdentifiersA(int partId0, int index0)
{
	Unmanaged->setShapeIdentifiersA(partId0, index0);
}

void StorageResult::SetShapeIdentifiersB(int partId1, int index1)
{
	Unmanaged->setShapeIdentifiersB(partId1, index1);
}

Vector3 StorageResult::ClosestPointInB::get()
{
	return Math::BtVector3ToVector3(&Unmanaged->m_closestPointInB);
}
void StorageResult::ClosestPointInB::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &Unmanaged->m_closestPointInB);
}

btScalar StorageResult::Distance::get()
{
	return Unmanaged->m_distance;
}
void StorageResult::Distance::set(btScalar value)
{
	Unmanaged->m_distance = value;
}

Vector3 StorageResult::NormalOnSurfaceB::get()
{
	return Math::BtVector3ToVector3(&Unmanaged->m_normalOnSurfaceB);
}
void StorageResult::NormalOnSurfaceB::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &Unmanaged->m_normalOnSurfaceB);
}


//btStorageResultWrapper::btStorageResultWrapper(StorageResult^ storageResult)
//{
//	_storageResult = storageResult;
//}

void btStorageResultWrapper::setShapeIdentifiersA(int partId0, int index0)
{
	_storageResult->SetShapeIdentifiersA(partId0, index0);
}

void btStorageResultWrapper::setShapeIdentifiersB(int partId1, int index1)
{
	_storageResult->SetShapeIdentifiersB(partId1, index1);
}