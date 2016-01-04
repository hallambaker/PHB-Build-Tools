// This header file is generated automatically
// 
// Changes to this file will be overwritten

// Generated on: (TBS)
// Tool vertion: (TBS)
// Defines:      __HEADER_PROTOGENLIB_STREAM_MEMORY_H

#ifndef __HEADER_PROTOGENLIB_STREAM_MEMORY_H
#define __HEADER_PROTOGENLIB_STREAM_MEMORY_H 1

#include <protogenlib\json_types.h>
PUBLIC_ENTRY JSON_Stream_Memory_Create (void *parent, int size, JSON_Stream **Stream_Out) ;
PUBLIC_ENTRY JSON_Stream_Memory_Read_Char (struct JSON_Stream *Stream_in, char *out) ;
PUBLIC_ENTRY JSON_Stream_Memory_Read (struct JSON_Stream *Stream_in, Int64 space, char *buffer, Int64 *read) ;
PUBLIC_ENTRY JSON_Stream_Memory_Write (struct JSON_Stream *Stream_in, Int64 length, char *buffer, Int64 *written) ;

#endif // __HEADER_PROTOGENLIB_STREAM_MEMORY_H
