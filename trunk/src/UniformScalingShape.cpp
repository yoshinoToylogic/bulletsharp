#include "StdAfx.h"

#include "UniformScalingShape.h"

UniformScalingShape::UniformScalingShape(btUniformScalingShape* shape)
: ConvexShape(shape)
{
}

UniformScalingShape::UniformScalingShape(ConvexShape^ convexChildShape, btScalar uniformScalingFactor)
: ConvexShape(new btUniformScalingShape(convexChildShape->UnmanagedPointer, uniformScalingFactor))
{
}

ConvexShape^ UniformScalingShape::ChildShape::get()
{
	return gcnew ConvexShape(UnmanagedPointer->getChildShape());
}

btScalar UniformScalingShape::UniformScalingFactor::get()
{
	return UnmanagedPointer->getUniformScalingFactor();
}

btUniformScalingShape* UniformScalingShape::UnmanagedPointer::get()
{
	return (btUniformScalingShape*)ConvexShape::UnmanagedPointer;
}
