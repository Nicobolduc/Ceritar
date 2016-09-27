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

namespace Ceritar.Logirack_CVS.Forms
{
    /// <summary>
    /// Cette classe contient les fonctions et évènements de la vue permettant de définir les gabarits d'une hiérarchie.
    /// </summary>
    public partial class frmTemplate : Form, Ceritar.TT3LightDLL.Controls.IFormController, Ceritar.CVS.Controllers.Interfaces.ITemplate
    {
        //Controller
        private ctr_Template mcCtrTemplate;

        //Columns grdTemplates
        private const short mintGrdTpl_HiCo_Action_col = 1;
        private const short mintGrdTpl_HiCo_NRI_col = 2;
        private const short mintGrdTpl_HiCo_IsSystemItem_col = 3;
        private const short mintGrdTpl_HiCo_Level_col = 4;
        private const short mintGrdTpl_HiCo_IsNode_col = 5;
        private const short mintGrdTpl_HiCo_Name_col = 6;
        private const short mintGrdTpl_HiCo_FolderType_NRI_col = 7;
        private const short mintGrdTpl_HiCo_FolderType_col = 8;

        //Classes
        private clsTTC1FlexGridWrapper mcGrdTemplate;
        private Ceritar.CVS.clsActionResults mcActionResults;
        private CellStyle csSystemItem;

        //Working variables
        private ushort mintTpl_TS;

        
        public frmTemplate()
        {
            InitializeComponent();
            
            mcCtrTemplate = new ctr_Template((Ceritar.CVS.Controllers.Interfaces.ITemplate) this);
                   
            mcGrdTemplate = new clsTTC1FlexGridWrapper();
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
            return formController.Item_NRI;
        }

        int CVS.Controllers.Interfaces.ITemplate.GetTemplate_NRI_Ref()
        {
            if (cboTemplate.SelectedValue != null)
            {
                return (int)cboTemplate.SelectedValue;
            }
            return 0;
        }

        ushort CVS.Controllers.Interfaces.ITemplate.GetTemplate_TS()
        {
            return mintTpl_TS;
        }

        structHierarchyComponent ITemplate.GetRacineSystem()
        {
            structHierarchyComponent structHiCo = new structHierarchyComponent();
            int intLastSystemRow;

            if ((int)cboTypes.SelectedValue == (int)ctr_Template.TemplateType.VERSION)
            {
                structHiCo = new structHierarchyComponent();

                intLastSystemRow = grdTemplate.FindRow("0", 1, mintGrdTpl_HiCo_IsSystemItem_col, false, false, false);
            }
            else
            {
                intLastSystemRow = 1;
            }

            if (intLastSystemRow > 0)
            {
                structHiCo.intHierarchyComponent_NRI = Int32.Parse(mcGrdTemplate[intLastSystemRow, mintGrdTpl_HiCo_NRI_col]);
                structHiCo.intNodeLevel = UInt16.Parse(mcGrdTemplate[intLastSystemRow, mintGrdTpl_HiCo_Level_col]);
                structHiCo.strName = mcGrdTemplate[intLastSystemRow, mintGrdTpl_HiCo_Name_col];
                structHiCo.Action = (sclsConstants.DML_Mode)grdTemplate[intLastSystemRow, mintGrdTpl_HiCo_Action_col];
                structHiCo.FolderType = (ctr_Template.FolderType)grdTemplate[intLastSystemRow, mintGrdTpl_HiCo_FolderType_NRI_col];
                Int32.TryParse(mcGrdTemplate[intLastSystemRow - 1, mintGrdTpl_HiCo_NRI_col], out structHiCo.Parent_NRI);
            }

            return structHiCo;
        }

