#pragma once

#pragma managed(push, off)
#include <BulletSoftBody/btSoftBody.h>
#pragma managed(pop)

#include "CollisionObject.h"

namespace BulletSharp
{
	ref class BroadphaseInterface;
	ref class CollisionObjectArray;
	ref class DbvtNode;
	ref class Dispatcher;
	ref class RigidBody;
	ref class SparseSdf;

	namespace SoftBody
	{
		ref class FaceArray;
		ref class LinkArray;
		ref class Material;
		ref class Node;
		ref class MaterialArray;
		ref class NodeArray;

		public ref class SoftBodyWorldInfo
		{
		private:
			btSoftBodyWorldInfo* _info;

		public:
			SoftBodyWorldInfo();

			property btScalar AirDensity
			{
				btScalar get();
				void set(btScalar value);
			}

			property BroadphaseInterface^ Broadphase
			{
				BroadphaseInterface^ get();
				void set(BroadphaseInterface^ value);
			}

			property BulletSharp::Dispatcher^ Dispatcher
			{
				BulletSharp::Dispatcher^ get();
				void set(BulletSharp::Dispatcher^ value);
			}

			property Vector3 Gravity
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property BulletSharp::SparseSdf^ SparseSdf
			{
				BulletSharp::SparseSdf^ get();
				void set(BulletSharp::SparseSdf^ value);
			}

			property btScalar WaterDensity
			{
				btScalar get();
				void set(btScalar value);
			}

			property Vector3 WaterNormal
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property btScalar WaterOffset
			{
				btScalar get();
				void set(btScalar value);
			}

		internal:
			property btSoftBodyWorldInfo* UnmanagedPointer
			{
				btSoftBodyWorldInfo* get();
				void set(btSoftBodyWorldInfo* value);
			};
		};

		public enum class AeroModel
		{
			VPoint = btSoftBody::eAeroModel::V_Point,
			VTwoSided = btSoftBody::eAeroModel::V_TwoSided,
			VOneSided = btSoftBody::eAeroModel::V_OneSided,
			FTwoSided = btSoftBody::eAeroModel::F_TwoSided,
			FOneSided = btSoftBody::eAeroModel::F_OneSided,
			End = btSoftBody::eAeroModel::END
		};

		[Flags]
		public enum class FCollisions
		{
			RvsMask = btSoftBody::fCollision::RVSmask,
			SdfRS = btSoftBody::fCollision::SDF_RS,
			CLRS = btSoftBody::fCollision::CL_RS,
			SvsMask = btSoftBody::fCollision::SVSmask,
			VFSS = btSoftBody::fCollision::VF_SS,
			CLSS = btSoftBody::fCollision::CL_SS,
			CLSelf = btSoftBody::fCollision::CL_SELF,
			Default = btSoftBody::fCollision::Default,
			End = btSoftBody::fCollision::END
		};

		[Flags]
		public enum class FMaterial
		{
			DebugDraw = btSoftBody::fMaterial::DebugDraw,
			Default = btSoftBody::fMaterial::Default,
			End = btSoftBody::fMaterial::END
		};

		public ref class Config
		{
		private:
			btSoftBody::Config* _config;

		internal:
			Config(btSoftBody::Config* config);

		public:
			property AeroModel AeroModel
			{
				BulletSharp::SoftBody::AeroModel get();
				void set(BulletSharp::SoftBody::AeroModel value);
			}

			property btScalar Chr
			{
				btScalar get();
				void set(btScalar value);
			}

			property btScalar DF
			{
				btScalar get();
				void set(btScalar value);
			}

			property btScalar DG
			{
				btScalar get();
				void set(btScalar value);
			}

			property btScalar DP
			{
				btScalar get();
				void set(btScalar value);
			}

			property FCollisions Collisions
			{
				FCollisions get();
				void set(FCollisions value);
			}

			property btScalar LF
			{
				btScalar get();
				void set(btScalar value);
			}

			property int PIterations
			{
				int get();
				void set(int value);
			}

			property btScalar PR
			{
				btScalar get();
				void set(btScalar value);
			}

			property btScalar VC
			{
				btScalar get();
				void set(btScalar value);
			}

			property btSoftBody::Config* UnmanagedPointer
			{
				btSoftBody::Config* get();
				void set(btSoftBody::Config* value);
			};
		};

		public ref class Element
		{
		internal:
			btSoftBody::Element* _element;
			Element(btSoftBody::Element* element);

			property btSoftBody::Element* UnmanagedPointer
			{
				btSoftBody::Element* get();
				void set(btSoftBody::Element* value);
			};
		};

		public ref class Feature : Element
		{
		internal:
			Feature(btSoftBody::Feature* feature);

		public:
			property BulletSharp::SoftBody::Material^ Material
			{
				BulletSharp::SoftBody::Material^ get();
				void set(BulletSharp::SoftBody::Material^ value);
			}

		internal:
			property btSoftBody::Feature* UnmanagedPointer
			{
				btSoftBody::Feature* get() new;
			}
		};

