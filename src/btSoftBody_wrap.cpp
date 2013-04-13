#include "btSoftBody_wrap.h"

btSoftBodyWorldInfo* btSoftBodyWorldInfo_new()
{
	return new btSoftBodyWorldInfo();
}

btScalar btSoftBodyWorldInfo_getAir_density(btSoftBodyWorldInfo* obj)
{
	return obj->air_density;
}

btBroadphaseInterface* btSoftBodyWorldInfo_getBroadphase(btSoftBodyWorldInfo* obj)
{
	return obj->m_broadphase;
}

btDispatcher* btSoftBodyWorldInfo_getDispatcher(btSoftBodyWorldInfo* obj)
{
	return obj->m_dispatcher;
}

void btSoftBodyWorldInfo_getGravity(btSoftBodyWorldInfo* obj)
{
	obj->m_gravity;
}

void btSoftBodyWorldInfo_getSparsesdf(btSoftBodyWorldInfo* obj)
{
	obj->m_sparsesdf;
}

btScalar btSoftBodyWorldInfo_getWater_density(btSoftBodyWorldInfo* obj)
{
	return obj->water_density;
}

void btSoftBodyWorldInfo_getWater_normal(btSoftBodyWorldInfo* obj)
{
	obj->water_normal;
}

btScalar btSoftBodyWorldInfo_getWater_offset(btSoftBodyWorldInfo* obj)
{
	return obj->water_offset;
}

void btSoftBodyWorldInfo_setAir_density(btSoftBodyWorldInfo* obj, btScalar value)
{
	obj->air_density = value;
}

void btSoftBodyWorldInfo_setBroadphase(btSoftBodyWorldInfo* obj, btBroadphaseInterface* value)
{
	obj->m_broadphase = value;
}

void btSoftBodyWorldInfo_setDispatcher(btSoftBodyWorldInfo* obj, btDispatcher* value)
{
	obj->m_dispatcher = value;
}
/*
void btSoftBodyWorldInfo_setGravity(btSoftBodyWorldInfo* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_gravity = value;
}

void btSoftBodyWorldInfo_setSparsesdf(btSoftBodyWorldInfo* obj, void value)
{
	obj->m_sparsesdf = value;
}
*/
void btSoftBodyWorldInfo_setWater_density(btSoftBodyWorldInfo* obj, btScalar value)
{
	obj->water_density = value;
}
/*
void btSoftBodyWorldInfo_setWater_normal(btSoftBodyWorldInfo* obj, void value)
{
	VECTOR3_CONV(value);
	obj->water_normal = value;
}
*/
void btSoftBodyWorldInfo_setWater_offset(btSoftBodyWorldInfo* obj, btScalar value)
{
	obj->water_offset = value;
}

void btSoftBodyWorldInfo_delete(btSoftBodyWorldInfo* obj)
{
	delete obj;
}

btSoftBody::eAeroModel* btSoftBody_eAeroModel_new()
{
	return new btSoftBody::eAeroModel();
}

void btSoftBody_eAeroModel_delete(btSoftBody::eAeroModel* obj)
{
	delete obj;
}

btSoftBody::eVSolver* btSoftBody_eVSolver_new()
{
	return new btSoftBody::eVSolver();
}

void btSoftBody_eVSolver_delete(btSoftBody::eVSolver* obj)
{
	delete obj;
}

btSoftBody::ePSolver* btSoftBody_ePSolver_new()
{
	return new btSoftBody::ePSolver();
}

void btSoftBody_ePSolver_delete(btSoftBody::ePSolver* obj)
{
	delete obj;
}

btSoftBody::eSolverPresets* btSoftBody_eSolverPresets_new()
{
	return new btSoftBody::eSolverPresets();
}

void btSoftBody_eSolverPresets_delete(btSoftBody::eSolverPresets* obj)
{
	delete obj;
}

btSoftBody::eFeature* btSoftBody_eFeature_new()
{
	return new btSoftBody::eFeature();
}

void btSoftBody_eFeature_delete(btSoftBody::eFeature* obj)
{
	delete obj;
}

btSoftBody::fCollision* btSoftBody_fCollision_new()
{
	return new btSoftBody::fCollision();
}

void btSoftBody_fCollision_delete(btSoftBody::fCollision* obj)
{
	delete obj;
}

btSoftBody::fMaterial* btSoftBody_fMaterial_new()
{
	return new btSoftBody::fMaterial();
}

void btSoftBody_fMaterial_delete(btSoftBody::fMaterial* obj)
{
	delete obj;
}

btSoftBody::sRayCast* btSoftBody_sRayCast_new()
{
	return new btSoftBody::sRayCast();
}

btSoftBody* btSoftBody_sRayCast_getBody(btSoftBody::sRayCast* obj)
{
	return obj->body;
}

void btSoftBody_sRayCast_getFeature(btSoftBody::sRayCast* obj)
{
	obj->feature;
}

btScalar btSoftBody_sRayCast_getFraction(btSoftBody::sRayCast* obj)
{
	return obj->fraction;
}

int btSoftBody_sRayCast_getIndex(btSoftBody::sRayCast* obj)
{
	return obj->index;
}

void btSoftBody_sRayCast_setBody(btSoftBody::sRayCast* obj, btSoftBody* value)
{
	obj->body = value;
}
/*
void btSoftBody_sRayCast_setFeature(btSoftBody::sRayCast* obj, void value)
{
	obj->feature = value;
}
*/
void btSoftBody_sRayCast_setFraction(btSoftBody::sRayCast* obj, btScalar value)
{
	obj->fraction = value;
}

void btSoftBody_sRayCast_setIndex(btSoftBody::sRayCast* obj, int value)
{
	obj->index = value;
}

void btSoftBody_sRayCast_delete(btSoftBody::sRayCast* obj)
{
	delete obj;
}

btScalar btSoftBody_ImplicitFn_Eval(btSoftBody::ImplicitFn* obj, btScalar* x)
{
	VECTOR3_CONV(x);
	return obj->Eval(VECTOR3_USE(x));
}

void btSoftBody_ImplicitFn_delete(btSoftBody::ImplicitFn* obj)
{
	delete obj;
}

btSoftBody::sCti* btSoftBody_sCti_new()
{
	return new btSoftBody::sCti();
}
/*
btCollisionObject* btSoftBody_sCti_getColObj(btSoftBody::sCti* obj)
{
	return obj->m_colObj;
}
*/
void btSoftBody_sCti_getNormal(btSoftBody::sCti* obj)
{
	obj->m_normal;
}

btScalar btSoftBody_sCti_getOffset(btSoftBody::sCti* obj)
{
	return obj->m_offset;
}

void btSoftBody_sCti_setColObj(btSoftBody::sCti* obj, btCollisionObject* value)
{
	obj->m_colObj = value;
}
/*
void btSoftBody_sCti_setNormal(btSoftBody::sCti* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_normal = value;
}
*/
void btSoftBody_sCti_setOffset(btSoftBody::sCti* obj, btScalar value)
{
	obj->m_offset = value;
}

void btSoftBody_sCti_delete(btSoftBody::sCti* obj)
{
	delete obj;
}

btSoftBody::sMedium* btSoftBody_sMedium_new()
{
	return new btSoftBody::sMedium();
}

btScalar btSoftBody_sMedium_getDensity(btSoftBody::sMedium* obj)
{
	return obj->m_density;
}

btScalar btSoftBody_sMedium_getPressure(btSoftBody::sMedium* obj)
{
	return obj->m_pressure;
}

void btSoftBody_sMedium_getVelocity(btSoftBody::sMedium* obj)
{
	obj->m_velocity;
}

void btSoftBody_sMedium_setDensity(btSoftBody::sMedium* obj, btScalar value)
{
	obj->m_density = value;
}

void btSoftBody_sMedium_setPressure(btSoftBody::sMedium* obj, btScalar value)
{
	obj->m_pressure = value;
}
/*
void btSoftBody_sMedium_setVelocity(btSoftBody::sMedium* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_velocity = value;
}
*/
void btSoftBody_sMedium_delete(btSoftBody::sMedium* obj)
{
	delete obj;
}

btSoftBody::Element* btSoftBody_Element_new()
{
	return new btSoftBody::Element();
}

void* btSoftBody_Element_getTag(btSoftBody::Element* obj)
{
	return obj->m_tag;
}

void btSoftBody_Element_setTag(btSoftBody::Element* obj, void* value)
{
	obj->m_tag = value;
}

void btSoftBody_Element_delete(btSoftBody::Element* obj)
{
	delete obj;
}

btSoftBody::Material* btSoftBody_Material_new()
{
	return new btSoftBody::Material();
}

int btSoftBody_Material_getFlags(btSoftBody::Material* obj)
{
	return obj->m_flags;
}

btScalar btSoftBody_Material_getKAST(btSoftBody::Material* obj)
{
	return obj->m_kAST;
}

btScalar btSoftBody_Material_getKLST(btSoftBody::Material* obj)
{
	return obj->m_kLST;
}

btScalar btSoftBody_Material_getKVST(btSoftBody::Material* obj)
{
	return obj->m_kVST;
}

void btSoftBody_Material_setFlags(btSoftBody::Material* obj, int value)
{
	obj->m_flags = value;
}

void btSoftBody_Material_setKAST(btSoftBody::Material* obj, btScalar value)
{
	obj->m_kAST = value;
}

void btSoftBody_Material_setKLST(btSoftBody::Material* obj, btScalar value)
{
	obj->m_kLST = value;
}

void btSoftBody_Material_setKVST(btSoftBody::Material* obj, btScalar value)
{
	obj->m_kVST = value;
}

btSoftBody::Feature* btSoftBody_Feature_new()
{
	return new btSoftBody::Feature();
}

btSoftBody::Material* btSoftBody_Feature_getMaterial(btSoftBody::Feature* obj)
{
	return obj->m_material;
}

void btSoftBody_Feature_setMaterial(btSoftBody::Feature* obj, btSoftBody::Material* value)
{
	obj->m_material = value;
}

btSoftBody::Node* btSoftBody_Node_new()
{
	return new btSoftBody::Node();
}

btScalar btSoftBody_Node_getArea(btSoftBody::Node* obj)
{
	return obj->m_area;
}

int btSoftBody_Node_getBattach(btSoftBody::Node* obj)
{
	return obj->m_battach;
}

void btSoftBody_Node_getF(btSoftBody::Node* obj)
{
	obj->m_f;
}

btScalar btSoftBody_Node_getIm(btSoftBody::Node* obj)
{
	return obj->m_im;
}

btDbvtNode* btSoftBody_Node_getLeaf(btSoftBody::Node* obj)
{
	return obj->m_leaf;
}

void btSoftBody_Node_getN(btSoftBody::Node* obj)
{
	obj->m_n;
}

void btSoftBody_Node_getQ(btSoftBody::Node* obj)
{
	obj->m_q;
}

void btSoftBody_Node_getV(btSoftBody::Node* obj)
{
	obj->m_v;
}

void btSoftBody_Node_getX(btSoftBody::Node* obj)
{
	obj->m_x;
}

void btSoftBody_Node_setArea(btSoftBody::Node* obj, btScalar value)
{
	obj->m_area = value;
}

void btSoftBody_Node_setBattach(btSoftBody::Node* obj, int value)
{
	obj->m_battach = value;
}
/*
void btSoftBody_Node_setF(btSoftBody::Node* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_f = value;
}
*/
void btSoftBody_Node_setIm(btSoftBody::Node* obj, btScalar value)
{
	obj->m_im = value;
}

