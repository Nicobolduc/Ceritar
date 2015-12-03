using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Configuration;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;


namespace Ceritar.CVS.Controllers
{
    public class ctr_CeritarApplication
    {
        private Interfaces.ICeritarApp mcView;
        private mod_CeA_CeritarApplication mcModCerApp;
        private clsActionResults mcActionResult;
        private clsSQL mcSQL;

        public enum ErrorCode_CeA
        {
            NAME_MANDATORY = 1,
            DESCRIPTION_MANDATORY = 2,
            DOMAIN_MANDATORY = 3,
            MODULES_LIST_MANDATORY = 4
        }


        public ctr_CeritarApplication(Interfaces.ICeritarApp rView)
        {
            mcModCerApp = new Models.Module_Configuration.mod_CeA_CeritarApplication();
            mcView = rView;
        }

        public clsActionResults Validate()
        {
            try
            {
                mcModCerApp = new Models.Module_Configuration.mod_CeA_CeritarApplication();
                mcModCerApp.CeritarApplication_NRI = mcView.GetCerApp_NRI();
                mcModCerApp.Name = mcView.GetName();
                mcModCerApp.Description = mcView.GetDescription();
                mcModCerApp.LstModules = mcView.GetLstModules();
                mcModCerApp.DML_Action = mcView.GetDML_Mode();
                mcModCerApp.Domaine_NRI = (mod_CeA_CeritarApplication.AppDomain)mcView.GetDomain_NRI();

                mcActionResult = mcModCerApp.Validate();
            }
            catch (Exception ex) {
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
                

                if (mcSQL.bln_BeginTransaction()){

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

        public string strGetDataLoad_SQL(int vintCeA_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerApp.CeA_TS, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Desc, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.ApD_NRI " + Environment.NewLine;

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

        public string strGetListe_Domains_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT AppDomain.ApD_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        AppDomain.ApD_Code " + Environment.NewLine;

            strSQL = strSQL + " FROM AppDomain " + Environment.NewLine;

            return strSQL;
        }
    }
}
