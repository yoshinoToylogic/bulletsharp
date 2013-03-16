using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class ActionInterface
	{
		internal IntPtr _native;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btActionInterface_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ActionInterface()
		{
			Dispose(false);
		}

		public void DebugDraw(IntPtr debugDrawer)
		{
			btActionInterface_debugDraw(_native, debugDrawer);
		}

		public void UpdateAction(IntPtr collisionWorld, float deltaTimeStep)
		{
			btActionInterface_updateAction(_native, collisionWorld, deltaTimeStep);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btActionInterface_delete(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btActionInterface_debugDraw(IntPtr obj, IntPtr debugDrawer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btActionInterface_updateAction(IntPtr obj, IntPtr collisionWorld, float deltaTimeStep);
	}
}
