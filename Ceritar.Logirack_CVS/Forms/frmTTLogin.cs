using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.Logirack_CVS.Forms
{
    public partial class frmTTLogin : Form
    {
        private int intNbTries = 1;

        public frmTTLogin()
        {
            InitializeComponent();
        }

        private void frmTTLogin_KeyDown(object sender, KeyEventArgs e)
        {
            string strUser_NRI = string.Empty;

            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(txtCode.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                strUser_NRI = TT3LightDLL.Classes.clsTTSQL.str_ADOSingleLookUp("TTU_NRI", "TTUser", "TTU_Active = 1 AND TTU_Code = " + clsTTApp.GetAppController.str_FixStringForSQL(txtCode.Text) + " AND TTU_Password = " + clsTTApp.GetAppController.str_FixStringForSQL(txtPassword.Text));

                if (!string.IsNullOrEmpty(strUser_NRI))
                {
                    clsTTApp.GetAppController.cUser.User_NRI = Int32.Parse(strUser_NRI);
                    clsTTApp.GetAppController.cUser.User_Code = txtCode.Text;

                    this.Close();
                }
                else if (intNbTries < 3)
                {
                    intNbTries++;
                    txtPassword.Text = string.Empty;
                }
                else
                {
                    this.Close();

                    Static_Classes.sclsMain.fbln_CreateNewUser();
                }
            }
            else if (e.KeyData == Keys.Escape)
            {
                Application.Exit();
            }
        }
    }
}
