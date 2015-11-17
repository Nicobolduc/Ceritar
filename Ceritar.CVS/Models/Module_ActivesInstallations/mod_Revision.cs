using Ceritar.CVS.Models.Module_Template;
using Ceritar.CVS.Models.Module_Configuration;
using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    internal class mod_Revision
    {
        //Model attributes
        private mod_HierarchyTemplate _cTemplateSource;
        private mod_Version _cVersion;
        private mod_User _cCreatedByUser;
        private List<string> _lstModifications;
        private string _strPath_Release;
        private string _strPath_Scripts;
        private string _strCreationDate;
        private int _intVersionNo;

        //Working variables


#region "Properties"

        internal mod_HierarchyTemplate TemplateSource
        {
            get { return _cTemplateSource; }
            set { _cTemplateSource = value; }
        }

        internal mod_Version Version
        {
            get { return _cVersion; }
            set { _cVersion = value; }
        }

        internal mod_User CreatedByUser
        {
            get { return _cCreatedByUser; }
            set { _cCreatedByUser = value; }
        }

        internal List<string> LstModifications
        {
            get { return _lstModifications; }
            set { _lstModifications = value; }
        }

        internal string Path_Release
        {
            get { return _strPath_Release; }
            set { _strPath_Release = value; }
        }

        internal string Path_Scripts
        {
            get { return _strPath_Scripts; }
            set { _strPath_Scripts = value; }
        }

        internal string CreationDate
        {
            get { return _strCreationDate; }
            set { _strCreationDate = value; }
        }

        internal int VersionNo
        {
            get { return _intVersionNo; }
            set { _intVersionNo = value; }
        }

#endregion

    }
}
