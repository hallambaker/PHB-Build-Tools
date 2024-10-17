using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.Document.Markdown.Tags;
using BM=Goedel.Document.Markdown.Tags;

namespace Goedel.Document.Markdown {

    public enum ElementType {
        Start,
        Meta,
        Layout,
        Block,
        Annotation,
        Item,    
        Close,
        Invalid
        }

    public class CatalogEntry {
        public CatalogEntry Parent = null;
        public ElementType ElementType = ElementType.Invalid;

        public _Choice Element;
        public List<CatalogEntry> Children = new();


        public int Level = -1;
        public string Start = null;
        public string Start1 = null;
        public string End = null;
        public string XMLTag = null;
        public string XMLFirst = null;
        public List<TagValue> XMLDefaults = new();

        public List<_Choice> PreEnclosures = new();
        public List<_Choice> PreWrappers = new();

        public List<CatalogEntry> StackEnclosures = new();
        public List<CatalogEntry> Wrappers = new();
        public bool Any = false;

        public bool Plaintext = false;

        public CatalogEntry DefaultStackEnclosure => StackEnclosures.Count > 0 ? StackEnclosures[0] : null;


        public void BackReferences() {
            foreach (var Item in PreEnclosures) { StackEnclosures.Add(Item.CatalogEntry); }
            foreach (var Item in PreWrappers) { Wrappers.Add(Item.CatalogEntry); }
            }


        public CatalogEntry() {
            }

        public string Key = null;

        public override string ToString() => Key;

        public CatalogEntry(Dictionary<string, CatalogEntry> Dictionary, 
                    CatalogEntry Parent, _Choice Item) {
            
            Element = Item;
            this.Parent = Parent;
            Item.CatalogEntry = this;

            if (Item.GetType() == typeof(BM.Meta)) {
                Key = Init(Dictionary, Parent, (BM.Meta)Item);
                ElementType = ElementType.Meta;
                }
            else if (Item.GetType() == typeof(BM.Item)) {
                Key = Init(Dictionary, Parent, (BM.Item)Item);
                ElementType = ElementType.Item;
                }
            else if (Item.GetType() == typeof(BM.Annotation)) {
                Key = Init(Dictionary, Parent, (BM.Annotation)Item);
                ElementType = ElementType.Annotation;
                }
            else if (Item.GetType() == typeof(BM.Layout)) {
                Key = Init(Dictionary, Parent, (BM.Layout)Item);
                ElementType = ElementType.Layout;
                }
            else if (Item.GetType() == typeof(BM.Block)) {
                Key = Init(Dictionary, Parent, (BM.Block)Item);
                ElementType = ElementType.Block;
                }


             //Console.WriteLine("{0} {1}", Key, Item.GetType());
                if (Key != null) {
                Dictionary.Add(Key, this);
                }
            }

        public void Init(Dictionary<string, CatalogEntry> Dictionary, CatalogEntry Parent, 
                    List<_Choice> Items) {
            foreach (var Item in Items) {
                switch (Item) {
                    case BM.Layout ItemT: {
                        var NewItem = new CatalogEntry(Dictionary, this, Item);
                        Children.Add(NewItem);
                        break;
                        }
                    case BM.Level ItemT: {
                        Level = ItemT.Value;
                        break;
                        }
                    case BM.Markup ItemT: {
                        Start = ItemT.Start;
                        Start1 = ItemT.Start1;
                        End = ItemT.End;
                        break;
                        }
                    case BM.XML ItemT: {
                        XMLTag = ItemT.Tag;
                        XMLFirst = ItemT.First;

                        foreach (var ItemD in ItemT.Entries) {
                            if (ItemD.GetType() == typeof(BM.Default)) {
                                var ItemDT = (Default)ItemD;
                                var Default = new TagValue(ItemDT.Tag, ItemDT.Value);
                                XMLDefaults.Add(Default);
                                }
                            }
                        break;
                        }
                    case BM.Stack ItemT: {
                        var RefItem = ItemT.Wrapper.Definition;
                        PreEnclosures.Add(RefItem);
                        break;
                        }
                    case BM.Wrap ItemT: {
                        var RefItem = ItemT.Wrapper.Definition;
                        PreWrappers.Add(RefItem);
                        break;
                        }
                    case BM.Meta ItemT: {
                        var Child = new CatalogEntry(Dictionary, this, Item);
                        Children.Add(Child);
                        break;
                        }
                    case BM.Any ItemT: {
                        Any = true;
                        break;
                        }
                    case BM.Flag ItemT: {
                        if (ItemT.Id.Label == "plaintext") {
                            Plaintext = true;
                            }
                        break;
                        }
                    }
                }
            }

