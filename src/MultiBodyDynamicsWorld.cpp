#include "StdAfx.h"

#ifndef DISABLE_FEATHERSTONE

#include "BroadphaseInterface.h"
#include "CollisionConfiguration.h"
#include "Dispatcher.h"
#include "MultiBody.h"
#include "MultiBodyConstraintSolver.h"
#include "MultiBodyDynamicsWorld.h"
#ifndef DISABLE_CONSTRAINTS
#include "MultiBodyConstraint.h"
#endif

#define Native static_cast<btMultiBodyDynamicsWorld*>(_native)

MultiBodyDynamicsWorld::MultiBodyDynamicsWorld(btMultiBodyDynamicsWorld* native)
	: DiscreteDynamicsWorld(native)
{
}

MultiBodyDynamicsWorld::MultiBodyDynamicsWorld(BulletSharp::Dispatcher^ dispatcher, BroadphaseInterface^ pairCache,
	MultiBodyConstraintSolver^ constraintSolver, CollisionConfiguration^ collisionConfiguration)
	: DiscreteDynamicsWorld(new btMultiBodyDynamicsWorld(dispatcher->_native, pairCache->_native,
		(btMultiBodyConstraintSolver*)constraintSolver->_native, collisionConfiguration->_native))
{
	_constraintSolver = constraintSolver;
	_dispatcher = dispatcher;
	_broadphase = pairCache;

	_bodies = gcnew System::Collections::Generic::List<MultiBody^>();
#ifndef DISABLE_CONSTRAINTS
	_constraints = gcnew System::Collections::Generic::List<MultiBodyConstraint^>();
#endif
}

void MultiBodyDynamicsWorld::AddMultiBody(MultiBody^ body, CollisionFilterGroups group, CollisionFilterGroups mask)
{
	Native->addMultiBody(body->_native, (short)group, (short)mask);
	_bodies->Add(body);
}

void MultiBodyDynamicsWorld::AddMultiBody(MultiBody^ body, short group, short mask)
{
	Native->addMultiBody(body->_native, group, mask);
	_bodies->Add(body);
}

void MultiBodyDynamicsWorld::AddMultiBody(MultiBody^ body)
{
	Native->addMultiBody(body->_native);
	_bodies->Add(body);
}
#ifndef DISABLE_CONSTRAINTS
void MultiBodyDynamicsWorld::AddMultiBodyConstraint(MultiBodyConstraint^ constraint)
{
	Native->addMultiBodyConstraint(constraint->_native);
	_constraints->Add(constraint);
}
#endif
void MultiBodyDynamicsWorld::RemoveMultiBody(MultiBody^ body)
{
	Native->removeMultiBody(body->_native);
	_bodies->Remove(body);
}
#ifndef DISABLE_CONSTRAINTS
void MultiBodyDynamicsWorld::RemoveMultiBodyConstraint(MultiBodyConstraint^ constraint)
{
	Native->removeMultiBodyConstraint(constraint->_native);
	_constraints->Remove(constraint);
}
#endif

#endif
