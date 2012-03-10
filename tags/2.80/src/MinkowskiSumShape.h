#pragma once

#include "ConvexInternalShape.h"

namespace BulletSharp
{
	ref class ConvexShape;

	public ref class MinkowskiSumShape : ConvexInternalShape
	{
	private:
		ConvexShape^ _shapeA;
		ConvexShape^ _shapeB;

	internal:
		MinkowskiSumShape(btMinkowskiSumShape* shape);

	public:
		MinkowskiSumShape(ConvexShape^ shapeA, ConvexShape^ shapeB);

		property ConvexShape^ ShapeA
		{
			ConvexShape^ get();
		}

		property ConvexShape^ ShapeB
		{
			ConvexShape^ get();
		}

		property Matrix TransformA
		{
			Matrix get();
			void set(Matrix value);
		}

		property Matrix TransformB
		{
			Matrix get();
			void set(Matrix value);
		}

	internal:
		property btMinkowskiSumShape* UnmanagedPointer
		{
			btMinkowskiSumShape* get() new;
		}
	};
};
