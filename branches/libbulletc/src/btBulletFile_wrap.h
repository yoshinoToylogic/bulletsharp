#include "main.h"

extern "C"
{
	EXPORT bParse_btBulletFile* btBulletFile_new();
	EXPORT bParse_btBulletFile* btBulletFile_new2(char* fileName);
	EXPORT bParse_btBulletFile* btBulletFile_new3(char* memoryBuffer, int len);
	EXPORT void btBulletFile_addStruct(bParse_btBulletFile* obj, char* structType, void* data, int len, void* oldPtr, int code);
	EXPORT void btBulletFile_getBvhs(bParse_btBulletFile* obj);
	EXPORT void btBulletFile_getCollisionObjects(bParse_btBulletFile* obj);
	EXPORT void btBulletFile_getCollisionShapes(bParse_btBulletFile* obj);
	EXPORT void btBulletFile_getConstraints(bParse_btBulletFile* obj);
	EXPORT void btBulletFile_getDataBlocks(bParse_btBulletFile* obj);
	EXPORT void btBulletFile_getDynamicsWorldInfo(bParse_btBulletFile* obj);
	EXPORT void btBulletFile_getRigidBodies(bParse_btBulletFile* obj);
	EXPORT void btBulletFile_getSoftBodies(bParse_btBulletFile* obj);
	EXPORT void btBulletFile_getTriangleInfoMaps(bParse_btBulletFile* obj);
	EXPORT void btBulletFile_parseData(bParse_btBulletFile* obj);
	/*EXPORT void btBulletFile_setBvhs(bParse_btBulletFile* obj, void value);
	EXPORT void btBulletFile_setCollisionObjects(bParse_btBulletFile* obj, void value);
	EXPORT void btBulletFile_setCollisionShapes(bParse_btBulletFile* obj, void value);
	EXPORT void btBulletFile_setConstraints(bParse_btBulletFile* obj, void value);
	EXPORT void btBulletFile_setDataBlocks(bParse_btBulletFile* obj, void value);
	EXPORT void btBulletFile_setDynamicsWorldInfo(bParse_btBulletFile* obj, void value);
	EXPORT void btBulletFile_setRigidBodies(bParse_btBulletFile* obj, void value);
	EXPORT void btBulletFile_setSoftBodies(bParse_btBulletFile* obj, void value);
	EXPORT void btBulletFile_setTriangleInfoMaps(bParse_btBulletFile* obj, void value);*/
}
