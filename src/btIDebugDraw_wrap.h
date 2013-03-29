#include "main.h"

typedef void (STDCALL pDrawBox)(const btVector3& bbMin, const btVector3& bbMax, const btScalar* trans, const btVector3& color);
typedef void (STDCALL pDrawCapsule)(btScalar radius, btScalar halfHeight, int upAxis, const btScalar* transform, const btVector3& color);
typedef void (STDCALL pDrawLine)(const btVector3& from, const btVector3& to, const btVector3& color);
typedef void (STDCALL pDrawSphere)(btScalar radius, const btScalar* transform, const btVector3& color);
typedef void (STDCALL pDrawTransform)(const btScalar* transform, btScalar orthoLen);
typedef int (STDCALL pGetDebugMode)();
typedef void (STDCALL pSimpleCallback)(int x);

class btIDebugDrawWrapper : public btIDebugDraw
{
private:
	pDrawBox _drawBoxCallback;
	pDrawCapsule _drawCapsule;
	pDrawLine _drawLineCallback;
	pDrawSphere _drawSphereCallback;
	pDrawTransform _drawTransformCallback;
	pGetDebugMode _getDebugModeCallback;

public:
	void* _debugDrawGCHandle;
	
	pSimpleCallback _cb;

	btIDebugDrawWrapper(void* debugDrawGCHandle, pDrawBox drawBoxCallback, pDrawCapsule drawCapsule, pDrawLine drawLineCallback,
		pDrawSphere drawSphereCallback, pDrawTransform drawTransformCallback, pGetDebugMode getDebugModeCallback, pSimpleCallback cb);

	virtual void draw3dText(const btVector3& location, const char* textString);
	virtual void drawAabb(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
		btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
		const btVector3& color, bool drawSect, btScalar stepDegrees);
	virtual void drawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
		btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
		const btVector3& color, bool drawSect);
	virtual void drawBox(const btVector3& bbMin, const btVector3& bbMax, const btTransform& trans, const btVector3& color);
	virtual void drawBox(const btVector3& bbMin, const btVector3& bbMax, const btVector3& color);
	virtual void drawCapsule(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void drawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void drawContactPoint(const btVector3& PointOnB, const btVector3& normalOnB, btScalar distance, int lifeTime, const btVector3& color);
	virtual void drawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void drawLine(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void drawLine(const btVector3& from, const btVector3& to, const btVector3& fromColor, const btVector3& toColor);
	virtual void drawPlane(const btVector3& planeNormal, btScalar planeConst, const btTransform& transform, const btVector3& color);
	virtual void drawSphere(const btVector3& p, btScalar radius, const btVector3& color);
	virtual void drawSphere(btScalar radius, const btTransform& transform, const btVector3& color);
	virtual void drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
		btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color, btScalar stepDegrees);
	virtual void drawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
		btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color);
	virtual void drawTransform(const btTransform& transform, btScalar orthoLen);
	virtual void drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2, const btVector3& color, btScalar);
	virtual void drawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
		const btVector3&, const btVector3&, const btVector3&, const btVector3& color, btScalar alpha);

	virtual void baseDrawAabb(const btVector3& from, const btVector3& to, const btVector3& color);
	virtual void baseDrawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
		btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
		const btVector3& color, bool drawSect, btScalar stepDegrees);
	virtual void baseDrawArc(const btVector3& center, const btVector3& normal, const btVector3& axis,
		btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle,
		const btVector3& color, bool drawSect);
	virtual void baseDrawBox(const btVector3& bbMin, const btVector3& bbMax, const btVector3& color);
	virtual void baseDrawCone(btScalar radius, btScalar height, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void baseDrawCylinder(btScalar radius, btScalar halfHeight, int upAxis, const btTransform& transform, const btVector3& color);
	virtual void baseDrawLine(const btVector3& from, const btVector3& to, const btVector3& fromColor, const btVector3& toColor);
	virtual void baseDrawPlane(const btVector3& planeNormal, btScalar planeConst, const btTransform& transform, const btVector3& color);
	virtual void baseDrawSphere(const btVector3& p, btScalar radius, const btVector3& color);
	virtual void baseDrawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
		btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color, btScalar stepDegrees);
	virtual void baseDrawSpherePatch(const btVector3& center, const btVector3& up, const btVector3& axis, btScalar radius,
		btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, const btVector3& color);
	virtual void baseDrawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2, const btVector3& color, btScalar);
	virtual void baseDrawTriangle(const btVector3& v0, const btVector3& v1, const btVector3& v2,
		const btVector3&, const btVector3&, const btVector3&, const btVector3& color, btScalar alpha);

	virtual void reportErrorWarning(const char* warningString);

	virtual void setDebugMode(int debugMode);
	virtual int	getDebugMode() const;
};

