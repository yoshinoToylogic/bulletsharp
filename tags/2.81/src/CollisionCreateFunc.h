#pragma once

#include "IDisposable.h"

namespace BulletSharp
{
	ref class CollisionAlgorithm;
	ref class CollisionAlgorithmConstructionInfo;
	ref class CollisionObject;
	ref class CollisionObjectWrapper;

	public ref class CollisionAlgorithmCreateFunc
	{
	private:
		btCollisionAlgorithmCreateFunc* _createFunc;
		bool _deleteObject;

	internal:
		CollisionAlgorithmCreateFunc(btCollisionAlgorithmCreateFunc* solver);

	public:
		!CollisionAlgorithmCreateFunc();
	protected:
		~CollisionAlgorithmCreateFunc();

	public:
		CollisionAlgorithmCreateFunc();

		CollisionAlgorithm^ CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo^ info,
			CollisionObjectWrapper^ body0Wrap, CollisionObjectWrapper^ body1Wrap);

		property bool IsDisposed
		{
			virtual bool get();
		}

		property bool Swapped
		{
			bool get();
			void set(bool value);
		}

	internal:
		property btCollisionAlgorithmCreateFunc* UnmanagedPointer
		{
			virtual btCollisionAlgorithmCreateFunc* get();
			void set(btCollisionAlgorithmCreateFunc* value);
		}
	};
};