#pragma once

#ifdef WIN32
#define STDCALL __stdcall*
#define uint unsigned int
#else
//#define STDCALL *
#define STDCALL __attribute__ ((stdcall))*
#endif

#ifdef _MSC_VER
#define EXPORT __declspec(dllexport)
#else
#if __GNUC__ >= 4
	#define EXPORT __attribute__ ((visibility("default")))
#else
	#define EXPORT
#endif
#endif

#ifndef BT_SCALAR_H
#ifdef BT_USE_DOUBLE_PRECISION
#define btScalar double
#else
#define btScalar float
#endif
#endif

#ifndef BULLET_COLLISION_COMMON_H
#define btActionInterface void
#define btIDebugDraw void
#define btCollisionWorld void
#define btBoxShape void
#define btSphereShape void
#define btCapsuleShape void
#define btCapsuleShapeX void
#define btCapsuleShapeZ void
#define btCapsuleShapeData void
#define btCylinderShape void
#define btCylinderShapeX void
#define btCylinderShapeZ void
#define btConeShape void
#define btConeShapeX void
#define btConeShapeZ void
#define btStaticPlaneShape void
#define btConvexHullShape void
#define btTriangleMesh void
#define btConvexTriangleMeshShape void
#define btBvhTriangleMeshShape void
#define btScaledBvhTriangleMeshShape void
#define btTriangleMeshShape void
#define btTriangleIndexVertexArray void
#define btCompoundShape void
#define btTetrahedronShape void
#define btEmptyShape void
#define btMultiSphereShape void
#define btUniformScalingShape void
#define btSphereSphereCollisionAlgorithm void
#define btDefaultCollisionConfiguration void
#define btCollisionDispatcher void
#define btSimpleBroadphase void
#define btAABB void
#define btAxisSweep3 void
#define bt32BitAxisSweep3 void
#define BT_BOX_BOX_TRANSFORM_CACHE void
#define btBroadphaseAabbCallback void
#define btBroadphaseInterface void
#define btBroadphasePair void
#define btBroadphasePairArray void
#define btBroadphasePairSortPredicate void
#define btBroadphaseProxy void
#define btBroadphaseRayCallback void
#define btCollisionAlgorithm void
#define btCollisionAlgorithmConstructionInfo void
#define btCollisionObjectWrapper void
#define btCollisionShape void
#define btCollisionWorld_AllHitsRayResultCallback void
#define btCollisionWorld_ClosestConvexResultCallback void
#define btCollisionWorld_ClosestRayResultCallback void
#define btCollisionWorld_ContactResultCallback void
#define btCollisionWorld_ConvexResultCallback void
#define btCollisionWorld_LocalConvexResult void
#define btCollisionWorld_LocalRayResult void
#define btCollisionWorld_LocalShapeInfo void
#define btCollisionWorld_RayResultCallback void
#define btCompoundShapeChild void
#define btConvexShape void
#define btDbvtBroadphase void
#define btDefaultCollisionConstructionInfo void
#define btDefaultMotionState void
#define btDiscreteCollisionDetectorInterface void
#define btDiscreteCollisionDetectorInterface_ClosestPointInput void
#define btDiscreteCollisionDetectorInterface_Result void
#define btDispatcher void
#define btDispatcherInfo void
#define btDynamicsWorld void
#define btHashedOverlappingPairCache void
#define btIndexedMesh void
#define IndexedMeshArray void
#define btManifoldArray void
#define btManifoldResult void
#define btMinkowskiPenetrationDepthSolver void
#define btMotionState void
#define btMultiSapBroadphase void
#define btNullPairCache void
#define btOverlapCallback void
#define btOverlapFilterCallback void
#define btOverlappingPairCache void
#define btOverlappingPairCallback void
#define btPolyhedralConvexAabbCachingShape void
#define btPolyhedralConvexShape void
#define btPositionAndRadius void
#define btRigidBody void
#define btRigidBody_btRigidBodyConstructionInfo void
#define btRotationalLimitMotor void
#define btSortedOverlappingPairCache void
#define btStorageResult void
#define btTranslationalLimitMotor void
#define btTriangleIndexVertexArray void
#define btMatrix3x3 void
#define btQuaternion void
#define btTransform void
#define btVector3 void
#define btVector4 void
#define btAligendBroadphasePairArray void
#define btAlignedCollisionObjectArray void
#define btAlignedIntArray void
#define btAlignedScalarArray void
#define btAlignedVector3Array void
#define btCollisionObjectArray void
#define eBT_PLANE_INTERSECTION_TYPE int
#define PHY_ScalarType int
#define btDynamicsWorldType int
#else
#define btAlignedBroadphasePairArray btAlignedObjectArray<btBroadphasePair>
#define btAlignedCollisionObjectArray btAlignedObjectArray<btCollisionObject*>
#define btAlignedIntArray btAlignedObjectArray<int>
#define btAlignedScalarArray btAlignedObjectArray<btScalar>
#define btAlignedVector3Array btAlignedObjectArray<btVector3>
#define btCollisionWorld_AllHitsRayResultCallback btCollisionWorld::AllHitsRayResultCallback
#define btCollisionWorld_ClosestConvexResultCallback btCollisionWorld::ClosestConvexResultCallback
#define btCollisionWorld_ClosestRayResultCallback btCollisionWorld::ClosestRayResultCallback
#define btCollisionWorld_ContactResultCallback btCollisionWorld::ContactResultCallback
#define btCollisionWorld_ConvexResultCallback btCollisionWorld::ConvexResultCallback
#define btCollisionWorld_LocalConvexResult btCollisionWorld::LocalConvexResult
#define btCollisionWorld_LocalRayResult btCollisionWorld::LocalRayResult
#define btCollisionWorld_LocalShapeInfo btCollisionWorld::LocalShapeInfo
#define btCollisionWorld_RayResultCallback btCollisionWorld::RayResultCallback
#define btDiscreteCollisionDetectorInterface_ClosestPointInput btDiscreteCollisionDetectorInterface::ClosestPointInput
#define btDiscreteCollisionDetectorInterface_Result btDiscreteCollisionDetectorInterface::Result
#define btRigidBody_btRigidBodyConstructionInfo btRigidBody::btRigidBodyConstructionInfo
#endif

