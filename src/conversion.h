#pragma once

#include "LinearMath/btVector3.h"
#include "LinearMath/btTransform.h"

#define BTTRANSFORM_TRANSPOSE
#define BTTRANSFORM_TO4X4


#ifdef WIN32
#define STDCALL __stdcall*
#define uint unsigned int
#else
#define STDCALL *
#endif

#ifdef _MSC_VER
#define EXPORT __declspec(dllexport)
#else
#define EXPORT
#endif

inline void btVector3ToVector3(const btVector3* v, btScalar* s)
{
	s[0] = v->getX();
	s[1] = v->getY();
	s[2] = v->getZ();
}

inline void Vector3TobtVector3(const btScalar* s, btVector3* v)
{
	v->setX(s[0]);
	v->setY(s[1]);
	v->setZ(s[2]);
}

inline void btTransformToMatrix(const btTransform* t, btScalar* m)
{
#ifdef BTTRANSFORM_TO4X4
#ifdef BTTRANSFORM_TRANSPOSE
	m[0] = t->getBasis().getRow(0).getX();
	m[4] = t->getBasis().getRow(0).getY();
	m[8] = t->getBasis().getRow(0).getZ();
	m[1] = t->getBasis().getRow(1).getX();
	m[5] = t->getBasis().getRow(1).getY();
	m[9] = t->getBasis().getRow(1).getZ();
	m[2] = t->getBasis().getRow(2).getX();
	m[6] = t->getBasis().getRow(2).getY();
	m[10] = t->getBasis().getRow(2).getZ();
#else
	m[0] = t->getBasis().getRow(0).getX();
	m[1] = t->getBasis().getRow(0).getY();
	m[2] = t->getBasis().getRow(0).getZ();
	m[4] = t->getBasis().getRow(1).getX();
	m[5] = t->getBasis().getRow(1).getY();
	m[6] = t->getBasis().getRow(1).getZ();
	m[8] = t->getBasis().getRow(2).getX();
	m[9] = t->getBasis().getRow(2).getY();
	m[10] = t->getBasis().getRow(2).getZ();
#endif
	m[12] = t->getOrigin().getX();
	m[13] = t->getOrigin().getY();
	m[14] = t->getOrigin().getZ();
	m[15] = 1;
#else
#ifdef BTTRANSFORM_TRANSPOSE
	m[0] = t->getBasis().getRow(0).getX();
	m[3] = t->getBasis().getRow(0).getY();
	m[6] = t->getBasis().getRow(0).getZ();
	m[1] = t->getBasis().getRow(1).getX();
	m[4] = t->getBasis().getRow(1).getY();
	m[7] = t->getBasis().getRow(1).getZ();
	m[2] = t->getBasis().getRow(2).getX();
	m[5] = t->getBasis().getRow(2).getY();
	m[8] = t->getBasis().getRow(2).getZ();
#else
	m[0] = t->getBasis().getRow(0).getX();
	m[1] = t->getBasis().getRow(0).getY();
	m[2] = t->getBasis().getRow(0).getZ();
	m[3] = t->getBasis().getRow(1).getX();
	m[4] = t->getBasis().getRow(1).getY();
	m[5] = t->getBasis().getRow(1).getZ();
	m[6] = t->getBasis().getRow(2).getX();
	m[7] = t->getBasis().getRow(2).getY();
	m[8] = t->getBasis().getRow(2).getZ();
#endif
	m[9] = t->getOrigin().getX();
	m[10] = t->getOrigin().getY();
	m[11] = t->getOrigin().getZ();
#endif
}

inline void MatrixTobtTransform(const btScalar* m, btTransform* t)
{
#ifdef BTTRANSFORM_TO4X4
#ifdef BTTRANSFORM_TRANSPOSE
	t->getBasis().setValue(m[0],m[4],m[8],m[1],m[5],m[9],m[2],m[6],m[10]);
#else
	t->getBasis().setValue(m[0],m[1],m[2],m[4],m[5],m[6],m[8],m[9],m[10]);
#endif
	t->getOrigin().setX(m[12]);
	t->getOrigin().setY(m[13]);
	t->getOrigin().setZ(m[14]);
#else
#ifdef BTTRANSFORM_TRANSPOSE
	t->getBasis().setValue(m[0],m[3],m[6],m[1],m[4],m[7],m[2],m[5],m[8]);
#else
	t->getBasis().setValue(m[0],m[1],m[2],m[3],m[4],m[5],m[6],m[7],m[8]);
#endif
	t->getOrigin().setX(m[9]);
	t->getOrigin().setY(m[10]);
	t->getOrigin().setZ(m[11]);
#endif
}


