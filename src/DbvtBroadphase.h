#pragma once

#include "BroadphaseInterface.h"
#include "BroadphaseProxy.h"

namespace BulletSharp
{
	ref class BroadphaseProxy;
	ref class Dbvt;
	ref class DbvtArray;
	ref class DbvtNode;
	ref class DbvtProxyPtrArray;
	ref class Dispatcher;
	ref class OverlappingPairCache;

#ifndef DISABLE_DBVT
	public ref class DbvtProxy : BroadphaseProxy
	{
	internal:
		DbvtProxy(btDbvtProxy* native);

	private:
		DbvtNode^ _leaf;
		DbvtProxyPtrArray^ _links;

	public:
		DbvtProxy(Vector3% aabbMin, Vector3% aabbMax, IntPtr userPointer, CollisionFilterGroups collisionFilterGroup,
			CollisionFilterGroups collisionFilterMask);
		DbvtProxy(Vector3 aabbMin, Vector3 aabbMax, IntPtr userPointer, CollisionFilterGroups collisionFilterGroup,
			CollisionFilterGroups collisionFilterMask);

		property DbvtNode^ Leaf
		{
			DbvtNode^ get();
			void set(DbvtNode^ value);
		}

		property DbvtProxyPtrArray^ Links
		{
			DbvtProxyPtrArray^ get();
		}

		property int Stage
		{
			int get();
			void set(int value);
		}
	};
#endif

	public ref class DbvtBroadphase : BroadphaseInterface
	{
#ifndef DISABLE_DBVT
	public:
		static int DynamicSet = btDbvtBroadphase::DYNAMIC_SET;
		static int FixedSet = btDbvtBroadphase::FIXED_SET;
		static int StageCount = btDbvtBroadphase::STAGECOUNT;
#endif

	internal:
		DbvtBroadphase(btDbvtBroadphase* native);

	public:
		DbvtBroadphase(BulletSharp::OverlappingPairCache^ pairCache);
		DbvtBroadphase();

#ifndef DISABLE_DBVT
		static void Benchmark(BroadphaseInterface^ broadphase);
		void Collide(Dispatcher^ dispatcher);
		void Optimize();
		void PerformDeferredRemoval(Dispatcher^ dispatcher);
		void SetAabbForceUpdate(BroadphaseProxy^ absproxy, Vector3% aabbMin, Vector3% aabbMax,
			Dispatcher^ dispatcher);
		void SetAabbForceUpdate(BroadphaseProxy^ absproxy, Vector3 aabbMin, Vector3 aabbMax,
			Dispatcher^ dispatcher);

		property int CId
		{
			int get();
			void set(int value);
		}

		property int CUpdates
		{
			int get();
			void set(int value);
		}

		property bool DeferredCollide
		{
			bool get();
			void set(bool value);
		}

		property int DUpdates
		{
			int get();
			void set(int value);
		}

		property int FixedLeft
		{
			int get();
			void set(int value);
		}

		property int FUpdates
		{
			int get();
			void set(int value);
		}

		property int GId
		{
			int get();
			void set(int value);
		}

		property bool NeedCleanup
		{
			bool get();
			void set(bool value);
		}

		property int NewPairs
		{
			int get();
			void set(int value);
		}

		property BulletSharp::OverlappingPairCache^ PairCache
		{
			BulletSharp::OverlappingPairCache^ get();
			void set(BulletSharp::OverlappingPairCache^ value);
		}

		property int PId
		{
			int get();
			void set(int value);
		}

		property btScalar Prediction
		{
			btScalar get();
			void set(btScalar value);
		}

		property bool ReleasePairCache
		{
			bool get();
			void set(bool value);
		}

		property DbvtArray^ Sets
		{
			DbvtArray^ get();
		}

		property int StageCurrent
		{
			int get();
			void set(int value);
		}

		property DbvtProxyPtrArray^ StageRoots
		{
			DbvtProxyPtrArray^ get();
		}

		property unsigned int UpdatesCall
		{
			unsigned int get();
			void set(unsigned int value);
		}

		property unsigned int UpdatesDone
		{
			unsigned int get();
			void set(unsigned int value);
		}

		property btScalar UpdatesRatio
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar VelocityPrediction
		{
			btScalar get();
			void set(btScalar prediction);
		}
#endif
	};
};
