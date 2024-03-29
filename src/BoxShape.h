#pragma once

#include "PolyhedralConvexShape.h"

namespace BulletSharp
{
	public ref class BoxShape : PolyhedralConvexShape
	{
	internal:
		BoxShape(btBoxShape* native);

	public:
		BoxShape(Vector3% boxHalfExtents);
		BoxShape(Vector3 boxHalfExtents);
		BoxShape(btScalar boxHalfExtentsX, btScalar boxHalfExtentsY, btScalar boxHalfExtentsZ);
		BoxShape(btScalar boxHalfExtent); // cube helper

		Vector4 GetPlaneEquation(int index);

		property Vector3 HalfExtentsWithMargin
		{
			Vector3 get();
		}

		property Vector3 HalfExtentsWithoutMargin
		{
			Vector3 get();
		}
	};
};
