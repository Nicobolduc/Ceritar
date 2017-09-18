using System;
using CrystalDecisions.CrystalReports.Engine;
using Ceritar.TT3LightDLL.Static_Classes;
using System.Data.SqlClient;

namespace Ceritar.TT3LightDLL.Classes
{
    public class clsCrystalReportHelper
    {
        public  bool blnSetReportToCurrentConnection(ReportDocument rcRptDoc)
        {
            bool blnValidReturn = false;

            try
            {
                CrystalDecisions.Shared.TableLogOnInfo tableLogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();
                CrystalDecisions.Shared.ConnectionInfo crConnectionInfo = new CrystalDecisions.Shared.ConnectionInfo();
                SqlConnectionStringBuilder appConnection = new SqlConnectionStringBuilder(clsTTApp.GetAppController.SQLConnection.ConnectionString);

                crConnectionInfo.ServerName = clsTTApp.GetAppController.SQLConnection.DataSource;
                crConnectionInfo.DatabaseName = clsTTApp.GetAppController.SQLConnection.Database;
                crConnectionInfo.UserID = appConnection.UserID;
                crConnectionInfo.Password = appConnection.Password;

                rcRptDoc.SetDatabaseLogon(appConnection.UserID, appConnection.Password, clsTTApp.GetAppController.SQLConnection.DataSource, clsTTApp.GetAppController.SQLConnection.Database, true);

                foreach (Table table in rcRptDoc.Database.Tables)
                {
                    tableLogoninfo = table.LogOnInfo;
                    tableLogoninfo.ConnectionInfo = crConnectionInfo;
                    table.ApplyLogOnInfo(tableLogoninfo);
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }
    }
}
