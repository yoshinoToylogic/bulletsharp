using System;
using System.Collections.Generic;
using System.Linq;

namespace BulletSharpGen
{
    class Program
    {
        const string NamespaceName = "BulletSharp";

        static void Main(string[] args)
        {
            // If true, outputs C++/CLI wrapper,
            // if false, outputs C wrapper with C# code.
            bool cppCliMode = false;

            //var subset = new AssemblySubset();
            //subset.LoadAssembly("..\\..\\..\\bulletsharp\\demos\\Generic\\bin\\Release\\BasicDemo.exe", "BulletSharp");
            //subset.LoadAssembly("..\\..\\..\\bulletsharp\\demos\\Generic\\bin\\Release\\DemoFramework.dll", "BulletSharp");

            //var parser = new CppReader("D:\\src\\bullet\\src\\");
            var reader = new CppReader("..\\..\\..\\bullet\\src\\");
            var parser = new BulletParser(reader.ClassDefinitions, reader.HeaderDefinitions);
            if (cppCliMode)
            {
                var writer = new CppWriter(parser.HeaderDefinitions, NamespaceName);
                writer.Output();
            }
            else
            {
                var writer = new CWriter(parser.HeaderDefinitions, NamespaceName);
                writer.Output();
            }


            //OutputSolution(TargetVS.VS2008, parser.HeaderDefinitions);
            OutputSolution(TargetVS.VS2010, parser.HeaderDefinitions);
            //OutputSolution(TargetVS.VS2012, parser.HeaderDefinitions);
            //OutputSolution(TargetVS.VS2013, parser.HeaderDefinitions);


            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        static void OutputSolution(TargetVS targetVS, Dictionary<string, HeaderDefinition> headerDefinitions)
        {
            string targetVersionString;
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
                default:
                    throw new NotImplementedException();
            }

            string rootFolder, slnRelDir;
            if (targetVS == TargetVS.VS2010)
            {
                rootFolder = "src\\";
                slnRelDir = "";
            }
            else
            {
                rootFolder = "..\\src\\";
                slnRelDir = "..\\";
            }

            List<ProjectConfiguration> confs = new List<ProjectConfiguration>();
            confs.Add(new ProjectConfiguration("Axiom", true, "GRAPHICS_AXIOM", slnRelDir + "..\\Axiom-SDK-0.8.3376.12322\\bin\\Net35"));
            confs.Add(new ProjectConfiguration("Axiom", false, "GRAPHICS_AXIOM", slnRelDir + "..\\Axiom-SDK-0.8.3376.12322\\bin\\Net35"));
            confs.Add(new ProjectConfiguration("Generic", true, "GRAPHICS_GENERIC"));
            confs.Add(new ProjectConfiguration("Generic", false, "GRAPHICS_GENERIC"));
            confs.Add(new ProjectConfiguration("Mogre", true, "GRAPHICS_MOGRE", "C:\\MogreSDK\\bin\\Debug"));
            confs.Add(new ProjectConfiguration("Mogre", false, "GRAPHICS_MOGRE", "C:\\MogreSDK\\bin\\Release"));
            confs.Add(new ProjectConfiguration("OpenTK", true, "GRAPHICS_OPENTK", "$(USERPROFILE)\\My Documents\\OpenTK\\1.0\\Binaries\\OpenTK\\Release"));
            confs.Add(new ProjectConfiguration("OpenTK", false, "GRAPHICS_OPENTK", "$(USERPROFILE)\\My Documents\\OpenTK\\1.0\\Binaries\\OpenTK\\Release"));
            confs.Add(new ProjectConfiguration("SharpDX", true, "GRAPHICS_SHARPDX", slnRelDir + "..\\sharpdx\\Source\\SharpDX\\bin\\Net40Debug"));
            confs.Add(new ProjectConfiguration("SharpDX", false, "GRAPHICS_SHARPDX", slnRelDir + "..\\sharpdx\\Source\\SharpDX\\bin\\Net40Release"));
            confs.Add(new ProjectConfiguration("SlimDX", true, "GRAPHICS_SLIMDX", "$(PROGRAMFILES)\\SlimDX SDK (January 2012)\\Bin\\net40\\;$(PROGRAMFILES(x86))\\SlimDX SDK (June 2010)\\Bin\\net40\\"));
            confs.Add(new ProjectConfiguration("SlimDX", false, "GRAPHICS_SLIMDX", "$(PROGRAMFILES)\\SlimDX SDK (January 2012)\\Bin\\net40\\;$(PROGRAMFILES(x86))\\SlimDX SDK (June 2010)\\Bin\\net40\\"));
            confs.Add(new ProjectConfiguration("SlimMath", true, "GRAPHICS_SLIMMATH", slnRelDir + "..\\SlimMath\\SlimMath\\bin\\Release"));
            confs.Add(new ProjectConfiguration("SlimMath", false, "GRAPHICS_SLIMMATH", slnRelDir + "..\\SlimMath\\SlimMath\\bin\\Debug"));
            if (targetVS != TargetVS.VS2008)
            {
                confs.Add(new ProjectConfiguration("Windows API Code Pack", true, "GRAPHICS_WAPICODEPACK", slnRelDir + "..\\Windows API Code Pack 1.1\\binaries\\DirectX"));
                confs.Add(new ProjectConfiguration("Windows API Code Pack", false, "GRAPHICS_WAPICODEPACK", slnRelDir + "..\\Windows API Code Pack 1.1\\binaries\\DirectX"));
            }
            confs.Add(new ProjectConfiguration("XNA 3.1", true, "GRAPHICS_XNA31", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\"));
            confs.Add(new ProjectConfiguration("XNA 3.1", false, "GRAPHICS_XNA31", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v3.1\\References\\Windows\\x86\\"));
            if (targetVS != TargetVS.VS2008)
            {
                confs.Add(new ProjectConfiguration("XNA 4.0", true, "GRAPHICS_XNA40", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\"));
                confs.Add(new ProjectConfiguration("XNA 4.0", false, "GRAPHICS_XNA40", "$(ProgramFiles)\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\;$(ProgramFiles(x86))\\Microsoft XNA\\XNA Game Studio\\v4.0\\References\\Windows\\x86\\"));
            }

            List<string> sourceFiles = new List<string>();
            sourceFiles.Add(rootFolder + "Stdafx");
            sourceFiles.Add(rootFolder + "AssemblyInfo");
            sourceFiles.Add(rootFolder + "Collections");
            sourceFiles.Add(rootFolder + "Math");
            sourceFiles.Add(rootFolder + "ObjectTable");
            sourceFiles.Add(rootFolder + "StringConv");
            sourceFiles.Add(rootFolder + "DataStream");
            sourceFiles.Add(rootFolder + "Matrix");
            //sourceFiles.Add(rootFolder + "Quaternion");
            sourceFiles.Add(rootFolder + "Utilities");
            //sourceFiles.Add(rootFolder + "Vector3");
            sourceFiles.Add(rootFolder + "Vector4");

            List<string> headerFiles = new List<string>();
            headerFiles.Add(rootFolder + "Stdafx");
            headerFiles.Add(rootFolder + "Collections");
            headerFiles.Add(rootFolder + "Enums");
            headerFiles.Add(rootFolder + "IDisposable");
            headerFiles.Add(rootFolder + "Math");
            headerFiles.Add(rootFolder + "ObjectTable");
            headerFiles.Add(rootFolder + "StringConv");
            headerFiles.Add(rootFolder + "Version");
            headerFiles.Add(rootFolder + "DataStream");
            headerFiles.Add(rootFolder + "Matrix");
            //headerFiles.Add(rootFolder + "Quaternion");
            headerFiles.Add(rootFolder + "Utilities");
            //headerFiles.Add(rootFolder + "Vector3");
            headerFiles.Add(rootFolder + "Vector4");

            string[] excludedClasses = new string[] { "ActionInterface", "AlignedAllocator", "bChunk", "bCommon",
            "bFile", "Box", "BulletFile", "cl_MiniCL_Defs", "cl_platform", "ContactProcessing", "ConvexHull",
            "ConvexHullComputer", "DantzigLCP", "DantzigSolver", "DefaultSoftBodySolver", "GenericPoolAllocator",
            "gim_array", "gim_bitset", "gim_box_collision", "gim_box_set", "gim_clip_polygon", "gim_contact",
            "gim_geom_types", "gim_hash_table", "gim_memory", "gim_radixsort", "gim_tri_collision", "GjkEpa2",
            "Gpu3DGridBroadphase", "Gpu3DGridBroadphaseSharedTypes", "GpuDefines", "GrahamScan2dConvexHull",
            "HashedSimplePairCache", "HashMap", "HeapManager", "IDebugDraw", "JacobianEntry", "List", "Material",
            "Matrix3x3", "MatrixX", "MiniCLTask", "MiniCLTaskScheduler", "MLCPSolver", "MLCPSolverInterface",
            "MultiBody", "MultiBodyConstraint", "MultiBodyConstraintSolver", "MultiBodyDynamicsWorld",
            "MultiBodyJointLimitConstraint", "MultiBodyJointMotor", "MultiBodyLink", "MultiBodyLinkCollider",
            "MultiBodyPoint2Point", "MultiBodySolverConstraint", "MultiSapBroadphase", "PlatformDefinitions",
            "PolarDecomposition", "PolyhedralContactClipping", "PosixThreadSupport", "PpuAddressSpace", "QuadWord",
            "RaycastCallback", "SequentialThreadSupport", "SimpleDynamicsWorld", "SoftBodyData", "SoftBodyInternals",
            "SoftBodySolvers", "SoftRigidCollisionAlgorithm", "SoftSoftCollisionAlgorithm", "SolveProjectedGaussSeidel",
            "SolverBody", "SolverConstraint", "SpuCollisionObjectWrapper", "SpuCollisionShapes",
            "SpuCollisionTaskProcess", "SpuContactManifoldCollisionAlgorithm", "SpuContactResult",
            "SpuConvexPenetrationDepthSolver", "SpuDoubleBuffer", "SpuGatheringCollisionTask",
            "SpuMinkowskiPenetrationDepthSolver", "SpuSampleTask", "SpuSampleTaskProcess", "SpuSync", "StackAlloc",
            "SubSimplexConvexCast", "Transform", "TrbDynBody", "TrbStateVec", "vectormath_aos", "vmInclude",
            "HacdCircularList", "HacdGraph", "HacdICHull", "HacdManifoldMesh", "HacdVector",
            "CompoundCompoundCollisionAlgorithm", "FixedConstraint"};

            List<string> bulletSourceFiles = new List<string>();
            List<string> bulletHeaderFiles = new List<string>();
            foreach (HeaderDefinition header in headerDefinitions.Values)
            {
                if (header.Classes.Count == 0 || excludedClasses.Contains(header.ManagedName))
                {
                    continue;
                }

                bulletSourceFiles.Add(rootFolder + header.ManagedName);
                bulletHeaderFiles.Add(rootFolder + header.ManagedName);
            }
            bulletSourceFiles.Add(rootFolder + "BulletMaterial");
            bulletSourceFiles.Add(rootFolder + "DebugDraw");
            bulletSourceFiles.Add(rootFolder + "IActionInterface");
            bulletSourceFiles.Add(rootFolder + "InternalEdgeUtility");
            bulletSourceFiles.Add(rootFolder + "OpenCL");
            bulletSourceFiles.Add(rootFolder + "SoftBodySolver");
            bulletSourceFiles.Add(rootFolder + "SoftBodySolverOpenCL");
            bulletHeaderFiles.Add(rootFolder + "BulletMaterial");
            bulletHeaderFiles.Add(rootFolder + "DebugDraw");
            bulletHeaderFiles.Add(rootFolder + "IActionInterface");
            bulletHeaderFiles.Add(rootFolder + "IDebugDraw");
            bulletHeaderFiles.Add(rootFolder + "InternalEdgeUtility");
            bulletHeaderFiles.Add(rootFolder + "OpenCL");
            bulletHeaderFiles.Add(rootFolder + "SoftBodySolver");
            bulletHeaderFiles.Add(rootFolder + "SoftBodySolverOpenCL");

            bulletSourceFiles.Sort();
            bulletHeaderFiles.Sort();
            sourceFiles.AddRange(bulletSourceFiles);
            headerFiles.AddRange(bulletHeaderFiles);

            var resourceFiles = new List<string>();
            resourceFiles.Add(rootFolder + "Resources.rc");

            var slnWriter = new SlnWriter(sourceFiles, headerFiles, resourceFiles, NamespaceName);
            slnWriter.IncludeDirectories = slnRelDir + "..\\bullet\\src;" + slnRelDir + "..\\bullet\\Extras\\HACD;" + slnRelDir + "..\\bullet\\Extras\\Serialize\\BulletWorldImporter;$(CUDA_INC_PATH);$(AMDAPPSDKROOT)include;";
            //slnWriter.IncludeDirectories = "..\\..\\bullet\\src;..\\..\\bullet\\Extras\\HACD;..\\..\\bullet\\Extras\\Serialize\\BulletWorldImporter;$(CUDA_INC_PATH);$(AMDAPPSDKROOT)include;";
            if (targetVS == TargetVS.VS2008)
            {
                slnWriter.LibraryDirectoriesRelease = slnRelDir + "..\\bullet\\msvc\\2008\\lib\\MinSizeRel";
                slnWriter.LibraryDirectoriesDebug = slnRelDir + "..\\bullet\\msvc\\2008\\lib\\Debug";
            }
            else
            {
                slnWriter.LibraryDirectoriesRelease = slnRelDir + "..\\bullet\\msvc\\" + targetVersionString + "\\lib\\MinSizeRel;$(AMDAPPSDKROOT)lib\\x86\\;$(CUDA_LIB_PATH);";
                slnWriter.LibraryDirectoriesDebug = slnRelDir + "..\\bullet\\msvc\\" + targetVersionString + "\\lib\\Debug;$(AMDAPPSDKROOT)lib\\x86\\;$(CUDA_LIB_PATH);";
            }
            slnWriter.Output(targetVS, confs, "src");
        }
    }
}
