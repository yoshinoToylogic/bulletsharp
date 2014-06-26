#include "main.h"

#ifndef _BT_ACTION_INTERFACE_H
#define pActionInterface_DebugDraw void*
#define pActionInterface_UpdateAction void*

#define btActionInterfaceWrapper void
#else
typedef void (*pActionInterface_DebugDraw)(btIDebugDraw* debugDrawer);
typedef void (*pActionInterface_UpdateAction)(btCollisionWorld* collisionWorld, btScalar deltaTimeStep);

class btActionInterfaceWrapper : public btActionInterface
{
private:
	pActionInterface_DebugDraw _debugDrawCallback;
	pActionInterface_UpdateAction _updateActionCallback;

public:
	void* _actionInterfaceGCHandle;

	btActionInterfaceWrapper(void* actionInterfaceGCHandle, pActionInterface_DebugDraw debugDrawCallback, pActionInterface_UpdateAction updateActionCallback);

	virtual void debugDraw(btIDebugDraw* debugDrawer);
	virtual void updateAction(btCollisionWorld* collisionWorld, btScalar deltaTimeStep);
};
#endif

extern "C"
{
	EXPORT btActionInterfaceWrapper* btActionInterfaceWrapper_new(void* actionInterfaceGCHandle, pActionInterface_DebugDraw debugDrawCallback, pActionInterface_UpdateAction updateActionCallback);
	EXPORT void* btActionInterfaceWrapper_getGCHandle(btActionInterfaceWrapper* obj);

	EXPORT void btActionInterface_debugDraw(btActionInterface* obj, btIDebugDraw* debugDrawer);
	EXPORT void btActionInterface_updateAction(btActionInterface* obj, btCollisionWorld* collisionWorld, btScalar deltaTimeStep);
	EXPORT void btActionInterface_delete(btActionInterface* obj);
}
