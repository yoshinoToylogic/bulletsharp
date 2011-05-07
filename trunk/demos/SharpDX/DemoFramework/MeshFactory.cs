﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using BulletSharp;
using BulletSharp.SoftBody;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using Buffer = SharpDX.Direct3D10.Buffer;
using Device = SharpDX.Direct3D10.Device;
using Face = BulletSharp.SoftBody.Face;
using Mesh = SharpDX.Direct3D10.Mesh;

namespace DemoFramework
{
    public struct InstanceData
    {
        public Matrix WorldTransform { get; set; }
        public uint Color { get; set; }
    }

    // Contains the geometry buffers and information of all instances of a particular shape.
    public class ShapeData : System.IDisposable
    {
        public Buffer VertexBuffer { get; private set; }
        public int VertexCount { get; set; }

        public Buffer IndexBuffer { get; set; }
        public int IndexCount { get; set; }
        public Format IndexFormat { get; set; }

        public Buffer InstanceDataBuffer { get; set; }
        public List<InstanceData> InstanceDataList { get; set; }

        public PrimitiveTopology PrimitiveTopology { get; set; }
        public VertexBufferBinding[] BufferBindings { get; private set; }

        public ShapeData()
        {
            InstanceDataList = new List<InstanceData>();
            PrimitiveTopology = PrimitiveTopology.TriangleList;
            BufferBindings = new VertexBufferBinding[2];
        }

        public void SetVertexBuffer(Device device, Vector3[] vectors)
        {
            BufferDescription vertexBufferDesc = new BufferDescription()
            {
                SizeInBytes = Marshal.SizeOf(typeof(Vector3)) * vectors.Length,
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.VertexBuffer
            };

            using (var data = new SharpDX.DataStream(vectors, false, false))
            {
                VertexBuffer = new Buffer(device, data, vertexBufferDesc);
                VertexBuffer.Unmap();
            }

            BufferBindings[0] = new VertexBufferBinding(VertexBuffer, 24, 0);
        }

        // Used with soft bodies
        public void SetDynamicVertexBuffer(Device device, Vector3[] vectors)
        {
            if (VertexBuffer != null && VertexBuffer.Description.SizeInBytes == vectors.Length * 12)
            {
                // Update existing buffer
                using (var data = VertexBuffer.Map(MapMode.WriteDiscard))
                {
                    data.WriteRange(vectors, 0, vectors.Length);
                    VertexBuffer.Unmap();
                }
            }
            else
            {
                // Create new buffer
                if (VertexBuffer != null)
                    VertexBuffer.Dispose();

                BufferDescription vertexBufferDesc = new BufferDescription()
                {
                    SizeInBytes = Marshal.SizeOf(typeof(Vector3)) * vectors.Length,
                    Usage = ResourceUsage.Dynamic,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write
                };

                using (var data = new SharpDX.DataStream(vectors, false, false))
                {
                    VertexBuffer = new Buffer(device, data, vertexBufferDesc);
                }

                BufferBindings[0] = new VertexBufferBinding(VertexBuffer, 24, 0);
            }
        }

        public void SetIndexBuffer(Device device, byte[] indices)
        {
            IndexFormat = Format.R8_UInt;

            BufferDescription boxIndexBufferDesc = new BufferDescription()
            {
                SizeInBytes = sizeof(byte) * indices.Length,
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.IndexBuffer
            };

            using (var data = new SharpDX.DataStream(indices, false, false))
            {
                IndexBuffer = new Buffer(device, data, boxIndexBufferDesc);
            }
        }

        public void Dispose()
        {
            if (VertexBuffer != null)
            {
                VertexBuffer.Dispose();
                VertexBuffer = null;
            }
        }
    }

    // This class creates graphical objects (boxes, cones, cylinders, spheres) on the fly.
    public class MeshFactory : System.IDisposable
    {
        Device device;
        Demo demo;
        //Dictionary<CollisionShape, Mesh> shapes = new Dictionary<CollisionShape, Mesh>();
        Dictionary<CollisionShape, ShapeData> shapes = new Dictionary<CollisionShape, ShapeData>();
        List<CollisionShape> removeList = new List<CollisionShape>();
        Effect planeShader = null;

