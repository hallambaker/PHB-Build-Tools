using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.FSR;
using Goedel.IO;

namespace Goedel.Document.Markdown;

public abstract class BlockParser {
    public MarkdownDocument Document;
    public TagCatalog TagCatalog;
    protected CatalogEntry CurrentCatalogEntry = null;
    protected List<TagValue> CurrentAttributes = null;
    protected Block CurrentBlock = null;


    /// <summary>
    /// Final cleanup. Pop remaining enclosures off the stack and set the
    /// values of Title and ShortTitle from the metadata values (if any).
    /// </summary>
    protected void FinishParse() {

        PopLayoutAll();

        if (Document.MetaDataLookup("title", out var MetaTags)) {
            Document.Title = MetaTags[0].Text;
            Document.ShortTitle = Document.Title;
            }
        if (Document.MetaDataLookup("short", out MetaTags)) {
            Document.ShortTitle = MetaTags[0].Text;
            }

        }

    // We have the following types of markup
    // 
    // Meta         - applies to the whole document
    // Layout       - applies to multiple paragraph blocks
    // Block        - applies to a complete block (e.g. heading)
    // Annotation   - applies to a part of a block

    protected Stack<Block> StackLayout = new();
    protected Stack<TextSegmentOpen> StackAnnotation = new();

    // Reads in one input paragraph chunk at a time and performs
    // block level parsing

    protected bool MatchInStack(Stack<Block> Stack, CatalogEntry Key) {
        foreach (var T in Stack) {
            if (T.CatalogEntry == Key) {
                return true;
                }
            }
        return false;
        }

    protected bool MatchInStack(Stack<Block> Stack, List<CatalogEntry> Keys) {
        if (Stack.Count == 0) {
            return false;
            }
        foreach (var Key in Keys) {

            if (Stack.Peek().CatalogEntry == Key) {
                return true;
                }
            }
        return false;
        }

    protected bool MatchInStack(Stack<TextSegmentOpen> Stack, CatalogEntry Key) {
        foreach (var T in Stack) {
            if (T.CatalogEntry == Key) {
                return true;
                }
            }
        return false;
        }

    protected void PopAnnotationAll() {
        while (PopAnnotation() != null) {
            }
        }

    // Utility function for managing the Annotation stack
    protected TextSegmentOpen PopAnnotation() {
        if (StackAnnotation.Count <= 0) {
            return null;
            }
        var Top = StackAnnotation.Pop();
        //var Close = new Close(Top);
        //Document.Blocks.Add(Close);

        CurrentBlock.AddSegmentClose(Top);
        return Top;
        }

    protected void PopAnnotation(CatalogEntry Key) {
        if (!MatchInStack(StackAnnotation, Key)) {
            return;
            }
        var Top = PopAnnotation();
        while (Top.CatalogEntry != Key) {
            Top = PopAnnotation();
            }
        }


    protected void FinishBlock() {
        PopAnnotationAll();
        CurrentBlock = null;
        }

    // Utility function for managing the Layout stack
    protected void PopLayoutAll() {

        while (PopLayout() != null) {
            }
        }

    protected Block PopLayout() {
        if (StackLayout.Count <= 0) {
            return null;
            }
        var Top = StackLayout.Pop();
        var Close = new Close(Top);
        Document.Blocks.Add(Close);
        return Top;
        }

    protected void PopLayout(CatalogEntry Key) {
        if (!MatchInStack(StackLayout, Key)) {
            return;
            }
        var Top = PopLayout();
        while (Top.CatalogEntry != Key) {
            Top = PopLayout();
            }
        }


    /// <summary>
    /// Purge the stack back to an element that is permitted to contain a block
    /// of type CatalogEntry
    /// </summary>
    /// <param name="CatalogEntry"></param>
    protected void PurgeStack(CatalogEntry CatalogEntry) {
        //Console.WriteLine($"Check  {CatalogEntry}");

        while (StackLayout.Count > 0) {
            var Top = StackLayout.Peek();
            // Can we find a Wrapper statement?

            //Console.WriteLine($"  Top  {Top}");

            if (Top.CatalogEntry.Wrappers.Count == 0) {
                if (IsStackEnclosure(CatalogEntry, Top.CatalogEntry)) {
                    //Console.WriteLine($"  Allow Any, is enclosure");
                    return;
                    }
                }

            foreach (var Allowed in Top.CatalogEntry.Wrappers) {
                if (Allowed == CatalogEntry) {
                    //Console.WriteLine($"  Wrapped");
                    return;
                    }
                }

            PopLayout();
            //Console.WriteLine($"  Popped!");
            }
        }


