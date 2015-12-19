using System;
using System.IO;
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


        public ctr_Revision(Interfaces.IRevision rView)
        {
            mcModRevision = new mod_Rev_Revision();
            
            mcView = rView;

            mcActionResult = new clsActionResults();
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

        public string strGetData_NewVersion(int vintVersion_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Version.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + "        MAX(ISNULL(Revision.Rev_No, 0)) + 1 AS NewRevisionNo, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;
            strSQL = strSQL + "     LEFT JOIN Revision ON Revision.Ver_NRI = Version.Ver_NRI " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerApp ON CerApp.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;

            strSQL = strSQL + " GROUP BY Version.Ver_No, CerApp.CeA_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetDataLoad_SQL(int vintRevision_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Revision.Rev_TS, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_No, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_DtCreation, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.TTU_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.CeC_NRI " + Environment.NewLine;

            strSQL = strSQL + " FROM Revision " + Environment.NewLine;

            strSQL = strSQL + " WHERE Revision.Rev_NRI = " + vintRevision_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_RevisionModifications_SQL(int vintRevision_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = '" + sclsConstants.DML_Mode.NO_MODE + "', " + Environment.NewLine;
            strSQL = strSQL + "        RevModifs.RevM_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        RevModifs.RevM_ChangeDesc " + Environment.NewLine;

            strSQL = strSQL + " FROM RevModifs " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY RevModifs.Rev_NRI = " + vintRevision_NRI + Environment.NewLine;

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
