#include "main.h"

extern "C"
{
	EXPORT btSoftBody_Cluster* btAlignedSoftBodyClusterArray_at(btAlignedSoftBodyClusterArray* obj, int n);
	EXPORT void btAlignedSoftBodyClusterArray_push_back(btAlignedSoftBodyClusterArray* obj, btSoftBody_Cluster* val);
	EXPORT void btAlignedSoftBodyClusterArray_resizeNoInitialize(btAlignedSoftBodyClusterArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyClusterArray_size(btAlignedSoftBodyClusterArray* obj);
	EXPORT void btAlignedSoftBodyClusterArray_delete(btAlignedSoftBodyClusterArray* obj);
}
