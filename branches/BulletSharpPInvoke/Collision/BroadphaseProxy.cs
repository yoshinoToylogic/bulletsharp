using System;

namespace BulletSharp
{
    public enum BroadphaseNativeType
    {
        // polyhedral convex shapes
        BoxShape,
        TriangleShape,
        TetrahedralShape,
        ConvexTriangleMeshShape,
        ConvexHullShape,
        CONVEX_POINT_CLOUD_SHAPE_PROXYTYPE,
        CUSTOM_POLYHEDRAL_SHAPE_TYPE,
        //implicit convex shapes
        IMPLICIT_CONVEX_SHAPES_START_HERE,
        SphereShape,
        MultiSphereShape,
        CapsuleShape,
        ConeShape,
        ConvexShape,
        CylinderShape,
        UniformScalingShape,
        MINKOWSKI_SUM_SHAPE_PROXYTYPE,
        MinkowskiDifferenceShape,
        Box2DShape,
        Convex2DShape,
        CUSTOM_CONVEX_SHAPE_TYPE,
        //concave shapes
        CONCAVE_SHAPES_START_HERE,
        //keep all the convex shapetype below here, for the check IsConvexShape in broadphase proxy!
        TriangleMeshShape,
        SCALED_TRIANGLE_MESH_SHAPE_PROXYTYPE,
        ///used for demo integration FAST/Swift collision library and Bullet
        FAST_CONCAVE_MESH_PROXYTYPE,
        //terrain
        TERRAIN_SHAPE_PROXYTYPE,
        ///Used for GIMPACT Trimesh integration
        GImpactShape,
        ///Multimaterial mesh
        MULTIMATERIAL_TRIANGLE_MESH_PROXYTYPE,

        EmptyShape,
        StaticPlane,
        CUSTOM_CONCAVE_SHAPE_TYPE,
        CONCAVE_SHAPES_END_HERE,

        CompoundShape,

        SoftBodyShape,
        HFFLUID_SHAPE_PROXYTYPE,
        HFFLUID_BUOYANT_CONVEX_SHAPE_PROXYTYPE,
        INVALID_SHAPE_PROXYTYPE,

        MAX_BROADPHASE_COLLISION_TYPES

    };

    public class BroadphaseProxy
    {
        internal IntPtr _native;
    }
}
