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
using Goedel.Utilities;

namespace Goedel.Tool.Command {
    public partial class _Choice {
        public Command DefaultCommand = null;
        public string Brief = "<Unspecified>";
        public string Default = null;
        public virtual Class ParentClass => Parent.ParentClass;

        public _Choice Parent;
        CommandParse _CommandParse;
        public CommandParse CommandParse {
            get => _CommandParse ?? Parent.CommandParse;
            set => _CommandParse = value;
            }

        public void Process (List<_Choice> options) {
            foreach (var Entry in options) {
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
            foreach (var entry in Top) {
                entry.CommandParse = this;
                }
            _InitChildren();
            }
        }

    public partial class Class {
        public string Description = "<Unknown>";
        public About About = null;
        public Help Help = null;
        public bool Main = true;
        public string ReturnType = null;
        public override Class ParentClass => this;
        public List<Enumerate> EnumItems = new List<Enumerate>();

        public override void Init (_Choice parent) {
            this.Parent = parent;
            base.Init(parent);
            foreach (var Entry in Entries) {
                switch (Entry) {
                    case Brief entryCast: {
                        Description = entryCast.Text;
                        break;
                        }
                    case About cast: {
                        About = cast;
                        break;
                        }
                    case Help cast: {
                        Help = cast;
                        break;
                        }
                    case Library cast: {
                        Main = false;
                        break;
                        }
                    case Return cast: {
                        ReturnType = cast.Type.Label;
                        break;
                        }
                    }
                }
            }
        }

    public partial class CommandSet {
        public override void Init (_Choice parent) {
            this.Parent = parent;
            base.Init(parent);
            }
        }


    public partial class Command {
        public List<EntryItem> EntryItems = new List<EntryItem>();

        public bool IsDefault;
        public bool Lazy = false;
        public Parser Parser = null;
        public List<Generator> Generator = new List<Generator>();
        public List<Script> Script = new List<Script>();
        public List<Enumerate> EnumItems => ParentClass.EnumItems;

        public override void Init (_Choice parent) {
            base.Init(parent);
            Parent = parent;
            int Index = 0;
            foreach (var entry in Entries) {
                entry.Init(this);
                switch (entry) {
                    case Brief entryCast: {
                        Brief = entryCast.Text;
                        break;
                        }
                    case Include include: {
                        var OptionSet = include.Id.Definition as OptionSet;
                        Assert.NotNull(NYI.Throw, String: include.Id.ToString());
                        foreach (var SubEntry in OptionSet.Options) {
                            switch (SubEntry) {
                                case Option Option: {
                                    EntryItems.Add(new EntryItem(Option) {
                                        Index = Index++
                                        });
                                    break;
                                    }

                                case Enumerate Enumerate: {
                                    var item = new EntryItem(Enumerate) {
                                        Index = Index++
                                        };
                                    EntryItems.Add(item);
                                    break;
                                    }
                                }
                            }
                        break;
                        }
                    case Parameter parameter: {
                        EntryItems.Add(new EntryItem(parameter) {
                            Index = Index++
                            });
                        break;
                        }
                    case Option option: {
                        EntryItems.Add(new EntryItem(option) {
                            Index = Index++
                            });
                        break;
                        }
                    case Enumerate enumerate: {
                        var item = new EntryItem(enumerate) {
                            Index = Index++
                            };
                        EntryItems.Add(item);
                        break;
                        }
                    case DefaultCommand entryCast: {
                        parent.DefaultCommand = this;
                        IsDefault = true;
                        break;
                        }
                    case Lazy entryCast: {
                        Lazy = true;
                        EntryItems.Add(new EntryItem(entryCast) {
                            Index = Index++
                            });
                        break;
                        }
                    case Parser entryCast: {
                        Parser = entryCast;
                        CommandParse.DeclareRegistry = true;
                        EntryItems.Add(new EntryItem(Parser) {
                            Index = Index++
                            });
                        break;
                        }
                    case Generator entryCast: {
                        CommandParse.DeclareRegistry = true;
                        Generator.Add(entryCast);
                        break;
                        }
                    case Script entryCast: {
                        CommandParse.DeclareRegistry = true;
                        EntryItems.Add(new EntryItem(entryCast) {
                            Index = Index++
                            });
                        Script.Add(entryCast);
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
        public string Default => Item.Default;
        public string Brief=> Item.Brief;
        public bool IsOption;
        public bool IsEnumerate;
        public bool HasEntry = true;

        public EntryItem(Enumerate enumerate) {
            Item = enumerate;
            ID = enumerate.Name.ToString();
            Tag = enumerate.Command;
            Type = "Enumeration<" + enumerate.Name.ToString() + ">";
            IsEnumerate = true;
            //Default = enumerate.Default;
            //Brief = enumerate.Brief;
            }


        public EntryItem (Option option) {
            Item = option;
            ID = option.Name.ToString();
            Tag = option.Command;
            Type = option.Type.ToString();
            IsOption = true;
            //Default = option.Default;
            //Brief = option.Brief;
            }

        public EntryItem (Parameter parameter) {
            Item = parameter;
            ID = parameter.Name.ToString();
            Tag = "";
            Type = parameter.Type.ToString();
            IsOption = false;
            //Default = parameter.Default;
            //Brief = parameter.Brief;
            }

        public EntryItem (Script script) {
            Item = script;
            ID = script.Id.ToString();
            Tag = script.Extension;
            Type = "NewFile";
            IsOption = true;
            //Default = script.Default;
            //Brief = script.Brief;
            }

        public EntryItem (Parser parser) {
            Item = parser;
            ID = parser.Class.ToString();
            Tag = "";
            Type = "ExistingFile";
            IsOption = false;
            //Default = parser.Default;
            //Brief = parser.Brief;
            }

        public EntryItem (Lazy lazy) {
            Item = lazy;
            ID = lazy.Name.ToString();
            Tag = lazy.Tag;
            Type ="Flag";
            IsOption = false;
            //Default = lazy.Default;
            //Brief = lazy.Brief;
            HasEntry = false;
            }




        }


    public partial class Parameter {
        public override void Init (_Choice parent) {
            base.Init(parent);
            this.Parent = parent;
            Process(Modifier);
            }
        }
    public partial class Option {
        public override void Init (_Choice parent) {
            base.Init(parent);
            this.Parent = parent;
            Process(Modifier);
            }
        }

    public partial class OptionSet {
        public override void Init(_Choice parent) {
            this.Parent = parent;
            base.Init(parent);
            Process(Options);

            }
        }

    public partial class Enumerate {
        public List<Case> Cases = new List<Case>();

        public override void Init(_Choice parent) {
            base.Init(parent);
            this.Parent = parent;
            Process(Modifier);
            ParentClass.EnumItems.Add(this);
            }
        }

    public partial class Case {
        public override void Init(_Choice parent) {
            base.Init(parent);
            this.Parent = parent;
            Process(Modifier);
            }
        }


    public partial class Lazy {
        public override void Init (_Choice parent) {
            base.Init(parent);
            this.Parent = parent;
            Process(Modifier);
            }
        }

    public partial class Parser {
        public override void Init (_Choice parent) {
            base.Init(parent);
            this.Parent = parent;
            Process(Modifier);
            }
        }

    public partial class Script {
        public override void Init (_Choice parent) {
            base.Init(parent);
            this.Parent = parent;
            Process(Modifier);
            }
        }


    }
