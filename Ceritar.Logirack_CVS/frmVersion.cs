using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Controls;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.CVS.Controllers.Interfaces;
using Ceritar.CVS.Controllers;


namespace Ceritar.Logirack_CVS
{
    public partial class frmVersion : Form, IFormController, IVersion
    {
        //Controller
        private ctr_Version mcCtrVersion;

        //Columns grdClients
        private const short mintGrdClients_Action_col = 1;
        private const short mintGrdClients_CeC_NRI_col = 2;
        private const short mintGrdClients_CeC_TS_col = 3;
        private const short mintGrdClients_CeC_Name_col = 4;

        //Columns grdRevModif
        private const short mintGrdRevMod_Action_col = 1;
        private const short mintGrdRevMod_RevM_NRI_col = 2;
        private const short mintGrdRevMod_RevM_Description_col = 3;

        //Classes
        private clsC1FlexGridWrapper mcGrdClients;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Working variables
        private ushort mintVersion_TS;


        public frmVersion()
        {
            InitializeComponent();

            mcCtrVersion = new ctr_Version((IVersion)this);

            mcGrdClients = new clsC1FlexGridWrapper();
            mcGrdClients.SetGridDisplay += mcGrdClients_SetGridDisplay;

        }

        

#region "Interfaces functions"

        ctlFormController IFormController.GetFormController()
        {
            return this.formController;
        }

#endregion
        
        
#region "Functions"

        private bool pfblnGrdClients_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdClients.bln_FillData(mcCtrVersion.strGetListe_Clients_SQL());
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
                sqlRecord = clsSQL.ADOSelect(mcCtrVersion.strGetDataLoad_SQL(formController.Item_ID));

                if (sqlRecord.Read())
                {
                    UInt16.TryParse(sqlRecord["Ver_TS"].ToString(), out mintVersion_TS);

                    txtCompiledBy.Text = sqlRecord["Ver_CompiledBy"].ToString();
                    txtVersionNo.Text = sqlRecord["Ver_No"].ToString();

                    dtpBuild.Value = DateTime.Parse(sqlRecord["Ver_DtCreation"].ToString());

                    cboApplications.SelectedValue = Int32.Parse(sqlRecord["CeA_NRI"].ToString());
                    cboGabarits.SelectedValue = Int32.Parse(sqlRecord["Tpl_NRI"].ToString());

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
                }
            }
        }

        private void ShowFolderBrowserDialog(ref TextBox txtAffected)
        {
            DialogResult dialogResult;

            if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
            {
                folderBrowserDialog.ShowNewFolderButton = false;

                if (txtAffected.Name == txtExecutablePath.Name && !string.IsNullOrEmpty(txtExecutablePath.Text))
                {
                    folderBrowserDialog.SelectedPath = txtExecutablePath.Text;
                }
                else
                {
                    folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                }
                
                dialogResult = folderBrowserDialog.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtAffected.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }


#endregion


        void mcGrdClients_SetGridDisplay()
        {}

        private void formController_LoadData(LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;

            if (!mcGrdClients.bln_Init(ref grdClients, ref btnGrdClientsAdd, ref btnGrdClientsDel))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetApplications_SQL(), "CeA_NRI", "CeA_Name", false, ref cboApplications))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrVersion.strGetTemplates_SQL(), "Tpl_NRI", "Tpl_Name", false, ref cboGabarits))
            { }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
            {
                blnValidReturn = true;
            }
            else if (!pfblnGrdClients_Load())
            { }
            else if (!pfblnData_Load())
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

        private void btnReplaceAppChangeDOC_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog("Word Documents (.docx)|*.docx", ref txtWordAppChangePath);
        }

        private void btnReplaceAppChangeXLS_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog("XML files (*.xml)|*.xml|*.xls|*.xlsx", ref txtExcelAppChangePath); 
        }

        private void btnReplaceTTApp_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog("Access files (*.mdb)|*.mdb", ref txtTTAppPath);
        }

        private void btnReplaceExecutable_Click(object sender, EventArgs e)
        {
            ShowFolderBrowserDialog(ref txtExecutablePath);
        }
    }
}
