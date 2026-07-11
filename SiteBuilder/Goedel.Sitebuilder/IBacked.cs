namespace Goedel.Sitebuilder;

/// <summary>Interface satisfied by backing classes.</summary>
public interface IBacked : IBinding{

    /// <summary>The set of frame resources.</summary>
    FrameSet FrameSet { get; set; }

    ///<summary>The identifier</summary>
    string Tag { get; }

    /// <summary>Unique identifier.</summary>
    string Id { get; }

    /// <summary>The frame fields.</summary>
    List<IFrameField> Fields { get; }

    /// <summary>Presentation to be displayed in this context.</summary>
    FramePresentation Presentation => null;

    /// <summary>The class type, used for deserialization.</summary>
    string Type { get; }

    /// <summary>The parent class</summary>
    FrameClass? Parent { get;  }

    /// <summary>Time rendering started.</summary>
    System.DateTime StartRender { get; set; }
    }