void btSoftBody_Node_setLeaf(btSoftBody::Node* obj, btDbvtNode* value)
{
	obj->m_leaf = value;
}
/*
void btSoftBody_Node_setN(btSoftBody::Node* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_n = value;
}

void btSoftBody_Node_setQ(btSoftBody::Node* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_q = value;
}

void btSoftBody_Node_setV(btSoftBody::Node* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_v = value;
}

void btSoftBody_Node_setX(btSoftBody::Node* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_x = value;
}
*/
btSoftBody::Link* btSoftBody_Link_new()
{
	return new btSoftBody::Link();
}

int btSoftBody_Link_getBbending(btSoftBody::Link* obj)
{
	return obj->m_bbending;
}

btScalar btSoftBody_Link_getC0(btSoftBody::Link* obj)
{
	return obj->m_c0;
}

btScalar btSoftBody_Link_getC1(btSoftBody::Link* obj)
{
	return obj->m_c1;
}

btScalar btSoftBody_Link_getC2(btSoftBody::Link* obj)
{
	return obj->m_c2;
}

void btSoftBody_Link_getC3(btSoftBody::Link* obj)
{
	obj->m_c3;
}
/*
* btSoftBody_Link_getN(btSoftBody::Link* obj)
{
	return obj->m_n;
}
*/
btScalar btSoftBody_Link_getRl(btSoftBody::Link* obj)
{
	return obj->m_rl;
}

void btSoftBody_Link_setBbending(btSoftBody::Link* obj, int value)
{
	obj->m_bbending = value;
}

void btSoftBody_Link_setC0(btSoftBody::Link* obj, btScalar value)
{
	obj->m_c0 = value;
}

void btSoftBody_Link_setC1(btSoftBody::Link* obj, btScalar value)
{
	obj->m_c1 = value;
}

void btSoftBody_Link_setC2(btSoftBody::Link* obj, btScalar value)
{
	obj->m_c2 = value;
}
/*
void btSoftBody_Link_setC3(btSoftBody::Link* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_c3 = value;
}

void btSoftBody_Link_setN(btSoftBody::Link* obj, * value)
{
	obj->m_n = value;
}
*/
void btSoftBody_Link_setRl(btSoftBody::Link* obj, btScalar value)
{
	obj->m_rl = value;
}

btSoftBody::Face* btSoftBody_Face_new()
{
	return new btSoftBody::Face();
}

btDbvtNode* btSoftBody_Face_getLeaf(btSoftBody::Face* obj)
{
	return obj->m_leaf;
}
/*
* btSoftBody_Face_getN(btSoftBody::Face* obj)
{
	return obj->m_n;
}
*/
void btSoftBody_Face_getNormal(btSoftBody::Face* obj)
{
	obj->m_normal;
}

btScalar btSoftBody_Face_getRa(btSoftBody::Face* obj)
{
	return obj->m_ra;
}

void btSoftBody_Face_setLeaf(btSoftBody::Face* obj, btDbvtNode* value)
{
	obj->m_leaf = value;
}
/*
void btSoftBody_Face_setN(btSoftBody::Face* obj, * value)
{
	obj->m_n = value;
}

void btSoftBody_Face_setNormal(btSoftBody::Face* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_normal = value;
}
*/
void btSoftBody_Face_setRa(btSoftBody::Face* obj, btScalar value)
{
	obj->m_ra = value;
}

btSoftBody::Tetra* btSoftBody_Tetra_new()
{
	return new btSoftBody::Tetra();
}
/*
btScalar* btSoftBody_Tetra_getC0(btSoftBody::Tetra* obj)
{
	return obj->m_c0;
}
*/
btScalar btSoftBody_Tetra_getC1(btSoftBody::Tetra* obj)
{
	return obj->m_c1;
}

btScalar btSoftBody_Tetra_getC2(btSoftBody::Tetra* obj)
{
	return obj->m_c2;
}

btDbvtNode* btSoftBody_Tetra_getLeaf(btSoftBody::Tetra* obj)
{
	return obj->m_leaf;
}
/*
* btSoftBody_Tetra_getN(btSoftBody::Tetra* obj)
{
	return obj->m_n;
}
*/
btScalar btSoftBody_Tetra_getRv(btSoftBody::Tetra* obj)
{
	return obj->m_rv;
}
/*
void btSoftBody_Tetra_setC0(btSoftBody::Tetra* obj, btScalar* value)
{
	VECTOR3_CONV(value);
	obj->m_c0 = value;
}
*/
void btSoftBody_Tetra_setC1(btSoftBody::Tetra* obj, btScalar value)
{
	obj->m_c1 = value;
}

void btSoftBody_Tetra_setC2(btSoftBody::Tetra* obj, btScalar value)
{
	obj->m_c2 = value;
}

void btSoftBody_Tetra_setLeaf(btSoftBody::Tetra* obj, btDbvtNode* value)
{
	obj->m_leaf = value;
}
/*
void btSoftBody_Tetra_setN(btSoftBody::Tetra* obj, * value)
{
	obj->m_n = value;
}
*/
void btSoftBody_Tetra_setRv(btSoftBody::Tetra* obj, btScalar value)
{
	obj->m_rv = value;
}

btSoftBody::RContact* btSoftBody_RContact_new()
{
	return new btSoftBody::RContact();
}

void btSoftBody_RContact_getC0(btSoftBody::RContact* obj)
{
	obj->m_c0;
}

void btSoftBody_RContact_getC1(btSoftBody::RContact* obj)
{
	obj->m_c1;
}

btScalar btSoftBody_RContact_getC2(btSoftBody::RContact* obj)
{
	return obj->m_c2;
}

btScalar btSoftBody_RContact_getC3(btSoftBody::RContact* obj)
{
	return obj->m_c3;
}

btScalar btSoftBody_RContact_getC4(btSoftBody::RContact* obj)
{
	return obj->m_c4;
}

void btSoftBody_RContact_getCti(btSoftBody::RContact* obj)
{
	obj->m_cti;
}

btSoftBody::Node* btSoftBody_RContact_getNode(btSoftBody::RContact* obj)
{
	return obj->m_node;
}
/*
void btSoftBody_RContact_setC0(btSoftBody::RContact* obj, void value)
{
	obj->m_c0 = value;
}

void btSoftBody_RContact_setC1(btSoftBody::RContact* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_c1 = value;
}
*/
void btSoftBody_RContact_setC2(btSoftBody::RContact* obj, btScalar value)
{
	obj->m_c2 = value;
}

void btSoftBody_RContact_setC3(btSoftBody::RContact* obj, btScalar value)
{
	obj->m_c3 = value;
}

void btSoftBody_RContact_setC4(btSoftBody::RContact* obj, btScalar value)
{
	obj->m_c4 = value;
}
/*
void btSoftBody_RContact_setCti(btSoftBody::RContact* obj, void value)
{
	obj->m_cti = value;
}
*/
void btSoftBody_RContact_setNode(btSoftBody::RContact* obj, btSoftBody::Node* value)
{
	obj->m_node = value;
}

void btSoftBody_RContact_delete(btSoftBody::RContact* obj)
{
	delete obj;
}

btSoftBody::SContact* btSoftBody_SContact_new()
{
	return new btSoftBody::SContact();
}

btScalar* btSoftBody_SContact_getCfm(btSoftBody::SContact* obj)
{
	return obj->m_cfm;
}

btSoftBody::Face* btSoftBody_SContact_getFace(btSoftBody::SContact* obj)
{
	return obj->m_face;
}

btScalar btSoftBody_SContact_getFriction(btSoftBody::SContact* obj)
{
	return obj->m_friction;
}

btScalar btSoftBody_SContact_getMargin(btSoftBody::SContact* obj)
{
	return obj->m_margin;
}

btSoftBody::Node* btSoftBody_SContact_getNode(btSoftBody::SContact* obj)
{
	return obj->m_node;
}

void btSoftBody_SContact_getNormal(btSoftBody::SContact* obj)
{
	obj->m_normal;
}

void btSoftBody_SContact_getWeights(btSoftBody::SContact* obj)
{
	obj->m_weights;
}
/*
void btSoftBody_SContact_setCfm(btSoftBody::SContact* obj, btScalar* value)
{
	obj->m_cfm = value;
}
*/
void btSoftBody_SContact_setFace(btSoftBody::SContact* obj, btSoftBody::Face* value)
{
	obj->m_face = value;
}

void btSoftBody_SContact_setFriction(btSoftBody::SContact* obj, btScalar value)
{
	obj->m_friction = value;
}

void btSoftBody_SContact_setMargin(btSoftBody::SContact* obj, btScalar value)
{
	obj->m_margin = value;
}

void btSoftBody_SContact_setNode(btSoftBody::SContact* obj, btSoftBody::Node* value)
{
	obj->m_node = value;
}
/*
void btSoftBody_SContact_setNormal(btSoftBody::SContact* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_normal = value;
}

void btSoftBody_SContact_setWeights(btSoftBody::SContact* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_weights = value;
}
*/
void btSoftBody_SContact_delete(btSoftBody::SContact* obj)
{
	delete obj;
}

btSoftBody::Anchor* btSoftBody_Anchor_new()
{
	return new btSoftBody::Anchor();
}

btRigidBody* btSoftBody_Anchor_getBody(btSoftBody::Anchor* obj)
{
	return obj->m_body;
}

void btSoftBody_Anchor_getC0(btSoftBody::Anchor* obj)
{
	obj->m_c0;
}

void btSoftBody_Anchor_getC1(btSoftBody::Anchor* obj)
{
	obj->m_c1;
}

btScalar btSoftBody_Anchor_getC2(btSoftBody::Anchor* obj)
{
	return obj->m_c2;
}

btScalar btSoftBody_Anchor_getInfluence(btSoftBody::Anchor* obj)
{
	return obj->m_influence;
}

void btSoftBody_Anchor_getLocal(btSoftBody::Anchor* obj)
{
	obj->m_local;
}

btSoftBody::Node* btSoftBody_Anchor_getNode(btSoftBody::Anchor* obj)
{
	return obj->m_node;
}

void btSoftBody_Anchor_setBody(btSoftBody::Anchor* obj, btRigidBody* value)
{
	obj->m_body = value;
}
/*
void btSoftBody_Anchor_setC0(btSoftBody::Anchor* obj, void value)
{
	obj->m_c0 = value;
}

void btSoftBody_Anchor_setC1(btSoftBody::Anchor* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_c1 = value;
}
*/
void btSoftBody_Anchor_setC2(btSoftBody::Anchor* obj, btScalar value)
{
	obj->m_c2 = value;
}

void btSoftBody_Anchor_setInfluence(btSoftBody::Anchor* obj, btScalar value)
{
	obj->m_influence = value;
}
/*
void btSoftBody_Anchor_setLocal(btSoftBody::Anchor* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_local = value;
}
*/
void btSoftBody_Anchor_setNode(btSoftBody::Anchor* obj, btSoftBody::Node* value)
{
	obj->m_node = value;
}

void btSoftBody_Anchor_delete(btSoftBody::Anchor* obj)
{
	delete obj;
}

btSoftBody::Note* btSoftBody_Note_new()
{
	return new btSoftBody::Note();
}

btScalar* btSoftBody_Note_getCoords(btSoftBody::Note* obj)
{
	return obj->m_coords;
}
/*
* btSoftBody_Note_getNodes(btSoftBody::Note* obj)
{
	return obj->m_nodes;
}
*/
void btSoftBody_Note_getOffset(btSoftBody::Note* obj)
{
	obj->m_offset;
}

