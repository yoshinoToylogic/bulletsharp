using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Collections.Generic;

namespace BulletSharp
{
	public class BulletWorldImporter : WorldImporter
	{
		public BulletWorldImporter(DynamicsWorld world)
			: base(world)
		{
		}

		public BulletWorldImporter()
			: base(null)
		{
		}
        
		public bool ConvertAllObjects(BulletFile file)
		{
            _shapeMap.Clear();
            _bodyMap.Clear();

            foreach (byte[] shapeData in file._collisionShapes)
            {
                CollisionShape shape = ConvertCollisionShape(shapeData, file.LibPointers);
                if (shape != null)
                {
                    foreach (KeyValuePair<long, byte[]> lib in file.LibPointers)
                    {
                        if (lib.Value == shapeData)
                        {
                            _shapeMap.Add(lib.Key, shape);
                            break;
                        }
                    }
                }
                /*
                if (shape && shapeData->m_name)
                {
                    char* newname = duplicateName(shapeData->m_name);
                    m_objectNameMap.insert(shape, newname);
                    m_nameShapeMap.insert(newname, shape);
                }*/
                //throw new NotImplementedException();
            }

            foreach (byte[] bodyData in file._rigidBodies)
            {
                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    ConvertRigidBodyFloat(bodyData);
                }
            }

            return true;
		}

        public bool LoadFile(string fileName, string preSwapFilenameOut)
		{
			BulletFile bulletFile = new BulletFile(fileName);
            bool result = LoadFileFromMemory(bulletFile);

            //now you could save the file in 'native' format using
            //bulletFile.WriteFile("native.bullet");
            if (result)
            {
                    if (preSwapFilenameOut != null)
                    {
                        bulletFile.PreSwap();
                        //bulletFile.WriteFile(preSwapFilenameOut);
                    }
                
            }

            return result;
		}

        public bool LoadFile(string fileName)
        {
            return LoadFile(fileName, null);
        }
        
		public bool LoadFileFromMemory(byte[] memoryBuffer, int len)
		{
            BulletFile bulletFile = new BulletFile(memoryBuffer, len);
            return LoadFileFromMemory(bulletFile);
		}
        
        public bool LoadFileFromMemory(BulletFile bulletFile)
		{
            if ((bulletFile.Flags & FileFlags.OK) != FileFlags.OK)
            {
                return false;
            }

            bulletFile.Parse(_verboseMode);

            if ((_verboseMode & FileVerboseMode.DumpChunks) == FileVerboseMode.DumpChunks)
            {
                //bulletFile.DumpChunks(bulletFile->FileDna);
            }

            return ConvertAllObjects(bulletFile);
		}
	}
}
