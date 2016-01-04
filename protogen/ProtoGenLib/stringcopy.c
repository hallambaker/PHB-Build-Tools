#include <protogenlib/json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib/common.h>

PUBLIC_ENTRY JSON_Create (void *parent, long type, 
			JSON_Registry *Registry, void **Result_Out) {
	JSON_Object *Result;
	
	BEGIN;
	*Result_Out = NULL;

	JSON_Allocate (parent, Registry[type].Size, (void**) &Result);

	Result->_Next = NULL;
	Result->_Type = type;

	*Result_Out = Result;

	END;
	}


//PUBLIC_ENTRY JSON_Extend (void *Data, Int64 size, void **Result_Out) {
//	void *Result;
//
//	BEGIN;
//
//	Result = realloc (Data, (int) size);
//	*Result_Out = Result;
//
//	END;
//	}
//
//
PUBLIC_ENTRY JSON_Allocate (void *parent, Int64 size, void **Result_Out) {
	//void *Result;

	BEGIN;

	//Result = malloc ((int) size);
	//MEMSET (Result, 0, size);
	//*Result_Out = Result;

	Z_Allocate (parent, (IntT) size, Result_Out);

	END;
	}

void *JSON_Allocate_PTR  (void *parent, size_t size) {

	void *Result;
	//JSON_Allocate (parent, (Int64) size, & Result);
	Z_Allocate (parent, size, &Result);

	return Result;
	}

void *JSON_Extend_PTR  (void *Data, size_t size) {

	void *Result;
	//JSON_Extend (Data, size, & Result);
	Z_Reallocate (Data, size, &Result);

	return Result;
	}

PUBLIC_ENTRY JSON_Free (void *item) {
	BEGIN;

	//Z_Free (item);

	END;
	}

//PUBLIC_ENTRY JSON_Copy_String (void *parent, char*string, char**string_out) {
//	char	*string_ptr;
//	int		length;
//	
//	BEGIN;
//
//	*string_out = NULL;
//
//	length = strlen (string);
//	string_ptr = ALLOCATEN (parent, char, length+1);
//	strcpy (string_ptr, string);
//
//	*string_out = string_ptr;
//
//	END;
//	}
//
//// Create a null terminated copy of a string fragment
////
//// All the bytes in the specified range are copied regardless of whether
//// a null byte occurs.
//
//PUBLIC_ENTRY JSON_Copy_String_Length (void *parent, char*string, int length, char**string_out) {
//	char	*string_ptr;
//	
//	BEGIN;
//
//	*string_out = NULL;
//
//	string_ptr = ALLOCATEN (parent, char, length+1);
//	memcpy (string_ptr, string, length);
//	string_ptr[length] = 0;
//
//	*string_out = string_ptr;
//
//	END;
//	}