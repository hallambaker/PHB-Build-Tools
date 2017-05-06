// Script Syntax Version:  1.0

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
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Command {
	public partial class GenerateCS : global::Goedel.Registry.Script {

		  public GenerateCS (TextWriter Output) : base (Output) {}
		//
		// To Do list:
		//
		//	*	Should sense the console width and properly format the usage output
		//	*	Should support brief descriptions of the options and output on usage
		//  *	Write backing code for ExistingFile NewFile Directory etc
		//	*	Support for shell mode
		//	*	Default command
		//	*	Predispatch call
		//  *	Allow debug stubs to be turned off to make code compact
		//
		 TYPE<_Choice> TypeType;
		 CommandParse CommandParse;
		 Separator Separator = new Separator (",");
		 string NameClassType = "Goedel.Registry.Type";
		 string NameClassFile = "Goedel.Registry._File";
		 string NameClassFlag = "Goedel.Registry._Flag";
		 string NameDispatchType = "Goedel.Registry.Dispatch";
		 Command DefaultCommand = null;
		

		//
		// Generate
		//
		public void Generate (CommandParse CommandParseIn) {
			 Goedel.Registry.Boilerplate.Header (_Output, "//  ", CommandParseIn.Options.Started);
			_Output.Write ("\n{0}", _Indent);
			// Goedel.Registry.Script.MITLicense (_Output, "//", 
			//			Goedel.Registry.Script.AssemblyCopyright, Goedel.Registry.Script.AssemblyCompany);
			 GenerateX (CommandParseIn);
			}
		

		//
		// GenerateX
		//
		public void GenerateX (CommandParse CommandParseIn) {
			 CommandParse = CommandParseIn;
			 CommandParse.Init();
			 TypeType = CommandParse.Registry.FindType ("TypeType");
			_Output.Write ("using System;\n{0}", _Indent);
			_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
			_Output.Write ("using System.IO;\n{0}", _Indent);
			_Output.Write ("using System.Text;\n{0}", _Indent);
			_Output.Write ("using Goedel.Registry;\n{0}", _Indent);
			_Output.Write ("using Goedel.Utilities;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Item in CommandParse.Top) {
				switch (Item._Tag ()) {
					case CommandParseType.Class: { 
					
					GenerateClass ((Class)Item);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			}
		 void GenerateCommandEntries (List<_Choice> Entries) {
		foreach  (_Choice Entry in Entries) {
			switch (Entry._Tag ()) {
				case CommandParseType.CommandSet: {
				  CommandSet Cast = (CommandSet) Entry; 
				_Output.Write ("{1}\n{0}", _Indent, Separator);
				_Output.Write ("				{{\"{1}\", DescribeCommandSet_{2}}}", _Indent, Cast.Tag.ToLower(), Cast.Id);
				break; }
				case CommandParseType.Command: {
				  Command Cast = (Command) Entry; 
				_Output.Write ("{1}\n{0}", _Indent, Separator);
				_Output.Write ("				{{\"{1}\", _{2}._DescribeCommand }}", _Indent, Cast.Tag.ToLower(), Cast.Id);
				break; }
				case CommandParseType.About: {
				  About Cast = (About) Entry; 
				_Output.Write ("{1}\n{0}", _Indent, Separator);
				_Output.Write ("				{{\"{1}\", _About }}", _Indent, Cast.Tag.ToLower());
			break; }
				}
			}
		 }
		

		//
		// GenerateClass
		//
		public void GenerateClass (Class Class) {
			_Output.Write ("namespace {1} {{\n{0}", _Indent, Class.Namespace);
			_Output.Write ("    public partial class CommandLineInterpreter : CommandLineInterpreterBase {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		static char UnixFlag = '-';\n{0}", _Indent);
			_Output.Write ("		static char WindowsFlag = '/';\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        static bool IsFlag(char c) {{\n{0}", _Indent);
			_Output.Write ("            return (c == UnixFlag) | (c == WindowsFlag) ;\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				switch (Entry._Tag ()) {
					case CommandParseType.CommandSet: {
					  CommandSet Cast = (CommandSet) Entry; 
					
					 Separator.IsFirst = true;
					_Output.Write ("		static DescribeCommandSet DescribeCommandSet_{1} = new DescribeCommandSet () {{\n{0}", _Indent, Cast.Id);
					_Output.Write ("            Identifier = \"{1}\",\n{0}", _Indent, Cast.Tag.ToLower());
					_Output.Write ("			Entries = new  SortedDictionary<string, DescribeCommand> () {{", _Indent);
					
					GenerateCommandEntries (Cast.Entries);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("				}} // End Entries\n{0}", _Indent);
					_Output.Write ("			}};\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        static CommandLineInterpreter () {{\n{0}", _Indent);
			_Output.Write ("            System.OperatingSystem OperatingSystem = System.Environment.OSVersion;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            if (OperatingSystem.Platform == PlatformID.Unix |\n{0}", _Indent);
			_Output.Write ("                    OperatingSystem.Platform == PlatformID.MacOSX) {{\n{0}", _Indent);
			_Output.Write ("                FlagIndicator = UnixFlag;\n{0}", _Indent);
			_Output.Write ("                }}\n{0}", _Indent);
			_Output.Write ("            else {{\n{0}", _Indent);
			_Output.Write ("                FlagIndicator = WindowsFlag;\n{0}", _Indent);
			_Output.Write ("                }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  (Class.DefaultCommand != null) ) {
				_Output.Write ("				DefaultCommand = _{1}._DescribeCommand;\n{0}", _Indent, Class.DefaultCommand.Id);
				}
			if (  (Class.Description != null) ) {
				_Output.Write ("				Description = \"{1}\";\n{0}", _Indent, Class.Description);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			Entries = new  SortedDictionary<string, DescribeCommand> () {{", _Indent);
			 Separator.IsFirst = true;
			GenerateCommandEntries (Class.Entries);
			_Output.Write ("{1}\n{0}", _Indent, Separator);
			_Output.Write ("				{{\"help\", _Brief }}\n{0}", _Indent);
			_Output.Write ("				}}; // End Entries\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				switch (Entry._Tag ()) {
					case CommandParseType.About: {
					  About Cast = (About) Entry; 
					_Output.Write ("        public static DescribeCommandEntry _About = new DescribeCommandEntry() {{\n{0}", _Indent);
					_Output.Write ("            Identifier = \"about\",\n{0}", _Indent);
					_Output.Write ("            HandleDelegate = About,\n{0}", _Indent);
					_Output.Write ("			Entries = new  List<DescribeEntry> () {{}}\n{0}", _Indent);
					_Output.Write ("            }};\n{0}", _Indent);
					break; }
					case CommandParseType.Brief: {
					  Brief Cast = (Brief) Entry; 
					_Output.Write ("        public static DescribeCommandEntry _Brief = new DescribeCommandEntry() {{\n{0}", _Indent);
					_Output.Write ("            Identifier = \"help\",\n{0}", _Indent);
					_Output.Write ("            HandleDelegate = Brief,\n{0}", _Indent);
					_Output.Write ("			Entries = new  List<DescribeEntry> () {{}}\n{0}", _Indent);
					_Output.Write ("            }};\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  CommandParse.Main ) {
				_Output.Write ("        static void Main(string[] args) {{\n{0}", _Indent);
				_Output.Write ("			var CLI = new CommandLineInterpreter ();\n{0}", _Indent);
				_Output.Write ("			CLI.MainMethod (args);\n{0}", _Indent);
				_Output.Write ("			}}\n{0}", _Indent);
				} else {
				_Output.Write ("\n{0}", _Indent);
				}
			_Output.Write ("        public void MainMethod(string[] args) {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			{1} Dispatch = new {2} ();\n{0}", _Indent, Class.Id, Class.Id);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			if (  CommandParse.Catcher ) {
				_Output.Write ("			try {{\n{0}", _Indent);
				}
			_Output.Write ("				Assert.False (args.Length == 0, NoCommand.Throw);\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("                if (IsFlag(args[0][0])) {{\n{0}", _Indent);
			_Output.Write ("					Dispatcher (Entries, Dispatch, args, 0);\n{0}", _Indent);
			_Output.Write ("                    }}\n{0}", _Indent);
			_Output.Write ("                else {{\n{0}", _Indent);
			if (  DefaultCommand != null ) {
				_Output.Write ("					Handle_{1} (Dispatch, args, 0);\n{0}", _Indent, DefaultCommand.Id);
				} else {
				_Output.Write ("                    Assert.True (false, NoCommand.Throw);\n{0}", _Indent);
				}
			_Output.Write ("                    }}\n{0}", _Indent);
			if (  CommandParse.Catcher ) {
				_Output.Write ("				}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("            catch (ParserException) {{\n{0}", _Indent);
				_Output.Write ("			    Brief ();\n{0}", _Indent);
				_Output.Write ("				}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("            catch (System.Exception Exception) {{\n{0}", _Indent);
				_Output.Write ("                Console.WriteLine(\"Application: {{0}}\", Exception.Message);\n{0}", _Indent);
				_Output.Write ("                }}\n{0}", _Indent);
				}
			_Output.Write ("            }} // Main\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				switch (Entry._Tag ()) {
					case CommandParseType.Command: {
					  Command Command = (Command) Entry; 
					
					 CommandHandler (Command, Class);
					break; }
					case CommandParseType.CommandSet: {
					  CommandSet CommandSet = (CommandSet) Entry; 
					foreach  (var Inner in CommandSet.Entries) {
						switch (Inner._Tag ()) {
							case CommandParseType.Command: {
							  Command Command = (Command) Inner; 
							
							 CommandHandler (Command, Class);
						break; }
							}
						}
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	}} // class Main\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// The stub class for carrying optional parameters for each command type\n{0}", _Indent);
			_Output.Write ("	// As with the main class each consists of an abstract main class \n{0}", _Indent);
			_Output.Write ("	// with partial virtual that can be extended as required.\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch \n{0}", _Indent);
			_Output.Write ("	// and Goedel.Registry.Type\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				switch (Entry._Tag ()) {
					case CommandParseType.OptionSet: {
					  OptionSet OptionSet = (OptionSet) Entry; 
					_Output.Write ("	public interface I{1} {{\n{0}", _Indent, OptionSet.Id);
					foreach  (_Choice OptionC in OptionSet.Options) {
						switch (OptionC._Tag ()) {
							case CommandParseType.Option: {
							  Option Option = (Option) OptionC; 
							_Output.Write ("		{1}			{2}{{get; set;}}\n{0}", _Indent, Option.Type, Option.Name);
						break; }
							}
						}
					_Output.Write ("		}}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					break; }
					case CommandParseType.Command: {
					  Command Cast = (Command) Entry; 
					
					CommandOptionClass (Cast);
					break; }
					case CommandParseType.CommandSet: {
					  CommandSet CommandSet = (CommandSet) Entry; 
					foreach  (var Inner in CommandSet.Entries) {
						switch (Inner._Tag ()) {
							case CommandParseType.Command: {
							  Command Command = (Command) Inner; 
							
							CommandOptionClass (Command);
						break; }
							}
						}
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			// This section contains declarations for the builtins and types.
			//
			foreach  (ID<_Choice> ID in TypeType.IDs) {
				_Output.Write ("    // Parameter type {1}\n{0}", _Indent, ID);
				if (  (ID.ToString() == "NewFile") | (ID.ToString() == "ExistingFile") ) {
					_Output.Write ("    public abstract class _{1} : {2} {{\n{0}", _Indent, ID, NameClassFile);
					} else if (  (ID.ToString() == "Flag")) {
					_Output.Write ("    public abstract class _{1} : {2} {{\n{0}", _Indent, ID, NameClassFlag);
					} else {
					_Output.Write ("    public abstract class _{1} : {2} {{\n{0}", _Indent, ID, NameClassType);
					}
				_Output.Write ("        public _{1}() {{\n{0}", _Indent, ID);
				_Output.Write ("            }}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				if (  CommandParse.Builtins ) {
					if (  (ID.ToString() == "Flag") ) {
						} else if (  (ID.ToString() == "NewFile")) {
						} else if (  (ID.ToString() == "ExistingFile")) {
						} else {
						_Output.Write ("		public string			Value {{\n{0}", _Indent);
						_Output.Write ("			get => Text;\n{0}", _Indent);
						_Output.Write ("			}}\n{0}", _Indent);
						}
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("        }} // _{1}\n{0}", _Indent, ID);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("    public partial class  {1} : _{2} {{\n{0}", _Indent, ID, ID);
				_Output.Write ("        public {1}() {{\n{0}", _Indent, ID);
				_Output.Write ("            }}\n{0}", _Indent);
				_Output.Write ("			\n{0}", _Indent);
				_Output.Write ("        public static {1} Factory (string Value) {{\n{0}", _Indent, ID);
				_Output.Write ("            var Result = new {1}();\n{0}", _Indent, ID);
				_Output.Write ("            Result.Default(Value);\n{0}", _Indent);
				_Output.Write ("            return Result;\n{0}", _Indent);
				_Output.Write ("            }}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("        }} // {1}\n{0}", _Indent, ID);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// The stub class just contains routines that echo their arguments and\n{0}", _Indent);
			_Output.Write ("	// write 'not yet implemented'\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// Eventually there will be a compiler option to suppress the debugging\n{0}", _Indent);
			_Output.Write ("	// to eliminate the redundant code\n{0}", _Indent);
			_Output.Write ("    public class _{1} : global::Goedel.Registry.DispatchShell {{\n{0}", _Indent, Class.Id);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice Entry in Class.Entries) {
				switch (Entry._Tag ()) {
					case CommandParseType.Command: {
					  Command Command = (Command) Entry; 
					
					CommandMethod (Command);
					break; }
					case CommandParseType.CommandSet: {
					  CommandSet CommandSet = (CommandSet) Entry; 
					foreach  (var Inner in CommandSet.Entries) {
						switch (Inner._Tag ()) {
							case CommandParseType.Command: {
							  Command Command = (Command) Inner; 
							
							CommandMethod (Command);
						break; }
							}
						}
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        }} // class _{1}\n{0}", _Indent, Class.Id);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    public partial class {1} : _{2} {{\n{0}", _Indent, Class.Id, Class.Id);
			_Output.Write ("        }} // class {1}\n{0}", _Indent, Class.Id);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    }} // namespace {1}\n{0}", _Indent, Class.Id);
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// CommandHandler
		//
		public void CommandHandler (Command Command, Class Class) {
			_Output.Write ("		private enum TagType_{1} {{\n{0}", _Indent, Command.Id);
			foreach  (_Choice CommandEntry in Command.Entries) {
				switch (CommandEntry._Tag ()) {
					case CommandParseType.Parameter: {
					  Parameter Param = (Parameter) CommandEntry; 
					_Output.Write ("			{1},\n{0}", _Indent, Param.Name);
					break; }
					case CommandParseType.Parser: {
					  Parser Param = (Parser) CommandEntry; 
					_Output.Write ("			{1},\n{0}", _Indent, Param.Class);
					break; }
					case CommandParseType.Option: {
					  Option Param = (Option) CommandEntry; 
					_Output.Write ("			{1},\n{0}", _Indent, Param.Name);
					break; }
					case CommandParseType.Lazy: {
					  Lazy Param = (Lazy) CommandEntry; 
					_Output.Write ("			{1},\n{0}", _Indent, Param.Name);
					break; }
					case CommandParseType.Script: {
					  Script Param = (Script) CommandEntry; 
					_Output.Write ("			{1},\n{0}", _Indent, Param.Id);
				break; }
					}
				}
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		public static void Handle_{1} (\n{0}", _Indent, Command.Id);
			_Output.Write ("					DispatchShell  DispatchIn, string[] Args, int Index) {{\n{0}", _Indent);
			_Output.Write ("			{1} Dispatch =	DispatchIn as {2};\n{0}", _Indent, Class.Id, Class.Id);
			_Output.Write ("			{1}		Options = new {2} ();\n{0}", _Indent, Command.Id, Command.Id);
			_Output.Write ("			ProcessOptions (Args, Index, Options);\n{0}", _Indent);
			_Output.Write ("			Dispatch.{1} (Options);\n{0}", _Indent, Command.Id);
			_Output.Write ("			}}\n{0}", _Indent);
			}
		

		//
		// CommandMethod
		//
		public void CommandMethod (Command Command) {
			 bool DefaultOutput = true;
			 string Lazy = null;
			foreach  (_Choice CommandEntry in Command.Entries) {
				switch (CommandEntry._Tag ()) {
					case CommandParseType.Parser: { 
					
					 DefaultOutput = false;
					break; }
					case CommandParseType.Generator: { 
					
					 DefaultOutput = false;
					break; }
					case CommandParseType.Script: { 
					
					 DefaultOutput = false;
					break; }
					case CommandParseType.Lazy: {
					  Lazy Cast = (Lazy) CommandEntry; 
					
					 Lazy = Cast.Name.ToString ();
				break; }
					}
				}
			_Output.Write ("		public virtual void {1} ( {2} Options) {{\n{0}", _Indent, Command.Id, Command.Id);
			if (  DefaultOutput ) {
				_Output.Write ("			CommandLineInterpreter.DescribeValues (Options);\n{0}", _Indent);
				} else {
				_Output.Write ("			string inputfile = null;\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				foreach  (_Choice CommandEntry in Command.Entries) {
					switch (CommandEntry._Tag ()) {
						case CommandParseType.Parser: {
						  Parser Parser = (Parser) CommandEntry; 
						_Output.Write ("			inputfile = Options.{1}.Text;\n{0}", _Indent, Parser.Class);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("            {1}.{2} Parse = new {3}.{4}() {{\n{0}", _Indent, Parser.Namespace, Parser.Class, Parser.Namespace, Parser.Class);
						foreach  (_Choice CommandEntry2 in Command.Entries) {
							switch (CommandEntry2._Tag ()) {
								case CommandParseType.Option: {
								  Option Option = (Option) CommandEntry2; 
								_Output.Write ("			    {1} = Options.{2}.Value,\n{0}", _Indent, Option.Name, Option.Name);
							break; }
								}
							}
						_Output.Write ("				Options = Options\n{0}", _Indent);
						_Output.Write ("				}};\n{0}", _Indent);
						_Output.Write ("        \n{0}", _Indent);
						_Output.Write ("			\n{0}", _Indent);
						_Output.Write ("			using (Stream infile =\n{0}", _Indent);
						_Output.Write ("                        new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {{\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("                Lexer Schema = new Lexer(inputfile);\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						_Output.Write ("                Schema.Process(infile, Parse);\n{0}", _Indent);
						_Output.Write ("                }}\n{0}", _Indent);
						_Output.Write ("\n{0}", _Indent);
						break; }
						case CommandParseType.Generator: { 
					break; }
						}
					}
				_Output.Write ("\n{0}", _Indent);
				foreach  (_Choice CommandEntry in Command.Entries) {
					switch (CommandEntry._Tag ()) {
						case CommandParseType.Script: {
						  Script Script = (Script) CommandEntry; 
						_Output.Write ("			// Script output of type {1} {2}\n{0}", _Indent, Script.Id, Script.Extension);
						_Output.Write ("			if (Options.{1}.Text != null) {{\n{0}", _Indent, Script.Id);
						_Output.Write ("				string outputfile = FileTools.DefaultOutput (inputfile, Options.{1}.Text, \n{0}", _Indent, Script.Id);
						_Output.Write ("					Options.{1}.Extension);\n{0}", _Indent, Script.Id);
						if (  Lazy != null ) {
							_Output.Write ("				if (Options.{1}.IsSet & FileTools.UpToDate (inputfile, outputfile)) {{\n{0}", _Indent, Lazy);
							_Output.Write ("					return;\n{0}", _Indent);
							_Output.Write ("					}}\n{0}", _Indent);
							_Output.Write ("				using (Stream outputStream =\n{0}", _Indent);
							_Output.Write ("							new FileStream(outputfile, FileMode.Create, FileAccess.Write)) {{\n{0}", _Indent);
							_Output.Write ("					using (TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8)) {{\n{0}", _Indent);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("						{1}.{2} Script = new {3}.{4} (OutputWriter);\n{0}", _Indent, Script.Namespace, Script.Class, Script.Namespace, Script.Class);
							_Output.Write ("\n{0}", _Indent);
							_Output.Write ("						Script.{1} (Parse);\n{0}", _Indent, Script.Id);
							_Output.Write ("						}}\n{0}", _Indent);
							_Output.Write ("					}}\n{0}", _Indent);
							}
						_Output.Write ("				}}\n{0}", _Indent);
					break; }
						}
					}
				}
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		

		//
		// CommandOptionClass
		//
		public void CommandOptionClass (Command Command) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    public class _{1} : {2} ", _Indent, Command.Id, NameDispatchType);
			foreach  (_Choice OptionC in Command.Entries) {
				switch (OptionC._Tag ()) {
					case CommandParseType.Include: {
					  Include Include = (Include) OptionC; 
					_Output.Write (",\n{0}", _Indent);
					_Output.Write ("							I{1}", _Indent, Include.Id);
				break; }
					}
				}
			_Output.Write (" {{\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		public override Goedel.Registry.Type[] _Data {{get; set;}} = new Goedel.Registry.Type [] {{", _Indent);
			 Separator.IsFirst = true;
			foreach  (var Entry in Command.EntryItems) {
				_Output.Write ("{1}\n{0}", _Indent, Separator);
				_Output.Write ("			new {1} ()", _Indent, Entry.Type);
				}
			_Output.Write ("			}} ;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (var Entry in Command.EntryItems) {
				 var Item = Entry.Item;
				_Output.Write ("		/// <summary>Field accessor for {1} [{2}]</summary>\n{0}", _Indent, Entry.IsOption.If("option","parameter"), Entry.Tag);
				_Output.Write ("		public virtual {1} {2} {{\n{0}", _Indent, Entry.Type, Entry.ID);
				_Output.Write ("			get => _Data[{1}] as {2};\n{0}", _Indent, Entry.Index, Entry.Type);
				_Output.Write ("			set => _Data[{1}]  = value;\n{0}", _Indent, Entry.Index);
				_Output.Write ("			}}\n{0}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		public static DescribeCommandEntry _DescribeCommand = new  DescribeCommandEntry () {{\n{0}", _Indent);
			_Output.Write ("			Identifier = \"{1}\",\n{0}", _Indent, Command.Tag.ToLower());
			_Output.Write ("			Brief =  \"{1}\",\n{0}", _Indent, Command.Brief);
			_Output.Write ("			HandleDelegate =  CommandLineInterpreter.Handle_{1},\n{0}", _Indent, Command.Id);
			_Output.Write ("			Lazy =  {1},\n{0}", _Indent, Command.Lazy.If("true","false"));
			//			Parser =  "#{Command.Parser}",
			_Output.Write ("			Entries = new List<DescribeEntry> () {{", _Indent);
			 Separator.IsFirst = true;
			foreach  (var EntryItem in Command.EntryItems) {
				 var Item = EntryItem.Item;
				_Output.Write ("{1}\n{0}", _Indent, Separator);
				_Output.Write ("				new DescribeEntryParameter () {{\n{0}", _Indent);
				_Output.Write ("					Identifier = \"{1}\", \n{0}", _Indent, EntryItem.ID);
				_Output.Write ("					Default = \"{1}\",\n{0}", _Indent, EntryItem.Default);
				_Output.Write ("					Brief = \"{1}\",\n{0}", _Indent, EntryItem.Brief);
				_Output.Write ("					Index = {1},\n{0}", _Indent, EntryItem.Index);
				_Output.Write ("					Key = \"{1}\"\n{0}", _Indent, EntryItem.Tag.ToLower());
				_Output.Write ("					}}", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("			}};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    public partial class {1} : _{2} {{\n{0}", _Indent, Command.Id, Command.Id);
			_Output.Write ("        }} // class {1}\n{0}", _Indent, Command.Id);
			}
		}
	}
