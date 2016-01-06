
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

    public partial class _Data_RecoverKey :Goedel.Trojan.Data  {
		public Dialog_RecoverKey Dialog;

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
		string _Input_AccountID;
		public string Input_AccountID {
            get { return _Input_AccountID; }
            set { _Input_AccountID = value;  Refresh (); }
            }
		string _Input_RecoveryShare1;
		public string Input_RecoveryShare1 {
            get { return _Input_RecoveryShare1; }
            set { _Input_RecoveryShare1 = value;  Refresh (); }
            }
		string _Input_RecoveryShare2;
		public string Input_RecoveryShare2 {
            get { return _Input_RecoveryShare2; }
            set { _Input_RecoveryShare2 = value;  Refresh (); }
            }

		// Output backing variables



		/// <summary>
		/// Here goes the action to be overriden
		/// </summary>

		public virtual bool Recover () {
			return true;
			}


		}

    public partial class RecoverKey : _Data_RecoverKey {

		
		public RecoverKey (AddAccount  Data) {
			_Data = Data;

			// NB call to the initializer before we creaate the dialog so the
			// dialog can display the initialized data.
			Initialize ();
			this.Dialog = new Dialog_RecoverKey (this);
			}
		}


    /// <summary>
	/// This is the code behind for the XAML generated class.
    /// </summary>
    public partial class Dialog_RecoverKey : Page {

		public RecoverKey  Data;


		public Dialog_RecoverKey (RecoverKey Data) {
			InitializeComponent();
			this.Data = Data;
			Refresh ();

			}

		public void Refresh () {
			Input_AccountID.Text  = Data.Input_AccountID;
			Input_RecoveryShare1.Text  = Data.Input_RecoveryShare1;
			Input_RecoveryShare2.Text  = Data.Input_RecoveryShare2;
			}

        private void Action_Recover(object sender, RoutedEventArgs e) {
			var Result = Data.Recover ();
			if (Result) {
				Data.Data.Navigate (Data.Data.Data_EnableApplications);
				}
            }


		private void Changed_Input_AccountID (object sender, TextChangedEventArgs e) {
			Data.Input_AccountID = Input_AccountID.Text;
			}
		private void Changed_Input_RecoveryShare1 (object sender, TextChangedEventArgs e) {
			Data.Input_RecoveryShare1 = Input_RecoveryShare1.Text;
			}
		private void Changed_Input_RecoveryShare2 (object sender, TextChangedEventArgs e) {
			Data.Input_RecoveryShare2 = Input_RecoveryShare2.Text;
			}

		}
	}

