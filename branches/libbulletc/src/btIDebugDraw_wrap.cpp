#include "btIDebugDraw_wrap.h"

btIDebugDrawWrapper::btIDebugDrawWrapper(void* debugDrawGCHandle, pDrawBox drawBoxCallback,
	pDrawCapsule drawCapsuleCallback, pDrawLine drawLineCallback, pDrawPlane drawPlaneCallback,
	pDrawSphere drawSphereCallback, pDrawTransform drawTransformCallback, pGetDebugMode getDebugModeCallback, pSimpleCallback cb)
{
	_debugDrawGCHandle = debugDrawGCHandle;
	_drawBoxCallback = drawBoxCallback;
	_drawCapsuleCallback = drawCapsuleCallback;
	_drawLineCallback = drawLineCallback;
	_drawPlaneCallback = drawPlaneCallback;
	_drawSphereCallback = drawSphereCallback;
	_drawTransformCallback = drawTransformCallback;
	_getDebugModeCallback = getDebugModeCallback;
	_cb = cb;
}

void btIDebugDrawWrapper::draw3dText(const btVector3& location, const char* textString)
{
	_cb(0);
	//_debugDraw->Draw3dText(Math::BtVector3ToVector3(&location), StringConv::UnmanagedToManaged(textString));
}

void btIDebugDrawWrapper::drawAabb(const btVector3& from, const btVector3& to, const btVector3& color)
{
	_cb(1);
	//_debugDraw->DrawAabb(
		//Math::BtVector3ToVector3(&from), Math::BtVector3ToVector3(&to), BtVectorToBtColor(color));
}

void btIDebugDrawWrapper::drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
	btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
	const btVector3& color, bool drawSect, btScalar stepDegrees)
{
	_cb(2);
	//_debugDraw->DrawArc(Math::BtVector3ToVector3(&center), Math::BtVector3ToVector3(&normal), Math::BtVector3ToVector3(&axis),
		//radiusA, radiusB, minAngle, maxAngle, BtVectorToBtColor(color), drawSect, stepDegrees);
}

void btIDebugDrawWrapper::drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
	btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
	const btVector3& color, bool drawSect)
{
	_cb(3);
	//_debugDraw->DrawArc(Math::BtVector3ToVector3(&center), Math::BtVector3ToVector3(&normal), Math::BtVector3ToVector3(&axis),
		//radiusA, radiusB, minAngle, maxAngle, BtVectorToBtColor(color), drawSect);
}

void btIDebugDrawWrapper::drawBox(const btVector3& bbMin, const btVector3& bbMax, const btTransform& trans, const btVector3& color)
{
	ATTRIBUTE_ALIGNED16(btScalar) transTemp[16];
	btTransformToMatrix(&trans, transTemp);
	_drawBoxCallback(bbMin, bbMax, transTemp, color);
}

void btIDebugDrawWrapper::drawBox(const btVector3& bbMin, const btVector3& bbMax, const btVector3& color)
{
	_cb(4);
	//_debugDraw->DrawBox(
		//Math::BtVector3ToVector3(&bbMin), Math::BtVector3ToVector3(&bbMax),	BtVectorToBtColor(color));
}

void btIDebugDrawWrapper::drawCapsule(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color)
{
	ATTRIBUTE_ALIGNED16(btScalar) transformTemp[16];
	btTransformToMatrix(&transform, transformTemp);
	_drawCapsuleCallback(radius, halfHeight, upAxis, transformTemp, color);
}

void btIDebugDrawWrapper::drawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform, const btVector3& color)
{
	_cb(5);
	//_debugDraw->DrawCone(radius, height, upAxis, Math::BtTransformToMatrix(&transform), BtVectorToBtColor(color));
}

void btIDebugDrawWrapper::drawContactPoint(const btVector3& PointOnB, const btVector3& normalOnB, btScalar distance, int lifeTime, const btVector3& color)
{
	_cb(6);
	//_debugDraw->DrawContactPoint(Math::BtVector3ToVector3(&PointOnB), Math::BtVector3ToVector3(&normalOnB),
		//distance, lifeTime, BtVectorToBtColor(color));
}

void btIDebugDrawWrapper::drawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color)
{
	_cb(7);
	//_debugDraw->DrawCylinder(radius, halfHeight, upAxis, Math::BtTransformToMatrix(&transform), BtVectorToBtColor(color));
}

