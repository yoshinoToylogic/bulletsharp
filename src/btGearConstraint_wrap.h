#include "main.h"

extern "C"
{
	EXPORT btGearConstraint* btGearConstraint_new(btRigidBody* rbA, btRigidBody* rbB, btScalar* axisInA, btScalar* axisInB, btScalar ratio);
	EXPORT btGearConstraint* btGearConstraint_new2(btRigidBody* rbA, btRigidBody* rbB, btScalar* axisInA, btScalar* axisInB);
}