        BufferDescription instanceDataDesc;
        InputLayout inputLayout;
        uint groundColor;
        uint activeColor;
        uint passiveColor;
        uint softBodyColor;
        int linkColor = System.Drawing.Color.Black.ToArgb();

        public MeshFactory(Demo demo)
        {
            this.demo = demo;
            this.device = demo.Device;
            
            instanceDataDesc = new BufferDescription()
            {
                SizeInBytes = 0,
                Usage = ResourceUsage.Dynamic,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
            };

            InputElement[] elements = new InputElement[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0),
                new InputElement("NORMAL", 0, Format.R32G32B32_Float, 12, 0, InputClassification.PerVertexData, 0),
                new InputElement("WORLD", 0, Format.R32G32B32A32_Float, 0, 1, InputClassification.PerInstanceData, 1),
                new InputElement("WORLD", 1, Format.R32G32B32A32_Float, 16, 1, InputClassification.PerInstanceData, 1),
                new InputElement("WORLD", 2, Format.R32G32B32A32_Float, 32, 1, InputClassification.PerInstanceData, 1),
                new InputElement("WORLD", 3, Format.R32G32B32A32_Float, 48, 1, InputClassification.PerInstanceData, 1),
                new InputElement("COLOR", 0, Format.R8G8B8A8_UNorm, 64, 1, InputClassification.PerInstanceData, 1)
            };
            inputLayout = new InputLayout(device, demo.ShadowGenPass.Description.Signature, elements);

            Color c = Color.Green;
            groundColor = (uint)c.R + ((uint)c.G << 8) + ((uint)c.B << 16) + ((uint)c.A << 24);
            c = Color.Orange;
            activeColor = (uint)c.R + ((uint)c.G << 8) + ((uint)c.B << 16) + ((uint)c.A << 24);
            c = Color.OrangeRed;
            passiveColor = (uint)c.R + ((uint)c.G << 8) + ((uint)c.B << 16) + ((uint)c.A << 24);
            c = Color.LightBlue;
            softBodyColor = (uint)c.R + ((uint)c.G << 8) + ((uint)c.B << 16) + ((uint)c.A << 24);
        }

        public void Clear()
        {
            foreach (ShapeData shapeData in shapes.Values)
            {
                shapeData.Dispose();
            }
            shapes.Clear();
        }

        public void Dispose()
        {
            Clear();

            if (planeShader != null)
                planeShader.Release();
        }

