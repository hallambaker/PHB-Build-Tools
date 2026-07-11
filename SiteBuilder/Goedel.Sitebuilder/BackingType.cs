namespace Goedel.Sitebuilder;


/// <summary>
/// Base type for extended backing types
/// </summary>
public record BackingType {
    }


/// <summary>
/// Backing type for uploaded file.
/// </summary>
/// <param name="OriginalFileName">Suggested file name</param>
/// <param name="Data">The raw data</param>
/// <param name="ContentType">IANA media type.</param>
public record BackingTypeFile(
            string OriginalFileName,
            string ContentType,
            byte[] Data
            ) : BackingType() {



    }





/// <summary>
/// Image backing type, allows the file name, alt text, etc. to be specified.
/// </summary>
/// <param name="File">The file identifier.</param>
/// <param name="Alt">The alt text.</param>
/// <param name="Width">The width (if known).</param>
/// <param name="Height">The height (if known).</param>
/// <param name="Link">Optional link target</param>
public record BackingTypeImage (
            string File,
            string Alt,
            int Width,
            int Height,
            string? Link=null): BackingType (){
    }




/// <summary>
/// Backing type for links
/// </summary>
/// <param name="Text">The text to display.</param>
/// <param name="Link">The link target.</param>
public record BackingTypeLink (
            string? Text,
            string? Link) {
    }
