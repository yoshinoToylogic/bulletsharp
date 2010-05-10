#include "StdAfx.h"

#ifndef DISABLE_SOFTBODY

#pragma managed(push, off)
#include <BulletSoftBody/btSoftBodyHelpers.h>
#pragma managed(pop)

#include <stdio.h>

#include "SoftBody.h"
#include "SoftBodyHelpers.h"
#include "StringConv.h"

float SoftBodyHelpers::CalculateUV(int resx, int resy, int ix, int iy, int id)
{
	return btSoftBodyHelpers::CalculateUV(resx, resy, ix, iy, id);
}

SoftBody^ SoftBodyHelpers::CreateEllipsoid(SoftBodyWorldInfo^ worldInfo,
	Vector3 center, Vector3 radius, int res)
{
	btVector3* centerTemp = Math::Vector3ToBtVector3(center);
	btVector3* radiusTemp = Math::Vector3ToBtVector3(radius);

	SoftBody^ body = gcnew SoftBody(btSoftBodyHelpers::CreateEllipsoid(*worldInfo->UnmanagedPointer,
		*centerTemp, *radiusTemp, res));

	delete centerTemp;
	delete radiusTemp;

	return body;
}

SoftBody^ SoftBodyHelpers::CreateFromConvexHull(BulletSharp::SoftBodyWorldInfo ^worldInfo,
	array<Vector3>^ vertices, bool randomizeConstraints)
{
	btVector3* btVertices = new btVector3[vertices->Length];
	for(int i=0; i<vertices->Length; i++)
		Math::Vector3ToBtVector3(vertices[i], &btVertices[i]);

	SoftBody^ body = gcnew SoftBody(btSoftBodyHelpers::CreateFromConvexHull(*worldInfo->UnmanagedPointer,
		btVertices, vertices->Length, randomizeConstraints));

	delete[] btVertices;
	return body;
}

SoftBody^ SoftBodyHelpers::CreateFromConvexHull(BulletSharp::SoftBodyWorldInfo ^worldInfo,
	array<Vector3>^ vertices)
{
	btVector3* btVertices = new btVector3[vertices->Length];
	for(int i=0; i<vertices->Length; i++)
		Math::Vector3ToBtVector3(vertices[i], &btVertices[i]);

	SoftBody^ body = gcnew SoftBody(btSoftBodyHelpers::CreateFromConvexHull(*worldInfo->UnmanagedPointer,
		btVertices, vertices->Length));
	
	delete[] btVertices;
	return body;
}

SoftBody^ SoftBodyHelpers::CreateFromTetGenData(SoftBodyWorldInfo^ worldInfo, String^ ele,
	String^ face, String^ node, bool bfacelinks, bool btetralinks, bool bfacesfromtetras)
{
	const char* eleTemp = StringConv::ManagedToUnmanaged(ele);
	const char* faceTemp = StringConv::ManagedToUnmanaged(face);
	const char* nodeTemp = StringConv::ManagedToUnmanaged(node);

	return gcnew SoftBody(btSoftBodyHelpers::CreateFromTetGenData(*worldInfo->UnmanagedPointer,
		eleTemp, faceTemp, nodeTemp, bfacelinks, btetralinks, bfacesfromtetras)
	);

	StringConv::FreeUnmanagedString(eleTemp);
	StringConv::FreeUnmanagedString(faceTemp);
	StringConv::FreeUnmanagedString(nodeTemp);
}

