﻿#define TEST_GIMPACT_TORUS

using System;
using BulletSharp;
using DemoFramework;
using SlimDX;

namespace GImpactTestDemo
{
    class Physics : PhysicsContext
    {
        const bool BulletTriangleCollision = true;
        const bool BulletGImpact = true;
        const bool BulletGImpactConvexDecomposition = true;

        CollisionShape trimeshShape;
        CollisionShape trimeshShape2;

        TriangleIndexVertexArray indexVertexArrays;
        TriangleIndexVertexArray indexVertexArrays2;

        Vector3 kinTorusTran;
	    Quaternion kinTorusRot;
        RigidBody kinematicTorus;

        public Physics()
        {
            CollisionConfiguration collisionConf;

            // collision configuration contains default setup for memory, collision setup
            collisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(collisionConf);

            //Broadphase = new SimpleBroadphase();
            Broadphase = new AxisSweep3_32Bit(new Vector3(-10000, -10000, -10000), new Vector3(10000, 10000, 10000), 1024);

            Solver = new SequentialImpulseConstraintSolver();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, Solver, collisionConf);
            World.Gravity = new Vector3(0, -10, 0);


            //create trimesh model and shape
            InitGImpactCollision();

            /// Create Scene
            float mass = 0.0f;

            CollisionShape staticboxShape1 = new BoxShape(200, 1, 200);//floor
            CollisionShapes.Add(staticboxShape1);
            LocalCreateRigidBody(mass, Matrix.Translation(0, -10, 0), staticboxShape1);

            CollisionShape staticboxShape2 = new BoxShape(1, 50, 200);//left wall
            CollisionShapes.Add(staticboxShape2);
            LocalCreateRigidBody(mass, Matrix.Translation(-200, 15, 0), staticboxShape2);

            CollisionShape staticboxShape3 = new BoxShape(1, 50, 200);//right wall
            CollisionShapes.Add(staticboxShape3);
            LocalCreateRigidBody(mass, Matrix.Translation(200, 15, 0), staticboxShape3);

            CollisionShape staticboxShape4 = new BoxShape(200, 50, 1);//front wall
            CollisionShapes.Add(staticboxShape4);
            LocalCreateRigidBody(mass, Matrix.Translation(0, 15, 200), staticboxShape4);

            CollisionShape staticboxShape5 = new BoxShape(200, 50, 1);//back wall
            CollisionShapes.Add(staticboxShape5);
            LocalCreateRigidBody(mass, Matrix.Translation(0, 15, -200), staticboxShape5);


            //static plane

            Vector3 normal = new Vector3(-0.5f, 0.5f, 0.0f);
            normal.Normalize();
            CollisionShape staticplaneShape6 = new StaticPlaneShape(normal, 0.5f);// A plane
            CollisionShapes.Add(staticplaneShape6);
            RigidBody staticBody2 = LocalCreateRigidBody(mass, Matrix.Translation(0, -9, 0), staticplaneShape6);


            //another static plane

            normal = new Vector3(0.5f, 0.7f, 0.0f);
            //normal.Normalize();
            CollisionShape staticplaneShape7 = new StaticPlaneShape(normal, 0.0f);// A plane
            CollisionShapes.Add(staticplaneShape7);
            staticBody2 = LocalCreateRigidBody(mass, Matrix.Translation(0, -10, 0), staticplaneShape7);


            /// Create Static Torus
	        float height = 28;
	        float step = 2.5f;
	        float massT = 1.0f;

            Matrix startTransform =
                Matrix.RotationQuaternion(Quaternion.RotationYawPitchRoll((float)Math.PI*0.5f, 0, (float)Math.PI*0.5f)) *
                Matrix.Translation(0, height, -5);

            if (BulletGImpact)
            {
                kinematicTorus = LocalCreateRigidBody(0, startTransform, trimeshShape);
            }
            else
            {
                //kinematicTorus = LocalCreateRigidBody(0, startTransform, CreateTorusShape());
            }

            //kinematicTorus.CollisionFlags = kinematicTorus.CollisionFlags | CollisionFlags.StaticObject;
	        //kinematicTorus.ActivationState = ActivationState.IslandSleeping;

            kinematicTorus.CollisionFlags = kinematicTorus.CollisionFlags | CollisionFlags.KinematicObject;
            kinematicTorus.ActivationState = ActivationState.DisableDeactivation;

