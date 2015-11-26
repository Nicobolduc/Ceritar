using System;
using System.Windows.Forms;
using Ceritar.Logirack_CVS;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.Logirack_CVS
{
    public static class sclsGenList
    {

        //Enums
        public enum GeneralLists_ID
        {
            EXPENSE_LIST_ID = 1
        }

        public enum GeneralList_GridCapID
        {
            EXPENSE_CAP = 1
        }


        #region "Functions / Subs"

        public static void ShowGenList(GeneralLists_ID vList_ID)
        {
            string strListGenTitle = string.Empty;
            string strSQL = string.Empty;
            
            try
            {
                frmGenericList frmGenList = new frmGenericList(vList_ID);
               
                switch (vList_ID)
                {
                    //case GeneralLists_ID.EXPENSE_LIST_ID:
                    //    strSQL = strGetExpenseList_SQL();
                    //    strListGenTitle = " - Dépenses";
                    //    //TODO caption pour ca
                    //    frmGenList.mintGridTag = Convert.ToString(GeneralList_GridCapID.EXPENSE_CAP);
                    //    frmGenList.SetFormToOpenName = frmExpense.Name;

                    //    break;

                    default:
                        break;
                    //Do nothing

                }


                if (strSQL != string.Empty)
                {
                    frmGenList.mstrGridSQL = strSQL;
                    frmGenList.Text = frmGenList.Text + strListGenTitle;
                    frmGenList.MdiParent = mdiGeneral.ActiveForm;

                    //frmGenList.formController.ShowForm(mConstants.Form_Mode.CONSULT_MODE);

                    //if (My.Forms.mdiGeneral.GetGenListChildCount == 0)
                    //{
                    //    frmGenList.Location = new Point(0, 0);
                    //}
                    //else
                    //{
                    //    frmGenList.Location = new Point(My.Forms.mdiGeneral.GetGenListChildCount * 25, My.Forms.mdiGeneral.GetGenListChildCount * 25);
                    //}

                    //My.Forms.mdiGeneral.AddGenListHandle((object)frmGenList, frmGenList.Handle.ToInt32);
                }

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        #endregion


        #region "SQL Queries"

        private static string strGetExpenseList_SQL(string vstrWhere = null)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + "  SELECT Expense.Exp_ID, " + Environment.NewLine;



            if (vstrWhere != string.Empty)
            {
            }

            strSQL = strSQL + "  ORDER BY Expense.Exp_Name " + Environment.NewLine;

            return strSQL;
        }

        #endregion

    }
}