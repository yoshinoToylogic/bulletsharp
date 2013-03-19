#include "btContactSolverInfo_wrap.h"

btContactSolverInfoData* btContactSolverInfoData_new()
{
	return new btContactSolverInfoData();
}

btScalar btContactSolverInfoData_getDamping(btContactSolverInfoData* obj)
{
	return obj->m_damping;
}

btScalar btContactSolverInfoData_getErp(btContactSolverInfoData* obj)
{
	return obj->m_erp;
}

btScalar btContactSolverInfoData_getErp2(btContactSolverInfoData* obj)
{
	return obj->m_erp2;
}

btScalar btContactSolverInfoData_getFriction(btContactSolverInfoData* obj)
{
	return obj->m_friction;
}

btScalar btContactSolverInfoData_getGlobalCfm(btContactSolverInfoData* obj)
{
	return obj->m_globalCfm;
}

btScalar btContactSolverInfoData_getLinearSlop(btContactSolverInfoData* obj)
{
	return obj->m_linearSlop;
}

btScalar btContactSolverInfoData_getMaxErrorReduction(btContactSolverInfoData* obj)
{
	return obj->m_maxErrorReduction;
}

btScalar btContactSolverInfoData_getMaxGyroscopicForce(btContactSolverInfoData* obj)
{
	return obj->m_maxGyroscopicForce;
}

int btContactSolverInfoData_getMinimumSolverBatchSize(btContactSolverInfoData* obj)
{
	return obj->m_minimumSolverBatchSize;
}

int btContactSolverInfoData_getNumIterations(btContactSolverInfoData* obj)
{
	return obj->m_numIterations;
}

int btContactSolverInfoData_getRestingContactRestitutionThreshold(btContactSolverInfoData* obj)
{
	return obj->m_restingContactRestitutionThreshold;
}

btScalar btContactSolverInfoData_getRestitution(btContactSolverInfoData* obj)
{
	return obj->m_restitution;
}

btScalar btContactSolverInfoData_getSingleAxisRollingFrictionThreshold(btContactSolverInfoData* obj)
{
	return obj->m_singleAxisRollingFrictionThreshold;
}

int btContactSolverInfoData_getSolverMode(btContactSolverInfoData* obj)
{
	return obj->m_solverMode;
}

btScalar btContactSolverInfoData_getSor(btContactSolverInfoData* obj)
{
	return obj->m_sor;
}

int btContactSolverInfoData_getSplitImpulse(btContactSolverInfoData* obj)
{
	return obj->m_splitImpulse;
}

btScalar btContactSolverInfoData_getSplitImpulsePenetrationThreshold(btContactSolverInfoData* obj)
{
	return obj->m_splitImpulsePenetrationThreshold;
}

btScalar btContactSolverInfoData_getSplitImpulseTurnErp(btContactSolverInfoData* obj)
{
	return obj->m_splitImpulseTurnErp;
}

btScalar btContactSolverInfoData_getTau(btContactSolverInfoData* obj)
{
	return obj->m_tau;
}

btScalar btContactSolverInfoData_getTimeStep(btContactSolverInfoData* obj)
{
	return obj->m_timeStep;
}

btScalar btContactSolverInfoData_getWarmstartingFactor(btContactSolverInfoData* obj)
{
	return obj->m_warmstartingFactor;
}

void btContactSolverInfoData_setDamping(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_damping = value;
}

void btContactSolverInfoData_setErp(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_erp = value;
}

void btContactSolverInfoData_setErp2(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_erp2 = value;
}

void btContactSolverInfoData_setFriction(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_friction = value;
}

void btContactSolverInfoData_setGlobalCfm(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_globalCfm = value;
}

void btContactSolverInfoData_setLinearSlop(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_linearSlop = value;
}

void btContactSolverInfoData_setMaxErrorReduction(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_maxErrorReduction = value;
}

void btContactSolverInfoData_setMaxGyroscopicForce(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_maxGyroscopicForce = value;
}

void btContactSolverInfoData_setMinimumSolverBatchSize(btContactSolverInfoData* obj, int value)
{
	obj->m_minimumSolverBatchSize = value;
}

void btContactSolverInfoData_setNumIterations(btContactSolverInfoData* obj, int value)
{
	obj->m_numIterations = value;
}

void btContactSolverInfoData_setRestingContactRestitutionThreshold(btContactSolverInfoData* obj, int value)
{
	obj->m_restingContactRestitutionThreshold = value;
}

void btContactSolverInfoData_setRestitution(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_restitution = value;
}

void btContactSolverInfoData_setSingleAxisRollingFrictionThreshold(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_singleAxisRollingFrictionThreshold = value;
}

