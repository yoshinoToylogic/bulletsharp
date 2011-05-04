#include "StdAfx.h"

#ifndef DISABLE_SOFTBODY

#include "AlignedObjectArray.h"
#include "CollisionObject.h"
#include "SoftBody.h"
#include "SoftBodySolver.h"
#include "SoftBodySolverVertexBuffer.h"

SoftBody::SoftBodySolver::SoftBodySolver(btSoftBodySolver* solver)
{
	_solver = solver;
}

void SoftBody::SoftBodySolver::CopyBackToSoftBodies()
{
	_solver->copyBackToSoftBodies();
}

void SoftBody::SoftBodySolver::Optimize(AlignedSoftBodyArray^ softBodies, bool forceUpdate)
{
	_solver->optimize(*softBodies->UnmanagedPointer, forceUpdate);
}

void SoftBody::SoftBodySolver::Optimize(AlignedSoftBodyArray^ softBodies)
{
	_solver->optimize(*softBodies->UnmanagedPointer);
}

void SoftBody::SoftBodySolver::ProcessCollision(SoftBody^ softBody, CollisionObject^ collisionObject)
{
	_solver->processCollision(softBody->UnmanagedPointer, collisionObject->UnmanagedPointer);
}

void SoftBody::SoftBodySolver::ProcessCollision(SoftBody^ softBody, SoftBody^ otherSoftBody)
{
	_solver->processCollision(softBody->UnmanagedPointer, otherSoftBody->UnmanagedPointer);
}

int SoftBody::SoftBodySolver::NumberOfPositionIterations::get()
{
	return _solver->getNumberOfPositionIterations();
}
void SoftBody::SoftBodySolver::NumberOfPositionIterations::set(int value)
{
	_solver->setNumberOfPositionIterations(value);
}

int SoftBody::SoftBodySolver::NumberOfVelocityIterations::get()
{
	return _solver->getNumberOfVelocityIterations();
}
void SoftBody::SoftBodySolver::NumberOfVelocityIterations::set(int value)
{
	_solver->setNumberOfVelocityIterations(value);
}

SolverType SoftBody::SoftBodySolver::SolverType::get()
{
	return (BulletSharp::SolverType)_solver->getSolverType();
}

float SoftBody::SoftBodySolver::TimeScale::get()
{
	return _solver->getTimeScale();
}

btSoftBodySolver* SoftBody::SoftBodySolver::UnmanagedPointer::get()
{
	return _solver;
}
void SoftBody::SoftBodySolver::UnmanagedPointer::set(btSoftBodySolver* value)
{
	_solver = value;
}


SoftBody::SoftBodySolverOutput::SoftBodySolverOutput(btSoftBodySolverOutput* solverOutput)
{
	_solverOutput = solverOutput;
}

void SoftBody::SoftBodySolverOutput::CopySoftBodyToVertexBuffer(SoftBody^ softBody, VertexBufferDescriptor^ vertexBuffer)
{
	_solverOutput->copySoftBodyToVertexBuffer(softBody->UnmanagedPointer, vertexBuffer->UnmanagedPointer);
}

btSoftBodySolverOutput* SoftBody::SoftBodySolverOutput::UnmanagedPointer::get()
{
	return _solverOutput;
}
void SoftBody::SoftBodySolverOutput::UnmanagedPointer::set(btSoftBodySolverOutput* value)
{
	_solverOutput = value;
}

#endif