using System;
using System.Collections;
using System.Data.SqlClient;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using System.Configuration;

namespace Ceritar.TT3LightDLL.Classes
{
    /// <summary>
    /// Cette classe offre toutes les fonctionnalités nécessaires pour interagir avec la base de données. 
    /// Notamment la gestion des transactions et des opérations de select, insert, update et delete.
    /// </summary>
    public sealed class clsTTSQL
    {

        //Private members
        private SqlConnection mcSQLConnection;
        private SqlCommand mcSQLCmd;
        private SqlTransaction mcSQLTransaction;
        private sclsConstants.DML_Mode _intDMLCommand;
        private bool mblnSilentMessage;
        private System.Collections.Specialized.StringDictionary mColFields;


        public enum MySQL_FieldTypes
        {
            BIT_TYPE = 0,
            INT_TYPE = 1,
            DECIMAL_TYPE = 2,
            VARCHAR_TYPE = 3,
            DATETIME_TYPE = 4,
            NRI_TYPE = 5
        }


#region "Properties"

        public bool TransactionStarted
        {
            get { return mcSQLTransaction == null; }
        }

        public sclsConstants.DML_Mode DLMCommand
        {
            get { return _intDMLCommand; }
            set { _intDMLCommand = value; }
        }

        public bool SilentMessage
        {
            get { return mblnSilentMessage; }
            set { mblnSilentMessage = value; }
        }

        public SqlConnection GetSQLConnection
        {
            get { return mcSQLConnection; }
        }

#endregion


#region "Constructor"

        public clsTTSQL()
        {
            mColFields = new System.Collections.Specialized.StringDictionary();

            OpenSQLServerConnection(ref mcSQLConnection);

            mcSQLCmd = new SqlCommand();
            mcSQLCmd.Connection = mcSQLConnection;
            mcSQLCmd.CommandType = System.Data.CommandType.Text;
        }

#endregion


#region "Shared Functions / Subs"

