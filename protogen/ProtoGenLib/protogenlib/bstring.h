// This header file is generated automatically
// 
// Changes to this file will be overwritten

// Generated on: (TBS)
// Tool vertion: (TBS)
// Defines:      __HEADER_PROTOGENLIB_BSTRING_H

#ifndef __HEADER_PROTOGENLIB_BSTRING_H
#define __HEADER_PROTOGENLIB_BSTRING_H 1

#include <protogenlib\json_types.h>
PUBLIC_ENTRY JSON_Object_Init (JSON_Group *Group) ;
PUBLIC_ENTRY JSON_Object_Normalize (JSON_Group *Group) ;
PUBLIC_ENTRY JSON_Object_Add (JSON_Group *Group, JSON_Object *Object) ;
PUBLIC_ENTRY JSON_Group_Add_StringZ (void *context, JSON_Group *Group, char *StringZ) ;
PUBLIC_ENTRY JSON_Object_Create (void *context, JSON_Object ** Result) ;
PUBLIC_ENTRY JSON_Object_Create_Boolean (void *context, boolean Data, JSON_Object ** Result) ;
PUBLIC_ENTRY JSON_Object_Create_Int64 (void *context, Int64 Data, JSON_Object ** Result) ;
PUBLIC_ENTRY JSON_Object_Create_Real64 (void *context, Real64 Data, JSON_Object ** Result) ;
PUBLIC_ENTRY JSON_Object_Create_DateTime (void *context, Time64 Data, JSON_Object ** Result) ;
PUBLIC_ENTRY JSON_Object_Create_StringZ (void *context, char * Data, JSON_Object ** Result_Out) ;
PUBLIC_ENTRY JSON_Object_Create_StringL (void *context, char * Data, int Length, JSON_Object ** Result_Out) ;
PUBLIC_ENTRY JSON_Object_Create_String (void *context, JSON_Object ** Result) ;
PUBLIC_ENTRY JSON_Object_Create_Binary (void *context, JSON_Object ** Result) ;
PUBLIC_ENTRY JSON_String_Free (JSON_String * string) ;
PUBLIC_ENTRY JSON_String_By_JString (void *context, JSON_String *In, JSON_String *Result) ;
PUBLIC_ENTRY JSON_String_By_2JString (void *context, JSON_String *In1, 
			JSON_String *In2, JSON_String *Result) ;
PUBLIC_ENTRY JSON_String_By_3JString (void *context, JSON_String *In1, 
			JSON_String *In2, JSON_String *In3, JSON_String *Result) ;
PUBLIC_ENTRY JSON_String_By_Padded_JString (void *context, char *In1, 
			JSON_String *In2, char *In3, JSON_String *Result) ;
PUBLIC_ENTRY JSON_String_By_Zstring (void *context, char *In, JSON_String *Result) ;
PUBLIC_ENTRY JSON_String_By_Lstring (void *context, char *In, int length, JSON_String *Result) ;
PUBLIC_ENTRY JSON_String_Write (char *tag, JSON_String *Data) ;
PUBLIC_ENTRY JSON_DateTime_By_Time64 (void *context, Time64 In, JSON_DateTime *Result) ;
PUBLIC_ENTRY JSON_String_Normalize (JSON_String *Data) ;
PUBLIC_ENTRY JSON_Binary_Free (JSON_Binary * data) ;
PUBLIC_ENTRY JSON_Group_Free (JSON_Group * data) ;
PUBLIC_ENTRY JSON_Binary_Set (void *context, char *In, int Length, JSON_Binary *Result) ;
PUBLIC_ENTRY JSON_Group_Set_Integer  (void *Context, Int64 *In, int Count, JSON_Group *Result) ;
PUBLIC_ENTRY JSON_Group_Set_Real64  (void *Context, Real64 *In, int Count, JSON_Group *Result) ;
PUBLIC_ENTRY JSON_Group_Set_StringZ  (void *Context, char**In, int Count, JSON_Group *Result) ;

#endif // __HEADER_PROTOGENLIB_BSTRING_H
