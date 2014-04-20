using System.IO;
using System.Collections.Generic;
using System.Text;

namespace BulletSharp
{
    public class Dna
    {
        private class NameInfo
        {
            public string Name { get; set; }
            public bool IsPointer { get; set; }
            public int Dim0 { get; set; }
            public int Dim1 { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private List<short> _lengths = new List<short>();
        private List<NameInfo> _names = new List<NameInfo>();
        private List<long> _structPtrs = new List<long>();
        private List<string> _types = new List<string>();

        private int _ptrLen;

        public Dna()
        {
        }

        public object GetName(int i)
        {
            return _names[i];
        }

        public void Init(BinaryReader reader, bool swap)
        {
            Stream stream = reader.BaseStream;

            // SDNA
            byte[] code = reader.ReadBytes(8);
            string codes = ASCIIEncoding.ASCII.GetString(code);

            // NAME
            if (!codes.Equals("SDNANAME"))
            {
                throw new InvalidDataException();
            }
            int dataLen = reader.ReadInt32();
            for (int i = 0; i < dataLen; i++)
            {
                NameInfo nameInfo = new NameInfo();

                List<byte> name = new List<byte>();
                byte ch = reader.ReadByte();
                while (ch != 0)
                {
                    name.Add(ch);
                    ch = reader.ReadByte();
                }
                nameInfo.Name = ASCIIEncoding.ASCII.GetString(name.ToArray());

                if (nameInfo.Name[0] == '*' || nameInfo.Name[1] == '*')
                {
                    nameInfo.IsPointer = true;
                }

                _names.Add(nameInfo);
            }
            stream.Position = (stream.Position + 3) & ~3;

            // TYPE
            code = reader.ReadBytes(4);
            codes = ASCIIEncoding.ASCII.GetString(code);
            if (!codes.Equals("TYPE"))
            {
                throw new InvalidDataException();
            }
            dataLen = reader.ReadInt32();
            for (int i = 0; i < dataLen; i++)
            {
                List<byte> name = new List<byte>();
                byte ch = reader.ReadByte();
                while (ch != 0)
                {
                    name.Add(ch);
                    ch = reader.ReadByte();
                }
                string type = ASCIIEncoding.ASCII.GetString(name.ToArray());
                _types.Add(type);
            }
            stream.Position = (stream.Position + 3) & ~3;

            // TLEN
            code = reader.ReadBytes(4);
            codes = ASCIIEncoding.ASCII.GetString(code);
            if (!codes.Equals("TLEN"))
            {
                throw new InvalidDataException();
            }
            for (int i = 0; i < _types.Count; i++)
            {
                short length = reader.ReadInt16();
                _lengths.Add(length);
            }
            stream.Position = (stream.Position + 3) & ~3;

            // STRC
            code = reader.ReadBytes(4);
            codes = ASCIIEncoding.ASCII.GetString(code);
            if (!codes.Equals("STRC"))
            {
                throw new InvalidDataException();
            }
            dataLen = reader.ReadInt32();
            long shtPtr = stream.Position;
            for (int i = 0; i < dataLen; i++)
            {
                _structPtrs.Add(shtPtr);
                if (swap)
                {
                }
                else
                {
                    stream.Position = shtPtr + 2;
                    shtPtr += (4 * reader.ReadInt16()) + 4;
                }
            }

            // build reverse lookups
            for (int i = 0; i < _structPtrs.Count; i++)
            {
                stream.Position = _structPtrs[i];
                int strcPtr = reader.ReadInt16();
                if (_ptrLen == 0 && _types[strcPtr].Equals("ListBase"))
                {
                    _ptrLen = _lengths[strcPtr] / 2;
                }
                //_structReverse.insert(strcPtr, i);
                //_typeLookup.insert(b3HashString(mTypes[strc[0]]), i);
            }
        }

        public void InitCmpFlags(Dna memoryDna)
        {
            // compare the file to memory
            // this ptr should be the file data
        }

        public bool LessThan(Dna file)
        {
            return _names.Count < _names.Count;
        }

        public int NumNames { get { return _names.Count; } }
    }
}
