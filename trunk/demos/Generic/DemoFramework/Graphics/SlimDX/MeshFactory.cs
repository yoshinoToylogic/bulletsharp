﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BulletSharp;
using BulletSharp.SoftBody;
using SlimDX.Direct3D9;
using DataStream = SlimDX.DataStream;
using Matrix = SlimDX.Matrix;
using Vector3 = SlimDX.Vector3;

namespace DemoFramework.SlimDX
{
    // This class creates graphical objects (boxes, cones, cylinders, spheres) on the fly.
    public class MeshFactory : System.IDisposable
    {
        Device device;
        Dictionary<CollisionShape, Mesh> shapes = new Dictionary<CollisionShape, Mesh>();
        Dictionary<CollisionShape, Mesh> complexShapes = new Dictionary<CollisionShape, Mesh>(); // these have more than 1 subset
        Effect planeShader;

        public MeshFactory(SlimDXGraphics graphics)
        {
            this.device = graphics.Device;
        }

        public void OnLostDevice()
        {
            if (planeShader != null)
                planeShader.OnLostDevice();
        }

        public void OnResetDevice()
        {
            if (planeShader != null)
                planeShader.OnResetDevice();
        }

        public void Dispose()
        {
            foreach (Mesh mesh in shapes.Values)
            {
                mesh.Dispose();
            }
            shapes.Clear();

            foreach (Mesh mesh in complexShapes.Values)
            {
                mesh.Dispose();
            }
            complexShapes.Clear();

            if (planeShader != null)
                planeShader.Dispose();
        }

        Mesh CreateShape(CollisionShape shape)
        {
            ushort[] indices;
            BulletSharp.Vector3[] vertices = ShapeGenerator.CreateShape(shape, out indices);

            int vertexCount = vertices.Length / 2;
            int indexCount = (indices != null) ? indices.Length : vertexCount;
            bool index32 = indexCount > 65536;

            Mesh mesh = new Mesh(device, indexCount / 3, vertexCount,
                MeshFlags.SystemMemory | (index32 ? MeshFlags.Use32Bit : 0), VertexFormat.Position | VertexFormat.Normal);

            int i = 0;
            DataStream vertexBuffer = mesh.LockVertexBuffer(LockFlags.Discard);
            vertexBuffer.WriteRange(vertices);
            mesh.UnlockVertexBuffer();

            if (indices == null)
            {
                indices = new ushort[indexCount];
                i = 0;
                while (i < indexCount)
                {
                    indices[i] = (ushort)i;
                    i++;
                }
            }

            i = 0;
            DataStream indexBuffer = mesh.LockIndexBuffer(LockFlags.Discard);
            if (index32)
            {
                while (indexBuffer.Position < indexBuffer.Length)
                    indexBuffer.Write((int)indices[i++]);
            }
            else
            {
                indexBuffer.WriteRange(indices);
            }
            mesh.UnlockIndexBuffer();

            shapes.Add(shape, mesh);
            return mesh;
        }

        Mesh CreateConeShape(ConeShape shape)
        {
            Mesh mesh = Mesh.CreateCylinder(device, 0, shape.Radius, shape.Height, 16, 1);
            shapes.Add(shape, mesh);
            return mesh;
        }

        Mesh CreateGImpactMeshShape(GImpactMeshShape shape)
        {
            BulletSharp.DataStream verts, indices;
            int numVerts, numFaces;
            PhyScalarType vertsType, indicesType;
            int vertexStride, indexStride;
            shape.MeshInterface.GetLockedReadOnlyVertexIndexData(out verts, out numVerts, out vertsType, out vertexStride,
                out indices, out indexStride, out numFaces, out indicesType);

            bool index32 = numVerts > 65536;

            Mesh mesh = new Mesh(device, numFaces, numVerts,
                MeshFlags.SystemMemory | (index32 ? MeshFlags.Use32Bit : 0), VertexFormat.Position | VertexFormat.Normal);

            DataStream vertexBuffer = mesh.LockVertexBuffer(LockFlags.Discard);
            while (vertexBuffer.Position < vertexBuffer.Length)
            {
                vertexBuffer.Write(verts.Read<Vector3>());
                vertexBuffer.Position += 12;
            }
            mesh.UnlockVertexBuffer();

            DataStream indexBuffer = mesh.LockIndexBuffer(LockFlags.Discard);
            if (index32)
            {
                while (indexBuffer.Position < indexBuffer.Length)
                    indexBuffer.Write(indices.Read<int>());
            }
            else
            {
                while (indexBuffer.Position < indexBuffer.Length)
                    indexBuffer.Write((short)indices.Read<int>());
            }
            mesh.UnlockIndexBuffer();

            mesh.ComputeNormals();
            shapes.Add(shape, mesh);

            return mesh;
        }

