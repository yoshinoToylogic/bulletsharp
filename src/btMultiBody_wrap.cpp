#include <BulletDynamics/Featherstone/btMultiBody.h>
#include <BulletDynamics/Featherstone/btMultiBodyLinkCollider.h>

#include "conversion.h"
#include "btMultiBody_wrap.h"

btMultiBody* btMultiBody_new(int n_links, btScalar mass, const btScalar* inertia, bool fixed_base_, bool can_sleep_)
{
	VECTOR3_CONV(inertia);
	return new btMultiBody(n_links, mass, VECTOR3_USE(inertia), fixed_base_, can_sleep_);
}

void btMultiBody_addBaseForce(btMultiBody* obj, const btScalar* f)
{
	VECTOR3_CONV(f);
	obj->addBaseForce(VECTOR3_USE(f));
}

void btMultiBody_addBaseTorque(btMultiBody* obj, const btScalar* t)
{
	VECTOR3_CONV(t);
	obj->addBaseTorque(VECTOR3_USE(t));
}

void btMultiBody_addJointTorque(btMultiBody* obj, int i, btScalar Q)
{
	obj->addJointTorque(i, Q);
}

void btMultiBody_addLinkForce(btMultiBody* obj, int i, const btScalar* f)
{
	VECTOR3_CONV(f);
	obj->addLinkForce(i, VECTOR3_USE(f));
}

void btMultiBody_addLinkTorque(btMultiBody* obj, int i, const btScalar* t)
{
	VECTOR3_CONV(t);
	obj->addLinkTorque(i, VECTOR3_USE(t));
}

void btMultiBody_applyDeltaVee(btMultiBody* obj, const btScalar* delta_vee, btScalar multiplier)
{
	obj->applyDeltaVee(delta_vee, multiplier);
}

void btMultiBody_applyDeltaVee2(btMultiBody* obj, const btScalar* delta_vee)
{
	obj->applyDeltaVee(delta_vee);
}

void btMultiBody_calcAccelerationDeltas(btMultiBody* obj, const btScalar* force, btScalar* output, btAlignedScalarArray* scratch_r, btAlignedVector3Array* scratch_v)
{
	obj->calcAccelerationDeltas(force, output, *scratch_r, *scratch_v);
}

void btMultiBody_checkMotionAndSleepIfRequired(btMultiBody* obj, btScalar timestep)
{
	obj->checkMotionAndSleepIfRequired(timestep);
}

void btMultiBody_clearForcesAndTorques(btMultiBody* obj)
{
	obj->clearForcesAndTorques();
}

void btMultiBody_clearVelocities(btMultiBody* obj)
{
	obj->clearVelocities();
}

void btMultiBody_fillContactJacobian(btMultiBody* obj, int link, const btScalar* contact_point, const btScalar* normal, btScalar* jac, btAlignedScalarArray* scratch_r, btAlignedVector3Array* scratch_v, btAlignedMatrix3x3Array* scratch_m)
{
	VECTOR3_CONV(contact_point);
	VECTOR3_CONV(normal);
	obj->fillContactJacobian(link, VECTOR3_USE(contact_point), VECTOR3_USE(normal), jac, *scratch_r, *scratch_v, *scratch_m);
}

btScalar btMultiBody_getAngularDamping(btMultiBody* obj)
{
	return obj->getAngularDamping();
}

void btMultiBody_getAngularMomentum(btMultiBody* obj, btScalar* value)
{
	VECTOR3_OUT_VAL(obj->getAngularMomentum(), value);
}

btMultiBodyLinkCollider* btMultiBody_getBaseCollider(btMultiBody* obj)
{
	return obj->getBaseCollider();
}

void btMultiBody_getBaseForce(btMultiBody* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getBaseForce(), value);
}

void btMultiBody_getBaseInertia(btMultiBody* obj, btScalar* inertia)
{
	VECTOR3_OUT(&obj->getBaseInertia(), inertia);
}

btScalar btMultiBody_getBaseMass(btMultiBody* obj)
{
	return obj->getBaseMass();
}

void btMultiBody_getBaseOmega(btMultiBody* obj, btScalar* omega)
{
	VECTOR3_OUT_VAL(obj->getBaseOmega(), omega);
}

void btMultiBody_getBasePos(btMultiBody* obj, btScalar* pos)
{
	VECTOR3_OUT(&obj->getBasePos(), pos);
}

void btMultiBody_getBaseTorque(btMultiBody* obj, btScalar* value)
{
	VECTOR3_OUT(&obj->getBaseTorque(), value);
}

void btMultiBody_getBaseVel(btMultiBody* obj, btScalar* vel)
{
	VECTOR3_OUT_VAL(obj->getBaseVel(), vel);
}

int btMultiBody_getCompanionId(btMultiBody* obj)
{
	return obj->getCompanionId();
}

btScalar btMultiBody_getJointPos(btMultiBody* obj, int i)
{
	return obj->getJointPos(i);
}