	        // Kinematic
	        kinTorusTran = new Vector3(-0.1f,0,0);
            kinTorusRot = Quaternion.RotationYawPitchRoll(0, (float)Math.PI * 0.01f, 0);

#if TEST_GIMPACT_TORUS

            if (BulletGImpact)
            {
                // Create dynamic Torus
                for (int i = 0; i < 6; i++)
                {
                    height -= step;
                    startTransform =
                        Matrix.RotationQuaternion(Quaternion.RotationYawPitchRoll(0, 0, (float)Math.PI * 0.5f)) *
                        Matrix.Translation(0, height, -5);
                    //RigidBody bodyA = LocalCreateRigidBody(massT, startTransform, trimeshShape);

                    height -= step;
                    startTransform =
                        Matrix.RotationQuaternion(Quaternion.RotationYawPitchRoll((float)Math.PI * 0.5f, 0, (float)Math.PI * 0.5f)) *
                        Matrix.Translation(0, height, -5);
                    //RigidBody bodyB = LocalCreateRigidBody(massT, startTransform, trimeshShape);
                }
            }
            else
            {
                /*
                // Create dynamic Torus
                for (int i = 0; i < 6; i++)
                {
                    height -= step;
                    startTransform.setOrigin(btVector3(0, height, -5));
                    startTransform.setRotation(btQuaternion(0, 0, 3.14159265 * 0.5));

                    btRigidBody* bodyA = localCreateRigidBody(massT, startTransform, createTorusShape());

                    height -= step;
                    startTransform.setOrigin(btVector3(0, height, -5));
                    startTransform.setRotation(btQuaternion(3.14159265 * 0.5, 0, 3.14159265 * 0.5));
                    btRigidBody* bodyB = localCreateRigidBody(massT, startTransform, createTorusShape());
                }
                */
            }
#endif
        }

        void InitGImpactCollision()
        {
           	// Create Torus Shape

            indexVertexArrays = new TriangleIndexVertexArray(TorusMesh.Indices, TorusMesh.Vertices);

            if (BulletGImpact)
            {
                if (BulletGImpactConvexDecomposition)
                {
                    //GImpactConvexDecompositionShape trimesh =
                    //    new GImpactConvexDecompositionShape(indexVertexArrays, new Vector3(1), 0.01f);
			        //trimesh.Margin = 0.07f;
			        //trimesh.UpdateBound();
                    //trimeshShape = trimesh;
                }
                else
                {
                    GImpactMeshShape trimesh = new GImpactMeshShape(indexVertexArrays);
			        trimesh.LocalScaling = new Vector3(1);
                    if (BulletTriangleCollision)
                        trimesh.Margin = 0.07f; //?????
                    else
                        trimesh.Margin = 0;
                    trimesh.UpdateBound();
                    trimeshShape = trimesh;
                }
            }
            else
            {
                //trimeshShape = new GImpactMeshData(indexVertexArrays);
            }


            /// Create Bunny Shape
    		indexVertexArrays2 = new TriangleIndexVertexArray(BunnyMesh.Indices, BunnyMesh.Vertices);

            if (BulletGImpact)
            {
                if (BulletGImpactConvexDecomposition)
                {
                    //GImpactConvexDecompositionShape trimesh2 =
                    //    new GImpactConvexDecompositionShape(indexVertexArrays, new Vector3(1), 0.01f);
			        //trimesh.Margin = 0.07f;
			        //trimesh.UpdateBound();
                    //trimeshShape = trimesh2;
                }
                else
                {
                    GImpactMeshShape trimesh2 = new GImpactMeshShape(indexVertexArrays2);
                    trimesh2.LocalScaling = new Vector3(1);
                    if (BulletTriangleCollision)
                        trimesh2.Margin = 0.07f; //?????
                    else
                        trimesh2.Margin = 0;
                    trimesh2.UpdateBound();
                    trimeshShape2 = trimesh2;
                }
            }
            else
            {
                //trimeshShape2 = new GImpactMeshData(indexVertexArrays2);
            }

            	//register GIMPACT algorithm
            if (BulletGImpact)
            {
                GImpactCollisionAlgorithm.RegisterAlgorithm(Dispatcher);
            }
            else
            {
                //ConcaveConcaveCollisionAlgorithm.RegisterAlgorithm(Dispatcher);
            }
        }
    }
}
