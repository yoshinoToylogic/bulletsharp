#pragma once

namespace BulletSharp
{
	class MotionStateWrapper;

	public ref class MotionState
	{
	internal:
		btMotionState* _unmanaged;

	internal:
		MotionState(btMotionState* motionState);

	protected:
		MotionState();
		~MotionState();

	public:
		void GetWorldTransform([Out] Matrix% transform);

		property Matrix WorldTransform
		{
			virtual Matrix get();
			virtual void set(Matrix value);
		}
	};

	class MotionStateWrapper : public btMotionState
	{
	public:
		auto_gcroot<MotionState^> _motionState;

		virtual void getWorldTransform(btTransform& worldTrans) const
		{
			Math::MatrixToBtTransform(_motionState->WorldTransform, &worldTrans);
		}

		virtual void setWorldTransform(const btTransform& worldTrans)
		{
			Math::BtTransformToMatrix(&worldTrans, _motionState->WorldTransform);
		}
	};
};
