using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Ceritar.CVS.Controllers;
using Ceritar.TT3LightDLL.Controls;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers.Interfaces;
using C1.Win.C1FlexGrid;

namespace Ceritar.Logirack_CVS
{
    /// <summary>
    /// Cette classe contient les fonctions et évènements de la vue permettant de définir les révisions d'une version.
    /// </summary>
    public partial class frmRevision : Form, IFormController, IRevision
    {
        //Controller
        private ctr_Revision mcCtrRevision;

        //Columns grdRevModifs
        private const short mintGrdRevMod_Action_col = 1;
        private const short mintGrdRevMod_RevM_NRI_col = 2;
        private const short mintGrdRevMod_RevM_ChangeDesc_col = 3;

        //Columns grdSatellites
        private const short mintGrdSat_Action_col = 1;
        private const short mintGrdSat_CSA_NRI_col = 2;
        private const short mintGrdSat_CSA_Name_col = 3;
        private const short mintGrdSat_SRe_NRI_col = 4;
        private const short mintGrdSat_CSA_ExeLocation_col = 5;
        private const short mintGrdSat_CSA_ExeIsFolder_col = 6;
        private const short mintGrdSat_CSA_ExportFolderName_col = 7;

        //Classes
        private clsC1FlexGridWrapper mcGrdRevModifs;
        private clsC1FlexGridWrapper mcGrdSatellites;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Public working variables
        public int mintVersion_NRI;

        //Working Variables     
        private int mstrCeritarApplication_NRI;
        private string mstrCeritarApplication_Name;
        private string mstrCeritarApplication_RPT_Name;
        private ushort mintRevision_TS;
        private bool mblnAppExeLocation_Changed;


        public frmRevision()
        {
            InitializeComponent();

            mcCtrRevision = new ctr_Revision(this);

            mcGrdRevModifs = new clsC1FlexGridWrapper();

            mcGrdSatellites = new clsC1FlexGridWrapper();
            mcGrdSatellites.SetGridDisplay += mcGrdSatellites_SetGridDisplay;

            dtpCreation.Value = DateTime.Now;
        }


#region "Interfaces functions"

        string IRevision.GetCompiledBy()
        {
            return txtCreatedBy.Text;
        }

        string IRevision.GetCreationDate()
        {
            return dtpCreation.Value.ToString(clsApp.GetAppController.str_GetServerDateTimeFormat);
        }

        sclsConstants.DML_Mode IRevision.GetDML_Action()
        {
            return formController.FormMode;
        }

        string IRevision.GetLocation_Release()
        {
            return txtReleasePath.Text;
        }

        string IRevision.GetLocation_Scripts()
        {
            return txtScriptsPath.Text;
        }

        byte IRevision.GetRevisionNo()
        {
            return byte.Parse(txtRevisionNo.Text);
        }

        int IRevision.GetRevision_NRI()
        {
            return formController.Item_NRI;
        }

        int IRevision.GetRevision_TS()
        {
            return mintRevision_TS;
        }

        int IRevision.GetTemplateSource_NRI()
        {
            return (int)(cboTemplates.SelectedValue == null ? 0 : cboTemplates.SelectedValue);
        }

        ushort IRevision.GetVersionNo()
        {
            return UInt16.Parse(txtVersionNo.Text);
        }

        ctlFormController IFormController.GetFormController()
        {
            return formController;
        }

        int IRevision.GetCeritarClient_NRI()
        {
            return (int)(cboClients.SelectedValue == null ? 0 : cboClients.SelectedValue);
        }

        string IRevision.GetCeritarClient_Name()
        {
            return cboClients.GetItemText(cboClients.SelectedItem);
        }

        System.Collections.Generic.List<string> IRevision.GetModificationsList()
        {
            List<string> lstModifications = new List<string>();

            for (int intRowIndex = 1; intRowIndex < grdRevModifs.Rows.Count; intRowIndex++)
            {
                if (mcGrdRevModifs[intRowIndex, mintGrdRevMod_Action_col] != sclsConstants.DML_Mode.DELETE_MODE.ToString())
                {
                    lstModifications.Add(mcGrdRevModifs[intRowIndex, mintGrdRevMod_RevM_ChangeDesc_col]);
                }
            }

            return lstModifications;
        }