#ifndef BULLET_DYNAMICS_COMMON_H
#define btAngularLimit void
#define btDiscreteDynamicsWorld void
#define btPoint2PointConstraint void
#define btHingeConstraint void
#define btConeTwistConstraint void
#define btGeneric6DofConstraint void
#define btSliderConstraint void
#define btGeneric6DofSpringConstraint void
#define btUniversalConstraint void
#define btHinge2Constraint void
#define btGearConstraint void
#define btSequentialImpulseConstraintSolver void
#define btRaycastVehicle void
#define btJointFeedback void
#define btConstraintArray void
#define btConstraintRow void
#define btConstraintSetting void
#define btConstraintSolver void
#define btContactSolverInfo void
#define btContactSolverInfoData void
#define btSimulationIslandManager void
#define btSimulationIslandManager_IslandCallback void
#define btSolverBody void
#define btTypedConstraint void
#define btTypedConstraint_btConstraintInfo1 void
#define btTypedConstraint_btConstraintInfo2 void
#define btUnionFind void
#define btTypedConstraintType int
#else
#define btSimulationIslandManager_IslandCallback btSimulationIslandManager::IslandCallback
#define btTypedConstraint_btConstraintInfo1 btTypedConstraint::btConstraintInfo1
#define btTypedConstraint_btConstraintInfo2 btTypedConstraint::btConstraintInfo2
#endif

#ifndef BT_BULLET_FILE_H
#define bParse_btBulletFile void
#else
#define bParse_btBulletFile bParse::btBulletFile
#endif

#ifndef BT_CHARACTER_CONTROLLER_INTERFACE_H
#define btCharacterControllerInterface void
#endif

#ifndef BT_COLLISION_CONFIGURATION
#define btCollisionConfiguration void
#endif

#ifndef BT_COLLISION_CREATE_FUNC
#define btCollisionAlgorithmCreateFunc void
#endif

#ifndef BT_COLLISION_OBJECT_H
#define btCollisionObject void
#endif

#ifndef BT_CONCAVE_SHAPE_H
#define btConcaveShape void
#endif

#ifndef BT_CONVEX_2D_SHAPE_H
#define btConvex2dShape void
#endif

#ifndef BT_CONVEX_INTERNAL_SHAPE_H
#define btConvexInternalShape void
#define btConvexInternalAabbCachingShape void
#endif

#ifndef BT_CONVEX_PENETRATION_DEPTH_H
#define btConvexPenetrationDepthSolver void
#endif

#ifndef BT_DBVT_BROADPHASE_H
#define btDbvtProxy void
#endif

#ifndef BT_DYNAMIC_BOUNDING_VOLUME_TREE_H
#define btDbvt void
#define btDbvtNode void
#endif

#ifndef BT_GHOST_OBJECT_H
#define btGhostObject void
#define btGhostPairCallback void
#define btPairCachingGhostObject void
#endif

#ifndef BT_GIMPACT_BVH_CONCAVE_COLLISION_ALGORITHM_H
#define btGImpactCollisionAlgorithm void
#endif

#ifndef BT_GJK_PAIR_DETECTOR_H
#define btGjkPairDetector void
#endif

#ifndef BT_KINEMATIC_CHARACTER_CONTROLLER_H
#define btKinematicCharacterController void
#endif