    protected bool IsStackEnclosure(CatalogEntry Child, CatalogEntry Parent) {
        foreach (var Match in Child.StackEnclosures) {
            if (Parent == Match) {
                return true;
                }
            }
        return false;
        }

    protected bool IsWrapper(CatalogEntry Child, CatalogEntry Parent) {
        if (Parent.Any | Parent.Wrappers.Count == 0) {
            return true;
            }
        foreach (var Match in Parent.Wrappers) {
            if (Child == Match) {
                return true;
                }
            }
        return false;
        }

    /// <summary>
    /// It is not clear why this is required together with purge stack
    /// Consider removal.
    /// </summary>
    /// <param name="CatalogEntry"></param>
    protected void CheckLayoutWrapper(CatalogEntry CatalogEntry) {
        while (StackLayout.Count > 0) {
            var Top = StackLayout.Peek();
            if (Top.CatalogEntry.Any) {
                return;
                }
            if (IsStackEnclosure(CatalogEntry, Top.CatalogEntry)) {
                return;
                }
            PopLayout();
            }
        }

    protected void PurgeStackWrapper(CatalogEntry NewChild) {
        if (StackLayout.Count == 0) {
            return;
            }
        var Top = StackLayout.Peek();
        if (IsWrapper(NewChild, Top.CatalogEntry)) {
            return;
            }
        PopLayout();
        PurgeStackWrapper(NewChild);
        }

    protected void PurgeStackEnclosure(CatalogEntry NewChild) {
        if (StackLayout.Count == 0) {
            return;
            }
        var Top = StackLayout.Peek();
        if (IsStackEnclosure(NewChild, Top.CatalogEntry)) {
            return;
            }
        PopLayout();
        PurgeStackEnclosure(NewChild);
        }


    protected bool FindStackEnclosure(CatalogEntry NewChild) {
        foreach (var Item in StackLayout) {
            if (IsStackEnclosure(NewChild, Item.CatalogEntry)) {
                return true;
                }
            }
        return false;
        }

    protected void GetCompatibleEnclosure(CatalogEntry NewChild) {

        if (NewChild.StackEnclosures.Count > 0) {
            if (FindStackEnclosure(NewChild)) {
                // There is a compatible enclosure in the stack already
                // Purge stack until child has Stack entry for top item.
                PurgeStackEnclosure(NewChild);
                }
            else {
                // Open the default enclosure at this point.
                // This may cause the stack to be purged, is recursive.
                MakeLayout(NewChild.DefaultStackEnclosure, null);
                }

            // Purge stack until child has Stack entry for top item.
            // This means the Top item must accept it.
            }
        else {
            // Purge stack until top item has wrap entry for child or wraps ANY
            PurgeStackWrapper(NewChild);
            }

        }

    protected void AddText(string Text) {
        if (CurrentBlock != null) {
            CurrentBlock.AddSegmentText(Text);
            return;
            }
        if (Text == null || Text.Trim() == "") {
            return;
            }
        CurrentBlock = new Paragraph() {
            CatalogEntry = CurrentCatalogEntry,
            Attributes = CurrentAttributes
            };
        Document.Blocks.Add(CurrentBlock);
        CurrentBlock.AddSegmentText(Text);
        }

    protected void StartNewParagraph() {
        FinishBlock();
        StartParagraph();
        }


    protected void StartParagraph() {
        if (CurrentBlock == null) {
            CurrentBlock = new Paragraph() {
                CatalogEntry = CurrentCatalogEntry
                };
            Document.Blocks.Add(CurrentBlock);
            }
        }

