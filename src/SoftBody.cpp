#include "StdAfx.h"

#ifndef DISABLE_SOFTBODY

#include "AlignedObjectArray.h"
#include "BroadphaseInterface.h"
#include "Dispatcher.h"
#include "RigidBody.h"
#include "SoftBody.h"
#include "SparseSdf.h"
#include "StringConv.h"
#ifndef DISABLE_DBVT
#include "Dbvt.h"
#endif

using namespace BulletSharp::SoftBody;

#pragma managed(push, off)
btSoftBodyWorldInfo* SoftBodyWorldInfo_New()
{
	btSoftBodyWorldInfo* info = new btSoftBodyWorldInfo();
	memset(info, 0, sizeof(btSoftBodyWorldInfo));
	return info;
}
#pragma managed(pop)

SoftBodyWorldInfo::SoftBodyWorldInfo(btSoftBodyWorldInfo* info)
{
	_info = info;
}

SoftBodyWorldInfo::SoftBodyWorldInfo()
{
	_info = SoftBodyWorldInfo_New();
}

btScalar SoftBodyWorldInfo::AirDensity::get()
{
	return _info->air_density;
}
void SoftBodyWorldInfo::AirDensity::set(btScalar value)
{
	_info->air_density = value;
}

BroadphaseInterface^ SoftBodyWorldInfo::Broadphase::get()
{
	if (_info->m_broadphase == nullptr)
		return nullptr;
	return gcnew BroadphaseInterface(_info->m_broadphase);
}
void SoftBodyWorldInfo::Broadphase::set(BroadphaseInterface^ value)
{
	_info->m_broadphase = value->UnmanagedPointer;
}

Dispatcher^ SoftBodyWorldInfo::Dispatcher::get()
{
	if (_info->m_dispatcher == nullptr)
		return nullptr;
	return gcnew BulletSharp::Dispatcher(_info->m_dispatcher);
}
void SoftBodyWorldInfo::Dispatcher::set(BulletSharp::Dispatcher^ value)
{
	_info->m_dispatcher = value->UnmanagedPointer;
}

Vector3 SoftBodyWorldInfo::Gravity::get()
{
	return Math::BtVector3ToVector3(&_info->m_gravity);
}
void SoftBodyWorldInfo::Gravity::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_info->m_gravity);
}

SparseSdf^ SoftBodyWorldInfo::SparseSdf::get()
{
	return gcnew BulletSharp::SparseSdf(&_info->m_sparsesdf);
}
void SoftBodyWorldInfo::SparseSdf::set(BulletSharp::SparseSdf^ value)
{
	_info->m_sparsesdf = *value->UnmanagedPointer;
}

btScalar SoftBodyWorldInfo::WaterDensity::get()
{
	return _info->water_density;
}
void SoftBodyWorldInfo::WaterDensity::set(btScalar value)
{
	_info->water_density = value;
}

Vector3 SoftBodyWorldInfo::WaterNormal::get()
{
	return Math::BtVector3ToVector3(&_info->water_normal);
}
void SoftBodyWorldInfo::WaterNormal::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &_info->water_normal);
}

btScalar SoftBodyWorldInfo::WaterOffset::get()
{
	return _info->water_offset;
}
void SoftBodyWorldInfo::WaterOffset::set(btScalar value)
{
	_info->water_offset = value;
}

btSoftBodyWorldInfo* SoftBodyWorldInfo::UnmanagedPointer::get()
{
	return _info;
}
void SoftBodyWorldInfo::UnmanagedPointer::set(btSoftBodyWorldInfo* value)
{
	_info = value;
}


btSoftBody::Body* Body::UnmanagedPointer::get()
{
	return _body;
}
void Body::UnmanagedPointer::set(btSoftBody::Body* value)
{
	_body = value;
}


btSoftBody::Cluster* Cluster::UnmanagedPointer::get()
{
	return _cluster;
}
void Cluster::UnmanagedPointer::set(btSoftBody::Cluster* value)
{
	_cluster = value;
}


Config::Config(btSoftBody::Config* config)
{
	_config = config;
}

AeroModel Config::AeroModel::get()
{
	return (BulletSharp::SoftBody::AeroModel)_config->aeromodel;
}
void Config::AeroModel::set(BulletSharp::SoftBody::AeroModel value)
{
	_config->aeromodel = (btSoftBody::eAeroModel::_)value;
}

btScalar Config::Chr::get()
{
	return _config->kCHR;
}
void Config::Chr::set(btScalar value)
{
	_config->kCHR = value;
}

btScalar Config::DF::get()
{
	return _config->kDF;
}
void Config::DF::set(btScalar value)
{
	_config->kDF = value;
}

btScalar Config::DG::get()
{
	return _config->kDG;
}
void Config::DG::set(btScalar value)
{
	_config->kDG = value;
}

btScalar Config::DP::get()
{
	return _config->kDP;
}
void Config::DP::set(btScalar value)
{
	_config->kDP = value;
}

btScalar Config::LF::get()
{
	return _config->kLF;
}
void Config::LF::set(btScalar value)
{
	_config->kLF = value;
}

FCollisions Config::Collisions::get()
{
	return (FCollisions)UnmanagedPointer->collisions;
}
void Config::Collisions::set(FCollisions value)
{
	UnmanagedPointer->collisions = (int)value;
}

int Config::PIterations::get()
{
	return UnmanagedPointer->piterations;
}
void Config::PIterations::set(int value)
{
	UnmanagedPointer->piterations = value;
}

btScalar Config::PR::get()
{
	return _config->kPR;
}
void Config::PR::set(btScalar value)
{
	_config->kPR = value;
}

btScalar Config::VC::get()
{
	return _config->kVC;
}
void Config::VC::set(btScalar value)
{
	_config->kVC = value;
}

btSoftBody::Config* Config::UnmanagedPointer::get()
{
	return _config;
}
void Config::UnmanagedPointer::set(btSoftBody::Config* value)
{
	_config = value;
}


Body::Body()
{
	_body = new btSoftBody::Body();
}


Cluster::Cluster()
{
	_cluster = new btSoftBody::Cluster();
}

Element::Element(btSoftBody::Element* element)
{
	_element = element;
}

