#include "main.h"

#ifndef BT_MOTIONSTATE_H
#define pGetWorldTransform void*
#define pSetWorldTransform void*

#define btMotionStateWrapper void
#else

typedef void (*pGetWorldTransform)(btScalar* worldTrans);
typedef void (*pSetWorldTransform)(const btScalar* worldTrans);

class btMotionStateWrapper : public btMotionState
{
private:
	pGetWorldTransform _getWorldTransformCallback;
	pSetWorldTransform _setWorldTransformCallback;
public:
	btMotionStateWrapper(pGetWorldTransform getWorldTransformCallback, pSetWorldTransform setWorldTransformCallback);
	virtual void getWorldTransform(btTransform& worldTrans) const;
	virtual void setWorldTransform(const btTransform& worldTrans);
};
#endif

extern "C"
{
	EXPORT void btMotionState_getWorldTransform(btMotionState* obj, btScalar* worldTrans);
	EXPORT void btMotionState_setWorldTransform(btMotionState* obj, const btScalar* worldTrans);
	EXPORT void btMotionState_delete(btMotionState* obj);

	EXPORT btMotionStateWrapper* btMotionStateWrapper_new(pGetWorldTransform getWorldTransformCallback, pSetWorldTransform setWorldTransformCallback);
}
