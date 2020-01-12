// Script Syntax Version:  1.0

//  © 2015-2019 by Phill Hallam-Baker
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
namespace Goedel.Tool.Version {
	public partial class Generate : global::Goedel.Registry.Script {

		

		//
		// GenerateCS
		//
		public void GenerateCS (VersionInfo Version) {
			_Output.Write ("using System;\n{0}", _Indent);
			_Output.Write ("using System.Reflection;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("[assembly: System.Reflection.AssemblyVersionAttribute(\"{1}\")]\n{0}", _Indent, Version.Assembly);
			_Output.Write ("[assembly: System.Reflection.AssemblyFileVersionAttribute(\"{1}\")]\n{0}", _Indent, Version.File);
			_Output.Write ("\n{0}", _Indent);
			}
		}
	}
