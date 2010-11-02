﻿using System;
using BulletSharp;
using BulletSharp.SoftBody;
using DemoFramework;
using SlimDX;

namespace SoftDemo
{
    class Physics : PhysicsContext
    {
        //int numObjects = 1;
        float Scaling = 1;
        //float waveHeight = 5;
        //float triangleHeight=8;
        //float CubeHalfExtents = 1.5f;
        //float extraHeight = -10.0f;

        int demo = 0;
        int numDemos = 20;

        SoftBodyWorldInfo softBodyWorldInfo;

        bool cutting;
        const int maxProxies = 32766;

        SoftRigidDynamicsWorld SoftWorld
        {
            get { return (SoftRigidDynamicsWorld)World; }
        }

        public void NextDemo()
        {
            demo++;
            if (demo >= numDemos)
                demo = 0;

            ResetScene();
            InitializeDemo();
        }

        public void PreviousDemo()
        {
            demo--;
            if (demo < 0)
                demo = numDemos - 1;

            ResetScene();
            InitializeDemo();
        }

        void ResetScene()
        {
            // Don't foreach here, it'll fail.
            while (World.CollisionObjectArray.Count > 0)
            {
                CollisionObject obj = World.CollisionObjectArray[0];

                RigidBody body = RigidBody.Upcast(obj);
                if (body != null && body.MotionState != null)
                    body.MotionState.Dispose();

                SoftBody softBody = SoftBody.Upcast(obj);
                if (softBody != null)
                {
                    SoftWorld.RemoveSoftBody(softBody);
                    softBody.Dispose();
                }
                else
                {
                    if (body != null)
                        World.RemoveRigidBody(body);
                    else
                        World.RemoveCollisionObject(obj);
                }
            }

            while (World.NumConstraints > 0)
            {
                TypedConstraint pc = World.GetConstraint(0);
                World.RemoveConstraint(pc);
                pc.Dispose();
            }
        }

        void InitializeDemo()
        {
            softBodyWorldInfo.SparseSdf.Reset();

            CollisionShape groundShape = new BoxShape(50, 50, 50);
            CollisionShapes.Add(groundShape);
            RigidBody body = LocalCreateRigidBody(0, Matrix.Translation(0, -62, 0), groundShape);
            body.UserObject = "Ground";

            /*
            CollisionShape boxShape = new BoxShape(1, 1, 1);
            CollisionShapes.Add(boxShape);
            LocalCreateRigidBody(1.0f, Matrix.Translation(0, 1, 0), boxShape);
            */

            softBodyWorldInfo.AirDensity = 1.2f;
            softBodyWorldInfo.WaterDensity = 0;
            softBodyWorldInfo.WaterOffset = 0;
            softBodyWorldInfo.WaterNormal = Vector3.Zero;
            softBodyWorldInfo.Gravity = new Vector3(0, -10, 0);

            if (demo == 0)
                Init_Cloth();
            else if (demo == 1)
                Init_Pressure();
            else if (demo == 2)
                Init_Volume();
            else if (demo == 3)
                Init_Ropes();
            else if (demo == 4)
                Init_RopeAttach();
            else if (demo == 5)
                Init_ClothAttach();
            else if (demo == 6)
                Init_Sticks();
            else if (demo == 7)
                Init_Collide();
            else if (demo == 8)
                Init_Collide2();
            else if (demo == 9)
                Init_Collide3();
            else if (demo == 10)
                Init_Impact();
            else if (demo == 11)
                Init_Aero();
            else if (demo == 12)
                Init_Friction();
            else if (demo == 13)
                Init_Torus();
            else if (demo == 14)
                Init_TorusMatch();
            else if (demo == 15)
                Init_Bunny();
            else if (demo == 16)
                Init_BunnyMatch();
            else if (demo == 17)
                Init_Cutting1();
            else if (demo == 18)
                Init_TetraCube();
            else if (demo == 19)
                Init_TetraBunny();
        }