        ShapeData CreateBoxShape(BoxShape shape)
        {
            Vector3 size = shape.HalfExtentsWithMargin;
            float x = size.X;
            float y = size.Y;
            float z = size.Z;

            ShapeData shapeData = new ShapeData();
            shapeData.VertexCount = 36;

            Vector3[] vectors = new Vector3[shapeData.VertexCount * 2];
            int v = 0;

            // Draw two sides
            for (int i = 1; i != -3; i -= 2)
            {
                vectors[v++] = new Vector3(i * x, y, -z); // Position
                vectors[v++] = new Vector3(i, 0, 0);      // Normal
                vectors[v++] = new Vector3(i * x, -y, -z);
                vectors[v++] = new Vector3(i, 0, 0);
                vectors[v++] = new Vector3(i * x, -y, z);
                vectors[v++] = new Vector3(i, 0, 0);
                vectors[v++] = new Vector3(i * x, y, -z);
                vectors[v++] = new Vector3(i, 0, 0);
                vectors[v++] = new Vector3(i * x, y, z);
                vectors[v++] = new Vector3(i, 0, 0);
                vectors[v++] = new Vector3(i * x, -y, z);
                vectors[v++] = new Vector3(i, 0, 0);
            }

            for (int i = 1; i != -3; i -= 2)
            {
                vectors[v++] = new Vector3(-x, y, i * z);
                vectors[v++] = new Vector3(0, 0, i);
                vectors[v++] = new Vector3(-x, -y, i * z);
                vectors[v++] = new Vector3(0, 0, i);
                vectors[v++] = new Vector3(x, -y, i * z);
                vectors[v++] = new Vector3(0, 0, i);
                vectors[v++] = new Vector3(-x, y, i * z);
                vectors[v++] = new Vector3(0, 0, i);
                vectors[v++] = new Vector3(x, y, i * z);
                vectors[v++] = new Vector3(0, 0, i);
                vectors[v++] = new Vector3(x, -y, i * z);
                vectors[v++] = new Vector3(0, 0, i);
            }

            for (int i = 1; i != -3; i -= 2)
            {
                vectors[v++] = new Vector3(-x, i * y, -z);
                vectors[v++] = new Vector3(0, i, 0);
                vectors[v++] = new Vector3(x, i * y, -z);
                vectors[v++] = new Vector3(0, i, 0);
                vectors[v++] = new Vector3(-x, i * y, z);
                vectors[v++] = new Vector3(0, i, 0);
                vectors[v++] = new Vector3(x, i * y, z);
                vectors[v++] = new Vector3(0, i, 0);
                vectors[v++] = new Vector3(-x, i * y, z);
                vectors[v++] = new Vector3(0, i, 0);
                vectors[v++] = new Vector3(x, i * y, -z);
                vectors[v++] = new Vector3(0, i, 0);
            }

            shapeData.SetVertexBuffer(device, vectors);

            return shapeData;
        }
        /*
        Mesh CreateCapsuleShape(CapsuleShape shape)
        {
            // Combine a cylinder and two spheres.
            Vector3 size = shape.ImplicitShapeDimensions;
            Mesh cylinder = Mesh.CreateCylinder(device, size.X, size.X, size.Y * 2, 8, 1);
            Mesh sphere = Mesh.CreateSphere(device, size.Z, 8, 4);
            Mesh[] meshes = new Mesh[] { sphere, cylinder, sphere };
            Matrix[] transforms = new Matrix[] {
                    Matrix.Translation(0, -size.Y, 0),
                    Matrix.RotationX((float)Math.PI / 2),
                    Matrix.Translation(0, size.Y, 0)};
            Mesh mesh = Mesh.Concatenate(device, meshes, MeshFlags.Managed, transforms, null);
            cylinder.Dispose();
            sphere.Dispose();

            complexShapes.Add(shape, mesh);
            return mesh;
        }

        Mesh CreateConeShape(ConeShape shape)
        {
            Mesh mesh = Mesh.CreateCylinder(device, 0, shape.Radius, shape.Height, 16, 1);
            shapes.Add(shape, mesh);
            return mesh;
        }
        */

        Vector3 GetVectorByAxis(Vector3 vector, int axis)
        {
            switch (axis)
            {
                case 0:
                    return new Vector3(vector.Y, vector.Z, vector.X);
                case 1:
                    return new Vector3(vector.Z, vector.Y, vector.X);
                default:
                    return vector;
            }
        }

