namespace Goedel.Sitebuilder;

/// <summary>Actions bound to a form button.</summary>
public enum ButtonAction {
    /// <summary>Do nothing.</summary>
    Null,

    /// <summary>Activate hypoetext link.</summary>
    Link,

    /// <summary>Activate a method.</summary>
    Method
    }

/// <summary>
/// Frame fields
/// </summary>
public abstract record FrameField : IFrameField {

    /// <summary>Identifier for use in code construction.</summary>
    public string PresentationId { get; set; }

    /// <inheritdoc/>
    public string? Prompt { get; set; }

    /// <inheritdoc/>
    public bool Hidden { get; set; } = false;

    /// <inheritdoc/>
    public string? Description { get; set; } = null;

    /// <inheritdoc/>
    public string Id { get; init; }

    /// <inheritdoc/>
    public string Tag { get; init; }

    /// <inheritdoc/>
    public virtual string Backing => null;

    /// <inheritdoc/>
    public abstract string Type { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id">The frame field identifier.</param>
    public FrameField(string id) {
        Id = id;
        Tag = id;
        }
    }

/// <summary>Frame button</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Label">Label to present to the user.</param>
/// <param name="Action">Action to perform</param>
/// <param name="ButtonAction">Action type</param>
public record FrameButton(
                string Id,
                string Label,
                string Action,

                 ButtonAction ButtonAction = ButtonAction.Link) : FrameField(Id) {

    /// <summary>Attribute to be used for given button types.</summary>
    public string? ActionType => ButtonAction switch {
        ButtonAction.Link => "href",
        ButtonAction.Method => "onclick",
        _ => null
        };

    /// <summary>Value to be used for given button types.</summary>
    public string ActionValue => ButtonAction switch {
        ButtonAction.Link => "/" + Action,
        ButtonAction.Method => $"{Action} ()",
        _ => null
        };


    /// <inheritdoc/>
    public override string Type => "FrameButton";

    /// <summary>Function returning the visibility status of the item.</summary>
    public Func<IBinding, ButtonVisibility?> GetActive { get; init; }

    /// <summary>Function returning optional integer value to be displayed.</summary>
    public Func<IBinding, int?> GetInteger { get; init; }

    /// <summary>Function returning optional text value to be displayed.</summary>
    public Func<IBinding, string?> GetText { get; init; }

    /// <summary>Function returning optional anchor value to be displayed.</summary>
    public Func<IBinding, string?> GetAnchor { get; init; }

    /// <summary>Return a customized version.</summary>
    public Func<IBinding, ButtonVisibility, string?> GetCustomized { get; init; }
    }


/// <summary>Parsed version of a frame button</summary>
/// <param name="Id"></param>
/// <param name="Label"></param>
/// <param name="Action"></param>
/// <param name="Active"></param>
/// <param name="Integer"></param>
/// <param name="Text"></param>
/// <param name="Anchor"></param>
/// <param name="Customized"></param>
/// <param name="ButtonAction"></param>
public record FrameButtonParsed(
                string Id,
                string Label,
                string Action,
                string? Active,
                string? Integer,
                string? Text,
                string? Anchor,
                string? Customized,
                ButtonAction ButtonAction) : FrameButton(Id, Label, Action, ButtonAction) {


    }


/// <summary>Frame entry with a reference value.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
public record FrameRef(
                    string Id) : FrameField(Id) {
    /// <inheritdoc/>
    public override string Type => "FrameRef";
    }

/// <summary>Menu frame entry.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Reference">The referenced menu</param>
public record FrameRefMenu(
                    string Id,
                    string Reference) : FrameRef(Id) {

    /// <inheritdoc/>
    public override string Type => "FrameRefMenu";

    /// <summary>The menu</summary>
    public FrameMenu Menu { get; set; }
    }

/// <summary>Backing class specifier.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Reference">The referenced backing class</param>
public record FrameRefClass(
                    string Id,
                    string Reference) : FrameRef(Id) {

    /// <inheritdoc/>
    public override string Backing => Reference;

    /// <inheritdoc/>
    public override string Type => "FrameRefClass";

    /// <summary>The frame backing class.</summary>
    public FrameClass Class { get; set; }


    //public string? PresentationId { get; set; }


    /// <summary>Factory method mapping the presentation to an instance.</summary>
    public Func<IBinding, FramePresentation?>? Presentation { get; set; }

    /// <summary>Setter.</summary>
    public Action<IBacked, IBacked?> Set { get; init; }

    /// <summary>Getter.</summary>
    public Func<IBacked, IBacked?> Get { get; init; }

    }

/// <summary>Reference to a typed class defined elsewhere.</summary>
/// <typeparam name="T">The type of the associated backing class.</typeparam>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Reference">The referenced backing class</param>
public record FrameRefClass<T>(
                    string Id,
                    string Reference) : FrameRefClass(Id, Reference) where T : FrameClass {


    }

/// <summary>Reference to a form defined elsewhere.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Reference">The referenced form</param>
/// <param name="Fields"></param>
public record FrameRefForm(
                    string Id,
                    string Reference,
                    List<IFrameField> Fields) : FrameRef(Id) {

    /// <inheritdoc/>
    public override string Backing => Reference;

    /// <inheritdoc/>
    public override string Type => "FrameRefClass";

    //public string? Action => $"/{Id}";


    /// <summary>The frame backing class.</summary>
    public FrameClass Class { get; set; }
    //public string? PresentationId { get; set; }

    /// <summary>Factory method mapping the presentation to an instance.</summary>
    public Func<IBinding, FramePresentation?>? Presentation { get; set; }

    /// <summary>Setter.</summary>
    public Action<IBacked, IBacked?> Set { get; init; }

    /// <summary>Getter.</summary>
    public Func<IBacked, IBacked?> Get { get; init; }

    /// <summary>Factory method returning a new instance.</summary>
    /// <returns>A new instance.</returns>
    public virtual FrameClass Factory() => null;
    }

/// <summary>Reference to a typed form defined elsewhere</summary>
/// <typeparam name="T">The type of the associated backing class.</typeparam>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Reference">The referenced form</param>
/// <param name="Fields"></param>
public record FrameRefForm<T>(
                    string Id,
                    string Reference,
                    List<IFrameField> Fields) : FrameRefForm(Id, Reference, Fields) where T : FrameClass, new() {

    /// <inheritdoc/>
    public override string Type => "FrameRefClass";

    /// <inheritdoc/>
    public override FrameClass Factory() => new T();
    }

/// <summary>Reference to a list.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Reference">The referenced list</param>
public record FrameRefList(
                    string Id,
                    string Reference) : FrameRef(Id) {

    /// <summary>Returns the nth element of a list <paramref name="x"/>. This 
    /// is a workarround for the limitations of the C#10 implementation of generics.</summary>
    /// <param name="x">List to return the item from.</param>
    /// <param name="index">Index of the element to return.</param>
    /// <returns>The item.</returns>
    public virtual FrameClass Item(Object? x, int index) => null;

    /// <summary>Count of the number of entries in the list <paramref name="x"/>.This 
    /// is a workarround for the limitations of the C#10 implementation of generics.</summary>
    /// <param name="x">List to return the entry count of.</param>
    /// <returns>The number of entries in the list.</returns>
    public virtual int Count(Object? x) => 0;

    /// <inheritdoc/>
    public override string Backing => $"List<{Reference}>";

    /// <inheritdoc/>
    public override string Type => "FrameRefClass";


    //public string PresentationId { get; set; }

    /// <summary>Factory method mapping the presentation to an instance.</summary>
    public Func<IBinding, FramePresentation?>? Presentation { get; set; }

    //public FrameClass Class { get; set; }


    /// <summary>Setter.</summary>
    public Action<IBacked, Object?> Set { get; init; }


    /// <summary>Getter.</summary>
    public Func<IBacked, Object?> Get { get; init; }
    }

/// <summary>Reference to a typed list.</summary>
/// <typeparam name="T">The type of the associated backing class.</typeparam>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Reference">The referenced list</param>
public record FrameRefList<T>(
                    string Id,
                    string Reference) : FrameRefList(Id, Reference) where T : FrameClass {

    /// <inheritdoc/>
    public override FrameClass Item(Object? x, int index) => (x as List<T>)![index];

    /// <inheritdoc/>
    public override int Count(Object? x) => (x as List<T>)!.Count;


    }




/// <summary>Boolean form entry, typically rendered as a checkbox.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Set"></param>
/// <param name="Get"></param>
public record FrameBoolean(
            string Id,
            Action<IBinding, bool?>? Set = null,
            Func<IBinding, bool?>? Get = null) : PropertyBoolean(Id, Set, Get), IFrameField {

    /// <summary>Prompt to be displayed to the user.</summary>
    public string Prompt { get; set; }

    /// <inheritdoc/>
    public bool Hidden { get; set; } = false;

    /// <inheritdoc/>
    public string Description { get; set; }


    /// <inheritdoc/>
    public string Backing => "bool";


    /// <inheritdoc/>
    public virtual string Type => "FrameBoolean";

    }


/// <summary>Integer form entry.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Set"></param>
/// <param name="Get"></param>

public record FrameInteger(
            string Id,
            Action<IBinding, int?>? Set = null,
             Func<IBinding, int?>? Get = null
            ) : PropertyInteger32(Id, Set, Get), IFrameField {

    /// <inheritdoc/>
    public string Prompt { get; set; }

    /// <inheritdoc/>
    public bool Hidden { get; set; } = false;

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public string Backing => "int";

    /// <inheritdoc/>
    public virtual string Type => "FrameInteger";
    }

/// <summary>Integer form entry that is a count of a set of items.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Set"></param>
/// <param name="Get"></param>
public record FrameCount(
            string Id,
            Action<IBinding, int?>? Set = null,
             Func<IBinding, int?>? Get = null
            ) : FrameInteger(Id, Set, Get) {

    /// <inheritdoc/>
    public override string Type => "FrameCount";
    }

/// <summary>Date Time form entry.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Set"></param>
/// <param name="Get"></param>
public record FrameDateTime(
            string Id,
            Action<IBinding, System.DateTime?>? Set = null,
            Func<IBinding, System.DateTime?>? Get = null
            ) : PropertyDateTime(Id, Set, Get), IFrameField {

    /// <inheritdoc/>
    public string Prompt { get; set; }

    /// <inheritdoc/>
    public bool Hidden { get; set; } = false;

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public string Backing => "System.DateTime";

    /// <inheritdoc/>
    public virtual string Type => "FrameDateTime";
    }

/// <summary>Text string form entry.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Set"></param>
/// <param name="Get"></param>
public record FrameString(
            string Id,
            Action<IBinding, string?>? Set = null,
            Func<IBinding, string?>? Get = null) : PropertyString(Id, Set, Get), IFrameField {

    /// <inheritdoc/>
    public string Prompt { get; set; }

    /// <inheritdoc/>
    public bool Hidden { get; set; } = false;

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>

    public string Backing => "string";

    /// <inheritdoc/>
    public virtual string Type => "FrameString";
    }


/// <summary>Text string form entry.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Set"></param>
/// <param name="Get"></param>
public record FrameBlock(
            string Id,
            Action<IBinding, string?>? Set = null,
            Func<IBinding, string?>? Get = null) : FrameString (Id, Set, Get) {

    /// <inheritdoc/>
    public List<System.String> Text { get; set; }


    /// <inheritdoc/>
    public override string Type => "FrameBlock";
    }




/// <summary>Text string form entry covering multiple lines.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Set"></param>
/// <param name="Get"></param>
public record FrameText(
            string Id,
            Action<IBinding, string?>? Set = null,
            Func<IBinding, string?>? Get = null) : FrameString(Id, Set, Get) {

    /// <inheritdoc/>
    public override string Type => "FrameText";
    }

/// <summary>Rich text string form entry with usual text decorations.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Set"></param>
/// <param name="Get"></param>
public record FrameRichText(
            string Id,
            Action<IBinding, string?>? Set = null,
            Func<IBinding, string?>? Get = null) : FrameString(Id, Set, Get) {

    /// <inheritdoc/>
    public override string Type => "FrameRichText";
    }

/// <summary>Anchor form entry.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Set"></param>
/// <param name="Get"></param>
public record FrameAnchor(
            string Id,
            Action<IBinding, BackingTypeLink?>? Set = null,
            Func<IBinding, BackingTypeLink?>? Get = null) : IFrameField {


    /// <inheritdoc/>
    public string Tag { get; init; } = Id;

    /// <inheritdoc/>
    public string Type => "FrameAnchor";

    /// <inheritdoc/>
    public string Backing => "BackingTypeLink";

    /// <inheritdoc/>
    public string Prompt { get; set; }

    /// <inheritdoc/>
    public bool Hidden { get; set; } = false;

    /// <inheritdoc/>
    public string Description { get; set; }
    }



//public record FrameResource<T>) {
//    }(


/// <summary>Image form entry.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Set"></param>
/// <param name="Get"></param>
public record FrameImage(
            string Id,
            Action<IBinding, string?>? Set = null,
            Func<IBinding, string?>? Get = null) : PropertyString(Id, Set, Get), IFrameField {


    /// <inheritdoc/>
    public string Prompt { get; set; }

    /// <inheritdoc/>
    public bool Hidden { get; set; } = false;

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public string Backing => "string";

    /// <inheritdoc/>
    public virtual string Type => "FrameImage";
    }

/// <summary>Avatar form entry.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Get"></param>
public record FrameAvatar(
            string Id,
            Func<IBinding, string?>? Get = null) : FrameField(Id) {

    /// <inheritdoc/>
    public override string Type => "FrameAvatar";

    }




// Non property entries, are not serialized.
/// <summary>Chooser form entry.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Options"></param>
public record FrameChooser(
                string Id,
                List<FrameChooserOption> Options) : FrameField(Id) {

    /// <inheritdoc/>
    public override string Type => "FrameButton";
    }

/// <summary>Option in a chooser form entry</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Label"></param>
public record FrameChooserOption(
            string Id,
            string Label) {
    }
/// <summary>Form separator decoration.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
public record FrameSeparator(string Id) : FrameField(Id) {

