#include "btDbvtBroadphase_wrap.h"

btDbvtBroadphase* btDbvtBroadphase_new()
{
	return new btDbvtBroadphase();
}

void btBroadphaseInterface_delete(btBroadphaseInterface* obj)
{
	delete obj;
}