btSoftBody::Element* Element::UnmanagedPointer::get()
{
	return _element;
}
void Element::UnmanagedPointer::set(btSoftBody::Element* element)
{
	_element = element;
}


Feature::Feature(btSoftBody::Feature* feature)
: Element(feature)
{
}

BulletSharp::SoftBody::Material^ Feature::Material::get()
{
	return gcnew BulletSharp::SoftBody::Material(UnmanagedPointer->m_material);
}
void Feature::Material::set(BulletSharp::SoftBody::Material^ value)
{
	UnmanagedPointer->m_material = value->UnmanagedPointer;
}

btSoftBody::Feature* Feature::UnmanagedPointer::get()
{
	return (btSoftBody::Feature*)Element::UnmanagedPointer;
}


Face::Face(btSoftBody::Face* face)
: Feature(face)
{
}


#ifndef DISABLE_DBVT
DbvtNode^ Face::Leaf::get()
{
	return gcnew DbvtNode(UnmanagedPointer->m_leaf);
}
void Face::Leaf::set(DbvtNode^ value)
{
	UnmanagedPointer->m_leaf = value->UnmanagedPointer;
}
#endif

array<BulletSharp::SoftBody::Node^>^ Face::N::get()
{
	array<Node^>^ nodeArray = gcnew array<Node^>(3);
	nodeArray[0] = gcnew Node(UnmanagedPointer->m_n[0]);
	nodeArray[1] = gcnew Node(UnmanagedPointer->m_n[1]);
	nodeArray[2] = gcnew Node(UnmanagedPointer->m_n[2]);
	return nodeArray;
}
void Face::N::set(array<BulletSharp::SoftBody::Node^>^ value)
{
	UnmanagedPointer->m_n[0] = value[0]->UnmanagedPointer;
	UnmanagedPointer->m_n[1] = value[1]->UnmanagedPointer;
	UnmanagedPointer->m_n[2] = value[2]->UnmanagedPointer;
}

Vector3 Face::Normal::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->m_normal);
}
void Face::Normal::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &UnmanagedPointer->m_normal);
}

btScalar Face::RestArea::get()
{
	return UnmanagedPointer->m_ra;
}
void Face::RestArea::set(btScalar value)
{
	UnmanagedPointer->m_ra = value;
}

btSoftBody::Face* Face::UnmanagedPointer::get()
{
	return (btSoftBody::Face*)Feature::UnmanagedPointer;
}


Impulse::Impulse()
{
	_impulse = new btSoftBody::Impulse();
}

btSoftBody::Impulse* BulletSharp::SoftBody::Impulse::UnmanagedPointer::get()
{
	return _impulse;
}
void BulletSharp::SoftBody::Impulse::UnmanagedPointer::set(btSoftBody::Impulse* value)
{
	_impulse = value;
}


btSoftBody::Joint::Specs* BulletSharp::SoftBody::Joint::Specs::UnmanagedPointer::get()
{
	return _specs;
}
void BulletSharp::SoftBody::Joint::Specs::UnmanagedPointer::set(btSoftBody::Joint::Specs* value)
{
	_specs = value;
}


BulletSharp::SoftBody::LJoint::Specs::Specs()
{
	UnmanagedPointer = new btSoftBody::LJoint::Specs();
}

btSoftBody::LJoint::Specs* BulletSharp::SoftBody::LJoint::Specs::UnmanagedPointer::get()
{
	return (btSoftBody::LJoint::Specs*)Joint::Specs::UnmanagedPointer;
}


BulletSharp::SoftBody::AJoint::Specs::Specs()
{
	UnmanagedPointer = new btSoftBody::AJoint::Specs();
}

btSoftBody::AJoint::Specs* BulletSharp::SoftBody::AJoint::Specs::UnmanagedPointer::get()
{
	return (btSoftBody::AJoint::Specs*)Joint::Specs::UnmanagedPointer;
}


Link::Link(btSoftBody::Link* link)
: Feature(link)
{
}

btScalar Link::C0::get()
{
	return UnmanagedPointer->m_c0;
}
void Link::C0::set(btScalar value)
{
	UnmanagedPointer->m_c0 = value;
}

btScalar Link::C1::get()
{
	return UnmanagedPointer->m_c1;
}
void Link::C1::set(btScalar value)
{
	UnmanagedPointer->m_c1 = value;
}

btScalar Link::C2::get()
{
	return UnmanagedPointer->m_c2;
}
void Link::C2::set(btScalar value)
{
	UnmanagedPointer->m_c2 = value;
}

Vector3 Link::C3::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->m_c3);
}
void Link::C3::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &UnmanagedPointer->m_c3);
}

bool Link::IsBending::get()
{
	return UnmanagedPointer->m_bbending;
}
void Link::IsBending::set(bool value)
{
	UnmanagedPointer->m_bbending = value;
}

array<BulletSharp::SoftBody::Node^>^ Link::N::get()
{
	array<Node^>^ nodeArray = gcnew array<Node^>(2);
	nodeArray[0] = gcnew Node(UnmanagedPointer->m_n[0]);
	nodeArray[1] = gcnew Node(UnmanagedPointer->m_n[1]);
	return nodeArray;
}
void Link::N::set(array<BulletSharp::SoftBody::Node^>^ value)
{
	UnmanagedPointer->m_n[0] = value[0]->UnmanagedPointer;
	UnmanagedPointer->m_n[1] = value[1]->UnmanagedPointer;
}

btScalar Link::RestLength::get()
{
	return UnmanagedPointer->m_rl;
}
void Link::RestLength::set(btScalar value)
{
	UnmanagedPointer->m_rl = value;
}

btSoftBody::Link* Link::UnmanagedPointer::get()
{
	return (btSoftBody::Link*)Feature::UnmanagedPointer;
}


BulletSharp::SoftBody::Material::Material(btSoftBody::Material* material)
: Element(material)
{
}

btScalar BulletSharp::SoftBody::Material::Ast::get()
{
	return UnmanagedPointer->m_kAST;
}
void BulletSharp::SoftBody::Material::Ast::set(btScalar value)
{
	UnmanagedPointer->m_kAST = value;
}

