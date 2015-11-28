using System;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;


namespace Ceritar.CVS.Controllers
{
    public class ctr_CeritarApplication
    {
        private Interfaces.ICeritarApp mcView;
        private Models.Module_Configuration.mod_CeritarApplication mcCerApp;
        private clsActionResults mcActionResult;

        public ctr_CeritarApplication(Interfaces.ICeritarApp vView)
        {
            mcCerApp = new Models.Module_Configuration.mod_CeritarApplication();
            mcView = vView;
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

        public clsActionResults Validate()
        {
            try
            {
                mcCerApp = new Models.Module_Configuration.mod_CeritarApplication();
                mcCerApp.CeritarApplication_NRI = mcView.GetCerApp_NRI();
                mcCerApp.Name = mcView.GetName();
                mcCerApp.Description = mcView.GetDescription();
                mcCerApp.LstModules = mcView.GetLstModules();
                mcCerApp.Action = mcView.GetDML_Mode();
                mcCerApp.Domaine_NRI = mcView.GetDomain_NRI();

                mcActionResult = mcCerApp.Validate();
            }
            catch (Exception ex) {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResult;
        }

        public clsActionResults Save()
        {
            try
            {
                mcActionResult = mcCerApp.Save();
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResult;
        }
    }
}
