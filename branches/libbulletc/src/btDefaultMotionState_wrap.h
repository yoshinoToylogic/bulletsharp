#include "main.h"

extern "C"
{
	EXPORT btDefaultMotionState* btDefaultMotionState_new();
	EXPORT btDefaultMotionState* btDefaultMotionState_new2(btScalar* startTrans);
	EXPORT btDefaultMotionState* btDefaultMotionState_new3(btScalar* startTrans, btScalar* centerOfMassOffset);
	EXPORT void btMotionState_getWorldTransform(btMotionState* obj, btScalar* outTransform);
	EXPORT void btMotionState_setWorldTransform(btMotionState* obj, btScalar* transform);
	EXPORT void btMotionState_delete(btMotionState* obj);
}
