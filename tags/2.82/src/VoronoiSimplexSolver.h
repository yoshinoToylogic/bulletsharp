#pragma once

#include "SimplexSolverInterface.h"

namespace BulletSharp
{
	public ref class SubSimplexClosestResult
	{
	internal:
		btSubSimplexClosestResult* _native;

		SubSimplexClosestResult(btSubSimplexClosestResult* result);

	public:
		bool IsValid();
		void Reset();
		void SetBarycentricCoordinates(btScalar a, btScalar b, btScalar c, btScalar	d);
		void SetBarycentricCoordinates(btScalar a, btScalar b, btScalar c);
		void SetBarycentricCoordinates(btScalar a, btScalar b);
		void SetBarycentricCoordinates(btScalar a);
		void SetBarycentricCoordinates();
	};

#ifdef NO_VIRTUAL_INTERFACE
	public ref class VoronoiSimplexSolver
#else
	public ref class VoronoiSimplexSolver : SimplexSolverInterface
#endif
	{
	internal:
		VoronoiSimplexSolver(btVoronoiSimplexSolver* solver);

#ifdef NO_VIRTUAL_INTERFACE
	internal:
		btVoronoiSimplexSolver* _native;

	public:
		!VoronoiSimplexSolver();
	protected:
		~VoronoiSimplexSolver();

	public:
		property bool IsDisposed
		{
			virtual bool get();
		}
#endif
	public:
		VoronoiSimplexSolver();

		void AddVertex(Vector3 w, Vector3 p, Vector3 q);
		void BackupClosest(Vector3 v);
		bool Closest(Vector3 v);
		bool ClosestPtPointTetrahedron(Vector3 p,
			Vector3 a, Vector3 b, Vector3 c, Vector3 d,
			[Out] SubSimplexClosestResult^% finalResult);
		bool ClosestPtPointTriangle(Vector3 p,
			Vector3 a, Vector3 b, Vector3 c,
			[Out] SubSimplexClosestResult^% result);
		void ComputePoints(Vector3 p1, Vector3 p2);
		bool EmptySimplex();
		bool FullSimplex();
		int GetSimplex([Out] array<Vector3>^% pBuf,
			[Out] array<Vector3>^% qBuf,
			[Out] array<Vector3>^% yBuf);
		bool InSimplex(Vector3 w);
		btScalar MaxVertex();
		int PointOutsideOfPlane(Vector3 p,
			Vector3 a, Vector3 b, Vector3 c, Vector3 d);
		//void ReduceVertices(UsageBitfield^ UsedVerts);
		void RemoveVertex(int index);
		void Reset();
		bool UpdateClosestVectorAndPoints();

		property int NumVertices
		{
			int get();
		}
	};
};
