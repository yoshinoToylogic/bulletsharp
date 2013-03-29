#include "main.h"

extern "C"
{
	EXPORT void btDiscreteCollisionDetectorInterface_Result_addContactPoint(btDiscreteCollisionDetectorInterface::Result* obj, btScalar* normalOnBInWorld, btScalar* pointInWorld, btScalar depth);
	EXPORT void btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersA(btDiscreteCollisionDetectorInterface::Result* obj, int partId0, int index0);
	EXPORT void btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersB(btDiscreteCollisionDetectorInterface::Result* obj, int partId1, int index1);
	EXPORT void btDiscreteCollisionDetectorInterface_Result_delete(btDiscreteCollisionDetectorInterface::Result* obj);

	EXPORT btDiscreteCollisionDetectorInterface::ClosestPointInput* btDiscreteCollisionDetectorInterface_ClosestPointInput_new();
	EXPORT btScalar btDiscreteCollisionDetectorInterface_ClosestPointInput_getMaximumDistanceSquared(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj);
	EXPORT btStackAlloc* btDiscreteCollisionDetectorInterface_ClosestPointInput_getStackAlloc(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj);
	EXPORT void btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformA(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj);
	EXPORT void btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformB(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj);
	EXPORT void btDiscreteCollisionDetectorInterface_ClosestPointInput_setMaximumDistanceSquared(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj, btScalar value);
	EXPORT void btDiscreteCollisionDetectorInterface_ClosestPointInput_setStackAlloc(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj, btStackAlloc* value);
	EXPORT void btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformA(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj, btScalar* value);
	EXPORT void btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformB(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj, btScalar* value);
	EXPORT void btDiscreteCollisionDetectorInterface_ClosestPointInput_delete(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj);
	EXPORT void btDiscreteCollisionDetectorInterface_getClosestPoints(btDiscreteCollisionDetectorInterface* obj, btDiscreteCollisionDetectorInterface::ClosestPointInput* input, btDiscreteCollisionDetectorInterface::Result* output, btIDebugDraw* debugDraw, bool swapResults);
	EXPORT void btDiscreteCollisionDetectorInterface_getClosestPoints2(btDiscreteCollisionDetectorInterface* obj, btDiscreteCollisionDetectorInterface::ClosestPointInput* input, btDiscreteCollisionDetectorInterface::Result* output, btIDebugDraw* debugDraw);
	EXPORT void btDiscreteCollisionDetectorInterface_delete(btDiscreteCollisionDetectorInterface* obj);

	EXPORT void btStorageResult_getClosestPointInB(btStorageResult* obj);
	EXPORT btScalar btStorageResult_getDistance(btStorageResult* obj);
	EXPORT void btStorageResult_getNormalOnSurfaceB(btStorageResult* obj);
	EXPORT void btStorageResult_setClosestPointInB(btStorageResult* obj, btScalar* value);
	EXPORT void btStorageResult_setDistance(btStorageResult* obj, btScalar value);
	EXPORT void btStorageResult_setNormalOnSurfaceB(btStorageResult* obj, btScalar* value);
}
