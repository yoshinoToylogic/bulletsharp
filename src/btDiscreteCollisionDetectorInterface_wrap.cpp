#include "btDiscreteCollisionDetectorInterface_wrap.h"

void btDiscreteCollisionDetectorInterface_Result_addContactPoint(btDiscreteCollisionDetectorInterface::Result* obj, btScalar* normalOnBInWorld, btScalar* pointInWorld, btScalar depth)
{
	VECTOR3_CONV(normalOnBInWorld);
	VECTOR3_CONV(pointInWorld);
	obj->addContactPoint(VECTOR3_USE(normalOnBInWorld), VECTOR3_USE(pointInWorld), depth);
}

void btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersA(btDiscreteCollisionDetectorInterface::Result* obj, int partId0, int index0)
{
	obj->setShapeIdentifiersA(partId0, index0);
}

void btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersB(btDiscreteCollisionDetectorInterface::Result* obj, int partId1, int index1)
{
	obj->setShapeIdentifiersB(partId1, index1);
}

void btDiscreteCollisionDetectorInterface_Result_delete(btDiscreteCollisionDetectorInterface::Result* obj)
{
	delete obj;
}

btDiscreteCollisionDetectorInterface::ClosestPointInput* btDiscreteCollisionDetectorInterface_ClosestPointInput_new()
{
	return new btDiscreteCollisionDetectorInterface::ClosestPointInput();
}

btScalar btDiscreteCollisionDetectorInterface_ClosestPointInput_getMaximumDistanceSquared(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj)
{
	return obj->m_maximumDistanceSquared;
}

btStackAlloc* btDiscreteCollisionDetectorInterface_ClosestPointInput_getStackAlloc(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj)
{
	return obj->m_stackAlloc;
}

void btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformA(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj)
{
	obj->m_transformA;
}

void btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformB(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj)
{
	obj->m_transformB;
}

void btDiscreteCollisionDetectorInterface_ClosestPointInput_setMaximumDistanceSquared(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj, btScalar value)
{
	obj->m_maximumDistanceSquared = value;
}

void btDiscreteCollisionDetectorInterface_ClosestPointInput_setStackAlloc(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj, btStackAlloc* value)
{
	obj->m_stackAlloc = value;
}

void btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformA(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj, btScalar* value)
{
	MatrixTobtTransform(value, &obj->m_transformA);
}

void btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformB(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj, btScalar* value)
{
	MatrixTobtTransform(value, &obj->m_transformB);
}

void btDiscreteCollisionDetectorInterface_ClosestPointInput_delete(btDiscreteCollisionDetectorInterface::ClosestPointInput* obj)
{
	delete obj;
}

void btDiscreteCollisionDetectorInterface_getClosestPoints(btDiscreteCollisionDetectorInterface* obj, btDiscreteCollisionDetectorInterface::ClosestPointInput* input, btDiscreteCollisionDetectorInterface::Result* output, btIDebugDraw* debugDraw, bool swapResults)
{
	obj->getClosestPoints(*input, *output, debugDraw, swapResults);
}

void btDiscreteCollisionDetectorInterface_getClosestPoints2(btDiscreteCollisionDetectorInterface* obj, btDiscreteCollisionDetectorInterface::ClosestPointInput* input, btDiscreteCollisionDetectorInterface::Result* output, btIDebugDraw* debugDraw)
{
	obj->getClosestPoints(*input, *output, debugDraw);
}

void btDiscreteCollisionDetectorInterface_delete(btDiscreteCollisionDetectorInterface* obj)
{
	delete obj;
}

void btStorageResult_getClosestPointInB(btStorageResult* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_closestPointInB, value);
}

btScalar btStorageResult_getDistance(btStorageResult* obj)
{
	return obj->m_distance;
}

void btStorageResult_getNormalOnSurfaceB(btStorageResult* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->m_normalOnSurfaceB, value);
}

void btStorageResult_setClosestPointInB(btStorageResult* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_closestPointInB);
}

void btStorageResult_setDistance(btStorageResult* obj, btScalar value)
{
	obj->m_distance = value;
}

void btStorageResult_setNormalOnSurfaceB(btStorageResult* obj, btScalar* value)
{
	VECTOR3_IN(value, &obj->m_normalOnSurfaceB);
}