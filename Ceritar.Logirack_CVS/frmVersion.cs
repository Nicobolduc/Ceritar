﻿using System;
using System.Data;
using C1.Win.C1FlexGrid;
using System.Data.SqlClient;
using System.Windows.Forms;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Controls;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.CVS.Controllers.Interfaces;
using Ceritar.CVS.Controllers;
using System.Collections.Generic;


namespace Ceritar.Logirack_CVS
{
    public partial class frmVersion : Form, IFormController, IVersion
    {
        //Controller
        private ctr_Version mcCtrVersion;

        //Columns grdClients
        private const short mintGrdClients_Action_col = 1;
        private const short mintGrdClients_CAV_NRI_col = 2;
        private const short mintGrdClients_CAV_TS_col = 3;
        private const short mintGrdClients_CeC_NRI_col = 4;
        private const short mintGrdClients_CeC_Name_col = 5;
        private const short mintGrdClients_Installed_col = 6;
        private const short mintGrdClients_IsCurrentVersion_col = 7;
        private const short mintGrdClients_License_col = 8;

        //Columns grdRev
        private const short mintGrdRev_Rev_NRI_col = 1;
        private const short mintGrdRev_Rev_TS_col = 2;
        private const short mintGrdRev_Description_col = 3;
        private const short mintGrdRev_CreationDate_col = 4;

        //Classes
        public clsC1FlexGridWrapper mcGrdClients;
        public clsC1FlexGridWrapper mcGrdRevisions;
        private Ceritar.CVS.clsActionResults mcActionResults;
        List<structClientAppVersion> lstClient_NRI;

        //Working variables
        private ushort mintVersion_TS;


        public frmVersion()
        {
            InitializeComponent();

            mcCtrVersion = new ctr_Version((IVersion)this);

            mcGrdClients = new clsC1FlexGridWrapper();
            mcGrdClients.SetGridDisplay += mcGrdClients_SetGridDisplay;
            mcGrdClients.ValidateGridData += mcGrdClients_ValidateGridData;
            mcGrdClients.AfterRowAdd += mcGrdClients_AfterRowAdd;

            mcGrdRevisions = new clsC1FlexGridWrapper();
            mcGrdRevisions.SetGridDisplay +=mcGrdRevisions_SetGridDisplay;

            dtpCreation.CustomFormat = clsApp.GetAppController.str_GetServerDateTimeFormat;
        }
       

#region "Interfaces functions"

        ctlFormController IFormController.GetFormController()
        {
            return this.formController;
        }

        sclsConstants.DML_Mode IVersion.GetDML_Action()
        {
            return formController.FormMode;
        }

        int IVersion.GetCeritarApplication_NRI()
        {
            return (int)cboApplications.SelectedValue;
        }

        string IVersion.GetCeritarApplication_Name()
        {
            return (cboApplications.SelectedIndex >= 0 ? cboApplications.GetItemText(cboApplications.SelectedItem) : string.Empty);
        }

        string IVersion.GetCompiledBy()
        {
            return txtCompiledBy.Text;
        }

        string IVersion.GetLocation_APP_CHANGEMENT()
        {
            return (string.IsNullOrEmpty(txtExcelAppChangePath.Text) ? txtWordAppChangePath.Text : txtExcelAppChangePath.Text);
        }

        string IVersion.GetLocation_Release()
        {
            return txtReleasePath.Text;
        }

        string IVersion.GetLocation_TTApp()
        {
            return txtTTAppPath.Text;
        }

        int IVersion.GetTemplateSource_NRI()
        {
            return (int)cboTemplates.SelectedValue;
        }

        ushort IVersion.GetVersionNo()
        {
            ushort intReturnValue = 0;

            UInt16.TryParse(txtVersionNo.Text, out intReturnValue);

            return  intReturnValue;
        }

        int IVersion.GetVersion_NRI()
        {
            return formController.Item_NRI;
        }

        int IVersion.GetVersion_TS()
        {
            return mintVersion_TS;
        }

        string IVersion.GetCreationDate()
        {
            return dtpCreation.Value.ToString();
        }

