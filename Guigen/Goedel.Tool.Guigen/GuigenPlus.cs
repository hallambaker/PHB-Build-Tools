
using Goedel.Utilities;
using Goedel.Registry;
using System.Text;

namespace Goedel.Tool.Guigen;


public interface IEntries {

    List<_Choice> AllEntries {get; }
    List<_Choice> InheritedEntries { get; }
    string IdLabel { get; }

    string IdLabelBase { get; }

    TOKEN<_Choice> Inherit { get; set; }

    public bool IsSubclass { get; }

    public string IfSubclassNew { get; }
    public string IfSubclassOverride { get; }
    }

public interface IField {

    List<_Choice> GetEntries { get; }

    int Index { get; set; }

    string IdLabel { get; }
    }

public partial class Guigen {
    public const string DefaultState = "Default";

    public Dictionary<string, string> States = new() { { DefaultState , DefaultState }  } ;
    public Dictionary<string, Prompt> Prompts = new();
    public SortedDictionary<string, string> Icons = new();

    public List<Section> Sections = new();
    public List<Action> Actions = new();
    public List<Selection> Selections = new();
    public List<Dialog> Dialogs = new();
    //public List<Binding> Bindings = new();
    public List<Result> Results = new();
    public List<Fail> Fails = new();

    public Class Class { get; set; } = null;

    public void AddIcon(string icon) {
        icon = icon.ToLower();

        if (!Icons.ContainsKey(icon)) {
            Icons.Add(icon, icon); 
            }

        }

    public void AddState(string state) {
        if (!States.ContainsKey(state)) {
            States.Add(state, state);
            }
        }

    public void AddPrompt(ID<_Choice> id, string text) => AddPrompt(id.Label, text);


    public void AddPrompt(REF<_Choice> id, string text) => AddPrompt(id.Label, text);

    public void AddPrompt(string key, string text) {
        if (!Prompts.TryGetValue(key, out _)) {
            Prompts.Add(key, new Prompt(key, text));
            }
        }


    }

public partial record Prompt(string Key, string Text) {


    }




public partial class _Choice {
    public virtual bool Active => true;

    public virtual string RecordId { get; }



    public virtual bool Readonly { get; set; } = false;


    public virtual bool NoSetter => Readonly;

    public int Index { get; set; } = -1;

    public string Summary { get; set; } = "";

    public virtual string BackerType => null;
    public virtual string BindingType => null;


    public virtual TOKEN<_Choice> Inherit { get; set; } = null;
    public bool IsSubclass => Inherit is not null;
    public virtual string? IdLabel => throw new NYI();
    public virtual string IdLabelBase => "_" + IdLabel;

    public virtual string? Width { get; set; } = null;

    public string IfSubclassNew => IsSubclass ? "new" : "";
    public string IfSubclassOverride => IsSubclass ? "override" : "virtual";

    public virtual string PromptQuoted => null;


    public void SetEntries(List<_Choice> entries) {
        foreach (var entry in entries) {
            switch (entry) {
                case Goedel.Tool.Guigen.Readonly: {
                    Readonly = true;
                    break;
                    }
                case Goedel.Tool.Guigen.Error error: {
                    _Base.AddPrompt(error.Id, error.Message);
                    break;
                    }
                case Inherit inherit: {
                    Inherit = inherit.Id;
                    break;
                    }
                case Width width: {
                    Width = width.Request.ToString();
                    break;
                    }
                }

            }

        }

    public void AddEntries(List<_Choice> source, List<_Choice> target) {

        foreach (var entry in source) {
            if (entry is Inherit inherit) {
                if (inherit.Id.Definition is Dialog dialog) {
                    AddEntries(dialog.Entries, target);
                    }

                }
            else {
                target.Add(entry);
                }
            }


        }
    }


public partial class Class {
    public override void Init(_Choice parent) {
        _Base.Class = this;
        }
    }

public partial class Section : IEntries {

    public string State { get; set; } = Guigen.DefaultState;

