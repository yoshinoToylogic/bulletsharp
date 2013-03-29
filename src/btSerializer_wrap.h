#include "main.h"

extern "C"
{
	EXPORT btChunk* btChunk_new();

	EXPORT btChunk* btSerializer_allocate(btSerializer* obj, size_t size, int numElements);
	EXPORT void btSerializer_finalizeChunk(btSerializer* obj, btChunk* chunk, char* structType, int chunkCode, void* oldPtr);
	EXPORT char* btSerializer_findNameForPointer(btSerializer* obj, void* ptr);
	EXPORT void* btSerializer_findPointer(btSerializer* obj, void* oldPtr);
	EXPORT void btSerializer_finishSerialization(btSerializer* obj);
	EXPORT unsigned char* btSerializer_getBufferPointer(btSerializer* obj);
	EXPORT int btSerializer_getCurrentBufferSize(btSerializer* obj);
	EXPORT int btSerializer_getSerializationFlags(btSerializer* obj);
	EXPORT void* btSerializer_getUniquePointer(btSerializer* obj, void* oldPtr);
	EXPORT void btSerializer_registerNameForPointer(btSerializer* obj, void* ptr, char* name);
	EXPORT void btSerializer_serializeName(btSerializer* obj, char* ptr);
	EXPORT void btSerializer_setSerializationFlags(btSerializer* obj, int flags);
	EXPORT void btSerializer_startSerialization(btSerializer* obj);

	EXPORT btPointerUid* btPointerUid_new();

	EXPORT btDefaultSerializer* btDefaultSerializer_new(int totalSize);
	EXPORT btDefaultSerializer* btDefaultSerializer_new2();
	EXPORT unsigned char* btDefaultSerializer_internalAlloc(btDefaultSerializer* obj, size_t size);
	EXPORT void btDefaultSerializer_writeHeader(btDefaultSerializer* obj, unsigned char* buffer);
}
