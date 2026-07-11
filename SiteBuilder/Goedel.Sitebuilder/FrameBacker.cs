using System.Reflection.Metadata;

namespace Goedel.Sitebuilder;

/// <summary>Parent class for frame backer description.</summary>
/// <remarks>Constructor returning new instance <paramref name="id"/></remarks>
/// <param name="id">The unique identifier.</param>
public abstract class FrameBacker(string id) {

    /// <inheritdoc/>
    public virtual FramePresentation Presentation { get; init; }

    /// <inheritdoc/>
    public System.DateTime StartRender { get; set; }

    /// <inheritdoc/>
    public string Id { get; set; } = id;

    /// <inheritdoc/>
    public string Tag { get; init; } = id;

    /// <inheritdoc/>
    public virtual List<IFrameField> Fields { get; set; }

    /// <summary>Array of bound properties.</summary>
#pragma warning disable IDE1006 // Naming Styles
    public virtual Protocol.Property[] _Properties => throw new NotImplementedException();

    /// <summary>The propery binding.</summary>
    public virtual Binding _Binding => throw new NotImplementedException();
#pragma warning restore IDE1006 // Naming Styles


    /// <summary>Dummy binding declaration.</summary>
    protected static readonly Binding<FrameClass> _binding = null!;


    }

/// <summary>Parent class for menu backer description</summary>
public class FramePage : FrameBacker, IBacked {

    /// <summary>The page context</summary>
    public IPageContext Context { get; set; } = null;
    //public string Anchor => $"/{PathStem}";

    /// <summary>Stem of the path to display.</summary>
    public virtual string PathStem => Id;


    //public int PathParameters { get; set; } = 0;



    /// <summary>List of resources to be entered in the page header.</summary>
    public List<Resource> Resources { get; set; } = null;

    /// <summary>List of resources to be entered in the page footer.</summary>
    public List<Resource> EndResources { get; set; } = null;

    /// <summary>The favicon to show.</summary>
    public Resource FaviCon { get; set; } = null;

    /// <summary>The page title</summary>
    public string PageTitle { get; set; } = null;


    /// <summary>The frame set to present</summary>
    public FrameSet FrameSet { get; set; }

    /// <summary>Page title.</summary>
    public string Title { get; init; }

    /// <summary>Container to wrap page in.</summary>
    public string? Container { get; init; }

    /// <inheritdoc/>
    public FrameClass? Parent { get; init; } = null;

    /// <inheritdoc/>
    public string Type => "FramePage";


    /// <summary>Constructor, return a new instance with the specified fields.</summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="title">Page title</param>
    /// <param name="fields">The page fields</param>
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

/// <summary>Backing class for a menu.</summary>
public  class FrameMenu : FrameBacker, IBacked {

    /// <summary>The page the menu appears in.</summary>
    public virtual FramePage Page { get; init; }

    /// <inheritdoc/>
    public FrameSet FrameSet { get; set; }


    /// <inheritdoc/>
    public string Type => "FrameMenu";

    /// <summary>Factory method, create a new instance for page <paramref name="page"/></summary>
    /// <param name="page">The page in which the menu appears.</param>
    /// <returns>The created instance.</returns>
    public virtual FrameMenu Create(FramePage page) => throw new NotImplementedException();

    /// <inheritdoc/>
    public FrameClass? Parent { get; init; } = null;


    /// <summary>Constructor, return a new instance with the specified fields.</summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="fields">The menu entries.</param>

    public FrameMenu(string id, List<IFrameField> fields) : base(id) {
        Fields = fields;
        }
    }

/// <summary>Frame selector backer, pick an entry from a collection.</summary>
public  class FrameSelector : FrameBacker, IBacked {

    /// <inheritdoc/>
    public FrameSet FrameSet { get; set; }

    /// <inheritdoc/>
    public string Type => "FrameSelector";

    /// <summary>The parent class</summary>
    public FrameClass? Parent { get; init; } = null;


    /// <summary>Constructor, return a new instance with the specified fields.</summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="fields">The selector fields</param>

    public FrameSelector(string id, List<IFrameField> fields) : base(id) {
        Fields = fields;
        }
    }

/// <summary>Frame backing class descriptor, pick an entry from a collection.</summary>
/// <remarks>Constructor, return a new instance with the specified fields.</remarks>
/// <param name="id">Unique identifier</param>
public class FrameClass(string id) : FrameBacker(id), IBacked {

    /// <summary>Default avatar icon.</summary>
    public const string DefaultAvatar = "Resources/Icons/AvatarDefault.svg";

    /// <inheritdoc/>
    public FrameSet FrameSet { get; set; }

    /// <inheritdoc/>
    public string Type => "FrameClass";

    /// <summary>The parent class (if it exists).</summary>
    public FrameClass? Parent { get; set; } = null;

    /// <summary>Parent identifier, used by code generator.</summary>
    public string? ParentId { get; init; } = null;

    //public virtual string? GetAvatar => DefaultAvatar;

    /// <summary>Asynchronous callback method</summary>
    /// <param name="persistPlace">The page context.</param>
    /// <returns>The transaction result.</returns>
    public virtual Task<CallbackResult> Callback(
                IPageContext persistPlace) {

        return Task.FromResult(new CallbackResult(HttpStatusCode.OK, null, null));
        }
    }



