//   Copyright © 2015 by Default Deny Security Inc.
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
using System.Collections;
using System.Collections.Generic;
using Goedel.Registry;

// To Do
//
// 1. Document!
//          I have forgotten what half these options do
// 3. Type checking
// 4. Fix brace handling anomalies (e.g. {{{D}}} does not do what it should
// 6. Write out the source file and the generation time into the output
// 7. Fix bug that causes text in comments appearing at end of line
//       to be included in output string

namespace Goedel.Tool.Script {
    
    public class ParseException : SystemException {
        public string Text;
        public override string ToString() => Text;
        }
    public class BadDirectiveException : ParseException {
        public BadDirectiveException(string TagIn) => Text = "Bad Directive [" + TagIn + "]";
        public BadDirectiveException() => Text = "Missing Directive";
        }
    public class MismatchedEndException : ParseException {
        //public MismatchedEndException() {}
        public MismatchedEndException(string Start, string End) => Text = "Mismatched End [" + Start + "/" + End + "]";
        }
    public class WrongArgumentsException : ParseException {
        public WrongArgumentsException(int Have, int Need) {
            string Defect = Have < Need ? "Insufficient" : "Too Many";
            Text = Defect + " Arguments ["+Have.ToString() + "/" + 
                Need.ToString () +"]";
            }
        }
    public class IncompleteOutputException : ParseException {
        public IncompleteOutputException(string TagIn) => Text = "Incomplete Output Specifier [" + TagIn + "]";
        }

    class StackItem {
        public int  Command;
        public int  State;
        public string Indent;
        public int  AutoClose;
        public string  SwitchVar;
        public string  SwitchType;

        public StackItem(int TagIn, int StateIn, string IndentIn, string SwitchVarIn, string SwitchTypeIn) {
            Command = TagIn;
            State = StateIn;
            Indent = IndentIn;
            AutoClose = -1;
            SwitchVar = SwitchVarIn;
            SwitchType = SwitchTypeIn;
            }
        }

    public class Script {

        public DateTime GenerateTime = DateTime.UtcNow;

        public Script() {
            }

        class ScriptCommand {
            public string Tag;
            public string Text;
            public string EndText;
            public int Next;
            public int Parameters;
            public int Stack;
            public int Allowed;
            public int Action;

            public ScriptCommand() {
                }

            public ScriptCommand(
                    string tag,
                    string text,
                    string endText,
                    int parameters,
                    int next,
                    int stack,
                    int allowed) {
                Tag = tag;
                EndText = endText;
                Text = text;
                Parameters = parameters;
                Next = next;
                Stack = stack;
                Allowed = allowed;

                if ((text != null) & text[0] == '%') {
                    Action = Convert.ToInt32 (text.Substring (1));
                    }
                else {
                    Action = Parameters;
                    }
                }
            }

        const string ClassText = XClassText;


        const string PClassText = XClassText;


        const string XClassText =
            "using System;\n" +
            "using System.IO;\n" +
            "using System.Collections.Generic;\n" +
            "using Goedel.Registry;\n" +
            "namespace {0} {{\n" +
            "\tpublic partial class {1} : global::Goedel.Registry.Script {{\n";

        const string ScriptText = "// Script Syntax Version: {0}";

        const string UsingText = "using {0};";

        const string FileText =
                "\n\n{4}//\n{4}// {0}\n{4}//\n{4}public static void {0} ({2} {3}) {{ /* File  */\n{4}\tusing (var _Output = new StreamWriter ({1})) {{\n{4}\t\tvar _Indent = \"\"; ";

        //#file 0MakeSiteDocs 1WebKey 2"Guide/key.md" 3CreateWeb 4Examples
        const string XFileText = "\n\n{5}//\n{5}// {1}\n{5}//\n" +
            "{5}public static void {1}({3} {4}) {{ /* XFile  */\n" +
            "{5}\t\tusing (var _Output = new StreamWriter({2})) {{\n" +
            "{5}\t\tvar obj = new {0}() {{ _Output = _Output, _Indent = \"\", _Filename = {2} }};\n" +
            "{5}\t\tobj._{1}({4});\n" +
            "{5}\t\t}}\n" +
            "{5}\t}}\n" +
            "{5}public void _{1}({3} {4}) {{\n"
            ;