        ShapeData CreateCylinderShape(CylinderShape shape)
        {
            int up = shape.UpAxis;
            float radius = shape.Radius;
            float halfHeight = shape.HalfExtentsWithoutMargin[up] + shape.Margin;

            int numSteps = 10;
            float angleStep = (2 * (float)Math.PI) / numSteps;

            ShapeData shapeData = new ShapeData();
            shapeData.VertexCount = 2 + 6 * numSteps;
            shapeData.IndexCount = (4 * numSteps + 2) * 3;

            Vector3[] vertices = new Vector3[shapeData.VertexCount * 2];
            byte[] indices = new byte[shapeData.IndexCount];

            int i = 0, v = 0;
            byte index = 0;
            byte baseIndex;
            Vector3 normal;

            // Draw two sides
            for (int side = 1; side != -3; side -= 2)
            {
                normal = GetVectorByAxis(side * Vector3.UnitY, up);

                baseIndex = index;
                vertices[v++] = GetVectorByAxis(new Vector3(0, side * halfHeight, 0), up);
                vertices[v++] = normal;

                vertices[v++] = GetVectorByAxis(new Vector3(0, side * halfHeight, radius), up);
                vertices[v++] = normal;
                index += 2;

                for (int j = 1; j < numSteps; j++)
                {
                    float x = radius * (float)Math.Sin(j * angleStep);
                    float z = radius * (float)Math.Cos(j * angleStep);

                    vertices[v++] = GetVectorByAxis(new Vector3(x, side * halfHeight, z), up);
                    vertices[v++] = normal;

                    indices[i++] = baseIndex;
                    indices[i++] = (byte)(index - 1);
                    indices[i++] = index++;
                }
                indices[i++] = baseIndex;
                indices[i++] = (byte)(index - 1);
                indices[i++] = (byte)(baseIndex + 1);
            }


            normal = GetVectorByAxis(new Vector3(0, 0, radius), up);
            normal.Normalize();

            baseIndex = index;
            vertices[v++] = GetVectorByAxis(new Vector3(0, halfHeight, radius), up);
            vertices[v++] = normal;

            vertices[v++] = GetVectorByAxis(new Vector3(0, -halfHeight, radius), up);
            vertices[v++] = normal;
            index += 2;

            for (int j = 1; j < numSteps + 1; j++)
            {
                float x = radius * (float)Math.Sin(j * angleStep);
                float z = radius * (float)Math.Cos(j * angleStep);

                normal = GetVectorByAxis(new Vector3(x, 0, z), up);
                normal.Normalize();

                vertices[v++] = GetVectorByAxis(new Vector3(x, halfHeight, z), up);
                vertices[v++] = normal;

                vertices[v++] = GetVectorByAxis(new Vector3(x, -halfHeight, z), up);
                vertices[v++] = normal;

                indices[i++] = (byte)(index - 2);
                indices[i++] = (byte)(index - 1);
                indices[i++] = (byte)index;
                indices[i++] = (byte)index;
                indices[i++] = (byte)(index - 1);
                indices[i++] = (byte)(index + 1);
                index += 2;
            }
            indices[i++] = (byte)(index - 2);
            indices[i++] = (byte)(index - 1);
            indices[i++] = (byte)(baseIndex);
            indices[i++] = (byte)(baseIndex);
            indices[i++] = (byte)(index - 1);
            indices[i++] = (byte)(baseIndex + 1);

            shapeData.SetVertexBuffer(device, vertices);
            shapeData.SetIndexBuffer(device, indices);

            return shapeData;
        }
        /*
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

            SlimDX.DataStream vertexBuffer = mesh.LockVertexBuffer(LockFlags.Discard);
            while (vertexBuffer.Position < vertexBuffer.Length)
            {
                vertexBuffer.Write(verts.Read<Vector3>());
                vertexBuffer.Position += 12;
            }
            mesh.UnlockVertexBuffer();

            SlimDX.DataStream indexBuffer = mesh.LockIndexBuffer(LockFlags.Discard);
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
            return mesh;
        }

        Mesh CreateConvexHullShape(ConvexHullShape shape)
        {
            int vertexCount = shape.NumPoints;
            int faceCount = vertexCount / 3;
            vertexCount = faceCount * 3; // must be 3 verts for every face

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
            mesh.UnlockVertexBuffer();

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
                Vector3 position = shape.GetSpherePosition(i);

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
        */
        ShapeData CreateSphereShape(SphereShape shape)
        {
            float radius = shape.Radius;

            int slices = (int)(radius * 10.0f);
            int stacks = (int)(radius * 10.0f);
            slices = (slices > 16) ? 16 : (slices < 3) ? 3 : slices;
            stacks = (stacks > 16) ? 16 : (stacks < 2) ? 2 : stacks;

            float hAngleStep = (float)Math.PI * 2 / slices;
            float vAngleStep = (float)Math.PI / stacks;

            ShapeData shapeData = new ShapeData();

            shapeData.VertexCount = 2 + slices * (stacks - 1);
            shapeData.IndexCount = 6 * slices * (stacks - 1);

            Vector3[] vertices = new Vector3[shapeData.VertexCount * 2];
            byte[] indices = new byte[shapeData.IndexCount];

            int i = 0, v = 0;


            // Vertices
            // Top and bottom
            vertices[v++] = new Vector3(0, -radius, 0);
            vertices[v++] = -Vector3.UnitY;
            vertices[v++] = new Vector3(0, radius, 0);
            vertices[v++] = Vector3.UnitY;

            // Stacks
            int j, k;
            float angle = 0;
            float vAngle = -(float)Math.PI / 2;
            Vector3 vTemp;
            for (j = 0; j < stacks - 1; j++)
            {
                vAngle += vAngleStep;

                for (k = 0; k < slices; k++)
                {
                    angle += hAngleStep;

                    vTemp = new Vector3((float)Math.Cos(vAngle) * (float)Math.Sin(angle), (float)Math.Sin(vAngle), (float)Math.Cos(vAngle) * (float)Math.Cos(angle));
                    vertices[v++] = vTemp * radius;
                    vertices[v++] = Vector3.Normalize(vTemp);
                }
            }


            // Indices
            // Top cap
            byte index = 2;
            for (k = 0; k < slices; k++)
            {
                indices[i++] = 0;
                indices[i++] = index;
                index++;
                indices[i++] = index;
            }
            indices[i - 1] = 2;

            // Stacks
            //for (j = 0; j < 1; j++)
            int sliceDiff = slices * 3;
            for (j = 0; j < stacks - 2; j++)
            {
                for (k = 0; k < slices; k++)
                {
                    indices[i++] = indices[i - sliceDiff];
                    indices[i++] = indices[i - sliceDiff];
                    indices[i++] = index;
                    index++;
                }

                for (k = 0; k < slices; k++)
                {
                    indices[i++] = indices[i - sliceDiff];
                    indices[i++] = indices[i - sliceDiff];
                    indices[i++] = indices[i - sliceDiff + 2];
                }
                indices[i - 1] = indices[i - sliceDiff + 1];
            }

            // Bottom cap
            index--;
            for (k = 0; k < slices; k++)
            {
                indices[i++] = 1;
                indices[i++] = index;
                index--;
                indices[i++] = index;
            }
            indices[i - 1] = indices[i - sliceDiff + 1];

            shapeData.SetVertexBuffer(device, vertices);
            shapeData.SetIndexBuffer(device, indices);

            return shapeData;
        }
        /*
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

            Vector3 planeOrigin = shape.PlaneNormal * shape.PlaneConstant;
            Vector3 vec0, vec1;
            PlaneSpace1(shape.PlaneNormal, out vec0, out vec1);
            float size = 1000;

            Vector3[] verts = new Vector3[4]
                {
                    planeOrigin + vec0*size,
                    planeOrigin - vec0*size,
                    planeOrigin + vec1*size,
                    planeOrigin - vec1*size
                };

            SlimDX.DataStream vertexBuffer = mesh.LockVertexBuffer(LockFlags.Discard);
            vertexBuffer.Write(verts[0]);
            vertexBuffer.Position += 12;
            vertexBuffer.Write(verts[1]);
            vertexBuffer.Position += 12;
            vertexBuffer.Write(verts[2]);
            vertexBuffer.Position += 12;
            vertexBuffer.Write(verts[3]);
            vertexBuffer.Position += 12;
            mesh.UnlockVertexBuffer();

            SlimDX.DataStream indexBuffer = mesh.LockIndexBuffer(LockFlags.Discard);
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
        */