        System.Collections.Generic.List<structHierarchyComponent> ITemplate.GetHierarchyComponentList()
        {
            List<structHierarchyComponent> lstHierarchyComponents = new List<structHierarchyComponent>();
            structHierarchyComponent structHiCo;

            for (int intRowIndex = 1; intRowIndex < grdTemplate.Rows.Count; intRowIndex++)
            {
                if (mcGrdTemplate[intRowIndex, mintGrdTpl_HiCo_IsSystemItem_col] != "1" && 
                    (int)grdTemplate[intRowIndex, mintGrdTpl_HiCo_FolderType_NRI_col] != (int)ctr_Template.FolderType.Ceritar_Application
                   ) 
                {
                    structHiCo = new structHierarchyComponent();
                    structHiCo.intHierarchyComponent_NRI = Int32.Parse(mcGrdTemplate[intRowIndex, mintGrdTpl_HiCo_NRI_col]);
                    structHiCo.intNodeLevel = UInt16.Parse(mcGrdTemplate[intRowIndex, mintGrdTpl_HiCo_Level_col]);
                    structHiCo.strName = mcGrdTemplate[intRowIndex, mintGrdTpl_HiCo_Name_col];
                    structHiCo.Action = (sclsConstants.DML_Mode) grdTemplate[intRowIndex, mintGrdTpl_HiCo_Action_col];
                    structHiCo.FolderType = (ctr_Template.FolderType)grdTemplate[intRowIndex, mintGrdTpl_HiCo_FolderType_NRI_col];

                    lstHierarchyComponents.Add(structHiCo);
                }                
            }

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
            int intLastUnmodifiableRow = 0;
            int intFirstNameOnlyModifiableRow = 0;
            int intLastNameOnlyModifiableRow = 0;
            System.Data.SqlClient.SqlDataReader cDataReader = null;

            try
            {
                mcGrdTemplate.GridIsLoading = true;

                cDataReader = clsTTSQL.ADOSelect(mcCtrTemplate.strGetListe_HierarchyComponents_SQL(formController.Item_NRI, ((int)cboTypes.SelectedValue == (int)ctr_Template.TemplateType.REVISION ? true : false)));

                if (cDataReader.HasRows)
                {
                    while (cDataReader.Read())
                    {
                        mcGrdTemplate.AddTreeItem(mintGrdTpl_HiCo_Name_col, cDataReader["HiCo_Name"].ToString(), Int16.Parse(cDataReader["HiCo_NodeLevel"].ToString()), true);

                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Action_col] = cDataReader["ActionCol"];
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_NRI_col] = cDataReader["HiCo_NRI"];
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] = cDataReader["IsSystem"];
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col] = cDataReader["HiCo_NodeLevel"];
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsNode_col] = cDataReader["IsNode"];
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_NRI_col] = cDataReader["FoT_NRI"];
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col] = cDataReader["FoT_Code"];

                        switch ((int)cboTypes.SelectedValue)
                        {
                            case (int)ctr_Template.TemplateType.VERSION:
                                if (mcGrdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] == "1" || 
                                    (int)grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Version_Number
                                   )
                                {
                                    intLastUnmodifiableRow = grdTemplate.Rows.Count - 1;
                                }

                                if (grdTemplate.Rows.Count > 2 && (int)grdTemplate[grdTemplate.Rows.Count - 2, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Ceritar_Application) intFirstNameOnlyModifiableRow = grdTemplate.Rows.Count - 1;

                                if (grdTemplate.Rows.Count > 2 && (int)grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Version_Number) intLastNameOnlyModifiableRow = grdTemplate.Rows.Count - 2;

                                break;

                            case (int)ctr_Template.TemplateType.REVISION:

                                if (mcGrdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] == "1" ||
                                    (int)grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Revision_Number
                                   )
                                {
                                    intLastUnmodifiableRow = grdTemplate.Rows.Count - 1;
                                }

                                break;
                        }
                        
                    }

                    cboFolderType.Visible = true;

                    CellRange crFixedRows = grdTemplate.GetCellRange(1, mintGrdTpl_HiCo_Name_col, intLastUnmodifiableRow, mintGrdTpl_HiCo_FolderType_col);
                    crFixedRows.Style = csSystemItem;
            
                    if (intLastUnmodifiableRow + 1 < grdTemplate.Rows.Count)
                    {
                        CellRange cr1 = grdTemplate.GetCellRange(intLastUnmodifiableRow + 1, mintGrdTpl_HiCo_Name_col, grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col);
                        cr1.Style = grdTemplate.Styles.Normal;

                        if (intFirstNameOnlyModifiableRow > 0 && intFirstNameOnlyModifiableRow <= intLastNameOnlyModifiableRow)
                        {
                            CellRange cr2 = grdTemplate.GetCellRange(intFirstNameOnlyModifiableRow, mintGrdTpl_HiCo_Name_col, intLastNameOnlyModifiableRow, mintGrdTpl_HiCo_Name_col);
                            cr2.Style = grdTemplate.Styles.Normal;
                        }
                    }

                    //Il est très important d'appliquer l'editor pour chacune des cells, car sinon le style se propage aux autres cellules.
                    for (int intRowIdx = intLastUnmodifiableRow + 1; intRowIdx < grdTemplate.Rows.Count; intRowIdx++)
                    {
                        CellRange cr = grdTemplate.GetCellRange(intRowIdx, mintGrdTpl_HiCo_FolderType_col, intRowIdx, mintGrdTpl_HiCo_FolderType_col);
                        cr.Style = grdTemplate.Styles.Add(null); //Imperatif de creer un nouveau style, sinon le courant sera modifié et appliqué aux autres cellules
                        cr.Style.Editor = cboFolderType;
                    }

                    grdTemplate.Row = grdTemplate.Rows.Count - 1;

                    blnValidReturn = true;
                }
                else
                {
                    blnValidReturn = true; //Should never happen, but to let delete
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

            mcGrdTemplate.GridIsLoading = false;

            return blnValidReturn;
        }

        private bool pfblnData_Load()
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;

            try
            {
                sqlRecord = clsTTSQL.ADOSelect(mcCtrTemplate.strGetDataLoad_SQL(formController.Item_NRI));

                if (sqlRecord.Read())
                {
                    mintTpl_TS = UInt16.Parse(sqlRecord["Tpl_TS"].ToString());
                    txtName.Text = sqlRecord["Tpl_Name"].ToString();
                    chkByDefault.Checked = Convert.ToBoolean(sqlRecord["Tpl_ByDefault"]);
                    cboTypes.SelectedValue = Int32.Parse(sqlRecord["TeT_NRI"].ToString());
                    cboApplications.SelectedValue = Int32.Parse(sqlRecord["CeA_NRI"].ToString());

                    if (sqlRecord["Tpl_NRI_Ref"] != DBNull.Value)
                    {
                        sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrTemplate.strGetTemplateRef_SQL((int)cboApplications.SelectedValue), "Tpl_NRI", "Tpl_Name", true, ref cboTemplate);

                        cboTemplate.SelectedValue = Int32.Parse(sqlRecord["Tpl_NRI_Ref"].ToString());
                        cboTemplate.Visible = true;
                    }
                        
                    blnValidReturn = true;
                }
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

        private bool pfblnCanEditRow()
        {
            bool blnCanEditRow = false;

            if (!formController.FormIsLoading &&
                !mcGrdTemplate.GridIsLoading &&
                grdTemplate.Rows.Count > 1 &&
                grdTemplate.Row > 0 &&
                (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE) &&
                (mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_IsSystemItem_col] == "0" | String.IsNullOrEmpty(mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_IsSystemItem_col]))
               )
            {
                switch ((int)cboTypes.SelectedValue)
                {
                    case (int)ctr_Template.TemplateType.VERSION:
                    
                        if (mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == ((int)ctr_Template.FolderType.Normal).ToString() && 
                            grdTemplate.Col == mintGrdTpl_HiCo_FolderType_col &&
                            pfblnRowsBetweenCerAppAndVersionNo(grdTemplate.Row))
                        {
                            blnCanEditRow = false;
                        }
                        else if (mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != ((int)ctr_Template.FolderType.Ceritar_Application).ToString() &&
                            mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != ((int)ctr_Template.FolderType.Version_Number).ToString()
                           )
                        {
                            blnCanEditRow = true;
                        }

                        break;

                    case (int)ctr_Template.TemplateType.REVISION:

                        if (mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != ((int)ctr_Template.FolderType.Ceritar_Application).ToString() &&
                            mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != ((int)ctr_Template.FolderType.Version_Number).ToString() &&
                            mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != ((int)ctr_Template.FolderType.Revision_Number).ToString() &&
                            !pfblnRowsBetweenCerAppAndVersionNo(grdTemplate.Row)
                           )
                        {
                            blnCanEditRow = true;
                        }

                        break;
                }
            }

            return blnCanEditRow;
        }

        private bool pfblnRowsBetweenCerAppAndVersionNo(int vintRowIndex)
        {
            bool blnRowIsBetweenVersionNo = false;
            bool blnRowIsBetweenCerApp = false;

            try
            {
               for (int intRowIndex = vintRowIndex + 1; intRowIndex < grdTemplate.Rows.Count; intRowIndex ++)
               {
                   if ((int)grdTemplate[intRowIndex, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Version_Number)
                   {
                       blnRowIsBetweenVersionNo = true;

                       break;
                   }
               }

               for (int intRowIndex = vintRowIndex - 1; blnRowIsBetweenVersionNo && intRowIndex > 0; intRowIndex--)
               {
                   if ((int)grdTemplate[intRowIndex, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Ceritar_Application)
                   {
                       blnRowIsBetweenCerApp = true;

                       break;
                   }
               }
            }
            catch (Exception ex)
            {
                blnRowIsBetweenVersionNo = true;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnRowIsBetweenVersionNo & blnRowIsBetweenCerApp;
        }

        private bool pfblnAddNodeRow(NodeTypeEnum nodeType)
        {
            bool blnValidReturn = false;
            int intNewRowIndex = 0;

            Node currentNode = grdTemplate.Rows[grdTemplate.Row].Node;
            currentNode.AddNode(nodeType, "");

            intNewRowIndex = grdTemplate.FindRow("", 1, mintGrdTpl_HiCo_Action_col, false, true, false);

            mcGrdTemplate.bln_SetRowActionToInsert(intNewRowIndex);

            grdTemplate[intNewRowIndex, mintGrdTpl_HiCo_NRI_col] = 0;
            grdTemplate[intNewRowIndex, mintGrdTpl_HiCo_IsSystemItem_col] = "0";
            grdTemplate[intNewRowIndex, mintGrdTpl_HiCo_IsNode_col] = "0";//(blnCurrentRowIsNode ? "1" : "0");
            grdTemplate[intNewRowIndex, mintGrdTpl_HiCo_FolderType_col] = ctr_Template.FolderType.Normal;
            grdTemplate[intNewRowIndex, mintGrdTpl_HiCo_FolderType_NRI_col] = (int)ctr_Template.FolderType.Normal;

            switch (nodeType)
            {
                case NodeTypeEnum.FirstChild:

                    grdTemplate[intNewRowIndex, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Row].Node.Level + 1;

                    break;

                case NodeTypeEnum.NextSibling:

                    grdTemplate[intNewRowIndex, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Row].Node.Level;

                    break;
            }

            if (pfblnRowsBetweenCerAppAndVersionNo(intNewRowIndex))
            {
                for (int intRowIndex = intNewRowIndex + 1; intRowIndex < grdTemplate.Rows.Count; intRowIndex++)
                {
                    grdTemplate.Rows[intRowIndex].Node.Level++;
                    grdTemplate[intRowIndex, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[intRowIndex].Node.Level;

                    mcGrdTemplate.bln_SetRowActionToUpdate(intRowIndex);

                    if (pfblnRowsBetweenCerAppAndVersionNo(intRowIndex - 1))
                    {
                        CellRange crUnmodifiableRow = grdTemplate.GetCellRange(intRowIndex - 1, mintGrdTpl_HiCo_FolderType_col, intRowIndex - 1, mintGrdTpl_HiCo_FolderType_col);
                        crUnmodifiableRow.Style = csSystemItem;
                    }
                }
            }
            else
            {
                CellRange cr = grdTemplate.GetCellRange(intNewRowIndex, mintGrdTpl_HiCo_FolderType_col, grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col);
                cr.StyleNew.Editor = cboFolderType;
            }
            
            grdTemplate.Row = intNewRowIndex;

            formController.ChangeMade = true;

            return blnValidReturn;
        }

        private bool pfblnSetButtonsActivation()
        {
            bool blnValidReturn = true;

            btnAddChild.Enabled = false;
            AddSibbling.Enabled = false;
            btnMoveLeft.Enabled = false;
            btnMoveRight.Enabled = false;
            btnDeleteRow.Enabled = false;

            switch ((int)cboTypes.SelectedValue)
            {
                case (int)ctr_Template.TemplateType.VERSION:

                    if (!formController.FormIsLoading &&
                        grdTemplate.Rows.Count > 1 &&
                        grdTemplate.Row > 0 &&
                        !String.IsNullOrEmpty(mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col]) &&
                        mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_IsSystemItem_col] == "0"
                       )
                    {
                        if (pfblnRowsBetweenCerAppAndVersionNo(grdTemplate.Row))
                        {
                            btnAddChild.Enabled = true;

                            btnDeleteRow.Enabled = mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_Action_col] == sclsConstants.DML_Mode.INSERT_MODE.ToString(); //TODO
                        }
                        else if ((int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Normal)
                        {
                            btnAddChild.Enabled = true;
                            AddSibbling.Enabled = true;
                            btnMoveLeft.Enabled = true;
                            btnMoveRight.Enabled = true;
                            btnDeleteRow.Enabled = true;
                        }
                        else if ((int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Version_Number ||
                                 (int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Ceritar_Application
                                )
                        {
                            btnAddChild.Enabled = true;
                        }
                        else if ((int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.External_Report ||
                                 (int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Scripts ||
                                 (int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Release ||
                                 (int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.CaptionsAndMenus
                                )
                        {
                            AddSibbling.Enabled = true;
                            btnMoveLeft.Enabled = true;
                            btnMoveRight.Enabled = true;
                            btnDeleteRow.Enabled = true;
                        }
                    }

                    break;

                case (int)ctr_Template.TemplateType.REVISION:

                    if (!formController.FormIsLoading &&
                        grdTemplate.Rows.Count > 1 &&
                        grdTemplate.Row > 0 &&
                        grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != null &&
                        (int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != (int)ctr_Template.FolderType.System &&
                        (int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != (int)ctr_Template.FolderType.Normal
                       )
                    {
                        AddSibbling.Enabled = false;
                    }

                    if (!formController.FormIsLoading &&
                        grdTemplate.Rows.Count > 1 &&
                        grdTemplate.Row > 0 &&
                        grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != null &&
                        (int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != (int)ctr_Template.FolderType.System &&
                        ((int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Ceritar_Application |
                        (int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Version_Number)
                       )
                    {
                        btnAddChild.Enabled = false;
                        AddSibbling.Enabled = false;
                        btnMoveLeft.Enabled = false;
                        btnMoveRight.Enabled = false;
                        btnDeleteRow.Enabled = false;
                    }
                    else if (!formController.FormIsLoading &&
                             grdTemplate.Rows.Count > 1 &&
                             grdTemplate.Row > 0 &&
                             grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != null &&
                             (int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] != (int)ctr_Template.FolderType.System &&
                             (int)grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] == (int)ctr_Template.FolderType.Revision_Number
                           )
                    {
                        btnAddChild.Enabled = true;
                    }
                    else if (grdTemplate.Rows.Count > 1 && !pfblnRowsBetweenCerAppAndVersionNo(grdTemplate.Row))
                    {
                        btnAddChild.Enabled = true;
                        AddSibbling.Enabled = true;
                        btnMoveLeft.Enabled = true;
                        btnMoveRight.Enabled = true;
                        btnDeleteRow.Enabled = true;
                    }

                    break;
            }

            return blnValidReturn;
        }

#endregion


        private void btnAddChild_Click(object sender, EventArgs e)
        {
            pfblnAddNodeRow(NodeTypeEnum.NextSibling); 
        }

        void mcGrdTemplate_SetGridDisplay()
        {
            grdTemplate.Tree.Column = mintGrdTpl_HiCo_Name_col;

            grdTemplate.Cols[mintGrdTpl_HiCo_Name_col].Width = 481;

            grdTemplate.Cols[mintGrdTpl_HiCo_Name_col].StyleDisplay.TextAlign = TextAlignEnum.LeftCenter;
        }

        private void formController_LoadData(TT3LightDLL.Controls.LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false; 
            Button placeHolder = null;

            formController.FormIsLoading = true;

            if (!mcGrdTemplate.bln_Init(ref grdTemplate, ref placeHolder, ref btnDeleteRow, true))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrTemplate.strGetApplications_SQL(), "CeA_NRI", "CeA_Name", true, ref cboApplications))
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrTemplate.strGetTemplateTypes_SQL(), "TeT_NRI", "TeT_Code", false, ref cboTypes))
            { }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE)
            {
                blnValidReturn = true;
            }
            else if (!pfblnData_Load()) 
            { }
            else if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrTemplate.strGetFolderTypes_SQL(), "FoT_NRI", "FoT_Code", false, ref cboFolderType))
            { }
            else if (!pfblnGrdTemplate_Load())
            { }
            else
            {
                blnValidReturn = true;
            }
                      
            formController.FormIsLoading = false;

            if (!blnValidReturn) this.Close();
        }

        private void formController_SetReadRights()
        {
            cboFolderType.Visible = false;
            cboTemplate.Visible = false;
            lblTemplate.Visible = false;

            switch (formController.FormMode)
            {
                case sclsConstants.DML_Mode.INSERT_MODE:

                    txtName.Enabled = true;

                    cboApplications.Enabled = true;
                    cboTypes.Enabled = true;

                    btnAddChild.Enabled = false;
                    AddSibbling.Enabled = false;
                    btnMoveLeft.Enabled = false;
                    btnMoveRight.Enabled = false;
                    btnDeleteRow.Enabled = false;

                    break;

                case sclsConstants.DML_Mode.UPDATE_MODE:

                    //txtName.Enabled = false;

                    cboApplications.Enabled = false;
                    cboTypes.Enabled = false;
                    cboTemplate.Enabled = false;
                    cboTemplate.Visible = (int)cboTypes.SelectedValue == (int)ctr_Template.TemplateType.REVISION;
                    lblTemplate.Visible = (int)cboTypes.SelectedValue == (int)ctr_Template.TemplateType.REVISION;

                    btnAddChild.Enabled = true;
                    AddSibbling.Enabled = true;
                    btnMoveLeft.Enabled = true;
                    btnMoveRight.Enabled = true;
                    btnDeleteRow.Enabled = true;

                    break;
            }
        }

        private void cboApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                grdTemplate.Rows.Count = 1;

                if (cboApplications.SelectedIndex > 0)
                {
                    switch ((int)cboTypes.SelectedValue)
                    {
                        case (int)ctr_Template.TemplateType.VERSION:

                            //Va chercher les lignes systèmes
                            pfblnGrdTemplate_Load();

                            //On insere une ligne non modifiable qui represente le dossier racine de l'application
                            mcGrdTemplate.AddTreeItem(mintGrdTpl_HiCo_Name_col, cboApplications.GetItemText(cboApplications.SelectedItem), Int32.Parse(grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col].ToString()) + 1, true);

                            mcGrdTemplate.bln_SetRowActionToInsert(grdTemplate.Rows.Count - 1);

                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_NRI_col] = 0;
                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] = "0";
                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Rows.Count - 1].Node.Level;
                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsNode_col] = "1";
                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col] = ctr_Template.FolderType.Ceritar_Application;
                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_NRI_col] = (int)ctr_Template.FolderType.Ceritar_Application;

                            //On insere ensuite une ligne non modifiable qui represente la version
                            mcGrdTemplate.AddTreeItem(mintGrdTpl_HiCo_Name_col, Ceritar.CVS.sclsAppConfigs.GetVersionNumberPrefix + "XXX", Int32.Parse(grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col].ToString()) + 1, true);

                            mcGrdTemplate.bln_SetRowActionToInsert(grdTemplate.Rows.Count - 1);

                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_NRI_col] = 0;
                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] = "0";
                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Rows.Count - 1].Node.Level;
                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsNode_col] = "1";
                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col] = ctr_Template.FolderType.Version_Number;
                            grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_NRI_col] = (int)ctr_Template.FolderType.Version_Number;


                            grdTemplate.Row = grdTemplate.Rows.Count - 1;

                            cboFolderType.Visible = true;

                            CellRange crVersionApp = grdTemplate.GetCellRange(1, mintGrdTpl_HiCo_FolderType_col, grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col);
                            crVersionApp.StyleNew.Editor = cboFolderType;

                            CellRange crRow = grdTemplate.GetCellRange(grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Name_col, grdTemplate.Rows.Count - 2, mintGrdTpl_HiCo_FolderType_col);
                            crRow.Style = csSystemItem;

                            formController.ChangeMade = true;

                            grdTemplate.Row = grdTemplate.Rows.Count - 1;

                            formController.FormIsLoading = true;
                    sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrTemplate.strGetFolderTypes_SQL(), "FoT_NRI", "FoT_Code", false, ref cboFolderType);
                    formController.FormIsLoading = false;

                            break;

                        case (int)ctr_Template.TemplateType.REVISION:

                            if (cboApplications.SelectedIndex > 0)
                            {
                                cboTemplate.Enabled = true;

                                sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrTemplate.strGetTemplateRef_SQL((int)cboApplications.SelectedValue), "Tpl_NRI", "Tpl_Name", true, ref cboTemplate);
                            }
                            else
                            {
                                cboTemplate.Enabled = false;
                            }
                            
                            break;
                    }
                }
                else
                {
                    cboTemplate.DataSource = null;
                    cboTemplate.Items.Clear();
                    formController.ChangeMade = false;
                }
            }         
        }

        private void formController_SaveData(TT3LightDLL.Controls.SaveDataEventArgs eventArgs)
        {
            if (formController.FormMode == sclsConstants.DML_Mode.DELETE_MODE)
            {
                mcActionResults = mcCtrTemplate.blnDeleteTemplate();
            }
            else if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
            {
                mcActionResults = mcCtrTemplate.Save();
            }

            if (!mcActionResults.IsValid)
            {
                clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI);
            }

            if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE) formController.Item_NRI = mcActionResults.GetNewItem_NRI;

            eventArgs.SaveSuccessful = mcActionResults.IsValid;
        }

        private void formController_ValidateForm(TT3LightDLL.Controls.ValidateFormEventArgs eventArgs)
        {
            mcActionResults = mcCtrTemplate.Validate();

            if (!mcActionResults.IsValid)
            {
                clsTTApp.GetAppController.ShowMessage(mcActionResults.GetErrorMessage_NRI);

                switch ((ctr_Template.ErrorCode_Tpl)mcActionResults.GetErrorCode)
                {
                    case ctr_Template.ErrorCode_Tpl.HIERARCHY_MANDATORY:

                        AddSibbling.Focus();
                        break;

                    case ctr_Template.ErrorCode_Tpl.NAME_MANDATORY:
                    case ctr_Template.ErrorCode_Tpl.TEMPLATE_NAME_UNIQUE:

                        txtName.Focus();
                        txtName.SelectAll();
                        break;

                    case ctr_Template.ErrorCode_Tpl.TEMPLATE_TYPE_MANDATORY:

                        cboTypes.Focus();
                        cboTypes.DroppedDown = true;
                        break;

                    case ctr_Template.ErrorCode_Tpl.CERITAR_APPLICATION_MANDATORY:

                        cboApplications.Focus();
                        cboApplications.DroppedDown = true;
                        break;

                    default:

                        switch ((ctr_Template.ErrorCode_HiCo)mcActionResults.GetErrorCode)
                        {
                            case ctr_Template.ErrorCode_HiCo.NAME_ON_DISK_MANDATORY:

                                grdTemplate.Select(grdTemplate.FindRow(string.Empty, 1, mintGrdTpl_HiCo_Name_col, false, true, false), mintGrdTpl_HiCo_Name_col, true);
                                break;

                            case ctr_Template.ErrorCode_HiCo.NAME_ON_DISK_INVALID:
                            case ctr_Template.ErrorCode_HiCo.NAME_ON_DISK_UNIQUE:

                                grdTemplate.Row = mcActionResults.RowInError;
                                break;
                        }
                        break;
                }   
            }
            else
            {
                //Do nothing
            }

            eventArgs.IsValid = mcActionResults.IsValid;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void cboTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
                grdTemplate.Rows.Count = 1;
                cboApplications.SelectedIndex = -1;

                switch ((int)cboTypes.SelectedValue)
                {
                    case (int)ctr_Template.TemplateType.VERSION:
                        cboTemplate.Visible = false;
                        lblTemplate.Visible = false;

                        break;

                    case (int)ctr_Template.TemplateType.REVISION:
                        cboTemplate.Visible = true;
                        lblTemplate.Visible = true;
                        cboTemplate.Enabled = false;

                        break;
                }
            }
        }

        private void grdTemplate_DoubleClick(object sender, EventArgs e)
        {
            if (pfblnCanEditRow())
            {
                grdTemplate.StartEditing();
            }
        }

        private void cboFolderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading && grdTemplate.Rows.Count > 1 && grdTemplate.Row > 0 && mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_IsSystemItem_col] != "1")
            {
                ComboBox cboCell = (ComboBox)grdTemplate.GetCellRange(grdTemplate.Row, mintGrdTpl_HiCo_FolderType_col).Style.Editor;
                grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_NRI_col] = cboCell.SelectedValue;
                grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_FolderType_col] = cboCell.GetItemText(cboCell.SelectedItem);

                if (cboFolderType.SelectedValue != null)
                {
                    switch ((int)cboFolderType.SelectedValue)
                    {
                        case (int)ctr_Template.FolderType.Version_Number:

                            grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_Name_col] = Ceritar.CVS.sclsAppConfigs.GetVersionNumberPrefix + "XXX";
                            break;

                        case (int)ctr_Template.FolderType.Revision_Number:

                            grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_Name_col] = Ceritar.CVS.sclsAppConfigs.GetRevisionNumberPrefix + "XX";
                            break;
                    }
                }
            }
        }

        private void grdTemplate_AfterRowColChange(object sender, RangeEventArgs e)
        {
            pfblnSetButtonsActivation();
        }

        private void chkByDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (!formController.FormIsLoading)
            {
                formController.ChangeMade = true;
            }
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            int intFolderType_Root = (int)ctr_Template.TemplateType.VERSION;

            switch ((int)cboTypes.SelectedValue)
            {
                case (int)ctr_Template.TemplateType.VERSION:

                    intFolderType_Root = (int)ctr_Template.FolderType.Version_Number;

                    break;

                case (int)ctr_Template.TemplateType.REVISION:

                    intFolderType_Root = (int)ctr_Template.FolderType.Revision_Number;

                    break;
            }

            int intRootRow = grdTemplate.FindRow(intFolderType_Root.ToString(), 1, mintGrdTpl_HiCo_FolderType_NRI_col, false, true, false);

            if (pfblnCanEditRow() && Int32.Parse(mcGrdTemplate[intRootRow, mintGrdTpl_HiCo_Level_col]) + 1 < Int32.Parse(mcGrdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_Level_col]))
            {
                grdTemplate.Rows[grdTemplate.Row].Node.Level--;
                grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Row].Node.Level;
            }
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if (pfblnCanEditRow())
            {
                grdTemplate.Rows[grdTemplate.Row].Node.Level++;
                grdTemplate[grdTemplate.Row, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Row].Node.Level;
            }
        }

        private void cmdAddSibbling_Click(object sender, EventArgs e)
        {
            pfblnAddNodeRow(NodeTypeEnum.FirstChild);
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            int intRowIndex = grdTemplate.Row + 1;

            if (pfblnRowsBetweenCerAppAndVersionNo(grdTemplate.Row))
            {
                for (int intRowIndex2 = grdTemplate.Row + 1; intRowIndex2 < grdTemplate.Rows.Count; intRowIndex2 ++)
                {
                    grdTemplate[intRowIndex2, mintGrdTpl_HiCo_Level_col] = Int32.Parse(mcGrdTemplate[intRowIndex2, mintGrdTpl_HiCo_Level_col]) - 1;

                    mcGrdTemplate.bln_SetRowActionToUpdate(intRowIndex2);
                    grdTemplate.Rows[intRowIndex2].Node.Level--;
                }
            }
            else
            {
                while (true)
                {
                    if (intRowIndex < grdTemplate.Rows.Count && grdTemplate.Rows[grdTemplate.Row].Node.Level < grdTemplate.Rows[intRowIndex].Node.Level)
                    {
                        mcGrdTemplate.bln_SetRowActionToDelete(intRowIndex);
                    }
                    else
                    {
                        break;
                    }
                    intRowIndex++;
                }
            }
        }

        private void cboTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader sqlRecord = null;
            int intCurrentNodeLevel = 0;

            grdTemplate.Rows.Count = 1;

            if (!formController.FormIsLoading && cboTemplate.SelectedIndex > 0)
            {
                //On insere une ligne non modifiable qui represente le dossier racine de l'application
                mcGrdTemplate.AddTreeItem(mintGrdTpl_HiCo_Name_col, cboApplications.GetItemText(cboApplications.SelectedItem), intCurrentNodeLevel, true);

                mcGrdTemplate.bln_SetRowActionToInsert(grdTemplate.Rows.Count - 1);

                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_NRI_col] = 0;
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] = "0";
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Rows.Count - 1].Node.Level;
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsNode_col] = "1";
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col] = ctr_Template.FolderType.Ceritar_Application;
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_NRI_col] = (int)ctr_Template.FolderType.Ceritar_Application;

                //On insere des lignes non modifiables qui represente les dossiers present dans la version
                sqlRecord = clsTTSQL.ADOSelect(mcCtrTemplate.strGetTemplateReference_Hierarchy((int)cboTemplate.SelectedValue));

                while (sqlRecord.Read())
                {
                    if (sqlRecord["FoT_NRI"].ToString() == ((int)ctr_Template.FolderType.Version_Number).ToString()) break;

                    if (sqlRecord["FoT_NRI"].ToString() != ((int)ctr_Template.FolderType.Ceritar_Application).ToString())
                    {
                        mcGrdTemplate.AddTreeItem(mintGrdTpl_HiCo_Name_col, sqlRecord["HiCo_Name"].ToString(), ++intCurrentNodeLevel, true);

                        mcGrdTemplate.bln_SetRowActionToInsert(grdTemplate.Rows.Count - 1);

                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_NRI_col] = 0;
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] = "0";
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Rows.Count - 1].Node.Level;
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsNode_col] = "1";
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col] = ctr_Template.FolderType.Normal;
                        grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_NRI_col] = (int)ctr_Template.FolderType.Normal;
                    }
                }

                //On insere ensuite une ligne non modifiable qui represente la version
                mcGrdTemplate.AddTreeItem(mintGrdTpl_HiCo_Name_col, Ceritar.CVS.sclsAppConfigs.GetVersionNumberPrefix + "XXX", ++intCurrentNodeLevel, true);

                mcGrdTemplate.bln_SetRowActionToInsert(grdTemplate.Rows.Count - 1);

                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_NRI_col] = 0;
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] = "0";
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Rows.Count - 1].Node.Level;
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsNode_col] = "1";
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col] = ctr_Template.FolderType.Version_Number;
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_NRI_col] = (int)ctr_Template.FolderType.Version_Number;

                //On insere finalement une ligne non modifiable qui represente la révision
                mcGrdTemplate.AddTreeItem(mintGrdTpl_HiCo_Name_col, Ceritar.CVS.sclsAppConfigs.GetRevisionNumberPrefix + "XX", ++intCurrentNodeLevel, true);

                mcGrdTemplate.bln_SetRowActionToInsert(grdTemplate.Rows.Count - 1);

                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_NRI_col] = 0;
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsSystemItem_col] = "0";
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_Level_col] = grdTemplate.Rows[grdTemplate.Rows.Count - 1].Node.Level;
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_IsNode_col] = "1";
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col] = ctr_Template.FolderType.Revision_Number;
                grdTemplate[grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_NRI_col] = (int)ctr_Template.FolderType.Revision_Number;


                grdTemplate.Row = grdTemplate.Rows.Count - 1;

                cboFolderType.Visible = true;

                CellRange crRevision = grdTemplate.GetCellRange(1, mintGrdTpl_HiCo_FolderType_col, grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col);
                crRevision.StyleNew.Editor = cboFolderType;

                CellRange crLastRow = grdTemplate.GetCellRange(1, mintGrdTpl_HiCo_Name_col, grdTemplate.Rows.Count - 1, mintGrdTpl_HiCo_FolderType_col);
                crLastRow.Style = csSystemItem;

                formController.ChangeMade = true;

                grdTemplate.Row = 1;


                formController.FormIsLoading = true;
                sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrTemplate.strGetFolderTypes_SQL(), "FoT_NRI", "FoT_Code", false, ref cboFolderType);
                formController.FormIsLoading = false;
            }
        }

    }
}
