using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;

namespace BulletSharp
{
	public class WorldImporter
	{
        private DynamicsWorld _dynamicsWorld;

        protected List<CollisionShape> _allocatedCollisionShapes = new List<CollisionShape>();
        protected List<RigidBody> _allocatedRigidBodies = new List<RigidBody>();

        protected List<CollisionObject> _bodyMap = new List<CollisionObject>();
        protected Dictionary<long, CollisionShape> _shapeMap = new Dictionary<long, CollisionShape>();
        protected FileVerboseMode _verboseMode;

		internal IntPtr _native;

		internal WorldImporter(IntPtr native)
		{
			_native = native;
		}

		public WorldImporter(DynamicsWorld world)
		{
            _dynamicsWorld = world;
		}

        private float ReadFloat(BinaryReader reader, int position)
        {
            reader.BaseStream.Position = position;
            return reader.ReadSingle();
        }

        private int ReadInt32(BinaryReader reader, int position)
        {
            reader.BaseStream.Position = position;
            return reader.ReadInt32();
        }

        private long ReadInt64(BinaryReader reader, int position)
        {
            reader.BaseStream.Position = position;
            return reader.ReadInt64();
        }

        private Matrix ReadMatrix(BinaryReader reader)
        {
            float[] m = new float[16];
            for (int i = 0; i < 16; i++)
            {
                m[i] = reader.ReadSingle();
            }
            /* Transpose
            m[0] = reader.ReadSingle();
            m[4] = reader.ReadSingle();
            m[8] = reader.ReadSingle();
            m[12] = reader.ReadSingle();
            m[1] = reader.ReadSingle();
            m[5] = reader.ReadSingle();
            m[9] = reader.ReadSingle();
            m[13] = reader.ReadSingle();
            m[2] = reader.ReadSingle();
            m[6] = reader.ReadSingle();
            m[10] = reader.ReadSingle();
            m[14] = reader.ReadSingle();
            m[3] = reader.ReadSingle();
            m[7] = reader.ReadSingle();
            m[11] = reader.ReadSingle();
            m[15] = reader.ReadSingle();
            */
            return new Matrix(m);
        }

        private Matrix ReadMatrix(BinaryReader reader, int position)
        {
            reader.BaseStream.Position = position;
            return ReadMatrix(reader);
        }

        private Vector3 ReadVector3(BinaryReader reader, int position)
        {
            reader.BaseStream.Position = position;
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            float z = reader.ReadSingle();
            return new Vector3(x, y, z);
        }

