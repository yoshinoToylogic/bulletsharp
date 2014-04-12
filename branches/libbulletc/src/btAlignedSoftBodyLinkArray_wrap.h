#include "main.h"

extern "C"
{
	EXPORT btSoftBody_Link* btAlignedSoftBodyLinkArray_at(btAlignedSoftBodyLinkArray* obj, int n);
	EXPORT void btAlignedSoftBodyLinkArray_push_back(btAlignedSoftBodyLinkArray* obj, btSoftBody_Link* val);
	EXPORT void btAlignedSoftBodyLinkArray_resizeNoInitialize(btAlignedSoftBodyLinkArray* obj, int newSize);
	EXPORT int btAlignedSoftBodyLinkArray_size(btAlignedSoftBodyLinkArray* obj);
	EXPORT void btAlignedSoftBodyLinkArray_delete(btAlignedSoftBodyLinkArray* obj);
}