        //#file 0MakeSiteDocs 1WebKey 2"Guide/key.md" 3CreateWeb 4Examples
        const string ZFileText = "\n\n{4}//\n{4}// {1}\n{4}//\n" +
            "{4}public static void {1}({0} {3}) {{ /* XFile  */\n" +
            "{4}\t\tusing ({3}._Output = new StreamWriter({2})) {{\n" +
            "{4}\t\t{3}._{1}({3});\n" +
            "{4}\t\t}}\n" +
            "{4}\t}}\n" +
            "{4}public void _{1}({0} {3}) {{\n"
            ;

        //        const string XFileText = @"

        //{5}///
        //{5}/// {1}
        //{5}///
        //public static void {1}({3} {4}) {{ //* XFile  *//
        //    using (var _Output = new StreamWriter(""{2}"")) {
        //        var obj = new {0}() { _Output = _Output, _Indent = "", _Filename = ""{2}"" };
        //        obj._{1}({4});
        //        }
        //    }
        //public void _{1}({3} {4}) { 
        //";

        const string MethodText =
            "\n\n{3}//\n{3}// {0}\n{3}//\n{3}public void {0} ({1} {2}) {{";

        const string Method2Text =
            "\n\n{5}//\n{5}// {0}\n{5}//\n{5}public void {0} ({1} {2}, {3} {4}) {{";
        const string Method3Text =
            "\n\n{7}//\n{7}// {0}\n{7}//\n{7}public void {0} ({1} {2}, {3} {4}, {5} {6}) {{";
   
        const string MethodnText =
            "\n\n{1}//\n{1}// {0}\n{1}//\n";     

        const string CommentText = "//{0}";

        const string PrefixText = "_Indent = {0} + _Indent; {{";
        const string DocumentText = "///{0}";
        const string InstructionText = "{0}";
        const string IfText = "if ( {0} ) {{";
        const string ElseIfText = "}} else if ( {0}) {{";
        const string ElseText = "}} else {{";
        const string ForEachText = "foreach {0} {{";
        //const string FilterText = "foreach (var _{0} in {1}) {{  if (_{0}._Tag() == {2}.{3}) {{ var {0} = ({3}) _{0}; ";
        //const string FilterText = "foreach (var _{2} in {3}) {{  if (_{2}._Tag() == {0}.{1}) {{ var {2} = ({1}) _{2}; ";
        //const string FilterText = "foreach (var _{2} in {3}) {{  if (_{2}.GetType() == typeof ({1})) {{ var {2} = ({1}) _{2}; ";
        const string FilterText = "foreach (var _{1} in {2}) {{  if (_{1}.GetType() == typeof ({0})) {{ var {1} = ({0}) _{1}; ";

        const string ForText = "for {0} {{";
        const string SwitchText = "switch ({0}) {{";
        const string CaseText = "case {0}: {{";
        const string EndCaseText = "break; }";
        const string DefaultText = "default : {{";
        const string CallText = "{0} ({1});";
        const string IndentText = "_Indent = _Indent + \"\\t\";";
        const string OutdentText = "_Indent = _Indent.Remove (0,1);";

        const string SwitchCastText = "switch ({1}._Tag ()) {{";
        const string CaseCastText = "case {2}.{0}: {{\n{4}  {0} {1} = ({0}) {3}; ";
        const string CaseNoCastText = "case {2}.{0}: {{ ";
        const string IfAsText = "if (({0} as {1}) != null) { var {1} {2} = {0} as {1} ;";
        const string ElseAsText = "} else if (({0} as {1}) != null) { var {1} {2} = {0} as {1} ;";

