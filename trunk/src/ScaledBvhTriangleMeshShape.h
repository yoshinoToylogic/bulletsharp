#pragma once

#include "ConcaveShape.h"

namespace BulletSharp
{
	ref class BvhTriangleMeshShape;

	public ref class ScaledBvhTriangleMeshShape : ConcaveShape
	{
	private:
		BvhTriangleMeshShape^ _childShape;

	internal:
		ScaledBvhTriangleMeshShape(btScaledBvhTriangleMeshShape* shape);

	public:
		ScaledBvhTriangleMeshShape(BvhTriangleMeshShape^ childShape, Vector3 localScaling);

		property BvhTriangleMeshShape^ ChildShape
		{
			BvhTriangleMeshShape^ get();
		}

	internal:
		property btScaledBvhTriangleMeshShape* UnmanagedPointer
		{
			btScaledBvhTriangleMeshShape* get();
		}
	};
};
