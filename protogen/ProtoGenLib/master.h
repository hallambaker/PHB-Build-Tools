#pragma once

#ifndef __HEADER_MASTER_PHB
#define __HEADER_MASTER_PHB true


#ifdef _WIN32
#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <iphlpapi.h>
#include <math.h>
#include <io.h>
#include <share.h>
#include <fcntl.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <direct.h>
#include <time.h>

#define WINDOWS TRUE

typedef int			boolean;
#define false		0
#define true		1

// fuckwitology here. 
// first edition is that you can't just test for NaN with equality
// second is that Microsoft don't ever use the standard macro names
#define isnan(x) _isnan(x)

static const unsigned long _Real64_NaN[2] = {0xffffffff, 0x7fffffff};
#define Real64_NaN (*(const float *) _Real64_NaN)

typedef __time64_t Time64;
typedef _locale_t Locale;

// Real64_INFINITY (DBL_MAX+DBL_MAX)
// Real64_NaN (INFINITY-INFINITY)



#define Directory_Separator '\\';


// Pragmas to disable idiot warning that would force me to use proprietary calls
#pragma warning(disable:4996)
#pragma warning(disable:4102)

#else 

#define UNIX TRUE

// UNIX specific code here
typedef int			boolean;
typedef unsigned long Time;
#include <sys/types.h>
#include <stdbool.h>
#define Directory_Separator '/';

#endif

#include <stdio.h>
#include <stdlib.h>
#include <string.h>


#define CRLF ("\r\n")
#if !defined(NULL)
    #define NULL ((void*)0)
#endif
#if !defined(null)
    #define null ((void*)0)
#endif

#ifdef OLD_MEMORY_ALLOC
#define ALLOCATE(p,t) (t*) malloc (sizeof (t))
#define ALLOCATEN(p,t, n) (t*) malloc (n*sizeof (t))
#define EXTEND(p, t, n) (t*) realloc (p, n*sizeof (t))
#define FREE(v) free(v)
#else

void *JSON_Allocate_PTR  (void *parent, size_t size);
void *JSON_Extend_PTR  (void *Data, size_t size);


#define ALLOCATE(p,t) (t*) JSON_Allocate_PTR (p, sizeof (t))
#define ALLOCATEN(p,t, n) (t*) JSON_Allocate_PTR (p,  (size_t) n*sizeof (t))
#define EXTEND(p, t, n) (t*) JSON_Extend_PTR (p, (size_t) n*sizeof (t))
#define FREE(v) JSON_Free(v)
#endif

#define MEMCPY(s,d,l) memcpy(s,d,(size_t) (l))
#define MEMSET(s,c,l) memset(s,c,(size_t) (l))
#define MEMCMP(s,c,l) memcmp(s,c,(size_t) (l))
#define MEMMOVE(s,c,l) memcmp(s,c,(size_t) (l))
#define MEMZERO(s,l) memset(s,0,(size_t) (l))

#define COUNT(x) (sizeof (x) / sizeof (x[0]))

#define FALSE 0
#define TRUE 1

#define JStringSet(s,v) {s.Length = strlen(v); s.Allocated = -1; s.Data = v;}

#define CharacterMapping(c,m) ( ((Byte) c) > sizeof (m) ? m[0] : m [(Byte) (c)])



// Every method is a function that returns an integer status code.
//
//		A zero or positive return indicates success
//		Negative return values indicate failure.

// Methods are defined as follows

//	STATUS method_name (parameter list) {
//         // Variable declarations here
//		BEGIN;
//		   // Code goes here
//		   THROW (x) // raise an exception
//      CATCH;
//         // Common x handling code goes here
//      HANDLE (error_code);
//         // Specific exception handling code for error (x)
//      END;
//      }

// The CATCH block is optional. Including a CATCH block without any 
// ASSERT or CHECK_STATUS macros may cause a compiler warning for the
// unused label Error


//#ifndef _cplusplus
//
//#define bool int
//#define true 1;
//#define false 0;
//
//#endif


#define END_PUBLIC_HEADERS
#define PUBLIC_ENTRY		int
#define PRIVATE_ENTRY		int

#define STATUS		int 
#define BEGIN		int _Status = 0;

#define CATCH		return _Status; Error:
#define END			return _Status;
#define RETURN		return _Status ;

#define RETURN_IF(t)				{if (t) END}

#define THROW_IF_ZERO(t,e)			{if ((t)==0)   {THROW(e)}}
#define THROW_IF_NOT_ZERO(t,e)		{if ((t)!=0)   {THROW(e)}}
#define THROW_IF_NULL(t,e)			{if ((t)==NULL)  { THROW(e)}}
#define THROW_IF_NOT_NULL(t,e)		{if ((t)!=NULL)  { THROW(e)}}
#define THROW_IF(t,e)				{if (t)   {THROW(e)}}
#define THROW_IF_TRUE(t,e)			{if (t)   {THROW(e)}}
#define THROW_IF_FALSE(t,e)			{if (!(t))  { THROW(e)}}


#define ONCE(t) {if (t) {return _Status;} else {t = FALSE;}}

#ifdef GOEDEL_ERROR_DEFINITIONS

Here will go the code to hook into the error codes generated using Goedel

#else
#define THROW(x)	{_Status = -1; goto Error;}
#endif 


#define CLEANUP		goto Clean;
#define CLEAN		Clean:;

#define HANDLE(x)   NYI

#define REPORT_ERROR(x,t) {printf ("%s %d", t, x); return x;}
#define ASSERT(p,x,t) if(!(p)) {REPORT_ERROR (x,t);}
#define ASSERT_CLEANUP(p,x,t,f) if(!(p)) {f; REPORT_ERROR (x,t);}

