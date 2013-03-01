#include "btBoxShape_wrap.h"

btBoxShape* btBoxShape_new(btScalar* boxHalfExtents)
{
	VECTOR3_CONV(boxHalfExtents);
	return new btBoxShape(VECTOR3_USE(boxHalfExtents));
}

btBoxShape* btBoxShape_new2(btScalar boxHalfExtent)
{
	return new btBoxShape(btVector3(boxHalfExtent, boxHalfExtent, boxHalfExtent));
}

btBoxShape* btBoxShape_new3(btScalar boxHalfExtentX, btScalar boxHalfExtentY, btScalar boxHalfExtentZ)
{
	return new btBoxShape(btVector3(boxHalfExtentX, boxHalfExtentY, boxHalfExtentZ));
}

void btBoxShape_getHalfExtentsWithMargin(btBoxShape* obj, btScalar* extents)
{
	VECTOR3_OUT(&obj->getHalfExtentsWithMargin(), extents);
}

void btBoxShape_getHalfExtentsWithoutMargin(btBoxShape* obj, btScalar* extents)
{
	VECTOR3_OUT(&obj->getHalfExtentsWithoutMargin(), extents);
}
