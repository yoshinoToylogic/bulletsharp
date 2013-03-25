#pragma once

namespace BulletSharp
{
	ref class RigidBody;

	public ref class WheelInfoConstructionInfo
	{
	internal:
		btWheelInfoConstructionInfo* _unmanaged;

	public:
		!WheelInfoConstructionInfo();
	protected:
		~WheelInfoConstructionInfo();

	public:
		WheelInfoConstructionInfo();

		property Vector3 ChassisConnectionCS
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property btScalar FrictionSlip
		{
			btScalar get();
			void set(btScalar value);
		}

		property bool IsFrontWheel
		{
			bool get();
			void set(bool value);
		}

		property btScalar MaxSuspensionForce
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar MaxSuspensionTravelCm
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar SuspensionRestLength
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar SuspensionStiffness
		{
			btScalar get();
			void set(btScalar value);
		}

		property Vector3 WheelAxleCS
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property btScalar WheelDampingCompression
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar WheelDampingRelaxation
		{
			btScalar get();
			void set(btScalar value);
		}

		property Vector3 WheelDirectionCS
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property btScalar WheelRadius
		{
			btScalar get();
			void set(btScalar value);
		}
	};

	public ref class WheelInfo
	{
	public:
		ref class RaycastInfo
		{
		private:
			btWheelInfo::RaycastInfo* _info;

		internal:
			RaycastInfo(btWheelInfo::RaycastInfo* info);

		public:
			RaycastInfo();

			property Vector3 ContactNormalWS
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property Vector3 ContactPointWS
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property Object^ GroundObject
			{
				Object^ get();
				void set(Object^ value);
			}

			property Vector3 HardPointWS
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property bool IsInContact
			{
				bool get();
				void set(bool value);
			}

			property btScalar SuspensionLength
			{
				btScalar get();
				void set(btScalar value);
			}

			property Vector3 WheelAxleWS
			{
				Vector3 get();
				void set(Vector3 value);
			}

			property Vector3 WheelDirectionWS
			{
				Vector3 get();
				void set(Vector3 value);
			}

		internal:
			property btWheelInfo::RaycastInfo* UnmanagedPointer
			{
				virtual btWheelInfo::RaycastInfo* get();
				void set(btWheelInfo::RaycastInfo* value);
			}
		};

	internal:
		btWheelInfo* _unmanaged;

		WheelInfo(btWheelInfo* wheelInfo);

	public:
		WheelInfo(WheelInfoConstructionInfo^ ci);

		void UpdateWheel(RigidBody^ chassis, RaycastInfo^ raycastInfo);

		property btScalar Brake
		{
			btScalar get();
			void set(btScalar value);
		}

		property Vector3 ChassisConnectionPointCS
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property Object^ ClientInfo
		{
			Object^ get();
			void set(Object^ value);
		}

		property btScalar ClippedInvContactDotSuspension
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar DeltaRotation
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar EngineForce
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar FrictionSlip
		{
			btScalar get();
			void set(btScalar value);
		}

		property bool IsFrontWheel
		{
			bool get();
			void set(bool value);
		}

		property btScalar MaxSuspensionForce
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar MaxSuspensionTravelCm
		{
			btScalar get();
			void set(btScalar value);
		}

		property RaycastInfo^ RaycastInfo_
		{
			RaycastInfo^ get();
			void set(RaycastInfo^ value);
		}

		property btScalar RollInfluence
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar Rotation
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar SkidInfo
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar Steering
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar SuspensionRelativeVelocity
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar SuspensionRestLength
		{
			btScalar get();
		}

		property btScalar SuspensionRestLength1
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar SuspensionStiffness
		{
			btScalar get();
			void set(btScalar value);
		}

		property Vector3 WheelAxleCS
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property btScalar WheelDampingCompression
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar WheelDampingRelaxation
		{
			btScalar get();
			void set(btScalar value);
		}

		property Vector3 WheelDirectionCS
		{
			Vector3 get();
			void set(Vector3 value);
		}

		property btScalar WheelRadius
		{
			btScalar get();
			void set(btScalar value);
		}

		property btScalar WheelSuspensionForce
		{
			btScalar get();
			void set(btScalar value);
		}

		property Matrix WorldTransform
		{
			Matrix get();
			void set(Matrix value);
		}
	};
}