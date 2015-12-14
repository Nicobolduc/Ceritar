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
            TEMPLATE_LIST_NRI = 2,
            VERSION_REVISION_LIST_NRI = 3,
            CERITAR_CLIENT_LIST_NRI = 4
        }

        public enum GeneralList_GridCapID
        {
            CERITAR_APPLICATION_CAP_NRI = 2,
            TEMPLATE_CAP_NRI = 8,
            VERSION_REVISION_CAP_NRI = 14
        }


#region "Functions / Subs"

        public static void ShowGenList(GeneralLists_ID vList_ID)
        {
            string strListGenTitle = string.Empty;
            string strSQL = string.Empty;
            int intItem_NRI = 0;
            
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

                    case GeneralLists_ID.CERITAR_CLIENT_LIST_NRI:
                        strSQL = strGetList_CeritarClient_SQL();
                        strListGenTitle = " - Fiche d'un client de Ceritar";
                        frmGenList.mintGridTag = ((int)GeneralList_GridCapID.CERITAR_APPLICATION_CAP_NRI).ToString();
                        frmGenList.SetFormToOpenName = typeof(frmCeritarClient).Name;

                        break;

                    case GeneralLists_ID.TEMPLATE_LIST_NRI:
                        strSQL = strGetList_Templates_SQL();
                        strListGenTitle = " - Gabarits des installations actives";
                        //TODO caption pour ca
                        frmGenList.mintGridTag = ((int) GeneralList_GridCapID.TEMPLATE_CAP_NRI).ToString();
                        frmGenList.SetFormToOpenName = typeof(frmTemplate).Name;

                        break;

                    case GeneralLists_ID.VERSION_REVISION_LIST_NRI:
                        strSQL = strGetList_Versions_SQL();
                        strListGenTitle = " - Versions et révisions";
                        frmGenList.mintGridTag = ((int) GeneralList_GridCapID.VERSION_REVISION_CAP_NRI).ToString();
                        frmGenList.SetFormToOpenName = typeof(frmVersion).Name;

                        break;

                    default:
                        break;

                }


                if (strSQL != string.Empty)
                {
                    frmGenList.mstrGridSQL = strSQL;
                    frmGenList.Text = frmGenList.Text + strListGenTitle;
                    frmGenList.MdiParent = mdiGeneral.ActiveForm;

                    frmGenList.formController.ShowForm(sclsConstants.DML_Mode.CONSULT_MODE, ref intItem_NRI);

                    //if (mdiGeneral.GetGenListChildCount == 0)
                    //{
                    //    frmGenList.Location = new Point(0, 0);
                    //}
                    //else
                    //{
                    //    frmGenList.Location = new Point(My.Forms.mdiGeneral.GetGenListChildCount * 25, My.Forms.mdiGeneral.GetGenListChildCount * 25);
                    //}

                    //mdiGeneral.AddGenListHandle((object)frmGenList, frmGenList.Handle.ToInt32);
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

        private static string strGetList_CeritarClient_SQL(string vstrWhere = null)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name " + Environment.NewLine;


            strSQL = strSQL + " FROM CerClient " + Environment.NewLine;

            if (vstrWhere != string.Empty)
            {
                strSQL = strSQL + vstrWhere + Environment.NewLine;
            }

            strSQL = strSQL + "  ORDER BY CerClient.CeC_Name " + Environment.NewLine;

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

        private static string strGetList_Versions_SQL(string vstrWhere = null)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Version.Ver_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_No " + Environment.NewLine;
            //strSQL = strSQL + "        CurrentlyInProd =  " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerApp ON CerApp.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;
            //strSQL = strSQL + "     LEFT JOIN ClientAppVersion ON ClientAppVersion.Ver_NRI = Version.Ver_NRI AND ClientAppVersion.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;

            if (vstrWhere != string.Empty)
            {
                strSQL = strSQL + vstrWhere + Environment.NewLine;
            }

            strSQL = strSQL + "  ORDER BY CerApp.CeA_Name, Version.Ver_No " + Environment.NewLine;

            return strSQL;
        }

#endregion

    }
}