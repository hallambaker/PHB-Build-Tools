using Goedel.Discovery;
using Goedel.Protocol;
using Goedel.Registry;

namespace Goedel.Sitebuilder;

public  interface IPageContext {
    }



public record FormReaction(
            string Id,
            string Text) {
    }

public class FrameSet {
    public FramePage Page { get; set; }

    ///<summary>Directory to store persisted data to.</summary>
    public string Directory { get; set; }

    ///<summary>Directory where the resource files are stored.</summary>
    public string ResourceFiles { get; set; }


    public IPersistSite PersistPlace { get; set; }



    public List<Resource> Resources { get; set; } = null;
    public List<Resource> EndResources { get; set; } = null;

    public virtual string Namespace { get; set; }
    public virtual List<FramePage> Pages { get; init; } = [];
    public virtual List<FrameMenu> Menus { get; init; } = [];
    public virtual List<FrameSelector> Selectors { get; init; } = [];
    public virtual List<FrameClass> Classes { get; init; } = [];


    public Dictionary<string, FramePage> PageDirectory { get; } = [];



    /// <summary>
    /// Resolve id references to field identifiers and compile the places
    /// directory.
    /// </summary>
    /// <param name="entry">The entry to resolve.</param>
    public void ResolveReferences(IBacked entry) {
        if (entry is FramePage page) {
            PageDirectory.Add(page.PathStem, page);
            }

        entry.FrameSet = this;
        foreach (var field in entry.Fields) {
            switch (field) {
                case FrameRefMenu item: {
                    item.Menu = GetField(Menus, item.Reference);
                    break;
                    }
                case FrameRefClass item: {
                    item.Class = GetField(Classes, item.Reference);
                    break;
                    }
                case FrameRefForm item: {
                    item.Class = GetField(Classes, item.Reference);
                    break;
                    }
                }
            }
        }

    T? GetField<T>(List<T> list, string id) where T: IBacked {
        foreach (var field in list) {
            if (field.Tag == id) {
                return field;
                }
            }

        return default;
        }

    public virtual string IconPath(string id) => $"Resources/Icons/{id}.svg";

    }


public interface IBacked : IBinding{
    FrameSet FrameSet { get; set; }
    ///<summary>The identifier</summary>
    string Tag { get; }
    string Id { get; }
    ///<summary>The fields</summary>
    List<IFrameField> Fields { get; }

    FramePresentation Presentation => null;

    string Type { get; }

    FrameClass? Parent { get;  }

    System.DateTime StartRender { get; set; }
    }


public class FrameBacker {

    public virtual FramePresentation Presentation { get; init; }
    public System.DateTime StartRender { get; set; }
    public string Id { get; set; }
    public string Tag { get; init; }
    public FrameBacker(string id) {
        Id = id;
        Tag = id;
        }
    public virtual Protocol.Property[] _Properties => throw new NotImplementedException();

    public virtual Binding _Binding => throw new NotImplementedException();

    }


public interface IPersistSite {

    ///<summary>State management interface to keep us logged in.</summary>
    ServerCookieManager ServerCookieManager { get; set; }

    ///<summary>The Oauth Client</summary>
    OauthClient OauthClient { get; set; }

    ///<summary>The frame defintions being serviced.</summary>
    FrameSet FrameSet { get; set; }

    }

public class FramePage: FrameBacker, IBacked {

    public IPageContext Context { get; set; } = null;
    public string Anchor => $"/{PathStem}";

    public virtual string PathStem => Id;

    public int PathParameters { get; set; } = 0;




    public List<Resource> Resources { get; set; } = null;

    public List<Resource> EndResources { get; set; } = null;
    public Resource FaviCon { get; set; } = null;
    public string PageTitle { get; set; } = null;



    public FrameSet FrameSet { get; set; }
    public string Title { get; init; }
    public string? Container { get; init; }
    public virtual List<IFrameField> Fields {get; init;}

    public FrameClass? Parent { get; init; } = null;

    public string Type => "FramePage";


    public FramePage(string id, string title, List<IFrameField> fields) : base(id) {
        Fields = fields;
        Title = title;
        }


    /// <summary>
    /// Request page produced from this template from the request context 
    /// <paramref name="context"/>.
    /// </summary>
    /// <param name="persistPlace"></param>
    /// <returns></returns>
    /// <param name="context"></param>
    public virtual FramePage GetPage(
                IPersistSite persistPlace, IPageContext context) => this;

    }

public class FrameMenu : FrameBacker, IBacked {

    public virtual FramePage Page { get; init; }

    public FrameSet FrameSet { get; set; }
    public virtual List<IFrameField> Fields { get; init; }

    public string Type => "FrameMenu";

    public virtual FrameMenu Create (FramePage page) => throw new NotImplementedException();


    public FrameClass? Parent { get; init; } = null;

    public FrameMenu(string id, List<IFrameField> fields) : base(id) {
        Fields = fields;
        }
    }

public class FrameSelector : FrameBacker, IBacked {
    public FrameSet FrameSet { get; set; }
    /// <inheritdoc/>
    public virtual List<IFrameField> Fields { get; init; }
    public string Type => "FrameSelector";

