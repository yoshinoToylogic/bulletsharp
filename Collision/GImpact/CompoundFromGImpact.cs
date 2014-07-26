using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public sealed class CompoundFromGImpact
	{
        private CompoundFromGImpact()
		{
		}

	    public static CompoundShape Create(GImpactMeshShape gImpactMesh, float depth)
	    {
            return new CompoundShape(btCompoundFromGImpact_btCreateCompoundFromGimpactShape(gImpactMesh._native, depth));
	    }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern IntPtr btCompoundFromGImpact_btCreateCompoundFromGimpactShape(IntPtr gImpactMesh, float depth);
	}
}
