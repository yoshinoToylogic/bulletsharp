#include "main.h"

#ifndef BT_SERIALIZER_H
#define pAllocate void*
#define pFinalizeChunk void*
#define pFindNameForPointer void*
#define pFindPointer void*
#define pFinishSerialization void*
#define pGetBufferPointer void*
#define pGetChunk void*
#define pGetCurrentBufferSize void*
#define pGetNumChunks void*
#define pGetSerializationFlags void*
#define pGetUniquePointer void*
#define pRegisterNameForPointer void*
#define pSerializeName void*
#define pSetSerializationFlags void*
#define pStartSerialization void*

#define btSerializerWrapper void
#else
typedef btChunk*(*pAllocate)(size_t size, int numElements);
typedef void(*pFinalizeChunk)(btChunk* chunk, const char* structType, int chunkCode, void* oldPtr);
typedef const char*(*pFindNameForPointer)(const void* ptr);
typedef void*(*pFindPointer)(void* oldPtr);
typedef void(*pFinishSerialization)();
typedef const unsigned char*(*pGetBufferPointer)();
typedef btChunk*(*pGetChunk)(int chunkIndex);
typedef int(*pGetCurrentBufferSize)();
typedef int(*pGetNumChunks)();
typedef int(*pGetSerializationFlags)();
typedef void*(*pGetUniquePointer)(void*oldPtr);
typedef void(*pRegisterNameForPointer)(const void* ptr, const char* name);
typedef void(*pSerializeName)(const char* ptr);
typedef void(*pSetSerializationFlags)(int flags);
typedef void(*pStartSerialization)();

class btSerializerWrapper : public btSerializer
{
private:
	pAllocate _allocateCallback;
	pFinalizeChunk _finalizeChunkCallback;
	pFindNameForPointer _findNameForPointerCallback;
	pFindPointer _findPointerCallback;
	pFinishSerialization _finishSerializationCallback;
	pGetBufferPointer _getBufferPointerCallback;
	pGetChunk _getChunkCallback;
	pGetCurrentBufferSize _getCurrentBufferSizeCallback;
	pGetNumChunks _getNumChunksCallback;
	pGetSerializationFlags _getSerializationFlagsCallback;
	pGetUniquePointer _getUniquePointerCallback;
	pRegisterNameForPointer _registerNameForPointerCallback;
	pSerializeName _serializeNameCallback;
	pSetSerializationFlags _setSerializationFlagsCallback;
	pStartSerialization _startSerializationCallback;

public:
	void* _serializerGCHandle;

	btSerializerWrapper(void* serializerGCHandle, pAllocate allocateCallback, pFinalizeChunk finalizeChunkCallback,
		pFindNameForPointer findNameForPointerCallback, pFindPointer findPointerCallback, pFinishSerialization finishSerializationCallback,
		pGetBufferPointer getBufferPointerCallback, pGetChunk getChunkCallback,
		pGetCurrentBufferSize getCurrentBufferSizeCallback, pGetNumChunks getNumChunksCallback,
		pGetSerializationFlags getSerializationFlagsCallback, pGetUniquePointer getUniquePointerCallback,
		pRegisterNameForPointer registerNameForPointerCallback, pSerializeName serializeNameCallback,
		pSetSerializationFlags setSerializationFlagsCallback, pStartSerialization startSerializationCallback);

	virtual btChunk* allocate(size_t size, int numElements);
	virtual void finalizeChunk(btChunk* chunk, const char* structType, int chunkCode, void* oldPtr);
	virtual const char* findNameForPointer(const void* ptr) const;
	virtual void* findPointer(void* oldPtr);
	virtual void finishSerialization();
	virtual const unsigned char* getBufferPointer() const;
	virtual btChunk* getChunk(int chunkIndex) const;
	virtual int getCurrentBufferSize() const;
	virtual int getNumChunks() const;
	virtual int getSerializationFlags() const;
	virtual void* getUniquePointer(void*oldPtr);
	virtual void registerNameForPointer(const void* ptr, const char* name);
	virtual void serializeName(const char* ptr);
	virtual void setSerializationFlags(int flags);
	virtual void startSerialization();
};
#endif

extern "C"
{
	EXPORT btChunk* btChunk_new();
	EXPORT int btChunk_getChunkCode(btChunk* obj);
	EXPORT int btChunk_getDna_nr(btChunk* obj);
	EXPORT int btChunk_getLength(btChunk* obj);
	EXPORT int btChunk_getNumber(btChunk* obj);
	EXPORT void* btChunk_getOldPtr(btChunk* obj);
	EXPORT void btChunk_setChunkCode(btChunk* obj, int value);
	EXPORT void btChunk_setDna_nr(btChunk* obj, int value);
	EXPORT void btChunk_setLength(btChunk* obj, int value);
	EXPORT void btChunk_setNumber(btChunk* obj, int value);
	EXPORT void btChunk_setOldPtr(btChunk* obj, void* value);
	EXPORT void btChunk_delete(btChunk* obj);

	EXPORT btSerializerWrapper* btSerializerWrapper_new(void* serializerGCHandle, pAllocate allocateCallback, pFinalizeChunk finalizeChunkCallback,
		pFindNameForPointer findNameForPointerCallback, pFindPointer findPointerCallback, pFinishSerialization finishSerializationCallback,
		pGetBufferPointer getBufferPointerCallback, pGetCurrentBufferSize getCurrentBufferSizeCallback,
		pGetSerializationFlags getSerializationFlagsCallback, pGetUniquePointer getUniquePointerCallback,
		pRegisterNameForPointer registerNameForPointerCallback, pSerializeName serializeNameCallback,
		pSetSerializationFlags setSerializationFlagsCallback, pStartSerialization startSerializationCallback);
	EXPORT void* btSerializerWrapper_getSerializerGCHandle(btSerializerWrapper* obj);
	EXPORT void btSerializer_delete(btSerializer* obj);
	/*
	EXPORT btPointerUid* btPointerUid_new();
	EXPORT void btPointerUid_delete(btPointerUid* obj);
	*/
	EXPORT btDefaultSerializer* btDefaultSerializer_new();
	EXPORT btDefaultSerializer* btDefaultSerializer_new2(int totalSize);
	EXPORT unsigned char* btDefaultSerializer_internalAlloc(btDefaultSerializer* obj, size_t size);
	EXPORT void btDefaultSerializer_writeHeader(btDefaultSerializer* obj, unsigned char* buffer);
}
