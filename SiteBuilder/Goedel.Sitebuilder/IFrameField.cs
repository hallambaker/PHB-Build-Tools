namespace Goedel.Sitebuilder;

/// <summary>
/// Frame field interface
/// </summary>
public interface IFrameField {

    ///<summary>The backing type</summary>
    string Backing { get; }

    ///<summary>Identifier</summary>
    string Id { get; }

    ///<summary>Tag</summary>
    string Tag { get; init; }

    ///<summary>Type</summary>
    string Type { get; }

    ///<summary>Prompt to show if used in a form.</summary>
    string Prompt { get; set; }

    ///<summary>If true, this is a hidden field.</summary>
    bool Hidden { get; set; }

    ///<summary>Long description for use in hover text, etc.</summary>
    string? Description { get; set; }


    }