int btSoftBody_Note_getRank(btSoftBody::Note* obj)
{
	return obj->m_rank;
}

const char* btSoftBody_Note_getText(btSoftBody::Note* obj)
{
	return obj->m_text;
}
/*
void btSoftBody_Note_setCoords(btSoftBody::Note* obj, btScalar* value)
{
	obj->m_coords = value;
}

void btSoftBody_Note_setNodes(btSoftBody::Note* obj, * value)
{
	obj->m_nodes = value;
}

void btSoftBody_Note_setOffset(btSoftBody::Note* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_offset = value;
}
*/
void btSoftBody_Note_setRank(btSoftBody::Note* obj, int value)
{
	obj->m_rank = value;
}

void btSoftBody_Note_setText(btSoftBody::Note* obj, char* value)
{
	obj->m_text = value;
}

btSoftBody::Pose* btSoftBody_Pose_new()
{
	return new btSoftBody::Pose();
}

void btSoftBody_Pose_getAqq(btSoftBody::Pose* obj)
{
	obj->m_aqq;
}

bool btSoftBody_Pose_getBframe(btSoftBody::Pose* obj)
{
	return obj->m_bframe;
}

bool btSoftBody_Pose_getBvolume(btSoftBody::Pose* obj)
{
	return obj->m_bvolume;
}

void btSoftBody_Pose_getCom(btSoftBody::Pose* obj)
{
	obj->m_com;
}
/*
btAlignedObjectArray* btSoftBody_Pose_getPos(btSoftBody::Pose* obj)
{
	return obj->m_pos;
}
*/
void btSoftBody_Pose_getRot(btSoftBody::Pose* obj)
{
	obj->m_rot;
}

void btSoftBody_Pose_getScl(btSoftBody::Pose* obj)
{
	obj->m_scl;
}
/*
btAlignedObjectArray* btSoftBody_Pose_getWgh(btSoftBody::Pose* obj)
{
	return obj->m_wgh;
}
*/
btScalar btSoftBody_Pose_getVolume(btSoftBody::Pose* obj)
{
	return obj->m_volume;
}
/*
void btSoftBody_Pose_setAqq(btSoftBody::Pose* obj, void value)
{
	obj->m_aqq = value;
}
*/
void btSoftBody_Pose_setBframe(btSoftBody::Pose* obj, bool value)
{
	obj->m_bframe = value;
}

void btSoftBody_Pose_setBvolume(btSoftBody::Pose* obj, bool value)
{
	obj->m_bvolume = value;
}
/*
void btSoftBody_Pose_setCom(btSoftBody::Pose* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_com = value;
}

void btSoftBody_Pose_setPos(btSoftBody::Pose* obj, btAlignedObjectArray* value)
{
	obj->m_pos = value;
}

void btSoftBody_Pose_setRot(btSoftBody::Pose* obj, void value)
{
	obj->m_rot = value;
}

void btSoftBody_Pose_setScl(btSoftBody::Pose* obj, void value)
{
	obj->m_scl = value;
}

void btSoftBody_Pose_setWgh(btSoftBody::Pose* obj, btAlignedObjectArray* value)
{
	obj->m_wgh = value;
}
*/
void btSoftBody_Pose_setVolume(btSoftBody::Pose* obj, btScalar value)
{
	obj->m_volume = value;
}

void btSoftBody_Pose_delete(btSoftBody::Pose* obj)
{
	delete obj;
}

btSoftBody::Cluster* btSoftBody_Cluster_new()
{
	return new btSoftBody::Cluster();
}

btScalar btSoftBody_Cluster_getAdamping(btSoftBody::Cluster* obj)
{
	return obj->m_adamping;
}

void btSoftBody_Cluster_getAv(btSoftBody::Cluster* obj)
{
	obj->m_av;
}

int btSoftBody_Cluster_getClusterIndex(btSoftBody::Cluster* obj)
{
	return obj->m_clusterIndex;
}

bool btSoftBody_Cluster_getCollide(btSoftBody::Cluster* obj)
{
	return obj->m_collide;
}

void btSoftBody_Cluster_getCom(btSoftBody::Cluster* obj)
{
	obj->m_com;
}

bool btSoftBody_Cluster_getContainsAnchor(btSoftBody::Cluster* obj)
{
	return obj->m_containsAnchor;
}
/*
btScalar* btSoftBody_Cluster_getDimpulses(btSoftBody::Cluster* obj)
{
	return obj->m_dimpulses;
}

btAlignedObjectArray* btSoftBody_Cluster_getFramerefs(btSoftBody::Cluster* obj)
{
	return obj->m_framerefs;
}
*/
void btSoftBody_Cluster_getFramexform(btSoftBody::Cluster* obj)
{
	obj->m_framexform;
}

btScalar btSoftBody_Cluster_getIdmass(btSoftBody::Cluster* obj)
{
	return obj->m_idmass;
}

btScalar btSoftBody_Cluster_getImass(btSoftBody::Cluster* obj)
{
	return obj->m_imass;
}

void btSoftBody_Cluster_getInvwi(btSoftBody::Cluster* obj)
{
	obj->m_invwi;
}

btScalar btSoftBody_Cluster_getLdamping(btSoftBody::Cluster* obj)
{
	return obj->m_ldamping;
}

btDbvtNode* btSoftBody_Cluster_getLeaf(btSoftBody::Cluster* obj)
{
	return obj->m_leaf;
}

void btSoftBody_Cluster_getLocii(btSoftBody::Cluster* obj)
{
	obj->m_locii;
}

void btSoftBody_Cluster_getLv(btSoftBody::Cluster* obj)
{
	obj->m_lv;
}
/*
btAlignedObjectArray* btSoftBody_Cluster_getMasses(btSoftBody::Cluster* obj)
{
	return obj->m_masses;
}
*/
btScalar btSoftBody_Cluster_getMatching(btSoftBody::Cluster* obj)
{
	return obj->m_matching;
}

btScalar btSoftBody_Cluster_getMaxSelfCollisionImpulse(btSoftBody::Cluster* obj)
{
	return obj->m_maxSelfCollisionImpulse;
}

btScalar btSoftBody_Cluster_getNdamping(btSoftBody::Cluster* obj)
{
	return obj->m_ndamping;
}

int btSoftBody_Cluster_getNdimpulses(btSoftBody::Cluster* obj)
{
	return obj->m_ndimpulses;
}

void btSoftBody_Cluster_getNodes(btSoftBody::Cluster* obj)
{
	obj->m_nodes;
}

int btSoftBody_Cluster_getNvimpulses(btSoftBody::Cluster* obj)
{
	return obj->m_nvimpulses;
}

btScalar btSoftBody_Cluster_getSelfCollisionImpulseFactor(btSoftBody::Cluster* obj)
{
	return obj->m_selfCollisionImpulseFactor;
}
/*
btScalar* btSoftBody_Cluster_getVimpulses(btSoftBody::Cluster* obj)
{
	return obj->m_vimpulses;
}
*/
void btSoftBody_Cluster_setAdamping(btSoftBody::Cluster* obj, btScalar value)
{
	obj->m_adamping = value;
}
/*
void btSoftBody_Cluster_setAv(btSoftBody::Cluster* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_av = value;
}*/

void btSoftBody_Cluster_setClusterIndex(btSoftBody::Cluster* obj, int value)
{
	obj->m_clusterIndex = value;
}

void btSoftBody_Cluster_setCollide(btSoftBody::Cluster* obj, bool value)
{
	obj->m_collide = value;
}
/*
void btSoftBody_Cluster_setCom(btSoftBody::Cluster* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_com = value;
}
*/
void btSoftBody_Cluster_setContainsAnchor(btSoftBody::Cluster* obj, bool value)
{
	obj->m_containsAnchor = value;
}
/*
void btSoftBody_Cluster_setDimpulses(btSoftBody::Cluster* obj, btScalar* value)
{
	VECTOR3_CONV(value);
	obj->m_dimpulses = value;
}

void btSoftBody_Cluster_setFramerefs(btSoftBody::Cluster* obj, btAlignedObjectArray* value)
{
	obj->m_framerefs = value;
}

void btSoftBody_Cluster_setFramexform(btSoftBody::Cluster* obj, void value)
{
	TRANSFORM_CONV(value);
	obj->m_framexform = value;
}
*/
void btSoftBody_Cluster_setIdmass(btSoftBody::Cluster* obj, btScalar value)
{
	obj->m_idmass = value;
}

void btSoftBody_Cluster_setImass(btSoftBody::Cluster* obj, btScalar value)
{
	obj->m_imass = value;
}
/*
void btSoftBody_Cluster_setInvwi(btSoftBody::Cluster* obj, void value)
{
	obj->m_invwi = value;
}
*/
void btSoftBody_Cluster_setLdamping(btSoftBody::Cluster* obj, btScalar value)
{
	obj->m_ldamping = value;
}

void btSoftBody_Cluster_setLeaf(btSoftBody::Cluster* obj, btDbvtNode* value)
{
	obj->m_leaf = value;
}
/*
void btSoftBody_Cluster_setLocii(btSoftBody::Cluster* obj, void value)
{
	obj->m_locii = value;
}

void btSoftBody_Cluster_setLv(btSoftBody::Cluster* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_lv = value;
}

void btSoftBody_Cluster_setMasses(btSoftBody::Cluster* obj, btAlignedObjectArray* value)
{
	obj->m_masses = value;
}
*/
void btSoftBody_Cluster_setMatching(btSoftBody::Cluster* obj, btScalar value)
{
	obj->m_matching = value;
}

void btSoftBody_Cluster_setMaxSelfCollisionImpulse(btSoftBody::Cluster* obj, btScalar value)
{
	obj->m_maxSelfCollisionImpulse = value;
}

void btSoftBody_Cluster_setNdamping(btSoftBody::Cluster* obj, btScalar value)
{
	obj->m_ndamping = value;
}

void btSoftBody_Cluster_setNdimpulses(btSoftBody::Cluster* obj, int value)
{
	obj->m_ndimpulses = value;
}
/*
void btSoftBody_Cluster_setNodes(btSoftBody::Cluster* obj, void value)
{
	obj->m_nodes = value;
}
*/
void btSoftBody_Cluster_setNvimpulses(btSoftBody::Cluster* obj, int value)
{
	obj->m_nvimpulses = value;
}

void btSoftBody_Cluster_setSelfCollisionImpulseFactor(btSoftBody::Cluster* obj, btScalar value)
{
	obj->m_selfCollisionImpulseFactor = value;
}
/*
void btSoftBody_Cluster_setVimpulses(btSoftBody::Cluster* obj, btScalar* value)
{
	VECTOR3_CONV(value);
	obj->m_vimpulses = value;
}
*/
void btSoftBody_Cluster_delete(btSoftBody::Cluster* obj)
{
	delete obj;
}

btSoftBody::Impulse* btSoftBody_Impulse_new()
{
	return new btSoftBody::Impulse();
}

int btSoftBody_Impulse_getAsDrift(btSoftBody::Impulse* obj)
{
	return obj->m_asDrift;
}

int btSoftBody_Impulse_getAsVelocity(btSoftBody::Impulse* obj)
{
	return obj->m_asVelocity;
}

void btSoftBody_Impulse_getDrift(btSoftBody::Impulse* obj)
{
	obj->m_drift;
}

