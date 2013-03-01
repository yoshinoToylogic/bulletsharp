#include "btCapsuleShape_wrap.h"

btCapsuleShape* btCapsuleShape_new(float radius, float height)
{
	return new btCapsuleShape(radius, height);
}

btCapsuleShapeX* btCapsuleShapeX_new(float radius, float height)
{
	return new btCapsuleShapeX(radius, height);
}

btCapsuleShapeZ* btCapsuleShapeZ_new(float radius, float height)
{
	return new btCapsuleShapeZ(radius, height);
}

float btCapsuleShape_getRadius(btCapsuleShape* obj)
{
	return obj->getRadius();
}

float btCapsuleShape_getHalfHeight(btCapsuleShape* obj)
{
	return obj->getHalfHeight();
}

int btCapsuleShape_getUpAxis(btCapsuleShape* obj)
{
	return obj->getUpAxis();
}
