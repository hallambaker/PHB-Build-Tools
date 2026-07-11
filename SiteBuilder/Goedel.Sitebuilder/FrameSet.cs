using Goedel.Discovery;
using Goedel.Protocol;
using Goedel.Registry;

namespace Goedel.Sitebuilder;

/// <summary>Page context.</summary>
public  interface IPageContext {
    }


/// <summary>Text to be shown to the user to explain the reason a form failed validation.</summary>
/// <param name="Id">Entry identifier.</param>
/// <param name="Text">Text to display.</param>
public record FormReaction(
            string Id,
            string Text) {
    }

/// <summary>Describes a set of frames making up a GUI.</summary>
public class FrameSet {

    /// <summary>The namespace to instantiate the frameset description into.</summary>
    public string Namespace { get; set; }




    /// <summary>The default page.</summary>
    public FramePage Page { get; set; }

    ///<summary>Directory to store persisted data to.</summary>
    public string Directory { get; init; }
    ///<summary>Directory to store per member data to.</summary>
    public string Members { get; init; }

    /// <summary>Directory to record logs.</summary>
    public string Logs { get; init; }

    ///<summary>Directory where the resource files are stored.</summary>
    public string ResourceFiles { get; init; }

    /// <summary>Directory to store repository files.</summary>
    public string RepositoryFiles { get; init; }

    /// <summary>Directory to store icons.</summary>
    public string IconFiles => Path.Combine(ResourceFiles, "resources/icons");


    ///// <summary>List of site administrators.</summary>
    //public List<string> Administrators { get; init; }

    ///// <summary>The default site.</summary>
    //public string DefaultSite { get; init; }



    //public IPersistSite PersistPlace { get; set; }

    /// <summary>Random seed value used to force static values for testing.</summary>
    public string RandomSeed { get; init; } = "";

    /// <summary>Directory of private keys.</summary>
    public string PrivateKeys { get; init; }


    /// <summary>Resources to be added to the start of every page.</summary>
    public List<Resource> Resources { get; set; } = null;

    /// <summary>Resources to be added to the end of every page.</summary>
    public List<Resource> EndResources { get; set; } = null;


    //public virtual string Namespace { get; set; }

    /// <summary>List of pages</summary>
    public virtual List<FramePage> Pages { get; init; } = [];

    /// <summary>List of menus</summary>
    public virtual List<FrameMenu> Menus { get; init; } = [];

    /// <summary>List of selectors</summary>
    public virtual List<FrameSelector> Selectors { get; init; } = [];

    /// <summary>List of backing classes.</summary>
    public virtual List<FrameClass> Classes { get; init; } = [];

    /// <summary>Directory mapping prefix to page template.</summary>
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

    static T? GetField<T>(List<T> list, string id) where T: IBacked {
        foreach (var field in list) {
            if (field.Tag == id) {
                return field;
                }
            }

        return default;
        }

    /// <summary>Maps Icon ID to the resource URI.</summary>
    /// <param name="id">The icon id</param>
    /// <returns>URI local path</returns>
    public virtual string IconPath(string id) => $"/Resources/Icons/{id}.svg";

    /// <summary>Maps member ID to the Member avatar ID.</summary>
    /// <param name="id">The member id</param>
    /// <returns>URI local path</returns>
    public virtual string MemberPath(string id) => $"/Resources/Members/{id}.svg";


    }

/// <summary>Button visibility states.</summary>
public enum ButtonVisibility {
    /// <summary>The button is visible and can be selected.</summary>
    Available,
    /// <summary>The button is visible and is active but cannot be selected 
    /// because it is already active.</summary>
    Active,
    /// <summary>The button is visible in a chosen state and can be selected 
    /// to unchoose it.</summary>
    Checked,
    /// <summary>The button is visible in a chosen state and can be unchosen.</summary>
    Disabled,
    /// <summary>The button is not visible</summary>
    None
    }


