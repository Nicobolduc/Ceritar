using System;
using System.Data;
using Ceritar.TT3LightDLL.Controls;
using System.Windows.Forms;

namespace Ceritar.Logirack_CVS
{
    public partial class frmVersionRevision : Form, IFormController
    {
        public frmVersionRevision()
        {
            InitializeComponent();
        }

        public void LoadDataEventHandler(LoadDataEventArgs eventArgs) {

            
        }

        ctlFormController IFormController.GetFormController()
        {
            return this.formController;
        }
    }
}
