#include "StdAfx.h"

#include "CollisionShape.h"
#include "MotionState.h"
#include "RigidBody.h"
#ifndef DISABLE_CONSTRAINTS
#include "TypedConstraint.h"
#endif

RigidBody::RigidBody(RigidBodyConstructionInfo^ info)
: CollisionObject(new btRigidBody(*info->UnmanagedPointer))
{
	_collisionShape = info->_collisionShape;
	_rootCollisionShape = info->_collisionShape;
	_motionState = info->_motionState;
}

RigidBody::RigidBody(btRigidBody* body)
: CollisionObject(body)
{
}

#ifndef DISABLE_CONSTRAINTS
void RigidBody::AddConstraintRef(TypedConstraint^ c)
{
	UnmanagedPointer->addConstraintRef(c->UnmanagedPointer);
}
#endif

void RigidBody::ApplyCentralForce(Vector3 force)
{
	UnmanagedPointer->applyCentralForce(*Math::Vector3ToBtVector3(force));
}

void RigidBody::ApplyCentralImpulse(Vector3 impulse)
{
	UnmanagedPointer->applyCentralImpulse(*Math::Vector3ToBtVector3(impulse));
}

void RigidBody::ApplyDamping(btScalar timeStep)
{
	UnmanagedPointer->applyDamping(timeStep);
}

void RigidBody::ApplyForce(Vector3 force, Vector3 rel_pos)
{
	UnmanagedPointer->applyForce(*Math::Vector3ToBtVector3(force), *Math::Vector3ToBtVector3(rel_pos));
}

void RigidBody::ApplyGravity()
{
	UnmanagedPointer->applyGravity();
}

void RigidBody::ApplyImpulse(Vector3 impulse, Vector3 rel_pos)
{
	UnmanagedPointer->applyImpulse(*Math::Vector3ToBtVector3(impulse), *Math::Vector3ToBtVector3(rel_pos));
}

void RigidBody::ApplyTorque(Vector3 torque)
{
	UnmanagedPointer->applyTorque(*Math::Vector3ToBtVector3(torque));
}

void RigidBody::ApplyTorqueImpulse(Vector3 torque)
{
	UnmanagedPointer->applyTorqueImpulse(*Math::Vector3ToBtVector3(torque));
}

void RigidBody::ClearForces()
{
	UnmanagedPointer->clearForces();
}

btScalar RigidBody::ComputeAngularImpulseDenominator(Vector3 axis)
{
	return UnmanagedPointer->computeAngularImpulseDenominator(*Math::Vector3ToBtVector3(axis));
}

btScalar RigidBody::ComputeImpulseDenominator(Vector3 pos, Vector3 normal)
{
	return UnmanagedPointer->computeImpulseDenominator(*Math::Vector3ToBtVector3(pos), *Math::Vector3ToBtVector3(normal));
}

void RigidBody::GetAabb([Out] Vector3% aabbMin, [Out] Vector3% aabbMax)
{
	btVector3* tmpAabbMin = new btVector3;
	btVector3* tmpAabbMax = new btVector3;
	
	UnmanagedPointer->getAabb(*tmpAabbMin, *tmpAabbMax);

	aabbMin = Math::BtVector3ToVector3(tmpAabbMin);
	aabbMax = Math::BtVector3ToVector3(tmpAabbMax);

	delete tmpAabbMin;
	delete tmpAabbMax;
}

void RigidBody::SetDamping(btScalar lin_damping, btScalar ang_damping)
{
	UnmanagedPointer->setDamping(lin_damping, ang_damping);
}

void RigidBody::SetMassProps(btScalar mass, Vector3 inertia)
{
	UnmanagedPointer->setMassProps(mass, *Math::Vector3ToBtVector3(inertia));
}

void RigidBody::SetSleepingThresholds(btScalar inertia, btScalar angular)
{
	UnmanagedPointer->setSleepingThresholds(inertia, angular);
}

void RigidBody::Translate(Vector3 v)
{
	UnmanagedPointer->translate(*Math::Vector3ToBtVector3(v));
}

void RigidBody::UpdateDeactivation(btScalar timeStep)
{
	UnmanagedPointer->updateDeactivation(timeStep);
}

void RigidBody::UpdateInertiaTensor()
{
	UnmanagedPointer->updateInertiaTensor();
}

bool RigidBody::WantsSleeping()
{
	return UnmanagedPointer->wantsSleeping();
}

RigidBody^ RigidBody::Upcast(CollisionObject^ colObj)
{
	btRigidBody* body = btRigidBody::upcast(colObj->UnmanagedPointer);
	if (body == nullptr)
		return nullptr;
	return gcnew RigidBody(body);
}


btScalar RigidBody::AngularDamping::get()
{
	return UnmanagedPointer->getAngularDamping();
}

Vector3 RigidBody::AngularFactor::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getAngularFactor());
}
void RigidBody::AngularFactor::set(Vector3 value)
{
	UnmanagedPointer->setAngularFactor(*Math::Vector3ToBtVector3(value));
}

