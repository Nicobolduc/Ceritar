﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.CVS.Controllers;
using Ceritar.CVS.Controllers.Interfaces;
using System.IO;
using System.Data;

namespace Ceritar.Logirack_CVS.Forms
{
    /// <summary>
    /// Cette classe contient les fonctions et évènements de la vue permettant de définir les applications de Ceritar.
    /// </summary>
    public partial class frmCeritarApp : Form, Ceritar.CVS.Controllers.Interfaces.ICeritarApp, Ceritar.TT3LightDLL.Controls.IFormController
    {
        //Controller
        private ctr_CeritarApplication mcCtrCeritarApp;

        //Columns grdModules
        private const short mintGrdMod_Action_col = 1;
        private const short mintGrdMod_ApM_NRI_col = 2;
        private const short mintGrdMod_ApM_TS_col = 3;
        private const short mintGrdMod_ApM_Description_col = 4;

        //Columns grdAppSat
        private const short mintGrdSat_Action_col = 1;
        private const short mintGrdSat_CSA_NRI_col = 2;
        private const short mintGrdSat_CSA_TS_col = 3;
        private const short mintGrdSat_CSA_Name_col = 4;
        private const short mintGrdSat_CSA_KitFolderName_col = 5;
        private const short mintGrdSat_CSA_Exe_IsFolder_col = 6;
        private const short mintGrdSat_CSA_Exe_PerCustomer_col = 7;

        //Classes
        private clsTTC1FlexGridWrapper mcGrdModules;
        private clsTTC1FlexGridWrapper mcGrdSatApp;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Working variables
        private ushort mintCerApp_TS;
        private string mstrWordProcedurePath = string.Empty;


        public frmCeritarApp()
        {
            InitializeComponent();
            
            mcCtrCeritarApp = new ctr_CeritarApplication((Ceritar.CVS.Controllers.Interfaces.ICeritarApp) this);

            mcGrdModules = new clsTTC1FlexGridWrapper();
            mcGrdModules.SetGridDisplay += new clsTTC1FlexGridWrapper.SetDisplayEventHandler(mcGrdModules_SetDisplay);

            mcGrdSatApp = new clsTTC1FlexGridWrapper();
            mcGrdSatApp.SetGridDisplay += mcGrdAppSat_SetGridDisplay;
            mcGrdSatApp.AfterClickAdd += mcGrdSatApp_AfterClickAdd;
        }


        #region "Interfaces functions"

        List<string> CVS.Controllers.Interfaces.ICeritarApp.GetLstModules()
        {
            List<string> lstModules = new List<string>();

            for (int intRowIdx = 1; intRowIdx <= grdModules.Rows.Count - 1; intRowIdx++)
            {
                lstModules.Add(mcGrdModules[intRowIdx, mintGrdMod_ApM_Description_col]);
            }

            return lstModules;
        }

        string CVS.Controllers.Interfaces.ICeritarApp.GetName()
        {
            return txtName.Text;
        }

        TT3LightDLL.Static_Classes.sclsConstants.DML_Mode CVS.Controllers.Interfaces.ICeritarApp.GetDML_Mode()
        {
            return formController.FormMode;
        }

        string CVS.Controllers.Interfaces.ICeritarApp.GetDescription()
        {
            return txtDescription.Text;
        }

        int CVS.Controllers.Interfaces.ICeritarApp.GetDomain_NRI()
        {
            return (int)cboDomain.SelectedValue;
        }

        int CVS.Controllers.Interfaces.ICeritarApp.GetCerApp_NRI()
        {
            return formController.Item_NRI;
        }

        public int GetCerApp_TS()
        {
            return mintCerApp_TS;
        }

        TT3LightDLL.Controls.ctlFormController TT3LightDLL.Controls.IFormController.GetFormController()
        {
            return this.formController;
        }

        int CVS.Controllers.Interfaces.ICeritarApp.GetCerApp_TS()
        {
            return mintCerApp_TS;
        }

