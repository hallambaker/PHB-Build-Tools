// This header file is generated automatically
// 
// Changes to this file will be overwritten

// Generated on: (TBS)
// Tool vertion: (TBS)
// Defines:      __HEADER_PROTOGENLIB_CONTEXT_H

#ifndef __HEADER_PROTOGENLIB_CONTEXT_H
#define __HEADER_PROTOGENLIB_CONTEXT_H 1

#include <protogenlib\json_types.h>
PUBLIC_ENTRY JSON_Initialize () ;
PUBLIC_ENTRY JSON_Close () ;
PUBLIC_ENTRY JSON_Create_Context (JSON_Stream *Stream, JSON_Context ** Out) ;
PUBLIC_ENTRY JSON_Create_SubContext (void *Parent, JSON_Stream *Stream, JSON_Context ** Out) ;

#endif // __HEADER_PROTOGENLIB_CONTEXT_H
