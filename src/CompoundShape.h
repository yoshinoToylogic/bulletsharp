#pragma once

#include "CollisionShape.h"

namespace BulletSharp
{
	ref class CompoundShapeChildArray;
	ref class Dbvt;
	ref class DbvtNode;

	public ref class CompoundShapeChild
	{
	private:
		CollisionShape^ _childShape;

	internal:
		btCompoundShapeChild* _native;
		CompoundShapeChild(btCompoundShapeChild* native, CollisionShape^ shape);

	public:
		property btScalar ChildMargin
		{
			btScalar get();
			void set(btScalar value);
		}

		property CollisionShape^ ChildShape
		{
			CollisionShape^ get();
			void set(CollisionShape^ value);
		}

		property BroadphaseNativeType ChildShapeType
		{
			BroadphaseNativeType get();
			void set(BroadphaseNativeType value);
		}

#ifndef DISABLE_DBVT
		property DbvtNode^ Node
		{
			DbvtNode^ get();
			void set(DbvtNode^ value);
		}
#endif

		property Matrix Transform
		{
			Matrix get();
			void set(Matrix value);
		}
	};

	public ref class CompoundShape : CollisionShape
	{
	internal:
		CompoundShape(btCompoundShape* native);

	private:
		CompoundShapeChildArray^ _childList;

	public:
		CompoundShape(bool enableDynamicAabbTree);
		CompoundShape();

		void AddChildShape(Matrix% localTransform, CollisionShape^ shape);
		void AddChildShape(Matrix localTransform, CollisionShape^ shape);
		void CalculatePrincipalAxisTransform(array<btScalar>^ masses, Matrix% principal, [Out] Vector3% inertia);
		void CreateAabbTreeFromChildren();
		CollisionShape^ GetChildShape(int index);
		Matrix GetChildTransform(int index);
		void RecalculateLocalAabb();
		void RemoveChildShape(CollisionShape^ shape);
		void RemoveChildShapeByIndex(int childShapeindex);
		void UpdateChildTransform(int childIndex, Matrix newChildTransform, bool shouldRecalculateLocalAabb);
		void UpdateChildTransform(int childIndex, Matrix newChildTransform);

		property CompoundShapeChildArray^ ChildList
		{
			CompoundShapeChildArray^ get();
		}

#ifndef DISABLE_DBVT
		property Dbvt^ DynamicAabbTree
		{
			Dbvt^ get();
		}
#endif

		property int NumChildShapes
		{
			int get();
		}

		property int UpdateRevision
		{
			int get();
		}
	};
};
