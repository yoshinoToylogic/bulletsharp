using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
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
        const int SizeOfBlenderHeader = 12;

        protected List<ChunkInd> _chunks = new List<ChunkInd>();
        protected long _dataStart;
        protected byte[] _fileBuffer;
        protected Dna _fileDna;
        protected FileFlags _flags;
        protected string _headerString;
        protected Dictionary<long, byte[]> _libPointers = new Dictionary<long,byte[]>();
        protected Dna _memoryDna;
        protected int _version;

        public bFile(string filename, string headerString)
        {
            _headerString = headerString;
            try
            {
                _fileBuffer = File.ReadAllBytes(filename);
                ParseHeader();
            }
            catch
            {

            }
        }

        public bFile(byte[] memoryBuffer, int len, string headerString)
        {
            _headerString = headerString;
            _fileBuffer = memoryBuffer;

            ParseHeader();
        }

        public abstract void AddDataBlock(byte[] dataBlock);

        /*
		public void DumpChunks(bDNA dna)
		{
			bFile_dumpChunks(_native, dna._native);
		}
        */
		public byte[] FindLibPointer(long ptr)
		{
            byte[] data;
            if (LibPointers.TryGetValue(ptr, out data))
            {
                return data;
            }
            return null;
		}

        protected long GetFileElement(Dna.StructDecl firstStruct, Dna.ElementDecl lookupElement, long data, out Dna.ElementDecl found)
        {
            foreach (Dna.ElementDecl element in firstStruct.Elements)
            {
                if (element.Name.Equals(lookupElement.Name))
                {
                    if (element.Type.Name.Equals(lookupElement.Type.Name))
                    {
                        found = element;
                        return data;
                    }
                    found = null;
                    return 0;
                }
                data += _fileDna.GetElementSize(element);
            }
            found = null;
            return 0;
        }

        protected void GetMatchingFileDna(Dna.StructDecl dna, Dna.ElementDecl lookupElement, BinaryWriter strcData, long data, bool fixupPointers)
        {
            // find the matching memory dna data
            // to the file being loaded. Fill the
            // memory with the file data...

            MemoryStream dataStream = new MemoryStream(_fileBuffer, false);
            dataStream.Position = data;
            BinaryReader dataReader = new BinaryReader(dataStream);

            foreach (Dna.ElementDecl element in dna.Elements)
            {
                int eleLen = _fileDna.GetElementSize(element);

                if ((_flags & FileFlags.BrokenDna) != 0)
                {
                    if (element.Type.Name.Equals("short") && element.Name.Name.Equals("int"))
                    {
                        eleLen = 0;
                    }
                }

                if (lookupElement.Name.Equals(element.Name))
                {
                    int arrayLen = element.Name.ArraySizeNew;

                    if (element.Name.Name[0] == '*')
                    {
                        SafeSwapPtr(strcData, dataReader);

                        if (fixupPointers)
                        {
                            if (arrayLen > 1)
                            {
                                throw new NotImplementedException();
                            }
                            else
                            {
                                if (element.Name.Name[1] == '*')
                                {
                                    throw new NotImplementedException();
                                }
                                else
                                {
                                    //_pointerFixupArray.push_back(strcData);
                                    //throw new NotImplementedException();
                                }
                            }
                        }
                        else
                        {
                            //Console.WriteLine("skipped {0} {1} : {2:X}", element.Type.Name, element.Name.Name, strcData.BaseStream.Position);
                        }
                    }
                    else if (element.Type.Name.Equals(lookupElement.Type.Name))
                    {
                        byte[] mem = new byte[eleLen];
                        dataReader.Read(mem, 0, eleLen);
                        strcData.Write(mem);
                    }
                    else
                    {
                        throw new NotImplementedException();
                        //GetElement(arrayLen, lookupType, type, data, strcData);
                    }

                    break;
                }
                dataStream.Position += eleLen;
            }

            dataReader.Dispose();
            dataStream.Dispose();
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

            if (swap)
            {
                throw new NotImplementedException();
            }

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
            else
            {
                if (varies)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    ChunkPtr4 c = new ChunkPtr4(reader);
                    dataChunk = new ChunkInd(ref c);
                }
            }

            if (dataChunk.Length < 0)
                return -1;

            return dataChunk.Length + ChunkUtils.GetOffset(flags);
        }

		public bool OK
		{
            get { return (_flags & FileFlags.OK) != 0; }
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

            MemoryStream memory = new MemoryStream(_fileBuffer, false);
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
            _fileDna.Init(reader, (_flags & FileFlags.EndianSwap) != 0);

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
            MemoryStream memory2 = new MemoryStream(memDna, false);
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

        protected void ParseStruct(BinaryWriter strc, BinaryReader dt, Dna.StructDecl fileStruct, Dna.StructDecl memoryStruct, bool fixupPointers)
        {
            if (fileStruct == null) return;
            if (memoryStruct == null) return;

            long dtPtr = dt.BaseStream.Position;

            foreach (Dna.ElementDecl element in memoryStruct.Elements)
            {
                if (element.Type.Struct != null && element.Name.Name[0] != '*')
                {
                    Dna.ElementDecl elementOld;
                    long cpo = GetFileElement(fileStruct, element, dtPtr, out elementOld);
                    if (cpo != 0)
                    {
                        dt.BaseStream.Position = cpo;
                        int arrayLen = elementOld.Name.ArraySizeNew;
                        Dna.StructDecl oldStruct = _fileDna.GetReverseType(element.Type.Name);
                        if (arrayLen == 1)
                        {
                            ParseStruct(strc, dt, oldStruct, element.Type.Struct, fixupPointers);
                        }
                        else
                        {
                            int fpLen = _fileDna.GetElementSize(element) / arrayLen;
                            for (int i = 0; i < arrayLen; i++)
                            {
                                ParseStruct(strc, dt, oldStruct, element.Type.Struct, fixupPointers);
                                dt.BaseStream.Position += fpLen;
                            }
                        }
                    }
                }
                else
                {
                    GetMatchingFileDna(fileStruct, element, strc, dtPtr, fixupPointers);
                }
            }
        }

		public void PreSwap()
		{
            throw new NotImplementedException();
		}

        protected byte[] ReadStruct(BinaryReader head, ChunkInd dataChunk)
        {
            bool ignoreEndianFlag = false;

            if ((_flags & FileFlags.EndianSwap) == FileFlags.EndianSwap)
            {
                //swap(head, dataChunk, ignoreEndianFlag);
            }

            if (!_fileDna.FlagEqual(dataChunk.DnaNR))
            {
                // Ouch! need to rebuild the struct
                Dna.StructDecl oldStruct = _fileDna.GetStruct(dataChunk.DnaNR);

                if ((_flags & FileFlags.BrokenDna) != 0)
                {
                    throw new NotImplementedException();
                }

                // Don't try to convert Link block data, just memcpy it. Other data can be converted.
                if (oldStruct.Type.Name.Equals("Link"))
                {
                    //Console.WriteLine("Link found");
                }
                else
                {
                    Dna.StructDecl reverseOld = _memoryDna.GetReverseType(oldStruct.Type.Name);
                    if (reverseOld != null)
                    {
                        byte[] structAlloc = new byte[dataChunk.NR * reverseOld.Type.Length];
                        AddDataBlock(structAlloc);
                        using (MemoryStream stream = new MemoryStream(structAlloc))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                for (int block = 0; block < dataChunk.NR; block++)
                                {
                                    ParseStruct(writer, head, oldStruct, reverseOld, true);
                                    //_libPointers.Add(old, cur);
                                }
                            }
                        }
                        return structAlloc;
                    }
                }

                //throw new NotImplementedException();
            }
            else
            {
#if DEBUG_EQUAL_STRUCTS
#endif
            }

            byte[] dataAlloc = new byte[dataChunk.Length];
            head.Read(dataAlloc, 0, dataAlloc.Length);
            return dataAlloc;
        }

		public void ResolvePointers(FileVerboseMode verboseMode)
		{
            Dna fileDna = (_fileDna != null) ? _fileDna : _memoryDna;

            if (true) // && ((_flags & FileFlags.BitsVaries | FileFlags.VersionVaries) != 0))
            {
                ResolvePointersMismatch();
            }

            if ((verboseMode & FileVerboseMode.ExportXml) != 0)
            {
                Console.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                //Console.WriteLine("<bullet_physics version={0} itemcount = {1}>", b3GetVersion(), _chunks.Count);
                throw new NotImplementedException();
            }

            foreach (ChunkInd dataChunk in _chunks)
            {
                if (_fileDna == null || fileDna.FlagEqual(dataChunk.DnaNR))
                {

                    Dna.StructDecl oldStruct = fileDna.GetStruct(dataChunk.DnaNR);

                    if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                    {
                        Console.WriteLine(" <{0} pointer={1}>", oldStruct.Type.Name, dataChunk.OldPtr);
                    }

                    ResolvePointersChunk(dataChunk, verboseMode);

                    if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                    {
                        Console.WriteLine(" </{0}>", oldStruct.Type.Name);
                    }
                }
                else
                {
                    Console.WriteLine("skipping struct");
                }
            }

            if ((verboseMode & FileVerboseMode.ExportXml) != 0)
            {
                Console.WriteLine("</bullet_physics>");
            }
		}

        protected void ResolvePointersChunk(ChunkInd dataChunk, FileVerboseMode verboseMode)
        {
            Dna fileDna = (_fileDna != null) ? _fileDna : _memoryDna;

            Dna.StructDecl oldStruct = fileDna.GetStruct(dataChunk.DnaNR);
            int oldLen = oldStruct.Type.Length;

            byte[] cur = FindLibPointer(dataChunk.OldPtr);
            using (MemoryStream stream = new MemoryStream(cur))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    for (int block = 0; block < dataChunk.NR; block++)
                    {
                        ResolvePointersStructRecursive(reader, dataChunk.DnaNR, verboseMode, 1);
                        reader.BaseStream.Position += oldLen;
                    }
                }
            }
        }

        protected void ResolvePointersMismatch()
        {
            //throw new NotImplementedException();
        }

        protected int ResolvePointersStructRecursive(BinaryReader reader, int dnaNr, FileVerboseMode verboseMode, int recursion)
        {
            Dna fileDna = (_fileDna != null) ? _fileDna : _memoryDna;
            Dna.StructDecl oldStruct = fileDna.GetStruct(dnaNr);

            int totalSize = 0;

            foreach (Dna.ElementDecl element in oldStruct.Elements)
            {
                int arrayLen = element.Name.ArraySizeNew;
                if (element.Name.Name[0] == '*')
                {
                    if (arrayLen > 1)
                    {
                        //throw new NotImplementedException();
                    }
                    else
                    {
                        //throw new NotImplementedException();
                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
                else
                {
                    if (element.Type.Struct != null)
                    {
                        for (int i = 0; i < arrayLen; i++)
                        {
                            //throw new NotImplementedException();
                            //byteOffset += ResolvePointersStructRecursive(elemPtr + byteOffset, revType, verboseMode, recursion + 1);
                        }
                    }
                    else
                    {
                        // export a simple type
                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
                int size = fileDna.GetElementSize(element);
                totalSize += size;
                //elemPtr += size;
            }

            return totalSize;
        }

        protected void SafeSwapPtr(BinaryWriter strcData, BinaryReader data)
        {
            int ptrFile = _fileDna.PointerSize;
            int ptrMem = _memoryDna.PointerSize;

            if (ptrFile == ptrMem)
            {
                byte[] mem = new byte[ptrMem];
                data.Read(mem, 0, ptrMem);
                strcData.Write(mem);
            }
            else if (ptrMem == 4 && ptrFile == 8)
            {
                throw new NotImplementedException();
            }
            else if (ptrMem == 8 && ptrFile == 4)
            {
                int uniqueId1 = data.ReadInt32();
                int uniqueId2 = data.ReadInt32();
                data.BaseStream.Position -= 4;
                if (uniqueId1 == uniqueId2)
                {
                    strcData.Write(uniqueId1);
                    strcData.Write(0);
                }
                else
                {
                    strcData.Write((long)uniqueId1);
                }
            }
            else
            {
                Console.WriteLine("Invalid pointer len {0} {1}", ptrFile, ptrMem);
            }
        }

		public void UpdateOldPointers()
		{
            for (int i = 0; i < _chunks.Count; i++)
            {
                //_chunks[i].OldPtr
                byte[] data = FindLibPointer(_chunks[i].OldPtr);
                data.ToString();
            }
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
        
		public Dictionary<long, byte[]> LibPointers
		{
            get { return _libPointers; }
		}
        
		public int Version
		{
            get { return _version; }
		}
	}
}
