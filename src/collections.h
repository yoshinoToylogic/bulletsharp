#include "main.h"

extern "C"
{
	EXPORT unsigned int uint_array_at(const unsigned int* a, int n);
	EXPORT void btVector3_array_at(const btVector3* a, int n, btScalar* value);
}
