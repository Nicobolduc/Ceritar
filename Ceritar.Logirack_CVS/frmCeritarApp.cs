using System;
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

namespace Ceritar.Logirack_CVS
{
    public partial class frmCeritarApp : Form, Ceritar.CVS.Controllers.Interfaces.ICeritarApp, Ceritar.TT3LightDLL.Controls.IFormController
    {
        //Controller
        private ctr_CeritarApplication mcCtrCeritarApp;

        //Columns grdModules
        private const short mintGrdMod_Action_col = 1;
        private const short mintGrdMod_ApM_NRI_col = 2;
        private const short mintGrdMod_ApM_TS_col = 3;
        private const short mintGrdMod_ApM_Description_col = 4;

        //Classes
        private clsFlexGridWrapper mcGrdModules;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Working variables
        private ushort mintCerApp_TS;


        public frmCeritarApp()
        {
            InitializeComponent();

            mcCtrCeritarApp = new ctr_CeritarApplication((Ceritar.CVS.Controllers.Interfaces.ICeritarApp)this);

            mcGrdModules = new clsFlexGridWrapper();
            mcGrdModules.SetGridDisplay += new clsFlexGridWrapper.SetDisplayEventHandler(mcGrdModules_SetDisplay);
            //mcGrdModules.ValidateGridData += mcGrdModules_ValidateGridData;
            //mcGrdModules.SaveGridData += mcGrdModules_SaveGridData;
        }


#region "IFormController"

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
            return txtDescription.Text;
        }

        TT3LightDLL.Static_Classes.sclsConstants.DML_Mode CVS.Controllers.Interfaces.ICeritarApp.GetDML_Mode()
        {
            return formController.FormMode;
        }

        string CVS.Controllers.Interfaces.ICeritarApp.GetDescription()
        {
            return txtName.Text;
        }

        int CVS.Controllers.Interfaces.ICeritarApp.GetDomain_NRI()
        {
            return (int)cboDomain.SelectedValue;
        }

        int CVS.Controllers.Interfaces.ICeritarApp.GetCerApp_NRI()
        {
            return formController.Item_ID;
        }

        void TT3LightDLL.Controls.IFormController.ShowForm(sclsConstants.DML_Mode vintFormMode, int rintItem_ID, bool vblnIsModal)
        {
            formController.ShowForm(vintFormMode, rintItem_ID, vblnIsModal);
        }

#endregion


        private bool pfblnGrdModules_Load()
        {
            bool blnValidReturn = false;

            blnValidReturn = mcGrdModules.bln_FillData(mcCtrCeritarApp.strGetListe_Modules_SQL(formController.Item_ID));

            return blnValidReturn;
        }

        private void formController_LoadData(TT3LightDLL.Controls.LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;

            if (!mcGrdModules.bln_Init(ref grdModules, ref btnGrdAdd, ref btnGrdDel))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrCeritarApp.strGetListe_Domains_SQL(), "ApD_NRI", "ApD_Code", false, ref cboDomain))
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

            if (!blnValidReturn)
            {
                this.Close();
            }
        }

        private void mcGrdModules_SetDisplay()
        {
            grdModules.Cols[mintGrdMod_ApM_Description_col].Width = 30;
        }

        private void formController_ValidateForm(TT3LightDLL.Controls.ValidateFormEventArgs eventArgs)
        {
            mcActionResults = mcCtrCeritarApp.Validate();

            eventArgs.IsValid = mcActionResults.IsValid;
        }

        private void formController_SaveData(TT3LightDLL.Controls.SaveDataEventArgs eventArgs)
        {
            mcActionResults = mcCtrCeritarApp.Save();

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

        private bool pfblnData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;

            try
            {
                sqlRecord = clsSQL.ADOSelect(mcCtrCeritarApp.strGetDataLoad_SQL(formController.Item_ID));

                if (sqlRecord.Read())
                {
                    UInt16.TryParse(sqlRecord["CeA_TS"].ToString(), out mintCerApp_TS);
                    txtName.Text = sqlRecord["CeA_Name"].ToString();
                    txtDescription.Text = sqlRecord["CeA_Desc"].ToString();
                    cboDomain.SelectedValue = Int32.Parse(sqlRecord["ApD_NRI"].ToString());

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

        private void grdModules_DoubleClick(object sender, EventArgs e)
        {
            switch (grdModules.Col)
            {
                case mintGrdMod_ApM_Description_col:

                    grdModules.StartEditing();
                    break;
            }
        }

        private void frmCeritarApp_Load(object sender, EventArgs e)
        {

        }
    }
}

#region "IFormController"


#endregion

#region "Properties"


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