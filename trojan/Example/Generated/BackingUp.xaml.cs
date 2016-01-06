
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

    public partial class _Data_BackingUp :Goedel.Trojan.Data  {
		public Dialog_BackingUp Dialog;

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
		string _Output_EscrowEncryption1;
		public string Output_EscrowEncryption1 {
            get { return _Output_EscrowEncryption1; }
            set { _Output_EscrowEncryption1 = value;   Refresh (); 				
				//if (Dialog != null) { Dialog.UpdateProgress(); } 
}
            }
		string _Output_EscrowEncryption2;
		public string Output_EscrowEncryption2 {
            get { return _Output_EscrowEncryption2; }
            set { _Output_EscrowEncryption2 = value;   Refresh (); 				
				//if (Dialog != null) { Dialog.UpdateProgress(); } 
}
            }

		public int Completion_EscrowKeys = 0;

		public virtual void Do_EscrowKeys () {
            Completion_EscrowKeys = -1;
			Dialog.UpdateProgress ();
			EscrowKeys ();
            Completion_EscrowKeys = 100;
			Dialog.UpdateProgress ();
			}

		public virtual void EscrowKeys () {
            Thread.Sleep(2000);
            Completion_EscrowKeys = 100;
			}


		/// <summary>
		/// Here goes the action to be overriden
		/// </summary>

		public virtual bool BackupNext () {
			return true;
			}


		}

    public partial class BackingUp : _Data_BackingUp {

		
		public BackingUp (AddAccount  Data) {
			_Data = Data;

			// NB call to the initializer before we creaate the dialog so the
			// dialog can display the initialized data.
			Initialize ();
			this.Dialog = new Dialog_BackingUp (this);
			}
		}


    /// <summary>
	/// This is the code behind for the XAML generated class.
    /// </summary>
    public partial class Dialog_BackingUp : Page {

		public BackingUp  Data;

		public BackgroundWorker BackgroundWorker;

		public Dialog_BackingUp (BackingUp Data) {
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
			Data.Do_EscrowKeys ();
            }

        public void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            }

        public void ProgressChanged(object sender, ProgressChangedEventArgs e) {
			if (Data.Completion_EscrowKeys >= 0) {
				Task_EscrowKeys.Value = Data.Completion_EscrowKeys;
				Task_EscrowKeys.IsIndeterminate = false;
				}
			else {
				Task_EscrowKeys.IsIndeterminate = true;
				}
			Refresh ();
            }

		public void UpdateProgress () {
			BackgroundWorker.ReportProgress(100);
			} 
		public void Refresh () {
			Output_EscrowEncryption1.Text  = Data.Output_EscrowEncryption1;
			Output_EscrowEncryption2.Text  = Data.Output_EscrowEncryption2;
			}

        private void Action_BackupNext(object sender, RoutedEventArgs e) {
			var Result = Data.BackupNext ();
			if (Result) {
				Data.Data.Navigate (Data.Data.Data_EnableApplications);
				}
            }



		}
	}

