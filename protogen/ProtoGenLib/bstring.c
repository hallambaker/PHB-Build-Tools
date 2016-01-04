#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib\common.h>


PUBLIC_ENTRY JSON_Object_Init (JSON_Group *Group) {
	BEGIN;
	
	Group->Count = -1;
	Group->First = NULL;
	Group->Members = NULL;
	Group->Last = NULL;

	END;
	}

PUBLIC_ENTRY JSON_Object_Normalize (JSON_Group *Group) {
	BEGIN;
	
	// if Group->Count == -1;
	// allocate array

	// add items into the array

	END;
	}

PUBLIC_ENTRY JSON_Object_Add (JSON_Group *Group, JSON_Object *Object) {
	BEGIN;
	
	if ((Group->Count <= 0) || (Group->First == NULL)) {
		Group->First = Object;
		Group->Count = 1;
		}
	else {
		Group->Last->_Next = Object;
		Group->Count ++;
		}
	Group->Last = Object;

	END;
	}

PUBLIC_ENTRY JSON_Group_Add_StringZ (void *context, JSON_Group *Group, char *StringZ) {
	JSON_Object *Object;
	
	BEGIN;
	
	JSON_Object_Create_StringZ (context, StringZ, &Object);
	JSON_Object_Add (Group, Object);

	END;
	}


PUBLIC_ENTRY JSON_Object_Create (void *context, JSON_Object ** Result) {
	BEGIN;

	JSON_Allocate (context, sizeof (JSON_Object), (void**) Result);
	(*Result)->_Next = NULL;
	
	END;
	}

PUBLIC_ENTRY JSON_Object_Create_Boolean (void *context, boolean Data, JSON_Object ** Result) {
	BEGIN;
	JSON_Object_Create (context, Result);

	(*Result)->_Type = JSON_TYPE_Boolean;
	(*Result) ->As.Boolean = Data;

	END;
	}

PUBLIC_ENTRY JSON_Object_Create_Int64 (void *context, Int64 Data, JSON_Object ** Result) {
	BEGIN;
	JSON_Object_Create (context, Result);

	(*Result)->_Type = JSON_TYPE_Int64;
	(*Result) ->As.Integer = Data;

	END;
	}


PUBLIC_ENTRY JSON_Object_Create_Real64 (void *context, Real64 Data, JSON_Object ** Result) {
	BEGIN;
	JSON_Object_Create (context, Result);

	(*Result)->_Type = JSON_TYPE_Real64;
	(*Result) ->As.Float = Data;

	END;
	}


PUBLIC_ENTRY JSON_Object_Create_DateTime (void *context, Time64 Data, JSON_Object ** Result) {
	BEGIN;
	JSON_Object_Create (context, Result);

	(*Result)->_Type = JSON_TYPE_DateTime;
	// conversion here


	END;
	}




PUBLIC_ENTRY JSON_Object_Create_StringZ (void *context, char * Data, JSON_Object ** Result_Out) {
	JSON_Object * Result;
	
	BEGIN;
	JSON_Object_Create (context, &Result);
	*Result_Out = Result;

	Result->_Type = JSON_TYPE_String;
	JSON_String_By_Zstring (context, Data, &(Result->As.String));

	END;
	}

PUBLIC_ENTRY JSON_Object_Create_StringL (void *context, char * Data, int Length, JSON_Object ** Result_Out) {
	JSON_Object * Result;
	
	BEGIN;
	JSON_Object_Create (context, &Result);
	*Result_Out = Result;

	Result->_Type = JSON_TYPE_String;
	JSON_String_By_Lstring (context, Data, Length, &(Result->As.String));

	END;
	}


// These need fix'n
// They create a wrapper round the object rather than return the object
PUBLIC_ENTRY JSON_Object_Create_String (void *context, JSON_Object ** Result) {
	BEGIN;
	JSON_Object_Create (context, Result);

	(*Result)->_Type = JSON_TYPE_String;
	(*Result) ->As.String.Data = NULL;

	END;
	}




