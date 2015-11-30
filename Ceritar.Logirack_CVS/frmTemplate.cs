using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Ceritar.CVS.Controllers;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using System.ComponentModel;
using C1.Win.C1FlexGrid;
using System.Windows.Forms;
using Ceritar.CVS.Controllers.Interfaces;

namespace Ceritar.Logirack_CVS
{
    public partial class frmTemplate : Form, Ceritar.TT3LightDLL.Controls.IFormController, Ceritar.CVS.Controllers.Interfaces.ITemplate
    {
        //Controller
        private ctr_Template mcCtrTemplate;

        //Columns grdTemplates
        private const short mintGrdTpl_Action_col = 1;
        private const short mintGrdTpl_HiCo_NRI_col = 2;
        private const short mintGrdTpl_HiCo_IsSystemItem_col = 3;
        private const short mintGrdTpl_HiCo_Level_col = 4;
        private const short mintGrdTpl_HiCo_IsRoot_col = 5;
        private const short mintGrdTpl_HiCo_Name_col = 6;

        //Classes
        private clsFlexGridWrapper mcGrdTemplate;
        private Ceritar.CVS.clsActionResults mcActionResults;
        private CellStyle csSystemItem;

        //Working variables
        private ushort mintTpl_TS;
        private bool mblnFormIsLoading;


        public frmTemplate()
        {
            InitializeComponent();

            mcCtrTemplate = new ctr_Template((Ceritar.CVS.Controllers.Interfaces.ITemplate) this);
                   
            mcGrdTemplate = new clsFlexGridWrapper();
            mcGrdTemplate.SetGridDisplay += mcGrdTemplate_SetGridDisplay;

            csSystemItem = grdTemplate.Styles.Add("SystemItem");
            csSystemItem.Font = new System.Drawing.Font(csSystemItem.Font.Name, csSystemItem.Font.Size, System.Drawing.FontStyle.Bold);
            csSystemItem.BackColor = System.Drawing.Color.LightGray;
        }


#region "Interfaces functions"

        bool CVS.Controllers.Interfaces.ITemplate.GetByDefaultValue()
        {
            return chkByDefault.Checked;
        }

        int CVS.Controllers.Interfaces.ITemplate.GetCeritarApplication_NRI()
        {
            return (int)cboApplications.SelectedValue;
        }

        sclsConstants.DML_Mode CVS.Controllers.Interfaces.ITemplate.GetDML_Action()
        {
            return formController.FormMode;
        }

        string CVS.Controllers.Interfaces.ITemplate.GetTemplateName()
        {
            return txtName.Text;
        }

        int CVS.Controllers.Interfaces.ITemplate.GetTemplateType_NRI()
        {
            return (int)cboTypes.SelectedValue;
        }

        int CVS.Controllers.Interfaces.ITemplate.GetTemplate_NRI()
        {
            return formController.Item_ID;
        }

        ushort CVS.Controllers.Interfaces.ITemplate.GetTemplate_TS()
        {
            return mintTpl_TS;
        }

        System.Collections.Generic.List<structHierarchyComponent> ITemplate.GetHierarchyComponentList()
        {
            List<structHierarchyComponent> lstHierarchyComponents = new List<structHierarchyComponent>();



            return lstHierarchyComponents;
        }

        TT3LightDLL.Controls.ctlFormController TT3LightDLL.Controls.IFormController.GetFormController()
        {
            return this.formController;
        }

#endregion


#region "Functions"

        private bool pfblnGrdTemplate_Load()
        {
            bool blnValidReturn = false;
            System.Data.SqlClient.SqlDataReader cDataReader = null;

            try
            {
                cDataReader = clsSQL.ADOSelect(mcCtrTemplate.strGetListe_HierarchyComponents_SQL(formController.Item_ID));

                if (!cDataReader.HasRows) blnValidReturn = false;

                while (cDataReader.Read())
                {
                    mcGrdTemplate.AddTreeItem(mintGrdTpl_HiCo_Name_col, cDataReader["HiCo_Name"].ToString(), Int16.Parse(cDataReader["HiCo_NodeLevel"].ToString()), true);

                    grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_NRI_col] = cDataReader["HiCo_NRI"];
                    grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] = cDataReader["IsSystem"];
                    grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col] = cDataReader["HiCo_NodeLevel"];
                    grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsRoot_col] = cDataReader["IsRoot"];

