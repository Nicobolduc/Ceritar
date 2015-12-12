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

namespace Ceritar.Logirack_CVS
{
    public partial class frmCeritarClient : Form , ICeritarClient, Ceritar.TT3LightDLL.Controls.IFormController
    {
        //Controller
        private ctr_CeritarClient mcCtrCeritarClient;

        //Columns grdApp
        private const short mintGrdApp_Action_col = 1;
        private const short mintGrdApp_CeA_NRI= 2;
        private const short mintGrdApp_CeA_Name_col = 3;

        //Classes
        private clsC1FlexGridWrapper mcGrdApp;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Working variables
        private ushort mintCerClient_TS;


        public frmCeritarClient()
        {
            InitializeComponent();

            mcCtrCeritarClient = new ctr_CeritarClient((ICeritarClient) this);

            mcGrdApp = new clsC1FlexGridWrapper();
            mcGrdApp.SetGridDisplay += new clsC1FlexGridWrapper.SetDisplayEventHandler(mcGrdApp_SetGridDisplay);
        }


 #region "Functions"

        private bool pfblnGrdModules_Load()
        {
            bool blnValidReturn = false;

            try
            {
                blnValidReturn = mcGrdApp.bln_FillData(mcCtrCeritarClient.strGetListe_Application_SQL(formController.Item_ID));
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
                sqlRecord = clsSQL.ADOSelect(mcCtrCeritarClient.strGetDataLoad_SQL(formController.Item_ID));

                if (sqlRecord.Read())
                {
                    UInt16.TryParse(sqlRecord["CeC_TS"].ToString(), out mintCerClient_TS);
                    txtName.Text = sqlRecord["CeC_Name"].ToString();
                   
      

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

        private void ctlFormController1_LoadData(TT3LightDLL.Controls.LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;

            if (!mcGrdApp.bln_Init(ref grdApp, ref btnGrdAdd, ref btnGrdDel))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrCeritarClient.strGetApplications_SQL(),"CeA_NRI","CeA_Name",false, ref cboApp))
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
            grdApp.Cols[mintGrdApp_CeA_Name_col].Width = 30;

            if (grdApp.Rows.Count > 1)
            {
                cboApp.Visible = true;
            }

            grdApp.Cols[mintGrdApp_CeA_Name_col].Style = grdApp.Styles.Normal;
            grdApp.Cols[mintGrdApp_CeA_Name_col].Style.Editor = cboApp;

           
        }

        private void txtNom_TextChanged(object sender, EventArgs e)
        {
            formController.ChangeMade = true;
        }
        

#region "Interfaces functions"  

        int ICeritarClient.GetCerClient_NRI()
        {
            return formController.Item_ID;
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
            
            for (int intRowIdx = 1; intRowIdx <= grdApp.Rows.Count - 1; intRowIdx++)
            {
                lstApplication.Add((int)grdApp[intRowIdx,mintGrdApp_CeA_Name_col]);
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
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cboApp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grdApp_DoubleClick(object sender, EventArgs e)
        {
            if(grdApp.Rows.Count > 1 & grdApp.Row > 0)
            {
                switch (grdApp.Col)
                {
                    case mintGrdApp_CeA_Name_col:

                        grdApp.StartEditing();

                        break;

                }
            }
        }
    }
}
