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
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;
using Goedel.Trojan;

namespace PersonalPrivacyAssistant {


    public partial class Wizard_AddAccount : Window {

        public AddAccount Data;

        public Wizard_AddAccount() {
            InitializeComponent();

            Data = new AddAccount(this);
            }

        }

    public partial class AddAccount : Goedel.Trojan.Data {
        public Wizard_AddAccount Wizard;

		AddAccountStart _Data_AddAccountStart = null;
		NormalorAdvanced _Data_NormalorAdvanced = null;
		SelectNormal _Data_SelectNormal = null;
		SelectConfiguration _Data_SelectConfiguration = null;
		SelectServices _Data_SelectServices = null;
		GenerateKeys _Data_GenerateKeys = null;
		Escrow _Data_Escrow = null;
		BackingUp _Data_BackingUp = null;
		EnableApplications _Data_EnableApplications = null;
		Publish _Data_Publish = null;
		Finish _Data_Finish = null;
		ConnectKey _Data_ConnectKey = null;
		ConnectWait _Data_ConnectWait = null;
		RecoverKey _Data_RecoverKey = null;
		TBS _Data_TBS = null;

		public AddAccountStart Data_AddAccountStart {
			get { _Data_AddAccountStart = _Data_AddAccountStart != null ? _Data_AddAccountStart : new AddAccountStart (this);
			return _Data_AddAccountStart; } }
		public NormalorAdvanced Data_NormalorAdvanced {
			get { _Data_NormalorAdvanced = _Data_NormalorAdvanced != null ? _Data_NormalorAdvanced : new NormalorAdvanced (this);
			return _Data_NormalorAdvanced; } }
		public SelectNormal Data_SelectNormal {
			get { _Data_SelectNormal = _Data_SelectNormal != null ? _Data_SelectNormal : new SelectNormal (this);
			return _Data_SelectNormal; } }
		public SelectConfiguration Data_SelectConfiguration {
			get { _Data_SelectConfiguration = _Data_SelectConfiguration != null ? _Data_SelectConfiguration : new SelectConfiguration (this);
			return _Data_SelectConfiguration; } }
		public SelectServices Data_SelectServices {
			get { _Data_SelectServices = _Data_SelectServices != null ? _Data_SelectServices : new SelectServices (this);
			return _Data_SelectServices; } }
		public GenerateKeys Data_GenerateKeys {
			get { _Data_GenerateKeys = _Data_GenerateKeys != null ? _Data_GenerateKeys : new GenerateKeys (this);
			return _Data_GenerateKeys; } }
		public Escrow Data_Escrow {
			get { _Data_Escrow = _Data_Escrow != null ? _Data_Escrow : new Escrow (this);
			return _Data_Escrow; } }
		public BackingUp Data_BackingUp {
			get { _Data_BackingUp = _Data_BackingUp != null ? _Data_BackingUp : new BackingUp (this);
			return _Data_BackingUp; } }
		public EnableApplications Data_EnableApplications {
			get { _Data_EnableApplications = _Data_EnableApplications != null ? _Data_EnableApplications : new EnableApplications (this);
			return _Data_EnableApplications; } }
		public Publish Data_Publish {
			get { _Data_Publish = _Data_Publish != null ? _Data_Publish : new Publish (this);
			return _Data_Publish; } }
		public Finish Data_Finish {
			get { _Data_Finish = _Data_Finish != null ? _Data_Finish : new Finish (this);
			return _Data_Finish; } }
		public ConnectKey Data_ConnectKey {
			get { _Data_ConnectKey = _Data_ConnectKey != null ? _Data_ConnectKey : new ConnectKey (this);
			return _Data_ConnectKey; } }
		public ConnectWait Data_ConnectWait {
			get { _Data_ConnectWait = _Data_ConnectWait != null ? _Data_ConnectWait : new ConnectWait (this);
			return _Data_ConnectWait; } }
		public RecoverKey Data_RecoverKey {
			get { _Data_RecoverKey = _Data_RecoverKey != null ? _Data_RecoverKey : new RecoverKey (this);
			return _Data_RecoverKey; } }
		public TBS Data_TBS {
			get { _Data_TBS = _Data_TBS != null ? _Data_TBS : new TBS (this);
			return _Data_TBS; } }


		public AddAccount (Wizard_AddAccount Wizard) {
			this.Wizard = Wizard;
			Initialize ();
			if (CurrentDialog == null) {
				Navigate (Data_AddAccountStart);
				}
			}


		/// <summary>
		/// The currently active dialog
		/// </summary>
		public Goedel.Trojan.Data CurrentDialog = null ;

		/// <summary>
		/// Navigate to a new dialog.
		/// </summary>
		public void Navigate (Goedel.Trojan.Data Dialog) {
			if (CurrentDialog != null) {
				CurrentDialog.Exit ();
				}
			CurrentDialog = Dialog;
			CurrentDialog.Enter ();

			Wizard.Main.Navigate (Dialog.Page);
			}

		}
	}

