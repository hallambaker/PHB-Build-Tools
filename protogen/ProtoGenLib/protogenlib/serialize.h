// This header file is generated automatically
// 
// Changes to this file will be overwritten

// Generated on: (TBS)
// Tool vertion: (TBS)
// Defines:      __HEADER_PROTOGENLIB_SERIALIZE_H

#ifndef __HEADER_PROTOGENLIB_SERIALIZE_H
#define __HEADER_PROTOGENLIB_SERIALIZE_H 1

#include <protogenlib\json_types.h>
PUBLIC_ENTRY JSON_Begin (JSON_Context *Context) ;
PUBLIC_ENTRY JSON_End (JSON_Context *Context) ;
PUBLIC_ENTRY 	JSON_Serialize_Tag (JSON_Context *Context, char *tag, boolean *Comma) ;
PUBLIC_ENTRY 	JSON_Serialize_Int64 (JSON_Context *Context, char *tag, Int64 Data,
			boolean Required, char *Default, boolean *Comma) ;
PUBLIC_ENTRY 	JSON_Serialize_Boolean (JSON_Context *Context, char *tag, boolean Data,
			boolean Required, char *Default, boolean *Comma) ;
PUBLIC_ENTRY 	JSON_Serialize_Real64 (JSON_Context *Context, char *tag, double Data,
			boolean Required, char *Default, boolean *Comma) ;
PUBLIC_ENTRY 	JSON_Serialize_Binary (JSON_Context *Context, char *tag, JSON_Binary *Data,
			boolean Required, char *Default, boolean *Comma) ;
PUBLIC_ENTRY 	JSON_Serialize_String (JSON_Context *Context, char *tag, JSON_String *Data,
			boolean Required, char *Default, boolean *Comma) ;
PUBLIC_ENTRY 	JSON_Serialize_DateTime (JSON_Context *Context, char *tag, JSON_DateTime *Data,
			boolean Required, char *Default, boolean *Comma) ;
PUBLIC_ENTRY 	JSON_Serialize_Decimal64 (JSON_Context *Context, char *tag, Decimal64 Data,
			boolean Required, char *Default, boolean *Comma) ;
PUBLIC_ENTRY JSON_Serialize_Item (JSON_Context *Context, JSON_Object *Data) ;
PUBLIC_ENTRY 	JSON_Serialize_Group (JSON_Context *Context, char *tag, JSON_Group *Data,
			boolean Required, char *Default, boolean *Comma) ;

#endif // __HEADER_PROTOGENLIB_SERIALIZE_H
