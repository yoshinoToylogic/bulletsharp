﻿using System;
using System.Collections.Generic;
using BulletSharp;
using BulletSharp.SoftBody;
using SlimDX;
using SlimDX.Direct3D9;

namespace DemoFramework
{
    // This class creates graphical objects (boxes, cones, cylinders, spheres) on the fly.
    public class GraphicObjectFactory : System.IDisposable
    {
        Device device;

        Dictionary<Vector3, Mesh> boxes = new Dictionary<Vector3, Mesh>();
        Dictionary<Vector3, Mesh> capsules = new Dictionary<Vector3, Mesh>();
        Dictionary<Vector2, Mesh> cones = new Dictionary<Vector2, Mesh>();
        Dictionary<ConvexHullShape, Mesh> hullShapes = new Dictionary<ConvexHullShape, Mesh>();
        Dictionary<Vector3, Mesh> cylinders = new Dictionary<Vector3, Mesh>();
        Dictionary<float, Mesh> spheres = new Dictionary<float, Mesh>();
        Dictionary<float, Dictionary<Vector3, Mesh>> positionedSpheres = new Dictionary<float, Dictionary<Vector3, Mesh>>();

        public GraphicObjectFactory(Device device)
        {
            this.device = device;
        }

        public void Dispose()
        {
            foreach (Mesh mesh in boxes.Values)
            {
                mesh.Dispose();
            }
            boxes.Clear();

            foreach (Mesh mesh in capsules.Values)
            {
                mesh.Dispose();
            }
            capsules.Clear();

            foreach (Mesh mesh in cones.Values)
            {
                mesh.Dispose();
            }
            cones.Clear();

            foreach (Mesh mesh in cylinders.Values)
            {
                mesh.Dispose();
            }
            cylinders.Clear();

            foreach (Mesh mesh in hullShapes.Values)
            {
                mesh.Dispose();
            }
            hullShapes.Clear();

            foreach (Mesh mesh in spheres.Values)
            {
                mesh.Dispose();
            }
            spheres.Clear();

            foreach (Dictionary<Vector3, Mesh> positions in positionedSpheres.Values)
            {
                foreach (Mesh mesh in positions.Values)
                    mesh.Dispose();
                positions.Clear();
            }
            positionedSpheres.Clear();
        }

        // Local transforms are needed for CompoundShapes.
        void DoLocalTransform(Matrix localTransform)
        {
            Matrix tempTr = device.GetTransform(TransformState.World);
            device.SetTransform(TransformState.World, localTransform * tempTr);
        }

        public void Render(CollisionObject body)
        {
            if (body.CollisionShape.ShapeType == BroadphaseNativeType.SoftBodyShape)
            {
                RenderSoftBody(SoftBody.Upcast(body));
            }
            else
            {
                Render(body.CollisionShape, Matrix.Identity);
            }
        }

        public void Render(CollisionShape shape, Matrix localTransform)
        {
            shape = shape.UpcastDetect();

            switch (shape.ShapeType)
            {
                case BroadphaseNativeType.BoxShape:
                    RenderBox((BoxShape)shape, localTransform);
                    return;
                case BroadphaseNativeType.ConeShape:
                    RenderCone((ConeShape)shape, localTransform);
                    return;
                case BroadphaseNativeType.Convex2DShape:
                    RenderConvex2dShape((Convex2DShape)shape, localTransform);
                    return;
                case BroadphaseNativeType.ConvexHullShape:
                    RenderConvexHullShape((ConvexHullShape)shape, localTransform);
                    return;
                case BroadphaseNativeType.CylinderShape:
                    RenderCylinder((CylinderShape)shape, localTransform);
                    return;
                case BroadphaseNativeType.SphereShape:
                    RenderSphere((SphereShape)shape, localTransform);
                    return;
                case BroadphaseNativeType.CompoundShape:
                    RenderCompoundShape((CompoundShape)shape, localTransform);
                    return;
                case BroadphaseNativeType.CapsuleShape:
                    RenderCapsuleShape((CapsuleShape)shape, localTransform);
                    return;
                case BroadphaseNativeType.MultiSphereShape:
                    RenderMultiSphereShape((MultiSphereShape)shape, localTransform);
                    return;
            }

            //throw new NotImplementedException();
        }

