#include "main.h"

extern "C"
{
    EXPORT btDbvtBroadphase* btDbvtBroadphase_new();
	EXPORT void btBroadphaseInterface_delete(btBroadphaseInterface* obj);
}
