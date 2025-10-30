using Goedel.Cryptography.Nist;
using Goedel.Discovery;
using Goedel.Registry;
using Goedel.Sitebuilder;

using System.Reflection.Emit;

namespace Goedel.Tool.Sitebuilder;

/// <summary>
/// Generate the backing data for the GUI.
/// </summary>
public partial class GenerateBacking {


    public void CreateFrame(FrameStruct parse) {

        parse.Init();

        var frameSet = new FrameSet();
        foreach (var entry in parse.Top) {
            var nameSpace = entry as Namespace;
            nameSpace?.Collect(frameSet);
            }

        CreateFrame(frameSet);
        }



    /// <summary>
    /// Return the first presentation from the list of fields 
    /// <paramref name="fields"/> or null if none is found.
    /// </summary>
    /// <param name="fields">The list of fields to scan.</param>
    /// <returns>The first presentation if found, otherwise null.</returns>
    public static FramePresentation? GetDefaultPresentation(List<IFrameField> fields) {
        foreach (var field in fields) {
            if (field is FramePresentation presentation) {
                return presentation;
                }
            }
        return null;
        }


    }


public partial class Namespace {



    public void Collect(FrameSet frameSet) {
        frameSet.Namespace = Id.Label;
        var dictionary = new Dictionary<string, FrameClass>();
        //foreach (var Name

        foreach (var entry in Entries) {
            switch (entry.Type) {
                case Page item: {
                    frameSet.Pages.Add(Collect(frameSet, entry.Id, item));
                    break;
                    }
                case Menu item: {
                    frameSet.Menus.Add(CollectMenu(frameSet, entry.Id.Label, item.Entries));
                    break;
                    }
                case Selector item: {
                    frameSet.Selectors.Add(Collect(frameSet, entry.Id, item));
                    break;
                    }
                case Class item: {
                    var newClass = Collect(frameSet, entry.Id, item);
                    dictionary.Add(newClass.Tag, newClass);
                    frameSet.Classes.Add(newClass);
                    break;
                    }
                case SubClass item: {
                    var newClass = Collect(frameSet, entry.Id, item);
                    dictionary.Add(newClass.Tag, newClass);
                    frameSet.Classes.Add(newClass);
                    break;
                    }
                }
            }


        foreach (var pair in dictionary) {
            if (pair.Value.ParentId is not null) {
                if (dictionary.TryGetValue(pair.Value.ParentId, out var parent)) {
                    pair.Value.Parent = parent;
                    }
                }

            }

        }

    public FramePage Collect(FrameSet frameSet, ID<_Choice> id, Page page) {
        var fields = CollectFields(frameSet, page.Entries);
       
        return new FramePage(id.Label, page.Title, fields) {
            Container = page.Container
            };
        }

    public FrameMenu CollectMenu(FrameSet frameSet, string id, List<FieldItem> entries) {
        var fields = CollectFields(frameSet, entries);

        return new FrameMenu(id, fields);
        }

    public FrameSelector Collect(FrameSet frameSet, ID<_Choice> id, Selector page) {
        var fields = CollectFields(frameSet, page.Entries);
        return new FrameSelector(id.Label, fields);
        }

    public FrameClass Collect(FrameSet frameSet, ID<_Choice> id, Class baseclass) {


        var properties = CollectProperties(frameSet, baseclass.Entries);

        return new FrameClass(id.Label) {
            Fields = properties
            };
        }

    public FrameClass Collect(FrameSet frameSet, ID<_Choice> id, SubClass subclass) {
        var properties = CollectProperties(frameSet, subclass.Entries);

        return new FrameClass(id.Label) {
            ParentId = subclass.Parent.Label,
            Fields = properties
            };
        }

    public List<IFrameField> CollectFields(FrameSet frameset, List<FieldItem> entries) {
        var result = new List<IFrameField>();
        foreach (var entry in entries) {
            Collect(frameset, result, entry);
            }

        return result;
        }

    public List<IFrameField> CollectProperties (FrameSet frameset, List<Property> entries) {
        var result = new List<IFrameField>();
        foreach (var entry in entries.IfEnumerable()) {
            Collect(frameset, result, entry);
            }
        return result;
        }

    private void Collect(FrameSet frameset, List<IFrameField> result, IFieldEntry entry) {

        var label = entry.Token.Label;

        switch (entry.TypeH) {
            case Button button: {
                result.Add(GetFrameButton(label, button));
                break;
                }
            case SubMenu menu: {
                result.Add(GetSubmenu(frameset, label, menu));
                break;
                }
            //case Show show: {
            //    result.Add(GetRef(frameset, label, show, show.Display.Label));
            //    break;
            //    }
            case IReference reference: {
                result.Add(GetRef(frameset, label, reference));
                break;
                }
            case Separator: {
                result.Add(new FrameSeparator(label));
                break;
                }
            case Chooser chooser: {
                result.Add(GetChooser(label, chooser));
                break;
                }
            case IIntrinsic field: {
                result.Add(GetIntrinsic(label, field));
                break;
                }
            case Presentation presentation: {
                result.Add(GetPresentation(frameset, label, presentation));
                break;
                }
            case File file: {
                result.Add(GetFile(label, file));
                break;
                }
            default: {
                break;
                }
            }
        }

