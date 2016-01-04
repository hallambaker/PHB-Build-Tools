#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib\common.h>
#include <float.h>



PUBLIC_ENTRY JSON_Begin (JSON_Context *Context) {
	
	BEGIN;

	JSON_Stream_Write_CharZ (Context->Stream, "{\n");

	END;
	}


PUBLIC_ENTRY JSON_End (JSON_Context *Context) {
	BEGIN;

	JSON_Stream_Write_CharZ (Context->Stream, "}\n");

	END;
	}

PUBLIC_ENTRY 	JSON_Serialize_Tag (JSON_Context *Context, char *tag, boolean *Comma) {
	BEGIN;

	if (*Comma) {
		JSON_Stream_Write_Char (Context->Stream, ',');
		JSON_Stream_Write_Char (Context->Stream, '\n');
		}
	*Comma = TRUE;

	JSON_Stream_Write_Char (Context->Stream, '\"');
	JSON_Stream_Write_CharZ (Context->Stream, tag);
	JSON_Stream_Write_Char (Context->Stream, '\"');
	JSON_Stream_Write_Char (Context->Stream, ':');

	END;
	}

PUBLIC_ENTRY 	JSON_Serialize_Int64 (JSON_Context *Context, char *tag, Int64 Data,
			boolean Required, char *Default, boolean *Comma) {

	BEGIN;

	if (!Required & (Data == Int64_NaN)){
		return TRUE;
		}

	JSON_Serialize_Tag (Context, tag, Comma);
	if (Data == Int64_NaN) {
		JSON_Stream_Write_CharZ (Context->Stream, "null");
		}
	else {
		JSON_Stream_Write_Integer (Context->Stream, Data);
		}

	END;
	}

PUBLIC_ENTRY 	JSON_Serialize_Boolean (JSON_Context *Context, char *tag, boolean Data,
			boolean Required, char *Default, boolean *Comma) {
	BEGIN;

	if (!Required & (Data == Boolean_NaN)){
		return TRUE;
		}

	JSON_Serialize_Tag (Context, tag, Comma);
	if (Data == Boolean_NaN) {
		JSON_Stream_Write_CharZ (Context->Stream, "null");
		}
	else {
		JSON_Stream_Write_Boolean (Context->Stream, Data);
		}

	END;
	}

PUBLIC_ENTRY 	JSON_Serialize_Real64 (JSON_Context *Context, char *tag, double Data,
			boolean Required, char *Default, boolean *Comma) {
	BEGIN;

	if (!Required & isnan(Data)){
		return TRUE;
		}

	JSON_Serialize_Tag (Context, tag, Comma);
	if (isnan(Data)) {
		JSON_Stream_Write_CharZ (Context->Stream, "null");
		}
	else {
		JSON_Stream_Write_Real64 (Context->Stream, Data);
		}

	END;
	}

PUBLIC_ENTRY 	JSON_Serialize_Binary (JSON_Context *Context, char *tag, JSON_Binary *Data,
			boolean Required, char *Default, boolean *Comma) {
	BEGIN;

	if (!Required & (Data->Data == NULL)){
		return TRUE;
		}

	JSON_Serialize_Tag (Context, tag, Comma);
	if (Data->Data == NULL) {
		JSON_Stream_Write_CharZ (Context->Stream, "null");
		}
	else {
		JSON_Stream_Write_Binary (Context->Stream, Data);
		}

	END;
	}

PUBLIC_ENTRY 	JSON_Serialize_String (JSON_Context *Context, char *tag, JSON_String *Data,
			boolean Required, char *Default, boolean *Comma) {
	BEGIN;

	if (!Required & (Data->Data == NULL)) {
		return TRUE;
		}

	JSON_Serialize_Tag (Context, tag, Comma);

	if (Required) {
		JSON_Stream_Write_CharZ (Context->Stream, "null");
		}
	else {
		JSON_Stream_Write_JString (Context->Stream, Data);
		}

	END;
	}

PUBLIC_ENTRY 	JSON_Serialize_DateTime (JSON_Context *Context, char *tag, JSON_DateTime *Data,
			boolean Required, char *Default, boolean *Comma) {
	BEGIN;

	if (!Required & (Data->Data == NULL)) {
		return TRUE;
		}

	JSON_Serialize_Tag (Context, tag, Comma);

	if (Required) {
		JSON_Stream_Write_CharZ (Context->Stream, "null");
		}
	else {
		JSON_Stream_Write_DateTime (Context->Stream, Data);
		}

	END;
	}


