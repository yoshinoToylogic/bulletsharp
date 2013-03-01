#include "btDynamicsWorld_wrap.h"

btDynamicsWorld* btDynamicsWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache,
	btConstraintSolver* constraintSolver, btCollisionConfiguration* collisionConfiguration)
{
    return new btDiscreteDynamicsWorld(dispatcher, pairCache, constraintSolver, collisionConfiguration);
}

void btDynamicsWorld_addConstraint(btDynamicsWorld* obj, btTypedConstraint* constraint, bool disableLinkedCollisions)
{
    obj->addConstraint(constraint, disableLinkedCollisions);
}

void btDynamicsWorld_addRigidBody(btDynamicsWorld* obj, btRigidBody* body)
{
    obj->addRigidBody(body);
}

void btDynamicsWorld_getGravity(btDynamicsWorld* obj, btScalar* gravity)
{
	VECTOR3_OUT(&obj->getGravity(), gravity);
}

void btDynamicsWorld_removeConstraint(btDynamicsWorld* obj, btTypedConstraint* constraint)
{
    obj->removeConstraint(constraint);
}

void btDynamicsWorld_removeRigidBody(btDynamicsWorld* obj, btRigidBody* body)
{
    obj->removeRigidBody(body);
}

void btDynamicsWorld_setGravity(btDynamicsWorld* obj, btScalar* gravity)
{
	VECTOR3_CONV(gravity);
    obj->setGravity(VECTOR3_USE(gravity));
}

int btDynamicsWorld_stepSimulation(btDynamicsWorld* obj, float timeStep, int maxSubSteps, float fixedTimeStep)
{
    return obj->stepSimulation(timeStep, maxSubSteps, fixedTimeStep);
}
