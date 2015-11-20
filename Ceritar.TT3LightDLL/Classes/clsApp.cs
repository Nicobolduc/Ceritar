﻿using Ceritar.CVS.Controllers;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Ceritar.TT3LightDLL.Classes
{

    public sealed class clsApp
    {

        //Private class members
        private SqlConnection mcMySQLConnection;
        private ctrl_User mctrlUser;
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

        public SqlConnection MySQLConnection
        {
            get { return mcMySQLConnection; }
        }

        public ctrl_User cUser
        {
            get { return this.mctrlUser; }
        }

        public string str_GetUserDateFormat
        {
            get
            {
                switch (mctrlUser.GetUserLangage)
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
                switch (mctrlUser.GetUserLangage)
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
            mctrlUser = new ctrl_User();

            blnOpenMySQLConnection();
        }

#endregion


#region "Functions / Subs"

        private bool blnOpenMySQLConnection()
        {
            bool blnValidReturn = false;

            try
            {
                mcMySQLConnection = new SqlConnection(@"Persist Security Info=False;User ID=sa;Password=*8059%Ce;Initial Catalog=Logirack_CVS_Dev;Data Source=localhost\SVR_SQL");
                
                mcMySQLConnection.Open();

                //MultipleActiveResultSets=true        

                blnValidReturn = true;
            }
            catch (SqlException ex)
            {
                blnValidReturn = false;

                MessageBox.Show("La connexion au serveur a échouée.");

                sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
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
                sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }

            return blnValidReturn;
        }

        public string str_GetCaption(int intCaptionID, short intLanguage)
        {
            string strCaption = string.Empty;

            try
            {
                strCaption = clsSQL.str_ADOSingleLookUp("ApC_Text", "AppCaption", "ApC_No = " + intCaptionID.ToString() + " AND ApL_ID = " + intLanguage);

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
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

        public void ShowMessage(int vintCaption_ID, MessageBoxButtons vmsgType = MessageBoxButtons.OK, List<string> vlstMsgParam = null)
        {
            string strMessage = string.Empty;
            int intParamCpt = 1;

            try
            {
                strMessage = _myUniqueInstance.str_GetCaption(vintCaption_ID, mctrlUser.GetUserLangage);

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
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }

                MessageBox.Show(strMessage, "Message", vmsgType);

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        public void DisableAllFormControls(Form rForm, TabPage rTabPage, Control rControl)
        {
            System.Windows.Forms.Control.ControlCollection controlCollection = default(System.Windows.Forms.Control.ControlCollection);

            try
            {
                if ((rControl != null))
                {
                    controlCollection = rControl.Controls;
                }
                else
                {
                    controlCollection = rForm.Controls;
                }

                foreach (Control objControl in controlCollection)
                {
                    switch (objControl.GetType().Name)
                    {
                        case "Button":
                        case "TextBox":
                        case "CheckBox":
                        case "RadioButton":
                        case "DateTimePicker":
                        case "ListView":
                        case "ComboBox":
                            objControl.Enabled = false;

                            break;

                        case "GroupBox":
                            DisableAllFormControls(null, null, objControl);

                            break;

                        case "DataGridView":
                            ((DataGridView)objControl).ReadOnly = true;

                            break;

                        //case "GridControl": //TODO
                        //    ((GridControl)objControl).BrowseOnly = true;

                        //break;

                        case "TabControl":
                            foreach (TabPage tp in ((TabControl)objControl).TabPages)
                            {
                                DisableAllFormControls(null, tp, null);
                            }

                            break;

                        default:
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        public void EmptyAllFormControls(System.Windows.Forms.Form rForm = null, TabPage rTabPage = null, Control rControl = null)
        {
            System.Windows.Forms.Control.ControlCollection controlCollection = default(System.Windows.Forms.Control.ControlCollection);

            try
            {
                if ((rControl != null))
                {

                    controlCollection = rControl.Controls;
                }
                else
                {
                    controlCollection = rForm.Controls;
                }

                foreach (Control objControl in controlCollection)
                {
                    switch (objControl.GetType().Name)
                    {
                        case "TextBox":
                            objControl.Text = string.Empty;

                            break;

                        case "CheckBox":
                            ((CheckBox)objControl).Checked = false;

                            break;

                        case "RadioButton":
                            ((RadioButton)objControl).Checked = false;

                            break;

                        case "ComboBox":
                            ((ComboBox)objControl).DataSource = null;
                            ((ComboBox)objControl).Items.Clear();

                            break;

                        case "GroupBox":
                            EmptyAllFormControls(null, null, objControl);

                            break;

                        case "TabControl":
                            foreach (TabPage tp in ((TabControl)objControl).TabPages)
                            {
                                EmptyAllFormControls(null, tp);
                            }

                            break;

                        default:
                            break;

                    }
                }

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
        }

#endregion


    }

}