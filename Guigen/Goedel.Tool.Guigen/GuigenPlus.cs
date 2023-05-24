
using Goedel.Utilities;
using Goedel.Registry;

namespace Goedel.Tool.Guigen;


public partial class Guigen {

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

    public string RecordId => "Section" + Id;

    public override void Init(_Choice parent) {

        base.Init(parent);
        _Base.Sections.Add(this);
        _Base.AddIcon(Icon);

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

    public string RecordId => "Action" + Id;

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.Actions.Add(this);
        _Base.AddIcon(Icon);
        }
    }

public partial class Dialog {

    public string RecordId => "Dialog" + Id;

    public override void Init(_Choice parent) {
        base.Init(parent);
        _Base.Dialogs.Add(this);
        }
    }


public partial class Chooser {

    public override void Init(_Choice parent) {
        base.Init(parent);
        }
    }