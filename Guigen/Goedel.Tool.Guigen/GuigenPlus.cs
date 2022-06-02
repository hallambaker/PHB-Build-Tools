
using Goedel.Utilities;
using Goedel.Registry;

namespace Goedel.Tool.Guigen;

public partial class _Choice {

    ///<summary>The enclosing application</summary> 
    public virtual Application GetApplication => _Parent?.GetApplication;

    ///<summary>The parent declaration</summary> 
    public Declaration Declaration => _Parent as Declaration;

    /// <summary>
    /// Process the declaration to set the value <see cref="Icon"/> if defined in
    /// <paramref name="entries"/>. If multiple definitions are specified,
    /// the first value specified is used.
    /// </summary>
    /// <param name="haveIcon">The declatation to process</param>
    /// <param name="entries">The option list</param>
    public static void ProcessIcon(IHaveIcon haveIcon, List<_Choice> entries) {
        foreach (var entry in entries) {
            switch (entry) {
                case Icon icon: {
                        haveIcon.Icon ??= icon;
                        break;
                        }
                case Text text: {
                        haveIcon.Text ??= text;
                        break;
                        }
                }
            }

        }

    /// <summary>
    /// Process the declaration to set the value <see cref="Icon"/> if defined in
    /// <paramref name="entries"/>. If multiple definitions are specified,
    /// the first value specified is used.
    /// </summary>
    /// <param name="haveStores">The declatation to process</param>
    /// <param name="entries">The option list</param>
    public static void ProcessStores(IHaveStores haveStores, List<_Choice> entries) {
        foreach (var entry in entries) {
            switch (entry) {
                case Catalog catalog: {
                        haveStores.Stores.Add(catalog);
                        break;
                        }
                case Spool spool: {
                        haveStores.Stores.Add(spool);
                        break;
                        }
                case Menu menu: {
                        haveStores.Menus.Add(menu);
                        break;
                        }
                }
            }

        }

    /// <summary>
    /// Process the declaration to set the value <see cref="Icon"/> if defined in
    /// <paramref name="entries"/>. If multiple definitions are specified,
    /// the first value specified is used.
    /// </summary>
    /// <param name="haveActions">The declatation to process</param>
    /// <param name="entries">The option list</param>
    public static void ProcessActions(IHaveActions haveActions, List<_Choice> entries) {
        foreach (var entry in entries) {
            switch (entry) {
                case Default defaultAction: {
                        haveActions.Defaults.Add(defaultAction);
                        break;
                        }
                case Selector selector: {
                        haveActions.Selectors.Add(selector);
                        break;
                        }
                case Action action: {
                        haveActions.Actions.Add(action);
                        break;
                        }
                }
            }

        }

    }


public partial class Application {
    public override Application GetApplication => this;


    public List<Structure> Structures = new ();
    public List<Environment> Environments = new();
    public List<Icon> Icons = new();
    public List<Menu> Menus = new();


    public SortedDictionary <string, MenuEntry> DictionaryMenuEntries = new ();
    public SortedDictionary<string, Icon> DictionaryIcons = new();

    public SortedDictionary<string, Icon> DictionaryIconsByFile = new();

    public override void Init(_Choice parent) {
        base.Init(parent);
        }

    }

public partial class Declaration {

    public override void Init(_Choice parent) {
        base.Init(parent);

        switch (Entry) {
            case Structure structure: {
                    GetApplication.Structures.Add (structure);
                    break;
                    }
            case Environment environment: {
                    GetApplication.Environments.Add(environment);
                    break;
                    }
            case Icon icon: {
                    GetApplication.Icons.Add(icon);
                    break;
                    }
            case Menu menu: {
                    GetApplication.Menus.Add(menu);
                    break;
                    }
            }

        }

    }

/// <summary>
/// Declaration has an icon specifier
/// </summary>
public interface IHaveIcon {
    
    ///<summary>The icon specification</summary> 
    public Icon Icon { get; set; }
    ///<summary>The text specification</summary> 
    public Text Text { get; set; }
    }