void btIDebugDrawWrapper::drawLine(const btVector3& from, const btVector3& to, const btVector3& color)
{
	_drawLineCallback(from, to, color);
}

void btIDebugDrawWrapper::drawLine(const btVector3& from, const btVector3& to, const btVector3& fromColor, const btVector3& toColor)
{
	_cb(8);
	//_debugDraw->DrawLine(
		//Math::BtVector3ToVector3(&from), Math::BtVector3ToVector3(&to), BtVectorToBtColor(fromColor), BtVectorToBtColor(toColor));
}

void btIDebugDrawWrapper::drawPlane(const btVector3& planeNormal, btScalar planeConst, const btTransform& transform, const btVector3& color)
{
	ATTRIBUTE_ALIGNED16(btScalar) transformTemp[16];
	btTransformToMatrix(&transform, transformTemp);
	_drawPlaneCallback(planeNormal, planeConst, transformTemp, color);
}

void btIDebugDrawWrapper::drawSphere(const btVector3& p, btScalar radius, const btVector3& color)
{
	_cb(10);
	//_debugDraw->DrawSphere(Math::BtVector3ToVector3(&p), radius, BtVectorToBtColor(color));
}

void btIDebugDrawWrapper::drawSphere(btScalar radius, const btTransform& transform, const btVector3& color)
{
	ATTRIBUTE_ALIGNED16(btScalar) transformTemp[16];
	btTransformToMatrix(&transform, transformTemp);
	_drawSphereCallback(radius, transformTemp, color);
}

void btIDebugDrawWrapper::drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
	btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color, btScalar stepDegrees)
{
	_cb(11);
	//_debugDraw->DrawSpherePatch(Math::BtVector3ToVector3(&center), Math::BtVector3ToVector3(&up), Math::BtVector3ToVector3(&axis),
		//radius, minTh, maxTh, minPs, maxPs, BtVectorToBtColor(color), stepDegrees);
}

void btIDebugDrawWrapper::drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
	btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color)
{
	_cb(12);
	//_debugDraw->DrawSpherePatch(Math::BtVector3ToVector3(&center), Math::BtVector3ToVector3(&up), Math::BtVector3ToVector3(&axis),
		//radius, minTh, maxTh, minPs, maxPs, BtVectorToBtColor(color));
}

void btIDebugDrawWrapper::drawTransform(const btTransform& transform, btScalar orthoLen)
{
	ATTRIBUTE_ALIGNED16(btScalar) transformTemp[16];
	btTransformToMatrix(&transform, transformTemp);
	_drawTransformCallback(transformTemp, orthoLen);
}

void btIDebugDrawWrapper::drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2, const btVector3& color, btScalar)
{
	_cb(13);
	//_debugDraw->DrawTriangle(Math::BtVector3ToVector3(&v0), Math::BtVector3ToVector3(&v1), Math::BtVector3ToVector3(&v2),
		//BtVectorToBtColor(color), 0);
}

void btIDebugDrawWrapper::drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
	const btVector3& n0, const btVector3& n1, const btVector3& n2, const btVector3& color, btScalar alpha)
{
	_cb(14);
	//_debugDraw->DrawTriangle(Math::BtVector3ToVector3(&v0), Math::BtVector3ToVector3(&v1), Math::BtVector3ToVector3(&v2),
		//Math::BtVector3ToVector3(&n0), Math::BtVector3ToVector3(&n1), Math::BtVector3ToVector3(&n2), BtVectorToBtColor(color), alpha);
}

void btIDebugDrawWrapper::baseDrawAabb(const btVector3& from, const btVector3& to, const btVector3& color)
{
	btIDebugDraw::drawAabb(from, to, color);
}

void btIDebugDrawWrapper::baseDrawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
	btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
	const btVector3& color, bool drawSect, btScalar stepDegrees)
{
	btIDebugDraw::drawArc(center, normal, axis, radiusA, radiusB, minAngle, maxAngle, color, drawSect, stepDegrees);
}

void btIDebugDrawWrapper::baseDrawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
	btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
	const btVector3& color, bool drawSect)
{
	btIDebugDraw::drawArc(center, normal, axis, radiusA, radiusB, minAngle, maxAngle, color, drawSect);
}

