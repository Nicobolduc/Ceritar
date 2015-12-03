using System;
using System.Collections.Generic;
using Ceritar.CVS.Controllers.Interfaces;
using Ceritar.CVS.Models.Module_Template;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;

namespace Ceritar.CVS.Controllers
{
    public class ctr_Template
    {
        private Interfaces.ITemplate mcView;
        private mod_Tpl_HierarchyTemplate mcModTemplate;
        private mod_Folder mcModFolder_Root;
        private clsActionResults mcActionResult;
        private clsSQL mcSQL;

        public enum FolderType
        {
            Normal = 1,
            Ceritar_Application = 2,
            Executable = 3,
            TTApp = 4,
            Script = 5,
            Other = 6
        }

        public enum ErrorCode_Tpl
        {
            NAME_MANDATORY = 1,
            TEMPLATE_TYPE_MANDATORY = 2,
            HIERARCHY_MANDATORY = 3,
            CERITAR_APPLICATION_MANDATORY = 4
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
            mod_Folder cFolder;
            structHierarchyComponent structRacine;
            List<structHierarchyComponent> lstHiCo;
            mod_Folder cParentFolder = null;

            try
            {
                mcModTemplate = new mod_Tpl_HierarchyTemplate();
                mcModTemplate.DML_Action = mcView.GetDML_Action();
                mcModTemplate.TemplateName = mcView.GetTemplateName();
                mcModTemplate.Template_NRI = mcView.GetTemplate_NRI();
                mcModTemplate.Template_TS = mcView.GetTemplate_TS();
                mcModTemplate.IsByDefault = mcView.GetByDefaultValue();
                mcModTemplate.TemplatType = (mod_Tpl_HierarchyTemplate.TemplateType)mcView.GetTemplateType_NRI();
                mcModTemplate.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();

                structRacine = mcView.GetRacineSystem();
                mcModTemplate.RacineSystem = new mod_Folder();
                mcModTemplate.RacineSystem.DML_Action = structRacine.Action;
                mcModTemplate.RacineSystem.HierarchyComponent_NRI = structRacine.intHierarchyComponent_NRI;
                mcModTemplate.RacineSystem.HierarchyComponent_TS = structRacine.intHierarchyComponent_TS;
                mcModTemplate.RacineSystem.NameOnDisk = structRacine.strName;
                mcModTemplate.RacineSystem.Template_NRI = mcModTemplate.Template_NRI;

                lstHiCo =  mcView.GetHierarchyComponentList();

                for (int intIdx = 0; intIdx < lstHiCo.Count; intIdx++) 
                {
                    cFolder = new mod_Folder();
                    cFolder.DML_Action = lstHiCo[intIdx].Action;
                    cFolder.HierarchyComponent_NRI = lstHiCo[intIdx].intHierarchyComponent_NRI;
                    cFolder.HierarchyComponent_TS = lstHiCo[intIdx].intHierarchyComponent_TS;
                    cFolder.NameOnDisk = lstHiCo[intIdx].strName;
                    cFolder.NodeLevel = lstHiCo[intIdx].intNodeLevel;
                    cFolder.Type = (FolderType)lstHiCo[intIdx].intFolderType_NRI;
                    cFolder.Template_NRI = mcModTemplate.Template_NRI;

                    if (intIdx == 0) //Il faut ajouter ls enfant ou bien sauvegarder le parent composant
                    {
                        mcModFolder_Root = cFolder;
                        cFolder.ParentComponent = mcModTemplate.RacineSystem;
                        cParentFolder = cFolder;
                    }
                    else if (cParentFolder.NodeLevel < cFolder.NodeLevel)
                    {
                        cFolder.ParentComponent = cParentFolder;
                        cParentFolder = cFolder;
                    }
                    else
                    {
                        cFolder.ParentComponent = cParentFolder;
                    }
                }

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
            bool blnValidReturn = false;

            try
            {
                mcSQL = new clsSQL();

                if (mcSQL.bln_BeginTransaction())
                {
                    mcModTemplate.SetcSQL = mcSQL;

                    blnValidReturn = mcModTemplate.blnSave();

                    mcActionResult = mcModTemplate.ActionResults;

                    if (blnValidReturn && mcActionResult.IsValid)
                    {
                        mcModFolder_Root.SetcSQL = mcSQL;

                        blnValidReturn = mcModFolder_Root.blnSave();

                        mcActionResult = mcModTemplate.ActionResults;
                    }
                }
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (!blnValidReturn & mcActionResult.IsValid)
                {
                    mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_SAVE_MSG, clsActionResults.BaseErrorCode.ERROR_SAVE);
                }
                else if (blnValidReturn & !mcActionResult.IsValid)
                {
                    blnValidReturn = false;
                }

                mcSQL.bln_EndTransaction(mcActionResult.IsValid);
                mcSQL = null;
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
            strSQL = strSQL + "   		HierarchyComp.HiCo_Name,  " + Environment.NewLine;
            strSQL = strSQL + "   		FoT_NRI = '',  " + Environment.NewLine;
            strSQL = strSQL + "   		FoT_Code = '' " + Environment.NewLine;

            strSQL = strSQL + " FROM AppConfig  " + Environment.NewLine;
            strSQL = strSQL + " 	INNER JOIN HierarchyComp ON HierarchyComp.ACg_NRI = AppConfig.ACg_NRI " + Environment.NewLine;
            //strSQL = strSQL + " 	INNER JOIN FolderType ON FolderType.FoT_NRI = HierarchyComp.FoT_NRI " + Environment.NewLine;

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
                strSQL = strSQL + "   		HierarchyComp.HiCo_Name,  " + Environment.NewLine;
                strSQL = strSQL + "   		FolderType.FoT_NRI,  " + Environment.NewLine;
                strSQL = strSQL + "   		FolderType.FoT_Code  " + Environment.NewLine;

                strSQL = strSQL + " FROM Template  " + Environment.NewLine;
                strSQL = strSQL + " 	INNER JOIN HierarchyComp " + Environment.NewLine;
                strSQL = strSQL + " 	    INNER JOIN FolderType ON FolderType.FoT_NRI = HierarchyComp.FoT_NRI " + Environment.NewLine;
                strSQL = strSQL + " 	ON HierarchyComp.Tpl_NRI = Template.Tpl_NRI " + Environment.NewLine;

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

        public string strGetListe_FolderTypes_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT FolderType.FoT_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        FolderType.FoT_Code " + Environment.NewLine;

            strSQL = strSQL + " FROM FolderType " + Environment.NewLine;

            strSQL = strSQL + " WHERE FolderType.FoT_Modifiable = 1 " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY FolderType.FoT_NRI " + Environment.NewLine;

            return strSQL;
        }
    }
}