PUBLIC_ENTRY 	JSON_Serialize_Decimal64 (JSON_Context *Context, char *tag, Decimal64 Data,
			boolean Required, char *Default, boolean *Comma) {
	BEGIN;

	if (!Required & (Data == Decimal64_NaN)) {
		return TRUE;
		}

	JSON_Serialize_Tag (Context, tag, Comma);

	if (Required) {
		JSON_Stream_Write_CharZ (Context->Stream, "null");
		}
	else {
		//JSON_Stream_Write_DateTime (Context->Stream, Data);
		}

	END;
	}

//PUBLIC_ENTRY 	JSON_Serialize_URI (JSON_Context *Context, char *tag, JSON_String *Data,
//			boolean Required, char *Default, boolean *Comma) {
//	BEGIN;
//
//	if (!Required & (Data->Data == NULL)) {
//		return TRUE;
//		}
//
//	JSON_Serialize_Tag (Context, tag, Comma);
//
//	if (Required) {
//		JSON_Stream_Write_CharZ (Context->Stream, "null");
//		}
//	else {
//		JSON_Stream_Write_JString (Context->Stream, Data);
//		}
//
//	END;
//	}



// Write out an object value into the stream
PUBLIC_ENTRY JSON_Serialize_Item (JSON_Context *Context, JSON_Object *Data) {
	BEGIN;

	if (Data == NULL) {
		JSON_Stream_Write_CharZ (Context->Stream, "null");
		return TRUE;
		}

	if (Data->_Type == JSON_TYPE_Boolean) {
		JSON_Stream_Write_Boolean (Context->Stream, Data->As.Boolean);
		}
	else if (Data->_Type == JSON_TYPE_Int64) {
		JSON_Stream_Write_Integer (Context->Stream, Data->As.Integer);
		}
	else if (Data->_Type == JSON_TYPE_Real64) {
		JSON_Stream_Write_Real64 (Context->Stream, Data->As.Float);
		}
	else if (Data->_Type == JSON_TYPE_String) {
		JSON_Stream_Write_JString (Context->Stream, &Data->As.String);
		}
	else if (Data->_Type == JSON_TYPE_Binary) {
		JSON_Stream_Write_Binary (Context->Stream, &Data->As.Binary);
		}
	else {
		// ok got an object here...
		JSON_Registry *Registry = Context->Registry  + Data->_Type;

		Registry->Serialize (Context, Data);

		}


	END;
	}


PUBLIC_ENTRY 	JSON_Serialize_Group (JSON_Context *Context, char *tag, JSON_Group *Data,
			boolean Required, char *Default, boolean *Comma) {
	
	JSON_Object *Entry;
	BEGIN;

	if (!Required & (Data->First == NULL)) {
		return TRUE;
		}

	JSON_Serialize_Tag (Context, tag, Comma);
	JSON_Stream_Write_Char (Context->Stream, '[');

	if (Data->First != NULL) {
		JSON_Serialize_Item (Context, Data->First);
		for (Entry = Data->First->_Next; Entry != NULL; Entry = Entry->_Next) {
			JSON_Stream_Write_Char (Context->Stream, ',');
			JSON_Stream_Write_Char (Context->Stream, '\n');
			JSON_Serialize_Item (Context, Entry);
			}
		}




	JSON_Stream_Write_Char (Context->Stream, ']');
	END;
	}

//PUBLIC_ENTRY 	JSON_Serialize_Group_Int64 (JSON_Context *Context, char *tag, JSON_Group *Data,
//			boolean Required, char *Default, boolean *Comma) {
//	BEGIN;
//	END;
//	}
//
//PUBLIC_ENTRY 	JSON_Serialize_Group_Boolean (JSON_Context *Context, char *tag, JSON_Group *Data,
//			boolean Required, char *Default, boolean *Comma) {
//	BEGIN;
//	END;
//	}
//
//PUBLIC_ENTRY 	JSON_Serialize_Group_Real64 (JSON_Context *Context, char *tag, JSON_Group *Data,
//			boolean Required, char *Default, boolean *Comma) {
//	BEGIN;
//	END;
//	}
//
//PUBLIC_ENTRY 	JSON_Serialize_Group_Binary (JSON_Context *Context, char *tag, JSON_Group *Data,
//			boolean Required, char *Default, boolean *Comma) {
//	BEGIN;
//	END;
//	}
//
//PUBLIC_ENTRY 	JSON_Serialize_Group_String (JSON_Context *Context, char *tag, JSON_Group *Data,
//			boolean Required, char *Default, boolean *Comma) {
//	BEGIN;
//	END;
//	}