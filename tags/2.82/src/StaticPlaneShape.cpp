#include "StdAfx.h"

#include "StaticPlaneShape.h"

#define Native static_cast<btStaticPlaneShape*>(_native)

StaticPlaneShape::StaticPlaneShape(btStaticPlaneShape* shape)
: BulletSharp::ConcaveShape(shape)
{
}

StaticPlaneShape::StaticPlaneShape(Vector3 planeNormal, btScalar planeConstant)
: BulletSharp::ConcaveShape(0)
{
	VECTOR3_DEF(planeNormal);
	UnmanagedPointer = new btStaticPlaneShape(VECTOR3_USE(planeNormal), planeConstant);
	VECTOR3_DEL(planeNormal);
}

btScalar StaticPlaneShape::PlaneConstant::get()
{
	return Native->getPlaneConstant();
}

Vector3 StaticPlaneShape::PlaneNormal::get()
{
	return Math::BtVector3ToVector3(&Native->getPlaneNormal());
}