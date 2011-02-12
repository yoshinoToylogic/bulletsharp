#pragma once

// Fully implemented as of 24 Feb 2010

#include "IDisposable.h"

namespace BulletSharp
{
	ref class BroadphaseProxy;
	ref class Dispatcher;
	ref class OverlappingPairCache;

	public ref class BroadphaseAabbCallback
	{
	protected:
		btBroadphaseAabbCallback* _aabbCallback;

	internal:
		BroadphaseAabbCallback(btBroadphaseAabbCallback* callback);

	public:
		bool Process(BroadphaseProxy^ proxy);

	internal:
		property btBroadphaseAabbCallback* UnmanagedPointer
		{
			virtual btBroadphaseAabbCallback* get();
			void set(btBroadphaseAabbCallback* value);
		}
	};

	public ref class BroadphaseRayCallback : BroadphaseAabbCallback
	{
	internal:
		BroadphaseRayCallback(btBroadphaseRayCallback* callback);

		property btBroadphaseRayCallback* UnmanagedPointer
		{
			btBroadphaseRayCallback* get() new;
		}
	};

	public ref class BroadphaseInterface : BulletSharp::IDisposable
	{
	public:
		virtual event EventHandler^ OnDisposing;
		virtual event EventHandler^ OnDisposed;

	private:
		btBroadphaseInterface* _broadphase;
	protected:
		OverlappingPairCache^ _pairCache;

	internal:
		BroadphaseInterface(btBroadphaseInterface* broadphase);
	public:
		!BroadphaseInterface();
	protected:
		~BroadphaseInterface();
	public:
		property bool IsDisposed
		{
			virtual bool get();
		}

	public:
		void AabbTest(Vector3 aabbMin, Vector3 aabbMax, BroadphaseAabbCallback^ callback);
		void CalculateOverlappingPairs(Dispatcher^ dispatcher);
		BroadphaseProxy^ CreateProxy(Vector3 aabbMin, Vector3 aabbMax,
			int shapeType, IntPtr userPtr, CollisionFilterGroups collisionFilterGroup,
			CollisionFilterGroups collisionFilterMask, Dispatcher^ dispatcher, IntPtr multiSapProxy);
		void DestroyProxy(BroadphaseProxy^ proxy, Dispatcher^ dispatcher);
		void GetAabb(BroadphaseProxy^ proxy, [Out] Vector3% aabbMin, [Out] Vector3% aabbMax);
		void GetBroadphaseAabb([Out] Vector3% aabbMin, [Out] Vector3% aabbMax);
		void PrintStats();
		void RayTest(Vector3 rayFrom, Vector3 rayTo, BroadphaseRayCallback^ rayCallback,
			Vector3 aabbMin, Vector3 aabbMax);
		void RayTest(Vector3 rayFrom, Vector3 rayTo, BroadphaseRayCallback^ rayCallback,
			Vector3 aabbMin);
		void RayTest(Vector3 rayFrom, Vector3 rayTo, BroadphaseRayCallback^ rayCallback);
		void ResetPool(Dispatcher^ dispatcher);
		void SetAabb(BroadphaseProxy^ proxy, Vector3 aabbMin, Vector3 aabbMax, Dispatcher^ dispatcher);

		property BulletSharp::OverlappingPairCache^ OverlappingPairCache
		{
			BulletSharp::OverlappingPairCache^ get();
		}

	internal:
		property btBroadphaseInterface* UnmanagedPointer
		{
			virtual btBroadphaseInterface* get();
			void set(btBroadphaseInterface* value);
		}
	};
};