        ShapeData InitShapeData(CollisionShape shape)
        {
            ShapeData shapeData;

            if (shapes.TryGetValue(shape, out shapeData) == false)
            {
                switch (shape.ShapeType)
                {
                    case BroadphaseNativeType.SoftBodyShape:
                        shapeData = CreateSoftBody();
                        break;
                    case BroadphaseNativeType.BoxShape:
                        shapeData = CreateBoxShape(shape as BoxShape);
                        break;
                    case BroadphaseNativeType.CylinderShape:
                        shapeData = CreateCylinderShape(shape as CylinderShape);
                        break;
                    case BroadphaseNativeType.CompoundShape:
                        CompoundShape compoundShape = shape as CompoundShape;
                        foreach (CompoundShapeChild child in compoundShape.ChildList)
                        {
                            InitShapeData(child.ChildShape);
                        }
                        return null;
                    case BroadphaseNativeType.SphereShape:
                        shapeData = CreateSphereShape(shape as SphereShape);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                // Create an initial instance data buffer for a single instance
                instanceDataDesc.SizeInBytes = Marshal.SizeOf(typeof(InstanceData));
                shapeData.InstanceDataBuffer = new Buffer(device, instanceDataDesc);
                shapeData.BufferBindings[1] = new VertexBufferBinding(shapeData.InstanceDataBuffer, instanceDataDesc.SizeInBytes, 0);

                shapes.Add(shape, shapeData);
            }

            return shapeData;
        }

        void InitInstanceData(CollisionObject colObj, CollisionShape shape, ShapeData shapeData, Matrix transform)
        {
            if (shape.ShapeType == BroadphaseNativeType.CompoundShape)
            {
                CompoundShape compoundShape = shape as CompoundShape;
                foreach (CompoundShapeChild child in compoundShape.ChildList)
                {
                    InitInstanceData(colObj, child.ChildShape, shapes[child.ChildShape], child.Transform * transform);
                }
            }
            else if (shape.ShapeType == BroadphaseNativeType.SoftBodyShape)
            {
                UpdateSoftBody(colObj as SoftBody, shapeData);

                InstanceData instanceData = new InstanceData()
                {
                    WorldTransform = transform,
                    Color = softBodyColor
                };
                shapeData.InstanceDataList.Add(instanceData);
            }
            else
            {
                InstanceData instanceData = new InstanceData()
                {
                    WorldTransform = transform,
                    Color = ground.Equals(colObj.UserObject) ? groundColor :
                        colObj.ActivationState == ActivationState.ActiveTag ? activeColor : passiveColor
                };
                shapeData.InstanceDataList.Add(instanceData);
            }
        }

        string ground = "Ground";

        public void InitInstancedRender(AlignedCollisionObjectArray objects)
        {
            // Clear instance data
            foreach (ShapeData s in shapes.Values)
                s.InstanceDataList.Clear();
            removeList.Clear();

            int i = objects.Count - 1;
            for (; i >= 0; i--)
            {
                CollisionObject colObj = objects[i];
                CollisionShape shape = colObj.CollisionShape;
                ShapeData shapeData = InitShapeData(shape);

                Matrix transform;
                if (colObj is SoftBody)
                {
                    transform = Matrix.Identity;
                }
                else
                {
                    transform = (colObj as RigidBody).MotionState.WorldTransform;
                }
                InitInstanceData(colObj, shape, shapeData, transform);
            }

            foreach (KeyValuePair<CollisionShape, ShapeData> sh in shapes)
            {
                ShapeData s = sh.Value;

                // Is the instance buffer the right size?
                if (s.InstanceDataBuffer.Description.SizeInBytes != s.InstanceDataList.Count * 68)
                {
                    // No, recreate it
                    s.InstanceDataBuffer.Dispose();

                    if (s.InstanceDataList.Count == 0)
                    {
                        if (s.IndexBuffer != null)
                            s.IndexBuffer.Dispose();
                        s.VertexBuffer.Dispose();
                        removeList.Add(sh.Key);
                        continue;
                    }

                    instanceDataDesc.SizeInBytes = s.InstanceDataList.Count * 68;
                    s.InstanceDataBuffer = new Buffer(device, instanceDataDesc);
                    s.BufferBindings[1] = new VertexBufferBinding(s.InstanceDataBuffer, 68, 0);
                }

                // Copy the instance data over to the instance buffer
                using (var data = s.InstanceDataBuffer.Map(MapMode.WriteDiscard))
                {
                    List<InstanceData> list = s.InstanceDataList;
                    for (i = 0; i < list.Count; i++)
                    {
                        InstanceData instance = list[i];
                        data.Write(instance.WorldTransform);
                        data.Write(instance.Color);
                    }
                    s.InstanceDataBuffer.Unmap();
                }
            }

            if (removeList.Count != 0)
            {
                for (i = removeList.Count - 1; i >= 0; i--)
                {
                    shapes.Remove(removeList[i]);
                }
            }
        }

        public void RenderInstanced()
        {
            Device.InputAssemblerStage ia = device.InputAssembler;
            ia.SetInputLayout(inputLayout);

            foreach (ShapeData s in shapes.Values)
            {
                ia.SetVertexBuffers(0, s.BufferBindings);
                ia.SetPrimitiveTopology(s.PrimitiveTopology);
                if (s.IndexBuffer != null)
                {
                    ia.SetIndexBuffer(s.IndexBuffer, s.IndexFormat, 0);
                    device.DrawIndexedInstanced(s.IndexCount, s.InstanceDataList.Count, 0, 0, 0);
                }
                else
                {
                    device.DrawInstanced(s.VertexCount, s.InstanceDataList.Count, 0, 0);
                }
            }
        }

        public void RenderComplexShape(CollisionShape shape, Mesh mesh)
        {
            switch (shape.ShapeType)
            {
                case BroadphaseNativeType.StaticPlane:
                    //RenderStaticPlaneShape(mesh);
                    break;
                case BroadphaseNativeType.CapsuleShape:
                    //RenderCapsuleShape(mesh);
                    break;
                case BroadphaseNativeType.MultiSphereShape:
                    //RenderMultiSphereShape((MultiSphereShape)shape, mesh);
                    break;
            }
        }
        /*
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

        void PlaneSpace1(Vector3 n, out Vector3 p, out Vector3 q)
        {
            if (Math.Abs(n[2]) > (Math.Sqrt(2) / 2))
            {
                // choose p in y-z plane
                float a = n[1] * n[1] + n[2] * n[2];
                float k = 1.0f / (float)Math.Sqrt(a);
                p = new Vector3(0, -n[2] * k, n[1] * k);
                // set q = n x p
                q = Vector3.Cross(n, p);
            }
            else
            {
                // choose p in x-y plane
                float a = n[0] * n[0] + n[1] * n[1];
                float k = 1.0f / (float)Math.Sqrt(a);
                p = new Vector3(-n[1] * k, n[0] * k, 0);
                // set q = n x p
                q = Vector3.Cross(n, p);
            }
        }
        */

        public ShapeData CreateSoftBody()
        {
            // Soft body geometry is recreated each frame. Nothing to do here.
            return new ShapeData();
        }

        public void UpdateSoftBody(SoftBody softBody, ShapeData shapeData)
        {
            AlignedFaceArray faces = softBody.Faces;

            if (faces.Count > 0)
            {
                shapeData.VertexCount = faces.Count * 3;

                Vector3[] vectors = new Vector3[shapeData.VertexCount * 2];
                int v = 0;

                int i;
                for (i = 0; i<faces.Count; i++)
                {
                    NodePtrArray nodes = faces[i].N;
                    Node n0 = nodes[0];
                    Node n1 = nodes[1];
                    Node n2 = nodes[2];
                    vectors[v] = n0.X;
                    vectors[v + 1] = n0.Normal;
                    vectors[v + 2] = n1.X;
                    vectors[v + 3] = n1.Normal;
                    vectors[v + 4] = n2.X;
                    vectors[v + 5] = n2.Normal;
                    v += 6;
                }

                shapeData.SetDynamicVertexBuffer(device, vectors);
            }
            else
            {
                AlignedTetraArray tetras = softBody.Tetras;
                int tetraCount = tetras.Count;

                if (tetraCount > 0)
                {
                    shapeData.VertexCount = tetraCount * 12;

                    Vector3[] vectors = new Vector3[tetraCount * 24];
                    int v = 0;

                    int count = tetras.Count;
                    for (int i = 0; i < count; i++)
                    {
                        NodePtrArray nodes = tetras[i].Nodes;
                        Vector3 v0 = nodes[0].X;
                        Vector3 v1 = nodes[1].X;
                        Vector3 v2 = nodes[2].X;
                        Vector3 v3 = nodes[3].X;
                        Vector3 v10 = v1 - v0;
                        Vector3 v02 = v0 - v2;

                        Vector3 normal = Vector3.Cross(v10, v02);
                        vectors[v] = v0;
                        vectors[v + 1] = normal;
                        vectors[v + 2] = v1;
                        vectors[v + 3] = normal;
                        vectors[v + 4] = v2;
                        vectors[v + 5] = normal;

                        normal = Vector3.Cross(v10, v3 - v0);
                        vectors[v + 6] = v0;
                        vectors[v + 7] = normal;
                        vectors[v + 8] = v1;
                        vectors[v + 9] = normal;
                        vectors[v + 10] = v3;
                        vectors[v + 11] = normal;

                        normal = Vector3.Cross(v2 - v1, v3 - v1);
                        vectors[v + 12] = v1;
                        vectors[v + 13] = normal;
                        vectors[v + 14] = v2;
                        vectors[v + 15] = normal;
                        vectors[v + 16] = v3;
                        vectors[v + 17] = normal;

                        normal = Vector3.Cross(v02, v3 - v2);
                        vectors[v + 18] = v2;
                        vectors[v + 19] = normal;
                        vectors[v + 20] = v0;
                        vectors[v + 21] = normal;
                        vectors[v + 22] = v3;
                        vectors[v + 23] = normal;
                        v += 24;
                    }

                    shapeData.SetDynamicVertexBuffer(device, vectors);
                }
                else if (softBody.Links.Count > 0)
                {
                    AlignedLinkArray links = softBody.Links;
                    int linkCount = links.Count;
                    shapeData.VertexCount = linkCount * 2;

                    Vector3[] vectors = new Vector3[linkCount * 4];

                    for (int i = 0; i < linkCount; i++)
                    {
                        NodePtrArray nodes = links[i].Nodes;
                        vectors[i * 4] = nodes[0].X;
                        vectors[i * 4 + 2] = nodes[1].X;
                    }

                    shapeData.PrimitiveTopology = PrimitiveTopology.LineList;
                    shapeData.SetDynamicVertexBuffer(device, vectors);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        /*
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
         * */

        public static Buffer CreateScreenQuad(Device device)
        {
            Buffer vertexBuffer;

            BufferDescription vertexBufferDesc = new BufferDescription()
            {
                SizeInBytes = sizeof(float) * 5 * 4,
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.VertexBuffer,
            };

            using (var data = new SharpDX.DataStream(vertexBufferDesc.SizeInBytes, false, true))
            {
                data.Write(new Vector3(0.5f, 0.5f, 0));
                data.Write(new Vector2(1, 0));
                data.Write(new Vector3(0.5f, -0.5f, 0));
                data.Write(new Vector2(1, 1));
                data.Write(new Vector3(-0.5f, 0.5f, 0));
                data.Write(new Vector2(0, 0));
                data.Write(new Vector3(-0.5f, -0.5f, 0));
                data.Write(new Vector2(0, 1));
                vertexBuffer = new Buffer(device, data, vertexBufferDesc);
                vertexBuffer.Unmap();
            }

            return vertexBuffer;
        }
    }
}