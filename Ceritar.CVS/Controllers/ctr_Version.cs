using System;
using Ceritar.CVS.Controllers.Interfaces;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Controllers
{
    public class ctr_Version
    {


        public ctr_Version(IVersion rView)
        {

        }



        public string strGetDataLoad_SQL(int vintVersion_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Version.Ver_TS, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_CompiledBy, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_DtCreation, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Tpl_NRI " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN Revision ON Revision.Ver_NRI = Version.Ver_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_Clients_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = " + (int)sclsConstants.DML_Mode.NO_MODE + ", " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_TS, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CerClient " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetApplications_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerApp.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CerApp " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerApp.CeA_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetTemplates_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Template.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Template " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY Template.Tpl_Name " + Environment.NewLine;

            return strSQL;
        }
    }
}
