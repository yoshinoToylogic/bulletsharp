using BulletSharp;
using System;

namespace OpenCLClothDemo
{
    class CLStuff
    {
        public static IntPtr cxMainContext;
        public static IntPtr device;
        public static IntPtr commandQueue;

        // BulletSharp has to be compiled with CL support if these types can't be found. See Stdafx.h
        public static CLDeviceType deviceType = CLDeviceType.All;

        public static void InitCL()
        {
            int ciErrNum = 0;
            cxMainContext = OclCommon.CreateContextFromType(deviceType, ref ciErrNum);
            if (ciErrNum != 0)
            {
                System.Windows.Forms.MessageBox.Show("OpenCL CreateContextFromType failed, error " + ciErrNum);
                return;
            }

            device = OclUtils.GetMaxFlopsDev(cxMainContext);

            OclUtils.PrintDeviceInfo(device);

            commandQueue = CL.CreateCommandQueue(cxMainContext, device, CLCommandQueueProperties.None, out ciErrNum);
            if (ciErrNum != 0)
            {
                System.Windows.Forms.MessageBox.Show("OpenCL CreateCommandQueue failed, error " + ciErrNum);
            }
        }
    }
}