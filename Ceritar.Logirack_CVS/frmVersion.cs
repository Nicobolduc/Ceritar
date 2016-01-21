using System;
using System.IO;
using System.Data;
using Microsoft.VisualBasic;
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
    /// <summary>
    /// Cette classe contient les fonctions et évènements de la vue permettant de définir les versions d'une application.
    /// </summary>
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
        private const short mintGrdClients_LocationReportExe_col = 8;
        private const short mintGrdClients_License_col = 9;
        private const short mintGrdClients_Selection_col = 10;

        //Columns grdRev
        private const short mintGrdRev_Rev_NRI_col = 1;
        private const short mintGrdRev_Rev_TS_col = 2;
        private const short mintGrdRev_Description_col = 3;
        private const short mintGrdRev_CreationDate_col = 4;

        //Tab pages
        private const short mintTab_Version = 0;
        private const short mintTab_Revision = 1;

        //Classes
        public clsC1FlexGridWrapper mcGrdClients;
        public clsC1FlexGridWrapper mcGrdRevisions;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Working variables
        private ushort mintVersion_TS;
        //private System.Collections.ArrayList _al = new System.Collections.ArrayList();

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
                Int32.TryParse(mcGrdClients[intRowIndex, mintGrdClients_CeC_NRI_col], out structCAV.intCeritarClient_NRI);
                Int32.TryParse(mcGrdClients[intRowIndex, mintGrdClients_CAV_NRI_col], out structCAV.intClientAppVersion_NRI);
                structCAV.intClientAppVersion_TS = Int32.Parse(mcGrdClients[intRowIndex, mintGrdClients_CAV_TS_col]);
                structCAV.strLicense = grdClients[intRowIndex, mintGrdClients_License_col].ToString();
                structCAV.strLocationReportExe = mcGrdClients[intRowIndex, mintGrdClients_LocationReportExe_col];

                lstClient_NRI.Add(structCAV);
            }

            return lstClient_NRI;
        }

        bool IVersion.GetIsDemo()
        {
            return chkDemoVersion.Checked;
        }