PUBLIC_ENTRY JSON_Object_Create_Binary (void *context, JSON_Object ** Result) {
	BEGIN;
	JSON_Object_Create (context, Result);

	(*Result)->_Type = JSON_TYPE_Binary;
	(*Result) ->As.String.Data = NULL;

	END;
	}


PUBLIC_ENTRY JSON_String_Free (JSON_String * string) {
	BEGIN;

	RETURN_IF (string->Data == NULL);
	RETURN_IF (string->Allocated < 0);

	JSON_Free (string->Data);


	END;
	}


PUBLIC_ENTRY JSON_String_By_JString (void *context, JSON_String *In, JSON_String *Result) {
	Int64 length;
	
	BEGIN;

	JSON_String_Free(Result);
	length = In->Length;

	Result->Length = length;
	Result->Allocated = length;
	JSON_Allocate (context, length+1, (void**) &Result->Data);
	MEMCPY (Result->Data, In->Data, length);
	Result->Data[length] = 0;

	END;
	}

PUBLIC_ENTRY JSON_String_By_2JString (void *context, JSON_String *In1, 
			JSON_String *In2, JSON_String *Result) {
	IntP length;
	
	BEGIN;

	JSON_String_Free(Result);
	length = In1->Length + In2->Length;

	Result->Length = length;
	Result->Allocated = length;
	JSON_Allocate (context, length+1, (void**) &Result->Data);

	MEMCPY (Result->Data, In1->Data, In1->Length);
	MEMCPY (Result->Data+In1->Length, In2->Data, In2->Length);
	Result->Data[length] = 0;

	END;
	}

PUBLIC_ENTRY JSON_String_By_3JString (void *context, JSON_String *In1, 
			JSON_String *In2, JSON_String *In3, JSON_String *Result) {
	IntP length;
	
	BEGIN;

	JSON_String_Free(Result);
	length = In1->Length + In2->Length + In3->Length;

	Result->Length = length;
	Result->Allocated = length;
	JSON_Allocate (context, length+1, (void**) &Result->Data);

	MEMCPY (Result->Data, In1->Data, In1->Length);
	MEMCPY (Result->Data+In1->Length, In2->Data, In2->Length);
	MEMCPY (Result->Data+In1->Length+In2->Length, In3->Data, In3->Length);
	Result->Data[length] = 0;

	END;
	}

PUBLIC_ENTRY JSON_String_By_Padded_JString (void *context, char *In1, 
			JSON_String *In2, char *In3, JSON_String *Result) {
	Int64 length, l1, l3;
	
	BEGIN;

	l1 = strlen (In1);
	l3 = strlen (In3);

	JSON_String_Free(Result);
	length = l1 + In2->Length + l3;

	Result->Length = length;
	Result->Allocated = length;
	JSON_Allocate (context, length+1, (void**) &Result->Data);

	MEMCPY (Result->Data, In1, l1);
	MEMCPY (Result->Data+l1, In2->Data, In2->Length);
	MEMCPY (Result->Data+l1+In2->Length, In3, l3);
	Result->Data[length] = 0;

	END;
	}


PUBLIC_ENTRY JSON_String_By_Zstring (void *context, char *In, JSON_String *Result) {
	int length;
	
	BEGIN;

	JSON_String_Free(Result);
	length = strlen (In);
	Result->Length = length;
	Result->Allocated = length;
	JSON_Allocate (context, length+1, (void**) &Result->Data);
	memcpy (Result->Data, In, length+1);

	END;
	}

PUBLIC_ENTRY JSON_String_By_Lstring (void *context, char *In, int length, JSON_String *Result) {

	BEGIN;

	JSON_String_Free(Result);
	Result->Length = length;
	Result->Allocated = length;
	JSON_Allocate (context, length+1, (void**) &Result->Data);
	memcpy (Result->Data, In, length);
	Result->Data[length] = 0;

	END;
	}

