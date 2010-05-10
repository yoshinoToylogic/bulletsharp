#pragma once

// Fully implemented as of 07 Mar 2010

#include "IDisposable.h"

namespace BulletSharp
{
	ref class BroadphasePair;
	ref class BroadphaseProxy;
	ref class Dispatcher;

	public ref class OverlappingPairCallback : BulletSharp::IDisposable
	{
	public:
		virtual event EventHandler^ OnDisposing;
		virtual event EventHandler^ OnDisposed;
	
	private:
		btOverlappingPairCallback* _pairCallback;

	internal:
		OverlappingPairCallback(btOverlappingPairCallback* pairCallback);
	
	public:
		!OverlappingPairCallback();
	protected:
		~OverlappingPairCallback();
	
	public:
		BroadphasePair^ AddOverlappingPair(BroadphaseProxy^ proxy0,
			BroadphaseProxy^ proxy1);
		IntPtr RemoveOverlappingPair(BroadphaseProxy^ proxy0,
			BroadphaseProxy^ proxy1, Dispatcher^ dispatcher);
		void RemoveOverlappingPairsContainingProxy(BroadphaseProxy^ proxy0,
			Dispatcher^ dispatcher);

		property bool IsDisposed
		{
			virtual bool get();
		}

	internal:
		property btOverlappingPairCallback* UnmanagedPointer
		{
			virtual btOverlappingPairCallback* get();
			void set(btOverlappingPairCallback* value);
		}
	};
};