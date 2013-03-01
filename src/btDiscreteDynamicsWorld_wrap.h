#include "main.h"

extern "C"
{
	EXPORT btDiscreteDynamicsWorld* btDiscreteDynamicsWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache, btConstraintSolver* constraintSolver, btCollisionConfiguration* collisionConfiguration);
}