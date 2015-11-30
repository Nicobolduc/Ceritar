using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Template;
using Ceritar.CVS.Models.Module_Configuration;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    internal class mod_Ver_Version
    {
        //Model attributes
        private string _strCompiledBy;
        private string _strLocation_APP_CHANGEMENT;
        private string _strLocation_Release;
        private string _strLocation_TTApp;
        private ushort _intVersionNo;
        private string _strCreationDate;
        private mod_TTU_User _cCreatedByUser;
        private List<mod_Rev_Revision> _lstRevisions;
        private mod_CeA_CeritarApplication _cApplication;
        private mod_Tpl_HierarchyTemplate _cTemplateSource;
        private List<mod_CeC_CeritarClient> _lstClientsUsing;


        //Working variables


#region "Properties"

        internal string CompiledBy
        {
            get { return _strCompiledBy; }
            set { _strCompiledBy = value; }
        }

        internal string Location_APP_CHANGEMENT
        {
            get { return _strLocation_APP_CHANGEMENT; }
            set { _strLocation_APP_CHANGEMENT = value; }
        }

        internal string Location_Release
        {
            get { return _strLocation_Release; }
            set { _strLocation_Release = value; }
        }

        internal string Location_TTApp
        {
            get { return _strLocation_TTApp; }
            set { _strLocation_TTApp = value; }
        }

        internal ushort VersionNo
        {
            get { return _intVersionNo; }
            set { _intVersionNo = value; }
        }

        internal string CreationDate
        {
            get { return _strCreationDate; }
            set { _strCreationDate = value; }

        }

        internal mod_TTU_User CreatedByUser
        {
            get { return _cCreatedByUser; }
            set { _cCreatedByUser = value; }
        }

        internal List<mod_Rev_Revision> LstRevisions
        {
            get { return _lstRevisions; }
            set { _lstRevisions = value; }
        }

        internal mod_CeA_CeritarApplication Application
        {
            get { return _cApplication; }
            set { _cApplication = value; }
        }

        internal mod_Tpl_HierarchyTemplate TemplateSource
        {
            get { return _cTemplateSource; }
            set { _cTemplateSource = value; }
        }

        internal List<mod_CeC_CeritarClient> LstClientsUsing
        {
            get { return _lstClientsUsing; }
            set { _lstClientsUsing = value; }
        }

#endregion




    }
}
