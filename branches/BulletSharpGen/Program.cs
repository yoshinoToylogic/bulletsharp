using System;

namespace BulletSharpGen
{
    class Program
    {
        static void Main(string[] args)
        {
            //var subset = new AssemblySubset();
            //subset.LoadAssembly("..\\..\\..\\bulletsharp\\demos\\Generic\\bin\\Release\\BasicDemo.exe", "BulletSharp");
            //subset.LoadAssembly("..\\..\\..\\bulletsharp\\demos\\Generic\\bin\\Release\\DemoFramework.dll", "BulletSharp");

            //var parser = new CppReader("D:\\src\\bullet\\src\\");
            var reader = new CppReader("..\\..\\..\\bullet\\src\\");
            var parser = new BulletParser(reader.ClassDefinitions, reader.HeaderDefinitions);
            var writer = new CWriter(parser.HeaderDefinitions, "BulletSharp");
            //var writer = new CppWriter(parser.HeaderDefinitions, "BulletSharp");
            writer.Output();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
