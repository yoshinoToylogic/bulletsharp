#include "main.h"

extern "C"
{
    EXPORT btDynamicsWorld* btDynamicsWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache, btConstraintSolver* constraintSolver, btCollisionConfiguration* collisionConfiguration);
    EXPORT void btDynamicsWorld_addConstraint(btDynamicsWorld* obj, btTypedConstraint* constraint, bool disableLinkedCollisions);
	EXPORT void btDynamicsWorld_addRigidBody(btDynamicsWorld* obj, btRigidBody* body);
	EXPORT void btDynamicsWorld_getGravity(btDynamicsWorld* obj, btScalar* gravity);
	EXPORT void btDynamicsWorld_removeRigidBody(btDynamicsWorld* obj, btRigidBody* body);
    EXPORT void btDynamicsWorld_removeConstraint(btDynamicsWorld* obj, btTypedConstraint* constraint);
    EXPORT void btDynamicsWorld_setGravity(btDynamicsWorld* obj, btScalar* gravity);
    EXPORT int btDynamicsWorld_stepSimulation(btDynamicsWorld* obj, float timeStep, int maxSubSteps, float fixedTimeStep);
}