        static Vector3 GetRandomVector(Random random)
        {
            return new Vector3((float)random.NextDouble(),
                    (float)random.NextDouble(), (float)random.NextDouble());
        }

        void CreateStack(CollisionShape boxShape, float halfCubeSize, int size, float zPos)
        {
            Matrix trans;
            float mass = 1;
            for (int i = 0; i < size; i++)
            {
                // This constructs a row, from left to right
                int rowSize = size - i;
                for (int j = 0; j < rowSize; j++)
                {
                    trans = Matrix.Translation(
                        -rowSize * halfCubeSize + halfCubeSize + j * 2.0f * halfCubeSize,
                        halfCubeSize + i * halfCubeSize * 2.0f, zPos);

                    RigidBody body = LocalCreateRigidBody(mass, trans, boxShape);
                }
            }
        }

        SoftBody Create_SoftBox(Vector3 p, Vector3 s)
        {
            Vector3 h = s * 0.5f;
            Vector3[] c = new Vector3[]{
                Vector3.Modulate(h, new Vector3(-1,-1,-1)),
		        Vector3.Modulate(h, new Vector3(+1,-1,-1)),
		        Vector3.Modulate(h, new Vector3(-1,+1,-1)),
		        Vector3.Modulate(h, new Vector3(+1,+1,-1)),
		        Vector3.Modulate(h, new Vector3(-1,-1,+1)),
		        Vector3.Modulate(h, new Vector3(+1,-1,+1)),
		        Vector3.Modulate(h, new Vector3(-1,+1,+1)),
		        Vector3.Modulate(h, new Vector3(+1,+1,+1))};
            SoftBody psb = SoftBodyHelpers.CreateFromConvexHull(softBodyWorldInfo, c);
            psb.GenerateBendingConstraints(2);
            psb.Translate(p);
            SoftWorld.AddSoftBody(psb);

            return (psb);
        }

        SoftBody Create_SoftBoulder(Vector3 p, Vector3 s, int np)
        {
            Random random = new Random();
            Vector3[] pts = new Vector3[np];
            for (int i = 0; i < np; ++i)
                pts[i] = Vector3.Modulate(GetRandomVector(random), s);

            SoftBody psb = SoftBodyHelpers.CreateFromConvexHull(softBodyWorldInfo, pts);
            psb.GenerateBendingConstraints(2);
            psb.Translate(p);
            SoftWorld.AddSoftBody(psb);

            return (psb);
        }

        void Create_RbUpStack(int count)
        {
            float mass = 10.0f;

            CompoundShape cylinderCompound = new CompoundShape();
            CollisionShape cylinderShape = new CylinderShapeX(4, 1, 1);
            CollisionShape boxShape = new BoxShape(4, 1, 1);
            cylinderCompound.AddChildShape(Matrix.Identity, boxShape);
            Quaternion orn = Quaternion.RotationYawPitchRoll((float)Math.PI / 2.0f, 0.0f, 0.0f);
            Matrix localTransform = Matrix.RotationQuaternion(orn);
            //localTransform *= Matrix.Translation(new Vector3(1,1,1));
            cylinderCompound.AddChildShape(localTransform, cylinderShape);

            CollisionShape[] shape = new CollisionShape[]{cylinderCompound,
		        new BoxShape(new Vector3(1,1,1)),
		        new SphereShape(1.5f)};

            for (int i = 0; i < count; ++i)
                LocalCreateRigidBody(mass, Matrix.Translation(0, 2 + 6 * i, 0), shape[i % shape.Length]);
        }

        void Create_BigBall(Vector3 position)
        {
            LocalCreateRigidBody(10.0f, Matrix.Translation(position), new SphereShape(1.5f));
        }

