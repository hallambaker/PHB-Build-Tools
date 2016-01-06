
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

    public partial class _Data_ConnectKey :Goedel.Trojan.Data  {
		public Dialog_ConnectKey Dialog;

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

		// Output backing variables



		/// <summary>
		/// Here goes the action to be overriden
		/// </summary>

		public virtual bool Connect () {
			return true;
			}


		}

    public partial class ConnectKey : _Data_ConnectKey {

		
		public ConnectKey (AddAccount  Data) {
			_Data = Data;

			// NB call to the initializer before we creaate the dialog so the
			// dialog can display the initialized data.
			Initialize ();
			this.Dialog = new Dialog_ConnectKey (this);
			}
		}


    /// <summary>
	/// This is the code behind for the XAML generated class.
    /// </summary>
    public partial class Dialog_ConnectKey : Page {

		public ConnectKey  Data;


		public Dialog_ConnectKey (ConnectKey Data) {
			InitializeComponent();
			this.Data = Data;
			Refresh ();

			}

		public void Refresh () {
			Input_AccountID.Text  = Data.Input_AccountID;
			}

        private void Action_Connect(object sender, RoutedEventArgs e) {
			var Result = Data.Connect ();
			if (Result) {
				Data.Data.Navigate (Data.Data.Data_ConnectWait);
				}
            }


		private void Changed_Input_AccountID (object sender, TextChangedEventArgs e) {
			Data.Input_AccountID = Input_AccountID.Text;
			}

		}
	}

