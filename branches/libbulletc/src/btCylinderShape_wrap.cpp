#include "conversion.h"
#include "btCylinderShape_wrap.h"

btCylinderShape* btCylinderShape_new(btScalar* halfExtents)
{
	VECTOR3_CONV(halfExtents);
	return new btCylinderShape(VECTOR3_USE(halfExtents));
}

void btCylinderShape_getHalfExtentsWithMargin(btCylinderShape* obj, btScalar* value)
{
	VECTOR3_OUT2(obj->getHalfExtentsWithMargin(), value);
}

void btCylinderShape_getHalfExtentsWithoutMargin(btCylinderShape* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getHalfExtentsWithoutMargin(), value);
}

btScalar btCylinderShape_getRadius(btCylinderShape* obj)
{
	return obj->getRadius();
}

int btCylinderShape_getUpAxis(btCylinderShape* obj)
{
	return obj->getUpAxis();
}

btCylinderShapeX* btCylinderShapeX_new(btScalar* halfExtents)
{
	VECTOR3_CONV(halfExtents);
	return new btCylinderShapeX(VECTOR3_USE(halfExtents));
}

btCylinderShapeZ* btCylinderShapeZ_new(btScalar* halfExtents)
{
	VECTOR3_CONV(halfExtents);
	return new btCylinderShapeZ(VECTOR3_USE(halfExtents));
}