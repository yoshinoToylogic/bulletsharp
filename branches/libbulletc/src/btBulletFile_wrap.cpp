#include "btBulletFile_wrap.h"

bParse::btBulletFile* btBulletFile_new()
{
	return new bParse::btBulletFile();
}

bParse::btBulletFile* btBulletFile_new2(char* fileName)
{
	return new bParse::btBulletFile(fileName);
}

bParse::btBulletFile* btBulletFile_new3(char* memoryBuffer, int len)
{
	return new bParse::btBulletFile(memoryBuffer, len);
}

void btBulletFile_addStruct(bParse::btBulletFile* obj, char* structType, void* data, int len, void* oldPtr, int code)
{
	obj->addStruct(structType, data, len, oldPtr, code);
}

void btBulletFile_getBvhs(bParse::btBulletFile* obj)
{
	obj->m_bvhs;
}

void btBulletFile_getCollisionObjects(bParse::btBulletFile* obj)
{
	obj->m_collisionObjects;
}

void btBulletFile_getCollisionShapes(bParse::btBulletFile* obj)
{
	obj->m_collisionShapes;
}

void btBulletFile_getConstraints(bParse::btBulletFile* obj)
{
	obj->m_constraints;
}

void btBulletFile_getDataBlocks(bParse::btBulletFile* obj)
{
	obj->m_dataBlocks;
}

void btBulletFile_getDynamicsWorldInfo(bParse::btBulletFile* obj)
{
	obj->m_dynamicsWorldInfo;
}

void btBulletFile_getRigidBodies(bParse::btBulletFile* obj)
{
	obj->m_rigidBodies;
}

void btBulletFile_getSoftBodies(bParse::btBulletFile* obj)
{
	obj->m_softBodies;
}

void btBulletFile_getTriangleInfoMaps(bParse::btBulletFile* obj)
{
	obj->m_triangleInfoMaps;
}

void btBulletFile_parseData(bParse::btBulletFile* obj)
{
	obj->parseData();
}
/*
void btBulletFile_setBvhs(bParse::btBulletFile* obj, void value)
{
	obj->m_bvhs = value;
}

void btBulletFile_setCollisionObjects(bParse::btBulletFile* obj, void value)
{
	obj->m_collisionObjects = value;
}

void btBulletFile_setCollisionShapes(bParse::btBulletFile* obj, void value)
{
	obj->m_collisionShapes = value;
}

void btBulletFile_setConstraints(bParse::btBulletFile* obj, void value)
{
	obj->m_constraints = value;
}

void btBulletFile_setDataBlocks(bParse::btBulletFile* obj, void value)
{
	obj->m_dataBlocks = value;
}

void btBulletFile_setDynamicsWorldInfo(bParse::btBulletFile* obj, void value)
{
	obj->m_dynamicsWorldInfo = value;
}

void btBulletFile_setRigidBodies(bParse::btBulletFile* obj, void value)
{
	obj->m_rigidBodies = value;
}

void btBulletFile_setSoftBodies(bParse::btBulletFile* obj, void value)
{
	obj->m_softBodies = value;
}

void btBulletFile_setTriangleInfoMaps(bParse::btBulletFile* obj, void value)
{
	obj->m_triangleInfoMaps = value;
}
*/