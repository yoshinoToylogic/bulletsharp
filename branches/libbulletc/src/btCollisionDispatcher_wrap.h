#include "main.h"

extern "C"
{
	EXPORT btCollisionDispatcher* btCollisionDispatcher_new(btCollisionConfiguration* config);
	EXPORT void btCollisionDispatcher_delete(btCollisionDispatcher* obj);
	EXPORT int btCollisionDispatcher_getDispatcherFlags(btCollisionDispatcher* obj);
	EXPORT void btCollisionDispatcher_setDispatcherFlags (btCollisionDispatcher* obj, int flags);
}
