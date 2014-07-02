#pragma once

#include "TypedConstraint.h"

namespace BulletSharp
{
	ref class PersistentManifold;
	ref class RigidBody;

	public ref class ContactConstraint : TypedConstraint
	{
	internal:
		ContactConstraint(btContactConstraint* native);

	public:
		ContactConstraint(PersistentManifold^ contactManifold, RigidBody^ rbA, RigidBody^ rbB);

		property PersistentManifold^ ContactManifold
		{
			PersistentManifold^ get();
			void set(PersistentManifold^ contactManifold);
		}
	};
};
