#include "conversion.h"
#include "btTriangleCallback_wrap.h"

btTriangleCallbackWrapper::btTriangleCallbackWrapper(pProcessTriangle processTriangleCallback)
{
	_processTriangleCallback = processTriangleCallback;
}

void btTriangleCallbackWrapper::processTriangle(btVector3* triangle, int partId, int triangleIndex)
{
	_processTriangleCallback(triangle, partId, triangleIndex);
}


btInternalTriangleIndexCallbackWrapper::btInternalTriangleIndexCallbackWrapper(pInternalProcessTriangleIndex internalProcessTriangleIndexCallback)
{
	_internalProcessTriangleIndexCallback = internalProcessTriangleIndexCallback;
}

void btInternalTriangleIndexCallbackWrapper::internalProcessTriangleIndex(btVector3* triangle, int partId, int triangleIndex)
{
	_internalProcessTriangleIndexCallback(triangle, partId, triangleIndex);
}


btTriangleCallbackWrapper* btTriangleCallbackWrapper_new(pProcessTriangle processTriangleCallback)
{
	return new btTriangleCallbackWrapper(processTriangleCallback);
}

void btTriangleCallback_delete(btTriangleCallback* obj)
{
	delete obj;
}


btInternalTriangleIndexCallbackWrapper* btInternalTriangleIndexCallbackWrapper_new(pInternalProcessTriangleIndex internalProcessTriangleIndexCallback)
{
	return new btInternalTriangleIndexCallbackWrapper(internalProcessTriangleIndexCallback);
}

void btInternalTriangleIndexCallback_delete(btInternalTriangleIndexCallback* obj)
{
	delete obj;
}