        public string Init(Dictionary<string, CatalogEntry> Dictionary, 
                        CatalogEntry Parent, BM.Meta Item) {
            string Key = Item.Id.ToString();
            this.Parent = Parent;
            Init(Dictionary, Parent, Item.Entries);
            return Key;
            }

        public string Init(Dictionary<string, CatalogEntry> Dictionary, 
                        CatalogEntry Parent, BM.Item Item) {
            string Key = Item.Id.ToString();
            Init(Dictionary, this, Item.Entries);
            return Key;
            }

        public string Init(Dictionary<string, CatalogEntry> Dictionary, 
                    CatalogEntry Parent, BM.Annotation Item) {
            string Key = Item.Id.ToString();
            Init(Dictionary, this, Item.Entries);
            return Key;
            }

        public string Init(Dictionary<string, CatalogEntry> Dictionary, 
                    CatalogEntry Parent, BM.Layout Item) {
            string Key = Item.Id.ToString();
            Init(Dictionary, this, Item.Entries);
            return Key;
            }
        public string Init(Dictionary<string, CatalogEntry> Dictionary, 
                    CatalogEntry Parent, BM.Block Item) {
            string Key = Item.Id.ToString();
            Init(Dictionary, this, Item.Entries);
            return Key;
            }
        }

    public class TagCatalog {

        public delegate MarkdownDocument DocumentProcessDelegate(DocumentSet Parent, 
                    FileInfo FileInfo, TagCatalog TagCatalog);
        public delegate void ProcessDelegate(string InPath, string OutPath);

        public DocumentProcessDelegate DocumentProcess;
        public ProcessDelegate Process;

        public FormatHTML DefaultFormat = null;
        public Dictionary<string, FormatHTML> Formats = new();


        Dictionary<string, CatalogEntry> Catalog;
        public CatalogEntry[] Defaults = new CatalogEntry[7];
        public CatalogEntry Default;



        public TagCatalog(MarkSchema Schema) {
            Catalog = new Dictionary<string, CatalogEntry>();

            foreach (var Item in Schema.Top) {
                var Class = (Class)Item;
                foreach (var Entry in Class.Entries) {
                    if (Entry.GetType() == typeof(BM.Format)) {
                        var Format = Entry as BM.Format;
                        var FormatHTML = new FormatHTML(Format);
                        DefaultFormat ??= FormatHTML;
                        Formats.Add(Format.Id.ToString(), FormatHTML);
                        }
                    else {
                        var E = new CatalogEntry(Catalog, null, Entry);
                        if (E.Level >= 0 & E.Level <= 6) {
                            if (Defaults[E.Level] == null) {
                                Defaults[E.Level] = E;
                                }
                            }
                        }
                    }
                }

            for (int i = 0; i < 7; i++) {
                if (Defaults[i] == null) {
                    Defaults[i] = new CatalogEntry() {
                        Level = i
                        };
                    if (i == 0) {
                        Defaults[i].Start = "<p>";
                        Defaults[i].Start1 = "<p>";
                        Defaults[i].End = "</p>";
                        }
                    else {
                        string X = i.ToString() + ">";
                        Defaults[i].Start = "<" + X;
                        Defaults[i].Start1 = "<" + X;
                        Defaults[i].End = "</" + X;
                        }
                    }
                }

            Default = Defaults[0];


            foreach (var Item in Catalog) {
                Item.Value.BackReferences();
                }
            }

        public CatalogEntry Find(List<TagValue> Attributes) {
            if ((Attributes != null) && (Attributes.Count > 0)) {
                return Find(Attributes[0].Tag);
                }
            return null;
            }

        public CatalogEntry Find(string Key) {
            if (Key == null) {
                return null;
                }

            var Found = Catalog.TryGetValue(Key.ToLower(), out var Result);
            Result = Found ? Result : null;
            return Result;
            }

        public CatalogEntry FindDefault(string Key) {
            var Entry = Find(Key);
            if (Entry == null) {
                Entry = Defaults[0];
                }
            return Entry;
            }

        }


    }