        /// <summary>
        /// The command table state machine. 
        /// 
        /// Document	= Comment*  ClassBlock
        /// ClassBlock	= Class (Comment | Instruction | MethodBlock)* End
        /// MethodBlock	= Method TextBlock End
        /// TextBlock	= Text | Instruction | IfBlock | ForBlock
        /// IfBlock		= If TextBlock* ElseIf* TextBlock* Else? TextBlock* End
        /// ForBlock	= (ForEach | For) TextBlock* End
        /// </summary>

        static ScriptCommand[] Commands = {
			//					Command		Text				    End		Params	Next,	Stack,	Allowed
			new ScriptCommand ("!",         CommentText,            null,   1,      -1,     0,      0),
            new ScriptCommand ("%",         InstructionText,        null,   1,      -1,     0,      3),
            new ScriptCommand ("//",        DocumentText,           null,   1,      -1,     0,      3),
            new ScriptCommand ("script",    ScriptText,             null,   1,      0,      0,      6),
            new ScriptCommand ("license",   "%13",                  null,   1,      -1,     0,      0),
            new ScriptCommand ("using",     UsingText,              null,   1,      0,      0,      6),
            new ScriptCommand ("prefix",    PrefixText,              "}",   1,      0,      1,      3),
            new ScriptCommand ("class",     ClassText,          "\t\t}\n\t}",   2,      1,      2,      6),
            new ScriptCommand ("pclass",    PClassText,         "\t\t}\n\t}",   2,      1,      2,      6),
            new ScriptCommand ("xclass",    XClassText,         "\t\t}\n\t}",   2,      1,      2,      6),

            new ScriptCommand ("file",      FileText,             "\t\t}\n\t\t\t}",  4,      2,      2,      1),
            new ScriptCommand ("xfile",     XFileText,            "\t\t\t}",  5,      2,      2,      1),
            new ScriptCommand ("zfile",     ZFileText,            "\t\t\t}",  4,      2,      2,      1),

            new ScriptCommand ("method",    MethodText,             "\t}",  3,      2,      1,      1),
            new ScriptCommand ("method2",   Method2Text,            "\t}",  5,      2,      1,      1),
            new ScriptCommand ("method3",   Method3Text,            "\t}",  7,      2,      1,      1),
            new ScriptCommand ("block",     MethodnText,            "",     1,      2,      1,      1),
            new ScriptCommand ("if",        IfText,                 "\t}",  1,      3,      1,      3),
            new ScriptCommand ("elseif",    ElseIfText,             null,   1,      3,      0,      4),
            new ScriptCommand ("else",      ElseText,               null,   0,      4,      0,      4),
            new ScriptCommand ("for",       ForText,                "\t}",  1,      2,      1,      3),
            new ScriptCommand ("foreach",   ForEachText,            "\t}",  1,      2,      1,      3),
            new ScriptCommand ("filter",    FilterText,      "\t\t}\n\t}",  3,      2,      1,      3),
            new ScriptCommand ("switch",    SwitchText,             "\t}",  1,      5,      1,      3),
            new ScriptCommand ("case",      CaseText,       EndCaseText,    1,      5,      0,      5),


            new ScriptCommand ("ifas",      IfAsText,               "\t}",  3,      7,      1,      3),
            new ScriptCommand ("elseas",    ElseAsText,              null,  3,      7,      0,      7),


            new ScriptCommand ("switchcast","%10",                  "\t}",  1,      5,      1,      3),
            new ScriptCommand ("casecast",  "%11",          EndCaseText,    2,      5,      0,      5),

            new ScriptCommand ("default",   DefaultText,     EndCaseText,   1,      6,      0,      5),
            new ScriptCommand ("call",      CallText,               null,   2,      -1,     0,      3),

            new ScriptCommand ("indent",    IndentText,             null,   0,      -1,     0,      3),
            new ScriptCommand ("outdent",   OutdentText,            null,   0,      -1,     0,      3),
            new ScriptCommand ("end",       "}",                    null,   0,      -1,     -1,     2)
            };

