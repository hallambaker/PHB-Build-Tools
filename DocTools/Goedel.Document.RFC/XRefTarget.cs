namespace Goedel.Document.RFC;

/// <summary>
/// Target of a cross reference.
/// </summary>
public class XRefTarget {

    ///<summary>The text.</summary> 
    public string Text => GetText();
    
    ///<summary>The section referenced.</summary> 
    public Section Section;
    
    ///<summary>The text block referenced.</summary> 
    public TextBlock TextBlock;

    /// <summary>
    /// Return the text corresponding to the target/
    /// </summary>
    /// <returns>The cross reference link text..</returns>
    public string GetText() {
        if (Section != null) {
            return "Section " + Section.Number;
            }
        return TextBlock?.AnchorText ?? "TBS";
        }


    }
