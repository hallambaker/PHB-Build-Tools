using Goedel.Registry;

namespace Goedel.Sitebuilder;



/// <summary>
/// Pagewriter adds in methods to emit FramePages and components.
/// </summary>
public partial class PageWriter : HtmlWriter {

    ///<summary>Text to use for the page.</summary>
    public PageText PageText { get; set;} = PageText.English;

    /// <summary>Reactions to be added to elements of a form.</summary>
    public List<FormReaction>? Reactions { get; set; } = null;

    FramePage FramePage { get; }

    FrameSet FrameSet =>  FramePage.FrameSet;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="page">The frame context to render in.</param>
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

        Text(title, "div", "class", "Heading");

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

            if (section.GetAnchor != null) {
                var anchor = section.GetAnchor(backer);
                Open("a", "class", "sectionLink", "href", anchor);
                }

            Render(backer, section.Fields);
            if (section.GetAnchor != null) {
                Close();
                }

            Close();
            }

        }

    /// <summary>
    /// Use the field specifier <paramref name="description"/> to render data from
    /// <paramref name="backer"/>.
    /// </summary>
    /// <param name="backer">The data to render.</param>
    /// <param name="description">Describes how to render the data.</param>
    public void RenderField(
            IBacked backer,

        IFrameField description) {

            switch (description) {
            case FrameButton item: {
                Render(item, backer);
                break;
                }
            case FrameRefMenu item: {
                Render(item, backer);
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
            case FrameRichText item: {
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

        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(FrameRefMenu description, IBacked backer) {

        //var menu = fieldRefMenu.Menu;

        // Construct the localized menu from the frame.
        var menu = description.Menu.Create(FramePage);
        menu.FrameSet = backer.FrameSet;
        var start = OpenClass("div", description.Tag);

        foreach (var field in menu.Fields) {

            switch (field) {
                case FrameButton item: {
                    Render(item, menu);
                    break;
                    }
                }

            }


        Close(start);
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(FrameButton description, IBacked backer) {


        var disabled = false;

        string icon = "";

        if (description.GetCustomized is not null) {
            if (description.GetActive is not null) {
                var active = description.GetActive(backer) ?? ButtonVisibility.Available;
                if (active == ButtonVisibility.None) {
                    return;
                    }
                icon = description.GetCustomized(backer, active);
                }
            else {
                icon = description.GetCustomized(backer, ButtonVisibility.Available);
                }
            }

        else if (description.GetActive is not null) {
            
            var active = description.GetActive(backer);
            switch (active) {
                case ButtonVisibility.None: {
                    return;
                    }
                case ButtonVisibility.Active: {
                    icon = FrameSet.IconPath(description.Tag +"Active");
                    disabled = true;
                    break;
                    }
                case ButtonVisibility.Disabled: {
                    icon = FrameSet.IconPath(description.Tag +"Disabled") ;
                    disabled = true;
                    break;
                    }
                default: {
                    icon = FrameSet.IconPath(description.Tag );
                    break;
                    }
                }
            }
        else {
            icon = FrameSet.IconPath(description.Tag);
            }


        var buttonType = disabled ? "ButtonDummy " : "Button ";
        var start = Open("div", "class", buttonType + description.Tag);

        if (!disabled) {
            var anchor = description.ActionValue;
            if (description.GetAnchor != null) {
                anchor = description.ActionValue + description.GetAnchor(backer);
                }
            Open("a", "class", "ButtonAnchor", description.ActionType, anchor, "title", description.Description);
            }
        else {
            Open("div", "class", "ButtonDummyAnchor");
            }

        ElementClass("img", "ButtonIcon", "src", icon, "alt", description.Label);
        TextClass(description.Label, "ButtonText", "div");

        if (description.GetText is not null) {
            var value = description?.GetText(backer);
            if (value is not null) {
                TextClass(value, "ButtonVar", "div");
                }
            }
        else if (description.GetInteger is not null) {
            var value = description?.GetInteger(backer).ToString();
            if (value is not null) {
                TextClass(value, "ButtonVar", "div");
                }
            }

        Close();
        Close(start);
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(IBacked backer, FrameSubmenu description) {


        var start = Open("div", "class", "dropdown");

        Open("button", "type", "button", "class", "dropdown-button");
        ElementClass("img", "ButtonIcon", "src", FrameSet.IconPath(description.Tag), "alt", description.Label);
        Close();

        Open("div", "class", "dropdown-content");
        foreach (var field in description.Fields) {
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






    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    /// <param name="max">Maximum number of items to render, if -1, show all.</param>
    /// <param name="first">First item to render</param>
    public void Render(
                IBacked backer,
                FrameRefList description,
                int max = -1,
                int first = 0) {


        var value = description.Get(backer);
        if (value is null) {
            return;
            }
        Open("div", "class", description.Tag);


        var count = description.Count(value);
        var id = description.Tag + "Item";

        var last = max < 0 ? count : count.Minimum(max - first);
        for (var i = first; i < last; i++) {

            var listItem = description.Item(value, i);

            if (description.Presentation is not null) {
                var presentation = description.Presentation(listItem);


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

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
                IBacked backer,
                FrameRefClass description) {
        var value = description.Get(backer);
        if (value is not null) {
            Open("div", "class", description.Id);

            // Change this to perform the extract presentation code on the thing we are about to render.


            if (description.Presentation is not null) {
                var presentation = description.Presentation(value);

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




    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
                IBacked backer,
                FrameChooser description) {
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
                IBacked backer,
                FrameBoolean description) {
        var value = description.Get(backer);
        if (value is not null) {



            OpenClass("div", description.Tag);
            Text(value.ToString());
            Close();
            }
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
                IBacked backer,
                FrameInteger description) {
        var value = description.Get(backer);
        if (value is not null) {
            OpenClass("div", description.Tag);
            Text(value.ToString());
            Close();
            }
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
                IBacked backer,
                FrameDateTime description) {
        var value = description.Get(backer);
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

            OpenClass("div", description.Tag);
            Text(result);
            Close();
            }
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
                IBacked backer,
                FrameString description) {
        var value = description.Get(backer);
        if (value is not null) {

            OpenClass("div", description.Tag);
            Text(value.ToString());
            Close();
            }
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
            IBacked backer,
            FrameRichText description) {
        var value = description.Get(backer);
        if (value is not null) {
            OpenClass("div", description.Tag);
            TextVerbatim(value);
            Close();
            }
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
                IBacked backer,
                FrameText description) {
        var value = description.Get(backer) ;
        if (value is not null) {
            OpenClass("div", description.Tag);
            Text(value.ToString());
            Close();
            }
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
                IBacked backer,
                FrameImage description) {
        var value = description.Get(backer);
        if (value is not null) {
            var file = SitebuilderConstants.Repository + value;
            ElementClass("img", description.Tag, "src", file, "alt", "");
            }
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
                IBacked backer,
                FrameAvatar description) {
        var value = description.Get(backer);
        if (value is not null) {
            //var file = SitebuilderConstants.Repository + value;
            ElementClass("img", description.Tag, "src", value, "alt", "");
            }
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
            IBacked backer,
            FrameCount description) {
        var value = description.Get(backer);

        if (value is not null) {

            OpenClass("div", description.Tag);
            Text(value.ToString());
            Close();
            }
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
            IBacked backer,
            FrameIcon description) {
        var value = FrameSet.IconPath(description.Tag);
        ElementClass("img", description.Tag, "src", value);
        }

    /// <summary>Render the element <paramref name="backer"/> as specified by
    /// <paramref name="description"/>.</summary>
    /// <param name="description">The item description.</param>
    /// <param name="backer">The backing instance.</param>
    public void Render(
            IBacked backer,
            FrameSeparator description) {
        ElementClass("hr", description.Tag);
        }

    string NormalizeId(string id) => id.Replace(".", "");
    }