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
        private const int mintMSG_NewUser = 43;

        //Working variables
        

        public mdiGeneral()
        {
            InitializeComponent();
        }

        private void main()
        {
            bool blnLoadSuccess = false;

            clsTTApp.Instanciate(this);

            lblStatus_BD.Text += clsTTApp.GetAppController.SQLConnection.Database;

            Application.ApplicationExit += new EventHandler(ApplicationExit);
            
            if (!IsUserAdministrator())
            {
                clsTTApp.GetAppController.ShowMessage(mintMSG_AdminRightWarning);
            }

            blnLoadSuccess = pfblnAutoLogInUser();

            if (!blnLoadSuccess) Application.Exit();

            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.VERSION_REVISION_LIST_NRI);
        }

        private bool pfblnAutoLogInUser()
        {
            bool blnValidReturn;
            string strUser_Code = string.Empty;
            DialogResult resultAnswer;
            int intUser_NRI = 0;

            try
            {
                blnValidReturn = true;

                strUser_Code = clsTTSQL.str_ADOSingleLookUp("TTInI_Value", "TTInI", "TTInI_Config = 'User_Code' AND TTInI_ComputerID = " + clsTTApp.GetAppController.str_FixStringForSQL(clsTTApp.GetAppController.cUser.GetUserComputerID));

                if (!string.IsNullOrEmpty(strUser_Code))
                {
                    Int32.TryParse(clsTTSQL.str_ADOSingleLookUp("TTU_NRI", "TTUser", "TTU_Active = 1 AND TTU_Code = " + clsTTApp.GetAppController.str_FixStringForSQL(strUser_Code)), out intUser_NRI);

                    clsTTApp.GetAppController.cUser.User_NRI = intUser_NRI;
                    clsTTApp.GetAppController.cUser.User_Code = strUser_Code;

                    if (string.IsNullOrEmpty(clsTTApp.GetAppController.cUser.User_Code) || clsTTApp.GetAppController.cUser.User_NRI <= 0)
                    {
                        blnValidReturn = Static_Classes.sclsMain.fbln_CreateNewUser(strUser_Code);
                    }
                }
                else
                {
                    resultAnswer = clsTTApp.GetAppController.ShowMessage(mintMSG_NewUser, MessageBoxButtons.YesNo);

                    if (resultAnswer == System.Windows.Forms.DialogResult.No)
                    {
                        frmTTLogin frmTTLogin = new frmTTLogin();

                        frmTTLogin.ShowDialog(this);

                        //Si l'application n'est pas fermée apres ce Call, c'est que le user est authentifié
                        blnValidReturn = clsTTApp.GetAppController.cUser.bln_SaveIniConfiguration("APP", "User_Code", clsTTApp.GetAppController.cUser.User_Code);
                    }
                    else
                    {
                        blnValidReturn = Static_Classes.sclsMain.fbln_CreateNewUser(); 
                    }
                }

                if (string.IsNullOrEmpty(clsTTApp.GetAppController.cUser.User_Code) || clsTTApp.GetAppController.cUser.User_NRI <= 0)
                {
                    Application.Exit();
                }
                else
                {
                    lblStatus_User.Text += clsTTApp.GetAppController.cUser.User_Code;
                }       
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
            if (clsTTApp.GetAppController.SQLConnection != null) clsTTApp.GetAppController.SQLConnection.Close();
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
            int intItem_NRI = clsTTApp.GetAppController.cUser.User_NRI;

            frmUser.MdiParent = this;

            ((TT3LightDLL.Controls.IFormController)frmUser).GetFormController().ShowForm(this, sclsConstants.DML_Mode.UPDATE_MODE, ref intItem_NRI);
        }
    }
}