    public FrameClass? Parent { get; init; } = null;

    public FrameSelector(string id, List<IFrameField> fields) : base(id) {
        Fields = fields;
        }
    }


public record CallbackResult(
            HttpStatusCode Code,
            List<FormReaction>? Reactions,
            string? Redirect,
            List<Cookie> Cookies = null
            ) {
    }


public class FrameClass : FrameBacker, IBacked {

    public const string DefaultAvatar = "Resources/Icons/AvatarDefault.svg";

    public FrameSet FrameSet { get; set; }
    public string Type => "FrameClass";
    public virtual List<IFrameField> Fields { get; set; }




    public FrameClass? Parent { get; set; } = null;

    public string? ParentId { get; init; } = null;

    public virtual string? GetAvatar => DefaultAvatar;



    public virtual Task<CallbackResult> Callback(
                IPersistSite persistPlace) {

        return Task.FromResult (new CallbackResult(HttpStatusCode.OK, null, null));
        }

    //public virtual HttpStatusCode Commit(
    //            IPersistPlace persistPlace,
    //            out string? redirect) {
    //    redirect = null;
    //    return HttpStatusCode.OK;
    //    }

    public FrameClass(string id) : base(id) {
        }
    }





/// <summary>
/// Frame fields
/// </summary>
public abstract record FrameField : IFrameField {

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

public enum ButtonVisibility {
    Available,
    Active,
    Disabled,
    None
    }

public enum ButtonAction {
    Null,
    Link,
    Method
    
    }

public record FrameButton(
                string Id,
                string Label,
                string Action,

                 ButtonAction ButtonAction= ButtonAction.Link) : FrameField (Id) {

    public string? ActionType => ButtonAction switch {
        ButtonAction.Link => "href",
        ButtonAction.Method => "onclick",
        _ => null
        };

    public string ActionValue => ButtonAction switch {
        ButtonAction.Link => "/" + Action,
        ButtonAction.Method => $"{Action} ()",
        _ => null
        };



    public override string Type => "FrameButton";

    public Func<IBinding, ButtonVisibility?> GetActive { get; init; }
    public Func<IBinding, int?> GetInteger { get; init; }
    public Func<IBinding, string?> GetText { get; init; }
    }

public record FrameButtonParsed(
                string Id,
                string Label,
                string Action,
                string? Active,
                string? Integer,
                string? Text,
                ButtonAction ButtonAction) : FrameButton(Id, Label, Action, ButtonAction) {


    }


public record FrameRef(
                    string Id) : FrameField(Id) {
    public override string Type => "FrameRef";
    }

public record FrameRefMenu(
                    string Id,
                    string Reference) : FrameRef(Id) {
    public override string Type => "FrameRefMenu";

    public FrameMenu Menu { get; set; }
    }


public record FrameRefClass(
                    string Id,
                    string Reference) : FrameRef(Id)  {

    /// <inheritdoc/>
    public override string Backing =>  Reference;

    public override string Type => "FrameRefClass";

    public FrameClass Class { get; set; }
    public string? PresentationId { get; set; }



    public Func<IBinding,FramePresentation?>? Presentation { get; set; }

    public Action<IBacked, IBacked?> Set { get; init; }
    public Func<IBacked, IBacked?> Get { get; init; }

    }

public record FrameRefClass<T>(
                    string Id,
                    string Reference) : FrameRefClass(Id, Reference) where T : FrameClass {


    }


public record FrameRefForm(
                    string Id,
                    string Reference,
                    List<IFrameField> Fields) : FrameRef(Id) {
    public override string Backing => Reference;

    public override string Type => "FrameRefClass";

    public string? Action => $"/{Id}";


    public FrameClass Class { get; set; }
    public string? PresentationId { get; set; }

    public Func<IBinding, FramePresentation?>? Presentation { get; set; }

    public Action<IBacked, IBacked?> Set { get; init; }
    public Func<IBacked, IBacked?> Get { get; init; }


    public virtual FrameClass Factory() => null; 
    }

public record FrameRefForm<T>(
                    string Id,
                    string Reference,
                    List<IFrameField> Fields ) : FrameRefForm(Id, Reference, Fields) where T : FrameClass, new() {

    public override string Type => "FrameRefClass";

    /// <inheritdoc/>
    public override FrameClass Factory() => new T();
    }


public record FrameRefList(
                    string Id,
                    string Reference) : FrameRef(Id) {


    public virtual FrameClass Item(Object? x, int index) => null;
    public virtual int Count(Object? x) => 0;

    /// <inheritdoc/>
    public override string Backing =>  $"List<{Reference}>" ;

    public override string Type => "FrameRefClass";


    public string PresentationId { get; set; }
    public Func<IBinding, FramePresentation?>? Presentation { get; set; }

    public FrameClass Class { get; set; }

    public Action<IBacked, Object?> Set { get; init; }
    public Func<IBacked, Object?> Get { get; init; }
    }


public record FrameRefList<T>(
                    string Id,
                    string Reference) : FrameRefList(Id, Reference) where T : FrameClass {


    public override FrameClass Item(Object? x, int index) => (x as List<T>)![index];

    public override int Count(Object? x)=> (x as List<T>)!.Count;


    }




// Boolean backing type
public record FrameBoolean(
            string Id,
            Action<IBinding, bool?>? Set = null,
            Func<IBinding, bool?>? Get = null) : PropertyBoolean(Id, Set, Get) ,IFrameField{
    public string Prompt { get; set; }
    public bool Hidden { get; set; } = false;
    public string Description { get; set; }
    public string Backing => "bool";
    public virtual string Type => "FrameBoolean";

    }


// Integer backing type

public record FrameInteger(
            string Id,
            Action<IBinding, int?>? Set = null,
             Func<IBinding, int?>? Get = null
            ) : PropertyInteger32(Id, Set, Get), IFrameField {
    public string Prompt { get; set; }
    public bool Hidden { get; set; } = false;
    public string Description { get; set; }
    public string Backing => "int";
    public virtual string Type => "FrameInteger";
    }
public record FrameCount(
            string Id,
            Action<IBinding, int?>? Set = null,
             Func<IBinding, int?>? Get = null
            ) : FrameInteger(Id, Set, Get) {
    public override string Type => "FrameCount";
        }

// DateTime bacxking type

public record FrameDateTime(
            string Id,
            Action<IBinding, System.DateTime?>? Set = null,
            Func<IBinding, System.DateTime?>? Get = null
            ) : PropertyDateTime(Id, Set, Get), IFrameField {
    public string Prompt { get; set; }
    public bool Hidden { get; set; } = false;
    public string Description { get; set; }
    public string Backing => "System.DateTime";
    public virtual string Type => "FrameDateTime";
    }


// String backing type

public record FrameString(
            string Id,
            Action<IBinding, string?>? Set = null,
            Func<IBinding, string?>? Get=null) : PropertyString (Id, Set, Get), IFrameField {
    public string Prompt { get; set; }
    public bool Hidden { get; set; } = false;
    public string Description { get; set; }

    public string Backing => "string";
    public virtual string Type => "FrameString";
    }



public record FrameText(
            string Id,
            Action<IBinding, string?>? Set = null,
            Func<IBinding, string?>? Get = null) : FrameString(Id, Set, Get) {
    public override string Type => "FrameText";
    }


public record FrameRichText(
            string Id,
            Action<IBinding, string?>? Set = null,
            Func<IBinding, string?>? Get = null) : FrameString(Id, Set, Get) {
    public override string Type => "FrameRichText";
    }


public record FrameAnchor(
            string Id,
            Action<IBinding, BackingTypeLink?>? Set = null,
            Func<IBinding, BackingTypeLink?>? Get = null) : IFrameField {

    public string Tag { get; init; } = Id;
    public string Type => "FrameAnchor";
    public string Backing => "BackingTypeLink";
    public string Prompt { get; set; }
    public bool Hidden { get; set; } = false;
    public string Description { get; set; }
    }



//public record FrameResource<T>) {
//    }(
 


public record FrameImage(
            string Id,
            Action<IBinding, string?>? Set = null,
            Func<IBinding, string?>? Get = null) : PropertyString(Id, Set, Get), IFrameField {
    
    public string Prompt { get; set; }
    public bool Hidden { get; set; } = false;
    public string Description { get; set; }
    public string Backing => "string";
    public virtual string Type => "FrameImage";
    }


public record FrameAvatar(
            string Id,
            Func<IBinding, string?>? Get = null) : FrameField(Id) {
    public override string Type => "FrameAvatar";

    }




// Non property entries, are not serialized.
public record FrameChooser(
                string Id,
                List<FrameChooserOption> Options) : FrameField(Id) {
    public override string Type => "FrameButton";
    }


public record FrameChooserOption(
            string Id,
            string Label) {
    }

public record FrameSeparator(string Id) : FrameField(Id) {
    public override string Type => "FrameSeparator";
    }

public record FramePresentation(string Id) : FrameField(Id) {
    public override string Type => "FramePresentation";

    public string UidField { get; init; }

    public Func<IBacked, string?> GetUid { get; init; }

    public virtual List<FrameSection> Sections { get; init; }
    }





public record FrameSection(string Id) {
    public virtual List<IFrameField> Fields { get; init; }

    }

public record FrameSubmenu(
                string Id,
                string Label) : FrameField(Id) {
    public override string Type => "FrameSubmenu";
    public virtual List<IFrameField> Fields { get; init; }

    }
public record FrameIcon(string Id) : FrameField(Id) {
    public override string Type => "FrameIcon";

    }


public record FrameFile(string Id) : Property(Id, false), IFrameField{

    public string FileType { get; set; }

    public string Prompt { get; set; }
    public bool Hidden { get; set; } = false;
    public string Description { get; set; }
    public string Backing => "BackingTypeFile";
    public virtual string Type => "FrameFile";

    public Action<IBacked, BackingTypeFile?> Set { get; init; }
    public Func<IBacked, BackingTypeFile?> Get { get; init; }


    public override bool IsNull(IBinding data) {
        throw new NotImplementedException();
        }


    }