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
    /// <summary>
    /// Cette classe représente la fenêtre principale de l'application via laquelle tous les menus sont accessibles.
    /// </summary>
    public partial class mdiGeneral : Form
    {
        public mdiGeneral()
        {
            InitializeComponent();
        }

        private void main()
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {        }

        private void installationsActivesToolStripMenuItem_Click(object sender, EventArgs e)
        {        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.VERSION_REVISION_LIST_NRI);
        }

        private void mdiGeneral_Load(object sender, EventArgs e)
        {
            main();

            lblDatabase.Text =  clsApp.GetAppController.SQLConnection.Database;
            lblCurrentUser.Text = "Utilisateur: Nicolas";
        }

        private void mdiGeneral_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clsApp.GetAppController.SQLConnection != null) clsApp.GetAppController.SQLConnection.Dispose();
        }

        private void mnuCerApp_Click(object sender, EventArgs e)
        {
            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.CERITAR_APPLICATION_LIST_NRI);
        }

        private void gabaritToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.TEMPLATE_LIST_NRI);
        }

        private void clientCeritarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.CERITAR_CLIENT_LIST_NRI);
        }
    }
}
