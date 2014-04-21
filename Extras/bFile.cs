using System;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;
using System.Text;

namespace BulletSharp
{
    [Flags]
    public enum FileFlags
    {
        None = 0x0,
        OK = 0x1,
        VoidIs8 = 0x2,
        EndianSwap = 0x4,
        File64 = 0x8,
        BitsVaries = 0x10,
        VersionVaries = 0x20,
        DoublePrecision = 0x40,
        BrokenDna = 0x80
    }

    [Flags]
    public enum FileVerboseMode
    {
        None = 0x0,
        ExportXml = 0x1,
        DumpDnaTypeDefinitions = 0x2,
        DumpChunks = 0x4,
        DumpFileInfo = 0x8
    }

	public abstract class bFile
	{
		internal IntPtr _native;

        const int SizeOfBlenderHeader = 12;

        protected long _dataStart;
        protected byte[] _fileBuffer;
        protected Dna _fileDna;
        protected FileFlags _flags;
        protected string _headerString;
        protected Dna _memoryDna;
        protected int _version;

        public bFile(string filename, string headerString)
        {
            _headerString = headerString;
            _fileBuffer = File.ReadAllBytes(filename);

            ParseHeader();
        }

		internal bFile(IntPtr native)
		{
			_native = native;
		}
        /*
		public void AddDataBlock(char dataBlock)
		{
			bFile_addDataBlock(_native, dataBlock._native);
		}
        
		public void DumpChunks(bDNA dna)
		{
			bFile_dumpChunks(_native, dna._native);
		}
        */
		public IntPtr FindLibPointer(IntPtr ptr)
		{
			return bFile_findLibPointer(_native, ptr);
		}

        // buffer offset util
		protected int GetNextBlock(out ChunkInd dataChunk, BinaryReader reader, FileFlags flags)
        {
            bool swap = false;
            bool varies = false;

            if ((flags & FileFlags.EndianSwap) == FileFlags.EndianSwap)
                swap = true;
            if ((flags & FileFlags.BitsVaries) == FileFlags.BitsVaries)
                varies = true;

            dataChunk = new ChunkInd();

            if (IntPtr.Size == 8)
            {
                if (varies)
                {
                    ChunkPtr4 c = new ChunkPtr4(reader);
                    dataChunk = new ChunkInd(ref c);
                }
                else
                {
                    ChunkPtr8 c = new ChunkPtr8(reader);
                    dataChunk = new ChunkInd(ref c);
                }
            }

            if (dataChunk.Length < 0)
                return -1;

            return dataChunk.Length + ChunkUtils.GetOffset(flags);
        }

		public bool Ok()
		{
			return bFile_ok(_native);
		}

		public abstract void Parse(FileVerboseMode verboseMode);
        public abstract void ParseData();

        protected void ParseHeader()
        {
            string header = Encoding.UTF8.GetString(_fileBuffer, 0, SizeOfBlenderHeader);
            if (!header.Substring(0, 6).Equals(_headerString.Substring(0, 6)))
            {
                return;
            }

            if (header[6] == 'd')
            {
                _flags |= FileFlags.DoublePrecision;
            }

            int.TryParse(header.Substring(9), out _version);

            // swap ptr sizes...
	        if (header[7] == '-')
	        {
		        _flags |= FileFlags.File64;
		        if (IntPtr.Size != 8)
			        _flags |= FileFlags.BitsVaries;
	        }
            else if (IntPtr.Size == 8)
            {
                _flags |= FileFlags.BitsVaries;
            }

            // swap endian...
            if (header[8] == 'V')
            {
                if (BitConverter.IsLittleEndian)
                    _flags |= FileFlags.EndianSwap;
            }
            else
            {
                if (!BitConverter.IsLittleEndian)
                    _flags |= FileFlags.EndianSwap;
            }

            _flags |= FileFlags.OK;
        }

