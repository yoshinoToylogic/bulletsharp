#include "main.h"

#ifndef _BT_ACTION_INTERFACE_H
#define pDebugDraw void*
#define pUpdateAction void*

#define btActionInterfaceWrapper void
#else
typedef void (*pDebugDraw)(btIDebugDraw* debugDrawer);
typedef void (*pUpdateAction)(btCollisionWorld* collisionWorld, btScalar deltaTimeStep);

class btActionInterfaceWrapper : public btActionInterface
{
private:
	pDebugDraw _debugDrawCallback;
	pUpdateAction _updateActionCallback;

public:
	void* _actionInterfaceGCHandle;

	btActionInterfaceWrapper(void* actionInterfaceGCHandle, pDebugDraw debugDrawCallback, pUpdateAction updateActionCallback);

	virtual void debugDraw(btIDebugDraw* debugDrawer);
	virtual void updateAction(btCollisionWorld* collisionWorld, btScalar deltaTimeStep);
};
#endif

extern "C"
{
	EXPORT btActionInterfaceWrapper* btActionInterfaceWrapper_new(void* actionInterfaceGCHandle, pDebugDraw debugDrawCallback, pUpdateAction updateActionCallback);
	EXPORT void btActionInterfaceWrapper_debugDraw(btActionInterface* obj, btIDebugDraw* debugDrawer);
	EXPORT void btActionInterfaceWrapper_updateAction(btActionInterface* obj, btCollisionWorld* collisionWorld, btScalar deltaTimeStep);
	EXPORT void* btActionInterfaceWrapper_getGCHandle(btActionInterfaceWrapper* obj);
	EXPORT void btActionInterface_delete(btActionInterface* obj);
}
