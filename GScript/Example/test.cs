// #! Comments can appear anywhere in the script 
// Comments can appear anywhere in the script
// #class TestNamespace OutputFormatter 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace TestNamespace {
	public partial class OutputFormatter {
		TextWriter _Output;
		public string _Indent = "";
		public OutputFormatter (TextWriter Output) {
			_Output = Output;
			}

		//  
		// #% int count = 4; 
		 int count = 4;
		// #method WriteSchema string s 
		

		//
		// WriteSchema
		//
		public void WriteSchema (string s) {
			// <? XML something or other ?> 
			_Output.Write ("<? XML something or other ?>\n{0}", _Indent);
			// <!-- Count is #{count:x}--> 
			_Output.Write ("<!-- Count is {1:x}-->\n{0}", _Indent, count);
			// <schema name="#{s}"  /> 
			_Output.Write ("<schema name=\"{1}\"  />\n{0}", _Indent, s);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		// #end class 
		}
	}
