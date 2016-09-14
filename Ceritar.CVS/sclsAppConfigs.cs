﻿using System;
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
        private static string[] _strReleaseValidExtensions = { ".dll", ".config", ".exe" };
        private const string _strVersionNumberPrefix = "V_";
        private const string _strRevisionNumberPrefix = "R_";

        internal enum CONFIG_TYPE
        {
            PATH_INSTALLATIONS_ACTIVES = 100,
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
                    _strRoot_DB_UPGRADE_SCRIPTS_Dir = clsTTSQL.str_ADOSingleLookUp("TTP_Value", "TTParam", "TTP_NRI = " + (int)CONFIG_TYPE.PATH_DB_UPGRADE_SCRIPTS);
                }

                return _strRoot_DB_UPGRADE_SCRIPTS_Dir;
            }
        }

        public static string GetRoot_INSTALLATIONS_ACTIVES
        {
            get
            {
                if (string.IsNullOrEmpty(_strRoot_INSTALLATIONS_ACTIVES_Dir) || !System.IO.Directory.Exists(_strRoot_INSTALLATIONS_ACTIVES_Dir))
                {
                    _strRoot_INSTALLATIONS_ACTIVES_Dir = clsTTSQL.str_ADOSingleLookUp("TTP_Value", "TTParam", "TTP_NRI = " + (int)CONFIG_TYPE.PATH_INSTALLATIONS_ACTIVES);
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
                    _strCaptionsAndMenusFileName = clsTTSQL.str_ADOSingleLookUp("TTP_Value", "TTParam", "TTP_NRI = " + (int)CONFIG_TYPE.FILENAME_CAPTIONS_AND_MENUS);
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

        public static string[] GetReleaseValidExtensions
        {
            get { return sclsAppConfigs._strReleaseValidExtensions; }
        }

#endregion

        
    }
}
