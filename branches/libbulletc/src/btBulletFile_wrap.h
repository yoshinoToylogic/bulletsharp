#include "main.h"

#include <../Extras/Serialize/BulletFileLoader/btBulletFile.h>

extern "C"
{
	EXPORT bParse::btBulletFile* btBulletFile_new();
	EXPORT bParse::btBulletFile* btBulletFile_new2(char* fileName);
	EXPORT bParse::btBulletFile* btBulletFile_new3(char* memoryBuffer, int len);
	EXPORT void btBulletFile_addStruct(bParse::btBulletFile* obj, char* structType, void* data, int len, void* oldPtr, int code);
	EXPORT void btBulletFile_getBvhs(bParse::btBulletFile* obj);
	EXPORT void btBulletFile_getCollisionObjects(bParse::btBulletFile* obj);
	EXPORT void btBulletFile_getCollisionShapes(bParse::btBulletFile* obj);
	EXPORT void btBulletFile_getConstraints(bParse::btBulletFile* obj);
	EXPORT void btBulletFile_getDataBlocks(bParse::btBulletFile* obj);
	EXPORT void btBulletFile_getDynamicsWorldInfo(bParse::btBulletFile* obj);
	EXPORT void btBulletFile_getRigidBodies(bParse::btBulletFile* obj);
	EXPORT void btBulletFile_getSoftBodies(bParse::btBulletFile* obj);
	EXPORT void btBulletFile_getTriangleInfoMaps(bParse::btBulletFile* obj);
	EXPORT void btBulletFile_parseData(bParse::btBulletFile* obj);
	/*EXPORT void btBulletFile_setBvhs(bParse::btBulletFile* obj, void value);
	EXPORT void btBulletFile_setCollisionObjects(bParse::btBulletFile* obj, void value);
	EXPORT void btBulletFile_setCollisionShapes(bParse::btBulletFile* obj, void value);
	EXPORT void btBulletFile_setConstraints(bParse::btBulletFile* obj, void value);
	EXPORT void btBulletFile_setDataBlocks(bParse::btBulletFile* obj, void value);
	EXPORT void btBulletFile_setDynamicsWorldInfo(bParse::btBulletFile* obj, void value);
	EXPORT void btBulletFile_setRigidBodies(bParse::btBulletFile* obj, void value);
	EXPORT void btBulletFile_setSoftBodies(bParse::btBulletFile* obj, void value);
	EXPORT void btBulletFile_setTriangleInfoMaps(bParse::btBulletFile* obj, void value);*/
}
