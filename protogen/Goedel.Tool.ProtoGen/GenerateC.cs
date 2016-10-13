// #script 1.0 
// Script Syntax Version:  1.0
// #license MITLicense 

//  Unknown by Unknown
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
// #xclass Goedel.Tool.ProtoGen Generate 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.ProtoGen {
	public partial class Generate : global::Goedel.Registry.Script {

		// #% string Prefix;  
		 string Prefix; 
		// #% string LibPrefix = "JSON";  
		 string LibPrefix = "JSON"; 
		// #% string XStructure = null; 
		 string XStructure = null;
		// #method GenerateH ProtoStruct ProtoStruct 
		

		//
		// GenerateH
		//
		public void GenerateH (ProtoStruct ProtoStruct) {
			//  
			_Output.Write ("\n{0}", _Indent);
			// ##pragma once 
			_Output.Write ("#pragma once\n{0}", _Indent);
			// // 
			_Output.Write ("//\n{0}", _Indent);
			// // Code generated automatically 
			_Output.Write ("// Code generated automatically\n{0}", _Indent);
			// // 
			_Output.Write ("//\n{0}", _Indent);
			// // Really Do not edit! 
			_Output.Write ("// Really Do not edit!\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// //  
			_Output.Write ("// \n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Item in ProtoStruct.Top) 
			foreach  (var Item in ProtoStruct.Top) {
				// #% Item.Normalize (); 
				 Item.Normalize ();
				// #switchcast ProtoStructType Item 
				switch (Item._Tag ()) {
					// #casecast Protocol Protocol 
					case ProtoStructType.Protocol: {
					  Protocol Protocol = (Protocol) Item; 
					// #% Prefix = Protocol.Prefix.ToString (); 
					
					 Prefix = Protocol.Prefix.ToString ();
					// #% string MacroID = "_HEADER_" + Protocol.Id.ToString().Replace ('.', '_'); 
					
					 string MacroID = "_HEADER_" + Protocol.Id.ToString().Replace ('.', '_');
					//  
					_Output.Write ("\n{0}", _Indent);
					// ##ifndef #{MacroID} 
					_Output.Write ("#ifndef {1}\n{0}", _Indent, MacroID);
					// ##define #{MacroID} 1 
					_Output.Write ("#define {1} 1\n{0}", _Indent, MacroID);
					//  
					_Output.Write ("\n{0}", _Indent);
					// // Protocol:	#{Protocol.Id} 
					_Output.Write ("// Protocol:	{1}\n{0}", _Indent, Protocol.Id);
					// // Prefix:		#{Prefix} 
					_Output.Write ("// Prefix:		{1}\n{0}", _Indent, Prefix);
					//  
					_Output.Write ("\n{0}", _Indent);
					// ##include <protogenlib\common.h> 
					_Output.Write ("#include <protogenlib\\common.h>\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Structure Structure in Protocol.Structures) 
					foreach  (Structure Structure in Protocol.Structures) {
						// // Structure #{Structure.Id} 
						_Output.Write ("// Structure {1}\n{0}", _Indent, Structure.Id);
						//  
						_Output.Write ("\n{0}", _Indent);
						// typedef struct _#{Prefix}_#{Structure.Id} { 
						_Output.Write ("typedef struct _{1}_{2} {{\n{0}", _Indent, Prefix, Structure.Id);
						// 	struct _#{Prefix}_#{Structure.Id}		*_Next; 
						_Output.Write ("	struct _{1}_{2}		*_Next;\n{0}", _Indent, Prefix, Structure.Id);
						// 	int								_Type; 
						_Output.Write ("	int								_Type;\n{0}", _Indent);
						// 	struct _JSON_String				_String;		// Maybe only include if declared to be a FORMAT? 
						_Output.Write ("	struct _JSON_String				_String;		// Maybe only include if declared to be a FORMAT?\n{0}", _Indent);
						// #call MakeCStructure Structure.AllEntries 
						MakeCStructure (Structure.AllEntries);
						// 	} #{Prefix}_#{Structure.Id}; 
						_Output.Write ("	}} {1}_{2};\n{0}", _Indent, Prefix, Structure.Id);
						//  
						_Output.Write ("\n{0}", _Indent);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// // Type tag values 
					_Output.Write ("// Type tag values\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #% int count = 0; 
					
					 int count = 0;
					// ##define #{Prefix}_TYPE_String  #{count++} 
					_Output.Write ("#define {1}_TYPE_String  {2}\n{0}", _Indent, Prefix, count++);
					// ##define #{Prefix}_TYPE_Binary  #{count++} 
					_Output.Write ("#define {1}_TYPE_Binary  {2}\n{0}", _Indent, Prefix, count++);
					// ##define #{Prefix}_TYPE_Int64  #{count++} 
					_Output.Write ("#define {1}_TYPE_Int64  {2}\n{0}", _Indent, Prefix, count++);
					// ##define #{Prefix}_TYPE_Decimal64  #{count++} 
					_Output.Write ("#define {1}_TYPE_Decimal64  {2}\n{0}", _Indent, Prefix, count++);
					// ##define #{Prefix}_TYPE_Real64  #{count++} 
					_Output.Write ("#define {1}_TYPE_Real64  {2}\n{0}", _Indent, Prefix, count++);
					// ##define #{Prefix}_TYPE_Boolean  #{count++} 
					_Output.Write ("#define {1}_TYPE_Boolean  {2}\n{0}", _Indent, Prefix, count++);
					// ##define #{Prefix}_TYPE_DateTime  #{count++} 
					_Output.Write ("#define {1}_TYPE_DateTime  {2}\n{0}", _Indent, Prefix, count++);
					// ##define #{Prefix}_TYPE_URI  #{count++} 
					_Output.Write ("#define {1}_TYPE_URI  {2}\n{0}", _Indent, Prefix, count++);
					// ##define #{Prefix}_TYPE_Format  #{count++} 
					_Output.Write ("#define {1}_TYPE_Format  {2}\n{0}", _Indent, Prefix, count++);
					// #foreach (Structure Structure in Protocol.Structures) 
					foreach  (Structure Structure in Protocol.Structures) {
						// ##define #{Prefix}_TYPE_#{Structure.Id}  #{count++} 
						_Output.Write ("#define {1}_TYPE_{2}  {3}\n{0}", _Indent, Prefix, Structure.Id, count++);
						// #end foreach 
						}
					// ##define #{Prefix}_TYPE__Count #{count++} 
					_Output.Write ("#define {1}_TYPE__Count {2}\n{0}", _Indent, Prefix, count++);
					//  
					_Output.Write ("\n{0}", _Indent);
					// // Function Prototype Definitions.. 
					_Output.Write ("// Function Prototype Definitions..\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Structure Structure in Protocol.Structures) 
					foreach  (Structure Structure in Protocol.Structures) {
						// // Structure #{Structure.Id} 
						_Output.Write ("// Structure {1}\n{0}", _Indent, Structure.Id);
						// int #{Prefix}_Serialize_#{Structure.Id} ( 
						_Output.Write ("int {1}_Serialize_{2} (\n{0}", _Indent, Prefix, Structure.Id);
						// 		#{LibPrefix}_Context *Context, #{Prefix}_#{Structure.Id} *Data); 
						_Output.Write ("		{1}_Context *Context, {2}_{3} *Data);\n{0}", _Indent, LibPrefix, Prefix, Structure.Id);
						// int #{Prefix}_Deserialize_#{Structure.Id} ( 
						_Output.Write ("int {1}_Deserialize_{2} (\n{0}", _Indent, Prefix, Structure.Id);
						// 		#{LibPrefix}_Context *Context, #{Prefix}_#{Structure.Id} **Data); 
						_Output.Write ("		{1}_Context *Context, {2}_{3} **Data);\n{0}", _Indent, LibPrefix, Prefix, Structure.Id);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// PUBLIC_ENTRY #{Prefix}__Initialize (); 
					_Output.Write ("PUBLIC_ENTRY {1}__Initialize ();\n{0}", _Indent, Prefix);
					// PUBLIC_ENTRY #{Prefix}_Initialize (); 
					_Output.Write ("PUBLIC_ENTRY {1}_Initialize ();\n{0}", _Indent, Prefix);
					// PUBLIC_ENTRY #{Prefix}__End (); 
					_Output.Write ("PUBLIC_ENTRY {1}__End ();\n{0}", _Indent, Prefix);
					// PUBLIC_ENTRY #{Prefix}_End (); 
					_Output.Write ("PUBLIC_ENTRY {1}_End ();\n{0}", _Indent, Prefix);
					//  
					_Output.Write ("\n{0}", _Indent);
					// ##define #{Prefix}_Initialize #{Prefix}__Initialize 
					_Output.Write ("#define {1}_Initialize {2}__Initialize\n{0}", _Indent, Prefix, Prefix);
					// ##define #{Prefix}_End #{Prefix}__End 
					_Output.Write ("#define {1}_End {2}__End\n{0}", _Indent, Prefix, Prefix);
					//  
					_Output.Write ("\n{0}", _Indent);
					// PUBLIC_ENTRY #{Prefix}_Create (void *parent, long type, void **Result_Out); 
					_Output.Write ("PUBLIC_ENTRY {1}_Create (void *parent, long type, void **Result_Out);\n{0}", _Indent, Prefix);
					// PUBLIC_ENTRY #{Prefix}_Create_Context (JSON_Stream *Stream, JSON_Context **Result_Out); 
					_Output.Write ("PUBLIC_ENTRY {1}_Create_Context (JSON_Stream *Stream, JSON_Context **Result_Out);\n{0}", _Indent, Prefix);
					// PUBLIC_ENTRY #{Prefix}_Free (void *item); 
					_Output.Write ("PUBLIC_ENTRY {1}_Free (void *item);\n{0}", _Indent, Prefix);
					// PUBLIC_ENTRY #{Prefix}_Deserialize_List (JSON_Context *Context, void **Data_out) ; 
					_Output.Write ("PUBLIC_ENTRY {1}_Deserialize_List (JSON_Context *Context, void **Data_out) ;\n{0}", _Indent, Prefix);
					// PUBLIC_ENTRY #{Prefix}_Deserialize (JSON_Context *Context, void **Data_out) ; 
					_Output.Write ("PUBLIC_ENTRY {1}_Deserialize (JSON_Context *Context, void **Data_out) ;\n{0}", _Indent, Prefix);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Structure Structure in Protocol.Structures) 
					foreach  (Structure Structure in Protocol.Structures) {
						// PUBLIC_ENTRY #{Prefix}_Create_#{Structure.Id} (void *parent, #{Prefix}_#{Structure.Id} **Result_Out) ; 
						_Output.Write ("PUBLIC_ENTRY {1}_Create_{2} (void *parent, {3}_{4} **Result_Out) ;\n{0}", _Indent, Prefix, Structure.Id, Prefix, Structure.Id);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// ##endif // #{MacroID} 
					_Output.Write ("#endif // {1}\n{0}", _Indent, MacroID);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method MakeCStructure List<_Choice> Members 
		

		//
		// MakeCStructure
		//
		public void MakeCStructure (List<_Choice> Members) {
			// #foreach (_Choice Member in Members) 
			foreach  (_Choice Member in Members) {
				// #call MakeCStructure Member 
				MakeCStructure (Member);
				// #end foreach 
				}
			// #end method 
			}
		//  
		// #method MakeCStructure _Choice Member 
		

		//
		// MakeCStructure
		//
		public void MakeCStructure (_Choice Member) {
			// #if Member.Multiple 
			if (  Member.Multiple ) {
				// 	JSON_Group				#{Member.ID}; 
				_Output.Write ("	JSON_Group				{1};\n{0}", _Indent, Member.ID);
				// #else  
				} else {
				// #switchcast ProtoStructType Member 
				switch (Member._Tag ()) {
					// #casecast Struct Cast 
					case ProtoStructType.Struct: {
					  Struct Cast = (Struct) Member; 
					// 	struct _#{Prefix}_#{Cast.Type}		*#{Member.ID}; 
					_Output.Write ("	struct _{1}_{2}		*{3};\n{0}", _Indent, Prefix, Cast.Type, Member.ID);
					// #default 
					break; }
					default : {
					// #if (Member.TypeC != null) 
					if (  (Member.TypeC != null) ) {
						// 	#{Member.TypeC}			#{Member.ID}; 
						_Output.Write ("	{1}			{2};\n{0}", _Indent, Member.TypeC, Member.ID);
						// #end if 
						}
					// #end switchcast 
				break; }
					}
				// #end if 
				}
			// #end method 
			}
		//  
		// #method GenerateC ProtoStruct ProtoStruct 
		

		//
		// GenerateC
		//
		public void GenerateC (ProtoStruct ProtoStruct) {
			// #call GenerateH ProtoStruct 
			GenerateH (ProtoStruct);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (_Choice Item in ProtoStruct.Top) 
			foreach  (_Choice Item in ProtoStruct.Top) {
				// #% Item.Normalize (); 
				 Item.Normalize ();
				// #switchcast ProtoStructType Item 
				switch (Item._Tag ()) {
					// #casecast Protocol Protocol 
					case ProtoStructType.Protocol: {
					  Protocol Protocol = (Protocol) Item; 
					// #% Prefix = Protocol.Prefix.ToString (); 
					
					 Prefix = Protocol.Prefix.ToString ();
					//  
					_Output.Write ("\n{0}", _Indent);
					// // Protocol:	#{Protocol.Id} 
					_Output.Write ("// Protocol:	{1}\n{0}", _Indent, Protocol.Id);
					// // Prefix:		#{Prefix} 
					_Output.Write ("// Prefix:		{1}\n{0}", _Indent, Prefix);
					//  
					_Output.Write ("\n{0}", _Indent);
					// // Metadata Tables 
					_Output.Write ("// Metadata Tables\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Structure Structure in Protocol.Structures) 
					foreach  (Structure Structure in Protocol.Structures) {
						// #% XStructure = Structure.ID; 
						 XStructure = Structure.ID;
						// #if (Structure.CountChildren > 0) 
						if (  (Structure.CountChildren > 0) ) {
							// JSON_Parse _#{Prefix}_Meta_#{Structure.Id} [] = { 
							_Output.Write ("JSON_Parse _{1}_Meta_{2} [] = {{\n{0}", _Indent, Prefix, Structure.Id);
							// #call MakeCDeserialize Structure.AllEntriesUnsorted 
							MakeCDeserialize (Structure.AllEntriesUnsorted);
							// 	} ; 
							_Output.Write ("	}} ;\n{0}", _Indent);
							// ##define _#{Prefix}_Meta_#{Structure.Id}_C COUNT (_#{Prefix}_Meta_#{Structure.Id}) 
							_Output.Write ("#define _{1}_Meta_{2}_C COUNT (_{3}_Meta_{4})\n{0}", _Indent, Prefix, Structure.Id, Prefix, Structure.Id);
							// #else 
							} else {
							// JSON_Parse *_#{Prefix}_Meta_#{Structure.Id} = NULL; 
							_Output.Write ("JSON_Parse *_{1}_Meta_{2} = NULL;\n{0}", _Indent, Prefix, Structure.Id);
							// ##define _#{Prefix}_Meta_#{Structure.Id}_C 0 
							_Output.Write ("#define _{1}_Meta_{2}_C 0\n{0}", _Indent, Prefix, Structure.Id);
							// #end if 
							}
						//  
						_Output.Write ("\n{0}", _Indent);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// static JSON_Registry _#{Prefix}_Meta [] = { 
					_Output.Write ("static JSON_Registry _{1}_Meta [] = {{\n{0}", _Indent, Prefix);
					// 	{"String", sizeof("String")-1}, 
					_Output.Write ("	{{\"String\", sizeof(\"String\")-1}},\n{0}", _Indent);
					// 	{"Binary", sizeof("Binary")-1}, 
					_Output.Write ("	{{\"Binary\", sizeof(\"Binary\")-1}},\n{0}", _Indent);
					// 	{"Int64", sizeof("Int64")-1}, 
					_Output.Write ("	{{\"Int64\", sizeof(\"Int64\")-1}},\n{0}", _Indent);
					// 	{"Decimal64", sizeof("Decimal64")-1}, 
					_Output.Write ("	{{\"Decimal64\", sizeof(\"Decimal64\")-1}},\n{0}", _Indent);
					// 	{"Real64", sizeof("Real64")-1}, 
					_Output.Write ("	{{\"Real64\", sizeof(\"Real64\")-1}},\n{0}", _Indent);
					// 	{"Boolean", sizeof("Boolean")-1}, 
					_Output.Write ("	{{\"Boolean\", sizeof(\"Boolean\")-1}},\n{0}", _Indent);
					// 	{"DateTime", sizeof("DateTime")-1},	 
					_Output.Write ("	{{\"DateTime\", sizeof(\"DateTime\")-1}},	\n{0}", _Indent);
					// 	{"URI", sizeof("URI")-1}, 
					_Output.Write ("	{{\"URI\", sizeof(\"URI\")-1}},\n{0}", _Indent);
					// 	{"Format", sizeof("Format")-1}#! 
					_Output.Write ("	{{\"Format\", sizeof(\"Format\")-1}}", _Indent);
					// #foreach (Structure Structure in Protocol.Structures) 
					foreach  (Structure Structure in Protocol.Structures) {
						// , 
						_Output.Write (",\n{0}", _Indent);
						// 	{"#{Structure.Id}", sizeof ("#{Structure.Id}")-1, NULL, _#{Prefix}_Meta_#{Structure.Id}_C, sizeof (#{Prefix}_#{Structure.Id}),  
						_Output.Write ("	{{\"{1}\", sizeof (\"{2}\")-1, NULL, _{3}_Meta_{4}_C, sizeof ({5}_{6}), \n{0}", _Indent, Structure.Id, Structure.Id, Prefix, Structure.Id, Prefix, Structure.Id);
						// 		(JSON_Serializer) #{Prefix}_Serialize_#{Structure.Id}, (JSON_Initializer) #{Prefix}_Deserialize_#{Structure.Id}, #! 
						_Output.Write ("		(JSON_Serializer) {1}_Serialize_{2}, (JSON_Initializer) {3}_Deserialize_{4}, ", _Indent, Prefix, Structure.Id, Prefix, Structure.Id);
						// (JSON_Initializer) #{Prefix}_Create_#{Structure.Id}} #! 
						_Output.Write ("(JSON_Initializer) {1}_Create_{2}}} ", _Indent, Prefix, Structure.Id);
						// #end foreach 
						}
					// }; 
					_Output.Write ("}};\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// JSON_Registry *#{Prefix}_Meta =  _#{Prefix}_Meta; 
					_Output.Write ("JSON_Registry *{1}_Meta =  _{2}_Meta;\n{0}", _Indent, Prefix, Prefix);
					//  
					_Output.Write ("\n{0}", _Indent);
					// // Function Definitions 
					_Output.Write ("// Function Definitions\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Structure Structure in Protocol.Structures) 
					foreach  (Structure Structure in Protocol.Structures) {
						// // Structure #{Structure.Id} 
						_Output.Write ("// Structure {1}\n{0}", _Indent, Structure.Id);
						//  
						_Output.Write ("\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						// PUBLIC_ENTRY #{Prefix}_Create_#{Structure.Id} (void *parent, #{Prefix}_#{Structure.Id} **Result_Out) { 
						_Output.Write ("PUBLIC_ENTRY {1}_Create_{2} (void *parent, {3}_{4} **Result_Out) {{\n{0}", _Indent, Prefix, Structure.Id, Prefix, Structure.Id);
						// 	#{Prefix}_#{Structure.Id} *Result; 
						_Output.Write ("	{1}_{2} *Result;\n{0}", _Indent, Prefix, Structure.Id);
						// 	BEGIN; 
						_Output.Write ("	BEGIN;\n{0}", _Indent);
						// 	JSON_Create (parent, #{Prefix}_TYPE_#{Structure.Id}, _#{Prefix}_Meta, (void**) &Result); 
						_Output.Write ("	JSON_Create (parent, {1}_TYPE_{2}, _{3}_Meta, (void**) &Result);\n{0}", _Indent, Prefix, Structure.Id, Prefix);
						// 	 
						_Output.Write ("	\n{0}", _Indent);
						// #foreach (_Choice Member in Structure.AllEntries) 
						foreach  (_Choice Member in Structure.AllEntries) {
							// #if Member.Multiple	 
							if (  Member.Multiple	 ) {
								// 	Result->#{Member.ID}.Count = -1; 
								_Output.Write ("	Result->{1}.Count = -1;\n{0}", _Indent, Member.ID);
								// 	Result->#{Member.ID}.First = NULL; 
								_Output.Write ("	Result->{1}.First = NULL;\n{0}", _Indent, Member.ID);
								// 	Result->#{Member.ID}.Last = NULL; 
								_Output.Write ("	Result->{1}.Last = NULL;\n{0}", _Indent, Member.ID);
								// 	Result->#{Member.ID}.Members = NULL; 
								_Output.Write ("	Result->{1}.Members = NULL;\n{0}", _Indent, Member.ID);
								// #else  
								} else {
								// #switchcast ProtoStructType Member 
								switch (Member._Tag ()) {
									// #casecast Boolean null 
									case ProtoStructType.Boolean: { 
									// 	Result->#{Member.ID} = Boolean_NaN; 
									_Output.Write ("	Result->{1} = Boolean_NaN;\n{0}", _Indent, Member.ID);
									// #casecast Integer null 
									break; }
									case ProtoStructType.Integer: { 
									// 	Result->#{Member.ID} = Int64_NaN; 
									_Output.Write ("	Result->{1} = Int64_NaN;\n{0}", _Indent, Member.ID);
									// #casecast Decimal null 
									break; }
									case ProtoStructType.Decimal: { 
									// 	Result->#{Member.ID} = Decimal64_NaN; 
									_Output.Write ("	Result->{1} = Decimal64_NaN;\n{0}", _Indent, Member.ID);
									// #casecast Float null 
									break; }
									case ProtoStructType.Float: { 
									// 	Result->#{Member.ID} = Real64_NaN; 
									_Output.Write ("	Result->{1} = Real64_NaN;\n{0}", _Indent, Member.ID);
									// #casecast String null 
									break; }
									case ProtoStructType.String: { 
									// 	Result->#{Member.ID}.Data = NULL; 
									_Output.Write ("	Result->{1}.Data = NULL;\n{0}", _Indent, Member.ID);
									// #casecast Binary null 
									break; }
									case ProtoStructType.Binary: { 
									// 	Result->#{Member.ID}.Data = NULL; 
									_Output.Write ("	Result->{1}.Data = NULL;\n{0}", _Indent, Member.ID);
									// #casecast Struct null 
									break; }
									case ProtoStructType.Struct: { 
									// 	Result->#{Member.ID} = NULL; 
									_Output.Write ("	Result->{1} = NULL;\n{0}", _Indent, Member.ID);
									// #end switchcast 
								break; }
									}
								// #end if 
								}
							// #end foreach	 
							}
						// 	 
						_Output.Write ("	\n{0}", _Indent);
						// 	*Result_Out = Result; 
						_Output.Write ("	*Result_Out = Result;\n{0}", _Indent);
						// 	END; 
						_Output.Write ("	END;\n{0}", _Indent);
						// 	} 
						_Output.Write ("	}}\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						// int #{Prefix}_Serialize_#{Structure.Id} (#{LibPrefix}_Context *Context, #{Prefix}_#{Structure.Id} *Data) { 
						_Output.Write ("int {1}_Serialize_{2} ({3}_Context *Context, {4}_{5} *Data) {{\n{0}", _Indent, Prefix, Structure.Id, LibPrefix, Prefix, Structure.Id);
						// 	boolean Comma = false; 
						_Output.Write ("	boolean Comma = false;\n{0}", _Indent);
						// 	BEGIN; 
						_Output.Write ("	BEGIN;\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						// 	if (Data == NULL) { 
						_Output.Write ("	if (Data == NULL) {{\n{0}", _Indent);
						// 		JSON_Stream_Write_Null (Context->Stream); 
						_Output.Write ("		JSON_Stream_Write_Null (Context->Stream);\n{0}", _Indent);
						// 		} 
						_Output.Write ("		}}\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						// 	#{LibPrefix}_Begin (Context); 
						_Output.Write ("	{1}_Begin (Context);\n{0}", _Indent, LibPrefix);
						// #call MakeCSerialize Structure.AllEntries 
						MakeCSerialize (Structure.AllEntries);
						// 	#{LibPrefix}_End (Context); 
						_Output.Write ("	{1}_End (Context);\n{0}", _Indent, LibPrefix);
						//  
						_Output.Write ("\n{0}", _Indent);
						// 	END; 
						_Output.Write ("	END;\n{0}", _Indent);
						// 	} 
						_Output.Write ("	}}\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						// int #{Prefix}_Deserialize_#{Structure.Id} ( 
						_Output.Write ("int {1}_Deserialize_{2} (\n{0}", _Indent, Prefix, Structure.Id);
						// 		#{LibPrefix}_Context *Context, #{Prefix}_#{Structure.Id} **Data) { 
						_Output.Write ("		{1}_Context *Context, {2}_{3} **Data) {{\n{0}", _Indent, LibPrefix, Prefix, Structure.Id);
						//  
						_Output.Write ("\n{0}", _Indent);
						// 	BEGIN; 
						_Output.Write ("	BEGIN;\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						// 	#{Prefix}_Create_#{Structure.Id} (Context, Data); 
						_Output.Write ("	{1}_Create_{2} (Context, Data);\n{0}", _Indent, Prefix, Structure.Id);
						//  
						_Output.Write ("\n{0}", _Indent);
						// 	return JSON_Deserialize (Context, _#{Prefix}_Meta + #{Prefix}_TYPE_#{Structure.Id}, *Data); 
						_Output.Write ("	return JSON_Deserialize (Context, _{1}_Meta + {2}_TYPE_{3}, *Data);\n{0}", _Indent, Prefix, Prefix, Structure.Id);
						// 	 
						_Output.Write ("	\n{0}", _Indent);
						// 	END; 
						_Output.Write ("	END;\n{0}", _Indent);
						// 	} 
						_Output.Write ("	}}\n{0}", _Indent);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// // Totally lame that this is necessary but it is. 
					_Output.Write ("// Totally lame that this is necessary but it is.\n{0}", _Indent);
					// // 
					_Output.Write ("//\n{0}", _Indent);
					// // C requires initializers be literal constants, it is not enough to declare 
					_Output.Write ("// C requires initializers be literal constants, it is not enough to declare\n{0}", _Indent);
					// // the variable to be of type const 
					_Output.Write ("// the variable to be of type const\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// PUBLIC_ENTRY #{Prefix}__Initialize () { 
					_Output.Write ("PUBLIC_ENTRY {1}__Initialize () {{\n{0}", _Indent, Prefix);
					// 	BEGIN; 
					_Output.Write ("	BEGIN;\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	JSON_Initialize (); 
					_Output.Write ("	JSON_Initialize ();\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Structure Structure in Protocol.Structures) 
					foreach  (Structure Structure in Protocol.Structures) {
						// 	_#{Prefix}_Meta [#{Prefix}_TYPE_#{Structure.Id}].Table = _#{Prefix}_Meta_#{Structure.Id}; 
						_Output.Write ("	_{1}_Meta [{2}_TYPE_{3}].Table = _{4}_Meta_{5};\n{0}", _Indent, Prefix, Prefix, Structure.Id, Prefix, Structure.Id);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	END; 
					_Output.Write ("	END;\n{0}", _Indent);
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// PUBLIC_ENTRY #{Prefix}__End () { 
					_Output.Write ("PUBLIC_ENTRY {1}__End () {{\n{0}", _Indent, Prefix);
					// 	BEGIN; 
					_Output.Write ("	BEGIN;\n{0}", _Indent);
					// 	END; 
					_Output.Write ("	END;\n{0}", _Indent);
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// PUBLIC_ENTRY #{Prefix}_Create (void *parent, long type, void **Result_Out) { 
					_Output.Write ("PUBLIC_ENTRY {1}_Create (void *parent, long type, void **Result_Out) {{\n{0}", _Indent, Prefix);
					// 	return JSON_Create (parent, type, _#{Prefix}_Meta, Result_Out); 
					_Output.Write ("	return JSON_Create (parent, type, _{1}_Meta, Result_Out);\n{0}", _Indent, Prefix);
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// PUBLIC_ENTRY #{Prefix}_Deserialize_List (JSON_Context *Context, void **Data_out) { 
					_Output.Write ("PUBLIC_ENTRY {1}_Deserialize_List (JSON_Context *Context, void **Data_out) {{\n{0}", _Indent, Prefix);
					// 	return JSON_Deserialize_Object (Context, Data_out, FALSE); 
					_Output.Write ("	return JSON_Deserialize_Object (Context, Data_out, FALSE);\n{0}", _Indent);
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					// PUBLIC_ENTRY #{Prefix}_Deserialize (JSON_Context *Context, void **Data_out) { 
					_Output.Write ("PUBLIC_ENTRY {1}_Deserialize (JSON_Context *Context, void **Data_out) {{\n{0}", _Indent, Prefix);
					// 	return JSON_Deserialize_Object (Context, Data_out, TRUE); 
					_Output.Write ("	return JSON_Deserialize_Object (Context, Data_out, TRUE);\n{0}", _Indent);
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// PUBLIC_ENTRY #{Prefix}_Create_Context (JSON_Stream *Stream, JSON_Context **Result_Out) { 
					_Output.Write ("PUBLIC_ENTRY {1}_Create_Context (JSON_Stream *Stream, JSON_Context **Result_Out) {{\n{0}", _Indent, Prefix);
					// 	JSON_Context		*Result; 
					_Output.Write ("	JSON_Context		*Result;\n{0}", _Indent);
					// 	BEGIN; 
					_Output.Write ("	BEGIN;\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	JSON_Create_Context (Stream, &Result); 
					_Output.Write ("	JSON_Create_Context (Stream, &Result);\n{0}", _Indent);
					// 	Result->Registry = _#{Prefix}_Meta; 
					_Output.Write ("	Result->Registry = _{1}_Meta;\n{0}", _Indent, Prefix);
					// 	Result->Count = COUNT(_#{Prefix}_Meta); 
					_Output.Write ("	Result->Count = COUNT(_{1}_Meta);\n{0}", _Indent, Prefix);
					// 	*Result_Out = Result; 
					_Output.Write ("	*Result_Out = Result;\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// 	END; 
					_Output.Write ("	END;\n{0}", _Indent);
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// PUBLIC_ENTRY #{Prefix}_Free (void *item) { 
					_Output.Write ("PUBLIC_ENTRY {1}_Free (void *item) {{\n{0}", _Indent, Prefix);
					// 	 
					_Output.Write ("	\n{0}", _Indent);
					// 	return JSON_Free (item); 
					_Output.Write ("	return JSON_Free (item);\n{0}", _Indent);
					// 	} 
					_Output.Write ("	}}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (Structure Structure in Protocol.Structures) 
					foreach  (Structure Structure in Protocol.Structures) {
						//  
						_Output.Write ("\n{0}", _Indent);
						//  
						_Output.Write ("\n{0}", _Indent);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method MakeCSerialize List<_Choice> Members 
		

		//
		// MakeCSerialize
		//
		public void MakeCSerialize (List<_Choice> Members) {
			// #foreach (_Choice Member in Members) 
			foreach  (_Choice Member in Members) {
				// #call MakeCSerialize Member 
				MakeCSerialize (Member);
				// #end foreach 
				}
			// #end method 
			}
		//  
		// #method MakeCSerialize _Choice Member 
		

		//
		// MakeCSerialize
		//
		public void MakeCSerialize (_Choice Member) {
			// #% string Pointer = Member.ByValue? "" : "&"; 
			 string Pointer = Member.ByValue? "" : "&";
			// #% string Required = Member.Required ? "TRUE" : "FALSE"; 
			 string Required = Member.Required ? "TRUE" : "FALSE";
			// #if Member.Multiple 
			if (  Member.Multiple ) {
				// 	JSON_Serialize_Group		(Context, "#{Member.ID}", &Data->#{Member.ID}, #{Required}, #{Member.DefaultC}, &Comma); 
				_Output.Write ("	JSON_Serialize_Group		(Context, \"{1}\", &Data->{2}, {3}, {4}, &Comma);\n{0}", _Indent, Member.ID, Member.ID, Required, Member.DefaultC);
				// #else  
				} else {
				// #switchcast ProtoStructType Member 
				switch (Member._Tag ()) {
					// #casecast Struct Cast 
					case ProtoStructType.Struct: {
					  Struct Cast = (Struct) Member; 
					// #if Member.Required 
					if (  Member.Required ) {
						// 	JSON_Serialize_Tag (Context, "#{Member.ID}", &Comma); 
						_Output.Write ("	JSON_Serialize_Tag (Context, \"{1}\", &Comma);\n{0}", _Indent, Member.ID);
						// 	#{Prefix}_Serialize_#{Cast.Type}		(Context,  #{Pointer}Data->#{Member.ID}); 
						_Output.Write ("	{1}_Serialize_{2}		(Context,  {3}Data->{4});\n{0}", _Indent, Prefix, Cast.Type, Pointer, Member.ID);
						// #else  
						} else {
						// 	if (#{Pointer}Data->#{Member.ID} != NULL) { 
						_Output.Write ("	if ({1}Data->{2} != NULL) {{\n{0}", _Indent, Pointer, Member.ID);
						// 		JSON_Serialize_Tag (Context, "#{Member.ID}", &Comma); 
						_Output.Write ("		JSON_Serialize_Tag (Context, \"{1}\", &Comma);\n{0}", _Indent, Member.ID);
						// 		#{Prefix}_Serialize_#{Cast.Type}		(Context,  #{Pointer}Data->#{Member.ID}); 
						_Output.Write ("		{1}_Serialize_{2}		(Context,  {3}Data->{4});\n{0}", _Indent, Prefix, Cast.Type, Pointer, Member.ID);
						// 	} 
						_Output.Write ("	}}\n{0}", _Indent);
						// #end if 
						}
					// #default 
					break; }
					default : {
					// #if (Member.TypeC != null) 
					if (  (Member.TypeC != null) ) {
						// 	JSON_Serialize_#{Member.TypeJ}		(Context, "#{Member.ID}", #{Pointer}Data->#{Member.ID}, #{Required}, #{Member.DefaultC}, &Comma); 
						_Output.Write ("	JSON_Serialize_{1}		(Context, \"{2}\", {3}Data->{4}, {5}, {6}, &Comma);\n{0}", _Indent, Member.TypeJ, Member.ID, Pointer, Member.ID, Required, Member.DefaultC);
						// #end if 
						}
					// #end switchcast 
				break; }
					}
				// #end if 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method MakeCDeserialize List<_Choice> Members 
		

		//
		// MakeCDeserialize
		//
		public void MakeCDeserialize (List<_Choice> Members) {
			// #% Int32 Comma = 0; 
			 Int32 Comma = 0;
			// #foreach (_Choice Member in Members) 
			foreach  (_Choice Member in Members) {
				// #call MakeComma Comma 
				MakeComma (Comma);
				// #call MakeCDeserialize Member 
				MakeCDeserialize (Member);
				// #end foreach 
				}
			// #end method 
			}
		//  
		// #method MakeCDeserialize _Choice Member 
		

		//
		// MakeCDeserialize
		//
		public void MakeCDeserialize (_Choice Member) {
			// #if Member.ID != null 
			if (  Member.ID != null ) {
				// #switchcast ProtoStructType Member 
				switch (Member._Tag ()) {
					// #casecast Struct Cast 
					case ProtoStructType.Struct: {
					  Struct Cast = (Struct) Member; 
					// 	{"#{Member.ID}", sizeof("#{Member.ID}")-1, OFFSETOF (#{Prefix}_#{XStructure}, #{Member.ID}), #! 
					_Output.Write ("	{{\"{1}\", sizeof(\"{2}\")-1, OFFSETOF ({3}_{4}, {5}), ", _Indent, Member.ID, Member.ID, Prefix, XStructure, Member.ID);
					// 	#{Prefix}_TYPE_#{Cast.Type}, #{Member.RequiredC}, #{Member.DefaultC}, NULL, NULL}, 
					_Output.Write ("	{1}_TYPE_{2}, {3}, {4}, NULL, NULL}},\n{0}", _Indent, Prefix, Cast.Type, Member.RequiredC, Member.DefaultC);
					// #default 
					break; }
					default : {
					// 	{"#{Member.ID}", sizeof("#{Member.ID}")-1, OFFSETOF (#{Prefix}_#{XStructure}, #{Member.ID}), #! 
					_Output.Write ("	{{\"{1}\", sizeof(\"{2}\")-1, OFFSETOF ({3}_{4}, {5}), ", _Indent, Member.ID, Member.ID, Prefix, XStructure, Member.ID);
					// 	#{Prefix}_TYPE_#{Member.TypeJ}, #{Member.RequiredC}, #{Member.DefaultC}, NULL, NULL}, 
					_Output.Write ("	{1}_TYPE_{2}, {3}, {4}, NULL, NULL}},\n{0}", _Indent, Prefix, Member.TypeJ, Member.RequiredC, Member.DefaultC);
					// #end switchcast 
				break; }
					}
				// #end if 
				}
			// #end method 
			}
		//  
		//  
		// #method MakeComma Object OCount 
		

		//
		// MakeComma
		//
		public void MakeComma (Object OCount) {
			// #% Int32 Count = (Int32) OCount; 
			 Int32 Count = (Int32) OCount;
			// #if Count >0 
			if (  Count >0 ) {
				// ,#! 
				_Output.Write (",", _Indent);
				// #end if 
				}
			// #%OCount = Count + 1; 
			// #end method 
			}
		//  
		// #end xclass 
		}
	}
