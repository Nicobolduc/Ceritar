using System;
using System.Windows.Forms;
using Ceritar.TT3LightDLL.Classes;

namespace Ceritar.Logirack_CVS
{
    public partial class frmRevision : Form
    {
        //Classes
        private clsC1FlexGridWrapper mcGrdRevModifs;


        public frmRevision()
        {
            InitializeComponent();


            mcGrdRevModifs = new clsC1FlexGridWrapper();
           
        }
    }
}
