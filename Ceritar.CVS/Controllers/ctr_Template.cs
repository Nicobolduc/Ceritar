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
        private clsActionResults mcActionResult = new clsActionResults();
        private clsSQL mcSQL;

        public enum FolderType
        {
            Normal = 1,
            Ceritar_Application = 2,
            Release = 3,
            CaptionsAndMenus = 4,
            Scripts = 5,
            Report = 6,
            Version_Number = 7,
            Other = 8
        }

        public enum TemplateType
        {
            VERSION = 1,
            REVISION = 2
        }

#region "Error codes"

        //Note: Errors codes must all have different values when in the same class

        public enum ErrorCode_Tpl
        {
            NAME_MANDATORY = 1,
            TEMPLATE_TYPE_MANDATORY = 2,
            HIERARCHY_MANDATORY = 3,
            CERITAR_APPLICATION_MANDATORY = 4,
            UNIQUE_DEFAULT_TEMPLATE = 5,
            ONLY_NORMAL_AND_OTHER_FOLDERTYPE_MULTIPLE = 6
        }

        public enum ErrorCode_HiCo
        {
            NAME_ON_DISK_MANDATORY = 10,
            FOLDER_TYPE_MANDATORY = 11
        }

#endregion

        public ctr_Template(Interfaces.ITemplate rView)
        {
            mcModTemplate = new mod_Tpl_HierarchyTemplate();
            mcView = rView;
        }

        public clsActionResults Validate()
        {
            mod_Folder cCurrentFolder;
            structHierarchyComponent structRacine;
            List<structHierarchyComponent> lstHiCo;
            mod_Folder cParentFolder = null;
            mod_Folder cPreviousFolder = null;

            mcActionResult.SetDefault();

            try
            {
                mcModTemplate.DML_Action = mcView.GetDML_Action();
                mcModTemplate.TemplateName = mcView.GetTemplateName();
                mcModTemplate.Template_NRI = mcView.GetTemplate_NRI();
                mcModTemplate.Template_TS = mcView.GetTemplate_TS();
                mcModTemplate.IsByDefault = mcView.GetByDefaultValue();
                mcModTemplate.TemplatType = (mod_Tpl_HierarchyTemplate.TemplateType)mcView.GetTemplateType_NRI();
                mcModTemplate.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();

                structRacine = mcView.GetRacineSystem();

                if (structRacine.intHierarchyComponent_NRI > 0 || mcModTemplate.DML_Action == sclsConstants.DML_Mode.INSERT_MODE)
                {
                    mcModTemplate.RacineSystem = new mod_Folder();
                    mcModTemplate.RacineSystem.DML_Action = structRacine.Action;
                    mcModTemplate.RacineSystem.HierarchyComponent_NRI = structRacine.intHierarchyComponent_NRI;
                    mcModTemplate.RacineSystem.HierarchyComponent_TS = structRacine.intHierarchyComponent_TS;
                    mcModTemplate.RacineSystem.NameOnDisk = structRacine.strName;
                    mcModTemplate.RacineSystem.Template_NRI = mcModTemplate.Template_NRI;
                    mcModTemplate.RacineSystem.ParentComponent = new mod_Folder();
                    mcModTemplate.RacineSystem.ParentComponent.HierarchyComponent_NRI = structRacine.Parent_NRI;
                    ((mod_Folder)mcModTemplate.RacineSystem).Type = structRacine.FolderType;
                    ((mod_Folder)mcModTemplate.RacineSystem).NodeLevel = structRacine.intNodeLevel;
                }

                lstHiCo = mcView.GetHierarchyComponentList();

                for (int intIdx = 0; intIdx < lstHiCo.Count; intIdx++)
                {
                    cCurrentFolder = new mod_Folder();
                    cCurrentFolder.DML_Action = lstHiCo[intIdx].Action;
                    cCurrentFolder.HierarchyComponent_NRI = lstHiCo[intIdx].intHierarchyComponent_NRI;
                    cCurrentFolder.HierarchyComponent_TS = lstHiCo[intIdx].intHierarchyComponent_TS;
                    cCurrentFolder.NameOnDisk = lstHiCo[intIdx].strName;
                    cCurrentFolder.NodeLevel = lstHiCo[intIdx].intNodeLevel;
                    cCurrentFolder.Type = (FolderType)lstHiCo[intIdx].FolderType;
                    cCurrentFolder.Template_NRI = mcModTemplate.Template_NRI;

                    if (intIdx == 0)
                    {
                        cParentFolder = ((mod_Folder)mcModTemplate.RacineSystem);
                        ((mod_Folder)mcModTemplate.RacineSystem).LstChildrensComponents.Add(cCurrentFolder);
                    }
                    else if (cPreviousFolder.NodeLevel < cCurrentFolder.NodeLevel)
                    {
                        cParentFolder = (mod_Folder)cPreviousFolder;
                        cPreviousFolder.LstChildrensComponents.Add(cCurrentFolder);
                    }
                    else if (cPreviousFolder.NodeLevel > cCurrentFolder.NodeLevel)
                    {
                        cParentFolder = (mod_Folder)cPreviousFolder.ParentComponent.ParentComponent;
                        cParentFolder.LstChildrensComponents.Add(cCurrentFolder);
                    }
                    else
                    {
                        cParentFolder.LstChildrensComponents.Add(cCurrentFolder);
                    }

                    cCurrentFolder.ParentComponent = cParentFolder;

                    cPreviousFolder = cCurrentFolder;
                }
                
                mcActionResult = mcModTemplate.Validate(); //TODO Valider hierarchy

                if (mcActionResult.IsValid)
                {
                    mcActionResult = ((mod_Folder)mcModTemplate.RacineSystem).Validate();
                }
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (!mcActionResult.IsValid) mcModTemplate = new mod_Tpl_HierarchyTemplate();
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
                        mcModTemplate.RacineSystem.SetcSQL = mcSQL;
                        mcModTemplate.RacineSystem.Template_NRI = mcModTemplate.Template_NRI;

                        blnValidReturn = mcModTemplate.RacineSystem.blnSave();

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
                mcModTemplate = new mod_Tpl_HierarchyTemplate();
            }

            return mcActionResult;
        }

        public clsActionResults blnDeleteTemplate()
        {
            bool blnValidReturn = false;

            mcActionResult.SetDefault();

            try
            {
                mcSQL = new clsSQL();

                mcModTemplate.SetcSQL = mcSQL;

                mcActionResult = mcModTemplate.Validate();

                if (!mcActionResult.IsValid)
                { }
                else if (!mcSQL.bln_BeginTransaction())
                { }
                else if (!mcSQL.bln_ADODelete("HierarchyComp", "Tpl_NRI = " + mcModTemplate.Template_NRI))
                { }
                else if (!mcModTemplate.blnSave())
                { }
                else
                {
                    blnValidReturn = true;
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


#region "SQL Queries"

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

            //strSQL = strSQL + " SELECT  ActionCol = " + (vintTemplate_NRI == 0 ? (int)sclsConstants.DML_Mode.INSERT_MODE: (int)sclsConstants.DML_Mode.NO_MODE) + "," + Environment.NewLine;
            //strSQL = strSQL + "   		HierarchyComp.HiCo_NRI,  " + Environment.NewLine;
            //strSQL = strSQL + "   		IsSystem = 1,  " + Environment.NewLine;
            //strSQL = strSQL + "   		HierarchyComp.HiCo_NodeLevel AS HiCo_NodeLevel,  " + Environment.NewLine;
            //strSQL = strSQL + "   		IsRoot = CASE WHEN HierarchyComp.HiCo_Parent_NRI = TNode.Parent OR HierarchyComp.HiCo_Parent_NRI IS NULL THEN 1 ELSE 0 END, " + Environment.NewLine;
            //strSQL = strSQL + "   		HierarchyComp.HiCo_Name,  " + Environment.NewLine;
            //strSQL = strSQL + "   		FoT_NRI = '',  " + Environment.NewLine;
            //strSQL = strSQL + "   		FoT_Code = '' " + Environment.NewLine;

            //strSQL = strSQL + " FROM AppConfig  " + Environment.NewLine;
            //strSQL = strSQL + " 	INNER JOIN HierarchyComp ON HierarchyComp.ACg_NRI = AppConfig.ACg_NRI " + Environment.NewLine;
            ////strSQL = strSQL + " 	INNER JOIN FolderType ON FolderType.FoT_NRI = HierarchyComp.FoT_NRI " + Environment.NewLine;

            //strSQL = strSQL + " 	LEFT JOIN ( SELECT MIN(HierarchyComp.HiCo_NRI) AS Enfant, MAX(Parent.HiCo_NRI) AS Parent  " + Environment.NewLine;
            //strSQL = strSQL + "   				FROM HierarchyComp   " + Environment.NewLine;
            //strSQL = strSQL + "   					INNER JOIN HierarchyComp Parent ON Parent.HiCo_NRI = HierarchyComp.HiCo_Parent_NRI   " + Environment.NewLine;
            //strSQL = strSQL + "   				WHERE HierarchyComp.HiCo_NodeLevel - 1 = Parent.HiCo_NodeLevel  " + Environment.NewLine;
            //strSQL = strSQL + "   				GROUP BY HierarchyComp.HiCo_NodeLevel  " + Environment.NewLine;
            //strSQL = strSQL + "   				) AS TNode ON TNode.Enfant = HierarchyComp.HiCo_NRI  " + Environment.NewLine;

            //strSQL = strSQL + " WHERE AppConfig.ACg_Racine_Name = 'InstallationsActives' " + Environment.NewLine;

            //if (vintTemplate_NRI > 0)
            //{
            //    strSQL = strSQL + " UNION ALL " + Environment.NewLine;

            //    strSQL = strSQL + " SELECT  ActionCol = 0,  " + Environment.NewLine;
            //    strSQL = strSQL + "   		HierarchyComp.HiCo_NRI,  " + Environment.NewLine;
            //    strSQL = strSQL + "   		IsSystem = 0,  " + Environment.NewLine;
            //    strSQL = strSQL + "   		HierarchyComp.HiCo_NodeLevel AS HiCo_NodeLevel,  " + Environment.NewLine;
            //    strSQL = strSQL + "   		IsRoot = CASE WHEN HierarchyComp.HiCo_Parent_NRI = TNode.Parent OR HierarchyComp.HiCo_Parent_NRI IS NULL THEN 1 ELSE 0 END, " + Environment.NewLine;
            //    strSQL = strSQL + "   		HierarchyComp.HiCo_Name,  " + Environment.NewLine;
            //    strSQL = strSQL + "   		FolderType.FoT_NRI,  " + Environment.NewLine;
            //    strSQL = strSQL + "   		FolderType.FoT_Code  " + Environment.NewLine;

            //    strSQL = strSQL + " FROM Template  " + Environment.NewLine;
            //    strSQL = strSQL + " 	INNER JOIN HierarchyComp " + Environment.NewLine;
            //    strSQL = strSQL + " 	    INNER JOIN FolderType ON FolderType.FoT_NRI = HierarchyComp.FoT_NRI " + Environment.NewLine;
            //    strSQL = strSQL + " 	ON HierarchyComp.Tpl_NRI = Template.Tpl_NRI " + Environment.NewLine;

            //    strSQL = strSQL + " 	LEFT JOIN ( SELECT MIN(HierarchyComp.HiCo_NRI) AS Enfant, MAX(Parent.HiCo_NRI) AS Parent  " + Environment.NewLine;
            //    strSQL = strSQL + "   				FROM HierarchyComp   " + Environment.NewLine;
            //    strSQL = strSQL + "   					INNER JOIN HierarchyComp Parent ON Parent.HiCo_NRI = HierarchyComp.HiCo_Parent_NRI   " + Environment.NewLine;
            //    strSQL = strSQL + "   				WHERE HierarchyComp.HiCo_NodeLevel - 1 = Parent.HiCo_NodeLevel  " + Environment.NewLine;
            //    strSQL = strSQL + "   				GROUP BY HierarchyComp.HiCo_NodeLevel  " + Environment.NewLine;
            //    strSQL = strSQL + "   				) AS TNode ON TNode.Enfant = HierarchyComp.HiCo_NRI  " + Environment.NewLine;

            //    strSQL = strSQL + " WHERE Template.Tpl_NRI = " + vintTemplate_NRI + Environment.NewLine;
            //}

            //strSQL = strSQL + " ORDER BY HierarchyComp.HiCo_NodeLevel " + Environment.NewLine;

            strSQL = strSQL + " WITH LstHierarchyComp " + Environment.NewLine;
            strSQL = strSQL + " AS " + Environment.NewLine;
            strSQL = strSQL + " ( " + Environment.NewLine;
            strSQL = strSQL + "     SELECT *, " + Environment.NewLine;
            strSQL = strSQL + " 		   CAST(0 AS varbinary(max)) AS Level " + Environment.NewLine;
            strSQL = strSQL + " 	FROM HierarchyComp  " + Environment.NewLine;
            strSQL = strSQL + " 	WHERE HiCo_Parent_NRI IS NULL " + Environment.NewLine;

            strSQL = strSQL + "     UNION ALL " + Environment.NewLine;

            strSQL = strSQL + "     SELECT HiCo_Childrens.*, " + Environment.NewLine;
            strSQL = strSQL + " 		   Level + CAST(HiCo_Childrens.HiCo_NRI AS varbinary(max)) AS Level " + Environment.NewLine;
            strSQL = strSQL + " 	FROM HierarchyComp HiCo_Childrens  " + Environment.NewLine;
            strSQL = strSQL + " 		INNER JOIN LstHierarchyComp on HiCo_Childrens.HiCo_Parent_NRI = LstHierarchyComp.HiCo_NRI " + Environment.NewLine;
            strSQL = strSQL + " 	WHERE HiCo_Childrens.HiCo_Parent_NRI IS NOT NULL " + Environment.NewLine;
            strSQL = strSQL + " ) " + Environment.NewLine;

            strSQL = strSQL + " SELECT  ActionCol = 0, " + Environment.NewLine;
            strSQL = strSQL + "    		LstHierarchyComp.HiCo_NRI,   " + Environment.NewLine;
            strSQL = strSQL + "    		IsSystem = CASE WHEN LstHierarchyComp.TTP_NRI IS NULL THEN 0 ELSE 1 END,   " + Environment.NewLine;
            strSQL = strSQL + "    		LstHierarchyComp.HiCo_NodeLevel,   " + Environment.NewLine;
            strSQL = strSQL + "    		IsNode = 0, --CASE WHEN LstHierarchyComp.HiCo_Parent_NRI = TNode.Parent OR LstHierarchyComp.HiCo_Parent_NRI IS NULL THEN 1 ELSE 0 END,  " + Environment.NewLine; //Not used anymore
            strSQL = strSQL + "    		LstHierarchyComp.HiCo_Name,   " + Environment.NewLine;
            strSQL = strSQL + "    		FoT_NRI = CASE WHEN LstHierarchyComp.TTP_NRI IS NULL THEN LstHierarchyComp.FoT_NRI ELSE 0 END,  " + Environment.NewLine;
            strSQL = strSQL + "    		FoT_Code = CASE WHEN LstHierarchyComp.TTP_NRI IS NULL THEN FolderType.FoT_Code ELSE NULL END  " + Environment.NewLine;

            strSQL = strSQL + " FROM LstHierarchyComp " + Environment.NewLine;
            strSQL = strSQL + " 	INNER JOIN FolderType ON FolderType.FoT_NRI = LstHierarchyComp.FoT_NRI  " + Environment.NewLine;

            //strSQL = strSQL + " 	LEFT JOIN ( SELECT MIN(HierarchyComp.HiCo_NRI) AS Enfant, MAX(Parent.HiCo_NRI) AS Parent   " + Environment.NewLine;
            //strSQL = strSQL + "    				FROM HierarchyComp    " + Environment.NewLine;
            //strSQL = strSQL + "    					INNER JOIN HierarchyComp Parent ON Parent.HiCo_NRI = HierarchyComp.HiCo_Parent_NRI    " + Environment.NewLine;
            //strSQL = strSQL + "    				WHERE HierarchyComp.HiCo_NodeLevel - 1 = Parent.HiCo_NodeLevel   " + Environment.NewLine;
            //strSQL = strSQL + "    				GROUP BY HierarchyComp.HiCo_NodeLevel   " + Environment.NewLine;
            //strSQL = strSQL + "    			   ) AS TNode ON TNode.Enfant = HiCo_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE LstHierarchyComp.TTP_NRI = " + (int)sclsAppConfigs.TTPARAM_TYPE.PATH_INSTALLATIONS_ACTIVES + " OR LstHierarchyComp.Tpl_NRI = " + vintTemplate_NRI + Environment.NewLine;

            strSQL = strSQL + " ORDER BY Level " + Environment.NewLine;

            return strSQL;
        }

        public string strGetApplications_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerApp.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CerApp " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerApp.CeA_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetTemplateTypes_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT TemplateType.TeT_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        TemplateType.TeT_Code " + Environment.NewLine;

            strSQL = strSQL + " FROM TemplateType " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY TemplateType.TeT_NRI " + Environment.NewLine;

            return strSQL;
        }

        public string strGetFolderTypes_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT FolderType.FoT_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        FolderType.FoT_Code " + Environment.NewLine;

            strSQL = strSQL + " FROM FolderType " + Environment.NewLine;

            strSQL = strSQL + " WHERE FolderType.FoT_Modifiable = 1 " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY FolderType.FoT_NRI " + Environment.NewLine;

            return strSQL;
        }

#endregion


    }
}