    /// <inheritdoc/>
    public override string Type => "FrameSeparator";
    }

/// <summary>Presentation on a form backing type.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
public record FramePresentation(string Id) : FrameField(Id) {

    /// <summary>Uid field, used for code construction.</summary>
    public string UidField { get; init; }

    /// <inheritdoc/>
    public override string Type => "FramePresentation";

    /// <summary>Factory method returning a new instance of the element 
    /// with the specified ID.</summary>
    public Func<IBacked, string?> GetUid { get; init; }


    /// <summary>Sections in the presentation.</summary>
    public virtual List<FrameSection> Sections { get; init; }
    }



/// <summary>Division within a document for formatting.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
public record FrameDiv(string Id) : IFrameField {

    /// <summary>Fields in the section.</summary>
    public virtual List<IFrameField> Fields { get; init; }

    /// <inheritdoc/>
    public string Backing { get; }

    /// <inheritdoc/>
    public string Tag { get; init; }

    /// <inheritdoc/>
    public string Type { get; }

    /// <inheritdoc/>
    public string Prompt { get; set; }

    /// <inheritdoc/>
    public bool Hidden { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }
    }



/// <summary>Section within a form.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
public record FrameSection(string Id) {

    /// <summary></summary>
    public string AnchorField { get; init; }

