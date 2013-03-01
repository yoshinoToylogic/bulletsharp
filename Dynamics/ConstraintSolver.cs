using System;

namespace BulletSharp
{
    public class ConstraintSolver
    {
        internal IntPtr _native;

        internal ConstraintSolver(IntPtr native)
        {
            _native = native;
        }

        public ConstraintSolver()
        {
            throw new NotImplementedException();
        }
    }
}
