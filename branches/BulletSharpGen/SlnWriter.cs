using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BulletSharpGen
{
    class SlnWriter
    {
        Dictionary<string, HeaderDefinition> headerDefinitions = new Dictionary<string, HeaderDefinition>();
        StreamWriter solutionWriter;
        string namespaceName;

        public SlnWriter(Dictionary<string, HeaderDefinition> headerDefinitions, string namespaceName)
        {
            this.headerDefinitions = headerDefinitions;
            this.namespaceName = namespaceName;
        }

        public void Output()
        {
            string outDirectory = "src";

            Directory.CreateDirectory(outDirectory);
            FileStream solutionFile = new FileStream(outDirectory + "\\" + namespaceName + ".sln", FileMode.Create, FileAccess.Write);
            solutionWriter = new StreamWriter(solutionFile, Encoding.UTF8);

            List<string> confs = new List<string>();
            confs.Add("Axiom");
            confs.Add("Generic");
            confs.Add("Mogre");
            confs.Add("OpenTK");
            confs.Add("SharpDX");
            confs.Add("SlimDX");
            confs.Add("SlimMath");
            confs.Add("Windows API Code Pack");
            confs.Add("XNA 3.1");
            confs.Add("XNA 4.0");

            Guid projectGuid = new Guid("5A0DEF7E-B7E3-45E9-A511-0F03CECFF8C0");
            string projectGuidString = projectGuid.ToString().ToUpper();

            solutionWriter.WriteLine();
            solutionWriter.WriteLine("Microsoft Visual Studio Solution File, Format Version 11.00");
            solutionWriter.WriteLine("# Visual Studio 2010");

            Guid vcppProjectType = new Guid("8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942");
            string vcppProjectTypeString = vcppProjectType.ToString().ToUpper();
            solutionWriter.Write("Project(\"{");
            solutionWriter.Write(vcppProjectTypeString);
            solutionWriter.Write("}\") = \"");
            solutionWriter.Write(namespaceName);
            solutionWriter.Write("\", \"");
            solutionWriter.Write(namespaceName);
            solutionWriter.Write(".vcxproj\", \"{");
            solutionWriter.Write(projectGuidString);
            solutionWriter.WriteLine("}\"");
            solutionWriter.WriteLine("EndProject");

            solutionWriter.WriteLine("Global");

            solutionWriter.WriteLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
            foreach (string conf in confs)
            {
                solutionWriter.Write("\t\tDebug ");
                solutionWriter.Write(conf);
                solutionWriter.Write("|Win32 = Debug ");
                solutionWriter.Write(conf);
                solutionWriter.WriteLine("|Win32");
            }
            foreach (string conf in confs)
            {
                solutionWriter.Write("\t\tRelease ");
                solutionWriter.Write(conf);
                solutionWriter.Write("|Win32 = Release ");
                solutionWriter.Write(conf);
                solutionWriter.WriteLine("|Win32");
            }
            solutionWriter.WriteLine("\tEndGlobalSection");

            solutionWriter.WriteLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
            foreach (string conf in confs)
            {
                solutionWriter.Write("\t\t{");
                solutionWriter.Write(projectGuidString);
                solutionWriter.Write("}.Debug ");
                solutionWriter.Write(conf);
                solutionWriter.Write("|Win32.ActiveCfg = Debug ");
                solutionWriter.Write(conf);
                solutionWriter.WriteLine("|Win32");

                solutionWriter.Write("\t\t{");
                solutionWriter.Write(projectGuidString);
                solutionWriter.Write("}.Debug ");
                solutionWriter.Write(conf);
                solutionWriter.Write("|Win32.Build.0 = Debug ");
                solutionWriter.Write(conf);
                solutionWriter.WriteLine("|Win32");
            }
            foreach (string conf in confs)
            {
                solutionWriter.Write("\t\t{");
                solutionWriter.Write(projectGuidString);
                solutionWriter.Write("}.Release ");
                solutionWriter.Write(conf);
                solutionWriter.Write("|Win32.ActiveCfg = Release ");
                solutionWriter.Write(conf);
                solutionWriter.WriteLine("|Win32");

                solutionWriter.Write("\t\t{");
                solutionWriter.Write(projectGuidString);
                solutionWriter.Write("}.Release ");
                solutionWriter.Write(conf);
                solutionWriter.Write("|Win32.Build.0 = Release ");
                solutionWriter.Write(conf);
                solutionWriter.WriteLine("|Win32");
            }
            solutionWriter.WriteLine("\tEndGlobalSection");

            solutionWriter.WriteLine("\tGlobalSection(SolutionProperties) = preSolution");
            solutionWriter.WriteLine("\t\tHideSolutionNode = FALSE");
            solutionWriter.WriteLine("\tEndGlobalSection");
            solutionWriter.WriteLine("EndGlobal");

            foreach (HeaderDefinition header in headerDefinitions.Values)
            {
                if (header.Classes.Count == 0)
                {
                    continue;
                }
            }

            solutionWriter.Dispose();
            solutionFile.Dispose();
        }
    }
}
