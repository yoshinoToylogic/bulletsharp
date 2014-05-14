#include "main.h"

#define btDefaultMotionState_delete(obj) btMotionState_delete(obj)

extern "C"
{
	EXPORT btDefaultMotionState* btDefaultMotionState_new(btScalar* startTrans, btScalar* centerOfMassOffset);
	EXPORT btDefaultMotionState* btDefaultMotionState_new2(btScalar* startTrans);
	EXPORT btDefaultMotionState* btDefaultMotionState_new3();
	EXPORT void btDefaultMotionState_getCenterOfMassOffset(btDefaultMotionState* obj, btScalar* value);
	EXPORT void btDefaultMotionState_getGraphicsWorldTrans(btDefaultMotionState* obj, btScalar* value);
	EXPORT void btDefaultMotionState_getStartWorldTrans(btDefaultMotionState* obj, btScalar* value);
	EXPORT void* btDefaultMotionState_getUserPointer(btDefaultMotionState* obj);
	EXPORT void btDefaultMotionState_setCenterOfMassOffset(btDefaultMotionState* obj, btScalar* value);
	EXPORT void btDefaultMotionState_setGraphicsWorldTrans(btDefaultMotionState* obj, btScalar* value);
	EXPORT void btDefaultMotionState_setStartWorldTrans(btDefaultMotionState* obj, btScalar* value);
	EXPORT void btDefaultMotionState_setUserPointer(btDefaultMotionState* obj, void* value);
	EXPORT void btMotionState_getWorldTransform(btMotionState* obj, btScalar* outTransform);
	EXPORT void btMotionState_setWorldTransform(btMotionState* obj, btScalar* transform);
	EXPORT void btMotionState_delete(btMotionState* obj);
}
