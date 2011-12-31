#include "StdAfx.h"

#include "CylinderShape.h"

CylinderShape::CylinderShape(btCylinderShape* shape)
: ConvexInternalShape(shape)
{
}

CylinderShape::CylinderShape(Vector3 halfExtents)
: ConvexInternalShape(0)
{
	btVector3* halfExtentsTemp = Math::Vector3ToBtVector3(halfExtents);
	UnmanagedPointer = new btCylinderShape(*halfExtentsTemp);
	delete halfExtentsTemp;
}

CylinderShape::CylinderShape(btScalar halfExtentsX, btScalar halfExtentsY, btScalar halfExtentsZ)
: ConvexInternalShape(0)
{
	btVector3* halfExtentsTemp = new btVector3(halfExtentsX,halfExtentsY,halfExtentsZ);
	UnmanagedPointer = new btCylinderShape(*halfExtentsTemp);
	delete halfExtentsTemp;
}

CylinderShape::CylinderShape(btScalar halfExtents)
: ConvexInternalShape(0)
{
	btVector3* halfExtentsTemp = new btVector3(halfExtents,halfExtents,halfExtents);
	UnmanagedPointer = new btCylinderShape(*halfExtentsTemp);
	delete halfExtentsTemp;
}

Vector3 CylinderShape::HalfExtentsWithMargin::get()
{
	btVector3* extentsTemp = new btVector3(UnmanagedPointer->getHalfExtentsWithMargin());
	Vector3 extents = Math::BtVector3ToVector3(extentsTemp);
	delete extentsTemp;
	return extents;
}

Vector3 CylinderShape::HalfExtentsWithoutMargin::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getHalfExtentsWithoutMargin());
}

btScalar CylinderShape::Radius::get()
{
	return UnmanagedPointer->getRadius();
}

int CylinderShape::UpAxis::get()
{
	return UnmanagedPointer->getUpAxis();
}

btCylinderShape* CylinderShape::UnmanagedPointer::get()
{
	return (btCylinderShape*)ConvexInternalShape::UnmanagedPointer;
}


CylinderShapeX::CylinderShapeX(Vector3 halfExtents)
: CylinderShape((btCylinderShape*)0)
{
	btVector3* halfExtentsTemp = Math::Vector3ToBtVector3(halfExtents);
	UnmanagedPointer = new btCylinderShapeX(*halfExtentsTemp);
	delete halfExtentsTemp;
}

CylinderShapeX::CylinderShapeX(btScalar halfExtentsX, btScalar halfExtentsY, btScalar halfExtentsZ)
: CylinderShape((btCylinderShape*)0)
{
	btVector3* halfExtentsTemp = new btVector3(halfExtentsX,halfExtentsY,halfExtentsZ);
	UnmanagedPointer = new btCylinderShapeX(*halfExtentsTemp);
	delete halfExtentsTemp;
}

CylinderShapeX::CylinderShapeX(btScalar halfExtents)
: CylinderShape((btCylinderShape*)0)
{
	btVector3* halfExtentsTemp = new btVector3(halfExtents,halfExtents,halfExtents);
	UnmanagedPointer = new btCylinderShapeX(*halfExtentsTemp);
	delete halfExtentsTemp;
}


CylinderShapeZ::CylinderShapeZ(Vector3 halfExtents)
: CylinderShape((btCylinderShape*)0)
{
	btVector3* halfExtentsTemp = Math::Vector3ToBtVector3(halfExtents);
	UnmanagedPointer = new btCylinderShapeZ(*halfExtentsTemp);
	delete halfExtentsTemp;
}

CylinderShapeZ::CylinderShapeZ(btScalar halfExtentsX, btScalar halfExtentsY, btScalar halfExtentsZ)
: CylinderShape((btCylinderShape*)0)
{
	btVector3* halfExtentsTemp = new btVector3(halfExtentsX,halfExtentsY,halfExtentsZ);
	UnmanagedPointer = new btCylinderShapeZ(*halfExtentsTemp);
	delete halfExtentsTemp;
}

CylinderShapeZ::CylinderShapeZ(btScalar halfExtents)
: CylinderShape((btCylinderShape*)0)
{
	btVector3* halfExtentsTemp = new btVector3(halfExtents,halfExtents,halfExtents);
	UnmanagedPointer = new btCylinderShapeZ(*halfExtentsTemp);
	delete halfExtentsTemp;
}