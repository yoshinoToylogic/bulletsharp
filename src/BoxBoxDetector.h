#pragma once

#include "DiscreteCollisionDetectorInterface.h"

namespace BulletSharp
{
	ref class BoxShape;

	public ref class BoxBoxDetector : DiscreteCollisionDetectorInterface
	{
	public:
		BoxBoxDetector(BoxShape^ box1, BoxShape^ box2);

		property BoxShape^ Box1
		{
			BoxShape^ get();
			void set(BoxShape^ value);
		}

		property BoxShape^ Box2
		{
			BoxShape^ get();
			void set(BoxShape^ value);
		}
	};
};
