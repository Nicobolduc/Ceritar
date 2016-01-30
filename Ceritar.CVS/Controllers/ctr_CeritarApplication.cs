using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Configuration;
using Ceritar.CVS.Models.Module_ActivesInstallations;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers.Interfaces;


namespace Ceritar.CVS.Controllers
{
    /// <summary>
    /// Cette classe représente le controleur qui fait le lien entre la vue permettant de définir les applications de Ceritar et le modèle mod_CeA_CeritarApplication.
    /// Elle passe par l'interface ICeritarApp afin d'extraire les informations de la vue.
    /// </summary>
    public class ctr_CeritarApplication
    {
        private Interfaces.ICeritarApp mcView;
        private mod_CeA_CeritarApplication mcModCerApp;
        private clsActionResults mcActionResult;
        private clsSQL mcSQL;

        public enum ErrorCode_CeA
        {
            NAME_MANDATORY = 1,
            NAME_INVALID = 2,
            DESCRIPTION_MANDATORY = 3,
            DOMAIN_MANDATORY = 4,
            MODULES_LIST_MANDATORY = 5,
            SATELLITE_LIST_MANDATORY = 6,
            SATELLITE_NAME_INVALID = 7
        }

        public enum ErrorCode_CSA
        {
            NAME_MANDATORY = 1,
            KIT_FOLDER_NAME_MANDATORY = 2
        }

        public ctr_CeritarApplication(Interfaces.ICeritarApp rView)
        {
            mcModCerApp = new mod_CeA_CeritarApplication();
            mcView = rView;
        }

        public clsActionResults Validate()
        {
            List<structCeritarSatelliteApp> lstSatelliteApps;
            mod_CSA_CeritarSatelliteApp cCSA;

            try
            {
                mcModCerApp = new mod_CeA_CeritarApplication();
                mcModCerApp.CeritarApplication_NRI = mcView.GetCerApp_NRI();
                mcModCerApp.Name = mcView.GetName();
                mcModCerApp.Description = mcView.GetDescription();
                mcModCerApp.ExternalReportAppName = mcView.GetExternalReportAppName();
                mcModCerApp.LstModules = mcView.GetLstModules();
                mcModCerApp.DML_Action = mcView.GetDML_Mode();
                mcModCerApp.Domaine_NRI = (mod_CeA_CeritarApplication.AppDomain)mcView.GetDomain_NRI();

                lstSatelliteApps = mcView.GetLstAppSatellites();

                foreach (structCeritarSatelliteApp structCSA in lstSatelliteApps)
                {
                    cCSA = new mod_CSA_CeritarSatelliteApp();
                    cCSA.CeritarApp_NRI = mcModCerApp.CeritarApplication_NRI;
                    cCSA.CeritarSatelliteApp_NRI = structCSA.intCeritarSatelliteApp_NRI;
                    cCSA.CeritarSatelliteApp_TS = structCSA.intCeritarSatelliteApp_TS;
                    cCSA.DML_Action = structCSA.Action;
                    cCSA.ExeIsFolder = structCSA.blnExeIsFolder;
                    cCSA.Name = structCSA.strSatelliteApp_Name;
                    cCSA.KitFolderName = structCSA.strKitExport_FolderName;

                    mcModCerApp.LstCeritarSatelliteApps.Add(cCSA);
                }

                mcActionResult = mcModCerApp.Validate();
            }
            catch (Exception ex) {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (!mcActionResult.IsValid) mcModCerApp = new mod_CeA_CeritarApplication();
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

                    mcModCerApp.SetcSQL = mcSQL;

                    blnValidReturn = mcModCerApp.blnSave();

                    mcActionResult = mcModCerApp.ActionResults;
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

        public string strGetDataLoad_SQL(int vintCeA_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerApp.CeA_TS, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Desc, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.ApD_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_ExternalRPTAppName " + Environment.NewLine;

            strSQL = strSQL + " FROM CerApp " + Environment.NewLine;

            strSQL = strSQL + " WHERE CerApp.CeA_NRI = " + vintCeA_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_Modules_SQL(int vintCerApp_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = " + (int)sclsConstants.DML_Mode.NO_MODE + ", " + Environment.NewLine;
            strSQL = strSQL + "        AppModule.ApM_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        AppModule.ApM_TS, " + Environment.NewLine;
            strSQL = strSQL + "        AppModule.ApM_Desc " + Environment.NewLine;

            strSQL = strSQL + " FROM AppModule " + Environment.NewLine;

            strSQL = strSQL + " WHERE AppModule.CeA_NRI = " + vintCerApp_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_SatelliteApps_SQL(int vintCerApp_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = " + (int)sclsConstants.DML_Mode.NO_MODE + ", " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_TS, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_KitFolderName, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_ExeIsFolder " + Environment.NewLine;

            strSQL = strSQL + " FROM CerSatApp " + Environment.NewLine;

            strSQL = strSQL + " WHERE CerSatApp.CeA_NRI = " + vintCerApp_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_Domains_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT AppDomain.ApD_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        AppDomain.ApD_Code " + Environment.NewLine;

            strSQL = strSQL + " FROM AppDomain " + Environment.NewLine;

            return strSQL;
        }

#endregion


    }
}
