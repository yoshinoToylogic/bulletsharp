using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulletSharpGen
{
    enum TargetVS
    {
        VS2008,
        VS2010,
        VS2012,
        VS2013
    }

    class SlnWriter
    {
        IEnumerable<string> sourceFiles, headerFiles, resourceFiles;
        string namespaceName;
        
        StreamWriter solutionWriter, projectWriter;
        TargetVS targetVS;

        public string IncludeDirectories { get; set; }
        public string LibraryDirectoriesDebug { get; set; }
        public string LibraryDirectoriesRelease { get; set; }

        public SlnWriter(IEnumerable<string> sourceFiles, IEnumerable<string> headerFiles, IEnumerable<string> resourceFiles, string namespaceName)
        {
            this.sourceFiles = sourceFiles;
            this.headerFiles = headerFiles;
            this.resourceFiles = resourceFiles;
            this.namespaceName = namespaceName;
        }

        void OutputProjectConfiguration(ProjectConfiguration conf)
        {
            if (targetVS == TargetVS.VS2008)
            {
                projectWriter.WriteLine("\t\t<Configuration");
                projectWriter.Write("\t\t\tName=\"");
                projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                projectWriter.Write(conf.Name);
                projectWriter.WriteLine("|Win32\"");
                projectWriter.WriteLine("\t\t\tOutputDirectory=\"$(SolutionDir)$(ConfigurationName)\"");
                projectWriter.WriteLine("\t\t\tIntermediateDirectory=\"$(ConfigurationName)\"");
                projectWriter.WriteLine("\t\t\tConfigurationType=\"2\"");
                projectWriter.WriteLine("\t\t\tCharacterSet=\"1\"");
                projectWriter.WriteLine("\t\t\tManagedExtensions=\"1\"");
                if (!conf.IsDebug)
                {
                    projectWriter.WriteLine("\t\t\tWholeProgramOptimization=\"1\"");
                }
                projectWriter.WriteLine("\t\t\t>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCPreBuildEventTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCCustomBuildTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCXMLDataGeneratorTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCWebServiceProxyGeneratorTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCMIDLTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCCLCompilerTool\"");
                if (conf.IsDebug)
                {
                    projectWriter.WriteLine("\t\t\t\tOptimization=\"0\"");
                }
                else
                {
                    projectWriter.WriteLine("\t\t\t\tInlineFunctionExpansion=\"2\"");
                    //projectWriter.WriteLine("\t\t\t\tFavorSizeOrSpeed=\"2\"");
                    projectWriter.WriteLine("\t\t\t\tFavorSizeOrSpeed=\"1\"");
                }
                if (!string.IsNullOrEmpty(IncludeDirectories))
                {
                    projectWriter.Write("\t\t\t\tAdditionalIncludeDirectories=\"");
                    projectWriter.Write(IncludeDirectories);
                    projectWriter.WriteLine("\"");
                }
                projectWriter.Write("\t\t\t\tAdditionalUsingDirectories=\"");
                projectWriter.Write(conf.UsingDirectories);
                projectWriter.WriteLine("\"");
                projectWriter.Write("\t\t\t\tPreprocessorDefinitions=\"");
                projectWriter.Write(conf.Definitions);
                if (!string.IsNullOrEmpty(conf.Definitions) && !conf.Definitions.EndsWith(";"))
                {
                    projectWriter.Write(';');
                }
                projectWriter.Write("WIN32;");
                if (conf.IsDebug)
                {
                    projectWriter.Write("_DEBUG;");
                }
                else
                {
                    projectWriter.Write("NDEBUG;");
                }
                projectWriter.WriteLine("\"");
                if (conf.IsDebug)
                {
                    projectWriter.WriteLine("\t\t\t\tRuntimeLibrary=\"3\"");
                }
                else
                {
                    projectWriter.WriteLine("\t\t\t\tRuntimeLibrary=\"2\"");
                }
                projectWriter.WriteLine("\t\t\t\tFloatingPointModel=\"0\"");
                //projectWriter.WriteLine("\t\t\t\tEnableEnhancedInstructionSet=\"0\"");
                projectWriter.WriteLine("\t\t\t\tUsePrecompiledHeader=\"2\"");
                if (conf.IsDebug)
                {
                    projectWriter.WriteLine("\t\t\t\tWarningLevel=\"3\"");
                    projectWriter.WriteLine("\t\t\t\tDebugInformationFormat=\"3\"");
                }
                else
                {
                    projectWriter.WriteLine("\t\t\t\tWarningLevel=\"1\"");
                }
                //projectWriter.WriteLine("\t\t\t\tDisableSpecificWarnings=\"4793\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCManagedResourceCompilerTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCResourceCompilerTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCPreLinkEventTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCLinkerTool\"");
                if (conf.IsDebug)
                {
                    projectWriter.WriteLine("\t\t\t\tAdditionalOptions=\"/NODEFAULTLIB:libcmtd /NODEFAULTLIB:msvcprtd\"");
                }
                else
                {
                    projectWriter.WriteLine("\t\t\t\tAdditionalOptions=\"/NODEFAULTLIB:libcmt /NODEFAULTLIB:msvcprt\"");
                }
                projectWriter.Write("\t\t\t\tAdditionalDependencies=\"");
                if (conf.IsDebug)
                {
                    projectWriter.WriteLine("LinearMath_Debug.lib BulletCollision_Debug.lib BulletDynamics_Debug.lib\"");
                }
                else
                {
                    projectWriter.WriteLine("LinearMath_MinSizeRel.lib BulletCollision_MinsizeRel.lib BulletDynamics_MinsizeRel.lib\"");
                }
                projectWriter.WriteLine("\t\t\t\tLinkIncremental=\"1\"");
                if (conf.IsDebug)
                {
                    if (!string.IsNullOrEmpty(LibraryDirectoriesDebug))
                    {
                        projectWriter.Write("\t\t\t\tAdditionalLibraryDirectories=\"");
                        projectWriter.Write(LibraryDirectoriesDebug);
                        projectWriter.WriteLine("\"");
                    }
                    projectWriter.WriteLine("\t\t\t\tGenerateDebugInformation=\"true\"");
                    projectWriter.WriteLine("\t\t\t\tAssemblyDebug=\"1\"");
                }
                else
                {
                    if (!string.IsNullOrEmpty(LibraryDirectoriesRelease))
                    {
                        projectWriter.Write("\t\t\t\tAdditionalLibraryDirectories=\"");
                        projectWriter.Write(LibraryDirectoriesRelease);
                        projectWriter.WriteLine("\"");
                    }
                    projectWriter.WriteLine("\t\t\t\tGenerateDebugInformation=\"false\"");
                }
                projectWriter.WriteLine("\t\t\t\tTargetMachine=\"1\"");
                //projectWriter.WriteLine("\t\t\t\tCLRUnmanagedCodeCheck=\"true\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCALinkTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCManifestTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCXDCMakeTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCBscMakeTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCFxCopTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCAppVerifierTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t\t<Tool");
                projectWriter.WriteLine("\t\t\t\tName=\"VCPostBuildEventTool\"");
                projectWriter.WriteLine("\t\t\t/>");
                projectWriter.WriteLine("\t\t</Configuration>");
            }
            else
            {
                projectWriter.Write("    <ProjectConfiguration Include=\"");
                projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                projectWriter.Write(conf.Name);
                projectWriter.WriteLine("|Win32\">");
                projectWriter.Write("      <Configuration>");
                projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                projectWriter.Write(conf.Name);
                projectWriter.WriteLine("</Configuration>");
                projectWriter.WriteLine("      <Platform>Win32</Platform>");
                projectWriter.WriteLine("    </ProjectConfiguration>");
            }
        }

        void OutputPropertyGroupConfiguration(ProjectConfiguration conf)
        {
            projectWriter.Write("  <PropertyGroup Condition=\"\'$(Configuration)|$(Platform)'=='");
            projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
            projectWriter.Write(conf.Name);
            projectWriter.WriteLine("|Win32'\" Label=\"Configuration\">");
            projectWriter.WriteLine("    <ConfigurationType>DynamicLibrary</ConfigurationType>");
            projectWriter.WriteLine("    <CharacterSet>Unicode</CharacterSet>");
            projectWriter.WriteLine("    <CLRSupport>true</CLRSupport>");
            if (conf.IsDebug)
            {
                if (targetVS == TargetVS.VS2012 || targetVS == TargetVS.VS2013)
                {
                    projectWriter.WriteLine("    <UseDebugLibraries>true</UseDebugLibraries>");
                }
            }
            else
            {
                projectWriter.WriteLine("    <WholeProgramOptimization>true</WholeProgramOptimization>");
            }
            if (targetVS == TargetVS.VS2012)
            {
                projectWriter.WriteLine("    <PlatformToolset>v110</PlatformToolset>");
            }
            else if (targetVS == TargetVS.VS2013)
            {
                projectWriter.WriteLine("    <PlatformToolset>v120</PlatformToolset>");
            }
            projectWriter.WriteLine("  </PropertyGroup>");
        }

        void OutputImportGroupPropertySheets(ProjectConfiguration conf)
        {
            projectWriter.Write("  <ImportGroup Condition=\"'$(Configuration)|$(Platform)'=='");
            projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
            projectWriter.Write(conf.Name);
            projectWriter.WriteLine("|Win32'\" Label=\"PropertySheets\">");
            projectWriter.WriteLine("    <Import Project=\"$(UserRootDir)\\Microsoft.Cpp.$(Platform).user.props\" Condition=\"exists('$(UserRootDir)\\Microsoft.Cpp.$(Platform).user.props')\" Label=\"LocalAppDataPlatform\" />");
            projectWriter.WriteLine("  </ImportGroup>");
        }

        void OutputPropertyGroupConfiguration2(ProjectConfiguration conf)
        {
            projectWriter.Write("    <OutDir Condition=\"'$(Configuration)|$(Platform)'=='");
            projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
            projectWriter.Write(conf.Name);
            projectWriter.WriteLine("|Win32'\">$(SolutionDir)$(Configuration)\\</OutDir>");
            projectWriter.Write("    <IntDir Condition=\"'$(Configuration)|$(Platform)'=='");
            projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
            projectWriter.Write(conf.Name);
            projectWriter.WriteLine("|Win32'\">$(Configuration)\\</IntDir>");
            projectWriter.Write("    <LinkIncremental Condition=\"'$(Configuration)|$(Platform)'=='");
            projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
            projectWriter.Write(conf.Name);
            projectWriter.WriteLine("|Win32'\">false</LinkIncremental>");
        }

        void OutputItemDefinitionGroup(ProjectConfiguration conf)
        {
            projectWriter.Write("  <ItemDefinitionGroup Condition=\"'$(Configuration)|$(Platform)'=='");
            projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
            projectWriter.Write(conf.Name);
            projectWriter.WriteLine("|Win32'\">");
            projectWriter.WriteLine("    <ClCompile>");
            if (conf.IsDebug)
            {
                projectWriter.WriteLine("      <Optimization>Disabled</Optimization>");
            }
            else
            {
                projectWriter.WriteLine("      <InlineFunctionExpansion>AnySuitable</InlineFunctionExpansion>");
                projectWriter.WriteLine("      <FavorSizeOrSpeed>Speed</FavorSizeOrSpeed>");
            }
            if (!string.IsNullOrEmpty(IncludeDirectories))
            {
                projectWriter.Write("      <AdditionalIncludeDirectories>");
                projectWriter.Write(IncludeDirectories);
                projectWriter.WriteLine("%(AdditionalIncludeDirectories)</AdditionalIncludeDirectories>");
            }
            projectWriter.Write("      <AdditionalUsingDirectories>");
            projectWriter.Write(conf.UsingDirectories);
            if (!string.IsNullOrEmpty(conf.UsingDirectories) && !conf.UsingDirectories.EndsWith(";"))
            {
                projectWriter.Write(';');
            }
            projectWriter.WriteLine("%(AdditionalUsingDirectories)</AdditionalUsingDirectories>");
            projectWriter.Write("      <PreprocessorDefinitions>");
            projectWriter.Write(conf.Definitions);
            if (!string.IsNullOrEmpty(conf.Definitions) && !conf.Definitions.EndsWith(";"))
            {
                projectWriter.Write(';');
            }
            projectWriter.Write("WIN32;");
            if (conf.IsDebug)
            {
                projectWriter.Write("_DEBUG;");
            }
            else
            {
                projectWriter.Write("NDEBUG;");
            }
            projectWriter.WriteLine("%(PreprocessorDefinitions)</PreprocessorDefinitions>");
            if (conf.IsDebug)
            {
                projectWriter.WriteLine("      <RuntimeLibrary>MultiThreadedDebugDLL</RuntimeLibrary>");
            }
            else
            {
                projectWriter.WriteLine("      <RuntimeLibrary>MultiThreadedDLL</RuntimeLibrary>");
                projectWriter.WriteLine("      <BufferSecurityCheck>false</BufferSecurityCheck>");
            }
            projectWriter.WriteLine("      <FloatingPointModel>Precise</FloatingPointModel>");
            projectWriter.WriteLine("      <PrecompiledHeader>Use</PrecompiledHeader>");
            if (conf.IsDebug)
            {
                projectWriter.WriteLine("      <WarningLevel>Level3</WarningLevel>");
                projectWriter.WriteLine("      <DebugInformationFormat>ProgramDatabase</DebugInformationFormat>");
            }
            else
            {
                projectWriter.WriteLine("      <WarningLevel>Level1</WarningLevel>");
            }
            //projectWriter.WriteLine("      <DisableSpecificWarnings>4793;%(DisableSpecificWarnings)</DisableSpecificWarnings>");
            projectWriter.WriteLine("    </ClCompile>");
            projectWriter.WriteLine("    <Link>");
            if (conf.IsDebug)
            {
                projectWriter.WriteLine("      <AdditionalDependencies>LinearMath_Debug.lib;BulletCollision_Debug.lib;BulletDynamics_Debug.lib</AdditionalDependencies>");
                if (!string.IsNullOrEmpty(LibraryDirectoriesDebug))
                {
                    projectWriter.Write("      <AdditionalLibraryDirectories>");
                    projectWriter.Write(LibraryDirectoriesDebug);
                    projectWriter.WriteLine("%(AdditionalLibraryDirectories)</AdditionalLibraryDirectories>");
                }
                projectWriter.WriteLine("      <GenerateDebugInformation>true</GenerateDebugInformation>");
                projectWriter.WriteLine("      <AssemblyDebug>true</AssemblyDebug>");
            }
            else
            {
                projectWriter.WriteLine("      <AdditionalDependencies>LinearMath_MinSizeRel.lib;BulletCollision_MinsizeRel.lib;BulletDynamics_MinsizeRel.lib</AdditionalDependencies>");
                if (!string.IsNullOrEmpty(LibraryDirectoriesRelease))
                {
                    projectWriter.Write("      <AdditionalLibraryDirectories>");
                    projectWriter.Write(LibraryDirectoriesRelease);
                    projectWriter.WriteLine("%(AdditionalLibraryDirectories)</AdditionalLibraryDirectories>");
                }
            }
            projectWriter.WriteLine("      <TargetMachine>MachineX86</TargetMachine>");
            //projectWriter.WriteLine("      <CLRUnmanagedCodeCheck>true</CLRUnmanagedCodeCheck>");
            projectWriter.WriteLine("    </Link>");
            projectWriter.WriteLine("  </ItemDefinitionGroup>");
        }

        void OutputItemGroupReference(string referenceName, string assemblyName)
        {
            if (targetVS == TargetVS.VS2008)
            {
                projectWriter.WriteLine("\t\t<AssemblyReference");
                projectWriter.Write("\t\t\tRelativePath=\"");
                projectWriter.Write(referenceName);
                projectWriter.WriteLine(".dll\"");
                projectWriter.Write("\t\t\tAssemblyName=\"");
                projectWriter.Write(assemblyName);
                projectWriter.WriteLine("\"");
                projectWriter.WriteLine("\t\t\tMinFrameworkVersion=\"131072\"");
                projectWriter.WriteLine("\t\t/>");
            }
            else
            {
                projectWriter.Write("    <Reference Include=\"");
                projectWriter.Write(referenceName);
                projectWriter.WriteLine("\">");
                projectWriter.WriteLine("      <CopyLocalSatelliteAssemblies>true</CopyLocalSatelliteAssemblies>");
                projectWriter.WriteLine("      <ReferenceOutputAssembly>true</ReferenceOutputAssembly>");
                projectWriter.WriteLine("    </Reference>");
            }
        }

        public void Output(TargetVS targetVS, List<ProjectConfiguration> confs, string outDirectory)
        {
            Directory.CreateDirectory(outDirectory);
            FileStream solutionFile = new FileStream(outDirectory + "\\" + namespaceName + ".sln", FileMode.Create, FileAccess.Write);
            solutionWriter = new StreamWriter(solutionFile, Encoding.UTF8);

            this.targetVS = targetVS;
            confs = confs.OrderBy(c => !c.IsDebug).ThenBy(c => c.Name).ToList();

            Guid projectGuid = new Guid("5A0DEF7E-B7E3-45E9-A511-0F03CECFF8C0");
            string projectGuidString = projectGuid.ToString().ToUpper();

            solutionWriter.WriteLine();
            solutionWriter.Write("Microsoft Visual Studio Solution File, Format Version ");
            switch (targetVS)
            {
                case TargetVS.VS2008:
                    solutionWriter.WriteLine("10.00");
                    solutionWriter.WriteLine("# Visual C++ Express 2008");
                    break;
                case TargetVS.VS2010:
                    solutionWriter.WriteLine("11.00");
                    solutionWriter.WriteLine("# Visual Studio 2010");
                    break;
                case TargetVS.VS2012:
                case TargetVS.VS2013:
                    solutionWriter.WriteLine("12.00");
                    solutionWriter.WriteLine("# Visual Studio 11");
                    break;
                default:
                    throw new NotImplementedException();
            }

            Guid vcppProjectType = new Guid("8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942");
            string vcppProjectTypeString = vcppProjectType.ToString().ToUpper();
            solutionWriter.Write("Project(\"{");
            solutionWriter.Write(vcppProjectTypeString);
            solutionWriter.Write("}\") = \"");
            solutionWriter.Write(namespaceName);
            solutionWriter.Write("\", \"");
            solutionWriter.Write(namespaceName);
            if (targetVS == TargetVS.VS2008)
            {
                solutionWriter.Write(".vcproj\", \"{");
            }
            else
            {
                solutionWriter.Write(".vcxproj\", \"{");
            }
            solutionWriter.Write(projectGuidString);
            solutionWriter.WriteLine("}\"");
            solutionWriter.WriteLine("EndProject");

            solutionWriter.WriteLine("Global");

            solutionWriter.WriteLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
            foreach (var conf in confs)
            {
                solutionWriter.Write("\t\t");
                solutionWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                solutionWriter.Write(conf.Name);
                solutionWriter.Write("|Win32 = ");
                solutionWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                solutionWriter.Write(conf.Name);
                solutionWriter.WriteLine("|Win32");
            }
            solutionWriter.WriteLine("\tEndGlobalSection");

            solutionWriter.WriteLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
            foreach (var conf in confs)
            {
                solutionWriter.Write("\t\t{");
                solutionWriter.Write(projectGuidString);
                solutionWriter.Write("}.");
                solutionWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                solutionWriter.Write(conf.Name);
                solutionWriter.Write("|Win32.ActiveCfg = ");
                solutionWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                solutionWriter.Write(conf.Name);
                solutionWriter.WriteLine("|Win32");

                solutionWriter.Write("\t\t{");
                solutionWriter.Write(projectGuidString);
                solutionWriter.Write("}.");
                solutionWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                solutionWriter.Write(conf.Name);
                solutionWriter.Write("|Win32.Build.0 = ");
                solutionWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                solutionWriter.Write(conf.Name);
                solutionWriter.WriteLine("|Win32");
            }
            solutionWriter.WriteLine("\tEndGlobalSection");

            solutionWriter.WriteLine("\tGlobalSection(SolutionProperties) = preSolution");
            solutionWriter.WriteLine("\t\tHideSolutionNode = FALSE");
            solutionWriter.WriteLine("\tEndGlobalSection");
            solutionWriter.WriteLine("EndGlobal");

            solutionWriter.Dispose();
            solutionFile.Dispose();


            string projectFilename = namespaceName + (targetVS == TargetVS.VS2008 ? ".vcproj" : ".vcxproj");
            FileStream projectFile = new FileStream(outDirectory + "\\" + projectFilename, FileMode.Create, FileAccess.Write);
            projectWriter = new StreamWriter(projectFile, Encoding.UTF8);
            
            if (targetVS == TargetVS.VS2008)
            {
                projectWriter.WriteLine("<?xml version=\"1.0\" encoding=\"Windows-1252\"?>");
                projectWriter.WriteLine("<VisualStudioProject");
                projectWriter.WriteLine("\tProjectType=\"Visual C++\"");
                projectWriter.WriteLine("\tVersion=\"9.00\"");
                projectWriter.Write("\tName=\"");
                projectWriter.Write(namespaceName);
                projectWriter.WriteLine("\"");
                projectWriter.Write("\tProjectGUID=\"{");
                projectWriter.Write(projectGuidString);
                projectWriter.WriteLine("}\"");
                projectWriter.Write("\tRootNamespace=\"");
                projectWriter.Write(namespaceName);
                projectWriter.WriteLine("\"");
                projectWriter.WriteLine("\tKeyword=\"ManagedCProj\"");
                projectWriter.WriteLine("	TargetFrameworkVersion=\"131072\"");
                projectWriter.WriteLine("\t>");
                projectWriter.WriteLine("\t<Platforms>");
                projectWriter.WriteLine("\t\t<Platform");
                projectWriter.WriteLine("\t\t\tName=\"Win32\"");
                projectWriter.WriteLine("\t\t/>");
                projectWriter.WriteLine("\t</Platforms>");
                projectWriter.WriteLine("\t<ToolFiles>");
                projectWriter.WriteLine("\t</ToolFiles>");
            }
            else
            {
                projectWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                projectWriter.Write("<Project DefaultTargets=\"Build\" ToolsVersion=\"");
                switch (targetVS)
                {
                    case TargetVS.VS2010:
                    case TargetVS.VS2012:
                        projectWriter.Write("4.0");
                        break;
                    case TargetVS.VS2013:
                        projectWriter.Write("12.0");
                        break;
                    default:
                        throw new NotImplementedException();
                }
                projectWriter.WriteLine("\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
            }

            if (targetVS == TargetVS.VS2008)
            {
                projectWriter.WriteLine("\t<Configurations>");
            }
            else
            {
                projectWriter.WriteLine("  <ItemGroup Label=\"ProjectConfigurations\">");
            }
            foreach (var conf in confs)
            {
                OutputProjectConfiguration(conf);
            }
            if (targetVS == TargetVS.VS2008)
            {
                projectWriter.WriteLine("\t</Configurations>");
            }
            else
            {
                projectWriter.WriteLine("  </ItemGroup>");

                projectWriter.WriteLine("  <PropertyGroup Label=\"Globals\">");
                projectWriter.Write("    <ProjectGuid>{");
                projectWriter.Write(projectGuidString);
                projectWriter.WriteLine("}</ProjectGuid>");
                projectWriter.Write("    <RootNamespace>");
                projectWriter.Write(namespaceName);
                projectWriter.WriteLine("</RootNamespace>");
                projectWriter.WriteLine("    <Keyword>ManagedCProj</Keyword>");
                projectWriter.WriteLine("  </PropertyGroup>");


                projectWriter.WriteLine("  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.Default.props\" />");


                foreach (var conf in confs)
                {
                    OutputPropertyGroupConfiguration(conf);
                }

                projectWriter.WriteLine("  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.props\" />");
                projectWriter.WriteLine("  <ImportGroup Label=\"ExtensionSettings\">");
                projectWriter.WriteLine("  </ImportGroup>");


                foreach (var conf in confs)
                {
                    OutputImportGroupPropertySheets(conf);
                }


                projectWriter.WriteLine("  <PropertyGroup Label=\"UserMacros\" />");
                projectWriter.WriteLine("  <PropertyGroup>");
                projectWriter.WriteLine("    <_ProjectFileVersion>10.0.30319.1</_ProjectFileVersion>");
                foreach (var conf in confs)
                {
                    OutputPropertyGroupConfiguration2(conf);
                }
                projectWriter.WriteLine("  </PropertyGroup>");


                foreach (var conf in confs)
                {
                    OutputItemDefinitionGroup(conf);
                }
            }

            if (targetVS == TargetVS.VS2008)
            {
                projectWriter.WriteLine("\t<References>");
            }
            else
            {
                projectWriter.WriteLine("  <ItemGroup>");
            }
            OutputItemGroupReference("System", "System, Version=2.0.0.0, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL");
            OutputItemGroupReference("System.Drawing", "System.Drawing, Version=2.0.0.0, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL");
            if (targetVS == TargetVS.VS2008)
            {
                projectWriter.WriteLine("\t</References>");
            }
            else
            {
                projectWriter.WriteLine("  </ItemGroup>");
            }

            
            if (targetVS == TargetVS.VS2008)
            {
                projectWriter.WriteLine("\t<Files>");
                
                projectWriter.WriteLine("\t\t<Filter");
                projectWriter.WriteLine("\t\t\tName=\"Source Files\"");
                projectWriter.WriteLine("\t\t\tFilter=\"cpp;c;cc;cxx;def;odl;idl;hpj;bat;asm;asmx\"");
                projectWriter.WriteLine("\t\t\tUniqueIdentifier=\"{4FC737F1-C7A5-4376-A066-2A32D752A2FF}\"");
                projectWriter.WriteLine("\t\t\t>");
                foreach (var sourceFile in sourceFiles)
                {
                    projectWriter.WriteLine("\t\t\t<File");
                    projectWriter.Write("\t\t\t\tRelativePath=\"");
                    projectWriter.Write(sourceFile);
                    projectWriter.WriteLine(".cpp\"");
                    if (sourceFile.EndsWith("Stdafx", StringComparison.InvariantCultureIgnoreCase))
                    {
                        projectWriter.WriteLine("\t\t\t\t>");
                        foreach (var conf in confs)
                        {
                            projectWriter.WriteLine("\t\t\t\t<FileConfiguration");
                            projectWriter.Write("\t\t\t\t\tName=\"");
                            projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                            projectWriter.Write(conf.Name);
                            projectWriter.WriteLine("|Win32\"");
                            projectWriter.WriteLine("\t\t\t\t\t>");
                            projectWriter.WriteLine("\t\t\t\t\t<Tool");
                            projectWriter.WriteLine("\t\t\t\t\t\tName=\"VCCLCompilerTool\"");
                            projectWriter.WriteLine("\t\t\t\t\t\tUsePrecompiledHeader=\"1\"");
                            projectWriter.WriteLine("\t\t\t\t\t/>");
                            projectWriter.WriteLine("\t\t\t\t</FileConfiguration>");
                        }
                    }
                    else
                    {
                        projectWriter.WriteLine("\t\t\t\t>");
                    }
                    projectWriter.WriteLine("\t\t\t</File>");
                }
                projectWriter.WriteLine("\t\t</Filter>");

                projectWriter.WriteLine("\t\t<Filter");
                projectWriter.WriteLine("\t\t\tName=\"Header Files\"");
                projectWriter.WriteLine("\t\t\tFilter=\"h;hpp;hxx;hm;inl;inc;xsd\"");
                projectWriter.WriteLine("\t\t\tUniqueIdentifier=\"{93995380-89BD-4b04-88EB-625FBE52EBFB}\"");
                projectWriter.WriteLine("\t\t\t>");
                foreach (var sourceFile in sourceFiles)
                {
                    projectWriter.WriteLine("\t\t\t<File");
                    projectWriter.Write("\t\t\t\tRelativePath=\"");
                    projectWriter.Write(sourceFile);
                    projectWriter.WriteLine(".h\"");
                    projectWriter.WriteLine("\t\t\t\t>");
                    projectWriter.WriteLine("\t\t\t</File>");
                }
                projectWriter.WriteLine("\t\t</Filter>");

                projectWriter.WriteLine("\t</Files>");

                projectWriter.WriteLine("\t<Globals>");
                projectWriter.WriteLine("\t</Globals>");
                projectWriter.WriteLine("</VisualStudioProject>");
            }
            else
            {
                projectWriter.WriteLine("  <ItemGroup>");
                foreach (var sourceFile in sourceFiles)
                {
                    projectWriter.Write("    <ClCompile Include=\"");
                    projectWriter.Write(sourceFile);
                    if (sourceFile.EndsWith("Stdafx", StringComparison.InvariantCultureIgnoreCase))
                    {
                        projectWriter.WriteLine(".cpp\">");
                        foreach (var conf in confs)
                        {
                            projectWriter.Write("      <PrecompiledHeader Condition=\"'$(Configuration)|$(Platform)'=='");
                            projectWriter.Write(conf.IsDebug ? "Debug " : "Release ");
                            projectWriter.Write(conf.Name);
                            projectWriter.WriteLine("|Win32'\">Create</PrecompiledHeader>");
                        }
                        projectWriter.WriteLine("    </ClCompile>");
                    }
                    else
                    {
                        projectWriter.WriteLine(".cpp\" />");
                    }
                }
                projectWriter.WriteLine("  </ItemGroup>");

                projectWriter.WriteLine("  <ItemGroup>");
                foreach (var resourceFile in resourceFiles)
                {
                    projectWriter.Write("    <ResourceCompile Include=\"");
                    projectWriter.Write(resourceFile);
                    projectWriter.WriteLine("\" />");
                }
                projectWriter.WriteLine("  </ItemGroup>");

                projectWriter.WriteLine("  <ItemGroup>");
                foreach (var headerFile in headerFiles)
                {
                    projectWriter.Write("    <ClInclude Include=\"");
                    projectWriter.Write(headerFile);
                    projectWriter.WriteLine(".h\" />");
                }
                projectWriter.WriteLine("  </ItemGroup>");

                projectWriter.WriteLine("  <Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.targets\" />");
                projectWriter.WriteLine("  <ImportGroup Label=\"ExtensionTargets\">");
                projectWriter.WriteLine("  </ImportGroup>");

                projectWriter.Write("</Project>");
            }

            projectWriter.Dispose();
            projectFile.Dispose();
        }
    }
}