void btSoftBody_Impulse_getVelocity(btSoftBody::Impulse* obj)
{
	obj->m_velocity;
}

void btSoftBody_Impulse_operator_n(btSoftBody::Impulse* obj)
{
	obj->operator-();
}

void btSoftBody_Impulse_operator_m(btSoftBody::Impulse* obj, btScalar x)
{
	obj->operator*(x);
}

void btSoftBody_Impulse_setAsDrift(btSoftBody::Impulse* obj, int value)
{
	obj->m_asDrift = value;
}

void btSoftBody_Impulse_setAsVelocity(btSoftBody::Impulse* obj, int value)
{
	obj->m_asVelocity = value;
}
/*
void btSoftBody_Impulse_setDrift(btSoftBody::Impulse* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_drift = value;
}

void btSoftBody_Impulse_setVelocity(btSoftBody::Impulse* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_velocity = value;
}
*/
void btSoftBody_Impulse_delete(btSoftBody::Impulse* obj)
{
	delete obj;
}

btSoftBody::Body* btSoftBody_Body_new()
{
	return new btSoftBody::Body();
}

btSoftBody::Body* btSoftBody_Body_new2(btCollisionObject* colObj)
{
	return new btSoftBody::Body(colObj);
}

btSoftBody::Body* btSoftBody_Body_new3(btSoftBody::Cluster* p)
{
	return new btSoftBody::Body(p);
}

void btSoftBody_Body_activate(btSoftBody::Body* obj)
{
	obj->activate();
}

void btSoftBody_Body_angularVelocity(btSoftBody::Body* obj, btScalar* rpos)
{
	VECTOR3_CONV(rpos);
	obj->angularVelocity(VECTOR3_USE(rpos));
}

void btSoftBody_Body_angularVelocity2(btSoftBody::Body* obj)
{
	obj->angularVelocity();
}

void btSoftBody_Body_applyAImpulse(btSoftBody::Body* obj, btSoftBody::Impulse* impulse)
{
	obj->applyAImpulse(*impulse);
}

void btSoftBody_Body_applyDAImpulse(btSoftBody::Body* obj, btScalar* impulse)
{
	VECTOR3_CONV(impulse);
	obj->applyDAImpulse(VECTOR3_USE(impulse));
}

void btSoftBody_Body_applyDCImpulse(btSoftBody::Body* obj, btScalar* impulse)
{
	VECTOR3_CONV(impulse);
	obj->applyDCImpulse(VECTOR3_USE(impulse));
}

void btSoftBody_Body_applyDImpulse(btSoftBody::Body* obj, btScalar* impulse, btScalar* rpos)
{
	VECTOR3_CONV(impulse);
	VECTOR3_CONV(rpos);
	obj->applyDImpulse(VECTOR3_USE(impulse), VECTOR3_USE(rpos));
}

void btSoftBody_Body_applyImpulse(btSoftBody::Body* obj, btSoftBody::Impulse* impulse, btScalar* rpos)
{
	VECTOR3_CONV(rpos);
	obj->applyImpulse(*impulse, VECTOR3_USE(rpos));
}

void btSoftBody_Body_applyVAImpulse(btSoftBody::Body* obj, btScalar* impulse)
{
	VECTOR3_CONV(impulse);
	obj->applyVAImpulse(VECTOR3_USE(impulse));
}

void btSoftBody_Body_applyVImpulse(btSoftBody::Body* obj, btScalar* impulse, btScalar* rpos)
{
	VECTOR3_CONV(impulse);
	VECTOR3_CONV(rpos);
	obj->applyVImpulse(VECTOR3_USE(impulse), VECTOR3_USE(rpos));
}
/*
btCollisionObject* btSoftBody_Body_getCollisionObject(btSoftBody::Body* obj)
{
	return obj->m_collisionObject;
}
*/
btRigidBody* btSoftBody_Body_getRigid(btSoftBody::Body* obj)
{
	return obj->m_rigid;
}

btSoftBody::Cluster* btSoftBody_Body_getSoft(btSoftBody::Body* obj)
{
	return obj->m_soft;
}

btScalar btSoftBody_Body_invMass(btSoftBody::Body* obj)
{
	return obj->invMass();
}
/*
btMatrix3x3* btSoftBody_Body_invWorldInertia(btSoftBody::Body* obj)
{
	return &obj->invWorldInertia();
}
*/
void btSoftBody_Body_linearVelocity(btSoftBody::Body* obj)
{
	obj->linearVelocity();
}

void btSoftBody_Body_setCollisionObject(btSoftBody::Body* obj, btCollisionObject* value)
{
	obj->m_collisionObject = value;
}

void btSoftBody_Body_setRigid(btSoftBody::Body* obj, btRigidBody* value)
{
	obj->m_rigid = value;
}

void btSoftBody_Body_setSoft(btSoftBody::Body* obj, btSoftBody::Cluster* value)
{
	obj->m_soft = value;
}

void btSoftBody_Body_velocity(btSoftBody::Body* obj, btScalar* rpos)
{
	VECTOR3_CONV(rpos);
	obj->velocity(VECTOR3_USE(rpos));
}
/*
btScalar* btSoftBody_Body_xform(btSoftBody::Body* obj)
{
	return &obj->xform();
}
*/
void btSoftBody_Body_delete(btSoftBody::Body* obj)
{
	delete obj;
}

btSoftBody::Joint::eType* btSoftBody_Joint_eType_new()
{
	return new btSoftBody::Joint::eType();
}

void btSoftBody_Joint_eType_delete(btSoftBody::Joint::eType* obj)
{
	delete obj;
}

btSoftBody::Joint::Specs* btSoftBody_Joint_Specs_new()
{
	return new btSoftBody::Joint::Specs();
}

btScalar btSoftBody_Joint_Specs_getCfm(btSoftBody::Joint::Specs* obj)
{
	return obj->cfm;
}

btScalar btSoftBody_Joint_Specs_getErp(btSoftBody::Joint::Specs* obj)
{
	return obj->erp;
}

btScalar btSoftBody_Joint_Specs_getSplit(btSoftBody::Joint::Specs* obj)
{
	return obj->split;
}

void btSoftBody_Joint_Specs_setCfm(btSoftBody::Joint::Specs* obj, btScalar value)
{
	obj->cfm = value;
}

void btSoftBody_Joint_Specs_setErp(btSoftBody::Joint::Specs* obj, btScalar value)
{
	obj->erp = value;
}

void btSoftBody_Joint_Specs_setSplit(btSoftBody::Joint::Specs* obj, btScalar value)
{
	obj->split = value;
}

void btSoftBody_Joint_Specs_delete(btSoftBody::Joint::Specs* obj)
{
	delete obj;
}

btSoftBody::Body* btSoftBody_Joint_getBodies(btSoftBody::Joint* obj)
{
	return obj->m_bodies;
}

btScalar btSoftBody_Joint_getCfm(btSoftBody::Joint* obj)
{
	return obj->m_cfm;
}

bool btSoftBody_Joint_getDelete(btSoftBody::Joint* obj)
{
	return obj->m_delete;
}

void btSoftBody_Joint_getDrift(btSoftBody::Joint* obj)
{
	obj->m_drift;
}

btScalar btSoftBody_Joint_getErp(btSoftBody::Joint* obj)
{
	return obj->m_erp;
}

void btSoftBody_Joint_getMassmatrix(btSoftBody::Joint* obj)
{
	obj->m_massmatrix;
}
/*
btScalar* btSoftBody_Joint_getRefs(btSoftBody::Joint* obj)
{
	return obj->m_refs;
}
*/
void btSoftBody_Joint_getSdrift(btSoftBody::Joint* obj)
{
	obj->m_sdrift;
}

btScalar btSoftBody_Joint_getSplit(btSoftBody::Joint* obj)
{
	return obj->m_split;
}

void btSoftBody_Joint_Prepare(btSoftBody::Joint* obj, btScalar dt, int iterations)
{
	obj->Prepare(dt, iterations);
}
/*
void btSoftBody_Joint_setBodies(btSoftBody::Joint* obj, btSoftBody::Body* value)
{
	obj->m_bodies = value;
}
*/
void btSoftBody_Joint_setCfm(btSoftBody::Joint* obj, btScalar value)
{
	obj->m_cfm = value;
}

void btSoftBody_Joint_setDelete(btSoftBody::Joint* obj, bool value)
{
	obj->m_delete = value;
}
/*
void btSoftBody_Joint_setDrift(btSoftBody::Joint* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_drift = value;
}
*/
void btSoftBody_Joint_setErp(btSoftBody::Joint* obj, btScalar value)
{
	obj->m_erp = value;
}
/*
void btSoftBody_Joint_setMassmatrix(btSoftBody::Joint* obj, void value)
{
	obj->m_massmatrix = value;
}

void btSoftBody_Joint_setRefs(btSoftBody::Joint* obj, btScalar* value)
{
	VECTOR3_CONV(value);
	obj->m_refs = value;
}

void btSoftBody_Joint_setSdrift(btSoftBody::Joint* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_sdrift = value;
}
*/
void btSoftBody_Joint_setSplit(btSoftBody::Joint* obj, btScalar value)
{
	obj->m_split = value;
}

void btSoftBody_Joint_Solve(btSoftBody::Joint* obj, btScalar dt, btScalar sor)
{
	obj->Solve(dt, sor);
}

void btSoftBody_Joint_Terminate(btSoftBody::Joint* obj, btScalar dt)
{
	obj->Terminate(dt);
}

void btSoftBody_Joint_Type(btSoftBody::Joint* obj)
{
	obj->Type();
}

void btSoftBody_Joint_delete(btSoftBody::Joint* obj)
{
	delete obj;
}

btSoftBody::LJoint* btSoftBody_LJoint_new()
{
	return new btSoftBody::LJoint();
}
/*
btScalar* btSoftBody_LJoint_getRpos(btSoftBody::LJoint* obj)
{
	return obj->m_rpos;
}

void btSoftBody_LJoint_setRpos(btSoftBody::LJoint* obj, btScalar* value)
{
	VECTOR3_CONV(value);
	obj->m_rpos = value;
}
*/
btSoftBody::AJoint::IControl* btSoftBody_AJoint_IControl_new()
{
	return new btSoftBody::AJoint::IControl();
}

btSoftBody::AJoint::IControl* btSoftBody_AJoint_IControl_Default()
{
	return btSoftBody::AJoint::IControl::Default();
}

void btSoftBody_AJoint_IControl_Prepare(btSoftBody::AJoint::IControl* obj, btSoftBody::AJoint* __unnamed0)
{
	obj->Prepare(__unnamed0);
}

btScalar btSoftBody_AJoint_IControl_Speed(btSoftBody::AJoint::IControl* obj, btSoftBody::AJoint* __unnamed0, btScalar current)
{
	return obj->Speed(__unnamed0, current);
}

void btSoftBody_AJoint_IControl_delete(btSoftBody::AJoint::IControl* obj)
{
	delete obj;
}

btSoftBody::AJoint* btSoftBody_AJoint_new()
{
	return new btSoftBody::AJoint();
}
/*
btScalar* btSoftBody_AJoint_getAxis(btSoftBody::AJoint* obj)
{
	return obj->m_axis;
}
*/
btSoftBody::AJoint::IControl* btSoftBody_AJoint_getIcontrol(btSoftBody::AJoint* obj)
{
	return obj->m_icontrol;
}
/*
void btSoftBody_AJoint_setAxis(btSoftBody::AJoint* obj, btScalar* value)
{
	VECTOR3_CONV(value);
	obj->m_axis = value;
}
*/
void btSoftBody_AJoint_setIcontrol(btSoftBody::AJoint* obj, btSoftBody::AJoint::IControl* value)
{
	obj->m_icontrol = value;
}

