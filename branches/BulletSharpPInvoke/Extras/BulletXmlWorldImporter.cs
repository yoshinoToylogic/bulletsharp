using System;
using System.Xml;

namespace BulletSharp
{
	public class BulletXmlWorldImporter : WorldImporter
	{
		public BulletXmlWorldImporter(DynamicsWorld world)
            : base(world)
		{
		}

		public bool LoadFile(string fileName)
		{
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            throw new NotImplementedException();
            return false;
		}
	}
}