    static FrameButtonParsed GetFrameButton(string label, Button button) {
        string? action = button.Action.Label;
        string? active = null;
        string? integer = null;
        string? text = null;
        string? description = null;
        ButtonAction buttonAction = ButtonAction.Link;

        foreach (var entry in button.Entries) {
            switch (entry.Type) {
                case Boolean : {
                    active = entry.Id.Label;
                    break;
                    }
                case Integer : {
                    integer = entry.Id.Label;
                    break;
                    }
                case String : {
                    text = entry.Id.Label;
                    break;
                    }
                case Sitebuilder.Description desc : {
                    description = desc.Text;
                    break;
                    }
                case Sitebuilder.Action: {
                    buttonAction = ButtonAction.Method ;
                    break;
                    }

                case Sitebuilder.Link link: {
                    buttonAction = ButtonAction.Link;
                    action = link.Uri;
                    break;
                    }
                }
            
            }

        var result = new FrameButtonParsed(label, button.Title, action,
            active, integer, text, buttonAction) {
            Description = description
            };


        return result;
        }


    public FrameSubmenu GetSubmenu(FrameSet frameset, string label, SubMenu submenu) {
        var fields = new List<IFrameField>();
        foreach (var entry in submenu.Entries) {
            Collect(frameset, fields, entry);
            }

        return new FrameSubmenu(label, submenu.Title) { 
            Fields = fields 
            };
        }


    public FramePresentation GetPresentation(FrameSet frameset, string label, Presentation presentation) {

        var sections = new List<FrameSection> ();
        foreach (var section in presentation.Sections) {
            var fields = new List<IFrameField>();
            foreach (var entry in section.Entries) {
                Collect(frameset, fields, entry);
                }

            sections.Add(new FrameSection(section.Id.Label) {
                Fields = fields
                });
            }

        var result = new FramePresentation(label) {
            Sections = sections,
            UidField = presentation.Id.Label
            };

        return result;
        }


    static string MakePath(_Choice item, string prefix="") => item switch {
        Field field => prefix + field.Id.Label,
        From from => MakePath (from.Type, prefix + from.Id.Label + "?."),
        _ => throw new Invalid()
        };


    public FrameRef? GetRef(
                FrameSet frameset,
                string id, IReference reference) {

        if (reference.Reference.Definition is not Entry entry) {
            return new FrameRef(id);
            }

        switch (entry.Type) {
            case Menu: {
                return new FrameRefMenu(id, entry.Id.Label);
                }
            case Class:
            case SubClass: {
                switch (reference) {
                    case List: {
                        return new FrameRefList(id, entry.Id.Label) {
                            PresentationId = reference.Display
                            };
                        }
                    case Form form: {
                        var fields = CollectFields(frameset, form.Entries);


                        return new FrameRefForm(id, entry.Id.Label, fields) {
                            PresentationId = reference.Display
                            };
                        }
                    case Is: {
                        return new FrameRefClass(id, entry.Id.Label) {
                            PresentationId = reference.Display
                            };
                        }
                    }
                throw new Invalid();
                }
            }
        return new FrameRef(id);


        }

    public static FrameFile GetFile(
                string id,
                File file) {

        return new FrameFile(id) {
            FileType = file.FileType?.Label,
            Prompt = file.Prompt,
            Description = file.Description
            };


        }

    public static FrameChooser GetChooser(
                string id,
                Chooser chooser) {

        var options = new List<FrameChooserOption>();
        foreach (var entry in chooser.Entries) {
            options.Add(new FrameChooserOption(entry.Action.Label, entry.Title));
            }

        return new FrameChooser(id, options);
        }

    public static IFrameField GetIntrinsic(
            string id,
            IIntrinsic field) => GetBackingFrame (id, field);

    public static IFrameField GetBackingFrame(string id, IIntrinsic field) {
        IFrameField result = field switch {
            Boolean => new FrameBoolean(id),
            Integer => new FrameInteger(id),
            DateTime => new FrameDateTime(id),
            String => new FrameString(id),
            Text => new FrameText(id),
            Anchor => new FrameAnchor(id),
            RichText => new FrameRichText(id),
            Image => new FrameImage(id),
            Icon => new FrameIcon(id),
            Avatar => new FrameAvatar(id),
            Count => new FrameCount(id),
            _ => throw new Invalid()
            };


        result.Prompt = field.Prompt;
        result.Hidden = field.Hidden;
        result.Description = field.Description;
        return result;
        }

    }

public interface IFieldEntry {
    public TOKEN<_Choice> Token { get; }
    public _Choice TypeH { get; }

    }

public partial class FieldItem : IFieldEntry {
    public TOKEN<_Choice> Token => Id;
    public _Choice TypeH => Type;
    }

