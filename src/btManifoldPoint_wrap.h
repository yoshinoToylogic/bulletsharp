#include "main.h"

extern "C"
{
	EXPORT btConstraintRow* btConstraintRow_new();
	EXPORT btScalar btConstraintRow_getAccumImpulse(btConstraintRow* obj);
	EXPORT btScalar btConstraintRow_getJacDiagInv(btConstraintRow* obj);
	EXPORT btScalar btConstraintRow_getLowerLimit(btConstraintRow* obj);
	EXPORT void btConstraintRow_getNormal(btConstraintRow* obj, btScalar* value);
	EXPORT btScalar btConstraintRow_getRhs(btConstraintRow* obj);
	EXPORT btScalar btConstraintRow_getUpperLimit(btConstraintRow* obj);
	EXPORT void btConstraintRow_setAccumImpulse(btConstraintRow* obj, btScalar value);
	EXPORT void btConstraintRow_setJacDiagInv(btConstraintRow* obj, btScalar value);
	EXPORT void btConstraintRow_setLowerLimit(btConstraintRow* obj, btScalar value);
	EXPORT void btConstraintRow_setNormal(btConstraintRow* obj, btScalar* value);
	EXPORT void btConstraintRow_setRhs(btConstraintRow* obj, btScalar value);
	EXPORT void btConstraintRow_setUpperLimit(btConstraintRow* obj, btScalar value);
	EXPORT void btConstraintRow_delete(btConstraintRow* obj);

	EXPORT btManifoldPoint* btManifoldPoint_new();
	EXPORT btManifoldPoint* btManifoldPoint_new2(const btScalar* pointA, const btScalar* pointB, const btScalar* normal, btScalar distance);
	EXPORT btScalar btManifoldPoint_getAppliedImpulse(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getAppliedImpulseLateral1(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getAppliedImpulseLateral2(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getCombinedFriction(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getCombinedRestitution(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getCombinedRollingFriction(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getContactCFM1(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getContactCFM2(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getContactMotion1(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getContactMotion2(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getDistance(btManifoldPoint* obj);
	EXPORT btScalar btManifoldPoint_getDistance1(btManifoldPoint* obj);
	EXPORT int btManifoldPoint_getIndex0(btManifoldPoint* obj);
	EXPORT int btManifoldPoint_getIndex1(btManifoldPoint* obj);
	EXPORT void btManifoldPoint_getLateralFrictionDir1(btManifoldPoint* obj, btScalar* value);
	EXPORT void btManifoldPoint_getLateralFrictionDir2(btManifoldPoint* obj, btScalar* value);
	EXPORT bool btManifoldPoint_getLateralFrictionInitialized(btManifoldPoint* obj);
	EXPORT int btManifoldPoint_getLifeTime(btManifoldPoint* obj);
	EXPORT void btManifoldPoint_getLocalPointA(btManifoldPoint* obj, btScalar* value);
	EXPORT void btManifoldPoint_getLocalPointB(btManifoldPoint* obj, btScalar* value);
	EXPORT void btManifoldPoint_getNormalWorldOnB(btManifoldPoint* obj, btScalar* value);
	EXPORT int btManifoldPoint_getPartId0(btManifoldPoint* obj);
	EXPORT int btManifoldPoint_getPartId1(btManifoldPoint* obj);
	EXPORT void btManifoldPoint_getPositionWorldOnA(btManifoldPoint* obj, btScalar* value);
	EXPORT void btManifoldPoint_getPositionWorldOnB(btManifoldPoint* obj, btScalar* value);
	EXPORT void* btManifoldPoint_getUserPersistentData(btManifoldPoint* obj);
	EXPORT void btManifoldPoint_setAppliedImpulse(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setAppliedImpulseLateral1(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setAppliedImpulseLateral2(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setCombinedFriction(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setCombinedRestitution(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setCombinedRollingFriction(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setContactCFM1(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setContactCFM2(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setContactMotion1(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setContactMotion2(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setDistance(btManifoldPoint* obj, btScalar dist);
	EXPORT void btManifoldPoint_setDistance1(btManifoldPoint* obj, btScalar value);
	EXPORT void btManifoldPoint_setIndex0(btManifoldPoint* obj, int value);
	EXPORT void btManifoldPoint_setIndex1(btManifoldPoint* obj, int value);
	EXPORT void btManifoldPoint_setLateralFrictionDir1(btManifoldPoint* obj, const btScalar* value);
	EXPORT void btManifoldPoint_setLateralFrictionDir2(btManifoldPoint* obj, const btScalar* value);
	EXPORT void btManifoldPoint_setLateralFrictionInitialized(btManifoldPoint* obj, bool value);
	EXPORT void btManifoldPoint_setLifeTime(btManifoldPoint* obj, int value);
	EXPORT void btManifoldPoint_setLocalPointA(btManifoldPoint* obj, const btScalar* value);
	EXPORT void btManifoldPoint_setLocalPointB(btManifoldPoint* obj, const btScalar* value);
	EXPORT void btManifoldPoint_setNormalWorldOnB(btManifoldPoint* obj, const btScalar* value);
	EXPORT void btManifoldPoint_setPartId0(btManifoldPoint* obj, int value);
	EXPORT void btManifoldPoint_setPartId1(btManifoldPoint* obj, int value);
	EXPORT void btManifoldPoint_setPositionWorldOnA(btManifoldPoint* obj, const btScalar* value);
	EXPORT void btManifoldPoint_setPositionWorldOnB(btManifoldPoint* obj, const btScalar* value);
	EXPORT void btManifoldPoint_setUserPersistentData(btManifoldPoint* obj, void* value);
	EXPORT void btManifoldPoint_delete(btManifoldPoint* obj);

	EXPORT ContactAddedCallback getGContactAddedCallback();
	EXPORT void setGContactAddedCallback(ContactAddedCallback value);
}