btSoftBody::CJoint* btSoftBody_CJoint_new()
{
	return new btSoftBody::CJoint();
}

btScalar btSoftBody_CJoint_getFriction(btSoftBody::CJoint* obj)
{
	return obj->m_friction;
}

int btSoftBody_CJoint_getLife(btSoftBody::CJoint* obj)
{
	return obj->m_life;
}

int btSoftBody_CJoint_getMaxlife(btSoftBody::CJoint* obj)
{
	return obj->m_maxlife;
}

void btSoftBody_CJoint_getNormal(btSoftBody::CJoint* obj)
{
	obj->m_normal;
}
/*
btScalar* btSoftBody_CJoint_getRpos(btSoftBody::CJoint* obj)
{
	return obj->m_rpos;
}
*/
void btSoftBody_CJoint_setFriction(btSoftBody::CJoint* obj, btScalar value)
{
	obj->m_friction = value;
}

void btSoftBody_CJoint_setLife(btSoftBody::CJoint* obj, int value)
{
	obj->m_life = value;
}

void btSoftBody_CJoint_setMaxlife(btSoftBody::CJoint* obj, int value)
{
	obj->m_maxlife = value;
}
/*
void btSoftBody_CJoint_setNormal(btSoftBody::CJoint* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_normal = value;
}

void btSoftBody_CJoint_setRpos(btSoftBody::CJoint* obj, btScalar* value)
{
	VECTOR3_CONV(value);
	obj->m_rpos = value;
}
*/
btSoftBody::Config* btSoftBody_Config_new()
{
	return new btSoftBody::Config();
}

void btSoftBody_Config_getAeromodel(btSoftBody::Config* obj)
{
	obj->aeromodel;
}

int btSoftBody_Config_getCiterations(btSoftBody::Config* obj)
{
	return obj->citerations;
}

int btSoftBody_Config_getCollisions(btSoftBody::Config* obj)
{
	return obj->collisions;
}

int btSoftBody_Config_getDiterations(btSoftBody::Config* obj)
{
	return obj->diterations;
}
/*
btAlignedObjectArray* btSoftBody_Config_getDsequence(btSoftBody::Config* obj)
{
	return obj->m_dsequence;
}
*/
btScalar btSoftBody_Config_getKAHR(btSoftBody::Config* obj)
{
	return obj->kAHR;
}

btScalar btSoftBody_Config_getKCHR(btSoftBody::Config* obj)
{
	return obj->kCHR;
}

btScalar btSoftBody_Config_getKDF(btSoftBody::Config* obj)
{
	return obj->kDF;
}

btScalar btSoftBody_Config_getKDG(btSoftBody::Config* obj)
{
	return obj->kDG;
}

btScalar btSoftBody_Config_getKDP(btSoftBody::Config* obj)
{
	return obj->kDP;
}

btScalar btSoftBody_Config_getKKHR(btSoftBody::Config* obj)
{
	return obj->kKHR;
}

btScalar btSoftBody_Config_getKLF(btSoftBody::Config* obj)
{
	return obj->kLF;
}

btScalar btSoftBody_Config_getKMT(btSoftBody::Config* obj)
{
	return obj->kMT;
}

btScalar btSoftBody_Config_getKPR(btSoftBody::Config* obj)
{
	return obj->kPR;
}

btScalar btSoftBody_Config_getKSHR(btSoftBody::Config* obj)
{
	return obj->kSHR;
}

btScalar btSoftBody_Config_getKSK_SPLT_CL(btSoftBody::Config* obj)
{
	return obj->kSK_SPLT_CL;
}

btScalar btSoftBody_Config_getKSKHR_CL(btSoftBody::Config* obj)
{
	return obj->kSKHR_CL;
}

btScalar btSoftBody_Config_getKSR_SPLT_CL(btSoftBody::Config* obj)
{
	return obj->kSR_SPLT_CL;
}

btScalar btSoftBody_Config_getKSRHR_CL(btSoftBody::Config* obj)
{
	return obj->kSRHR_CL;
}

btScalar btSoftBody_Config_getKSS_SPLT_CL(btSoftBody::Config* obj)
{
	return obj->kSS_SPLT_CL;
}

btScalar btSoftBody_Config_getKSSHR_CL(btSoftBody::Config* obj)
{
	return obj->kSSHR_CL;
}

btScalar btSoftBody_Config_getKVC(btSoftBody::Config* obj)
{
	return obj->kVC;
}

btScalar btSoftBody_Config_getKVCF(btSoftBody::Config* obj)
{
	return obj->kVCF;
}

btScalar btSoftBody_Config_getMaxvolume(btSoftBody::Config* obj)
{
	return obj->maxvolume;
}

int btSoftBody_Config_getPiterations(btSoftBody::Config* obj)
{
	return obj->piterations;
}
/*
btAlignedObjectArray* btSoftBody_Config_getPsequence(btSoftBody::Config* obj)
{
	return obj->m_psequence;
}
*/
btScalar btSoftBody_Config_getTimescale(btSoftBody::Config* obj)
{
	return obj->timescale;
}

int btSoftBody_Config_getViterations(btSoftBody::Config* obj)
{
	return obj->viterations;
}
/*
btAlignedObjectArray* btSoftBody_Config_getVsequence(btSoftBody::Config* obj)
{
	return obj->m_vsequence;
}

void btSoftBody_Config_setAeromodel(btSoftBody::Config* obj, void value)
{
	obj->aeromodel = value;
}
*/
void btSoftBody_Config_setCiterations(btSoftBody::Config* obj, int value)
{
	obj->citerations = value;
}

void btSoftBody_Config_setCollisions(btSoftBody::Config* obj, int value)
{
	obj->collisions = value;
}

void btSoftBody_Config_setDiterations(btSoftBody::Config* obj, int value)
{
	obj->diterations = value;
}
/*
void btSoftBody_Config_setDsequence(btSoftBody::Config* obj, btAlignedObjectArray* value)
{
	obj->m_dsequence = value;
}
*/
void btSoftBody_Config_setKAHR(btSoftBody::Config* obj, btScalar value)
{
	obj->kAHR = value;
}

void btSoftBody_Config_setKCHR(btSoftBody::Config* obj, btScalar value)
{
	obj->kCHR = value;
}

void btSoftBody_Config_setKDF(btSoftBody::Config* obj, btScalar value)
{
	obj->kDF = value;
}

void btSoftBody_Config_setKDG(btSoftBody::Config* obj, btScalar value)
{
	obj->kDG = value;
}

void btSoftBody_Config_setKDP(btSoftBody::Config* obj, btScalar value)
{
	obj->kDP = value;
}

void btSoftBody_Config_setKKHR(btSoftBody::Config* obj, btScalar value)
{
	obj->kKHR = value;
}

void btSoftBody_Config_setKLF(btSoftBody::Config* obj, btScalar value)
{
	obj->kLF = value;
}

void btSoftBody_Config_setKMT(btSoftBody::Config* obj, btScalar value)
{
	obj->kMT = value;
}

void btSoftBody_Config_setKPR(btSoftBody::Config* obj, btScalar value)
{
	obj->kPR = value;
}

void btSoftBody_Config_setKSHR(btSoftBody::Config* obj, btScalar value)
{
	obj->kSHR = value;
}

void btSoftBody_Config_setKSK_SPLT_CL(btSoftBody::Config* obj, btScalar value)
{
	obj->kSK_SPLT_CL = value;
}

void btSoftBody_Config_setKSKHR_CL(btSoftBody::Config* obj, btScalar value)
{
	obj->kSKHR_CL = value;
}

void btSoftBody_Config_setKSR_SPLT_CL(btSoftBody::Config* obj, btScalar value)
{
	obj->kSR_SPLT_CL = value;
}

void btSoftBody_Config_setKSRHR_CL(btSoftBody::Config* obj, btScalar value)
{
	obj->kSRHR_CL = value;
}

void btSoftBody_Config_setKSS_SPLT_CL(btSoftBody::Config* obj, btScalar value)
{
	obj->kSS_SPLT_CL = value;
}

void btSoftBody_Config_setKSSHR_CL(btSoftBody::Config* obj, btScalar value)
{
	obj->kSSHR_CL = value;
}

void btSoftBody_Config_setKVC(btSoftBody::Config* obj, btScalar value)
{
	obj->kVC = value;
}

void btSoftBody_Config_setKVCF(btSoftBody::Config* obj, btScalar value)
{
	obj->kVCF = value;
}

void btSoftBody_Config_setMaxvolume(btSoftBody::Config* obj, btScalar value)
{
	obj->maxvolume = value;
}

void btSoftBody_Config_setPiterations(btSoftBody::Config* obj, int value)
{
	obj->piterations = value;
}
/*
void btSoftBody_Config_setPsequence(btSoftBody::Config* obj, btAlignedObjectArray* value)
{
	obj->m_psequence = value;
}
*/
void btSoftBody_Config_setTimescale(btSoftBody::Config* obj, btScalar value)
{
	obj->timescale = value;
}

void btSoftBody_Config_setViterations(btSoftBody::Config* obj, int value)
{
	obj->viterations = value;
}
/*
void btSoftBody_Config_setVsequence(btSoftBody::Config* obj, btAlignedObjectArray* value)
{
	obj->m_vsequence = value;
}
*/
void btSoftBody_Config_delete(btSoftBody::Config* obj)
{
	delete obj;
}

btSoftBody::SolverState* btSoftBody_SolverState_new()
{
	return new btSoftBody::SolverState();
}

btScalar btSoftBody_SolverState_getIsdt(btSoftBody::SolverState* obj)
{
	return obj->isdt;
}

btScalar btSoftBody_SolverState_getRadmrg(btSoftBody::SolverState* obj)
{
	return obj->radmrg;
}

btScalar btSoftBody_SolverState_getSdt(btSoftBody::SolverState* obj)
{
	return obj->sdt;
}

btScalar btSoftBody_SolverState_getUpdmrg(btSoftBody::SolverState* obj)
{
	return obj->updmrg;
}

btScalar btSoftBody_SolverState_getVelmrg(btSoftBody::SolverState* obj)
{
	return obj->velmrg;
}

void btSoftBody_SolverState_setIsdt(btSoftBody::SolverState* obj, btScalar value)
{
	obj->isdt = value;
}

void btSoftBody_SolverState_setRadmrg(btSoftBody::SolverState* obj, btScalar value)
{
	obj->radmrg = value;
}

void btSoftBody_SolverState_setSdt(btSoftBody::SolverState* obj, btScalar value)
{
	obj->sdt = value;
}

void btSoftBody_SolverState_setUpdmrg(btSoftBody::SolverState* obj, btScalar value)
{
	obj->updmrg = value;
}

void btSoftBody_SolverState_setVelmrg(btSoftBody::SolverState* obj, btScalar value)
{
	obj->velmrg = value;
}

void btSoftBody_SolverState_delete(btSoftBody::SolverState* obj)
{
	delete obj;
}

btSoftBody::RayFromToCaster* btSoftBody_RayFromToCaster_new(btScalar* rayFrom, btScalar* rayTo, btScalar mxt)
{
	VECTOR3_CONV(rayFrom);
	VECTOR3_CONV(rayTo);
	return new btSoftBody::RayFromToCaster(VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), mxt);
}

