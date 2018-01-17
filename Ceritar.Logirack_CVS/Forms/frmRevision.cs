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
using System.Threading;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;

namespace Ceritar.Logirack_CVS.Forms
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
        private const short mintGrdSat_CSV_NRI_col = 8;
        private const short mintGrdSat_CSV_ExePerCustomer_col = 9;

        //Classes
        private clsTTC1FlexGridWrapper mcGrdRevModifs;
        private clsTTC1FlexGridWrapper mcGrdSatellites;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Public working variables
        public int mintVersion_NRI;

        //Working Variables     
        private int mstrCeritarApplication_NRI;
        private string mstrCeritarApplication_Name;
        private string mstrCeritarApplication_RPT_Name;
        private string mstrVariousFileLocation;
        private string mstrVariousFolderLocation;
        private ushort mintRevision_TS;
        private bool mblnAppExeLocationExistsOnLoad;


        public frmRevision()
        {
            InitializeComponent();

            mcCtrRevision = new ctr_Revision(this);

            mcGrdRevModifs = new clsTTC1FlexGridWrapper();

            mcGrdSatellites = new clsTTC1FlexGridWrapper();
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
            return dtpCreation.Value.ToString(clsTTApp.GetAppController.str_GetServerDateTimeFormat);
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

        string IRevision.GetNote()
        {
            return txtNote.Text;
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

                    structSRe.Action = clsTTApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(grdSatellites[intRowIndex, mintGrdSat_Action_col]);
                    structSRe.intCeritarSatelliteApp_NRI = Int32.Parse(mcGrdSatellites[intRowIndex, mintGrdSat_CSA_NRI_col]);
                    Int32.TryParse(mcGrdSatellites[intRowIndex, mintGrdSat_SRe_NRI_col], out structSRe.intSatRevision_NRI);
                    structSRe.strLocationSatelliteExe = mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExeLocation_col];
                    structSRe.strCeritarSatelliteApp_Name = mcGrdSatellites[intRowIndex, mintGrdSat_CSA_Name_col];
                    structSRe.blnExeIsFolder = Convert.ToBoolean(mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExeIsFolder_col]);
                    structSRe.blnExePerCustomer = Convert.ToBoolean(mcGrdSatellites[intRowIndex, mintGrdSat_CSV_ExePerCustomer_col]);
                    structSRe.strExportFolderName = mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExportFolderName_col];
                    Int32.TryParse(mcGrdSatellites[intRowIndex, mintGrdSat_CSV_NRI_col], out structSRe.intClientSatVersion_NRI);

                    if (!mcGrdSatellites.bln_CellIsEmpty(intRowIndex, mintGrdSat_SRe_NRI_col))
                    {
                        structSRe.intSatRevision_NRI = Int32.Parse(mcGrdSatellites[intRowIndex, mintGrdSat_SRe_NRI_col]);
                    }
                    
                    lstSatelliteApps.Add(structSRe);
                }
            }

            return lstSatelliteApps;
        }

        bool IRevision.GetExeIsExternalReport()
        {
            return optRptOnly.Checked;
        }

        bool IRevision.GetExeWithExternalReport()
        {
            return optExeAndRpt.Checked;
        }

        string IRevision.GetCreatedBy()
        {
            return txtCreatedBy.Text;
        }

        string IRevision.GetLocation_VariousFile()
        {
            return mstrVariousFileLocation;
        }

        string IRevision.GetLocation_VariousFolder()
        {
            return mstrVariousFolderLocation;
        }

        bool IRevision.GetIfScriptsAreToAppend()
        {
            return chkAddScripts.Checked;
        }

        bool IRevision.IsPreparationMode()
        {
            return chkPreparation.Checked;
        }

        #endregion


        #region "Functions"

        private bool pfblnData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;

            try
            {
                sqlRecord = clsTTSQL.ADOSelect(mcCtrRevision.strGetDataLoad_SQL(mintVersion_NRI, formController.Item_NRI));

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

                        if (sqlRecord["Rev_Note"] != DBNull.Value)
                            txtNote.Text = sqlRecord["Rev_Note"].ToString();

                        mblnAppExeLocationExistsOnLoad = !string.IsNullOrEmpty(txtReleasePath.Text);

                        cboClients.SelectedValue = Int32.Parse(sqlRecord["CeC_NRI"].ToString());
                        cboTemplates.SelectedValue = Int32.Parse(sqlRecord["Tpl_NRI"].ToString());

                        dtpCreation.Value = DateTime.Parse(sqlRecord["Rev_DtCreation"].ToString());

                        optRptOnly.Checked = Convert.ToBoolean(sqlRecord["Rev_ExeIsReport"].ToString());
                        optExeAndRpt.Checked = Convert.ToBoolean(sqlRecord["Rev_ExeWithReport"].ToString());

                        chkPreparation.Checked = Convert.ToBoolean(sqlRecord["Rev_PreparationMode"].ToString());
                        chkPreparation.Enabled = chkPreparation.Checked;

                        if (optRptOnly.Checked)
                        {
                            btnSelectExecutableFolderPath.Enabled = false;
                        }
                        else if (optExeAndRpt.Checked)
                        {
                            btnSelectExecutableFolderPath.Enabled = true;
                        }
                        else
                        {
                            optExeOnly.Checked = true;
                        }

                        btnShowScriptsFolder.FlatAppearance.BorderSize = 2;

                        if (Directory.Exists(txtScriptsPath.Text) || File.Exists(txtScriptsPath.Text))
                        {
                            btnShowScriptsFolder.FlatAppearance.BorderColor = Color.Lime;            
                        }
                        else if (!string.IsNullOrEmpty(txtScriptsPath.Text))
                        {
                            btnShowScriptsFolder.FlatAppearance.BorderColor = Color.Red;
                        }
                        else
                        {
                            btnShowScriptsFolder.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
                        }

                        btnShowExecutableFolder.FlatAppearance.BorderSize = 2;

                        if (Directory.Exists(txtReleasePath.Text) || File.Exists(txtReleasePath.Text))
                        {
                            btnShowExecutableFolder.FlatAppearance.BorderColor = Color.Lime;
                        }
                        else if (!string.IsNullOrEmpty(txtReleasePath.Text))
                        {
                            btnShowExecutableFolder.FlatAppearance.BorderColor = Color.Red;
                        }
                        else
                        {
                            btnShowExecutableFolder.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
                        }
                    }
                    else
                    {
                        chkPreparation.Enabled = true;                     
                    }

                    optExeAndRpt.Visible = !string.IsNullOrEmpty(mstrCeritarApplication_RPT_Name);
                    optRptOnly.Visible = !string.IsNullOrEmpty(mstrCeritarApplication_RPT_Name);

                    blnValidReturn = true;
                }

                dtpCreation.CustomFormat = clsTTApp.GetAppController.str_GetServerDateTimeFormat;

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

        private void ShowFolderBrowserDialog(ref TextBox txtAffected, string vstrDialogDescription = "", bool vblnChangeMade = true)
        {
            DialogResult dialogResult;

            if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
            {
                folderBrowserDialog.ShowNewFolderButton = false;
                folderBrowserDialog.Description = vstrDialogDescription;

                if (!string.IsNullOrEmpty(txtAffected.Text))
                {
                    folderBrowserDialog.SelectedPath = txtAffected.Text;
                }
                else
                {
                    folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                    folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }

                dialogResult = folderBrowserDialog.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtAffected.Text = folderBrowserDialog.SelectedPath;

                    formController.ChangeMade = vblnChangeMade;

                    if (txtAffected.Name == txtScriptsPath.Name) btnShowScriptsFolder.FlatAppearance.BorderColor = Color.Yellow;

                    if (txtAffected.Name == txtReleasePath.Name) btnShowExecutableFolder.FlatAppearance.BorderColor = Color.Yellow;
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

                            if (((TextBox)rControl).Name == txtScriptsPath.Name) btnShowScriptsFolder.FlatAppearance.BorderColor = Color.Yellow;

                            if (((TextBox)rControl).Name == txtReleasePath.Name) btnShowExecutableFolder.FlatAppearance.BorderColor = Color.Yellow;
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
                btnLocationSatExe.FlatStyle = FlatStyle.Flat;
                btnLocationSatExe.FlatAppearance.BorderSize = 1;

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
                    if (mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col] != txtTemp.Text && mcGrdSatellites.bln_CellIsEmpty(grdSatellites.Row, mintGrdSat_SRe_NRI_col))
                    {
                        grdSatellites[grdSatellites.Row, mintGrdSat_Action_col] = sclsConstants.DML_Mode.INSERT_MODE;

                        ((HostedCellControl)mcGrdSatellites.LstHostedCellControls[grdSatellites.Row - 1]).GetCellControl.BackColor = System.Drawing.Color.Yellow;
                    }
                    else if (mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col] != txtTemp.Text)
                    {
                        grdSatellites[grdSatellites.Row, mintGrdSat_Action_col] = sclsConstants.DML_Mode.UPDATE_MODE;

                        ((HostedCellControl)mcGrdSatellites.LstHostedCellControls[grdSatellites.Row - 1]).GetCellControl.BackColor = System.Drawing.Color.Yellow;
                    }

                    grdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col] = txtTemp.Text;       
                }
            }
            else
            {
                ShowOpenFileDialog("Executables (*.exe)|*.exe", grdSatellites, mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col], true);
            }

            mcGrdSatellites.GridIsLoading = false;
        }

        private bool pfblnExportRevisionKit()
        {
            bool blnValidReturn = false;
            TextBox txtTemp = new TextBox();

            try
            {
                ShowFolderBrowserDialog(ref txtTemp, "Sélectionnez l'emplacement où sauvegarder l'archive.", false);

                if (!string.IsNullOrEmpty(txtTemp.Text))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    blnValidReturn = mcCtrRevision.blnExportRevisionKit(txtTemp.Text);

                    if (blnValidReturn)
                    {
                        MessageBox.Show("Export effectué avec succès!" + Environment.NewLine + "À l'emplacement suivant : " + txtTemp.Text, "Message", MessageBoxButtons.OK);
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

#endregion


        private void formController_LoadData(LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;
            Button btnPlaceHolder = null;

            mblnAppExeLocationExistsOnLoad = false;

            mcGrdSatellites.LstHostedCellControls = new List<HostedCellControl>();

            mstrVariousFileLocation = string.Empty;
            mstrVariousFolderLocation = string.Empty;
            btnSelectVariousFilePath.BackColor = System.Drawing.SystemColors.Control;
            btnSelectVariousFolderPath.BackColor = System.Drawing.SystemColors.Control;

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
                clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI);

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
            frmWorkInProgress frmWorking = new frmWorkInProgress();
            Thread newThread = null;

            try
            {
                //if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
                //{
                    newThread = new Thread(() => frmWorking.ShowDialog());
                    newThread.Start();
                //}

                mcActionResults = mcCtrRevision.Save();

                if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE) formController.Item_NRI = mcActionResults.GetNewItem_NRI;

                if (!mcActionResults.IsValid)
                {
                    clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI);
                }
                else if (formController.FormMode != sclsConstants.DML_Mode.INSERT_MODE)
                {
                    if (mcActionResults.SuccessMessage_NRI > 0) clsTTApp.GetAppController.ShowMessage(mcActionResults.SuccessMessage_NRI);
                }         
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                //if (newThread != null)
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        frmWorking.Close();//Access your controls
                    }));
                

                eventArgs.SaveSuccessful = mcActionResults.IsValid;
            }          
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
            grdSatellites.Cols[mintGrdSat_CSA_Name_col].Width = 250;
            grdSatellites.Cols[mintGrdSat_CSA_ExeLocation_col].Width = 30;

            if (grdSatellites.Rows.Count > 1)
            {
                for (int intRowIndex = 1; intRowIndex < grdSatellites.Rows.Count; intRowIndex++)
                {
                    mcGrdSatellites.LstHostedCellControls.Add(new HostedCellControl(grdSatellites, pfblnGetNewLocationSatelliteExeButton(), intRowIndex, mintGrdSat_CSA_ExeLocation_col));
                    
                    if (!mcGrdSatellites.bln_CellIsEmpty(intRowIndex, mintGrdSat_CSA_ExeLocation_col))
                    {
                        //mcGrdSatellites.LstHostedCellControls[intRowIndex - 1].GetCellControl.BackColor = System.Drawing.Color.Yellow;

                        if (Directory.Exists(mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExeLocation_col]) || File.Exists(mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExeLocation_col]))
                        {
                            ((Button)mcGrdSatellites.LstHostedCellControls[intRowIndex - 1].GetCellControl).BackColor = Color.Lime;
                        }
                        else if (!string.IsNullOrEmpty(mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExeLocation_col]))
                        {
                            ((Button)mcGrdSatellites.LstHostedCellControls[intRowIndex - 1].GetCellControl).BackColor = Color.Red;
                        }
                        else
                        {
                            ((Button)mcGrdSatellites.LstHostedCellControls[intRowIndex - 1].GetCellControl).BackColor = System.Drawing.SystemColors.Control;
                        }
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
                case (int)sclsConstants.DML_Mode.INSERT_MODE:

                    btnSelectVariousFilePath.Enabled = false;
                    btnSelectVariousFolderPath.Enabled = false;
                    btnShowRootFolder.Enabled = false;
                    btnExportRevision.Enabled = false;                                      

                    break;

                case (int)sclsConstants.DML_Mode.UPDATE_MODE:

                    cboTemplates.Enabled = false;
                    cboClients.Enabled = false;
                    txtCreatedBy.ReadOnly = true;
                    btnSelectVariousFilePath.Enabled = true;
                    btnSelectVariousFolderPath.Enabled = true;
                    btnShowRootFolder.Enabled = true;
                    btnExportRevision.Enabled = true;

                    break;
            }

            ChkPreparationControlsActivation();
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

        private void txtCreatedBy_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading) 
            {
                formController.ChangeMade = true;
            }
        }

        private void optExeAndRpt_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;

                if (mblnAppExeLocationExistsOnLoad)
                {
                    txtReleasePath.Text = string.Empty;
                    mblnAppExeLocationExistsOnLoad = false;
                    btnShowRootFolder.Enabled = false;
                }
            }
        }

        private void optRptOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;

                if (optRptOnly.Checked)
                {
                    btnSelectExecutableFolderPath.Enabled = false;
                }
                else
                {
                    btnSelectExecutableFolderPath.Enabled = true;
                }

                if (mblnAppExeLocationExistsOnLoad)
                {
                    txtReleasePath.Text = string.Empty;
                    mblnAppExeLocationExistsOnLoad = false;
                    btnShowRootFolder.Enabled = false;
                }
            }
        }

        private void optExeOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading && mblnAppExeLocationExistsOnLoad)
            {
                txtReleasePath.Text = string.Empty;
                mblnAppExeLocationExistsOnLoad = false;
                btnShowRootFolder.Enabled = false;
            }
        }

        private void btnSelectVariousFilePath_Click(object sender, EventArgs e)
        {
            TextBox txtPlaceHolder = new TextBox();

            ShowOpenFileDialog("Executable (*.*)|*.*", txtPlaceHolder, mstrVariousFileLocation, true);

            mstrVariousFileLocation = txtPlaceHolder.Text;

            if (!string.IsNullOrEmpty(mstrVariousFileLocation)) btnSelectVariousFilePath.BackColor = System.Drawing.Color.Yellow;
        }

        private void btnSelectVariousFolderPath_Click(object sender, EventArgs e)
        {
            TextBox txtPlaceHolder = new TextBox();

            txtPlaceHolder.Text = mstrVariousFolderLocation;

            ShowFolderBrowserDialog(ref txtPlaceHolder);

            mstrVariousFolderLocation = txtPlaceHolder.Text;

            if (!string.IsNullOrEmpty(mstrVariousFolderLocation)) btnSelectVariousFolderPath.BackColor = System.Drawing.Color.Yellow;
        }

        private void btnShowRootFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string strRootFolder = mcCtrRevision.str_GetRevisionFolderPath((int)cboTemplates.SelectedValue, txtVersionNo.Text);

                System.Diagnostics.Process.Start(@strRootFolder);
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        private void btnExportRevision_Click(object sender, EventArgs e)
        {
            pfblnExportRevisionKit();
        }

        private void mnuiDelete_Click(object sender, EventArgs e)
        {
            bool blnValidReturn = false;

            if (mcGrdSatellites.bln_RowEditIsValid() && !mcGrdSatellites.bln_CellIsEmpty(grdSatellites.Row, mintGrdSat_SRe_NRI_col)) //&& e.ClickedItem.Name.Equals(mnuiDelete.Name))
            {
                blnValidReturn = mcCtrRevision.blnDeleteSatelliteRevision(Int32.Parse(mcGrdSatellites[grdSatellites.Row, mintGrdSat_SRe_NRI_col]));

                if (blnValidReturn) formController.ReloadForm();
            }
        }

        private void txtNote_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void mnuiShowInExplorer_Click(object sender, EventArgs e)
        {
            string strLocation = mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col];

            if (mcGrdSatellites.bln_RowEditIsValid() && !string.IsNullOrEmpty(strLocation))
            {
                if (File.Exists(strLocation) || Directory.Exists(strLocation))
                {
                    if ((File.GetAttributes(strLocation) & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        System.Diagnostics.Process.Start(strLocation);
                    }
                    else
                    {
                        System.Diagnostics.Process.Start("explorer.exe", "/select, " + strLocation);
                    }
                }
            }
        }

        private void btnPrintPairValidation_Click(object sender, EventArgs e)
        {
            ReportDocument rptDoc = new ReportDocument();
            frmCrystalReportViewer frmReportViewer = new frmCrystalReportViewer();

            CrystalDecisions.Shared.TableLogOnInfo tableLogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();
            CrystalDecisions.Shared.ConnectionInfo crConnectionInfo = new CrystalDecisions.Shared.ConnectionInfo();
            SqlConnectionStringBuilder appConnection = new SqlConnectionStringBuilder(clsTTApp.GetAppController.SQLConnection.ConnectionString);

            rptDoc.Load(Application.StartupPath + @"\Crystal Reports\" + typeof(Crystal_Reports.rptSignature).Name + @".rpt");

            crConnectionInfo.ServerName = clsTTApp.GetAppController.SQLConnection.DataSource;
            crConnectionInfo.DatabaseName = clsTTApp.GetAppController.SQLConnection.Database;
            crConnectionInfo.UserID = appConnection.UserID;
            crConnectionInfo.Password = appConnection.Password;

            rptDoc.SetDatabaseLogon(appConnection.UserID, appConnection.Password, clsTTApp.GetAppController.SQLConnection.DataSource, clsTTApp.GetAppController.SQLConnection.Database, true);

            foreach (Table table in rptDoc.Database.Tables)
            {
                tableLogoninfo = table.LogOnInfo;
                tableLogoninfo.ConnectionInfo = crConnectionInfo;
                table.ApplyLogOnInfo(tableLogoninfo);
            }            

            rptDoc.SetParameterValue("@Rev_NRI", formController.Item_NRI);
            rptDoc.SetParameterValue("CurrentUserName", clsTTApp.GetAppController.cUser.User_FirsName + " " + clsTTApp.GetAppController.cUser.User_LastName);

            rptDoc.SetParameterValue("@Rev_NRI", formController.Item_NRI, rptDoc.Subreports["rptSub_Signature_Objectif"].Name.ToString());

            rptDoc.SetParameterValue("@Rev_NRI", formController.Item_NRI, rptDoc.Subreports["rptSub_Signature_Contenu_App"].Name.ToString());

            rptDoc.SetParameterValue("ScriptsList", mcCtrRevision.strGetRevisionScriptsList(formController.Item_NRI), rptDoc.Subreports["rptSub_Signature_Contenu_Script"].Name.ToString());          

            frmReportViewer.crystalReportViewer1.ReportSource = rptDoc;
            frmReportViewer.crystalReportViewer1.Refresh();

            frmReportViewer.Show();
        }

        private void chkPreparation_CheckStateChanged(object sender, EventArgs e)
        {
            ChkPreparationControlsActivation();

            formController.ChangeMade = true;
        }
                
        private void ChkPreparationControlsActivation()
        {
            if (chkPreparation.Checked)
            {
                gbExe.Enabled = false;
                gbSatellites.Enabled = false;
                gbScripts.Enabled = false;

                btnShowRootFolder.Enabled = false;
                btnPrintPairValidation.Enabled = false;
                btnExportRevision.Enabled = false;
            }
            else
            {
                gbExe.Enabled = true;
                gbSatellites.Enabled = true;
                gbScripts.Enabled = true;

                btnShowRootFolder.Enabled = true;
                btnPrintPairValidation.Enabled = true;
                btnExportRevision.Enabled = true;
            }
        }
    }
}
