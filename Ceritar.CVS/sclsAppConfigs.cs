using System;
using System.Collections.Generic;

namespace Ceritar.CVS
{
    public static class sclsAppConfigs
    {
        private static String _strRoot_DB_UPGRADE_SCRIPTS_Dir;
        private static string _strRoot_INSTALLATIONS_ACTIVES_Dir;

        public static string Root_DB_UPGRADE_SCRIPTS
        {
            get { return _strRoot_DB_UPGRADE_SCRIPTS_Dir; }
            set { _strRoot_DB_UPGRADE_SCRIPTS_Dir = value; }
        }

        public static string Root_INSTALLATIONS_ACTIVES
        {
            get { return _strRoot_INSTALLATIONS_ACTIVES_Dir; }
            set { _strRoot_INSTALLATIONS_ACTIVES_Dir = value; }
        }
    }
}