        public static SqlDataReader ADOSelect(string vstrSQL) 
        {
            SqlCommand cSQLCmd = null;
            SqlDataReader cSQLReader = null;
        
            try 
            {
                cSQLCmd = new SqlCommand(vstrSQL, clsTTApp.GetAppController.SQLConnection);

                cSQLReader = cSQLCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return cSQLReader;
        }

        public static string str_ADOSingleLookUp(string vstrField, string vstrTable, string vstrWhere)
        {
            string strReturnValue = string.Empty;
            SqlCommand cSQLCmd = null;
            SqlDataReader cSQLReader = null;
            string strSQL = string.Empty;

            try
            {
                strSQL = "SELECT " + vstrField + " AS " + clsTTApp.GetAppController.str_FixStringForSQL(vstrField) + " FROM " + vstrTable + " WHERE " + vstrWhere;

                cSQLCmd = new SqlCommand(strSQL, clsTTApp.GetAppController.SQLConnection);

                cSQLReader = cSQLCmd.ExecuteReader();

                if (cSQLReader.Read())
                {
                    strReturnValue = cSQLReader[vstrField].ToString();
                }
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if ((cSQLReader != null)) cSQLReader.Dispose();
            }

            return strReturnValue;
        }

        public static bool bln_ADOValid_TS(string vstrTableName, string vstrNRI_FieldName, int vintItem_NRI, string vstrTS_FieldName, int vintItem_TS)
        {
            bool blnValidReturn = false;
            //SqlCommand cSQLCmd = null;
            SqlDataReader cSQLReader = null;

            try
            {

                blnValidReturn = true;

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if ((cSQLReader != null)) cSQLReader.Dispose();
            }

            return blnValidReturn;
        }

#endregion


#region "Functions / Subs"

        public bool bln_RefreshFields()
        {

            bool blnValidReturn = true;

            mColFields = new System.Collections.Specialized.StringDictionary();

            return blnValidReturn;
        }

        public bool bln_BeginTransaction()
        {
            bool blnValidReturn = false;

            try
            {
                mcSQLTransaction = mcSQLConnection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                mcSQLCmd.Transaction = mcSQLTransaction;

                blnValidReturn = true;
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        public bool bln_EndTransaction(bool vblnCommitChanges)
        {
            bool blnValidReturn = false;

            try
            {
                if (mcSQLTransaction != null)
                {
                    if (vblnCommitChanges)
                    {
                        mcSQLTransaction.Commit();
                    }
                    else
                    {
                        mcSQLTransaction.Rollback();
                    }
                }

                blnValidReturn = true;

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (mcSQLTransaction != null) mcSQLTransaction.Dispose();
                if (mcSQLCmd != null) mcSQLCmd.Dispose();

                mcSQLTransaction = null;
            }

            return blnValidReturn;
        }

        public bool bln_AddField(string vstrField, object vobjValue, MySQL_FieldTypes vintDBType)
        {
            bool blnValidReturn = true;
            string vstrValue = string.Empty;

            try
            {
                if (vobjValue == null | vobjValue == System.DBNull.Value)
                {
                    vstrValue = string.Empty;
                }
                else
                {
                    vstrValue = vobjValue.ToString();
                }

                if (string.IsNullOrEmpty(vstrValue))
                {
                    vstrValue = "NULL";
                }
                else
                {
                    switch (vintDBType)
                    {
                        case clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE:
                            vstrValue = clsTTApp.GetAppController.str_FixStringForSQL(vstrValue);

                            break;

                        case clsTTSQL.MySQL_FieldTypes.DATETIME_TYPE:
                            vstrValue = clsTTApp.GetAppController.str_FixStringForSQL(vstrValue);

                            break;

                        case clsTTSQL.MySQL_FieldTypes.DECIMAL_TYPE:
                        case clsTTSQL.MySQL_FieldTypes.INT_TYPE:
                            vstrValue = vstrValue.ToString();

                            break;

                        case clsTTSQL.MySQL_FieldTypes.NRI_TYPE:
                            if (vstrValue == "0")
                            {
                                vstrValue = "NULL";
                            }
                            else
                            {
                                vstrValue = vstrValue.ToString();
                            }

                            break;

                        case clsTTSQL.MySQL_FieldTypes.BIT_TYPE:
                            if (vstrValue == true.ToString() || vstrValue == "1")
                            {
                                vstrValue = "1";
                            }
                            else if (vstrValue == false.ToString() || vstrValue == "0")
                            {
                                vstrValue = "0";
                            }
                            else
                            {
                                blnValidReturn = false;
                            }

                            break;
                    }
                }

                if (blnValidReturn)
                {
                    mColFields.Add(vstrField, vstrValue);
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        public bool bln_ADOInsert(string vstrTable, out int rintNewItem_ID)
        {
            bool blnValidReturn = false;
            string strSQL = string.Empty;
            string strFields = string.Empty;
            string strValues = string.Empty;

            rintNewItem_ID = 0;

            strSQL = " INSERT INTO " + vstrTable + Environment.NewLine;

            try
            {

                foreach (string strKey in mColFields.Keys)
                {
                    strFields = strFields + Convert.ToString((strFields == string.Empty ? " (" : ",")) + strKey;

                    strValues = strValues + Convert.ToString((strValues == string.Empty ? " VALUES (" : ",")) + mColFields[strKey].ToString();
                }

                strFields = strFields + ") " + Environment.NewLine;

                strValues = strValues + ") " + Environment.NewLine;

                mcSQLCmd.CommandText = strSQL + strFields + strValues;

                mcSQLCmd.ExecuteNonQuery();

                mcSQLCmd.CommandText = "SELECT @@IDENTITY";

                Int32.TryParse(mcSQLCmd.ExecuteScalar().ToString(), out rintNewItem_ID);

                mColFields.Clear();

                blnValidReturn = true;

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        public bool bln_ADOUpdate(string vstrTable, string vstrWhere)
        {
            bool blnValidReturn = false;
            string strSQL = string.Empty;
            string strFields = string.Empty;

            strSQL = "          UPDATE " + vstrTable + Environment.NewLine;
            strSQL = strSQL + " SET ";

            try
            {
                foreach (string strKey in mColFields.Keys)
                {
                    strFields = strFields + Convert.ToString((strFields == string.Empty ? string.Empty : ",")) + strKey.ToString() + "=" + mColFields[strKey].ToString();
                }

                mcSQLCmd.CommandText = strSQL + strFields + " WHERE " + vstrWhere;

                mcSQLCmd.ExecuteNonQuery();

                mColFields.Clear();

                blnValidReturn = true;

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                mColFields.Clear();
            }

            return blnValidReturn;
        }

        public bool bln_ADODelete(string vstrTable, string vstrWhere)
        {
            bool blnValidReturn = false;
            string strSQL = string.Empty;

            strSQL = " DELETE FROM " + vstrTable + Environment.NewLine;

            try
            {
                mcSQLCmd.CommandText = strSQL + " WHERE " + vstrWhere;

                mcSQLCmd.ExecuteNonQuery();

                mColFields.Clear();

                blnValidReturn = true;

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        public SqlDataReader ADOSelect_Trans(string vstrSQL)
        {
            SqlDataReader cSQLReader = null;

            try
            {
                mcSQLCmd.CommandText = vstrSQL;

                cSQLReader = mcSQLCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return cSQLReader;
        }

        public bool bln_ADOExecute(string vstrSQL)
        {
            bool blnValidReturn = false;

            try
            {
                mcSQLCmd.CommandText = vstrSQL;

                mcSQLCmd.ExecuteNonQuery();

                blnValidReturn = true;

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        public string str_ADOSingleLookUp_Trans(string vstrField, string vstrTable, string vstrWhere)
        {
            SqlDataReader cSQLReader = null;
            string strReturnValue = string.Empty;
            string strSQL = string.Empty;

            try
            {
                strSQL = "SELECT " + vstrField + " AS " + clsTTApp.GetAppController.str_FixStringForSQL(vstrField) + " FROM " + vstrTable + " WHERE " + vstrWhere;

                mcSQLCmd.CommandText = strSQL;

                cSQLReader = mcSQLCmd.ExecuteReader();

                if (cSQLReader.Read())
                {
                    strReturnValue = cSQLReader[vstrField].ToString();
                }
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if ((cSQLReader != null)) cSQLReader.Dispose();
            }

            return strReturnValue;
        }

        public static bool bln_CheckReferenceIntegrity(string vstrForeignTableName, string vstrForeignKeyName, int vintForeignKeyValue, params string[] vlstTablesToIgnore)
        {
            bool blnValidReturn = false;
            string strSQL = string.Empty;
            //clsSQL cSQL = new clsSQL();

            try
            {
                //TODO
                //cSQL.bln_BeginTransaction();

                //strSQL = "DELETE FROM " + vstrForeignTableName + " WHERE " + vstrForeignKeyName + " = " + vintForeignKeyValue;

                //blnValidReturn = cSQL.bln_ADOExecute(strSQL);
                blnValidReturn = true;

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                //cSQL.bln_EndTransaction(false);
            }

            if (!blnValidReturn)
            {
                clsTTApp.GetAppController.ShowMessage((int) sclsConstants.Validation_Message.INVALID_REFERENCE_INTEGRITY);
            }

            return blnValidReturn;

        }

        internal static void OpenSQLServerConnection(ref SqlConnection rcSQLConnection)
        {
            try
            {
                string strDatabase = ConfigurationManager.AppSettings["Database"];
                string strServer = @ConfigurationManager.AppSettings["Server"];
                //OSQL -S BOLDUC-PC\SVR_SQL -E 
                //sp_password NULL, '1234', 'sa' GO
                //rcSQLConnection = new SqlConnection(@"Persist Security Info=true;
                //                                      User ID=ltuser;
                //                                      Password=ltuser;
                //                                      Initial Catalog=Logirack_CVS;
                //                                      Data Source=SVR-SQL14;
                //                                      MultipleActiveResultSets=True");

                rcSQLConnection = new SqlConnection(@"Persist Security Info=true;User ID=sa;Password=Ce*8059%Ce;Initial Catalog=" + strDatabase + @";Data Source=" + strServer + ";MultipleActiveResultSets=True");

                //rcSQLConnection = new SqlConnection(@"Persist Security Info=true;User ID=sa;Password=*8059%Ce;Initial Catalog=Logirack_CVS;Data Source=localhost\SVR_SQL16;MultipleActiveResultSets=True");
                //                rcSQLConnection = new SqlConnection(@"Persist Security Info=False;
                //                                                                        User ID=sa;
                //                                                                        Password=1234;
                //                                                                        Initial Catalog=Logirack_CVS_Dev;
                //                                                                        Data Source=24.200.162.199\SVR_SQL;
                //                                                                        MultipleActiveResultSets=True");

                rcSQLConnection.Open();
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("La connexion au serveur a échouée.");

                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
                rcSQLConnection.Dispose();

#if ! Debug
                System.Windows.Forms.Application.Exit();
#endif
            }


#endregion


        }
    }
}