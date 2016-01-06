
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

    public partial class _Data_ConnectWait :Goedel.Trojan.Data  {
		public Dialog_ConnectWait Dialog;

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
		string _Output_AccountID;
		public string Output_AccountID {
            get { return _Output_AccountID; }
            set { _Output_AccountID = value;   Refresh (); }
            }
		string _Output_ConfirmCode;
		public string Output_ConfirmCode {
            get { return _Output_ConfirmCode; }
            set { _Output_ConfirmCode = value;   Refresh (); }
            }




		}

    public partial class ConnectWait : _Data_ConnectWait {

		
		public ConnectWait (AddAccount  Data) {
			_Data = Data;

			// NB call to the initializer before we creaate the dialog so the
			// dialog can display the initialized data.
			Initialize ();
			this.Dialog = new Dialog_ConnectWait (this);
			}
		}


    /// <summary>
	/// This is the code behind for the XAML generated class.
    /// </summary>
    public partial class Dialog_ConnectWait : Page {

		public ConnectWait  Data;


		public Dialog_ConnectWait (ConnectWait Data) {
			InitializeComponent();
			this.Data = Data;
			Refresh ();

			}

		public void Refresh () {
			Output_AccountID.Text  = Data.Output_AccountID;
			Output_ConfirmCode.Text  = Data.Output_ConfirmCode;
			}




		}
	}