void btContactSolverInfoData_setSolverMode(btContactSolverInfoData* obj, int value)
{
	obj->m_solverMode = value;
}

void btContactSolverInfoData_setSor(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_sor = value;
}

void btContactSolverInfoData_setSplitImpulse(btContactSolverInfoData* obj, int value)
{
	obj->m_splitImpulse = value;
}

void btContactSolverInfoData_setSplitImpulsePenetrationThreshold(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_splitImpulsePenetrationThreshold = value;
}

void btContactSolverInfoData_setSplitImpulseTurnErp(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_splitImpulseTurnErp = value;
}

void btContactSolverInfoData_setTau(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_tau = value;
}

void btContactSolverInfoData_setTimeStep(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_timeStep = value;
}

void btContactSolverInfoData_setWarmstartingFactor(btContactSolverInfoData* obj, btScalar value)
{
	obj->m_warmstartingFactor = value;
}

void btContactSolverInfoData_delete(btContactSolverInfoData* obj)
{
	delete obj;
}

btContactSolverInfo* btContactSolverInfo_new()
{
	return new btContactSolverInfo();
}

btContactSolverInfoDoubleData* btContactSolverInfoDoubleData_new()
{
	return new btContactSolverInfoDoubleData();
}

double btContactSolverInfoDoubleData_getDamping(btContactSolverInfoDoubleData* obj)
{
	return obj->m_damping;
}

double btContactSolverInfoDoubleData_getErp(btContactSolverInfoDoubleData* obj)
{
	return obj->m_erp;
}

double btContactSolverInfoDoubleData_getErp2(btContactSolverInfoDoubleData* obj)
{
	return obj->m_erp2;
}

double btContactSolverInfoDoubleData_getFriction(btContactSolverInfoDoubleData* obj)
{
	return obj->m_friction;
}

double btContactSolverInfoDoubleData_getGlobalCfm(btContactSolverInfoDoubleData* obj)
{
	return obj->m_globalCfm;
}

double btContactSolverInfoDoubleData_getLinearSlop(btContactSolverInfoDoubleData* obj)
{
	return obj->m_linearSlop;
}

double btContactSolverInfoDoubleData_getMaxErrorReduction(btContactSolverInfoDoubleData* obj)
{
	return obj->m_maxErrorReduction;
}

double btContactSolverInfoDoubleData_getMaxGyroscopicForce(btContactSolverInfoDoubleData* obj)
{
	return obj->m_maxGyroscopicForce;
}

int btContactSolverInfoDoubleData_getMinimumSolverBatchSize(btContactSolverInfoDoubleData* obj)
{
	return obj->m_minimumSolverBatchSize;
}

int btContactSolverInfoDoubleData_getNumIterations(btContactSolverInfoDoubleData* obj)
{
	return obj->m_numIterations;
}

char* btContactSolverInfoDoubleData_getPadding(btContactSolverInfoDoubleData* obj)
{
	return obj->m_padding;
}

int btContactSolverInfoDoubleData_getRestingContactRestitutionThreshold(btContactSolverInfoDoubleData* obj)
{
	return obj->m_restingContactRestitutionThreshold;
}

double btContactSolverInfoDoubleData_getRestitution(btContactSolverInfoDoubleData* obj)
{
	return obj->m_restitution;
}

double btContactSolverInfoDoubleData_getSingleAxisRollingFrictionThreshold(btContactSolverInfoDoubleData* obj)
{
	return obj->m_singleAxisRollingFrictionThreshold;
}

int btContactSolverInfoDoubleData_getSolverMode(btContactSolverInfoDoubleData* obj)
{
	return obj->m_solverMode;
}

double btContactSolverInfoDoubleData_getSor(btContactSolverInfoDoubleData* obj)
{
	return obj->m_sor;
}

int btContactSolverInfoDoubleData_getSplitImpulse(btContactSolverInfoDoubleData* obj)
{
	return obj->m_splitImpulse;
}

double btContactSolverInfoDoubleData_getSplitImpulsePenetrationThreshold(btContactSolverInfoDoubleData* obj)
{
	return obj->m_splitImpulsePenetrationThreshold;
}

double btContactSolverInfoDoubleData_getSplitImpulseTurnErp(btContactSolverInfoDoubleData* obj)
{
	return obj->m_splitImpulseTurnErp;
}

double btContactSolverInfoDoubleData_getTau(btContactSolverInfoDoubleData* obj)
{
	return obj->m_tau;
}

double btContactSolverInfoDoubleData_getTimeStep(btContactSolverInfoDoubleData* obj)
{
	return obj->m_timeStep;
}

