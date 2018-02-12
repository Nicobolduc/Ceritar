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
        private clsTTSQL mcSQL;

        public enum ErrorCode_CeC
        {
            NAME_MANDATORY = 1,
            NAME_INVALID = 2
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
                mcSQL = new clsTTSQL();

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

            strSQL = strSQL + " DECLARE @CerClient_NRI INT = " + vintCeC_NRI + Environment.NewLine;

            strSQL = strSQL + " SELECT T1.AppName, " + Environment.NewLine;
            strSQL = strSQL + "        T1.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + "        T1.Rev_No " + Environment.NewLine;
            strSQL = strSQL + " FROM ( " + Environment.NewLine;

            strSQL = strSQL + " --Application principal " + Environment.NewLine;
            strSQL = strSQL + " SELECT AppName = CerApp.CeA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        MIN(TVer.CeA_NRI) AS CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + " 	   MAX(TVer.Ver_No) AS Ver_No, " + Environment.NewLine;
            strSQL = strSQL + " 	   MAX(TVer.Rev_No) AS Rev_No, " + Environment.NewLine;
            strSQL = strSQL + " 	   SEQ = 1 " + Environment.NewLine;

            strSQL = strSQL + " FROM CerApp " + Environment.NewLine;
            strSQL = strSQL + " 	CROSS APPLY (SELECT TOP 1 Version.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + " 							  TRef.Rev_No, " + Environment.NewLine;
            strSQL = strSQL + "  							  ClientAppVersion.CeA_NRI " + Environment.NewLine;
            strSQL = strSQL + " 				 FROM ClientAppVersion " + Environment.NewLine;
            strSQL = strSQL + " 					INNER JOIN Version ON Version.Ver_NRI = ClientAppVersion.Ver_NRI " + Environment.NewLine;
					
            strSQL = strSQL + " 					OUTER APPLY (SELECT TOP 1 Revision.Rev_No " + Environment.NewLine;
            strSQL = strSQL + " 								 FROM Revision " + Environment.NewLine;
            strSQL = strSQL + " 								 WHERE Revision.Ver_NRI = ClientAppVersion.Ver_NRI " + Environment.NewLine;
            strSQL = strSQL + " 								   AND Revision.CeC_NRI = ClientAppVersion.CeC_NRI " + Environment.NewLine;
            strSQL = strSQL + " 								   AND Revision.Rev_Location_Exe IS NOT NULL " + Environment.NewLine;
            strSQL = strSQL + " 								   AND Revision.Rev_ExeIsReport = 0 " + Environment.NewLine;
            strSQL = strSQL + " 								   AND Revision.Rev_PreparationMode = 0 " + Environment.NewLine;
            strSQL = strSQL + " 								 ORDER BY Revision.Rev_No DESC " + Environment.NewLine;
            strSQL = strSQL + " 								) AS TRef " + Environment.NewLine;
								
            strSQL = strSQL + " 				 WHERE ClientAppVersion.CeA_NRI = CerApp.CeA_NRI " + Environment.NewLine;
            strSQL = strSQL + " 				   AND ClientAppVersion.CeC_NRI = @CerClient_NRI " + Environment.NewLine;
            strSQL = strSQL + " 				   AND ClientAppVersion.CAV_DtInstalledProd IS NOT NULL " + Environment.NewLine;

            //strSQL = strSQL + " 				 ORDER BY ClientAppVersion.CeA_NRI DESC, Version.Ver_No DESC " + Environment.NewLine;
            strSQL = strSQL + " 				) AS TVer " + Environment.NewLine;
				
            strSQL = strSQL + " GROUP BY CerApp.CeA_NRI, CerApp.CeA_Name " + Environment.NewLine;


            strSQL = strSQL + " UNION ALL " + Environment.NewLine;

            strSQL = strSQL + " --Rapports externes de l'application principal " + Environment.NewLine;
            strSQL = strSQL + " SELECT AppName = REPLACE(CerApp.CeA_ExternalRPTAppName, '.exe', '' ), " + Environment.NewLine;
            strSQL = strSQL + "        MIN(TVer.CeA_NRI) AS CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "  	   MAX(TVer.Ver_No) AS Ver_No, " + Environment.NewLine;
            strSQL = strSQL + "  	   MAX(TVer.Rev_No) AS Rev_No, " + Environment.NewLine;
            strSQL = strSQL + " 	   SEQ = 2 " + Environment.NewLine;

            strSQL = strSQL + "  FROM CerApp " + Environment.NewLine;
            strSQL = strSQL + "  	CROSS APPLY (SELECT TOP 1 Version.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + "  							  TRef.Rev_No, " + Environment.NewLine;
            strSQL = strSQL + "  							  ClientAppVersion.CeA_NRI " + Environment.NewLine;
            strSQL = strSQL + "  				 FROM ClientAppVersion " + Environment.NewLine;

            strSQL = strSQL + "  					INNER JOIN Version ON Version.Ver_NRI = ClientAppVersion.Ver_NRI " + Environment.NewLine;
            strSQL = strSQL + "  					OUTER APPLY (SELECT TOP 1 Revision.Rev_No " + Environment.NewLine;
            strSQL = strSQL + "  								 FROM Revision " + Environment.NewLine;
            strSQL = strSQL + "  								 WHERE Revision.Ver_NRI = ClientAppVersion.Ver_NRI " + Environment.NewLine;
            strSQL = strSQL + "  								   AND Revision.CeC_NRI = ClientAppVersion.CeC_NRI " + Environment.NewLine;
            strSQL = strSQL + "  								   AND ((Revision.Rev_Location_Exe IS NOT NULL AND Revision.Rev_ExeWithReport = 1) OR Revision.Rev_ExeIsReport = 1)" + Environment.NewLine;
            strSQL = strSQL + " 								   AND Revision.Rev_PreparationMode = 0 " + Environment.NewLine;
            strSQL = strSQL + "  								 ORDER BY Revision.Rev_No DESC " + Environment.NewLine;
            strSQL = strSQL + "  								) AS TRef " + Environment.NewLine;

            strSQL = strSQL + "  				 WHERE ClientAppVersion.CeA_NRI = CerApp.CeA_NRI " + Environment.NewLine;
            strSQL = strSQL + "  				   AND ClientAppVersion.CeC_NRI = @CerClient_NRI " + Environment.NewLine;
            strSQL = strSQL + "  				   AND ClientAppVersion.CAV_DtInstalledProd IS NOT NULL " + Environment.NewLine;
            strSQL = strSQL + "  				   AND ClientAppVersion.CAV_ReportExe_Location IS NOT NULL " + Environment.NewLine;

            //strSQL = strSQL + "  				 ORDER BY ClientAppVersion.CeA_NRI DESC, Version.Ver_No DESC " + Environment.NewLine;
            strSQL = strSQL + "  				) AS TVer " + Environment.NewLine;

            strSQL = strSQL + "  GROUP BY CerApp.CeA_NRI, CerApp.CeA_ExternalRPTAppName " + Environment.NewLine;

            strSQL = strSQL + " UNION ALL " + Environment.NewLine;


            strSQL = strSQL + " --Applications satellites " + Environment.NewLine;
            strSQL = strSQL + " SELECT AppName = CerSatApp.CSA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        MIN(CerSatApp.CeA_NRI) AS CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + " 	   MAX(TVer.Ver_No) AS Ver_No, " + Environment.NewLine;
            strSQL = strSQL + " 	   MAX(TVer.Rev_No) AS Rev_No, " + Environment.NewLine;
            strSQL = strSQL + " 	   SEQ = 3 " + Environment.NewLine;

            strSQL = strSQL + " FROM CerSatApp " + Environment.NewLine;
            strSQL = strSQL + " 	CROSS APPLY (SELECT TOP 1 Version.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + " 							  TRef.Rev_No " + Environment.NewLine;
            strSQL = strSQL + " 				 FROM ClientSatVersion " + Environment.NewLine;
            strSQL = strSQL + " 					INNER JOIN Version ON Version.Ver_NRI = ClientSatVersion.Ver_NRI " + Environment.NewLine;
            strSQL = strSQL + " 					INNER JOIN (SELECT MAX(ClientAppVersion.CAV_DtInstalledProd) As DtInstalled, ClientAppVersion.Ver_NRI, ClientAppVersion.CeC_NRI FROM ClientAppVersion GROUP BY ClientAppVersion.Ver_NRI, ClientAppVersion.CeC_NRI) AS TCAV ON TCAV.Ver_NRI = Version.Ver_NRI AND TCAV.CeC_NRI = ClientSatVersion.CeC_NRI " + Environment.NewLine;

            strSQL = strSQL + " 					OUTER APPLY (SELECT TOP 1 Revision.Rev_No " + Environment.NewLine;
            strSQL = strSQL + " 								 FROM Revision " + Environment.NewLine;
            strSQL = strSQL + " 									INNER JOIN SatRevision ON SatRevision.Rev_NRI = Revision.Rev_NRI " + Environment.NewLine;
            strSQL = strSQL + " 								 WHERE SatRevision.CSA_NRI = ClientSatVersion.CSA_NRI " + Environment.NewLine;
            strSQL = strSQL + " 								   AND Revision.Ver_NRI = Version.Ver_NRI " + Environment.NewLine;
            strSQL = strSQL + " 								   AND Revision.Rev_PreparationMode = 0 " + Environment.NewLine;
            strSQL = strSQL + " 								 ORDER BY Revision.Rev_No DESC " + Environment.NewLine;
            strSQL = strSQL + " 								) AS TRef " + Environment.NewLine;
								
            strSQL = strSQL + " 				 WHERE ClientSatVersion.CSA_NRI = CerSatApp.CSA_NRI " + Environment.NewLine;
            strSQL = strSQL + " 				   AND ClientSatVersion.CeC_NRI = @CerClient_NRI " + Environment.NewLine;
            strSQL = strSQL + " 				   AND TCAV.DtInstalled IS NOT NULL " + Environment.NewLine;

            //strSQL = strSQL + " 				 ORDER BY ClientSatVersion.CSA_NRI DESC, Version.Ver_No DESC " + Environment.NewLine;
            strSQL = strSQL + " 				) AS TVer " + Environment.NewLine;
				
            strSQL = strSQL + " GROUP BY CerSatApp.CSA_NRI, CerSatApp.CSA_Name " + Environment.NewLine;

            strSQL = strSQL + "    ) AS T1 " + Environment.NewLine;
            strSQL = strSQL + " ORDER BY T1.CeA_NRI DESC, T1.SEQ, T1.Ver_No DESC " + Environment.NewLine;

            return strSQL;
        }

#endregion


    }
}