        void Create_BigPlate()
        {
            Vector3 position = new Vector3(0, 4, 0.5f);
            RigidBody body = LocalCreateRigidBody(15.0f, Matrix.Translation(position), new BoxShape(5, 1, 5));
            body.Friction = 1;
        }

        void Init_Pressure()
        {
            SoftBody psb = SoftBodyHelpers.CreateEllipsoid(softBodyWorldInfo, new Vector3(35, 25, 0),
                new Vector3(3, 3, 3), 512);
            psb.Materials[0].Lst = 0.1f;
            psb.Cfg.DF = 1;
            psb.Cfg.DP = 0.001f; // fun factor...
            psb.Cfg.PR = 2500;
            psb.SetTotalMass(30, true);
            SoftWorld.AddSoftBody(psb);

            Create_BigPlate();
            Create_LinearStair(10, Vector3.Zero, new Vector3(2, 1, 5));
        }

        void Init_Ropes()
        {
            const int n = 15;
            for (int i = 0; i < n; i++)
            {
                SoftBody psb = SoftBodyHelpers.CreateRope(softBodyWorldInfo,
                    new Vector3(-10, 0, i * 0.25f),
                    new Vector3(10, 0, i * 0.25f), 16, 1 + 2);
                psb.Cfg.PIterations = 4;
                psb.Materials[0].Lst = 0.1f + (i / (float)(n - 1)) * 0.9f;
                psb.TotalMass = 20;
                SoftWorld.AddSoftBody(psb);
            }
        }

        SoftBody Create_Rope(Vector3 p)
        {
            SoftBody psb = SoftBodyHelpers.CreateRope(softBodyWorldInfo, p, p + new Vector3(10, 0, 0), 8, 1);
            psb.TotalMass = 50;
            SoftWorld.AddSoftBody(psb);
            return (psb);
        }

        void Init_RopeAttach()
        {
            softBodyWorldInfo.SparseSdf.RemoveReferences(null);
            RigidBody body = LocalCreateRigidBody(50, Matrix.Translation(12, 8, 0), new BoxShape(2, 6, 2));
            SoftBody psb0 = Create_Rope(new Vector3(0, 8, -1));
            SoftBody psb1 = Create_Rope(new Vector3(0, 8, +1));
            psb0.AppendAnchor(psb0.Nodes.Count - 1, body);
            psb1.AppendAnchor(psb1.Nodes.Count - 1, body);
        }

        void Init_ClothAttach()
        {
            float s = 4;
            float h = 6;
            int r = 9;
            SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, new Vector3(-s, h, -s),
                new Vector3(+s, h, -s),
                new Vector3(-s, h, +s),
                new Vector3(+s, h, +s), r, r, 4 + 8, true);
            SoftWorld.AddSoftBody(psb);

            RigidBody body = LocalCreateRigidBody(20, Matrix.Translation(0, h, -(s + 3.5f)), new BoxShape(s, 1, 3));
            psb.AppendAnchor(0, body);
            psb.AppendAnchor(r - 1, body);
            body.UserObject = "LargeBox";
            cutting = true;
        }

        void Create_LinearStair(int count, Vector3 origin, Vector3 sizes)
        {
            BoxShape shape = new BoxShape(sizes);
            for (int i = 0; i < count; i++)
            {
                RigidBody body = LocalCreateRigidBody(0,
                    Matrix.Translation(origin + new Vector3(sizes.X * 2 * i, sizes.Y * 2 * i, 0)), shape);
                body.Friction = 1;
            }
        }

        void Init_Impact()
        {
            SoftBody psb = SoftBodyHelpers.CreateRope(softBodyWorldInfo,
                Vector3.Zero, new Vector3(0, -1, 0), 0, 1);
            SoftWorld.AddSoftBody(psb);
            psb.Cfg.Chr = 0.5f;
            LocalCreateRigidBody(10, Matrix.Translation(0, 20, 0), new BoxShape(2));
        }

