﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;

namespace Ceritar.Logirack_CVS
{
    public partial class mdiGeneral : Form
    {
        public mdiGeneral()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void installationsActivesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVersionRevision frmVersionAndRevision = new frmVersionRevision();

            frmVersionAndRevision.MdiParent = this;
            
            frmVersionAndRevision.ctrlForm.ShowForm(sclsConstants.Form_Mode.UPDATE_MODE, 0, false);
        }

        private void mdiGeneral_Load(object sender, EventArgs e)
        {
            main();

            lblDatabase.Text =  clsApp.GetAppController.MySQLConnection.Database;
            lblCurrentUser.Text = " Nicolas";
        }

        private void main() {

        }

        private void mdiGeneral_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clsApp.GetAppController.MySQLConnection != null) clsApp.GetAppController.MySQLConnection.Dispose();
        }
    }
}