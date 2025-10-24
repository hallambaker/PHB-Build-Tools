using Goedel.Registry;

namespace Goedel.Sitebuilder;



/// <summary>
/// Pagewriter adds in methods to emit FramePages and components.
/// </summary>
public partial class PageWriter : HtmlWriter {

    ///<summary>Text to use for the page.</summary>
    public PageText PageText { get; set;} = PageText.English;

    FramePage FramePage { get; }

    FrameSet FrameSet =>  FramePage.FrameSet;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="frameset">The frame set context to render in.</param>
    /// <param name="textWriter">The text writer to write to.</param>
    public PageWriter(
            FramePage page,
            TextWriter textWriter
            ) : base(textWriter) {
        FramePage = page;
        }

    /// <summary>
    /// Render page.
    /// </summary>
    public void Render() {

        FramePage.StartRender = System.DateTime.Now;
        var title = FramePage.PageTitle ?? FramePage.Title;

        // Basics, title and favicon
        Head(title, FramePage.FaviCon);

        // Stylesheets and scripts with usual defaults
        Reources(FramePage.FrameSet.Resources);
        Reources(FramePage.Resources);

        Body();

        if (FramePage.Container is not null) {
            Open("div", "class", FramePage.Container);
            }
        else {
            Open("div", "class", FramePage.Tag);
            }

        Text(title, "div", "class", "Title");

        RenderFields(FramePage);
        Close();


        Reources(FramePage.FrameSet.EndResources);
        Reources(FramePage.EndResources);
        Finish();
        }

    /// <summary>
    /// Render the fields of <paramref name="backer"/> using the default presentation.
    /// </summary>
    /// <param name="backer"></param>
    public void RenderFields(IBacked backer) {
        Render(backer, backer.Fields);
        }

    /// <summary>
    /// Render the list of fields <paramref name="fields"/> of <paramref name="backer"/>.
    /// </summary>
    /// <param name="backer">The data to render.</param>
    /// <param name="fields">The field descriptions.</param>
    public void Render(IBacked backer, List<IFrameField> fields) {
        foreach (var field in fields) {
            RenderField(backer, field);
            }

        }

    /// <summary>
    /// Render the data <paramref name="backer"/> using presentation template
    /// <paramref name="presentation"/>.
    /// </summary>
    /// <param name="backer">The data to render.</param>
    /// <param name="presentation">The presentation definition.</param>
    public void RenderSections(IBacked backer, FramePresentation presentation) {

        foreach (var section in presentation.Sections) {

            Open("section", "class", section.Id);
            Render(backer, section.Fields);
            Close();
            }

        }

    /// <summary>
    /// Use the field specifier <paramref name="field"/> to render data from
    /// <paramref name="backer"/>.
    /// </summary>
    /// <param name="backer">The data to render.</param>
    /// <param name="field">The field to render</param>
    public void RenderField(
            IBacked backer,
            IFrameField field) {

        var id = NormalizeId(field.Tag);
        //OpenClass("div", id);
        switch (field) {
            case FrameButton item: {
                Render(item, backer);
                break;
                }
            case FrameRefMenu item: {
                Render(backer, item);
                break;
                }
            case FrameRefClass item: {
                Render(backer,item);
                break;
                }
            case FrameRefList item: {
                Render(backer,item);
                break;
                }
            case FrameChooser item: {
                Render(backer, item);
                break;
                }
            case FrameBoolean item: {
                Render(backer,item);
                break;
                }
            case FrameCount item: {
                Render(backer, item);
                break;
                }
            case FrameInteger item: {
                Render(backer, item);
                break;
                }
            case FrameDateTime item: {
                Render(backer, item);
                break;
                }
            case FrameText item: {
                Render(backer, item);
                break;
                }
            case FrameString item: {
                Render(backer, item);
                break;
                }
            case FrameImage item: {
                Render(backer, item);
                break;
                }
            case FrameAvatar item: {
                Render(backer, item);
                break;
                }
            case FrameSeparator item: {
                Render(backer, item);
                break;
                }
            case FrameIcon item: {
                Render(backer, item);
                break;
                }
            case FrameSubmenu item: {
                Render(backer, item);
                break;
                }
            case FrameRefForm item: {
                Render(backer, item);
                break;
                }
            default : {
                break;
                }

            }
        //Close();
        }


    public void Render(IBacked backer, FrameRefMenu fieldRefMenu) {

        //var menu = fieldRefMenu.Menu;

        // Construct the localized menu from the frame.
        var menu = fieldRefMenu.Menu.Create(FramePage);
        var start = OpenClass("div", fieldRefMenu.Tag);

        foreach (var field in menu.Fields) {

            switch (field) {
                case FrameButton item: {
                    Render(item, fieldRefMenu.Menu);
                    break;
                    }
                }

            }


        Close(start);
        }


    public void Render(FrameButton button, IBacked backer) {


        var disabled = false;

        var icon = button.Tag;
        if (button.GetActive is not null) {
            var active = button.GetActive(backer);
            switch (active) {
                case ButtonVisibility.None: {
                    return;
                    }
                case ButtonVisibility.Active: {
                    icon = icon + "Active";
                    disabled = true;
                    break;
                    }
                case ButtonVisibility.Disabled: {
                    icon = icon + "Disabled";
                    disabled = true;
                    break;
                    }
                }
            }

        var buttonType = disabled ? "ButtonDummy " : "Button ";
        var start = Open("div", "class", buttonType + button.Tag);

        if (!disabled) {


            Open("a", "class", "ButtonAnchor", button.ActionType,  button.ActionValue, "title", button.Description);
            }
        else {
            Open("div", "class", "ButtonDummyAnchor");
            }

        ElementClass("img", "ButtonIcon", "src", FrameSet.IconPath(icon), "alt", button.Label);
        TextClass(button.Label, "ButtonText", "div");

        if (button.GetText is not null) {
            var value = button?.GetText(backer);
            if (value is not null) {
                TextClass(value, "ButtonVar", "div");
                }
            }
        else if (button.GetInteger is not null) {
            var value = button?.GetInteger(backer).ToString();
            if (value is not null) {
                TextClass(value, "ButtonVar", "div");
                }
            }

        Close();
        Close(start);
        }

