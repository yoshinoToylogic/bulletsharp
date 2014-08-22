#include "main.h"

#ifndef BT_TRIANGLE_CALLBACK_H
#define pInternalProcessTriangleIndex void*
#define pProcessTriangle void*

#define btTriangleCallbackWrapper void
#define btInternalTriangleIndexCallbackWrapper void
#else
typedef void (*pInternalProcessTriangleIndex)(btVector3* triangle, int partId, int triangleIndex);
typedef void (*pProcessTriangle)(btVector3* triangle, int partId, int triangleIndex);

class btTriangleCallbackWrapper : public btTriangleCallback
{
private:
	pProcessTriangle _processTriangleCallback;
public:
	btTriangleCallbackWrapper(pProcessTriangle processTriangleCallback);
	virtual void processTriangle(btVector3* triangle, int partId, int triangleIndex);
};

class btInternalTriangleIndexCallbackWrapper : public btInternalTriangleIndexCallback
{
private:
	pInternalProcessTriangleIndex _internalProcessTriangleIndexCallback;
public:
	btInternalTriangleIndexCallbackWrapper(pInternalProcessTriangleIndex internalProcessTriangleIndexCallback);
	virtual void internalProcessTriangleIndex(btVector3* triangle, int partId, int triangleIndex);
};
#endif

extern "C"
{
	EXPORT btTriangleCallbackWrapper* btTriangleCallbackWrapper_new(pProcessTriangle processTriangleCallback);
	EXPORT void btTriangleCallback_delete(btTriangleCallback* obj);

	EXPORT btInternalTriangleIndexCallbackWrapper* btInternalTriangleIndexCallbackWrapper_new(pInternalProcessTriangleIndex internalProcessTriangleIndexCallback);
	EXPORT void btInternalTriangleIndexCallback_delete(btInternalTriangleIndexCallback* obj);
}
