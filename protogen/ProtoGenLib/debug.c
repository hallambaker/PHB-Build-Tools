#include "common.h"

END_PUBLIC_HEADERS

#include <keystorelib/common.h>
extern JSON_Object *Dump_Object;

PUBLIC_ENTRY PHB_Dump_Phinger () {
	
	KeyStore_Phinger *Phinger = (KeyStore_Phinger*) Dump_Object;
	BEGIN;

	if (Dump_Object == NULL) return -1;

	END;
	}