double btContactSolverInfoDoubleData_getWarmstartingFactor(btContactSolverInfoDoubleData* obj)
{
	return obj->m_warmstartingFactor;
}

void btContactSolverInfoDoubleData_setDamping(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_damping = value;
}

void btContactSolverInfoDoubleData_setErp(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_erp = value;
}

void btContactSolverInfoDoubleData_setErp2(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_erp2 = value;
}

void btContactSolverInfoDoubleData_setFriction(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_friction = value;
}

void btContactSolverInfoDoubleData_setGlobalCfm(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_globalCfm = value;
}

void btContactSolverInfoDoubleData_setLinearSlop(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_linearSlop = value;
}

void btContactSolverInfoDoubleData_setMaxErrorReduction(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_maxErrorReduction = value;
}

void btContactSolverInfoDoubleData_setMaxGyroscopicForce(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_maxGyroscopicForce = value;
}

void btContactSolverInfoDoubleData_setMinimumSolverBatchSize(btContactSolverInfoDoubleData* obj, int value)
{
	obj->m_minimumSolverBatchSize = value;
}

void btContactSolverInfoDoubleData_setNumIterations(btContactSolverInfoDoubleData* obj, int value)
{
	obj->m_numIterations = value;
}
/*
void btContactSolverInfoDoubleData_setPadding(btContactSolverInfoDoubleData* obj, char* value)
{
	obj->m_padding = value;
}
*/
void btContactSolverInfoDoubleData_setRestingContactRestitutionThreshold(btContactSolverInfoDoubleData* obj, int value)
{
	obj->m_restingContactRestitutionThreshold = value;
}

void btContactSolverInfoDoubleData_setRestitution(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_restitution = value;
}

void btContactSolverInfoDoubleData_setSingleAxisRollingFrictionThreshold(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_singleAxisRollingFrictionThreshold = value;
}

void btContactSolverInfoDoubleData_setSolverMode(btContactSolverInfoDoubleData* obj, int value)
{
	obj->m_solverMode = value;
}

void btContactSolverInfoDoubleData_setSor(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_sor = value;
}

void btContactSolverInfoDoubleData_setSplitImpulse(btContactSolverInfoDoubleData* obj, int value)
{
	obj->m_splitImpulse = value;
}

void btContactSolverInfoDoubleData_setSplitImpulsePenetrationThreshold(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_splitImpulsePenetrationThreshold = value;
}

void btContactSolverInfoDoubleData_setSplitImpulseTurnErp(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_splitImpulseTurnErp = value;
}

void btContactSolverInfoDoubleData_setTau(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_tau = value;
}

void btContactSolverInfoDoubleData_setTimeStep(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_timeStep = value;
}

void btContactSolverInfoDoubleData_setWarmstartingFactor(btContactSolverInfoDoubleData* obj, double value)
{
	obj->m_warmstartingFactor = value;
}

void btContactSolverInfoDoubleData_delete(btContactSolverInfoDoubleData* obj)
{
	delete obj;
}

btContactSolverInfoFloatData* btContactSolverInfoFloatData_new()
{
	return new btContactSolverInfoFloatData();
}

float btContactSolverInfoFloatData_getDamping(btContactSolverInfoFloatData* obj)
{
	return obj->m_damping;
}

float btContactSolverInfoFloatData_getErp(btContactSolverInfoFloatData* obj)
{
	return obj->m_erp;
}

float btContactSolverInfoFloatData_getErp2(btContactSolverInfoFloatData* obj)
{
	return obj->m_erp2;
}

float btContactSolverInfoFloatData_getFriction(btContactSolverInfoFloatData* obj)
{
	return obj->m_friction;
}

float btContactSolverInfoFloatData_getGlobalCfm(btContactSolverInfoFloatData* obj)
{
	return obj->m_globalCfm;
}

float btContactSolverInfoFloatData_getLinearSlop(btContactSolverInfoFloatData* obj)
{
	return obj->m_linearSlop;
}

float btContactSolverInfoFloatData_getMaxErrorReduction(btContactSolverInfoFloatData* obj)
{
	return obj->m_maxErrorReduction;
}

float btContactSolverInfoFloatData_getMaxGyroscopicForce(btContactSolverInfoFloatData* obj)
{
	return obj->m_maxGyroscopicForce;
}

int btContactSolverInfoFloatData_getMinimumSolverBatchSize(btContactSolverInfoFloatData* obj)
{
	return obj->m_minimumSolverBatchSize;
}

int btContactSolverInfoFloatData_getNumIterations(btContactSolverInfoFloatData* obj)
{
	return obj->m_numIterations;
}

