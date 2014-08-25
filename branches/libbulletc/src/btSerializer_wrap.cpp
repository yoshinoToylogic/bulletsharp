#include <LinearMath/btSerializer.h>

#include "conversion.h"
#include "btSerializer_wrap.h"

btChunk* btChunk_new()
{
	return new btChunk();
}

int btChunk_getChunkCode(btChunk* obj)
{
	return obj->m_chunkCode;
}

int btChunk_getDna_nr(btChunk* obj)
{
	return obj->m_dna_nr;
}

int btChunk_getLength(btChunk* obj)
{
	return obj->m_length;
}

int btChunk_getNumber(btChunk* obj)
{
	return obj->m_number;
}

void* btChunk_getOldPtr(btChunk* obj)
{
	return obj->m_oldPtr;
}

void btChunk_setChunkCode(btChunk* obj, int value)
{
	obj->m_chunkCode = value;
}

void btChunk_setDna_nr(btChunk* obj, int value)
{
	obj->m_dna_nr = value;
}

void btChunk_setLength(btChunk* obj, int value)
{
	obj->m_length = value;
}

void btChunk_setNumber(btChunk* obj, int value)
{
	obj->m_number = value;
}

void btChunk_setOldPtr(btChunk* obj, void* value)
{
	obj->m_oldPtr = value;
}

void btChunk_delete(btChunk* obj)
{
	delete obj;
}


btSerializerWrapper::btSerializerWrapper(void* serializerGCHandle, pAllocate allocateCallback, pFinalizeChunk finalizeChunkCallback,
	pFindNameForPointer findNameForPointerCallback, pFindPointer findPointerCallback, pFinishSerialization finishSerializationCallback,
	pGetBufferPointer getBufferPointerCallback, pGetCurrentBufferSize getCurrentBufferSizeCallback,
	pGetSerializationFlags getSerializationFlagsCallback, pGetUniquePointer getUniquePointerCallback,
	pRegisterNameForPointer registerNameForPointerCallback, pSerializeName serializeNameCallback,
	pSetSerializationFlags setSerializationFlagsCallback, pStartSerialization startSerializationCallback)
{
	_serializerGCHandle = serializerGCHandle;
	_allocateCallback = allocateCallback;
	_finalizeChunkCallback = finalizeChunkCallback;
	_findNameForPointerCallback = findNameForPointerCallback;
	_findPointerCallback = findPointerCallback;
	_finishSerializationCallback = finishSerializationCallback;
	_getBufferPointerCallback = getBufferPointerCallback;
	_getCurrentBufferSizeCallback = getCurrentBufferSizeCallback;
	_getSerializationFlagsCallback = getSerializationFlagsCallback;
	_getUniquePointerCallback = getUniquePointerCallback;
	_registerNameForPointerCallback = registerNameForPointerCallback;
	_serializeNameCallback = serializeNameCallback;
	_setSerializationFlagsCallback = setSerializationFlagsCallback;
	_startSerializationCallback = startSerializationCallback;
}

btChunk* btSerializerWrapper::allocate(size_t size, int numElements)
{
	return _allocateCallback(size, numElements);
}

void btSerializerWrapper::finalizeChunk(btChunk* chunk, const char* structType, int chunkCode, void* oldPtr)
{
	_finalizeChunkCallback(chunk, structType, chunkCode, oldPtr);
}

const char* btSerializerWrapper::findNameForPointer(const void* ptr) const
{
	return _findNameForPointerCallback(ptr);
}

void* btSerializerWrapper::findPointer(void* oldPtr)
{
	return _findPointerCallback(oldPtr);
}

void btSerializerWrapper::finishSerialization()
{
	_finishSerializationCallback();
}

const unsigned char* btSerializerWrapper::getBufferPointer() const
{
	return _getBufferPointerCallback();
}

int btSerializerWrapper::getCurrentBufferSize() const
{
	return _getCurrentBufferSizeCallback();
}

int btSerializerWrapper::getSerializationFlags() const
{
	return _getSerializationFlagsCallback();
}

void* btSerializerWrapper::getUniquePointer(void*oldPtr)
{
	return _getUniquePointerCallback(oldPtr);
}

void btSerializerWrapper::registerNameForPointer(const void* ptr, const char* name)
{
	_registerNameForPointerCallback(ptr, name);
}

void btSerializerWrapper::serializeName(const char* ptr)
{
	_serializeNameCallback(ptr);
}

void btSerializerWrapper::setSerializationFlags(int flags)
{
	_setSerializationFlagsCallback(flags);
}

void btSerializerWrapper::startSerialization()
{
	_startSerializationCallback();
}


btSerializerWrapper* btSerializerWrapper_new(void* serializerGCHandle, pAllocate allocateCallback, pFinalizeChunk finalizeChunkCallback,
	pFindNameForPointer findNameForPointerCallback, pFindPointer findPointerCallback, pFinishSerialization finishSerializationCallback,
	pGetBufferPointer getBufferPointerCallback, pGetCurrentBufferSize getCurrentBufferSizeCallback,
	pGetSerializationFlags getSerializationFlagsCallback, pGetUniquePointer getUniquePointerCallback,
	pRegisterNameForPointer registerNameForPointerCallback, pSerializeName serializeNameCallback,
	pSetSerializationFlags setSerializationFlagsCallback, pStartSerialization startSerializationCallback)
{
	return new btSerializerWrapper(serializerGCHandle, allocateCallback, finalizeChunkCallback, findNameForPointerCallback,
		findPointerCallback, finishSerializationCallback, getBufferPointerCallback, getCurrentBufferSizeCallback,
		getSerializationFlagsCallback, getUniquePointerCallback, registerNameForPointerCallback, serializeNameCallback,
		setSerializationFlagsCallback, startSerializationCallback);
}

void* btSerializerWrapper_getSerializerGCHandle(btSerializerWrapper* obj)
{
	return obj->_serializerGCHandle;
}

void btSerializer_delete(btSerializer* obj)
{
	delete obj;
}

/*
btPointerUid* btPointerUid_new()
{
	return new btPointerUid();
}

void btPointerUid_delete(btPointerUid* obj)
{
	delete obj;
}
*/


btDefaultSerializer* btDefaultSerializer_new()
{
	return new btDefaultSerializer();
}

btDefaultSerializer* btDefaultSerializer_new2(int totalSize)
{
	return new btDefaultSerializer(totalSize);
}

unsigned char* btDefaultSerializer_internalAlloc(btDefaultSerializer* obj, size_t size)
{
	return obj->internalAlloc(size);
}

void btDefaultSerializer_writeHeader(btDefaultSerializer* obj, unsigned char* buffer)
{
	obj->writeHeader(buffer);
}
