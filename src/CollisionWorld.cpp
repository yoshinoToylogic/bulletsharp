#include "StdAfx.h"

#include "CollisionWorld.h"

CollisionWorld::RayResultCallback::RayResultCallback(btCollisionWorld::RayResultCallback* callback)
{
	_callback = callback;
}

CollisionWorld::RayResultCallback::~RayResultCallback()
{
	this->!RayResultCallback();
}

CollisionWorld::RayResultCallback::!RayResultCallback()
{
	if( this->IsDisposed == true )
		return;
	
	OnDisposing( this, nullptr );
	
	_callback = NULL;
	
	OnDisposed( this, nullptr );
}

BulletSharp::CollisionObject^ CollisionWorld::RayResultCallback::CollisionObject::get()
{
	return gcnew BulletSharp::CollisionObject(_callback->m_collisionObject);
}

bool CollisionWorld::RayResultCallback::HasHit::get()
{
	return _callback->hasHit();
}

bool CollisionWorld::RayResultCallback::IsDisposed::get()
{
	return (_callback == NULL);
}

btCollisionWorld::RayResultCallback* CollisionWorld::RayResultCallback::UnmanagedPointer::get()
{
	return _callback;
}
void CollisionWorld::RayResultCallback::UnmanagedPointer::set(btCollisionWorld::RayResultCallback* value)
{
	_callback = value;
}


CollisionWorld::ClosestRayResultCallback::ClosestRayResultCallback(Vector3 rayFromWorld, Vector3 rayToWorld)
: RayResultCallback(new btCollisionWorld::ClosestRayResultCallback(
	*Math::Vector3ToBtVec3(rayFromWorld), *Math::Vector3ToBtVec3(rayToWorld)))
{
}

Vector3 CollisionWorld::ClosestRayResultCallback::HitPointWorld::get()
{
	return Math::BtVec3ToVector3(&UnmanagedPointer->m_hitPointWorld);
}
void CollisionWorld::ClosestRayResultCallback::HitPointWorld::set(Vector3 value)
{
	Math::Vector3ToBtVec3(value, &UnmanagedPointer->m_hitPointWorld);
}

btCollisionWorld::ClosestRayResultCallback* CollisionWorld::ClosestRayResultCallback::UnmanagedPointer::get()
{
	return (btCollisionWorld::ClosestRayResultCallback*)RayResultCallback::UnmanagedPointer;
}


CollisionWorld::CollisionWorld(btCollisionWorld* world)
{
	_world = world;
}

CollisionWorld::CollisionWorld(BulletSharp::IDispatcher^ dispatcher, BroadphaseInterface^ pairCache, CollisionConfiguration^ collisionConfiguration)
{
	_world = new btCollisionWorld(dispatcher->UnmanagedPointer, pairCache->UnmanagedPointer, collisionConfiguration->UnmanagedPointer);
	_dispatcher = dispatcher;
	_broadphase = pairCache;
}

CollisionWorld::~CollisionWorld()
{
	this->!CollisionWorld();
}

CollisionWorld::!CollisionWorld()
{
	if( this->IsDisposed == true )
		return;
	
	OnDisposing( this, nullptr );
	
	_world = NULL;
	
	OnDisposed( this, nullptr );
}

bool CollisionWorld::IsDisposed::get()
{
	return (_world == NULL);
}

void CollisionWorld::AddCollisionObject(CollisionObject^ collisionObject,
	CollisionFilterGroups collisionFilterGroup, CollisionFilterGroups collisionFilterMask)
{
	_world->addCollisionObject(collisionObject->UnmanagedPointer, (short int)collisionFilterGroup, (short int)collisionFilterMask);
}

void CollisionWorld::PerformDiscreteCollisionDetection()
{
	_world->performDiscreteCollisionDetection();
}

void CollisionWorld::RayTest(Vector3 rayFromWorld, Vector3 rayToWorld, RayResultCallback^ resultCallback)
{
	_world->rayTest(*Math::Vector3ToBtVec3(rayFromWorld), *Math::Vector3ToBtVec3(rayToWorld),
		*resultCallback->UnmanagedPointer);
}

void CollisionWorld::RemoveCollisionObject(CollisionObject^ collisionObject)
{
	_world->removeCollisionObject(collisionObject->UnmanagedPointer);
}

void CollisionWorld::UpdateAabbs()
{
	_world->updateAabbs();
}

void CollisionWorld::UpdateSingleAabb(CollisionObject^ colObj)
{
	_world->updateSingleAabb(colObj->UnmanagedPointer);
}

BroadphaseInterface^ CollisionWorld::Broadphase::get()
{
	return _broadphase;
}
void CollisionWorld::Broadphase::set(BroadphaseInterface^ value)
{
	_world->setBroadphase(value->UnmanagedPointer);
	_broadphase = value;
}

BulletSharp::CollisionObjectArray^ CollisionWorld::CollisionObjectArray::get()
{
	if (_collisionObjects == nullptr)
	{
		btCollisionObjectArray* objArray = new btCollisionObjectArray;
		*objArray = _world->getCollisionObjectArray();;
		_collisionObjects = gcnew BulletSharp::CollisionObjectArray(objArray);
	}
	return _collisionObjects;
}

DebugDraw^ CollisionWorld::DebugDrawer::get()
{
	return _debugDraw;
}
void CollisionWorld::DebugDrawer::set(DebugDraw^ value)
{
	_debugDraw = value;
	_world->setDebugDrawer(_debugDraw->_debugWrapper);
}

BulletSharp::IDispatcher^ CollisionWorld::IDispatcher::get()
{
	return _dispatcher;
}

int CollisionWorld::NumCollisionObjects::get()
{
	return _world->getNumCollisionObjects();
}

OverlappingPairCache^ CollisionWorld::PairCache::get()
{
	return _pairCache;
}

btCollisionWorld* CollisionWorld::UnmanagedPointer::get()
{
	return _world;
}
void CollisionWorld::UnmanagedPointer::set(btCollisionWorld* value)
{
	_world = value;
}
