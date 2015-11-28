using System;
using System.Collections;
using System.Data.SqlClient;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.TT3LightDLL.Classes
{

    public class clsSQL
    {

        //Private members
        private bool mblnTransactionStarted;
        private SqlCommand mcSQLCmd;
        private SqlTransaction mcSQLTransaction;
        private sclsConstants.DML_Mode _intDMLCommand;

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

        public bool blnTransactionStarted
        {
            get { return mblnTransactionStarted; }
        }

        public sclsConstants.DML_Mode DLMCommand
        {
            get { return _intDMLCommand; }
            set { _intDMLCommand = value; }
        }

#endregion


#region "Constructor"

        public clsSQL()
        {

            mcSQLCmd = new SqlCommand();

            mColFields = new System.Collections.Specialized.StringDictionary();
        }

#endregion


#region "Shared Functions / Subs"

        public static SqlDataReader ADOSelect(string vstrSQL) 
        {

        SqlCommand mySQLCmd = null;
        SqlDataReader mySQLReader = null;
        
        try 
        {
            mySQLCmd = new SqlCommand(vstrSQL, clsApp.GetAppController.SQLConnection);

            mySQLReader = mySQLCmd.ExecuteReader();

        }
        catch (Exception ex)
        {
            sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
        }
        finally
        {
            if ((mySQLCmd != null))
            {
                mySQLCmd.Dispose();
            }
        }

        return mySQLReader;
        }

        public bool bln_ADOExecute(string vstrSQL)
        {
            bool blnValidReturn = false;
            //SqlCommand mySQLCmd = null;
            //SqlDataReader mySQLReader = null;

            try
            {
                mcSQLCmd.CommandText = vstrSQL; //= new SqlCommand(vstrSQL, clsApp.GetAppController.SQLConnection);

                //mySQLReader = mcSQLCmd.ExecuteReader();
                mcSQLCmd.ExecuteNonQuery();

                blnValidReturn = true;

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                //if ((mySQLCmd != null))
                //{
                //    mySQLCmd.Dispose();
                //}
            }

            return blnValidReturn;
        }

        public static string str_ADOSingleLookUp(string vstrField, string vstrTable, string vstrWhere)
        {
            string strReturnValue = string.Empty;
            SqlCommand mySQLCmd = null;
            SqlDataReader mySQLReader = null;
            string strSQL = string.Empty;

            try
            {
                strSQL = "SELECT " + vstrField + " AS " + clsApp.GetAppController.str_FixStringForSQL(vstrField) + " FROM " + vstrTable + " WHERE " + vstrWhere;

                mySQLCmd = new SqlCommand(strSQL, clsApp.GetAppController.SQLConnection);

                mySQLReader = mySQLCmd.ExecuteReader();

                if (mySQLReader.Read())
                {
                    strReturnValue = mySQLReader[vstrField].ToString();
                }

                mySQLCmd.Dispose();

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if ((mySQLReader != null))
                {
                    mySQLReader.Dispose();
                }
            }

            return strReturnValue;
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
                mcSQLTransaction = clsApp.GetAppController.SQLConnection.BeginTransaction(System.Data.IsolationLevel.Serializable);
                mblnTransactionStarted = true;
                mcSQLCmd.Transaction = mcSQLTransaction;
                mcSQLCmd.Connection = clsApp.GetAppController.SQLConnection;
                mcSQLCmd.CommandType = System.Data.CommandType.Text;

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
                if (vblnCommitChanges)
                {
                    mcSQLTransaction.Commit();
                }
                else
                {
                    mcSQLTransaction.Rollback();
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
                mblnTransactionStarted = false;
                mcSQLTransaction.Dispose();
                mcSQLCmd.Dispose();
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
                        case clsSQL.MySQL_FieldTypes.VARCHAR_TYPE:
                            vstrValue = clsApp.GetAppController.str_FixStringForSQL(vstrValue);

                            break;
                        case clsSQL.MySQL_FieldTypes.DATETIME_TYPE:
                            vstrValue = String.Format(clsApp.GetAppController.str_GetServerDateTimeFormat, Convert.ToDateTime(vstrValue));
                            vstrValue = clsApp.GetAppController.str_FixStringForSQL(vstrValue);

                            break;
                        case clsSQL.MySQL_FieldTypes.DECIMAL_TYPE:
                        case clsSQL.MySQL_FieldTypes.INT_TYPE:
                            vstrValue = vstrValue.ToString();

                            break;
                        case clsSQL.MySQL_FieldTypes.NRI_TYPE:
                            if (vstrValue == "0")
                            {
                                vstrValue = "NULL";
                            }
                            else
                            {
                                vstrValue = vstrValue.ToString();
                            }

                            break;
                        case clsSQL.MySQL_FieldTypes.BIT_TYPE:
                            if (vstrValue == true.ToString())
                            {
                                vstrValue = "1";
                            }
                            else if (vstrValue == false.ToString())
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

                mColFields.Add(vstrField, vstrValue);

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
                    //mColFields.Item(strKey).ToString()()

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

        public static bool bln_CheckReferenceIntegrity(string vstrForeignTableName, string vstrForeignKeyName, int vintForeignKeyValue)
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
                clsApp.GetAppController.ShowMessage((int) sclsConstants.Error_Message.ERROR_ITEM_USED_MSG);
            }

            return blnValidReturn;

        }

#endregion

    }

}