#define CHECK_ZERO_RETURN(f,x) {_Status = (f); ASSERT(_Status==0, _Status, x);}
#define CHECK_ZERO_RETURN_CLEANUP(f,x,r) {_Status = (f); ASSERT_CLEANUP(_Status==0, _Status, x, r);}

#define CHECK_RETURN(f,x) CHECK_ZERO_RETURN(f,x)
#define CHECK_RETURN_CLEANUP(f,x,r) CHECK_ZERO_RETURN_CLEANUP(f,x,r)

#define CHECK_STATUS(f) ASSERT(f>=0, -1, "Error TBS")


#define CHECK_NOT_NULL(x, e) {if (x == null) {THROW (e);}} 
#define CHECK_TRUE(x, e) {if (!x) {THROW (e);}} 

//#define NYI(x) DEBUG_PRINT((x))
#define NYI(x) 

// define the default buffer size to be large enough to fit a full jumbo IP packet.
#define DEFAULT_BUFFER 65536
#define MAX_COMMAND_LENGTH 512
#define MAX_RESPONSE_LENGTH 512

#define IS_DIGIT(x) (((x)>='0') & ((x)<='9'))
#define IS_UPPER(x) ((x>='A') & (x<='Z'))
#define IS_LOWER(x) ((x>='a') & (x<='z'))
#define IS_LETTER(x) (IS_UPPER(x) | IS_LOWER(x))
#define IS_ALPHANUM(x) (IS_DIGIT(x) | IS_LETTER(x))

#define TO_UPPER(x) (IS_LOWER(x) ? x + 'A' - 'a' : x)
#define TO_LOWER(x) (IS_UPPER(x) ? x + 'a' - 'A' : x)
#define TO_INT(x) ((x) - '0' )
#define TO_DIGIT(x) ((char)('0' + x))

#define IN_RANGE(x,u,l) ((x>=u) & (x<=l))

#define IS_BASE64_x(x) ((x == '-') | (x == '_') | (x == '+') | (x == '/'))
#define IS_BASE64(x) (IS_DIGIT(x) | IS_LETTER(x) | IS_BASE64_x(x))


#define TP_SUCCESS(x)		IN_RANGE(x, 200, 399)
#define TP_FAIL(x)			(!TP_SUCCESS(x))

#define TP_COMPLETE(x)		IN_RANGE(x, 200, 299)
#define TP_INTERMEDIATE(x)	IN_RANGE(x, 300, 399)
#define TP_TRANSIENT(x)		IN_RANGE(x, 400, 499)
#define TP_PERMANENT(x)		IN_RANGE(x, 500, 599)

#define UTF8_BYTES_1		0x7f
#define UTF8_BYTES_2		0x7ff
#define UTF8_BYTES_3		0xffff
#define UTF8_BYTES_4		0x1fffff
#define UTF8_BYTES_5		0x73fffff
#define UTF8_BYTES_6		0x7fffffff


#define MASK_CHECK(x,m,t) ((x&m) == t)
#define BIT_SET(x,t) MASK_CHECK(x,t,t)

#define OFFSET_CAST(v,o) ((char*) v + o);
#define OFFSETOF(st, m) ((size_t)(&((st *)0)->m))

#define OFFSET_DECLARE(t,n,v,o)  t *n = (t*)  ((char*) v + o);

#define CAST(t,d,v) t *d = (t*) (v);

typedef boolean			Boolean;
typedef char			Byte;
typedef float			Real32;
typedef double			Real64;
typedef int				Int32;
typedef long long		Int64;
typedef Int64			Decimal64;
typedef unsigned int	UInt32;
typedef unsigned long	UInt64;

// Will move to using IntP in all situations where a pointer sized
// integer is required.

//typedef size_t			IntP;
typedef Int64			IntP;
typedef size_t			IntT;

// The decimal multipleier is used in Decimal64 arithmetic
#define DECIMAL_MULTIPLIER	1000000000
#define DECIMAL_MAX (LLONG_MAX / DECIMAL_MULTIPLIER)

#define DPLUS(x,y)  (x+y)
#define DMINUS(x,y)  (x-y)
#define DMULT(x,y)  ((  (x/DECIMAL_MULTIPLIER) * y) +    \
		((x%DECIMAL_MULTIPLIER) * (y/DECIMAL_MULTIPLIER)) + \
		((x%DECIMAL_MULTIPLIER) * (y%DECIMAL_MULTIPLIER) / DECIMAL_MULTIPLIER))

#define DReal64(x)  (((Real64) x) /DECIMAL_MULTIPLIER)


#define Int64_NaN		LONG_MIN
#define Decimal64_NaN	LONG_MIN
#define Int32_NaN		INT_MIN
#define Boolean_NaN		INT_MIN

#define MAX_FORMATTED_DATE	36


#define TP_OPTION_TYPE_NULL			0
#define TP_OPTION_TYPE_INTEGER		1
#define TP_OPTION_TYPE_KEYWORD		2

#define ARRAY_WRAP(x) {x, COUNT(x)}




#ifdef DEBUG_OUTPUT
#define DEBUG_PRINT(x) printf x
#define DEBUG_ACTION(x) x
#define DEBUG_JSTRING(t,x) JSON_String_Write(t,x);
#else
#define DEBUG_PRINT(x) 
#define DEBUG_ACTION(x)
#define DEBUG_JSTRING(t,x) 
#endif



#endif // __HEADER_MASTER_PHB