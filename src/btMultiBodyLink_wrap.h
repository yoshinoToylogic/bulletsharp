#include "main.h"

extern "C"
{
	EXPORT btMultibodyLink* btMultibodyLink_new();
	EXPORT void btMultibodyLink_getApplied_force(btMultibodyLink* obj, btScalar* value);
	EXPORT void btMultibodyLink_getApplied_torque(btMultibodyLink* obj, btScalar* value);
	EXPORT void btMultibodyLink_getAxis_bottom(btMultibodyLink* obj, btScalar* value);
	EXPORT void btMultibodyLink_getAxis_top(btMultibodyLink* obj, btScalar* value);
	EXPORT void btMultibodyLink_getCached_r_vector(btMultibodyLink* obj, btScalar* value);
	EXPORT void btMultibodyLink_getCached_rot_parent_to_this(btMultibodyLink* obj, btScalar* value);
	EXPORT btMultiBodyLinkCollider* btMultibodyLink_getCollider(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_getD_vector(btMultibodyLink* obj, btScalar* value);
	EXPORT void btMultibodyLink_getE_vector(btMultibodyLink* obj, btScalar* value);
	EXPORT int btMultibodyLink_getFlags(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_getInertia(btMultibodyLink* obj, btScalar* value);
	EXPORT bool btMultibodyLink_getIs_revolute(btMultibodyLink* obj);
	EXPORT btScalar btMultibodyLink_getJoint_pos(btMultibodyLink* obj);
	EXPORT btScalar btMultibodyLink_getJoint_torque(btMultibodyLink* obj);
	EXPORT btScalar btMultibodyLink_getMass(btMultibodyLink* obj);
	EXPORT int btMultibodyLink_getParent(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_getZero_rot_parent_to_this(btMultibodyLink* obj, btScalar* value);
	EXPORT void btMultibodyLink_setApplied_force(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setApplied_torque(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setAxis_bottom(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setAxis_top(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setCached_r_vector(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setCached_rot_parent_to_this(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setCollider(btMultibodyLink* obj, btMultiBodyLinkCollider* value);
	EXPORT void btMultibodyLink_setD_vector(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setE_vector(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setFlags(btMultibodyLink* obj, int value);
	EXPORT void btMultibodyLink_setInertia(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_setIs_revolute(btMultibodyLink* obj, bool value);
	EXPORT void btMultibodyLink_setJoint_pos(btMultibodyLink* obj, btScalar value);
	EXPORT void btMultibodyLink_setJoint_torque(btMultibodyLink* obj, btScalar value);
	EXPORT void btMultibodyLink_setMass(btMultibodyLink* obj, btScalar value);
	EXPORT void btMultibodyLink_setParent(btMultibodyLink* obj, int value);
	EXPORT void btMultibodyLink_setZero_rot_parent_to_this(btMultibodyLink* obj, const btScalar* value);
	EXPORT void btMultibodyLink_updateCache(btMultibodyLink* obj);
	EXPORT void btMultibodyLink_delete(btMultibodyLink* obj);
}
