#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib\common.h>

// A lot of these routines could be eliminated by a set of macros that
// simply cast to a socket structure. The connection would then absorb
// the TP_Socket structure rather than having a pointer to one.

// It is poor man's inheritance, but this is C.



PUBLIC_ENTRY JSON_Buffer_Create (void *Parent, 
	JSON_Buffer **big_buffer_out, Int64 size) {
	JSON_Buffer *big_buffer;
	JSON_Chunk *buffer;
	BEGIN;

	big_buffer = ALLOCATE (Parent, JSON_Buffer);
	buffer = ALLOCATE (big_buffer, JSON_Chunk);
	JSON_Chunk_Init(big_buffer, buffer, size);

	big_buffer->size = buffer->size;
	big_buffer->allocate = buffer->size;

	big_buffer->all = buffer;
	big_buffer->read = buffer;
	big_buffer->mark = ALLOCATE (big_buffer, JSON_Chunk);
	big_buffer->write = buffer;

	big_buffer->read_buffer = buffer->buffer;
	big_buffer->read_length = 0;

	big_buffer->write_buffer = buffer->buffer;
	big_buffer->write_length = buffer->size;

	*big_buffer_out = big_buffer;

	END;
	}

PUBLIC_ENTRY JSON_Buffer_Free (JSON_Buffer *big_buffer) {


	BEGIN;

	// This will automatically free all the memory blocks used
	FREE (big_buffer);

	END;
	}

// Add in chunks of data from a source


PUBLIC_ENTRY JSON_Buffer_Write (JSON_Buffer *big_buffer, char *data, Int64 length) {
	BEGIN;

	if (length <= big_buffer->write_length) {
		memcpy (big_buffer->write_buffer, data, (size_t) length);
		big_buffer->write_buffer += length;
		big_buffer->write_length -= length;
		big_buffer->read_length += length;
		}
	else {
		JSON_Chunk *new_buffer;

		memcpy (big_buffer->write_buffer, data, (size_t) big_buffer->write_length);
		length -= big_buffer->write_length;
		big_buffer->read_length += big_buffer->write_length;

		new_buffer = ALLOCATE (big_buffer, JSON_Chunk);
		JSON_Chunk_Init(big_buffer, new_buffer, big_buffer->allocate);
		big_buffer->write->next = new_buffer;

		big_buffer->write = new_buffer;

		memcpy (new_buffer->buffer, data+big_buffer->write_length, (size_t) length) ;

		big_buffer->write_buffer = new_buffer->buffer+length;
		big_buffer->write_length = new_buffer->size - length;
		}


	END;
	}


// The code accesses the buffers directly, the library just manages the
// pointers

// All data that has been read can be desroyed. If there was a need to keep the
// data then another mechanism 'peek' would be needed.
PUBLIC_ENTRY JSON_Buffer_MoveRead (JSON_Buffer *big_buffer, Int64 octets) {
	JSON_Chunk *buffer;
	BEGIN;

	if (octets > big_buffer->read_length) {
		return -1;
		}

	ASSERT (octets <= big_buffer->read_length, -1, "read error y");

	buffer = big_buffer->read;
	big_buffer->read_length -= octets;

	if (big_buffer->read_length <= 0) {
		ASSERT (big_buffer->read == big_buffer->write, -1, "internal buffer");

		// Reset the big buffer to the beginning

		buffer->index = 0;
		big_buffer->read_buffer = buffer->buffer;
		big_buffer->read_length = 0;
		big_buffer->write_buffer = buffer->buffer;
		big_buffer->write_length = buffer->size;
		}

	else {
		buffer->read_index += octets;
		if (buffer->read_index == buffer->size) {
			big_buffer->read = buffer->next;
			big_buffer->read_buffer = buffer->next->buffer;
			big_buffer->read_length = 0;

			// Don't free up the buffer space at this point. 
			// Should redo this so that the buffer space is freed automatically
			// when the mark pointer advances.
			//FREE (buffer);		// all done, can free the memory
			}
		}

	END;
	}

PUBLIC_ENTRY JSON_Buffer_MoveWrite (JSON_Buffer *big_buffer, Int64 octets) {
	JSON_Chunk *buffer, *new_buffer;
	BEGIN;

	ASSERT (octets <= big_buffer->write_length, -1, "write error");

	buffer = big_buffer->write;
	big_buffer->write_length -= octets;
	buffer->index += octets;

	if (big_buffer->write_length <= 0) {
		new_buffer = ALLOCATE (big_buffer, JSON_Chunk);
		JSON_Chunk_Init(big_buffer, new_buffer, big_buffer->allocate);
		buffer->next = new_buffer;

		big_buffer->write_buffer = new_buffer->buffer;
		big_buffer->write_length = new_buffer->size;
		big_buffer->write = new_buffer;
		}

	END;
	}





PUBLIC_ENTRY JSON_Chunk_Create (void *Parent, JSON_Chunk **buffer_out, Int64 size) {
	JSON_Chunk *buffer;
	BEGIN;

	buffer = ALLOCATE (Parent, JSON_Chunk);
	JSON_Chunk_Init(Parent, buffer, size);

	*buffer_out = buffer;

	END;
	}


PUBLIC_ENTRY JSON_Chunk_Init (void *Parent, JSON_Chunk *buffer, Int64 size) {
	BEGIN;
	
	buffer->buffer = (size > 0) ? (ALLOCATEN (Parent, char, size+1)) : NULL;
	buffer->size = size;
	buffer->index = 0;
	buffer->read_index = 0;

	END;
	}







