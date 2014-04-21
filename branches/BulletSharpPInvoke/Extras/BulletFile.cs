using System;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;

namespace BulletSharp
{
    class DnaID
    {
        public static readonly int Sdna = MakeID("SDNA");

        public static readonly int Array = MakeID("ARAY");
        public static readonly int BoxShape = MakeID("BOXS");
        public static readonly int CollisionObject = MakeID("COBJ");
        public static readonly int Constraint = MakeID("CONS");
        public static readonly int Dna = MakeID("DNA1");
        public static readonly int DynamicsWorld = MakeID("DWLD");
        public static readonly int QuantizedBvh = MakeID("QBVH");
        public static readonly int RigidBody = MakeID("RBDY");
        public static readonly int SBMaterial = MakeID("SBMT");
        public static readonly int SBNode = MakeID("SBND");
        public static readonly int Shape = MakeID("SHAP");
        public static readonly int SoftBody = MakeID("SBDY");
        public static readonly int TriangleInfoMap = MakeID("TMAP");

        static int MakeID(string id)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (id[0]) + (id[1]  << 8) + (id[2] << 16) + (id[3] << 24);
            }
            return (id[3]) + (id[2] << 8) + (id[1] << 16) + (id[0] << 24);
        }
    }

	public class BulletFile : bFile
	{
        protected byte[] _dnaCopy;

		public BulletFile()
			: base(btBulletFile_new())
		{
		}
        
		public BulletFile(string fileName)
            : base(fileName, "BULLET ")
		{
		}
        /*
		public BulletFile(char memoryBuffer, int len)
			: base(btBulletFile_new3(memoryBuffer._native, len))
		{
		}

		public void AddStruct(char structType, IntPtr data, int len, IntPtr oldPtr, int code)
		{
			btBulletFile_addStruct(_native, structType._native, data, len, oldPtr, code);
		}
        */
        public override void Parse(FileVerboseMode verboseMode)
        {
            if (IntPtr.Size == 8)
            {
                _dnaCopy = new byte[BulletDna.BulletDnaStr64.Length];
                Buffer.BlockCopy(BulletDna.BulletDnaStr64, 0, _dnaCopy, 0, _dnaCopy.Length);
            }
            else
            {
                _dnaCopy = new byte[BulletDna.BulletDnaStr.Length];
                Buffer.BlockCopy(BulletDna.BulletDnaStr, 0, _dnaCopy, 0, _dnaCopy.Length);
            }
            ParseInternal(verboseMode, _dnaCopy);

            //the parsing will convert to cpu endian
            _flags &= ~FileFlags.EndianSwap;
            
            _fileBuffer[8] = BitConverter.IsLittleEndian ? (byte)'v' : (byte)'V';
        }

		public override void ParseData()
		{
            //Console.WriteLine("Building datablocks");
            //Console.WriteLine("Chunk size = {0}", CHUNK_HEADER_LEN);
            //Console.WriteLine("File chunk size = {0}", ChunkUtils.GetOffset(_flags));

            bool brokenDna = (_flags & FileFlags.BrokenDna) == FileFlags.BrokenDna;
            bool swap = (_flags & FileFlags.EndianSwap) == FileFlags.EndianSwap;

            MemoryStream memory = new MemoryStream(_fileBuffer);
            BinaryReader reader = new BinaryReader(memory);

            _dataStart = 12;
            long dataPtr = _dataStart;
            memory.Position = dataPtr;

            ChunkInd dataChunk;
            int seek = GetNextBlock(out dataChunk, reader, _flags);

            if (swap)
            {
                //swapLen(dataPtr);
            }

            while (dataChunk.Code != DnaID.Dna)
            {
                if (!brokenDna || dataChunk.Code != DnaID.QuantizedBvh)
                {
                    // One behind
                    if (dataChunk.Code == DnaID.Sdna)
                    {
                        break;
                    }

                    // same as (BHEAD+DATA dependency)
			        long dataPtrHead = dataPtr + ChunkUtils.GetOffset(_flags);
                    if (dataChunk.DnaNR >= 0)
                    {

                    }
                }
                else
                {
                    //Console.WriteLine("skipping B3_QUANTIZED_BVH_CODE due to broken DNA");
                }

                dataPtr += seek;
                memory.Position = dataPtr;

                seek = GetNextBlock(out dataChunk, reader, _flags);
                if (swap)
                {
                    //swapLen(dataPtr);
                }

                if (seek < 0)
                    break;
            }

            reader.Dispose();
            memory.Dispose();
		}
        /*
		public void Bvhs
		{
			get { return btBulletFile_getBvhs(_native); }
		}

		public void CollisionObjects
		{
			get { return btBulletFile_getCollisionObjects(_native); }
		}

		public void CollisionShapes
		{
			get { return btBulletFile_getCollisionShapes(_native); }
		}

		public void Constraints
		{
			get { return btBulletFile_getConstraints(_native); }
		}

		public void DataBlocks
		{
			get { return btBulletFile_getDataBlocks(_native); }
		}

		public void DynamicsWorldInfo
		{
			get { return btBulletFile_getDynamicsWorldInfo(_native); }
		}

		public void RigidBodies
		{
			get { return btBulletFile_getRigidBodies(_native); }
		}

		public void SoftBodies
		{
			get { return btBulletFile_getSoftBodies(_native); }
		}

		public void TriangleInfoMaps
		{
			get { return btBulletFile_getTriangleInfoMaps(_native); }
		}
        */
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBulletFile_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBulletFile_new2(IntPtr fileName);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btBulletFile_new3(IntPtr memoryBuffer, int len);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBulletFile_addStruct(IntPtr obj, IntPtr structType, IntPtr data, int len, IntPtr oldPtr, int code);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBulletFile_getBvhs(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBulletFile_getCollisionObjects(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBulletFile_getCollisionShapes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBulletFile_getConstraints(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBulletFile_getDataBlocks(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBulletFile_getDynamicsWorldInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBulletFile_getRigidBodies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBulletFile_getSoftBodies(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btBulletFile_getTriangleInfoMaps(IntPtr obj);
	}
}
