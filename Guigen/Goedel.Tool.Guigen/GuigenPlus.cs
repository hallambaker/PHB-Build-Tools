
using Goedel.Utilities;
using Goedel.Registry;

namespace Goedel.Tool.Guigen;


public partial class Guigen {

    public Dictionary<string, Prompt> Prompts = new();
    public Dictionary<string, string> Icons = new();

    public List<Section> Sections = new();
    public List<Action> Actions = new();
    public List<Dialog> Dialogs = new();

    public Class Class { get; set; } = null;

    public void AddIcon(string icon) {
        if (!Icons.ContainsKey(icon)) {
            Icons.Add(icon, icon); 
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
    }


public partial class Class {
    public override void Init(_Choice parent) {
        _Base.Class = this;
        }
    }

public partial class Section {

    public bool Primary { get; set; } = false;

    public string RecordId => "Section" + Id.Label;
    public string QuotedId => Id.Label.Quoted();

    public override void Init(_Choice parent) {

        base.Init(parent);
        _Base.Sections.Add(this);
        _Base.AddIcon(Icon);
        _Base.AddPrompt(Id, Prompt);
        foreach (var child in Entries) {
            if (child is Primary) {
                Primary = true;
                }
            }
        }
    }

public partial class Primary {
    public override bool Active => false;
    }



public partial class Action {

    public string RecordId => "Action" + Id.Label;
    public string QuotedId => Id.Label.Quoted();

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.Actions.Add(this);
        _Base.AddIcon(Icon);
        _Base.AddPrompt(Id, Prompt);
        }
    }

public partial class Dialog {

    public string RecordId => "Dialog" + Id.Label;
    public string QuotedId => Id.Label.Quoted();

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.Dialogs.Add(this);
        }
    }


public partial class Chooser {
    public string QuotedId => Id.Label.Quoted();

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        }
    }



public partial class Button {
    public string QuotedId => Id.Label.Quoted();

    public override void Init(_Choice parent) {
        base.Init(parent);
        }
    }

public partial class Text {
    public string QuotedId => Id.Label.Quoted();

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        }
    }
public partial class Color {
    public string QuotedId => Id.Label.Quoted();

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        }
    }
public partial class Size {
    public string QuotedId => Id.Label.Quoted();

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        }
    }
public partial class Decimal {
    public string QuotedId => Id.Label.Quoted();

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        }
    }
public partial class Icon {
    public string QuotedId => Id.Label.Quoted();

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.AddPrompt(Id, Prompt);
        }
    }