btScalar RigidBody::AngularSleepingThreshold::get()
{
	return UnmanagedPointer->getAngularSleepingThreshold();
}

Vector3 RigidBody::AngularVelocity::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getAngularVelocity());
}
void RigidBody::AngularVelocity::set(Vector3 value)
{
	UnmanagedPointer->setAngularVelocity(*Math::Vector3ToBtVector3(value));
}

Vector3 RigidBody::CenterOfMassPosition::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getCenterOfMassPosition());
}

Matrix RigidBody::CenterOfMassTransform::get()
{
	return Math::BtTransformToMatrix(&UnmanagedPointer->getCenterOfMassTransform());
}
void RigidBody::CenterOfMassTransform::set(Matrix value)
{
	btTransform* valueTemp = Math::MatrixToBtTransform(value);
	UnmanagedPointer->setCenterOfMassTransform(*valueTemp);
	delete valueTemp;
}

Vector3 RigidBody::Gravity::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getGravity());
}
void RigidBody::Gravity::set(Vector3 value)
{
	btVector3* valueTemp = Math::Vector3ToBtVector3(value);
	UnmanagedPointer->setGravity(*valueTemp);
	delete valueTemp;
}

Vector3 RigidBody::InvInertiaDiagLocal::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getInvInertiaDiagLocal());
}
void RigidBody::InvInertiaDiagLocal::set(Vector3 value)
{
	btVector3* valueTemp = Math::Vector3ToBtVector3(value);
	UnmanagedPointer->setInvInertiaDiagLocal(*valueTemp);
	delete valueTemp;
}

Vector3 RigidBody::LinearFactor::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getLinearFactor());
}
void RigidBody::LinearFactor::set(Vector3 value)
{
	btVector3* valueTemp = Math::Vector3ToBtVector3(value);
	UnmanagedPointer->setLinearFactor(*valueTemp);
	delete valueTemp;
}

Vector3 RigidBody::LinearVelocity::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getLinearVelocity());
}
void RigidBody::LinearVelocity::set(Vector3 value)
{
	btVector3* valueTemp = Math::Vector3ToBtVector3(value);
	UnmanagedPointer->setLinearVelocity(*valueTemp);
	delete valueTemp;
}

BulletSharp::MotionState^ RigidBody::MotionState::get()
{
	if (_motionState == nullptr)
	{
		btMotionState* state = UnmanagedPointer->getMotionState();
		if (state != nullptr)
			_motionState = gcnew BulletSharp::MotionState(state);
	}
	return _motionState;
}
void RigidBody::MotionState::set(BulletSharp::MotionState^ value)
{
	UnmanagedPointer->setMotionState(value->UnmanagedPointer);
	_motionState = value;
}

btRigidBody* RigidBody::UnmanagedPointer::get()
{
	return (btRigidBody*)CollisionObject::UnmanagedPointer;
}


btRigidBody::btRigidBodyConstructionInfo* RigidBody_GetUnmanagedConstructionInfo(
	btScalar mass, btMotionState* motionState, btCollisionShape* collisionShape, btVector3* localInertia = new btVector3())
{
	btRigidBody::btRigidBodyConstructionInfo* ret =
		new btRigidBody::btRigidBodyConstructionInfo(mass, motionState, collisionShape, *localInertia);
	delete localInertia;
	return ret;
}

RigidBody::RigidBodyConstructionInfo::RigidBodyConstructionInfo(btScalar mass, BulletSharp::MotionState^ motionState, BulletSharp::CollisionShape^ collisionShape)
{
	btCollisionShape* btColShape = (collisionShape != nullptr) ?
		collisionShape->UnmanagedPointer : nullptr;
	btMotionState* btMotState = (motionState != nullptr) ?
		motionState->UnmanagedPointer : nullptr;

	_info = RigidBody_GetUnmanagedConstructionInfo(mass, btMotState, btColShape);
	_collisionShape = collisionShape;
	_motionState = motionState;
}

RigidBody::RigidBodyConstructionInfo::RigidBodyConstructionInfo(btScalar mass, BulletSharp::MotionState^ motionState, BulletSharp::CollisionShape^ collisionShape, Vector3 localInertia)
{
	btCollisionShape* btColShape = (collisionShape != nullptr) ?
		collisionShape->UnmanagedPointer : nullptr;
	btMotionState* btMotState = (motionState != nullptr) ?
		motionState->UnmanagedPointer : nullptr;

	_info = RigidBody_GetUnmanagedConstructionInfo(mass, btMotState, btColShape, Math::Vector3ToBtVector3(localInertia));
	_collisionShape = collisionShape;
	_motionState = motionState;
}

btScalar RigidBody::RigidBodyConstructionInfo::Mass::get()
{
	return _info->m_mass;
}
void RigidBody::RigidBodyConstructionInfo::Mass::set(btScalar mass)
{
	_info->m_mass = mass;
}

btRigidBody::btRigidBodyConstructionInfo* RigidBody::RigidBodyConstructionInfo::UnmanagedPointer::get()
{
	return _info;
}