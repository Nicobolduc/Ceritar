using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Ceritar.CVS.Controllers;
using Ceritar.TT3LightDLL.Controls;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;

namespace Ceritar.Logirack_CVS
{
    /// <summary>
    /// Cette classe contient les fonctions et évènements de la vue permettant de définir les révisions d'une version.
    /// </summary>
    public partial class frmRevision : Form, IFormController, Ceritar.CVS.Controllers.Interfaces.IRevision
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
        private const short mintGrdSat_CSA_Sel_col = 4;

        //Classes
        private clsC1FlexGridWrapper mcGrdRevModifs;
        private clsC1FlexGridWrapper mcGrdSatellites;
        private Ceritar.CVS.clsActionResults mcActionResults;

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

            mcGrdSatellites = new clsC1FlexGridWrapper();
            mcGrdSatellites.SetGridDisplay += mcGrdSatellites_SetGridDisplay;

            dtpCreation.Value = DateTime.Now;
        }


#region "Interfaces functions"

        string CVS.Controllers.Interfaces.IRevision.GetCompiledBy()
        {
            return txtCreatedBy.Text;
        }

        string CVS.Controllers.Interfaces.IRevision.GetCreationDate()
        {
            return dtpCreation.Value.ToString(clsApp.GetAppController.str_GetServerDateTimeFormat);
        }

        sclsConstants.DML_Mode CVS.Controllers.Interfaces.IRevision.GetDML_Action()
        {
            return formController.FormMode;
        }

        string CVS.Controllers.Interfaces.IRevision.GetLocation_Release()
        {
            return txtReleasePath.Text;
        }

        string CVS.Controllers.Interfaces.IRevision.GetLocation_Scripts()
        {
            return txtScriptsPath.Text;
        }

        byte CVS.Controllers.Interfaces.IRevision.GetRevisionNo()
        {
            return byte.Parse(txtRevisionNo.Text);
        }

        int CVS.Controllers.Interfaces.IRevision.GetRevision_NRI()
        {
            return formController.Item_NRI;
        }

        int CVS.Controllers.Interfaces.IRevision.GetRevision_TS()
        {
            return mintRevision_TS;
        }

        int CVS.Controllers.Interfaces.IRevision.GetTemplateSource_NRI()
        {
            return (int)(cboTemplates.SelectedValue == null ? 0 : cboTemplates.SelectedValue);
        }

        ushort CVS.Controllers.Interfaces.IRevision.GetVersionNo()
        {
            return UInt16.Parse(txtVersionNo.Text);
        }

        ctlFormController IFormController.GetFormController()
        {
            return formController;
        }

        int CVS.Controllers.Interfaces.IRevision.GetCeritarClient_NRI()
        {
            return (int)(cboClients.SelectedValue == null ? 0 : cboClients.SelectedValue);
        }

        string CVS.Controllers.Interfaces.IRevision.GetCeritarClient_Name()
        {
            return cboClients.GetItemText(cboClients.SelectedItem);
        }

        System.Collections.Generic.List<string> CVS.Controllers.Interfaces.IRevision.GetModificationsList()
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

        int CVS.Controllers.Interfaces.IRevision.GetVersion_NRI()
        {
            return mintVersion_NRI;
        }

        string CVS.Controllers.Interfaces.IRevision.GetCeritarApplication_Name()
        {
            return mstrCeritarApplication_Name;
        }

        string CVS.Controllers.Interfaces.IRevision.GetSelectedSatellitteApp_Name()
        {
            string strSatelliteAppName = string.Empty;

            for (int intRowIndex = 0; intRowIndex < grdSatellites.Rows.Count; intRowIndex++)
            {
                if (mcGrdSatellites.bln_CellIsChecked(intRowIndex, mintGrdSat_CSA_Sel_col)) 
                    strSatelliteAppName = mcGrdSatellites[intRowIndex, mintGrdSat_CSA_Name_col];
            }

            return strSatelliteAppName;
        }

        int CVS.Controllers.Interfaces.IRevision.GetSelectedSatellitteApp_NRI()
        {
            int intSatelliteApp_NRI = 0;

            for (int intRowIndex = 0; intRowIndex < grdSatellites.Rows.Count; intRowIndex++)
            {
                if (mcGrdSatellites.bln_CellIsChecked(intRowIndex, mintGrdSat_CSA_Sel_col))
                    Int32.TryParse(mcGrdSatellites[intRowIndex, mintGrdSat_CSA_NRI_col], out intSatelliteApp_NRI);
            }

            return intSatelliteApp_NRI;
        }

        bool CVS.Controllers.Interfaces.IRevision.GetExeIsExternalReport()
        {
            return chkExeIsRPT.Checked;
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
                    mstrCeritarApplication_Name = sqlRecord["CeA_Name"].ToString();

                    if (formController.FormMode != sclsConstants.DML_Mode.INSERT_MODE)
                    {
                        UInt16.TryParse(sqlRecord["Rev_TS"].ToString(), out mintRevision_TS);

                        txtReleasePath.Text = sqlRecord["Rev_Location_Exe"].ToString();
                        txtScriptsPath.Text = sqlRecord["Rev_Location_Scripts"].ToString();

                        cboClients.SelectedValue = Int32.Parse(sqlRecord["CeC_NRI"].ToString());
                        cboTemplates.SelectedValue = Int32.Parse(sqlRecord["Tpl_NRI"].ToString());

                        dtpCreation.Value = DateTime.Parse(sqlRecord["Rev_DtCreation"].ToString());

                        chkExeIsRPT.Checked = Convert.ToBoolean(sqlRecord["Rev_ExeIsReport"].ToString());

                        if (chkExeIsRPT.Checked)
                        {
                            pfblnGrdSatellites_UncheckAll();
                            btnSelectExecutableFolderPath.Enabled = false;
                            grdSatellites.Enabled = false;
                        }
                        else
                        {
                            grdSatellites.Enabled = true;
                            btnSelectExecutableFolderPath.Enabled = true;
                        }
                    }

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

        private bool pfblnGrdSatellites_UncheckAll()
        {
            bool blnValidReturn = true;

            try
            {
                for (int intRowIndex = 1; intRowIndex < grdSatellites.Rows.Count; intRowIndex++)
                {
                    grdSatellites[intRowIndex, mintGrdSat_CSA_Sel_col] = "0";
                }
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

                        formController.ChangeMade = true;
                    }
                }
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

#endregion


        private void formController_LoadData(LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;
            Button btnPlaceHolder = null;

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

            if (formController.FormMode != sclsConstants.DML_Mode.DELETE_MODE)
            {
                blnValidReturn = mcCtrRevision.blnBuildRevisionHierarchy((int)cboTemplates.SelectedValue);
            }
            else
            {
                blnValidReturn = true;
            }
            
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
            grdSatellites.Cols[mintGrdSat_CSA_Name_col].Width = 220;

            mcGrdSatellites.SetColType_CheckBox(mintGrdSat_CSA_Sel_col);
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
            ShowOpenFileDialog("Scripts (*.sql)|*.sql|(*.txt)|*.txt", txtScriptsPath, txtScriptsPath.Text, true);
        }

        private void btnSelectExecutableFilePath_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog("Executable (*.exe)|*.exe", txtReleasePath, txtReleasePath.Text, true);
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

                    break;
            }
        }

        private void grdSatellites_DoubleClick(object sender, EventArgs e)
        {
            switch (grdSatellites.Col)
            {
                case mintGrdSat_CSA_Sel_col:

                    mcGrdSatellites.GridIsLoading = true;

                    int intSelectedRow = grdSatellites.FindRow("true", 1, mintGrdSat_CSA_Sel_col, false, true, false);

                    if (intSelectedRow >= 1 & intSelectedRow != grdSatellites.Row) mcGrdSatellites[intSelectedRow, mintGrdSat_CSA_Sel_col] = "0";

                    mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_Sel_col] = (mcGrdSatellites[grdSatellites.Row, mintGrdSat_CSA_Sel_col] == "0" ? "1" : "0");

                    mcGrdSatellites.GridIsLoading = false;

                    formController.ChangeMade = true;

                    break;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;

                if (chkExeIsRPT.Checked)
                {
                    pfblnGrdSatellites_UncheckAll();
                    grdSatellites.Enabled = false;
                    btnSelectExecutableFolderPath.Enabled = false;                    
                }
                else
                {
                    txtReleasePath.Text = string.Empty;
                    grdSatellites.Enabled = true;
                    btnSelectExecutableFolderPath.Enabled = true;
                }
            }
        }

    }
}
