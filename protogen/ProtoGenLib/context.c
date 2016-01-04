#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib\common.h>
#include <locale.h>

static Boolean Initialized = FALSE;
Locale UTC_Locale;


PUBLIC_ENTRY JSON_Initialize () {
	BEGIN;

	if (Initialized) return 0;
	Initialized = TRUE;

	// The locale UTC is needed to avoid conversion of times to the local locale 
	// Why does this have to be so difficult???
	UTC_Locale = _create_locale (LC_TIME, "UTC");


	END;
	}

PUBLIC_ENTRY JSON_Close () {
	BEGIN;
	END;
	}


PUBLIC_ENTRY JSON_Create_Context (JSON_Stream *Stream, JSON_Context ** Out) {
	return JSON_Create_SubContext (Stream, Stream, Out);
	}

PUBLIC_ENTRY JSON_Create_SubContext (void *Parent, JSON_Stream *Stream, JSON_Context ** Out) {
	JSON_Context *Result;
	JSON_Minibuffer *minibuffer;
	JSON_Parse_Stack *stack;

	BEGIN;

	JSON_Allocate (Parent, sizeof (JSON_Context), (void **) &Result);

	Result->Stream = Stream;
	Result->MaxNested = JSON_SIZE_MAX_NESTED;
	Result->BufferChunk = JSON_SIZE_BUFFER_CHUNK;

	minibuffer = ALLOCATE (Result, JSON_Minibuffer);
	minibuffer->buffer[0] = 0;
	minibuffer->index = 0;

	JSON_Allocate (Result, sizeof (JSON_Parse_Stack) * Result->MaxNested, (void**) &stack);

	Result->Minibuffer = minibuffer;
	Result->Stack = stack;
	//Result->stack_pointer = 0;
	
	*Out = Result;
	END;
	}