        protected CollisionShape ConvertCollisionShape(byte[] shapeData)
        {
            CollisionShape shape = null;
            MemoryStream stream = new MemoryStream(shapeData, false);
            BinaryReader reader = new BinaryReader(stream);

            BroadphaseNativeType type = (BroadphaseNativeType) ReadInt32(reader, 8);
            switch (type)
            {
                case BroadphaseNativeType.StaticPlane:
                    {
                        Vector3 planeNormal = ReadVector3(reader, 32);
                        float planeConstant = ReadFloat(reader, 48);
                        shape = CreatePlaneShape(planeNormal, planeConstant);
                        shape.LocalScaling = ReadVector3(reader, 16);
                        break;
                    }
                case BroadphaseNativeType.CompoundShape:
                    {
                        CompoundShape compoundShape = CreateCompoundShape();
                        shape = compoundShape;
                        break;
                    }
                case BroadphaseNativeType.CylinderShape:
                case BroadphaseNativeType.ConeShape:
                case BroadphaseNativeType.CapsuleShape:
                case BroadphaseNativeType.BoxShape:
                case BroadphaseNativeType.SphereShape:
                case BroadphaseNativeType.MultiSphereShape:
                case BroadphaseNativeType.ConvexHullShape:
                    {
                        Vector3 localScaling = ReadVector3(reader, 16);
                        Vector3 implicitShapeDimensions = ReadVector3(reader, 32);
                        float collisionMargin = ReadFloat(reader, 48);
                        switch (type)
                        {
                            case BroadphaseNativeType.ConvexHullShape:
                                int numPoints = ReadInt32(reader, 64);
                                long unscaledPointsFloatPtr = ReadInt64(reader, 52);
                                unscaledPointsFloatPtr.ToString();
                                ConvexHullShape hullShape = CreateConvexHullShape();
                                hullShape.Margin = collisionMargin;
                                shape = hullShape;
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                        break;
                    }
                default:
                    throw new NotImplementedException();
            }

            reader.Dispose();
            stream.Dispose();
            return shape;
        }

        protected void ConvertRigidBodyFloat(byte[] bodyData)
        {
            MemoryStream stream = new MemoryStream(bodyData, false);
            BinaryReader reader = new BinaryReader(stream);
            /*
            float[] f = new float[body.Length / 4];
            int i = 0;
            while (true)
            {
                f[i] = reader.ReadSingle();
                i++;
            }
            */

            Matrix startTransform = ReadMatrix(reader, 32);
            startTransform.ToString();
            float inverseMass = ReadFloat(reader, 464);
            float mass = inverseMass.Equals(0) ? 0 : 1.0f / inverseMass;

            long collisionShape = ReadInt64(reader, 8);
            CollisionShape shape = _shapeMap[collisionShape];

            if (shape.IsNonMoving)
            {
                mass = 0.0f;
            }
            bool isDynamic;
            Vector3 localInertia;
            if (mass != 0.0f)
            {
                isDynamic = true;
                shape.CalculateLocalInertia(mass, out localInertia);
            }
            else
            {
                isDynamic = false;
                localInertia = Vector3.Zero;
            }
            string name = null;
            RigidBody body = CreateRigidBody(isDynamic, mass, startTransform, shape, name);
            _bodyMap.Add(body);

            reader.Dispose();
            stream.Dispose();
        }

		public CollisionShape CreateBoxShape(Vector3 halfExtents)
		{
			return new BoxShape(btWorldImporter_createBoxShape(_native, ref halfExtents));
		}
        /*
		public BvhTriangleMeshShape CreateBvhTriangleMeshShape(StridingMeshInterface trimesh, OptimizedBvh bvh)
		{
			return btWorldImporter_createBvhTriangleMeshShape(_native, trimesh._native, bvh._native);
		}
        */
		public CollisionShape CreateCapsuleShapeZ(float radius, float height)
		{
            return new CapsuleShapeZ(btWorldImporter_createCapsuleShapeZ(_native, radius, height));
		}

		public CollisionShape CreateCapsuleShapeX(float radius, float height)
		{
            return new CapsuleShapeX(btWorldImporter_createCapsuleShapeX(_native, radius, height));
		}

		public CollisionShape CreateCapsuleShapeY(float radius, float height)
		{
            return new CapsuleShape(btWorldImporter_createCapsuleShapeY(_native, radius, height));
		}

		public CollisionObject CreateCollisionObject(Matrix startTransform, CollisionShape shape, string bodyName)
		{
			return CollisionObject.GetManaged(btWorldImporter_createCollisionObject(_native, ref startTransform, shape._native, bodyName));
		}

		public CompoundShape CreateCompoundShape()
		{
            CompoundShape shape = new CompoundShape();
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateConeShapeZ(float radius, float height)
		{
			return new ConeShapeZ(radius, height);
		}

		public CollisionShape CreateConeShapeX(float radius, float height)
		{
			return new ConeShapeX(radius, height);
		}

		public CollisionShape CreateConeShapeY(float radius, float height)
		{
			return new ConeShape(radius, height);
		}

		public ConeTwistConstraint CreateConeTwistConstraint(RigidBody rbA, Matrix rbAFrame)
		{
            return new ConeTwistConstraint(btWorldImporter_createConeTwistConstraint(_native, rbA._native, ref rbAFrame));
		}

		public ConeTwistConstraint CreateConeTwistConstraint(RigidBody rbA, RigidBody rbB, Matrix rbAFrame, Matrix rbBFrame)
		{
            return new ConeTwistConstraint(btWorldImporter_createConeTwistConstraint2(_native, rbA._native, rbB._native, ref rbAFrame, ref rbBFrame));
		}

		public ConvexHullShape CreateConvexHullShape()
		{
            ConvexHullShape shape = new ConvexHullShape();
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}
        /*
		public CollisionShape CreateConvexTriangleMeshShape(StridingMeshInterface trimesh)
		{
			return btWorldImporter_createConvexTriangleMeshShape(_native, trimesh._native);
		}
        */
		public CollisionShape CreateCylinderShapeZ(float radius, float height)
		{
            return new CylinderShapeZ(btWorldImporter_createCylinderShapeZ(_native, radius, height));
		}

		public CollisionShape CreateCylinderShapeX(float radius, float height)
		{
            return new CylinderShapeX(btWorldImporter_createCylinderShapeX(_native, radius, height));
		}

		public CollisionShape CreateCylinderShapeY(float radius, float height)
		{
            return new CylinderShape(btWorldImporter_createCylinderShapeY(_native, radius, height));
		}

		public GearConstraint CreateGearConstraint(RigidBody rbA, RigidBody rbB, Vector3 axisInA, Vector3 axisInB, float ratio)
		{
			return new GearConstraint(rbA, rbB, ref axisInA, ref axisInB, ratio);
		}

		public Generic6DofConstraint CreateGeneric6DofConstraint(RigidBody rbB, Matrix frameInB, bool useLinearReferenceFrameB)
		{
			return new Generic6DofConstraint(rbB, frameInB, useLinearReferenceFrameB);
		}

		public Generic6DofConstraint CreateGeneric6DofConstraint(RigidBody rbA, RigidBody rbB, Matrix frameInA, Matrix frameInB, bool useLinearReferenceFrameA)
		{
			return new Generic6DofConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
		}

		public Generic6DofSpringConstraint CreateGeneric6DofSpringConstraint(RigidBody rbA, RigidBody rbB, Matrix frameInA, Matrix frameInB, bool useLinearReferenceFrameA)
		{
			return new Generic6DofSpringConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
		}

		public GImpactMeshShape CreateGimpactShape(StridingMeshInterface trimesh)
		{
            return new GImpactMeshShape(trimesh._native);
		}

		public HingeConstraint CreateHingeConstraint(RigidBody rbA, RigidBody rbB, Matrix rbAFrame, Matrix rbBFrame, bool useReferenceFrameA)
		{
            return new HingeConstraint(btWorldImporter_createHingeConstraint(_native, rbA._native, rbB._native, ref rbAFrame, ref rbBFrame, useReferenceFrameA));
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, RigidBody rbB, Matrix rbAFrame, Matrix rbBFrame)
		{
			return new HingeConstraint(btWorldImporter_createHingeConstraint2(_native, rbA._native, rbB._native, ref rbAFrame, ref rbBFrame));
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, Matrix rbAFrame, bool useReferenceFrameA)
		{
			return new HingeConstraint(btWorldImporter_createHingeConstraint3(_native, rbA._native, ref rbAFrame, useReferenceFrameA));
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, Matrix rbAFrame)
		{
			return new HingeConstraint(btWorldImporter_createHingeConstraint4(_native, rbA._native, ref rbAFrame));
		}
        /*
		public TriangleIndexVertexArray CreateMeshInterface(StridingMeshInterfaceData meshData)
		{
			return btWorldImporter_createMeshInterface(_native, meshData._native);
		}

		public MultiSphereShape CreateMultiSphereShape(Vector3 positions, float radi, int numSpheres)
		{
			return new MultiSphereShape(_native, ref positions, radi._native, numSpheres);
		}

		public OptimizedBvh CreateOptimizedBvh()
		{
			return new OptimizedBvh(_native);
		}
        */
		public CollisionShape CreatePlaneShape(Vector3 planeNormal, float planeConstant)
		{
            StaticPlaneShape shape = new StaticPlaneShape(planeNormal, planeConstant);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public Point2PointConstraint CreatePoint2PointConstraint(RigidBody rbA, RigidBody rbB, Vector3 pivotInA, Vector3 pivotInB)
		{
            return new Point2PointConstraint(btWorldImporter_createPoint2PointConstraint(_native, rbA._native, rbB._native, ref pivotInA, ref pivotInB));
		}

		public Point2PointConstraint CreatePoint2PointConstraint(RigidBody rbA, Vector3 pivotInA)
		{
			return new Point2PointConstraint(btWorldImporter_createPoint2PointConstraint2(_native, rbA._native, ref pivotInA));
		}

		public virtual RigidBody CreateRigidBody(bool isDynamic, float mass, Matrix startTransform, CollisionShape shape, string bodyName)
		{
            Vector3 localInertia;
            if (mass != 0.0f)
            {
                shape.CalculateLocalInertia(mass, out localInertia);
            }
            else
            {
                localInertia = Vector3.Zero;
            }

            RigidBodyConstructionInfo info = new RigidBodyConstructionInfo(mass, null, shape, localInertia);
            RigidBody body = new RigidBody(info);
            info.Dispose();
            body.WorldTransform = startTransform;

            if (_dynamicsWorld != null)
            {
                _dynamicsWorld.AddRigidBody(body);
            }

            if (bodyName != null)
            {
                //_objectNameMap.insert(body, bodyName);
                //_nameBodyMap.insert(bodyName, body);
            }
            _allocatedRigidBodies.Add(body);
            return body;
		}

		public ScaledBvhTriangleMeshShape CreateScaledTrangleMeshShape(BvhTriangleMeshShape meshShape, Vector3 localScalingbtBvhTriangleMeshShape)
		{
			return new ScaledBvhTriangleMeshShape(meshShape, localScalingbtBvhTriangleMeshShape);
		}

		public SliderConstraint CreateSliderConstraint(RigidBody rbB, Matrix frameInB, bool useLinearReferenceFrameA)
		{
			return new SliderConstraint(rbB, frameInB, useLinearReferenceFrameA);
		}

		public SliderConstraint CreateSliderConstraint(RigidBody rbA, RigidBody rbB, Matrix frameInA, Matrix frameInB, bool useLinearReferenceFrameA)
		{
			return new SliderConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
		}

		public CollisionShape CreateSphereShape(float radius)
		{
			return new SphereShape(radius);
		}
        /*
		public StridingMeshInterfaceData CreateStridingMeshInterfaceData(StridingMeshInterfaceData interfaceData)
		{
			return btWorldImporter_createStridingMeshInterfaceData(_native, interfaceData._native);
		}

		public TriangleInfoMap CreateTriangleInfoMap()
		{
			return btWorldImporter_createTriangleInfoMap(_native);
		}
        */
		public TriangleIndexVertexArray CreateTriangleMeshContainer()
		{
            return new TriangleIndexVertexArray(btWorldImporter_createTriangleMeshContainer(_native));
		}

		public void DeleteAllData()
		{
			btWorldImporter_deleteAllData(_native);
		}
        /*
		public OptimizedBvh GetBvhByIndex(int index)
		{
			return btWorldImporter_getBvhByIndex(_native, index);
		}
        */
		public CollisionShape GetCollisionShapeByIndex(int index)
		{
            return CollisionShape.GetManaged(btWorldImporter_getCollisionShapeByIndex(_native, index));
		}

		public CollisionShape GetCollisionShapeByName(string name)
		{
            return CollisionShape.GetManaged(btWorldImporter_getCollisionShapeByName(_native, name));
		}

		public TypedConstraint GetConstraintByIndex(int index)
		{
            return TypedConstraint.GetManaged(btWorldImporter_getConstraintByIndex(_native, index));
		}

		public TypedConstraint GetConstraintByName(string name)
		{
            return TypedConstraint.GetManaged(btWorldImporter_getConstraintByName(_native, name));
		}

		public string GetNameForPointer(IntPtr ptr)
		{
			return btWorldImporter_getNameForPointer(_native, ptr);
		}

		public CollisionObject GetRigidBodyByIndex(int index)
		{
            return CollisionObject.GetManaged(btWorldImporter_getRigidBodyByIndex(_native, index));
		}

		public RigidBody GetRigidBodyByName(string name)
		{
            return CollisionObject.GetManaged(btWorldImporter_getRigidBodyByName(_native, name)) as RigidBody;
		}
        /*
		public TriangleInfoMap GetTriangleInfoMapByIndex(int index)
		{
			return btWorldImporter_getTriangleInfoMapByIndex(_native, index);
		}
        */
		public void SetDynamicsWorldInfo(Vector3 gravity, ContactSolverInfo solverInfo)
		{
			btWorldImporter_setDynamicsWorldInfo(_native, ref gravity, solverInfo._native);
		}

		public int NumBvhs
		{
			get { return btWorldImporter_getNumBvhs(_native); }
		}

		public int NumCollisionShapes
		{
			get { return btWorldImporter_getNumCollisionShapes(_native); }
		}

		public int NumConstraints
		{
			get { return btWorldImporter_getNumConstraints(_native); }
		}

		public int NumRigidBodies
		{
			get { return btWorldImporter_getNumRigidBodies(_native); }
		}

		public int NumTriangleInfoMaps
		{
			get { return btWorldImporter_getNumTriangleInfoMaps(_native); }
		}

		public int VerboseMode
		{
			get { return btWorldImporter_getVerboseMode(_native); }
			set { btWorldImporter_setVerboseMode(_native, value); }
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
				btWorldImporter_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~WorldImporter()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createBoxShape(IntPtr obj, [In] ref Vector3 halfExtents);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createBvhTriangleMeshShape(IntPtr obj, IntPtr trimesh, IntPtr bvh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createCapsuleShapeZ(IntPtr obj, float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createCapsuleShapeX(IntPtr obj, float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createCapsuleShapeY(IntPtr obj, float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createCollisionObject(IntPtr obj, [In] ref Matrix startTransform, IntPtr shape, string bodyName);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createConeShapeZ(IntPtr obj, float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createConeShapeX(IntPtr obj, float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createConeShapeY(IntPtr obj, float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createConeTwistConstraint(IntPtr obj, IntPtr rbA, [In] ref Matrix rbAFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createConeTwistConstraint2(IntPtr obj, IntPtr rbA, IntPtr rbB, [In] ref Matrix rbAFrame, [In] ref Matrix rbBFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createConvexTriangleMeshShape(IntPtr obj, IntPtr trimesh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createCylinderShapeZ(IntPtr obj, float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createCylinderShapeX(IntPtr obj, float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createCylinderShapeY(IntPtr obj, float radius, float height);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createGearConstraint(IntPtr obj, IntPtr rbA, IntPtr rbB, [In] ref Vector3 axisInA, [In] ref Vector3 axisInB, float ratio);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createGeneric6DofConstraint(IntPtr obj, IntPtr rbB, [In] ref Matrix frameInB, bool useLinearReferenceFrameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createGeneric6DofConstraint2(IntPtr obj, IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, bool useLinearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createGeneric6DofSpringConstraint(IntPtr obj, IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, bool useLinearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createGimpactShape(IntPtr obj, IntPtr trimesh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btWorldImporter_createHingeConstraint(IntPtr obj, IntPtr rbA, IntPtr rbB, [In] ref Matrix rbAFrame, [In] ref Matrix rbBFrame, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btWorldImporter_createHingeConstraint2(IntPtr obj, IntPtr rbA, IntPtr rbB, [In] ref Matrix rbAFrame, [In] ref Matrix rbBFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btWorldImporter_createHingeConstraint3(IntPtr obj, IntPtr rbA, [In] ref Matrix rbAFrame, bool useReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btWorldImporter_createHingeConstraint4(IntPtr obj, IntPtr rbA, [In] ref Matrix rbAFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createMeshInterface(IntPtr obj, IntPtr meshData);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createMultiSphereShape(IntPtr obj, IntPtr positions, IntPtr radi, int numSpheres);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createOptimizedBvh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btWorldImporter_createPoint2PointConstraint(IntPtr obj, IntPtr rbA, IntPtr rbB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btWorldImporter_createPoint2PointConstraint2(IntPtr obj, IntPtr rbA, [In] ref Vector3 pivotInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createScaledTrangleMeshShape(IntPtr obj, IntPtr meshShape, IntPtr localScalingbtBvhTriangleMeshShape);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createSliderConstraint(IntPtr obj, IntPtr rbB, IntPtr frameInB, bool useLinearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btWorldImporter_createSliderConstraint2(IntPtr obj, IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, bool useLinearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createSphereShape(IntPtr obj, float radius);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createStridingMeshInterfaceData(IntPtr obj, IntPtr interfaceData);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createTriangleInfoMap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_createTriangleMeshContainer(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btWorldImporter_deleteAllData(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_getBvhByIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_getCollisionShapeByIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_getCollisionShapeByName(IntPtr obj, string name);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_getConstraintByIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btWorldImporter_getConstraintByName(IntPtr obj, string name);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern string btWorldImporter_getNameForPointer(IntPtr obj, IntPtr ptr);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btWorldImporter_getNumBvhs(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btWorldImporter_getNumCollisionShapes(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btWorldImporter_getNumConstraints(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btWorldImporter_getNumRigidBodies(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btWorldImporter_getNumTriangleInfoMaps(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_getRigidBodyByIndex(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btWorldImporter_getRigidBodyByName(IntPtr obj, string name);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btWorldImporter_getTriangleInfoMapByIndex(IntPtr obj, int index);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern int btWorldImporter_getVerboseMode(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btWorldImporter_setDynamicsWorldInfo(IntPtr obj, [In] ref Vector3 gravity, IntPtr solverInfo);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btWorldImporter_setVerboseMode(IntPtr obj, int verboseMode);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btWorldImporter_delete(IntPtr obj);
	}
}