        Mesh CreateMultiSphereShape(MultiSphereShape shape)
        {
            Mesh mesh = null;

            int i;
            for (i = 0; i < shape.SphereCount; i++)
            {
                Vector3 position = MathHelper.Convert(shape.GetSpherePosition(i));

                Mesh sphereMesh = Mesh.CreateSphere(device, shape.GetSphereRadius(i), 12, 12);
                if (i == 0)
                {
                    Matrix[] transform = new Matrix[] { Matrix.Translation(position) };
                    mesh = Mesh.Concatenate(device, new Mesh[] { sphereMesh }, MeshFlags.Managed, transform, null);
                }
                else
                {
                    Mesh multiSphereMeshNew;
                    Matrix[] transform = new Matrix[] { Matrix.Identity, Matrix.Translation(position) };
                    multiSphereMeshNew = Mesh.Concatenate(device, new Mesh[] { mesh, sphereMesh }, MeshFlags.Managed, transform, null);
                    mesh.Dispose();
                    mesh = multiSphereMeshNew;
                }
                sphereMesh.Dispose();
            }

            complexShapes.Add(shape, mesh);
            return mesh;
        }

        Mesh CreateStaticPlaneShape(StaticPlaneShape shape)
        {
            // Load shader
            if (planeShader == null)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream shaderStream = assembly.GetManifestResourceStream("DemoFramework.checker_shader.fx");

                planeShader = Effect.FromStream(device, shaderStream, ShaderFlags.None);
            }


            Vector3[] vertices = new Vector3[4 * 2];

            Mesh mesh = new Mesh(device, 2, 4, MeshFlags.SystemMemory, VertexFormat.Position | VertexFormat.Normal);

            BulletSharp.Vector3 planeOrigin = shape.PlaneNormal * shape.PlaneConstant;
            BulletSharp.Vector3 vec0, vec1;
            PlaneSpace1(shape.PlaneNormal, out vec0, out vec1);
            float size = 1000;

            BulletSharp.Vector3[] verts = new BulletSharp.Vector3[4]
                {
                    planeOrigin + vec0*size,
                    planeOrigin - vec0*size,
                    planeOrigin + vec1*size,
                    planeOrigin - vec1*size
                };

            DataStream vertexBuffer = mesh.LockVertexBuffer(LockFlags.Discard);
            vertexBuffer.Write(verts[0]);
            vertexBuffer.Position += 12;
            vertexBuffer.Write(verts[1]);
            vertexBuffer.Position += 12;
            vertexBuffer.Write(verts[2]);
            vertexBuffer.Position += 12;
            vertexBuffer.Write(verts[3]);
            vertexBuffer.Position += 12;
            mesh.UnlockVertexBuffer();

            DataStream indexBuffer = mesh.LockIndexBuffer(LockFlags.Discard);
            indexBuffer.Write((short)1);
            indexBuffer.Write((short)2);
            indexBuffer.Write((short)0);
            indexBuffer.Write((short)1);
            indexBuffer.Write((short)3);
            indexBuffer.Write((short)0);
            mesh.UnlockIndexBuffer();

            mesh.ComputeNormals();

            complexShapes.Add(shape, mesh);

            return mesh;
        }