void btIDebugDrawWrapper::baseDrawBox(const btVector3& bbMin, const btVector3& bbMax, const btVector3& color)
{
	btIDebugDraw::drawBox(bbMin, bbMax, color);
}

void btIDebugDrawWrapper::baseDrawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform, const btVector3& color)
{
	btIDebugDraw::drawCone(radius, height, upAxis, transform, color);
}

void btIDebugDrawWrapper::baseDrawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color)
{
	btIDebugDraw::drawCylinder(radius, halfHeight, upAxis, transform, color);
}

void btIDebugDrawWrapper::baseDrawLine(const btVector3& from, const btVector3& to, const btVector3& fromColor, const btVector3& toColor)
{
	btIDebugDraw::drawLine(from, to, fromColor, toColor);
}

void btIDebugDrawWrapper::baseDrawSphere(const btVector3& p, btScalar radius, const btVector3& color)
{
	btIDebugDraw::drawSphere(p, radius, color);
}

void btIDebugDrawWrapper::baseDrawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
	btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color, btScalar stepDegrees)
{
	btIDebugDraw::drawSpherePatch(center, up, axis, radius, minTh, maxTh, minPs, maxPs, color, stepDegrees);
}

void btIDebugDrawWrapper::baseDrawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
	btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color)
{
	btIDebugDraw::drawSpherePatch(center, up, axis, radius, minTh, maxTh, minPs, maxPs, color);
}

void btIDebugDrawWrapper::baseDrawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2, const btVector3& color, btScalar)
{
	btIDebugDraw::drawTriangle(v0, v1, v2, color, 0);
}

void btIDebugDrawWrapper::baseDrawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
	const btVector3& n0, const btVector3& n1, const btVector3& n2, const btVector3& color, btScalar alpha)
{
	btIDebugDraw::drawTriangle(v0, v1, v2, n0, n1, n2, color, alpha);
}

void btIDebugDrawWrapper::reportErrorWarning(const char* warningString)
{
	//_debugDraw->ReportErrorWarning(StringConv::UnmanagedToManaged(warningString));
}

void btIDebugDrawWrapper::setDebugMode(int debugMode)
{
	_cb(15);
	//_debugDraw->DebugMode = (BulletSharp::DebugDrawModes)debugMode;
}
int	btIDebugDrawWrapper::getDebugMode() const
{
	return _getDebugModeCallback();
}

btIDebugDrawWrapper* btIDebugDrawWrapper_new(void* debugDrawGCHandle, pDrawBox drawBoxCallback, pDrawCapsule drawCapsule, pDrawLine drawLineCallback,
	pDrawPlane drawPlaneCallback, pDrawSphere drawSphereCallback, pDrawTransform drawTransformCallback, pGetDebugMode getDebugModeCallback, pSimpleCallback cb)
{
	return new btIDebugDrawWrapper(debugDrawGCHandle, drawBoxCallback, drawCapsule, drawLineCallback, drawPlaneCallback, drawSphereCallback, drawTransformCallback, getDebugModeCallback, cb);
}

void* btIDebugDrawWrapper_getDebugDrawGCHandle(btIDebugDrawWrapper* obj)
{
	return obj->_debugDrawGCHandle;
}

void btIDebugDraw_draw3dText(btIDebugDraw* obj, btScalar* location, char* textString)
{
	VECTOR3_CONV(location);
	obj->draw3dText(VECTOR3_USE(location), textString);
}

void btIDebugDraw_drawAabb(btIDebugDraw* obj, btScalar* from, btScalar* to, btScalar* color)
{
	VECTOR3_CONV(from);
	VECTOR3_CONV(to);
	VECTOR3_CONV(color);
	obj->drawAabb(VECTOR3_USE(from), VECTOR3_USE(to), VECTOR3_USE(color));
}

void btIDebugDraw_drawArc(btIDebugDraw* obj, btScalar* center, btScalar* normal, btScalar* axis, btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle, btScalar* color, bool drawSect, btScalar stepDegrees)
{
	VECTOR3_CONV(center);
	VECTOR3_CONV(normal);
	VECTOR3_CONV(axis);
	VECTOR3_CONV(color);
	obj->drawArc(VECTOR3_USE(center), VECTOR3_USE(normal), VECTOR3_USE(axis), radiusA, radiusB, minAngle, maxAngle, VECTOR3_USE(color), drawSect, stepDegrees);
}

