#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib\common.h>

extern Locale UTC_Locale;

PUBLIC_ENTRY JSON_DateTime_Free (JSON_DateTime * string) {
	BEGIN;

	if (string->Data != NULL) {
		JSON_Free (string->Data);
		}

	END;
	}

// Convert the DateTime Value to a string
PUBLIC_ENTRY JSON_DateTime_Encode (void *context, JSON_DateTime *DateTime) {
	struct tm	TimeStruct;
	int length;
	BEGIN;

	length = 30; // The 64 bit epoch expires in 599,730,285,487CE
		// So longest time string is "599730285487-01-01T00:00:00Z"

	_gmtime64_s (&TimeStruct, &DateTime->time);
	JSON_Allocate (context, length+1, (void**) &DateTime->Data);

	// want "2013-12-13T00:00:01Z"
	_strftime_l (DateTime->Data, length, "%Y-%m-%dT%H:%M:%SZ", &TimeStruct, UTC_Locale);

	// DateTime->Data

	END;
	}


PUBLIC_ENTRY JSON_DateTime_Decode (void *context, char *In, JSON_DateTime *Result) {
	int length;
	
	BEGIN;

	JSON_DateTime_Free(Result);
	length = strlen (In);
	Result->Length = length;
	Result->Allocated = length;
	JSON_Allocate (context, length+1, (void**) &Result->Data);
	memcpy (Result->Data, In, length+1);

	if (length < 30) {
		JSON_DateTime_Map (In, &Result->time);
		}


	END;
	}


static int get_year (char **string_p, int *result) {
	unsigned int acc = 0;
	int	i = 0;
	char *string = *string_p;
	*result = -1;

	if (string == NULL) return -1;


	for (i=0; (i<8) & IS_DIGIT (string [i]); i++){
		acc = acc *10 + (string [i] - '0');
		}
	if ((string[i++] != '-') | (i < 4)) {
		string = NULL;
		}

	*result = acc - 1900;
	*string_p = string + i;

	return string != NULL;
	}

static int get_digits (char **string_p, int *result, int max, int min, char separator) {
	unsigned int acc = 0;
	int	i = 0;
	char *string = *string_p;
	char c;
	*result = -1;

	if (string == NULL) return -1;

	c = string [i++];
	if (!IS_DIGIT (c)) {
		*string_p = NULL;
		return -1;
		}
	acc = (c - '0');

	c = string [i++];
	if (!IS_DIGIT (c)) {
		*string_p = NULL;
		return -1;
		}
	acc = (acc * 10) + (c - '0');

	c = string [i++];
	if (c != separator) {
		*string_p = NULL;
		return -1;
		}

	*result = acc;
	*string_p = string + 3;

	return string != NULL;
	}

// want "2013-12-13T00:00:01Z"
PUBLIC_ENTRY JSON_DateTime_Map (char* String, Time64 *Result) {
	struct tm TimeStruct;

	BEGIN;

	*Result = -1;

	TimeStruct.tm_isdst = 0;
	get_year   (&String, &TimeStruct.tm_year);
	get_digits (&String, &TimeStruct.tm_mon, 12, 1, '-');
	get_digits (&String, &TimeStruct.tm_mday, 31, 1, 'T');
	get_digits (&String, &TimeStruct.tm_hour, 23, 0, ':');
	get_digits (&String, &TimeStruct.tm_min, 59, 0, ':');
	get_digits (&String, &TimeStruct.tm_sec, 60, 0, 'Z');

	if (String != NULL) {
		*Result = _mkgmtime64 (&TimeStruct);
		}
	else {
		*Result = -1;
		}

	END;
	}