    /// <summary>Factory method returning by anchor.</summary>
    public virtual Func<IBacked, string?>? GetAnchor { get; init; } = null;

    /// <summary>Fields in the section.</summary>
    public virtual List<IFrameField> Fields { get; init; }

    }

/// <summary>Submenu</summary>
/// <param name="Id">Unique identifier within the frame.</param>
/// <param name="Label"></param>
public record FrameSubmenu(
                string Id,
                string Label) : FrameField(Id) {

    /// <inheritdoc/>
    public override string Type => "FrameSubmenu";

    /// <summary>Fields in the section.</summary>
    public virtual List<IFrameField> Fields { get; init; }

    }

/// <summary>Icon</summary>
/// <param name="Id">Unique identifier within the frame.</param>
public record FrameIcon(string Id) : FrameField(Id) {

    /// <inheritdoc/>
    public override string Type => "FrameIcon";

    }

/// <summary>File entry.</summary>
/// <param name="Id">Unique identifier within the frame.</param>
public record FrameFile(string Id) : Property(Id, false), IFrameField {

    /// <summary>The file type</summary>
    public string FileType { get; set; }

    /// <summary>User prompt</summary>
    public string Prompt { get; set; }

    /// <summary>If true, entry is hidden</summary>
    public bool Hidden { get; set; } = false;

    /// <summary>Description of the entry</summary>
    public string Description { get; set; }

    /// <summary>Backing type.</summary>
    public string Backing => "BackingTypeFile";

    /// <summary>Type</summary>
    public virtual string Type => "FrameFile";

    /// <summary>Setter.</summary>
    public Action<IBinding, BackingTypeFile?> Set { get; init; }

    /// <summary>Getter.</summary>
    public Func<IBinding, BackingTypeFile?> Get { get; init; }

    /// <inheritdoc/>
    public override bool IsNull(IBinding data) {
        throw new NotImplementedException();
        }


    }