        Mesh CreateTriangleMeshShape(TriangleMeshShape shape)
        {
            StridingMeshInterface meshInterface = shape.MeshInterface;

            BulletSharp.DataStream verts, indices;
            int numVerts, numFaces;
            PhyScalarType vertsType, indicesType;
            int vertexStride, indexStride;
            meshInterface.GetLockedReadOnlyVertexIndexData(out verts, out numVerts, out vertsType, out vertexStride,
                out indices, out indexStride, out numFaces, out indicesType);

            bool index32 = numVerts > 65536;

            Mesh mesh = new Mesh(device, numFaces, numVerts,
                 MeshFlags.SystemMemory | (index32 ? MeshFlags.Use32Bit : 0), VertexFormat.Position | VertexFormat.Normal);
            DataStream data = mesh.LockVertexBuffer(LockFlags.None);
            while (verts.Position < verts.Length)
            {
                Vector3 v = verts.Read<Vector3>();
                data.Write(v);

                verts.Position += vertexStride - 12;

                // Normals will be calculated later
                data.Position += 12;
            }
            mesh.UnlockVertexBuffer();

            data = mesh.LockIndexBuffer(LockFlags.None);
            while (indices.Position < indices.Length)
            {
                int index = indices.Read<int>();
                if (index32)
                    data.Write(index);
                else
                    data.Write((short)index);
            }
            mesh.UnlockVertexBuffer();

            mesh.ComputeNormals();

            shapes.Add(shape, mesh);
            return mesh;
        }

        public void Render(CollisionObject body)
        {
            switch (body.CollisionShape.ShapeType)
            {
                case BroadphaseNativeType.SoftBodyShape:
                    device.SetTransform(TransformState.World, Matrix.Identity);
                    RenderSoftBody(body as SoftBody);
                    break;
                case BroadphaseNativeType.CompoundShape:
                    CompoundShape compoundShape = body.CollisionShape as CompoundShape;
                    //if (compoundShape.NumChildShapes == 0)
                    //    return;
                    foreach (CompoundShapeChild child in compoundShape.ChildList)
                    {
                        device.SetTransform(TransformState.World,
                            MathHelper.Convert(child.Transform) * MathHelper.Convert(body.WorldTransform));
                        Render(child.ChildShape);
                    }
                    break;
                default:
                    device.SetTransform(TransformState.World, MathHelper.Convert(body.WorldTransform));
                    Render(body.CollisionShape);
                    break;
            }
        }

        public void Render(CollisionShape shape)
        {
            Mesh mesh;

            if (shapes.TryGetValue(shape, out mesh))
            {
                mesh.DrawSubset(0);
                return;
            }

            if (complexShapes.TryGetValue(shape, out mesh))
            {
                RenderComplexShape(shape, mesh);
                return;
            }

            // Create the graphics mesh or go to child shapes.
            switch (shape.ShapeType)
            {
                case BroadphaseNativeType.ConeShape:
                    mesh = CreateConeShape(shape as ConeShape);
                    break;
                case BroadphaseNativeType.GImpactShape:
                    mesh = CreateGImpactMeshShape(shape as GImpactMeshShape);
                    break;
                case BroadphaseNativeType.Convex2DShape:
                    Render((shape as Convex2DShape).ChildShape);
                    return;
                case BroadphaseNativeType.TriangleMeshShape:
                    mesh = CreateTriangleMeshShape(shape as TriangleMeshShape);
                    break;
                default:
                    mesh = CreateShape(shape);
                    break;
            }

            // If the shape has one subset, render it.
            if (mesh != null)
            {
                mesh.DrawSubset(0);
                return;
            }

            switch (shape.ShapeType)
            {
                case BroadphaseNativeType.MultiSphereShape:
                    mesh = CreateMultiSphereShape(shape as MultiSphereShape);
                    break;
                case BroadphaseNativeType.StaticPlane:
                    mesh = CreateStaticPlaneShape(shape as StaticPlaneShape);
                    break;
            }

            RenderComplexShape(shape, mesh);
        }

