#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib\common.h>
#include <math.h>

// This implementation is sub optimal
//
// Would be better to read the data into a local buffer to avoid the function call
// per character.

PUBLIC_ENTRY JSON_Stream_Get_Block (JSON_Stream *Stream, Int64 Length, char *Data, Int64 *Read) {
	struct JSON_Stream *Stream_s = (struct JSON_Stream *) Stream;
	BEGIN;

	RETURN_IF (Length == 0);

	THROW_IF (Stream->lookahead == JSON_STREAM_LOOKAHEAD_EOF, -1);

	if (Stream->lookahead == JSON_STREAM_LOOKAHEAD_Char) {
		*Data++ = Stream->last;
		Length --;
		Stream->lookahead = FALSE;
		}

	_Status = Stream->read (Stream_s, Length, Data, Read);

	CATCH;
	END;
	}


PUBLIC_ENTRY JSON_Stream_Get_Char (JSON_Stream *Stream, char *Data) {
	struct JSON_Stream *Stream_s = (struct JSON_Stream *) Stream;

	if (Stream->lookahead == JSON_STREAM_LOOKAHEAD_Char) {
		*Data = Stream->last;
		Stream->lookahead = FALSE;
		return 0;
		}
	else if (Stream->lookahead == JSON_STREAM_LOOKAHEAD_EOF) {
		return -1;
		}
	else {
		int status;
		status = Stream->readc (Stream_s, &Stream->last);
		*Data = Stream->last;
		return status;
		}
	}

PUBLIC_ENTRY JSON_Stream_Peek_Char (JSON_Stream *Stream, char *Data) {
	struct JSON_Stream *Stream_s = (struct JSON_Stream *) Stream;


	if (Stream->lookahead == JSON_STREAM_LOOKAHEAD_Char) {
		*Data = Stream->last;
		return 0;
		}
	else if (Stream->lookahead == JSON_STREAM_LOOKAHEAD_EOF) {
		return -1;
		}
	else {
		int status = Stream->readc (Stream_s, &Stream->last);
		if (status < 0 ) {
			Stream->lookahead = JSON_STREAM_LOOKAHEAD_EOF;
			return -1;
			}
		Stream->lookahead = JSON_STREAM_LOOKAHEAD_Char;
		*Data = Stream->last;
		return 0;
		}
	}


PUBLIC_ENTRY JSON_Stream_Unget_Char (JSON_Stream *Stream) {
	struct JSON_Stream *Stream_s = (struct JSON_Stream *) Stream;
	BEGIN;

	if (Stream->lookahead) {
		return -1;		// can only push back one character
		}
	else {
		Stream->lookahead = TRUE;
		return 0;
		}
	}


PUBLIC_ENTRY JSON_Stream_Mark (JSON_Stream *Stream) {
	JSON_Buffer *Buffer = Stream->Buffer;
	BEGIN;

	*(Buffer->mark) = *(Buffer->read);
	Buffer->mark->read_length = Buffer->read_length;

	END;
	}

PUBLIC_ENTRY JSON_Stream_Rewind (JSON_Stream *Stream) {
	JSON_Buffer *Buffer = Stream->Buffer;
	BEGIN;

	*(Buffer->read) = *(Buffer->mark);
	Buffer->read_length = Buffer->mark->read_length;
	Stream->lookahead = JSON_STREAM_LOOKAHEAD_Null;

	END;
	}


PUBLIC_ENTRY JSON_Stream_Write (JSON_Stream *Stream, Int64 length, char *buffer, Int64 *written) {
	struct JSON_Stream *Stream_s = (struct JSON_Stream *) Stream;


	return Stream->write (Stream_s, length, buffer, written);

	}


PUBLIC_ENTRY JSON_Stream_Write_Char (JSON_Stream *Stream, char Data) {
	struct JSON_Stream *Stream_s = (struct JSON_Stream *) Stream;
	Int64		written;

	return Stream->write (Stream_s, 1, &Data, &written);

	}


PUBLIC_ENTRY JSON_Stream_Write_CharZ (JSON_Stream *Stream, char *Data) {
	struct JSON_Stream *Stream_s = (struct JSON_Stream *) Stream;
	Int64		written;
	Int64		length;

	length = strlen (Data);
	return Stream->write (Stream_s, length, Data, &written);

	}




