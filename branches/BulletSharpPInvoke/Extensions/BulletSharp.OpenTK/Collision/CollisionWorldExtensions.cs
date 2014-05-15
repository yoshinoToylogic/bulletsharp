using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CollisionWorldExtensions
	{
		public unsafe static void ConvexSweepTest(this CollisionWorld obj, ConvexShape castShape, ref OpenTK.Matrix4 from, ref OpenTK.Matrix4 to, ConvexResultCallback resultCallback, float allowedCcdPenetration)
		{
			fixed (OpenTK.Matrix4* fromPtr = &from)
			{
				fixed (OpenTK.Matrix4* toPtr = &to)
				{
					obj.ConvexSweepTest(castShape, ref *(BulletSharp.Math.Matrix*)fromPtr, ref *(BulletSharp.Math.Matrix*)toPtr, resultCallback, allowedCcdPenetration);
				}
			}
		}

		public unsafe static void ConvexSweepTest(this CollisionWorld obj, ConvexShape castShape, ref OpenTK.Matrix4 from, ref OpenTK.Matrix4 to, ConvexResultCallback resultCallback)
		{
			fixed (OpenTK.Matrix4* fromPtr = &from)
			{
				fixed (OpenTK.Matrix4* toPtr = &to)
				{
					obj.ConvexSweepTest(castShape, ref *(BulletSharp.Math.Matrix*)fromPtr, ref *(BulletSharp.Math.Matrix*)toPtr, resultCallback);
				}
			}
		}

		public unsafe static void DebugDrawObject(this CollisionWorld obj, ref OpenTK.Matrix4 worldTransform, CollisionShape shape, ref OpenTK.Vector3 color)
		{
			fixed (OpenTK.Matrix4* worldTransformPtr = &worldTransform)
			{
				fixed (OpenTK.Vector3* colorPtr = &color)
				{
					obj.DebugDrawObject(ref *(BulletSharp.Math.Matrix*)worldTransformPtr, shape, ref *(BulletSharp.Math.Vector3*)colorPtr);
				}
			}
		}

		public unsafe static void ObjectQuerySingle(this CollisionWorld obj, ConvexShape castShape, ref OpenTK.Matrix4 rayFromTrans, ref OpenTK.Matrix4 rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape, ref OpenTK.Matrix4 colObjWorldTransform, ConvexResultCallback resultCallback, float allowedPenetration)
		{
			fixed (OpenTK.Matrix4* rayFromTransPtr = &rayFromTrans)
			{
				fixed (OpenTK.Matrix4* rayToTransPtr = &rayToTrans)
				{
					fixed (OpenTK.Matrix4* colObjWorldTransformPtr = &colObjWorldTransform)
					{
						obj.ObjectQuerySingle(castShape, ref *(BulletSharp.Math.Matrix*)rayFromTransPtr, ref *(BulletSharp.Math.Matrix*)rayToTransPtr, collisionObject, collisionShape, ref *(BulletSharp.Math.Matrix*)colObjWorldTransformPtr, resultCallback, allowedPenetration);
					}
				}
			}
		}

		public unsafe static void ObjectQuerySingleInternal(this CollisionWorld obj, ConvexShape castShape, ref OpenTK.Matrix4 convexFromTrans, ref OpenTK.Matrix4 convexToTrans, CollisionObjectWrapper colObjWrap, ConvexResultCallback resultCallback, float allowedPenetration)
		{
			fixed (OpenTK.Matrix4* convexFromTransPtr = &convexFromTrans)
			{
				fixed (OpenTK.Matrix4* convexToTransPtr = &convexToTrans)
				{
					obj.ObjectQuerySingleInternal(castShape, ref *(BulletSharp.Math.Matrix*)convexFromTransPtr, ref *(BulletSharp.Math.Matrix*)convexToTransPtr, colObjWrap, resultCallback, allowedPenetration);
				}
			}
		}

		public unsafe static void RayTest(this CollisionWorld obj, ref OpenTK.Vector3 rayFromWorld, ref OpenTK.Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			fixed (OpenTK.Vector3* rayFromWorldPtr = &rayFromWorld)
			{
				fixed (OpenTK.Vector3* rayToWorldPtr = &rayToWorld)
				{
					obj.RayTest(ref *(BulletSharp.Math.Vector3*)rayFromWorldPtr, ref *(BulletSharp.Math.Vector3*)rayToWorldPtr, resultCallback);
				}
			}
		}

		public unsafe static void RayTestSingle(this CollisionWorld obj, ref OpenTK.Matrix4 rayFromTrans, ref OpenTK.Matrix4 rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape, ref OpenTK.Matrix4 colObjWorldTransform, RayResultCallback resultCallback)
		{
			fixed (OpenTK.Matrix4* rayFromTransPtr = &rayFromTrans)
			{
				fixed (OpenTK.Matrix4* rayToTransPtr = &rayToTrans)
				{
					fixed (OpenTK.Matrix4* colObjWorldTransformPtr = &colObjWorldTransform)
					{
						obj.RayTestSingle(ref *(BulletSharp.Math.Matrix*)rayFromTransPtr, ref *(BulletSharp.Math.Matrix*)rayToTransPtr, collisionObject, collisionShape, ref *(BulletSharp.Math.Matrix*)colObjWorldTransformPtr, resultCallback);
					}
				}
			}
		}

		public unsafe static void RayTestSingleInternal(this CollisionWorld obj, ref OpenTK.Matrix4 rayFromTrans, ref OpenTK.Matrix4 rayToTrans, CollisionObjectWrapper collisionObjectWrap, RayResultCallback resultCallback)
		{
			fixed (OpenTK.Matrix4* rayFromTransPtr = &rayFromTrans)
			{
				fixed (OpenTK.Matrix4* rayToTransPtr = &rayToTrans)
				{
					obj.RayTestSingleInternal(ref *(BulletSharp.Math.Matrix*)rayFromTransPtr, ref *(BulletSharp.Math.Matrix*)rayToTransPtr, collisionObjectWrap, resultCallback);
				}
			}
		}
	}
}
