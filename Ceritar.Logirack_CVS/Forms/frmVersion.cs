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
using System.Threading;

namespace Ceritar.Logirack_CVS.Forms
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
        private const short mintGrdClients_LatestRPTExe_col = 8;
        private const short mintGrdClients_LocationReportExe_col = 9;
        private const short mintGrdClients_License_col = 10;
        private const short mintGrdClients_Selection_col = 11;
        private const short mintGrdClients_LocationScriptsRoot_col = 12;
        private const short mintGrdClients_DateInstalled_col = 13; 

        //Columns grdSatellites
        private const short mintGrdSat_Action_col = 1;
        private const short mintGrdSat_CSA_NRI_col = 2;
        private const short mintGrdSat_CSA_TS_col = 3;
        private const short mintGrdSat_CSA_Name_col = 4;
        private const short mintGrdSat_CSA_LatestExe_col = 5;
        private const short mintGrdSat_CSA_KitFolderName_col = 6;
        private const short mintGrdSat_CSA_ExeIsFolder_col = 7;
        private const short mintGrdSat_CSV_NRI_col = 8;
        private const short mintGrdSat_CSV_LocationExe_col = 9;
        private const short mintGrdSat_CSV_ExePerCustomer_col = 10;

        //Columns grdRev
        private const short mintGrdRev_Rev_NRI_col = 1;
        private const short mintGrdRev_Rev_TS_col = 2;
        private const short mintGrdRev_Rev_IsValid_col = 3;
        private const short mintGrdRev_Number_col = 4;
        private const short mintGrdRev_CeritarClientName_col = 5;
        private const short mintGrdRev_Description_col = 6;
        private const short mintGrdRev_CreationDate_col = 7;
        private const short mintGrdRev_InPreparation_col = 8;

        //Tab pages
        private const short mintTab_Version = 0;
        private const short mintTab_Revision = 1;

        //Classes
        public clsTTC1FlexGridWrapper mcGrdClients;
        public clsTTC1FlexGridWrapper mcGrdSatelliteApps;
        public clsTTC1FlexGridWrapper mcGrdRevisions;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Messages
        private const int mintMSG_ChangesWillBeLostOnRowChange = 27;
        private const int mintMSG_ApplicationNotExistsInSystem = 38;
        private const int mintMSG_AreYouSure = 49;

        //Working variables
        private ushort mintVersion_TS;
        private bool mblnGrdSatellitesChangeMade;
        private string mstrExternalReportAppName;
        private string mstrVariousFileLocation;
        private string mstrVariousFolderLocation;
        private int mintGrdClient_SelectedRow = 1;
        private int mintCreatedByUser_NRI;
        private int mintCeritarApplication_NRI_Master;
        private string mstrLatestFilePathUsed = string.Empty;
        private string mstrLatestFolderPathUsed = string.Empty;
        
        

        public frmVersion()
        {
            InitializeComponent();

            mcCtrVersion = new ctr_Version((IVersion)this);

            mcGrdClients = new clsTTC1FlexGridWrapper();
            mcGrdClients.SetGridDisplay += mcGrdClients_SetGridDisplay;
            mcGrdClients.AfterClickAdd += mcGrdClients_AfterClickAdd;
            mcGrdClients.BeforeClickAdd += mcGrdClients_BeforeClickAdd;

            mcGrdSatelliteApps = new clsTTC1FlexGridWrapper();
            mcGrdSatelliteApps.SetGridDisplay += mcGrdSatelliteApps_SetGridDisplay;

            mcGrdRevisions = new clsTTC1FlexGridWrapper();
            mcGrdRevisions.SetGridDisplay +=mcGrdRevisions_SetGridDisplay;

            dtpCreation.Value = DateTime.Now;
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

        int IVersion.GetCeritarApplication_NRI_Master()
        {
            return mintCeritarApplication_NRI_Master;
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
            return dtpCreation.Value.ToString(clsTTApp.GetAppController.str_GetServerDateTimeFormat);
        }

        List<structClientAppVersion> IVersion.GetClientsList()
        {
            List<structClientAppVersion> lstClient = new List<structClientAppVersion>();
            structClientAppVersion structCAV;

            for (int intRowIndex = 1; intRowIndex < grdClients.Rows.Count; intRowIndex++)
            {
                structCAV = new structClientAppVersion();

                structCAV.Action = clsTTApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(grdClients[intRowIndex, mintGrdClients_Action_col]);
                structCAV.strDateInstalled = (Convert.ToBoolean(grdClients[intRowIndex, mintGrdClients_Installed_col]) ? DateTime.Now.ToString() : string.Empty);
                structCAV.blnIsCurrentVersion = Convert.ToBoolean(grdClients[intRowIndex, mintGrdClients_IsCurrentVersion_col]);
                structCAV.strCeritarClient_Name = mcGrdClients[intRowIndex, mintGrdClients_CeC_Name_col];
                Int32.TryParse(mcGrdClients[intRowIndex, mintGrdClients_CeC_NRI_col], out structCAV.intCeritarClient_NRI);
                Int32.TryParse(mcGrdClients[intRowIndex, mintGrdClients_CAV_NRI_col], out structCAV.intClientAppVersion_NRI);
                structCAV.intClientAppVersion_TS = Int32.Parse(mcGrdClients[intRowIndex, mintGrdClients_CAV_TS_col]);
                structCAV.strLicense = grdClients[intRowIndex, mintGrdClients_License_col].ToString();
                structCAV.strLocationReportExe = mcGrdClients[intRowIndex, mintGrdClients_LocationReportExe_col];
                structCAV.strLocationScriptsRoot = mcGrdClients[intRowIndex, mintGrdClients_LocationScriptsRoot_col] == "" ? ""/*System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)*/ : mcGrdClients[intRowIndex, mintGrdClients_LocationScriptsRoot_col];

                lstClient.Add(structCAV);
            }

            return lstClient;
        }

        List<structClientSatVersion> IVersion.GetClientSatellitesList(int vintCeritarClient_NRI)
        {
            List<structClientSatVersion> lstSatelliteApps = new List<structClientSatVersion>();
            structClientSatVersion structCSV;

            for (int intRowIndex = 1; intRowIndex < grdSatellite.Rows.Count; intRowIndex++)
            {
                if (!mcGrdSatelliteApps.bln_CellIsEmpty(intRowIndex, mintGrdSat_CSV_LocationExe_col))
                {
                    if (mcGrdClients[grdClients.Row, mintGrdClients_CeC_NRI_col].ToString() == vintCeritarClient_NRI.ToString() || vintCeritarClient_NRI == 0)
                    {
                        structCSV = new structClientSatVersion();

                        structCSV.Action = clsTTApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(grdSatellite[intRowIndex, mintGrdSat_Action_col]);
                        structCSV.intCeritarSatelliteApp_NRI = Int32.Parse(mcGrdSatelliteApps[intRowIndex, mintGrdSat_CSA_NRI_col]);
                        structCSV.intCeritarClient_NRI = Int32.Parse(mcGrdClients[grdClients.Row, mintGrdClients_CeC_NRI_col]);
                        Int32.TryParse(mcGrdSatelliteApps[intRowIndex, mintGrdSat_CSV_NRI_col], out structCSV.intClientSatVersion_NRI);
                        structCSV.strCeritarClient_Name = mcGrdClients[grdClients.Row, mintGrdClients_CeC_Name_col];
                        structCSV.strLocationSatelliteExe = mcGrdSatelliteApps[intRowIndex, mintGrdSat_CSV_LocationExe_col];
                        structCSV.strCeritarSatelliteApp_Name = mcGrdSatelliteApps[intRowIndex, mintGrdSat_CSA_Name_col];
                        structCSV.strKitFolderName = mcGrdSatelliteApps[intRowIndex, mintGrdSat_CSA_KitFolderName_col];
                        structCSV.blnExeIsFolder = Convert.ToBoolean(mcGrdSatelliteApps[intRowIndex, mintGrdSat_CSA_ExeIsFolder_col]);
                        structCSV.blnExePerCustomer = Convert.ToBoolean(mcGrdSatelliteApps[intRowIndex, mintGrdSat_CSV_ExePerCustomer_col]);

                        lstSatelliteApps.Add(structCSV);
                    }                    
                }
            }

            return lstSatelliteApps;
        }

        bool IVersion.GetIsDemo()
        {
            return chkDemoVersion.Checked;
        }

        bool IVersion.GetIncludeScriptsOnRefresh()
        {
            return chkIncludeScripts.Checked;
        }

        structClientAppVersion IVersion.GetSelectedClient()
        {
            structClientAppVersion structCAV = new structClientAppVersion();

            structCAV.Action = clsTTApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(grdClients[grdClients.Row, mintGrdClients_Action_col]);
            structCAV.strDateInstalled = (Convert.ToBoolean(grdClients[grdClients.Row, mintGrdClients_Installed_col]) ? DateTime.Now.ToString() : string.Empty);
            structCAV.blnIsCurrentVersion = Convert.ToBoolean(grdClients[grdClients.Row, mintGrdClients_IsCurrentVersion_col]);
            structCAV.strCeritarClient_Name = mcGrdClients[grdClients.Row, mintGrdClients_CeC_Name_col];
            Int32.TryParse(mcGrdClients[grdClients.Row, mintGrdClients_CeC_NRI_col], out structCAV.intCeritarClient_NRI);
            Int32.TryParse(mcGrdClients[grdClients.Row, mintGrdClients_CAV_NRI_col], out structCAV.intClientAppVersion_NRI);
            structCAV.intClientAppVersion_TS = Int32.Parse(mcGrdClients[grdClients.Row, mintGrdClients_CAV_TS_col]);
            structCAV.strLicense = grdClients[grdClients.Row, mintGrdClients_License_col].ToString();
            structCAV.strLocationReportExe = mcGrdClients[grdClients.Row, mintGrdClients_LocationReportExe_col];
            structCAV.strLocationScriptsRoot = mcGrdClients[grdClients.Row, mintGrdClients_LocationScriptsRoot_col];

            return structCAV;
        }

        string IVersion.GetLocation_VariousFile()
        {
            return mstrVariousFileLocation;
        }

        string IVersion.GetLocation_VariousFolder()
        {
            return mstrVariousFolderLocation;
        }

        string IVersion.GetDescription()
        {
            return txtDescription.Text;
        }

        string IVersion.GetLatestVersionNo()
        {
            return clsTTApp.GetAppController.str_ShowInputMessage(mintMSG_ApplicationNotExistsInSystem, "Attention", "", mcGrdClients[grdClients.Row, mintGrdClients_CeC_Name_col]);
        }

        string IVersion.GetExternalRPTAppName()
        {
            return mstrExternalReportAppName;
        }

        int IVersion.GetCreatedByUser_NRI()
        {
            return (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE? clsTTApp.GetAppController.cUser.User_NRI : mintCreatedByUser_NRI);
        }

        bool IVersion.GetIsBaseKit()
        {
            return chkBaseKit.Checked;
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

        private bool pfblnGrdSatelliteApps_Load(int vintGrdClient_RowToLoad = 0)
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

                    mcGrdSatelliteApps.LstHostedCellControls = new List<HostedCellControl>();

                    blnValidReturn = mcGrdSatelliteApps.bln_FillData(mcCtrVersion.strGetListe_SatelliteApps_SQL(formController.Item_NRI, Int32.Parse(mcGrdClients[intRowToLoad, mintGrdClients_CeC_NRI_col])));

                    mblnGrdSatellitesChangeMade = false;

                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    blnValidReturn = true;
                    mcGrdSatelliteApps.ClearGrid();
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
            ctr_Revision cCtrRevision = new ctr_Revision(null);

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                grdRevisions.BeginUpdate();

                blnValidReturn = mcGrdRevisions.bln_FillData(mcCtrVersion.strGetListe_Revisions_SQL(formController.Item_NRI));

                if (grdRevisions.Rows.Count > 1)
                {
                    btnGrdRevDel.Enabled = true;
                    btnGrdRevUpdate.Enabled = mcGrdClients.bln_RowEditIsValid();

                    grdRevisions.Row = 1;
   
                    grdRevisions.Cols[mintGrdRev_Rev_IsValid_col].ImageAlign = ImageAlignEnum.CenterTop;

                    for (int intRowIndex = 1; intRowIndex < grdRevisions.Rows.Count; intRowIndex++)
                    {
                        if (mcGrdRevisions.CellIsChecked(intRowIndex, mintGrdRev_InPreparation_col))
                        {
                            grdRevisions.SetCellImage(intRowIndex, mintGrdRev_Rev_IsValid_col, Properties.Resources.Under_construction);
                        }
                        else if (cCtrRevision.blnRevisionPathIsValid(Convert.ToInt32(grdRevisions[intRowIndex, mintGrdRev_Rev_NRI_col])))
                        {
                            grdRevisions.SetCellImage(intRowIndex, mintGrdRev_Rev_IsValid_col, Properties.Resources.valid);
                        }
                        else
                        {
                            grdRevisions.SetCellImage(intRowIndex, mintGrdRev_Rev_IsValid_col, Properties.Resources.invalid);
                        }                  
                    }
                }
                else
                {
                    btnGrdRevDel.Enabled = false;
                    btnGrdRevUpdate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            grdRevisions.EndUpdate();

            return blnValidReturn;
        }

        private bool pfblnData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;
            string strAppChangeLocation = string.Empty;

            mblnGrdSatellitesChangeMade = false;
            
            try
            {
                sqlRecord = clsTTSQL.ADOSelect(mcCtrVersion.strGetDataLoad_SQL(formController.Item_NRI));

                if (sqlRecord.Read())
                {
                    UInt16.TryParse(sqlRecord["Ver_TS"].ToString(), out mintVersion_TS);
                    Int32.TryParse(sqlRecord["TTU_NRI"].ToString(), out mintCreatedByUser_NRI);

                    txtCreatedBy.Text = sqlRecord["CreatedByNom"].ToString();
                    txtCompiledBy.Text = sqlRecord["Ver_CompiledBy"].ToString();
                    txtVersionNo.Text = sqlRecord["Ver_No"].ToString();

                    if (sqlRecord["Ver_Description"] != DBNull.Value)
                        txtDescription.Text = sqlRecord["Ver_Description"].ToString();

                    strAppChangeLocation = sqlRecord["Ver_AppChange_Location"].ToString();

                    if (!string.IsNullOrEmpty(strAppChangeLocation))
                    {
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
                    }
                    
                    txtReleasePath.Text = sqlRecord["Ver_Release_Location"].ToString();
                    txtTTAppPath.Text = sqlRecord["Ver_CaptionsAndMenus_Location"].ToString();

                    dtpCreation.Value = DateTime.Parse(sqlRecord["Ver_DtCreation"].ToString());

                    cboApplications.SelectedValue = Int32.Parse(sqlRecord["CeA_NRI"].ToString());

                    sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetTemplates_SQL((int)cboApplications.SelectedValue), "Tpl_NRI", "Tpl_Name", false, ref cboTemplates);

                    cboTemplates.SelectedValue = Int32.Parse(sqlRecord["Tpl_NRI"].ToString());

                    chkDemoVersion.Checked = Convert.ToBoolean(sqlRecord["Ver_IsDemo"]);

                    mstrExternalReportAppName = sqlRecord["CeA_ExternalRPTAppName"].ToString();

                    if (sqlRecord["CeA_NRI_Master"] != DBNull.Value)
                        mintCeritarApplication_NRI_Master = Int32.Parse(sqlRecord["CeA_NRI_Master"].ToString());

                    if (mintCeritarApplication_NRI_Master > 0) chkIncludeScripts.Enabled = false;

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

        private Button pfblnGetNewLocationReportButton()
        {
            Button btnLocationReport = new Button();

            try
            {
                btnLocationReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                btnLocationReport.Image = ((System.Drawing.Image)(Properties.Resources.ellipsis));
                btnLocationReport.UseVisualStyleBackColor = true;
                btnLocationReport.FlatStyle = FlatStyle.Flat;

                btnLocationReport.Click += btnReplaceReportExe_Click;
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return btnLocationReport;
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

                btnLocationSatExe.Click += btnReplaceSatelliteExe_Click;
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return btnLocationSatExe;
        }

        private void ShowOpenFileDialog(string vstrExtensionsFilter, Object rControl, string vstrInitialDirectory = "", bool vblnShowFile = false)
        {
            DialogResult dialogResult;
            
            try
            {
                if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
                {
                    if (vblnShowFile && File.Exists(vstrInitialDirectory))
                    {
                        openFileDialog.FileName = vstrInitialDirectory;

                        vstrInitialDirectory = Path.GetDirectoryName(vstrInitialDirectory);
                    }
                    else
                    {
                        openFileDialog.FileName = string.Empty;

                        vstrInitialDirectory = mstrLatestFilePathUsed == string.Empty ? System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : mstrLatestFilePathUsed;
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
                            if (((C1FlexGrid)rControl).Name == grdClients.Name)
                            {
                                if (!mcGrdClients[grdClients.Row, mintGrdClients_LocationReportExe_col].Equals(openFileDialog.FileName))
                                {
                                    ((HostedCellControl)mcGrdClients.LstHostedCellControls[grdClients.Row - 1]).GetCellControl.BackColor = System.Drawing.Color.Yellow;
                                    ((C1FlexGrid)rControl)[grdClients.Row, mintGrdClients_LocationReportExe_col] = openFileDialog.FileName;
                                }
                            }

                            if (((C1FlexGrid)rControl).Name == grdSatellite.Name)
                            {
                                if (mcGrdSatelliteApps.bln_CellIsEmpty(grdSatellite.Row, mintGrdSat_CSV_NRI_col) & mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col] != openFileDialog.FileName)
                                {
                                    grdSatellite[grdSatellite.Row, mintGrdSat_Action_col] = sclsConstants.DML_Mode.INSERT_MODE;

                                    ((HostedCellControl)mcGrdSatelliteApps.LstHostedCellControls[grdSatellite.Row - 1]).GetCellControl.BackColor = System.Drawing.Color.Yellow;

                                    mblnGrdSatellitesChangeMade = true;
                                } 
                                else if (mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col] != openFileDialog.FileName)
                                {
                                    grdSatellite[grdSatellite.Row, mintGrdSat_Action_col] = sclsConstants.DML_Mode.UPDATE_MODE;

                                    ((HostedCellControl)mcGrdSatelliteApps.LstHostedCellControls[grdSatellite.Row - 1]).GetCellControl.BackColor = System.Drawing.Color.Yellow;

                                    mblnGrdSatellitesChangeMade = true;
                                }

                                ((C1FlexGrid)rControl)[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col] = openFileDialog.FileName;
                            }
                        }

                        mstrLatestFilePathUsed = openFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }         
        }

        private void ShowFolderBrowserDialog(ref TextBox rtxtAffected, Button rbtnSource, string vstrDialogDescription = "")
        {
            DialogResult dialogResult;

            try
            {
                if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
                {
                    folderBrowserDialog.ShowNewFolderButton = false;
                    folderBrowserDialog.Description = vstrDialogDescription;

                    if (rbtnSource.BackColor == System.Drawing.Color.Yellow && !string.IsNullOrEmpty(rtxtAffected.Text)) //Disabled, c/'est fatiguant et inutile maintenant qu'on a le right click pour ouvrir l'explorer
                    {
                        folderBrowserDialog.SelectedPath = rtxtAffected.Text;
                    }
                    else
                    {
                        if (mstrLatestFolderPathUsed == string.Empty)
                        {
                            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                            folderBrowserDialog.SelectedPath = string.Empty;
                        }
                        else
                        {
                            folderBrowserDialog.SelectedPath = mstrLatestFolderPathUsed;
                        }
                    }
                    
                    dialogResult = folderBrowserDialog.ShowDialog();

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        rtxtAffected.Text = folderBrowserDialog.SelectedPath;

                        mstrLatestFolderPathUsed = folderBrowserDialog.SelectedPath;
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
            int intCeritarClient_NRI;
            int intSelectedRow;
            TextBox txtTemp = new TextBox();

            try
            {
                ShowFolderBrowserDialog(ref txtTemp, btnExportInstallationKit, "Sélectionnez l'emplacement où sauvegarder l'archive.");

                intSelectedRow = grdClients.Row; //FindRow(true, 1, mintGrdClients_Selection_col, false);

                if (!string.IsNullOrEmpty(txtTemp.Text) & intSelectedRow > 0)
                {
                    blnValidReturn = Int32.TryParse(mcGrdClients[intSelectedRow, mintGrdClients_CeC_NRI_col], out intCeritarClient_NRI);

                    if (blnValidReturn)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        grdClients.Row = intSelectedRow;
                        grdClients.Refresh();

                        frmWorkInProgress frmWorking = new frmWorkInProgress();
                        Thread newThread = new Thread(() => frmWorking.ShowDialog());

                        newThread.Start();

                        blnValidReturn = mcCtrVersion.blnExportVersionInstallationKit((int)cboTemplates.SelectedValue, intCeritarClient_NRI, txtTemp.Text);

                        if (newThread != null)
                            newThread.Abort();

                        if (blnValidReturn)
                        {
                            MessageBox.Show("Export effectué avec succès!" + Environment.NewLine + "À l'emplacement suivant : " + txtTemp.Text, "Message" ,MessageBoxButtons.OK);
                        }

                        Cursor.Current = Cursors.Default;
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


        void mcGrdClients_SetGridDisplay()
        {
            grdClients.Cols[mintGrdClients_CeC_Name_col].Width = 220;
            grdClients.Cols[mintGrdClients_Installed_col].Width = 49;
            grdClients.Cols[mintGrdClients_IsCurrentVersion_col].Width = 51;
            grdClients.Cols[mintGrdClients_LocationReportExe_col].Width = 45;
            //grdClients.Cols[mintGrdClients_Selection_col].Width = 20;
            grdClients.Cols[mintGrdClients_LatestRPTExe_col].Width = 120;

            grdClients.Cols[mintGrdClients_IsCurrentVersion_col].DataType = typeof(bool);
            grdClients.Cols[mintGrdClients_Installed_col].DataType = typeof(bool);
            grdClients.Cols[mintGrdClients_Selection_col].DataType = typeof(bool);

            grdClients.Cols[mintGrdClients_LocationReportExe_col].Visible = !string.IsNullOrEmpty(mstrExternalReportAppName);

            if (grdClients.Rows.Count > 1)
            {
                cboClients.Visible = true;

                for (int intRowIndex = 1; intRowIndex < grdClients.Rows.Count; intRowIndex++)
                {
                    mcGrdClients.LstHostedCellControls.Add(new HostedCellControl(grdClients, pfblnGetNewLocationReportButton(), intRowIndex, mintGrdClients_LocationReportExe_col));

                    if (!mcGrdClients.bln_CellIsEmpty(intRowIndex, mintGrdClients_LocationReportExe_col))
                    {
                        //mcGrdClients.LstHostedCellControls[intRowIndex - 1].GetCellControl.BackColor = System.Drawing.Color.Yellow;

                        if (Directory.Exists(mcGrdClients[intRowIndex, mintGrdClients_LocationReportExe_col]) || File.Exists(mcGrdClients[intRowIndex, mintGrdClients_LocationReportExe_col]))
                        {
                            ((Button)mcGrdClients.LstHostedCellControls[intRowIndex - 1].GetCellControl).BackColor = System.Drawing.Color.Lime;
                        }
                        else if (!string.IsNullOrEmpty(mcGrdClients[intRowIndex, mintGrdClients_LocationReportExe_col]))
                        {
                            ((Button)mcGrdClients.LstHostedCellControls[intRowIndex - 1].GetCellControl).BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            ((Button)mcGrdClients.LstHostedCellControls[intRowIndex - 1].GetCellControl).BackColor = System.Drawing.SystemColors.Control;
                        }
                    }
                }   
            }

            mcGrdClients.SetColType_ComboBox(ref cboClients, mcCtrVersion.strGetClients_SQL(), mintGrdClients_CeC_Name_col, "CeC_NRI", "CeC_Name", false);
        }

        void mcGrdSatelliteApps_SetGridDisplay()
        {
            grdSatellite.Cols[mintGrdSat_CSA_Name_col].Width = 220;
            grdSatellite.Cols[mintGrdSat_CSA_LatestExe_col].Width = 120;
            grdSatellite.Cols[mintGrdSat_CSV_LocationExe_col].Width = 17;

            if (grdSatellite.Rows.Count > 1)
            {
                for (int intRowIndex = 1; intRowIndex < grdSatellite.Rows.Count; intRowIndex++)
                {
                    mcGrdSatelliteApps.LstHostedCellControls.Add(new HostedCellControl(grdSatellite, pfblnGetNewLocationSatelliteExeButton(), intRowIndex, mintGrdSat_CSV_LocationExe_col));

                    if (!mcGrdSatelliteApps.bln_CellIsEmpty(intRowIndex, mintGrdSat_CSV_LocationExe_col))
                    {
                        //mcGrdSatelliteApps.LstHostedCellControls[intRowIndex - 1].GetCellControl.BackColor = System.Drawing.Color.Yellow;

                        if (Directory.Exists(mcGrdSatelliteApps[intRowIndex, mintGrdSat_CSV_LocationExe_col]) || File.Exists(mcGrdSatelliteApps[intRowIndex, mintGrdSat_CSV_LocationExe_col]))
                        {
                            ((Button)mcGrdSatelliteApps.LstHostedCellControls[intRowIndex - 1].GetCellControl).BackColor = System.Drawing.Color.Lime;
                        }
                        else if (!string.IsNullOrEmpty(mcGrdSatelliteApps[intRowIndex, mintGrdSat_CSV_LocationExe_col]))
                        {
                            ((Button)mcGrdSatelliteApps.LstHostedCellControls[intRowIndex - 1].GetCellControl).BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            ((Button)mcGrdSatelliteApps.LstHostedCellControls[intRowIndex - 1].GetCellControl).BackColor = System.Drawing.SystemColors.Control;
                        }
                    }
                }
            }
        }

        void mcGrdRevisions_SetGridDisplay()
        {
            grdRevisions.Cols[mintGrdRev_Number_col].Width = 65;
            grdRevisions.Cols[mintGrdRev_Rev_IsValid_col].Width = 45;
            grdRevisions.Cols[mintGrdRev_CeritarClientName_col].Width = 175;
            grdRevisions.Cols[mintGrdRev_Description_col].Width = 750;

            grdRevisions.Cols[mintGrdRev_InPreparation_col].DataType = typeof(bool);
        }

        private void formController_LoadData(LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;
            Button btnPlaceHolder = null;

            mcGrdClients.LstHostedCellControls = new List<HostedCellControl>();
            mcGrdSatelliteApps.LstHostedCellControls = new List<HostedCellControl>();

            mstrVariousFileLocation = string.Empty;
            mstrVariousFolderLocation = string.Empty;
            btnSelectVariousFilePath.BackColor = System.Drawing.SystemColors.Control;
            btnSelectVariousFolderPath.BackColor = System.Drawing.SystemColors.Control;
            
            if (!mcGrdClients.bln_Init(ref grdClients, ref btnGrdClientsAdd, ref btnGrdClientsDel))
            { }
            if (!mcGrdSatelliteApps.bln_Init(ref grdSatellite, ref btnPlaceHolder, ref btnPlaceHolder))
            { }
            if (!mcGrdRevisions.bln_Init(ref grdRevisions, ref btnPlaceHolder, ref btnPlaceHolder))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetApplications_SQL(), "CeA_NRI", "CeA_Name", false, ref cboApplications))
            { }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
            {
                blnValidReturn = sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetTemplates_SQL((int)cboApplications.SelectedValue), "Tpl_NRI", "Tpl_Name", false, ref cboTemplates);

                txtCreatedBy.Text = clsTTApp.GetAppController.cUser.User_Code;

                mstrExternalReportAppName = clsTTSQL.str_ADOSingleLookUp("CeA_ExternalRPTAppName", "CerApp", "CeA_NRI = " + Int32.Parse(cboApplications.SelectedValue.ToString()).ToString());

                grdClients.Cols[mintGrdClients_LocationReportExe_col].Visible = !string.IsNullOrEmpty(mstrExternalReportAppName);

                cboTemplates.SelectedIndex = (cboTemplates.Items.Count > 0 ? 0 : -1);

                blnValidReturn = true;
            }
            else if (!pfblnData_Load())
            { }
            else if (!pfblnGrdClients_Load())
            { }
            else if (!pfblnGrdSatelliteApps_Load())
            { }
            else if (!pfblnGrdRevisions_Load())
            { }
            else
            {
                formController.FormIsLoading = false;

                grdClients.Row = (mintGrdClient_SelectedRow >= grdClients.Rows.Count ? 1 : mintGrdClient_SelectedRow);

                blnValidReturn = true;
            }

            this.ActiveControl = txtVersionNo;
            txtVersionNo.Focus();
      
            if (!blnValidReturn) this.Close();
        }

        private void formController_ValidateForm(ValidateFormEventArgs eventArgs)
        {
            eventArgs.IsValid = false;

            mcActionResults = mcCtrVersion.Validate();

            if (!mcActionResults.IsValid)
            {
                clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI, MessageBoxButtons.OK, mcActionResults.GetLstParams);

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

                        if (mcActionResults.RowInError > 0 & grdClients.Rows.Count > 1)
                        {
                            ((HostedCellControl)mcGrdClients.LstHostedCellControls[grdClients.Row-1]).GetCellControl.Focus();
                        }
                        
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
            frmWorkInProgress frmWorking = new frmWorkInProgress();
            Thread newThread = null;

            try
            {
                if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || mcGrdClients[grdClients.Row, mintGrdClients_Action_col] == TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.INSERT_MODE.ToString())
                {
                    newThread = new Thread(() => frmWorking.ShowDialog());
                    newThread.Start();
                }
                
                mintGrdClient_SelectedRow = grdClients.Row;

                mcActionResults = mcCtrVersion.Save();

                if (!mcActionResults.IsValid)
                {
                    clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI, MessageBoxButtons.OK, mcActionResults.GetLstParams);

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
                else if (mcActionResults.GetErrorMessage_NRI > 0)
                {
                    clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI, MessageBoxButtons.OK);
                }

                if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE) formController.Item_NRI = mcActionResults.GetNewItem_NRI;
            }
            finally
            {
                if (newThread != null)
                    newThread.Abort();

                eventArgs.SaveSuccessful = mcActionResults.IsValid;
            }
        }

        private void btnReplaceAppChangeDOC_Click(object sender, EventArgs e)
        {
            string strPreviousLocation = txtWordAppChangePath.Text;

            ShowOpenFileDialog("Word Documents (.docx)|*.docx", txtWordAppChangePath, txtWordAppChangePath.Text, true);

            if (!string.IsNullOrEmpty(txtWordAppChangePath.Text)) txtExcelAppChangePath.Text = string.Empty;

            if (!strPreviousLocation.Equals(txtWordAppChangePath.Text)) btnGenerate_Blink(true);
        }

        private void btnReplaceAppChangeXLS_Click(object sender, EventArgs e)
        {
            string strPreviousLocation = txtExcelAppChangePath.Text;

            ShowOpenFileDialog("Excel|*.xls;*.xlsx", txtExcelAppChangePath, txtExcelAppChangePath.Text, true);

            if (!string.IsNullOrEmpty(txtExcelAppChangePath.Text)) txtWordAppChangePath.Text = string.Empty;

            if (!strPreviousLocation.Equals(txtExcelAppChangePath.Text)) btnGenerate_Blink(true);
        }

        private void btnReplaceTTApp_Click(object sender, EventArgs e)
        {
            string strPreviousLocation = txtTTAppPath.Text;

            ShowOpenFileDialog("Access files (*.mdb)|*.mdb", txtTTAppPath, (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE ? txtTTAppPath.Text : string.Empty), true);

            if (!strPreviousLocation.Equals(txtTTAppPath.Text)) btnGenerate_Blink(true);
        }

        private void btnReplaceExecutable_Click(object sender, EventArgs e)
        {
            string strPreviousLocation = txtReleasePath.Text;

            ShowFolderBrowserDialog(ref txtReleasePath, btnReplaceExecutable);

            if (!strPreviousLocation.Equals(txtReleasePath.Text)) btnGenerate_Blink(true);
        }

        private void grdClients_DoubleClick(object sender, EventArgs e)
        {
            if (grdClients.Rows.Count > 1 & grdClients.Row > 0)
            {
                switch (grdClients.Col)
                {
                    case mintGrdClients_CeC_Name_col:

                        if (mcGrdClients[grdClients.Row, mintGrdClients_Action_col] == sclsConstants.DML_Mode.INSERT_MODE.ToString())
                        {
                            string strListOfClientSelected = string.Empty;

                            for (int intRowIdx = 1; intRowIdx < grdClients.Rows.Count; intRowIdx++)
                            {
                                strListOfClientSelected = strListOfClientSelected + (strListOfClientSelected == string.Empty ? mcGrdClients[intRowIdx, mintGrdClients_CeC_NRI_col] : "," + Conversion.Val(mcGrdClients[intRowIdx, mintGrdClients_CeC_NRI_col]));
                            }

                            sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetClients_SQL(strListOfClientSelected), "CeC_NRI", "Cec_Name", false, ref cboClients);

                            grdClients.StartEditing();
                        }

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

                        //Deplacer dans le rowColChanged
                        //if (mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.INSERT_MODE.ToString())
                        //{
                        //    mcGrdClients.GridIsLoading = true;

                        //    int intSelectedRow = grdClients.FindRow("true", 1, mintGrdClients_Selection_col, false, true, false);

                        //    if (intSelectedRow >= 1 & intSelectedRow != grdClients.Row) mcGrdClients[intSelectedRow, mintGrdClients_Selection_col] = "0";

                        //    mcGrdClients[grdClients.Row, mintGrdClients_Selection_col] = (mcGrdClients[grdClients.Row, mintGrdClients_Selection_col] == "0" ? "1" : "0");

                        //    btnExportInstallationKit.Enabled = mcGrdClients[grdClients.Row, mintGrdClients_Selection_col] == "1";
                        //    chkBaseKit.Enabled = btnExportInstallationKit.Enabled;
                        //    lblBaseKit.Enabled = btnExportInstallationKit.Enabled;

                        //    mcGrdClients.GridIsLoading = false;
                        //}

                        break;
                }
            }
        }

        private void cboApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading & mcGrdClients.bln_RowEditIsValid())
            {
                formController.ChangeMade = true;

                mcGrdSatelliteApps.ClearGrid();

                grdClients[grdClients.Row, mintGrdClients_CeC_NRI_col] = null;
                grdClients[grdClients.Row, mintGrdClients_CeC_Name_col] = null; 
            }

            if (cboApplications.SelectedIndex >= 0 && !formController.FormIsLoading)
            {
                sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetTemplates_SQL(Int32.Parse(cboApplications.SelectedValue.ToString())), "Tpl_NRI", "Tpl_Name", false, ref cboTemplates);

                mstrExternalReportAppName = clsTTSQL.str_ADOSingleLookUp("CeA_ExternalRPTAppName", "CerApp", "CeA_NRI = " + Int32.Parse(cboApplications.SelectedValue.ToString()).ToString());
                Int32.TryParse(clsTTSQL.str_ADOSingleLookUp("CeA_NRI_Master", "CerApp", "CeA_NRI = " + Int32.Parse(cboApplications.SelectedValue.ToString()).ToString()), out mintCeritarApplication_NRI_Master);

                grdClients.Cols[mintGrdClients_LocationReportExe_col].Visible = !string.IsNullOrEmpty(mstrExternalReportAppName);
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
                grdClients[grdClients.Row, mintGrdClients_CeC_NRI_col] = cboClients.SelectedValue;

                if (grdSatellite.Rows.Count <= 1)
                    pfblnGrdSatelliteApps_Load();
                
                formController.ChangeMade = true;
            }
        }

        void mcGrdClients_AfterClickAdd()
        {
            grdClients[grdClients.Row, mintGrdClients_CAV_NRI_col] = 0;
            grdClients[grdClients.Row, mintGrdClients_CAV_TS_col] = 0;
            grdClients[grdClients.Row, mintGrdClients_Installed_col] = 0;
            grdClients[grdClients.Row, mintGrdClients_IsCurrentVersion_col] = 0;
            grdClients[grdClients.Row, mintGrdClients_License_col] = "";

            mcGrdClients.LstHostedCellControls.Add(new HostedCellControl(grdClients, pfblnGetNewLocationReportButton(), grdClients.Row, mintGrdClients_LocationReportExe_col));

            mcGrdSatelliteApps.ClearGrid();
        }

        void mcGrdClients_BeforeClickAdd(ref bool vblnCancel)
        {
            if (mblnGrdSatellitesChangeMade || mcGrdClients[grdClients.Row, mintGrdClients_Action_col] == sclsConstants.DML_Mode.INSERT_MODE.ToString())
            {
                DialogResult msgResult = clsTTApp.GetAppController.ShowMessage(mintMSG_ChangesWillBeLostOnRowChange, MessageBoxButtons.YesNo);

                mblnGrdSatellitesChangeMade = msgResult == DialogResult.No;

                if (msgResult == System.Windows.Forms.DialogResult.Yes)
                {
                    formController.FormIsLoading = true;
                    pfblnGrdClients_Load();
                    pfblnGrdSatelliteApps_Load();
                    formController.FormIsLoading = false;

                    btnGenerate_Blink(false);
                }
            }
            
            vblnCancel = mblnGrdSatellitesChangeMade;
        }

        private void btnGenerate_Blink(bool vblnDoBlinking)
        {
            if (vblnDoBlinking && 
                formController.FormMode != sclsConstants.DML_Mode.INSERT_MODE && 
                grdClients.Rows.Count > 1 &&
                mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.INSERT_MODE.ToString()
               )
            {
                tmrGenerateBlink.Start();
                
                btnGenerate.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                tmrGenerateBlink.Stop();

                btnGenerate.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        private void formController_SetReadRights()
        {
            btnGenerate_Blink(false);

            switch (formController.FormMode)
            {
                case sclsConstants.DML_Mode.INSERT_MODE:

                    chkIncludeScripts.Enabled = false;
                    chkIncludeScripts.Checked = true;
                    chkBaseKit.Checked = false;
                    chkBaseKit.Enabled = false;

                    btnGenerate.Enabled = false;
                    btnExportInstallationKit.Enabled = false;
                    lblBaseKit.Enabled = false;
                    btnSelectVariousFilePath.Enabled = false;
                    btnSelectVariousFolderPath.Enabled = false;
                    btnShowRootFolder.Enabled = false;

                    tab.TabPages[mintTab_Revision].Enabled = false;

                    break;

                case sclsConstants.DML_Mode.UPDATE_MODE:

                    tab.TabPages[mintTab_Revision].Enabled = true;

                    chkIncludeScripts.Checked = false;
                    //chkBaseKit.Enabled = false;

                    btnGenerate.Enabled = true;
                    //btnExportInstallationKit.Enabled = false;
                    //lblBaseKit.Enabled = false;
                    btnGrdRevAdd.Enabled = true;
                    btnSelectVariousFilePath.Enabled = true;
                    btnSelectVariousFolderPath.Enabled = true;
                    btnShowRootFolder.Enabled = true;

                    cboApplications.Enabled = false;
                    cboTemplates.Enabled = false;

                    txtCompiledBy.ReadOnly = true;
                    txtVersionNo.ReadOnly = true;                 

                    break;

                case sclsConstants.DML_Mode.DELETE_MODE: case sclsConstants.DML_Mode.CONSULT_MODE:

                    chkBaseKit.Enabled = true;
                    btnGrdRevDel.Enabled = true;
                    btnGrdRevUpdate.Enabled = false;
                    btnShowRootFolder.Enabled = true;
                    btnShowDB_UpgradeScripts.Enabled = true;
                    txtDescription.Enabled = false;
                    btnGrdClientsAdd.Enabled = false;
                    btnGrdClientsDel.Enabled = false;
                    btnShowAccess.Enabled = true;
                    btnShowExcel.Enabled = true;
                    btnShowExecutable.Enabled = true;
                    btnShowWord.Enabled = true;

                    break;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!formController.ChangeMade)
            {
                Cursor.Current = Cursors.WaitCursor;

                mintGrdClient_SelectedRow = grdClients.Row;

                frmWorkInProgress frmWorking = new frmWorkInProgress();
                Thread newThread = new Thread(() => frmWorking.ShowDialog());

                if (chkIncludeScripts.Checked)
                    newThread.Start();

                mcCtrVersion.blnUpdateVersionHierarchy();

                if (newThread != null && newThread.ThreadState == ThreadState.Running)
                    newThread.Abort();

                mcActionResults = mcCtrVersion.GetActionResult;

                if (!mcActionResults.IsValid)
                {
                    clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI, MessageBoxButtons.OK, mcActionResults.GetLstParams);

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
                    btnGenerate_Blink(false);

                    formController.FormIsLoading = true;
                    formController.ReloadForm();
                    formController.FormIsLoading = false;

                    if (mcActionResults.SuccessMessage_NRI > 0) clsTTApp.GetAppController.ShowMessage(mcActionResults.SuccessMessage_NRI);
                }
                grdClients.Row = mintGrdClient_SelectedRow;

                Cursor.Current = Cursors.Default;
            }
            else
            {
                clsTTApp.GetAppController.ShowMessage((int)sclsConstants.Validation_Message.INVALID_ACTION_IF_CHANGE_MADE);
            }
        }

        private void btnGrdRevAdd_Click(object sender, EventArgs e)
        {
            if (formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
            {
                int intNewItem_NRI = 0;
                frmRevision frmVersion = new frmRevision();

                frmVersion.mintVersion_NRI = formController.Item_NRI;
                frmVersion.mintCeritarApplication_NRI_Master = mintCeritarApplication_NRI_Master;
                frmVersion.formController.ShowForm(this, sclsConstants.DML_Mode.INSERT_MODE, ref intNewItem_NRI, false, true);

                pfblnGrdRevisions_Load();

                grdRevisions.Row = grdRevisions.Rows.Count - 1;

                btnRefresh.SetToRefresh = true;
            }
        }

        private void btnGrdRevDel_Click(object sender, EventArgs e)
        {
            if (mcGrdRevisions.bln_RowEditIsValid() || formController.FormMode == sclsConstants.DML_Mode.DELETE_MODE)
            {
                int intItem_NRI = Int32.Parse(mcGrdRevisions[grdRevisions.Row, mintGrdRev_Rev_NRI_col]);
                frmRevision frmRevision = new frmRevision();

                frmRevision.mintVersion_NRI = formController.Item_NRI;

                frmRevision.formController.ShowForm(this, sclsConstants.DML_Mode.DELETE_MODE, ref intItem_NRI, false,true);

                pfblnGrdRevisions_Load();

                grdRevisions.Row = grdRevisions.Rows.Count - 1;

                btnRefresh.SetToRefresh = true;
            }
        }

        private void txtVersionNo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtVersionNo.Text != string.Empty && !Microsoft.VisualBasic.Information.IsNumeric(txtVersionNo.Text))
            {
                clsTTApp.GetAppController.ShowMessage((int)sclsConstants.Validation_Message.NUMERIC_VALUE);
                e.Cancel = true;
                txtVersionNo.SelectAll();
            }
        }

        private void btnReplaceReportExe_Click(object sender, EventArgs e)
        {
            string strPreviousLocation = string.Empty;
            int intClickedRow = 0;
            DialogResult msgResult = DialogResult.Yes;

            mcGrdClients.GridIsLoading = true;

            foreach (HostedCellControl control in mcGrdClients.LstHostedCellControls)
            {
                if (control.GetCellControl.Handle == ((Button)sender).Handle)
                {
                    intClickedRow = control.GetRowLinked.Index;

                    if (mblnGrdSatellitesChangeMade & grdClients.Row != intClickedRow)
                    {
                        msgResult = clsTTApp.GetAppController.ShowMessage(mintMSG_ChangesWillBeLostOnRowChange, MessageBoxButtons.YesNo);
                    }

                    break;
                }
            }

            if (msgResult == DialogResult.Yes)
            {
                grdClients.Row = intClickedRow;
                
                strPreviousLocation = mcGrdClients[grdClients.Row, mintGrdClients_LocationReportExe_col];

                ShowOpenFileDialog("LogirackTransport RPT (*.exe)|*.exe", grdClients, mcGrdClients[grdClients.Row, mintGrdClients_LocationReportExe_col], true);

                if (!strPreviousLocation.Equals(mcGrdClients[grdClients.Row, mintGrdClients_LocationReportExe_col]))
                {
                    btnGenerate_Blink(true);
                    mblnGrdSatellitesChangeMade = true;
                }     
            }

            mcGrdClients.GridIsLoading = false;
        }

        private void btnReplaceSatelliteExe_Click(object sender, EventArgs e)
        {
            TextBox txtTemp = new TextBox();
            mcGrdSatelliteApps.GridIsLoading = true;
            string strPreviousLocation = string.Empty;

            foreach (HostedCellControl control in mcGrdSatelliteApps.LstHostedCellControls)
            {
                if (control.GetCellControl.Handle == ((Button)sender).Handle)
                {
                    grdSatellite.Row = control.GetRowLinked.Index;
                }
            }

            if (mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSA_ExeIsFolder_col] == "True")
            {
                strPreviousLocation = mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col];
                txtTemp.Text = mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col];

                ShowFolderBrowserDialog(ref txtTemp, (Button)sender);

                if (!string.IsNullOrEmpty(txtTemp.Text))
                {
                    if (mcGrdSatelliteApps.bln_CellIsEmpty(grdSatellite.Row, mintGrdSat_CSV_NRI_col) & mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col] != txtTemp.Text)
                    {
                        grdSatellite[grdSatellite.Row, mintGrdSat_Action_col] = sclsConstants.DML_Mode.INSERT_MODE;

                        ((HostedCellControl)mcGrdSatelliteApps.LstHostedCellControls[grdSatellite.Row - 1]).GetCellControl.BackColor = System.Drawing.Color.Yellow;

                        mblnGrdSatellitesChangeMade = true;
                    }
                    else if (mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col] != txtTemp.Text)
                    {
                        grdSatellite[grdSatellite.Row, mintGrdSat_Action_col] = sclsConstants.DML_Mode.UPDATE_MODE;

                        ((HostedCellControl)mcGrdSatelliteApps.LstHostedCellControls[grdSatellite.Row - 1]).GetCellControl.BackColor = System.Drawing.Color.Yellow;

                        mblnGrdSatellitesChangeMade = true;
                    }

                    grdSatellite[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col] = txtTemp.Text;        
                }              
            }
            else
            {
                strPreviousLocation = mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col];

                ShowOpenFileDialog("Executables (exe, apk)|*.exe;*.apk", grdSatellite, mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col], true);
            }

            if (!strPreviousLocation.Equals(mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col])) btnGenerate_Blink(true);
            
            mcGrdSatelliteApps.GridIsLoading = false;
        }

        private void btnGrdClientsDel_Click(object sender, EventArgs e)
        {
            if (mcGrdClients.bln_RowEditIsValid() && mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.DELETE_MODE.ToString())
            {
                ((HostedCellControl)mcGrdClients.LstHostedCellControls[grdClients.Row - 1]).GetCellControl.Dispose();

                mcGrdClients.LstHostedCellControls.RemoveAt(grdClients.Row - 1);

                mcGrdSatelliteApps.ClearGrid();
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
                pfblnGrdSatelliteApps_Load();

                btnGenerate.Enabled = mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.INSERT_MODE.ToString() && mcGrdClients.bln_RowEditIsValid() && formController.FormMode != sclsConstants.DML_Mode.INSERT_MODE;
                chkIncludeScripts.Enabled = btnGenerate.Enabled;
                chkIncludeScripts.Checked = !chkIncludeScripts.Enabled;
            }

            if (mcGrdClients[grdClients.Row, mintGrdClients_Action_col] != sclsConstants.DML_Mode.INSERT_MODE.ToString())
            {
                blnEnableControls = true;
            }
            else
            {
                blnEnableControls = false;
            }

            btnExportInstallationKit.Enabled = blnEnableControls;
            chkBaseKit.Enabled = blnEnableControls;
            lblBaseKit.Enabled = blnEnableControls;
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
                try
                {
                    System.Diagnostics.Process.Start(txtExcelAppChangePath.Text);
                }
                catch (Exception ex)
                {
                    sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
                }               
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
                    try
                    {
                        System.Diagnostics.Process.Start("explorer.exe", "/select, " + txtTTAppPath.Text);
                    }
                    catch (Exception ex)
                    {
                        sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
                    }
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

        private void grdClients_BeforeRowColChange(object sender, RangeEventArgs e)
        {
            if (!formController.FormIsLoading && (mblnGrdSatellitesChangeMade || mcGrdClients[e.OldRange.r1, mintGrdClients_Action_col] == sclsConstants.DML_Mode.INSERT_MODE.ToString()) & e.NewRange.r1 != e.OldRange.r1)
            {
                DialogResult msgResult = clsTTApp.GetAppController.ShowMessage(mintMSG_ChangesWillBeLostOnRowChange, MessageBoxButtons.YesNo);

                if (msgResult == System.Windows.Forms.DialogResult.Yes)
                {
                    formController.FormIsLoading = true;
                    pfblnGrdClients_Load();
                    pfblnGrdSatelliteApps_Load(e.NewRange.r1);
                    formController.FormIsLoading = false;

                    btnGenerate.Enabled = true;
                    chkIncludeScripts.Enabled = true;
                    chkIncludeScripts.Checked = false;

                    btnGenerate_Blink(false);
                }

                if (msgResult == DialogResult.No) e.Cancel = true;
            }
        }

        private void grdRevisions_DoubleClick(object sender, EventArgs e)
        {
            if (mcGrdRevisions.bln_RowEditIsValid())
            {
                int intRevision_NRI = Int32.Parse(mcGrdRevisions[grdRevisions.Row, mintGrdRev_Rev_NRI_col]);
                int intSelectedRow = grdRevisions.Row;

                frmRevision frmRevision = new frmRevision();

                frmRevision.mintVersion_NRI = formController.Item_NRI;

                frmRevision.formController.ShowForm(this, sclsConstants.DML_Mode.UPDATE_MODE, ref intRevision_NRI, false, true);

                pfblnGrdRevisions_Load();

                grdRevisions.Row = intSelectedRow >= 1 ? intSelectedRow : grdRevisions.Rows.Count - 1;

                btnRefresh.SetToRefresh = true;
            }
        }

        private void btnGrdRevUpdate_Click(object sender, EventArgs e)
        {
            grdRevisions_DoubleClick(grdRevisions, new EventArgs());
        }

        private void btnRefresh_Click()
        {
            pfblnGrdRevisions_Load();
        }

        private void btnSelectVariousFilePath_Click(object sender, EventArgs e)
        {
            string strPreviousLocation = mstrVariousFileLocation;
            TextBox txtPlaceHolder = new TextBox();

            ShowOpenFileDialog("Executable (*.*)|*.*", txtPlaceHolder, mstrVariousFileLocation, true);

            mstrVariousFileLocation = txtPlaceHolder.Text;

            if (!string.IsNullOrEmpty(mstrVariousFileLocation)) btnSelectVariousFilePath.BackColor = System.Drawing.Color.Yellow;

            if (!strPreviousLocation.Equals(mstrVariousFileLocation)) btnGenerate_Blink(true);
        }

        private void btnSelectVariousFolderPath_Click(object sender, EventArgs e)
        {
            string strPreviousLocation = mstrVariousFolderLocation;
            TextBox txtPlaceHolder = new TextBox();

            txtPlaceHolder.Text = mstrVariousFolderLocation;

            ShowFolderBrowserDialog(ref txtPlaceHolder, btnSelectVariousFolderPath);

            mstrVariousFolderLocation = txtPlaceHolder.Text;

            if (!string.IsNullOrEmpty(mstrVariousFolderLocation)) btnSelectVariousFolderPath.BackColor = System.Drawing.Color.Yellow;

            if (!strPreviousLocation.Equals(mstrVariousFolderLocation)) btnGenerate_Blink(true);
        }

        private void grdClients_MouseMove(object sender, MouseEventArgs e)
        {
            if (!formController.FormIsLoading && grdClients.MouseRow > 0 && grdClients.MouseRow < grdClients.Rows.Count)
            {
                switch (grdClients.Col)
                {
                    case mintGrdClients_Installed_col:

                        if (!string.IsNullOrEmpty(mcGrdClients[grdClients.MouseRow, mintGrdClients_DateInstalled_col]))
                        {
                            toolTip.Show(clsTTApp.GetAppController.str_GetServerFormatedDate(mcGrdClients[grdClients.MouseRow, mintGrdClients_DateInstalled_col]), grdClients, e.Location.X + 5, e.Location.Y - 10, 1000);
                        }
                           
                        break;
                }
            }
        }

        private void btnShowRootFolder_Click(object sender, EventArgs e)
        {
            string strRootFolder = CVS.Controllers.ctr_Version.str_GetVersionFolderPath((int)cboTemplates.SelectedValue, txtVersionNo.Text);

            try
            {
                System.Diagnostics.Process.Start(@strRootFolder);
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        private void tmrGenerateBlink_Tick(object sender, EventArgs e)
        {
            if (btnGenerate.BackColor == System.Drawing.Color.Yellow)
            {
                btnGenerate.BackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                btnGenerate.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void btnShowDB_UpgradeScripts_Click(object sender, EventArgs e)
        {
            string strCerApplicationName;

            if (mintCeritarApplication_NRI_Master > 0)
            {
                strCerApplicationName = clsTTSQL.str_ADOSingleLookUp("CeA_Name", "CerApp", "CeA_NRI = " + mintCeritarApplication_NRI_Master);
            }
            else
            {
                strCerApplicationName = (cboApplications.SelectedIndex >= 0 ? cboApplications.GetItemText(cboApplications.SelectedItem) : string.Empty);
            }
            string strRootFolder = Path.Combine(CVS.sclsAppConfigs.GetRoot_DB_UPGRADE_SCRIPTS, strCerApplicationName);

            try
            {
                System.Diagnostics.Process.Start(@strRootFolder);
            }
            catch (Exception)
            {
                clsTTApp.GetAppController.ShowMessage((int)sclsConstants.Validation_Message.INVALID_PATH, MessageBoxButtons.OK, strRootFolder);
            }
        }

        private void grdClients_Click(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                mintGrdClient_SelectedRow = grdClients.Row;
            }
        }

        private void formController_BeNotify(BeNotifyEventArgs eventArgs)
        {
            int intGrdRev_RowSelected = 1;

            switch (eventArgs.LstReceivedValues[0].ToString())
            {
                case "frmRevision":
                    intGrdRev_RowSelected = grdRevisions.Row;

                    pfblnGrdRevisions_Load();

                    grdRevisions.Row = (intGrdRev_RowSelected >= grdRevisions.Rows.Count ? grdRevisions.Rows.Count - 1 : intGrdRev_RowSelected);

                    break;
            }
        }

        private void mnuClientSatVersion_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void mnuiShowInExplorer_Click(object sender, EventArgs e)
        {
            string strLocation = mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col];

            if (mcGrdSatelliteApps.bln_RowEditIsValid() && !string.IsNullOrEmpty(strLocation))
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

        private void mnuiDelete_Click(object sender, EventArgs e)
        {
            bool blnValidReturn = false;

            if (mcGrdSatelliteApps.bln_RowEditIsValid() && !mcGrdSatelliteApps.bln_CellIsEmpty(grdSatellite.Row, mintGrdSat_CSV_NRI_col))
            {
                Cursor.Current = Cursors.WaitCursor;

                blnValidReturn = mcCtrVersion.blnDeleteClientSatelliteVersion(Int32.Parse(mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_NRI_col]));

                if (blnValidReturn) formController.ReloadForm();

                Cursor.Current = Cursors.Default;
            }
            else if (grdSatellite.Rows.Count > 1 && grdSatellite.Row > 0 && !mcGrdSatelliteApps.bln_CellIsEmpty(grdSatellite.Row, mintGrdSat_CSV_LocationExe_col))
            {
                mcGrdSatelliteApps[grdSatellite.Row, mintGrdSat_CSV_LocationExe_col] = string.Empty;
                ((HostedCellControl)mcGrdSatelliteApps.LstHostedCellControls[grdSatellite.Row - 1]).GetCellControl.BackColor = System.Drawing.SystemColors.Control;
            }
        }
    }
}