        static bool[,] AllowedTransitions = {
			//	0		1		2		3		4		5       6       7
			{	true,	true,	true,	true,	true,	true,   true,   true},		// 0 ! always allowed
			{	false,	true,	false,	false,	false,	false,  false,  false},		// 1 method only allows in class
			{	false,	true,	true,	true,	true,	true,   true,   false},		// 2 end allowed except before class
			{	false,	false,	true,	true,	true,	true,   true,   true},		// 3 %, if, for, switch
			{	false,	false,	false,	true,	false,	false,  false,  false},		// 4 elseif
			{	false,	false,	false,	false,	false,	true,   false,  false},		// 5 case
			{	true,	false,	false,	false,	false,	false,  false,  false},     // 6 default
            {   false,  false,  false,  false,  false,  false,  false,  true},		// 7 ifas
            };

        public  string Indent = "";
        public  int State = 0;
        public  string SwitchVar = null;
        public string SwitchType = null;

        public string Comment = "//";

        List <StackItem> Stack = new List<StackItem> ();
        TextWriter Writer;
        int ln = 0;

        public void Process(Stream Input, string filename, TextWriter Writer) {
            var Reader = new StreamReader(Input);
            Process(filename, Reader, Writer);
            }

        /// <summary>
        /// Process the specified input stream and write data to the output stream.
        /// </summary>
        /// <param name="Filename">The file name of the source file.</param>
        /// <param name="Reader">The input stream.</param>
        /// <param name="Writer">The output stream.</param>
        public void Process(string Filename, TextReader Reader, TextWriter Writer) {
            this.Writer = Writer;

            Stack.Add(new StackItem(0, State, Indent, SwitchVar, SwitchType));

            //ArrayList Stack = new ArrayList();
            for (string line = Reader.ReadLine(); line != null; line = Reader.ReadLine()) {
                try {
                    ln++;
                    //this.Writer.WriteLine("{2}// {0} ", line, state, Indent);

                    if (line == "") {
                        if (State >= 2) {
                            ProcessLine(line);
                            }
                        }
                    else if (line[0] == '#') {			// should change to first nonblank
                        if (line == "#") {
                            throw new BadDirectiveException();
                            }
                        else if ((line[1] == '{') | (line[1] == '#')) {
                            ProcessLine(line);
                            }
                        else {

                            if (Filename != null) {
                                //Writer.WriteLine ("#line {0} \"{1}\"", ln, filename);
                                }
                            ProcessCommand(line);
                            }
                        }
                    else if (State >= 2) {
                        ProcessLine(line);
                        }
                    }

                catch (ParseException Exception) {
                    Console.WriteLine("{0} at line {1}", Exception.Text, ln);
                    }
                }
            }

