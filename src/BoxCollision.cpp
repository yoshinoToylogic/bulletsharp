#include "StdAfx.h"

#ifndef DISABLE_GIMPACT

#include "BoxCollision.h"

Aabb::Aabb(btAABB* native, bool preventDelete)
{
	_native = native;
	_preventDelete = preventDelete;
}

Aabb::~Aabb()
{
	this->!Aabb();
}

Aabb::!Aabb()
{
	if (!_preventDelete)
	{
		delete _native;
	}
	_native = NULL;
}

Aabb::Aabb()
{
	_native = new btAABB();
}

Aabb::Aabb(Vector3 V1, Vector3 V2, Vector3 V3)
{
	VECTOR3_DEF(V1);
	VECTOR3_DEF(V2);
	VECTOR3_DEF(V3);
	_native = new btAABB(VECTOR3_USE(V1), VECTOR3_USE(V2), VECTOR3_USE(V3));
	VECTOR3_DEL(V1);
	VECTOR3_DEL(V2);
	VECTOR3_DEL(V3);
}

Aabb::Aabb(Vector3 V1, Vector3 V2, Vector3 V3, btScalar margin)
{
	VECTOR3_DEF(V1);
	VECTOR3_DEF(V2);
	VECTOR3_DEF(V3);
	_native = new btAABB(VECTOR3_USE(V1), VECTOR3_USE(V2), VECTOR3_USE(V3), margin);
	VECTOR3_DEL(V1);
	VECTOR3_DEL(V2);
	VECTOR3_DEL(V3);
}

Aabb::Aabb(Aabb^ other)
{
	_native = new btAABB(*other->_native);
}

Aabb::Aabb(Aabb^ other, btScalar margin)
{
	_native = new btAABB(*other->_native, margin);
}

void Aabb::AppyTransform(Matrix trans)
{
	TRANSFORM_CONV(trans);
	_native->appy_transform(TRANSFORM_USE(trans));
	TRANSFORM_DEL(trans);
}
/*
void Aabb::AppyTransformTransCache(BT_BOX_BOX_TRANSFORM_CACHE^ trans)
{
	_native->appy_transform_trans_cache(*trans->_native);
}

bool Aabb::CollidePlane(Vector4^ plane)
{
	return _native->collide_plane(*(btVector4*)plane->_native);
}
*/
bool Aabb::CollideRay(Vector3 vorigin, Vector3 vdir)
{
	VECTOR3_DEF(vorigin);
	VECTOR3_DEF(vdir);
	return _native->collide_ray(VECTOR3_USE(vorigin), VECTOR3_USE(vdir));
	VECTOR3_DEL(vorigin);
	VECTOR3_DEL(vdir);
}
/*
bool Aabb::CollideTriangleExact(Vector3 p1, Vector3 p2, Vector3 p3, Vector4 triangle_plane)
{
	VECTOR3_DEF(p1);
	VECTOR3_DEF(p2);
	VECTOR3_DEF(p3);
	return _native->collide_triangle_exact(VECTOR3_USE(p1), VECTOR3_USE(p2), VECTOR3_USE(p3),
		*(btVector4*)triangle_plane->_native);
	VECTOR3_DEL(p1);
	VECTOR3_DEL(p2);
	VECTOR3_DEL(p3);
}
*/
void Aabb::CopyWithMargin(Aabb^ other, btScalar margin)
{
	_native->copy_with_margin(*other->_native, margin);
}

void Aabb::FindIntersection(Aabb^ other, Aabb^ intersection)
{
	_native->find_intersection(*other->_native, *intersection->_native);
}

void Aabb::GetCenterExtend(Vector3 center, Vector3 extend)
{
	VECTOR3_DEF(center);
	VECTOR3_DEF(extend);
	_native->get_center_extend(VECTOR3_USE(center), VECTOR3_USE(extend));
	VECTOR3_DEL(center);
	VECTOR3_DEL(extend);
}

bool Aabb::HasCollision(Aabb^ other)
{
	return _native->has_collision(*other->_native);
}

void Aabb::IncrementMargin(btScalar margin)
{
	_native->increment_margin(margin);
}

void Aabb::Invalidate()
{
	_native->invalidate();
}

void Aabb::Merge(Aabb^ box)
{
	_native->merge(*box->_native);
}
/*
bool Aabb::OverlappingTransCache(Aabb^ box, BT_BOX_BOX_TRANSFORM_CACHE^ transcache,
	bool fulltest)
{
	return _native->overlapping_trans_cache(*box->_native, *transcache->_native, fulltest);
}
*/
bool Aabb::OverlappingTransConservative(Aabb^ box, Matrix trans1_to_0)
{
	TRANSFORM_CONV(trans1_to_0);
	return _native->overlapping_trans_conservative(*box->_native, TRANSFORM_USE(trans1_to_0));
	TRANSFORM_DEL(trans1_to_0);
}
/*
bool Aabb::OverlappingTransConservative2(Aabb^ box, BT_BOX_BOX_TRANSFORM_CACHE^ trans1_to_0)
{
	return _native->overlapping_trans_conservative2(*box->_native, *trans1_to_0->_native);
}

eBT_PLANE_INTERSECTION_TYPE Aabb::PlaneClassify(Vector4^ plane)
{
	return _native->plane_classify(*(btVector4*)plane->_native);
}
*/
void Aabb::ProjectionInterval(Vector3 direction, [Out] btScalar% vmin, [Out] btScalar% vmax)
{
	VECTOR3_DEF(direction);
	btScalar vminTemp, vmaxTemp;
	_native->projection_interval(VECTOR3_USE(direction), vminTemp, vmaxTemp);
	VECTOR3_DEL(direction);
	vmin = vminTemp;
	vmax = vmaxTemp;
}

Vector3 Aabb::Max::get()
{
	return Math::BtVector3ToVector3(&_native->m_max);
}
void Aabb::Max::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->m_max);
}

Vector3 Aabb::Min::get()
{
	return Math::BtVector3ToVector3(&_native->m_min);
}
void Aabb::Min::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_native->m_min);
}

#endif