char* btContactSolverInfoFloatData_getPadding(btContactSolverInfoFloatData* obj)
{
	return obj->m_padding;
}

int btContactSolverInfoFloatData_getRestingContactRestitutionThreshold(btContactSolverInfoFloatData* obj)
{
	return obj->m_restingContactRestitutionThreshold;
}

float btContactSolverInfoFloatData_getRestitution(btContactSolverInfoFloatData* obj)
{
	return obj->m_restitution;
}

float btContactSolverInfoFloatData_getSingleAxisRollingFrictionThreshold(btContactSolverInfoFloatData* obj)
{
	return obj->m_singleAxisRollingFrictionThreshold;
}

int btContactSolverInfoFloatData_getSolverMode(btContactSolverInfoFloatData* obj)
{
	return obj->m_solverMode;
}

float btContactSolverInfoFloatData_getSor(btContactSolverInfoFloatData* obj)
{
	return obj->m_sor;
}

int btContactSolverInfoFloatData_getSplitImpulse(btContactSolverInfoFloatData* obj)
{
	return obj->m_splitImpulse;
}

float btContactSolverInfoFloatData_getSplitImpulsePenetrationThreshold(btContactSolverInfoFloatData* obj)
{
	return obj->m_splitImpulsePenetrationThreshold;
}

float btContactSolverInfoFloatData_getSplitImpulseTurnErp(btContactSolverInfoFloatData* obj)
{
	return obj->m_splitImpulseTurnErp;
}

float btContactSolverInfoFloatData_getTau(btContactSolverInfoFloatData* obj)
{
	return obj->m_tau;
}

float btContactSolverInfoFloatData_getTimeStep(btContactSolverInfoFloatData* obj)
{
	return obj->m_timeStep;
}

float btContactSolverInfoFloatData_getWarmstartingFactor(btContactSolverInfoFloatData* obj)
{
	return obj->m_warmstartingFactor;
}

void btContactSolverInfoFloatData_setDamping(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_damping = value;
}

void btContactSolverInfoFloatData_setErp(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_erp = value;
}

void btContactSolverInfoFloatData_setErp2(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_erp2 = value;
}

void btContactSolverInfoFloatData_setFriction(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_friction = value;
}

void btContactSolverInfoFloatData_setGlobalCfm(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_globalCfm = value;
}

void btContactSolverInfoFloatData_setLinearSlop(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_linearSlop = value;
}

void btContactSolverInfoFloatData_setMaxErrorReduction(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_maxErrorReduction = value;
}

void btContactSolverInfoFloatData_setMaxGyroscopicForce(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_maxGyroscopicForce = value;
}

void btContactSolverInfoFloatData_setMinimumSolverBatchSize(btContactSolverInfoFloatData* obj, int value)
{
	obj->m_minimumSolverBatchSize = value;
}

void btContactSolverInfoFloatData_setNumIterations(btContactSolverInfoFloatData* obj, int value)
{
	obj->m_numIterations = value;
}
/*
void btContactSolverInfoFloatData_setPadding(btContactSolverInfoFloatData* obj, char* value)
{
	obj->m_padding = value;
}
*/
void btContactSolverInfoFloatData_setRestingContactRestitutionThreshold(btContactSolverInfoFloatData* obj, int value)
{
	obj->m_restingContactRestitutionThreshold = value;
}

void btContactSolverInfoFloatData_setRestitution(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_restitution = value;
}

void btContactSolverInfoFloatData_setSingleAxisRollingFrictionThreshold(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_singleAxisRollingFrictionThreshold = value;
}

void btContactSolverInfoFloatData_setSolverMode(btContactSolverInfoFloatData* obj, int value)
{
	obj->m_solverMode = value;
}

void btContactSolverInfoFloatData_setSor(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_sor = value;
}

void btContactSolverInfoFloatData_setSplitImpulse(btContactSolverInfoFloatData* obj, int value)
{
	obj->m_splitImpulse = value;
}

void btContactSolverInfoFloatData_setSplitImpulsePenetrationThreshold(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_splitImpulsePenetrationThreshold = value;
}

void btContactSolverInfoFloatData_setSplitImpulseTurnErp(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_splitImpulseTurnErp = value;
}

void btContactSolverInfoFloatData_setTau(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_tau = value;
}

void btContactSolverInfoFloatData_setTimeStep(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_timeStep = value;
}

void btContactSolverInfoFloatData_setWarmstartingFactor(btContactSolverInfoFloatData* obj, float value)
{
	obj->m_warmstartingFactor = value;
}

void btContactSolverInfoFloatData_delete(btContactSolverInfoFloatData* obj)
{
	delete obj;
}
