// This header file is generated automatically
// 
// Changes to this file will be overwritten

// Generated on: (TBS)
// Tool vertion: (TBS)
// Defines:      __HEADER_PROTOGENLIB_DESERIALIZE_H

#ifndef __HEADER_PROTOGENLIB_DESERIALIZE_H
#define __HEADER_PROTOGENLIB_DESERIALIZE_H 1

#include <protogenlib\json_types.h>
PUBLIC_ENTRY JSON_Deserialize (JSON_Context *Context, JSON_Registry *Registry, 
				void *Data) ;
PUBLIC_ENTRY JSON_Deserialize_Object (JSON_Context *Context, void **Data_out, Boolean Multiple) ;
PUBLIC_ENTRY JSON_Deserialize_Parse (JSON_Context *Context, JSON_Registry *Registry, 
				void *Data) ;
PUBLIC_ENTRY JSON_Deserialize_Add (JSON_Context *Context, JSON_Parse *Parse, void *Data) ;
PUBLIC_ENTRY JSON_Deserialize_Create_Add (JSON_Context *Context, JSON_Parse *Parse,  
			void *Data, void ** NewData) ;
PUBLIC_ENTRY JSON_Deserialize_Create (JSON_Parse *Parse, JSON_Registry *Registry, 
			JSON_Context *Context, void *Data, void ** NewData) ;
PUBLIC_ENTRY JSON_Deserialize_Fill (JSON_Context *Context, JSON_Parse *Parse, void *Data) ;
PUBLIC_ENTRY JSON_Deserialize_Lexer (JSON_Stream *Stream, JSON_Minibuffer *minibuffer) ;
PUBLIC_ENTRY JSON_Context_Read_Boolean (JSON_Context *Context, Boolean *Data) ;
PUBLIC_ENTRY JSON_Context_Read_Int64 (JSON_Context *Context, Int64 *Data) ;
PUBLIC_ENTRY JSON_Context_Read_Real64 (JSON_Context *Context, Real64 *Data) ;
PUBLIC_ENTRY JSON_Context_Read_String (JSON_Context *Context, JSON_String *Data) ;
PUBLIC_ENTRY JSON_Context_Read_Binary (JSON_Context *Context, JSON_Binary *Data) ;
PUBLIC_ENTRY JSON_Context_Read_DateTime (JSON_Context *Context, JSON_DateTime *Data) ;

#endif // __HEADER_PROTOGENLIB_DESERIALIZE_H
