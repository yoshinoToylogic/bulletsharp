#pragma once

// Fully implemented as of 02 July 2010

#pragma managed(push, off)
#include <BulletCollision/Gimpact/btTriangleShapeEx.h>
#pragma managed(pop)

#include "TriangleShape.h"

namespace BulletSharp
{
	public ref class GimTriangleContact
	{
	private:
		GIM_TRIANGLE_CONTACT* _contact;

	public:
		GimTriangleContact();
		GimTriangleContact(GimTriangleContact^ other);

		void CopyFrom(GimTriangleContact^ other);
		void MergePoints(Vector4 plane, btScalar margin, array<Vector3>^ points);

		property btScalar PenetrationDepth
		{
			btScalar get();
			void set(btScalar value);
		}

		property int PointCount
		{
			int get();
			void set(int value);
		}

		property array<Vector3>^ Points
		{
			array<Vector3>^ get();
			void set(array<Vector3>^ value);
		}

		property Vector4 SeparatingNormal
		{
			Vector4 get();
			void set(Vector4 value);
		}

	internal:
		property GIM_TRIANGLE_CONTACT* UnmanagedPointer
		{
			GIM_TRIANGLE_CONTACT* get();
			void set(GIM_TRIANGLE_CONTACT* value);
		}
	};

	public ref class PrimitiveTriangle
	{
	private:
		btPrimitiveTriangle* _triangle;

	internal:
		PrimitiveTriangle(btPrimitiveTriangle* triangle);

	public:
		PrimitiveTriangle();

		void ApplyTransform(Matrix transform);
		void BuildTriPlane();
		int ClipTriangle(PrimitiveTriangle^ other, array<Vector3>^ clippedPoints);
		bool FindTriangleCollisionClipMethod(PrimitiveTriangle^ other, GimTriangleContact^ contacts);
		void GetEdgePlane(int edge_index, [Out] Vector4% plane);
		bool OverlapTestConservative(PrimitiveTriangle^ other);

		property btScalar Dummy
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar Margin
		{
			btScalar get();
			void set(btScalar value);
		}

		property Vector4 Plane
		{
			Vector4 get();
			void set(Vector4 value);
		}

		property array<Vector3>^ Vectors
		{
			array<Vector3>^ get();
			void set(array<Vector3>^ value);
		}

	internal:
		property btPrimitiveTriangle* UnmanagedPointer
		{
			btPrimitiveTriangle* get();
			void set(btPrimitiveTriangle* value);
		}
	};

	public ref class TriangleShapeEx : TriangleShape
	{
	internal:
		TriangleShapeEx(btTriangleShapeEx* triangle);

	public:
		TriangleShapeEx();
		TriangleShapeEx(Vector3 p0, Vector3 p1, Vector3 p2);
		TriangleShapeEx(TriangleShapeEx^ other);

		void ApplyTransform(Matrix transform);
		void BuildTriPlane(Vector4 plane);
		bool OverlapTestConservative(TriangleShapeEx^ other);

	internal:
		property btTriangleShapeEx* UnmanagedPointer
		{
			btTriangleShapeEx* get();
		}
	};
};
