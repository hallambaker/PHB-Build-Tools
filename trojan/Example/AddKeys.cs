using System;
using System.Collections.Generic;


namespace PersonalPrivacyAssistant {

    public partial class Data_AddAccount {
        public override void Initialize() {
            }
        }

    public partial class Data_BackingUp {
        public override void Initialize() {
            Output_EscrowEncryption1 = "Helloooo";
            Output_EscrowEncryption2 = "Wurld!";
            }
        }

    public partial class Data_GenerateKeys {
        public override void Initialize() {
            }

        string UserName;

        public void GetData () {
            UserName = Wizard.Dialog_SelectNormal.Data.Input_RealName;
            }

        public override void GenerateMasterRoot() {
            base.GenerateMasterRoot();
            UserName = Data.Data_SelectNormal.Input_RealName;
            }

        public override void GenerateIntermediate() {
            base.GenerateIntermediate();
            }

        public override void GenerateEscrow() {
            base.GenerateEscrow();
            }

        }
    }
