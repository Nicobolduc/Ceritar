using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.CVS.Controllers;
using System;
using Ceritar.CVS.Controllers.Interfaces;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Controls;

namespace Ceritar.Logirack_CVS.Forms
{
    public partial class frmUser : Form, IUser, Ceritar.TT3LightDLL.Controls.IFormController
    {
        //Controller
        private ctr_User mcCtrUser;

        //Classes
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Working variables
        private ushort mintUser_TS;

        //Public variables
        public string mstrUser_Code = string.Empty;


        public frmUser()
        {
            InitializeComponent();

            mcCtrUser = new ctr_User((IUser)this);
        }


#region "Interfaces functions"

        ctlFormController IFormController.GetFormController()
        {
            return formController;
        }

        string IUser.GetCode()
        {
            return txtCode.Text;
        }

        sclsConstants.DML_Mode IUser.GetDML_Mode()
        {
            return formController.FormMode;
        }

        string IUser.GetEMail()
        {
            return txtEmail.Text;
        }

        string IUser.GetFirstName()
        {
            return txtFirstName.Text;
        }

        string IUser.GetLastName()
        {
            return txtLastName.Text;
        }

        string IUser.GetPassword()
        {
            return txtPassword.Text;
        }

        int IUser.GetUser_TS()
        {
            return mintUser_TS;
        }

        int IUser.GetUser_NRI()
        {
            return formController.Item_NRI;
        }

        bool IUser.IsActive()
        {
            return chkActive.Checked;
        }

        int IUser.GetCeritarApp_NRI_Default()
        {
            return (int)cboApplications.SelectedValue;
        }

#endregion


#region "Functions"

        private bool pfblnData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;

            try
            {
                sqlRecord = clsTTSQL.ADOSelect(mcCtrUser.strGetDataLoad_SQL(formController.Item_NRI));

                if (sqlRecord.Read())
                {
                    UInt16.TryParse(sqlRecord["TTU_TS"].ToString(), out mintUser_TS);

                    txtCode.Text = sqlRecord["TTU_Code"].ToString();
                    txtFirstName.Text = sqlRecord["TTU_FirstName"].ToString();
                    txtLastName.Text = sqlRecord["TTU_LastName"].ToString();
                    txtPassword.Text = sqlRecord["TTU_Password"].ToString();
                    txtEmail.Text = sqlRecord["TTU_Email"].ToString();

                    chkActive.Checked = Convert.ToBoolean(sqlRecord["TTU_Active"].ToString());

                    if (sqlRecord["CeA_NRI_Default"] != System.DBNull.Value)
                        cboApplications.SelectedValue = Int32.Parse(sqlRecord["CeA_NRI_Default"].ToString());

                    blnValidReturn = true;
                }
                
                return blnValidReturn;
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (sqlRecord != null) sqlRecord.Dispose();
            }

            return blnValidReturn;
        }

#endregion


        private void formController_LoadData(LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;

            if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrUser.strGetApplications_SQL(), "CeA_NRI", "CeA_Name", true, ref cboApplications))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrUser.strGetAppLanguages_SQL(), "ApL_NRI", "ApL_Code", false, ref cboLanguages))
            { }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
            {
                txtCode.Text = mstrUser_Code;

                if (mstrUser_Code != string.Empty) txtPassword.Focus();

                blnValidReturn = true;
            }
            else if (!pfblnData_Load())
            { }
            else
            {
                blnValidReturn = true;
            }

            if (blnValidReturn)
            {
                cboLanguages.SelectedValue = Int32.Parse(clsTTApp.GetAppController.cUser.UserLanguage.ToString());
            }

            if (!blnValidReturn) this.Close();
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void chkActive_CheckStateChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void formController_ValidateForm(ValidateFormEventArgs eventArgs)
        {
            mcActionResults = mcCtrUser.Validate();

            if (!mcActionResults.IsValid)
            {
                clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI);

                switch ((ctr_User.ErrorCode_TTU)mcActionResults.GetErrorCode)
                {
                    case ctr_User.ErrorCode_TTU.CODE_MANDATORY:
                    case ctr_User.ErrorCode_TTU.CODE_UNIQUE:

                        txtCode.Focus();
                        txtCode.SelectAll();

                        break;

                    case ctr_User.ErrorCode_TTU.EMAIL_INVALID:

                        txtEmail.Focus();
                        txtEmail.SelectAll();

                        break;

                    case ctr_User.ErrorCode_TTU.FIRST_NAME_MANDATORY:

                        txtFirstName.Focus();
                        txtFirstName.SelectAll();

                        break;

                    case ctr_User.ErrorCode_TTU.LAST_NAME_MANDATORY:

                        txtLastName.Focus();
                        txtLastName.SelectAll();

                        break;
                }
            }

            eventArgs.IsValid = mcActionResults.IsValid;
        }

        private void formController_SaveData(SaveDataEventArgs eventArgs)
        {
            mcActionResults = mcCtrUser.Save();

            if (!mcActionResults.IsValid)
            {
                clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI);
            }

            if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE) formController.Item_NRI = mcActionResults.GetNewItem_NRI;

            if (mcActionResults.IsValid)
            {
                clsTTApp.GetAppController.cUser.User_NRI = formController.Item_NRI;
                clsTTApp.GetAppController.cUser.User_Code = txtCode.Text;
            }

            eventArgs.SaveSuccessful = mcActionResults.IsValid;
        }

        private void cboApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void formController_SetReadRights()
        {
            switch (formController.FormMode)
            {
                case sclsConstants.DML_Mode.UPDATE_MODE:

                    txtPassword.PasswordChar = '*';

                    break;
            }
        }

    }
}
