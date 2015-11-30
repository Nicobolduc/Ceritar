using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Template;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Controllers
{
    public class ctr_Template
    {
        private Interfaces.ITemplate mcView;
        private mod_Tpl_HierarchyTemplate mcModTemplate;
        private clsActionResults mcActionResult;

        public enum ErrorCode_Tpl
        {
            NAME_MANDATORY = 1,
            TEMPLATE_TYPE_MANDATORY = 2,
            HIERARCHY_MANDATORY = 3
        }

        public enum ErrorCode_HiCo
        {
            NAME_ON_DISK_MANDATORY = 1,
            FOLDER_TYPE_MANDATORY = 2
        }


        public ctr_Template(Interfaces.ITemplate rView)
        {
            mcModTemplate = new mod_Tpl_HierarchyTemplate();
            mcView = rView;
        }

        public clsActionResults Validate()
        {
            try
            {
                mcModTemplate = new mod_Tpl_HierarchyTemplate();
                mcModTemplate.DML_Action = mcView.GetDML_Action();
                mcModTemplate.TemplateName = mcView.GetTemplateName();
                mcModTemplate.Template_NRI = mcView.GetTemplate_NRI();
                mcModTemplate.Template_TS = mcView.GetTemplate_TS();
                mcModTemplate.IsByDefault = mcView.GetByDefaultValue();
                mcModTemplate.TemplatType = mcView.GetTemplateType_NRI();

                mcActionResult = mcModTemplate.Validate();
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResult;
        }

        public clsActionResults Save()
        {
            try
            {
                if (mcModTemplate.Save())
                {

                }
                mcActionResult = mcModTemplate.ActionResults;
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResult;
        }
        
        public string strGetDataLoad_SQL(int vintTpl_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Template.Tpl_TS, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_Name, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_ByDefault, " + Environment.NewLine;
            strSQL = strSQL + "        Template.TeT_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Template.CeA_NRI " + Environment.NewLine;

            strSQL = strSQL + " FROM Template " + Environment.NewLine;

            strSQL = strSQL + " WHERE Template.Tpl_NRI = " + vintTpl_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_HierarchyComponents_SQL(int vintTemplate_NRI = 0)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT  ActionCol = " + (vintTemplate_NRI == 0 ? (int)sclsConstants.DML_Mode.INSERT_MODE: (int)sclsConstants.DML_Mode.NO_MODE) + "," + Environment.NewLine;
            strSQL = strSQL + "   		HierarchyComp.HiCo_NRI,  " + Environment.NewLine;
            strSQL = strSQL + "   		IsSystem = 1,  " + Environment.NewLine;
            strSQL = strSQL + "   		HierarchyComp.HiCo_NodeLevel AS HiCo_NodeLevel,  " + Environment.NewLine;
            strSQL = strSQL + "   		IsRoot = CASE WHEN HierarchyComp.HiCo_Parent_NRI = TNode.Parent OR HierarchyComp.HiCo_Parent_NRI IS NULL THEN 1 ELSE 0 END, " + Environment.NewLine;
            strSQL = strSQL + "   		HierarchyComp.HiCo_Name  " + Environment.NewLine;       

            strSQL = strSQL + " FROM AppConfig  " + Environment.NewLine;
            strSQL = strSQL + " 	INNER JOIN HierarchyComp ON HierarchyComp.ACg_NRI = AppConfig.ACg_NRI " + Environment.NewLine;

            strSQL = strSQL + " 	LEFT JOIN ( SELECT MIN(HierarchyComp.HiCo_NRI) AS Enfant, MAX(Parent.HiCo_NRI) AS Parent  " + Environment.NewLine;
            strSQL = strSQL + "   				FROM HierarchyComp   " + Environment.NewLine;
            strSQL = strSQL + "   					INNER JOIN HierarchyComp Parent ON Parent.HiCo_NRI = HierarchyComp.HiCo_Parent_NRI   " + Environment.NewLine;
            strSQL = strSQL + "   				WHERE HierarchyComp.HiCo_NodeLevel - 1 = Parent.HiCo_NodeLevel  " + Environment.NewLine;
            strSQL = strSQL + "   				GROUP BY HierarchyComp.HiCo_NodeLevel  " + Environment.NewLine;
            strSQL = strSQL + "   				) AS TNode ON TNode.Enfant = HierarchyComp.HiCo_NRI  " + Environment.NewLine;

            strSQL = strSQL + " WHERE AppConfig.ACg_Racine_Name = 'InstallationsActives' " + Environment.NewLine;

            if (vintTemplate_NRI > 0)
            {
                strSQL = strSQL + " UNION ALL " + Environment.NewLine;

                strSQL = strSQL + " SELECT  ActionCol = 0,  " + Environment.NewLine;
                strSQL = strSQL + "   		HierarchyComp.HiCo_NRI,  " + Environment.NewLine;
                strSQL = strSQL + "   		IsSystem = 0,  " + Environment.NewLine;
                strSQL = strSQL + "   		HierarchyComp.HiCo_NodeLevel AS HiCo_NodeLevel,  " + Environment.NewLine;
                strSQL = strSQL + "   		IsRoot = CASE WHEN HierarchyComp.HiCo_Parent_NRI = TNode.Parent OR HierarchyComp.HiCo_Parent_NRI IS NULL THEN 1 ELSE 0 END, " + Environment.NewLine;
                strSQL = strSQL + "   		HierarchyComp.HiCo_Name  " + Environment.NewLine;            

                strSQL = strSQL + " FROM Template  " + Environment.NewLine;
                strSQL = strSQL + " 	LEFT JOIN HierarchyComp ON HierarchyComp.Tpl_NRI = Template.Tpl_NRI " + Environment.NewLine;

                strSQL = strSQL + " 	LEFT JOIN ( SELECT MIN(HierarchyComp.HiCo_NRI) AS Enfant, MAX(Parent.HiCo_NRI) AS Parent  " + Environment.NewLine;
                strSQL = strSQL + "   				FROM HierarchyComp   " + Environment.NewLine;
                strSQL = strSQL + "   					INNER JOIN HierarchyComp Parent ON Parent.HiCo_NRI = HierarchyComp.HiCo_Parent_NRI   " + Environment.NewLine;
                strSQL = strSQL + "   				WHERE HierarchyComp.HiCo_NodeLevel - 1 = Parent.HiCo_NodeLevel  " + Environment.NewLine;
                strSQL = strSQL + "   				GROUP BY HierarchyComp.HiCo_NodeLevel  " + Environment.NewLine;
                strSQL = strSQL + "   				) AS TNode ON TNode.Enfant = HierarchyComp.HiCo_NRI  " + Environment.NewLine;

                strSQL = strSQL + " WHERE Template.Tpl_NRI = " + vintTemplate_NRI + Environment.NewLine;
            }
            
            strSQL = strSQL + " ORDER BY HierarchyComp.HiCo_NodeLevel  " + Environment.NewLine;


            return strSQL;
        }

        public string strGetListe_Applications_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerApp.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CerApp " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerApp.CeA_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_TemplateTypes_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT TemplateType.TeT_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        TemplateType.TeT_Code " + Environment.NewLine;

            strSQL = strSQL + " FROM TemplateType " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY TemplateType.TeT_NRI " + Environment.NewLine;

            return strSQL;
        }
    }
}
