#include <BulletDynamics/Featherstone/btMultiBodyLink.h>
#include <BulletDynamics/Featherstone/btMultiBodyLinkCollider.h>

#include "conversion.h"
#include "btMultiBodyLink_wrap.h"

btMultibodyLink* btMultibodyLink_new()
{
	return new btMultibodyLink();
}

void btMultibodyLink_getApplied_force(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->applied_force, value);
}

void btMultibodyLink_getApplied_torque(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->applied_torque, value);
}

void btMultibodyLink_getAxis_bottom(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->axis_bottom, value);
}

void btMultibodyLink_getAxis_top(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->axis_top, value);
}

void btMultibodyLink_getCached_r_vector(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->cached_r_vector, value);
}

void btMultibodyLink_getCached_rot_parent_to_this(btMultibodyLink* obj, btScalar* value)
{
	QUATERNION_OUT(&obj->cached_rot_parent_to_this, value);
}

btMultiBodyLinkCollider* btMultibodyLink_getCollider(btMultibodyLink* obj)
{
	return obj->m_collider;
}

void btMultibodyLink_getD_vector(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->d_vector, value);
}

void btMultibodyLink_getE_vector(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->e_vector, value);
}

int btMultibodyLink_getFlags(btMultibodyLink* obj)
{
	return obj->m_flags;
}

void btMultibodyLink_getInertia(btMultibodyLink* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->inertia, value);
}

bool btMultibodyLink_getIs_revolute(btMultibodyLink* obj)
{
	return obj->is_revolute;
}

btScalar btMultibodyLink_getJoint_pos(btMultibodyLink* obj)
{
	return obj->joint_pos;
}

btScalar btMultibodyLink_getJoint_torque(btMultibodyLink* obj)
{
	return obj->joint_torque;
}

btScalar btMultibodyLink_getMass(btMultibodyLink* obj)
{
	return obj->mass;
}

int btMultibodyLink_getParent(btMultibodyLink* obj)
{
	return obj->parent;
}

void btMultibodyLink_getZero_rot_parent_to_this(btMultibodyLink* obj, btScalar* value)
{
	QUATERNION_OUT(&obj->zero_rot_parent_to_this, value);
}

void btMultibodyLink_setApplied_force(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->applied_force);
}

void btMultibodyLink_setApplied_torque(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->applied_torque);
}

void btMultibodyLink_setAxis_bottom(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->axis_bottom);
}

void btMultibodyLink_setAxis_top(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->axis_top);
}

void btMultibodyLink_setCached_r_vector(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->cached_r_vector);
}

void btMultibodyLink_setCached_rot_parent_to_this(btMultibodyLink* obj, const btScalar* value)
{
	QUATERNION_IN(value, &obj->cached_rot_parent_to_this);
}

void btMultibodyLink_setCollider(btMultibodyLink* obj, btMultiBodyLinkCollider* value)
{
	obj->m_collider = value;
}

void btMultibodyLink_setD_vector(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->d_vector);
}

void btMultibodyLink_setE_vector(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->e_vector);
}

void btMultibodyLink_setFlags(btMultibodyLink* obj, int value)
{
	obj->m_flags = value;
}

void btMultibodyLink_setInertia(btMultibodyLink* obj, const btScalar* value)
{
	VECTOR3_IN(value, &obj->inertia);
}

void btMultibodyLink_setIs_revolute(btMultibodyLink* obj, bool value)
{
	obj->is_revolute = value;
}

void btMultibodyLink_setJoint_pos(btMultibodyLink* obj, btScalar value)
{
	obj->joint_pos = value;
}

void btMultibodyLink_setJoint_torque(btMultibodyLink* obj, btScalar value)
{
	obj->joint_torque = value;
}

void btMultibodyLink_setMass(btMultibodyLink* obj, btScalar value)
{
	obj->mass = value;
}

void btMultibodyLink_setParent(btMultibodyLink* obj, int value)
{
	obj->parent = value;
}

void btMultibodyLink_setZero_rot_parent_to_this(btMultibodyLink* obj, const btScalar* value)
{
	QUATERNION_IN(value, &obj->zero_rot_parent_to_this);
}

void btMultibodyLink_updateCache(btMultibodyLink* obj)
{
	obj->updateCache();
}

void btMultibodyLink_delete(btMultibodyLink* obj)
{
	delete obj;
}
