#include "main.h"

extern "C"
{
	EXPORT btMultibodyLink* btMultibodyLink_new();
	EXPORT btSpatialMotionVector* btMultibodyLink_getAbsFrameLocVelocity(btMultibodyLink* obj);
	EXPORT btSpatialMotionVector* btMultibodyLink_getAbsFrameTotVelocity(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_getAppliedForce(btMultibodyLink* obj, btScalar* value);
	EXPORT void btMultibodyLink_getAppliedTorque(btMultibodyLink* obj, btScalar* value);
	EXPORT btSpatialMotionVector* btMultibodyLink_getAxes(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_getAxisBottom(btMultibodyLink* obj, int dof, btScalar* value);
	EXPORT void btMultibodyLink_getAxisTop(btMultibodyLink* obj, int dof, btScalar* value);
	EXPORT void btMultibodyLink_getCachedRotParentToThis(btMultibodyLink* obj, btScalar* value);
	EXPORT void btMultibodyLink_getCachedRVector(btMultibodyLink* obj, btScalar* value);
	EXPORT int btMultibodyLink_getCfgOffset(btMultibodyLink* obj);
	EXPORT btMultiBodyLinkCollider* btMultibodyLink_getCollider(btMultibodyLink* obj);
	EXPORT int btMultibodyLink_getDofCount(btMultibodyLink* obj);
	EXPORT int btMultibodyLink_getDofOffset(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_getDVector(btMultibodyLink* obj, btScalar* value);
	EXPORT void btMultibodyLink_getEVector(btMultibodyLink* obj, btScalar* value);
	EXPORT int btMultibodyLink_getFlags(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_getInertiaLocal(btMultibodyLink* obj, btScalar* value);
	EXPORT btScalar* btMultibodyLink_getJointPos(btMultibodyLink* obj);
	EXPORT btScalar* btMultibodyLink_getJointTorque(btMultibodyLink* obj);
	//EXPORT eFeatherstoneJointType btMultibodyLink_getJointType(btMultibodyLink* obj);
	EXPORT btScalar btMultibodyLink_getMass(btMultibodyLink* obj);
	EXPORT int btMultibodyLink_getParent(btMultibodyLink* obj);
	EXPORT int btMultibodyLink_getPosVarCount(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_getZeroRotParentToThis(btMultibodyLink* obj, btScalar* value);
	//EXPORT void btMultibodyLink_setAbsFrameLocVelocity(btMultibodyLink* obj, const btSpatialMotionVector* value);
	//EXPORT void btMultibodyLink_setAbsFrameTotVelocity(btMultibodyLink* obj, const btSpatialMotionVector* value);
	EXPORT void btMultibodyLink_setAppliedForce(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setAppliedTorque(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setAxisBottom(btMultibodyLink* obj, int dof, const btScalar* x, const btScalar* y, const btScalar* z);
	EXPORT void btMultibodyLink_setAxisBottom2(btMultibodyLink* obj, int dof, const btScalar* axis);
	EXPORT void btMultibodyLink_setAxisTop(btMultibodyLink* obj, int dof, const btScalar* axis);
	EXPORT void btMultibodyLink_setAxisTop2(btMultibodyLink* obj, int dof, const btScalar* x, const btScalar* y, const btScalar* z);
	EXPORT void btMultibodyLink_setCachedRotParentToThis(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setCachedRVector(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setCfgOffset(btMultibodyLink* obj, int value);
	EXPORT void btMultibodyLink_setCollider(btMultibodyLink* obj, btMultiBodyLinkCollider* value);
	EXPORT void btMultibodyLink_setDofCount(btMultibodyLink* obj, int value);
	EXPORT void btMultibodyLink_setDofOffset(btMultibodyLink* obj, int value);
	EXPORT void btMultibodyLink_setDVector(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setEVector(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setFlags(btMultibodyLink* obj, int value);
	EXPORT void btMultibodyLink_setInertiaLocal(btMultibodyLink* obj, const btScalar* value);
	//EXPORT void btMultibodyLink_setJointType(btMultibodyLink* obj, eFeatherstoneJointType value);
	EXPORT void btMultibodyLink_setMass(btMultibodyLink* obj, btScalar value);
	EXPORT void btMultibodyLink_setParent(btMultibodyLink* obj, int value);
	EXPORT void btMultibodyLink_setPosVarCount(btMultibodyLink* obj, int value);
	EXPORT void btMultibodyLink_setZeroRotParentToThis(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_updateCache(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_updateCacheMultiDof(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_updateCacheMultiDof2(btMultibodyLink* obj, btScalar* pq);
	EXPORT void btMultibodyLink_delete(btMultibodyLink* obj);
}
