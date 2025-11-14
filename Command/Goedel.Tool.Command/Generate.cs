// Script Syntax Version:  1.0

//  © 2015-2021 by Threshold Secrets LLC.
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
using  Goedel.Utilities;
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Command;
public partial class GenerateCS : global::Goedel.Registry.Script {

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
	 string NameDispatchType = "Goedel.Command.Dispatch";
	
	/// <summary>	
	/// Generate
	/// </summary>
	/// <param name="CommandParseIn"></param>
	public void Generate (CommandParse CommandParseIn) {
		// Goedel.Registry.Boilerplate.Header (_Output, "//  ", CommandParseIn.Options.Started);
		// Goedel.Registry.Script.MITLicense (_Output, "//", 
		//			Goedel.Registry.Script.AssemblyCopyright, Goedel.Registry.Script.AssemblyCompany);
		 GenerateX (CommandParseIn);
		}
	
	/// <summary>	
	/// GenerateX
	/// </summary>
	/// <param name="CommandParseIn"></param>
	public void GenerateX (CommandParse CommandParseIn) {
		 CommandParse = CommandParseIn;
		 CommandParse.Init();
		 TypeType = CommandParse.Registry.FindType ("TypeType");
		 Registry.Boilerplate.Header(_Output, "//  ", DateTime.Now);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("using System;\n{0}", _Indent);
		_Output.Write ("using System.Collections.Generic;\n{0}", _Indent);
		_Output.Write ("using System.IO;\n{0}", _Indent);
		_Output.Write ("using System.Text;\n{0}", _Indent);
		_Output.Write ("using Goedel.Command;\n{0}", _Indent);
		if (  CommandParse.DeclareRegistry ) {
			_Output.Write ("using Goedel.Registry;\n{0}", _Indent);
			}
		_Output.Write ("using Goedel.Utilities;\n{0}", _Indent);
		_Output.Write ("#pragma warning disable IDE0079\n{0}", _Indent);
		_Output.Write ("#pragma warning disable IDE1006\n{0}", _Indent);
		_Output.Write ("#pragma warning disable CS1591\n{0}", _Indent);
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
			_Output.Write ("			{{\"{1}\", DescribeCommandSet_{2}}}", _Indent, Cast.Tag.ToLower(), Cast.Id);
			break; }
			case CommandParseType.Command: {
			  Command Cast = (Command) Entry; 
			_Output.Write ("{1}\n{0}", _Indent, Separator);
			_Output.Write ("			{{\"{1}\", _{2}._DescribeCommand }}", _Indent, Cast.Tag.ToLower(), Cast.Id);
		break; }
			}
		}
	 }
	
	/// <summary>	
	/// GenerateCommandSet
	/// </summary>
	/// <param name="CommandSet"></param>
	public void GenerateCommandSet (CommandSet CommandSet) {
		 Separator.IsFirst = true;
		_Output.Write ("	public static DescribeCommandSet DescribeCommandSet_{1} => new  () {{\n{0}", _Indent, CommandSet.Id);
		_Output.Write ("        Identifier = \"{1}\",\n{0}", _Indent, CommandSet.Tag.ToLower());
		_Output.Write ("		Brief = \"{1}\",\n{0}", _Indent, CommandSet.Brief);
		_Output.Write ("		Entries = new  () {{", _Indent);
		GenerateCommandEntries (CommandSet.Entries);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			}} // End Entries\n{0}", _Indent);
		_Output.Write ("		}};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Entry in CommandSet.Entries) {
			switch (Entry._Tag ()) {
				case CommandParseType.CommandSet: {
				  CommandSet Cast = (CommandSet) Entry; 
				
				GenerateCommandSet (Cast);
			break; }
				}
			}
		}
	
	/// <summary>	
	/// GenerateClass
	/// </summary>
	/// <param name="Class"></param>
	public void GenerateClass (Class Class) {
		_Output.Write ("namespace {1};\n{0}", _Indent, Class.Namespace);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Entry in Class.EnumItems) {
			_Output.Write ("// Enumeration type\n{0}", _Indent);
			_Output.Write ("public enum {1} {{", _Indent, Entry.Name.Label);
			 Separator.IsFirst = true;
			foreach  (var item in Entry.Modifier) {
				switch (item._Tag ()) {
					case CommandParseType.Case: {
					  Case Cast = (Case) item; 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("    /// <summary>Case \"{1}\": {2}</summary>\n{0}", _Indent, Cast.Tag, Cast.Brief.If());
					_Output.Write ("    {1}", _Indent, Cast.Id.Label);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class CommandLineInterpreter : CommandLineInterpreterBase {{\n{0}", _Indent);
		_Output.Write ("        \n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	/// <summary>The command entries</summary>\n{0}", _Indent);
		_Output.Write ("    public static SortedDictionary<string, DescribeCommand> Entries {{ get; set; }}\n{0}", _Indent);
		_Output.Write ("	/// <summary>The default command.</summary>\n{0}", _Indent);
		_Output.Write ("	public static DescribeCommandEntry DefaultCommand {{ get; set; }}\n{0}", _Indent);
		_Output.Write ("	/// <summary>Description of the comman</summary>\n{0}", _Indent);
		_Output.Write ("	public static string Description {{ get; set; }} = \"<Not specified>\";\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	static readonly char UnixFlag = '-';\n{0}", _Indent);
		_Output.Write ("	static readonly char WindowsFlag = '/';\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		if (  (Class.Help != null)	 ) {
			_Output.Write ("    /// <summary>\n{0}", _Indent);
			_Output.Write ("    /// Default help dispatch\n{0}", _Indent);
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"Dispatch\">The command description.</param>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"args\">The set of arguments.</param>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"index\">The first unparsed argument.</param>\n{0}", _Indent);
			_Output.Write ("    public static void Help (DispatchShell Dispatch, string[] args, int index) =>\n{0}", _Indent);
			_Output.Write ("        Brief(Description, DefaultCommand, Entries);\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    public readonly static DescribeCommandEntry DescribeHelp = new () {{\n{0}", _Indent);
			_Output.Write ("        Identifier = \"help\",\n{0}", _Indent);
			_Output.Write ("        HandleDelegate = Help,\n{0}", _Indent);
			_Output.Write ("        Entries = new () {{ }}\n{0}", _Indent);
			_Output.Write ("        }};\n{0}", _Indent);
			}
		if (  (Class.About != null) ) {
			_Output.Write ("    /// <summary>\n{0}", _Indent);
			_Output.Write ("    /// Describe the application invoked by the command.\n{0}", _Indent);
			_Output.Write ("    /// </summary>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"Dispatch\">The command description.</param>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"args\">The set of arguments.</param>\n{0}", _Indent);
			_Output.Write ("    /// <param name=\"index\">The first unparsed argument.</param>\n{0}", _Indent);
			_Output.Write ("    public static void About (DispatchShell Dispatch, string[] args, int index) =>\n{0}", _Indent);
			_Output.Write ("        FileTools.About();\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    public readonly static DescribeCommandEntry DescribeAbout = new () {{\n{0}", _Indent);
			_Output.Write ("        Identifier = \"about\",\n{0}", _Indent);
			_Output.Write ("        HandleDelegate = About,\n{0}", _Indent);
			_Output.Write ("        Entries = new () {{ }}\n{0}", _Indent);
			_Output.Write ("        }};\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Entry in Class.EnumItems) {
			_Output.Write ("	public readonly static DescribeEntryEnumerate Describe{1} = new  () {{\n{0}", _Indent, Entry.Name.Label);
			_Output.Write ("        Identifier = \"{1}\",\n{0}", _Indent, Entry.Command);
			_Output.Write ("        Brief = \"{1}\",\n{0}", _Indent, Entry.Brief);
			_Output.Write ("        Entries = new () {{ ", _Indent);
			 Separator.IsFirst = true;
			foreach  (var item in Entry.Modifier) {
				switch (item._Tag ()) {
					case CommandParseType.Case: {
					  Case Cast = (Case) item; 
					_Output.Write ("{1}\n{0}", _Indent, Separator);
					_Output.Write ("			new DescribeCase () {{\n{0}", _Indent);
					_Output.Write ("				Identifier = \"{1}\",\n{0}", _Indent, Cast.Tag);
					_Output.Write ("				Brief = \"{1}\",\n{0}", _Indent, Cast.Brief);
					_Output.Write ("				Value = (int) {1}.{2}\n{0}", _Indent, Entry.Name.Label, Cast.Id.Label);
					_Output.Write ("				}}", _Indent);
				break; }
					}
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("		}};\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		//        static bool IsFlag(char c) =>
		//            (c == UnixFlag) | (c == WindowsFlag) ;
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (_Choice Entry in Class.Entries) {
			switch (Entry._Tag ()) {
				case CommandParseType.CommandSet: {
				  CommandSet Cast = (CommandSet) Entry; 
				
				GenerateCommandSet (Cast);
			break; }
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    static CommandLineInterpreter () {{\n{0}", _Indent);
		_Output.Write ("        System.OperatingSystem OperatingSystem = System.Environment.OSVersion;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("        if (OperatingSystem.Platform == PlatformID.Unix |\n{0}", _Indent);
		_Output.Write ("                OperatingSystem.Platform == PlatformID.MacOSX) {{\n{0}", _Indent);
		_Output.Write ("            FlagIndicator = UnixFlag;\n{0}", _Indent);
		_Output.Write ("            }}\n{0}", _Indent);
		_Output.Write ("        else {{\n{0}", _Indent);
		_Output.Write ("            FlagIndicator = WindowsFlag;\n{0}", _Indent);
		_Output.Write ("            }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		if (  (Class.DefaultCommand != null) ) {
			_Output.Write ("			DefaultCommand = _{1}._DescribeCommand;\n{0}", _Indent, Class.DefaultCommand.Id);
			}
		if (  (Class.Description != null) ) {
			_Output.Write ("			Description = \"{1}\";\n{0}", _Indent, Class.Description);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("		Entries = new   () {{", _Indent);
		 Separator.IsFirst = true;
		GenerateCommandEntries (Class.Entries);
		if (  (Class.About != null) ) {
			_Output.Write ("{1}\n{0}", _Indent, Separator);
			_Output.Write ("			{{\"about\", DescribeAbout }}", _Indent);
			}
		if (  (Class.Help != null) ) {
			_Output.Write ("{1}\n{0}", _Indent, Separator);
			_Output.Write ("			{{\"help\", DescribeHelp }}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			}}; // End Entries\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("        }}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		if (  Class.Main ) {
			_Output.Write ("    static void Main(string[] args) {{\n{0}", _Indent);
			_Output.Write ("		var CLI = new CommandLineInterpreter ();\n{0}", _Indent);
			_Output.Write ("		CLI.MainMethod (args);\n{0}", _Indent);
			_Output.Write ("		}}\n{0}", _Indent);
			} else {
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    public void MainMethod(string[] Args) {{\n{0}", _Indent);
		_Output.Write ("		{1} Dispatch = new ();\n{0}", _Indent, Class.Id);
		_Output.Write ("\n{0}", _Indent);
		if (  CommandParse.Catcher ) {
			_Output.Write ("		try {{\n{0}", _Indent);
			_Output.Write ("			MainMethod (Dispatch, Args);\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("        catch (Goedel.Command.ParserException) {{\n{0}", _Indent);
			_Output.Write ("			Brief(Description, DefaultCommand, Entries);\n{0}", _Indent);
			_Output.Write ("			}}\n{0}", _Indent);
			_Output.Write ("        catch (System.Exception Exception) {{\n{0}", _Indent);
			_Output.Write ("            Console.WriteLine(\"Application: {{0}}\", Exception.Message);\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			} else {
			_Output.Write ("		MainMethod (Dispatch, Args);\n{0}", _Indent);
			}
		_Output.Write ("		}}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    public void MainMethod({1} Dispatch, string[] Args) =>\n{0}", _Indent, Class.Id);
		_Output.Write ("		Dispatcher (Entries, DefaultCommand, Dispatch, Args, 0);\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
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
				
				 CommandSetHandler (CommandSet, Class);
			break; }
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("}} // class Main\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("// The stub class for carrying optional parameters for each command type\n{0}", _Indent);
		_Output.Write ("// As with the main class each consists of an abstract main class \n{0}", _Indent);
		_Output.Write ("// with partial virtual that can be extended as required.\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("// All subclasses inherit from the abstract classes Goedel.Regisrty.Dispatch \n{0}", _Indent);
		_Output.Write ("// and Goedel.Command.Type\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (_Choice Entry in Class.Entries) {
			switch (Entry._Tag ()) {
				case CommandParseType.OptionSet: {
				  OptionSet OptionSet = (OptionSet) Entry; 
				_Output.Write ("public interface I{1} {{\n{0}", _Indent, OptionSet.Id);
				foreach  (_Choice OptionC in OptionSet.Options) {
					switch (OptionC._Tag ()) {
						case CommandParseType.Option: {
						  Option Option = (Option) OptionC; 
						_Output.Write ("	{1}			{2}{{get; set;}}\n{0}", _Indent, Option.Type, Option.Name);
					break; }
						}
					}
				_Output.Write ("	}}\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				break; }
				case CommandParseType.Command: {
				  Command Cast = (Command) Entry; 
				
				CommandOptionClass (Cast);
				break; }
				case CommandParseType.CommandSet: {
				  CommandSet Cast = (CommandSet) Entry; 
				
				CommandSetOptionClass (Cast);
			break; }
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class  Flag : Goedel.Command._Flag {{\n{0}", _Indent);
		_Output.Write ("    public Flag(string value=null) : base (value) {{}}\n{0}", _Indent);
		_Output.Write ("    }} // Flag\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class  File : Goedel.Command._File {{\n{0}", _Indent);
		_Output.Write ("	public File(string value=null) : base (value) {{}}\n{0}", _Indent);
		_Output.Write ("    }} // File\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class  NewFile : Goedel.Command._NewFile {{\n{0}", _Indent);
		_Output.Write ("	public NewFile(string value=null) : base (value) {{}}\n{0}", _Indent);
		_Output.Write ("    }} // NewFile\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class  ExistingFile : Goedel.Command._ExistingFile {{\n{0}", _Indent);
		_Output.Write ("	public ExistingFile(string value=null) : base (value) {{}}\n{0}", _Indent);
		_Output.Write ("    }} // ExistingFile\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class  Integer : Goedel.Command._Integer {{\n{0}", _Indent);
		_Output.Write ("	public Integer(string value=null) : base (value) {{}}\n{0}", _Indent);
		_Output.Write ("    }} // Integer\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class  String : Goedel.Command._String {{\n{0}", _Indent);
		_Output.Write ("	public String(string value=null) : base (value) {{}}\n{0}", _Indent);
		_Output.Write ("    }} // String\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class  Enumeration<T> : _Enumeration<T> {{\n{0}", _Indent);
		_Output.Write ("    public Enumeration(DescribeEntryEnumerate description) : base(description){{\n{0}", _Indent);
		_Output.Write ("        }}\n{0}", _Indent);
		_Output.Write ("    }} // _Enumeration<T>\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("// The stub class just contains routines that echo their arguments and\n{0}", _Indent);
		_Output.Write ("// write 'not yet implemented'\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("// Eventually there will be a compiler option to suppress the debugging\n{0}", _Indent);
		_Output.Write ("// to eliminate the redundant code\n{0}", _Indent);
		_Output.Write ("public class _{1} : global::Goedel.Command.DispatchShell {{\n{0}", _Indent, Class.Id);
		_Output.Write ("\n{0}", _Indent);
		foreach  (_Choice Entry in Class.Entries) {
			switch (Entry._Tag ()) {
				case CommandParseType.Command: {
				  Command Command = (Command) Entry; 
				
				CommandMethod (Command);
				break; }
				case CommandParseType.CommandSet: {
				  CommandSet CommandSet = (CommandSet) Entry; 
				
				CommandSetMethod (CommandSet);
			break; }
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("    }} // class _{1}\n{0}", _Indent, Class.Id);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class {1} : _{2} {{\n{0}", _Indent, Class.Id, Class.Id);
		_Output.Write ("    }} // class {1}\n{0}", _Indent, Class.Id);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		}
	
	/// <summary>	
	/// CommandHandler
	/// </summary>
	/// <param name="Command"></param>
	/// <param name="Class"></param>
	public void CommandHandler (Command Command, Class Class) {
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	public static void Handle_{1} (\n{0}", _Indent, Command.Id);
		_Output.Write ("				DispatchShell  DispatchIn, string[] Args, int Index) {{\n{0}", _Indent);
		_Output.Write ("		{1} Dispatch =	DispatchIn as {2};\n{0}", _Indent, Class.Id, Class.Id);
		_Output.Write ("		{1}		Options = new ();\n{0}", _Indent, Command.Id);
		_Output.Write ("		ProcessOptions (Args, Index, Options);\n{0}", _Indent);
		_Output.Write ("		Dispatch._PreProcess (Options);\n{0}", _Indent);
		if (  Class.ReturnType == null ) {
			_Output.Write ("		Dispatch.{1} (Options);\n{0}", _Indent, Command.Id);
			} else {
			_Output.Write ("		var result = Dispatch.{1} (Options);\n{0}", _Indent, Command.Id);
			_Output.Write ("		Dispatch._PostProcess (result);\n{0}", _Indent);
			}
		_Output.Write ("		}}\n{0}", _Indent);
		}
	
	/// <summary>	
	/// CommandMethod
	/// </summary>
	/// <param name="Command"></param>
	public void CommandMethod (Command Command) {
		 bool DefaultOutput = true;
		 string Lazy = null;
		 var Class = Command.ParentClass;
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
		_Output.Write ("	public virtual {1} {2} ( {3} Options) ", _Indent, Class.ReturnType ?? "void", Command.Id, Command.Id);
		if (  DefaultOutput ) {
			if (  Class.ReturnType != null ) {
				_Output.Write ("{{\n{0}", _Indent);
				_Output.Write ("		CommandLineInterpreter.DescribeValues (Options);\n{0}", _Indent);
				_Output.Write ("		return null;\n{0}", _Indent);
				_Output.Write ("		}}\n{0}", _Indent);
				} else {
				_Output.Write ("=>\n{0}", _Indent);
				_Output.Write ("		CommandLineInterpreter.DescribeValues (Options);\n{0}", _Indent);
				}
			} else {
			_Output.Write ("{{\n{0}", _Indent);
			_Output.Write ("		string inputfile = null;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			foreach  (_Choice CommandEntry in Command.Entries) {
				switch (CommandEntry._Tag ()) {
					case CommandParseType.Parser: {
					  Parser Parser = (Parser) CommandEntry; 
					_Output.Write ("		inputfile = Options.{1}.Text;\n{0}", _Indent, Parser.Class);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("        {1}.{2} Parse = new () {{\n{0}", _Indent, Parser.Namespace, Parser.Class);
					foreach  (_Choice CommandEntry2 in Command.Entries) {
						switch (CommandEntry2._Tag ()) {
							case CommandParseType.Option: {
							  Option Option = (Option) CommandEntry2; 
							_Output.Write ("			{1} = Options.{2}.Value,\n{0}", _Indent, Option.Name, Option.Name);
						break; }
							}
						}
					_Output.Write ("			}};\n{0}", _Indent);
					_Output.Write ("        \n{0}", _Indent);
					_Output.Write ("			\n{0}", _Indent);
					_Output.Write ("		using (Stream infile =\n{0}", _Indent);
					_Output.Write ("                    new FileStream(inputfile, FileMode.Open, FileAccess.Read)) {{\n{0}", _Indent);
					_Output.Write ("            Lexer Schema = new Lexer(inputfile);\n{0}", _Indent);
					_Output.Write ("            Schema.Process(infile, Parse);\n{0}", _Indent);
					_Output.Write ("            }}\n{0}", _Indent);
					_Output.Write ("		Parse.Init();\n{0}", _Indent);
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
					_Output.Write ("		// Script output of type {1} {2}\n{0}", _Indent, Script.Id, Script.Extension);
					_Output.Write ("		if (Options.{1}.Text != null) {{\n{0}", _Indent, Script.Id);
					_Output.Write ("			string outputfile = Options.{1}.Text; // Automatically defaults\n{0}", _Indent, Script.Id);
					if (  Lazy != null ) {
						_Output.Write ("			if (Options.{1}.Value & FileTools.UpToDate (inputfile, outputfile)) {{\n{0}", _Indent, Lazy);
						_Output.Write ("				return;\n{0}", _Indent);
						_Output.Write ("				}}\n{0}", _Indent);
						}
					_Output.Write ("            using Stream outputStream =\n{0}", _Indent);
					_Output.Write ("                        new FileStream(outputfile, FileMode.Create, FileAccess.Write);\n{0}", _Indent);
					_Output.Write ("            using TextWriter OutputWriter = new StreamWriter(outputStream, Encoding.UTF8);\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("			{1}.{2} Script = new () {{ \n{0}", _Indent, Script.Namespace, Script.Class);
					_Output.Write ("				_Output= OutputWriter \n{0}", _Indent);
					_Output.Write ("				}};\n{0}", _Indent);
					_Output.Write ("			Script.{1} (Parse);\n{0}", _Indent, Script.Id);
					_Output.Write ("			}}\n{0}", _Indent);
				break; }
					}
				}
			_Output.Write ("		}}\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		}
	
	/// <summary>	
	/// CommandSetMethod
	/// </summary>
	/// <param name="CommandSet"></param>
	public void CommandSetMethod (CommandSet CommandSet) {
		foreach  (var Inner in CommandSet.Entries) {
			switch (Inner._Tag ()) {
				case CommandParseType.Command: {
				  Command Cast = (Command) Inner; 
				
				CommandMethod (Cast);
				break; }
				case CommandParseType.CommandSet: {
				  CommandSet Cast = (CommandSet) Inner; 
				
				CommandSetMethod (Cast);
			break; }
				}
			}
		}
	
	/// <summary>	
	/// CommandSetHandler
	/// </summary>
	/// <param name="CommandSet"></param>
	/// <param name="Class"></param>
	public void CommandSetHandler (CommandSet CommandSet, Class Class) {
		foreach  (var Inner in CommandSet.Entries) {
			switch (Inner._Tag ()) {
				case CommandParseType.Command: {
				  Command Command = (Command) Inner; 
				
				 CommandHandler (Command, Class);
				break; }
				case CommandParseType.CommandSet: {
				  CommandSet Cast = (CommandSet) Inner; 
				
				 CommandSetHandler (Cast, Class);
			break; }
				}
			}
		}
	
	/// <summary>	
	/// CommandSetOptionClass
	/// </summary>
	/// <param name="CommandSet"></param>
	public void CommandSetOptionClass (CommandSet CommandSet) {
		foreach  (var Inner in CommandSet.Entries) {
			switch (Inner._Tag ()) {
				case CommandParseType.Command: {
				  Command Cast = (Command) Inner; 
				
				CommandOptionClass (Cast);
				break; }
				case CommandParseType.CommandSet: {
				  CommandSet Cast = (CommandSet) Inner; 
				
				CommandSetOptionClass (Cast);
			break; }
				}
			}
		}
	
	/// <summary>	
	/// CommandOptionClass
	/// </summary>
	/// <param name="Command"></param>
	public void CommandOptionClass (Command Command) {
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public class _{1} : {2} ", _Indent, Command.Id, NameDispatchType);
		foreach  (_Choice OptionC in Command.Entries) {
			switch (OptionC._Tag ()) {
				case CommandParseType.Include: {
				  Include Include = (Include) OptionC; 
				_Output.Write (",\n{0}", _Indent);
				_Output.Write ("						I{1}", _Indent, Include.Id);
			break; }
				}
			}
		_Output.Write ("{{\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	public override Goedel.Command.Type[] _Data {{get; set;}} = new Goedel.Command.Type[] {{", _Indent);
		 Separator.IsFirst = true;
		foreach  (var Entry in Command.EntryItems) {
			_Output.Write ("{1}\n{0}", _Indent, Separator);
			if (  (Entry.IsEnumerate)  ) {
				_Output.Write ("		new {1} (CommandLineInterpreter.Describe{2})", _Indent, Entry.Type, Entry.ID);
				} else {
				_Output.Write ("		new {1} ()", _Indent, Entry.Type);
				}
			}
		_Output.Write ("		}} ;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Entry in Command.EntryItems) {
			 var Item = Entry.Item;
			_Output.Write ("	/// <summary>Field accessor for {1} [{2}]</summary>\n{0}", _Indent, Entry.IsOption.If("option","parameter"), Entry.Tag);
			_Output.Write ("	public virtual {1} {2} {{\n{0}", _Indent, Entry.Type, Entry.ID);
			_Output.Write ("		get => _Data[{1}] as {2};\n{0}", _Indent, Entry.Index, Entry.Type);
			_Output.Write ("		set => _Data[{1}]  = value;\n{0}", _Indent, Entry.Index);
			_Output.Write ("		}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	public virtual string _{1} {{\n{0}", _Indent, Entry.ID);
			//			get => (_Data[#{Entry.Index}] as #{Entry.Type}).Value.To;
			_Output.Write ("		set => _Data[{1}].Parameter (value);\n{0}", _Indent, Entry.Index);
			_Output.Write ("		}}\n{0}", _Indent);
			}
		_Output.Write ("	public override DescribeCommandEntry DescribeCommand {{get; set;}} = _DescribeCommand;\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	public readonly static DescribeCommandEntry _DescribeCommand = new   () {{\n{0}", _Indent);
		_Output.Write ("		Identifier = \"{1}\",\n{0}", _Indent, Command.Tag.ToLower());
		_Output.Write ("		Brief =  \"{1}\",\n{0}", _Indent, Command.Brief);
		_Output.Write ("		HandleDelegate =  CommandLineInterpreter.Handle_{1},\n{0}", _Indent, Command.Id);
		_Output.Write ("		Lazy =  {1},\n{0}", _Indent, Command.Lazy.If("true","false"));
		if (  (Command.IsDefault) ) {
			_Output.Write ("        IsDefault = true,\n{0}", _Indent);
			}
		_Output.Write ("		Entries = new List<DescribeEntry> () {{", _Indent);
		 Separator.IsFirst = true;
		foreach  (var EntryItem in Command.EntryItems) {
			 var Item = EntryItem.Item;
			if (  EntryItem.HasEntry ) {
				_Output.Write ("{1}\n{0}", _Indent, Separator);
				if (  EntryItem.IsOption ) {
					_Output.Write ("			new DescribeEntryOption () {{\n{0}", _Indent);
					} else if (  EntryItem.IsEnumerate) {
					_Output.Write ("			new DescribeEntryEnumerate () {{\n{0}", _Indent);
					} else {
					_Output.Write ("			new DescribeEntryParameter () {{\n{0}", _Indent);
					}
				_Output.Write ("				Identifier = \"{1}\", \n{0}", _Indent, EntryItem.ID);
				_Output.Write ("				Default = {1}, // null if null\n{0}", _Indent, EntryItem.Default.QuotedOrNull());
				_Output.Write ("				Brief = {1},\n{0}", _Indent, EntryItem.Brief.Quoted());
				_Output.Write ("				Index = {1},\n{0}", _Indent, EntryItem.Index);
				_Output.Write ("				Key = \"{1}\"\n{0}", _Indent, EntryItem.Tag.ToLower());
				_Output.Write ("				}}", _Indent);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("			}}\n{0}", _Indent);
		_Output.Write ("		}};\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("	}}\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("public partial class {1} : _{2} {{\n{0}", _Indent, Command.Id, Command.Id);
		_Output.Write ("    }} // class {1}\n{0}", _Indent, Command.Id);
		}
	}
