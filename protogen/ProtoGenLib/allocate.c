#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS

#define DEBUG_OUTPUT true
#include <protogenlib\common.h>

Boolean Z_initialized = FALSE;
IntT Z_Count_Allocate;
IntT Z_Count_Free;
IntT Z_Count_Active;
IntT Z_Count_Bytes;

IntT Z_Canary = 0x01234567;

STATUS Z_Init () {
	BEGIN;

	ONCE (Z_initialized);
	Z_Count_Allocate = 0;
	Z_Count_Free = 0;
	Z_Count_Active = 0;
	Z_Count_Bytes = 0;

	END;
	}

STATUS Z_Terminate () {
	BEGIN;

	ASSERT (Z_Count_Active == 0, -1, "Memory Leak detected");

	CATCH;


	END;
	}



#define Z_SIZE (sizeof (Z_Header))
#define Z_DATA(x)	((char *) (x)) + sizeof (Z_Header) 
#define Z_HEADER(x)	(Z_Header*) (((char *) (x)) - sizeof (Z_Header) )
#define Z_CANARY(x)	ASSERT ((x)->Canary == Z_Canary, -1, "Fatal memory error");


STATUS Z_Allocate (void *Parent, IntT Size, void **ResultOut) {
	Z_Header	*Result = NULL;

	BEGIN;

	*ResultOut = NULL;

	Result = (Z_Header*) malloc (Size + sizeof (Z_Header));
	Result->Canary = Z_Canary;
	Result->Parent = Z_HEADER (Parent);
	if (Parent != NULL) {
		Z_CANARY (Result->Parent);
		if (Result->Parent->First == NULL) {
			Result->Parent->First = Result;
			Result->Parent->Last = Result;
			Result->Prev = NULL;
			Result->Next = NULL;					// No existing chain, make one
			}
		else {
			Result->Prev = Result->Parent->Last;
			Result->Parent->Last->Next = Result;
			Result->Parent->Last = Result;			// Add it in at the end
			}
		}
	else {
		Result->Prev = NULL;
		Result->Next = NULL;						// No parent, won't use these
		}

	Result->First = NULL;
	Result->Last = NULL;							// Never children at allocation

	Z_Count_Allocate ++;
	Z_Count_Active ++;
	Z_Count_Bytes += Size;

	*ResultOut = Z_DATA (Result);

	DEBUG_ACTION (Z_Check_Header (Result));

	CATCH;

	END;
	}


STATUS Z_Reallocate (void *Data, IntT Size, void **ResultOut) {
	Z_Header *Header, *NewHeader, *Parent, *Prev, *Next;

	BEGIN;

	Header = Z_HEADER (Data);
	DEBUG_ACTION (Z_Check_Header (Header));

	Parent = Header->Parent;
	Prev = Header->Prev;
	Next = Header->Next;

	NewHeader = (Z_Header *) realloc (Header, Size+Z_SIZE);

	if (Parent != NULL) {
		if (Parent->First == Header) {
			Parent->First = NewHeader;
			}
		if (Parent->Last == Header) {
			Parent->Last = NewHeader;
			}
		}

	if (Prev != NULL) {
		Prev->Next = NewHeader;
		}
	if (Next != NULL) {
		Next->Prev = NewHeader;
		}

	*ResultOut = Z_DATA (NewHeader);

	END;
	}

STATUS Z_Free (void *Data) {
	Z_Header *Header;
	
	BEGIN;


	Header = Z_HEADER (Data);
	DEBUG_ACTION (Z_Check_Header (Header));

	Z_CANARY (Header);

	// unlink from parent - only do this for the head
	if (Header->Parent != NULL) {
		Z_CANARY (Header->Parent);

		if (Header->Parent->First == Header) {
			if (Header->Parent->Last == Header) {
				Header->Parent->First = NULL;
				Header->Parent->Last = NULL;
				}
			else {
				Header->Parent->First = Header->Next;
				Header->Next->Prev = NULL;
				}
			}
		else if (Header->Parent->Last == Header) {
			Header->Parent->Last = Header->Prev;
			Header->Prev->Next = NULL;
			}
		else {
			Header->Next->Prev = Header->Prev;
			Header->Prev->Next = Header->Next;
			}
		}

	// free this
	Z_Free_Child (Header);

	END;
	}

STATUS Z_Free_Child (Z_Header *Header) {
	Z_Header *Child;
	IntT Size;
	BEGIN;

	Z_CANARY (Header);
	Size = Header->Size;

	//free up the children
	for (Child = Header->First; Child != NULL; Child = Child->Next) {
		Z_Free_Child (Child);
		}

	// update stats
	Z_Count_Free ++;
	Z_Count_Active --;
	Z_Count_Bytes -= Size;

	// Zero memory using a macro that will not be optimized out
	SecureZeroMemory (Header, Size + Z_SIZE);
	
	// Free this
	free (Header);

	END;
	}

STATUS Z_Check (void *Data) {
	Z_Header *Header;
	
	BEGIN;

	printf ("\nAlloc %d Free %d Size %d\n",
			Z_Count_Allocate, Z_Count_Free, Z_Count_Bytes);

	Header = Z_HEADER (Data);
	Z_Check_Header (Header);




	END;
	}

STATUS Z_Check_Header (Z_Header *Header) {
	Z_Header *Child, *Prev;
	BEGIN;

	printf ("Checking %p [%p-%p] Next %p Prev %P Size %d\n",
			Header, Header->First, Header->Last, Header->Next, Header->Prev,
			Header->Size);

	// Here traverse the tree and check that everything is OK
	Z_CANARY (Header);

	if (Header->Parent != NULL) {
		Z_CANARY (Header->Parent);
		}

	if (Header->First != NULL) {
		Z_CANARY (Header->First);

		Prev = Header->First;
		ASSERT (Prev->Prev == NULL, -1, "Bad previous pointer");
		ASSERT (Prev->Parent == Header, -1, "Bad parent pointer");
		Z_Check_Header (Prev);

		for (Child = Header->First->Next; Child != NULL; Child = Child->Next) {
			ASSERT (Child->Prev == Prev, -1, "Bad previous pointer");
			ASSERT (Child->Parent == Header, -1, "Bad parent pointer");
			Z_Check_Header (Child);
			Prev = Child;
			}
		ASSERT (Header->Last == Prev, -1, "Bad last pointer");

		}

	END;
	}