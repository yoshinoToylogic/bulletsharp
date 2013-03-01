#include "btCollisionShape_wrap.h"

void btCollisionShape_delete(btCollisionShape* obj)
{
	delete obj;
}

void btCollisionShape_calculateLocalInertia(btCollisionShape* obj, float mass, btVector3* inertia)
{
	obj->calculateLocalInertia(mass, *inertia);
}

float btCollisionShape_getAngularMotionDisk(btCollisionShape* obj)
{
	return obj->getAngularMotionDisc();
}

void btCollisionShape_getLocalScaling(btCollisionShape* obj, btScalar* scaling)
{
	VECTOR3_OUT(&obj->getLocalScaling(), scaling);
}

float btCollisionShape_getContactBreakingThreshold(btCollisionShape* obj, btScalar defaultContactThresholdFactor)
{
	return obj->getContactBreakingThreshold(defaultContactThresholdFactor);
}

float btCollisionShape_getMargin(btCollisionShape* obj)
{
	return obj->getMargin();
}

int btCollisionShape_getShapeType(btCollisionShape* obj)
{
	return obj->getShapeType();
}

void* btCollisionShape_getUserPointer(btCollisionShape* obj)
{
	return obj->getUserPointer();
}

bool btCollisionShape_isCompound(btCollisionShape* obj)
{
	return obj->isCompound();
}

bool btCollisionShape_isConcave(btCollisionShape* obj)
{
	return obj->isConcave();
}

bool btCollisionShape_isConvex(btCollisionShape* obj)
{
	return obj->isConvex();
}

bool btCollisionShape_isInfinite(btCollisionShape* obj)
{
	return obj->isInfinite();
}

bool btCollisionShape_isPolyhedral(btCollisionShape* obj)
{
	return obj->isPolyhedral();
}

void btCollisionShape_setLocalScaling(btCollisionShape* obj, btScalar* scaling)
{
	VECTOR3_CONV(scaling);
	obj->setLocalScaling(VECTOR3_USE(scaling));
}

void btCollisionShape_setMargin(btCollisionShape* obj, float margin)
{
	obj->setMargin(margin);
}

void btCollisionShape_setUserPointer(btCollisionShape* obj, void* ptr)
{
	obj->setUserPointer(ptr);
}
