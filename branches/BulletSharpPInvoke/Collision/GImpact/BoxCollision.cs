using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class BT_BOX_BOX_TRANSFORM_CACHE
	{
		internal IntPtr _native;

		internal BT_BOX_BOX_TRANSFORM_CACHE(IntPtr native)
		{
			_native = native;
		}

		public BT_BOX_BOX_TRANSFORM_CACHE()
		{
			_native = BT_BOX_BOX_TRANSFORM_CACHE_new();
		}

		public void Calc_absolute_matrix()
		{
			BT_BOX_BOX_TRANSFORM_CACHE_calc_absolute_matrix(_native);
		}

		public void Calc_from_full_invert(Matrix trans0, Matrix trans1)
		{
			BT_BOX_BOX_TRANSFORM_CACHE_calc_from_full_invert(_native, ref trans0, ref trans1);
		}

		public void Calc_from_homogenic(Matrix trans0, Matrix trans1)
		{
			BT_BOX_BOX_TRANSFORM_CACHE_calc_from_homogenic(_native, ref trans0, ref trans1);
		}

		public void Transform(Vector3 point)
		{
			BT_BOX_BOX_TRANSFORM_CACHE_transform(_native, ref point);
		}
        /*
		public void AR
		{
			get { return BT_BOX_BOX_TRANSFORM_CACHE_getAR(_native); }
			set { BT_BOX_BOX_TRANSFORM_CACHE_setAR(_native, value._native); }
		}

		public void R1to0
		{
			get { return BT_BOX_BOX_TRANSFORM_CACHE_getR1to0(_native); }
			set { BT_BOX_BOX_TRANSFORM_CACHE_setR1to0(_native, value._native); }
		}

		public void T1to0
		{
			get { return BT_BOX_BOX_TRANSFORM_CACHE_getT1to0(_native); }
			set { BT_BOX_BOX_TRANSFORM_CACHE_setT1to0(_native, value._native); }
		}
        */
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				BT_BOX_BOX_TRANSFORM_CACHE_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~BT_BOX_BOX_TRANSFORM_CACHE()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr BT_BOX_BOX_TRANSFORM_CACHE_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_calc_absolute_matrix(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_calc_from_full_invert(IntPtr obj, [In] ref Matrix trans0, [In] ref Matrix trans1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_calc_from_homogenic(IntPtr obj, [In] ref Matrix trans0, [In] ref Matrix trans1);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_getAR(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_getR1to0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_getT1to0(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_setAR(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_setR1to0(IntPtr obj, [In] ref Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_setT1to0(IntPtr obj, Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_transform(IntPtr obj, [In] ref Vector3 point);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void BT_BOX_BOX_TRANSFORM_CACHE_delete(IntPtr obj);
	}

	public class AABB
	{
		internal IntPtr _native;

		internal AABB(IntPtr native)
		{
			_native = native;
		}

		public AABB()
		{
			_native = btAABB_new();
		}

		public AABB(Vector3 V1, Vector3 V2, Vector3 V3)
		{
			_native = btAABB_new2(ref V1, ref V2, ref V3);
		}

		public AABB(Vector3 V1, Vector3 V2, Vector3 V3, float margin)
		{
			_native = btAABB_new3(ref V1, ref V2, ref V3, margin);
		}

		public AABB(AABB other)
		{
			_native = btAABB_new4(other._native);
		}

		public AABB(AABB other, float margin)
		{
			_native = btAABB_new5(other._native, margin);
		}

		public void Appy_transform(Matrix trans)
		{
			btAABB_appy_transform(_native, ref trans);
		}

		public void Appy_transform_trans_cache(BT_BOX_BOX_TRANSFORM_CACHE trans)
		{
			btAABB_appy_transform_trans_cache(_native, trans._native);
		}

		public bool Collide_plane(Vector4 plane)
		{
			return btAABB_collide_plane(_native, ref plane);
		}

		public bool Collide_ray(Vector3 vorigin, Vector3 vdir)
		{
			return btAABB_collide_ray(_native, ref vorigin, ref vdir);
		}
        /*
		public bool Collide_triangle_exact(Vector3 p1, Vector3 p2, Vector3 p3, Vector4 triangle_plane)
		{
			return btAABB_collide_triangle_exact(_native, ref p1, ref p2, ref p3, ref triangle_plane);
		}
        */
		public void Copy_with_margin(AABB other, float margin)
		{
			btAABB_copy_with_margin(_native, other._native, margin);
		}

		public void Find_intersection(AABB other, AABB intersection)
		{
			btAABB_find_intersection(_native, other._native, intersection._native);
		}

		public void Get_center_extend(Vector3 center, Vector3 extend)
		{
			btAABB_get_center_extend(_native, ref center, ref extend);
		}

		public bool Has_collision(AABB other)
		{
			return btAABB_has_collision(_native, other._native);
		}

		public void Increment_margin(float margin)
		{
			btAABB_increment_margin(_native, margin);
		}

		public void Invalidate()
		{
			btAABB_invalidate(_native);
		}

		public void Merge(AABB box)
		{
			btAABB_merge(_native, box._native);
		}

		public bool Overlapping_trans_cache(AABB box, BT_BOX_BOX_TRANSFORM_CACHE transcache, bool fulltest)
		{
			return btAABB_overlapping_trans_cache(_native, box._native, transcache._native, fulltest);
		}

		public bool Overlapping_trans_conservative(AABB box, Matrix trans1_to_0)
		{
			return btAABB_overlapping_trans_conservative(_native, box._native, ref trans1_to_0);
		}

		public bool Overlapping_trans_conservative2(AABB box, BT_BOX_BOX_TRANSFORM_CACHE trans1_to_0)
		{
			return btAABB_overlapping_trans_conservative2(_native, box._native, trans1_to_0._native);
		}
        /*
		public eBT_PLANE_INTERSECTION_TYPE Plane_classify(Vector4 plane)
		{
			return btAABB_plane_classify(_native, plane._native);
		}

		public void Projection_interval(Vector3 direction, float vmin, float vmax)
		{
			btAABB_projection_interval(_native, ref direction, vmin._native, vmax._native);
		}

		public void Max
		{
			get { return btAABB_getMax(_native); }
			set { btAABB_setMax(_native, value._native); }
		}

		public void Min
		{
			get { return btAABB_getMin(_native); }
			set { btAABB_setMin(_native, value._native); }
		}
        */
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btAABB_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~AABB()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAABB_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAABB_new2([In] ref Vector3 V1, [In] ref Vector3 V2, [In] ref Vector3 V3);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAABB_new3([In] ref Vector3 V1, [In] ref Vector3 V2, [In] ref Vector3 V3, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAABB_new4(IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAABB_new5(IntPtr other, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_appy_transform(IntPtr obj, [In] ref Matrix trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_appy_transform_trans_cache(IntPtr obj, IntPtr trans);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btAABB_collide_plane(IntPtr obj, [In] ref Vector4 plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btAABB_collide_ray(IntPtr obj, [In] ref Vector3 vorigin, [In] ref Vector3 vdir);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btAABB_collide_triangle_exact(IntPtr obj, [In] ref Vector3 p1, [In] ref Vector3 p2, [In] ref Vector3 p3, IntPtr triangle_plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_copy_with_margin(IntPtr obj, IntPtr other, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_find_intersection(IntPtr obj, IntPtr other, IntPtr intersection);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_get_center_extend(IntPtr obj, [In] ref Vector3 center, [In] ref Vector3 extend);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_getMax(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_getMin(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btAABB_has_collision(IntPtr obj, IntPtr other);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_increment_margin(IntPtr obj, float margin);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_invalidate(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_merge(IntPtr obj, IntPtr box);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btAABB_overlapping_trans_cache(IntPtr obj, IntPtr box, IntPtr transcache, bool fulltest);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btAABB_overlapping_trans_conservative(IntPtr obj, IntPtr box, [In] ref Matrix trans1_to_0);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btAABB_overlapping_trans_conservative2(IntPtr obj, IntPtr box, IntPtr trans1_to_0);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern eBT_PLANE_INTERSECTION_TYPE btAABB_plane_classify(IntPtr obj, IntPtr plane);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_projection_interval(IntPtr obj, [In] ref Vector3 direction, IntPtr vmin, IntPtr vmax);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_setMax(IntPtr obj, Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_setMin(IntPtr obj, Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAABB_delete(IntPtr obj);
	}
}