        int IRevision.GetVersion_NRI()
        {
            return mintVersion_NRI;
        }

        int IRevision.GetCeritarApplication_NRI()
        {
            return mstrCeritarApplication_NRI;
        }

        string IRevision.GetCeritarApplication_Name()
        {
            return mstrCeritarApplication_Name;
        }

        List<structSatRevision> IRevision.GetRevisionSatelliteList()
        {
            List<structSatRevision> lstSatelliteApps = new List<structSatRevision>();
            structSatRevision structSRe;

            for (int intRowIndex = 1; intRowIndex < grdSatellites.Rows.Count; intRowIndex++)
            {
                if (!string.IsNullOrEmpty(mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExeLocation_col]))
                {
                    structSRe = new structSatRevision();

                    structSRe.Action = clsApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(grdSatellites[intRowIndex, mintGrdSat_Action_col]);
                    structSRe.intCeritarSatelliteApp_NRI = Int32.Parse(mcGrdSatellites[intRowIndex, mintGrdSat_CSA_NRI_col]);
                    Int32.TryParse(mcGrdSatellites[intRowIndex, mintGrdSat_SRe_NRI_col], out structSRe.intSatRevision_NRI);
                    structSRe.strLocationSatelliteExe = mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExeLocation_col];
                    structSRe.strCeritarSatelliteApp_Name = mcGrdSatellites[intRowIndex, mintGrdSat_CSA_Name_col];
                    structSRe.blnExeIsFolder = Convert.ToBoolean(mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExeIsFolder_col]);
                    structSRe.strExportFolderName = mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExportFolderName_col];

                    lstSatelliteApps.Add(structSRe);
                }
            }

            return lstSatelliteApps;
        }

        bool IRevision.GetExeIsExternalReport()
        {
            return chkExeIsRPT.Checked;
        }

        bool IRevision.GetExeWithExternalReport()
        {
            return chkExeAndRpt.Checked;
        }

        string IRevision.GetCreatedBy()
        {
            return txtCreatedBy.Text;
        }

#endregion