FMaterial BulletSharp::SoftBody::Material::Flags::get()
{
	return (FMaterial)UnmanagedPointer->m_flags;
}
void BulletSharp::SoftBody::Material::Flags::set(FMaterial value)
{
	UnmanagedPointer->m_flags = (int)value;
}

btScalar BulletSharp::SoftBody::Material::Lst::get()
{
	return UnmanagedPointer->m_kLST;
}
void BulletSharp::SoftBody::Material::Lst::set(btScalar value)
{
	UnmanagedPointer->m_kAST = value;
}

btScalar BulletSharp::SoftBody::Material::Vst::get()
{
	return UnmanagedPointer->m_kVST;
}
void BulletSharp::SoftBody::Material::Vst::set(btScalar value)
{
	UnmanagedPointer->m_kVST = value;
}

btSoftBody::Material* BulletSharp::SoftBody::Material::UnmanagedPointer::get()
{
	return (btSoftBody::Material*)Element::UnmanagedPointer;
}


BulletSharp::SoftBody::Node::Node(btSoftBody::Node* node)
: Feature(node)
{
}

btScalar BulletSharp::SoftBody::Node::Area::get()
{
	return UnmanagedPointer->m_area;
}
void BulletSharp::SoftBody::Node::Area::set(btScalar value)
{
	UnmanagedPointer->m_area = value;
}

Vector3 BulletSharp::SoftBody::Node::Force::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->m_f);
}
void BulletSharp::SoftBody::Node::Force::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &UnmanagedPointer->m_f);
}

btScalar BulletSharp::SoftBody::Node::InverseMass::get()
{
	return UnmanagedPointer->m_im;
}
void BulletSharp::SoftBody::Node::InverseMass::set(btScalar value)
{
	UnmanagedPointer->m_im = value;
}

bool BulletSharp::SoftBody::Node::IsAttached::get()
{
	return UnmanagedPointer->m_battach;
}
void BulletSharp::SoftBody::Node::IsAttached::set(bool value)
{
	UnmanagedPointer->m_battach = value;
}

#ifndef DISABLE_DBVT
DbvtNode^ BulletSharp::SoftBody::Node::Leaf::get()
{
	return gcnew DbvtNode(UnmanagedPointer->m_leaf);
}
void BulletSharp::SoftBody::Node::Leaf::set(DbvtNode^ value)
{
	UnmanagedPointer->m_leaf = value->UnmanagedPointer;
}
#endif

Vector3 BulletSharp::SoftBody::Node::Normal::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->m_n);
}
void BulletSharp::SoftBody::Node::Normal::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &UnmanagedPointer->m_n);
}

Vector3 BulletSharp::SoftBody::Node::Q::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->m_q);
}
void BulletSharp::SoftBody::Node::Q::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &UnmanagedPointer->m_q);
}

Vector3 BulletSharp::SoftBody::Node::Velocity::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->m_v);
}
void BulletSharp::SoftBody::Node::Velocity::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &UnmanagedPointer->m_v);
}

Vector3 BulletSharp::SoftBody::Node::X::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->m_x);
}
void BulletSharp::SoftBody::Node::X::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &UnmanagedPointer->m_x);
}

btSoftBody::Node* BulletSharp::SoftBody::Node::UnmanagedPointer::get()
{
	return (btSoftBody::Node*)Feature::UnmanagedPointer;
}


Note::Note(btSoftBody::Note* note)
: Element(note)
{
}


BulletSharp::SoftBody::Pose::Pose(btSoftBody::Pose* pose)
{
	_pose = pose;
}

Matrix BulletSharp::SoftBody::Pose::Aqq::get()
{
	return Math::BtMatrix3x3ToMatrix(&UnmanagedPointer->m_aqq);
}
void BulletSharp::SoftBody::Pose::Aqq::set(Matrix value)
{
	Math::MatrixToBtMatrix3x3(value, &UnmanagedPointer->m_aqq);
}

Vector3 BulletSharp::SoftBody::Pose::Com::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->m_com);
}
void BulletSharp::SoftBody::Pose::Com::set(Vector3 value)
{
	Math::Vector3ToBtVector3(value, &UnmanagedPointer->m_com);
}

bool BulletSharp::SoftBody::Pose::IsFrameValid::get()
{
	return _pose->m_bframe;
}
void BulletSharp::SoftBody::Pose::IsFrameValid::set(bool value)
{
	_pose->m_bframe = value;
}

bool BulletSharp::SoftBody::Pose::IsVolumeValid::get()
{
	return _pose->m_bvolume;
}
void BulletSharp::SoftBody::Pose::IsVolumeValid::set(bool value)
{
	_pose->m_bvolume = value;
}

Vector3Array^ BulletSharp::SoftBody::Pose::Positions::get()
{
	return gcnew Vector3Array(&_pose->m_pos);
}
void BulletSharp::SoftBody::Pose::Positions::set(Vector3Array^ value)
{
	_pose->m_pos = *value->UnmanagedPointer;
}

Matrix BulletSharp::SoftBody::Pose::Rotation::get()
{
	return Math::BtMatrix3x3ToMatrix(&UnmanagedPointer->m_rot);
}
void BulletSharp::SoftBody::Pose::Rotation::set(Matrix value)
{
	Math::MatrixToBtMatrix3x3(value, &UnmanagedPointer->m_rot);
}

Matrix BulletSharp::SoftBody::Pose::Scale::get()
{
	return Math::BtMatrix3x3ToMatrix(&UnmanagedPointer->m_scl);
}
void BulletSharp::SoftBody::Pose::Scale::set(Matrix value)
{
	Math::MatrixToBtMatrix3x3(value, &UnmanagedPointer->m_scl);
}

btScalar BulletSharp::SoftBody::Pose::Volume::get()
{
	return _pose->m_volume;
}
void BulletSharp::SoftBody::Pose::Volume::set(btScalar value)
{
	_pose->m_volume = value;
}

ScalarArray^ BulletSharp::SoftBody::Pose::Weights::get()
{
	return gcnew ScalarArray(&_pose->m_wgh);
}
void BulletSharp::SoftBody::Pose::Weights::set(ScalarArray^ value)
{
	_pose->m_wgh = *value->UnmanagedPointer;
}

