using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ceritar.Logirack_CVS.Static_Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace Ceritar.Logirack_CVS.Forms
{
    /// <summary>
    /// Cette classe représente la fenêtre principale de l'application via laquelle tous les menus sont accessibles.
    /// </summary>
    public partial class mdiGeneral : Form
    {
        [DllImport("Advapi32.dll", EntryPoint = "GetUserName", ExactSpelling = false, SetLastError = true)]
        static extern bool GetUserName([MarshalAs(UnmanagedType.LPArray)] byte[] lpBuffer, [MarshalAs(UnmanagedType.LPArray)] Int32[] nSize);

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
            bool blnCeritUpLaunched = false;
            int intAppChecked = 0;
            string strCommand = string.Join(" ", Environment.GetCommandLineArgs().Skip(1).ToArray());

            if (strCommand != string.Empty)
            {
                intAppChecked = Strings.InStr(1, strCommand, "AppChecked");
            }
            else
            {
                //Do nothing, c'est le CeritUp qui a relancé l'application
            }

            if (intAppChecked == 0) pfblnCeritUp_CheckForUpdate(ref blnCeritUpLaunched);

            if (!blnCeritUpLaunched)
            {
                clsTTApp.Instanciate(this);

                lblStatus_BD.Text += clsTTApp.GetAppController.SQLConnection.Database;

                Application.ApplicationExit += new EventHandler(ApplicationExit);

                if (!IsUserAdministrator())
                {
                    clsTTApp.GetAppController.ShowMessage(mintMSG_AdminRightWarning);
                }

                blnLoadSuccess = pfblnAutoLogInUser();
            }       
            else
            {
                //Do nothing, close application
            }            

            if (!blnLoadSuccess || blnCeritUpLaunched) Application.Exit();

            sclsGenList.ShowGenList(sclsGenList.GeneralLists_ID.VERSION_REVISION_LIST_NRI);
        }

        private bool pfblnCeritUp_CheckForUpdate(ref bool rblnCeritUpLaunched)
        {
            bool blnValidReturn = true;
            byte[] str = new byte[256];
            Int32[] len = new Int32[1];
            len[0] = 256;
            System.Diagnostics.Process[] lstCVSProcessRunning;
            string strUserName = string.Empty;

            System.IO.FileInfo strCurrentExeName = new System.IO.FileInfo(System.Windows.Forms.Application.ExecutablePath);

            rblnCeritUpLaunched = false;

            //Regarde si on a CeritUP dans le repertoire
            if (Microsoft.VisualBasic.FileSystem.Dir(System.IO.Path.Combine(System.IO.Directory.GetParent(Application.ExecutablePath).FullName, "CeritUP.exe")) != string.Empty)
            {
                //On va chercher le username et on enlève les espaces superflus          
                GetUserName(str, len);
                strUserName = System.Text.Encoding.ASCII.GetString(str).TrimEnd('\0');

                //Regarde le nombre de process d'ouvert de Logirack. Si on en a plus d'un on cancelle la maj
                var vCVSProcessRunning = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(strCurrentExeName.Name)).Where(p => p.StartInfo.UserName == strUserName).ToArray();
                lstCVSProcessRunning = vCVSProcessRunning;

                if (lstCVSProcessRunning.Length <= 1)
                {
                    rblnCeritUpLaunched = true;
                    Microsoft.VisualBasic.Interaction.Shell(System.IO.Path.Combine(strCurrentExeName.DirectoryName, "CeritUP.exe ") +  System.IO.Path.GetFileNameWithoutExtension(strCurrentExeName.Name), Microsoft.VisualBasic.AppWinStyle.NormalFocus);
                }
                else
                {
                    //do nothing
                }
            }
            else
            {
                //do nothing: pas de ceritup, pas besoin de passer ici
            }

            return blnValidReturn;
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
                    lblStatus_User.Text = clsTTApp.GetAppController.cUser.User_Code;
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

            clsTTForms.GetTTForms.GenericList_Count = 0;
        }

        private void mnuUser_Click(object sender, EventArgs e)
        {
            frmUser frmUser = new frmUser();
            int intItem_NRI = clsTTApp.GetAppController.cUser.User_NRI;

            frmUser.MdiParent = this;

            ((TT3LightDLL.Controls.IFormController)frmUser).GetFormController().ShowForm(this, sclsConstants.DML_Mode.UPDATE_MODE, ref intItem_NRI);
        }

        private void mnuLogOut_Click(object sender, EventArgs e)
        {
            mnuCloseAllWindows_Click(this, new EventArgs());
            
            foreach (ToolStripMenuItem mnuItem in mnuMain.Items)
            {
                if (mnuItem.Name != mnuFile.Name)
                {
                    mnuItem.Enabled = false;
                }
                else
                {
                    mnuLogIn.Visible = true;
                    mnuLogOut.Visible = false;
                }
            }
        }

        private void mnuLogIn_Click(object sender, EventArgs e)
        {
            frmTTLogin frmTTLogin = new frmTTLogin();

            frmTTLogin.ShowDialog(this);

            //Si l'application n'est pas fermée apres ce Call, c'est que le user est authentifié
            clsTTApp.GetAppController.cUser.bln_SaveIniConfiguration("APP", "User_Code", clsTTApp.GetAppController.cUser.User_Code);

            if (string.IsNullOrEmpty(clsTTApp.GetAppController.cUser.User_Code) || clsTTApp.GetAppController.cUser.User_NRI <= 0)
            {
                Application.Exit();
            }
            else
            {
                lblStatus_User.Text = clsTTApp.GetAppController.cUser.User_Code;
            }

            foreach (ToolStripMenuItem mnuItem in mnuMain.Items)
            {
                mnuItem.Enabled = true;

            }

            mnuLogIn.Visible = false;
            mnuLogOut.Visible = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frmAbout = new frmAbout();

            frmAbout.Show();
        }

        private void mnuConfigurations_Click(object sender, EventArgs e)
        {
            frmConfigurations frmConfig = new frmConfigurations();

            frmConfig.Show();
        }

        private void mnuGenererTTApp_Click(object sender, EventArgs e)
        {
            frmGenererTTApp frmGenererTTApp = new frmGenererTTApp();
            int intItem_NRI = 0;
            frmGenererTTApp.MdiParent = this;

            ((TT3LightDLL.Controls.IFormController)frmGenererTTApp).GetFormController().ShowForm(this, sclsConstants.DML_Mode.UPDATE_MODE, ref intItem_NRI);
        }
    }
}