    public List<_Choice> AllEntries => Entries;
    public virtual List<_Choice> InheritedEntries => Entries;
    public bool Primary { get; set; } = false;

    public override string RecordId => "Section" + Id.Label;
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;
    public override void Init(_Choice parent) {

        base.Init(parent);
        _Base.Sections.Add(this);
        _Base.AddIcon(Icon);
        _Base.AddPrompt(Id, Prompt);
        foreach (var child in Entries) {
            if (child is Primary) {
                Primary = true;
                }
            if (child is Condition condition) {
                State = condition.Id.Label;
                _Base.AddState(State);
                }

            }
        }
    }


//public partial class Binding : IEntries {
//    public List<_Choice> AllEntries => Entries;
//    public virtual List<_Choice> InheritedEntries => Entries;

//    public override string RecordId => "Binding" + Id.Label;
//    public string QuotedId => Id.Label.Quoted();
//    public override string IdLabel => Id.Label;

//    public override string IdLabelBase =>  IdLabel;
//    public override void Init(_Choice parent) {

//        base.Init(parent);
//        _Base.Bindings.Add(this);
//        }
//    }


public partial class Result : IEntries {
    public override bool Active => false;

    public List<_Choice> AllEntries => Entries;
    public virtual List<_Choice> InheritedEntries => Entries;

    public override string RecordId => "Result" + Id.Label;
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;

    public override string IdLabelBase => IdLabel;
    public override void Init(_Choice parent) {

        base.Init(parent);
        _Base.Results.Add(this);
        }
    }


public partial class Fail : IEntries {
    public override bool Active => false;

    public List<_Choice> AllEntries => Entries;
    public virtual List<_Choice> InheritedEntries => Entries;

    public override string RecordId => "Fail" + Id.Label;
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;

    public override string IdLabelBase => IdLabel;
    public override void Init(_Choice parent) {

        base.Init(parent);
        _Base.Fails.Add(this);
        _Base.AddPrompt(Id, Message);
        }
    }



public partial class Primary {
    public override bool Active => false;
    }

public partial class Filter {
    public override bool Active => false;
    }

public partial class Readonly {
    public override bool Active => false;
    }

public partial class Return {
    public override bool Active => false;
    }

public partial class Selection : IEntries {

    public List<_Choice> AllEntries => Entries;
    public virtual List<_Choice> InheritedEntries => Entries;

    public override string BindingType => "GuiBoundPropertySelection";
    public override string RecordId => "Selection" + Id.Label;

    public override string PromptQuoted => Prompt.Quoted();
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;

    public string Target => RecordId;

    public string DispatchType => _Parent.IdLabel;

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.Selections.Add(this);
        _Base.AddIcon(Icon);
        _Base.AddPrompt(Id, Prompt);
        }

    }


public partial class Action : IEntries {

    public List<_Choice> AllEntries => Entries;
    public virtual List<_Choice> InheritedEntries => Entries;
    public override string RecordId => "Action" + Id.Label;
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;
    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.Actions.Add(this);
        _Base.AddIcon(Icon);
        _Base.AddPrompt(Id, Prompt);
        }
    }

public partial class Dialog : IEntries {
    public List<_Choice> AllEntries => Entries;
    public virtual List<_Choice> InheritedEntries { get; }  = new();

    public override string RecordId => "Dialog" + Id.Label;
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddIcon(Icon);
        _Base.AddPrompt(Id, Prompt);
        _Base.Dialogs.Add(this);
        SetEntries(Entries);

        AddEntries(Entries, InheritedEntries);

        }



    }


#region // Field Bindings

/// <summary>
/// Button offers an action to the user.
/// </summary>
public partial class Button {
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;

    public override string BindingType => "GuiBoundPropertyButton";
    public _Choice TargetObject => Id.ID.Object;
    public string Target => TargetObject.RecordId;

    public override void Init(_Choice parent) {
        base.Init(parent);
        }
    }

