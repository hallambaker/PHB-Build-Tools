using Goedel.Protocol.Web;
using Goedel.Registry;

namespace Goedel.Sitebuilder;



/// <summary>
/// Pagewriter adds in methods to emit FramePages and components.
/// </summary>
public partial class PageWriter : HtmlWriter {

    /// <summary>Debug flag.</summary>
    public static bool DebugForm = false;

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
            IBacked backer,
            FrameRefForm description) {

        // create the form 
        if (DebugForm) {
            OpenClass("form", description.Tag, "action", "https://httpbin.org/post", "method", "post");
            }
        else {
            OpenClass("form", description.Tag, "action", $"/{FramePage.PathStem}?{description.Tag}", "method",
                "post", "enctype", "multipart/form-data");
            }
        //Element("input", "type", "hidden", "id", "-Form", "name", item.Tag, "value", item.Tag!);

        var value = description.Get(backer);
        foreach (var field in description.Fields) {
            RenderFormField(value, field);
            }

        Element("input", "type", "submit", "value", PageText.Submit);
        Element("input", "type", "reset", "value", PageText.Reset);

        Close();
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void RenderFormField(

            IBacked? backer,
            IFrameField description) {


        var id = NormalizeId(description.Tag);
        OpenClass("div", id);
        switch (description) {
            case FrameString item: {
                RenderForm(backer, item, id);
                break;
                }
            case FrameFile item: {
                RenderForm(backer, item, id);
                break;
                }
            default: {
                break;
                }
            }

        if (Reactions is not null) {
            foreach (var reaction in Reactions) {
                if (reaction.Id == description.Tag) {
                    Text(reaction.Text, "p", "class", "InputError");
                    }
                }
            }

        Close();
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    /// <param name="id">Identifier of the form element.</param>
    public void RenderForm(
                IBacked? backer,
                FrameFile description,
                string id) {

        BackingTypeFile? value = null;


        if (backer is not null) {
            value = description.Get(backer);
            }


        if (description.Hidden) {
            //Element("input", "type", "hidden", "id", id, "name", item.Tag, "value", value!);
            }
        else {
            Text(description.Prompt, "label", "class", "InputLabel", "for", id);
            Element("input", "class", "InputForm", "type", "file", "id", id, "name", description.Tag);
            }



        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    /// <param name="id">Identifier of the form element.</param>
    public void RenderForm(
                        IBacked? backer,
                FrameString description,
                string id) {

        string? value=null;
        if (backer is not null) {
            value = description.Get(backer);
            }



        if (!description.Hidden) {
            Text(description.Prompt, "label", "class", "InputLabel", "for", id);
            }


        switch (description) {
            case FrameText: {
                if (description.Hidden) {
                    Element("input", "type", "hidden", "id", id, "name", description.Tag, "value", value!);
                    }
                else {
                    Text("", "textarea", "class", "InputForm", "id", id, "name", description.Tag, "value", value!);
                    }
                break;
                }
            case FrameRichText: {
                if (description.Hidden) {
                    Element("input", "type", "hidden", "id", id, "name", description.Tag, "value", value!);
                    }
                else {
                    Text("", "div", "class", "InputForm", "id", "richtext");
                    }
                break;
                }
            default: {
                if (description.Hidden) {
                    Element("input", "type", "hidden", "id", id, "name", description.Tag, "value", value!);
                    }
                else {
                    Element("input", "class", "InputForm", "type", "text", "id", description.Tag, "name", description.Tag, "value", value!);
                    }

                break;
                }
            }


        }



    }