using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Models.Module_ActivesInstallations;

namespace Ceritar.CVS.Controllers
{
    /// <summary>
    /// Cette classe représente le controleur qui fait le lien entre la vue permettant de définir les révisions d'une version et le modèle mod_Ver_Version.
    /// Elle passe par l'interface IVersion afin d'extraire les informations de la vue.
    /// </summary>
    public class ctr_Revision
    {
        private Interfaces.IRevision mcView;
        private mod_Rev_Revision mcModRevision;
        private clsActionResults mcActionResult;
        private clsSQL mcSQL;

        public enum ErrorCode_Rev
        {
            MODIFICATION_LIST_MANDATORY = 1,
            REVISION_NUMBER_MANDATORY = 2,
            TEMPLATE_MANDATORY = 3,
            VERSION_MANDATORY = 4,
            CERITAR_CLIENT_MANDATORY = 5,
            EXE_OR_SCRIPT_MANDATORY = 6
        }

        public ctr_Revision(Interfaces.IRevision rView)
        {
            mcModRevision = new mod_Rev_Revision();
            
            mcView = rView;

            mcActionResult = new clsActionResults();
        }

        public clsActionResults Validate()
        {
            mcModRevision = new mod_Rev_Revision();

            try
            {
                mcModRevision.DML_Action = mcView.GetDML_Action();
                mcModRevision.Revision_Number = mcView.GetRevisionNo();

                mcModRevision.CeritarClient = new Models.Module_Configuration.mod_CeC_CeritarClient();
                mcModRevision.CeritarClient.CeritarClient_NRI = mcView.GetCeritarClient_NRI();
                //mcModRevision.CreatedByUser = new mod_TTU_User();
                //mcModRevision.CreatedByUser.
                mcModRevision.CreationDate = mcView.GetCreationDate();
                mcModRevision.LstModifications = mcView.GetModificationsList();
                mcModRevision.Path_Release = mcView.GetLocation_Release();
                mcModRevision.Path_Scripts = mcView.GetLocation_Scripts();
                mcModRevision.Revision_NRI = mcView.GetRevision_NRI();
                mcModRevision.Revision_TS = mcView.GetRevision_TS();

                mcModRevision.TemplateSource = new Models.Module_Template.mod_Tpl_HierarchyTemplate();
                mcModRevision.TemplateSource.Template_NRI = mcView.GetTemplateSource_NRI();

                mcModRevision.Version = new mod_Ver_Version();
                mcModRevision.Version.Version_NRI = mcView.GetVersion_NRI();
                mcModRevision.Version.VersionNo = mcView.GetVersionNo();

                mcActionResult = mcModRevision.Validate();
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
                    mcModRevision.SetcSQL = mcSQL;

                    blnValidReturn = mcModRevision.blnSave();

                    mcActionResult = mcModRevision.ActionResults;
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

        public bool blnBuildRevisionHierarchy(int vintTemplate_NRI)
        {
            bool blnValidReturn = false;
            SqlCommand cSQLCmd = default(SqlCommand);
            SqlDataReader cSQLReader = null;
            DirectoryInfo currentFolderInfos = null;

            mcActionResult.SetDefault();

            try
            {
                //TODO Use template
                currentFolderInfos = new DirectoryInfo(Path.Combine(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES, 
                                                                    mcView.GetCeritarApplication_Name(), 
                                                                    sclsAppConfigs.GetVersionNumberPrefix + mcView.GetVersionNo().ToString(), 
                                                                    sclsAppConfigs.GetRevisionNumberPrefix + mcView.GetRevisionNo().ToString()));

                if (!string.IsNullOrEmpty(mcView.GetLocation_Scripts()))
                {
                    clsApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_Scripts(), Path.Combine(currentFolderInfos.FullName, "Scripts"), true, true);
                }

                if (!string.IsNullOrEmpty(mcView.GetLocation_Release()))
                {
                    clsApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_Scripts(), Path.Combine(currentFolderInfos.FullName, "Release"), true, true);
                }

                mcActionResult.SetValid();
            }
            catch (FileNotFoundException exPath)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exPath.FileName);
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (cSQLReader != null) cSQLReader.Dispose();

                if (blnValidReturn) mcActionResult.SetValid();
            }

            return blnValidReturn;
        }


#region "SQL Queries"

        public string strGetDataLoad_SQL(int vintVersion_NRI)
        {
            string strSQL = string.Empty;
            string strNewRevisionNo = string.Empty;

            if (mcView.GetDML_Action() == sclsConstants.DML_Mode.INSERT_MODE)
            {
                strNewRevisionNo = clsSQL.str_ADOSingleLookUp("MAX(ISNULL(Revision.Rev_No, 0)) + 1", "Version LEFT JOIN Revision ON Revision.Ver_NRI = Version.Ver_NRI", "Version.Ver_NRI = " + vintVersion_NRI);
            }

            strSQL = strSQL + " SELECT Version.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_TS, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_DtCreation, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_Location_Exe, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_Location_Scripts, " + Environment.NewLine;
            strSQL = strSQL + "        RevisionNo = CASE WHEN Revision.Rev_NRI IS NULL THEN " + clsApp.GetAppController.str_FixStringForSQL(strNewRevisionNo) + " ELSE Revision.Rev_No END, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_NRI " + Environment.NewLine;
                
            strSQL = strSQL + " FROM Version " + Environment.NewLine;
            strSQL = strSQL + "     LEFT JOIN Revision " + Environment.NewLine;
            strSQL = strSQL + "         INNER JOIN CerClient ON CerClient.CeC_NRI = Revision.CeC_NRI " + Environment.NewLine;
            strSQL = strSQL + "     ON Revision.Ver_NRI = Version.Ver_NRI " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerApp ON CerApp.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;
            //strSQL = strSQL + "     INNER JOIN TTUser ON TTUser.TTU_NRI = Revision.TTU_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_RevisionModifications_SQL(int vintRevision_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = '" + sclsConstants.DML_Mode.NO_MODE + "', " + Environment.NewLine;
            strSQL = strSQL + "        RevModifs.RevM_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        RevModifs.RevM_ChangeDesc " + Environment.NewLine;

            strSQL = strSQL + " FROM RevModifs " + Environment.NewLine;

            strSQL = strSQL + " WHERE RevModifs.Rev_NRI = " + vintRevision_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetTemplates_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Template.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Template " + Environment.NewLine;

            strSQL = strSQL + " WHERE Template.TeT_NRI = " + (int)ctr_Template.TemplateType.REVISION + Environment.NewLine;

            strSQL = strSQL + " ORDER BY Template.Tpl_ByDefault DESC, Template.Tpl_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetClients_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CerClient " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_Name " + Environment.NewLine;

            return strSQL;
        }

#endregion
        

    }
}