btSoftBody::Face* btSoftBody_RayFromToCaster_getFace(btSoftBody::RayFromToCaster* obj)
{
	return obj->m_face;
}

btScalar btSoftBody_RayFromToCaster_getMint(btSoftBody::RayFromToCaster* obj)
{
	return obj->m_mint;
}

void btSoftBody_RayFromToCaster_getRayFrom(btSoftBody::RayFromToCaster* obj)
{
	obj->m_rayFrom;
}

void btSoftBody_RayFromToCaster_getRayNormalizedDirection(btSoftBody::RayFromToCaster* obj)
{
	obj->m_rayNormalizedDirection;
}

void btSoftBody_RayFromToCaster_getRayTo(btSoftBody::RayFromToCaster* obj)
{
	obj->m_rayTo;
}

int btSoftBody_RayFromToCaster_getTests(btSoftBody::RayFromToCaster* obj)
{
	return obj->m_tests;
}
/*
btScalar btSoftBody_RayFromToCaster_rayFromToTriangle(btScalar* rayFrom, btScalar* rayTo, btScalar* rayNormalizedDirection, btScalar* a, btScalar* b, btScalar* c, btScalar maxt)
{
	VECTOR3_CONV(rayFrom);
	VECTOR3_CONV(rayTo);
	VECTOR3_CONV(rayNormalizedDirection);
	VECTOR3_CONV(a);
	VECTOR3_CONV(b);
	VECTOR3_CONV(c);
	return btSoftBody::RayFromToCaster::rayFromToTriangle(VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), VECTOR3_USE(rayNormalizedDirection), VECTOR3_USE(a), VECTOR3_USE(b), VECTOR3_USE(c), maxt);
}

btScalar btSoftBody_RayFromToCaster_rayFromToTriangle2(btScalar* rayFrom, btScalar* rayTo, btScalar* rayNormalizedDirection, btScalar* a, btScalar* b, btScalar* c)
{
	VECTOR3_CONV(rayFrom);
	VECTOR3_CONV(rayTo);
	VECTOR3_CONV(rayNormalizedDirection);
	VECTOR3_CONV(a);
	VECTOR3_CONV(b);
	VECTOR3_CONV(c);
	return btSoftBody::RayFromToCaster::rayFromToTriangle(VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), VECTOR3_USE(rayNormalizedDirection), VECTOR3_USE(a), VECTOR3_USE(b), VECTOR3_USE(c));
}
*/
void btSoftBody_RayFromToCaster_setFace(btSoftBody::RayFromToCaster* obj, btSoftBody::Face* value)
{
	obj->m_face = value;
}

void btSoftBody_RayFromToCaster_setMint(btSoftBody::RayFromToCaster* obj, btScalar value)
{
	obj->m_mint = value;
}
/*
void btSoftBody_RayFromToCaster_setRayFrom(btSoftBody::RayFromToCaster* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_rayFrom = value;
}

void btSoftBody_RayFromToCaster_setRayNormalizedDirection(btSoftBody::RayFromToCaster* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_rayNormalizedDirection = value;
}

void btSoftBody_RayFromToCaster_setRayTo(btSoftBody::RayFromToCaster* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_rayTo = value;
}
*/
void btSoftBody_RayFromToCaster_setTests(btSoftBody::RayFromToCaster* obj, int value)
{
	obj->m_tests = value;
}
/*
btSoftBody* btSoftBody_new(btSoftBodyWorldInfo* worldInfo, int node_count, btScalar* x, btScalar* m)
{
	VECTOR3_CONV(x);
	return new btSoftBody(worldInfo, node_count, VECTOR3_USE(x), m);
}
*/
btSoftBody* btSoftBody_new2(btSoftBodyWorldInfo* worldInfo)
{
	return new btSoftBody(worldInfo);
}

void btSoftBody_addAeroForceToFace(btSoftBody* obj, btScalar* windVelocity, int faceIndex)
{
	VECTOR3_CONV(windVelocity);
	obj->addAeroForceToFace(VECTOR3_USE(windVelocity), faceIndex);
}

void btSoftBody_addAeroForceToNode(btSoftBody* obj, btScalar* windVelocity, int nodeIndex)
{
	VECTOR3_CONV(windVelocity);
	obj->addAeroForceToNode(VECTOR3_USE(windVelocity), nodeIndex);
}

void btSoftBody_addForce(btSoftBody* obj, btScalar* force)
{
	VECTOR3_CONV(force);
	obj->addForce(VECTOR3_USE(force));
}

void btSoftBody_addForce2(btSoftBody* obj, btScalar* force, int node)
{
	VECTOR3_CONV(force);
	obj->addForce(VECTOR3_USE(force), node);
}

void btSoftBody_addVelocity(btSoftBody* obj, btScalar* velocity, int node)
{
	VECTOR3_CONV(velocity);
	obj->addVelocity(VECTOR3_USE(velocity), node);
}

void btSoftBody_addVelocity2(btSoftBody* obj, btScalar* velocity)
{
	VECTOR3_CONV(velocity);
	obj->addVelocity(VECTOR3_USE(velocity));
}

void btSoftBody_appendAnchor(btSoftBody* obj, int node, btRigidBody* body, btScalar* localPivot, bool disableCollisionBetweenLinkedBodies, btScalar influence)
{
	VECTOR3_CONV(localPivot);
	obj->appendAnchor(node, body, VECTOR3_USE(localPivot), disableCollisionBetweenLinkedBodies, influence);
}

void btSoftBody_appendAnchor2(btSoftBody* obj, int node, btRigidBody* body, btScalar* localPivot, bool disableCollisionBetweenLinkedBodies)
{
	VECTOR3_CONV(localPivot);
	obj->appendAnchor(node, body, VECTOR3_USE(localPivot), disableCollisionBetweenLinkedBodies);
}

void btSoftBody_appendAnchor3(btSoftBody* obj, int node, btRigidBody* body, btScalar* localPivot)
{
	VECTOR3_CONV(localPivot);
	obj->appendAnchor(node, body, VECTOR3_USE(localPivot));
}

void btSoftBody_appendAnchor4(btSoftBody* obj, int node, btRigidBody* body, bool disableCollisionBetweenLinkedBodies, btScalar influence)
{
	obj->appendAnchor(node, body, disableCollisionBetweenLinkedBodies, influence);
}

void btSoftBody_appendAnchor5(btSoftBody* obj, int node, btRigidBody* body, bool disableCollisionBetweenLinkedBodies)
{
	obj->appendAnchor(node, body, disableCollisionBetweenLinkedBodies);
}

void btSoftBody_appendAnchor6(btSoftBody* obj, int node, btRigidBody* body)
{
	obj->appendAnchor(node, body);
}
/*
void btSoftBody_appendAngularJoint(btSoftBody* obj, btSoftBody::Joint::Specs* specs, void body)
{
	obj->appendAngularJoint(*specs, body);
}

void btSoftBody_appendAngularJoint2(btSoftBody* obj, btSoftBody::Joint::Specs* specs)
{
	obj->appendAngularJoint(*specs);
}

void btSoftBody_appendAngularJoint3(btSoftBody* obj, btSoftBody::Joint::Specs* specs, btSoftBody::Cluster* body0, void body1)
{
	obj->appendAngularJoint(*specs, body0, body1);
}

void btSoftBody_appendAngularJoint4(btSoftBody* obj, btSoftBody::Joint::Specs* specs, btSoftBody* body)
{
	obj->appendAngularJoint(*specs, body);
}
*/
void btSoftBody_appendFace(btSoftBody* obj, int model, btSoftBody::Material* mat)
{
	obj->appendFace(model, mat);
}

void btSoftBody_appendFace2(btSoftBody* obj, int model)
{
	obj->appendFace(model);
}

void btSoftBody_appendFace3(btSoftBody* obj)
{
	obj->appendFace();
}

void btSoftBody_appendFace4(btSoftBody* obj, int node0, int node1, int node2, btSoftBody::Material* mat)
{
	obj->appendFace(node0, node1, node2, mat);
}

void btSoftBody_appendFace5(btSoftBody* obj, int node0, int node1, int node2)
{
	obj->appendFace(node0, node1, node2);
}
/*
void btSoftBody_appendLinearJoint(btSoftBody* obj, btSoftBody::Joint::Specs* specs, btSoftBody* body)
{
	obj->appendLinearJoint(*specs, body);
}

void btSoftBody_appendLinearJoint2(btSoftBody* obj, btSoftBody::Joint::Specs* specs, void body)
{
	obj->appendLinearJoint(*specs, body);
}

void btSoftBody_appendLinearJoint3(btSoftBody* obj, btSoftBody::Joint::Specs* specs)
{
	obj->appendLinearJoint(*specs);
}

void btSoftBody_appendLinearJoint4(btSoftBody* obj, btSoftBody::Joint::Specs* specs, btSoftBody::Cluster* body0, void body1)
{
	obj->appendLinearJoint(*specs, body0, body1);
}
*/
void btSoftBody_appendLink(btSoftBody* obj, int node0, int node1, btSoftBody::Material* mat, bool bcheckexist)
{
	obj->appendLink(node0, node1, mat, bcheckexist);
}

void btSoftBody_appendLink2(btSoftBody* obj, int node0, int node1, btSoftBody::Material* mat)
{
	obj->appendLink(node0, node1, mat);
}

void btSoftBody_appendLink3(btSoftBody* obj, int node0, int node1)
{
	obj->appendLink(node0, node1);
}

void btSoftBody_appendLink4(btSoftBody* obj, int model, btSoftBody::Material* mat)
{
	obj->appendLink(model, mat);
}

void btSoftBody_appendLink5(btSoftBody* obj, int model)
{
	obj->appendLink(model);
}

void btSoftBody_appendLink6(btSoftBody* obj)
{
	obj->appendLink();
}

void btSoftBody_appendLink7(btSoftBody* obj, btSoftBody::Node* node0, btSoftBody::Node* node1, btSoftBody::Material* mat, bool bcheckexist)
{
	obj->appendLink(node0, node1, mat, bcheckexist);
}

void btSoftBody_appendLink8(btSoftBody* obj, btSoftBody::Node* node0, btSoftBody::Node* node1, btSoftBody::Material* mat)
{
	obj->appendLink(node0, node1, mat);
}

void btSoftBody_appendLink9(btSoftBody* obj, btSoftBody::Node* node0, btSoftBody::Node* node1)
{
	obj->appendLink(node0, node1);
}

btSoftBody::Material* btSoftBody_appendMaterial(btSoftBody* obj)
{
	return obj->appendMaterial();
}

void btSoftBody_appendNode(btSoftBody* obj, btScalar* x, btScalar m)
{
	VECTOR3_CONV(x);
	obj->appendNode(VECTOR3_USE(x), m);
}

void btSoftBody_appendNote(btSoftBody* obj, char* text, btScalar* o, btSoftBody::Face* feature)
{
	VECTOR3_CONV(o);
	obj->appendNote(text, VECTOR3_USE(o), feature);
}

void btSoftBody_appendNote2(btSoftBody* obj, char* text, btScalar* o, btSoftBody::Link* feature)
{
	VECTOR3_CONV(o);
	obj->appendNote(text, VECTOR3_USE(o), feature);
}

void btSoftBody_appendNote3(btSoftBody* obj, char* text, btScalar* o, btSoftBody::Node* feature)
{
	VECTOR3_CONV(o);
	obj->appendNote(text, VECTOR3_USE(o), feature);
}

