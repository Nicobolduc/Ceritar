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
            CERITAR_APPLICATION_LIST_NRI = 1,
            TEMPLATE_LIST_NRI = 2
        }

        public enum GeneralList_GridCapID
        {
            CERITAR_APPLICATION_CAP_NRI = 2,
            TEMPLATE_CAP_NRI = 8
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
                    case GeneralLists_ID.CERITAR_APPLICATION_LIST_NRI:
                        strSQL = strGetList_CeritarApplications_SQL();
                        strListGenTitle = " - Fiche d'une application de Ceritar";
                        //TODO caption pour ca
                        frmGenList.mintGridTag = ((int) GeneralList_GridCapID.CERITAR_APPLICATION_CAP_NRI).ToString();
                        frmGenList.SetFormToOpenName = typeof(frmCeritarApp).Name;

                        break;

                    case GeneralLists_ID.TEMPLATE_LIST_NRI:
                        strSQL = strGetList_Templates_SQL();
                        strListGenTitle = " - Gabarits des installations actives";
                        //TODO caption pour ca
                        frmGenList.mintGridTag = ((int)GeneralList_GridCapID.TEMPLATE_CAP_NRI).ToString();
                        frmGenList.SetFormToOpenName = typeof(frmTemplate).Name;

                        break;

                    default:
                        break;

                }


                if (strSQL != string.Empty)
                {
                    frmGenList.mstrGridSQL = strSQL;
                    frmGenList.Text = frmGenList.Text + strListGenTitle;
                    frmGenList.MdiParent = mdiGeneral.ActiveForm;

                    frmGenList.formController.ShowForm(sclsConstants.DML_Mode.CONSULT_MODE);

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

        private static string strGetList_CeritarApplications_SQL(string vstrWhere = null)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerApp.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Desc " + Environment.NewLine;

            strSQL = strSQL + " FROM CerApp " + Environment.NewLine;   

            if (vstrWhere != string.Empty)
            {
                strSQL = strSQL + vstrWhere + Environment.NewLine;
            }

            strSQL = strSQL + "  ORDER BY CerApp.CeA_Name " + Environment.NewLine;

            return strSQL;
        }

        private static string strGetList_Templates_SQL(string vstrWhere = null)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Template.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Template " + Environment.NewLine;

            if (vstrWhere != string.Empty)
            {
                strSQL = strSQL + vstrWhere + Environment.NewLine;
            }

            strSQL = strSQL + "  ORDER BY Template.Tpl_Name " + Environment.NewLine;

            return strSQL;
        }

#endregion

    }
}