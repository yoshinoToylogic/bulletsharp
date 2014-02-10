#include "main.h"

extern "C"
{
	EXPORT btManifoldArray* btManifoldArray_new();
	EXPORT int btManifoldArray_size(btManifoldArray* obj);
	EXPORT btPersistentManifold* btManifoldArray_at(btManifoldArray* obj, int n);
	EXPORT void btManifoldArray_resizeNoInitialize(btManifoldArray* obj, int newSize);
	EXPORT void btManifoldArray_delete(btManifoldArray* obj);
}