void btSoftBody_appendNote4(btSoftBody* obj, char* text, btScalar* o, btVector4* c, btSoftBody::Node* n0, btSoftBody::Node* n1, btSoftBody::Node* n2, btSoftBody::Node* n3)
{
	VECTOR3_CONV(o);
	obj->appendNote(text, VECTOR3_USE(o), *c, n0, n1, n2, n3);
}

void btSoftBody_appendNote5(btSoftBody* obj, char* text, btScalar* o, btVector4* c, btSoftBody::Node* n0, btSoftBody::Node* n1, btSoftBody::Node* n2)
{
	VECTOR3_CONV(o);
	obj->appendNote(text, VECTOR3_USE(o), *c, n0, n1, n2);
}

void btSoftBody_appendNote6(btSoftBody* obj, char* text, btScalar* o, btVector4* c, btSoftBody::Node* n0, btSoftBody::Node* n1)
{
	VECTOR3_CONV(o);
	obj->appendNote(text, VECTOR3_USE(o), *c, n0, n1);
}

void btSoftBody_appendNote7(btSoftBody* obj, char* text, btScalar* o, btVector4* c, btSoftBody::Node* n0)
{
	VECTOR3_CONV(o);
	obj->appendNote(text, VECTOR3_USE(o), *c, n0);
}

void btSoftBody_appendNote8(btSoftBody* obj, char* text, btScalar* o, btVector4* c)
{
	VECTOR3_CONV(o);
	obj->appendNote(text, VECTOR3_USE(o), *c);
}

void btSoftBody_appendNote9(btSoftBody* obj, char* text, btScalar* o)
{
	VECTOR3_CONV(o);
	obj->appendNote(text, VECTOR3_USE(o));
}

void btSoftBody_appendTetra(btSoftBody* obj, int model, btSoftBody::Material* mat)
{
	obj->appendTetra(model, mat);
}

void btSoftBody_appendTetra2(btSoftBody* obj, int node0, int node1, int node2, int node3, btSoftBody::Material* mat)
{
	obj->appendTetra(node0, node1, node2, node3, mat);
}

void btSoftBody_appendTetra3(btSoftBody* obj, int node0, int node1, int node2, int node3)
{
	obj->appendTetra(node0, node1, node2, node3);
}

void btSoftBody_applyClusters(btSoftBody* obj, bool drift)
{
	obj->applyClusters(drift);
}

void btSoftBody_applyForces(btSoftBody* obj)
{
	obj->applyForces();
}

bool btSoftBody_checkContact(btSoftBody* obj, btCollisionObjectWrapper* colObjWrap, btScalar* x, btScalar margin, btSoftBody::sCti* cti)
{
	VECTOR3_CONV(x);
	return obj->checkContact(colObjWrap, VECTOR3_USE(x), margin, *cti);
}

bool btSoftBody_checkFace(btSoftBody* obj, int node0, int node1, int node2)
{
	return obj->checkFace(node0, node1, node2);
}

bool btSoftBody_checkLink(btSoftBody* obj, btSoftBody::Node* node0, btSoftBody::Node* node1)
{
	return obj->checkLink(node0, node1);
}

bool btSoftBody_checkLink2(btSoftBody* obj, int node0, int node1)
{
	return obj->checkLink(node0, node1);
}

void btSoftBody_cleanupClusters(btSoftBody* obj)
{
	obj->cleanupClusters();
}

void btSoftBody_clusterAImpulse(btSoftBody::Cluster* cluster, btSoftBody::Impulse* impulse)
{
	btSoftBody::clusterAImpulse(cluster, *impulse);
}

void btSoftBody_clusterCom(btSoftBody::Cluster* cluster)
{
	btSoftBody::clusterCom(cluster);
}

void btSoftBody_clusterCom2(btSoftBody* obj, int cluster)
{
	obj->clusterCom(cluster);
}

int btSoftBody_clusterCount(btSoftBody* obj)
{
	return obj->clusterCount();
}

void btSoftBody_clusterDAImpulse(btSoftBody::Cluster* cluster, btScalar* impulse)
{
	VECTOR3_CONV(impulse);
	btSoftBody::clusterDAImpulse(cluster, VECTOR3_USE(impulse));
}

void btSoftBody_clusterDCImpulse(btSoftBody::Cluster* cluster, btScalar* impulse)
{
	VECTOR3_CONV(impulse);
	btSoftBody::clusterDCImpulse(cluster, VECTOR3_USE(impulse));
}

void btSoftBody_clusterDImpulse(btSoftBody::Cluster* cluster, btScalar* rpos, btScalar* impulse)
{
	VECTOR3_CONV(rpos);
	VECTOR3_CONV(impulse);
	btSoftBody::clusterDImpulse(cluster, VECTOR3_USE(rpos), VECTOR3_USE(impulse));
}

void btSoftBody_clusterImpulse(btSoftBody::Cluster* cluster, btScalar* rpos, btSoftBody::Impulse* impulse)
{
	VECTOR3_CONV(rpos);
	btSoftBody::clusterImpulse(cluster, VECTOR3_USE(rpos), *impulse);
}

void btSoftBody_clusterVAImpulse(btSoftBody::Cluster* cluster, btScalar* impulse)
{
	VECTOR3_CONV(impulse);
	btSoftBody::clusterVAImpulse(cluster, VECTOR3_USE(impulse));
}

void btSoftBody_clusterVelocity(btSoftBody::Cluster* cluster, btScalar* rpos)
{
	VECTOR3_CONV(rpos);
	btSoftBody::clusterVelocity(cluster, VECTOR3_USE(rpos));
}

void btSoftBody_clusterVImpulse(btSoftBody::Cluster* cluster, btScalar* rpos, btScalar* impulse)
{
	VECTOR3_CONV(rpos);
	VECTOR3_CONV(impulse);
	btSoftBody::clusterVImpulse(cluster, VECTOR3_USE(rpos), VECTOR3_USE(impulse));
}

bool btSoftBody_cutLink(btSoftBody* obj, int node0, int node1, btScalar position)
{
	return obj->cutLink(node0, node1, position);
}

bool btSoftBody_cutLink2(btSoftBody* obj, btSoftBody::Node* node0, btSoftBody::Node* node1, btScalar position)
{
	return obj->cutLink(node0, node1, position);
}

void btSoftBody_dampClusters(btSoftBody* obj)
{
	obj->dampClusters();
}

void btSoftBody_defaultCollisionHandler(btSoftBody* obj, btCollisionObjectWrapper* pcoWrap)
{
	obj->defaultCollisionHandler(pcoWrap);
}

void btSoftBody_defaultCollisionHandler2(btSoftBody* obj, btSoftBody* psb)
{
	obj->defaultCollisionHandler(psb);
}

void btSoftBody_evaluateCom(btSoftBody* obj)
{
	obj->evaluateCom();
}

int btSoftBody_generateBendingConstraints(btSoftBody* obj, int distance, btSoftBody::Material* mat)
{
	return obj->generateBendingConstraints(distance, mat);
}

int btSoftBody_generateBendingConstraints2(btSoftBody* obj, int distance)
{
	return obj->generateBendingConstraints(distance);
}

int btSoftBody_generateClusters(btSoftBody* obj, int k, int maxiterations)
{
	return obj->generateClusters(k, maxiterations);
}

int btSoftBody_generateClusters2(btSoftBody* obj, int k)
{
	return obj->generateClusters(k);
}

void btSoftBody_getAabb(btSoftBody* obj, btScalar* aabbMin, btScalar* aabbMax)
{
	VECTOR3_CONV(aabbMin);
	VECTOR3_CONV(aabbMax);
	obj->getAabb(VECTOR3_USE(aabbMin), VECTOR3_USE(aabbMax));
}
/*
btAlignedObjectArray* btSoftBody_getAnchors(btSoftBody* obj)
{
	return obj->m_anchors;
}

btScalar* btSoftBody_getBounds(btSoftBody* obj)
{
	return obj->m_bounds;
}
*/
bool btSoftBody_getBUpdateRtCst(btSoftBody* obj)
{
	return obj->m_bUpdateRtCst;
}

void btSoftBody_getCdbvt(btSoftBody* obj)
{
	obj->m_cdbvt;
}

void btSoftBody_getCfg(btSoftBody* obj)
{
	obj->m_cfg;
}

void btSoftBody_getClusterConnectivity(btSoftBody* obj)
{
	obj->m_clusterConnectivity;
}
/*
btAlignedObjectArray* btSoftBody_getClusters(btSoftBody* obj)
{
	return obj->m_clusters;
}
*/
void btSoftBody_getCollisionDisabledObjects(btSoftBody* obj)
{
	obj->m_collisionDisabledObjects;
}
/*
btAlignedObjectArray* btSoftBody_getFaces(btSoftBody* obj)
{
	return obj->m_faces;
}
*/
void btSoftBody_getFdbvt(btSoftBody* obj)
{
	obj->m_fdbvt;
}

void btSoftBody_getInitialWorldTransform(btSoftBody* obj)
{
	obj->m_initialWorldTransform;
}
/*
btAlignedObjectArray* btSoftBody_getJoints(btSoftBody* obj)
{
	return obj->m_joints;
}

btAlignedObjectArray* btSoftBody_getLinks(btSoftBody* obj)
{
	return obj->m_links;
}

btScalar btSoftBody_getMass(btSoftBody* obj, int node)
{
	return obj->getMass(node);
}

btAlignedObjectArray* btSoftBody_getMaterials(btSoftBody* obj)
{
	return obj->m_materials;
}

void btSoftBody_getNdbvt(btSoftBody* obj)
{
	obj->m_ndbvt;
}

btAlignedObjectArray* btSoftBody_getNodes(btSoftBody* obj)
{
	return obj->m_nodes;
}

btAlignedObjectArray* btSoftBody_getNotes(btSoftBody* obj)
{
	return obj->m_notes;
}

void btSoftBody_getPose(btSoftBody* obj)
{
	obj->m_pose;
}

btAlignedObjectArray* btSoftBody_getRcontacts(btSoftBody* obj)
{
	return obj->m_rcontacts;
}
*/
btScalar btSoftBody_getRestLengthScale(btSoftBody* obj)
{
	return obj->getRestLengthScale();
}

btScalar btSoftBody_getRestLengthScale2(btSoftBody* obj)
{
	return obj->m_restLengthScale;
}
/*
btAlignedObjectArray* btSoftBody_getScontacts(btSoftBody* obj)
{
	return obj->m_scontacts;
}
*/
btSoftBodySolver* btSoftBody_getSoftBodySolver(btSoftBody* obj)
{
	return obj->m_softBodySolver;
}

btSoftBodySolver* btSoftBody_getSoftBodySolver2(btSoftBody* obj)
{
	return obj->getSoftBodySolver();
}
/*
* btSoftBody_getSolver(void solver)
{
	return btSoftBody::getSolver(solver);
}

* btSoftBody_getSolver2(void solver)
{
	return btSoftBody::getSolver(solver);
}

void btSoftBody_getSst(btSoftBody* obj)
{
	obj->m_sst;
}
*/
void* btSoftBody_getTag(btSoftBody* obj)
{
	return obj->m_tag;
}
/*
btAlignedObjectArray* btSoftBody_getTetras(btSoftBody* obj)
{
	return obj->m_tetras;
}
*/
btScalar btSoftBody_getTimeacc(btSoftBody* obj)
{
	return obj->m_timeacc;
}

