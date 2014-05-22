using BulletSharp.Math;
using System;
using System.IO;

namespace BulletSharp
{
    class BulletWriter : BinaryWriter
    {
        public BulletWriter(Stream stream)
            : base(stream)
        {
        }

        public void Write(float value, int position)
        {
            BaseStream.Position = position;
            Write(value);
        }

        public void Write(int value, int position)
        {
            BaseStream.Position = position;
            Write(value);
        }

        public void Write(long value, int position)
        {
            BaseStream.Position = position;
            Write(value);
        }

        public void Write(Matrix value)
        {
            for (int i = 0; i < 16; i++)
            {
                Write(value[i]);
            }

            /* Transpose
            Write(value[0]);
            Write(value[4]);
            Write(value[8]);
            Write(value[12]);
            Write(value[1]);
            Write(value[5]);
            Write(value[9]);
            Write(value[13]);
            Write(value[2]);
            Write(value[6]);
            Write(value[10]);
            Write(value[14]);
            Write(value[3]);
            Write(value[7]);
            Write(value[11]);
            Write(value[15]);
            */
        }

        public void Write(Matrix value, int position)
        {
            BaseStream.Position = position;
            Write(value);
        }

        public void Write(IntPtr value)
        {
            if (IntPtr.Size == 8)
            {
                Write(value.ToInt64());
            }
            else
            {
                Write(value.ToInt32());
            }
        }

        public void Write(IntPtr value, int position)
        {
            BaseStream.Position = position;
            Write(value);
        }

        public void Write(Vector3 value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
            BaseStream.Position += 4; // Write(value.W);
        }

        public void Write(Vector3 value, int position)
        {
            BaseStream.Position = position;
            Write(value);
        }
    }
}
