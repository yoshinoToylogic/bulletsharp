#include "btSerializer_wrap.h"

btChunk* btChunk_new()
{
	return new btChunk();
}

btChunk* btSerializer_allocate(btSerializer* obj, size_t size, int numElements)
{
	obj->allocate(size, numElements);
}

void btSerializer_finalizeChunk(btSerializer* obj, btChunk* chunk, char* structType, int chunkCode, void* oldPtr)
{
	obj->finalizeChunk(chunk, structType, chunkCode, oldPtr);
}

char* btSerializer_findNameForPointer(btSerializer* obj, void* ptr)
{
	obj->findNameForPointer(ptr);
}

void* btSerializer_findPointer(btSerializer* obj, void* oldPtr)
{
	obj->findPointer(oldPtr);
}

void btSerializer_finishSerialization(btSerializer* obj)
{
	obj->finishSerialization();
}

unsigned char* btSerializer_getBufferPointer(btSerializer* obj)
{
	obj->getBufferPointer();
}

int btSerializer_getCurrentBufferSize(btSerializer* obj)
{
	return obj->getCurrentBufferSize();
}

int btSerializer_getSerializationFlags(btSerializer* obj)
{
	return obj->getSerializationFlags();
}

void* btSerializer_getUniquePointer(btSerializer* obj, void* oldPtr)
{
	obj->getUniquePointer(oldPtr);
}

void btSerializer_registerNameForPointer(btSerializer* obj, void* ptr, char* name)
{
	obj->registerNameForPointer(ptr, name);
}

void btSerializer_serializeName(btSerializer* obj, char* ptr)
{
	obj->serializeName(ptr);
}

void btSerializer_setSerializationFlags(btSerializer* obj, int flags)
{
	obj->setSerializationFlags(flags);
}

void btSerializer_startSerialization(btSerializer* obj)
{
	obj->startSerialization();
}

btPointerUid* btPointerUid_new()
{
	return new btPointerUid();
}

btDefaultSerializer* btDefaultSerializer_new(int totalSize)
{
	return new btDefaultSerializer(totalSize);
}

btDefaultSerializer* btDefaultSerializer_new2()
{
	return new btDefaultSerializer();
}

unsigned char* btDefaultSerializer_internalAlloc(btDefaultSerializer* obj, size_t size)
{
	obj->internalAlloc(size);
}

void btDefaultSerializer_writeHeader(btDefaultSerializer* obj, unsigned char* buffer)
{
	obj->writeHeader(buffer);
}
