using System;
using System.Data.SqlClient;
using System.IO;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Controllers
{
    public class ctr_OutilCeritar
    {
        private clsActionResults mcActionResult;
        //private clsTTSQL mcSQL;


        public clsActionResults GenerateTTAppScript(int vintCeA_NRI, string vstrExportLocation)
        {
            bool blnValidReturn = false;
            SqlDataReader sqlRecord = null;
            string connectionString = "";

            try
            {
                mcActionResult = new clsActionResults();

                sqlRecord = clsTTSQL.ADOSelect("SELECT CeA_DevServer, CeA_DevDatabase FROM CerApp WHERE CeA_NRI = " + vintCeA_NRI.ToString());

                if (sqlRecord.Read())
                {
                    connectionString = "Persist Security Info=true;User ID=sa;Password=*8059%Ce;Initial Catalog=" + sqlRecord["CeA_DevDatabase"].ToString() + @";Data Source=" + sqlRecord["CeA_DevServer"].ToString();
                }

                if (connectionString != string.Empty)
                {
                    sqlRecord.Close();

                    using (SqlConnection cSQLDev = new SqlConnection(connectionString))
                    {
                        cSQLDev.Open();

                        using (SqlCommand cSqlCmd = new SqlCommand("EXEC sp_ScriptTTAppData", cSQLDev))
                        {
                            sqlRecord = cSqlCmd.ExecuteReader();

                            if (File.Exists(vstrExportLocation)) File.Delete(vstrExportLocation);

                            using (StreamWriter writer = new StreamWriter(vstrExportLocation, false, System.Text.Encoding.Unicode))
                            {
                                while (sqlRecord.Read())
                                {
                                    writer.WriteLine(sqlRecord[0].ToString().Trim());
                                }
                            }
                            mcActionResult.SetValid();
                            blnValidReturn = true;
                        }
                    }
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

                if (!sqlRecord.IsClosed) sqlRecord.Close();
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

    #endregion
    }
}
