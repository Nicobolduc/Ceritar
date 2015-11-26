using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers;

namespace Ceritar.Logirack_CVS
{
    public partial class frmCeritarApp : Form, Ceritar.CVS.Controllers.Interfaces.ICeritarApp
    {
        //Controller
        private ctr_CeritarApplication mcCtrCeritarApp;

        //Columns grdModules
        private const short mintGrdMod_ApM_NRI_col = 1;
        private const short mintGrdMod_ApM_Description_col = 2;

        //Classes
        private clsFlexGridWrapper mcGrdModules;
        private Ceritar.CVS.clsActionResults mcActionResults;

        public frmCeritarApp()
        {
            InitializeComponent();

            mcCtrCeritarApp = new ctr_CeritarApplication((Ceritar.CVS.Controllers.Interfaces.ICeritarApp)this);

            mcGrdModules = new clsFlexGridWrapper();
            mcGrdModules.SetDisplay += new clsFlexGridWrapper.SetDisplayEventHandler(mcGrdModules_SetDisplay);
            mcGrdModules.ValidateGridData += mcGrdModules_ValidateGridData;
            mcGrdModules.SaveGridData += mcGrdModules_SaveGridData;
        }

        private bool pfblnGrdModules_Load()
        {
            bool blnValidReturn = false;

            blnValidReturn = mcGrdModules.bln_FillData(mcCtrCeritarApp.strGetListe_Modules(formController.Item_ID));

            return blnValidReturn;
        }

        private void formController_LoadData(TT3LightDLL.Controls.LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;

            mcGrdModules.bln_Init(ref grdModules, ref btnGrdAdd, ref btnGrdDel);

            blnValidReturn = pfblnGrdModules_Load();

            if (!blnValidReturn)
            {
                this.Close();
            }
        }

        void mcGrdModules_SaveGridData(SaveGridDataEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        void mcGrdModules_ValidateGridData(ValidateGridEventArgs eventArgs)
        {
            throw new NotImplementedException();
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

        string CVS.Controllers.Interfaces.ICeritarApp.GetDescription()
        {
            return txtName.Text;
        }

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

        TT3LightDLL.Static_Classes.sclsConstants.DML_Mode CVS.Controllers.Interfaces.ICeritarApp.GetDML_Mode()
        {
            return formController.FormMode;
        }
    }
}
