#include "main.h"

extern "C"
{
	EXPORT btCompoundShapeChild* btCompoundShapeChild_array_at(btCompoundShapeChild* a, int n);
	EXPORT unsigned int uint_array_at(const unsigned int* a, int n);
	EXPORT void btVector3_array_at(const btVector3* a, int n, btScalar* value);
	EXPORT btAlignedObjectArray<btVector3>* btAlignedVector3Array_new();
	EXPORT void btAlignedVector3Array_at(btAlignedObjectArray<btVector3>* obj, int n, btScalar* value);
	EXPORT void btAlignedVector3Array_push_back(btAlignedObjectArray<btVector3>* obj, btScalar* value);
	EXPORT void btAlignedVector3Array_push_back2(btAlignedObjectArray<btVector3>* obj, btScalar* value); // btVector4
	EXPORT void btAlignedVector3Array_set(btAlignedObjectArray<btVector3>* obj, int n, btScalar* value);
	EXPORT int btAlignedVector3Array_size(btAlignedObjectArray<btVector3>* obj);
	EXPORT void btAlignedVector3Array_delete(btAlignedObjectArray<btVector3>* obj);
}
