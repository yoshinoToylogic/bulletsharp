#include "StdAfx.h"
#include "Collections.h"
#include "CompoundShape.h"

#ifndef DISABLE_DBVT
#include "Dbvt.h"
#include "DbvtBroadphase.h"
#endif
#ifndef DISABLE_SOFTBODY
#include "SoftBody.h"
#endif

ListDebugView::ListDebugView(IEnumerable^ list)
{
	_list = list;
}

ArrayList^ ListDebugView::Items::get()
{
	ArrayList^ list = gcnew ArrayList();
	for each(Object^ o in _list)
		list->Add(o);
	return list;
}


Vector3ListDebugView::Vector3ListDebugView(Generic::IList<Vector3>^ list)
{
	_list = list;
}

array<Vector3>^ Vector3ListDebugView::Items::get()
{
	array<Vector3>^ arr = gcnew array<Vector3>(_list->Count);
	_list->CopyTo(arr, 0);
	return arr;
}


generic<class T>
ListEnumerator<T>::ListEnumerator(Generic::IList<T>^ list)
{
	_list = list;
	i=-1;
	_count = _list->Count;
}

generic<class T>
T ListEnumerator<T>::GenericCurrent::get()
{
	return _list[i];
}

generic<class T>
Object^ ListEnumerator<T>::Current::get()
{
	return GenericCurrent::get();
}

generic<class T>
bool ListEnumerator<T>::MoveNext()
{
	i++;
	return (i < _count);
}

generic<class T>
void ListEnumerator<T>::Reset()
{
	i=-1;
}


generic<class T>
GenericList<T>::GenericList(void* array, int length)
{
	_unmanaged = array;
	_length = length;
	isReadOnly = false;
}

generic<class T>
GenericList<T>::GenericList(const void* array, int length)
{
	_unmanaged = (void*)array;
	_length = length;
	isReadOnly = true;
}

generic<class T>
void GenericList<T>::Add(T item)
{
	throw gcnew System::NotSupportedException("Cannot resize array.");
}

generic<class T>
void GenericList<T>::Clear()
{
	throw gcnew System::NotSupportedException("Cannot resize array.");
}

generic<class T>
IEnumerator^ GenericList<T>::GetEnumerator()
{
	return gcnew ListEnumerator<T>(this);
}

generic<class T>
Generic::IEnumerator<T>^ GenericList<T>::GetSpecializedEnumerator()
{
	return gcnew ListEnumerator<T>(this);
}

generic<class T>
bool GenericList<T>::Contains(T item)
{
	return IndexOf(item) != -1;
}

generic<class T>
int GenericList<T>::IndexOf(T item)
{
	/*
	If we're here, it means that the underlying struct type doesn't have
	an equality operator(==). So we use its C# counterpart,
	which does a member-wise comparison.
	*/
	int i;
	for (i=0; i<Count; i++)
	{
		if (this[i]->Equals(item))
			return i;
	}
	return -1;
}

generic<class T>
void GenericList<T>::Insert(int index, T item)
{
	throw gcnew System::NotSupportedException("Cannot resize array.");
}

generic<class T>
bool GenericList<T>::Remove(T item)
{
	throw gcnew System::NotSupportedException("Cannot resize array.");
}

generic<class T>
void GenericList<T>::RemoveAt(int index)
{
	throw gcnew System::NotSupportedException("Cannot resize array.");
}

generic<class T>
int GenericList<T>::Count::get()
{
	return _length;
}

generic<class T>
bool GenericList<T>::IsReadOnly::get()
{
	return isReadOnly;
}


#define Unmanaged static_cast<bool*>(_unmanaged)

BoolArray::BoolArray(bool* boolArray, int length)
: GenericList<bool>(boolArray, length)
{
}

BoolArray::BoolArray(const bool* boolArray, int length)
: GenericList<bool>(boolArray, length)
{
}

bool BoolArray::Contains(bool item)
{
	int i;
	int length = Count;
	for (i=Count; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return true;
		}
	}
	return false;
}

void BoolArray::CopyTo(array<bool>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	if (arrayIndex + Count > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = Unmanaged[i];
	}
}

int BoolArray::IndexOf(bool item)
{
	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return i;
		}
	}
	return -1;
}

bool BoolArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	return Unmanaged[index];
}
void BoolArray::default::set(int index, bool value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	Unmanaged[index] = value;
}


#ifndef DISABLE_SOFTBODY

#undef Unmanaged
#define Unmanaged static_cast<btSoftBody::Body*>(_unmanaged)

