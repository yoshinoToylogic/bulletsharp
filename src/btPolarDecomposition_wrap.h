#include "main.h"

extern "C"
{
	EXPORT btPolarDecomposition* btPolarDecomposition_new(btScalar tolerance, unsigned int maxIterations);
	EXPORT btPolarDecomposition* btPolarDecomposition_new2(btScalar tolerance);
	EXPORT btPolarDecomposition* btPolarDecomposition_new3();
	EXPORT unsigned int btPolarDecomposition_decompose(btPolarDecomposition* obj, btMatrix3x3* a, btMatrix3x3* u, btMatrix3x3* h);
	EXPORT unsigned int btPolarDecomposition_maxIterations(btPolarDecomposition* obj);
	EXPORT void btPolarDecomposition_delete(btPolarDecomposition* obj);
}
