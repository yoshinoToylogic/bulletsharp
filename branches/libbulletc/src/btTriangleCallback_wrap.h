#include "main.h"

extern "C"
{
	EXPORT void btTriangleCallback_processTriangle(btTriangleCallback* obj, btScalar* triangle, int partId, int triangleIndex);
	EXPORT void btTriangleCallback_delete(btTriangleCallback* obj);

	EXPORT void btInternalTriangleIndexCallback_internalProcessTriangleIndex(btInternalTriangleIndexCallback* obj, btScalar* triangle, int partId, int triangleIndex);
	EXPORT void btInternalTriangleIndexCallback_delete(btInternalTriangleIndexCallback* obj);
}