        List<structCeritarSatelliteApp> ICeritarApp.GetLstAppSatellites()
        {
            List<structCeritarSatelliteApp> lstSatelliteApp = new List<structCeritarSatelliteApp>();
            structCeritarSatelliteApp structCSA;

            for (int intRowIdx = 1; intRowIdx <= grdSatApp.Rows.Count - 1; intRowIdx++)
            {
                structCSA = new structCeritarSatelliteApp();
                structCSA.Action = clsTTApp.GetAppController.ConvertToEnum<sclsConstants.DML_Mode>(grdSatApp[intRowIdx, mintGrdSat_Action_col]);
                structCSA.blnExeIsFolder = Convert.ToBoolean(grdSatApp[intRowIdx, mintGrdSat_CSA_Exe_IsFolder_col]); 
                structCSA.blnExePerCustomer = Convert.ToBoolean(grdSatApp[intRowIdx, mintGrdSat_CSA_Exe_PerCustomer_col]);
                Int32.TryParse(mcGrdSatApp[intRowIdx, mintGrdSat_CSA_NRI_col], out structCSA.intCeritarSatelliteApp_NRI);
                Int32.TryParse(mcGrdSatApp[intRowIdx, mintGrdSat_CSA_TS_col], out structCSA.intCeritarSatelliteApp_TS);
                structCSA.strSatelliteApp_Name = mcGrdSatApp[intRowIdx, mintGrdSat_CSA_Name_col];
                structCSA.strKitExport_FolderName = mcGrdSatApp[intRowIdx, mintGrdSat_CSA_KitFolderName_col];

                lstSatelliteApp.Add(structCSA);
            }

            return lstSatelliteApp;
        }

        string ICeritarApp.GetExternalReportAppName()
        {
            return chkReportAppExternal.Checked ? txtReportAppExternal.Text : string.Empty;
        }

        bool ICeritarApp.IsGeneratingRevisionNoScript()
        {
            return (!chkMasterApp.Checked ? false : chkGenRevNoScript.Checked);
        }

        bool ICeritarApp.IsManagingTTApp()
        {
            return chkManageTTApp.Checked;
        }

        int ICeritarApp.GetCerApp_NRI_Master()
        {
            if (chkMasterApp.Checked) return 0;

            return (int)cboMasterApp.SelectedValue;
        }


        string ICeritarApp.GetServeurDevName()
        {
            if (!chkMasterApp.Checked) return string.Empty;

            return txtServeurDev.Text;
        }

        string ICeritarApp.GetDatabaseDevName()
        {
            if (!chkMasterApp.Checked) return string.Empty;

            return txtBDDev.Text;
        }

        #endregion


        #region "Functions"

        private bool pfblnGrdModules_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdModules.bln_FillData(mcCtrCeritarApp.strGetListe_Modules_SQL(formController.Item_NRI));
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        private bool pfblnGrdSatelliteApps_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdSatApp.bln_FillData(mcCtrCeritarApp.strGetListe_SatelliteApps_SQL(formController.Item_NRI));
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
                sqlRecord = clsTTSQL.ADOSelect(mcCtrCeritarApp.strGetDataLoad_SQL(formController.Item_NRI));

