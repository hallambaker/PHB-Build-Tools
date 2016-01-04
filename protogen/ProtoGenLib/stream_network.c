#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib\common.h>

PUBLIC_ENTRY JSON_Stream_Network_Create (void *parent, int size, 
					SOCKET Socket, JSON_Stream **Stream_Out) {
	JSON_Stream *Stream;
	BEGIN;

	if (size==0) {
		size = JSON_SIZE_BUFFER_CHUNK;
		}

	JSON_Allocate (parent, sizeof (JSON_Stream), (void**) &Stream);

	Stream->size = size;
	Stream->type = JSON_STREAM_Network;
	JSON_Buffer_Create (parent, &Stream->Buffer, size);

	Stream->read = JSON_Stream_Network_Read;
	Stream->readc = JSON_Stream_Network_Read_Char;
	Stream->write = JSON_Stream_Network_Write;
	Stream->lookahead = JSON_STREAM_LOOKAHEAD_Null; // not a valid character

	Stream->socket_in = Socket;
	Stream->socket_out = Socket;

	*Stream_Out = Stream;

	END;
	}


PUBLIC_ENTRY JSON_Stream_Network_Read_Char (struct JSON_Stream *Stream_in, char *out) {

	JSON_Stream *Stream = (JSON_Stream*) Stream_in;
	JSON_Buffer *Buffer = Stream->Buffer;
	JSON_Chunk  *Chunk;

	
	BEGIN;

	if (Buffer->read_length <= 0) {
		char *NewData;
		int Received;
		NewData = ALLOCATEN (Stream, char, Stream->size);

		Received = recv (Stream->socket_in, NewData, (int) Stream->size, 0);

		JSON_Buffer_Write (Stream->Buffer, NewData, Received);
		}

	Chunk = Buffer->read;
	*out = Chunk->buffer [Chunk->read_index];
	JSON_Buffer_MoveRead (Buffer, 1);

	END;
	}

PUBLIC_ENTRY JSON_Stream_Network_Read (struct JSON_Stream *Stream_in, Int64 space, char *buffer, Int64 *read) {

	JSON_Stream *Stream = (JSON_Stream*) Stream_in;
	JSON_Buffer *Buffer = Stream->Buffer;
	int i, result = 0;

	BEGIN;
	
	RETURN_IF (space <= 0) ;

	for (i=0; (i<space) & (result >= 0); i++) {
		result = JSON_Stream_Network_Read_Char (Stream_in, buffer+i);

		}
	*read = i;

	//if (Buffer->read_length > 0) {
	//	JSON_Chunk  *Chunk = Buffer->read;

	//	int FromBuffer = Chunk->read_length < space ? Chunk->read_length : space;

	//	memcpy (buffer, Chunk->buffer+Chunk->read_index, FromBuffer);
	//	Chunk->read_index += FromBuffer;
	//	space -=  FromBuffer;
	//	buffer +=  FromBuffer;
	//	*read = FromBuffer;
	//	}
	//else {
	//	*read = 0;
	//	}

	//RETURN_IF (space <= 0) ;
	//Received = recv (Stream->socket_in, buffer, space, 0);
	//*read += Received;

	END;
	}

// This part just outputs data, the buffering is handled internally
PUBLIC_ENTRY JSON_Stream_Network_Write (struct JSON_Stream *Stream_in, Int64 length, char *buffer, Int64 *written) {
	//int i;
	JSON_Stream *Stream = (JSON_Stream*) Stream_in;

	int Sent;
	BEGIN;

	//for (i=0; i<length; i++) {
	//	printf ("%c", buffer[i]);
	//	}

	Sent = send (Stream->socket_out, buffer, (int) length,  0);
	*written = Sent;

	END;
	} 