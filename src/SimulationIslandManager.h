#pragma once

namespace BulletSharp
{
	ref class CollisionWorld;
	ref class Dispatcher;
	ref class UnionFind;

	public ref class SimulationIslandManager : ITrackingDisposable
	{
	public:
		/*ref class IslandCallback abstract : IDisposable
		{
		internal:
			btSimulationIslandManager::IslandCallback* _native;
			IslandCallback(btSimulationIslandManager::IslandCallback* native);

		public:
			!IslandCallback();
		protected:
			~IslandCallback();

		public:
			IslandCallback();

			void ProcessIsland(array<CollisionObject^>^ bodies, array<PersistentManifold^>^ manifolds,
				int islandId);

			property bool IsDisposed
			{
				virtual bool get();
			}
		};
		*/
		virtual event EventHandler^ OnDisposing;
		virtual event EventHandler^ OnDisposed;

	private:
		bool _preventDelete;

	internal:
		btSimulationIslandManager* _native;
		SimulationIslandManager(btSimulationIslandManager* native, bool preventDelete);

	public:
		!SimulationIslandManager();
	protected:
		~SimulationIslandManager();

	public:
		SimulationIslandManager();

		//void BuildAndProcessIslands(Dispatcher^ dispatcher, CollisionWorld^ collisionWorld,
		//	IslandCallback^ callback);
		void BuildIslands(Dispatcher^ dispatcher, CollisionWorld^ colWorld);
		void FindUnions(Dispatcher^ dispatcher, CollisionWorld^ colWorld);
		void InitUnionFind(int n);
		void StoreIslandActivationState(CollisionWorld^ world);
		void UpdateActivationState(CollisionWorld^ colWorld, Dispatcher^ dispatcher);

		property bool IsDisposed
		{
			virtual bool get();
		}

		property bool SplitIslands
		{
			bool get();
			void set(bool doSplitIslands);
		}

		property UnionFind^ UnionFind
		{
			BulletSharp::UnionFind^ get();
		}
	};
};
