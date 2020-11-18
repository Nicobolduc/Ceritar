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

        //Columns grdClients
        private const short mintGrdClients_Action_col = 1;
        private const short mintGrdClients_CAR_NRI_col = 2;
        private const short mintGrdClients_CAR_TS_col = 3;
        private const short mintGrdClients_CeC_NRI_col = 4;
        private const short mintGrdClients_CeC_Name_col = 5;

        //Columns grdRevModifs
        private const short mintGrdRevMod_CeC_NRI_For_col = 1;
        private const short mintGrdRevMod_CeC_Name_For_col = 2;
        private const short mintGrdRevMod_RevM_DtHr_col = 3;
        private const short mintGrdRevMod_RevM_NRI_col = 4;
        private const short mintGrdRevMod_Action_col = 5;
        private const short mintGrdRevMod_RevM_ChangeDesc_col = 6;

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
        public clsTTC1FlexGridWrapper mcGrdClients;
        private clsTTC1FlexGridWrapper mcGrdRevModifs;
        private clsTTC1FlexGridWrapper mcGrdSatellites;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Public working variables
        public int mintVersion_NRI;

        //Messages
        private const int mintMSG_ValidCharacters = 58;
        private const int mintMSG_ChangesWillBeLostOnRowChange = 27;

        //Working Variables     
        private int mintCeritarApplication_NRI;
        public int mintCeritarApplication_NRI_Master;
        private string mstrCeritarApplication_Name;
        private string mstrCeritarApplication_RPT_Name;
        private string mstrVariousFileLocation;
        private string mstrVariousFolderLocation;
        private ushort mintRevision_TS;
        private bool mblnAppExeLocationExistsOnLoad;
        private sbyte mintAttributedRevisionNo;
        private int mintGrdClient_SelectedRow = 1;
        private bool mblnGrdSatellitesChangeMade;


        public frmRevision()
        {
            InitializeComponent();

            mcCtrRevision = new ctr_Revision(this);

            mcGrdRevModifs = new clsTTC1FlexGridWrapper();

            mcGrdSatellites = new clsTTC1FlexGridWrapper();
            mcGrdSatellites.SetGridDisplay += mcGrdSatellites_SetGridDisplay;

            mcGrdRevModifs.AfterClickAdd += mcGrdRevModifs_AfterClickAdd;
            mcGrdRevModifs.SetGridDisplay += McGrdRevModifs_SetGridDisplay;

            mcGrdClients = new clsTTC1FlexGridWrapper();
            mcGrdClients.SetGridDisplay += mcGrdClients_SetGridDisplay;
            mcGrdClients.AfterClickAdd += mcGrdClients_AfterClickAdd;
            mcGrdClients.BeforeClickAdd += mcGrdClients_BeforeClickAdd;

            dtpCreation.Value = DateTime.Now;
        }

        private void McGrdRevModifs_SetGridDisplay()
        {
            grdRevModifs.Cols[mintGrdRevMod_RevM_DtHr_col].Width = 92;
            grdRevModifs.Cols[mintGrdRevMod_CeC_Name_For_col].Width = 100;

            grdRevModifs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictAll;
            grdRevModifs.Cols[mintGrdRevMod_CeC_NRI_For_col].AllowMerging = true;
            grdRevModifs.Cols[mintGrdRevMod_RevM_DtHr_col].AllowMerging = true;
            grdRevModifs.Cols[mintGrdRevMod_CeC_Name_For_col].AllowMerging = true;
            
            if (grdRevModifs.Rows.Count > 1)
            {
                cboClientsRevModif.Visible = true;
                dtpRevModif.Visible = true;
            }

            mcGrdRevModifs.SetColType_ComboBox(ref cboClientsRevModif, mcCtrRevision.strGetClients_SQL(formController.Item_NRI), mintGrdRevMod_CeC_Name_For_col, "CeC_NRI", "CeC_Name", false);
            mcGrdRevModifs.SetColType_DateTimePicker(ref dtpRevModif, mintGrdRevMod_RevM_DtHr_col, clsTTApp.GetAppController.str_Get_FRQC_DateTimeFormat);
        }

        private void mcGrdClients_BeforeClickAdd(ref bool vblnCancel)
        {
            if (mblnGrdSatellitesChangeMade || mcGrdClients[grdClients.Row, mintGrdClients_Action_col] == sclsConstants.DML_Mode.INSERT_MODE.ToString())
            {
                DialogResult msgResult = clsTTApp.GetAppController.ShowMessage(mintMSG_ChangesWillBeLostOnRowChange, MessageBoxButtons.YesNo);

                mblnGrdSatellitesChangeMade = msgResult == DialogResult.No;

                if (msgResult == System.Windows.Forms.DialogResult.Yes)
                {
                    formController.FormIsLoading = true;
                    pfblnGrdClients_Load();
                    pfblnGrdSatellites_Load();
                    formController.FormIsLoading = false;
                }
            }

            vblnCancel = mblnGrdSatellitesChangeMade;
        }

        private void mcGrdClients_AfterClickAdd()
        {
            grdClients[grdClients.Row, mintGrdClients_CAR_NRI_col] = 0;
            grdClients[grdClients.Row, mintGrdClients_CAR_TS_col] = 0;

            mcGrdSatellites.ClearGrid();
        }

        private void mcGrdClients_SetGridDisplay()
        {
            grdClients.Cols[mintGrdClients_CeC_Name_col].Width = 220;

            if (grdClients.Rows.Count > 1)
            {
                cboClients.Visible = true;
            }

            mcGrdClients.SetColType_ComboBox(ref cboClients, mcCtrRevision.strGetClients_SQL(formController.Item_NRI), mintGrdClients_CeC_Name_col, "CeC_NRI", "CeC_Name", false);
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

        sbyte IRevision.GetRevisionNo()
        {
            return sbyte.Parse((txtRevisionNo.Text == string.Empty ? "-1" : txtRevisionNo.Text));
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

        string IRevision.GetCeritarClient_Name()
        {
            if (grdClients.Rows.Count <= 1)
                throw new InvalidArgumentException("Erreur invalide non gerer",0);

            return mcGrdClients[grdClients.Row, mintGrdClients_CeC_Name_col];
        }

        System.Collections.Generic.List<structRevModifs> IRevision.GetModificationsList()
        {
            List<structRevModifs> lstModifications = new List<structRevModifs>();
            structRevModifs strcutRevModifs;

            for (int intRowIndex = 1; intRowIndex < grdRevModifs.Rows.Count; intRowIndex++)
            {
                if (mcGrdRevModifs[intRowIndex, mintGrdRevMod_Action_col] != sclsConstants.DML_Mode.DELETE_MODE.ToString())
                {
                    strcutRevModifs = new structRevModifs();

                    strcutRevModifs.Action = clsTTApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(mcGrdRevModifs[intRowIndex, mintGrdRevMod_Action_col]);
                    strcutRevModifs.dthrSent = DateTime.Parse(mcGrdRevModifs[intRowIndex, mintGrdRevMod_RevM_DtHr_col]);
                    strcutRevModifs.intCeritarClient_NRI = Int32.Parse(mcGrdRevModifs[intRowIndex, mintGrdRevMod_CeC_NRI_For_col]);
                    strcutRevModifs.strDescriptionModif = mcGrdRevModifs[intRowIndex, mintGrdRevMod_RevM_ChangeDesc_col].ToString();

                    lstModifications.Add(strcutRevModifs);
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
            return mintCeritarApplication_NRI;
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
                    structSRe.blnExePerCustomer = Convert.ToBoolean(Int32.Parse(mcGrdSatellites[intRowIndex, mintGrdSat_CSV_ExePerCustomer_col]));
                    structSRe.strExportFolderName = mcGrdSatellites[intRowIndex, mintGrdSat_CSA_ExportFolderName_col];
                    Int32.TryParse(mcGrdSatellites[intRowIndex, mintGrdSat_CSV_NRI_col], out structSRe.intClientSatVersion_NRI);

                    if (!mcGrdSatellites.bln_CellIsEmpty(intRowIndex, mintGrdSat_SRe_NRI_col))
                    {
                        structSRe.intSatRevision_NRI = Int32.Parse(mcGrdSatellites[intRowIndex, mintGrdSat_SRe_NRI_col]);
                    }

                    if (structSRe.blnExePerCustomer | structSRe.inCeritarClient_NRI_Specific > 0)
                    {
                        structSRe.inCeritarClient_NRI_Specific = Int32.Parse(mcGrdClients[grdClients.Row, mintGrdClients_CeC_NRI_col]);
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

        bool IRevision.IsPreviousRevisionScriptsIncluded()
        {
            return !chkExcludePreviousRevScripts.Checked;
        }

        string IRevision.GetRevisionsToInclude()
        {
            return System.Text.RegularExpressions.Regex.Replace(txtRevisionIncluses.Text, @"\s+", "");
        }

        structClientAppRevision IRevision.GetSelectedClient()
        {
            structClientAppRevision structCAR = new structClientAppRevision();

            structCAR.Action = clsTTApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(grdClients[grdClients.Row, mintGrdClients_Action_col]);
            Int32.TryParse(mcGrdClients[grdClients.Row, mintGrdClients_CeC_NRI_col], out structCAR.intCeritarClient_NRI);
            Int32.TryParse(mcGrdClients[grdClients.Row, mintGrdClients_CAR_NRI_col], out structCAR.intClientAppRevision_NRI);
            structCAR.intClientAppRevision_TS = Int32.Parse(mcGrdClients[grdClients.Row, mintGrdClients_CAR_TS_col]);

            return structCAR;
        }

        List<structClientAppRevision> IRevision.GetClientsList()
        {
            List<structClientAppRevision> lstClient = new List<structClientAppRevision>();
            structClientAppRevision structCAR;

            for (int intRowIndex = 1; intRowIndex < grdClients.Rows.Count; intRowIndex++)
            {
                structCAR = new structClientAppRevision();

                structCAR.Action = clsTTApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(grdClients[intRowIndex, mintGrdClients_Action_col]);
                Int32.TryParse(mcGrdClients[intRowIndex, mintGrdClients_CeC_NRI_col], out structCAR.intCeritarClient_NRI);
                Int32.TryParse(mcGrdClients[intRowIndex, mintGrdClients_CAR_NRI_col], out structCAR.intClientAppRevision_NRI);
                structCAR.intClientAppRevision_TS = Int32.Parse(mcGrdClients[intRowIndex, mintGrdClients_CAR_TS_col]);

                lstClient.Add(structCAR);
            }

            return lstClient;
        }

        bool IRevision.GetScriptsOnly()
        {
            return chkScriptOnly.Checked;
        }

        bool IRevision.GetScriptsMerged()
        {
            return chkScriptMerged.Checked;
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
                    mintAttributedRevisionNo = SByte.Parse(txtRevisionNo.Text);

                    txtVersionNo.Text = sqlRecord["Ver_No"].ToString();
                    mintCeritarApplication_NRI = Int32.Parse(sqlRecord["CeA_NRI"].ToString());
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

                        cboTemplates.SelectedValue = Int32.Parse(sqlRecord["Tpl_NRI"].ToString());

                        dtpCreation.Value = DateTime.Parse(sqlRecord["Rev_DtCreation"].ToString());

                        optRptOnly.Checked = Convert.ToBoolean(sqlRecord["Rev_ExeIsReport"].ToString());
                        optExeAndRpt.Checked = Convert.ToBoolean(sqlRecord["Rev_ExeWithReport"].ToString());

                        chkPreparation.Checked = Convert.ToBoolean(sqlRecord["Rev_PreparationMode"].ToString());
                        chkPreparation.Enabled = chkPreparation.Checked;

                        if (sqlRecord["CeA_NRI_Master"] != DBNull.Value)
                            mintCeritarApplication_NRI_Master = Int32.Parse(sqlRecord["CeA_NRI_Master"].ToString());

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

                dtpCreation.CustomFormat = clsTTApp.GetAppController.str_Get_FRQC_DateTimeFormat;

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

        private bool pfblnGrdSatellites_Load(int vintGrdClient_RowToLoad = 0)
        { 
            bool blnValidReturn = false;
            int intRowToLoad = 0;

            try
            {
                if (vintGrdClient_RowToLoad > 0)
                {
                    intRowToLoad = vintGrdClient_RowToLoad;
                }
                else if (grdClients.Rows.Count > 1)
                {
                    intRowToLoad = grdClients.Row;
                }

                if (intRowToLoad >= 1 && !mcGrdClients.bln_CellIsEmpty(intRowToLoad, mintGrdClients_CeC_NRI_col))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    mcGrdSatellites.LstHostedCellControls = new List<HostedCellControl>();

                    blnValidReturn = mcGrdSatellites.bln_FillData(mcCtrRevision.strGetListe_SatelliteApps_SQL(Int32.Parse(mcGrdClients[intRowToLoad, mintGrdClients_CeC_NRI_col])));

                    mblnGrdSatellitesChangeMade = false;

                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    mcGrdSatellites.ClearGrid();
                    blnValidReturn = true;
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        private void ShowFolderBrowserDialog(ref TextBox txtAffected, Button rbtnSource, string vstrDialogDescription = "", bool vblnChangeMade = true)
        {
            DialogResult dialogResult;

            if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
            {
                folderBrowserDialog.ShowNewFolderButton = false;
                folderBrowserDialog.Description = vstrDialogDescription;

                if (rbtnSource.BackColor == Color.Yellow && !string.IsNullOrEmpty(txtAffected.Text)) //Disabled, c/'est fatiguant et inutile maintenant qu'on a le right click pour ouvrir l'explorer
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

                            if (((TextBox)rControl).Name == txtScriptsPath.Name)
                            {
                                btnShowScriptsFolder.FlatAppearance.BorderColor = Color.Yellow;
                                //if (txtScriptsPath.Text != string.Empty) chkScriptOnly.Enabled = true;
                            } 

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

                ShowFolderBrowserDialog(ref txtTemp, ((Button)sender));

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
                ShowOpenFileDialog("Executables (exe, apk)|*.exe;*.apk", grdSatellites, mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_ExeLocation_col], true);
            }

            mcGrdSatellites.GridIsLoading = false;
        }

        private bool pfblnExportRevisionKit()
        {
            bool blnValidReturn = false;
            TextBox txtTemp = new TextBox();

            try
            {
                ShowFolderBrowserDialog(ref txtTemp, btnExportRevision, "Sélectionnez l'emplacement où sauvegarder l'archive.", false);

                if (!string.IsNullOrEmpty(txtTemp.Text))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    blnValidReturn = mcCtrRevision.blnExportRevisionKit(txtTemp.Text);

                    Cursor.Current = Cursors.Default;

                    if (blnValidReturn)
                    {
                        MessageBox.Show("Export effectué avec SUCCÈS!" + Environment.NewLine + "À l'emplacement suivant : " + txtTemp.Text, "Message", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("ERREUR lors de l'export." + Environment.NewLine, "Message", MessageBoxButtons.OK);
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

        private bool pfblnGrdClients_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdClients.bln_FillData(mcCtrRevision.strGetListe_Clients_SQL(formController.Item_NRI));

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

        private bool pfblnEnableDisable_btnGrdClientsDelete(int vintCurrentRow)
        {
            bool blnValidReturn = true;

            try
            {
                if (mcGrdClients.bln_RowEditIsValid())
                {
                    if (string.IsNullOrEmpty(mcGrdClients[vintCurrentRow, mintGrdClients_Action_col]) || mcGrdClients[vintCurrentRow, mintGrdClients_Action_col] == sclsConstants.DML_Mode.INSERT_MODE.ToString())
                    {
                        btnGrdClientsDel.Enabled = true;
                    }
                    else
                    {
                        btnGrdClientsDel.Enabled = true;

                        for (int intIdx = 1; intIdx < grdSatellites.Rows.Count; intIdx++)
                        {
                            if (!mcGrdSatellites.bln_CellIsEmpty(intIdx, mintGrdSat_SRe_NRI_col) & (mcGrdSatellites[intIdx, mintGrdSat_CSV_ExePerCustomer_col] == "1" | grdClients.Rows.Count <= 2))
                            {
                                btnGrdClientsDel.Enabled = false;
                                break;
                            }
                        }

                        if (btnGrdClientsDel.Enabled)
                        {
                            int intNbRowRestant = 0;

                            for (int intIdx = 1; intIdx < grdClients.Rows.Count; intIdx++)
                            {
                                if (mcGrdClients[intIdx, mintGrdClients_Action_col] != sclsConstants.DML_Mode.DELETE_MODE.ToString())
                                {
                                    intNbRowRestant ++;
                                }
                            }

                            btnGrdClientsDel.Enabled = (intNbRowRestant > 1);
                        }
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

        #endregion


        private void formController_LoadData(LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;
            Button btnPlaceHolder = null;

            mblnAppExeLocationExistsOnLoad = false;

            mcGrdSatellites.LstHostedCellControls = new List<HostedCellControl>();
            mcGrdRevModifs.DefaultActionCol = mintGrdRevMod_Action_col;

            mstrVariousFileLocation = string.Empty;
            mstrVariousFolderLocation = string.Empty;
            btnSelectVariousFilePath.BackColor = System.Drawing.SystemColors.Control;
            btnSelectVariousFolderPath.BackColor = System.Drawing.SystemColors.Control;

            if (!mcGrdClients.bln_Init(ref grdClients, ref btnGrdClientsAdd, ref btnGrdClientsDel))
            { }
            if (!mcGrdRevModifs.bln_Init(ref grdRevModifs, ref btnGrdRevAdd, ref btnGrdRevDel))
            { }
            if (!mcGrdSatellites.bln_Init(ref grdSatellites, ref btnPlaceHolder, ref btnPlaceHolder))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrRevision.strGetTemplates_SQL(), "Tpl_NRI", "Tpl_Name", false, ref cboTemplates))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrRevision.strGetClients_SQL(formController.Item_NRI), "CeC_NRI", "CeC_Name", true, ref cboClients))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrRevision.strGetClients_SQL(formController.Item_NRI), "CeC_NRI", "CeC_Name", true, ref cboClientsRevModif))
            { }
            else if (!pfblnData_Load())
            { }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
            {
                blnValidReturn = true;
            }
            else if (!pfblnGrdClients_Load())
            { }       
            else if (!pfblnGrdRevModifs_Load())
            { }
            else if (!pfblnGrdSatellites_Load(mintGrdClient_SelectedRow))
            { }
            else
            {
                grdClients.Row = (mintGrdClient_SelectedRow >= grdClients.Rows.Count ? 1 : mintGrdClient_SelectedRow);

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

                        btnGrdClientsAdd.Focus();
                        //cboClients.Focus();
                        //cboClients.DroppedDown = true;
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

                    case ctr_Revision.ErrorCode_Rev.CLIENT_NAME_MANDATORY:

                        string strListOfClientSelected = string.Empty;

                        for (int intRowIdx = 1; intRowIdx < grdClients.Rows.Count; intRowIdx++)
                        {
                            strListOfClientSelected = strListOfClientSelected + (strListOfClientSelected == string.Empty ? mcGrdClients[intRowIdx, mintGrdClients_CeC_NRI_col] : "," + Microsoft.VisualBasic.Conversion.Val(mcGrdClients[intRowIdx, mintGrdClients_CeC_NRI_col]));
                        }

                        sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrRevision.strGetClients_SQL(formController.Item_NRI, strListOfClientSelected), "CeC_NRI", "Cec_Name", false, ref cboClients);

                        grdClients.Row = mcActionResults.RowInError;
                        grdClients.StartEditing();

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
#if DEBUG
                Console.WriteLine("Mode=Debug");
#else
                newThread = new Thread(() => frmWorking.ShowDialog());
                newThread.Start();
#endif
                if (mcGrdClients[grdClients.Row, mintGrdClients_Action_col] == sclsConstants.DML_Mode.DELETE_MODE.ToString())
                {
                    mintGrdClient_SelectedRow = 1;
                }
                else
                {
                    mintGrdClient_SelectedRow = grdClients.Row;
                }

                mcActionResults = mcCtrRevision.Save();

                if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE) formController.Item_NRI = mcActionResults.GetNewItem_NRI;

                if (!mcActionResults.IsValid)
                {
                    clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI, MessageBoxButtons.OK, mcActionResults.GetLstParams);
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

                        grdRevModifs.StartEditing(grdRevModifs.Row, mintGrdRevMod_RevM_ChangeDesc_col);

                        formController.ChangeMade = true;
                        break;

                    case mintGrdRevMod_CeC_Name_For_col:
                        if (formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
                        {
                            string strListOfClientSelected = mcGrdRevModifs[grdRevModifs.Row, mintGrdRevMod_CeC_NRI_For_col];

                            sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrRevision.strGetClients_SQL(formController.Item_NRI, strListOfClientSelected), "CeC_NRI", "Cec_Name", false, ref cboClientsRevModif);

                            grdRevModifs.StartEditing();
                        }

                        break;

                    case mintGrdRevMod_RevM_DtHr_col:
                        if (formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
                        {

                            grdRevModifs.StartEditing();
                        }

                        break;
                }
            }
        }

        private void mcGrdRevModifs_AfterClickAdd()
        {
            grdRevModifs.StartEditing(grdRevModifs.Row, mintGrdRevMod_RevM_ChangeDesc_col);

            grdRevModifs[grdRevModifs.Row, mintGrdRevMod_Action_col] = sclsConstants.DML_Mode.INSERT_MODE.ToString();
            grdRevModifs[grdRevModifs.Row, mintGrdRevMod_RevM_DtHr_col] = DateTime.Now;
            grdRevModifs[grdRevModifs.Row, mintGrdRevMod_CeC_NRI_For_col] = grdClients[grdClients.Row, mintGrdClients_CeC_NRI_col];
            grdRevModifs[grdRevModifs.Row, mintGrdRevMod_CeC_Name_For_col] = grdClients[grdClients.Row, mintGrdClients_CeC_Name_col];

            formController.ChangeMade = true;
        }

        void mcGrdSatellites_SetGridDisplay()
        {
            grdSatellites.Cols[mintGrdSat_CSA_Name_col].Width = 250;
            grdSatellites.Cols[mintGrdSat_CSA_ExeLocation_col].Width = 30;

            grdSatellites.Cols[mintGrdSat_CSV_ExePerCustomer_col].DataType = typeof(bool);

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
            ShowFolderBrowserDialog(ref txtScriptsPath, btnSelectScriptsFolderPath);
        }

        private void btnSelectExecutableFolderPath_Click(object sender, EventArgs e)
        {
            ShowFolderBrowserDialog(ref txtReleasePath, btnSelectExecutableFolderPath);
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
            if (!formController.FormIsLoading & cboClients.SelectedIndex > -1)
            {
                grdClients[grdClients.Row, mintGrdClients_CeC_NRI_col] = cboClients.SelectedValue;

                if (grdSatellites.Rows.Count <= 1)
                    pfblnGrdSatellites_Load();

                formController.ChangeMade = true;
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
                    chkScriptOnly.Enabled = false;
                    chkScriptMerged.Enabled = false;

                    if (mintCeritarApplication_NRI_Master > 0)
                    {
                        gbScripts.Enabled = false;
                        chkExcludePreviousRevScripts.Enabled = false;
                    }

                    break;

                case (int)sclsConstants.DML_Mode.UPDATE_MODE:

                    cboTemplates.Enabled = false;
                    txtCreatedBy.ReadOnly = true;
                    btnSelectVariousFilePath.Enabled = true;
                    btnSelectVariousFolderPath.Enabled = true;
                    btnShowRootFolder.Enabled = true;
                    btnExportRevision.Enabled = true;

                    chkScriptMerged.Enabled = false;

                    if (txtScriptsPath.Text != string.Empty)
                    {
                        chkScriptOnly.Enabled = true;
                    }

                    if (mintCeritarApplication_NRI_Master > 0)
                    {
                        gbScripts.Enabled = false;
                        chkExcludePreviousRevScripts.Enabled = false;
                    }

                    break;

                case (int)sclsConstants.DML_Mode.DELETE_MODE: case (int)sclsConstants.DML_Mode.CONSULT_MODE:
                    btnGrdClientsAdd.Enabled = false;
                    btnGrdClientsDel.Enabled = false;

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

            ShowFolderBrowserDialog(ref txtPlaceHolder, btnSelectVariousFolderPath);

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

                txtRevisionNo.Text = string.Empty;
            }
            else
            {
                gbExe.Enabled = true;
                gbSatellites.Enabled = true;
                if(mintCeritarApplication_NRI_Master==0) gbScripts.Enabled = true;

                btnShowRootFolder.Enabled = true;
                btnPrintPairValidation.Enabled = true;
                btnExportRevision.Enabled = true;

                txtRevisionNo.Text = mintAttributedRevisionNo.ToString();
            }
        }

        private void mnuCopyLines_Click(object sender, EventArgs e)
        {
            if (grdRevModifs.Row > 0)
            {
                string strToCopy = string.Empty;

                for (int intRowIndex = grdRevModifs.Row; intRowIndex < grdRevModifs.Rows.Count; intRowIndex++)
                {
                    strToCopy += mcGrdRevModifs[intRowIndex, mintGrdRevMod_RevM_ChangeDesc_col];

                    if (intRowIndex != grdRevModifs.Rows.Count-1) strToCopy += Environment.NewLine;
                }

                Clipboard.Clear();
                Clipboard.SetText(strToCopy);
            }
        }

        private void chkExcludePreviousRevScripts_CheckedChanged(object sender, EventArgs e)
        {
            txtRevisionIncluses.Enabled = !chkExcludePreviousRevScripts.Checked;

            chkScriptMerged.Enabled = !chkExcludePreviousRevScripts.Checked;
        }

        private void txtRevisionIncluses_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtRevisionIncluses.Text != string.Empty && !System.Text.RegularExpressions.Regex.IsMatch(txtRevisionIncluses.Text, "^[1-9-;,]"))
            {
                clsTTApp.GetAppController.ShowMessage(mintMSG_ValidCharacters);
                e.Cancel = true;
            }
        }

        private void grdClients_Click(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                mintGrdClient_SelectedRow = grdClients.Row;
            }
        }

        private void grdClients_DoubleClick(object sender, EventArgs e)
        {
            if (grdClients.Rows.Count > 1 & grdClients.Row > 0)
            {
                switch (grdClients.Col)
                {
                    case mintGrdClients_CeC_Name_col:

                        if (mcGrdClients[grdClients.Row, mintGrdClients_Action_col] == sclsConstants.DML_Mode.INSERT_MODE.ToString() || chkPreparation.Checked)
                        {
                            string strListOfClientSelected = string.Empty;

                            for (int intRowIdx = 1; intRowIdx < grdClients.Rows.Count; intRowIdx++)
                            {
                                strListOfClientSelected = strListOfClientSelected + (strListOfClientSelected == string.Empty ? mcGrdClients[intRowIdx, mintGrdClients_CeC_NRI_col] : "," + Microsoft.VisualBasic.Conversion.Val(mcGrdClients[intRowIdx, mintGrdClients_CeC_NRI_col]));
                            }

                            sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrRevision.strGetClients_SQL(formController.Item_NRI, strListOfClientSelected), "CeC_NRI", "Cec_Name", false, ref cboClients);

                            grdClients.StartEditing();
                        }

                        break;
                }
            }
        }

        private void btnGrdClientsDel_Click(object sender, EventArgs e)
        {
            if (mcGrdClients.bln_RowEditIsValid() && mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.DELETE_MODE.ToString())
            {
                mcGrdSatellites.ClearGrid();
            }
        }

        private void grdClients_AfterRowColChange(object sender, RangeEventArgs e)
        {
            if (grdClients.Rows.Count > 1 && grdClients.Row > 0 && e.OldRange.r1 != e.NewRange.r2)
            {
                GrdClientRowColChangeActions();
            }
            else
            {
                //Do nothing
            }

            pfblnEnableDisable_btnGrdClientsDelete(e.NewRange.r1);
        }

        private void GrdClientRowColChangeActions()
        {
            bool blnEnableControls = false;

            if (!formController.FormIsLoading)
            {
                pfblnGrdSatellites_Load();

                btnGenerate.Enabled = mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.INSERT_MODE.ToString() && mcGrdClients.bln_RowEditIsValid() && formController.FormMode != sclsConstants.DML_Mode.INSERT_MODE;
            }

            if (mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.INSERT_MODE.ToString())
            {
                blnEnableControls = true;
            }
            else
            {
                blnEnableControls = false;
            }      

            btnExportRevision.Enabled = blnEnableControls;
        }

        private void grdClients_BeforeRowColChange(object sender, RangeEventArgs e)
        {
            if (!formController.FormIsLoading && (mblnGrdSatellitesChangeMade || mcGrdClients[e.OldRange.r1, mintGrdClients_Action_col] == sclsConstants.DML_Mode.DELETE_MODE.ToString() || mcGrdClients[e.OldRange.r1, mintGrdClients_Action_col] == sclsConstants.DML_Mode.INSERT_MODE.ToString()) & e.NewRange.r1 != e.OldRange.r1)
            {
                DialogResult msgResult = clsTTApp.GetAppController.ShowMessage(mintMSG_ChangesWillBeLostOnRowChange, MessageBoxButtons.YesNo);

                if (msgResult == System.Windows.Forms.DialogResult.Yes)
                {
                    formController.FormIsLoading = true;
                    pfblnGrdClients_Load();
                    pfblnGrdSatellites_Load(e.NewRange.r1);
                    formController.FormIsLoading = false;

                    btnGenerate.Enabled = true;
                }

                if (msgResult == DialogResult.No) e.Cancel = true;
            }
        }

        private void cboClientsRevModif_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading & cboClientsRevModif.SelectedIndex > -1)
            {
                grdRevModifs[grdRevModifs.Row, mintGrdRevMod_CeC_NRI_For_col] = cboClientsRevModif.SelectedValue;

                formController.ChangeMade = true;
            }
        }

        private void dtpRevModif_ValueChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading & cboClientsRevModif.SelectedIndex > -1)
            {
                grdRevModifs[grdRevModifs.Row, mintGrdRevMod_RevM_DtHr_col] = dtpRevModif.Value;

                formController.ChangeMade = true;
            }
        }
    }
}
