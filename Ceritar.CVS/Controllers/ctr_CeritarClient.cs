using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Configuration;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;

namespace Ceritar.CVS.Controllers
{
    /// <summary>
    /// Cette classe représente le controleur qui fait le lien entre la vue permettant de définir les clients de Ceritar et le modèle mod_CeC_CeritarClient.
    /// Elle passe par l'interface ICeritarClient afin d'extraire les informations de la vue.
    /// </summary>
    public class ctr_CeritarClient
    {
        private Interfaces.ICeritarClient mcView;
        private mod_CeC_CeritarClient mcModCerClient;
        private clsActionResults mcActionResult;
        private clsSQL mcSQL;

        public enum ErrorCode_CeC
        {
            NAME_MANDATORY = 1
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

                mcModCerClient.CeritarClient_NRI = mcView.GetCerClient_NRI();
                mcModCerClient.CeritarClient_TS = mcView.GetCerClient_TS();
                mcModCerClient.CompanyName = mcView.GetName();
                mcModCerClient.DML_Action = mcView.GetDML_Mode();
                mcModCerClient.IsActive = mcView.GetIsActive();

                mcActionResult = mcModCerClient.Validate();
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
            try
            {
                mcSQL = new clsSQL();

                if (mcSQL.bln_BeginTransaction())
                {
                    mcModCerClient.SetcSQL = mcSQL;

                    mcModCerClient.blnSave();

                    mcActionResult = mcModCerClient.ActionResults;
                }
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                mcSQL.bln_EndTransaction(mcActionResult.IsValid);
                mcSQL = null;
            }

            return mcActionResult;
        }

#region "SQL Queries"
        
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

            strSQL = strSQL + " SELECT CerApp.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM ClientAppVersion " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerApp ON CerApp.CeA_NRI = ClientAppVersion.CeA_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE ClientAppVersion.CeC_NRI = " + vintCeC_NRI + Environment.NewLine;
            strSQL = strSQL + "   AND ClientAppVersion.CAV_Installed = 1 " + Environment.NewLine;

            strSQL = strSQL + " GROUP BY CerApp.CeA_NRI, CerApp.CeA_Name " + Environment.NewLine;
            
            return strSQL;
        }

#endregion


    }
}