        public void RenderComplexShape(CollisionShape shape, Mesh mesh)
        {
            switch (shape.ShapeType)
            {
                case BroadphaseNativeType.StaticPlane:
                    RenderStaticPlaneShape(mesh);
                    break;
                case BroadphaseNativeType.CapsuleShape:
                    RenderCapsuleShape(mesh);
                    break;
                case BroadphaseNativeType.MultiSphereShape:
                    RenderMultiSphereShape(shape as MultiSphereShape, mesh);
                    break;
            }
        }

        public void RenderCapsuleShape(Mesh mesh)
        {
            mesh.DrawSubset(0);
            mesh.DrawSubset(1);
            mesh.DrawSubset(2);
        }

        public void RenderMultiSphereShape(MultiSphereShape shape, Mesh mesh)
        {
            int count = shape.SphereCount;
            for (int i = 0; i < count; i++)
                mesh.DrawSubset(i);
        }

        void RenderStaticPlaneShape(Mesh mesh)
        {
            Cull cullMode = device.GetRenderState<Cull>(RenderState.CullMode);
            device.SetRenderState(RenderState.CullMode, Cull.None);
            planeShader.Begin();
            Matrix matrix = device.GetTransform(TransformState.World);
            planeShader.SetValue("World", matrix);
            matrix = device.GetTransform(TransformState.View) * device.GetTransform(TransformState.Projection);
            planeShader.SetValue("ViewProjection", matrix);
            planeShader.BeginPass(0);
            mesh.DrawSubset(0);
            planeShader.EndPass();
            planeShader.End();
            device.SetRenderState(RenderState.CullMode, cullMode);
        }

        void PlaneSpace1(BulletSharp.Vector3 n, out BulletSharp.Vector3 p, out BulletSharp.Vector3 q)
        {
            if (Math.Abs(n[2]) > (Math.Sqrt(2) / 2))
            {
                // choose p in y-z plane
                float a = n[1] * n[1] + n[2] * n[2];
                float k = 1.0f / (float)Math.Sqrt(a);
                p = new BulletSharp.Vector3(0, -n[2] * k, n[1] * k);
                // set q = n x p
                q = BulletSharp.Vector3.Cross(n, p);
            }
            else
            {
                // choose p in x-y plane
                float a = n[0] * n[0] + n[1] * n[1];
                float k = 1.0f / (float)Math.Sqrt(a);
                p = new BulletSharp.Vector3(-n[1] * k, n[0] * k, 0);
                // set q = n x p
                q = BulletSharp.Vector3.Cross(n, p);
            }
        }

