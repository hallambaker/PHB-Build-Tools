﻿using System;
using System.Collections.Generic;
using Goedel.Trojan;
using Gtk;

namespace Goedel.Trojan.GTK {

    /// <summary>
    /// Binding for the standard GTK binding targeting desktops.
    /// Note that support for phones and tablets is likely to be
    /// best supported in a sub class.
    /// </summary>
    public partial class BindingGTK : Binding {
        static bool Initialized = false;
        public static Gdk.RGBA ColorPaper;
        public static Gdk.RGBA ColorInk;
        public static Gdk.RGBA ColorHighlight;
        public static Gdk.RGBA ColorHighlightInk;
        public static Gdk.RGBA ColorInactive;

        Gtk.Window GTKWindow;
        Gtk.MenuBar MenuBar;
        Gtk.TreeStore GTKModel;
        Gtk.Box GTKVBox;
        Gtk.Paned GTKPaneMain;
        Gtk.TreeView TreeView;

        Gtk.Frame GTKFrameWork;


        Window Window;
        Model Model;



        public BindingGTK() {
            if (!Initialized) {
                InitializeColors();
                }

            Application.Init();
            }


        void InitializeColors() {
            ColorPaper = new Gdk.RGBA();
            ColorPaper.Parse ("white");
            ColorInk = new Gdk.RGBA();
            ColorInk.Parse("black");
            ColorHighlight = new Gdk.RGBA();
            ColorHighlight.Parse("blue");
            ColorHighlightInk = new Gdk.RGBA();
            ColorHighlightInk.Parse("white");
            ColorInactive = new Gdk.RGBA();
            ColorInactive.Parse("grey");
            }

        public override void Initialize(Window Window) {
            this.Window = Window;
            Model = Window.Model;


            GTKWindow = new Gtk.Window(Window.Title);
            GTKWindow.Resize(800, 600);
            GTKWindow.OverrideBackgroundColor(StateFlags.Normal, ColorPaper);
            GTKWindow.OverrideColor(StateFlags.Normal, ColorInk);
            GTKWindow.DeleteEvent += delete_event;

            // Create a Vertical box in which to stack the Menu bar, split pane.
            GTKVBox = new VBox(false, 0);
            GTKWindow.Add(GTKVBox);

            // Add the menu bar
            MenuBar = MakeMenuBar(Window.Menu);
            GTKVBox.PackStart(MenuBar, false, true, 0);

            // Pane below with slider for Selector, Document area
            GTKPaneMain = new HPaned();
            GTKVBox.Add(GTKPaneMain);

            TreeView = new Gtk.TreeView();

            // We add the event handlers (i.e. the control part) to the tree
            TreeView.RowActivated += SelectorActivated;         //On double click
            TreeView.Selection.Changed += SelectorSelected;    // On select (single click)
               // Raise a context menu here??
               //  Connect to the ButtonPressEvent 
               //  Raise a popup button

            // Create columns [View]
            Gtk.TreeViewColumn TreeViewColumTitle = new Gtk.TreeViewColumn();
            TreeViewColumTitle.Title = "Profile";
            Gtk.CellRendererText NameCellTitle = new Gtk.CellRendererText();
            TreeViewColumTitle.PackStart(NameCellTitle, true);
            TreeViewColumTitle.SetCellDataFunc(NameCellTitle, new Gtk.TreeCellDataFunc(RenderTitle));
            //NameCellTitle.AddNotification(SelectorObjectNotify);


            NameCellTitle.Mode = CellRendererMode.Activatable;

            // Populate the model
            // Note that we could dispense with this step if we generated an ITreeModel
            // interface in the Object class.
            BindModel(Model);

            // Attach everything to the pane
            TreeView.Model = GTKModel;
            TreeView.AppendColumn(TreeViewColumTitle);

            GTKPaneMain.Add1(TreeView);

            GTKFrameWork = new Frame();
            GTKFrameWork.SetSizeRequest(80, 60);
            GTKPaneMain.Add2(GTKFrameWork);

            GTKWindow.ShowAll();
            }


        private void BindModel(Model Model) {
            GTKModel = new Gtk.TreeStore(typeof(Object));
            foreach (Object Object in Model.Selector) {
                var BindingData = new BindingDataGTK(this, Object);
                BindingData.Iter = GTKModel.AppendValues(Object);
                Object.BindingData = BindingData;
                }
            }


        private void RenderTitle(Gtk.TreeViewColumn Column, Gtk.CellRenderer Cell, 
                            Gtk.ITreeModel GTKModel, Gtk.TreeIter Iter) {
            Object Object = (Object)GTKModel.GetValue(Iter, 0);
            (Cell as Gtk.CellRendererText).Text = Object.Title;
            }

        //Gtk.Table GTKTable = null; 


