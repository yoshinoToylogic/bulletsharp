#include "StdAfx.h"

#include "VectorMathAos.h"

using namespace BulletSharp::VectorMath;

Aos::Matrix3::Matrix3(VMVector3^ col0, VMVector3^ col1, VMVector3^ col2)
{
	_matrix = new Vectormath::Aos::Matrix3(*col0->UnmanagedPointer, *col1->UnmanagedPointer, *col2->UnmanagedPointer);
}

Vectormath::Aos::Vector3* Matrix3_MultiplyVector3(Vectormath::Aos::Matrix3* matrix, Vectormath::Aos::Vector3* vector)
{
	Vectormath::Aos::Vector3* result = new Vectormath::Aos::Vector3();
	*result = (*matrix) * (*vector);
	return result;
}
Aos::VMVector3^ Aos::Matrix3::operator *(Matrix3^ matrix, Aos::VMVector3^ vec)
{
	return gcnew Aos::VMVector3(Matrix3_MultiplyVector3(matrix->UnmanagedPointer, vec->UnmanagedPointer));
}

Vectormath::Aos::Matrix3* Aos::Matrix3::UnmanagedPointer::get()
{
	return _matrix;
}


Aos::VMVector3::VMVector3(Vectormath::Aos::Vector3* vector)
{
	_vector = vector;
}

Aos::VMVector3::VMVector3()
{
	_vector = new Vectormath::Aos::Vector3();
}

Aos::VMVector3::VMVector3(float x, float y, float z)
{
	_vector = new Vectormath::Aos::Vector3(x, y, z);
}

float VMVector3_GetX(Vectormath::Aos::Vector3* vector)
{
	return vector->getX();
}
float Aos::VMVector3::X::get()
{
	return VMVector3_GetX(_vector);
}

float VMVector3_GetY(Vectormath::Aos::Vector3* vector)
{
	return vector->getY();
}
float Aos::VMVector3::Y::get()
{
	return VMVector3_GetY(_vector);
}

float VMVector3_GetZ(Vectormath::Aos::Vector3* vector)
{
	return vector->getZ();
}
float Aos::VMVector3::Z::get()
{
	return VMVector3_GetZ(_vector);
}

Vectormath::Aos::Vector3* Aos::VMVector3::UnmanagedPointer::get()
{
	return _vector;
}
