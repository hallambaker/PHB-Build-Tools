// This header file is generated automatically
// 
// Changes to this file will be overwritten

// Generated on: (TBS)
// Tool vertion: (TBS)
// Defines:      __HEADER_PROTOGENLIB_STREAM_H

#ifndef __HEADER_PROTOGENLIB_STREAM_H
#define __HEADER_PROTOGENLIB_STREAM_H 1

#include <protogenlib\json_types.h>
PUBLIC_ENTRY JSON_Stream_Get_Block (JSON_Stream *Stream, Int64 Length, char *Data, Int64 *Read) ;
PUBLIC_ENTRY JSON_Stream_Get_Char (JSON_Stream *Stream, char *Data) ;
PUBLIC_ENTRY JSON_Stream_Peek_Char (JSON_Stream *Stream, char *Data) ;
PUBLIC_ENTRY JSON_Stream_Unget_Char (JSON_Stream *Stream) ;
PUBLIC_ENTRY JSON_Stream_Mark (JSON_Stream *Stream) ;
PUBLIC_ENTRY JSON_Stream_Rewind (JSON_Stream *Stream) ;
PUBLIC_ENTRY JSON_Stream_Write (JSON_Stream *Stream, Int64 length, char *buffer, Int64 *written) ;
PUBLIC_ENTRY JSON_Stream_Write_Char (JSON_Stream *Stream, char Data) ;
PUBLIC_ENTRY JSON_Stream_Write_CharZ (JSON_Stream *Stream, char *Data) ;
PUBLIC_ENTRY JSON_Stream_Write_String (JSON_Stream *Stream, JSON_String *Data) ;
PUBLIC_ENTRY JSON_Stream_Write_JString (JSON_Stream *Stream, JSON_String *Data) ;
PUBLIC_ENTRY JSON_Stream_Write_Integer (JSON_Stream *Stream, Int64 Data) ;
PUBLIC_ENTRY JSON_Stream_Write_Real64 (JSON_Stream *Stream, Real64 Data) ;
PUBLIC_ENTRY JSON_Stream_Write_Null (JSON_Stream *Stream) ;
PUBLIC_ENTRY JSON_Stream_Write_Boolean (JSON_Stream *Stream, Boolean Data) ;
PUBLIC_ENTRY JSON_Stream_Write_DateTime (JSON_Stream *Stream, JSON_DateTime *DateTime) ;

#endif // __HEADER_PROTOGENLIB_STREAM_H
