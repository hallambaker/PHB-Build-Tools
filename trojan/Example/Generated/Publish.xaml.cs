
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

    public partial class _Data_Publish :Goedel.Trojan.Data  {
		public Dialog_Publish Dialog;

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

		public int Completion_PublishKeys = 0;

		public virtual void Do_PublishKeys () {
            Completion_PublishKeys = -1;
			Dialog.UpdateProgress ();
			PublishKeys ();
            Completion_PublishKeys = 100;
			Dialog.UpdateProgress ();
			}

		public virtual void PublishKeys () {
            Thread.Sleep(2000);
            Completion_PublishKeys = 100;
			}



		}

    public partial class Publish : _Data_Publish {

		
		public Publish (AddAccount  Data) {
			_Data = Data;

			// NB call to the initializer before we creaate the dialog so the
			// dialog can display the initialized data.
			Initialize ();
			this.Dialog = new Dialog_Publish (this);
			}
		}


    /// <summary>
	/// This is the code behind for the XAML generated class.
    /// </summary>
    public partial class Dialog_Publish : Page {

		public Publish  Data;

		public BackgroundWorker BackgroundWorker;

		public Dialog_Publish (Publish Data) {
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
			Data.Do_PublishKeys ();
            }

        public void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			Data.Data.Navigate (Data.Data.Data_Finish);
            }

        public void ProgressChanged(object sender, ProgressChangedEventArgs e) {
			if (Data.Completion_PublishKeys >= 0) {
				Task_PublishKeys.Value = Data.Completion_PublishKeys;
				Task_PublishKeys.IsIndeterminate = false;
				}
			else {
				Task_PublishKeys.IsIndeterminate = true;
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