public partial class Property : IFieldEntry {
    public TOKEN<_Choice> Token => Id;
    public _Choice TypeH => Type;
    }


public interface IIntrinsic {
    string Prompt { get; }
    bool Hidden { get; }
    string Description { get; }
    }

public partial class Boolean : IIntrinsic {
    }
public partial class Integer : IIntrinsic {
    }
public partial class DateTime : IIntrinsic {
    }
public partial class String : IIntrinsic {
    }

public partial class Anchor : IIntrinsic {
    }
public partial class Text : IIntrinsic {
    }
public partial class RichText : IIntrinsic {
    }
public partial class Image : IIntrinsic {
    }
public partial class Icon : IIntrinsic {
    }
public partial class Count : IIntrinsic {
    }
public partial class Avatar : IIntrinsic {
    }

public interface IReference {
    public REF<_Choice> Reference { get; }

    public string? Display { get; set; }
    }

public partial class Is : IReference {
    public REF<_Choice> Reference => Parent;
    }

public partial class Form : IReference {
    public REF<_Choice> Reference => Id;
    }




public partial class List : IReference {
    public REF<_Choice> Reference => Of;
    }

public partial class Return : IReference {
    public REF<_Choice> Reference => To;
    }


public partial class FrameStruct {

    public override void Init() {

        foreach (var entry in Top) {
            entry._InitChildren(null);
            }

        }

    }

public enum Attribute {
    // Integer presentations
    Range,

    // DateTime presentations
    Compact,
    Local,
    UTC,

    // String presentations
    Comment,
    Post,
    Rich,

    Default
    }

public partial class _Choice {

    ///<summary>Description text.</summary>
    public string? Description { get; set; } = null;

    ///<summary>Description text.</summary>
    public string? Prompt { get; set; } = null;

    public bool Hidden { get; set; } = false;

    public string? Container { get; set; } = null;
    public string? Display { get; set; } = null;

    ///<summary>If true, include this field in serialization.</summary>
    public bool Include { get; set; } = true;

    ///<summary></summary>
    public bool IsRange { get; set; } = true;
    ///<summary></summary>
    public bool ReadOnly { get; set; } = false;
    ///<summary></summary>
    public Attribute Attribute { get; set; } = Attribute.Default;


    public TOKEN<_Choice> FileType { get; set; } = null;

    }

public partial class Namespace {
    }



public static class Extensions {
    }



public partial class Class  {

    public List<Property> Entries { get; set; }



    public override void Init(_Choice? parent) {
        foreach (var entry in TypeEntries) {
            entry.Init(this);
            switch (entry.Type) {
                case Fields fields: {
                    Entries = fields.Entries;
                    break;
                    }
                case Description description: {
                    Description = description.Text;
                    break;
                    }
                }
            }
        }
    }

public partial class SubClass {

    public List<Property> Entries { get; set; }

    public override void Init(_Choice? parent) {

        // Raise the fields entry to be a child of this.
        foreach (var entry in TypeEntries) {
            switch (entry.Type) {
                case Fields fields: {
                    Entries = fields.Entries;
                    break;
                    }
                }
            }
        }
    }

public partial class Display {

    public override void Init(_Choice? parent) {
        parent._Parent.Display = Id.Label;
        }

    }

public partial class Description {

    public override void Init(_Choice? parent) {
        parent._Parent.Description = Text;
        }

    }

public partial class Exclude {

    public override void Init(_Choice? parent) {
        parent.Include = false;
        }

    }


public partial class ReadOnly {

    public override void Init(_Choice? parent) {
        parent.ReadOnly = true;
        }

    }


public partial class Compact {

    public override void Init(_Choice? parent) {
        parent.Attribute = Attribute.Compact;
        }
    }

public partial class Local {

    public override void Init(_Choice? parent) {
        parent.Attribute = Attribute.Local;
        }
    }

public partial class UTC {

    public override void Init(_Choice? parent) {
        parent.Attribute = Attribute.UTC;
        }
    }

public partial class Comment {

    public override void Init(_Choice? parent) {
        parent.Attribute = Attribute.Comment;
        }
    }

public partial class Prompt {

    public override void Init(_Choice? parent) {
        parent._Parent.Prompt = Text;
        }
    }


public partial class Container {

    public override void Init(_Choice? parent) {
        var fieldItem = parent as FieldItem;
        parent._Parent.Container = fieldItem.Id.Label;
        }
    }

public partial class Hidden {

    public override void Init(_Choice? parent) {
        parent._Parent.Hidden = true;
        }
    }

public partial class Post {

    public override void Init(_Choice? parent) {
        parent.Attribute = Attribute.Post;
        }
    }

public partial class Rich {

    public override void Init(_Choice? parent) {
        parent.Attribute = Attribute.Rich;
        }
    }

public partial class FileType {

    public override void Init(_Choice? parent) {
        parent._Parent.FileType = Id;
        }
    }