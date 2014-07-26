#include "main.h"

extern "C"
{
	EXPORT btMultiBodyJointMotor* btMultiBodyJointMotor_new(btMultiBody* body, int link, btScalar desiredVelocity, btScalar maxMotorImpulse);
}