                if (sqlRecord.Read())
                {
                    UInt16.TryParse(sqlRecord["CeA_TS"].ToString(), out mintCerApp_TS);
                    txtName.Text = sqlRecord["CeA_Name"].ToString();
                    txtDescription.Text = sqlRecord["CeA_Desc"].ToString();
                    txtReportAppExternal.Text = sqlRecord["CeA_ExternalRPTAppName"].ToString();
                    txtServeurDev.Text = sqlRecord["CeA_DevServer"].ToString();
                    txtBDDev.Text = sqlRecord["CeA_DevDatabase"].ToString();

                    cboDomain.SelectedValue = Int32.Parse(sqlRecord["ApD_NRI"].ToString());

                    chkGenRevNoScript.Checked = bool.Parse(sqlRecord["CeA_AutoGenRevisionNoScript"].ToString());
                    chkManageTTApp.Checked = bool.Parse(sqlRecord["CeA_ManageTTApp"].ToString());
                    chkMasterApp.Checked = sqlRecord["CeA_NRI_Master"] == DBNull.Value;

                    cboMasterApp.Enabled = !chkMasterApp.Checked;

                    if (chkMasterApp.Checked)
                    {
                        txtServeurDev.Enabled = true;
                        txtBDDev.Enabled = true;
                    }
                    else
                    {
                        txtServeurDev.Enabled = false;
                        txtBDDev.Enabled = false;
                    }

                    if (sqlRecord["ItI_NRI_Procedure"] != DBNull.Value)
                    {
                        btnShowWord.Tag = Int32.Parse(sqlRecord["ItI_NRI_Procedure"].ToString());
                    }
                    else
                    {
                        btnShowWord.Tag = 0;
                    }

                    if (sqlRecord["CeA_NRI_Master"] != DBNull.Value)
                    {
                        cboMasterApp.SelectedValue = Int32.Parse(sqlRecord["CeA_NRI_Master"].ToString());

                        chkGenRevNoScript.Enabled = false;
                    }
                    else
                    {
                        chkGenRevNoScript.Enabled = true;
                    }

                    if (!string.IsNullOrEmpty(txtReportAppExternal.Text))
                    {
                        chkReportAppExternal.Checked = true;

                        if (formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
                        {
                            txtReportAppExternal.ReadOnly = false;
                        }
                    }
                    else
                    {
                        chkReportAppExternal.Checked = false;
                    }

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
        

        private void formController_LoadData(TT3LightDLL.Controls.LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;

            if (!mcGrdModules.bln_Init(ref grdModules, ref btnGrdModAdd, ref btnGrdModDel))
            { }
            if (!mcGrdSatApp.bln_Init(ref grdSatApp, ref btnGrdSatAdd, ref btnGrdSatDel))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrCeritarApp.strGetListe_Domains_SQL(), "ApD_NRI", "ApD_Code", true, ref cboDomain))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrCeritarApp.strGetListe_MasterCeritarApplications_SQL(formController.Item_NRI), "CeA_NRI", "CeA_Name", true, ref cboMasterApp))
            { }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
            { 
                blnValidReturn = true; 
            }
            else if (!pfblnGrdModules_Load())
            { }
            else if (!pfblnGrdSatelliteApps_Load())
            { }
            else if (!pfblnData_Load())
            { }
            else
            {
                blnValidReturn = true;
            }

