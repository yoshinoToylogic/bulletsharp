#include "main.h"

#include <BulletCollision/Gimpact/btGImpactShape.h>

extern "C"
{
	EXPORT btTetrahedronShapeEx* btTetrahedronShapeEx_new();
	EXPORT void btTetrahedronShapeEx_setVertices(btTetrahedronShapeEx* obj, btScalar* v0, btScalar* v1, btScalar* v2, btScalar* v3);

	EXPORT bool btGImpactShapeInterface_childrenHasTransform(btGImpactShapeInterface* obj);
	EXPORT const btGImpactBoxSet* btGImpactShapeInterface_getBoxSet(btGImpactShapeInterface* obj);
	EXPORT void btGImpactShapeInterface_getBulletTetrahedron(btGImpactShapeInterface* obj, int prim_index, btTetrahedronShapeEx* tetrahedron);
	EXPORT void btGImpactShapeInterface_getBulletTriangle(btGImpactShapeInterface* obj, int prim_index, btTriangleShapeEx* triangle);
	EXPORT void btGImpactShapeInterface_getChildAabb(btGImpactShapeInterface* obj, int child_index, btScalar* t, btScalar* aabbMin, btScalar* aabbMax);
	EXPORT btCollisionShape* btGImpactShapeInterface_getChildShape(btGImpactShapeInterface* obj, int index);
	EXPORT void btGImpactShapeInterface_getChildTransform(btGImpactShapeInterface* obj, int index);
	EXPORT eGIMPACT_SHAPE_TYPE btGImpactShapeInterface_getGImpactShapeType(btGImpactShapeInterface* obj);
	EXPORT const btAABB* btGImpactShapeInterface_getLocalBox(btGImpactShapeInterface* obj);
	EXPORT int btGImpactShapeInterface_getNumChildShapes(btGImpactShapeInterface* obj);
	EXPORT const btPrimitiveManagerBase* btGImpactShapeInterface_getPrimitiveManager(btGImpactShapeInterface* obj);
	EXPORT void btGImpactShapeInterface_getPrimitiveTriangle(btGImpactShapeInterface* obj, int index, btPrimitiveTriangle* triangle);
	EXPORT bool btGImpactShapeInterface_hasBoxSet(btGImpactShapeInterface* obj);
	EXPORT void btGImpactShapeInterface_lockChildShapes(btGImpactShapeInterface* obj);
	EXPORT bool btGImpactShapeInterface_needsRetrieveTetrahedrons(btGImpactShapeInterface* obj);
	EXPORT bool btGImpactShapeInterface_needsRetrieveTriangles(btGImpactShapeInterface* obj);
	EXPORT void btGImpactShapeInterface_postUpdate(btGImpactShapeInterface* obj);
	EXPORT void btGImpactShapeInterface_processAllTrianglesRay(btGImpactShapeInterface* obj, btTriangleCallback* __unnamed0, btScalar* __unnamed1, btScalar* __unnamed2);
	EXPORT void btGImpactShapeInterface_rayTest(btGImpactShapeInterface* obj, btScalar* rayFrom, btScalar* rayTo, btCollisionWorld::RayResultCallback* resultCallback);
	EXPORT void btGImpactShapeInterface_setChildTransform(btGImpactShapeInterface* obj, int index, btScalar* transform);
	EXPORT void btGImpactShapeInterface_unlockChildShapes(btGImpactShapeInterface* obj);
	EXPORT void btGImpactShapeInterface_updateBound(btGImpactShapeInterface* obj);

	EXPORT btGImpactCompoundShape::CompoundPrimitiveManager* btGImpactCompoundShape_CompoundPrimitiveManager_new(btGImpactCompoundShape::CompoundPrimitiveManager* compound);
	EXPORT btGImpactCompoundShape::CompoundPrimitiveManager* btGImpactCompoundShape_CompoundPrimitiveManager_new2(btGImpactCompoundShape* compoundShape);
	EXPORT btGImpactCompoundShape::CompoundPrimitiveManager* btGImpactCompoundShape_CompoundPrimitiveManager_new3();
	EXPORT btGImpactCompoundShape* btGImpactCompoundShape_CompoundPrimitiveManager_getCompoundShape(btGImpactCompoundShape::CompoundPrimitiveManager* obj);
	EXPORT void btGImpactCompoundShape_CompoundPrimitiveManager_setCompoundShape(btGImpactCompoundShape::CompoundPrimitiveManager* obj, btGImpactCompoundShape* value);
	EXPORT btGImpactCompoundShape* btGImpactCompoundShape_new(bool children_has_transform);
	EXPORT btGImpactCompoundShape* btGImpactCompoundShape_new2();
	EXPORT void btGImpactCompoundShape_addChildShape(btGImpactCompoundShape* obj, btScalar* localTransform, btCollisionShape* shape);
	EXPORT void btGImpactCompoundShape_addChildShape2(btGImpactCompoundShape* obj, btCollisionShape* shape);
	EXPORT btGImpactCompoundShape::CompoundPrimitiveManager* btGImpactCompoundShape_getCompoundPrimitiveManager(btGImpactCompoundShape* obj);

	EXPORT btGImpactMeshShapePart::TrimeshPrimitiveManager* btGImpactMeshShapePart_TrimeshPrimitiveManager_new(btStridingMeshInterface* meshInterface, int part);
	EXPORT btGImpactMeshShapePart::TrimeshPrimitiveManager* btGImpactMeshShapePart_TrimeshPrimitiveManager_new2(btGImpactMeshShapePart::TrimeshPrimitiveManager* manager);
	EXPORT btGImpactMeshShapePart::TrimeshPrimitiveManager* btGImpactMeshShapePart_TrimeshPrimitiveManager_new3();
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_bullet_triangle(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, int prim_index, btTriangleShapeEx* triangle);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_indices(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, int face_index, unsigned int* i0, unsigned int* i1, unsigned int* i2);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, unsigned int vertex_index, btScalar* vertex);
	EXPORT int btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex_count(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT const unsigned char* btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexbase(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT int btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexstride(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT PHY_ScalarType btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndicestype(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT int btGImpactMeshShapePart_TrimeshPrimitiveManager_getLock_count(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT btScalar btGImpactMeshShapePart_TrimeshPrimitiveManager_getMargin(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT btStridingMeshInterface* btGImpactMeshShapePart_TrimeshPrimitiveManager_getMeshInterface(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT int btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumfaces(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT int btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumverts(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT int btGImpactMeshShapePart_TrimeshPrimitiveManager_getPart(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_getScale(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT int btGImpactMeshShapePart_TrimeshPrimitiveManager_getStride(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT PHY_ScalarType btGImpactMeshShapePart_TrimeshPrimitiveManager_getType(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT const unsigned char* btGImpactMeshShapePart_TrimeshPrimitiveManager_getVertexbase(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_lock(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexbase(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, unsigned char* value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexstride(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, int value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndicestype(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, PHY_ScalarType value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setLock_count(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, int value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setMargin(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, btScalar value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setMeshInterface(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, btStridingMeshInterface* value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumfaces(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, int value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumverts(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, int value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setPart(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, int value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setScale(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, btScalar* value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setStride(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, int value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setType(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, PHY_ScalarType value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_setVertexbase(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj, unsigned char* value);
	EXPORT void btGImpactMeshShapePart_TrimeshPrimitiveManager_unlock(btGImpactMeshShapePart::TrimeshPrimitiveManager* obj);
	EXPORT btGImpactMeshShapePart* btGImpactMeshShapePart_new();
	EXPORT btGImpactMeshShapePart* btGImpactMeshShapePart_new2(btStridingMeshInterface* meshInterface, int part);
	EXPORT int btGImpactMeshShapePart_getPart(btGImpactMeshShapePart* obj);
	EXPORT btGImpactMeshShapePart::TrimeshPrimitiveManager* btGImpactMeshShapePart_getTrimeshPrimitiveManager(btGImpactMeshShapePart* obj);
	EXPORT void btGImpactMeshShapePart_getVertex(btGImpactMeshShapePart* obj, int vertex_index, btScalar* vertex);
	EXPORT int btGImpactMeshShapePart_getVertexCount(btGImpactMeshShapePart* obj);

	EXPORT btGImpactMeshShape* btGImpactMeshShape_new(btStridingMeshInterface* meshInterface);
	EXPORT btStridingMeshInterface* btGImpactMeshShape_getMeshInterface(btGImpactMeshShape* obj);
	EXPORT btGImpactMeshShapePart* btGImpactMeshShape_getMeshPart(btGImpactMeshShape* obj, int index);
	EXPORT int btGImpactMeshShape_getMeshPartCount(btGImpactMeshShape* obj);
}