        List<structClientAppVersion> IVersion.GetClientsList()
        {
            List<structClientAppVersion> lstClient_NRI = new List<structClientAppVersion>();
            structClientAppVersion structCAV;

            for (int intRowIndex = 1; intRowIndex < grdClients.Rows.Count; intRowIndex++)
            {
                structCAV = new structClientAppVersion();

                structCAV.Action = clsApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(grdClients[intRowIndex, mintGrdClients_Action_col]);
                structCAV.blnInstalled = Convert.ToBoolean(grdClients[intRowIndex, mintGrdClients_Installed_col]);
                structCAV.blnIsCurrentVersion = Convert.ToBoolean(grdClients[intRowIndex, mintGrdClients_IsCurrentVersion_col]);
                structCAV.strCeritarClient_Name = mcGrdClients[intRowIndex, mintGrdClients_CeC_Name_col];
                structCAV.intCeritarClient_NRI = Int32.Parse(mcGrdClients[intRowIndex, mintGrdClients_CeC_NRI_col]);
                structCAV.intClientAppVersion_NRI = Int32.Parse(mcGrdClients[intRowIndex, mintGrdClients_CAV_NRI_col]);
                structCAV.intClientAppVersion_TS = Int32.Parse(mcGrdClients[intRowIndex, mintGrdClients_CAV_TS_col]);
                structCAV.strLicense = grdClients[intRowIndex, mintGrdClients_License_col].ToString();

                lstClient_NRI.Add(structCAV);
            }

            return lstClient_NRI;
        }

#endregion
        
        
#region "Functions"

        private bool pfblnGrdClients_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdClients.bln_FillData(mcCtrVersion.strGetListe_Clients_SQL(formController.Item_NRI));
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        private bool pfblnGrdRevisions_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdRevisions.bln_FillData(mcCtrVersion.strGetListe_Revisions_SQL(formController.Item_NRI));
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        private bool pfblnData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;

