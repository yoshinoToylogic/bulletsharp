#include "main.h"

extern "C"
{
	EXPORT btManifoldArray* btManifoldArray_new();
	EXPORT int btManifoldArray_size(btManifoldArray* obj);
	EXPORT btPersistentManifold* btManifoldArray_at(btManifoldArray* obj, int n);
}
