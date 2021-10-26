using System;
using Ceritar.TT3LightDLL.Classes;

namespace Ceritar.CVS
{
    /// <summary>
    /// Cette classe statique contient des propriétés potentiellement variables communes à toute la DLL.
    /// </summary>
    public static class sclsAppConfigs
    {
        private static string _strRoot_DB_UPGRADE_SCRIPTS_Dir;
        private static string _strRoot_INSTALLATIONS_ACTIVES_Dir;
        private static string _strAppRevisionFileName = "DB_UpdateRevisionNo";
        private static string _strMissingCeritarSecurityFileName = "Missing_Ceritar_Security";
        private static string _strTTAppDataFileName = "TTAppData";
        private static string _strScriptsFolderName = "Scripts";
        private static string _strRevisionAllScriptFolderName = "Rev_AllScripts";
        private static string _strPreviousRevisionAllScriptFolderName = "PreviousRev_AllScripts";
        private static string _strCaptionsAndMenusFileName;
        private static readonly string[] _strReleaseInvalidExtensions = { ".xml", ".ini", ".log", ".txt", ".sample", ".scc" };
        private static readonly string[] _strReleaseInvalidFolders = { "zh-CN", "Document", "Document_RPT" };
        private const string _strVersionNumberPrefix = "V_";
        private const string _strRevisionNumberPrefix = "R_";

        internal enum CONFIG_TYPE_NRI
        {
            PATH_DB_UPGRADE_SCRIPTS = 102,
            FILENAME_CAPTIONS_AND_MENUS = 103
        }


#region "Properties"

        public static string GetRoot_DB_UPGRADE_SCRIPTS
        {
            get
            {
                if (string.IsNullOrEmpty(_strRoot_DB_UPGRADE_SCRIPTS_Dir) || !System.IO.Directory.Exists(_strRoot_DB_UPGRADE_SCRIPTS_Dir))
                {
                    _strRoot_DB_UPGRADE_SCRIPTS_Dir = clsTTSQL.str_ADOSingleLookUp("TTP_Value", "TTParam", "TTP_NRI = " + (int)CONFIG_TYPE_NRI.PATH_DB_UPGRADE_SCRIPTS);
                }

                return _strRoot_DB_UPGRADE_SCRIPTS_Dir;
            }
        }

