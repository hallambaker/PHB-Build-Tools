#include <protogenlib\json_types.h>

END_PUBLIC_HEADERS
#include <protogenlib\common.h>

static const char b64_table[65] =  "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
static const char b64u_table[65] = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

static const char reverse_table[128] = {
   64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,  //  15
   64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,  //  31
   64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 62, 64, 62, 64, 63,  //  47
   52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 64, 64, 64, 64, 64, 64,  //  63
   64,  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14,  //  79
   15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 64, 64, 64, 64, 63,  //  95
   64, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,  // 111
   41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 64, 64, 64, 64, 64   // 127
	};


// Don't need to check the array bounds here because it is only ever called
// after the input has been checked with IS_BASE64

static void UnMap (char acc[4], char *out) {
	int b0, b1, b2, b3;
	unsigned int R;

	b0 = reverse_table [acc[0]];
	b1 = reverse_table [acc[1]];
	b2 = reverse_table [acc[2]];
	b3 = reverse_table [acc[3]];

	R = (b0<<18) | (b1<<12) | (b2<<6) | (b3);

	out [0] = (R >> 16) & 0xff;
	out [1] = (R >> 8) & 0xff;
	out [2] = R & 0xff;

	}

static void Map (char i1, char i2, char i3, char *out) {

	unsigned int R;
		
	R = ((unsigned char)i1<<16) | ((unsigned char)i2<<8) | ((unsigned char)i3);

	out[3] = b64_table[R & 0x3f];
	out[2] = b64_table[(R>>6) & 0x3f];
	out[1] = b64_table[(R>>12) & 0x3f];
	out[0] = b64_table[(R>>18) & 0x3f];
	}

static void MapU (char i1, char i2, char i3, char *out) {

	unsigned int R;
		
	R = (i1<<16) | (i2<<8) | (i3);

	out[3] = b64u_table[R & 0x3f];
	out[2] = b64u_table[(R>>6) & 0x3f];
	out[1] = b64u_table[(R>>12) & 0x3f];
	out[0] = b64u_table[(R>>18) & 0x3f];
	}


PUBLIC_ENTRY base64_encode(char *data_in, int length_in, char*data_out, int *length_out) {

	int i;
	int out_index=0;
	BEGIN;

	ASSERT (*length_out > 1+ ((1 + (length_in / 3) * 4)), -1, "Buffer too small");

	for (i=0; i<length_in-2; i+=3) {
		Map (data_in[i], data_in[i+1], data_in[i+2], data_out + out_index);
		out_index += 4;
		}

	if (length_in - i == 1) {
		char buffer [4];
		Map (data_in[i], 0, 0, buffer);
		data_out[out_index++] = buffer[0];
		data_out[out_index++] = buffer[1];
		data_out[out_index++] = '=';
		data_out[out_index++] = '=';
		}
	else if (length_in - i == 2) {
		char buffer [4];
		Map (data_in[i], data_in[i+1], 0, buffer);
		data_out[out_index++] = buffer[0];
		data_out[out_index++] = buffer[1];
		data_out[out_index++] = buffer[2];
		data_out[out_index++] = '=';
		}
	data_out[out_index] = 0; // null terminate the string just in case

	*length_out = out_index;

	END;
	}

PUBLIC_ENTRY base64uri_encode(char *data_in, int length_in, char*data_out, int *length_out) {

	int i;
	int out_index=0;
	BEGIN;

	ASSERT (*length_out > 1+ ((1 + (length_in / 3) * 4)), -1, "Buffer too small");

	for (i=0; i<length_in-2; i+=3) {
		MapU (data_in[i], data_in[i+1], data_in[i+2], data_out + out_index);
		out_index += 4;
		}

	if (length_in - i == 1) {
		char buffer [4];
		MapU (data_in[i], 0, 0, buffer);
		data_out[out_index++] = buffer[0];
		data_out[out_index++] = buffer[1];
		data_out[out_index++] = '=';
		data_out[out_index++] = '=';
		}
	else if (length_in - i == 2) {
		char buffer [4];
		MapU (data_in[i], data_in[i+1], 0, buffer);
		data_out[out_index++] = buffer[0];
		data_out[out_index++] = buffer[1];
		data_out[out_index++] = buffer[2];
		data_out[out_index++] = '=';
		}
	data_out[out_index] = 0; // null terminate the string just in case

	*length_out = out_index;

	END;
	}

// MIID PjCC Aiag AwIB AgIR AOr4 LpsL Ca5t 4Q3P _rJk 1nww DQYJ KoZI hvcN AQEN BQAw

PUBLIC_ENTRY JSON_Stream_Write_Binary (JSON_Stream *Stream, JSON_Binary *Data) {
	
	int i;
	char b0, b1, b2;
	char chars[5];

	BEGIN;
	chars[4]=0;

	JSON_Stream_Write_Char (Stream, '\"');

	for (i =0; i<(Data->Length-2);) {
		b0 = Data->Data[i++];
		b1 = Data->Data[i++];
		b2 = Data->Data[i++];
		Map (b0, b1, b2, chars);
		JSON_Stream_Write_CharZ (Stream, chars);
		if (i%48 == 0) {
			JSON_Stream_Write_Char (Stream, '\n');
			}
		}
	if (Data->Length - i == 1) {
		Map (Data->Data[i], 0, 0, chars);
		chars [2] = '=';
		chars [3] = '=';
		JSON_Stream_Write_CharZ (Stream, chars);
		}
	else if (Data->Length - i == 2) {
		Map (Data->Data[i], Data->Data[i+1], 0, chars);
		chars [3] = '=';
		JSON_Stream_Write_CharZ (Stream, chars);
		}

	JSON_Stream_Write_Char (Stream, '\"');
	
	END;
	}



PUBLIC_ENTRY JSON_Stream_Read_Binary (JSON_Stream *Stream, JSON_Binary *Data) {
	int i = 0;
	char c = 0;
	char acc [4];
	int p = 0;
	BEGIN;

	while ((c != '"') & (Data->Length+3 < Data->Allocated)){
		JSON_Stream_Get_Char (Stream, &c);
		//printf ("%c", c);

		if (IS_BASE64(c)) {
			acc [p++] = c;

			if (p >=4) {

				UnMap (acc, Data->Data + Data->Length);
				Data->Length += 3;
				p=0;
				}
			}
		}

	if (p == 2) {
		char res [3];
		UnMap (acc, res);
		Data->Data [Data->Length++] = res [0];
		}

	else if (p == 3) {
		char res [3];
		UnMap (acc, res);
		Data->Data [Data->Length++] = res [0];
		Data->Data [Data->Length++] = res [1];
		}
	
	// Convenience for null terminated string data
	Data->Data [Data->Length] = 0;

	END;
	}