PUBLIC_ENTRY JSON_String_Write (char *tag, JSON_String *Data) {
	int i;
	
	BEGIN;

	printf ("%s", tag);
	if (Data->Data == NULL) {
		printf ("<null>");
		}
	else {
		for (i=0; i<Data->Length; i++) {
			printf ("%c", Data->Data[i]);
			}
		}
	printf ("\n");

	END;
	}


PUBLIC_ENTRY JSON_DateTime_By_Time64 (void *context, Time64 In, JSON_DateTime *Result) {
	BEGIN;

	Result->time = In;

	END;
	}

// Normalize sets the Length and Allocated fields using the contents of Data
//
// Allocated is set to 0 since the string is assumed not writable
// Length is set to the length of the string ignoring the null termination byte

static char * NULL_String = "";

PUBLIC_ENTRY JSON_String_Normalize (JSON_String *Data) {
	BEGIN;

	if (Data->Data != NULL) {
		Data->Length = strlen (Data->Data);
		}
	else {
		Data->Data = NULL_String;
		Data->Length = 0;
		}
	Data->Allocated = 0;

	END;
	}


PUBLIC_ENTRY JSON_Binary_Free (JSON_Binary * data) {
	BEGIN;

	if (data->Data != NULL) {
		JSON_Free (data->Data);
		}

	END;
	}

PUBLIC_ENTRY JSON_Group_Free (JSON_Group * data) {
	BEGIN;

	if (data->Members != NULL) {
		JSON_Free (data->Members);
		}

	END;
	}


PUBLIC_ENTRY JSON_Binary_Set (void *context, char *In, int Length, JSON_Binary *Result) {
	BEGIN;

	JSON_Binary_Free(Result);

	Result->Length = Length;
	JSON_Allocate (context, Length, (void**) &Result->Data);
	memcpy (Result->Data, In, Length);

	END;
	}

PUBLIC_ENTRY JSON_Group_Set_Integer  (void *Context, Int64 *In, int Count, JSON_Group *Result) {
	int i;
	JSON_Object *Object, *Last;
	BEGIN;

	JSON_Group_Free (Result);

	Result ->Count = Count;

	if (Count == 0) return 0;

	JSON_Object_Create_Int64 (Context, In[0], &Object);
	Result->First = Object;
	Last = Object;
	for (i=1; i<Count; i++) {
		JSON_Object_Create_Int64 (Context, In[i], &Object);
		Last->_Next = Object;
		Last = Object;
		}
	Result->Last = Last;
	Last->_Next = NULL;

	END;
	}

PUBLIC_ENTRY JSON_Group_Set_Real64  (void *Context, Real64 *In, int Count, JSON_Group *Result) {
	int i;
	JSON_Object *Object, *Last;
	BEGIN;

	JSON_Group_Free (Result);

	Result ->Count = Count;

	if (Count == 0) return 0;

	JSON_Object_Create_Real64 (Context, In[0], &Object);
	Result->First = Object;
	Last = Object;
	for (i=1; i<Count; i++) {
		JSON_Object_Create_Real64 (Context, In[i], &Object);
		Last->_Next = Object;
		Last = Object;
		}
	Result->Last = Last;
	Last->_Next = NULL;

	END;
	}


PUBLIC_ENTRY JSON_Group_Set_StringZ  (void *Context, char**In, int Count, JSON_Group *Result) {
	int i;
	JSON_Object *Object, *Last;
	BEGIN;

	JSON_Group_Free (Result);

	Result ->Count = Count;

	if (Count == 0) return 0;

	JSON_Object_Create_StringZ (Context, In[0], &Object);
	Result->First = Object;
	Last = Object;
	for (i=1; i<Count; i++) {
		JSON_Object_Create_StringZ (Context, In[i], &Object);
		Last->_Next = Object;
		Last = Object;
		}
	Result->Last = Last;
	Last->_Next = NULL;

	END;
	}