            try
            {
                sqlRecord = clsSQL.ADOSelect(mcCtrVersion.strGetDataLoad_SQL(formController.Item_NRI));

                if (sqlRecord.Read())
                {
                    UInt16.TryParse(sqlRecord["Ver_TS"].ToString(), out mintVersion_TS);

                    txtCompiledBy.Text = sqlRecord["Ver_CompiledBy"].ToString();
                    txtVersionNo.Text = sqlRecord["Ver_No"].ToString();

                    //dtpCreation.Value = DateTime.Parse(sqlRecord["Ver_DtCreation"].ToString());

                    cboApplications.SelectedValue = Int32.Parse(sqlRecord["CeA_NRI"].ToString());
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

        private void ShowOpenFileDialog(string strTypeFilters, ref TextBox txtAffected)
        {
            DialogResult dialogResult;

            if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
            {
                openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = strTypeFilters;

                dialogResult = openFileDialog.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtAffected.Text = openFileDialog.FileName;

                    formController.ChangeMade = true;
                }
            }
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


        void mcGrdClients_SetGridDisplay()
        {
            grdClients.Cols[mintGrdClients_CeC_Name_col].Width = 220;
            grdClients.Cols[mintGrdClients_Installed_col].Width = 55;

            grdClients.Cols[mintGrdClients_IsCurrentVersion_col].DataType = typeof(bool);
            grdClients.Cols[mintGrdClients_Installed_col].DataType = typeof(bool);

            if (grdClients.Rows.Count > 1)
            {
                cboClients.Visible = true;
            }

            grdClients.Cols[mintGrdClients_CeC_Name_col].Style = grdClients.Styles.Normal;
            grdClients.Cols[mintGrdClients_CeC_Name_col].Style.Editor = cboClients;
        }

        void mcGrdRevisions_SetGridDisplay()
        {
            grdRevisions.Cols[mintGrdRev_Description_col].Width = 220;

        }

        private void formController_LoadData(LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;
            Button btnPlaceHolder = null;

            if (!mcGrdClients.bln_Init(ref grdClients, ref btnGrdClientsAdd, ref btnGrdClientsDel))
            { }
            if (!mcGrdRevisions.bln_Init(ref grdRevisions, ref btnPlaceHolder, ref btnPlaceHolder))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetApplications_SQL(), "CeA_NRI", "CeA_Name", false, ref cboApplications))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetTemplates_SQL(), "Tpl_NRI", "Tpl_Name", false, ref cboTemplates))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetClients_SQL(), "CeC_NRI", "Cec_Name", false, ref cboClients))
            { }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
            {
                cboTemplates.SelectedIndex = (cboTemplates.Items.Count > 0 ? 0 : -1);

                blnValidReturn = true;
            }
            else if (!pfblnData_Load())
            { }
            else if (!pfblnGrdClients_Load())
            { }
            else if (!pfblnGrdRevisions_Load())
            { }
            else
            {
                blnValidReturn = true;
            }
      
            if (!blnValidReturn) this.Close();
        }

        private void formController_ValidateForm(ValidateFormEventArgs eventArgs)
        {
            eventArgs.IsValid = false;

            if (!mcGrdClients.blnValidateGridData())
            {
                clsApp.GetAppController.ShowMessage((int)sclsConstants.Validation_Message.MANDATORY_GRID, MessageBoxButtons.OK, groupBox2.Text);
            }
            else
            {
                mcActionResults = mcCtrVersion.Validate();

                if (!mcActionResults.IsValid)
                {
                    switch ((ctr_Version.ErrorCode_Ver)mcActionResults.GetErrorCode)
                    {
                        case ctr_Version.ErrorCode_Ver.APP_CHANGEMENT_MANDATORY:

                            txtExcelAppChangePath.Focus();
                            break;

                        case ctr_Version.ErrorCode_Ver.CERITAR_APP_MANDATORY:

                            cboApplications.Focus();
                            cboApplications.DroppedDown = true;
                            break;

                        case ctr_Version.ErrorCode_Ver.CLIENTS_LIST_MANDATORY:

                            btnGrdClientsAdd.Focus();
                            break;

                        case ctr_Version.ErrorCode_Ver.COMPILED_BY_MANDATORY:

                            txtCompiledBy.Focus();
                            break;

                        case ctr_Version.ErrorCode_Ver.RELEASE_MANDATORY:

                            txtReleasePath.Focus();
                            break;

                        case ctr_Version.ErrorCode_Ver.TEMPLATE_MANDATORY:

                            cboTemplates.Focus();
                            cboTemplates.DroppedDown = true;
                            break;

                        case ctr_Version.ErrorCode_Ver.TTAPP_MANDATORY:

                            txtTTAppPath.Focus();
                            break;

                        case ctr_Version.ErrorCode_Ver.VERSION_NO_MANDATORY:
                            
                            txtVersionNo.Focus();
                            break;

                        case ctr_Version.ErrorCode_Ver.VERSION_NO_UNIQUE_AND_BIGGER_PREVIOUS:
                            
                            txtVersionNo.Focus();
                            txtVersionNo.SelectAll();
                            break;
                    }

                    clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI);
                }

                eventArgs.IsValid = mcActionResults.IsValid;
            }
        }

        private void formController_SaveData(SaveDataEventArgs eventArgs)
        {
            mcActionResults = mcCtrVersion.Save();

            if (!mcActionResults.IsValid)
            {
                clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI);
            }

            eventArgs.SaveSuccessful = mcActionResults.IsValid;
        }

        private void btnReplaceAppChangeDOC_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog("Word Documents (.docx)|*.docx", ref txtWordAppChangePath);
        }

        private void btnReplaceAppChangeXLS_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog("Excel|*.xls|Excel 2010|*.xlsx", ref txtExcelAppChangePath); 
        }

        private void btnReplaceTTApp_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog("Access files (*.mdb)|*.mdb", ref txtTTAppPath);
        }

        private void btnReplaceExecutable_Click(object sender, EventArgs e)
        {
            ShowFolderBrowserDialog(ref txtReleasePath);
        }

        private void grdClients_DoubleClick(object sender, EventArgs e)
        {
            if (grdClients.Rows.Count > 1 & grdClients.Row > 0)
            {
                switch (grdClients.Col)
                {
                    case mintGrdClients_CeC_Name_col:

                        grdClients.StartEditing();

                        break;

                    case mintGrdClients_Installed_col:

                        mcGrdClients[grdClients.Row, mintGrdClients_Installed_col] = (mcGrdClients[grdClients.Row, mintGrdClients_Installed_col] == "0"? "1" : "0");

                        break;

                    case mintGrdClients_IsCurrentVersion_col:

                        mcGrdClients[grdClients.Row, mintGrdClients_IsCurrentVersion_col] = (mcGrdClients[grdClients.Row, mintGrdClients_IsCurrentVersion_col] == "0" ? "1" : "0");

                        if (mcGrdClients[grdClients.Row, mintGrdClients_IsCurrentVersion_col] == "1")
                        {

                            mcGrdClients[grdClients.Row, mintGrdClients_Installed_col]  = "1";
                        }

                        break;
                }
            }
        }

        private void cboApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void cboGabarits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void txtCompiledBy_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void txtVersionNo_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void cboClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading & cboClients.SelectedIndex > -1)
            {
                grdClients[grdClients.Row, mintGrdClients_CeC_Name_col] = cboClients.SelectedText;
                grdClients[grdClients.Row, mintGrdClients_CeC_NRI_col] = cboClients.SelectedValue;

                formController.ChangeMade = true;
            }
        }

        void mcGrdClients_AfterRowAdd()
        {
            grdClients[grdClients.Row, mintGrdClients_CAV_NRI_col] = 0;
            grdClients[grdClients.Row, mintGrdClients_CAV_TS_col] = 0;
            grdClients[grdClients.Row, mintGrdClients_Installed_col] = 0;
            grdClients[grdClients.Row, mintGrdClients_IsCurrentVersion_col] = 0;
            grdClients[grdClients.Row, mintGrdClients_License_col] = "";
        }

        void mcGrdClients_ValidateGridData(ValidateGridDataEventArgs eventArgs)
        {
            eventArgs.IsValid = false;

            structClientAppVersion structCAV;

            for (int intRowIndex = 1; intRowIndex < grdClients.Rows.Count; intRowIndex++)
            {
                eventArgs.IsValid = false;

                structCAV = new structClientAppVersion();
                
                structCAV.Action = clsApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(grdClients[intRowIndex, mintGrdClients_Action_col]);
                Int32.TryParse(mcGrdClients[intRowIndex, mintGrdClients_CeC_NRI_col], out structCAV.intCeritarClient_NRI);
                structCAV.intClientAppVersion_NRI = Int32.Parse(mcGrdClients[intRowIndex, mintGrdClients_CAV_NRI_col]);
                structCAV.intClientAppVersion_TS = Int32.Parse(mcGrdClients[intRowIndex, mintGrdClients_CAV_TS_col]);
                structCAV.strLicense = grdClients[intRowIndex, mintGrdClients_License_col].ToString();
                structCAV.blnInstalled = Convert.ToBoolean(grdClients[intRowIndex, mintGrdClients_Installed_col]);
                structCAV.blnIsCurrentVersion = Convert.ToBoolean(grdClients[intRowIndex, mintGrdClients_IsCurrentVersion_col]);

                mcActionResults = mcCtrVersion.Validate_Client(structCAV);

                if (!mcActionResults.IsValid)
                {
                    clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI);

                    switch ((ctr_Version.ErrorCode_Ver)mcActionResults.GetErrorCode)
                    {
                        case ctr_Version.ErrorCode_Ver.CLIENT_NAME_MANDATORY:

                            ((ComboBox)grdClients.GetCellRange(1, mintGrdClients_CeC_Name_col).Style.Editor).DroppedDown = true;

                            break;
                    }
                }
                else
                {
                    eventArgs.IsValid = true;
                }
            }
        }

        private void formController_SetReadRights()
        {
            switch (formController.FormMode)
            {
                case sclsConstants.DML_Mode.INSERT_MODE:

                    btnGenerate.Enabled = false;

                    break;

                case sclsConstants.DML_Mode.UPDATE_MODE:

                    cboApplications.Enabled = false;
                    cboTemplates.Enabled = false;
                    
                    txtVersionNo.ReadOnly = true;

                    break;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            mcCtrVersion.blnBuildVersionHierarchy((int)cboTemplates.SelectedValue);

            mcActionResults = mcCtrVersion.GetActionResult;

            if (!mcActionResults.IsValid)
            {
                clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI, MessageBoxButtons.OK, mcActionResults.GetLstParams);
            }

            this.Cursor = Cursors.Default;
        }

        private void btnGrdRevAdd_Click(object sender, EventArgs e)
        {
            if (formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
            {
                int intNewItem_NRI = 0;
                frmRevision frmVersion = new frmRevision();

                frmVersion.mintVersion_NRI = formController.Item_NRI;
                frmVersion.formController.ShowForm(sclsConstants.DML_Mode.INSERT_MODE, ref intNewItem_NRI, true);

                pfblnGrdRevisions_Load();
            }
        }

        private void btnGrdRevDel_Click(object sender, EventArgs e)
        {
            if (formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
            {
                int intItem_NRI = (int)grdRevisions[grdRevisions.Row, mintGrdRev_Rev_NRI_col];
                frmRevision frmVersion = new frmRevision();

                frmVersion.formController.ShowForm(sclsConstants.DML_Mode.DELETE_MODE, ref intItem_NRI, true);

                pfblnGrdRevisions_Load();
            }
        }

        private void txtVersionNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Microsoft.VisualBasic.Information.IsNumeric(txtVersionNo.Text))
            {
                clsApp.GetAppController.ShowMessage((int)sclsConstants.Validation_Message.NUMERIC_VALUE);
                e.Cancel = true;
                txtVersionNo.SelectAll();
            }
        }


    }
}