void btIDebugDraw_drawArc2(btIDebugDraw* obj, btScalar* center, btScalar* normal, btScalar* axis, btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle, btScalar* color, bool drawSect)
{
	VECTOR3_CONV(center);
	VECTOR3_CONV(normal);
	VECTOR3_CONV(axis);
	VECTOR3_CONV(color);
	obj->drawArc(VECTOR3_USE(center), VECTOR3_USE(normal), VECTOR3_USE(axis), radiusA, radiusB, minAngle, maxAngle, VECTOR3_USE(color), drawSect);
}

void btIDebugDraw_drawBox(btIDebugDraw* obj, btScalar* bbMin, btScalar* bbMax, btScalar* trans, btScalar* color)
{
	VECTOR3_CONV(bbMin);
	VECTOR3_CONV(bbMax);
	TRANSFORM_CONV(trans);
	VECTOR3_CONV(color);
	obj->drawBox(VECTOR3_USE(bbMin), VECTOR3_USE(bbMax), TRANSFORM_USE(trans), VECTOR3_USE(color));
}

void btIDebugDraw_drawBox2(btIDebugDraw* obj, btScalar* bbMin, btScalar* bbMax, btScalar* color)
{
	VECTOR3_CONV(bbMin);
	VECTOR3_CONV(bbMax);
	VECTOR3_CONV(color);
	obj->drawBox(VECTOR3_USE(bbMin), VECTOR3_USE(bbMax), VECTOR3_USE(color));
}

void btIDebugDraw_drawCapsule(btIDebugDraw* obj, btScalar radius, btScalar halfHeight, int upAxis, btScalar* transform, btScalar* color)
{
	TRANSFORM_CONV(transform);
	VECTOR3_CONV(color);
	obj->drawCapsule(radius, halfHeight, upAxis, TRANSFORM_USE(transform), VECTOR3_USE(color));
}

void btIDebugDraw_drawCone(btIDebugDraw* obj, btScalar radius, btScalar height, int upAxis, btScalar* transform, btScalar* color)
{
	TRANSFORM_CONV(transform);
	VECTOR3_CONV(color);
	obj->drawCone(radius, height, upAxis, TRANSFORM_USE(transform), VECTOR3_USE(color));
}

void btIDebugDraw_drawContactPoint(btIDebugDraw* obj, btScalar* PointOnB, btScalar* normalOnB, btScalar distance, int lifeTime, btScalar* color)
{
	VECTOR3_CONV(PointOnB);
	VECTOR3_CONV(normalOnB);
	VECTOR3_CONV(color);
	obj->drawContactPoint(VECTOR3_USE(PointOnB), VECTOR3_USE(normalOnB), distance, lifeTime, VECTOR3_USE(color));
}

void btIDebugDraw_drawCylinder(btIDebugDraw* obj, btScalar radius, btScalar halfHeight, int upAxis, btScalar* transform, btScalar* color)
{
	TRANSFORM_CONV(transform);
	VECTOR3_CONV(color);
	obj->drawCylinder(radius, halfHeight, upAxis, TRANSFORM_USE(transform), VECTOR3_USE(color));
}

void btIDebugDraw_drawLine(btIDebugDraw* obj, btScalar* from, btScalar* to, btScalar* color)
{
	VECTOR3_CONV(from);
	VECTOR3_CONV(to);
	VECTOR3_CONV(color);
	obj->drawLine(VECTOR3_USE(from), VECTOR3_USE(to), VECTOR3_USE(color));
}

void btIDebugDraw_drawLine2(btIDebugDraw* obj, btScalar* from, btScalar* to, btScalar* fromColor, btScalar* toColor)
{
	VECTOR3_CONV(from);
	VECTOR3_CONV(to);
	VECTOR3_CONV(fromColor);
	VECTOR3_CONV(toColor);
	obj->drawLine(VECTOR3_USE(from), VECTOR3_USE(to), VECTOR3_USE(fromColor), VECTOR3_USE(toColor));
}

