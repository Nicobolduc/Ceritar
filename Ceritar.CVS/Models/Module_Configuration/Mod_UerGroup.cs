using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_Configuration
{
    internal class mod_UerGroup
    {
        //Model attributes
        private string _strGroupCode;
        private string _strGroupDescription;
        private bool _blnRight_AdminAccount;
        private bool _blnRight_ValidateLicense;
        private bool _blnRight_VersionCreation;
        private bool _blnRight_RevisionCreation;
        private bool _blnRight_HierarchyModification;
        private bool _blnRight_ClientModification;
        private bool _blnRight_ApplicationModification;
        private List<mod_User> _lstUers;


        //Working variables


#region "Properties"

        internal string GroupCode
        {
            get { return _strGroupCode; }
            set { _strGroupCode = value; }
        }

        internal string GroupDescription
        {
            get { return _strGroupDescription; }
            set { _strGroupDescription = value; }
        }

        internal bool Right_AdminAccount
        {
            get { return _blnRight_AdminAccount; }
            set { _blnRight_AdminAccount = value; }
        }

        internal bool Right_ValidateLicense
        {
            get { return _blnRight_ValidateLicense; }
            set { _blnRight_ValidateLicense = value; }
        }

        internal bool Right_VersionCreation
        {
            get { return _blnRight_VersionCreation; }
            set { _blnRight_VersionCreation = value; }
        }

        internal bool Right_RevisionCreation
        {
            get { return _blnRight_RevisionCreation; }
            set { _blnRight_RevisionCreation = value; }
        }

        internal bool Right_HierarchyModification
        {
            get { return _blnRight_HierarchyModification; }
            set { _blnRight_HierarchyModification = value; }
        }

        internal bool Right_ClientModification
        {
            get { return _blnRight_ClientModification; }
            set { _blnRight_ClientModification = value; }
        }

        internal bool Right_ApplicationModification
        {
            get { return _blnRight_ApplicationModification; }
            set { _blnRight_ApplicationModification = value; }
        }

        internal List<mod_User> LstUers
        {
            get { return _lstUers; }
            set { _lstUers = value; }
        }

#endregion

    }
}

