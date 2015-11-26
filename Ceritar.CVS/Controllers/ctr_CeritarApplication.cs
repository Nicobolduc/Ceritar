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

        public string strGetListe_Modules(int vintCerApp_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT AppModule.ApM_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        AppModule.ApM_TS, " + Environment.NewLine;
            strSQL = strSQL + "        AppModule.ApM_Desc " + Environment.NewLine;
            strSQL = strSQL + " FROM AppModule " + Environment.NewLine;
            strSQL = strSQL + " WHERE AppModule.CeA_NRI = " + vintCerApp_NRI + Environment.NewLine;

            return strSQL;
        }

        public clsActionResults Validate()
        {
            try
            {
                mcCerApp = new Models.Module_Configuration.mod_CeritarApplication();
                mcCerApp.Name = mcView.GetName();
                mcCerApp.Description = mcView.GetDescription();
                mcCerApp.LstModules = mcView.GetLstModules();
                mcCerApp.Action = mcView.GetDML_Mode();

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
