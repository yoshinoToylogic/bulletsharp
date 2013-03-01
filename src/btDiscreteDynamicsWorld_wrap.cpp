#include "btDiscreteDynamicsWorld_wrap.h"

btDiscreteDynamicsWorld* btDiscreteDynamicsWorld_new(btDispatcher* dispatcher, btBroadphaseInterface* pairCache,
	btConstraintSolver* constraintSolver, btCollisionConfiguration* collisionConfiguration)
{
	return new btDiscreteDynamicsWorld(dispatcher, pairCache, constraintSolver, collisionConfiguration);
}
