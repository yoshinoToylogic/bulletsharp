#pragma once

#include "IDisposable.h"

namespace BulletSharp
{
	ref class Dispatcher;
	ref class DispatcherInfo;
	ref class CollisionObject;
	ref class CollisionObjectWrapper;
	ref class ManifoldResult;
	ref class AlignedManifoldArray;
	ref class PersistentManifold;

	public ref class CollisionAlgorithmConstructionInfo
	{
	internal:
		btCollisionAlgorithmConstructionInfo* _unmanaged;

	public:
		!CollisionAlgorithmConstructionInfo();
	protected:
		~CollisionAlgorithmConstructionInfo();

	public:
		CollisionAlgorithmConstructionInfo();
		CollisionAlgorithmConstructionInfo(Dispatcher^ dispatcher, int temp);

		property BulletSharp::Dispatcher^ Dispatcher
		{
			BulletSharp::Dispatcher^ get();
			void set(BulletSharp::Dispatcher^ value);
		}

		property PersistentManifold^ Manifold
		{
			PersistentManifold^ get();
			void set(PersistentManifold^ value);
		}
	};

	public ref class CollisionAlgorithm : BulletSharp::IDisposable
	{
	public:
		virtual event EventHandler^ OnDisposing;
		virtual event EventHandler^ OnDisposed;

	private:
		btCollisionAlgorithm* _algorithm;

	internal:
		CollisionAlgorithm(btCollisionAlgorithm* algorithm);

	public:
		!CollisionAlgorithm();
	protected:
		~CollisionAlgorithm();

	public:
		btScalar CalculateTimeOfImpact(CollisionObject^ body0, CollisionObject^ body1,
			DispatcherInfo^ dispatchInfo, ManifoldResult^ resultOut);
		void GetAllContactManifolds(AlignedManifoldArray^ manifoldArray);
		void ProcessCollision(CollisionObjectWrapper^ body0Wrap, CollisionObjectWrapper^ body1Wrap,
			DispatcherInfo^ dispatchInfo, ManifoldResult^ resultOut);

		property bool IsDisposed
		{
			virtual bool get();
		}

	internal:
		property btCollisionAlgorithm* UnmanagedPointer
		{
			virtual btCollisionAlgorithm* get();
			void set(btCollisionAlgorithm* value);
		}
	};
};