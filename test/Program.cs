using BulletSharp;
using System;

namespace BulletSharpTest
{
    class Program
    {
        static public void TestWeakRef(string name, WeakReference wr)
        {
            if (wr.IsAlive)
            {
                Console.Write(name + " GC collection FAILED! ");
                Console.WriteLine("Gen: " + GC.GetGeneration(wr.Target));
            }
            else
            {
                Console.WriteLine(name + " GC collection OK");
            }
        }

        static DiscreteDynamicsWorld world;

        static public void TestGCCollection()
        {
            var conf = new DefaultCollisionConfiguration();
            var dispatcher = new CollisionDispatcher(conf);
            var broadphase = new DbvtBroadphase();
            world = new DiscreteDynamicsWorld(dispatcher, broadphase, null, conf);
            world.Gravity = new Vector3(0, -10, 0);

            var groundShape = new BoxShape(50, 1, 50);
            var localInertia = groundShape.CalculateLocalInertia(0.0f);
            var motionState = new DefaultMotionState();
            var constInfo = new RigidBodyConstructionInfo(0.0f, motionState, groundShape, localInertia);
            var groundObject = new RigidBody(constInfo);

            float mass = 1.0f;
            var dynamicShape = new SphereShape(mass);
            localInertia = dynamicShape.CalculateLocalInertia(0.0f);
            var motionState2 = new DefaultMotionState();
            var constInfo2 = new RigidBodyConstructionInfo(mass, motionState2, dynamicShape, localInertia);
            var dynamicObject = new RigidBody(constInfo);
            world.AddRigidBody(dynamicObject);

            var conf_wr = new WeakReference(conf);
            var dispatcher_wr = new WeakReference(dispatcher);
            var broadphase_wr = new WeakReference(broadphase);
            var world_wr = new WeakReference(broadphase);
            var groundShape_wr = new WeakReference(groundShape);
            var constInfo_wr = new WeakReference(constInfo);
            var groundObject_wr = new WeakReference(groundObject);
            var dynamicShape_wr = new WeakReference(dynamicShape);
            var constInfo2_wr = new WeakReference(constInfo2);
            var dynamicObject_wr = new WeakReference(dynamicObject);

            dispatcher.NearCallback = DispatcherNearCallback;
            dispatcher.NearCallback = null;

            //conf.Dispose();
            conf = null;
            dispatcher.OnDisposing += onDisposing;
            dispatcher.OnDisposed += onDisposed;
            //dispatcher.Dispose();
            dispatcher = null;
            broadphase.OnDisposing += onDisposing;
            broadphase.OnDisposed += onDisposed;
            //broadphase.Dispose();
            broadphase = null;
            world.OnDisposing += onDisposing;
            world.OnDisposed += onDisposed;
            world.SetInternalTickCallback(new DynamicsWorld.InternalTickCallback(WorldPreTickCallback));
            world.StepSimulation(1.0f / 60.0f);
            //world.SetInternalTickCallback(null);
            //world.Dispose();
            world = null;

            groundShape = null;
            constInfo = null;
            groundObject = null;
            dynamicShape = null;
            constInfo2 = null;
            dynamicObject = null;

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

            TestWeakRef("CollisionConfiguration", conf_wr);
            TestWeakRef("CollisionDispatcher", dispatcher_wr);
            TestWeakRef("DbvtBroadphase", broadphase_wr);
            TestWeakRef("DiscreteDynamicsWorld", world_wr);
            TestWeakRef("BoxShape", groundShape_wr);
            TestWeakRef("RigidBodyConstructionInfo", constInfo_wr);
            TestWeakRef("RigidBody", groundObject_wr);
            TestWeakRef("SphereShape", dynamicShape_wr);
            TestWeakRef("RigidBodyConstructionInfo", constInfo2_wr);
            TestWeakRef("RigidBody", dynamicObject_wr);
        }

        static void onDisposed(object sender, EventArgs e)
        {
            Console.WriteLine("OnDisposed: " + sender.ToString());
        }

        static void onDisposing(object sender, EventArgs e)
        {
            Console.WriteLine("OnDisposing: " + sender.ToString());
        }

        static void WorldPreTickCallback(DynamicsWorld world2, float timeStep)
        {
            Console.WriteLine("WorldPreTickCallback");
            if (object.ReferenceEquals(world, world2))
            {
                Console.WriteLine("World reference lost!");
            }
        }

        static void DispatcherNearCallback(BroadphasePair collisionPair, CollisionDispatcher dispatcher,
			DispatcherInfo dispatchInfo)
        {
            Console.WriteLine("DispatcherNearCallback");
        }

        static void Main(string[] args)
        {
            TestGCCollection();

            Console.ReadKey();
        }
    }
}
