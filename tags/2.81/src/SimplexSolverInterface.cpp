#include "StdAfx.h"

#ifndef DISABLE_UNCOMMON
#ifndef NO_VIRTUAL_INTERFACE

#include "SimplexSolverInterface.h"

SimplexSolverInterface::SimplexSolverInterface(btSimplexSolverInterface* simplexSolver)
{
	_simplexSolver = simplexSolver;
}

SimplexSolverInterface::~SimplexSolverInterface()
{
	this->!SimplexSolverInterface();
}

SimplexSolverInterface::!SimplexSolverInterface()
{
	delete _simplexSolver;
	_simplexSolver = NULL;
}

bool SimplexSolverInterface::IsDisposed::get()
{
	return (_simplexSolver == NULL);
}

btSimplexSolverInterface* SimplexSolverInterface::UnmanagedPointer::get()
{
	return _simplexSolver;
}
void SimplexSolverInterface::UnmanagedPointer::set(btSimplexSolverInterface* value)
{
	_simplexSolver = value;
}

#endif
#endif