#ifndef BT_MANIFOLD_CONTACT_POINT_H
#define btManifoldPoint void
#define ContactAddedCallback void
#endif

#ifndef BT_OPTIMIZED_BVH_H
#define btOptimizedBvh void
#endif

#ifndef __BT_PARALLEL_CONSTRAINT_SOLVER_H
#define btParallelConstraintSolver void
#endif

#ifndef BT_PERSISTENT_MANIFOLD_H
#define btPersistentManifold void
#define ContactDestroyedCallback void
#define ContactProcessedCallback void
#endif

#ifndef BT_POINT_COLLECTOR_H
#define btPointCollector void
#endif

#ifndef _BT_POOL_ALLOCATOR_H
#define btPoolAllocator void
#endif

#ifndef _BT_POLYHEDRAL_FEATURES_H
#define btConvexPolyhedron void
#define btFace void
#define btAlignedFaceArray void
#else
#define btAlignedFaceArray btAlignedObjectArray<btFace>
#endif

#ifndef BT_RAYCASTVEHICLE_H
#define btDefaultVehicleRaycaster void
#define btRaycastVehicle_btVehicleTuning void
#define btVehicleRaycaster void
#define btVehicleRaycaster_btVehicleRaycasterResult void
#define btWheelInfo void
#define btWheelInfo_RaycastInfo void
#define btWheelInfoConstructionInfo void
#else
#define btRaycastVehicle_btVehicleTuning btRaycastVehicle::btVehicleTuning
#define btVehicleRaycaster_btVehicleRaycasterResult btVehicleRaycaster::btVehicleRaycasterResult
#define btWheelInfo_RaycastInfo btWheelInfo::RaycastInfo
#endif

#ifndef BT_SERIALIZER_H
#define btChunk void
#define btDefaultSerializer void
#define btPointerUid void
#define btSerializer void
#endif

#ifndef BT_SHAPE_HULL_H
#define btShapeHull void
#endif

#ifndef BT_SPU_COLLISION_TASK_PROCESS_H
#define SpuCollisionTaskProcess void
#define SpuGatherAndProcessWorkUnitInput void
#endif

#ifndef BT_SPU_GATHERING_COLLISION__DISPATCHER_H
#define SpuGatheringCollisionDispatcher void
#endif

#ifndef _BT_SOFT_BODY_H
#define btSoftBody void
#define btSoftBodyWorldInfo void
#define btSoftBody_eAeroModel int
#define btSoftBody_eFeature int
#define btSoftBody_ePSolver int
#define btSoftBody_eSolverPresets int
#define btSoftBody_eVSolver int
#define btSoftBody_fCollision int
#define btSoftBody_fMaterial int
#define btSoftBody_AJoint void
#define btSoftBody_AJoint_IControl void
#define btSoftBody_Anchor void
#define btSoftBody_Body void
#define btSoftBody_CJoint void
#define btSoftBody_Cluster void
#define btSoftBody_Config void
#define btSoftBody_Element void
#define btSoftBody_Face void
#define btSoftBody_Feature void
#define btSoftBody_ImplicitFn void
#define btSoftBody_Impulse void
#define btSoftBody_Joint void
#define btSoftBody_Joint_eType void
#define btSoftBody_Joint_Specs void
#define btSoftBody_Link void
#define btSoftBody_LJoint void
#define btSoftBody_Material void
#define btSoftBody_Node void
#define btSoftBody_Note void
#define btSoftBody_Pose void
#define btSoftBody_RayFromToCaster void
#define btSoftBody_RContact void
#define btSoftBody_SContact void
#define btSoftBody_sCti void
#define btSoftBody_sMedium void
#define btSoftBody_SolverState void
#define btSoftBody_sRayCast void
#define btSoftBody_Tetra void
#else
#define btSoftBody_eAeroModel btSoftBody::eAeroModel
#define btSoftBody_eFeature btSoftBody::eFeature
#define btSoftBody_ePSolver btSoftBody::ePSolver
#define btSoftBody_eSolverPresets btSoftBody::eSolverPresets
#define btSoftBody_eVSolver btSoftBody::eVSolver
#define btSoftBody_fCollision btSoftBody::fCollision
#define btSoftBody_fMaterial btSoftBody::fMaterial
#define btSoftBody_AJoint btSoftBody::AJoint
#define btSoftBody_AJoint_IControl btSoftBody::AJoint::IControl
#define btSoftBody_Anchor btSoftBody::Anchor
#define btSoftBody_Body btSoftBody::Body
#define btSoftBody_CJoint btSoftBody::CJoint
#define btSoftBody_Cluster btSoftBody::Cluster
#define btSoftBody_Config btSoftBody::Config
#define btSoftBody_Element btSoftBody::Element
#define btSoftBody_Face btSoftBody::Face
#define btSoftBody_Feature btSoftBody::Feature
#define btSoftBody_ImplicitFn btSoftBody::ImplicitFn
#define btSoftBody_Impulse btSoftBody::Impulse
#define btSoftBody_Joint btSoftBody::Joint
#define btSoftBody_Joint_eType btSoftBody::Joint::eType
#define btSoftBody_Joint_Specs btSoftBody::Joint::Specs
#define btSoftBody_Link btSoftBody::Link
#define btSoftBody_LJoint btSoftBody::LJoint
#define btSoftBody_Material btSoftBody::Material
#define btSoftBody_Node btSoftBody::Node
#define btSoftBody_Note btSoftBody::Note
#define btSoftBody_Pose btSoftBody::Pose
#define btSoftBody_RayFromToCaster btSoftBody::RayFromToCaster
#define btSoftBody_RContact btSoftBody::RContact
#define btSoftBody_SContact btSoftBody::SContact
#define btSoftBody_sCti btSoftBody::sCti
#define btSoftBody_sMedium btSoftBody::sMedium
#define btSoftBody_SolverState btSoftBody::SolverState
#define btSoftBody_sRayCast btSoftBody::sRayCast
#define btSoftBody_Tetra btSoftBody::Tetra
#endif

