using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Configuration;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;

namespace Ceritar.CVS.Controllers
{
    public class ctr_CeritarClient
    {
        private Interfaces.ICeritarClient mcView;
        private mod_CeC_CeritarClient mcModCerClient;
        private clsActionResults mcActionResult;
        private clsSQL mcSQL;

        public enum ErrorCode_CeC
        {
            NAME_MANDATORY = 1,
            IS_ACTIVE_MANDATORY = 2,
            APPLICATION_LIST_MANDATORY = 4
        }

        public ctr_CeritarClient(Interfaces.ICeritarClient rView)
        {
            mcModCerClient = new Models.Module_Configuration.mod_CeC_CeritarClient();
            mcView = rView;

        }
        public clsActionResults Validate()
            {
            try
            {
                mcModCerClient = new Models.Module_Configuration.mod_CeC_CeritarClient();


            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResult;
        }

        #region "SQL Queries"

        public string strGetApplications_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerApp.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CerApp " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerApp.CeA_Name " + Environment.NewLine;

            return strSQL;
        }
        
        public string strGetDataLoad_SQL(int vintCeC_NRI)
        {
            string strSQL = string.Empty;
            strSQL = strSQL + " SELECT CerClient.CeC_TS, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_IsActive " + Environment.NewLine;

            strSQL = strSQL + " FROM CerClient " + Environment.NewLine;

            strSQL = strSQL + " WHERE CerClient.CeC_NRI = " + vintCeC_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_Application_SQL(int vintCeC_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = " + (int)sclsConstants.DML_Mode.NO_MODE + ", " + Environment.NewLine;
            strSQL = strSQL + "        CeC_CeA.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CeC_CeA " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerApp ON CerApp.CeA_NRI = CeC_CeA.CeA_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE CeC_CeA.CeC_NRI = " + vintCeC_NRI + Environment.NewLine;
            
            return strSQL;
        }



#endregion
    }


}
