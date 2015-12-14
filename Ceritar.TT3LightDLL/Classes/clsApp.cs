﻿using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using System;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Ceritar.TT3LightDLL.Classes
{

    public sealed class clsApp
    {

        //Private class members
        private SqlConnection mcMySQLConnection;
        private clsUser mctrlUser;
        private System.Text.RegularExpressions.Regex mcStringCleaner = new System.Text.RegularExpressions.Regex("'", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.CultureInvariant | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        private static clsApp _myUniqueInstance;


        public static clsApp GetAppController
        {
            get
            {
                if (_myUniqueInstance == null)
                {
                    _myUniqueInstance = new clsApp();
                }

                return _myUniqueInstance;
            }
        }


#region "Properties"

        public SqlConnection SQLConnection
        {
            get { return mcMySQLConnection; }
        }

        public clsUser cUser
        {
            get { return this.mctrlUser; }
        }

        public string str_GetUserDateFormat
        {
            get
            {
                switch (mctrlUser.GetUserLanguage)
                {
                    case (short)sclsConstants.Language.FRENCH_QC:
                        // Return "dd-MM-yyyy"

                        return "yyyy-MM-dd";

                    case (short)sclsConstants.Language.ENGLISH_CA:
                        // Return "MM-dd-yyyy"

                        return "yyyy-MM-dd";

                    default:
                        return System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
                }
            }
        }

        public string str_GetUserDateTimeFormat
        {
            get
            {
                switch (mctrlUser.GetUserLanguage)
                {
                    case (short)sclsConstants.Language.FRENCH_QC:
                        //Return "dd-MM-yyyy HH:mm:ss"

                        return "yyyy-MM-dd HH:mm:ss";
                    case (short)sclsConstants.Language.ENGLISH_CA:
                        //Return "MM-dd-yyyy HH:mm:ss"

                        return "yyyy-MM-dd HH:mm:ss";
                    default:
                        return System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern + " " + System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern;
                }
            }
        }

        public string str_GetServerTimeFormat
        {
            get { return "HH:mm:ss"; }
        }

        public string str_GetServerDateFormat
        {
            get { return "yyyy-MM-dd"; }
        }

        public string str_GetServerDateTimeFormat
        {
            get { return "yyyy-MM-dd HH:mm:ss"; }
        }

#endregion


#region "Constructors"


        private clsApp()
        {
            mctrlUser = new clsUser();

            blnOpenSQLServerConnection();
        }

#endregion


#region "Functions / Subs"

        private bool blnOpenSQLServerConnection()
        {
            bool blnValidReturn = false;

            try
            {
                //mcMySQLConnection = new SqlConnection(@"Persist Security Info=False;User ID=sa;Password=sa;Initial Catalog=Logirack_CVS_Dev;Data Source=localhost\SVR_SQL");
                mcMySQLConnection = new SqlConnection(@"Persist Security Info=False;
                                                        User ID=sa;
                                                        Password=sa;
                                                        Initial Catalog=Logirack_CVS_Dev;
                                                        Data Source=24.200.162.199\SVR_SQL;
                                                        MultipleActiveResultSets=True");
                
                mcMySQLConnection.Open();

                //MultipleActiveResultSets=true        

                blnValidReturn = true;
            }
            catch (SqlException ex)
            {
                blnValidReturn = false;

                MessageBox.Show("La connexion au serveur a échouée.");

                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
                mcMySQLConnection.Dispose();
                
#if Debug
			    Application.Exit();
#endif
            }
            finally
            {
                //if (mcMySQLConnection != null) mcMySQLConnection.Dispose();
            }

            return blnValidReturn;
        }

        public bool bln_CTLBindCaption(ref Control rControl)
        {
            bool blnValidReturn = false;
            string strCaption = string.Empty;

            try
            {

                clsSQL.str_ADOSingleLookUp("ApC_Text", "AppCaption", "ApC_ID = " + Convert.ToString(rControl.Tag));

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        public string str_GetCaption(int intCaptionID, short intLanguage)
        {
            string strCaption = string.Empty;

            try
            {
                strCaption = clsSQL.str_ADOSingleLookUp("ApC_Text", "AppCaption", "ApC_No = " + intCaptionID.ToString() + " AND ApL_NRI = " + intLanguage);

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return strCaption;
        }

        public string str_FixStringForSQL(string vstrStringToFix)
        {
            return "'" + mcStringCleaner.Replace(vstrStringToFix, "''") + "'";
        }

        public string str_FixDateForSQL(string vstrDateToFix)
        {
            return str_FixStringForSQL(String.Format(str_GetServerDateTimeFormat, Convert.ToDateTime(vstrDateToFix)));
        }

        public System.DateTime str_SetDateToMidnightServerFormat(string vdtDateToSet)
        {
            return Convert.ToDateTime(String.Format(str_GetServerDateFormat, Convert.ToDateTime(String.Format(str_GetServerDateTimeFormat, Convert.ToDateTime(vdtDateToSet)) + " 00:00:00")));
        }

        public System.DateTime GetFormatedDate(string vdtToFormat)
        {
            return DateTime.ParseExact(vdtToFormat, str_GetUserDateFormat, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void ShowMessage(int vintCaption_NRI, MessageBoxButtons vmsgType = MessageBoxButtons.OK, params string[] vlstMsgParam)
        {
            string strMessage = string.Empty;
            int intParamCpt = 1;

            try
            {
                strMessage = _myUniqueInstance.str_GetCaption(vintCaption_NRI, mctrlUser.GetUserLanguage);

                if ((vlstMsgParam != null))
                {

                    foreach (string strParam in vlstMsgParam)
                    {

                        if (strMessage.Contains("@" + intParamCpt.ToString()))
                        {
                            strMessage = strMessage.Replace("@" + intParamCpt.ToString(), strParam);

                            intParamCpt += 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                MessageBox.Show(strMessage, "Message", vmsgType);

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        public T ConvertToEnum<T>(object vobjValue)
        {
            T enumVal = (T)Enum.Parse(typeof(T), vobjValue.ToString());

            return enumVal;
        }

        public bool blnCopyFolderContent(string vstrSourceFolderPath, string vstrDestinationFolderPath, bool vblnOverwrite = true, bool vblnCreateFolderIfNotExist = false)
        {
            bool blnValidReturn = false;
            string strFileDestinationPath = string.Empty;

            try
            {
                string[] lstReleaseFiles = Directory.GetFiles(vstrSourceFolderPath);

                foreach (string strCurrentFile in lstReleaseFiles)
                {
                    if (vblnCreateFolderIfNotExist && !Directory.Exists(vstrDestinationFolderPath)) Directory.CreateDirectory(vstrDestinationFolderPath);

                    strFileDestinationPath = Path.Combine(vstrDestinationFolderPath, Path.GetFileName(strCurrentFile));

                    File.Copy(strCurrentFile, strFileDestinationPath, vblnOverwrite);
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

#endregion


    }

}