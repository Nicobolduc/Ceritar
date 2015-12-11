using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_ActivesInstallations;
using Ceritar.CVS.Controllers.Interfaces;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;

namespace Ceritar.CVS.Controllers
{
    public class ctr_Version
    {
        private Interfaces.IVersion mcView;
        private mod_Ver_Version mcModVersion;
        private clsActionResults mcActionResult;
        private clsSQL mcSQL;

        public enum ErrorCode_Ver
        {
            COMPILED_BY_MANDATORY = 1,
            CLIENTS_LIST_MANDATORY = 2,
            VERSION_NO_MANDATORY = 3,
            APP_CHANGEMENT_MANDATORY = 4,
            TTAPP_MANDATORY = 5,
            RELEASE_MANDATORY = 6,
            CERITAR_APP_MANDATORY = 7,
            TEMPLATE_MANDATORY = 8
        }

        public ctr_Version(IVersion rView)
        {
            mcModVersion = new mod_Ver_Version();
            mcView = rView;
        }

        public clsActionResults Validate()
        {
            List<int> lstCeritarClient_NRI = new List<int>();

            try
            {
                mcModVersion = new mod_Ver_Version();
                mcModVersion.Version_NRI = mcView.GetVersion_NRI();
                mcModVersion.Version_TS = mcView.GetVersion_TS();
                mcModVersion.VersionNo = mcView.GetVersionNo();
                mcModVersion.Application = new Models.Module_Configuration.mod_CeA_CeritarApplication();
                mcModVersion.Application.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                mcModVersion.Location_APP_CHANGEMENT = mcView.GetLocation_APP_CHANGEMENT();
                mcModVersion.Location_Release = mcView.GetLocation_Release();
                mcModVersion.Location_TTApp = mcView.GetLocation_TTApp();
                mcModVersion.TemplateSource = new Models.Module_Template.mod_Tpl_HierarchyTemplate();
                mcModVersion.TemplateSource.Template_NRI = mcView.GetTemplateSource_NRI();
                mcModVersion.LstClientsUsing = mcView.GetClientUsingList();              

                mcActionResult = mcModVersion.Validate();
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

                    mcModVersion.SetcSQL = mcSQL;

                    blnValidReturn = mcModVersion.blnSave();

                    mcActionResult = mcModVersion.ActionResults;
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

        public string strGetDataLoad_SQL(int vintVersion_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Version.Ver_TS, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_CompiledBy, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_DtCreation, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Tpl_NRI " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN Revision ON Revision.Ver_NRI = Version.Ver_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_Clients_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = " + (int)sclsConstants.DML_Mode.NO_MODE + ", " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_TS, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CerClient " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_Name " + Environment.NewLine;

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

        public string strGetTemplates_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Template.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Template " + Environment.NewLine;

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
