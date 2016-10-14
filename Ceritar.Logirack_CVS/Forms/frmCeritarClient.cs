using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.CVS.Controllers;
using System;
using Ceritar.CVS.Controllers.Interfaces;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Controls;

namespace Ceritar.Logirack_CVS.Forms
{
    /// <summary>
    /// Cette classe contient les fonctions et évènements de la vue permettant de définir les clients de Ceritar.
    /// </summary>
    public partial class frmCeritarClient : Form , ICeritarClient, Ceritar.TT3LightDLL.Controls.IFormController
    {
        //Controller
        private ctr_CeritarClient mcCtrCeritarClient;

        //Columns grdApp
        private const short mintGrdApp_CeA_Name_col = 1;
        private const short mintGrdApp_CurrentVersion_col = 2;
        private const short mintGrdApp_CurrentRevision_col = 3;

        //Classes
        private clsTTC1FlexGridWrapper mcGrdApp;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Working variables
        private ushort mintCerClient_TS;


        public frmCeritarClient()
        {
            InitializeComponent();

            mcCtrCeritarClient = new ctr_CeritarClient((ICeritarClient) this);

            mcGrdApp = new clsTTC1FlexGridWrapper();
            mcGrdApp.HasActionColumn = false;
            mcGrdApp.SetGridDisplay += new clsTTC1FlexGridWrapper.SetDisplayEventHandler(mcGrdApp_SetGridDisplay);
        }


#region "Interfaces functions"

        int ICeritarClient.GetCerClient_NRI()
        {
            return formController.Item_NRI;
        }

        int ICeritarClient.GetCerClient_TS()
        {
            return mintCerClient_TS;
        }

        string ICeritarClient.GetName()
        {
            return txtName.Text;
        }

        List<int> ICeritarClient.GetLstCerApplication()
        {
            List<int> lstApplication = new List<int>();

            for (int intRowIdx = 1; intRowIdx <= grdCerApp.Rows.Count - 1; intRowIdx++)
            {
                lstApplication.Add((int)grdCerApp[intRowIdx, mintGrdApp_CeA_Name_col]);
            }

            return lstApplication;
        }

        sclsConstants.DML_Mode ICeritarClient.GetDML_Mode()
        {
            return formController.FormMode;
        }

        ctlFormController IFormController.GetFormController()
        {
            return this.formController;
        }

        bool ICeritarClient.GetIsActive()
        {
            return chkActive.Checked;
        }

#endregion


 #region "Functions"

        private bool pfblnGrdModules_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdApp.bln_FillData(mcCtrCeritarClient.strGetListe_Application_SQL(formController.Item_NRI));
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
                sqlRecord = clsTTSQL.ADOSelect(mcCtrCeritarClient.strGetDataLoad_SQL(formController.Item_NRI));

                if (sqlRecord.Read())
                {
                    UInt16.TryParse(sqlRecord["CeC_TS"].ToString(), out mintCerClient_TS);
                    txtName.Text = sqlRecord["CeC_Name"].ToString();

                    chkActive.Checked = Convert.ToBoolean(sqlRecord["CeC_IsActive"].ToString());

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


        private void formController_LoadData(LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;
            Button btnPlaceHolder = null;

            if (!mcGrdApp.bln_Init(ref grdCerApp, ref btnPlaceHolder, ref btnPlaceHolder))
            { }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
            {
                blnValidReturn = true;
            }
            else if (!pfblnGrdModules_Load())
            { }
            else if (!pfblnData_Load())
            { }
            else
            {
                blnValidReturn = true;
            }

            if (!blnValidReturn) this.Close();
        }

        private void mcGrdApp_SetGridDisplay()
        {
            grdCerApp.Cols[mintGrdApp_CeA_Name_col].Width = 200;
            grdCerApp.Cols[mintGrdApp_CurrentVersion_col].Width = 100;
            grdCerApp.Cols[mintGrdApp_CurrentRevision_col].Width = 100;
        }

        private void txtNom_TextChanged(object sender, EventArgs e)
        {
            formController.ChangeMade = true;
        }        

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void formController_ValidateForm(ValidateFormEventArgs eventArgs)
        {
            mcActionResults = mcCtrCeritarClient.Validate();

            if (!mcActionResults.IsValid)
            {
                clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI);

                switch ((ctr_CeritarClient.ErrorCode_CeC)mcActionResults.GetErrorCode)
                {
                    case ctr_CeritarClient.ErrorCode_CeC.NAME_MANDATORY:
                    case ctr_CeritarClient.ErrorCode_CeC.NAME_INVALID:

                        txtName.Focus();
                        txtName.SelectAll();

                        break;
                }
            }

            eventArgs.IsValid = mcActionResults.IsValid;
        }

        private void formController_SaveData(SaveDataEventArgs eventArgs)
        {
            mcActionResults = mcCtrCeritarClient.Save();

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

    }
}