SoftBody^ SoftBodyHelpers::CreateFromTetGenFile(SoftBodyWorldInfo^ worldInfo, String^ ele,
	String^ face, String^ node, bool bfacelinks, bool btetralinks, bool bfacesfromtetras)
{
	FILE* f_read;
	long fileSize;
	char* elementStr;
	char* faceStr;
	char* nodeStr;

	// Read elements file
	if (ele != nullptr)
	{
		const char* elementFileStr = StringConv::ManagedToUnmanaged(ele);
		errno_t ret = fopen_s(&f_read, elementFileStr, "rb");
		StringConv::FreeUnmanagedString(elementFileStr);
		if (ret != 0)
			return nullptr;
		fseek(f_read, 0, SEEK_END);
		fileSize = ftell(f_read);
		elementStr = (char*) malloc(fileSize+1);
		if (!elementStr)
		{
			fclose(f_read);
			return nullptr;
		}
		fseek(f_read, 0, SEEK_SET);
		fread(elementStr, 1, fileSize, f_read);
		elementStr[fileSize] = 0;
		fclose(f_read);
	}
	else
	{
		elementStr = 0;
	}

	if (face != nullptr)
	{
		// Read faces file
		const char* faceFileStr = StringConv::ManagedToUnmanaged(face);
		errno_t ret = fopen_s(&f_read, faceFileStr, "rb");
		StringConv::FreeUnmanagedString(faceFileStr);
		if (ret != 0)
		{
			if (elementStr)
				free(elementStr);
			return nullptr;
		}
		fseek(f_read, 0, SEEK_END);
		fileSize = ftell(f_read);
		faceStr = (char*) malloc(fileSize+1);
		if (!faceStr)
		{
			fclose(f_read);
			if (elementStr)
				free(elementStr);
			return nullptr;
		}
		fseek(f_read, 0, SEEK_SET);
		fread(faceStr, 1, fileSize, f_read);
		faceStr[fileSize] = 0;
		fclose(f_read);
	}
	else
	{
		faceStr = 0;
	}

	// Read nodes file
	const char* nodeFileStr = StringConv::ManagedToUnmanaged(node);
	errno_t ret = fopen_s(&f_read, nodeFileStr, "rb");
	StringConv::FreeUnmanagedString(nodeFileStr);
	if (ret != 0)
	{
		if (elementStr)
			free(elementStr);
		if (faceStr)
			free(faceStr);
		return nullptr;
	}
	fseek(f_read, 0, SEEK_END);
	fileSize = ftell(f_read);
	nodeStr = (char*) malloc(fileSize+1);
	if (!nodeStr)
	{
		fclose(f_read);
		if (elementStr)
			free(elementStr);
		if (faceStr)
			free(faceStr);
		return nullptr;
	}
	fseek(f_read, 0, SEEK_SET);
	fread(nodeStr, 1, fileSize, f_read);
	nodeStr[fileSize] = 0;
	fclose(f_read);

	SoftBody^ body = gcnew SoftBody(btSoftBodyHelpers::CreateFromTetGenData(
		*worldInfo->UnmanagedPointer, elementStr, faceStr, nodeStr,
		bfacelinks, btetralinks, bfacesfromtetras)
	);

	free(elementStr);
	free(faceStr);
	free(nodeStr);

	return body;
}

SoftBody^ SoftBodyHelpers::CreatePatch(SoftBodyWorldInfo^ worldInfo,
	Vector3 corner00, Vector3 corner10, Vector3 corner01, Vector3 corner11,
	int resx, int resy, int fixeds, bool gendiags)
{
	btVector3* corner00Temp = Math::Vector3ToBtVector3(corner00);
	btVector3* corner10Temp = Math::Vector3ToBtVector3(corner10);
	btVector3* corner01Temp = Math::Vector3ToBtVector3(corner01);
	btVector3* corner11Temp = Math::Vector3ToBtVector3(corner11);

	SoftBody^ body = gcnew SoftBody(btSoftBodyHelpers::CreatePatch(*worldInfo->UnmanagedPointer,
		*corner00Temp, *corner10Temp, *corner01Temp, *corner11Temp, resx, resy, fixeds, gendiags));

	delete corner00Temp;
	delete corner10Temp;
	delete corner01Temp;
	delete corner11Temp;

	return body;
}

SoftBody^ SoftBodyHelpers::CreateRope(SoftBodyWorldInfo^ worldInfo,
	Vector3 from, Vector3 to, int res, int fixeds)
{
	btVector3* fromTemp = Math::Vector3ToBtVector3(from);
	btVector3* toTemp = Math::Vector3ToBtVector3(to);

	SoftBody^ body = gcnew SoftBody(btSoftBodyHelpers::CreateRope(
		*worldInfo->UnmanagedPointer, *fromTemp, *toTemp, res, fixeds));

	delete fromTemp;
	delete toTemp;

	return body;
}

#endif