                    if (mcGrdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] == "1")
                    {
                        CellRange crRow = grdTemplate.GetCellRange(grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Name_col);

                        crRow.Style = csSystemItem;
                    }
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (cDataReader != null) cDataReader.Dispose();
            }

            return blnValidReturn;
        }

        private bool pfblnData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;

            try
            {
                sqlRecord = clsSQL.ADOSelect(mcCtrTemplate.strGetDataLoad_SQL(formController.Item_ID));

                if (sqlRecord.Read())
                {
                    mintTpl_TS = UInt16.Parse(sqlRecord["Tpl_TS"].ToString());
                    txtName.Text = sqlRecord["Tpl_Name"].ToString();
                    cboTypes.SelectedValue = Int32.Parse(sqlRecord["TeT_NRI"].ToString());
                    cboApplications.SelectedValue = Int32.Parse(sqlRecord["CeA_NRI"].ToString());
                    chkByDefault.Checked = Convert.ToBoolean(sqlRecord["Tpl_ByDefault"]);

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
        

        private void btnAddNode_Click(object sender, EventArgs e)
        {
            if (mcGrdTemplate[grdTemplate.Row + 1, mintGrdTpl_HiCo_IsSystemItem_col] == "0" | String.IsNullOrEmpty(mcGrdTemplate[grdTemplate.Row + 1, mintGrdTpl_HiCo_IsSystemItem_col]))
            {
                mcGrdTemplate.AddTreeItem(mintGrdTpl_HiCo_Name_col, "", grdTemplate.Rows[grdTemplate.Row].Node.Level, true, grdTemplate.Row + 1);
            }
        }

        void mcGrdTemplate_SetGridDisplay()
        {
            grdTemplate.Tree.Column = mintGrdTpl_HiCo_Name_col;
        }

        private void formController_LoadData(TT3LightDLL.Controls.LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false; 
            Button placeHolder = null;

            mblnFormIsLoading = true;

            if (!mcGrdTemplate.bln_Init(ref grdTemplate, ref placeHolder, ref placeHolder, true))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrTemplate.strGetListe_Applications_SQL(), "CeA_NRI", "CeA_Name", true, ref cboApplications))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrTemplate.strGetListe_TemplateTypes_SQL(), "TeT_NRI", "TeT_Code", false, ref cboTypes))
            { }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
            {
                blnValidReturn = true;
            }
            else if (pfblnGrdTemplate_Load()) 
            { }
            else if (!pfblnData_Load())
            { }
            else
            {
                blnValidReturn = true;
            }

            mblnFormIsLoading = false;

            if (!blnValidReturn) this.Close();
        }

        private void grdTemplate_DoubleClick(object sender, EventArgs e)
        {
            if (grdTemplate.Rows.Count > 1 && grdTemplate.Row > 0 && mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_IsSystemItem_col] != "1")
            {
                grdTemplate.StartEditing();
            }
        }

        private void formController_SetReadRights()
        {
            switch (formController.FormMode)
            {
                case sclsConstants.DML_Mode.INSERT_MODE:

                    txtName.Enabled = true;

                    cboApplications.Enabled = true;
                    cboTypes.Enabled = true;

                    btnAddNode.Enabled = false;
                    btnMoveLeft.Enabled = false;
                    btnMoveRight.Enabled = false;

                    break;

                case sclsConstants.DML_Mode.UPDATE_MODE:

                    txtName.Enabled = false;

                    cboApplications.Enabled = false;
                    cboTypes.Enabled = false;

                    btnAddNode.Enabled = true;
                    btnMoveLeft.Enabled = true;
                    btnMoveRight.Enabled = true;

                    break;
            }
        }

        private void cboApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mblnFormIsLoading)
            {
                if (cboApplications.SelectedIndex > 0)
                {
                    pfblnGrdTemplate_Load();

                    mcGrdTemplate.AddTreeItem(mintGrdTpl_HiCo_Name_col, cboApplications.GetItemText(cboApplications.SelectedItem), Int32.Parse(grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col].ToString()) + 1, true);

                    grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_Action_col] = sclsConstants.DML_Mode.INSERT_MODE;
                    grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_NRI_col] = 0;
                    grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] = "1";
                    grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Rows.Count - 1].Node.Level;
                    grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsRoot_col] = "1";

                    CellRange crRow = grdTemplate.GetCellRange(grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Name_col);

                    crRow.Style = csSystemItem;

                    btnAddNode.Enabled = true;
                    btnMoveLeft.Enabled = true;
                    btnMoveRight.Enabled = true;

                    grdTemplate.Row = grdTemplate.Rows.Count - 1;
                }
                else
                {
                    grdTemplate.Rows.Count = 1;
                }
            }         
        }

        private void formController_SaveData(TT3LightDLL.Controls.SaveDataEventArgs eventArgs)
        {
            mcActionResults = mcCtrTemplate.Save();

            eventArgs.SaveSuccessful = mcActionResults.IsValid;
        }
    }
}
