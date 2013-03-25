#include "collections.h"

unsigned int uint_array_at(const unsigned int* a, int n)
{
	return a[n];
}

void btVector3_array_at(const btVector3* a, int n, btScalar* value)
{
	VECTOR3_OUT(&a[n], value);
}