inline void btMatrix3x3ToMatrix(const btMatrix3x3* t, btScalar* m)
{
#ifdef BTTRANSFORM_TO4X4
#ifdef BTTRANSFORM_TRANSPOSE
	m[0] = t->getRow(0).getX();
	m[4] = t->getRow(0).getY();
	m[8] = t->getRow(0).getZ();
	m[1] = t->getRow(1).getX();
	m[5] = t->getRow(1).getY();
	m[9] = t->getRow(1).getZ();
	m[2] = t->getRow(2).getX();
	m[6] = t->getRow(2).getY();
	m[10] = t->getRow(2).getZ();
#else
	m[0] = t->getRow(0).getX();
	m[1] = t->getRow(0).getY();
	m[2] = t->getRow(0).getZ();
	m[4] = t->getRow(1).getX();
	m[5] = t->getRow(1).getY();
	m[6] = t->getRow(1).getZ();
	m[8] = t->getRow(2).getX();
	m[9] = t->getRow(2).getY();
	m[10] = t->getRow(2).getZ();
#endif
	//m[12] = 0;
	//m[13] = 0;
	//m[14] = 0;
	m[15] = 1;
#else
#ifdef BTTRANSFORM_TRANSPOSE
	m[0] = t->getRow(0).getX();
	m[3] = t->getRow(0).getY();
	m[6] = t->getRow(0).getZ();
	m[1] = t->getRow(1).getX();
	m[4] = t->getRow(1).getY();
	m[7] = t->getRow(1).getZ();
	m[2] = t->getRow(2).getX();
	m[5] = t->getRow(2).getY();
	m[8] = t->getRow(2).getZ();
#else
	m[0] = t->getRow(0).getX();
	m[1] = t->getRow(0).getY();
	m[2] = t->getRow(0).getZ();
	m[3] = t->getRow(1).getX();
	m[4] = t->getRow(1).getY();
	m[5] = t->getRow(1).getZ();
	m[6] = t->getRow(2).getX();
	m[7] = t->getRow(2).getY();
	m[8] = t->getRow(2).getZ();
#endif
	//m[9] = 0;
	//m[10] = 0;
	//m[11] = 0;
#endif
}


// SSE requires math structs to be aligned to 16-byte boundaries.
// Alignment cannot be guaranteed in .NET, so aligned temporary intermediate variables
// must be used to exchange vectors and transforms with Bullet (if SSE is enabled).
#define TEMP(var) var ## Temp
#if defined(BT_USE_SIMD_VECTOR3) && defined(BT_USE_SSE_IN_API) && defined(BT_USE_SSE)
#define VECTOR3_DEF(vec) ATTRIBUTE_ALIGNED16(btVector3) TEMP(vec)
#define VECTOR3_IN(invec, vec) Vector3TobtVector3(invec, vec)
#define VECTOR3_CONV(vec) VECTOR3_DEF(vec); VECTOR3_IN(vec, &TEMP(vec))
#define VECTOR3_USE(vec) TEMP(vec)
#define VECTOR3_OUT(vec, outvec) btVector3ToVector3(vec, outvec)
#define VECTOR3_DEF_OUT(vec) VECTOR3_OUT(&TEMP(vec), vec)
#define TRANSFORM_DEF(tr) ATTRIBUTE_ALIGNED16(btTransform) TEMP(tr)
#define MATRIX3X3_DEF(tr) ATTRIBUTE_ALIGNED16(btMatrix3x3) TEMP(tr)
#else
#define VECTOR3_DEF(vec)
#define VECTOR3_IN(invec, vec) *vec = *(btVector3*)invec
#define VECTOR3_CONV(vec)
#define VECTOR3_USE(vec) *(btVector3*)vec
#define VECTOR3_OUT(vec, outvec) *(btVector3*)outvec = *vec
#define VECTOR3_DEF_OUT
#define TRANSFORM_DEF(tr) btTransform TEMP(tr)
#define MATRIX3X3_DEF(tr) btMatrix3x3 TEMP(tr)
#endif
#define TRANSFORM_CONV(tr) TRANSFORM_DEF(tr); MatrixTobtTransform(tr, &TEMP(tr))
#define TRANSFORM_USE(tr) TEMP(tr)
#define TRANSFORM_DEF_OUT(tr) btTransformToMatrix(&TEMP(tr), tr)
#define MATRIX3X3_DEF_OUT(tr) btMatrix3x3ToMatrix(&TEMP(tr), tr)
