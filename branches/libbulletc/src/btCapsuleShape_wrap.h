#include "main.h"

extern "C"
{
    EXPORT btCapsuleShape* btCapsuleShape_new(float radius, float height);
	EXPORT btCapsuleShapeX* btCapsuleShapeX_new(float radius, float height);
	EXPORT btCapsuleShapeZ* btCapsuleShapeZ_new(float radius, float height);
    EXPORT float btCapsuleShape_getRadius(btCapsuleShape* obj);
    EXPORT float btCapsuleShape_getHalfHeight(btCapsuleShape* obj);
    EXPORT int btCapsuleShape_getUpAxis(btCapsuleShape* obj);
}