/// <summary>
/// Declaration has an icon specifier
/// </summary>
public interface IHaveStores {

    ///<summary>The menu specification</summary> 
    public List<Menu> Menus { get; set; }
    ///<summary>The icon specification</summary> 
    public List<_Choice> Stores { get; set; }
    }

/// <summary>
/// Declaration has an icon specifier
/// </summary>
public interface IHaveActions {

    ///<summary>The Default specification </summary> 
    public List<Default> Defaults { get; set; }
    ///<summary>The Default specification </summary> 
    public List<Selector> Selectors { get; set; }
    ///<summary>The Default specification </summary> 
    public List<Action> Actions { get; set; }
    }

public partial class Structure : IHaveIcon {
    ///<inheritdoc/>
    public Icon Icon { get; set; }
    ///<inheritdoc/>
    public Text Text { get; set; }
    ///<summary>The parent declaration</summary> 
    public Declaration Declaration => _Parent as Declaration;
    ///<summary>The parent declaration identifier</summary> 
    public ID<_Choice> Id => Declaration?.Id;

    public override void Init(_Choice parent) {
        base.Init(parent);

        ProcessIcon(this, Entries);
        }
    }

public partial class Icon {


    ///<summary>The parent declaration identifier</summary> 
    public ID<_Choice> Id => Declaration?.Id;

    public override void Init(_Choice parent) {
        base.Init(parent);

        if (Id != null) {
            GetApplication.DictionaryIcons.Add(Id.Label, this);
            }
        GetApplication.DictionaryIconsByFile.AddSafe(File, this);

        }
    }
public partial class Environment : IHaveIcon , IHaveStores {
    ///<inheritdoc/>
    public Icon Icon { get; set; }
    ///<inheritdoc/>
    public Text Text { get; set; }
    ///<inheritdoc/>
    public List<Menu> Menus { get; set; } = new();
    ///<inheritdoc/>
    public List<_Choice> Stores { get; set; } = new();

    ///<summary>The parent declaration</summary> 
    public Declaration Declaration => _Parent as Declaration;
    ///<summary>The parent declaration identifier</summary> 
    public ID<_Choice> Id => Declaration?.Id;

    public override void Init(_Choice parent) {
        base.Init(parent);

        ProcessIcon(this, Entries);
        ProcessStores(this, Entries);
        }
    }

public partial class Catalog : IHaveActions {
    ///<inheritdoc/>
    public List<Default> Defaults { get; set; } = new();
    ///<inheritdoc/> 
    public List<Selector> Selectors { get; set; } = new();
    ///<inheritdoc/>
    public List<Action> Actions { get; set; } = new();

    public override void Init(_Choice parent) {
        base.Init(parent);

        ProcessActions(this, Entries);

        }
    }

public partial class Spool : IHaveActions {
    ///<inheritdoc/>
    public List<Default> Defaults { get; set; } = new();
    ///<inheritdoc/> 
    public List<Selector> Selectors { get; set; } = new();
    ///<inheritdoc/>
    public List<Action> Actions { get; set; } = new();


    public override void Init(_Choice parent) {
        base.Init(parent);

        ProcessActions(this, Entries);

        }
    }



public partial class MenuEntry : IHaveIcon {

    public override Application GetApplication => _Parent.GetApplication;

    ///<inheritdoc/>
    public Icon Icon { get; set; }
    ///<inheritdoc/>
    public Text Text { get; set; }

    public override void Init(_Choice parent) {
        base.Init(parent);

        ProcessIcon(this, Entries);


        if (GetApplication.DictionaryMenuEntries.TryGetValue(Id.Label, out var entry)) {
            Icon ??= entry.Icon;
            entry.Icon ??= Icon;
            Text ??= entry.Text;
            entry.Text ??= Text;
            }
        else {
            GetApplication.DictionaryMenuEntries.Add(Id.Label, this);
            }


        }

    }




