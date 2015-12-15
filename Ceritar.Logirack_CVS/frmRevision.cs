using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Ceritar.CVS.Controllers;
using Ceritar.TT3LightDLL.Controls;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;

namespace Ceritar.Logirack_CVS
{
    public partial class frmRevision : Form, IFormController, Ceritar.CVS.Controllers.Interfaces.IRevision
    {
        //Controller
        private ctr_Revision mcCtrRevision;

        //Columns grdRevModifs
        private const short mintGrdRevMod_Action_col = 1;
        private const short mintGrdRevMod_RevM_NRI_col = 2;
        private const short mintGrdRevMod_RevM_TS_col = 3;
        private const short mintGrdRevMod_RevM_ChangeDesc_col = 4;

        //Classes
        private clsC1FlexGridWrapper mcGrdRevModifs;

        //Public working variables
        public int mintVersion_NRI;

        //Working Variables     
        private string mstrCeritarApplication_Name;
        private ushort mintRevision_TS;


        public frmRevision()
        {
            InitializeComponent();

            mcCtrRevision = new ctr_Revision(this);

            mcGrdRevModifs = new clsC1FlexGridWrapper();       
        }


#region "Interfaces functions"

        int CVS.Controllers.Interfaces.IRevision.GetCeritarApplication_NRI()
        {
            throw new NotImplementedException();
        }

        string CVS.Controllers.Interfaces.IRevision.GetCeritarApplication_Name()
        {
            return mstrCeritarApplication_Name;
        }

        string CVS.Controllers.Interfaces.IRevision.GetCompiledBy()
        {
            throw new NotImplementedException();
        }

        string CVS.Controllers.Interfaces.IRevision.GetCreationDate()
        {
            throw new NotImplementedException();
        }

        sclsConstants.DML_Mode CVS.Controllers.Interfaces.IRevision.GetDML_Action()
        {
            throw new NotImplementedException();
        }

        string CVS.Controllers.Interfaces.IRevision.GetLocation_Release()
        {
            return txtReleasePath.Text;
        }

        string CVS.Controllers.Interfaces.IRevision.GetLocation_Scripts()
        {
            return txtScriptsPath.Text;
        }

        ushort CVS.Controllers.Interfaces.IRevision.GetRevisionNo()
        {
            return UInt16.Parse(txtRevisionNo.Text);
        }

        int CVS.Controllers.Interfaces.IRevision.GetRevision_NRI()
        {
            throw new NotImplementedException();
        }

        int CVS.Controllers.Interfaces.IRevision.GetRevision_TS()
        {
            throw new NotImplementedException();
        }

        int CVS.Controllers.Interfaces.IRevision.GetTemplateSource_NRI()
        {
            throw new NotImplementedException();
        }

        ushort CVS.Controllers.Interfaces.IRevision.GetVersionNo()
        {
            return UInt16.Parse(txtVersionNo.Text);
        }

        ctlFormController IFormController.GetFormController()
        {
            return formController;
        }

#endregion


#region "Functions"

        private bool pfblnData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;

            try
            {
                sqlRecord = clsSQL.ADOSelect(mcCtrRevision.strGetDataLoad_SQL(formController.Item_NRI));

                if (sqlRecord.Read())
                {
                    UInt16.TryParse(sqlRecord["Rev_TS"].ToString(), out mintRevision_TS);

                    //txtCompiledBy.Text = sqlRecord["Ver_CompiledBy"].ToString();
                    txtVersionNo.Text = sqlRecord["Rev_No"].ToString();

                    //dtpCreation.Value = DateTime.Parse(sqlRecord["Ver_DtCreation"].ToString());

                    cboClients.SelectedValue = Int32.Parse(sqlRecord["CeC_NRI"].ToString());
                    cboTemplates.SelectedValue = Int32.Parse(sqlRecord["Tpl_NRI"].ToString());

                    blnValidReturn = true;
                }

                dtpCreation.CustomFormat = clsApp.GetAppController.str_GetServerDateTimeFormat;

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

        private bool pfblnNewVersionData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;

            try
            {
                sqlRecord = clsSQL.ADOSelect(mcCtrRevision.strGetData_NewVersion(mintVersion_NRI));

                if (sqlRecord.Read())
                {
                    txtRevisionNo.Text = sqlRecord["NewRevisionNo"].ToString();
                    txtVersionNo.Text = sqlRecord["Ver_No"].ToString();
                    mstrCeritarApplication_Name = sqlRecord["CeA_Name"].ToString();

                    blnValidReturn = true;
                }

                dtpCreation.CustomFormat = clsApp.GetAppController.str_GetServerDateTimeFormat;

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

        private bool pfblnGrdRevModifs_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdRevModifs.bln_FillData(mcCtrRevision.strGetListe_RevisionModifications_SQL(formController.Item_NRI));
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        private void ShowFolderBrowserDialog(ref TextBox txtAffected)
        {
            DialogResult dialogResult;

            if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
            {
                folderBrowserDialog.ShowNewFolderButton = false;

                if (txtAffected.Name == txtReleasePath.Name && !string.IsNullOrEmpty(txtReleasePath.Text))
                {
                    folderBrowserDialog.SelectedPath = txtReleasePath.Text;
                }
                else
                {
                    folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                }

                dialogResult = folderBrowserDialog.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtAffected.Text = folderBrowserDialog.SelectedPath;

                    formController.ChangeMade = true;
                }
            }
        }

#endregion


        private void formController_LoadData(LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;

            if (!mcGrdRevModifs.bln_Init(ref grdRevModifs, ref btnGrdRevAdd, ref btnGrdRevDel))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrRevision.strGetTemplates_SQL(), "Tpl_NRI", "Tpl_Name", false, ref cboTemplates))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrRevision.strGetClients_SQL(), "CeC_NRI", "CeC_Name", true, ref cboClients))
            { }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
            {
                cboTemplates.SelectedIndex = (cboTemplates.Items.Count > 0 ? 0 : -1);

                blnValidReturn = pfblnNewVersionData_Load();
            }
            else if (!pfblnData_Load())
            { }
            else if (!pfblnGrdRevModifs_Load())
            { }
            else
            {
                blnValidReturn = true;
            }

            if (!blnValidReturn) this.Close();
        }

        private void formController_ValidateForm(ValidateFormEventArgs eventArgs)
        {

        }

        private void formController_SaveData(SaveDataEventArgs eventArgs)
        {

        }

        private void grdRevModifs_DoubleClick(object sender, EventArgs e)
        {
            if (grdRevModifs.Rows.Count > 1 && grdRevModifs.Row > 0)
            {
                switch (grdRevModifs.Col)
                {
                    case mintGrdRevMod_RevM_ChangeDesc_col:

                        grdRevModifs.StartEditing();
                        break;
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            mcCtrRevision.blnBuildRevisionHierarchy((int)cboTemplates.SelectedValue);
        }

        private void btnSelectScriptsFolderPath_Click(object sender, EventArgs e)
        {
            ShowFolderBrowserDialog(ref txtScriptsPath);
        }



        
    }
}
