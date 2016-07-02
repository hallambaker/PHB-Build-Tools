using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Goedel.Trojan;

namespace Goedel.Trojan.GTK {

    /// <summary>
    /// A class to create a ListView object that is bound to a list of objects that
    /// conform to a particular object prototype.
    /// </summary>
    class ListView {

        Gtk.ListStore GtkData;
        List<Object> Values;
        Object Prototype;

        public ListView(Gtk.TreeView TreeView, Object Prototype, List<Object> Values) {
            this.Prototype = Prototype;
            this.Values = Values;
            GtkData = new Gtk.ListStore(typeof(Object));
            TreeView.Model = GtkData;

            var ink = BindingGTK.ColorInk;
            ink.Alpha = 1;
            //TreeView.OverrideColor(Gtk.StateFlags.Normal, ink);
            //TreeView.ModifyFg(Gtk.StateType.Normal, new Gdk.Color (0,0,0));
            //TreeView.ModifyBase(Gtk.StateType.Normal, new Gdk.Color(0, 255, 255));

            for (var i = 0; i < Prototype.Entries.Count; i++) {
                var Entry = Prototype.Entries[i];
                TreeView.AppendColumn(TreeViewColumn(Entry, i));
                }

            BindData();
            }

        public void BindData() {
            GtkData.Clear();
            foreach (var Value in Values) {
                GtkData.AppendValues(Value);
                }

            }


        private Gtk.TreeViewColumn TreeViewColumn(ObjectEntry Entry, int i) {
            var Result = new Gtk.TreeViewColumn();
            var CellRenderer = GetCellRenderer(Result, Entry, i);

            Result.Title = Entry.Label;
            Result.PackStart(CellRenderer, true);

            return Result;
            }

        private Gtk.CellRenderer GetCellRenderer(Gtk.TreeViewColumn Column, 
                            ObjectEntry Entry, int i) {

            if ((Entry as ObjectFieldString != null) | (Entry as ObjectFieldInteger != null)){
                return new CellRendererText(Column, i);
                }

            return null;
            }


        private class CellRendererText : Gtk.CellRendererText {
            int Index;

            public CellRendererText(Gtk.TreeViewColumn Column, int Index) {
                this.Index = Index;

                Column.SetCellDataFunc(this, new Gtk.TreeCellDataFunc(CellRender));
                }


            private void CellRender(
                    Gtk.TreeViewColumn Column,
                    Gtk.CellRenderer Cell,
                    Gtk.ITreeModel Model,
                    Gtk.TreeIter Iter) {
                Object Object = (Object)Model.GetValue(Iter, 0);
                var ObjectEntry = Object.Entries?[Index];


                var CellRendererText = (Cell as Gtk.CellRendererText);

                if (ObjectEntry as ObjectFieldSecret != null) {
                    CellRendererText.Text = (ObjectEntry as ObjectFieldSecret).Value;
                    }
                else if (ObjectEntry as ObjectFieldString != null) {
                    CellRendererText.Text = (ObjectEntry as ObjectFieldString).Value;
                    }

                else if (ObjectEntry as ObjectFieldInteger != null) {
                    CellRendererText.Text = (ObjectEntry as ObjectFieldInteger).Value.ToString();
                    }
                }

            }

        private class CellRendererBoolean : Gtk.CellRendererToggle {
            int Index;

            public CellRendererBoolean(Gtk.TreeViewColumn Column, int Index) {
                this.Index = Index;

                Column.SetCellDataFunc(this, new Gtk.TreeCellDataFunc(CellRender));
                }


            private void CellRender(
                    Gtk.TreeViewColumn Column,
                    Gtk.CellRenderer Cell,
                    Gtk.ITreeModel Model,
                    Gtk.TreeIter Iter) {
                Object Object = (Object)Model.GetValue(Iter, 0);
                var ObjectEntry = Object.Entries?[Index];
                var ObjectFieldBoolean = ObjectEntry as ObjectFieldBoolean;


                var CellRendererBoolean = (Cell as Gtk.CellRendererToggle);


                CellRendererBoolean.Active = ObjectFieldBoolean.Value;
                }

            }

        }
    }
