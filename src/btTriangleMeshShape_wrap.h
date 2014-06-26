#include "main.h"

extern "C"
{
	EXPORT void btTriangleMeshShape_getLocalAabbMax(btTriangleMeshShape* obj, btScalar* value);
	EXPORT void btTriangleMeshShape_getLocalAabbMin(btTriangleMeshShape* obj, btScalar* value);
	EXPORT btStridingMeshInterface* btTriangleMeshShape_getMeshInterface(btTriangleMeshShape* obj);
	EXPORT void btTriangleMeshShape_localGetSupportingVertex(btTriangleMeshShape* obj, btScalar* vec, btScalar* value);
	EXPORT void btTriangleMeshShape_localGetSupportingVertexWithoutMargin(btTriangleMeshShape* obj, btScalar* vec, btScalar* value);
	EXPORT void btTriangleMeshShape_recalcLocalAabb(btTriangleMeshShape* obj);
}