    public void Render(IBacked backer, FrameSubmenu item) {


        var start = Open("div", "class", "dropdown");

        Open("button", "type", "button", "class", "dropdown-button");
        ElementClass("img", "ButtonIcon", "src", FrameSet.IconPath(item.Tag), "alt", item.Label);
        Close();

        Open("div", "class", "dropdown-content");
        foreach (var field in item.Fields) {
            if (field is FrameButton button) {
                Open("button", "type", "button", "class", "dropdown-subbutton");
                Element("img", "class", "ButtonIcon", "src", FrameSet.IconPath(button.Tag), "alt", button.Label);
                Text(button.Label, "div");
                Close();
                }
            }
        Close();

        Close(start);




        }







    public void Render(
                IBacked backer,
                FrameRefList item,
                int max = -1,
                int first = 0) {


        var value = item.Get(backer);
        if (value is null) {
            return;
            }
        Open("div", "class", item.Tag);


        var count = item.Count(value);
        var id = item.Tag + "Item";

        var last = max < 0 ? count : count.Minimum(max - first);
        for (var i = first; i < last; i++) {

            var listItem = item.Item(value, i);

            if (item.Presentation is not null) {
                var presentation = item.Presentation(listItem);


                if (presentation is not null) {
                    Open("section", "class", presentation.Tag);
                    RenderSections(listItem, presentation);
                    Close();
                    }
                else {
                    RenderFields(listItem);
                    }
                }
            else {
                RenderFields(listItem);
                }



            //Render(backer, listItem.Fields);

            // Change this to perform the extract presentation code on the thing we are about to render.
            //Open("div", "class", id);
            //RenderFields(item.Item(value, i));
            //Close();
            }

        Close();
        }


    public void Render(
                IBacked backer,
                FrameRefClass item) {
        var value = item.Get(backer);
        if (value is not null) {
            Open("div", "class", item.Id);

            // Change this to perform the extract presentation code on the thing we are about to render.


            if (item.Presentation is not null) {
                var presentation = item.Presentation(value);

                if (presentation is not null) {
                    Open("section", "class", presentation.Tag);
                    RenderSections(value, presentation);
                    Close();
                    }
                else {
                    RenderFields(value);
                    }
                }
            else {
                RenderFields(value);
                }
            Close();
            }
        }





    public void Render(
                IBacked backer,
                FrameChooser item) {
        }
    public void Render(
                IBacked backer,
                FrameBoolean item) {
        var value = item.Get(backer);
        if (value is not null) {



            OpenClass("div", item.Tag);
            Text(value.ToString());
            Close();
            }
        }
    public void Render(
                IBacked backer,
                FrameInteger item) {
        var value = item.Get(backer);
        if (value is not null) {
            OpenClass("div", item.Tag);
            Text(value.ToString());
            Close();
            }
        }
    public void Render(
                IBacked backer,
                FrameDateTime item) {
        var value = item.Get(backer);
        if (value is not null ) {
            var interval = (backer.StartRender - (System.DateTime)value);

            string result = "?";
            if (interval.Days > 365) {
                result = (interval.Days / 365).ToString() + "yr";
                }
            else if (interval.Days > 30) {
                result = (interval.Days / 30).ToString() + "mo";
                }
            else if (interval.Days > 7) {
                result = (interval.Days / 7).ToString() + "w";
                }
            else if (interval.Days > 0) {
                result = (interval.Days).ToString() + "d";
                }
            else if (interval.Minutes > 0) {
                result = (interval.Minutes).ToString() + "m";
                }
            else {
                result = (interval.Minutes).ToString() + "s";
                }

            OpenClass("div", item.Tag);
            Text(result);
            Close();
            }
        }
    public void Render(
                IBacked backer,
                FrameString item) {
        var value = item.Get(backer);
        if (value is not null) {

            OpenClass("div", item.Tag);
            Text(value.ToString());
            Close();
            }
        }
    public void Render(
                IBacked backer,
                FrameText item) {
        var value = item.Get(backer) ;
        if (value is not null) {
            OpenClass("div", item.Tag);
            Text(value.ToString());
            Close();
            }
        }
    public void Render(
                IBacked backer,
                FrameImage item) {
        var value = item.Get(backer);
        if (value is not null) {
            var file = "Images/" + value;


            //OpenClass("div", item.Id);
            ElementClass("img", item.Tag, "src", file, "alt", "");
            //Close();
            }
        }

    public void Render(
                IBacked backer,
                FrameAvatar item) {
        var value = item.Get(backer);
        if (value is not null) {
            var file = "Images/" + value;
            //OpenClass("div", item.Id);
            ElementClass("img", item.Tag, "src", file, "alt", "");
            //Close();
            }
        }
    public void Render(
            IBacked backer,
            FrameCount item) {
        var value = item.Get(backer);

        if (value is not null) {

            OpenClass("div", item.Tag);
            Text(value.ToString());
            Close();
            }
        }

    public void Render(
            IBacked backer,
            FrameIcon item) {
        var value = FrameSet.IconPath(item.Tag);
        ElementClass("img", item.Tag, "src", value);
        }


    public void Render(
            IBacked backer,
            FrameSeparator item) {
        ElementClass("hr", item.Tag);
        }

    string NormalizeId(string id) => id.Replace(".", "");
    }