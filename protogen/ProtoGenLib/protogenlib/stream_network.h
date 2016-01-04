// This header file is generated automatically
// 
// Changes to this file will be overwritten

// Generated on: (TBS)
// Tool vertion: (TBS)
// Defines:      __HEADER_PROTOGENLIB_STREAM_NETWORK_H

#ifndef __HEADER_PROTOGENLIB_STREAM_NETWORK_H
#define __HEADER_PROTOGENLIB_STREAM_NETWORK_H 1

#include <protogenlib\json_types.h>
PUBLIC_ENTRY JSON_Stream_Network_Create (void *parent, int size, 
					SOCKET Socket, JSON_Stream **Stream_Out) ;
PUBLIC_ENTRY JSON_Stream_Network_Read_Char (struct JSON_Stream *Stream_in, char *out) ;
PUBLIC_ENTRY JSON_Stream_Network_Read (struct JSON_Stream *Stream_in, Int64 space, char *buffer, Int64 *read) ;
PUBLIC_ENTRY JSON_Stream_Network_Write (struct JSON_Stream *Stream_in, Int64 length, char *buffer, Int64 *written) ;

#endif // __HEADER_PROTOGENLIB_STREAM_NETWORK_H