        public void RenderBox(BoxShape shape, Matrix localTransform)
        {
            if (localTransform.IsIdentity == false)
                DoLocalTransform(localTransform);

            Mesh boxMesh;
            Vector3 size = shape.HalfExtentsWithMargin;

            if (boxes.TryGetValue(size, out boxMesh) == false)
            {
                boxMesh = Mesh.CreateBox(device, size.X * 2, size.Y * 2, size.Z * 2);
                boxes.Add(size, boxMesh);
            }

            boxMesh.DrawSubset(0);
        }

        public void RenderCapsuleShape(CapsuleShape shape, Matrix localTransform)
        {
            if (localTransform.IsIdentity == false)
                DoLocalTransform(localTransform);

            Mesh compoundMesh;
            Vector3 size = shape.ImplicitShapeDimensions;

            if (capsules.TryGetValue(size, out compoundMesh) == false)
            {
                // Combine a cylinder and two spheres.
                Mesh cylinder = Mesh.CreateCylinder(device, size.X, size.X, size.Y * 2, 8, 1);
                Mesh sphere = Mesh.CreateSphere(device, size.Z, 8, 4);
                Mesh[] meshes = new Mesh[] { sphere, cylinder, sphere };
                Matrix[] transforms = new Matrix[] {
                    Matrix.Translation(0, -size.Y, 0),
                    Matrix.RotationX((float)Math.PI / 2),
                    Matrix.Translation(0, size.Y, 0)};
                compoundMesh = Mesh.Concatenate(device, meshes, MeshFlags.Managed, transforms, null);
                cylinder.Dispose();
                sphere.Dispose();

                capsules.Add(size, compoundMesh);
            }

            compoundMesh.DrawSubset(0);
            compoundMesh.DrawSubset(1);
            compoundMesh.DrawSubset(2);
        }

        public void RenderCone(ConeShape shape, Matrix localTransform)
        {
            if (localTransform.IsIdentity == false)
                DoLocalTransform(localTransform);

            Mesh coneMesh;
            float radius = shape.Radius;
            float height = shape.Height;
            Vector2 dimensions = new Vector2(radius, height);

            if (cones.TryGetValue(dimensions, out coneMesh) == false)
            {
                coneMesh = Mesh.CreateCylinder(device, 0, radius, height, 16, 1);
                cones.Add(dimensions, coneMesh);
            }

            coneMesh.DrawSubset(0);
        }

        public void RenderCylinder(CylinderShape shape, Matrix localTransform)
        {
            if (localTransform.IsIdentity == false)
                DoLocalTransform(localTransform);

            int upAxis = shape.UpAxis;
            float radius = shape.Radius;
            float halfHeight = shape.HalfExtentsWithoutMargin[upAxis] + shape.Margin;
            Vector3 size = new Vector3(radius, halfHeight, upAxis);

            Mesh mesh;

            if (cylinders.TryGetValue(size, out mesh) == false)
            {
                mesh = Mesh.CreateCylinder(device, radius, radius, halfHeight * 2, 16, 1);
                if (upAxis == 0)
                {
                    Matrix[] transform = new Matrix[] { Matrix.RotationY((float)Math.PI / 2) };
                    Mesh meshRotated = Mesh.Concatenate(device, new Mesh[] { mesh }, MeshFlags.Managed, transform, null);
                    mesh.Dispose();
                    mesh = meshRotated;
                }
                else if (upAxis == 1)
                {
                    Matrix[] transform = new Matrix[] { Matrix.RotationX((float)Math.PI / 2) };
                    Mesh cylinderMeshRot = Mesh.Concatenate(device, new Mesh[] { mesh }, MeshFlags.Managed, transform, null);
                    mesh.Dispose();
                    mesh = cylinderMeshRot;
                }
                cylinders.Add(size, mesh);
            }

            mesh.DrawSubset(0);
        }

        public void RenderSphere(SphereShape shape, Matrix localTransform)
        {
            if (localTransform.IsIdentity == false)
                DoLocalTransform(localTransform);

            Mesh sphereMesh;
            float radius = shape.Radius;

            if (spheres.TryGetValue(radius, out sphereMesh) == false)
            {
                sphereMesh = Mesh.CreateSphere(device, radius, 16, 16);
                spheres.Add(radius, sphereMesh);
            }

            sphereMesh.DrawSubset(0);
        }

        public void RenderConvex2dShape(Convex2DShape shape, Matrix localTransform)
        {
            if (localTransform.IsIdentity == false)
                DoLocalTransform(localTransform);

            Render(shape.ChildShape, Matrix.Identity);
        }

