using  System.Text;
using  Goedel.Trace;
using  Goedel.Protocol;
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Trace.Documentation {
	/// <summary>A Goedel script.</summary>
	public partial class ExampleGenerator : global::Goedel.Registry.Script {
		/// <summary>Default constructor.</summary>
		public ExampleGenerator () : base () {
			}
		/// <summary>Constructor with output stream.</summary>
		/// <param name="Output">The output stream</param>
		public ExampleGenerator (TextWriter Output) : base (Output) {
			}

		

		//
		// AdminExamples
		//
		public void AdminExamples (TraceExamples Example) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		}
	}
