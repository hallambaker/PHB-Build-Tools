namespace Goedel.Sitebuilder;

/// <summary>
/// Text used in page generation, to be pulled from properties
/// </summary>
public record PageText {

    ///<summary>The submit label text</summary>
    public string Submit { get; init; } = "Submit";

    ///<summary>The reset label text</summary>
    public string Reset { get; init; } = "Reset";

    ///<summary>The cancel label text</summary>
    public string Cancel { get; init; } = "Cancel";

    ///<summary></summary>
    public string InvalidEntry { get; init; } = "Invalid Entry";

    ///<summary></summary>
    public string Required { get; init; } = "Required";

    ///<summary>English text</summary>
    public static PageText English { get; } = new PageText();



    }
