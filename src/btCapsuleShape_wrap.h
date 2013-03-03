#include "main.h"

extern "C"
{
	EXPORT btCapsuleShape* btCapsuleShape_new(btScalar radius, btScalar height);
	EXPORT btScalar btCapsuleShape_getHalfHeight(btCapsuleShape* obj);
	EXPORT btScalar btCapsuleShape_getRadius(btCapsuleShape* obj);
	EXPORT int btCapsuleShape_getUpAxis(btCapsuleShape* obj);

	EXPORT btCapsuleShapeX* btCapsuleShapeX_new(btScalar radius, btScalar height);

	EXPORT btCapsuleShapeZ* btCapsuleShapeZ_new(btScalar radius, btScalar height);

	EXPORT btCapsuleShapeData* btCapsuleShapeData_new();
	EXPORT void btCapsuleShapeData_delete(btCapsuleShapeData* obj);
}