    protected void MakeLayout(CatalogEntry CatalogEntry, List<TagValue> Attributes) {
        FinishBlock();

        // here we need to check up on the state of the stack
        GetCompatibleEnclosure(CatalogEntry);

        CurrentBlock = Block.MakeBlock(CatalogEntry, Attributes);
        Document.Blocks.Add(CurrentBlock);
        StackLayout.Push(CurrentBlock);
        }




    public void ProcessParagraphText(string Text) {
        var Reader = new LexStringReader(Text);
        var Lexer = new MarkDownParagraph(Reader);
        var Token = Lexer.GetToken();
        while (Token != MarkDownParagraph.Token.Null) {
            //Console.WriteLine("   {0} : {1}", Token, Lexer.Data);
            foreach (var Tag in Lexer.Attributes) {
                //Console.WriteLine("      {0} = {1}", Tag.Tag, Tag.Value);
                }

            switch (Token) {
                case MarkDownParagraph.Token.Open: {
                        OpenTag(Lexer.Data, Lexer.Attributes, false);
                        break;
                        }
                case MarkDownParagraph.Token.Close: {
                        CloseTag(Lexer.Tag, Lexer.Attributes);
                        break;
                        }
                case MarkDownParagraph.Token.Empty: {
                        OpenTag(Lexer.Data, Lexer.Attributes, true);
                        break;
                        }
                case MarkDownParagraph.Token.Text: {
                        AddText(Lexer.Data);
                        break;
                        }
                }

            Token = Lexer.GetToken();
            }
        }

    void OpenTag(string Tag, List<TagValue> Attributes, bool Empty) {
        var CatalogEntry = ProcessTextTag(Attributes, Tag);

        if (CatalogEntry == null) {
            return;
            }

        if (CatalogEntry.Key == "include") {
            //Console.WriteLine("Read file {0}", Attributes[0].Value);
            ParseRegistry.Include(Attributes[0].Value, TagCatalog, Document);
            return;
            }
        MakeElement(CatalogEntry, Attributes, Empty);
        }


    public void MakeElement(CatalogEntry CatalogEntry, List<TagValue> Attributes, bool Empty) {
        switch (CatalogEntry.ElementType) {
            case ElementType.Layout: {
                    if (!Empty) {
                        MakeLayout(CatalogEntry, Attributes);
                        }
                    break;
                    }
            case ElementType.Meta: {
                    FinishBlock();
                    Meta Meta = new(CatalogEntry, Attributes) {
                        BlockType = BlockType.Meta
                        };
                    Document.MetaDataAdd(CatalogEntry, Meta);
                    CurrentBlock = Meta;
                    Document.Blocks.Add(CurrentBlock);
                    break;
                    }
            case ElementType.Annotation: {
                    StartParagraph();
                    var Segment = CurrentBlock.AddSegmentOpen(CatalogEntry, Attributes);
                    StackAnnotation.Push(Segment);
                    break;
                    }
            case ElementType.Item: {
                    StartParagraph();
                    CurrentBlock.AddSegmentEmpty(CatalogEntry, Attributes);
                    break;
                    }
            case ElementType.Block: {
                    FinishBlock();
                    CurrentAttributes = Attributes;

                    GetCompatibleEnclosure(CatalogEntry);
                    CurrentCatalogEntry = CatalogEntry;
                    break;
                    }
            }
        }


    public void CloseTag(string Tag, List<TagValue> Attributes) {
        var CatalogEntry = TagCatalog.Find(Tag);
        if (CatalogEntry == null) {
            return;
            }

        switch (CatalogEntry.ElementType) {
            case ElementType.Layout: {
                    PopLayout(CatalogEntry);
                    break;
                    }
            case ElementType.Annotation: {
                    PopAnnotation(CatalogEntry);
                    break;
                    }
            }
        }


    CatalogEntry ProcessTextTag(List<TagValue> Attributes, string Tag) {
        // Process the tag part
        //
        // TagCatalog.Find returns null if the Attributes are empty,
        // Otherwise a Catalog entry or 'unknown' is returned.

        var CatalogEntry = TagCatalog.Find(Attributes);
        if (CatalogEntry == null) {
            CatalogEntry = TagCatalog.Find(Tag);
            }
        return CatalogEntry;
        }

    }
