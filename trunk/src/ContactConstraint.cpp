#include "StdAfx.h"

#if 0
#ifndef DISABLE_CONSTRAINTS

#include "ContactConstraint.h"
#include "PersistentManifold.h"
#include "RigidBody.h"

ContactConstraint::ContactConstraint(btContactConstraint* native)
	: TypedConstraint(native)
{
}

ContactConstraint::ContactConstraint(PersistentManifold^ contactManifold, RigidBody^ rigidBodyA,
	RigidBody^ rigidBodyB)
	: TypedConstraint(new btContactConstraint((btPersistentManifold*)contactManifold->_native,
		*(btRigidBody*)rbA->_native, *(btRigidBody*)rbB->_native))
{
}

PersistentManifold^ ContactConstraint::ContactManifold::get()
{
	return gcnew PersistentManifold(Native->getContactManifold());
}
void ContactConstraint::ContactManifold::set(PersistentManifold^ contactManifold)
{
	Native->setContactManifold((btPersistentManifold*)contactManifold->_native);
}

#endif
#endif