btSoftBody::Pose* BulletSharp::SoftBody::Pose::UnmanagedPointer::get()
{
	return _pose;
}
void BulletSharp::SoftBody::Pose::UnmanagedPointer::set(btSoftBody::Pose* pose)
{
	_pose = pose;
}


SolverState::SolverState(btSoftBody::SolverState* solverState)
{
	_solverState = solverState;
}

btScalar SolverState::InverseSdt::get()
{
	return _solverState->isdt;
}
void SolverState::InverseSdt::set(btScalar value)
{
	_solverState->isdt = value;
}

btScalar SolverState::RadialMargin::get()
{
	return _solverState->radmrg;
}
void SolverState::RadialMargin::set(btScalar value)
{
	_solverState->radmrg = value;
}

btScalar SolverState::Sdt::get()
{
	return _solverState->sdt;
}
void SolverState::Sdt::set(btScalar value)
{
	_solverState->sdt = value;
}

btScalar SolverState::VelocityMargin::get()
{
	return _solverState->velmrg;
}
void SolverState::VelocityMargin::set(btScalar value)
{
	_solverState->velmrg = value;
}

btScalar SolverState::UpdateMargin::get()
{
	return _solverState->updmrg;
}
void SolverState::UpdateMargin::set(btScalar value)
{
	_solverState->updmrg = value;
}

btSoftBody::SolverState* SolverState::UnmanagedPointer::get()
{
	return _solverState;
}
void SolverState::UnmanagedPointer::set(btSoftBody::SolverState* solverState)
{
	_solverState = solverState;
}


Tetra::Tetra(btSoftBody::Tetra* tetra)
: Feature(tetra)
{
}


BulletSharp::SoftBody::SoftBody::SoftBody(btSoftBody* body)
: CollisionObject(body)
{
}

BulletSharp::SoftBody::SoftBody::SoftBody(SoftBodyWorldInfo^ worldInfo, array<Vector3>^ x, array<btScalar>^ m)
: CollisionObject(0)
{
	int node_count = (x->Length < m->Length) ? x->Length : m->Length;

	pin_ptr<btScalar> m_ptr = &m[0];

	btVector3* x_ptr = new btVector3[node_count];

	for(int i=0; i<node_count; i++)
		Math::Vector3ToBtVector3(x[i], &x_ptr[i]);

	UnmanagedPointer = new btSoftBody(worldInfo->UnmanagedPointer, node_count, x_ptr, m_ptr);

	delete[] x_ptr;
}

void BulletSharp::SoftBody::SoftBody::AddForce(Vector3 force, int node)
{
	btVector3* forceTemp = Math::Vector3ToBtVector3(force);
	UnmanagedPointer->addForce(*forceTemp, node);
	delete forceTemp;
}

void BulletSharp::SoftBody::SoftBody::AddForce(Vector3 force)
{
	UnmanagedPointer->addForce(*Math::Vector3ToBtVector3(force));
}

void BulletSharp::SoftBody::SoftBody::AddVelocity(Vector3 velocity, int node)
{
	btVector3* velocityTemp = Math::Vector3ToBtVector3(velocity);
	UnmanagedPointer->addVelocity(*velocityTemp, node);
	delete velocityTemp;
}

void BulletSharp::SoftBody::SoftBody::AddVelocity(Vector3 velocity)
{
	UnmanagedPointer->addVelocity(*Math::Vector3ToBtVector3(velocity));
}

void BulletSharp::SoftBody::SoftBody::AppendAnchor(int node, RigidBody^ body, bool disableCollisionBetweenLinkedBodies)
{
	UnmanagedPointer->appendAnchor(node, body->UnmanagedPointer, disableCollisionBetweenLinkedBodies);
}