        EntryForm EntryForm = null;
        private void SetWorkObject(Object Object) {
            // Here we write out an interface widget for Object to the Work Frame.

            if (EntryForm != null) {
                GTKFrameWork.Remove(EntryForm);
                EntryForm.Dispose();
                }
            EntryForm = new EntryForm(Object);
            GTKFrameWork.Add(EntryForm);
            //EntryForm.Show();
            EntryForm.ShowAll();
                     

            }

        private Gtk.Widget MakeFieldWidget(Object Object, ObjectEntry Entry) {
            var ObjectField = Entry as ObjectField;
            if (ObjectField == null) return null;

            //switch (ObjectField.Type) {
            //    case WidgetType.String: {
            //        var Value = Object.GetField(ObjectField.Index) as string;
            //        var Field = new Gtk.Entry(Value);
            //        Field.IsEditable = false;
            //        return Field;
            //        }
            //    case WidgetType.Integer: {
            //        var Field = new Gtk.Entry("666");
            //        return Field;
            //        }
            //    case WidgetType.Secret: {
            //        var Field = new Gtk.Entry("Initial Text");
            //        Field.Visibility = false;
            //        return Field;
            //        }
                //}
            return null;
            }

        // Events ...
        void SelectorActivated(object Sender, RowActivatedArgs args) {
            Console.WriteLine("Hello!");

            var View = (TreeView)Sender;

            TreeIter Iter;

            if (View.Model.GetIter(out Iter, args.Path)) {
                Console.WriteLine("   Something");

                var Object = (Object) View.Model.GetValue(Iter, 0);
                SetWorkObject(Object);
                }

            }

        static void SelectorSelected(object obj, EventArgs args) {
            Console.WriteLine("Selected!");
            }



        // runs when the user deletes the window using the "close
        // window" widget in the window frame.
        static void delete_event(object obj, DeleteEventArgs args) {
            Application.Quit();
            }


        private Gtk.MenuBar MakeMenuBar(Menu Menu) {
            var MenuBar = new MenuBar();

            foreach (var Entry in Menu.Entries) {
                var MenuEntry = Entry as MenuEntry;
                if (MenuEntry != null) {
                    MenuItem MenuItem = new MenuItem(Model, MenuEntry);

                    // Process the submenus
                    var SubMenu = MenuEntry as SubMenu;
                    if (SubMenu != null) {
                        MenuItem.Submenu = MakeMenu(SubMenu.Sub);
                        }

                    MenuBar.Append(MenuItem);
                    }
                }

            return MenuBar;
            }


        private Gtk.Menu MakeMenu(Menu Menu) {
            var Result = new Gtk.Menu();

            foreach (var Entry in Menu.Entries) {
                var MenuEntry = Entry as MenuEntry;
                if (MenuEntry != null) {
                    MenuItem MenuItem = new MenuItem(Model, MenuEntry);

                    // Process the submenus
                    var SubMenu = MenuEntry as SubMenu;
                    if (SubMenu != null) {
                        MenuItem.Submenu = MakeMenu(SubMenu.Sub);
                        }

                    Result.Append(MenuItem);
                    }
                }

            return Result;
            }

        /// <summary>
        /// Method used by calling application to transfer control to GTK GUI
        /// </summary>
        public override void Run() {
            Application.Run();
            }



        /// <summary>
        /// Callback to terminate control by GTK GUI.
        /// </summary>
        public override void Quit() {
            Application.Quit();
            }

        public override void Wizard(Wizard Wizard) {
            var GTKWizard = new GTKWizard(Wizard);

            }


        public override void About(About About) {

            var AboutDialog = new Gtk.AboutDialog();
            AboutDialog.ProgramName = About.Name;
            AboutDialog.Version = About.Version;
            AboutDialog.Copyright = "© Copyright something";
            AboutDialog.Run();
            AboutDialog.Destroy();
            }


        public override ObjectHandle GetHandle(Object Object) {
            return null;
            }

        }


    public class BindingDataGTK : BindingData {
        BindingGTK Binding;
        Object Object;

        /// <summary>
        /// The Tree iterator element
        /// </summary>
        public Gtk.TreeIter Iter;

        public BindingDataGTK(BindingGTK Binding, Object Object) {
            this.Binding = Binding;
            this.Object = Object;
            }

        public override void Refresh() {
            }



        }



    public class MenuItem : Gtk.MenuItem {
        MenuEntry MenuEntry;
        Model Model;

        public MenuItem(Model Model, MenuEntry MenuEntry) : base(MenuEntry.Label) {
            this.Model = Model;
            this.MenuEntry = MenuEntry;
            Activated += OnActivated;
            }

        void OnActivated(object Sender, EventArgs Args) {
            Model.Dispach(MenuEntry.Id);
            }

        }




    }