/// <summary>
/// Chooser selects one item from a list box of many entries.
/// </summary>
public partial class Chooser {
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;
    public override string PromptQuoted => Prompt.Quoted();

    public override string BackerType => "ISelectCollection";
    public override bool NoSetter => true;
    public override string BindingType => "GuiBoundPropertyChooser";
    public override string RecordId => "Binding" + Id.Label;
    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        SetEntries(Entries);
        }
    }

/// <summary>
/// List presents a short list of items usually 0-5
/// </summary>
public partial class List {
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;
    public override string BackerType => "ISelectCollection";
    public string DialogType => "Dialog" + Type.Label;
    public override string PromptQuoted => Prompt.Quoted();

    public override string BindingType => "GuiBoundPropertyList";
    public override string RecordId => "Binding" + Id.Label;
    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        SetEntries(Entries);
        }
    }

public partial class Boolean : IField {
    public string QuotedId => Id.Label.Quoted();

    public List<_Choice> GetEntries => Entries;

    public override string IdLabel => Id.Label;
    public override string BackerType => "bool";

    public override string PromptQuoted => Prompt.Quoted();

    public override string BindingType => "GuiBoundPropertyBoolean";

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        SetEntries(Entries);
        }
    }


public partial class Text : IField {
    public string QuotedId => Id.Label.Quoted();

    public List<_Choice> GetEntries => Entries;

    public override string IdLabel => Id.Label;
    public override string BackerType => "string";

    public override string PromptQuoted => Prompt.Quoted();
    public override string BindingType => "GuiBoundPropertyString";

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        SetEntries(Entries);
        }
    }

public partial class TextArea : IField {
    public string QuotedId => Id.Label.Quoted();

    public List<_Choice> GetEntries => Entries;

    public override string IdLabel => Id.Label;
    public override string BackerType => "string";

    public override string PromptQuoted => Prompt.Quoted();
    public override string BindingType => "GuiBoundTextArea";

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        SetEntries(Entries);
        }
    }

public partial class Color {
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;
    public override string BackerType => "IFieldColor";

    public override string PromptQuoted => Prompt.Quoted();
    public override string BindingType => "GuiBoundPropertyColor";
    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        SetEntries(Entries);
        }
    }
public partial class Size {
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;
    public override string BackerType => "IFieldSize";
    public override string PromptQuoted => Prompt.Quoted();

    public override string BindingType => "GuiBoundPropertySize";
    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        SetEntries(Entries);
        }
    }
public partial class Decimal {
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;
    public override string BackerType => "decimal?";
    public override string PromptQuoted => Prompt.Quoted();
    public override string BindingType => "GuiBoundPropertyDecimal";
    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        SetEntries(Entries);
        }
    }

public partial class Integer {
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;
    public override string BackerType => "int?";
    public override string PromptQuoted => Prompt.Quoted();

    public override string BindingType => "GuiBoundPropertyInteger";
    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        SetEntries(Entries);
        }
    }


public partial class QRScan {
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;
    public override string BackerType => "GuiQR?";
    public override string PromptQuoted => Prompt.Quoted();

    public override string BindingType => "GuiBoundPropertyQRScan";
    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        SetEntries(Entries);
        }
    }



public partial class Icon {
    public string QuotedId => Id.Label.Quoted();
    public override string IdLabel => Id.Label;

    public override string BackerType => "IFieldIcon";

    public override bool Readonly { get; set; } = true;
    public override string PromptQuoted => Prompt.Quoted();

    public override string BindingType => "GuiBoundPropertyIcon";
    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        _Base.AddIcon(File);
        }
    }



#endregion



public partial class Condition {

    public override bool Active => false;
    }
public partial class Description {

    public override bool Active => false;
    }
public partial class Hidden {
    public override string BackerType => "string";
    public override string IdLabel => Id.Label;
    public override bool Active => false;


    public override void Init(_Choice parent) {
        //Readonly = true;
        }
    }

public partial class Inherit {

    public override bool Active => false;
    }