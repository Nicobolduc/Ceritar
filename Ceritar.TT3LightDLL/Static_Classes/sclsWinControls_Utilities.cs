using System;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Classes;
using System.Windows.Forms;

namespace Ceritar.TT3LightDLL.Static_Classes
{
    public static class sclsWinControls_Utilities
    {

        public static bool blnComboBox_LoadFromSQL(string vstrSQL, string vstrValueMember, string vstrDisplayMember, bool vblnAllowEmpty, ref ComboBox rcboToLoad)
        {
            bool blnValidReturn = false;
            SqlCommand mySQLCmd = default(SqlCommand);
            SqlDataReader mySQLReader = null;
            BindingList<KeyValuePair<int, string>> myBindingList = new BindingList<KeyValuePair<int, string>>();

            try
            {
                rcboToLoad.DataSource = null;

                mySQLCmd = new SqlCommand(vstrSQL, clsApp.GetAppController.SQLConnection);

                mySQLReader = mySQLCmd.ExecuteReader();

                if (vblnAllowEmpty)
                {
                    myBindingList.Add(new KeyValuePair<int, string>(0, ""));
                }

                while (mySQLReader.Read())
                {
                    if (mySQLReader[vstrValueMember] != DBNull.Value)
                    {
                        myBindingList.Add(new KeyValuePair<int, string>((Int32) mySQLReader[vstrValueMember], Convert.ToString(mySQLReader[vstrDisplayMember])));
                    }
                }
                
                rcboToLoad.DataSource = myBindingList;
                rcboToLoad.ValueMember = "Key";
                rcboToLoad.DisplayMember = "Value";
                rcboToLoad.SelectedIndex = Convert.ToInt32((mySQLReader.HasRows ? 0 : -1));

                blnValidReturn = true;

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
            }
            finally
            {
                if ((mySQLReader != null))
                {
                    mySQLReader.Dispose();
                }
            }

            return blnValidReturn;
        }

    }

}