        void ProcessCommand(
                   string line) {
            char[] splitchars = { ' ', '\t' };
            string[] arguments = line.Split(splitchars);

            string tag = arguments[0].Substring(1);
            //int match = -1;
            bool search = true;

            for (int i = 0; (i < Commands.Length) & search; i++) {
                if ((Commands[i].Tag == tag)) {
                    if (true | AllowedTransitions[Commands[i].Allowed, State]) {
                        search = false;
                        StackItem Top = Stack[Stack.Count - 1];
                        if (Commands[i].Stack < 0) {
                            Stack.RemoveAt(Stack.Count - 1);
                            State = Top.State;
                            Indent = Top.Indent;
                            SwitchVar = Top.SwitchVar;
                            SwitchType = Top.SwitchType;
                            //Writer.WriteLine ("// Pop {0} - {1}", Stack[index], Commands[Top].Command);
                            if (arguments.Length > 1) {
                                //Writer.WriteLine ("// Argument {0}", arguments[1]);
                                if (arguments[1] != Commands[Top.Command].Tag) {
                                    throw new MismatchedEndException (Commands[Top.Command].Tag, arguments[1]);
                                    }
                                }
                            if (Top.AutoClose >= 0) {
                                // should outdent for this
                                Writer.Write(Indent);
                                Writer.WriteLine(Commands[Top.AutoClose].EndText);
                                }

                            Writer.Write(Indent);
                            Writer.WriteLine(Commands[Top.Command].EndText);
                            }
                        else {
                            if (Commands[i].Stack > 0) {
                                Stack.Add(new StackItem(i, State, Indent, SwitchVar, SwitchType));
                                //Indent = Indent + "\t";
                                }
                            else {
                                if (Top.AutoClose >= 0) {
                                    // should outdent for this
                                    Writer.Write(Indent);
                                    Writer.WriteLine(Commands[i].EndText);
                                    }
                                if (Commands[i].EndText != null) {
                                    Top.AutoClose = i;
                                    }
                                }

                            if (Commands[i].Next > 0) {
                                State = Commands[i].Next;
                                }
                            Writer.Write(Indent);

                            switch (Commands[i].Action) {
                                case 0: {
                                        Writer.WriteLine(Commands[i].Text, Indent);
                                        break;
                                        }
                                case 1: {
                                        String substring = line.Substring(1 + tag.Length);
                                        Writer.WriteLine(Commands[i].Text, substring, Indent);
                                        break;
                                        }
                                case 2: {
                                        CheckArguments (arguments.Length, 2);
                                        Writer.WriteLine(Commands[i].Text, arguments[1], arguments[2], 
                                            Indent);
                                        break;
                                        }
                                case 3: {
                                        CheckArguments (arguments.Length, 3);
                                        Writer.WriteLine(Commands[i].Text, arguments[1], arguments[2], 
                                            arguments[3], Indent);
                                        break;
                                        }
                                case 4: {
                                        CheckArguments (arguments.Length, 4);
                                        Writer.WriteLine(Commands[i].Text, arguments[1], arguments[2], 
                                            arguments[3], arguments[4], Indent);
                                        break;
                                        }
                                case 5: {
                                        CheckArguments (arguments.Length, 5);
                                        Writer.WriteLine(Commands[i].Text, arguments[1], arguments[2], 
                                            arguments[3], arguments[4], arguments[5], Indent);
                                        break;
                                        }
                                case 6: {
                                        CheckArguments (arguments.Length, 6);
                                        Writer.WriteLine(Commands[i].Text, arguments[1], arguments[2], 
                                            arguments[3], arguments[4], arguments[5], arguments[6], Indent);
                                        break;
                                        }

                                case 7: {
                                        CheckArguments (arguments.Length, 7);
                                        Writer.WriteLine(Commands[i].Text, arguments[1], arguments[2], 
                                            arguments[3], arguments[4], arguments[5], arguments[6], 
                                            arguments[7], Indent);
                                        break;
                                        }

                                case 10: {
                                        CheckArguments (arguments.Length, 2);
                                        Writer.WriteLine(SwitchCastText, arguments[1], arguments[2]);
                                        SwitchVar = arguments[2]; 
                                        SwitchType = arguments[1];
                                        break;
                                        }
                                case 11: {
                                    CheckArguments(arguments.Length, 2);
                                    if (arguments[2] == "null") {
                                        Writer.WriteLine(CaseNoCastText, arguments[1], arguments[2], SwitchType, SwitchVar, Indent);
                                        }
                                    else {
                                        Writer.WriteLine(CaseCastText, arguments[1], arguments[2], SwitchType, SwitchVar, Indent);
                                        }
                                    break;
                                    }
                                case 12: {
                                        CheckArguments (arguments.Length, 2);
                                        Writer.WriteLine(ClassText, arguments[1], arguments[2], GenerateTime);
                                        break;
                                        }

                                case 13: {
                                        CheckArguments (arguments.Length, 1);
                                        Registry.Boilerplate.License (Writer, "//  ", arguments[1]);
                                        Writer.WriteLine ();
                                        break;
                                        }
                                case 14: {

                                        break;
                                        }
                                }
                            if (Commands[i].Stack == 1) {
                                Indent = Indent + "\t";
                                }
                            else if (Commands[i].Stack == 2) {
                                Indent = Indent + "\t\t";
                                }
                            }
                        }
                    else {
                        //match = i;
                        }
                    }
                }
            }

        void CheckArguments(int Have, int Need) {

            if (Have - 1 != Need) {
                throw new WrongArgumentsException(Have - 1, Need);
                }
            }

