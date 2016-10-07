// #using System.Text 
using  System.Text;
// #pclass Goedel.Registry Boilerplate 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Registry {
	public partial class Boilerplate : global::Goedel.Registry.Script {
		public Boilerplate () : base () {
			}
		public Boilerplate (TextWriter Output) : base (Output) {
			}

		// #block License 
		

		//
		//  License
		//

			// #%	public static void License (TextWriter _Output, string Comment, string Name) { 
				public static void License (TextWriter _Output, string Comment, string Name) {
			// #%		string Copyright	=	Script.AssemblyCopyright; 
					string Copyright	=	Script.AssemblyCopyright;
			// #%		string Holder		=	Script.AssemblyCompany; 
					string Holder		=	Script.AssemblyCompany;
			// #%		switch (Name) { 
					switch (Name) {
			// #%			case "MITLicense" : {  MITLicense (_Output, Comment, Copyright, Holder); break;} 
						case "MITLicense" : {  MITLicense (_Output, Comment, Copyright, Holder); break;}
			// #%			case "BSD3License" : {  BSD3License (_Output, Comment, Copyright, Holder); break;} 
						case "BSD3License" : {  BSD3License (_Output, Comment, Copyright, Holder); break;}
			// #%			case "BSD2License" : {  BSD2License (_Output, Comment, Copyright, Holder); break;} 
						case "BSD2License" : {  BSD2License (_Output, Comment, Copyright, Holder); break;}
			// #%			case "Apache2License" : { Apache2License (_Output, Comment, Copyright, Holder); break;} 
						case "Apache2License" : { Apache2License (_Output, Comment, Copyright, Holder); break;}
			// #%			case "ISCLicense" : { ISCLicense (_Output, Comment, Copyright, Holder); break;} 
						case "ISCLicense" : { ISCLicense (_Output, Comment, Copyright, Holder); break;}
			// #%			default : break; 
						default : break;
			// #%			} 
						}
			// #%		} 
					}
			// #end block 
		
		//  
		// #block Header 
		

		//
		//  Header
		//

			// #% public static void Header (TextWriter _Output, string Comment, DateTime GenerateTime) { 
			 public static void Header (TextWriter _Output, string Comment, DateTime GenerateTime) {
			// #% string _Indent = ""; 
			 string _Indent = "";
			// #prefix Comment 
			_Indent =  Comment + _Indent; {
				//  
				_Output.Write ("\n{0}", _Indent);
				// This file was automatically generated at #{GenerateTime.ToLocalTime()} 
				_Output.Write ("This file was automatically generated at {1}\n{0}", _Indent, GenerateTime.ToLocalTime());
				//   
				_Output.Write (" \n{0}", _Indent);
				// Changes to this file may be overwritten without warning 
				_Output.Write ("Changes to this file may be overwritten without warning\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// Generator:  #{Script.AssemblyTitle} version #{Script.AssemblyVersion} 
				_Output.Write ("Generator:  {1} version {2}\n{0}", _Indent, Script.AssemblyTitle, Script.AssemblyVersion);
				//     Goedel Script Version : 0.1   Generated  
				_Output.Write ("    Goedel Script Version : 0.1   Generated \n{0}", _Indent);
				//     Goedel Schema Version : 0.1   Generated 
				_Output.Write ("    Goedel Schema Version : 0.1   Generated\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//     Copyright : #{Script.AssemblyCopyright} 
				_Output.Write ("    Copyright : {1}\n{0}", _Indent, Script.AssemblyCopyright);
				//  
				_Output.Write ("\n{0}", _Indent);
				// Build Platform: #{Script.Platform} #{Script.PlatformVersion} 
				_Output.Write ("Build Platform: {1} {2}\n{0}", _Indent, Script.Platform, Script.PlatformVersion);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end prefix 
			}
			// #% } 
			 }
			// #end block 
		
		//  
		//  
		// #block MITLicense 
		

		//
		//  MITLicense
		//

			// #% public static void MITLicense (TextWriter _Output, string Comment, string Copyright, string Holder) { 
			 public static void MITLicense (TextWriter _Output, string Comment, string Copyright, string Holder) {
			// #% string _Indent = ""; 
			 string _Indent = "";
			// #prefix Comment 
			_Indent =  Comment + _Indent; {
				//  
				_Output.Write ("\n{0}", _Indent);
				// #{Copyright} by #{Holder} 
				_Output.Write ("{1} by {2}\n{0}", _Indent, Copyright, Holder);
				//  
				_Output.Write ("\n{0}", _Indent);
				// Permission is hereby granted, free of charge, to any person obtaining a copy 
				_Output.Write ("Permission is hereby granted, free of charge, to any person obtaining a copy\n{0}", _Indent);
				// of this software and associated documentation files (the "Software"), to deal 
				_Output.Write ("of this software and associated documentation files (the \"Software\"), to deal\n{0}", _Indent);
				// in the Software without restriction, including without limitation the rights 
				_Output.Write ("in the Software without restriction, including without limitation the rights\n{0}", _Indent);
				// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
				_Output.Write ("to use, copy, modify, merge, publish, distribute, sublicense, and/or sell\n{0}", _Indent);
				// copies of the Software, and to permit persons to whom the Software is 
				_Output.Write ("copies of the Software, and to permit persons to whom the Software is\n{0}", _Indent);
				// furnished to do so, subject to the following conditions: 
				_Output.Write ("furnished to do so, subject to the following conditions:\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// The above copyright notice and this permission notice shall be included in 
				_Output.Write ("The above copyright notice and this permission notice shall be included in\n{0}", _Indent);
				// all copies or substantial portions of the Software. 
				_Output.Write ("all copies or substantial portions of the Software.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
				_Output.Write ("THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\n{0}", _Indent);
				// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
				_Output.Write ("IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\n{0}", _Indent);
				// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
				_Output.Write ("FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE\n{0}", _Indent);
				// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
				_Output.Write ("AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\n{0}", _Indent);
				// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
				_Output.Write ("LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\n{0}", _Indent);
				// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
				_Output.Write ("OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN\n{0}", _Indent);
				// THE SOFTWARE. 
				_Output.Write ("THE SOFTWARE.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end prefix 
			}
			// #% } 
			 }
			// #end block 
		
		//  
		// #block 
		

		//
		// 
		//

			// #% public static void BSD3License (TextWriter _Output, string Comment, string Copyright, string Holder) { 
			 public static void BSD3License (TextWriter _Output, string Comment, string Copyright, string Holder) {
			// #% string _Indent = ""; 
			 string _Indent = "";
			// #prefix Comment 
			_Indent =  Comment + _Indent; {
				//  
				_Output.Write ("\n{0}", _Indent);
				// #{Copyright} #{Holder} 
				_Output.Write ("{1} {2}\n{0}", _Indent, Copyright, Holder);
				// All rights reserved. 
				_Output.Write ("All rights reserved.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// Redistribution and use in source and binary forms, with or without 
				_Output.Write ("Redistribution and use in source and binary forms, with or without\n{0}", _Indent);
				// modification, are permitted provided that the following conditions are met: 
				_Output.Write ("modification, are permitted provided that the following conditions are met:\n{0}", _Indent);
				//     * Redistributions of source code must retain the above copyright 
				_Output.Write ("    * Redistributions of source code must retain the above copyright\n{0}", _Indent);
				//       notice, this list of conditions and the following disclaimer. 
				_Output.Write ("      notice, this list of conditions and the following disclaimer.\n{0}", _Indent);
				//     * Redistributions in binary form must reproduce the above copyright 
				_Output.Write ("    * Redistributions in binary form must reproduce the above copyright\n{0}", _Indent);
				//       notice, this list of conditions and the following disclaimer in the 
				_Output.Write ("      notice, this list of conditions and the following disclaimer in the\n{0}", _Indent);
				//       documentation and/or other materials provided with the distribution. 
				_Output.Write ("      documentation and/or other materials provided with the distribution.\n{0}", _Indent);
				//     * Neither the name of the #{Holder} nor the 
				_Output.Write ("    * Neither the name of the {1} nor the\n{0}", _Indent, Holder);
				//       names of its contributors may be used to endorse or promote products 
				_Output.Write ("      names of its contributors may be used to endorse or promote products\n{0}", _Indent);
				//       derived from this software without specific prior written permission. 
				_Output.Write ("      derived from this software without specific prior written permission.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
				_Output.Write ("THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS \"AS IS\" AND\n{0}", _Indent);
				// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
				_Output.Write ("ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED\n{0}", _Indent);
				// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
				_Output.Write ("WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE\n{0}", _Indent);
				// DISCLAIMED. IN NO EVENT SHALL #{Holder.ToUpper()} BE LIABLE FOR ANY 
				_Output.Write ("DISCLAIMED. IN NO EVENT SHALL {1} BE LIABLE FOR ANY\n{0}", _Indent, Holder.ToUpper());
				// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
				_Output.Write ("DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES\n{0}", _Indent);
				// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
				_Output.Write ("(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;\n{0}", _Indent);
				// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND 
				_Output.Write ("LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND\n{0}", _Indent);
				// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
				_Output.Write ("ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT\n{0}", _Indent);
				// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
				_Output.Write ("(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS\n{0}", _Indent);
				// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
				_Output.Write ("SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end prefix 
			}
			// #% } 
			 }
			// #end block 
		
		//  
		//  
		// #block 
		

		//
		// 
		//

			// #% public static void BSD2License (TextWriter _Output, string Comment, string Copyright, string Holder) { 
			 public static void BSD2License (TextWriter _Output, string Comment, string Copyright, string Holder) {
			// #% string _Indent = ""; 
			 string _Indent = "";
			// #prefix Comment 
			_Indent =  Comment + _Indent; {
				//  
				_Output.Write ("\n{0}", _Indent);
				// #{Copyright} #{Holder}. All rights reserved. 
				_Output.Write ("{1} {2}. All rights reserved.\n{0}", _Indent, Copyright, Holder);
				//  
				_Output.Write ("\n{0}", _Indent);
				// Redistribution and use in source and binary forms, with or without modification, are 
				_Output.Write ("Redistribution and use in source and binary forms, with or without modification, are\n{0}", _Indent);
				// permitted provided that the following conditions are met: 
				_Output.Write ("permitted provided that the following conditions are met:\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//    1. Redistributions of source code must retain the above copyright notice, this list of 
				_Output.Write ("   1. Redistributions of source code must retain the above copyright notice, this list of\n{0}", _Indent);
				//       conditions and the following disclaimer. 
				_Output.Write ("      conditions and the following disclaimer.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//    2. Redistributions in binary form must reproduce the above copyright notice, this list 
				_Output.Write ("   2. Redistributions in binary form must reproduce the above copyright notice, this list\n{0}", _Indent);
				//       of conditions and the following disclaimer in the documentation and/or other materials 
				_Output.Write ("      of conditions and the following disclaimer in the documentation and/or other materials\n{0}", _Indent);
				//       provided with the distribution. 
				_Output.Write ("      provided with the distribution.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// THIS SOFTWARE IS PROVIDED BY #{Holder.ToUpper()} ''AS IS'' AND ANY EXPRESS OR IMPLIED 
				_Output.Write ("THIS SOFTWARE IS PROVIDED BY {1} ''AS IS'' AND ANY EXPRESS OR IMPLIED\n{0}", _Indent, Holder.ToUpper());
				// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND 
				_Output.Write ("WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND\n{0}", _Indent);
				// FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> OR 
				_Output.Write ("FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> OR\n{0}", _Indent);
				// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
				_Output.Write ("CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR\n{0}", _Indent);
				// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
				_Output.Write ("CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR\n{0}", _Indent);
				// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON 
				_Output.Write ("SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON\n{0}", _Indent);
				// ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
				_Output.Write ("ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING\n{0}", _Indent);
				// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF 
				_Output.Write ("NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF\n{0}", _Indent);
				// ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
				_Output.Write ("ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// The views and conclusions contained in the software and documentation are those of the 
				_Output.Write ("The views and conclusions contained in the software and documentation are those of the\n{0}", _Indent);
				// authors and should not be interpreted as representing official policies, either expressed 
				_Output.Write ("authors and should not be interpreted as representing official policies, either expressed\n{0}", _Indent);
				// or implied, of #{Holder}. 
				_Output.Write ("or implied, of {1}.\n{0}", _Indent, Holder);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end prefix 
			}
			// #% } 
			 }
			// #end block 
		
		//  
		//  
		// #block 
		

		//
		// 
		//

			// #% public static void Apache2License (TextWriter _Output, string Comment, string Copyright, string Holder) { 
			 public static void Apache2License (TextWriter _Output, string Comment, string Copyright, string Holder) {
			// #% string _Indent = ""; 
			 string _Indent = "";
			// #prefix Comment 
			_Indent =  Comment + _Indent; {
				//  
				_Output.Write ("\n{0}", _Indent);
				// #{Copyright} #{Holder} 
				_Output.Write ("{1} {2}\n{0}", _Indent, Copyright, Holder);
				//  
				_Output.Write ("\n{0}", _Indent);
				//    Licensed under the Apache License, Version 2.0 (the "License"); 
				_Output.Write ("   Licensed under the Apache License, Version 2.0 (the \"License\");\n{0}", _Indent);
				//    you may not use this file except in compliance with the License. 
				_Output.Write ("   you may not use this file except in compliance with the License.\n{0}", _Indent);
				//    You may obtain a copy of the License at 
				_Output.Write ("   You may obtain a copy of the License at\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//        http://www.apache.org/licenses/LICENSE-2.0 
				_Output.Write ("       http://www.apache.org/licenses/LICENSE-2.0\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//    Unless required by applicable law or agreed to in writing, software 
				_Output.Write ("   Unless required by applicable law or agreed to in writing, software\n{0}", _Indent);
				//    distributed under the License is distributed on an "AS IS" BASIS, 
				_Output.Write ("   distributed under the License is distributed on an \"AS IS\" BASIS,\n{0}", _Indent);
				//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
				_Output.Write ("   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.\n{0}", _Indent);
				//    See the License for the specific language governing permissions and 
				_Output.Write ("   See the License for the specific language governing permissions and\n{0}", _Indent);
				//    limitations under the License. 
				_Output.Write ("   limitations under the License.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end prefix 
			}
			// #% } 
			 }
			// #end block 
		
		//  
		// #block 
		

		//
		// 
		//

			// #% public static void ISCLicense (TextWriter _Output, string Comment, string Copyright, string Holder) { 
			 public static void ISCLicense (TextWriter _Output, string Comment, string Copyright, string Holder) {
			// #% string _Indent = ""; 
			 string _Indent = "";
			// #prefix Comment 
			_Indent =  Comment + _Indent; {
				//  
				_Output.Write ("\n{0}", _Indent);
				// #{Copyright}, #{Holder} 
				_Output.Write ("{1}, {2}\n{0}", _Indent, Copyright, Holder);
				//  
				_Output.Write ("\n{0}", _Indent);
				// Permission to use, copy, modify, and/or distribute this software for any 
				_Output.Write ("Permission to use, copy, modify, and/or distribute this software for any\n{0}", _Indent);
				// purpose with or without fee is hereby granted, provided that the above 
				_Output.Write ("purpose with or without fee is hereby granted, provided that the above\n{0}", _Indent);
				// copyright notice and this permission notice appear in all copies. 
				_Output.Write ("copyright notice and this permission notice appear in all copies.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES 
				_Output.Write ("THE SOFTWARE IS PROVIDED \"AS IS\" AND THE AUTHOR DISCLAIMS ALL WARRANTIES\n{0}", _Indent);
				// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF 
				_Output.Write ("WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF\n{0}", _Indent);
				// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR 
				_Output.Write ("MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR\n{0}", _Indent);
				// ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES 
				_Output.Write ("ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES\n{0}", _Indent);
				// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN 
				_Output.Write ("WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN\n{0}", _Indent);
				// ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF 
				_Output.Write ("ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF\n{0}", _Indent);
				// OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE. 
				_Output.Write ("OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end prefix 
			}
			// #% } 
			 }
			// #end block 
		
		//  
		//  
		// #end pclass 
		}
	}
