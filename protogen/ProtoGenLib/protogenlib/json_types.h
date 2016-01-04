#pragma once


#ifndef __HEADER_JSON_TYPES_H
#define __HEADER_JSON_TYPES_H true

// Common definitions to make C programming habbitable
#include <protogenlib\master.h>



typedef struct _Z_Header {
	struct _Z_Header		*Parent;
	struct _Z_Header		*Prev, *Next;
	struct _Z_Header		*First, *Last;
	IntT					Size;
	IntT					Count;
	IntT					Canary;
	} Z_Header;



typedef struct _JSON_Group {
	long					Count;
	struct _JSON_Object		*First;
	struct _JSON_Object		*Last;
	struct _JSON_Object		**Members;
	} JSON_Group;

#define JSON_SIZE_BUFFER_CHUNK		65536
#define JSON_SIZE_MINIBUFFER		256
#define JSON_SIZE_MAX_NESTED		256




#define JSON_STREAM_Memory			0
#define JSON_STREAM_File			1
#define JSON_STREAM_Network			2
#define JSON_STREAM_BufferedFile	3

#define JSON_ENCODING_A			0
#define JSON_ENCODING_B			1
#define JSON_ENCODING_C			2
#define JSON_ENCODING_D			3

#define JSON_STREAM_LOOKAHEAD_Null		0
#define JSON_STREAM_LOOKAHEAD_Char		1
#define JSON_STREAM_LOOKAHEAD_EOF		2




#define JSON_ForEach(v,g) for (v = (g)->First; v != NULL; v = v->_Next) 
#define JSON_String_Zero(s) {(s)->Length = 0; (s)->Allocated = 0; (s)-> Data= NULL;}
#define JSON_String_Set(s,v) {(s)->Length = strlen(v); (s)->Allocated = 0; (s)-> Data= (v);}

// The CNF_String structure is a length delimited string

typedef struct _JSON_String {
	Int64					Length;
	Int64					Allocated;
	char					*Data;
	} JSON_String;

typedef struct _JSON_DateTime {
	Int64					Length;
	Int64					Allocated;
	char					*Data;
	Time64					time;
	} JSON_DateTime;

typedef struct _JSON_Format {
	struct _JSON_Object				*_Next;
	int								_Type;
	struct _JSON_String				_String;
	} JSON_Format;


// The CNF_Binary structure is a binary data blob

typedef struct _JSON_Binary {
	Int64					Length;
	Int64					Allocated;
	char					*Data;
	} JSON_Binary;

typedef struct _JSON_Parse_Stack {
	int							State;
	struct _JSON_Registry		*Registry;
	void						*Data;
	int							Slot;
	} JSON_Parse_Stack;


typedef struct _JSON_Minibuffer {
	int						Token;
	Int64					index;
	int						count_b64;
	int						count_utf8;
	Boolean					overflow;
	char					buffer[JSON_SIZE_MINIBUFFER+1];
	} JSON_Minibuffer;


// While the minibuffer and the stack could be consolidated into 
// the context, this approach is more robust and allows us to
// extend either one if we choose by simply reallocating it

typedef struct _JSON_Context {
	struct _JSON_Stream			*Stream;
	struct _JSON_Registry		*Registry;
	int							Count;
	int							MaxNested;
	int							BufferChunk;
	int							Last_Token;
	struct _JSON_Minibuffer		*Minibuffer;
	struct _JSON_Parse_Stack	*Stack;
	} JSON_Context;

typedef struct _JSON_Stream {
	int						type;
	int						encoding;
	struct _JSON_Buffer		*Buffer;
	SOCKET					socket_in;
	SOCKET					socket_out;
	int						(*read) (struct JSON_Stream*, Int64, char*, Int64*);
	int						(*readc) (struct JSON_Stream*, char*);
	int						(*write) (struct JSON_Stream*, Int64, char*, Int64*);
	int						lookahead;
	char					last;
	Int64						size;
	} JSON_Stream;

typedef int	(*JSON_Serializer) (struct _JSON_Context*, void *);
typedef int	(*JSON_Initializer) (struct _JSON_Context*, void **);
typedef int	(*JSON_FormatSerializer) ( void *, void **);
typedef int	(*JSON_FormatDeserializer) ( void *, void **);


typedef struct _JSON_Parse {
	char					*Tag;
	int						Tag_Length;
	int						Offset;
	int						Type;
	boolean					Required;
	char					*Default;
	JSON_FormatSerializer	Serialize;
	JSON_FormatDeserializer	Deserialize;
	} JSON_Parse;

typedef struct _JSON_Registry {
	char					*Tag;
	int						Tag_Length;
	JSON_Parse				*Table;
	int						Count;
	size_t					Size;
	JSON_Serializer			Serialize;
	JSON_Initializer		Deserialize;
	JSON_Initializer		Create;
	} JSON_Registry;

typedef struct _JSON_Object {
	struct _JSON_Object				*_Next;
	int								_Type;
	union {
		boolean						Boolean;
		Int64						Integer;
		Real64						Float;
		struct _JSON_String			String;
		struct _JSON_Binary			Binary;
		struct _JSON_DateTime		DateTime;
		struct _JSON_Format			Format;
		}							As;
	} JSON_Object;

#define JSON_OBJECT_Header 	\
	struct _JSON_Object				*_Next; \
	int								_Type;


// Buffer structure
typedef struct _JSON_Chunk {
	struct _JSON_Chunk			*next;
	char						*buffer;
	Int64							size;
	Int64							index;
	Int64							read_index;
	Int64							read_length;
	} JSON_Chunk;


typedef struct _JSON_Buffer {
	struct _JSON_Chunk			*all;
	struct _JSON_Chunk			*read;
	struct _JSON_Chunk			*mark;
	struct _JSON_Chunk			*write;
	Int64							size;
	Int64							allocate;

	char						*read_buffer;
	Int64							read_length;
	char						*write_buffer;
	Int64							write_length;

	} JSON_Buffer;



#define JSON_TYPE_String  0
#define JSON_TYPE_Binary  1
#define JSON_TYPE_Int64  2
#define JSON_TYPE_Decimal64  3
#define JSON_TYPE_Real64  4
#define JSON_TYPE_Boolean  5
#define JSON_TYPE_DateTime  6
#define JSON_TYPE_URI  7
#define JSON_TYPE_Format  8

#define JSON_TYPE_Intrinsics  9




#ifndef END_PUBLIC_HEADERS
#define END_PUBLIC_HEADERS
#endif


#endif //__HEADER_JSON_TYPES_H