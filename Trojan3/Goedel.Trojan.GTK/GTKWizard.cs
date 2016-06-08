using System;
using System.Collections.Generic;
using Goedel.Trojan;
using Gtk;

namespace Goedel.Trojan.GTK {
    public class GTKWizard : Gtk.Window {

        public Grid GridMain = null;
        public Grid GridProgress = null;
        public Grid GridDescription = null;
        public Grid GridNavigate = null;
        public GridForm GridField = null;


        List<BreadCrumb> BreadCrumbs = new List<BreadCrumb>();

        Wizard Wizard;

        public int StepIndex {
            get {
                return Wizard.StepIndex;
                }

            set {
                Wizard.StepIndex = value;
                Render();
                }
            }

        /// <summary>
        /// Free up all the resources used.
        /// </summary>
        ~GTKWizard() {
            if (GridMain != null) GridMain.Destroy();
            if (GridProgress != null) GridProgress.Destroy();
            if (GridDescription != null) GridDescription.Destroy();
            if (GridNavigate != null) GridNavigate.Destroy();
            foreach (var BreadCrumb in BreadCrumbs) {
                BreadCrumb.Destroy();
                }
            }


        public GTKWizard(Wizard Wizard) : base (Wizard.Title) {
            this.Wizard = Wizard;

            if (Wizard.Steps.Count < 1) {
                return;
                }

            SetDefaultSize(800, 600);
            SetPosition(WindowPosition.Center);

            GridMain = new Grid();
            Add(GridMain);

            GridProgress = new Grid();

            GridProgress.ColumnHomogeneous = true;

            var BreadCrumb = new BreadCrumb("Start");
            BreadCrumbs.Add(BreadCrumb);
            GridProgress.Attach(BreadCrumb, 0, 0, 1, 1);
            foreach (var Step in Wizard.Steps) {
                var Next = new BreadCrumb(Step.Title);
                GridProgress.AttachNextTo(Next, BreadCrumb, PositionType.Right, 1, 1);
                BreadCrumb = Next;
                BreadCrumbs.Add(BreadCrumb);
                }

            GridMain.Attach(GridProgress, 0, 0, 1, 1);

            Render();

            }

        void Render () {
            // Delete the old description grid.
            if (GridDescription != null) {
                GridMain.Remove(GridDescription);
                GridDescription.Destroy();
                GridDescription = null;
                }
            if (GridNavigate != null) {
                GridMain.Remove(GridNavigate);
                GridNavigate.Destroy();
                GridNavigate = null;
                }

            if (StepIndex < 0) {
                RenderSplash();
                }
            else {
                RenderStep();
                }

            var i = -1;
            foreach (var BreadCrumb in BreadCrumbs) {
                BreadCrumb.Complete = (i < StepIndex);
                i++;
                }

            GridNavigate = new NavigateGrid(this, StepIndex, Wizard.Steps.Count);
            GridMain.Attach(GridNavigate, 0, 3, 1, 1);

            ShowAll();
            }

        void RenderSplash() {
            TextGrid.Render(ref GridDescription, Wizard.Title, Wizard.Texts);
            GridMain.Attach(GridDescription, 0, 1, 1, 1);
            }

        void RenderStep() {
            var Step = Wizard.Steps[StepIndex];
            TextGrid.Render(ref GridDescription, Step.Title, Step.Description);
            GridMain.Attach(GridDescription, 0, 1, 1, 1);

            if (GridField != null) {
                GridField.Destroy();
                }

            GridField = new GridForm (Step.Value, Step.Value.Entries);
            GridMain.Attach(GridField, 0, 2, 1, 1);
            }

        private class BreadCrumb : Label {
            private bool _Complete;

            public bool Complete {
                set {
                    _Complete = value;
                    if (!_Complete) {
                        OverrideBackgroundColor(StateFlags.Normal, BindingGTK.ColorPaper);
                        OverrideColor(StateFlags.Normal, BindingGTK.ColorInk);
                        }
                    else {
                        OverrideBackgroundColor(StateFlags.Normal, BindingGTK.ColorHighlight);
                        OverrideColor(StateFlags.Normal, BindingGTK.ColorHighlightInk);
                        }
                    }
                }
            public BreadCrumb(string Text) : base (Text) {
                _Complete = false;
                Halign = Align.Center;
                Valign = Align.Start;
                Hexpand = true;
                Vexpand = false;
                Show();
                }

            }


