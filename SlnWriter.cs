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
        Dictionary<string, HeaderDefinition> headerDefinitions = new Dictionary<string, HeaderDefinition>();
        StreamWriter solutionWriter, projectWriter;
        string namespaceName;
        TargetVS targetVS = TargetVS.VS2010;
        string targetVersionString;

        public SlnWriter(Dictionary<string, HeaderDefinition> headerDefinitions, string namespaceName)
        {
            this.headerDefinitions = headerDefinitions;
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
                projectWriter.WriteLine("\t\t\t\tAdditionalIncludeDirectories=\"..\\..\\bullet\\src;..\\..\\bullet\\Extras\\HACD;..\\..\\bullet\\Extras\\Serialize\\BulletWorldImporter\"");
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
                //projectWriter.WriteLine("\t\t\t\tAdditionalOptions=\"/NODEFAULTLIB:libcmt /NODEFAULTLIB:msvcprt\"");
                projectWriter.Write("\t\t\t\tAdditionalDependencies=\"");
                if (conf.IsDebug)
                {
                    projectWriter.WriteLine("LinearMath_Debug.lib;BulletCollision_Debug.lib;BulletDynamics_Debug.lib\"");
                }
                else
                {
                    projectWriter.WriteLine("LinearMath_MinSizeRel.lib;BulletCollision_MinsizeRel.lib;BulletDynamics_MinsizeRel.lib\"");
                }
                projectWriter.WriteLine("\t\t\t\tLinkIncremental=\"1\"");
                if (conf.IsDebug)
                {
                    projectWriter.WriteLine("\t\t\t\tAdditionalLibraryDirectories=\"..\\..\\bullet\\msvc\\2008\\lib\\Debug\"");
                    projectWriter.WriteLine("\t\t\t\tGenerateDebugInformation=\"true\"");
                    projectWriter.WriteLine("\t\t\t\tAssemblyDebug=\"1\"");
                }
                else
                {
                    projectWriter.WriteLine("\t\t\t\tAdditionalLibraryDirectories=\"..\\..\\bullet\\msvc\\2008\\lib\\MinSizeRel\"");
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
            //projectWriter.WriteLine("      <AdditionalIncludeDirectories>..\\bullet\\src;..\\bullet\\Extras\\HACD;..\\bullet\\Extras\\Serialize\\BulletWorldImporter;$(CUDA_INC_PATH);$(AMDAPPSDKROOT)include;%(AdditionalIncludeDirectories)</AdditionalIncludeDirectories>");
            projectWriter.WriteLine("      <AdditionalIncludeDirectories>..\\..\\bullet\\src;..\\..\\bullet\\Extras\\HACD;..\\..\\bullet\\Extras\\Serialize\\BulletWorldImporter;$(CUDA_INC_PATH);$(AMDAPPSDKROOT)include;%(AdditionalIncludeDirectories)</AdditionalIncludeDirectories>");
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
                projectWriter.WriteLine("      <AdditionalLibraryDirectories>..\\..\\bullet\\msvc\\" + targetVersionString + "\\lib\\Debug;$(ATISTREAMSDKROOT)lib\\x86\\;$(CUDA_LIB_PATH);%(AdditionalLibraryDirectories)</AdditionalLibraryDirectories>");
                projectWriter.WriteLine("      <GenerateDebugInformation>true</GenerateDebugInformation>");
                projectWriter.WriteLine("      <AssemblyDebug>true</AssemblyDebug>");
            }
            else
            {
                projectWriter.WriteLine("      <AdditionalDependencies>LinearMath_MinSizeRel.lib;BulletCollision_MinsizeRel.lib;BulletDynamics_MinsizeRel.lib</AdditionalDependencies>");
                projectWriter.WriteLine("      <AdditionalLibraryDirectories>..\\..\\bullet\\msvc\\" + targetVersionString + "\\lib\\MinSizeRel;$(ATISTREAMSDKROOT)lib\\x86\\;$(CUDA_LIB_PATH);%(AdditionalLibraryDirectories)</AdditionalLibraryDirectories>");
            }
            projectWriter.WriteLine("      <TargetMachine>MachineX86</TargetMachine>");
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

        public void Output()
        {
            string outDirectory = "src";

            Directory.CreateDirectory(outDirectory);
            FileStream solutionFile = new FileStream(outDirectory + "\\" + namespaceName + ".sln", FileMode.Create, FileAccess.Write);
            solutionWriter = new StreamWriter(solutionFile, Encoding.UTF8);

            List<ProjectConfiguration> confs = new List<ProjectConfiguration>();
            confs.Add(new ProjectConfiguration("Axiom", true, "GRAPHICS_AXIOM", "..\\..\\Axiom-SDK-0.8.3376.12322\\bin\\Net35"));
            confs.Add(new ProjectConfiguration("Axiom", false, "GRAPHICS_AXIOM", "..\\..\\Axiom-SDK-0.8.3376.12322\\bin\\Net35"));
            confs.Add(new ProjectConfiguration("Generic", true, "GRAPHICS_GENERIC"));
            confs.Add(new ProjectConfiguration("Generic", false, "GRAPHICS_GENERIC"));
            confs.Add(new ProjectConfiguration("Mogre", true, "GRAPHICS_MOGRE", "C:\\MogreSDK\\bin\\Debug"));
            confs.Add(new ProjectConfiguration("Mogre", false, "GRAPHICS_MOGRE", "C:\\MogreSDK\\bin\\Release"));
            confs.Add(new ProjectConfiguration("OpenTK", true, "GRAPHICS_OPENTK", "$(USERPROFILE)\\My Documents\\OpenTK\\1.0\\Binaries\\OpenTK\\Release"));
            confs.Add(new ProjectConfiguration("OpenTK", false, "GRAPHICS_OPENTK", "$(USERPROFILE)\\My Documents\\OpenTK\\1.0\\Binaries\\OpenTK\\Release"));
            confs.Add(new ProjectConfiguration("SharpDX", true, "GRAPHICS_SHARPDX", "..\\..\\sharpdx\\Source\\SharpDX\\bin\\Net40Debug"));
            confs.Add(new ProjectConfiguration("SharpDX", false, "GRAPHICS_SHARPDX", "..\\..\\sharpdx\\Source\\SharpDX\\bin\\Net40Release"));
            confs.Add(new ProjectConfiguration("SlimDX", true, "GRAPHICS_SLIMDX", "$(PROGRAMFILES)\\SlimDX SDK (January 2012)\\Bin\\net40\\;$(PROGRAMFILES(x86))\\SlimDX SDK (June 2010)\\Bin\\net40\\"));
            confs.Add(new ProjectConfiguration("SlimDX", false, "GRAPHICS_SLIMDX", "$(PROGRAMFILES)\\SlimDX SDK (January 2012)\\Bin\\net40\\;$(PROGRAMFILES(x86))\\SlimDX SDK (June 2010)\\Bin\\net40\\"));
            confs.Add(new ProjectConfiguration("SlimMath", true, "GRAPHICS_SLIMMATH", "..\\..\\SlimMath\\SlimMath\\bin\\Release"));
            confs.Add(new ProjectConfiguration("SlimMath", false, "GRAPHICS_SLIMMATH", "..\\..\\SlimMath\\SlimMath\\bin\\Debug"));
            confs.Add(new ProjectConfiguration("Windows API Code Pack", true, "GRAPHICS_WAPICODEPACK", "..\\..\\Windows API Code Pack 1.1\\binaries\\DirectX"));
            confs.Add(new ProjectConfiguration("Windows API Code Pack", false, "GRAPHICS_WAPICODEPACK", "..\\..\\Windows API Code Pack 1.1\\binaries\\DirectX"));
            confs.Add(new ProjectConfiguration("XNA 3.1", true, "GRAPHICS_XNA31", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\"));
            confs.Add(new ProjectConfiguration("XNA 3.1", false, "GRAPHICS_XNA31", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\"));
            confs.Add(new ProjectConfiguration("XNA 4.0", true, "GRAPHICS_XNA40", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\"));
            confs.Add(new ProjectConfiguration("XNA 4.0", false, "GRAPHICS_XNA40", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\"));

            confs = confs.OrderBy(c => !c.IsDebug).ThenBy(c => c.Name).ToList();


            switch (targetVS)
            {
                case TargetVS.VS2008:
                    targetVersionString = "2008";
                    break;
                case TargetVS.VS2010:
                    targetVersionString = "2010";
                    break;
                case TargetVS.VS2012:
                    targetVersionString = "2012";
                    break;
                case TargetVS.VS2013:
                    targetVersionString = "2013";
                    break;
            }


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

            /*
            projectWriter.WriteLine("  <ItemGroup>");
            foreach (HeaderDefinition header in headerDefinitions.Values.OrderBy(header => header.ManagedName))
            {
                if (header.Classes.Count == 0)
                {
                    continue;
                }

                projectWriter.Write("    <ClCompile Include=\"..\\");
                projectWriter.Write(outDirectory);
                projectWriter.Write('\\');
                projectWriter.Write(header.ManagedName);
                projectWriter.WriteLine(".cpp\" />");
            }
            projectWriter.WriteLine("  </ItemGroup>");
            */

            if (targetVS == TargetVS.VS2008)
            {
                projectWriter.WriteLine("\t<Globals>");
                projectWriter.WriteLine("\t</Globals>");
                projectWriter.WriteLine("</VisualStudioProject>");
            }
            else
            {
                projectWriter.WriteLine("  <ItemGroup>");
                projectWriter.WriteLine("    <ResourceCompile Include=\"..\\src\\Resources.rc\" />");
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