        public static string GetRoot_INSTALLATIONS_ACTIVES
        {
            get
            {
                string strSQL = string.Empty;
                System.Data.SqlClient.SqlDataReader sqlRecord = null;

                if (string.IsNullOrEmpty(_strRoot_INSTALLATIONS_ACTIVES_Dir) || !System.IO.Directory.Exists(_strRoot_INSTALLATIONS_ACTIVES_Dir))
                {
                    _strRoot_INSTALLATIONS_ACTIVES_Dir = string.Empty;
                    //_strRoot_INSTALLATIONS_ACTIVES_Dir = clsTTSQL.str_ADOSingleLookUp("TTP_Value", "TTParam", "TTP_NRI = " + (int)CONFIG_TYPE_NRI.PATH_INSTALLATIONS_ACTIVES);

                    strSQL = strSQL + " WITH LstHierarchyComp " + Environment.NewLine;
                    strSQL = strSQL + " AS " + Environment.NewLine;
                    strSQL = strSQL + " ( " + Environment.NewLine;
                    strSQL = strSQL + "     SELECT *, " + Environment.NewLine;
                    strSQL = strSQL + " 		   CASE WHEN FoT_NRI = " + (int)Controllers.ctr_Template.FolderType.System + " THEN CAST(0 AS varbinary(max)) ELSE CAST(HierarchyComp.HiCo_NRI AS varbinary(max)) END AS Level " + Environment.NewLine;
                    strSQL = strSQL + " 	FROM HierarchyComp  " + Environment.NewLine;
                    strSQL = strSQL + " 	WHERE HiCo_Parent_NRI IS NULL " + Environment.NewLine;

                    strSQL = strSQL + "     UNION ALL " + Environment.NewLine;

                    strSQL = strSQL + "     SELECT HiCo_Childrens.*, " + Environment.NewLine;
                    strSQL = strSQL + " 		   Level + CAST(HiCo_Childrens.HiCo_NRI AS varbinary(max)) AS Level " + Environment.NewLine;
                    strSQL = strSQL + " 	FROM HierarchyComp HiCo_Childrens  " + Environment.NewLine;
                    strSQL = strSQL + " 		INNER JOIN LstHierarchyComp on HiCo_Childrens.HiCo_Parent_NRI = LstHierarchyComp.HiCo_NRI " + Environment.NewLine;
                    strSQL = strSQL + " 	WHERE HiCo_Childrens.HiCo_Parent_NRI IS NOT NULL " + Environment.NewLine;
                    strSQL = strSQL + " ) " + Environment.NewLine;

                    strSQL = strSQL + " SELECT LstHierarchyComp.HiCo_Name " + Environment.NewLine;

                    strSQL = strSQL + " FROM LstHierarchyComp " + Environment.NewLine;

                    strSQL = strSQL + " WHERE LstHierarchyComp.FoT_NRI = " + (int)Controllers.ctr_Template.FolderType.System + Environment.NewLine;

                    strSQL = strSQL + " ORDER BY Level " + Environment.NewLine;

                    sqlRecord = clsTTSQL.ADOSelect(strSQL);

                    while (sqlRecord.Read())
                    {
                        _strRoot_INSTALLATIONS_ACTIVES_Dir = System.IO.Path.Combine(_strRoot_INSTALLATIONS_ACTIVES_Dir, sqlRecord["HiCo_Name"].ToString());
                    }

                    if (sqlRecord != null) sqlRecord.Dispose();
                }

                return System.IO.Path.Combine(_strRoot_INSTALLATIONS_ACTIVES_Dir + (_strRoot_INSTALLATIONS_ACTIVES_Dir.Substring(_strRoot_INSTALLATIONS_ACTIVES_Dir.Length - 1, 1) == "\\" ? "" : "\\"));
            }
        }

        public static string GetCaptionsAndMenusFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_strCaptionsAndMenusFileName))
                {
                    _strCaptionsAndMenusFileName = clsTTSQL.str_ADOSingleLookUp("TTP_Value", "TTParam", "TTP_NRI = " + (int)CONFIG_TYPE_NRI.FILENAME_CAPTIONS_AND_MENUS);
                }

                return _strCaptionsAndMenusFileName;
            }
        }

        public static string GetRevisionAllScriptFolderName
        {
            get { return sclsAppConfigs._strRevisionAllScriptFolderName; }
        }

        public static string GetPreviousRevisionAllScriptFolderName
        {
            get { return sclsAppConfigs._strPreviousRevisionAllScriptFolderName; }
        }

        public static string GetScriptsFolderName
        {
            get { return sclsAppConfigs._strScriptsFolderName; }
        }

        public static string GetVersionNumberPrefix
        {
            get { return _strVersionNumberPrefix; }
        }

        public static string GetRevisionNumberPrefix
        {
            get { return _strRevisionNumberPrefix; }
        }

        public static string[] GetReleaseInvalidExtensions
        {
            get { return _strReleaseInvalidExtensions; }
        }

        public static string[] GetReleaseInvalidFolders
        {
            get { return _strReleaseInvalidFolders; }
        }

        #endregion


        public static string GetAppRevisionFileName(string vstrRevisionNo)
        {
            return "00_" + _strAppRevisionFileName + vstrRevisionNo + ".sql";
        }

        public static string GetTTAppDataFileName(bool vblnWithoutNoAndExt = false)
        {
            if (vblnWithoutNoAndExt)
            {
                return _strTTAppDataFileName;
            }
            else
            {
                return "999_" + _strTTAppDataFileName + ".sql";
            }
        }

        public static string GetMissingCeritarSecurityFileName(bool vblnWithoutNoAndExt = false)
        {
            if (vblnWithoutNoAndExt)
            {
                return _strMissingCeritarSecurityFileName;
            }
            else
            {
                return "998_" + _strMissingCeritarSecurityFileName + ".sql";
            }
        }
    }
}
