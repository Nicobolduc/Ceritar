using Ceritar.TT3LightDLL.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ceritar.Logirack_CVS
{
    public partial class frmCeritarClient : Form
    {
        //Classes
        private clsC1FlexGridWrapper mcGrdApp;

        public frmCeritarClient()
        {
            InitializeComponent();

            mcGrdApp.SetGridDisplay += McGrdApp_SetGridDisplay;
        }

        private void McGrdApp_SetGridDisplay()
        {
            throw new NotImplementedException();
        }

        private void ctlFormController1_LoadData(TT3LightDLL.Controls.LoadDataEventArgs eventArgs)
        {

        }
    }
}
