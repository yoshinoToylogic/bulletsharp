#include "main.h"

extern "C"
{
	EXPORT btPersistentManifold* btPersistentManifold_new();
	EXPORT btPersistentManifold* btPersistentManifold_new2(btCollisionObject* body0, btCollisionObject* body1, int __unnamed2, btScalar contactBreakingThreshold, btScalar contactProcessingThreshold);
	EXPORT int btPersistentManifold_addManifoldPoint(btPersistentManifold* obj, btManifoldPoint* newPoint, bool isPredictive);
	EXPORT int btPersistentManifold_addManifoldPoint2(btPersistentManifold* obj, btManifoldPoint* newPoint);
	EXPORT void btPersistentManifold_clearManifold(btPersistentManifold* obj);
	EXPORT void btPersistentManifold_clearUserCache(btPersistentManifold* obj, btManifoldPoint* pt);
	EXPORT const btCollisionObject* btPersistentManifold_getBody0(btPersistentManifold* obj);
	EXPORT const btCollisionObject* btPersistentManifold_getBody1(btPersistentManifold* obj);
	EXPORT int btPersistentManifold_getCacheEntry(btPersistentManifold* obj, btManifoldPoint* newPoint);
	EXPORT int btPersistentManifold_getCompanionIdA(btPersistentManifold* obj);
	EXPORT int btPersistentManifold_getCompanionIdB(btPersistentManifold* obj);
	EXPORT btScalar btPersistentManifold_getContactBreakingThreshold(btPersistentManifold* obj);
	EXPORT btManifoldPoint* btPersistentManifold_getContactPoint(btPersistentManifold* obj, int index);
	EXPORT btScalar btPersistentManifold_getContactProcessingThreshold(btPersistentManifold* obj);
	EXPORT int btPersistentManifold_getIndex1a(btPersistentManifold* obj);
	EXPORT int btPersistentManifold_getNumContacts(btPersistentManifold* obj);
	EXPORT void btPersistentManifold_refreshContactPoints(btPersistentManifold* obj, btScalar* trA, btScalar* trB);
	EXPORT void btPersistentManifold_removeContactPoint(btPersistentManifold* obj, int index);
	EXPORT void btPersistentManifold_replaceContactPoint(btPersistentManifold* obj, btManifoldPoint* newPoint, int insertIndex);
	EXPORT void btPersistentManifold_setBodies(btPersistentManifold* obj, btCollisionObject* body0, btCollisionObject* body1);
	EXPORT void btPersistentManifold_setCompanionIdA(btPersistentManifold* obj, int value);
	EXPORT void btPersistentManifold_setCompanionIdB(btPersistentManifold* obj, int value);
	EXPORT void btPersistentManifold_setContactBreakingThreshold(btPersistentManifold* obj, btScalar contactBreakingThreshold);
	EXPORT void btPersistentManifold_setContactProcessingThreshold(btPersistentManifold* obj, btScalar contactProcessingThreshold);
	EXPORT void btPersistentManifold_setIndex1a(btPersistentManifold* obj, int value);
	EXPORT void btPersistentManifold_setNumContacts(btPersistentManifold* obj, int cachedPoints);
	EXPORT bool btPersistentManifold_validContactDistance(btPersistentManifold* obj, btManifoldPoint* pt);

	EXPORT ContactDestroyedCallback getGContactDestroyedCallback();
	EXPORT ContactProcessedCallback getGContactProcessedCallback();
	EXPORT void setGContactDestroyedCallback(ContactDestroyedCallback value);
	EXPORT void setGContactProcessedCallback(ContactProcessedCallback value);
}
