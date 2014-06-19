#include "conversion.h"
#include "btActionInterface_wrap.h"

btActionInterfaceWrapper::btActionInterfaceWrapper(void* actionInterfaceGCHandle, pDebugDraw debugDrawCallback, pUpdateAction updateActionCallback)
{
	_actionInterfaceGCHandle = actionInterfaceGCHandle;
	_debugDrawCallback = debugDrawCallback;
	_updateActionCallback = updateActionCallback;
}

void btActionInterfaceWrapper::debugDraw(btIDebugDraw* debugDrawer)
{
	_debugDrawCallback(debugDrawer);
}

void btActionInterfaceWrapper::updateAction(btCollisionWorld* collisionWorld, btScalar deltaTimeStep)
{
	_updateActionCallback(collisionWorld, deltaTimeStep);
}

void btActionInterface_debugDraw(btActionInterface* obj, btIDebugDraw* debugDrawer)
{
	obj->debugDraw(debugDrawer);
}

void btActionInterface_updateAction(btActionInterface* obj, btCollisionWorld* collisionWorld, btScalar deltaTimeStep)
{
	obj->updateAction(collisionWorld, deltaTimeStep);
}

void btActionInterface_delete(btActionInterface* obj)
{
	delete obj;
}


btActionInterfaceWrapper* btActionInterfaceWrapper_new(void* actionInterfaceGCHandle, pDebugDraw debugDrawCallback, pUpdateAction updateActionCallback)
{
	return new btActionInterfaceWrapper(actionInterfaceGCHandle, debugDrawCallback, updateActionCallback);
}

void* btActionInterfaceWrapper_getGCHandle(btActionInterfaceWrapper* obj)
{
	return obj->_actionInterfaceGCHandle;
}