void BulletSharp::SoftBody::SoftBody::AppendAnchor(int node, RigidBody^ body)
{
	UnmanagedPointer->appendAnchor(node, body->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendFace(int node0, int node1, int node2, Material^ material)
{
	UnmanagedPointer->appendFace(node0, node1, node2, material->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendFace(int node0, int node1, int node2)
{
	UnmanagedPointer->appendFace(node0, node1, node2);
}

void BulletSharp::SoftBody::SoftBody::AppendFace(int model, Material^ material)
{
	UnmanagedPointer->appendFace(model, material->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendFace(int model)
{
	UnmanagedPointer->appendFace(model);
}

void BulletSharp::SoftBody::SoftBody::AppendFace()
{
	UnmanagedPointer->appendFace();
}

void BulletSharp::SoftBody::SoftBody::AppendLinearJoint(LJoint::Specs^ specs, Cluster^ body0, Body^ body1)
{
	UnmanagedPointer->appendLinearJoint(*specs->UnmanagedPointer, body0->UnmanagedPointer, *body1->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendLinearJoint(LJoint::Specs^ specs, Body^ body1)
{
	UnmanagedPointer->appendLinearJoint(*specs->UnmanagedPointer, *body1->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendLinearJoint(LJoint::Specs^ specs, SoftBody^ body1)
{
	UnmanagedPointer->appendLinearJoint(*specs->UnmanagedPointer, body1->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendAngularJoint(AJoint::Specs^ specs, Cluster^ body0, Body^ body1)
{
	UnmanagedPointer->appendAngularJoint(*specs->UnmanagedPointer, body0->UnmanagedPointer, *body1->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendAngularJoint(AJoint::Specs^ specs, Body^ body1)
{
	UnmanagedPointer->appendAngularJoint(*specs->UnmanagedPointer, *body1->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendAngularJoint(AJoint::Specs^ specs, SoftBody^ body1)
{
	UnmanagedPointer->appendAngularJoint(*specs->UnmanagedPointer, body1->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendLink(int node0, int node1, Material^ material, bool bCheckExist)
{
	UnmanagedPointer->appendLink(node0, node1, material->UnmanagedPointer, bCheckExist);
}

void BulletSharp::SoftBody::SoftBody::AppendLink(int node0, int node1, Material^ material)
{
	UnmanagedPointer->appendLink(node0, node1, material->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendLink(int node0, int node1)
{
	UnmanagedPointer->appendLink(node0, node1);
}

void BulletSharp::SoftBody::SoftBody::AppendLink(Node^ node0, Node^ node1, Material^ material, bool bCheckExist)
{
	UnmanagedPointer->appendLink(node0->UnmanagedPointer, node1->UnmanagedPointer, material->UnmanagedPointer, bCheckExist);
}

void BulletSharp::SoftBody::SoftBody::AppendLink(Node^ node0, Node^ node1, Material^ material)
{
	UnmanagedPointer->appendLink(node0->UnmanagedPointer, node1->UnmanagedPointer, material->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendLink(Node^ node0, Node^ node1)
{
	UnmanagedPointer->appendLink(node0->UnmanagedPointer, node1->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendLink(int model, Material^ material)
{
	UnmanagedPointer->appendLink(model, material->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendLink(int model)
{
	UnmanagedPointer->appendLink(model);
}

void BulletSharp::SoftBody::SoftBody::AppendLink()
{
	UnmanagedPointer->appendLink();
}

BulletSharp::SoftBody::Material^ BulletSharp::SoftBody::SoftBody::AppendMaterial()
{
	return gcnew Material(UnmanagedPointer->appendMaterial());
}

void BulletSharp::SoftBody::SoftBody::AppendNote(String^ text, Vector3 o, Vector4 c, Node^ n0, Node^ n1, Node^ n2, Node^ n3)
{
	const char* textTemp = StringConv::ManagedToUnmanaged(text);
	btVector3* oTemp = Math::Vector3ToBtVector3(o);
	btVector4* cTemp = Math::Vector4ToBtVector4(c);

	UnmanagedPointer->appendNote(textTemp, *oTemp, *cTemp, n0->UnmanagedPointer, n1->UnmanagedPointer, n2->UnmanagedPointer, n3->UnmanagedPointer);

	StringConv::FreeUnmanagedString(textTemp);
	delete oTemp;
	delete cTemp;
}

void BulletSharp::SoftBody::SoftBody::AppendNote(String^ text, Vector3 o, Vector4 c, Node^ n0, Node^ n1, Node^ n2)
{
	const char* textTemp = StringConv::ManagedToUnmanaged(text);
	btVector3* oTemp = Math::Vector3ToBtVector3(o);
	btVector4* cTemp = Math::Vector4ToBtVector4(c);

	UnmanagedPointer->appendNote(textTemp, *oTemp, *cTemp, n0->UnmanagedPointer, n1->UnmanagedPointer, n2->UnmanagedPointer);

	StringConv::FreeUnmanagedString(textTemp);
	delete oTemp;
	delete cTemp;
}

void BulletSharp::SoftBody::SoftBody::AppendNote(String^ text, Vector3 o, Vector4 c, Node^ n0, Node^ n1)
{
	const char* textTemp = StringConv::ManagedToUnmanaged(text);
	btVector3* oTemp = Math::Vector3ToBtVector3(o);
	btVector4* cTemp = Math::Vector4ToBtVector4(c);

	UnmanagedPointer->appendNote(textTemp, *oTemp, *cTemp, n0->UnmanagedPointer, n1->UnmanagedPointer);

	StringConv::FreeUnmanagedString(textTemp);
	delete oTemp;
	delete cTemp;
}

void BulletSharp::SoftBody::SoftBody::AppendNote(String^ text, Vector3 o, Vector4 c, Node^ n0)
{
	const char* textTemp = StringConv::ManagedToUnmanaged(text);
	btVector3* oTemp = Math::Vector3ToBtVector3(o);
	btVector4* cTemp = Math::Vector4ToBtVector4(c);

	UnmanagedPointer->appendNote(textTemp, *oTemp, *cTemp, n0->UnmanagedPointer);

	StringConv::FreeUnmanagedString(textTemp);
	delete oTemp;
	delete cTemp;
}

void BulletSharp::SoftBody::SoftBody::AppendNote(String^ text, Vector3 o, Vector4 c)
{
	const char* textTemp = StringConv::ManagedToUnmanaged(text);
	btVector3* oTemp = Math::Vector3ToBtVector3(o);
	btVector4* cTemp = Math::Vector4ToBtVector4(c);

	UnmanagedPointer->appendNote(textTemp, *oTemp, *cTemp);

	StringConv::FreeUnmanagedString(textTemp);
	delete oTemp;
	delete cTemp;
}

void BulletSharp::SoftBody::SoftBody::AppendNode(Vector3 x, btScalar m)
{
	btVector3* xTemp = Math::Vector3ToBtVector3(x);
	UnmanagedPointer->appendNode(*xTemp, m);
	delete xTemp;
}

void SoftBody_AppendNote(btSoftBody* body, const char* text, btVector3* c)
{
	body->appendNote(text, *c);
}

void BulletSharp::SoftBody::SoftBody::AppendNote(String^ text, Vector3 o)
{
	const char* textTemp = StringConv::ManagedToUnmanaged(text);
	btVector3* oTemp = Math::Vector3ToBtVector3(o);

	SoftBody_AppendNote(UnmanagedPointer, textTemp, oTemp);

	StringConv::FreeUnmanagedString(textTemp);
	delete oTemp;
}

void BulletSharp::SoftBody::SoftBody::AppendNote(String^ text, Vector3 o, Node^ feature)
{
	const char* textTemp = StringConv::ManagedToUnmanaged(text);
	btVector3* oTemp = Math::Vector3ToBtVector3(o);

	UnmanagedPointer->appendNote(textTemp, *oTemp, feature->UnmanagedPointer);

	StringConv::FreeUnmanagedString(textTemp);
	delete oTemp;
}

void BulletSharp::SoftBody::SoftBody::AppendNote(String^ text, Vector3 o, Link^ feature)
{
	const char* textTemp = StringConv::ManagedToUnmanaged(text);
	btVector3* oTemp = Math::Vector3ToBtVector3(o);

	UnmanagedPointer->appendNote(textTemp, *oTemp, feature->UnmanagedPointer);

	StringConv::FreeUnmanagedString(textTemp);
	delete oTemp;
}

void BulletSharp::SoftBody::SoftBody::AppendNote(String^ text, Vector3 o, Face^ feature)
{
	const char* textTemp = StringConv::ManagedToUnmanaged(text);
	btVector3* oTemp = Math::Vector3ToBtVector3(o);

	UnmanagedPointer->appendNote(textTemp, *oTemp, feature->UnmanagedPointer);

	StringConv::FreeUnmanagedString(textTemp);
	delete oTemp;
}

void BulletSharp::SoftBody::SoftBody::AppendTetra(int model, Material^ material)
{
	UnmanagedPointer->appendTetra(model, material->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendTetra(int node0, int node1, int node2, int node3, Material^ material)
{
	UnmanagedPointer->appendTetra(node0, node1, node2, node3, material->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::AppendTetra(int node0, int node1, int node2, int node3)
{
	UnmanagedPointer->appendTetra(node0, node1, node2, node3);
}

bool BulletSharp::SoftBody::SoftBody::CheckFace(int node0, int node1, int node2)
{
	return UnmanagedPointer->checkFace(node0, node1, node2);
}

bool BulletSharp::SoftBody::SoftBody::CheckLink(int node0, int node1)
{
	return UnmanagedPointer->checkLink(node0, node1);
}

bool BulletSharp::SoftBody::SoftBody::CheckLink(Node^ node0, Node^ node1)
{
	return UnmanagedPointer->checkLink(node0->UnmanagedPointer, node1->UnmanagedPointer);
}

void SoftBody_ClusterCOM(btSoftBody::Cluster* cluster, btVector3* com)
{
	*com = btSoftBody::clusterCom(cluster);
}
Vector3 BulletSharp::SoftBody::SoftBody::ClusterCom(Cluster^ cluster)
{
	btVector3* tempClusterCom = new btVector3();
	SoftBody_ClusterCOM(cluster->UnmanagedPointer, tempClusterCom);
	Vector3 com = Math::BtVector3ToVector3(tempClusterCom);
	delete tempClusterCom;
	return com;
}

void SoftBody_ClusterCOM(btSoftBody* softBody, int cluster, btVector3* com)
{
	*com = softBody->clusterCom(cluster);
}
Vector3 BulletSharp::SoftBody::SoftBody::ClusterCom(int cluster)
{
	btVector3* tempClusterCom = new btVector3();
	SoftBody_ClusterCOM(UnmanagedPointer, cluster, tempClusterCom);
	Vector3 com = Math::BtVector3ToVector3(tempClusterCom);
	delete tempClusterCom;
	return com;
}

void SoftBody_ClusterVelocity(btSoftBody::Cluster* cluster, btVector3* rpos, btVector3* velocity)
{
	*velocity = btSoftBody::clusterVelocity(cluster, *rpos);
}
Vector3 BulletSharp::SoftBody::SoftBody::ClusterVelocity(Cluster^ cluster, Vector3 rpos)
{
	btVector3* tempVelocity = new btVector3();
	btVector3* tempRpos = Math::Vector3ToBtVector3(rpos);
	SoftBody_ClusterVelocity(cluster->UnmanagedPointer, tempRpos, tempVelocity);
	Vector3 velocity = Math::BtVector3ToVector3(tempVelocity);
	delete tempVelocity;
	delete tempRpos;
	return velocity;
}

void BulletSharp::SoftBody::SoftBody::ClusterVImpulse(Cluster^ cluster, Vector3 rpos, Vector3 impulse)
{
	btVector3* tempRpos = Math::Vector3ToBtVector3(rpos);
	btVector3* tempImpulse = Math::Vector3ToBtVector3(impulse);
	btSoftBody::clusterVImpulse(cluster->UnmanagedPointer, *tempRpos, *tempImpulse);
	delete tempRpos;
	delete tempImpulse;
}

void BulletSharp::SoftBody::SoftBody::ClusterDImpulse(Cluster^ cluster, Vector3 rpos, Vector3 impulse)
{
	btVector3* tempRpos = Math::Vector3ToBtVector3(rpos);
	btVector3* tempImpulse = Math::Vector3ToBtVector3(impulse);
	btSoftBody::clusterDImpulse(cluster->UnmanagedPointer, *tempRpos, *tempImpulse);
	delete tempRpos;
	delete tempImpulse;
}

void BulletSharp::SoftBody::SoftBody::ClusterImpulse(Cluster^ cluster, Vector3 rpos, Impulse^ impulse)
{
	btVector3* tempRpos = Math::Vector3ToBtVector3(rpos);
	btSoftBody::clusterImpulse(cluster->UnmanagedPointer, *tempRpos, *impulse->UnmanagedPointer);
	delete tempRpos;
}

void BulletSharp::SoftBody::SoftBody::ClusterVAImpulse(Cluster^ cluster, Vector3 impulse)
{
	btVector3* tempImpulse = Math::Vector3ToBtVector3(impulse);
	btSoftBody::clusterVAImpulse(cluster->UnmanagedPointer, *tempImpulse);
	delete tempImpulse;
}

void BulletSharp::SoftBody::SoftBody::ClusterDAImpulse(Cluster^ cluster, Vector3 impulse)
{
	btVector3* tempImpulse = Math::Vector3ToBtVector3(impulse);
	btSoftBody::clusterDAImpulse(cluster->UnmanagedPointer, *tempImpulse);
	delete tempImpulse;
}

void BulletSharp::SoftBody::SoftBody::ClusterAImpulse(Cluster^ cluster, Impulse^ impulse)
{
	btSoftBody::clusterAImpulse(cluster->UnmanagedPointer, *impulse->UnmanagedPointer);
}

void BulletSharp::SoftBody::SoftBody::ClusterDCImpulse(Cluster^ cluster, Vector3 impulse)
{
	btVector3* tempImpulse = Math::Vector3ToBtVector3(impulse);
	btSoftBody::clusterDCImpulse(cluster->UnmanagedPointer, *tempImpulse);
	delete tempImpulse;
}

int BulletSharp::SoftBody::SoftBody::GenerateBendingConstraints(int distance, Material^ material)
{
	return UnmanagedPointer->generateBendingConstraints(distance, material->UnmanagedPointer);
}

int BulletSharp::SoftBody::SoftBody::GenerateBendingConstraints(int distance)
{
	return UnmanagedPointer->generateBendingConstraints(distance);
}

int BulletSharp::SoftBody::SoftBody::GenerateClusters(int k, int maxIterations)
{
	return UnmanagedPointer->generateClusters(k, maxIterations);
}

int BulletSharp::SoftBody::SoftBody::GenerateClusters(int k)
{
	return UnmanagedPointer->generateClusters(k);
}

btScalar BulletSharp::SoftBody::SoftBody::GetMass(int node)
{
	return UnmanagedPointer->getMass(node);
}

void BulletSharp::SoftBody::SoftBody::RandomizeConstraints()
{
	UnmanagedPointer->randomizeConstraints();
}

void BulletSharp::SoftBody::SoftBody::ReleaseCluster(int index)
{
	UnmanagedPointer->releaseCluster(index);
}

void BulletSharp::SoftBody::SoftBody::ReleaseClusters()
{
	UnmanagedPointer->releaseClusters();
}

void BulletSharp::SoftBody::SoftBody::Rotate(Quaternion rotation)
{
	btQuaternion* rotationTemp = Math::QuaternionToBtQuat(rotation);
	UnmanagedPointer->rotate(*rotationTemp);
	delete rotationTemp;
}

void BulletSharp::SoftBody::SoftBody::Scale(Vector3 scale)
{
	btVector3* scaleTemp = Math::Vector3ToBtVector3(scale);
	UnmanagedPointer->scale(*scaleTemp);
	delete scaleTemp;
}

void BulletSharp::SoftBody::SoftBody::SetMass(int node, btScalar mass)
{
	UnmanagedPointer->setMass(node, mass);
}

void BulletSharp::SoftBody::SoftBody::SetPose(bool bVolume, bool bFrame)
{
	UnmanagedPointer->setPose(bVolume, bFrame);
}

void BulletSharp::SoftBody::SoftBody::SetTotalDensity(btScalar density)
{
	UnmanagedPointer->setTotalDensity(density);
}

void BulletSharp::SoftBody::SoftBody::SetTotalMass(btScalar mass, bool fromFaces)
{
	UnmanagedPointer->setTotalMass(mass, fromFaces);
}

void BulletSharp::SoftBody::SoftBody::SetTotalMass(btScalar mass)
{
	UnmanagedPointer->setTotalMass(mass);
}

void BulletSharp::SoftBody::SoftBody::SetVelocity(Vector3 velocity)
{
	btVector3* velocityTemp = Math::Vector3ToBtVector3(velocity);
	UnmanagedPointer->setVelocity(*velocityTemp);
	delete velocityTemp;
}

void BulletSharp::SoftBody::SoftBody::SetVolumeDensity(btScalar density)
{
	UnmanagedPointer->setVolumeDensity(density);
}

void BulletSharp::SoftBody::SoftBody::SetVolumeMass(btScalar mass)
{
	UnmanagedPointer->setVolumeMass(mass);
}

void BulletSharp::SoftBody::SoftBody::StaticSolve(int iterations)
{
	UnmanagedPointer->staticSolve(iterations);
}

void BulletSharp::SoftBody::SoftBody::Transform(Matrix transform)
{
	btTransform* transformTemp = Math::MatrixToBtTransform(transform);
	UnmanagedPointer->transform(*transformTemp);
	delete transformTemp;
}

void BulletSharp::SoftBody::SoftBody::Translate(Vector3 translation)
{
	btVector3* translationTemp = Math::Vector3ToBtVector3(translation);
	UnmanagedPointer->translate(*translationTemp);
	delete translationTemp;
}

void BulletSharp::SoftBody::SoftBody::Translate(btScalar x, btScalar y, btScalar z)
{
	Translate(Vector3(x,y,z));
}

BulletSharp::SoftBody::SoftBody^ BulletSharp::SoftBody::SoftBody::Upcast(CollisionObject^ colObj)
{
	btSoftBody* body = btSoftBody::upcast(colObj->UnmanagedPointer);
	if (body == nullptr)
		return nullptr;
	return gcnew SoftBody(body);
}

array<Vector3>^ BulletSharp::SoftBody::SoftBody::Bounds::get()
{
	array<Vector3>^ boundsArray = gcnew array<Vector3>(2);
	boundsArray[0] = Math::BtVector3ToVector3(&UnmanagedPointer->m_bounds[0]);
	boundsArray[1] = Math::BtVector3ToVector3(&UnmanagedPointer->m_bounds[1]);
	return boundsArray;
}
void BulletSharp::SoftBody::SoftBody::Bounds::set(array<Vector3>^ value)
{
	Math::Vector3ToBtVector3(value[0], &UnmanagedPointer->m_bounds[0]);
	Math::Vector3ToBtVector3(value[1], &UnmanagedPointer->m_bounds[1]);
}

Config^ BulletSharp::SoftBody::SoftBody::Cfg::get()
{
	return gcnew Config(&UnmanagedPointer->m_cfg);
}
void BulletSharp::SoftBody::SoftBody::Cfg::set(Config^ value)
{
	UnmanagedPointer->m_cfg = *value->UnmanagedPointer;
}

int BulletSharp::SoftBody::SoftBody::ClusterCount::get()
{
	return UnmanagedPointer->clusterCount();
}

CollisionObjectArray^ BulletSharp::SoftBody::SoftBody::CollisionDisabledObjects::get()
{
	return gcnew CollisionObjectArray(&UnmanagedPointer->m_collisionDisabledObjects);
}
void BulletSharp::SoftBody::SoftBody::CollisionDisabledObjects::set(CollisionObjectArray^ value)
{
	UnmanagedPointer->m_collisionDisabledObjects = *value->UnmanagedPointer;
}

FaceArray^ BulletSharp::SoftBody::SoftBody::Faces::get()
{
	return gcnew FaceArray(&UnmanagedPointer->m_faces);
}
void BulletSharp::SoftBody::SoftBody::Faces::set(FaceArray^ value)
{
	UnmanagedPointer->m_faces = *value->UnmanagedPointer;
}

LinkArray^ BulletSharp::SoftBody::SoftBody::Links::get()
{
	return gcnew LinkArray(&UnmanagedPointer->m_links);
}
void BulletSharp::SoftBody::SoftBody::Links::set(LinkArray^ value)
{
	UnmanagedPointer->m_links = *value->UnmanagedPointer;
}

Matrix BulletSharp::SoftBody::SoftBody::InitialWorldTransform::get()
{
	return Math::BtTransformToMatrix(&UnmanagedPointer->m_initialWorldTransform);
}
void BulletSharp::SoftBody::SoftBody::InitialWorldTransform::set(Matrix value)
{
	return Math::MatrixToBtTransform(value, &UnmanagedPointer->m_initialWorldTransform);
}

MaterialArray^ BulletSharp::SoftBody::SoftBody::Materials::get()
{
	return gcnew MaterialArray(&UnmanagedPointer->m_materials);
}
void BulletSharp::SoftBody::SoftBody::Materials::set(MaterialArray^ value)
{
	UnmanagedPointer->m_materials = *value->UnmanagedPointer;
}

BulletSharp::SoftBody::NodeArray^ BulletSharp::SoftBody::SoftBody::Nodes::get()
{
	return gcnew NodeArray(&UnmanagedPointer->m_nodes);
}
void BulletSharp::SoftBody::SoftBody::Nodes::set(NodeArray^ value)
{
	UnmanagedPointer->m_nodes = *value->UnmanagedPointer;
}

BulletSharp::SoftBody::Pose^ BulletSharp::SoftBody::SoftBody::Pose::get()
{
	return gcnew BulletSharp::SoftBody::Pose(&UnmanagedPointer->m_pose);
}
#pragma managed(push, off)
void SoftBody_SetPose(btSoftBody* body, btSoftBody::Pose* pose)
{
	body->m_pose = *pose;
}
#pragma managed(pop)
void BulletSharp::SoftBody::SoftBody::Pose::set(BulletSharp::SoftBody::Pose^ value)
{
	SoftBody_SetPose(UnmanagedPointer, value->UnmanagedPointer);
}

SolverState^ BulletSharp::SoftBody::SoftBody::SolverState::get()
{
	return gcnew BulletSharp::SoftBody::SolverState(&UnmanagedPointer->m_sst);
}
void BulletSharp::SoftBody::SoftBody::SolverState::set(BulletSharp::SoftBody::SolverState^ value)
{
	UnmanagedPointer->m_sst = *value->UnmanagedPointer;
}

btScalar BulletSharp::SoftBody::SoftBody::TimeAccumulator::get()
{
	return UnmanagedPointer->m_timeacc;
}
void BulletSharp::SoftBody::SoftBody::TimeAccumulator::set(btScalar value)
{
	UnmanagedPointer->m_timeacc = value;
}

btScalar BulletSharp::SoftBody::SoftBody::TotalMass::get()
{
	return UnmanagedPointer->getTotalMass();
}
void BulletSharp::SoftBody::SoftBody::TotalMass::set(btScalar value)
{
	UnmanagedPointer->setTotalMass(value);
}

bool BulletSharp::SoftBody::SoftBody::UpdateRuntimeConstants::get()
{
	return UnmanagedPointer->m_bUpdateRtCst;
}
void BulletSharp::SoftBody::SoftBody::UpdateRuntimeConstants::set(bool value)
{
	UnmanagedPointer->m_bUpdateRtCst = value;
}

IntArray^ BulletSharp::SoftBody::SoftBody::UserIndexMapping::get()
{
	return gcnew IntArray(&UnmanagedPointer->m_userIndexMapping);
}
void BulletSharp::SoftBody::SoftBody::UserIndexMapping::set(IntArray^ value)
{
	UnmanagedPointer->m_userIndexMapping = *value->UnmanagedPointer;
}

btScalar BulletSharp::SoftBody::SoftBody::Volume::get()
{
	return UnmanagedPointer->getVolume();
}

Vector3 BulletSharp::SoftBody::SoftBody::WindVelocity::get()
{
	return Math::BtVector3ToVector3(&UnmanagedPointer->getWindVelocity());
}
void BulletSharp::SoftBody::SoftBody::WindVelocity::set(Vector3 value)
{
	btVector3* tempWindVelocity = Math::Vector3ToBtVector3(value);
	UnmanagedPointer->setWindVelocity(*tempWindVelocity);
	delete tempWindVelocity;
}

SoftBodyWorldInfo^ BulletSharp::SoftBody::SoftBody::WorldInfo::get()
{
	return gcnew SoftBodyWorldInfo(UnmanagedPointer->getWorldInfo());
}
void BulletSharp::SoftBody::SoftBody::WorldInfo::set(SoftBodyWorldInfo^ value)
{
	UnmanagedPointer->m_worldInfo = value->UnmanagedPointer;
}

#ifndef DISABLE_DBVT
Dbvt^ BulletSharp::SoftBody::SoftBody::ClusterDbvt::get()
{
	return gcnew Dbvt(&UnmanagedPointer->m_cdbvt);
}
void BulletSharp::SoftBody::SoftBody::ClusterDbvt::set(Dbvt^ value)
{
	UnmanagedPointer->m_cdbvt = *value->UnmanagedPointer;
}

Dbvt^ BulletSharp::SoftBody::SoftBody::FaceDbvt::get()
{
	return gcnew Dbvt(&UnmanagedPointer->m_fdbvt);
}
void BulletSharp::SoftBody::SoftBody::FaceDbvt::set(Dbvt^ value)
{
	UnmanagedPointer->m_fdbvt = *value->UnmanagedPointer;
}

Dbvt^ BulletSharp::SoftBody::SoftBody::NodeDbvt::get()
{
	return gcnew Dbvt(&UnmanagedPointer->m_ndbvt);
}
void BulletSharp::SoftBody::SoftBody::NodeDbvt::set(Dbvt^ value)
{
	UnmanagedPointer->m_ndbvt = *value->UnmanagedPointer;
}
#endif

btSoftBody* BulletSharp::SoftBody::SoftBody::UnmanagedPointer::get()
{
	return (btSoftBody*)CollisionObject::UnmanagedPointer;
}

#endif
