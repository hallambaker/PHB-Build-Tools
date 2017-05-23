using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Utilities;
using Goedel.Registry;

namespace Goedel.Command {

    /// <summary>
    /// Base class for command line interpreters
    /// </summary>
    public abstract class CommandLineInterpreterBase {
        /// <summary></summary>
        public static SortedDictionary<string, DescribeCommand> Entries;
        /// <summary></summary>
        public static DescribeCommandEntry DefaultCommand;
        /// <summary></summary>
        public static string Description = "<Not specified>";
        /// <summary></summary>
        public static char FlagIndicator = '/';


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Dispatch"></param>
        /// <param name="args"></param>
        /// <param name="index"></param>
        public static void Brief (DispatchShell Dispatch, string[] args, int index) {
            Brief();
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Dispatch"></param>
        /// <param name="args"></param>
        /// <param name="index"></param>
        public static void About (DispatchShell Dispatch, string[] args, int index) {
            FileTools.About();
            }



        /// <summary>
        /// 
        /// </summary>
        public static void Brief () {
            Console.WriteLine(Description);
            Console.WriteLine("");

            if (DefaultCommand != null) {
                DefaultCommand.Describe(FlagIndicator);

                }
            foreach (var Entry in Entries) {
                if (!Entry.Value.IsDefault) {
                    Entry.Value.Describe(FlagIndicator);
                    }
                }

            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Args"></param>
        /// <param name="Index"></param>
        /// <param name="Options"></param>
        public static void ProcessOptions (string[] Args, int Index, Dispatch Options) {
            var Describe = Options.DescribeCommand;
            var CommandLex = new CommandLex();

            var Parameter = 0;

            // Set all data slots to their default value
            for (var j = 0; j < Describe.Entries.Count; j++) {
                if (Describe.Entries[j].Default != null) {
                    Options._Data[j].Parameter(Describe.Entries[j].Default);
                    }
                }

            for (var i = Index; i < Args.Length; i++) {
                var Arg = Args[i];
                var Token = CommandLex.GetToken(Arg);

                switch (Token) {
                    case CommandLex.Token.Empty: {
                        break;
                        }
                    case CommandLex.Token.Flag: {
                        var Entry = Match(Describe.Entries, CommandLex.Flag) as DescribeEntryValue;
                        Assert.NotNull(Entry, UnknownOption.Throw);
                        var Data = Options._Data[Entry.Index];
                        if ((i + 1 < Args.Length) && !IsFlagged(Args[i + 1])) {
                            i++;
                            Arg = Args[i];

                            SetValue(Data, Args[i]);
                            }
                        if (Data as _Flag != null) {
                            (Data as _Flag).Value = !CommandLex.Not;
                            Data.ByDefault = false;
                            }
                        break;
                        }
                    case CommandLex.Token.FlagValue: {
                        var Entry = Match(Describe.Entries, CommandLex.Flag) as DescribeEntryValue; ;
                        Assert.NotNull(Entry, UnknownOption.Throw);

                        SetValue(Options._Data[Entry.Index], CommandLex.Value);

                        break;
                        }
                    case CommandLex.Token.Value: {
                        var Search = true;
                        for (var j = Parameter; Search & j < Describe.Entries.Count; j++) {
                            var Entry = Describe.Entries[j] as DescribeEntryParameter;
                            if (Entry != null) {
                                SetValue(Options._Data[Entry.Index], CommandLex.Value);
                                j++;
                                Search = false;
                                }
                            }
                        break;
                        }
                    }
                }
            }


        static void SetValue (Type Data, string Value) {
            Data.Parameter(Value); // Hack: consolidate
            Data.ByDefault = false; // Has been set explicitly
            }

        static DescribeEntry Match (List<DescribeEntry> Entries, string Flag) {
            foreach (var Entry in Entries) {
                if (Entry.Key.ToLower() == Flag.ToLower()) {
                    return Entry;
                    }
                }
            return null;
            }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Dispatch"></param>
        public static void DescribeValues (Dispatch Dispatch) {
            }


        public static bool IsFlagged (string Text) {
            if (Text == null) {
                return false;
                }
            if (Text.Length <= 0) {
                return false;
                }
            return Text[0] == '-' | Text[0] == '/';
            }


        /// <summary>
        /// The main dispatch point
        /// </summary>
        /// <param name="Entries"></param>
        /// <param name="Dispatch"></param>
        /// <param name="Args"></param>
        /// <param name="Index"></param>
        public void Dispatcher (SortedDictionary<string, DescribeCommand> Entries,
                DispatchShell Dispatch, string[] Args, int Index) {
            // NYI: This should really be set up to take a Command set description
            // as the input.

            

            if (Args.Length == 0) {
                Assert.NotNull(DefaultCommand, UnknownCommand.Throw);
                DefaultCommand.HandleDelegate(Dispatch, Args, Index);
                return;
                }

            Assert.True(Index < Args.Length, UnknownCommand.Throw);

            var Arg = Args[Index];
            Assert.True(Arg.Length > 0, UnknownCommand.Throw); // Should never happen.
            var Flagged = IsFlagged(Arg);
            var Command = Flagged ? Arg.Substring(1).ToLower() : Arg.ToLower();

            if (DefaultCommand != null & !Flagged) {
                DefaultCommand.HandleDelegate(Dispatch, Args, Index);
                return;
                }

            // NYI: no, it could be a default command and an option.
            Assert.True(Entries.TryGetValue(Command, out var DescribeCommand), UnknownCommand.Throw);
            switch (DescribeCommand) {

                case DescribeCommandEntry DescribeCommandEntry: {
                    DescribeCommandEntry.HandleDelegate(Dispatch, Args, Index + 1);
                    break;
                    }
                case DescribeCommandSet DescribeCommandSet: {
                    Dispatcher(DescribeCommandSet.Entries, Dispatch, Args, Index + 1);
                    break;
                    }
                }

            }
        }


    /// <summary>
    /// 
    /// </summary>
    public abstract class DescribeEntry {
        /// <summary></summary>
        public string Identifier { get; set; }
        /// <summary></summary>
        public string Brief { get; set; }
        /// <summary></summary>
        public string Default { get; set; }
        /// <summary></summary>
        public string Key { get; set; }
        }

    /// <summary>
    /// 
    /// </summary>
    public abstract class DescribeCommand : DescribeEntry {
        /// <summary></summary>
        public bool IsDefault = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FlagIndicator"></param>
        public abstract void Describe (char FlagIndicator);
        }

    /// <summary>
    /// 
    /// </summary>
    public class DescribeCommandEntry : DescribeCommand {

        /// <summary></summary>
        public HandleDelegate HandleDelegate { get; set; }
        /// <summary></summary>
        public bool Lazy { get; set; } = false;
        //public Parser Parser { get; set; } = null;
        /// <summary></summary>
        public List<DescribeEntry> Entries { get; set; }
        /// <summary></summary>
        public int Index;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FlagIndicator"></param>
        public override void Describe (char FlagIndicator) {
            Console.WriteLine("{0}{1}", FlagIndicator, Identifier);
            foreach (var Entry in Entries) {

                switch (Entry) {
                    case DescribeCommandEntry SubCommand: {
                        Console.WriteLine("    {0}{1}  {2}", FlagIndicator, Entry.Key, SubCommand.Brief);
                        break;
                        }
                    case DescribeEntryParameter Parameter: {
                        Console.WriteLine("    {0}   {1}", Entry.Key, Parameter.Brief);
                        break;
                        }
                    case DescribeEntryOption Option: {
                        Console.WriteLine("    {0}{1}   {2}", FlagIndicator, Entry.Key, Option.Brief);
                        break;
                        }
                    }
                }
            }


        }

    /// <summary>
    /// 
    /// </summary>
    public class DescribeCommandSet : DescribeCommand {
        /// <summary></summary>
        public SortedDictionary<string, DescribeCommand> Entries;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FlagIndicator"></param>
        public override void Describe (char FlagIndicator) {
            Console.WriteLine("{0}", Identifier);
            foreach (var Entry in Entries) {
                switch (Entry.Value) {
                    case DescribeCommandEntry SubCommand: {
                        Console.WriteLine("    {0}{1}   {2}", FlagIndicator, Entry.Key, SubCommand.Brief);
                        break;
                        }
                    case DescribeCommandSet Parameter: {
                        Console.WriteLine("    {0}{1}", Entry.Key, Parameter.Brief);
                        break;
                        }

                    }
                }
            }
        }

    /// <summary>
    /// 
    /// </summary>
    public class DescribeEntryValue : DescribeEntry {
        /// <summary></summary>
        public int Index { get; set; } // Index into array of Type
        }

    /// <summary>
    /// 
    /// </summary>
    public class DescribeEntryOption : DescribeEntryValue {
        }

    /// <summary>
    /// 
    /// </summary>
    public class DescribeEntryParameter : DescribeEntryValue {
        }

    /// <summary>
    /// 
    /// </summary>
    public abstract class DispatchShell : Dispatch{

        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Dispatch"></param>
    /// <param name="args"></param>
    /// <param name="index"></param>
    public delegate void HandleDelegate (DispatchShell Dispatch, string[] args, int index);



  //  /// <summary>Type registry entry</summary>
  //  public class RegistryEntry {
  //      /// <summary>the tag</summary>
  //      public string Tag;

  //      /// <summary>Index of the tag</summary>
  //      public int Index;
  //      }

  //  /// <summary>Type registry</summary>
  //  public class Registry {

  //      /// <summary>The tagged registry entries</summary>
  //      List<RegistryEntry> Entries = new List<RegistryEntry>();

  //      /// <summary>
  //      /// Register an entry
  //      /// </summary>
  //      /// <param name="Tag">The tag value</param>
  //      /// <param name="Index">The position in the definition array</param>
  //      public void Register(string Tag, int Index) {
  //          RegistryEntry Entry = new RegistryEntry() {
  //              Tag = Tag,
  //              Index = Index
  //              };
  //          Entries.Add(Entry);
  //          }

  //      /// <summary>
  //      /// Find the first matching entry
  //      /// </summary>
  //      /// <param name="Match">The tag to match</param>
  //      /// <returns>The match index.</returns>
		//public int Find(string Match) {
  //          RegistryEntry Entry = Entries.Find(delegate (RegistryEntry TT) { return TT.Tag == Match; });

  //          if (Entry == null) {
  //              throw new Exception("Unknown option: " + Match);
  //              }
  //          return Entry.Index;
  //          }

  //      }
    }
