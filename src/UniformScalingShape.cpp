#include "StdAfx.h"

#include "UniformScalingShape.h"

#define Unmanaged static_cast<btUniformScalingShape*>(_unmanaged)

UniformScalingShape::UniformScalingShape(btUniformScalingShape* shape)
: ConvexShape(shape)
{
}

UniformScalingShape::UniformScalingShape(ConvexShape^ convexChildShape, btScalar uniformScalingFactor)
: ConvexShape(new btUniformScalingShape((btConvexShape*)convexChildShape->_unmanaged, uniformScalingFactor))
{
}

ConvexShape^ UniformScalingShape::ChildShape::get()
{
	btConvexShape* childShape = Unmanaged->getChildShape();
	ReturnCachedObject(ConvexShape, _childShape, childShape);
}

btScalar UniformScalingShape::UniformScalingFactor::get()
{
	return Unmanaged->getUniformScalingFactor();
}