void btIDebugDraw_drawPlane(btIDebugDraw* obj, btScalar* planeNormal, btScalar planeConst, btScalar* transform, btScalar* color)
{
	VECTOR3_CONV(planeNormal);
	TRANSFORM_CONV(transform);
	VECTOR3_CONV(color);
	obj->drawPlane(VECTOR3_USE(planeNormal), planeConst, TRANSFORM_USE(transform), VECTOR3_USE(color));
}

void btIDebugDraw_drawSphere(btIDebugDraw* obj, btScalar* p, btScalar radius, btScalar* color)
{
	VECTOR3_CONV(p);
	VECTOR3_CONV(color);
	obj->drawSphere(VECTOR3_USE(p), radius, VECTOR3_USE(color));
}

void btIDebugDraw_drawSphere2(btIDebugDraw* obj, btScalar radius, btScalar* transform, btScalar* color)
{
	TRANSFORM_CONV(transform);
	VECTOR3_CONV(color);
	obj->drawSphere(radius, TRANSFORM_USE(transform), VECTOR3_USE(color));
}

void btIDebugDraw_drawSpherePatch(btIDebugDraw* obj, btScalar* center, btScalar* up, btScalar* axis, btScalar radius, btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, btScalar* color, btScalar stepDegrees)
{
	VECTOR3_CONV(center);
	VECTOR3_CONV(up);
	VECTOR3_CONV(axis);
	VECTOR3_CONV(color);
	obj->drawSpherePatch(VECTOR3_USE(center), VECTOR3_USE(up), VECTOR3_USE(axis), radius, minTh, maxTh, minPs, maxPs, VECTOR3_USE(color), stepDegrees);
}

void btIDebugDraw_drawSpherePatch2(btIDebugDraw* obj, btScalar* center, btScalar* up, btScalar* axis, btScalar radius, btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, btScalar* color)
{
	VECTOR3_CONV(center);
	VECTOR3_CONV(up);
	VECTOR3_CONV(axis);
	VECTOR3_CONV(color);
	obj->drawSpherePatch(VECTOR3_USE(center), VECTOR3_USE(up), VECTOR3_USE(axis), radius, minTh, maxTh, minPs, maxPs, VECTOR3_USE(color));
}

void btIDebugDraw_drawTransform(btIDebugDraw* obj, btScalar* transform, btScalar orthoLen)
{
	TRANSFORM_CONV(transform);
	obj->drawTransform(TRANSFORM_USE(transform), orthoLen);
}

void btIDebugDraw_drawTriangle(btIDebugDraw* obj, btScalar* v0, btScalar* v1, btScalar* v2, btScalar* color, btScalar __unnamed4)
{
	VECTOR3_CONV(v0);
	VECTOR3_CONV(v1);
	VECTOR3_CONV(v2);
	VECTOR3_CONV(color);
	obj->drawTriangle(VECTOR3_USE(v0), VECTOR3_USE(v1), VECTOR3_USE(v2), VECTOR3_USE(color), __unnamed4);
}

void btIDebugDraw_drawTriangle2(btIDebugDraw* obj, btScalar* v0, btScalar* v1, btScalar* v2, btScalar* __unnamed3, btScalar* __unnamed4, btScalar* __unnamed5, btScalar* color, btScalar alpha)
{
	VECTOR3_CONV(v0);
	VECTOR3_CONV(v1);
	VECTOR3_CONV(v2);
	VECTOR3_CONV(__unnamed3);
	VECTOR3_CONV(__unnamed4);
	VECTOR3_CONV(__unnamed5);
	VECTOR3_CONV(color);
	obj->drawTriangle(VECTOR3_USE(v0), VECTOR3_USE(v1), VECTOR3_USE(v2), VECTOR3_USE(__unnamed3), VECTOR3_USE(__unnamed4), VECTOR3_USE(__unnamed5), VECTOR3_USE(color), alpha);
}

int btIDebugDraw_getDebugMode(btIDebugDraw* obj)
{
	return obj->getDebugMode();
}

void btIDebugDraw_reportErrorWarning(btIDebugDraw* obj, char* warningString)
{
	obj->reportErrorWarning(warningString);
}

void btIDebugDraw_setDebugMode(btIDebugDraw* obj, int debugMode)
{
	obj->setDebugMode(debugMode);
}

void btIDebugDraw_delete(btIDebugDraw* obj)
{
	delete obj;
}