btScalar btSoftBody_getTotalMass(btSoftBody* obj)
{
	return obj->getTotalMass();
}

void btSoftBody_getUserIndexMapping(btSoftBody* obj)
{
	obj->m_userIndexMapping;
}

void btSoftBody_getWindVelocity(btSoftBody* obj)
{
	obj->m_windVelocity;
}
/*
btScalar* btSoftBody_getWindVelocity2(btSoftBody* obj)
{
	return &obj->getWindVelocity();
}
*/
btScalar btSoftBody_getVolume(btSoftBody* obj)
{
	return obj->getVolume();
}

btSoftBodyWorldInfo* btSoftBody_getWorldInfo(btSoftBody* obj)
{
	return obj->getWorldInfo();
}

btSoftBodyWorldInfo* btSoftBody_getWorldInfo2(btSoftBody* obj)
{
	return obj->m_worldInfo;
}

void btSoftBody_indicesToPointers(btSoftBody* obj, int* map)
{
	obj->indicesToPointers(map);
}

void btSoftBody_indicesToPointers2(btSoftBody* obj)
{
	obj->indicesToPointers();
}

void btSoftBody_initDefaults(btSoftBody* obj)
{
	obj->initDefaults();
}

void btSoftBody_initializeClusters(btSoftBody* obj)
{
	obj->initializeClusters();
}

void btSoftBody_initializeFaceTree(btSoftBody* obj)
{
	obj->initializeFaceTree();
}

void btSoftBody_integrateMotion(btSoftBody* obj)
{
	obj->integrateMotion();
}

void btSoftBody_pointersToIndices(btSoftBody* obj)
{
	obj->pointersToIndices();
}

void btSoftBody_predictMotion(btSoftBody* obj, btScalar dt)
{
	obj->predictMotion(dt);
}

void btSoftBody_prepareClusters(btSoftBody* obj, int iterations)
{
	obj->prepareClusters(iterations);
}

void btSoftBody_PSolve_Anchors(btSoftBody* psb, btScalar kst, btScalar ti)
{
	btSoftBody::PSolve_Anchors(psb, kst, ti);
}

void btSoftBody_PSolve_Links(btSoftBody* psb, btScalar kst, btScalar ti)
{
	btSoftBody::PSolve_Links(psb, kst, ti);
}

void btSoftBody_PSolve_RContacts(btSoftBody* psb, btScalar kst, btScalar ti)
{
	btSoftBody::PSolve_RContacts(psb, kst, ti);
}

void btSoftBody_PSolve_SContacts(btSoftBody* psb, btScalar __unnamed1, btScalar ti)
{
	btSoftBody::PSolve_SContacts(psb, __unnamed1, ti);
}

void btSoftBody_randomizeConstraints(btSoftBody* obj)
{
	obj->randomizeConstraints();
}
/*
int btSoftBody_rayTest(btSoftBody* obj, btScalar* rayFrom, btScalar* rayTo, btScalar* mint, _* feature, int* index, bool bcountonly)
{
	VECTOR3_CONV(rayFrom);
	VECTOR3_CONV(rayTo);
	return obj->rayTest(VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), *mint, *feature, *index, bcountonly);
}
*/
bool btSoftBody_rayTest2(btSoftBody* obj, btScalar* rayFrom, btScalar* rayTo, btSoftBody::sRayCast* results)
{
	VECTOR3_CONV(rayFrom);
	VECTOR3_CONV(rayTo);
	return obj->rayTest(VECTOR3_USE(rayFrom), VECTOR3_USE(rayTo), *results);
}

void btSoftBody_refine(btSoftBody* obj, btSoftBody::ImplicitFn* ifn, btScalar accurary, bool cut)
{
	obj->refine(ifn, accurary, cut);
}

void btSoftBody_releaseCluster(btSoftBody* obj, int index)
{
	obj->releaseCluster(index);
}

void btSoftBody_releaseClusters(btSoftBody* obj)
{
	obj->releaseClusters();
}

void btSoftBody_resetLinkRestLengths(btSoftBody* obj)
{
	obj->resetLinkRestLengths();
}

void btSoftBody_rotate(btSoftBody* obj, btQuaternion* rot)
{
	obj->rotate(*rot);
}

void btSoftBody_scale(btSoftBody* obj, btScalar* scl)
{
	VECTOR3_CONV(scl);
	obj->scale(VECTOR3_USE(scl));
}
/*
void btSoftBody_setAnchors(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_anchors = value;
}

void btSoftBody_setBounds(btSoftBody* obj, btScalar* value)
{
	VECTOR3_CONV(value);
	obj->m_bounds = value;
}
*/
void btSoftBody_setBUpdateRtCst(btSoftBody* obj, bool value)
{
	obj->m_bUpdateRtCst = value;
}
/*
void btSoftBody_setCdbvt(btSoftBody* obj, void value)
{
	obj->m_cdbvt = value;
}

void btSoftBody_setCfg(btSoftBody* obj, void value)
{
	obj->m_cfg = value;
}

void btSoftBody_setClusterConnectivity(btSoftBody* obj, void value)
{
	obj->m_clusterConnectivity = value;
}

void btSoftBody_setClusters(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_clusters = value;
}

void btSoftBody_setCollisionDisabledObjects(btSoftBody* obj, void value)
{
	obj->m_collisionDisabledObjects = value;
}

void btSoftBody_setFaces(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_faces = value;
}

void btSoftBody_setFdbvt(btSoftBody* obj, void value)
{
	obj->m_fdbvt = value;
}

void btSoftBody_setInitialWorldTransform(btSoftBody* obj, void value)
{
	TRANSFORM_CONV(value);
	obj->m_initialWorldTransform = value;
}

void btSoftBody_setJoints(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_joints = value;
}

void btSoftBody_setLinks(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_links = value;
}
*/
void btSoftBody_setMass(btSoftBody* obj, int node, btScalar mass)
{
	obj->setMass(node, mass);
}
/*
void btSoftBody_setMaterials(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_materials = value;
}

void btSoftBody_setNdbvt(btSoftBody* obj, void value)
{
	obj->m_ndbvt = value;
}

void btSoftBody_setNodes(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_nodes = value;
}

void btSoftBody_setNotes(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_notes = value;
}

void btSoftBody_setPose(btSoftBody* obj, void value)
{
	obj->m_pose = value;
}
*/
void btSoftBody_setPose2(btSoftBody* obj, bool bvolume, bool bframe)
{
	obj->setPose(bvolume, bframe);
}
/*
void btSoftBody_setRcontacts(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_rcontacts = value;
}
*/
void btSoftBody_setRestLengthScale(btSoftBody* obj, btScalar restLength)
{
	obj->setRestLengthScale(restLength);
}

void btSoftBody_setRestLengthScale2(btSoftBody* obj, btScalar value)
{
	obj->m_restLengthScale = value;
}
/*
void btSoftBody_setScontacts(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_scontacts = value;
}
*/
void btSoftBody_setSoftBodySolver(btSoftBody* obj, btSoftBodySolver* softBodySolver)
{
	obj->setSoftBodySolver(softBodySolver);
}

void btSoftBody_setSoftBodySolver2(btSoftBody* obj, btSoftBodySolver* value)
{
	obj->m_softBodySolver = value;
}
/*
void btSoftBody_setSolver(btSoftBody* obj, void preset)
{
	obj->setSolver(preset);
}

void btSoftBody_setSst(btSoftBody* obj, void value)
{
	obj->m_sst = value;
}
*/
void btSoftBody_setTag(btSoftBody* obj, void* value)
{
	obj->m_tag = value;
}
/*
void btSoftBody_setTetras(btSoftBody* obj, btAlignedObjectArray* value)
{
	obj->m_tetras = value;
}
*/
void btSoftBody_setTimeacc(btSoftBody* obj, btScalar value)
{
	obj->m_timeacc = value;
}

void btSoftBody_setTotalDensity(btSoftBody* obj, btScalar density)
{
	obj->setTotalDensity(density);
}

void btSoftBody_setTotalMass(btSoftBody* obj, btScalar mass, bool fromfaces)
{
	obj->setTotalMass(mass, fromfaces);
}

void btSoftBody_setTotalMass2(btSoftBody* obj, btScalar mass)
{
	obj->setTotalMass(mass);
}
/*
void btSoftBody_setUserIndexMapping(btSoftBody* obj, void value)
{
	obj->m_userIndexMapping = value;
}
*/
void btSoftBody_setVelocity(btSoftBody* obj, btScalar* velocity)
{
	VECTOR3_CONV(velocity);
	obj->setVelocity(VECTOR3_USE(velocity));
}

void btSoftBody_setWindVelocity(btSoftBody* obj, btScalar* velocity)
{
	VECTOR3_CONV(velocity);
	obj->setWindVelocity(VECTOR3_USE(velocity));
}
/*
void btSoftBody_setWindVelocity2(btSoftBody* obj, void value)
{
	VECTOR3_CONV(value);
	obj->m_windVelocity = value;
}
*/
void btSoftBody_setVolumeDensity(btSoftBody* obj, btScalar density)
{
	obj->setVolumeDensity(density);
}

void btSoftBody_setVolumeMass(btSoftBody* obj, btScalar mass)
{
	obj->setVolumeMass(mass);
}

void btSoftBody_setWorldInfo(btSoftBody* obj, btSoftBodyWorldInfo* value)
{
	obj->m_worldInfo = value;
}
/*
void btSoftBody_solveClusters(btAlignedObjectArray* bodies)
{
	btSoftBody::solveClusters(*bodies);
}
*/
void btSoftBody_solveClusters2(btSoftBody* obj, btScalar sor)
{
	obj->solveClusters(sor);
}
/*
void btSoftBody_solveCommonConstraints(* bodies, int count, int iterations)
{
	btSoftBody::solveCommonConstraints(bodies, count, iterations);
}
*/
void btSoftBody_solveConstraints(btSoftBody* obj)
{
	obj->solveConstraints();
}

void btSoftBody_staticSolve(btSoftBody* obj, int iterations)
{
	obj->staticSolve(iterations);
}

void btSoftBody_transform(btSoftBody* obj, btScalar* trs)
{
	TRANSFORM_CONV(trs);
	obj->transform(TRANSFORM_USE(trs));
}

void btSoftBody_translate(btSoftBody* obj, btScalar* trs)
{
	VECTOR3_CONV(trs);
	obj->translate(VECTOR3_USE(trs));
}

btSoftBody* btSoftBody_upcast(btCollisionObject* colObj)
{
	return btSoftBody::upcast(colObj);
}

void btSoftBody_updateArea(btSoftBody* obj, bool averageArea)
{
	obj->updateArea(averageArea);
}

void btSoftBody_updateArea2(btSoftBody* obj)
{
	obj->updateArea();
}

void btSoftBody_updateBounds(btSoftBody* obj)
{
	obj->updateBounds();
}

void btSoftBody_updateClusters(btSoftBody* obj)
{
	obj->updateClusters();
}

void btSoftBody_updateConstants(btSoftBody* obj)
{
	obj->updateConstants();
}

void btSoftBody_updateLinkConstants(btSoftBody* obj)
{
	obj->updateLinkConstants();
}

void btSoftBody_updateNormals(btSoftBody* obj)
{
	obj->updateNormals();
}

void btSoftBody_updatePose(btSoftBody* obj)
{
	obj->updatePose();
}

void btSoftBody_VSolve_Links(btSoftBody* psb, btScalar kst)
{
	btSoftBody::VSolve_Links(psb, kst);
}