        static string EscapeCharacter(char c) {
            if ((c == '"') | (c == '\\')) {
                return "\\" + c;
                }
            else if (c == '{') {
                return "{{";
                }
            else if (c == '}') {
                return "}}";
                }
            else {
                return c.ToString();
                }
            }

        static string CEscapeCharacter(char c) {
            if ((c == '"') | (c == '\\')) {
                return "\\" + c;
                }
            else {
                return c.ToString();
                }
            }

        /// <summary>
        /// Replace Regular expressions in a line of characters and write the
        /// result to the Writer.
        ///
        /// Expressions of the form (c* #[ c* (| c*)? #])* are transformed 
        /// into the corresponding text formatting strings.
        /// </summary>
        /// <param name="Writer"></param>
        /// <param name="line"></param>

        // State 0
        //		# -> 1
        //		. -> 0		line (.)
        // State 1
        //		{ -> 2
        //		! -> 4
        //		. -> 0		line (.)
        // State 2
        //		| -> 3
        //		} -> 0
        //		. -> 2		id (.)
        // State 3
        //		} -> 0
        //		. -> 3		format (.)
        // State 4
        //		. -> 4		comment (.)
        // Terminal states
        // 0 - append "\n"
        // 1 - append "#\n"
        // 2 - error
        // 3 - error
        // 4 - append the comment, no newline
        void ProcessLine(string line) {
            int state = 0;
            string escapedLine = "", id = "", format = "";
            ArrayList IdStack = new ArrayList();
            foreach (char c in line) {
                switch (state) {
                    case 0: {
                            if (c == '#') {
                                state = 1;
                                }
                            else {
                                escapedLine = escapedLine + EscapeCharacter(c);
                                }
                            break;
                            }
                    case 1: {
                            if (c == '{') {
                                state = 2;
                                id = "";
                                format = "";
                                }
                            else if (c == '!') {
                                // this is a bug, need to transfer the comment to the output file
                                // stop it breaking the output for time being
                                //escapedLine = escapedLine + "//";
                                state = 4;
                                }
                            else {
                                escapedLine = escapedLine + EscapeCharacter(c);
                                state = 0;
                                }
                            break;
                            }
                    case 2: {
                            if (c == ':') {
                                state = 3;
                                }
                            else if (c == '}') {
                                int count = IdStack.Count + 1;
                                escapedLine = escapedLine + "{" + count.ToString() + "}";
                                // forget the C Escaped version from this point on as we won't use it
                                IdStack.Add(id);
                                state = 0;
                                }
                            else {
                                id = id + c;
                                }
                            break;
                            }
                    case 3: {
                            if (c == '}') {
                                int count = IdStack.Count + 1;
                                escapedLine = escapedLine + "{" + count.ToString() +
                                        ":" + format + "}";
                                IdStack.Add(id);
                                state = 0;
                                }
                            else {
                                format = format + c;
                                }
                            break;
                            }
                    case 4: {
                            escapedLine = escapedLine + EscapeCharacter(c);
                            break;
                            }
                    }
                }
            switch (state) {
                case 0:
                case 1: {
                        escapedLine = escapedLine + "\\n{0}";
                        break;
                        }
                case 4: {
                        break;
                        }
                default: {
                        Error("Unfinished output production", ln);
                        break;
                        }
                }
            if (IdStack.Count == 0) {
                // Have no parameters so do not escape braces.
                Writer.Write(Indent);
                Writer.Write("_Output.Write (\"{0}\", _Indent);\n", escapedLine);
                }
            else {
                // May have to go back and fix brace escaping later due to the
                // {{{D}}} ambiguity.
                Writer.Write(Indent);
                Writer.Write("_Output.Write (\"{0}\", _Indent", escapedLine);
                foreach (string s in IdStack) {
                    Writer.Write(", {0}", s);
                    }
                Writer.Write(");\n");
                }
            }

        static void Error(string s, int line) => Console.Error.WriteLine("***ERROR: {0} {1}", s, line);
        }
    }