#ifndef BT_SOFT_BODY_HELPERS_H
#define fDrawFlags void
#endif

#ifndef BT_SOFT_BODY_SOLVERS_H
#define btSoftBodySolver void
#endif

#ifndef BT_SOFT_RIGID_DYNAMICS_WORLD_H
#define btSoftBodyArray void
#define btSoftRigidDynamicsWorld void
#endif

#ifndef BT_STRIDING_MESHINTERFACE_H
#define btStridingMeshInterface void
#define btStridingMeshInterfaceData void
#endif

#ifndef BT_THREAD_SUPPORT_INTERFACE_H
#define btBarrier void
#define btCriticalSection void
#define btThreadSupportInterface void
#endif

#ifndef BT_TRIANGLE_CALLBACK_H
#define btInternalTriangleIndexCallback void
#define btTriangleCallback void
#endif

#ifndef _BT_TRIANGLE_INFO_MAP_H
#define btTriangleInfoMap void
#endif

#ifndef BT_VORONOI_SIMPLEX_SOLVER_H
#define btSubSimplexClosestResult void
#define btUsageBitfield void
#define btVoronoiSimplexSolver void
#endif

#ifndef BT_WIN32_THREAD_SUPPORT_H
#define Win32ThreadSupport void
#define Win32ThreadSupport_Win32ThreadConstructionInfo void
typedef void (*Win32ThreadFunc)(void* userPtr,void* lsMemory);
typedef void* (*Win32lsMemorySetupFunc)();
#else
#define Win32ThreadSupport_Win32ThreadConstructionInfo Win32ThreadSupport::Win32ThreadConstructionInfo
#endif

#ifndef BT_WORLD_IMPORTER_H
#define btWorldImporter void
#endif

#ifndef BULLET_WORLD_IMPORTER_H
#define btBulletWorldImporter void
#endif

#ifndef BT_BULLET_XML_WORLD_IMPORTER_H
#define btBulletXmlWorldImporter void
#endif

#ifndef GIMPACT_SHAPE_H
#define btGImpactBoxSet void
#define btGImpactCompoundShape void
#define btGImpactCompoundShape_CompoundPrimitiveManager void
#define btGImpactMeshShapePart_TrimeshPrimitiveManager void
#define btGImpactMeshShape void
#define btGImpactMeshShapePart void
#define btGImpactShapeInterface void
#define btTetrahedronShapeEx void
#define eGIMPACT_SHAPE_TYPE int
#else
#define btGImpactCompoundShape_CompoundPrimitiveManager btGImpactCompoundShape::CompoundPrimitiveManager
#define btGImpactMeshShapePart_TrimeshPrimitiveManager btGImpactMeshShapePart::TrimeshPrimitiveManager
#endif

#ifndef GIMPACT_TRIANGLE_SHAPE_EX_H
#define btPrimitiveTriangle void
#define btTriangleShapeEx void
#endif

#ifndef GIM_BOX_SET_H_INCLUDED
#define btPrimitiveManagerBase void
#endif

#ifndef POLARDECOMPOSITION_H
#define btPolarDecomposition void
#endif

#ifndef SPU_GATHERING_COLLISION_TASK_H
#if defined(_WIN64)
	typedef unsigned __int64 ppu_address_t;
#elif defined(__LP64__) || defined(__x86_64__)
	typedef unsigned long int ppu_address_t;
#else
	typedef unsigned int ppu_address_t;
#endif
#define CollisionTask_LocalStoreMemory void
#define SpuGatherAndProcessPairsTaskDesc void
#endif
