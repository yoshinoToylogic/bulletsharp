using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
    public class DefaultMotionState : MotionState
    {
        internal DefaultMotionState(IntPtr native)
            : base(native)
        {
            _native = native;
        }

        public DefaultMotionState()
            : base(BulletAPI_CreateBtDefaultMotionState())
        {
        }

        public DefaultMotionState(Matrix startTrans)
            : base(btDefaultMotionState_new2(ref startTrans))
        {
        }

        public DefaultMotionState(Matrix startTrans, Matrix centerOfMassOffset)
            : base(BulletAPI_CreateBtDefaultMotionStateStartTransCenterOfMassOffset(startTrans, centerOfMassOffset))
        {
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr BulletAPI_CreateBtDefaultMotionState();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btDefaultMotionState_new2([In] ref Matrix startTrans);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr BulletAPI_CreateBtDefaultMotionStateStartTransCenterOfMassOffset(Matrix startTrans,
                                                                                                     Matrix
                                                                                                         centerOfMassOffset);
    }
}
