// This header file is generated automatically
// 
// Changes to this file will be overwritten

// Generated on: (TBS)
// Tool vertion: (TBS)
// Defines:      __HEADER_PROTOGENLIB_BASE64_H

#ifndef __HEADER_PROTOGENLIB_BASE64_H
#define __HEADER_PROTOGENLIB_BASE64_H 1

#include <protogenlib\json_types.h>
PUBLIC_ENTRY base64_encode(char *data_in, int length_in, char*data_out, int *length_out) ;
PUBLIC_ENTRY base64uri_encode(char *data_in, int length_in, char*data_out, int *length_out) ;
PUBLIC_ENTRY JSON_Stream_Write_Binary (JSON_Stream *Stream, JSON_Binary *Data) ;
PUBLIC_ENTRY JSON_Stream_Read_Binary (JSON_Stream *Stream, JSON_Binary *Data) ;

#endif // __HEADER_PROTOGENLIB_BASE64_H
