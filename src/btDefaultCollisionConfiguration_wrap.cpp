#include "btDefaultCollisionConfiguration_wrap.h"

btDefaultCollisionConfiguration* btDefaultCollisionConfiguration_new(const btDefaultCollisionConstructionInfo* constructionInfo)
{
    return new btDefaultCollisionConfiguration(*constructionInfo);
}

btDefaultCollisionConfiguration* btDefaultCollisionConfiguration_new2()
{
    return new btDefaultCollisionConfiguration();
}
