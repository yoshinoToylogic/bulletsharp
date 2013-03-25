using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class CharacterControllerInterface : ActionInterface
	{
		internal CharacterControllerInterface(IntPtr native)
			: base(native)
		{
		}

		public bool CanJump()
		{
			return btCharacterControllerInterface_canJump(_native);
		}

		public void Jump()
		{
			btCharacterControllerInterface_jump(_native);
		}

		public bool OnGround()
		{
			return btCharacterControllerInterface_onGround(_native);
		}

		public void PlayerStep(CollisionWorld collisionWorld, float dt)
		{
			btCharacterControllerInterface_playerStep(_native, collisionWorld._native, dt);
		}

		public void PreStep(CollisionWorld collisionWorld)
		{
			btCharacterControllerInterface_preStep(_native, collisionWorld._native);
		}

		public void Reset()
		{
			btCharacterControllerInterface_reset(_native);
		}

		public void SetWalkDirection(Vector3 walkDirection)
		{
			btCharacterControllerInterface_setWalkDirection(_native, ref walkDirection);
		}

		public void SetVelocityForTimeInterval(Vector3 velocity, float timeInterval)
		{
			btCharacterControllerInterface_setVelocityForTimeInterval(_native, ref velocity, timeInterval);
		}

		public void Warp(Vector3 origin)
		{
			btCharacterControllerInterface_warp(_native, ref origin);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btCharacterControllerInterface_canJump(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCharacterControllerInterface_jump(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btCharacterControllerInterface_onGround(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCharacterControllerInterface_playerStep(IntPtr obj, IntPtr collisionWorld, float dt);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCharacterControllerInterface_preStep(IntPtr obj, IntPtr collisionWorld);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCharacterControllerInterface_reset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCharacterControllerInterface_setWalkDirection(IntPtr obj, [In] ref Vector3 walkDirection);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btCharacterControllerInterface_setVelocityForTimeInterval(IntPtr obj, [In] ref Vector3 velocity, float timeInterval);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btCharacterControllerInterface_warp(IntPtr obj, [In] ref Vector3 origin);
	}
}
