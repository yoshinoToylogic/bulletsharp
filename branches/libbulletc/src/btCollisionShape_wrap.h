#include "main.h"

extern "C"
{
	EXPORT void btCollisionShape_delete(btCollisionShape* obj);
	EXPORT void btCollisionShape_calculateLocalInertia(btCollisionShape* obj, float mass, btVector3* inertia);
	EXPORT float btCollisionShape_getAngularMotionDisk(btCollisionShape* obj);
	EXPORT void btCollisionShape_getLocalScaling(btCollisionShape* obj, btScalar* scaling);
	EXPORT float btCollisionShape_getContactBreakingThreshold(btCollisionShape* obj, btScalar defaultContactThresholdFactor);
	EXPORT float btCollisionShape_getMargin(btCollisionShape* obj);
	EXPORT int btCollisionShape_getShapeType(btCollisionShape* obj);
	EXPORT void* btCollisionShape_getUserPointer(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isCompound(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isConcave(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isConvex(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isInfinite(btCollisionShape* obj);
	EXPORT bool btCollisionShape_isPolyhedral(btCollisionShape* obj);
	EXPORT void btCollisionShape_setLocalScaling(btCollisionShape* obj, btScalar* scaling);
	EXPORT void btCollisionShape_setMargin(btCollisionShape* obj, float margin);
	EXPORT void btCollisionShape_setUserPointer(btCollisionShape* obj, void* ptr);
}