        public void RenderConvexHullShape(ConvexHullShape shape, Matrix localTransform)
        {
            if (localTransform.IsIdentity == false)
                DoLocalTransform(localTransform);
            
            Mesh hullMesh;
            if (hullShapes.TryGetValue(shape, out hullMesh) == false)
            {
                int vertexCount = shape.NumPoints;
                int faceCount = vertexCount / 3;
                vertexCount = faceCount * 3; // must be 3 verts for every face

                bool index32 = vertexCount > 65536;

                hullMesh = new Mesh(device, faceCount, vertexCount,
                    MeshFlags.SystemMemory | (index32 ? MeshFlags.Use32Bit : 0), VertexFormat.Position | VertexFormat.Normal);


                SlimDX.DataStream indices = hullMesh.LockIndexBuffer(LockFlags.Discard);
                int i;
                if (index32)
                {
                    for (i = 0; i < vertexCount; i++)
                        indices.Write(i);
                }
                else
                {
                    for (i = 0; i < vertexCount; i++)
                        indices.Write((short)i);
                }
                hullMesh.UnlockIndexBuffer();

                SlimDX.DataStream verts = hullMesh.LockVertexBuffer(LockFlags.Discard);
                Vector3Array points = shape.UnscaledPoints;
                Vector3 scale = Vector3.Multiply(shape.LocalScaling, 1.0f + shape.Margin);
                for (i = 0; i < vertexCount; )
                {
                    verts.Write(Vector3.Modulate(points[i++], scale));
                    verts.Position += 12;
                    verts.Write(Vector3.Modulate(points[i++], scale));
                    verts.Position += 12;
                    verts.Write(Vector3.Modulate(points[i++], scale));
                    verts.Position += 12;
                }
                hullMesh.UnlockVertexBuffer();

                hullMesh.ComputeNormals();
                hullShapes.Add(shape, hullMesh);
            }

            hullMesh.DrawSubset(0);
        }

        public void RenderCompoundShape(CompoundShape shape, Matrix localTransform)
        {
            if (localTransform.IsIdentity == false)
                DoLocalTransform(localTransform);

            if (shape.NumChildShapes > 0)
            {
                foreach (CompoundShapeChild child in shape.ChildList)
                {
                    Render(child.ChildShape, child.Transform);
                }
            }
        }

        public void RenderMultiSphereShape(MultiSphereShape shape, Matrix localTransform)
        {
            if (localTransform.IsIdentity == false)
                DoLocalTransform(localTransform);

            int i;
            for (i = 0; i < shape.SphereCount; i++)
            {
                Vector3 position = shape.GetSpherePosition(i);
                float radius = shape.GetSphereRadius(i);

                Dictionary<Vector3, Mesh> positions;
                Mesh sphereMesh;

                if (positionedSpheres.TryGetValue(radius, out positions) == false)
                {
                    positions = new Dictionary<Vector3, Mesh>();
                    sphereMesh = Mesh.CreateSphere(device, radius, 12, 12);
                    if (position != Vector3.Zero)
                    {
                        Matrix[] transform = new Matrix[] { Matrix.Translation(position) };
                        Mesh sphereMeshRot = Mesh.Concatenate(device, new Mesh[] { sphereMesh }, MeshFlags.Managed, transform, null);
                        sphereMesh.Dispose();
                        sphereMesh = sphereMeshRot;
                    }
                    positions.Add(position, sphereMesh);
                    positionedSpheres.Add(radius, positions);
                }
                else
                {
                    if (positions.TryGetValue(position, out sphereMesh) == false)
                    {
                        sphereMesh = Mesh.CreateSphere(device, radius, 12, 12);
                        positions.Add(position, sphereMesh);
                    }
                }
                sphereMesh.DrawSubset(0);
            }
        }

