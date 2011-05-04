#pragma once

// Fully implemented as of 07 Dec 2010

#include "ConvexShape.h"

namespace BulletSharp
{
	public ref class UniformScalingShape : ConvexShape
	{
	private:
		ConvexShape^ _childShape;

	internal:
		UniformScalingShape(btUniformScalingShape* shape);

	public:
		UniformScalingShape(ConvexShape^ convexChildShape, btScalar uniformScalingFactor);

		property ConvexShape^ ChildShape
		{
			ConvexShape^ get();
		}

		property btScalar UniformScalingFactor
		{
			btScalar get();
		}

	internal:
		property btUniformScalingShape* UnmanagedPointer
		{
			btUniformScalingShape* get() new;
		}
	};
};