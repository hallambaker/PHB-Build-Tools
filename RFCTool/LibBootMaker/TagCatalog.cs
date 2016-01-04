using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Goedel.BootMark;
using BM=Goedel.BootMark;

namespace Goedel.MarkLib {

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
        public List<CatalogEntry> Children = new List<CatalogEntry>();


        public int Level = -1;
        public string Start = null;
        public string Start1 = null;
        public string End = null;
        public string XMLTag = null;
        public string XMLFirst = null;
        public List<TagValue> XMLDefaults = new List<TagValue>();

        public List<_Choice> PreEnclosures = new List<_Choice> ();
        public List<_Choice> PreWrappers = new List<_Choice>();

        public List<CatalogEntry> Enclosures = new List<CatalogEntry>();
        public List<CatalogEntry> Wrappers = new List<CatalogEntry>();        
        
        public CatalogEntry Enclosure {
            get {
                return Enclosures.Count > 0 ? Enclosures[0] : null;
                }
            }

        public void BackReferences() {
            foreach (var Item in PreEnclosures) { Enclosures.Add(Item.CatalogEntry); }
            foreach (var Item in PreWrappers) { Wrappers.Add(Item.CatalogEntry); }
            }


        public CatalogEntry() {
            }

        public string Key = null;

        public override string ToString () {
            return Key;
            }

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
                if (Item.GetType() == typeof(BM.Layout)) {
                    var NewItem = new CatalogEntry(Dictionary, this, Item);
                    Children.Add(NewItem);
                    }
                else if (Item.GetType() == typeof(BM.Level)) {
                    var ItemT = (BM.Level)Item;
                    Level = ItemT.Value;
                    }
                else if (Item.GetType() == typeof(BM.Markup)) {
                    var ItemT = (BM.Markup)Item;
                    Start = ItemT.Start;
                    Start1 = ItemT.Start1;
                    End = ItemT.End;
                    }
                else if (Item.GetType() == typeof(BM.XML)) {
                    var ItemT = (BM.XML)Item;
                    XMLTag= ItemT.Tag;
                    XMLFirst = ItemT.First;

                    foreach (var ItemD in ItemT.Entries) {
                        if (ItemD.GetType() == typeof(BM.Default)) {
                            var ItemDT = (Default)ItemD;
                            var Default = new TagValue(ItemDT.Tag, ItemDT.Value);
                            XMLDefaults.Add(Default);
                            }
                        }

                    }
                else if (Item.GetType() == typeof(BM.Stack)) {
                    var ItemT = (BM.Stack)Item;
                    var RefItem = ItemT.Wrapper.Definition;
                    PreEnclosures.Add(RefItem);
                    }
                else if (Item.GetType() == typeof(BM.Wrap)) {
                    var ItemT = (BM.Wrap)Item;
                    var RefItem = ItemT.Wrapper.Definition;
                    PreWrappers.Add(RefItem);
                    }
                else if (Item.GetType() == typeof(BM.Meta)) {
                    var Child = new CatalogEntry(Dictionary, this, Item);
                    Children.Add(Child);
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

        public delegate Document DocumentProcessD(DocumentSet Parent, 
                    FileInfo FileInfo, TagCatalog TagCatalog);
        public delegate void ProcessD(string InPath, string OutPath);

        public DocumentProcessD DocumentProcess;
        public ProcessD Process;


        Dictionary<string, CatalogEntry> Catalog;
        public CatalogEntry[] Defaults = new CatalogEntry[7];
        public CatalogEntry Default;
        //public CatalogEntry DefaultBullet;
        //public CatalogEntry DefaultNumbered;
        //public CatalogEntry DefaultDefinedTerm;
        //public CatalogEntry DefaultDefinedData;

        public TagCatalog(string SchemaFile) {

            }


        public TagCatalog(MarkSchema Schema) {
            Catalog = new Dictionary<string, CatalogEntry>();

            foreach (var Item in Schema.Top) {
                var Class = (Class)Item;
                foreach (var Entry in Class.Entries) {
                    var E = new CatalogEntry(Catalog, null, Entry);
                    if (E.Level >= 0 & E.Level <= 6) {
                        if (Defaults[E.Level] == null) {
                            Defaults[E.Level] = E;
                            }
                        }
                    }
                }

            for (int i = 0; i < 7; i++) {
                if (Defaults[i] == null) {
                    Defaults[i] = new CatalogEntry();
                    Defaults[i].Level = i;
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
            if (Key == null) return null;

            CatalogEntry Result;
            var Found = Catalog.TryGetValue(Key.ToLower(), out Result);
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