        private class TextGrid : Grid {
            Label LabelTitle = null;
            List<Label> TextLabels = new List<Label>();

            /// <summary>
            /// Free all the resources used.
            /// </summary>
            ~TextGrid() {
                Destroy();
                }

            public TextGrid(string Title, List<string> Texts) {
                LabelTitle = new Label(Title);
                LabelTitle.Wrap = true;
                LabelTitle.Halign = Align.Start;
                LabelTitle.Valign = Align.Start;

                Attach(LabelTitle, 0, 0, 1, 1);

                int Row = 1;
                foreach (var Text in Texts) {
                    var Label = new Label(Text);
                    Label.Xalign = 0;
                    Label.LineWrap = true;
                    Label.Justify = Justification.Left;
                    Label.Halign = Align.Start;
                    Label.Valign = Align.Start;
                    Label.MarginLeft = 10;
                    Label.MarginRight = 10;
                    Label.MarginTop = 10;
                    Label.MarginBottom = 10;
                    Attach(Label, 0, Row++, 1, 1);

                    TextLabels.Add(Label);
                    }

                }

            /// <summary>
            /// Destructor. MUST NOT call any further methods after Destroy has been called;
            /// </summary>
            public override void Destroy() {
                base.Destroy();
                if (LabelTitle != null) LabelTitle.Destroy();
                foreach (var TextLabel in TextLabels) {
                    TextLabel.Destroy();
                    }

                }


            public static void Render(ref Grid TextGrid, string Title, List<string> Texts) {
                if (TextGrid != null) {
                    TextGrid.Dispose();
                    }
                TextGrid = new TextGrid(Title, Texts);
                }

            }

        private class NavigateGrid : Grid {

            int StepIndex;
            int StepCount;

            Button Next;
            Button Previous;


            public NavigateGrid(GTKWizard GTKWizard, int StepIndex, int StepCount) {
                this.StepCount = StepCount;
                this.StepIndex = StepIndex;

                if (StepIndex < 0) {
                    Next = new NavigateButtonNext(GTKWizard, DialogText.WizardButtonStart, 0);
                    Next.Valign = Align.End;
                    Next.Halign = Align.End;
                    Attach(Next, 0, 0, 1, 1);
                    }

                else {
                    Previous = new NavigateButtonPrev(GTKWizard, DialogText.WizardButtonBack, StepIndex-1);
                    Previous.Valign = Align.End;
                    Previous.Halign = Align.End;
                    Attach(Previous, 0, 0, 1, 1);

                    var PreviousString = StepIndex + 1 == StepCount ? 
                            DialogText.WizardButtonFinish : DialogText.WizardButtonNext;
                    Next = new NavigateButtonNext(GTKWizard, PreviousString, StepIndex+1);
                    Next.Valign = Align.End;
                    Next.Halign = Align.End;
                    Attach(Next, 1, 0, 1, 1);
                    }

                }

            }

        public class NavigateButtonPrev : Button {

            int Step;
            GTKWizard GTKWizard;

            public NavigateButtonPrev(GTKWizard GTKWizard, string Text, int Step) : base (Text) {
                this.Step = Step;
                this.GTKWizard = GTKWizard;
                Clicked += OnClick;
                }


            public void OnClick (object sender, EventArgs args) {
                GTKWizard.StepIndex = Step;
                }


            }

        public class NavigateButtonNext : Button {

            int Step;
            GTKWizard GTKWizard;

            public NavigateButtonNext(GTKWizard GTKWizard, string Text, int Step) : base(Text) {
                this.Step = Step;
                this.GTKWizard = GTKWizard;
                Clicked += OnClick;
                }


            public void OnClick(object sender, EventArgs args) {
                var Wizard = GTKWizard.Wizard;
                if (Step <= 0) {
                    GTKWizard.StepIndex = 0;
                    return;
                    }

                var Valid = GTKWizard.GridField.Valid();

                if (Valid) {
                    Wizard.CurrentStep.Value.Dispatch(Wizard);

                    if (Step < Wizard.Steps.Count) {

                        GTKWizard.StepIndex = Step;
                        }
                    else {
                        Wizard.Dispatch();
                        GTKWizard.Destroy();
                        }
                    }
                }


            }



        }
    }
