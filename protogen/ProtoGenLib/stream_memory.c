#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib\common.h>

PUBLIC_ENTRY JSON_Stream_Memory_Create (void *parent, int size, JSON_Stream **Stream_Out) {
	JSON_Stream *Stream;
	BEGIN;

	if (size==0) {
		size = JSON_SIZE_BUFFER_CHUNK;
		}

	JSON_Allocate (parent, sizeof (JSON_Stream), (void**) &Stream);


	Stream->type = JSON_STREAM_Memory;
	JSON_Buffer_Create (parent, &Stream->Buffer, size);
	Stream->read = JSON_Stream_Memory_Read;
	Stream->readc = JSON_Stream_Memory_Read_Char;
	Stream->write = JSON_Stream_Memory_Write;
	Stream->lookahead = JSON_STREAM_LOOKAHEAD_Null; // not a valid character

	*Stream_Out = Stream;

	END;
	}


PUBLIC_ENTRY JSON_Stream_Memory_Read_Char (struct JSON_Stream *Stream_in, char *out) {

	JSON_Stream *Stream = (JSON_Stream*) Stream_in;
	JSON_Buffer *Buffer = Stream->Buffer;
	JSON_Chunk  *Chunk = Buffer->read;
	BEGIN;

	if (Buffer->read_length <= 0) {
		return -1;
		}

	*out = Chunk->buffer [Chunk->read_index];
	JSON_Buffer_MoveRead (Buffer, 1);

	END;
	}

PUBLIC_ENTRY JSON_Stream_Memory_Read (struct JSON_Stream *Stream_in, Int64 space, char *buffer, Int64 *read) {

	JSON_Stream *Stream = (JSON_Stream*) Stream_in;
	BEGIN;



	END;
	}


PUBLIC_ENTRY JSON_Stream_Memory_Write (struct JSON_Stream *Stream_in, Int64 length, char *buffer, Int64 *written) {
	int i;
	JSON_Stream *Stream = (JSON_Stream*) Stream_in;
	BEGIN;

	for (i=0; i<length; i++) {
		printf ("%c", buffer[i]);
		}

	JSON_Buffer_Write (Stream->Buffer, buffer, length);

	END;
	} 