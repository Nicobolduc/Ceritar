using System;
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
using System.Security.Principal;

namespace Ceritar.Logirack_CVS
{
    /// <summary>
    /// Cette classe représente la fenêtre principale de l'application via laquelle tous les menus sont accessibles.
    /// </summary>
    public partial class mdiGeneral : Form
    {
        //Messages
        private const int mintMSG_AdminRightWarning = 40;

        public mdiGeneral()
        {
            InitializeComponent();
        }

        private void main()
        {
            clsApp.Instanciate(this);

            Application.ApplicationExit += new EventHandler(ApplicationExit);

            if (!IsUserAdministrator())
            {
                clsApp.GetAppController.ShowMessage(mintMSG_AdminRightWarning);
            }

            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.VERSION_REVISION_LIST_NRI);
        }

        public static bool IsUserAdministrator()
        {
            bool blnIsAdmin;

            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);

                blnIsAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                blnIsAdmin = false;
            }
            catch (Exception)
            {
                blnIsAdmin = false;
            }

            return blnIsAdmin;
        }

        private void ApplicationExit(object sender, EventArgs e)
        {
            if (clsApp.GetAppController.SQLConnection != null) clsApp.GetAppController.SQLConnection.Close();
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

        private void fermerToutesLesFenêtresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
          
            foreach (Form aForm in this.MdiChildren)
            {
                if (aForm is TT3LightDLL.Controls.IFormController) ((TT3LightDLL.Controls.IFormController)aForm).GetFormController().mblnDisableBeNotify = true;

                aForm.Close();
            }

            this.ResumeLayout();
        }
    }
}
