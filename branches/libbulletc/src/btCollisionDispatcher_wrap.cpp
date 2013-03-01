#include "btCollisionDispatcher_wrap.h"

btCollisionDispatcher* btCollisionDispatcher_new(btCollisionConfiguration* config)
{
	return new btCollisionDispatcher(config);
}

void btCollisionDispatcher_delete(btCollisionDispatcher* obj)
{
	delete obj;
}

EXPORT int btCollisionDispatcher_getDispatcherFlags(btCollisionDispatcher* obj)
{
	return obj->getDispatcherFlags();
}

EXPORT void btCollisionDispatcher_setDispatcherFlags(btCollisionDispatcher* obj, int flags)
{
	obj->setDispatcherFlags(flags);
}