extern "C"
{
	EXPORT btIDebugDrawWrapper* btIDebugDrawWrapper_new(void* debugDrawGCHandle, pDrawBox drawBoxCallback, pDrawCapsule drawCapsule,
		pDrawLine drawLineCallback, pDrawSphere drawSphereCallback, pDrawTransform drawTransformCallback, pGetDebugMode getDebugModeCallback, pSimpleCallback cb);
	EXPORT void* btIDebugDrawWrapper_getDebugDrawGCHandle(btIDebugDrawWrapper* obj);

	EXPORT void btIDebugDraw_draw3dText(btIDebugDraw* obj, btVector3* location, char* textString);
	EXPORT void btIDebugDraw_drawAabb(btIDebugDraw* obj, btVector3* from, btVector3* to, btVector3* color);
	EXPORT void btIDebugDraw_drawArc(btIDebugDraw* obj, btVector3* center, btVector3* normal, btVector3* axis, btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle, btVector3* color, bool drawSect, btScalar stepDegrees);
	EXPORT void btIDebugDraw_drawArc2(btIDebugDraw* obj, btVector3* center, btVector3* normal, btVector3* axis, btScalar radiusA, btScalar radiusB, btScalar minAngle, btScalar maxAngle, btVector3* color, bool drawSect);
	EXPORT void btIDebugDraw_drawBox(btIDebugDraw* obj, btVector3* bbMin, btVector3* bbMax, btTransform* trans, btVector3* color);
	EXPORT void btIDebugDraw_drawBox2(btIDebugDraw* obj, btVector3* bbMin, btVector3* bbMax, btVector3* color);
	EXPORT void btIDebugDraw_drawCapsule(btIDebugDraw* obj, btScalar radius, btScalar halfHeight, int upAxis, btTransform* transform, btVector3* color);
	EXPORT void btIDebugDraw_drawCone(btIDebugDraw* obj, btScalar radius, btScalar height, int upAxis, btTransform* transform, btVector3* color);
	EXPORT void btIDebugDraw_drawContactPoint(btIDebugDraw* obj, btVector3* PointOnB, btVector3* normalOnB, btScalar distance, int lifeTime, btVector3* color);
	EXPORT void btIDebugDraw_drawCylinder(btIDebugDraw* obj, btScalar radius, btScalar halfHeight, int upAxis, btTransform* transform, btVector3* color);
	EXPORT void btIDebugDraw_drawLine(btIDebugDraw* obj, btVector3* from, btVector3* to, btVector3* color);
	EXPORT void btIDebugDraw_drawLine2(btIDebugDraw* obj, btVector3* from, btVector3* to, btVector3* fromColor, btVector3* toColor);
	EXPORT void btIDebugDraw_drawPlane(btIDebugDraw* obj, btVector3* planeNormal, btScalar planeConst, btTransform* transform, btVector3* color);
	EXPORT void btIDebugDraw_drawSphere(btIDebugDraw* obj, btVector3* p, btScalar radius, btVector3* color);
	EXPORT void btIDebugDraw_drawSphere2(btIDebugDraw* obj, btScalar radius, btTransform* transform, btVector3* color);
	EXPORT void btIDebugDraw_drawSpherePatch(btIDebugDraw* obj, btVector3* center, btVector3* up, btVector3* axis, btScalar radius, btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, btVector3* color, btScalar stepDegrees);
	EXPORT void btIDebugDraw_drawSpherePatch2(btIDebugDraw* obj, btVector3* center, btVector3* up, btVector3* axis, btScalar radius, btScalar minTh, btScalar maxTh, btScalar minPs, btScalar maxPs, btVector3* color);
	EXPORT void btIDebugDraw_drawTransform(btIDebugDraw* obj, btTransform* transform, btScalar orthoLen);
	EXPORT void btIDebugDraw_drawTriangle(btIDebugDraw* obj, btVector3* v0, btVector3* v1, btVector3* v2, btVector3* color, btScalar __unnamed4);
	EXPORT void btIDebugDraw_drawTriangle2(btIDebugDraw* obj, btVector3* v0, btVector3* v1, btVector3* v2, btVector3* __unnamed3, btVector3* __unnamed4, btVector3* __unnamed5, btVector3* color, btScalar alpha);
	EXPORT int btIDebugDraw_getDebugMode(btIDebugDraw* obj);
	EXPORT void btIDebugDraw_reportErrorWarning(btIDebugDraw* obj, char* warningString);
	EXPORT void btIDebugDraw_setDebugMode(btIDebugDraw* obj, int debugMode);
	EXPORT void btIDebugDraw_delete(btIDebugDraw* obj);
}
