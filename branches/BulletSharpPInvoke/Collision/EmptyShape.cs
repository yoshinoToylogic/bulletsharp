using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class EmptyShape : ConcaveShape
	{
		internal EmptyShape(IntPtr native)
			: base(native)
		{
		}

		public EmptyShape()
			: base(btEmptyShape_new())
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btEmptyShape_new();
	}
}
