using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public interface IAction
    {
        void DebugDraw(IDebugDraw debugDrawer);
        void UpdateAction(CollisionWorld collisionWorld, float deltaTimeStep);
    }

    internal class ActionInterfaceWrapper
	{
    	internal IntPtr _native;
        internal IAction _actionInterface;
        internal DynamicsWorld _world;

        delegate void DebugDrawUnmanagedDelegate(IntPtr debugDrawer);
        delegate void UpdateActionUnmanagedDelegate(IntPtr collisionWorld, float deltaTimeStep);

        DebugDrawUnmanagedDelegate _debugDraw;
        UpdateActionUnmanagedDelegate _updateAction;
        /*
		internal ActionInterface(IntPtr native)
		{
			_native = native;
		}
        */
        public ActionInterfaceWrapper(IAction actionInterface, DynamicsWorld world)
        {
            _actionInterface = actionInterface;
            _world = world;

            _debugDraw = new DebugDrawUnmanagedDelegate(DebugDrawUnmanaged);
            _updateAction = new UpdateActionUnmanagedDelegate(UpdateActionUnmanaged);

            _native = btActionInterfaceWrapper_new(GCHandle.ToIntPtr(GCHandle.Alloc(this)),
                Marshal.GetFunctionPointerForDelegate(_debugDraw),
                Marshal.GetFunctionPointerForDelegate(_updateAction));
        }

        internal static IAction GetManaged(IntPtr native)
        {
            if (native == IntPtr.Zero)
            {
                return null;
            }

            IntPtr handle = btActionInterfaceWrapper_getGCHandle(native);
            return GCHandle.FromIntPtr(handle).Target as IAction;
        }

        private void DebugDrawUnmanaged(IntPtr debugDrawer)
        {
            _actionInterface.DebugDraw(DebugDraw.GetManaged(debugDrawer));
        }

        private void UpdateActionUnmanaged(IntPtr collisionWorld, float deltaTimeStep)
        {
            _actionInterface.UpdateAction(_world, deltaTimeStep);
        }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
                IntPtr handle = btActionInterfaceWrapper_getGCHandle(_native);
                GCHandle.FromIntPtr(handle).Free();
				btActionInterface_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ActionInterfaceWrapper()
		{
			Dispose(false);
		}

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btActionInterfaceWrapper_new(IntPtr actionInterfaceGCHandle, IntPtr debugDrawCallback, IntPtr updateActionCallback);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btActionInterfaceWrapper_getGCHandle(IntPtr obj);

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btActionInterface_delete(IntPtr obj);
    }
}