SoftBody::BodyArray::BodyArray(btSoftBody::Body* bodyArray, int length)
: GenericList<Body^>(bodyArray, length)
{
}

SoftBody::BodyArray::BodyArray(const btSoftBody::Body* bodyArray, int length)
: GenericList<Body^>(bodyArray, length)
{
}

void SoftBody::BodyArray::CopyTo(array<Body^>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	int length = Count;
	if (arrayIndex + length > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = gcnew Body(&Unmanaged[i]);
	}
}

SoftBody::Body^ SoftBody::BodyArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	return gcnew Body(&Unmanaged[index]);
}

void SoftBody::BodyArray::default::set(int index, Body^ value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	Unmanaged[index] = *value->_unmanaged;
}
#endif


#undef Unmanaged
#define Unmanaged static_cast<btCompoundShapeChild*>(_unmanaged)

CompoundShapeChildArray::CompoundShapeChildArray(btCompoundShapeChild* shapeArray, int length)
: GenericList<CompoundShapeChild^>(shapeArray, length)
{
}

CompoundShapeChildArray::CompoundShapeChildArray(const btCompoundShapeChild* shapeArray, int length)
: GenericList<CompoundShapeChild^>(shapeArray, length)
{
}

void CompoundShapeChildArray::CopyTo(array<CompoundShapeChild^>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	int length = Count;
	if (arrayIndex + length > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = gcnew CompoundShapeChild(&Unmanaged[i]);
	}
}

CompoundShapeChild^ CompoundShapeChildArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	return gcnew CompoundShapeChild(&Unmanaged[index]);
}

void CompoundShapeChildArray_SetDefault(btCompoundShapeChild* shapeArray,
	int index, btCompoundShapeChild* shape)
{
	shapeArray[index] = *shape;
}
void CompoundShapeChildArray::default::set(int index, CompoundShapeChild^ value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	CompoundShapeChildArray_SetDefault(Unmanaged, index, value->UnmanagedPointer);
}


#ifndef DISABLE_DBVT

#undef Unmanaged
#define Unmanaged static_cast<btDbvt*>(_unmanaged)

DbvtArray::DbvtArray(btDbvt* dbvtArray, int length)
: GenericList<Dbvt^>(dbvtArray, length)
{
}

DbvtArray::DbvtArray(const btDbvt* dbvtArray, int length)
: GenericList<Dbvt^>(dbvtArray, length)
{
}

void DbvtArray::CopyTo(array<Dbvt^>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	int length = Count;
	if (arrayIndex + length > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = gcnew Dbvt(&Unmanaged[i]);
	}
}

Dbvt^ DbvtArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");

	return gcnew Dbvt(&Unmanaged[index]);
}
void DbvtArray::default::set(int index, Dbvt^ value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	Unmanaged[index] = *value->UnmanagedPointer;

}


#undef Unmanaged
#define Unmanaged static_cast<btDbvtNode**>(_unmanaged)

DbvtNodePtrArray::DbvtNodePtrArray(btDbvtNode** nodePtrArray, int length)
: GenericList<DbvtNode^>(nodePtrArray, length)
{
}

DbvtNodePtrArray::DbvtNodePtrArray(const btDbvtNode** nodePtrArray, int length)
: GenericList<DbvtNode^>(nodePtrArray, length)
{
}

void DbvtNodePtrArray::CopyTo(array<DbvtNode^>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	int length = Count;
	if (arrayIndex + length > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = gcnew DbvtNode(Unmanaged[i]);
	}
}

DbvtNode^ DbvtNodePtrArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");

	btDbvtNode* nodePtr = Unmanaged[index];
	if (nodePtr == 0)
		return nullptr;

	return gcnew DbvtNode(nodePtr);
}
void DbvtNodePtrArray::default::set(int index, DbvtNode^ value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");

	Unmanaged[index] = GetUnmanagedNullable(value);
}


#undef Unmanaged
#define Unmanaged static_cast<btDbvtProxy**>(_unmanaged)

DbvtProxyPtrArray::DbvtProxyPtrArray(btDbvtProxy** proxyPtrArray, int length)
: GenericList<DbvtProxy^>(proxyPtrArray, length)
{
}

DbvtProxyPtrArray::DbvtProxyPtrArray(const btDbvtProxy** proxyPtrArray, int length)
: GenericList<DbvtProxy^>(proxyPtrArray, length)
{
}

void DbvtProxyPtrArray::CopyTo(array<DbvtProxy^>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	int length = Count;
	if (arrayIndex + length > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = dynamic_cast<DbvtProxy^>(DbvtProxy::GetManaged(Unmanaged[i]));
	}
}