#region "Functions"

        private bool pfblnData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;

            try
            {
                sqlRecord = clsSQL.ADOSelect(mcCtrRevision.strGetDataLoad_SQL(mintVersion_NRI, formController.Item_NRI));

                if (sqlRecord.Read())
                {
                    txtRevisionNo.Text = sqlRecord["RevisionNo"].ToString();
                    txtVersionNo.Text = sqlRecord["Ver_No"].ToString();
                    mstrCeritarApplication_NRI = Int32.Parse(sqlRecord["CeA_NRI"].ToString());
                    mstrCeritarApplication_Name = sqlRecord["CeA_Name"].ToString();
                    mstrCeritarApplication_RPT_Name = sqlRecord["CeA_ExternalRPTAppName"].ToString();

                    if (formController.FormMode != sclsConstants.DML_Mode.INSERT_MODE)
                    {
                        UInt16.TryParse(sqlRecord["Rev_TS"].ToString(), out mintRevision_TS);

                        txtReleasePath.Text = sqlRecord["Rev_Location_Exe"].ToString();
                        txtScriptsPath.Text = sqlRecord["Rev_Location_Scripts"].ToString();
                        txtCreatedBy.Text = sqlRecord["Rev_CreatedBy"].ToString();

                        cboClients.SelectedValue = Int32.Parse(sqlRecord["CeC_NRI"].ToString());
                        cboTemplates.SelectedValue = Int32.Parse(sqlRecord["Tpl_NRI"].ToString());

                        dtpCreation.Value = DateTime.Parse(sqlRecord["Rev_DtCreation"].ToString());

                        chkExeIsRPT.Checked = Convert.ToBoolean(sqlRecord["Rev_ExeIsReport"].ToString());
                        chkExeAndRpt.Checked = Convert.ToBoolean(sqlRecord["Rev_ExeWithReport"].ToString());

                        if (chkExeIsRPT.Checked)
                        {
                            btnSelectExecutableFolderPath.Enabled = false;
                            grdSatellites.Enabled = false;
                            chkExeAndRpt.Enabled = false;
                        }
                        else
                        {
                            grdSatellites.Enabled = true;
                            btnSelectExecutableFolderPath.Enabled = true;
                            chkExeAndRpt.Enabled = true;
                        }
                    }

                    chkExeAndRpt.Visible = !string.IsNullOrEmpty(mstrCeritarApplication_RPT_Name);
                    chkExeIsRPT.Visible = !string.IsNullOrEmpty(mstrCeritarApplication_RPT_Name);

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

        private bool pfblnGrdSatellites_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdSatellites.bln_FillData(mcCtrRevision.strGetListe_SatelliteApps_SQL());
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

                if (!string.IsNullOrEmpty(txtAffected.Text))
                {
                    folderBrowserDialog.SelectedPath = txtAffected.Text;
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

                    if (txtAffected.Name == txtReleasePath.Name) mblnAppExeLocation_Changed = true;
                }
            }
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

                            if (((TextBox)rControl).Name == txtReleasePath.Name) mblnAppExeLocation_Changed = true;
                        }

                        if (rControl.GetType() == typeof(C1FlexGrid))
                        {
                            if (((C1FlexGrid)rControl).Name == grdSatellites.Name)
                            {
                                if (mcGrdSatellites.bln_CellIsEmpty(grdSatellites.Row, mintGrdSat_SRe_NRI_col) & mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col] != openFileDialog.FileName)
                                {
                                    grdSatellites[grdSatellites.Row, mintGrdSat_Action_col] = sclsConstants.DML_Mode.INSERT_MODE;
                                }
                                else if (mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col] != openFileDialog.FileName)
                                {
                                    grdSatellites[grdSatellites.Row, mintGrdSat_Action_col] = sclsConstants.DML_Mode.UPDATE_MODE;
                                }

                                ((C1FlexGrid)rControl)[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col] = openFileDialog.FileName;
                                ((HostedCellControl)mcGrdSatellites.LstHostedCellControls[grdSatellites.Row - 1]).GetCellControl.BackColor = System.Drawing.Color.Yellow;
                            }
                        }

                        formController.ChangeMade = true;
                    }
                }
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        private Button pfblnGetNewLocationSatelliteExeButton()
        {
            Button btnLocationSatExe = new Button();

            try
            {
                btnLocationSatExe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                btnLocationSatExe.Image = ((System.Drawing.Image)(Properties.Resources.ellipsis));
                btnLocationSatExe.UseVisualStyleBackColor = true;

                btnLocationSatExe.Click += btnReplaceSatelliteExe_Click;
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return btnLocationSatExe;
        }

        private void btnReplaceSatelliteExe_Click(object sender, EventArgs e)
        {
            TextBox txtTemp = new TextBox();
            mcGrdSatellites.GridIsLoading = true;

            foreach (HostedCellControl control in mcGrdSatellites.LstHostedCellControls)
            {
                if (control.GetCellControl.Handle == ((Button)sender).Handle)
                {
                    grdSatellites.Row = control.GetRowLinked.Index;
                }
            }

            if (mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeIsFolder_col] == "True")
            {
                txtTemp.Text = mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col];

                ShowFolderBrowserDialog(ref txtTemp);

                if (!string.IsNullOrEmpty(txtTemp.Text))
                {
                    if (mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col] != txtTemp.Text)
                    {
                        grdSatellites[grdSatellites.Row, mintGrdSat_Action_col] = sclsConstants.DML_Mode.INSERT_MODE;
                    }
                    else if (mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col] != txtTemp.Text)
                    {
                        grdSatellites[grdSatellites.Row, mintGrdSat_Action_col] = sclsConstants.DML_Mode.UPDATE_MODE;
                    }

                    grdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col] = txtTemp.Text;

                    ((HostedCellControl)mcGrdSatellites.LstHostedCellControls[grdSatellites.Row - 1]).GetCellControl.BackColor = System.Drawing.Color.Yellow;
                }
            }
            else
            {
                ShowOpenFileDialog("Executables (*.exe)|*.exe", grdSatellites, mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col], true);
            }

            mcGrdSatellites.GridIsLoading = false;
        }