            if (!blnValidReturn) this.Close();
        }

        private void mcGrdModules_SetDisplay()
        {
            grdModules.Cols[mintGrdMod_ApM_Description_col].Width = 30;
        }

        private void mcGrdAppSat_SetGridDisplay()
        {
            grdSatApp.Cols[mintGrdSat_CSA_Name_col].Width = 250;
            grdSatApp.Cols[mintGrdSat_CSA_KitFolderName_col].Width = 135;
            grdSatApp.Cols[mintGrdSat_CSA_Exe_IsFolder_col].Width = 120;

            mcGrdSatApp.SetColType_CheckBox(mintGrdSat_CSA_Exe_IsFolder_col, 80);
            mcGrdSatApp.SetColType_CheckBox(mintGrdSat_CSA_Exe_PerCustomer_col);
        }

        private void formController_ValidateForm(TT3LightDLL.Controls.ValidateFormEventArgs eventArgs)
        {
            mcActionResults = mcCtrCeritarApp.Validate();

            if (!mcActionResults.IsValid)
            {
                clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI);

                switch ((ctr_CeritarApplication.ErrorCode_CeA)mcActionResults.GetErrorCode)
                {
                    case ctr_CeritarApplication.ErrorCode_CeA.DESCRIPTION_MANDATORY:

                        txtDescription.Focus();
                        txtDescription.SelectAll();
                        break;

                    case ctr_CeritarApplication.ErrorCode_CeA.NAME_MANDATORY:
                    case ctr_CeritarApplication.ErrorCode_CeA.NAME_INVALID:

                        txtName.Focus();
                        txtName.SelectAll();
                        break;

                    case ctr_CeritarApplication.ErrorCode_CeA.MODULES_LIST_MANDATORY:

                        if (grdModules.Rows.Count > 1)
                        {
                            grdModules.Row = mcActionResults.RowInError;
                        } 
                        else 
                        {
                            btnGrdModAdd.Focus();
                        }
                        
                        break;

                    case ctr_CeritarApplication.ErrorCode_CeA.DOMAIN_MANDATORY:

                        cboDomain.Focus();
                        cboDomain.DroppedDown = true;
                        break;

                    case ctr_CeritarApplication.ErrorCode_CeA.SATELLITE_LIST_MANDATORY:
                    case ctr_CeritarApplication.ErrorCode_CeA.SATELLITE_NAME_INVALID:

                        grdSatApp.Row = mcActionResults.RowInError;

                        break;
                }

                switch ((ctr_CeritarApplication.ErrorCode_CSA)mcActionResults.GetErrorCode)
                {
                    case ctr_CeritarApplication.ErrorCode_CSA.NAME_MANDATORY:
                    case ctr_CeritarApplication.ErrorCode_CSA.KIT_FOLDER_NAME_MANDATORY:

                        grdSatApp.Row = mcActionResults.RowInError;
                        break;
                }
            }
            else
            {
                if (!chkMasterApp.Checked && cboMasterApp.SelectedIndex == -1)
                {
                    cboMasterApp.Focus();
                    cboMasterApp.DroppedDown = true;
                }
                    
            }    

            eventArgs.IsValid = mcActionResults.IsValid;
        }

        private void formController_SaveData(TT3LightDLL.Controls.SaveDataEventArgs eventArgs)
        {
            mcActionResults = mcCtrCeritarApp.Save();
            
            if (!mcActionResults.IsValid)
            {
                clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI);

            }
            else if (mcActionResults.SuccessMessage_NRI > 0)
            {
                clsTTApp.GetAppController.ShowMessage(mcActionResults.SuccessMessage_NRI);
            }

            if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE) formController.Item_NRI = mcActionResults.GetNewItem_NRI;

            eventArgs.SaveSuccessful = mcActionResults.IsValid;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            formController.ChangeMade = true;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            formController.ChangeMade = true;
        }

        private void grdModules_Validated(object sender, EventArgs e)
        {
            formController.ChangeMade = true;
        }
      
        private void grdModules_DoubleClick(object sender, EventArgs e)
        {
            switch (grdModules.Col)
            {
                case mintGrdMod_ApM_Description_col:

                    if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE | formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
                    {
                        grdModules.StartEditing();
                        formController.ChangeMade = true;
                    }

                    break;
            }
        }

        private void btnGrdDel_MouseUp(object sender, MouseEventArgs e)
        {
            if (grdModules.Row > 0)
            {
                grdModules.RemoveItem(grdModules.Row);
            }
        }

        private void grdAppSat_DoubleClick(object sender, EventArgs e)
        {
            if (mcGrdSatApp.bln_RowEditIsValid())
            {
                switch (grdSatApp.Col)
                {
                    case mintGrdSat_CSA_Name_col:

                        //if (mcGrdSatApp[grdSatApp.Row, mintGrdSat_Action_col] == sclsConstants.DML_Mode.INSERT_MODE.ToString())
                        //{
                            grdSatApp.StartEditing();
                            formController.ChangeMade = true;
                        //}

                        break;

                    case mintGrdSat_CSA_KitFolderName_col:

                        grdSatApp.StartEditing();
                        formController.ChangeMade = true;

                        break;

                    case mintGrdSat_CSA_Exe_IsFolder_col:

                        mcGrdSatApp[grdSatApp.Row, mintGrdSat_CSA_Exe_IsFolder_col] = (mcGrdSatApp[grdSatApp.Row, mintGrdSat_CSA_Exe_IsFolder_col] == "0" ? "1" : "0");

                        break;

                    case mintGrdSat_CSA_Exe_PerCustomer_col:

                        mcGrdSatApp[grdSatApp.Row, mintGrdSat_CSA_Exe_PerCustomer_col] = (mcGrdSatApp[grdSatApp.Row, mintGrdSat_CSA_Exe_PerCustomer_col] == "0" ? "1" : "0");

                        break;
                }
            }
        }

        private void grdAppSat_Validated(object sender, EventArgs e)
        {
            formController.ChangeMade = true;
        }

        private void cboDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void chkReportAppExternal_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                txtReportAppExternal.ReadOnly = !chkReportAppExternal.Checked;

                formController.ChangeMade = true;
            }
        }

        private void txtReportAppExternal_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void chkGenRevNoScript_Click(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void chkTTAppNeeded_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void mcGrdSatApp_AfterClickAdd()
        {
            mcGrdSatApp[grdSatApp.Row, mintGrdSat_CSA_Exe_IsFolder_col] = "1";
        }

        private void formController_SetReadRights()
        {
            switch ((int)formController.FormMode)
            {
                case (int)sclsConstants.DML_Mode.INSERT_MODE:

                    cboMasterApp.Enabled = false;
                    btnReplaceWordDOC.Enabled = false;
                    btnShowWord.Enabled = false;

                    break;

                case (int)sclsConstants.DML_Mode.UPDATE_MODE:
                    btnReplaceWordDOC.Enabled = true;
                    btnShowWord.Enabled = true;

                    break;
            }
        }

        private void chkMasterApp_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                cboMasterApp.Enabled = !chkMasterApp.Checked;
                chkGenRevNoScript.Enabled = chkMasterApp.Checked;

                if (chkMasterApp.Checked)
                {
                    txtServeurDev.Enabled = true;
                    txtBDDev.Enabled = true;
                }
                else
                {
                    txtServeurDev.Enabled = false;
                    txtBDDev.Enabled = false;
                }

                formController.ChangeMade = true;
            }
        }

        private void cboMasterApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void grdSatApp_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            if (grdSatApp.Col == mintGrdSat_CSA_KitFolderName_col)
            {
                if (grdSatApp.Editor.Text.Length > 25)
                {
                    clsTTApp.GetAppController.ShowMessage((int)sclsConstants.Validation_Message.MAXIMUM_NUMBER_CHARACTER, MessageBoxButtons.OK, "25");
                    e.Cancel = true;
                }
                else
                {
                    //Do nothing
                }
            }
            else
            {
                //Do nothing
            }
        }

        private void btnReplaceAppChangeDOC_Click(object sender, EventArgs e)
        {
            string strPreviousLocation = mstrWordProcedurePath;

            ShowOpenFileDialog("Word Documents (.docx)|*.docx", mstrWordProcedurePath, true);

            if (mstrWordProcedurePath != string.Empty)
            {
                clsTTApp.GetAppController.SaveFileToDB(mstrWordProcedurePath, Path.GetFileNameWithoutExtension(mstrWordProcedurePath), "CerApp", formController.Item_NRI, Int32.Parse(btnShowWord.Tag.ToString()));

                btnShowWord.Tag = clsTTSQL.str_ADOSingleLookUp("ItI_NRI", "ItI", "ItI_ItemNRI = " + formController.Item_NRI + " AND ItI_Table = " + clsTTApp.GetAppController.str_FixStringForSQL("CerApp"));
            }
        }

        private void ShowOpenFileDialog(string vstrExtensionsFilter, string vstrInitialDirectory = "", bool vblnShowFile = false)
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

                        vstrInitialDirectory = mstrWordProcedurePath == string.Empty ? System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : mstrWordProcedurePath;
                    }

                    openFileDialog.InitialDirectory = vstrInitialDirectory;
                    openFileDialog.Multiselect = false;
                    openFileDialog.Filter = vstrExtensionsFilter;

                    dialogResult = openFileDialog.ShowDialog();

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        mstrWordProcedurePath = openFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        private void btnShowWord_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(btnShowWord.Tag.ToString()) > 0)
                clsTTApp.GetAppController.LoadFileFromDB(Int32.Parse(btnShowWord.Tag.ToString()));
        }

        private void txtServeurDev_TextChanged(object sender, EventArgs e)
        {
            formController.ChangeMade = true;
        }

        private void txtBDDev_TextChanged(object sender, EventArgs e)
        {
            formController.ChangeMade = true;
        }
    }
}



//TEMPLATES

#region "Properties"


#endregion

#region "IFormController"


#endregion

#region "Functions"


#endregion


#region "Interfaces functions"


#endregion


//try
            //{

            //}
            //catch (Exception ex)
            //{
            //    blnValidReturn = false;
            //    sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            //}
            //finally
            //{
            //    if (!blnValidReturn)
            //    {

            //    }
            //}