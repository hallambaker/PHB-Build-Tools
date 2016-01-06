
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;
using Goedel.Trojan;

namespace PersonalPrivacyAssistant {

    public partial class _Data_GenerateKeys :Goedel.Trojan.Data  {
		public Dialog_GenerateKeys Dialog;

		public override Page Page {
		    get { return Dialog; }
			}

		protected AddAccount _Data;
		public AddAccount Data {
			get {return _Data;}
			}


		public void Refresh () {
			if (Dialog != null) {
				Dialog.Refresh ();
				}
			}

//		protected Wizard_AddAccount  _Wizard;	
//		public Wizard_AddAccount  Wizard {
//				get {return _Wizard;}
//				}
//		public Data_AddAccount  Data {
//				get {return Wizard.Data;}
//				}

		// Input backing variables

		// Output backing variables

		public int Completion_GenerateMasterRoot = 0;
		public int Completion_GenerateIntermediate = 0;
		public int Completion_GenerateEscrow = 0;

		public virtual void Do_GenerateMasterRoot () {
            Completion_GenerateMasterRoot = -1;
			Dialog.UpdateProgress ();
			GenerateMasterRoot ();
            Completion_GenerateMasterRoot = 100;
			Dialog.UpdateProgress ();
			}

		public virtual void GenerateMasterRoot () {
            Thread.Sleep(2000);
            Completion_GenerateMasterRoot = 100;
			}
		public virtual void Do_GenerateIntermediate () {
            Completion_GenerateIntermediate = -1;
			Dialog.UpdateProgress ();
			GenerateIntermediate ();
            Completion_GenerateIntermediate = 100;
			Dialog.UpdateProgress ();
			}

		public virtual void GenerateIntermediate () {
            Thread.Sleep(2000);
            Completion_GenerateIntermediate = 100;
			}
		public virtual void Do_GenerateEscrow () {
            Completion_GenerateEscrow = -1;
			Dialog.UpdateProgress ();
			GenerateEscrow ();
            Completion_GenerateEscrow = 100;
			Dialog.UpdateProgress ();
			}

		public virtual void GenerateEscrow () {
            Thread.Sleep(2000);
            Completion_GenerateEscrow = 100;
			}



		}

    public partial class GenerateKeys : _Data_GenerateKeys {

		
		public GenerateKeys (AddAccount  Data) {
			_Data = Data;

			// NB call to the initializer before we creaate the dialog so the
			// dialog can display the initialized data.
			Initialize ();
			this.Dialog = new Dialog_GenerateKeys (this);
			}
		}


    /// <summary>
	/// This is the code behind for the XAML generated class.
    /// </summary>
    public partial class Dialog_GenerateKeys : Page {

		public GenerateKeys  Data;

		public BackgroundWorker BackgroundWorker;

		public Dialog_GenerateKeys (GenerateKeys Data) {
			InitializeComponent();
			this.Data = Data;
			Refresh ();

            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.WorkerReportsProgress = true;
            BackgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
            BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            BackgroundWorker.RunWorkerAsync();
			}

        // Should probably move this to the Data class so that it can be inherited
        public void DoWork(object sender, DoWorkEventArgs e) {
			Data.Do_GenerateMasterRoot ();
			Data.Do_GenerateIntermediate ();
			Data.Do_GenerateEscrow ();
            }

        public void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			Data.Data.Navigate (Data.Data.Data_Escrow);
            }

        public void ProgressChanged(object sender, ProgressChangedEventArgs e) {
			if (Data.Completion_GenerateMasterRoot >= 0) {
				Task_GenerateMasterRoot.Value = Data.Completion_GenerateMasterRoot;
				Task_GenerateMasterRoot.IsIndeterminate = false;
				}
			else {
				Task_GenerateMasterRoot.IsIndeterminate = true;
				}
			if (Data.Completion_GenerateIntermediate >= 0) {
				Task_GenerateIntermediate.Value = Data.Completion_GenerateIntermediate;
				Task_GenerateIntermediate.IsIndeterminate = false;
				}
			else {
				Task_GenerateIntermediate.IsIndeterminate = true;
				}
			if (Data.Completion_GenerateEscrow >= 0) {
				Task_GenerateEscrow.Value = Data.Completion_GenerateEscrow;
				Task_GenerateEscrow.IsIndeterminate = false;
				}
			else {
				Task_GenerateEscrow.IsIndeterminate = true;
				}
			Refresh ();
            }

		public void UpdateProgress () {
			BackgroundWorker.ReportProgress(100);
			} 
		public void Refresh () {
			}




		}
	}

