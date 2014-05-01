using System;
using System.IO;

namespace BulletSharp
{
    class BulletReader : BinaryReader
    {
        public BulletReader(MemoryStream stream)
            : base(stream)
        {
        }

        public float ReadSingle(int position)
        {
            BaseStream.Position = position;
            return ReadSingle();
        }

        public int ReadInt32(int position)
        {
            BaseStream.Position = position;
            return ReadInt32();
        }

        public long ReadInt64(int position)
        {
            BaseStream.Position = position;
            return ReadInt64();
        }

        public Matrix ReadMatrix()
        {
            float[] m = new float[16];
            for (int i = 0; i < 16; i++)
            {
                m[i] = ReadSingle();
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

        public Matrix ReadMatrix(int position)
        {
            BaseStream.Position = position;
            return ReadMatrix();
        }

        public long ReadPtr()
        {
            return (IntPtr.Size == 8) ? ReadInt64() : ReadInt32();
        }

        public long ReadPtr(int position)
        {
            BaseStream.Position = position;
            return ReadPtr();
        }

        public Vector3 ReadVector3()
        {
            float x = ReadSingle();
            float y = ReadSingle();
            float z = ReadSingle();
            BaseStream.Position += 4; // float w = ReadSingle();
            return new Vector3(x, y, z);
        }

        public Vector3 ReadVector3(int position)
        {
            BaseStream.Position = position;
            return ReadVector3();
        }
    }
}
