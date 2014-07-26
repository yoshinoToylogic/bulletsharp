using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class BoxBoxDetector : DiscreteCollisionDetectorInterface
	{
		public BoxBoxDetector(BoxShape box1, BoxShape box2)
			: base(btBoxBoxDetector_new(box1._native, box2._native))
		{
		}

		public BoxShape Box1
		{
			get { return CollisionShape.GetManaged(btBoxBoxDetector_getBox1(_native)) as BoxShape; }
			set { btBoxBoxDetector_setBox1(_native, value._native); }
		}

		public BoxShape Box2
		{
			get { return CollisionShape.GetManaged(btBoxBoxDetector_getBox2(_native)) as BoxShape; }
			set { btBoxBoxDetector_setBox2(_native, value._native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBoxBoxDetector_new(IntPtr box1, IntPtr box2);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBoxBoxDetector_getBox1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBoxBoxDetector_getBox2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBoxBoxDetector_setBox1(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBoxBoxDetector_setBox2(IntPtr obj, IntPtr value);
	}
}