        public void RenderSoftBody(SoftBody softBody)
        {
            Cull cullMode = device.GetRenderState<Cull>(RenderState.CullMode);
            device.SetRenderState(RenderState.CullMode, Cull.None);

            AlignedFaceArray faces = softBody.Faces;
            int faceCount = faces.Count;

            if (faceCount > 0)
            {
                PositionedNormal[] vectors = new PositionedNormal[faceCount * 6];
                int v = 0;

                int i;
                for (i = 0; i < faces.Count; i++)
                {
                    NodePtrArray nodes = faces[i].N;
                    Node n0 = nodes[0];
                    Node n1 = nodes[1];
                    Node n2 = nodes[2];
                    n0.GetX(out vectors[v].Position);
                    n0.GetNormal(out vectors[v].Normal);
                    n1.GetX(out vectors[v + 1].Position);
                    n1.GetNormal(out vectors[v + 1].Normal);
                    n2.GetX(out vectors[v + 2].Position);
                    n2.GetNormal(out vectors[v + 2].Normal);
                    v += 3;
                }

                device.VertexFormat = PositionedNormal.FVF;
                device.DrawUserPrimitives(PrimitiveType.TriangleList, faces.Count, vectors);
            }
            else
            {
                AlignedTetraArray tetras = softBody.Tetras;
                int tetraCount = tetras.Count;

                if (tetraCount > 0)
                {
                    PositionedNormal[] vectors = new PositionedNormal[tetraCount * 12];
                    int v = 0;

                    for (int i = 0; i < tetraCount; i++)
                    {
                        NodePtrArray nodes = tetras[i].Nodes;
                        BulletSharp.Vector3 v0 = nodes[0].X;
                        BulletSharp.Vector3 v1 = nodes[1].X;
                        BulletSharp.Vector3 v2 = nodes[2].X;
                        BulletSharp.Vector3 v3 = nodes[3].X;
                        BulletSharp.Vector3 v10 = v1 - v0;
                        BulletSharp.Vector3 v02 = v0 - v2;

                        BulletSharp.Vector3 normal = BulletSharp.Vector3.Cross(v10, v02);
                        normal.Normalize();
                        vectors[v].Position = v0;
                        vectors[v].Normal = normal;
                        vectors[v + 1].Position = v1;
                        vectors[v + 1].Normal = normal;
                        vectors[v + 2].Position = v2;
                        vectors[v + 2].Normal = normal;

                        normal = BulletSharp.Vector3.Cross(v10, v3 - v0);
                        normal.Normalize();
                        vectors[v + 3].Position = v0;
                        vectors[v + 3].Normal = normal;
                        vectors[v + 4].Position = v1;
                        vectors[v + 4].Normal = normal;
                        vectors[v + 5].Position = v3;
                        vectors[v + 5].Normal = normal;

                        normal = BulletSharp.Vector3.Cross(v2 - v1, v3 - v1);
                        normal.Normalize();
                        vectors[v + 6].Position = v1;
                        vectors[v + 6].Normal = normal;
                        vectors[v + 7].Position = v2;
                        vectors[v + 7].Normal = normal;
                        vectors[v + 8].Position = v3;
                        vectors[v + 8].Normal = normal;

                        normal = BulletSharp.Vector3.Cross(v02, v3 - v2);
                        normal.Normalize();
                        vectors[v + 9].Position = v2;
                        vectors[v + 9].Normal = normal;
                        vectors[v + 10].Position = v0;
                        vectors[v + 10].Normal = normal;
                        vectors[v + 11].Position = v3;
                        vectors[v + 11].Normal = normal;
                        v += 12;
                    }
                    device.VertexFormat = PositionedNormal.FVF;
                    device.DrawUserPrimitives(PrimitiveType.TriangleList, tetraCount * 4, vectors);
                }
                else if (softBody.Links.Count > 0)
                {
                    AlignedLinkArray links = softBody.Links;
                    int linkCount = links.Count;
                    int linkColor = System.Drawing.Color.Black.ToArgb();

                    device.VertexFormat = PositionColored.FVF;

                    PositionColored[] linkArray = new PositionColored[linkCount * 2];

                    for (int i = 0; i < linkCount; i++)
                    {
                        Link link = links[i];
                        linkArray[i * 2].Position = link.Nodes[0].X;
                        linkArray[i * 2].Color = linkColor;
                        linkArray[i * 2 + 1].Position = link.Nodes[1].X;
                        linkArray[i * 2 + 1].Color = linkColor;
                    }
                    device.DrawUserPrimitives(PrimitiveType.LineList, links.Count, linkArray);
                }
            }

            device.SetRenderState(RenderState.CullMode, cullMode);
        }

        public void RenderSoftBodyTextured(SoftBody softBody)
        {
            if (!(softBody.UserObject is Array))
                return;

            object[] userObjArr = softBody.UserObject as object[];
            FloatArray vertexBuffer = userObjArr[0] as FloatArray;
            IntArray indexBuffer = userObjArr[1] as IntArray;

            int vertexCount = (vertexBuffer.Count / 8);

            if (vertexCount > 0)
            {
                int faceCount = indexBuffer.Count / 2;

                bool index32 = vertexCount > 65536;

                Mesh mesh = new Mesh(device, faceCount, vertexCount,
                    MeshFlags.SystemMemory | (index32 ? MeshFlags.Use32Bit : 0),
                    VertexFormat.Position | VertexFormat.Normal | VertexFormat.Texture1);

                DataStream indices = mesh.LockIndexBuffer(LockFlags.Discard);
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

                DataStream verts = mesh.LockVertexBuffer(LockFlags.Discard);
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