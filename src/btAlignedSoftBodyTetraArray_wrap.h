#include "main.h"

extern "C"
{
	EXPORT btSoftBody_Tetra* btAlignedSoftBodyTetraArray_at(btAlignedSoftBodyTetraArray* obj, int n);
	EXPORT void btAlignedSoftBodyTetraArray_push_back(btAlignedSoftBodyTetraArray* obj, btSoftBody_Tetra* val);
	EXPORT void btAlignedSoftBodyTetraArray_resizeNoInitialize(btAlignedSoftBodyTetraArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyTetraArray_size(btAlignedSoftBodyTetraArray* obj);
	EXPORT void btAlignedSoftBodyTetraArray_delete(btAlignedSoftBodyTetraArray* obj);
}