        public void RenderSoftBody(SoftBody softBody)
        {
            AlignedFaceArray faces = softBody.Faces;
            int faceCount = faces.Count;

            if (faceCount > 0)
            {
                int vertexCount = faceCount * 3;
                bool index32 = vertexCount > 65536;

                Mesh mesh = new Mesh(device, faceCount, vertexCount,
                    MeshFlags.SystemMemory | (index32 ? MeshFlags.Use32Bit : 0), VertexFormat.Position | VertexFormat.Normal);

                SlimDX.DataStream indices = mesh.LockIndexBuffer(LockFlags.Discard);
                int i;
                if (index32)
                {
                    for (i = 0; i < vertexCount; i++)
                        indices.Write(i);
                }
                else
                {
                    for (i = 0; i < vertexCount; i++)
                        indices.Write((short)i);
                }
                mesh.UnlockIndexBuffer();

                SlimDX.DataStream verts = mesh.LockVertexBuffer(LockFlags.Discard);
                foreach (Face face in faces)
                {
                    NodePtrArray nodes = face.N;
                    verts.Write(nodes[0].X);
                    verts.Position += 12;
                    verts.Write(nodes[1].X);
                    verts.Position += 12;
                    verts.Write(nodes[2].X);
                    verts.Position += 12;
                }
                mesh.UnlockVertexBuffer();

                mesh.ComputeNormals();
                mesh.DrawSubset(0);
                mesh.Dispose();
            }
            else
            {
                AlignedTetraArray tetras = softBody.Tetras;
                int tetraCount = tetras.Count;

                if (tetraCount > 0)
                {
                    int vertexCount = tetraCount * 12;
                    bool index32 = vertexCount > 65536;

                    Mesh mesh = new Mesh(device, tetraCount * 4, vertexCount,
                        MeshFlags.SystemMemory | (index32 ? MeshFlags.Use32Bit : 0), VertexFormat.Position | VertexFormat.Normal);


                    SlimDX.DataStream indices = mesh.LockIndexBuffer(LockFlags.Discard);
                    int i;
                    if (index32)
                    {
                        for (i = 0; i < vertexCount; i++)
                            indices.Write(i);
                    }
                    else
                    {
                        for (i = 0; i < vertexCount; i++)
                            indices.Write((short)i);
                    }
                    mesh.UnlockIndexBuffer();


                    SlimDX.DataStream verts = mesh.LockVertexBuffer(LockFlags.Discard);
                    foreach (Tetra t in tetras)
                    {
                        NodePtrArray nodes = t.Nodes;

                        verts.Write(nodes[2].X);
                        verts.Position += 12;
                        verts.Write(nodes[1].X);
                        verts.Position += 12;
                        verts.Write(nodes[0].X);
                        verts.Position += 12;

                        verts.Write(nodes[0].X);
                        verts.Position += 12;
                        verts.Write(nodes[1].X);
                        verts.Position += 12;
                        verts.Write(nodes[3].X);
                        verts.Position += 12;

                        verts.Write(nodes[2].X);
                        verts.Position += 12;
                        verts.Write(nodes[3].X);
                        verts.Position += 12;
                        verts.Write(nodes[1].X);
                        verts.Position += 12;

                        verts.Write(nodes[2].X);
                        verts.Position += 12;
                        verts.Write(nodes[0].X);
                        verts.Position += 12;
                        verts.Write(nodes[3].X);
                        verts.Position += 12;
                    }
                    mesh.UnlockVertexBuffer();

                    mesh.ComputeNormals();
                    mesh.DrawSubset(0);
                    mesh.Dispose();
                }
            }
        }

        public void RenderSoftBodyTextured(SoftBody softBody)
        {
            if (!(softBody.UserObject is Array))
                return;

            object[] userObjArr = softBody.UserObject as object[];
            FloatArray vertexBuffer = userObjArr[0] as FloatArray;
            IntArray indexBuffer = userObjArr[1] as IntArray;

            int vertexCount = (vertexBuffer.Count / 8) ;

            if (vertexCount > 0)
            {
                int faceCount = indexBuffer.Count / 2;

                bool index32 = vertexCount > 65536;

                Mesh mesh = new Mesh(device, faceCount, vertexCount,
                    MeshFlags.SystemMemory | (index32 ? MeshFlags.Use32Bit : 0),
                    VertexFormat.Position | VertexFormat.Normal | VertexFormat.Texture1);

                SlimDX.DataStream indices = mesh.LockIndexBuffer(LockFlags.Discard);
                if (index32)
                {
                    foreach (int i in indexBuffer)
                        indices.Write(i);
                }
                else
                {
                    foreach (int i in indexBuffer)
                        indices.Write((short)i);
                }
                mesh.UnlockIndexBuffer();

                SlimDX.DataStream verts = mesh.LockVertexBuffer(LockFlags.Discard);
                foreach (float f in vertexBuffer)
                    verts.Write(f);
                mesh.UnlockVertexBuffer();

                mesh.ComputeNormals();
                mesh.DrawSubset(0);
                mesh.Dispose();
            }
        }
    }
}