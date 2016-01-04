#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib\common.h>


PUBLIC_ENTRY JSON_Stream_Socket_Create (void *parent, Int64 size, JSON_Stream **Stream_Out) {
	JSON_Stream *Stream;
	BEGIN;

	if (size==0) {
		size = JSON_SIZE_BUFFER_CHUNK;
		}

	JSON_Allocate (parent, sizeof (JSON_Stream), (void**) &Stream);
	Stream->type = JSON_STREAM_BufferedFile;
	JSON_Buffer_Create (parent, &Stream->Buffer, size+1);
	Stream->read = JSON_Stream_Memory_Read;
	Stream->readc = JSON_Stream_Memory_Read_Char;
	Stream->lookahead = JSON_STREAM_LOOKAHEAD_Null; // not a valid character

	*Stream_Out = Stream;

	END;
	}

// This routine reads a static file in one go
//
// The routine could probably be made faster by memory mapping the file
// But it is only intended to be used for grabbing configuration files
// that will be processed in one go.

PUBLIC_ENTRY JSON_Stream_File_Read (void *parent, char *Name, JSON_Stream **Stream_Out) {
	JSON_Stream *	Stream;
	Int64			Length = 0;
	int				found, count;

	int				file_handle;

	BEGIN;


	*Stream_Out = NULL;

#ifdef WINDOWS
	{
		struct __stat64			Stat;

		found = _stat64 (Name, &Stat);
		if (found <0) {
			char buffer [1024];
			_getcwd (buffer, 1024);
			printf ("IN %s / %s", buffer, Name);
			
			return -1;
			}
		Length = (Int64) Stat.st_size;
		}
#else 
#endif

	JSON_Stream_Socket_Create (parent, Length, &Stream);
	*Stream_Out = Stream;

#ifdef WINDOWS

	found = _sopen_s (&file_handle, Name, _O_RDONLY | _O_BINARY, _SH_DENYWR, _S_IREAD);
	count = read (file_handle, Stream->Buffer->all->buffer, 
				(unsigned int) Stream->Buffer->all->size);
	Stream->Buffer->read_length = count;

#else 
#endif


	END;
	}


