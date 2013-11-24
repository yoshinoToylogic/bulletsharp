using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BulletSharpGen
{
    [Flags]
    enum WriteTo
    {
        None = 0,
        Header = 1,
        Source = 2,
        CS = 4,
        DllImport = 8,
        All = Header | Source | CS
    }

    class WrapperWriter
    {
        protected Dictionary<string, HeaderDefinition> headerDefinitions = new Dictionary<string, HeaderDefinition>();
        protected string NamespaceName { get; private set; }

        protected StreamWriter headerWriter, sourceWriter, csWriter;
        protected StringBuilder dllImport = new StringBuilder();
        protected bool hasHeaderWhiteSpace;
        protected bool hasSourceWhiteSpace;
        protected bool hasCSWhiteSpace;

        public WrapperWriter(Dictionary<string, HeaderDefinition> headerDefinitions, string namespaceName)
        {
            this.headerDefinitions = headerDefinitions;
            this.NamespaceName = namespaceName;
        }

        public void Write(string s, WriteTo to = WriteTo.All)
        {
            if ((to & WriteTo.Header) == WriteTo.Header)
            {
                headerWriter.Write(s);
            }
            if ((to & WriteTo.Source) == WriteTo.Source)
            {
                sourceWriter.Write(s);
            }
            if ((to & WriteTo.CS) == WriteTo.CS)
            {
                csWriter.Write(s);
            }
            if ((to & WriteTo.DllImport) == WriteTo.DllImport)
            {
                dllImport.Append(s);
            }
        }

        public void Write(char c, WriteTo to = WriteTo.All)
        {
            Write(c.ToString(), to);
        }

        public void WriteLine(string s, WriteTo to = WriteTo.All)
        {
            Write(s, to);
            WriteLine(to);
        }

        public void WriteLine(char c, WriteTo to = WriteTo.All)
        {
            Write(c, to);
            WriteLine(to);
        }

        public void WriteLine(WriteTo to = WriteTo.All)
        {
            Write("\r\n", to);
        }

        public void OutputTabs(int n, WriteTo to = WriteTo.Header)
        {
            for (int i = 0; i < n; i++)
            {
                Write('\t', to);
            }
        }

        public void EnsureWhiteSpace(WriteTo to = WriteTo.Header)
        {
            if ((to & WriteTo.Header) == WriteTo.Header)
            {
                if (!hasHeaderWhiteSpace)
                {
                    headerWriter.WriteLine();
                    hasHeaderWhiteSpace = true;
                }
            }
            if ((to & WriteTo.Source) == WriteTo.Source)
            {
                if (!hasSourceWhiteSpace)
                {
                    sourceWriter.WriteLine();
                    hasSourceWhiteSpace = true;
                }
            }
            if ((to & WriteTo.CS) == WriteTo.CS)
            {
                if (!hasCSWhiteSpace)
                {
                    csWriter.WriteLine();
                    hasCSWhiteSpace = true;
                }
            }
        }
    }
}