#endregion


        private void formController_LoadData(LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;
            Button btnPlaceHolder = null;

            mblnAppExeLocation_Changed = false;

            mcGrdSatellites.LstHostedCellControls = new List<HostedCellControl>();

            if (!mcGrdRevModifs.bln_Init(ref grdRevModifs, ref btnGrdRevAdd, ref btnGrdRevDel))
            { }
            if (!mcGrdSatellites.bln_Init(ref grdSatellites, ref btnPlaceHolder, ref btnPlaceHolder))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrRevision.strGetTemplates_SQL(), "Tpl_NRI", "Tpl_Name", false, ref cboTemplates))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrRevision.strGetClients_SQL(), "CeC_NRI", "CeC_Name", true, ref cboClients))
            { }
            else if (!pfblnData_Load())
            { }
            else if (!pfblnGrdRevModifs_Load())
            { }
            else if (!pfblnGrdSatellites_Load())
            { }
            else
            {
                blnValidReturn = true;
            }

            if (!blnValidReturn) this.Close();
        }

        private void formController_ValidateForm(ValidateFormEventArgs eventArgs)
        {
            mcActionResults = mcCtrRevision.Validate();

            if (!mcActionResults.IsValid)
            {
                clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI);

                switch ((ctr_Revision.ErrorCode_Rev)mcActionResults.GetErrorCode)
                {
                    case ctr_Revision.ErrorCode_Rev.CERITAR_CLIENT_MANDATORY:

                        cboClients.Focus();
                        cboClients.DroppedDown = true;
                        break;

                    case ctr_Revision.ErrorCode_Rev.EXE_OR_SCRIPT_MANDATORY:

                        btnSelectExecutableFilePath.Focus();  
                        break;

                    case ctr_Revision.ErrorCode_Rev.MODIFICATION_LIST_MANDATORY:

                        btnGrdRevAdd.Focus();
                        grdRevModifs.Row = mcActionResults.RowInError;
                        break;

                    case ctr_Revision.ErrorCode_Rev.TEMPLATE_MANDATORY:

                        cboTemplates.Focus();
                        cboTemplates.DroppedDown = true;
                        break;

                    case ctr_Revision.ErrorCode_Rev.REPORT_EXE_MANDATORY:

                        btnSelectExecutableFilePath.Focus();
                        break;

                    case ctr_Revision.ErrorCode_Rev.CREATED_BY_MANDATORY:

                        txtCreatedBy.Focus();
                        break;
                }
            }
            else
            {
                //Do nothing
            }

            eventArgs.IsValid = mcActionResults.IsValid;
        }

        private void formController_SaveData(SaveDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;

           /* if (formController.FormMode != sclsConstants.DML_Mode.DELETE_MODE || true)
            {
                blnValidReturn = mcCtrRevision.blnBuildRevisionHierarchy((int)cboTemplates.SelectedValue);
            }
            else
            {*/
                blnValidReturn = true;
            //}
            
            if (blnValidReturn)
            {
                mcActionResults = mcCtrRevision.Save();

                if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE) formController.Item_NRI = mcActionResults.GetNewItem_NRI;
            }

            if (!mcActionResults.IsValid)
            {
                clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI);
            }

            eventArgs.SaveSuccessful = mcActionResults.IsValid;
        }

        private void grdRevModifs_DoubleClick(object sender, EventArgs e)
        {
            if (grdRevModifs.Rows.Count > 1 && grdRevModifs.Row > 0)
            {
                switch (grdRevModifs.Col)
                {
                    case mintGrdRevMod_RevM_ChangeDesc_col:

                        grdRevModifs.StartEditing();

                        formController.ChangeMade = true;
                        break;
                }
            }
        }

        void mcGrdSatellites_SetGridDisplay()
        {
            grdSatellites.Cols[mintGrdSat_CSA_Name_col].Width = 200;
            grdSatellites.Cols[mintGrdSat_CSA_ExeLocation_col].Width = 42;

            if (grdSatellites.Rows.Count > 1)
            {
                for (int intRowIndex = 1; intRowIndex < grdSatellites.Rows.Count; intRowIndex++)
                {
                    mcGrdSatellites.LstHostedCellControls.Add(new HostedCellControl(grdSatellites, pfblnGetNewLocationSatelliteExeButton(), intRowIndex, mintGrdSat_CSA_ExeLocation_col));

                    if (!mcGrdSatellites.bln_CellIsEmpty(intRowIndex, mintGrdSat_CSA_ExeLocation_col))
                    {
                        mcGrdSatellites.LstHostedCellControls[intRowIndex - 1].GetCellControl.BackColor = System.Drawing.Color.Yellow;
                    }
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

        private void btnSelectExecutableFolderPath_Click(object sender, EventArgs e)
        {
            ShowFolderBrowserDialog(ref txtReleasePath);
        }

        private void btnSelectScriptsFilePath_Click(object sender, EventArgs e)
        {
            string strInitialDirectory = formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE ? txtScriptsPath.Text : string.Empty;

            ShowOpenFileDialog("Scripts (*.sql)|*.sql|(*.txt)|*.txt", txtScriptsPath, strInitialDirectory, true);
        }

        private void btnSelectExecutableFilePath_Click(object sender, EventArgs e)
        {
            string strInitialDirectory = formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE ? txtReleasePath.Text : string.Empty;

            ShowOpenFileDialog("Executable (*.exe)|*.exe", txtReleasePath, strInitialDirectory, true);
        }

        private void btnShowScriptsFolder_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtScriptsPath.Text) || Directory.Exists(txtScriptsPath.Text))
            {
                if ((File.GetAttributes(txtScriptsPath.Text) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    System.Diagnostics.Process.Start(txtScriptsPath.Text);
                }
                else
                {
                    System.Diagnostics.Process.Start("explorer.exe", "/select, " + txtScriptsPath.Text);
                }
            }
        }

        private void btnShowExecutableFolder_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtReleasePath.Text) || Directory.Exists(txtReleasePath.Text))
            {
                if ((File.GetAttributes(txtReleasePath.Text) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    System.Diagnostics.Process.Start(txtReleasePath.Text);
                }
                else
                {
                    System.Diagnostics.Process.Start("explorer.exe", "/select, " + txtReleasePath.Text);
                }
            }
        }

        private void cboClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;

                pfblnGrdSatellites_Load();
            }
        }

        private void formController_SetReadRights()
        {
            switch ((int)formController.FormMode)
            {
                case (int)sclsConstants.DML_Mode.UPDATE_MODE:

                    cboTemplates.Enabled = false;
                    txtCreatedBy.Enabled = false;

                    break;
            }
        }

        //private void grdSatellites_DoubleClick(object sender, EventArgs e)
        //{
        //    switch (grdSatellites.Col)
        //    {
        //        case mintGrdSat_CSA_Sel_col:

        //            mcGrdSatellites.GridIsLoading = true;

        //            int intSelectedRow = grdSatellites.FindRow("true", 1, mintGrdSat_CSA_Sel_col, false, true, false);

        //            if (intSelectedRow >= 1 & intSelectedRow != grdSatellites.Row) mcGrdSatellites[intSelectedRow, mintGrdSat_CSA_Sel_col] = "0";

        //            mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_Sel_col] = (mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_Sel_col] == "0" ? "1" : "0");

        //            mcGrdSatellites.GridIsLoading = false;

        //            formController.ChangeMade = true;

        //            break;
        //    }
        //}

        private void chkExeIsRPT_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;

                chkExeAndRpt.Enabled = !chkExeIsRPT.Checked;

                if (chkExeIsRPT.Checked)
                {
                    grdSatellites.Enabled = false;
                    btnSelectExecutableFolderPath.Enabled = false;
                    chkExeAndRpt.Checked = false;
                }
                else
                {
                    txtReleasePath.Text = string.Empty;
                    grdSatellites.Enabled = true;
                    btnSelectExecutableFolderPath.Enabled = true;
                }
            }
        }

        private void txtCreatedBy_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading) 
            {
                formController.ChangeMade = true;
            }
        }

        private void chkExeAndRpt_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                chkExeIsRPT.Enabled = !chkExeAndRpt.Checked;

                if (chkExeAndRpt.Checked)
                {
                    chkExeIsRPT.Checked = false;
                }
                formController.ChangeMade = true;
            }
        }

        private void chkExeAndRpt_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //TODO
        }
         //e.Cancel = true;

         //   if (mblnAppExeLocation_Changed || formController.FormMode != sclsConstants.DML_Mode.UPDATE_MODE)
         //   {
         //       e.Cancel = false;
         //   }

    }
}
