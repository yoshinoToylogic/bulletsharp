using System;

namespace BulletSharpGen
{
    class Program
    {
        static void Main(string[] args)
        {
            //var parser = new CppReader("D:\\src\\bullet\\src\\");
            var reader = new CppReader("..\\..\\..\\bullet\\src\\");
            var parser = new BulletParser(reader.ClassDefinitions, reader.HeaderDefinitions);
            var writer = new CppWriter(parser.HeaderDefinitions, "BulletSharp");
            writer.Output();
            Console.ReadKey();
        }
    }
}
