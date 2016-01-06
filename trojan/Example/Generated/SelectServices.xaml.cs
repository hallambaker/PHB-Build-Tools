
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

    public partial class _Data_SelectServices :Goedel.Trojan.Data  {
		public Dialog_SelectServices Dialog;

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



		/// <summary>
		/// Here goes the action to be overriden
		/// </summary>

		public virtual bool PrismProof () {
			return true;
			}

		/// <summary>
		/// Here goes the action to be overriden
		/// </summary>

		public virtual bool Comodo () {
			return true;
			}

		/// <summary>
		/// Here goes the action to be overriden
		/// </summary>

		public virtual bool Other () {
			return true;
			}


		}

    public partial class SelectServices : _Data_SelectServices {

		
		public SelectServices (AddAccount  Data) {
			_Data = Data;

			// NB call to the initializer before we creaate the dialog so the
			// dialog can display the initialized data.
			Initialize ();
			this.Dialog = new Dialog_SelectServices (this);
			}
		}


    /// <summary>
	/// This is the code behind for the XAML generated class.
    /// </summary>
    public partial class Dialog_SelectServices : Page {

		public SelectServices  Data;


		public Dialog_SelectServices (SelectServices Data) {
			InitializeComponent();
			this.Data = Data;
			Refresh ();

			}

		public void Refresh () {
			}

        private void Action_PrismProof(object sender, RoutedEventArgs e) {
			var Result = Data.PrismProof ();
			if (Result) {
				Data.Data.Navigate (Data.Data.Data_GenerateKeys);
				}
            }
        private void Action_Comodo(object sender, RoutedEventArgs e) {
			var Result = Data.Comodo ();
			if (Result) {
				Data.Data.Navigate (Data.Data.Data_GenerateKeys);
				}
            }
        private void Action_Other(object sender, RoutedEventArgs e) {
			var Result = Data.Other ();
			if (Result) {
				Data.Data.Navigate (Data.Data.Data_GenerateKeys);
				}
            }



		}
	}

