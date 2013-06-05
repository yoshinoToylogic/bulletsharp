#include <../Extras/Serialize/BulletFileLoader/btBulletFile.h>

#include "btBulletFile_wrap.h"

bParse_btBulletFile* btBulletFile_new()
{
	return new bParse_btBulletFile();
}

bParse_btBulletFile* btBulletFile_new2(char* fileName)
{
	return new bParse_btBulletFile(fileName);
}

bParse_btBulletFile* btBulletFile_new3(char* memoryBuffer, int len)
{
	return new bParse_btBulletFile(memoryBuffer, len);
}

void btBulletFile_addStruct(bParse_btBulletFile* obj, char* structType, void* data, int len, void* oldPtr, int code)
{
	obj->addStruct(structType, data, len, oldPtr, code);
}

void btBulletFile_getBvhs(bParse_btBulletFile* obj)
{
	obj->m_bvhs;
}

void btBulletFile_getCollisionObjects(bParse_btBulletFile* obj)
{
	obj->m_collisionObjects;
}

void btBulletFile_getCollisionShapes(bParse_btBulletFile* obj)
{
	obj->m_collisionShapes;
}

void btBulletFile_getConstraints(bParse_btBulletFile* obj)
{
	obj->m_constraints;
}

void btBulletFile_getDataBlocks(bParse_btBulletFile* obj)
{
	obj->m_dataBlocks;
}

void btBulletFile_getDynamicsWorldInfo(bParse_btBulletFile* obj)
{
	obj->m_dynamicsWorldInfo;
}

void btBulletFile_getRigidBodies(bParse_btBulletFile* obj)
{
	obj->m_rigidBodies;
}

void btBulletFile_getSoftBodies(bParse_btBulletFile* obj)
{
	obj->m_softBodies;
}

void btBulletFile_getTriangleInfoMaps(bParse_btBulletFile* obj)
{
	obj->m_triangleInfoMaps;
}

void btBulletFile_parseData(bParse_btBulletFile* obj)
{
	obj->parseData();
}
/*
void btBulletFile_setBvhs(bParse_btBulletFile* obj, void value)
{
	obj->m_bvhs = value;
}

void btBulletFile_setCollisionObjects(bParse_btBulletFile* obj, void value)
{
	obj->m_collisionObjects = value;
}

void btBulletFile_setCollisionShapes(bParse_btBulletFile* obj, void value)
{
	obj->m_collisionShapes = value;
}

void btBulletFile_setConstraints(bParse_btBulletFile* obj, void value)
{
	obj->m_constraints = value;
}

void btBulletFile_setDataBlocks(bParse_btBulletFile* obj, void value)
{
	obj->m_dataBlocks = value;
}

void btBulletFile_setDynamicsWorldInfo(bParse_btBulletFile* obj, void value)
{
	obj->m_dynamicsWorldInfo = value;
}

void btBulletFile_setRigidBodies(bParse_btBulletFile* obj, void value)
{
	obj->m_rigidBodies = value;
}

void btBulletFile_setSoftBodies(bParse_btBulletFile* obj, void value)
{
	obj->m_softBodies = value;
}

void btBulletFile_setTriangleInfoMaps(bParse_btBulletFile* obj, void value)
{
	obj->m_triangleInfoMaps = value;
}
*/