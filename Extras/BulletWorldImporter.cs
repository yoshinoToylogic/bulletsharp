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
                CollisionShape shape = ConvertCollisionShape(shapeData);
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
			return btBulletWorldImporter_loadFile(_native, fileName, preSwapFilenameOut);
		}

        public bool LoadFile(string fileName)
		{
			BulletFile bulletFile = new BulletFile(fileName);
            bool result = LoadFileFromMemory(bulletFile);

            /*
            //now you could save the file in 'native' format using
            //bulletFile2->writeFile("native.bullet");
            if (result)
            {
                    if (preSwapFilenameOut)
                    {
                            bulletFile2->preSwap();
                            bulletFile2->writeFile(preSwapFilenameOut);
                    }
                
            }
            */

            bulletFile.Dispose();
            return result;
		}
        /*
		public bool LoadFileFromMemory(char memoryBuffer, int len)
		{
			return btBulletWorldImporter_loadFileFromMemory(_native, memoryBuffer._native, len);
		}
        */
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
        
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btBulletWorldImporter_loadFile(IntPtr obj, string fileName, string preSwapFilenameOut);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern bool btBulletWorldImporter_loadFile2(IntPtr obj, string fileName);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern bool btBulletWorldImporter_loadFileFromMemory(IntPtr obj, IntPtr memoryBuffer, int len);
	}
}