PUBLIC_ENTRY JSON_Stream_Write_String (JSON_Stream *Stream, JSON_String *Data) {
	struct JSON_Stream *Stream_s = (struct JSON_Stream *) Stream;
	Int64		written;
	BEGIN;

	Stream->write (Stream_s, Data->Length, Data->Data, &written);

	END;
	}

// Write out UTF8 string to stream applying JSON escaping rules
PUBLIC_ENTRY JSON_Stream_Write_JString (JSON_Stream *Stream, JSON_String *Data) {
	struct JSON_Stream *Stream_s = (struct JSON_Stream *) Stream;
	Int64		written;
	BEGIN;

	JSON_Stream_Write_Char (Stream, '\"');
	Stream->write (Stream_s, Data->Length, Data->Data, &written);
	JSON_Stream_Write_Char (Stream, '\"');

	END;
	}

// Only the above function are allowed to call the Stream->write method directly.
// All the other functions are channelled through one of the above


// Write out an integer value (Int64)
//
// The value Int64_NaN (LONG_MIN) is used to represent NaN.


PUBLIC_ENTRY JSON_Stream_Write_Integer (JSON_Stream *Stream, Int64 Data) {

	UInt64		accumulator, a2;
	UInt64		position;
	BEGIN;

	if (Data == Int64_NaN) {
		JSON_Stream_Write_CharZ (Stream, "null");
		}
	else if (Data < 0) {
		JSON_Stream_Write_Char (Stream, '-');
		accumulator = (UInt64) -Data;
		}
	else {
		accumulator = (UInt64) Data;
		}

	if (accumulator == 0) {
		JSON_Stream_Write_Char (Stream, '0');
		}

	a2 = accumulator/10; position = 1;
	while (a2 > 0) {
		position = position *10;
		a2 = a2/10;
		}
	

	while (position > 1) {
		UInt64 digit;

		digit = (accumulator /position);
		JSON_Stream_Write_Char (Stream, TO_DIGIT (digit) );
		accumulator = accumulator - (digit * position);
		position = position /10;
		}
	
	JSON_Stream_Write_Char (Stream, TO_DIGIT (accumulator) );
	
	END;
	}

#define MAX_FLOAT_CHARS 24
// Yet another irritation of C, the standard libraries for fcvt are broken
// So Windows and Unix fixed it in different ways

// By default we print out to 18 digits. But note that this may still 
// introduce rounding errors on round tripping

PUBLIC_ENTRY JSON_Stream_Write_Real64 (JSON_Stream *Stream, Real64 Data) {


	char		buffer [MAX_FLOAT_CHARS+8];
	BEGIN;

#ifdef _WIN32
	_gcvt_s (buffer, MAX_FLOAT_CHARS, Data, 18);

#else
	snprintf (buffer, MAX_FLOAT_CHARS, "%e23.18", Data);
#endif
	
	JSON_Stream_Write_CharZ (Stream, buffer);
	
	END;
	}


PUBLIC_ENTRY JSON_Stream_Write_Null (JSON_Stream *Stream) {
	BEGIN;

	JSON_Stream_Write_CharZ (Stream, "null");
	
	END;
	}


PUBLIC_ENTRY JSON_Stream_Write_Boolean (JSON_Stream *Stream, Boolean Data) {
	BEGIN;

	if (Data == FALSE) {
		JSON_Stream_Write_CharZ (Stream, "false");
		}
	else if (Data == Boolean_NaN) {
		JSON_Stream_Write_CharZ (Stream, "null");
		}
	else {
		JSON_Stream_Write_CharZ (Stream, "true");
		}

	END;
	}

PUBLIC_ENTRY JSON_Stream_Write_DateTime (JSON_Stream *Stream, JSON_DateTime *DateTime) {
	BEGIN;

	//JSON_DateTime_Encode (DateTime);
	JSON_Stream_Write_Char (Stream, '\"');
	JSON_Stream_Write_CharZ (Stream, DateTime->Data);
	JSON_Stream_Write_Char (Stream, '\"');

	END;
	}