        void Init_Collide()
        {
            for (int i = 0; i < 3; ++i)
            {
                SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, TorusMesh.Vertices, TorusMesh.Indices);
                psb.GenerateBendingConstraints(2);
                psb.Cfg.PIterations = 2;
                psb.Cfg.Collisions |= FCollisions.VFSS;
                psb.RandomizeConstraints();
                Matrix m = Matrix.RotationYawPitchRoll((float)Math.PI / 2 * (1 - (i & 1)), (float)Math.PI / 2 * (i & 1), 0) *
                    Matrix.Translation(3 * i, 2, 0);
                psb.Transform(m);
                psb.Scale(new Vector3(2, 2, 2));
                psb.SetTotalMass(50, true);
                SoftWorld.AddSoftBody(psb);
            }
            cutting = true;
        }

        void Init_Collide2()
        {
            for (int i = 0; i < 3; ++i)
            {
                SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, BunnyMesh.Vertices, BunnyMesh.Indices);
                Material pm = psb.AppendMaterial();
                pm.Lst = 0.5f;
                pm.Flags -= FMaterial.DebugDraw;
                psb.GenerateBendingConstraints(2, pm);
                psb.Cfg.PIterations = 2;
                psb.Cfg.DF = 0.5f;
                psb.Cfg.Collisions |= FCollisions.VFSS;
                psb.RandomizeConstraints();
                Matrix m = Matrix.RotationYawPitchRoll(0, (float)Math.PI / 2 * (i & 1), 0) *
                    Matrix.Translation(0, -1 + 5 * i, 0);
                psb.Transform(m);
                psb.Scale(new Vector3(6, 6, 6));
                psb.SetTotalMass(100, true);
                SoftWorld.AddSoftBody(psb);
            }
            cutting = true;
        }

        void Init_Collide3()
        {
            {
                const float s = 8;
                SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, new Vector3(-s, 0, -s),
                new Vector3(+s, 0, -s),
                new Vector3(-s, 0, +s),
                new Vector3(+s, 0, +s),
                15, 15, 1 + 2 + 4 + 8, true);
                psb.Materials[0].Lst = 0.4f;
                psb.Cfg.Collisions |= FCollisions.VFSS;
                psb.TotalMass = 150;
                SoftWorld.AddSoftBody(psb);
            }
            {
                const float s = 4;
                Vector3 o = new Vector3(5, 10, 0);
                SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo,
                new Vector3(-s, 0, -s) + o,
                new Vector3(+s, 0, -s) + o,
                new Vector3(-s, 0, +s) + o,
                new Vector3(+s, 0, +s) + o,
                7, 7, 0, true);
                Material pm = psb.AppendMaterial();
                pm.Lst = 0.1f;
                pm.Flags -= FMaterial.DebugDraw;
                psb.GenerateBendingConstraints(2, pm);
                psb.Materials[0].Lst = 0.5f;
                psb.Cfg.Collisions |= FCollisions.VFSS;
                psb.TotalMass = 150;
                SoftWorld.AddSoftBody(psb);
                cutting = true;
            }
        }

        // Aerodynamic forces, 50x1g flyers
        void Init_Aero()
        {
            const float s = 2;
            const int segments = 6;
            const int count = 50;
            Random random = new Random();
            for (int i = 0; i < count; ++i)
            {
                SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo,
                    new Vector3(-s, 0, -s), new Vector3(+s, 0, -s),
                    new Vector3(-s, 0, +s), new Vector3(+s, 0, +s),
                    segments, segments, 0, true);
                Material pm = psb.AppendMaterial();
                pm.Flags -= FMaterial.DebugDraw;
                psb.GenerateBendingConstraints(2, pm);
                psb.Cfg.LF = 0.004f;
                psb.Cfg.DG = 0.0003f;
                psb.Cfg.AeroModel = AeroModel.VTwoSided;
                Matrix trans = Matrix.Identity;
                Vector3 ra = 0.1f * GetRandomVector(random);
                Vector3 rp = 75 * GetRandomVector(random) + new Vector3(-50, 15, 0);
                Quaternion rot = Quaternion.RotationYawPitchRoll(
                    (float)Math.PI / 8 + ra.X, (float)-Math.PI / 7 + ra.Y, ra.Z);
                trans *= Matrix.RotationQuaternion(rot);
                trans *= Matrix.Translation(rp);
                psb.Transform(trans);
                psb.TotalMass = 0.1f;
                psb.AddForce(new Vector3(0, (float)random.NextDouble(), 0), 0);
                SoftWorld.AddSoftBody(psb);
            }
        }

        void Init_Friction()
        {
            const float bs = 2;
            const float ts = bs + bs / 4;
            for (int i = 0, ni = 20; i < ni; ++i)
            {
                Vector3 p = new Vector3(-ni * ts / 2 + i * ts, bs, 40);
                SoftBody psb = Create_SoftBox(p, new Vector3(bs, bs, bs));
                psb.Cfg.DF = 0.1f * ((i + 1) / (float)ni);
                psb.AddVelocity(new Vector3(0, 0, -10));
            }
        }

        void Init_TetraBunny()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTetGenData(softBodyWorldInfo,
                Bunny.GetElements(), null, Bunny.GetNodes(), false, true, true);
            SoftWorld.AddSoftBody(psb);
            psb.Rotate(Quaternion.RotationYawPitchRoll((float)Math.PI / 2, 0, 0));
            psb.SetVolumeMass(150);
            psb.Cfg.PIterations = 2;
            //psb.Cfg.PIterations = 1;
            cutting = true;
            //psb.CollisionShape.Margin = 0.01f;
            psb.Cfg.Collisions = FCollisions.CLSS | FCollisions.CLRS; //| FCollisions.CLSelf;

            ///pass zero in generateClusters to create  cluster for each tetrahedron or triangle
            psb.GenerateClusters(0);
            //psb.Materials[0].Lst = 0.2f;
            psb.Cfg.DF = 10;
        }

        void Init_TetraCube()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTetGenFile(softBodyWorldInfo,
                "data\\cube.ele", null, "data\\cube.node", false, true, true);
            SoftWorld.AddSoftBody(psb);
            psb.Scale(new Vector3(4, 4, 4));
            psb.Translate(0, 5, 0);
            psb.SetVolumeMass(300);

            ///fix one vertex
            //psb.SetMass(0,0);
            //psb.SetMass(10,0);
            //psb.SetMass(20,0);
            psb.Cfg.PIterations = 1;
            //psb.GenerateClusters(128);
            psb.GenerateClusters(16);
            //psb.CollisionShape.Margin = 0.5f;

            psb.CollisionShape.Margin = 0.01f;
            psb.Cfg.Collisions = FCollisions.CLSS | FCollisions.CLRS;
            // | SoftBody.FCollisions.CLSelf;
            psb.Materials[0].Lst = 0.8f;
            cutting = true;
        }

        void Init_Volume()
        {
            SoftBody psb = SoftBodyHelpers.CreateEllipsoid(softBodyWorldInfo, new Vector3(35, 25, 0),
                new Vector3(1, 1, 1) * 3, 512);
            psb.Materials[0].Lst = 0.45f;
            psb.Cfg.VC = 20;
            psb.SetTotalMass(50, true);
            psb.SetPose(true, false);
            SoftWorld.AddSoftBody(psb);

            Create_BigPlate();
            Create_LinearStair(10, Vector3.Zero, new Vector3(2, 1, 5));
        }

        void Init_Sticks()
        {
            const int n = 16;
            const int sg = 4;
            const float sz = 5;
            const float hg = 4;
            const float inf = 1 / (float)(n - 1);
            for (int y = 0; y < n; ++y)
            {
                for (int x = 0; x < n; ++x)
                {
                    Vector3 org = new Vector3(-sz + sz * 2 * x * inf,
                        -10, -sz + sz * 2 * y * inf);

                    SoftBody psb = SoftBodyHelpers.CreateRope(softBodyWorldInfo, org,
                        org + new Vector3(hg * 0.001f, hg, 0), sg, 1);

                    psb.Cfg.DP = 0.005f;
                    psb.Cfg.Chr = 0.1f;
                    for (int i = 0; i < 3; ++i)
                    {
                        psb.GenerateBendingConstraints(2 + i);
                    }
                    psb.SetMass(1, 0);
                    psb.SetTotalMass(0.01f);
                    SoftWorld.AddSoftBody(psb);

                }
            }
            Create_BigBall(new Vector3(0, 13, 0));
        }

        void Init_Bending()
        {
            const float s = 4;
            Vector3[] x = new Vector3[]{new Vector3(-s,0,-s),
		        new Vector3(+s,0,-s),
		        new Vector3(+s,0,+s),
		        new Vector3(-s,0,+s)};
            float[] m = new float[] { 0, 0, 0, 1 };
            SoftBody psb = new SoftBody(softBodyWorldInfo, x, m);
            psb.AppendLink(0, 1);
            psb.AppendLink(1, 2);
            psb.AppendLink(2, 3);
            psb.AppendLink(3, 0);
            psb.AppendLink(0, 2);

            SoftWorld.AddSoftBody(psb);
        }

        void Init_Cloth()
        {
            float s = 8;
            SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, new Vector3(-s, 0, -s),
                new Vector3(+s, 0, -s),
                new Vector3(-s, 0, +s),
                new Vector3(+s, 0, +s),
                31, 31,
                //		31,31,
                1 + 2 + 4 + 8, true);

            psb.CollisionShape.Margin = 0.5f;
            Material pm = psb.AppendMaterial();
            pm.Lst = 0.4f;
            pm.Flags -= FMaterial.DebugDraw;
            psb.GenerateBendingConstraints(2, pm);
            psb.TotalMass = 150;
            SoftWorld.AddSoftBody(psb);

            Create_RbUpStack(10);
            cutting = true;
        }

        void Init_Bunny()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, BunnyMesh.Vertices, BunnyMesh.Indices);
            Material pm = psb.AppendMaterial();
            pm.Lst = 0.5f;
            pm.Flags -= FMaterial.DebugDraw;
            psb.GenerateBendingConstraints(2, pm);
            psb.Cfg.PIterations = 2;
            psb.Cfg.DF = 0.5f;
            psb.RandomizeConstraints();
            Matrix m = Matrix.RotationYawPitchRoll((float)Math.PI / 2, 0, 0) *
                Matrix.Translation(0, 4, 0);
            psb.Transform(m);
            psb.Scale(new Vector3(6, 6, 6));
            psb.SetTotalMass(100, true);
            SoftWorld.AddSoftBody(psb);
            cutting = true;
        }

        void Init_BunnyMatch()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, BunnyMesh.Vertices, BunnyMesh.Indices);
            psb.Cfg.DF = 0.5f;
            psb.Cfg.MT = 0.05f;
            psb.Cfg.PIterations = 5;
            psb.RandomizeConstraints();
            psb.Scale(new Vector3(6, 6, 6));
            psb.SetTotalMass(100, true);
            psb.SetPose(false, true);
            SoftWorld.AddSoftBody(psb);
        }

        void Init_Torus()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, TorusMesh.Vertices, TorusMesh.Indices);
            psb.GenerateBendingConstraints(2);
            psb.Cfg.PIterations = 2;
            psb.RandomizeConstraints();
            Matrix m = Matrix.RotationYawPitchRoll((float)Math.PI / 2, 0, 0) *
                Matrix.Translation(0, 4, 0);
            psb.Transform(m);
            psb.Scale(new Vector3(2, 2, 2));
            psb.SetTotalMass(50, true);
            SoftWorld.AddSoftBody(psb);
            cutting = true;
        }

        void Init_TorusMatch()
        {
            SoftBody psb = SoftBodyHelpers.CreateFromTriMesh(softBodyWorldInfo, TorusMesh.Vertices, TorusMesh.Indices);
            psb.Materials[0].Lst = 0.1f;
            psb.Cfg.MT = 0.05f;
            psb.RandomizeConstraints();
            Matrix m = Matrix.RotationYawPitchRoll((float)Math.PI / 2, 0, 0) *
                Matrix.Translation(0, 4, 0);
            psb.Transform(m);
            psb.Scale(new Vector3(2, 2, 2));
            psb.SetTotalMass(50, true);
            psb.SetPose(false, true);
            SoftWorld.AddSoftBody(psb);
        }

        void Init_Cutting1()
        {
            const float s = 6;
            const float h = 2;
            const int r = 16;
            Vector3[] p = new Vector3[]{new Vector3(+s,h,-s),
		        new Vector3(-s,h,-s),
		        new Vector3(+s,h,+s),
		        new Vector3(-s,h,+s)};
            SoftBody psb = SoftBodyHelpers.CreatePatch(softBodyWorldInfo, p[0], p[1], p[2], p[3], r, r, 1 + 2 + 4 + 8, true);
            SoftWorld.AddSoftBody(psb);
            psb.Cfg.PIterations = 1;
            cutting = true;
        }

        void Create_Gear(Vector3 pos, float speed)
        {
	        Matrix startTransform = Matrix.Translation(pos);
	        CompoundShape shape = new CompoundShape();
#if true
	        shape.AddChildShape(Matrix.Identity, new BoxShape(5,1,6));
	        shape.AddChildShape(Matrix.RotationZ((float)Math.PI), new BoxShape(5,1,6));
#else
            shape.AddChildShape(Matrix.Identity, new CylinderShapeZ(5,1,7));
            shape.AddChildShape(Matrix.RotationZ((float)Math.PI), new BoxShape(4,1,8));
#endif
            RigidBody body = LocalCreateRigidBody(10, startTransform, shape);
	        body.Friction = 1;
	        HingeConstraint hinge = new HingeConstraint(body, Matrix.Identity);
	        if(speed!=0) hinge.EnableAngularMotor(true,speed,3);
	        World.AddConstraint(hinge);
        }

        public Physics()
        {
            CollisionConfiguration collisionConf;

            // collision configuration contains default setup for memory, collision setup
            collisionConf = new SoftBodyRigidBodyCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(collisionConf);

            Broadphase = new AxisSweep3(new Vector3(-1000, -1000, -1000),
                new Vector3(1000, 1000, 1000), maxProxies);

            // the default constraint solver.
            Solver = new SequentialImpulseConstraintSolver();

            softBodyWorldInfo = new SoftBodyWorldInfo();
            softBodyWorldInfo.AirDensity = 1.2f;
            softBodyWorldInfo.WaterDensity = 0;
            softBodyWorldInfo.WaterOffset = 0;
            softBodyWorldInfo.WaterNormal = Vector3.Zero;
            softBodyWorldInfo.Gravity = new Vector3(0, -10, 0);
            softBodyWorldInfo.Dispatcher = Dispatcher;
            softBodyWorldInfo.Broadphase = Broadphase;
            softBodyWorldInfo.SparseSdf.Initialize();

            World = new SoftRigidDynamicsWorld(Dispatcher, Broadphase, Solver, collisionConf);
            World.Gravity = new Vector3(0, -10, 0);
            World.DispatchInfo.EnableSpu = true;

            InitializeDemo();
        }

        public override int Update(float elapsedTime)
        {
            int subSteps = base.Update(elapsedTime);

            softBodyWorldInfo.SparseSdf.GarbageCollect();

            return subSteps;
        }
    }
}