btScalar btMultiBody_getJointTorque(btMultiBody* obj, int i)
{
	return obj->getJointTorque(i);
}

btScalar btMultiBody_getJointVel(btMultiBody* obj, int i)
{
	return obj->getJointVel(i);
}

btScalar btMultiBody_getKineticEnergy(btMultiBody* obj)
{
	return obj->getKineticEnergy();
}

btScalar btMultiBody_getLinearDamping(btMultiBody* obj)
{
	return obj->getLinearDamping();
}

btMultibodyLink* btMultiBody_getLink(btMultiBody* obj, int index)
{
	return &obj->getLink(index);
}

void btMultiBody_getLinkForce(btMultiBody* obj, int i, btScalar* value)
{
	VECTOR3_OUT(&obj->getLinkForce(i), value);
}

void btMultiBody_getLinkInertia(btMultiBody* obj, int i, btScalar* value)
{
	VECTOR3_OUT(&obj->getLinkInertia(i), value);
}

btScalar btMultiBody_getLinkMass(btMultiBody* obj, int i)
{
	return obj->getLinkMass(i);
}

void btMultiBody_getLinkTorque(btMultiBody* obj, int i, btScalar* value)
{
	VECTOR3_OUT(&obj->getLinkTorque(i), value);
}

btScalar btMultiBody_getMaxAppliedImpulse(btMultiBody* obj)
{
	return obj->getMaxAppliedImpulse();
}

int btMultiBody_getNumLinks(btMultiBody* obj)
{
	return obj->getNumLinks();
}

int btMultiBody_getParent(btMultiBody* obj, int link_num)
{
	return obj->getParent(link_num);
}

void btMultiBody_getParentToLocalRot(btMultiBody* obj, int i, btScalar* value)
{
	QUATERNION_OUT(&obj->getParentToLocalRot(i), value);
}

void btMultiBody_getRVector(btMultiBody* obj, int i, btScalar* value)
{
	VECTOR3_OUT(&obj->getRVector(i), value);
}

bool btMultiBody_getUseGyroTerm(btMultiBody* obj)
{
	return obj->getUseGyroTerm();
}

const btScalar* btMultiBody_getVelocityVector(btMultiBody* obj)
{
	return obj->getVelocityVector();
}

void btMultiBody_getWorldToBaseRot(btMultiBody* obj, btScalar* rot)
{
	QUATERNION_OUT(&obj->getWorldToBaseRot(), rot);
}

void btMultiBody_goToSleep(btMultiBody* obj)
{
	obj->goToSleep();
}

bool btMultiBody_hasFixedBase(btMultiBody* obj)
{
	return obj->hasFixedBase();
}

bool btMultiBody_hasSelfCollision(btMultiBody* obj)
{
	return obj->hasSelfCollision();
}

bool btMultiBody_isAwake(btMultiBody* obj)
{
	return obj->isAwake();
}

void btMultiBody_localDirToWorld(btMultiBody* obj, int i, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->localDirToWorld(i, VECTOR3_USE(vec)), value);
}

void btMultiBody_localPosToWorld(btMultiBody* obj, int i, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->localPosToWorld(i, VECTOR3_USE(vec)), value);
}

void btMultiBody_setBaseCollider(btMultiBody* obj, btMultiBodyLinkCollider* collider)
{
	obj->setBaseCollider(collider);
}

void btMultiBody_setBaseInertia(btMultiBody* obj, const btScalar* inertia)
{
	VECTOR3_CONV(inertia);
	obj->setBaseInertia(VECTOR3_USE(inertia));
}

void btMultiBody_setBaseMass(btMultiBody* obj, btScalar mass)
{
	obj->setBaseMass(mass);
}

void btMultiBody_setBaseOmega(btMultiBody* obj, const btScalar* omega)
{
	VECTOR3_CONV(omega);
	obj->setBaseOmega(VECTOR3_USE(omega));
}

void btMultiBody_setBasePos(btMultiBody* obj, const btScalar* pos)
{
	VECTOR3_CONV(pos);
	obj->setBasePos(VECTOR3_USE(pos));
}

void btMultiBody_setBaseVel(btMultiBody* obj, const btScalar* vel)
{
	VECTOR3_CONV(vel);
	obj->setBaseVel(VECTOR3_USE(vel));
}

void btMultiBody_setCanSleep(btMultiBody* obj, bool canSleep)
{
	obj->setCanSleep(canSleep);
}

void btMultiBody_setCompanionId(btMultiBody* obj, int id)
{
	obj->setCompanionId(id);
}

void btMultiBody_setHasSelfCollision(btMultiBody* obj, bool hasSelfCollision)
{
	obj->setHasSelfCollision(hasSelfCollision);
}

void btMultiBody_setJointPos(btMultiBody* obj, int i, btScalar q)
{
	obj->setJointPos(i, q);
}

