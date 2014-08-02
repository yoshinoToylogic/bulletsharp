using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;
using BulletSharp.Math;

namespace BulletSharp
{
	public class WorldImporter
	{
        private DynamicsWorld _dynamicsWorld;

        protected List<OptimizedBvh> _allocatedBvhs = new List<OptimizedBvh>();
        protected List<CollisionShape> _allocatedCollisionShapes = new List<CollisionShape>();
        protected List<TypedConstraint> _allocatedConstraints = new List<TypedConstraint>();
        protected List<RigidBody> _allocatedRigidBodies = new List<RigidBody>();
        protected List<TriangleIndexVertexArray> _allocatedTriangleIndexArrays = new List<TriangleIndexVertexArray>();
        protected List<TriangleInfoMap> _allocatedTriangleInfoMaps = new List<TriangleInfoMap>();

        protected List<CollisionObject> _bodyMap = new List<CollisionObject>();
        protected Dictionary<long, CollisionShape> _shapeMap = new Dictionary<long, CollisionShape>();
        protected FileVerboseMode _verboseMode;

		public WorldImporter(DynamicsWorld world)
		{
            _dynamicsWorld = world;
		}

        protected CollisionShape ConvertCollisionShape(byte[] shapeData, Dictionary<long, byte[]> libPointers)
        {
            CollisionShape shape = null;
            MemoryStream stream = new MemoryStream(shapeData, false);
            BulletReader reader = new BulletReader(stream);

            BroadphaseNativeType type = (BroadphaseNativeType) reader.ReadInt32(IntPtr.Size);
            stream.Position += 4; // m_padding
            switch (type)
            {
                case BroadphaseNativeType.StaticPlane:
                    {
                        Vector3 localScaling = reader.ReadVector3();
                        Vector3 planeNormal = reader.ReadVector3();
                        float planeConstant = reader.ReadSingle();
                        shape = CreatePlaneShape(ref planeNormal, planeConstant);
                        shape.LocalScaling = localScaling;
                        break;
                    }
                case BroadphaseNativeType.CompoundShape:
                    {
                        long childShapesPtr = reader.ReadPtr();
                        byte[] childShapes = libPointers[childShapesPtr];
                        int numChildShapes = reader.ReadInt32();
                        //float collisionMargin = reader.ReadInt32();
                        CompoundShape compoundShape = CreateCompoundShape();
                        using (MemoryStream shapeStream = new MemoryStream(childShapes))
                        {
                            using (BulletReader shapeReader = new BulletReader(shapeStream))
                            {
                                for (int i = 0; i < numChildShapes; i++)
                                {
                                    Matrix localTransform = shapeReader.ReadMatrix();
                                    long childShapePtr = shapeReader.ReadPtr();
                                    int childShapeType = shapeReader.ReadInt32();
                                    float childMargin = shapeReader.ReadSingle();
                                    CollisionShape childShape = ConvertCollisionShape(libPointers[childShapePtr], libPointers);
                                    compoundShape.AddChildShape(localTransform, childShape);
                                }
                            }
                        }
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
                        Vector3 localScaling = reader.ReadVector3();
                        Vector3 implicitShapeDimensions = reader.ReadVector3();
                        float collisionMargin = reader.ReadSingle();
                        stream.Position += 4; // m_padding
                        switch (type)
                        {
                            case BroadphaseNativeType.BoxShape:
                                {
                                    Vector3 boxExtents = implicitShapeDimensions / localScaling + new Vector3(collisionMargin);
                                    BoxShape box = CreateBoxShape(ref boxExtents) as BoxShape;
                                    //box.InitializePolyhedralFeatures();
                                    shape = box;
                                    break;
                                }
                            case BroadphaseNativeType.ConvexHullShape:
                                {
                                    long unscaledPointsFloatPtr = reader.ReadPtr();
                                    long unscaledPointsDoublePtr = reader.ReadPtr();
                                    int numPoints = reader.ReadInt32();
                                    
                                    byte[] points = libPointers[unscaledPointsFloatPtr];
                                    ConvexHullShape hullShape = CreateConvexHullShape();
                                    using (MemoryStream pointStream = new MemoryStream(points))
                                    {
                                        using (BulletReader pointReader = new BulletReader(pointStream))
                                        {
                                            for (int i = 0; i < numPoints; i++)
                                            {
                                                hullShape.AddPoint(pointReader.ReadVector3());
                                            }
                                        }
                                    }
                                    hullShape.Margin = collisionMargin;
                                    //hullShape.InitializePolyhedralFeatures();
                                    shape = hullShape;
                                    break;
                                }
                            case BroadphaseNativeType.MultiSphereShape:
                                {
                                    long localPositionArrayPtr = reader.ReadPtr();
                                    byte[] localPositionArray = libPointers[localPositionArrayPtr];
                                    int localPositionArraySize = reader.ReadInt32();
                                    Vector3[] positions = new Vector3[localPositionArraySize];
                                    float[] radi = new float[localPositionArraySize];
                                    using (MemoryStream localPositionArrayStream = new MemoryStream(localPositionArray))
                                    {
                                        using (BulletReader localPositionArrayReader = new BulletReader(localPositionArrayStream))
                                        {
                                            for (int i = 0; i < localPositionArraySize; i++)
                                            {
                                                positions[i] = localPositionArrayReader.ReadVector3();
                                                radi[i] = localPositionArrayReader.ReadSingle();
                                            }
                                        }
                                    }
                                    shape = CreateMultiSphereShape(positions, radi);
                                    break;
                                }
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
            BulletReader reader = new BulletReader(stream);

            //long broadphaseHandlePtr = reader.ReadPtr();
            long collisionShapePtr = reader.ReadPtr(IntPtr.Size);
            Matrix startTransform = reader.ReadMatrix(IntPtr.Size * 4);
            float inverseMass = reader.ReadSingle((IntPtr.Size == 8) ? 464 : 448);

            float mass = inverseMass.Equals(0) ? 0 : 1.0f / inverseMass;
            CollisionShape shape = _shapeMap[collisionShapePtr];

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
            RigidBody body = CreateRigidBody(isDynamic, mass, ref startTransform, shape, name);
            _bodyMap.Add(body);

            reader.Dispose();
            stream.Dispose();
        }

		public CollisionShape CreateBoxShape(ref Vector3 halfExtents)
		{
            BoxShape shape = new BoxShape(halfExtents);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}
        /*
		public BvhTriangleMeshShape CreateBvhTriangleMeshShape(StridingMeshInterface trimesh, OptimizedBvh bvh)
		{
			return btWorldImporter_createBvhTriangleMeshShape(_native, trimesh._native, bvh._native);
		}
        */
		public CollisionShape CreateCapsuleShapeZ(float radius, float height)
		{
            CapsuleShapeZ shape = new CapsuleShapeZ(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCapsuleShapeX(float radius, float height)
		{
            CapsuleShapeX shape = new CapsuleShapeX(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCapsuleShapeY(float radius, float height)
		{
            CapsuleShape shape = new CapsuleShape(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionObject CreateCollisionObject(ref Matrix startTransform, CollisionShape shape, string bodyName)
		{
            return CreateRigidBody(false, 0, ref startTransform, shape, bodyName);
		}

		public CompoundShape CreateCompoundShape()
		{
            CompoundShape shape = new CompoundShape();
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateConeShapeZ(float radius, float height)
		{
			ConeShape shape = new ConeShapeZ(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateConeShapeX(float radius, float height)
		{
			ConeShape shape = new ConeShapeX(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateConeShapeY(float radius, float height)
		{
			ConeShape shape = new ConeShape(radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public ConeTwistConstraint CreateConeTwistConstraint(RigidBody rbA, ref Matrix rbAFrame)
		{
            ConeTwistConstraint constraint = new ConeTwistConstraint(rbA, ref rbAFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public ConeTwistConstraint CreateConeTwistConstraint(RigidBody rbA, RigidBody rbB, ref Matrix rbAFrame, ref Matrix rbBFrame)
		{
            ConeTwistConstraint constraint = new ConeTwistConstraint(rbA, rbB, ref rbAFrame, ref rbBFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
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
            CylinderShapeZ shape = new CylinderShapeZ(radius, radius, height);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCylinderShapeX(float radius, float height)
		{
            CylinderShapeX shape = new CylinderShapeX(height, radius, radius);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public CollisionShape CreateCylinderShapeY(float radius, float height)
		{
            CylinderShape shape = new CylinderShape(radius, height, radius);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public GearConstraint CreateGearConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 axisInA, ref Vector3 axisInB, float ratio)
		{
			GearConstraint constraint = new GearConstraint(rbA, rbB, ref axisInA, ref axisInB, ratio);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Generic6DofConstraint CreateGeneric6DofConstraint(RigidBody rbB, ref Matrix frameInB, bool useLinearReferenceFrameB)
		{
			Generic6DofConstraint constraint = new Generic6DofConstraint(rbB, frameInB, useLinearReferenceFrameB);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Generic6DofConstraint CreateGeneric6DofConstraint(RigidBody rbA, RigidBody rbB, ref Matrix frameInA, ref Matrix frameInB, bool useLinearReferenceFrameA)
		{
			Generic6DofConstraint constraint = new Generic6DofConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Generic6DofSpringConstraint CreateGeneric6DofSpringConstraint(RigidBody rbA, RigidBody rbB, ref Matrix frameInA, ref Matrix frameInB, bool useLinearReferenceFrameA)
		{
			Generic6DofSpringConstraint constraint = new Generic6DofSpringConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public GImpactMeshShape CreateGimpactShape(StridingMeshInterface trimesh)
		{
            GImpactMeshShape shape = new GImpactMeshShape(trimesh._native);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public HingeConstraint CreateHingeConstraint(RigidBody rbA, RigidBody rbB, ref Matrix rbAFrame, ref Matrix rbBFrame, bool useReferenceFrameA)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, rbB, ref rbAFrame, ref rbBFrame, useReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, RigidBody rbB, ref Matrix rbAFrame, ref Matrix rbBFrame)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, rbB, ref rbAFrame, ref rbBFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, ref Matrix rbAFrame, bool useReferenceFrameA)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, ref rbAFrame, useReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

        public HingeConstraint CreateHingeConstraint(RigidBody rbA, ref Matrix rbAFrame)
		{
            HingeConstraint constraint = new HingeConstraint(rbA, ref rbAFrame);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}
        /*
		public TriangleIndexVertexArray CreateMeshInterface(StridingMeshInterfaceData meshData)
		{
			return btWorldImporter_createMeshInterface(_native, meshData._native);
		}
        */
		public MultiSphereShape CreateMultiSphereShape(Vector3[] positions, float[] radi)
		{
			MultiSphereShape shape = new MultiSphereShape(positions, radi);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}
        
		public OptimizedBvh CreateOptimizedBvh()
		{
			OptimizedBvh bvh = new OptimizedBvh();
            _allocatedBvhs.Add(bvh);
            return bvh;
		}
        
		public CollisionShape CreatePlaneShape(ref Vector3 planeNormal, float planeConstant)
		{
            StaticPlaneShape shape = new StaticPlaneShape(planeNormal, planeConstant);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public Point2PointConstraint CreatePoint2PointConstraint(RigidBody rbA, RigidBody rbB, ref Vector3 pivotInA, ref Vector3 pivotInB)
		{
            Point2PointConstraint constraint = new Point2PointConstraint(rbA, rbB, ref pivotInA, ref pivotInB);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public Point2PointConstraint CreatePoint2PointConstraint(RigidBody rbA, ref Vector3 pivotInA)
		{
			Point2PointConstraint constraint = new Point2PointConstraint(rbA, ref pivotInA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public virtual RigidBody CreateRigidBody(bool isDynamic, float mass, ref Matrix startTransform, CollisionShape shape, string bodyName)
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

		public ScaledBvhTriangleMeshShape CreateScaledTrangleMeshShape(BvhTriangleMeshShape meshShape, ref Vector3 localScalingbtBvhTriangleMeshShape)
		{
            ScaledBvhTriangleMeshShape shape = new ScaledBvhTriangleMeshShape(meshShape, localScalingbtBvhTriangleMeshShape);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}

		public SliderConstraint CreateSliderConstraint(RigidBody rbB, ref Matrix frameInB, bool useLinearReferenceFrameA)
		{
			SliderConstraint constraint = new SliderConstraint(rbB, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public SliderConstraint CreateSliderConstraint(RigidBody rbA, RigidBody rbB, ref Matrix frameInA, ref Matrix frameInB, bool useLinearReferenceFrameA)
		{
            SliderConstraint constraint = new SliderConstraint(rbA, rbB, frameInA, frameInB, useLinearReferenceFrameA);
            _allocatedConstraints.Add(constraint);
            return constraint;
		}

		public CollisionShape CreateSphereShape(float radius)
		{
			SphereShape shape = new SphereShape(radius);
            _allocatedCollisionShapes.Add(shape);
            return shape;
		}
        /*
		public StridingMeshInterfaceData CreateStridingMeshInterfaceData(StridingMeshInterfaceData interfaceData)
		{
			return btWorldImporter_createStridingMeshInterfaceData(_native, interfaceData._native);
		}
        */
		public TriangleInfoMap CreateTriangleInfoMap()
		{
            TriangleInfoMap tim = new TriangleInfoMap();
            _allocatedTriangleInfoMaps.Add(tim);
            return tim;
		}

        public TriangleIndexVertexArray CreateTriangleMeshContainer()
		{
            TriangleIndexVertexArray tiva = new TriangleIndexVertexArray();
            _allocatedTriangleIndexArrays.Add(tiva);
            return tiva;
		}

		public void DeleteAllData()
		{
            foreach (TypedConstraint constraint in _allocatedConstraints)
            {
                if (_dynamicsWorld != null)
                {
                    _dynamicsWorld.RemoveConstraint(constraint);
                }
                constraint.Dispose();
            }
            _allocatedConstraints.Clear();

            foreach (RigidBody rigidBody in _allocatedRigidBodies)
            {
                if (_dynamicsWorld != null)
                {
                    _dynamicsWorld.RemoveRigidBody(rigidBody);
                }
                rigidBody.Dispose();
            }
            _allocatedRigidBodies.Clear();

            foreach (CollisionShape shape in _allocatedCollisionShapes)
            {
                shape.Dispose();
            }
            _allocatedCollisionShapes.Clear();

            foreach (OptimizedBvh bvh in _allocatedBvhs)
            {
                bvh.Dispose();
            }
            _allocatedBvhs.Clear();
        
            foreach (TriangleInfoMap tim in _allocatedTriangleInfoMaps)
            {
                tim.Dispose();
            }
            _allocatedTriangleInfoMaps.Clear();

            foreach (TriangleIndexVertexArray tiva in _allocatedTriangleIndexArrays)
            {
                tiva.Dispose();
            }
            _allocatedTriangleIndexArrays.Clear();

            //TODO: _allocatedbtStridingMeshInterfaceDatas
		}
        
		public OptimizedBvh GetBvhByIndex(int index)
		{
            return _allocatedBvhs[index];
		}
        
		public CollisionShape GetCollisionShapeByIndex(int index)
		{
            return _allocatedCollisionShapes[index];
		}

		public CollisionShape GetCollisionShapeByName(string name)
		{
            throw new NotImplementedException();
		}

		public TypedConstraint GetConstraintByIndex(int index)
		{
            return _allocatedConstraints[index];
		}

		public TypedConstraint GetConstraintByName(string name)
		{
            throw new NotImplementedException();
		}

		public string GetNameForPointer(IntPtr ptr)
		{
            throw new NotImplementedException();
		}

		public CollisionObject GetRigidBodyByIndex(int index)
		{
            return _allocatedRigidBodies[index];
		}

		public RigidBody GetRigidBodyByName(string name)
		{
            throw new NotImplementedException();
		}
        
		public TriangleInfoMap GetTriangleInfoMapByIndex(int index)
		{
            return _allocatedTriangleInfoMaps[index];
		}
        
		public void SetDynamicsWorldInfo(ref Vector3 gravity, ContactSolverInfo solverInfo)
		{
            if (_dynamicsWorld != null)
            {
                _dynamicsWorld.SetGravity(ref gravity);
            }
		}

		public int NumBvhs
		{
            get { return _allocatedBvhs.Count; }
		}

		public int NumCollisionShapes
		{
            get { return _allocatedCollisionShapes.Count; }
		}

		public int NumConstraints
		{
			get { return _allocatedConstraints.Count; }
		}

		public int NumRigidBodies
		{
            get { return _allocatedRigidBodies.Count; }
		}

		public int NumTriangleInfoMaps
		{
            get { return _allocatedTriangleInfoMaps.Count; }
		}

        public FileVerboseMode VerboseMode
        {
            get { return _verboseMode; }
            set { _verboseMode = value; }
        }
	}
}
