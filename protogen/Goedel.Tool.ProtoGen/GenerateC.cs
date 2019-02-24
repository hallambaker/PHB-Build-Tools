// Script Syntax Version:  1.0

//  Copyright ©  2017 by 
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  
//  
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.ProtoGen {
	public partial class Generate : global::Goedel.Registry.Script {

		 string Prefix; 
		 string LibPrefix = "JSON"; 
		 string XStructure = null;
		

		//
		// GenerateH
		//
		public void GenerateH (ProtoStruct ProtoStruct) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("#pragma once\n{0}", _Indent);
			_Output.Write ("//\n{0}", _Indent);
			_Output.Write ("// Code generated automatically\n{0}", _Indent);
			_Output.Write ("//\n{0}", _Indent);
			_Output.Write ("// Really Do not edit!\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("// \n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (var Item in ProtoStruct.Top) {
				 Item.Normalize ();
				switch (Item._Tag ()) {
					case ProtoStructType.Protocol: {
					  Protocol Protocol = (Protocol) Item; 
					
					 Prefix = Protocol.Prefix.ToString ();
					
					 string MacroID = "_HEADER_" + Protocol.Id.ToString().Replace ('.', '_');
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("#ifndef {1}\n{0}", _Indent, MacroID);
					_Output.Write ("#define {1} 1\n{0}", _Indent, MacroID);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("// Protocol:	{1}\n{0}", _Indent, Protocol.Id);
					_Output.Write ("// Prefix:		{1}\n{0}", _Indent, Prefix);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("#include <protogenlib\\common.h>\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					foreach  (Structure Structure in Protocol.Structures) {
						_Output.Write ("// Structure {1}\n{0}", _Indent, Structure.Id);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("typedef struct _{1}_{2} {{\n{0}", _Indent, Prefix, Structure.Id);
						_Output.Write ("	struct _{1}_{2}		*_Next;\n{0}", _Indent, Prefix, Structure.Id);
						_Output.Write ("	int								_Type;\n{0}", _Indent);
						_Output.Write ("	struct _JSON_String				_String;		// Maybe only include if declared to be a FORMAT?\n{0}", _Indent);
						MakeCStructure (Structure.AllEntries);
						_Output.Write ("	}} {1}_{2};\n{0}", _Indent, Prefix, Structure.Id);
						_Output.Write ("\n{0}", _Indent);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("// Type tag values\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					
					 int count = 0;
					_Output.Write ("#define {1}_TYPE_String  {2}\n{0}", _Indent, Prefix, count++);
					_Output.Write ("#define {1}_TYPE_Binary  {2}\n{0}", _Indent, Prefix, count++);
					_Output.Write ("#define {1}_TYPE_Int64  {2}\n{0}", _Indent, Prefix, count++);
					_Output.Write ("#define {1}_TYPE_Decimal64  {2}\n{0}", _Indent, Prefix, count++);
					_Output.Write ("#define {1}_TYPE_Real64  {2}\n{0}", _Indent, Prefix, count++);
					_Output.Write ("#define {1}_TYPE_Boolean  {2}\n{0}", _Indent, Prefix, count++);
					_Output.Write ("#define {1}_TYPE_DateTime  {2}\n{0}", _Indent, Prefix, count++);
					_Output.Write ("#define {1}_TYPE_URI  {2}\n{0}", _Indent, Prefix, count++);
					_Output.Write ("#define {1}_TYPE_Format  {2}\n{0}", _Indent, Prefix, count++);
					foreach  (Structure Structure in Protocol.Structures) {
						_Output.Write ("#define {1}_TYPE_{2}  {3}\n{0}", _Indent, Prefix, Structure.Id, count++);
						}
					_Output.Write ("#define {1}_TYPE__Count {2}\n{0}", _Indent, Prefix, count++);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("// Function Prototype Definitions..\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					foreach  (Structure Structure in Protocol.Structures) {
						_Output.Write ("// Structure {1}\n{0}", _Indent, Structure.Id);
						_Output.Write ("int {1}_Serialize_{2} (\n{0}", _Indent, Prefix, Structure.Id);
						_Output.Write ("		{1}_Context *Context, {2}_{3} *Data);\n{0}", _Indent, LibPrefix, Prefix, Structure.Id);
						_Output.Write ("int {1}_Deserialize_{2} (\n{0}", _Indent, Prefix, Structure.Id);
						_Output.Write ("		{1}_Context *Context, {2}_{3} **Data);\n{0}", _Indent, LibPrefix, Prefix, Structure.Id);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("PUBLIC_ENTRY {1}__Initialize ();\n{0}", _Indent, Prefix);
					_Output.Write ("PUBLIC_ENTRY {1}_Initialize ();\n{0}", _Indent, Prefix);
					_Output.Write ("PUBLIC_ENTRY {1}__End ();\n{0}", _Indent, Prefix);
					_Output.Write ("PUBLIC_ENTRY {1}_End ();\n{0}", _Indent, Prefix);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("#define {1}_Initialize {2}__Initialize\n{0}", _Indent, Prefix, Prefix);
					_Output.Write ("#define {1}_End {2}__End\n{0}", _Indent, Prefix, Prefix);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("PUBLIC_ENTRY {1}_Create (void *parent, long type, void **Result_Out);\n{0}", _Indent, Prefix);
					_Output.Write ("PUBLIC_ENTRY {1}_Create_Context (JSON_Stream *Stream, JSON_Context **Result_Out);\n{0}", _Indent, Prefix);
					_Output.Write ("PUBLIC_ENTRY {1}_Free (void *item);\n{0}", _Indent, Prefix);
					_Output.Write ("PUBLIC_ENTRY {1}_Deserialize_List (JSON_Context *Context, void **Data_out) ;\n{0}", _Indent, Prefix);
					_Output.Write ("PUBLIC_ENTRY {1}_Deserialize (JSON_Context *Context, void **Data_out) ;\n{0}", _Indent, Prefix);
					_Output.Write ("\n{0}", _Indent);
					foreach  (Structure Structure in Protocol.Structures) {
						_Output.Write ("PUBLIC_ENTRY {1}_Create_{2} (void *parent, {3}_{4} **Result_Out) ;\n{0}", _Indent, Prefix, Structure.Id, Prefix, Structure.Id);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("#endif // {1}\n{0}", _Indent, MacroID);
					_Output.Write ("\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// MakeCStructure
		//
		public void MakeCStructure (List<_Choice> Members) {
			foreach  (_Choice Member in Members) {
				MakeCStructure (Member);
				}
			}
		

		//
		// MakeCStructure
		//
		public void MakeCStructure (_Choice Member) {
			if (  Member.Multiple ) {
				_Output.Write ("	JSON_Group				{1};\n{0}", _Indent, Member.ID);
				} else {
				switch (Member._Tag ()) {
					case ProtoStructType.Struct: {
					  Struct Cast = (Struct) Member; 
					_Output.Write ("	struct _{1}_{2}		*{3};\n{0}", _Indent, Prefix, Cast.Type, Member.ID);
					break; }
					default : {
					if (  (Member.TypeC != null) ) {
						_Output.Write ("	{1}			{2};\n{0}", _Indent, Member.TypeC, Member.ID);
						}
				break; }
					}
				}
			}
		

		//
		// GenerateC
		//
		public void GenerateC (ProtoStruct ProtoStruct) {
			GenerateH (ProtoStruct);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Item in ProtoStruct.Top) {
				 Item.Normalize ();
				switch (Item._Tag ()) {
					case ProtoStructType.Protocol: {
					  Protocol Protocol = (Protocol) Item; 
					
					 Prefix = Protocol.Prefix.ToString ();
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("// Protocol:	{1}\n{0}", _Indent, Protocol.Id);
					_Output.Write ("// Prefix:		{1}\n{0}", _Indent, Prefix);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("// Metadata Tables\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					foreach  (Structure Structure in Protocol.Structures) {
						 XStructure = Structure.ID;
						if (  (Structure.CountChildren > 0) ) {
							_Output.Write ("JSON_Parse _{1}_Meta_{2} [] = {{\n{0}", _Indent, Prefix, Structure.Id);
							MakeCDeserialize (Structure.AllEntriesUnsorted);
							_Output.Write ("	}} ;\n{0}", _Indent);
							_Output.Write ("#define _{1}_Meta_{2}_C COUNT (_{3}_Meta_{4})\n{0}", _Indent, Prefix, Structure.Id, Prefix, Structure.Id);
							} else {
							_Output.Write ("JSON_Parse *_{1}_Meta_{2} = NULL;\n{0}", _Indent, Prefix, Structure.Id);
							_Output.Write ("#define _{1}_Meta_{2}_C 0\n{0}", _Indent, Prefix, Structure.Id);
							}
						_Output.Write ("\n{0}", _Indent);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("static JSON_Registry _{1}_Meta [] = {{\n{0}", _Indent, Prefix);
					_Output.Write ("	{{\"String\", sizeof(\"String\")-1}},\n{0}", _Indent);
					_Output.Write ("	{{\"Binary\", sizeof(\"Binary\")-1}},\n{0}", _Indent);
					_Output.Write ("	{{\"Int64\", sizeof(\"Int64\")-1}},\n{0}", _Indent);
					_Output.Write ("	{{\"Decimal64\", sizeof(\"Decimal64\")-1}},\n{0}", _Indent);
					_Output.Write ("	{{\"Real64\", sizeof(\"Real64\")-1}},\n{0}", _Indent);
					_Output.Write ("	{{\"Boolean\", sizeof(\"Boolean\")-1}},\n{0}", _Indent);
					_Output.Write ("	{{\"DateTime\", sizeof(\"DateTime\")-1}},	\n{0}", _Indent);
					_Output.Write ("	{{\"URI\", sizeof(\"URI\")-1}},\n{0}", _Indent);
					_Output.Write ("	{{\"Format\", sizeof(\"Format\")-1}}", _Indent);
					foreach  (Structure Structure in Protocol.Structures) {
						_Output.Write (",\n{0}", _Indent);
						_Output.Write ("	{{\"{1}\", sizeof (\"{2}\")-1, NULL, _{3}_Meta_{4}_C, sizeof ({5}_{6}), \n{0}", _Indent, Structure.Id, Structure.Id, Prefix, Structure.Id, Prefix, Structure.Id);
						_Output.Write ("		(JSON_Serializer) {1}_Serialize_{2}, (JSON_Initializer) {3}_Deserialize_{4}, ", _Indent, Prefix, Structure.Id, Prefix, Structure.Id);
						_Output.Write ("(JSON_Initializer) {1}_Create_{2}}} ", _Indent, Prefix, Structure.Id);
						}
					_Output.Write ("}};\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("JSON_Registry *{1}_Meta =  _{2}_Meta;\n{0}", _Indent, Prefix, Prefix);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("// Function Definitions\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					foreach  (Structure Structure in Protocol.Structures) {
						_Output.Write ("// Structure {1}\n{0}", _Indent, Structure.Id);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("PUBLIC_ENTRY {1}_Create_{2} (void *parent, {3}_{4} **Result_Out) {{\n{0}", _Indent, Prefix, Structure.Id, Prefix, Structure.Id);
						_Output.Write ("	{1}_{2} *Result;\n{0}", _Indent, Prefix, Structure.Id);
						_Output.Write ("	BEGIN;\n{0}", _Indent);
						_Output.Write ("	JSON_Create (parent, {1}_TYPE_{2}, _{3}_Meta, (void**) &Result);\n{0}", _Indent, Prefix, Structure.Id, Prefix);
						_Output.Write ("	\n{0}", _Indent);
						foreach  (_Choice Member in Structure.AllEntries) {
							if (  Member.Multiple	 ) {
								_Output.Write ("	Result->{1}.Count = -1;\n{0}", _Indent, Member.ID);
								_Output.Write ("	Result->{1}.First = NULL;\n{0}", _Indent, Member.ID);
								_Output.Write ("	Result->{1}.Last = NULL;\n{0}", _Indent, Member.ID);
								_Output.Write ("	Result->{1}.Members = NULL;\n{0}", _Indent, Member.ID);
								} else {
								switch (Member._Tag ()) {
									case ProtoStructType.Boolean: { 
									_Output.Write ("	Result->{1} = Boolean_NaN;\n{0}", _Indent, Member.ID);
									break; }
									case ProtoStructType.Integer: { 
									_Output.Write ("	Result->{1} = Int64_NaN;\n{0}", _Indent, Member.ID);
									break; }
									case ProtoStructType.Decimal: { 
									_Output.Write ("	Result->{1} = Decimal64_NaN;\n{0}", _Indent, Member.ID);
									break; }
									case ProtoStructType.Float: { 
									_Output.Write ("	Result->{1} = Real64_NaN;\n{0}", _Indent, Member.ID);
									break; }
									case ProtoStructType.String: { 
									_Output.Write ("	Result->{1}.Data = NULL;\n{0}", _Indent, Member.ID);
									break; }
									case ProtoStructType.Binary: { 
									_Output.Write ("	Result->{1}.Data = NULL;\n{0}", _Indent, Member.ID);
									break; }
									case ProtoStructType.Struct: { 
									_Output.Write ("	Result->{1} = NULL;\n{0}", _Indent, Member.ID);
								break; }
									}
								}
							}
						_Output.Write ("	\n{0}", _Indent);
						_Output.Write ("	*Result_Out = Result;\n{0}", _Indent);
						_Output.Write ("	END;\n{0}", _Indent);
						_Output.Write ("	}}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("int {1}_Serialize_{2} ({3}_Context *Context, {4}_{5} *Data) {{\n{0}", _Indent, Prefix, Structure.Id, LibPrefix, Prefix, Structure.Id);
						_Output.Write ("	boolean Comma = false;\n{0}", _Indent);
						_Output.Write ("	BEGIN;\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	if (Data == NULL) {{\n{0}", _Indent);
						_Output.Write ("		JSON_Stream_Write_Null (Context->Stream);\n{0}", _Indent);
						_Output.Write ("		}}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	{1}_Begin (Context);\n{0}", _Indent, LibPrefix);
						MakeCSerialize (Structure.AllEntries);
						_Output.Write ("	{1}_End (Context);\n{0}", _Indent, LibPrefix);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	END;\n{0}", _Indent);
						_Output.Write ("	}}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("int {1}_Deserialize_{2} (\n{0}", _Indent, Prefix, Structure.Id);
						_Output.Write ("		{1}_Context *Context, {2}_{3} **Data) {{\n{0}", _Indent, LibPrefix, Prefix, Structure.Id);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	BEGIN;\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	{1}_Create_{2} (Context, Data);\n{0}", _Indent, Prefix, Structure.Id);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("	return JSON_Deserialize (Context, _{1}_Meta + {2}_TYPE_{3}, *Data);\n{0}", _Indent, Prefix, Prefix, Structure.Id);
						_Output.Write ("	\n{0}", _Indent);
						_Output.Write ("	END;\n{0}", _Indent);
						_Output.Write ("	}}\n{0}", _Indent);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("// Totally lame that this is necessary but it is.\n{0}", _Indent);
					_Output.Write ("//\n{0}", _Indent);
					_Output.Write ("// C requires initializers be literal constants, it is not enough to declare\n{0}", _Indent);
					_Output.Write ("// the variable to be of type const\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("PUBLIC_ENTRY {1}__Initialize () {{\n{0}", _Indent, Prefix);
					_Output.Write ("	BEGIN;\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	JSON_Initialize ();\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					foreach  (Structure Structure in Protocol.Structures) {
						_Output.Write ("	_{1}_Meta [{2}_TYPE_{3}].Table = _{4}_Meta_{5};\n{0}", _Indent, Prefix, Prefix, Structure.Id, Prefix, Structure.Id);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	END;\n{0}", _Indent);
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("PUBLIC_ENTRY {1}__End () {{\n{0}", _Indent, Prefix);
					_Output.Write ("	BEGIN;\n{0}", _Indent);
					_Output.Write ("	END;\n{0}", _Indent);
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("PUBLIC_ENTRY {1}_Create (void *parent, long type, void **Result_Out) {{\n{0}", _Indent, Prefix);
					_Output.Write ("	return JSON_Create (parent, type, _{1}_Meta, Result_Out);\n{0}", _Indent, Prefix);
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("PUBLIC_ENTRY {1}_Deserialize_List (JSON_Context *Context, void **Data_out) {{\n{0}", _Indent, Prefix);
					_Output.Write ("	return JSON_Deserialize_Object (Context, Data_out, FALSE);\n{0}", _Indent);
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("PUBLIC_ENTRY {1}_Deserialize (JSON_Context *Context, void **Data_out) {{\n{0}", _Indent, Prefix);
					_Output.Write ("	return JSON_Deserialize_Object (Context, Data_out, TRUE);\n{0}", _Indent);
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("PUBLIC_ENTRY {1}_Create_Context (JSON_Stream *Stream, JSON_Context **Result_Out) {{\n{0}", _Indent, Prefix);
					_Output.Write ("	JSON_Context		*Result;\n{0}", _Indent);
					_Output.Write ("	BEGIN;\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	JSON_Create_Context (Stream, &Result);\n{0}", _Indent);
					_Output.Write ("	Result->Registry = _{1}_Meta;\n{0}", _Indent, Prefix);
					_Output.Write ("	Result->Count = COUNT(_{1}_Meta);\n{0}", _Indent, Prefix);
					_Output.Write ("	*Result_Out = Result;\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("	END;\n{0}", _Indent);
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("PUBLIC_ENTRY {1}_Free (void *item) {{\n{0}", _Indent, Prefix);
					_Output.Write ("	\n{0}", _Indent);
					_Output.Write ("	return JSON_Free (item);\n{0}", _Indent);
					_Output.Write ("	}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					foreach  (Structure Structure in Protocol.Structures) {
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// MakeCSerialize
		//
		public void MakeCSerialize (List<_Choice> Members) {
			foreach  (_Choice Member in Members) {
				MakeCSerialize (Member);
				}
			}
		

		//
		// MakeCSerialize
		//
		public void MakeCSerialize (_Choice Member) {
			 string Pointer = Member.ByValue? "" : "&";
			 string Required = Member.Required ? "TRUE" : "FALSE";
			if (  Member.Multiple ) {
				_Output.Write ("	JSON_Serialize_Group		(Context, \"{1}\", &Data->{2}, {3}, {4}, &Comma);\n{0}", _Indent, Member.ID, Member.ID, Required, Member.DefaultC);
				} else {
				switch (Member._Tag ()) {
					case ProtoStructType.Struct: {
					  Struct Cast = (Struct) Member; 
					if (  Member.Required ) {
						_Output.Write ("	JSON_Serialize_Tag (Context, \"{1}\", &Comma);\n{0}", _Indent, Member.ID);
						_Output.Write ("	{1}_Serialize_{2}		(Context,  {3}Data->{4});\n{0}", _Indent, Prefix, Cast.Type, Pointer, Member.ID);
						} else {
						_Output.Write ("	if ({1}Data->{2} != NULL) {{\n{0}", _Indent, Pointer, Member.ID);
						_Output.Write ("		JSON_Serialize_Tag (Context, \"{1}\", &Comma);\n{0}", _Indent, Member.ID);
						_Output.Write ("		{1}_Serialize_{2}		(Context,  {3}Data->{4});\n{0}", _Indent, Prefix, Cast.Type, Pointer, Member.ID);
						_Output.Write ("	}}\n{0}", _Indent);
						}
					break; }
					default : {
					if (  (Member.TypeC != null) ) {
						_Output.Write ("	JSON_Serialize_{1}		(Context, \"{2}\", {3}Data->{4}, {5}, {6}, &Comma);\n{0}", _Indent, Member.TypeJ, Member.ID, Pointer, Member.ID, Required, Member.DefaultC);
						}
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// MakeCDeserialize
		//
		public void MakeCDeserialize (List<_Choice> Members) {
			 Int32 Comma = 0;
			foreach  (_Choice Member in Members) {
				MakeComma (Comma);
				MakeCDeserialize (Member);
				}
			}
		

		//
		// MakeCDeserialize
		//
		public void MakeCDeserialize (_Choice Member) {
			if (  Member.ID != null ) {
				switch (Member._Tag ()) {
					case ProtoStructType.Struct: {
					  Struct Cast = (Struct) Member; 
					_Output.Write ("	{{\"{1}\", sizeof(\"{2}\")-1, OFFSETOF ({3}_{4}, {5}), ", _Indent, Member.ID, Member.ID, Prefix, XStructure, Member.ID);
					_Output.Write ("	{1}_TYPE_{2}, {3}, {4}, NULL, NULL}},\n{0}", _Indent, Prefix, Cast.Type, Member.RequiredC, Member.DefaultC);
					break; }
					default : {
					_Output.Write ("	{{\"{1}\", sizeof(\"{2}\")-1, OFFSETOF ({3}_{4}, {5}), ", _Indent, Member.ID, Member.ID, Prefix, XStructure, Member.ID);
					_Output.Write ("	{1}_TYPE_{2}, {3}, {4}, NULL, NULL}},\n{0}", _Indent, Prefix, Member.TypeJ, Member.RequiredC, Member.DefaultC);
				break; }
					}
				}
			}
		

		//
		// MakeComma
		//
		public void MakeComma (Object OCount) {
			 Int32 Count = (Int32) OCount;
			if (  Count >0 ) {
				_Output.Write (",", _Indent);
				}
			}
		}
	}
