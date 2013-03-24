#pragma once

#include <iostream>
#include <LinearMath/btAlignedAllocator.h>
#include <LinearMath/btVector3.h>
#include <LinearMath/btMatrix3x3.h>
#include <LinearMath/btQuickprof.h>
#include <btBulletCollisionCommon.h>
#include <btBulletDynamicsCommon.h>
#include "conversion.h"

#define ALIGNED_NEW_FORCE(targetClass) new (btAlignedAlloc(sizeof(targetClass), 16)) targetClass
#define ALIGNED_FREE_FORCE(target) btAlignedFree(target)

#if defined(BT_USE_SIMD_VECTOR3) && defined(BT_USE_SSE_IN_API) && defined(BT_USE_SSE)
#define ALIGNED_NEW(targetClass) ALIGNED_NEW_FORCE(targetClass)
#define ALIGNED_FREE(target) ALIGNED_FREE_FORCE(target)
#else
#define ALIGNED_NEW(targetClass) new targetClass
#define ALIGNED_FREE(target) delete target
#endif
