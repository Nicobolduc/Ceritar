using System;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Classes;
using System.Windows.Forms;

namespace Ceritar.TT3LightDLL.Static_Classes
{
    /// <summary>
    /// Cette classe statique contient des fonctions génériques utilisables pour toutes les Forms de l'application.
    /// </summary>
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

        public static void DisableAllFormControls(Form rForm, TabPage rTabPage, Control rControl)
        {
            Control.ControlCollection controlCollection = default(Control.ControlCollection);

            try
            {
                if ((rControl != null))
                {
                    controlCollection = rControl.Controls;
                }
                else if (rTabPage != null)
                {
                    controlCollection = rTabPage.Controls;
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
                        case "CheckBox":
                        case "RadioButton":
                        case "DateTimePicker":
                        case "ListView":
                        case "ComboBox":
                            objControl.Enabled = false;

                            break;

                        case "TextBox":
                            ((TextBox)objControl).ReadOnly = true;

                            break;

                        case "GroupBox":
                            DisableAllFormControls(null, null, objControl);

                            break;

                        case "DataGridView":
                            ((DataGridView)objControl).ReadOnly = true;

                            break;

                        //case "C1FlexGrid":
                        //    ((C1.Win.C1FlexGrid.C1FlexGrid) objControl).Enabled = false;

                        //    break;

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
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        public static void EmptyAllFormControls(System.Windows.Forms.Form rForm = null, TabPage rTabPage = null, Control rControl = null)
        {
            System.Windows.Forms.Control.ControlCollection controlCollection = default(System.Windows.Forms.Control.ControlCollection);

            try
            {
                if ((rControl != null))
                {

                    controlCollection = rControl.Controls;
                }
                else if (rTabPage != null)
                {
                    controlCollection = rTabPage.Controls;
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
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }
    }
}