#include <../Extras/Serialize/BulletXmlWorldImporter/btBulletXmlWorldImporter.h>

#include "conversion.h"
#include "btBulletXmlWorldImporter_wrap.h"

btBulletXmlWorldImporter* btBulletXmlWorldImporter_new(btDynamicsWorld* world)
{
	return new btBulletXmlWorldImporter(world);
}

bool btBulletXmlWorldImporter_loadFile(btBulletXmlWorldImporter* obj, const char* fileName)
{
	return obj->loadFile(fileName);
}
