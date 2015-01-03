#pragma once

#include "IDisposable.h"

namespace BulletSharp
{
	interface class IDebugDraw;

	public ref class ConvexCast : BulletSharp::IDisposable
	{
	public:
		ref class CastResult
		{
		internal:
			btConvexCast::CastResult* _native;
			CastResult(btConvexCast::CastResult* castResult);

		public:
			!CastResult();
		protected:
			~CastResult();

		public:
			CastResult();

#ifndef DISABLE_DEBUGDRAW
			void DebugDraw (btScalar fraction);
#endif
			void DrawCoordSystem (Matrix trans);
			void ReportFailure(int errNo, int numIterations);

			property btScalar AllowedPenetration
			{
				btScalar get();
				void set(btScalar value);
			}

#ifndef DISABLE_DEBUGDRAW
			property IDebugDraw^ DebugDrawer
			{
				IDebugDraw^ get();
				void set(IDebugDraw^ value);
			}
#endif

			property btScalar Fraction
			{
				btScalar get();
				void set(btScalar value);
			}

			property Vector3 HitPoint
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property Matrix HitTransformA
			{
				Matrix get();
				void set(Matrix value);
			}

			property Matrix HitTransformB
			{
				Matrix get();
				void set(Matrix value);
			}

			property Vector3 Normal
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property bool IsDisposed
			{
				virtual bool get();
			}
		};

		virtual event EventHandler^ OnDisposing;
		virtual event EventHandler^ OnDisposed;

	internal:
		btConvexCast* _native;

		ConvexCast(btConvexCast* convexCast);

	public:
		!ConvexCast();
	protected:
		~ConvexCast();

	public:
		bool CalcTimeOfImpact(Matrix fromA, Matrix toA, Matrix fromB, Matrix toB, CastResult^ result);

		property bool IsDisposed
		{
			virtual bool get();
		}
	};
};