DbvtProxy^ DbvtProxyPtrArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");

	btDbvtProxy* proxyPtr = Unmanaged[index];
	if (proxyPtr == 0)
		return nullptr;

	return dynamic_cast<DbvtProxy^>(DbvtProxy::GetManaged(proxyPtr));
}
void DbvtProxyPtrArray::default::set(int index, DbvtProxy^ value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");

	Unmanaged[index] = GetUnmanagedNullable(value);
}
#endif


#undef Unmanaged
#define Unmanaged static_cast<float*>(_unmanaged)

FloatArray::FloatArray(float* floatArray, int length)
: GenericList<float>(floatArray, length)
{
}

FloatArray::FloatArray(const float* floatArray, int length)
: GenericList<float>(floatArray, length)
{
}

FloatArray::FloatArray(int length)
: GenericList<float>(new float[length], length)
{
}

bool FloatArray::Contains(float item)
{
	int i;
	int length = Count;
	for (i=Count; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return true;
		}
	}
	return false;
}

void FloatArray::CopyTo(array<float>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	if (arrayIndex + Count > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = Unmanaged[i];
	}
}

int FloatArray::IndexOf(float item)
{
	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return i;
		}
	}
	return -1;
}

float FloatArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	return Unmanaged[index];
}
void FloatArray::default::set(int index, float value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	Unmanaged[index] = value;
}


#undef Unmanaged
#define Unmanaged static_cast<int*>(_unmanaged)

IntArray::IntArray(int* intArray, int length)
: GenericList<int>(intArray, length)
{
}

IntArray::IntArray(const int* intArray, int length)
: GenericList<int>(intArray, length)
{
}

IntArray::IntArray(int length)
: GenericList<int>(new int[length], length)
{
}

bool IntArray::Contains(int item)
{
	int i;
	int length = Count;
	for (i=Count; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return true;
		}
	}
	return false;
}

void IntArray::CopyTo(array<int>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	if (arrayIndex + Count > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = Unmanaged[i];
	}
}

int IntArray::IndexOf(int item)
{
	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return i;
		}
	}
	return -1;
}

int IntArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	return Unmanaged[index];
}
void IntArray::default::set(int index, int value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	Unmanaged[index] = value;
}


#ifndef DISABLE_SOFTBODY

#undef Unmanaged
#define Unmanaged static_cast<btSoftBody::Node**>(_unmanaged)

SoftBody::NodePtrArray::NodePtrArray(btSoftBody::Node** nodePtrArray, int length)
: GenericList<Node^>(nodePtrArray, length)
{
}

SoftBody::NodePtrArray::NodePtrArray(const btSoftBody::Node** nodePtrArray, int length)
: GenericList<Node^>(nodePtrArray, length)
{
}

void SoftBody::NodePtrArray::CopyTo(array<Node^>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	int length = Count;
	if (arrayIndex + length > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = gcnew Node(Unmanaged[i]);
	}
}

SoftBody::Node^ SoftBody::NodePtrArray::default::get(int index)
{
	btSoftBody::Node* nodePtr = Unmanaged[index];
	if (nodePtr == 0)
		return nullptr;
	return gcnew Node(nodePtr);
}
void SoftBody::NodePtrArray::default::set(int index, Node^ value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	Unmanaged[index] = (btSoftBody::Node*)GetUnmanagedNullable(value);
}
#endif


#undef Unmanaged
#define Unmanaged static_cast<btScalar*>(_unmanaged)

ScalarArray::ScalarArray(btScalar* scalarArray, int length)
: GenericList<btScalar>(scalarArray, length)
{
}

ScalarArray::ScalarArray(const btScalar* scalarArray, int length)
: GenericList<btScalar>(scalarArray, length)
{
}

ScalarArray::ScalarArray(int length)
: GenericList<btScalar>(new btScalar[length], length)
{
}

bool ScalarArray::Contains(btScalar item)
{
	int i;
	int length = Count;
	for (i=Count; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return true;
		}
	}
	return false;
}

void ScalarArray::CopyTo(array<btScalar>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	if (arrayIndex + Count > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = Unmanaged[i];
	}
}

int ScalarArray::IndexOf(btScalar item)
{
	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return i;
		}
	}
	return -1;
}

btScalar ScalarArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	return Unmanaged[index];
}
void ScalarArray::default::set(int index, btScalar value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	Unmanaged[index] = value;
}


#undef Unmanaged
#define Unmanaged static_cast<unsigned int*>(_unmanaged)

