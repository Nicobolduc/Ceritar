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
    public partial class frmWorkInProgress : Form
    {
        public frmWorkInProgress()
        {
            InitializeComponent();
        }

        private void frmWorkInProgress_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        private void frmWorkInProgress_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }
    }
}
