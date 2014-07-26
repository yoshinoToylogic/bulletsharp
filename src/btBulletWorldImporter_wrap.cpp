#include <../Extras/Serialize/BulletWorldImporter/btBulletWorldImporter.h>
#include <../Extras/Serialize/BulletFileLoader/btBulletFile.h>

#include "conversion.h"
#include "btBulletWorldImporter_wrap.h"

btBulletWorldImporter* btBulletWorldImporter_new()
{
	return new btBulletWorldImporter();
}

btBulletWorldImporter* btBulletWorldImporter_new2(btDynamicsWorld* world)
{
	return new btBulletWorldImporter(world);
}

bool btBulletWorldImporter_convertAllObjects(btBulletWorldImporter* obj, bParse_btBulletFile* file)
{
	return obj->convertAllObjects(file);
}

bool btBulletWorldImporter_loadFile(btBulletWorldImporter* obj, const char* fileName)
{
	return obj->loadFile(fileName);
}

bool btBulletWorldImporter_loadFile2(btBulletWorldImporter* obj, const char* fileName, const char* preSwapFilenameOut)
{
	return obj->loadFile(fileName, preSwapFilenameOut);
}

bool btBulletWorldImporter_loadFileFromMemory(btBulletWorldImporter* obj, char* memoryBuffer, int len)
{
	return obj->loadFileFromMemory(memoryBuffer, len);
}

bool btBulletWorldImporter_loadFileFromMemory2(btBulletWorldImporter* obj, bParse_btBulletFile* file)
{
	return obj->loadFileFromMemory(file);
}
