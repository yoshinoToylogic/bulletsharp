#include "main.h"

#include <../Extras/Serialize/BulletWorldImporter/btWorldImporter.h>

extern "C"
{
	EXPORT btWorldImporter* btWorldImporter_new(btDynamicsWorld* world);
	EXPORT btCollisionShape* btWorldImporter_createBoxShape(btWorldImporter* obj, btScalar* halfExtents);
	EXPORT btBvhTriangleMeshShape* btWorldImporter_createBvhTriangleMeshShape(btWorldImporter* obj, btStridingMeshInterface* trimesh, btOptimizedBvh* bvh);
	EXPORT btCollisionShape* btWorldImporter_createCapsuleShapeZ(btWorldImporter* obj, btScalar radius, btScalar height);
	EXPORT btCollisionShape* btWorldImporter_createCapsuleShapeX(btWorldImporter* obj, btScalar radius, btScalar height);
	EXPORT btCollisionShape* btWorldImporter_createCapsuleShapeY(btWorldImporter* obj, btScalar radius, btScalar height);
	EXPORT btCollisionObject* btWorldImporter_createCollisionObject(btWorldImporter* obj, btScalar* startTransform, btCollisionShape* shape, char* bodyName);
	EXPORT btCompoundShape* btWorldImporter_createCompoundShape(btWorldImporter* obj);
	EXPORT btConeTwistConstraint* btWorldImporter_createConeTwistConstraint(btWorldImporter* obj, btRigidBody* rbA, btScalar* rbAFrame);
	EXPORT btConeTwistConstraint* btWorldImporter_createConeTwistConstraint2(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, btScalar* rbAFrame, btScalar* rbBFrame);
	EXPORT btConvexHullShape* btWorldImporter_createConvexHullShape(btWorldImporter* obj);
	EXPORT btCollisionShape* btWorldImporter_createConvexTriangleMeshShape(btWorldImporter* obj, btStridingMeshInterface* trimesh);
	EXPORT btCollisionShape* btWorldImporter_createCylinderShapeZ(btWorldImporter* obj, btScalar radius, btScalar height);
	EXPORT btCollisionShape* btWorldImporter_createCylinderShapeX(btWorldImporter* obj, btScalar radius, btScalar height);
	EXPORT btCollisionShape* btWorldImporter_createCylinderShapeY(btWorldImporter* obj, btScalar radius, btScalar height);
	EXPORT btGeneric6DofConstraint* btWorldImporter_createGeneric6DofConstraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, btScalar* frameInA, btScalar* frameInB, bool useLinearReferenceFrameA);
	EXPORT btGeneric6DofConstraint* btWorldImporter_createGeneric6DofConstraint2(btWorldImporter* obj, btRigidBody* rbB, btScalar* frameInB, bool useLinearReferenceFrameB);
	EXPORT btGeneric6DofSpringConstraint* btWorldImporter_createGeneric6DofSpringConstraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, btScalar* frameInA, btScalar* frameInB, bool useLinearReferenceFrameA);
	EXPORT btGImpactMeshShape* btWorldImporter_createGimpactShape(btWorldImporter* obj, btStridingMeshInterface* trimesh);
	EXPORT btHingeConstraint* btWorldImporter_createHingeConstraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, btScalar* rbAFrame, btScalar* rbBFrame, bool useReferenceFrameA);
	EXPORT btHingeConstraint* btWorldImporter_createHingeConstraint2(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, btScalar* rbAFrame, btScalar* rbBFrame);
	EXPORT btHingeConstraint* btWorldImporter_createHingeConstraint3(btWorldImporter* obj, btRigidBody* rbA, btScalar* rbAFrame, bool useReferenceFrameA);
	EXPORT btHingeConstraint* btWorldImporter_createHingeConstraint4(btWorldImporter* obj, btRigidBody* rbA, btScalar* rbAFrame);
	EXPORT btTriangleIndexVertexArray* btWorldImporter_createMeshInterface(btWorldImporter* obj, btStridingMeshInterfaceData* meshData);
	EXPORT btMultiSphereShape* btWorldImporter_createMultiSphereShape(btWorldImporter* obj, btScalar* positions, btScalar* radi, int numSpheres);
	EXPORT btOptimizedBvh* btWorldImporter_createOptimizedBvh(btWorldImporter* obj);
	EXPORT btCollisionShape* btWorldImporter_createPlaneShape(btWorldImporter* obj, btScalar* planeNormal, btScalar planeConstant);
	EXPORT btPoint2PointConstraint* btWorldImporter_createPoint2PointConstraint(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, btScalar* pivotInA, btScalar* pivotInB);
	EXPORT btPoint2PointConstraint* btWorldImporter_createPoint2PointConstraint2(btWorldImporter* obj, btRigidBody* rbA, btScalar* pivotInA);
	EXPORT btRigidBody* btWorldImporter_createRigidBody(btWorldImporter* obj, bool isDynamic, btScalar mass, btScalar* startTransform, btCollisionShape* shape, char* bodyName);
	EXPORT btScaledBvhTriangleMeshShape* btWorldImporter_createScaledTrangleMeshShape(btWorldImporter* obj, btBvhTriangleMeshShape* meshShape, btScalar* localScalingbtBvhTriangleMeshShape);
	EXPORT btSliderConstraint* btWorldImporter_createSliderConstraint(btWorldImporter* obj, btRigidBody* rbB, btScalar* frameInB, bool useLinearReferenceFrameA);
	EXPORT btSliderConstraint* btWorldImporter_createSliderConstraint2(btWorldImporter* obj, btRigidBody* rbA, btRigidBody* rbB, btScalar* frameInA, btScalar* frameInB, bool useLinearReferenceFrameA);
	EXPORT btCollisionShape* btWorldImporter_createSphereShape(btWorldImporter* obj, btScalar radius);
	EXPORT btStridingMeshInterfaceData* btWorldImporter_createStridingMeshInterfaceData(btWorldImporter* obj, btStridingMeshInterfaceData* interfaceData);
	EXPORT btTriangleInfoMap* btWorldImporter_createTriangleInfoMap(btWorldImporter* obj);
	EXPORT btTriangleIndexVertexArray* btWorldImporter_createTriangleMeshContainer(btWorldImporter* obj);
	EXPORT void btWorldImporter_deleteAllData(btWorldImporter* obj);
	EXPORT btOptimizedBvh* btWorldImporter_getBvhByIndex(btWorldImporter* obj, int index);
	EXPORT btCollisionShape* btWorldImporter_getCollisionShapeByIndex(btWorldImporter* obj, int index);
	EXPORT btCollisionShape* btWorldImporter_getCollisionShapeByName(btWorldImporter* obj, char* name);
	EXPORT btTypedConstraint* btWorldImporter_getConstraintByIndex(btWorldImporter* obj, int index);
	EXPORT btTypedConstraint* btWorldImporter_getConstraintByName(btWorldImporter* obj, char* name);
	EXPORT const char* btWorldImporter_getNameForPointer(btWorldImporter* obj, void* ptr);
	EXPORT int btWorldImporter_getNumBvhs(btWorldImporter* obj);
	EXPORT int btWorldImporter_getNumCollisionShapes(btWorldImporter* obj);
	EXPORT int btWorldImporter_getNumConstraints(btWorldImporter* obj);
	EXPORT int btWorldImporter_getNumRigidBodies(btWorldImporter* obj);
	EXPORT int btWorldImporter_getNumTriangleInfoMaps(btWorldImporter* obj);
	EXPORT btCollisionObject* btWorldImporter_getRigidBodyByIndex(btWorldImporter* obj, int index);
	EXPORT btRigidBody* btWorldImporter_getRigidBodyByName(btWorldImporter* obj, char* name);
	EXPORT btTriangleInfoMap* btWorldImporter_getTriangleInfoMapByIndex(btWorldImporter* obj, int index);
	EXPORT int btWorldImporter_getVerboseMode(btWorldImporter* obj);
	EXPORT void btWorldImporter_setDynamicsWorldInfo(btWorldImporter* obj, btScalar* gravity, btContactSolverInfo* solverInfo);
	EXPORT void btWorldImporter_setVerboseMode(btWorldImporter* obj, int verboseMode);
	EXPORT void btWorldImporter_delete(btWorldImporter* obj);
}
