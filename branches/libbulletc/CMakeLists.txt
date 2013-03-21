CMAKE_MINIMUM_REQUIRED (VERSION 2.6)
PROJECT (libbulletc)

FIND_PATH(BULLET_INCLUDE_DIR NAMES btBulletCollisionCommon.h
  PATHS
  ${PROJECT_SOURCE_DIR}/../../bullet/src/
  ${PROJECT_SOURCE_DIR}/../bullet/src/
  ENV CPATH
  /usr/include
  /usr/local/include
  /opt/local/include
  NO_DEFAULT_PATH
)
INCLUDE_DIRECTORIES(${BULLET_INCLUDE_DIR})

IF(${CMAKE_GENERATOR} MATCHES "Visual Studio 9 2008")
    SET(REL_LIB_DIR msvc/2008)
ELSE(${CMAKE_GENERATOR} MATCHES "Visual Studio 10")
    SET(REL_LIB_DIR msvc/2010)
ELSE(${CMAKE_GENERATOR} MATCHES "Visual Studio 11")
    SET(REL_LIB_DIR msvc/2012)
ENDIF()
SET(BULLET_LIB_DIR ${BULLET_INCLUDE_DIR}/../${REL_LIB_DIR}/lib/${CMAKE_CFG_INTDIR})

LINK_DIRECTORIES(${BULLET_LIB_DIR}/${CMAKE_BUILD_TYPE})

ADD_LIBRARY(libbulletc SHARED
    dllmain.cpp
    src/conversion.h
    src/main.h
    src/btActionInterface_wrap.cpp
    src/btActionInterface_wrap.h
    src/btAlignedCollisionObjectArray_wrap.cpp
    src/btAlignedCollisionObjectArray_wrap.h
    src/btAxisSweep3_wrap.cpp
    src/btAxisSweep3_wrap.h
    src/btBoxShape_wrap.cpp
    src/btBoxShape_wrap.h
    src/btBroadphaseInterface_wrap.cpp
    src/btBroadphaseInterface_wrap.h
    src/btBroadphaseProxy_wrap.cpp
    src/btBroadphaseProxy_wrap.h
    src/btCapsuleShape_wrap.cpp
    src/btCapsuleShape_wrap.h
    src/btCollisionConfiguration_wrap.cpp
    src/btCollisionConfiguration_wrap.h
    src/btCollisionDispatcher_wrap.cpp
    src/btCollisionDispatcher_wrap.h
    src/btCollisionObject_wrap.cpp
    src/btCollisionObject_wrap.h
    src/btCollisionShape_wrap.cpp
    src/btCollisionShape_wrap.h
    src/btCollisionWorld_wrap.cpp
    src/btCollisionWorld_wrap.h
    src/btConeShape_wrap.cpp
    src/btConeShape_wrap.h
    src/btConeTwistConstraint_wrap.cpp
    src/btConeTwistConstraint_wrap.h
    src/btConstraintSolver_wrap.cpp
    src/btConstraintSolver_wrap.h
    src/btContactSolverInfo_wrap.cpp
    src/btContactSolverInfo_wrap.h
    src/btConvex2dShape_wrap.cpp
    src/btConvex2dShape_wrap.h
    src/btConvexHullShape_wrap.cpp
    src/btConvexHullShape_wrap.h
    src/btConvexShape_wrap.cpp
    src/btConvexShape_wrap.h
    src/btCylinderShape_wrap.cpp
    src/btCylinderShape_wrap.h
    src/btDbvtBroadphase_wrap.cpp
    src/btDbvtBroadphase_wrap.h
    src/btDefaultCollisionConfiguration_wrap.cpp
    src/btDefaultCollisionConfiguration_wrap.h
    src/btDefaultMotionState_wrap.cpp
    src/btDefaultMotionState_wrap.h
    src/btDiscreteDynamicsWorld_wrap.cpp
    src/btDiscreteDynamicsWorld_wrap.h
    src/btDispatcher_wrap.cpp
    src/btDispatcher_wrap.h
    src/btDynamicsWorld_wrap.cpp
    src/btDynamicsWorld_wrap.h
    src/btGeneric6DofConstraint_wrap.cpp
    src/btGeneric6DofConstraint_wrap.h
    src/btHingeConstraint_wrap.cpp
    src/btHingeConstraint_wrap.h
    src/btOverlappingPairCache_wrap.cpp
    src/btOverlappingPairCache_wrap.h
    src/btOverlappingPairCallback_wrap.cpp
    src/btOverlappingPairCallback_wrap.h
    src/btPoint2PointConstraint_wrap.cpp
    src/btPoint2PointConstraint_wrap.h
    src/btPolyhedralConvexShape_wrap.cpp
    src/btPolyhedralConvexShape_wrap.h
    src/btRigidBody_wrap.cpp
    src/btRigidBody_wrap.h
    src/btSequentialImpulseConstraintSolver_wrap.cpp
    src/btSequentialImpulseConstraintSolver_wrap.h
    src/btSimulationIslandManager_wrap.cpp
    src/btSimulationIslandManager_wrap.h
    src/btSphereShape_wrap.cpp
    src/btTypedConstraint_wrap.cpp
    src/btTypedConstraint_wrap.h
)

TARGET_LINK_LIBRARIES(libbulletc BulletCollision_${CMAKE_CFG_INTDIR}.lib)
TARGET_LINK_LIBRARIES(libbulletc BulletDynamics_${CMAKE_CFG_INTDIR}.lib)
TARGET_LINK_LIBRARIES(libbulletc LinearMath_${CMAKE_CFG_INTDIR}.lib)
