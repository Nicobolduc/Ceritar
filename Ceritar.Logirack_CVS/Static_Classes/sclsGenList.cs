using System;
using System.Windows.Forms;
using Ceritar.Logirack_CVS.Forms;
using Ceritar.Logirack_CVS;
using Ceritar.Logirack_CVS.Static_Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;

namespace Ceritar.Logirack_CVS.Static_Classes
{
    /// <summary>
    /// Cette classe statique contients les informations relatives aux listes génériques affichées par l'écran "frmGenericList".
    /// </summary>
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
            CERITAR_APPLICATION_GRID_CAP_NRI = 2,
            CERITAR_CLIENT_GRID_CAP_NRI = 8,
            TEMPLATE_GRID_CAP_NRI = 52,
            VERSION_REVISION_GRID_CAP_NRI = 14
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

                strListGenTitle = " - ";

                switch (vList_ID)
                {
                    case GeneralLists_ID.CERITAR_APPLICATION_LIST_NRI:
                        strSQL = strGetList_CeritarApplications_SQL();

                        frmGenList.Tag = 44;

                        strListGenTitle += clsTTApp.GetAppController.str_GetCaption((int)frmGenList.Tag, clsTTApp.GetAppController.cUser.UserLanguage);
                        
                        frmGenList.mintGridTag = ((int) GeneralList_GridCapID.CERITAR_APPLICATION_GRID_CAP_NRI).ToString();
                        frmGenList.SetFormToOpenName = typeof(frmCeritarApp).Name;

                        break;

                    case GeneralLists_ID.CERITAR_CLIENT_LIST_NRI:
                        strSQL = strGetList_CeritarClient_SQL();
                        
                        frmGenList.Tag = 45;

                        strListGenTitle += clsTTApp.GetAppController.str_GetCaption((int)frmGenList.Tag, clsTTApp.GetAppController.cUser.UserLanguage);

                        frmGenList.mintGridTag = ((int)GeneralList_GridCapID.CERITAR_CLIENT_GRID_CAP_NRI).ToString();
                        frmGenList.SetFormToOpenName = typeof(frmCeritarClient).Name;

                        break;

                    case GeneralLists_ID.TEMPLATE_LIST_NRI:
                        strSQL = strGetList_Templates_SQL();
                        
                        frmGenList.Tag = 46;

                        strListGenTitle += clsTTApp.GetAppController.str_GetCaption((int)frmGenList.Tag, clsTTApp.GetAppController.cUser.UserLanguage);

                        frmGenList.mintGridTag = ((int) GeneralList_GridCapID.TEMPLATE_GRID_CAP_NRI).ToString();
                        frmGenList.SetFormToOpenName = typeof(frmTemplate).Name;

                        break;

                    case GeneralLists_ID.VERSION_REVISION_LIST_NRI:
                        strSQL = strGetList_Versions_SQL();
                        
                        frmGenList.Tag = 47;

                        strListGenTitle += clsTTApp.GetAppController.str_GetCaption((int)frmGenList.Tag, clsTTApp.GetAppController.cUser.UserLanguage);

                        frmGenList.mintGridTag = ((int) GeneralList_GridCapID.VERSION_REVISION_GRID_CAP_NRI).ToString();
                        frmGenList.SetFormToOpenName = typeof(frmVersion).Name;

                        break;

                    default:
                        break;

                }


                if (strSQL != string.Empty)
                {
                    frmGenList.mstrGridSQL = strSQL;
                    frmGenList.Text = frmGenList.Text + strListGenTitle;
                    frmGenList.MdiParent = TT3LightDLL.Classes.clsTTApp.GetAppController.GetMDI;
                    frmGenList.Location = clsTTForms.GetTTForms.GetGenericListLocation();

                    frmGenList.formController.ShowForm(frmGenList, sclsConstants.DML_Mode.CONSULT_MODE, ref intItem_NRI);
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
            strSQL = strSQL + "        CerClient.CeC_Name, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_IsActive " + Environment.NewLine;
            
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
            strSQL = strSQL + "        CerApp.CeA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        TemplateType.TeT_Code, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Template " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerApp ON CerApp.CeA_NRI = Template.CeA_NRI " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN TemplateType ON TemplateType.TeT_NRI = Template.TeT_NRI " + Environment.NewLine;

            if (vstrWhere != string.Empty)
            {
                strSQL = strSQL + vstrWhere + Environment.NewLine;
            }

            strSQL = strSQL + "  ORDER BY CerApp.CeA_Name, TemplateType.TeT_Code, Template.Tpl_Name " + Environment.NewLine;

            return strSQL;
        }

        private static string strGetList_Versions_SQL(string vstrWhere = null)
        {
            string strSQL = string.Empty;
            string strDefaultApplication = string.Empty;

            strDefaultApplication = clsTTSQL.str_ADOSingleLookUp("CeA_NRI_Default", "TTUser", "TTU_NRI = " + clsTTApp.GetAppController.cUser.User_NRI);

            strSQL = strSQL + " SELECT Version.Ver_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_No " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerApp ON CerApp.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;
            //strSQL = strSQL + "     LEFT JOIN ClientAppVersion ON ClientAppVersion.Ver_NRI = Version.Ver_NRI AND ClientAppVersion.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;

            if (vstrWhere != string.Empty)
            {
                strSQL = strSQL + vstrWhere + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(strDefaultApplication))
            {
                strSQL = strSQL + " ORDER BY CASE WHEN CerApp.CeA_NRI = " + strDefaultApplication + " THEN 0 ELSE 1 END, CerApp.CeA_Name, Version.Ver_No DESC " + Environment.NewLine;
            }
            else
            {
                strSQL = strSQL + " ORDER BY CerApp.CeA_Name, Version.Ver_No DESC " + Environment.NewLine;
            }
            
            return strSQL;
        }

#endregion

    }
}