UIntArray::UIntArray(unsigned int* uintArray, int length)
: GenericList<unsigned int>(uintArray, length)
{
}

UIntArray::UIntArray(const unsigned int* uintArray, int length)
: GenericList<unsigned int>(uintArray, length)
{
}

bool UIntArray::Contains(unsigned int item)
{
	int i;
	int length = Count;
	for (i=Count; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return true;
		}
	}
	return false;
}

void UIntArray::CopyTo(array<unsigned int>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	if (arrayIndex + Count > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = Unmanaged[i];
	}
}

int UIntArray::IndexOf(unsigned int item)
{
	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return i;
		}
	}
	return -1;
}

unsigned int UIntArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	return Unmanaged[index];
}
void UIntArray::default::set(int index, unsigned int value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	Unmanaged[index] = value;
}


#undef Unmanaged
#define Unmanaged static_cast<unsigned short*>(_unmanaged)

UShortArray::UShortArray(unsigned short* uShortArray, int length)
: GenericList<unsigned short>(uShortArray, length)
{
}

UShortArray::UShortArray(const unsigned short* uShortArray, int length)
: GenericList<unsigned short>(uShortArray, length)
{
}

bool UShortArray::Contains(unsigned short item)
{
	int i;
	int length = Count;
	for (i=Count; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return true;
		}
	}
	return false;
}

void UShortArray::CopyTo(array<unsigned short>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	if (arrayIndex + Count > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		array[arrayIndex+i] = Unmanaged[i];
	}
}

int UShortArray::IndexOf(unsigned short item)
{
	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		if (item == Unmanaged[i])
		{
			return i;
		}
	}
	return -1;
}

unsigned short UShortArray::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	return Unmanaged[index];
}
void UShortArray::default::set(int index, unsigned short value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	Unmanaged[index] = value;
}


#undef Unmanaged
#define Unmanaged static_cast<btVector3*>(_unmanaged)

Vector3Array::Vector3Array(btVector3* vector3Array, int length)
: GenericList<Vector3>(vector3Array, length)
{
}

Vector3Array::Vector3Array(const btVector3* vector3Array, int length)
: GenericList<Vector3>(vector3Array, length)
{
}

Vector3Array::Vector3Array(int length)
: GenericList<Vector3>(new btVector3[length], length)
{
}

bool Vector3Array::Contains(Vector3 item)
{
	VECTOR3_DEF(item);

	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		btVector3* vector = &Unmanaged[i];
		if (VECTOR3_PTR(item)->m_floats[0] == vector->m_floats[0] &&
			VECTOR3_PTR(item)->m_floats[1] == vector->m_floats[1] &&
			VECTOR3_PTR(item)->m_floats[2] == vector->m_floats[2])
		{
			VECTOR3_DEL(item);
			return true;
		}
	}
	VECTOR3_DEL(item);
	return false;
}

void Vector3Array::CopyTo(array<Vector3>^ array, int arrayIndex)
{
	if (array == nullptr)
		throw gcnew ArgumentNullException("array");

	if (arrayIndex < 0)
		throw gcnew ArgumentOutOfRangeException("array");

	int length = Count;
	if (arrayIndex + length > array->Length)
		throw gcnew ArgumentException("Array too small.");

	int i;
	for (i=0; i<length; i++)
	{
		Math::BtVector3ToVector3(&Unmanaged[i], array[arrayIndex+i]);
	}
}

int Vector3Array::IndexOf(Vector3 item)
{
	VECTOR3_DEF(item);

	int i;
	int length = Count;
	for (i=0; i<length; i++)
	{
		btVector3* vector = &Unmanaged[i];
		if (VECTOR3_PTR(item)->m_floats[0] == vector->m_floats[0] &&
			VECTOR3_PTR(item)->m_floats[1] == vector->m_floats[1] &&
			VECTOR3_PTR(item)->m_floats[2] == vector->m_floats[2])
		{
			VECTOR3_DEL(item);
			return i;
		}
	}
	VECTOR3_DEL(item);
	return -1;
}

Vector3 Vector3Array::default::get(int index)
{
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	return Math::BtVector3ToVector3(&Unmanaged[index]);
}
void Vector3Array::default::set(int index, Vector3 value)
{
	if (IsReadOnly)
		throw gcnew InvalidOperationException("List is read-only.");
	if (index < 0 || index >= Count)
		throw gcnew ArgumentOutOfRangeException("index");
	Math::Vector3ToBtVector3(value, &Unmanaged[index]);
}