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

        //Classes
        private clsC1FlexGridWrapper mcGrdModules;
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Working variables
        private ushort mintCerApp_TS;


        public frmCeritarApp()
        {
            InitializeComponent();
            
            mcCtrCeritarApp = new ctr_CeritarApplication((Ceritar.CVS.Controllers.Interfaces.ICeritarApp) this);

            mcGrdModules = new clsC1FlexGridWrapper();
            mcGrdModules.SetGridDisplay += new clsC1FlexGridWrapper.SetDisplayEventHandler(mcGrdModules_SetDisplay);
            //mcGrdModules.ValidateGridData += mcGrdModules_ValidateGridData;
            //mcGrdModules.SaveGridData += mcGrdModules_SaveGridData;
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

        private bool pfblnData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;

            try
            {
                sqlRecord = clsSQL.ADOSelect(mcCtrCeritarApp.strGetDataLoad_SQL(formController.Item_NRI));

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

#endregion
        

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

            if (!blnValidReturn) this.Close();
        }

        private void mcGrdModules_SetDisplay()
        {
            grdModules.Cols[mintGrdMod_ApM_Description_col].Width = 30;
        }

        private void formController_ValidateForm(TT3LightDLL.Controls.ValidateFormEventArgs eventArgs)
        {
            mcActionResults = mcCtrCeritarApp.Validate();

            if (!mcActionResults.IsValid)
            {
                switch ((ctr_CeritarApplication.ErrorCode_CeA)mcActionResults.GetErrorCode)
                {
                    case ctr_CeritarApplication.ErrorCode_CeA.DESCRIPTION_MANDATORY:

                        txtDescription.Focus();
                        txtDescription.SelectAll();
                        break;

                    case ctr_CeritarApplication.ErrorCode_CeA.NAME_MANDATORY:

                        txtName.Focus();
                        txtName.SelectAll();
                        break;

                    case ctr_CeritarApplication.ErrorCode_CeA.MODULES_LIST_MANDATORY:

                        btnGrdAdd.Focus();
                        break;

                    case ctr_CeritarApplication.ErrorCode_CeA.DOMAIN_MANDATORY:

                        cboDomain.Focus();
                        cboDomain.DroppedDown = true;
                        break;
                }

                clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI);
            }
            else
            {
                //Do nothing
            }    

            eventArgs.IsValid = mcActionResults.IsValid;
        }

        private void formController_SaveData(TT3LightDLL.Controls.SaveDataEventArgs eventArgs)
        {
            mcActionResults = mcCtrCeritarApp.Save();
            
            if (!mcActionResults.IsValid)
            {
                clsApp.GetAppController.ShowMessage(mcActionResults.GetMessage_NRI);
            }
            formController.Item_NRI = mcActionResults.GetNewItem_NRI;
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
                    else
                    {
                        //Do nothing
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

        private void grdModules_Validating(object sender, CancelEventArgs e)
        {
            if (mcGrdModules[grdModules.Row, mintGrdMod_ApM_Description_col] == "")
            {
                e.Cancel = true;
                clsApp.GetAppController.ShowMessage((int)sclsConstants.Validation_Message.MANDATORY_GRID);
            }
        }

        //private void grdModules_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        //{
        //    if (mcGrdModules[grdModules.Row, mintGrdMod_Action_col] == ((int)sclsConstants.DML_Mode.INSERT_MODE).ToString())
        //    {
        //        grdModules.StartEditing(grdModules.Row, mintGrdMod_Action_col);
        //    }
        //}

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