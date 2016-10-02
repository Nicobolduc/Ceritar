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
        private static string _strScriptsFolderName = "Scripts";
        private static string _strReleaseFolderName = "Release";
        private static string _strRevisionAllScriptFolderName = "Rev_AllScripts";
        private static string _strCaptionsAndMenusFileName;
        private static string[] _strReleaseInvalidExtensions = { ".xml", ".pdb", ".ini", ".log", ".txt", ".sample" };
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

        public static string GetScriptsFolderName
        {
            get { return sclsAppConfigs._strScriptsFolderName; }
        }

        public static string GetReleaseFolderName
        {
            get { return sclsAppConfigs._strReleaseFolderName; }
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
            get { return sclsAppConfigs._strReleaseInvalidExtensions; }
        }

#endregion

        
    }
}
