#include "main.h"

extern "C"
{
	EXPORT btMultiBodyLinkCollider* btMultiBodyLinkCollider_new(btMultiBody* multiBody, int link);
	EXPORT bool btMultiBodyLinkCollider_checkCollideWithOverride(btMultiBodyLinkCollider* obj, const btCollisionObject* co);
	EXPORT int btMultiBodyLinkCollider_getLink(btMultiBodyLinkCollider* obj);
	EXPORT btMultiBody* btMultiBodyLinkCollider_getMultiBody(btMultiBodyLinkCollider* obj);
	EXPORT void btMultiBodyLinkCollider_setLink(btMultiBodyLinkCollider* obj, int value);
	EXPORT void btMultiBodyLinkCollider_setMultiBody(btMultiBodyLinkCollider* obj, btMultiBody* value);
	EXPORT btMultiBodyLinkCollider* btMultiBodyLinkCollider_upcast(btCollisionObject* colObj);
}
