// This header file is generated automatically
// 
// Changes to this file will be overwritten

// Generated on: (TBS)
// Tool vertion: (TBS)
// Defines:      __HEADER_PROTOGENLIB_BUFFER_H

#ifndef __HEADER_PROTOGENLIB_BUFFER_H
#define __HEADER_PROTOGENLIB_BUFFER_H 1

#include <protogenlib\json_types.h>
PUBLIC_ENTRY JSON_Buffer_Create (void *Parent, 
	JSON_Buffer **big_buffer_out, Int64 size) ;
PUBLIC_ENTRY JSON_Buffer_Free (JSON_Buffer *big_buffer) ;
PUBLIC_ENTRY JSON_Buffer_Write (JSON_Buffer *big_buffer, char *data, Int64 length) ;
PUBLIC_ENTRY JSON_Buffer_MoveRead (JSON_Buffer *big_buffer, Int64 octets) ;
PUBLIC_ENTRY JSON_Buffer_MoveWrite (JSON_Buffer *big_buffer, Int64 octets) ;
PUBLIC_ENTRY JSON_Chunk_Create (void *Parent, JSON_Chunk **buffer_out, Int64 size) ;
PUBLIC_ENTRY JSON_Chunk_Init (void *Parent, JSON_Chunk *buffer, Int64 size) ;

#endif // __HEADER_PROTOGENLIB_BUFFER_H
