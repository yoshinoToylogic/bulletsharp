using System;

namespace BulletSharpGen
{
    class Program
    {
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
                var writer = new CppWriter(parser.HeaderDefinitions, "BulletSharp");
                writer.Output();
            }
            else
            {
                var writer = new CWriter(parser.HeaderDefinitions, "BulletSharp");
                writer.Output();
            }
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
