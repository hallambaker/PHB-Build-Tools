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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goedel.Tool.Command {
    public partial class _Choice {
        public Command DefaultCommand = null;
        public string Brief = "<Unspecified>";
        public string Default = null;

        public _Choice Parent;
        CommandParse _CommandParse;
        public CommandParse CommandParse {
            get => _CommandParse ?? Parent.CommandParse;
            set => _CommandParse = value;
            }

        public void Process (List<_Choice> Options) {
            foreach (var Entry in Options) {
                switch (Entry) {
                    case Brief EntryCast: {
                        Brief = EntryCast.Text;
                        break;
                        }
                    case Default EntryCast: {
                        Default = EntryCast.Text;
                        break;
                        }
                    }
                }
            }
        }

    public partial class CommandParse : Goedel.Registry.Parser {
        public bool Main = true;
        public bool Builtins = true;
        public bool Catcher = false;
        public bool DeclareRegistry = false;

        /// <summary>Initialize.</summary>
        public override void Init () {
            foreach (var Entry in Top) {
                Entry.CommandParse = this;
                }
            _InitChildren();
            }
        }

    public partial class Class {
        public string Description = "<Unknown>";
        public About About = null;
        public bool Main = true;

        public override void Init (_Choice Parent) {
            this.Parent = Parent;
            base.Init(Parent);
            foreach (var Entry in Entries) {
                switch (Entry) {
                    case Brief EntryCast: {
                        Description = EntryCast.Text;
                        break;
                        }
                    case About Cast: {
                        About = Cast;
                        break;
                        }
                    case Library Cast: {
                        Main = false;
                        break;
                        }
                    }
                }
            }
        }

    public partial class CommandSet {
        public override void Init (_Choice Parent) {
            this.Parent = Parent;
            base.Init(Parent);
            }
        }


    public partial class Command {
        public List<EntryItem> EntryItems = new List<EntryItem>();
        public bool IsDefault;
        public bool Lazy = false;
        public Parser Parser = null;
        public List<Generator> Generator = new List<Generator>();
        public List<Script> Script = new List<Script>();

        public override void Init (_Choice Parent) {
            base.Init(Parent);
            this.Parent = Parent;
            int Index = 0;
            foreach (var Entry in Entries) {
                Entry.Init(this);
                switch (Entry) {
                    case Brief EntryCast: {
                        Brief = EntryCast.Text;
                        break;
                        }
                    case Include Include: {
                        var OptionSet = Include.Id.Definition as OptionSet;
                        foreach (var SubEntry in OptionSet.Options) {
                            switch (SubEntry) {
                                case Option Option: {
                                    EntryItems.Add(new EntryItem(Option) {
                                        Index = Index++
                                        });
                                    break;
                                    }
                                }
                            }
                        break;
                        }
                    case Parameter Parameter: {
                        EntryItems.Add(new EntryItem(Parameter) {
                            Index = Index++
                            });
                        break;
                        }
                    case Option Option: {
                        EntryItems.Add(new EntryItem(Option) {
                            Index = Index++
                            });
                        break;
                        }
                    case DefaultCommand EntryCast: {
                        Parent.DefaultCommand = this;
                        IsDefault = true;
                        break;
                        }
                    case Lazy EntryCast: {
                        Lazy = true;
                        EntryItems.Add(new EntryItem(EntryCast) {
                            Index = Index++
                            });
                        break;
                        }
                    case Parser EntryCast: {
                        Parser = EntryCast;
                        CommandParse.DeclareRegistry = true;
                        EntryItems.Add(new EntryItem(Parser) {
                            Index = Index++
                            });
                        break;
                        }
                    case Generator EntryCast: {
                        CommandParse.DeclareRegistry = true;
                        Generator.Add(EntryCast);
                        break;
                        }
                    case Script EntryCast: {
                        CommandParse.DeclareRegistry = true;
                        EntryItems.Add(new EntryItem(EntryCast) {
                            Index = Index++
                            });
                        Script.Add(EntryCast);
                        break;
                        }
                    }
                }
            }
        }


    public class EntryItem {
        /// <summary>
        /// 
        /// </summary>
        public _Choice Item { get; set; }
        public int Index { get; set; }
        public string ID { get; set; }
        public string Tag { get; set; }
        public string Type { get; set; }
        public string Default { get; set; } = "Default";
        public string Brief { get; set; } = "Brief";
        public bool IsOption;
        public bool HasEntry = true;


        public EntryItem (Option Option) {
            Item = Option;
            ID = Option.Name.ToString();
            Tag = Option.Command;
            Type = Option.Type.ToString();
            IsOption = true;
            Default = Option.Default;
            Brief = Option.Brief;
            }

        public EntryItem (Parameter Parameter) {
            Item = Parameter;
            ID = Parameter.Name.ToString();
            Tag = "";
            Type = Parameter.Type.ToString();
            IsOption = false;
            Default = Parameter.Default;
            Brief = Parameter.Brief;
            }

        public EntryItem (Script Script) {
            Item = Script;
            ID = Script.Id.ToString();
            Tag = Script.Extension;
            Type = "NewFile";
            IsOption = true;
            Default = Script.Default;
            Brief = Script.Brief;
            }

        public EntryItem (Parser Parser) {
            Item = Parser;
            ID = Parser.Class.ToString();
            Tag = "";
            Type = "ExistingFile";
            IsOption = false;
            Default = Parser.Default;
            Brief = Parser.Brief;
            }

        public EntryItem (Lazy Lazy) {
            Item = Lazy;
            ID = Lazy.Name.ToString();
            Tag = Lazy.Tag;
            Type ="Flag";
            IsOption = false;
            Default = Lazy.Default;
            Brief = Lazy.Brief;
            HasEntry = false;
            }




        }


    public partial class Parameter {
        public override void Init (_Choice Parent) {
            base.Init(Parent);
            Process(Modifier);
            }
        }
    public partial class Option {
        public override void Init (_Choice Parent) {
            base.Init(Parent);
            Process(Modifier);
            }
        }

    public partial class Lazy {
        public override void Init (_Choice Parent) {
            base.Init(Parent);
            Process(Modifier);
            }
        }

    public partial class Parser {
        public override void Init (_Choice Parent) {
            base.Init(Parent);
            Process(Modifier);
            }
        }

    public partial class Script {
        public override void Init (_Choice Parent) {
            base.Init(Parent);
            Process(Modifier);
            }
        }


    }
