#pragma once

namespace BulletSharp
{
	public ref class TriangleCallback : IDisposable
	{
	internal:
		btTriangleCallback* _native;
		TriangleCallback(btTriangleCallback* callback);

	public:
		!TriangleCallback();
	protected:
		~TriangleCallback();

	public:
		void ProcessTriangle(Vector3 triangle, int partId, int triangleIndex);

		property bool IsDisposed
		{
			virtual bool get();
		}
	};

#ifndef DISABLE_INTERNAL
	public ref class InternalTriangleIndexCallback : IDisposable
	{
	internal:
		btInternalTriangleIndexCallback* _native;

		InternalTriangleIndexCallback(btInternalTriangleIndexCallback* callback);

	public:
		!InternalTriangleIndexCallback();
	protected:
		~InternalTriangleIndexCallback();

	public:
		void InternalProcessTriangleIndex(Vector3 triangle, int partId, int triangleIndex);

		property bool IsDisposed
		{
			virtual bool get();
		}
	};
#endif
};