		public ref class Link : Feature
		{
		internal:
			Link(btSoftBody::Link* link);

		public:
			property btScalar C0
			{
				btScalar get();
				void set(btScalar value);
			}

			property btScalar C1
			{
				btScalar get();
				void set(btScalar value);
			}

			property btScalar C2
			{
				btScalar get();
				void set(btScalar value);
			}

			property Vector3 C3
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property bool IsBending
			{
				bool get();
				void set(bool value);
			}

			property array<Node^>^ N
			{
				array<Node^>^ get();
				void set(array<Node^>^ value);
			}

			property btScalar RestLength
			{
				btScalar get();
				void set(btScalar value);
			}

		internal:
			property btSoftBody::Link* UnmanagedPointer
			{
				btSoftBody::Link* get() new;
			}
		};

		public ref class Material : Element
		{
		internal:
			Material(btSoftBody::Material* material);

		public:
			property btScalar Ast
			{
				btScalar get();
				void set(btScalar value);
			}

			property FMaterial Flags
			{
				FMaterial get();
				void set(FMaterial value);
			}

			property btScalar Lst
			{
				btScalar get();
				void set(btScalar value);
			}

			property btScalar Vst
			{
				btScalar get();
				void set(btScalar value);
			}

		internal:
			property btSoftBody::Material* UnmanagedPointer
			{
				btSoftBody::Material* get() new;
			}
		};

		public ref class Node : Feature
		{
		internal:
			Node(btSoftBody::Node* node);

		public:
			property btScalar Area
			{
				btScalar get();
				void set(btScalar value);
			}

			property Vector3 Force
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property btScalar InverseMass
			{
				btScalar get();
				void set(btScalar value);
			}

			property bool IsAttached
			{
				bool get();
				void set(bool value);
			}

#ifndef DISABLE_DBVT
			property DbvtNode^ Leaf
			{
				DbvtNode^ get();
				void set(DbvtNode^ value);
			}
#endif

			property Vector3 Normal
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property Vector3 Q
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property Vector3 Velocity
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property Vector3 X
			{
				Vector3 get();
				void set(Vector3 value);
			}

		internal:
			property btSoftBody::Node* UnmanagedPointer
			{
				btSoftBody::Node* get() new;
			}
		};

		public ref class Face : Feature
		{
		internal:
			Face(btSoftBody::Face* face);

		public:
#ifndef DISABLE_DBVT
			property DbvtNode^ Leaf
			{
				DbvtNode^ get();
				void set(DbvtNode^ value);
			}
#endif
			property array<Node^>^ N
			{
				array<Node^>^ get();
				void set(array<Node^>^ value);
			}

			property Vector3 Normal
			{
				Vector3 get();
				void set(Vector3 set);
			}

			property btScalar RestArea
			{
				btScalar get();
				void set(btScalar value);
			}

		internal:
			property btSoftBody::Face* UnmanagedPointer
			{
				btSoftBody::Face* get() new;
			}
		};

		public ref class Note : Element
		{
		internal:
			Note(btSoftBody::Note* note);
		};

		public ref class Tetra : Feature
		{
		internal:
			Tetra(btSoftBody::Tetra* tetra);
		};

		public ref class SoftBody : CollisionObject
		{
		internal:
			SoftBody(btSoftBody* body);

		public:
			SoftBody(SoftBodyWorldInfo^ worldInfo, array<Vector3>^ x, array<btScalar>^ m);

			void AddForce(Vector3 force, int node);
			void AddForce(Vector3 force);
			void AddVelocity(Vector3 velocity, int node);
			void AddVelocity(Vector3 velocity);
			void AppendAnchor(int node, RigidBody^ body, bool disableCollisionBetweenLinkedBodies);
			void AppendAnchor(int node, RigidBody^ body);
			void AppendLink(int node0, int node1, Material^ material, bool bCheckExist);
			void AppendLink(int node0, int node1, Material^ material);
			void AppendLink(int node0, int node1);
			Material^ AppendMaterial();
			int GenerateBendingConstraints(int distance, Material^ material);
			int GenerateBendingConstraints(int distance);
			int GenerateClusters(int k, int maxIterations);
			int GenerateClusters(int k);
			void Scale(Vector3 scale);
			void SetPose(bool bVolume, bool bFrame);
			void SetTotalMass(btScalar mass, bool fromFaces);
			void SetTotalMass(btScalar mass);
			void SetVolumeMass(btScalar mass);
			void StaticSolve(int iterations);
			void Transform(Matrix transform);
			void Translate(Vector3 translation);
			void Translate(btScalar x, btScalar y, btScalar z); // helper

			static SoftBody^ Upcast(CollisionObject^ colObj);

			property Config^ Cfg
			{
				Config^ get();
				void set(Config^ value);
			}

			property CollisionObjectArray^ CollisionDisabledObjects
			{
				CollisionObjectArray^ get();
				void set(CollisionObjectArray^ value);
			}

			property FaceArray^ Faces
			{
				FaceArray^ get();
				void set(FaceArray^ value);
			}

			property LinkArray^ Links
			{
				LinkArray^ get();
				void set(LinkArray^ value);
			}

			property MaterialArray^ Materials
			{
				MaterialArray^ get();
				void set(MaterialArray^ value);
			}

			property NodeArray^ Nodes
			{
				NodeArray^ get();
				void set(NodeArray^ value);
			}

			property btScalar TotalMass
			{
				btScalar get();
				void set(btScalar value);
			}

		internal:
			property btSoftBody* UnmanagedPointer
			{
				btSoftBody* get() new;
			}
		};
	};
};
