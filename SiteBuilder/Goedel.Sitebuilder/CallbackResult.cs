namespace Goedel.Sitebuilder;

/// <summary>Record returning the result of server transaction request.</summary>
/// <param name="Code">The HTTP status.</param>
/// <param name="Reactions">The list of reactions.</param>
/// <param name="Redirect">Page to return the user to.</param>
/// <param name="Cookies">Cookies to be entered of cleared.</param>
public record CallbackResult(
            HttpStatusCode Code,
            List<FormReaction>? Reactions,
            string? Redirect,
            List<Cookie> Cookies = null
            ) {

    /// <summary>Factory method, create a new callback result for <paramref name="path"/></summary>
    /// <param name="path">Page to return the user to.</param>
    /// <returns>The created instance.</returns>
    public static CallbackResult CreatedRedirect(string path) => new CallbackResult(
        HttpStatusCode.Created, null, path);



    }



