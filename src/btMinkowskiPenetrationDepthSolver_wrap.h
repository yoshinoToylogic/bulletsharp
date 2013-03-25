#include "main.h"

#include <BulletCollision/NarrowPhaseCollision/btMinkowskiPenetrationDepthSolver.h>

extern "C"
{
	EXPORT btMinkowskiPenetrationDepthSolver* btMinkowskiPenetrationDepthSolver_new();
}
