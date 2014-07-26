#include <BulletDynamics/Featherstone/btMultiBodyJointMotor.h>

#include "btMultiBodyJointMotor_wrap.h"

btMultiBodyJointMotor* btMultiBodyJointMotor_new(btMultiBody* body, int link, btScalar desiredVelocity, btScalar maxMotorImpulse)
{
	return new btMultiBodyJointMotor(body, link, desiredVelocity, maxMotorImpulse);
}
