#include "main.h"

#include <BulletCollision/CollisionShapes/btConvex2dShape.h>

extern "C"
{
	EXPORT btConvex2dShape* btConvex2dShape_new(btConvexShape* convexChildShape);
	EXPORT btConvexShape* btConvex2dShape_getChildShape(btConvex2dShape* obj);
}
