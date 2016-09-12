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

namespace Ceritar.Logirack_CVS.Forms
{
    /// <summary>
    /// Cette classe représente la fenêtre principale de l'application via laquelle tous les menus sont accessibles.
    /// </summary>
    public partial class mdiGeneral : Form
    {
        //Messages
        private const int mintMSG_AdminRightWarning = 40;

        //Working variables
        private int childFormNumber = 0;

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

            pfblnAutoLogInUser();

            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.VERSION_REVISION_LIST_NRI);
        }

        private bool pfblnAutoLogInUser()
        {
            bool blnValidReturn;
            string strUser_Code;
            int intUser_NRI = 0;

            try
            {
                strUser_Code = clsSQL.str_ADOSingleLookUp("TTInI_Value", "TTInI", "TTInI_Config = 'User_Code' AND TTInI_ComputerID = " + clsApp.GetAppController.str_FixStringForSQL(clsApp.GetAppController.cUser.str_GetUserComputerID()));

                if (!string.IsNullOrEmpty(strUser_Code))
                {
                    Int32.TryParse(clsSQL.str_ADOSingleLookUp("TTU_NRI", "TTUser", "TTU_Code = " + clsApp.GetAppController.str_FixStringForSQL(strUser_Code)), out intUser_NRI);

                    clsApp.GetAppController.cUser.User_NRI = intUser_NRI;
                }

                if (string.IsNullOrEmpty(strUser_Code) || intUser_NRI <= 0)
                {
                    frmUser frmUser = new frmUser();

                    frmUser.MdiParent = this;

                    ((TT3LightDLL.Controls.IFormController)frmUser).GetFormController().ShowForm(this, sclsConstants.DML_Mode.INSERT_MODE, ref intUser_NRI, true, true);
                }

                blnValidReturn = true;
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
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

        private void mnuVersion_Click(object sender, EventArgs e)
        {
            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.VERSION_REVISION_LIST_NRI);
        }

        private void mdiGeneral_Load(object sender, EventArgs e)
        {
            main();

            //lblDatabase.Text = clsApp.GetAppController.SQLConnection.Database;
            //lblCurrentUser.Text = "Utilisateur: Nicolas";
        }

        private void mnuCerApp_Click(object sender, EventArgs e)
        {
            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.CERITAR_APPLICATION_LIST_NRI);
        }

        private void mnuGabarit_Click(object sender, EventArgs e)
        {
            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.TEMPLATE_LIST_NRI);
        }

        private void mnuCerClient_Click(object sender, EventArgs e)
        {
            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.CERITAR_CLIENT_LIST_NRI);
        }

        private void mnuCloseAllWindows_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();

            foreach (Form aForm in this.MdiChildren)
            {
                if (aForm is TT3LightDLL.Controls.IFormController) ((TT3LightDLL.Controls.IFormController)aForm).GetFormController().mblnDisableBeNotify = true;

                aForm.Close();
            }

            this.ResumeLayout();
        }

        private void mnuUser_Click(object sender, EventArgs e)
        {
            frmUser frmUser = new frmUser();
            int intItem_NRI = clsApp.GetAppController.cUser.User_NRI;

            frmUser.MdiParent = this;

            ((TT3LightDLL.Controls.IFormController)frmUser).GetFormController().ShowForm(this, sclsConstants.DML_Mode.UPDATE_MODE, ref intItem_NRI);
        }
    }
}