#endregion
        
        
#region "Functions"

        private bool pfblnGrdClients_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdClients.bln_FillData(mcCtrVersion.strGetListe_Clients_SQL(formController.Item_NRI));

                if (blnValidReturn)
                {
                    pfblnEnableDisable_btnGrdClientsDelete(1);
                }
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
            string strAppChangeLocation = string.Empty;

            try
            {
                sqlRecord = clsSQL.ADOSelect(mcCtrVersion.strGetDataLoad_SQL(formController.Item_NRI));

                if (sqlRecord.Read())
                {
                    UInt16.TryParse(sqlRecord["Ver_TS"].ToString(), out mintVersion_TS);

                    txtCompiledBy.Text = sqlRecord["Ver_CompiledBy"].ToString();
                    txtVersionNo.Text = sqlRecord["Ver_No"].ToString();

                    strAppChangeLocation = sqlRecord["Ver_AppChange_Location"].ToString();

                    if (strAppChangeLocation.Substring(strAppChangeLocation.Length - 4, 4) == "xlsx" || strAppChangeLocation.Substring(strAppChangeLocation.Length - 3, 3) == "xls")
                    {
                        txtExcelAppChangePath.Text = sqlRecord["Ver_AppChange_Location"].ToString();
                        txtWordAppChangePath.Text = string.Empty;
                    }
                    else if (strAppChangeLocation.Substring(strAppChangeLocation.Length - 4, 4) == "docx" || strAppChangeLocation.Substring(strAppChangeLocation.Length - 3, 3) == "doc")
                    {
                        txtWordAppChangePath.Text = sqlRecord["Ver_AppChange_Location"].ToString();
                        txtExcelAppChangePath.Text = string.Empty;
                    }

                    txtReleasePath.Text = sqlRecord["Ver_Release_Location"].ToString();
                    txtTTAppPath.Text = sqlRecord["Ver_CaptionsAndMenus_Location"].ToString();

                    //dtpCreation.Value = DateTime.Parse(sqlRecord["Ver_DtCreation"].ToString());

                    cboApplications.SelectedValue = Int32.Parse(sqlRecord["CeA_NRI"].ToString());
                    cboTemplates.SelectedValue = Int32.Parse(sqlRecord["Tpl_NRI"].ToString());

                    chkDemoVersion.Checked = Convert.ToBoolean(sqlRecord["Ver_IsDemo"]);

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

        private Button pfblnGetNewLocationReportButton()
        {
            Button btnLocationReport = new Button();

            try
            {
                btnLocationReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                btnLocationReport.Image = ((System.Drawing.Image)(Properties.Resources.ellipsis));
                btnLocationReport.UseVisualStyleBackColor = true;

                btnLocationReport.Click += btnReplaceReportExe_Click;
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return btnLocationReport;
        }

        private void ShowOpenFileDialog(string vstrExtensionsFilter, Object rControl, string vstrInitialDirectory = "", bool vblnShowFile = false)
        {
            DialogResult dialogResult;
            
            try
            {
                if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
                {
                    if (vblnShowFile && vstrInitialDirectory != "")
                    {
                        openFileDialog.FileName = vstrInitialDirectory;

                        vstrInitialDirectory = Path.GetDirectoryName(vstrInitialDirectory);
                    }
                    else
                    {
                        openFileDialog.FileName = string.Empty;

                        vstrInitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    }

                    openFileDialog.InitialDirectory = vstrInitialDirectory;
                    openFileDialog.Multiselect = false;
                    openFileDialog.Filter = vstrExtensionsFilter;

                    dialogResult = openFileDialog.ShowDialog();

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        if (rControl.GetType() == typeof(TextBox))
                        {
                            ((TextBox)rControl).Text = openFileDialog.FileName;
                        }

                        if (rControl.GetType() == typeof(C1FlexGrid))
                        {
                            ((C1FlexGrid)rControl)[grdClients.Row, mintGrdClients_LocationReportExe_col] = openFileDialog.FileName;
                            ((HostedCellControl)mcGrdClients.LstHostedCellControls[grdClients.Row - 1]).GetCellControl.BackColor = System.Drawing.Color.Yellow;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }         
        }

        private void ShowFolderBrowserDialog(ref TextBox rtxtAffected)
        {
            DialogResult dialogResult;

            try
            {
                if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
                {
                    folderBrowserDialog.ShowNewFolderButton = false;

                    if (!string.IsNullOrEmpty(rtxtAffected.Text))
                    {
                        folderBrowserDialog.SelectedPath = rtxtAffected.Text;
                    }
                    else
                    {
                        folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                    }
                    
                    dialogResult = folderBrowserDialog.ShowDialog();

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        rtxtAffected.Text = folderBrowserDialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        private bool pfblnEnableDisable_btnGrdClientsDelete(int vintCurrentRow)
        {
            bool blnValidReturn = false;

            try
            {
                if (mcGrdClients.bln_RowsSelValid())
                {
                    if (string.IsNullOrEmpty(mcGrdClients[vintCurrentRow, mintGrdClients_Action_col]) || mcGrdClients[vintCurrentRow, mintGrdClients_Action_col] == sclsConstants.DML_Mode.INSERT_MODE.ToString())
                    {
                        btnGrdClientsDel.Enabled = true;
                    }
                    else
                    {
                        btnGrdClientsDel.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        private bool pfblnExportInstallationKit()
        {
            bool blnValidReturn = false;
            TextBox txtTemp = new TextBox();

            try
            {
                ShowFolderBrowserDialog(ref txtTemp);



            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

#endregion


        void mcGrdClients_SetGridDisplay()
        {
            grdClients.Cols[mintGrdClients_CeC_Name_col].Width = 210;
            grdClients.Cols[mintGrdClients_Installed_col].Width = 48;
            grdClients.Cols[mintGrdClients_IsCurrentVersion_col].Width = 50;
            grdClients.Cols[mintGrdClients_LocationReportExe_col].Width = 42;
            grdClients.Cols[mintGrdClients_Selection_col].Width = 20;

            grdClients.Cols[mintGrdClients_IsCurrentVersion_col].DataType = typeof(bool);
            grdClients.Cols[mintGrdClients_Installed_col].DataType = typeof(bool);
            grdClients.Cols[mintGrdClients_Selection_col].DataType = typeof(bool);

            if (grdClients.Rows.Count > 1)
            {
                cboClients.Visible = true;

                for (int intRowIndex = 1; intRowIndex < grdClients.Rows.Count; intRowIndex++)
                {
                    mcGrdClients.LstHostedCellControls.Add(new HostedCellControl(grdClients, pfblnGetNewLocationReportButton(), intRowIndex, mintGrdClients_LocationReportExe_col));

                    if (!mcGrdClients.bln_CellIsEmpty(intRowIndex, mintGrdClients_LocationReportExe_col))
                    {
                        mcGrdClients.LstHostedCellControls[intRowIndex - 1].GetCellControl.BackColor = System.Drawing.Color.Yellow;
                    }
                }   
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

            mcGrdClients.LstHostedCellControls = new List<HostedCellControl>();

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

            mcActionResults = mcCtrVersion.Validate();

            if (!mcActionResults.IsValid)
            {
                clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI);

                switch ((ctr_Version.ErrorCode_Ver)mcActionResults.GetErrorCode)
                {
                    case ctr_Version.ErrorCode_Ver.APP_CHANGEMENT_MANDATORY:

                        btnReplaceAppChangeXLS.Focus();
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

                        btnReplaceExecutable.Focus();
                        break;

                    case ctr_Version.ErrorCode_Ver.TEMPLATE_MANDATORY:

                        cboTemplates.Focus();
                        cboTemplates.DroppedDown = true;
                        break;

                    case ctr_Version.ErrorCode_Ver.TTAPP_MANDATORY:

                        btnReplaceTTApp.Focus();
                        break;

                    case ctr_Version.ErrorCode_Ver.VERSION_NO_MANDATORY:
                            
                        txtVersionNo.Focus();
                        break;

                    case ctr_Version.ErrorCode_Ver.VERSION_NO_UNIQUE_AND_BIGGER_PREVIOUS:
                            
                        txtVersionNo.Focus();
                        txtVersionNo.SelectAll();
                        break;

                    case ctr_Version.ErrorCode_Ver.CLIENT_NAME_MANDATORY:

                        string strListOfClientSelected = string.Empty;

                        for (int intRowIdx = 1; intRowIdx < grdClients.Rows.Count; intRowIdx++)
                        {
                            strListOfClientSelected = strListOfClientSelected + (strListOfClientSelected == string.Empty ? mcGrdClients[intRowIdx, mintGrdClients_CeC_NRI_col] : "," + Conversion.Val(mcGrdClients[intRowIdx, mintGrdClients_CeC_NRI_col]));
                        }
                        
                        sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetClients_SQL(strListOfClientSelected), "CeC_NRI", "Cec_Name", false, ref cboClients);

                        grdClients.Row = mcActionResults.RowInError;
                        grdClients.StartEditing();

                        break;

                    case ctr_Version.ErrorCode_Ver.REPORT_MANDATORY:

                        ((HostedCellControl)mcGrdClients.LstHostedCellControls[mcActionResults.RowInError - 1]).GetCellControl.Focus();

                        break;

                    case ctr_Version.ErrorCode_Ver.DEMO_CANT_BE_INSTALLED:

                        chkDemoVersion.Focus();

                        break;
                }
            }

            eventArgs.IsValid = mcActionResults.IsValid;
        }

        private void formController_SaveData(SaveDataEventArgs eventArgs)
        {
            mcActionResults = mcCtrVersion.Save();

            if (!mcActionResults.IsValid)
            {
                clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI, MessageBoxButtons.OK, mcActionResults.GetLstParams);

                switch ((ctr_Version.ErrorCode_Ver)mcActionResults.GetErrorCode)
                {
                    case ctr_Version.ErrorCode_Ver.APP_CHANGEMENT_MANDATORY:

                        btnReplaceAppChangeXLS.Focus();

                        break;

                    case ctr_Version.ErrorCode_Ver.RELEASE_MANDATORY:

                        btnReplaceExecutable.Focus();

                        break;

                    case ctr_Version.ErrorCode_Ver.TTAPP_MANDATORY:

                        btnReplaceTTApp.Focus();

                        break;
                }
            }

            eventArgs.SaveSuccessful = mcActionResults.IsValid;
        }

        private void btnReplaceAppChangeDOC_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog("Word Documents (.docx)|*.docx", txtWordAppChangePath, txtWordAppChangePath.Text, true);

            if (!string.IsNullOrEmpty(txtWordAppChangePath.Text)) txtExcelAppChangePath.Text = string.Empty;
        }

        private void btnReplaceAppChangeXLS_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog("Excel|*.xls|Excel 2010|*.xlsx", txtExcelAppChangePath, txtExcelAppChangePath.Text, true);

            if (!string.IsNullOrEmpty(txtExcelAppChangePath.Text)) txtWordAppChangePath.Text = string.Empty;
        }

        private void btnReplaceTTApp_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog("Access files (*.mdb)|*.mdb", txtTTAppPath, (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE ? txtTTAppPath.Text : string.Empty), true);
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

                        string strListOfClientSelected = string.Empty;

                        for (int intRowIdx = 1; intRowIdx < grdClients.Rows.Count; intRowIdx++)
                        {
                            strListOfClientSelected = strListOfClientSelected + (strListOfClientSelected == string.Empty ? mcGrdClients[intRowIdx, mintGrdClients_CeC_NRI_col] : "," + Conversion.Val(mcGrdClients[intRowIdx, mintGrdClients_CeC_NRI_col]));
                        }
                        
                        sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetClients_SQL(strListOfClientSelected), "CeC_NRI", "Cec_Name", false, ref cboClients);

                        grdClients.StartEditing();

                        break;

                    case mintGrdClients_Installed_col:

                        if (mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.INSERT_MODE.ToString())
                        {
                            mcGrdClients[grdClients.Row, mintGrdClients_Installed_col] = (mcGrdClients[grdClients.Row, mintGrdClients_Installed_col] == "0" ? "1" : "0");

                            if (mcGrdClients[grdClients.Row, mintGrdClients_Installed_col] == "0")
                            {

                                mcGrdClients[grdClients.Row, mintGrdClients_IsCurrentVersion_col] = "0";
                            }
                        }
                        
                        break;

                    case mintGrdClients_IsCurrentVersion_col:

                        if (mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.INSERT_MODE.ToString())
                        {
                            mcGrdClients[grdClients.Row, mintGrdClients_IsCurrentVersion_col] = (mcGrdClients[grdClients.Row, mintGrdClients_IsCurrentVersion_col] == "0" ? "1" : "0");

                            if (mcGrdClients[grdClients.Row, mintGrdClients_IsCurrentVersion_col] == "1")
                            {

                                mcGrdClients[grdClients.Row, mintGrdClients_Installed_col] = "1";
                            }
                        }

                        break;

                    case mintGrdClients_Selection_col:

                        if (mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.INSERT_MODE.ToString())
                        {
                            mcGrdClients[grdClients.Row, mintGrdClients_Selection_col] = (mcGrdClients[grdClients.Row, mintGrdClients_Selection_col] == "0" ? "1" : "0");
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

            mcGrdClients.LstHostedCellControls.Add(new HostedCellControl(grdClients, pfblnGetNewLocationReportButton(), grdClients.Row, mintGrdClients_LocationReportExe_col));
        }

        void mcGrdClients_ValidateGridData(ValidateGridDataEventArgs eventArgs)
        {
            eventArgs.IsValid = true;

            //List<structClientAppVersion> lstStructCAV = ((IVersion)this).GetClientsList();

            //if (grdClients.Rows.Count <= 1)
            //{
            //    clsApp.GetAppController.ShowMessage((int)sclsConstants.Validation_Message.MANDATORY_GRID, MessageBoxButtons.OK, groupBox2.Text);
            //}
            //else
            //{
            //    for (int intRowIndex = 0; intRowIndex < lstStructCAV.Count; intRowIndex++)
            //    {
            //        eventArgs.IsValid = false;

            //        mcActionResults = mcCtrVersion.Validate_Client(lstStructCAV[intRowIndex]);

            //        if (!mcActionResults.IsValid)
            //        {
            //            clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI);

            //            switch ((ctr_Version.ErrorCode_Ver)mcActionResults.GetErrorCode)
            //            {
            //                case ctr_Version.ErrorCode_Ver.CLIENT_NAME_MANDATORY:

            //                    ((ComboBox)grdClients.GetCellRange(1, mintGrdClients_CeC_Name_col).Style.Editor).DroppedDown = true;

            //                    break;

            //                case ctr_Version.ErrorCode_Ver.REPORT_MANDATORY:

            //                    ((HostedCellControl)mcGrdClients.LstHostedCellControls[intRowIndex]).GetCellControl.Focus();

            //                    break;
            //            }

            //            break;
            //        }
            //        else
            //        {
            //            eventArgs.IsValid = true;
            //        }
            //    }
            //}
        }

        private void formController_SetReadRights()
        {
            switch (formController.FormMode)
            {
                case sclsConstants.DML_Mode.INSERT_MODE:

                    btnGenerate.Enabled = false;

                    tab.TabPages[mintTab_Revision].Enabled = false;

                    break;

                case sclsConstants.DML_Mode.UPDATE_MODE:

                    cboApplications.Enabled = false;
                    cboTemplates.Enabled = false;
                    txtCompiledBy.Enabled = false;
                    txtVersionNo.ReadOnly = true;

                    break;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!formController.ChangeMade)
            {
                this.Cursor = Cursors.WaitCursor;

                mcCtrVersion.blnUpdateVersionHierarchy();

                mcActionResults = mcCtrVersion.GetActionResult;

                if (!mcActionResults.IsValid)
                {
                    clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI, MessageBoxButtons.OK, mcActionResults.GetLstParams);

                    switch ((ctr_Version.ErrorCode_Ver)mcActionResults.GetErrorCode)
                    {
                        case ctr_Version.ErrorCode_Ver.APP_CHANGEMENT_MANDATORY:

                            btnReplaceAppChangeXLS.Focus();

                            break;

                        case ctr_Version.ErrorCode_Ver.RELEASE_MANDATORY:

                            btnReplaceExecutable.Focus();

                            break;

                        case ctr_Version.ErrorCode_Ver.TTAPP_MANDATORY:

                            btnReplaceTTApp.Focus();

                            break;

                        case ctr_Version.ErrorCode_Ver.REPORT_MANDATORY:

                            grdClients.Select(grdClients.Row, mintGrdClients_LocationReportExe_col);

                            break;
                    }
                }
                else
                {
                    formController.FormIsLoading = true;
                    formController_LoadData(new LoadDataEventArgs(formController.Item_NRI));
                    formController.FormIsLoading = false; //TODO METHOD 
                }

                this.Cursor = Cursors.Default;
            }
            else
            {
                clsApp.GetAppController.ShowMessage((int)sclsConstants.Validation_Message.INVALID_ACTION_IF_CHANGE_MADE);
            }
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
            if (mcGrdRevisions.bln_RowsSelValid())
            {
                int intItem_NRI = (int)grdRevisions[grdRevisions.Row, mintGrdRev_Rev_NRI_col];
                frmRevision frmVersion = new frmRevision();

                frmVersion.formController.ShowForm(sclsConstants.DML_Mode.DELETE_MODE, ref intItem_NRI, true);

                pfblnGrdRevisions_Load();
            }
        }

        private void txtVersionNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtVersionNo.Text != string.Empty && !Microsoft.VisualBasic.Information.IsNumeric(txtVersionNo.Text))
            {
                clsApp.GetAppController.ShowMessage((int)sclsConstants.Validation_Message.NUMERIC_VALUE);
                e.Cancel = true;
                txtVersionNo.SelectAll();
            }
        }

        private void btnReplaceReportExe_Click(object sender, EventArgs e)
        {
            mcGrdClients.GridIsLoading = true;

            foreach (HostedCellControl control in mcGrdClients.LstHostedCellControls)
            {
                if (control.GetCellControl.Handle == ((Button)sender).Handle)
                {
                    grdClients.Row = control.GetRowLinked.Index;
                }
            }

            ShowOpenFileDialog("LogirackTransport RPT (*.exe)|*.exe",grdClients, mcGrdClients[grdClients.Row, mintGrdClients_LocationReportExe_col], true);

            mcGrdClients.GridIsLoading = false;
        }

        private void btnGrdClientsDel_Click(object sender, EventArgs e)
        {
            if (mcGrdClients.bln_RowsSelValid() && mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.DELETE_MODE.ToString())
            {
                ((HostedCellControl)mcGrdClients.LstHostedCellControls[grdClients.Row - 1]).GetCellControl.Dispose();

                mcGrdClients.LstHostedCellControls.RemoveAt(grdClients.Row - 1);
            }
        }

        private void grdClients_AfterRowColChange(object sender, RangeEventArgs e)
        {
            pfblnEnableDisable_btnGrdClientsDelete(e.NewRange.r1);
        }

        private void btnShowExecutable_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReleasePath.Text) && Directory.Exists(txtReleasePath.Text))
            {
                System.Diagnostics.Process.Start(txtReleasePath.Text);
            }
        }

        private void btnShowExcel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExcelAppChangePath.Text) && File.Exists(txtExcelAppChangePath.Text))
            {
                System.Diagnostics.Process.Start(txtExcelAppChangePath.Text);
            }
        }

        private void btnShowWord_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWordAppChangePath.Text) && File.Exists(txtWordAppChangePath.Text))
            {
                System.Diagnostics.Process.Start(txtWordAppChangePath.Text);
            }
        }

        private void btnShowAccess_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTTAppPath.Text) && File.Exists(txtTTAppPath.Text))
            {
                if (File.Exists(txtTTAppPath.Text))
                {
                    System.Diagnostics.Process.Start("explorer.exe", "/select, " + txtTTAppPath.Text);
                }
            }
        }

        private void chkDemoVersion_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void btnExportInstallationKit_Click(object sender, EventArgs e)
        {
            pfblnExportInstallationKit();
        }

    }
}