        protected void ParseInternal(FileVerboseMode verboseMode, byte[] memDna)
        {
            if ((_flags & FileFlags.OK) != FileFlags.OK)
            {
                return;
            }

            ChunkInd dna = new ChunkInd();
            dna.OldPtr = 0;

            MemoryStream memory = new MemoryStream(_fileBuffer);
            BinaryReader reader = new BinaryReader(memory);

            int i = 0;
            while (memory.Position < memory.Length)
            {
                // looking for the data's starting position
                // and the start of SDNA decls

                byte[] code = reader.ReadBytes(4);
                string codes = ASCIIEncoding.ASCII.GetString(code);

                if (_dataStart == 0 && codes.Equals("REND"))
                {
                    _dataStart = memory.Position;
                }

                if (codes.Equals("DNA1"))
                {
                    // read the DNA1 block and extract SDNA
                    reader.BaseStream.Position = i;
                    if (GetNextBlock(out dna, reader, _flags) > 0)
                    {
                        string sdnaname = ASCIIEncoding.ASCII.GetString(reader.ReadBytes(8));
                        if (sdnaname.Equals("SDNANAME"))
                        {
                            dna.OldPtr = i + ChunkUtils.GetOffset(_flags);
                        }
                        else
                        {
                            dna.OldPtr = 0;
                        }
                    }
                    else
                    {
                        dna.OldPtr = 0;
                    }
                }
                else if (codes.Equals("SDNA"))
                {
                    // Some Bullet files are missing the DNA1 block
                    // In Blender it's DNA1 + ChunkUtils::getOffset() + SDNA + NAME
                    // In Bullet tests its SDNA + NAME

                    dna.OldPtr = i;
                    dna.Length = (int)memory.Length - i;

                    // Also no REND block, so exit now.
                    if (_version == 276)
                    {
                        break;
                    }
                }

                if (_dataStart != 0 && dna.OldPtr != 0)
                {
                    break;
                }

                i++;
                memory.Position = i;
            }

            if (dna.OldPtr == 0 || dna.Length == 0)
            {
                //Console.WriteLine("Failed to find DNA1+SDNA pair");
                _flags &= ~FileFlags.OK;
                return;
            }

            _fileDna = new Dna();

            // _fileDna.Init will convert part of DNA file endianness to current CPU endianness if necessary
            memory.Position = dna.OldPtr;
            _fileDna.Init(reader, (_flags & FileFlags.EndianSwap) == FileFlags.EndianSwap);

            if (_version == 276)
            {
                for (i = 0; i < _fileDna.NumNames; i++)
                {
                    if (_fileDna.GetName(i).Equals("int"))
                    {
                        _flags |= FileFlags.BrokenDna;
                    }
                }
                if ((_flags & FileFlags.BrokenDna) == FileFlags.BrokenDna)
                {
                    //Console.WriteLine("warning: fixing some broken DNA version");
                }
            }

            //if ((verboseMode & FileVerboseMode.DumpDnaTypeDefinitions) == FileVerboseMode.DumpDnaTypeDefinitions)
            //    _fileDna.DumpTypeDefinitions();

            _memoryDna = new Dna();
            MemoryStream memory2 = new MemoryStream(memDna);
            BinaryReader reader2 = new BinaryReader(memory2);
            _memoryDna.Init(reader2, !BitConverter.IsLittleEndian);
            reader2.Dispose();
            memory2.Dispose();

            if (_memoryDna.NumNames != _fileDna.NumNames)
            {
                _flags |= FileFlags.VersionVaries;
                //Console.WriteLine ("Warning, file DNA is different than built in, performance is reduced. Best to re-export file with a matching version/platform");
            }

            if (_memoryDna.LessThan(_fileDna))
            {
                //Console.WriteLine ("Warning, file DNA is newer than built in.");
            }

            _fileDna.InitCmpFlags(_memoryDna);

            ParseData();

            ResolvePointers(verboseMode);

            UpdateOldPointers();

            reader.Dispose();
            memory.Dispose();
        }

		public void PreSwap()
		{
			bFile_preSwap(_native);
		}

        protected byte[] ReadStruct(byte[] head, ChunkInd dataChunk)
        {
            bool ignoreEndianFlag = false;

            if ((_flags & FileFlags.EndianSwap) == FileFlags.EndianSwap)
            {
                //swap(head, dataChunk, ignoreEndianFlag);
            }

            if (!_fileDna.FlagEqual(dataChunk.DnaNR))
            {
            }

            return null;
        }

		public void ResolvePointers(FileVerboseMode verboseMode)
		{
            //Console.WriteLine("resolvePointersStructMismatch");
            
            throw new NotImplementedException();
		}

		public void UpdateOldPointers()
		{
			bFile_updateOldPointers(_native);
		}
        /*
		public int Write(char fileName, bool fixupPointers)
		{
			return bFile_write(_native, fileName._native, fixupPointers);
		}

		public int Write(char fileName)
		{
			return bFile_write2(_native, fileName._native);
		}

		public void WriteChunks(FILE fp, bool fixupPointers)
		{
			bFile_writeChunks(_native, fp._native, fixupPointers);
		}

		public void WriteDNA(FILE fp)
		{
			bFile_writeDNA(_native, fp._native);
		}

		public void WriteFile(char fileName)
		{
			bFile_writeFile(_native, fileName._native);
		}

		public bDNA FileDNA
		{
			get { return bFile_getFileDNA(_native); }
		}
        */
        public FileFlags Flags
		{
            get { return _flags; }
		}
        /*
		public bPtrMap LibPointers
		{
			get { return bFile_getLibPointers(_native); }
		}
        */
		public int Version
		{
			get { return bFile_getVersion(_native); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				bFile_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~bFile()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void bFile_addDataBlock(IntPtr obj, IntPtr dataBlock);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void bFile_dumpChunks(IntPtr obj, IntPtr dna);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr bFile_findLibPointer(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr bFile_getFileDNA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr bFile_getLibPointers(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int bFile_getVersion(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool bFile_ok(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void bFile_preSwap(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void bFile_updateOldPointers(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int bFile_write(IntPtr obj, IntPtr fileName, bool fixupPointers);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int bFile_write2(IntPtr obj, IntPtr fileName);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void bFile_writeChunks(IntPtr obj, IntPtr fp, bool fixupPointers);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void bFile_writeDNA(IntPtr obj, IntPtr fp);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void bFile_writeFile(IntPtr obj, IntPtr fileName);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void bFile_delete(IntPtr obj);
	}
}
