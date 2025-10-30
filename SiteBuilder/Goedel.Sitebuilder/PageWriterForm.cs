using Goedel.Protocol.Web;
using Goedel.Registry;

namespace Goedel.Sitebuilder;



/// <summary>
/// Pagewriter adds in methods to emit FramePages and components.
/// </summary>
public partial class PageWriter : HtmlWriter {
    public static bool DebugForm = false;

    public void Render(
            IBacked backer,
            FrameRefForm item) {

        // create the form 
        if (DebugForm) {
            OpenClass("form", item.Tag, "action", "https://httpbin.org/post", "method", "post");
            }
        else {
            OpenClass("form", item.Tag, "action", $"/{FramePage.PathStem}?{item.Tag}", "method",
                "post", "enctype", "multipart/form-data");
            }
        //Element("input", "type", "hidden", "id", "-Form", "name", item.Tag, "value", item.Tag!);

        var value = item.Get(backer);
        foreach (var field in item.Fields) {
            RenderFormField(value, field);
            }

        Element("input", "type", "submit", "value", PageText.Submit);
        Element("input", "type", "reset", "value", PageText.Reset);

        Close();
        }


    public void RenderFormField(

            IBacked? backer,
            IFrameField field) {


        var id = NormalizeId(field.Tag);
        OpenClass("div", id);
        switch (field) {
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
                if (reaction.Id == field.Tag) {
                    Text(reaction.Text, "p", "class", "InputError");
                    }
                }
            }

        Close();
        }


    public void RenderForm(
                IBacked? backer,
                FrameFile item,
                string id) {

        BackingTypeFile? value = null;


        if (backer is not null) {
            value = item.Get(backer);
            }


        if (item.Hidden) {
            //Element("input", "type", "hidden", "id", id, "name", item.Tag, "value", value!);
            }
        else {
            Text(item.Prompt, "label", "class", "InputLabel", "for", id);
            Element("input", "class", "InputForm", "type", "file", "id", id, "name", item.Tag);
            }



        }


    public void RenderForm(
                        IBacked? backer,
                FrameString item,
                string id) {

        string? value=null;
        if (backer is not null) {
            value = item.Get(backer);
            }



        if (!item.Hidden) {
            Text(item.Prompt, "label", "class", "InputLabel", "for", id);
            }


        switch (item) {
            case FrameText: {
                if (item.Hidden) {
                    Element("input", "type", "hidden", "id", id, "name", item.Tag, "value", value!);
                    }
                else {
                    Text("", "textarea", "class", "InputForm", "id", id, "name", item.Tag, "value", value!);
                    }
                break;
                }
            case FrameRichText: {
                if (item.Hidden) {
                    Element("input", "type", "hidden", "id", id, "name", item.Tag, "value", value!);
                    }
                else {
                    Text("", "div", "class", "InputForm", "id", "richtext");
                    }
                break;
                }
            default: {
                if (item.Hidden) {
                    Element("input", "type", "hidden", "id", id, "name", item.Tag, "value", value!);
                    }
                else {
                    Element("input", "class", "InputForm", "type", "text", "id", item.Tag, "name", item.Tag, "value", value!);
                    }

                break;
                }
            }


        }



    }