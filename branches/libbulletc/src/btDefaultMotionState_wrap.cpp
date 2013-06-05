#include "conversion.h"
#include "btDefaultMotionState_wrap.h"

btDefaultMotionState* btDefaultMotionState_new()
{
	return ALIGNED_NEW(btDefaultMotionState) ();
}

btDefaultMotionState* btDefaultMotionState_new2(btScalar* startTrans)
{
	TRANSFORM_CONV(startTrans);
	return ALIGNED_NEW(btDefaultMotionState) (TRANSFORM_USE(startTrans));
}

btDefaultMotionState* btDefaultMotionState_new3(btScalar* startTrans, btScalar* centerOfMassOffset)
{
	TRANSFORM_CONV(startTrans);
	TRANSFORM_CONV(centerOfMassOffset);
	return ALIGNED_NEW(btDefaultMotionState) (TRANSFORM_USE(startTrans), TRANSFORM_USE(centerOfMassOffset));
}

void btMotionState_delete(btMotionState* obj)
{
	ALIGNED_FREE(obj);
}

void btMotionState_getWorldTransform(btMotionState* obj, btScalar* outTransform)
{
	TRANSFORM_DEF(outTransform);
	obj->getWorldTransform(TRANSFORM_USE(outTransform));
	TRANSFORM_DEF_OUT(outTransform);
}

void btMotionState_setWorldTransform(btMotionState* obj, btScalar* transform)
{
	TRANSFORM_CONV(transform);
	obj->setWorldTransform(TRANSFORM_USE(transform));
}