void btMultiBody_setJointVel(btMultiBody* obj, int i, btScalar qdot)
{
	obj->setJointVel(i, qdot);
}

void btMultiBody_setLinearDamping(btMultiBody* obj, btScalar damp)
{
	obj->setLinearDamping(damp);
}

void btMultiBody_setMaxAppliedImpulse(btMultiBody* obj, btScalar maxImp)
{
	obj->setMaxAppliedImpulse(maxImp);
}

void btMultiBody_setNumLinks(btMultiBody* obj, int numLinks)
{
	obj->setNumLinks(numLinks);
}

void btMultiBody_setupPrismatic(btMultiBody* obj, int i, btScalar mass, const btScalar* inertia, int parent, const btScalar* rot_parent_to_this, const btScalar* joint_axis, const btScalar* r_vector_when_q_zero)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(rot_parent_to_this);
	VECTOR3_CONV(joint_axis);
	VECTOR3_CONV(r_vector_when_q_zero);
	obj->setupPrismatic(i, mass, VECTOR3_USE(inertia), parent, QUATERNION_USE(rot_parent_to_this), VECTOR3_USE(joint_axis), VECTOR3_USE(r_vector_when_q_zero));
}

void btMultiBody_setupPrismatic2(btMultiBody* obj, int i, btScalar mass, const btScalar* inertia, int parent, const btScalar* rot_parent_to_this, const btScalar* joint_axis, const btScalar* r_vector_when_q_zero, bool disableParentCollision)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(rot_parent_to_this);
	VECTOR3_CONV(joint_axis);
	VECTOR3_CONV(r_vector_when_q_zero);
	obj->setupPrismatic(i, mass, VECTOR3_USE(inertia), parent, QUATERNION_USE(rot_parent_to_this), VECTOR3_USE(joint_axis), VECTOR3_USE(r_vector_when_q_zero), disableParentCollision);
}

void btMultiBody_setupRevolute(btMultiBody* obj, int i, btScalar mass, const btScalar* inertia, int parent, const btScalar* zero_rot_parent_to_this, const btScalar* joint_axis, const btScalar* parent_axis_position, const btScalar* my_axis_position)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(zero_rot_parent_to_this);
	VECTOR3_CONV(joint_axis);
	VECTOR3_CONV(parent_axis_position);
	VECTOR3_CONV(my_axis_position);
	obj->setupRevolute(i, mass, VECTOR3_USE(inertia), parent, QUATERNION_USE(zero_rot_parent_to_this), VECTOR3_USE(joint_axis), VECTOR3_USE(parent_axis_position), VECTOR3_USE(my_axis_position));
}

void btMultiBody_setupRevolute2(btMultiBody* obj, int i, btScalar mass, const btScalar* inertia, int parent, const btScalar* zero_rot_parent_to_this, const btScalar* joint_axis, const btScalar* parent_axis_position, const btScalar* my_axis_position, bool disableParentCollision)
{
	VECTOR3_CONV(inertia);
	QUATERNION_CONV(zero_rot_parent_to_this);
	VECTOR3_CONV(joint_axis);
	VECTOR3_CONV(parent_axis_position);
	VECTOR3_CONV(my_axis_position);
	obj->setupRevolute(i, mass, VECTOR3_USE(inertia), parent, QUATERNION_USE(zero_rot_parent_to_this), VECTOR3_USE(joint_axis), VECTOR3_USE(parent_axis_position), VECTOR3_USE(my_axis_position), disableParentCollision);
}

void btMultiBody_setUseGyroTerm(btMultiBody* obj, bool useGyro)
{
	obj->setUseGyroTerm(useGyro);
}

void btMultiBody_setWorldToBaseRot(btMultiBody* obj, const btScalar* rot)
{
	QUATERNION_CONV(rot);
	obj->setWorldToBaseRot(QUATERNION_USE(rot));
}

void btMultiBody_stepPositions(btMultiBody* obj, btScalar dt)
{
	obj->stepPositions(dt);
}

void btMultiBody_stepVelocities(btMultiBody* obj, btScalar dt, btAlignedScalarArray* scratch_r, btAlignedVector3Array* scratch_v, btAlignedMatrix3x3Array* scratch_m)
{
	obj->stepVelocities(dt, *scratch_r, *scratch_v, *scratch_m);
}

void btMultiBody_wakeUp(btMultiBody* obj)
{
	obj->wakeUp();
}

void btMultiBody_worldDirToLocal(btMultiBody* obj, int i, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->worldDirToLocal(i, VECTOR3_USE(vec)), value);
}

void btMultiBody_worldPosToLocal(btMultiBody* obj, int i, const btScalar* vec, btScalar* value)
{
	VECTOR3_CONV(vec);
	VECTOR3_OUT_VAL(obj->worldPosToLocal(i, VECTOR3_USE(vec)), value);
}

void btMultiBody_delete(btMultiBody* obj)
{
	delete obj;
}
