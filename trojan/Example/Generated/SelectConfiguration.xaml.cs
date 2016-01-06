
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

    public partial class _Data_SelectConfiguration :Goedel.Trojan.Data  {
		public Dialog_SelectConfiguration Dialog;

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

		public virtual bool Managed () {
			return true;
			}

		/// <summary>
		/// Here goes the action to be overriden
		/// </summary>

		public virtual bool Independent () {
			return true;
			}


		}

    public partial class SelectConfiguration : _Data_SelectConfiguration {

		
		public SelectConfiguration (AddAccount  Data) {
			_Data = Data;

			// NB call to the initializer before we creaate the dialog so the
			// dialog can display the initialized data.
			Initialize ();
			this.Dialog = new Dialog_SelectConfiguration (this);
			}
		}


    /// <summary>
	/// This is the code behind for the XAML generated class.
    /// </summary>
    public partial class Dialog_SelectConfiguration : Page {

		public SelectConfiguration  Data;


		public Dialog_SelectConfiguration (SelectConfiguration Data) {
			InitializeComponent();
			this.Data = Data;
			Refresh ();

			}

		public void Refresh () {
			}

        private void Action_Managed(object sender, RoutedEventArgs e) {
			var Result = Data.Managed ();
			if (Result) {
				Data.Data.Navigate (Data.Data.Data_SelectServices);
				}
            }
        private void Action_Independent(object sender, RoutedEventArgs e) {
			var Result = Data.Independent ();
			if (Result) {
				Data.Data.Navigate (Data.Data.Data_GenerateKeys);
				}
            }



		}
	}

