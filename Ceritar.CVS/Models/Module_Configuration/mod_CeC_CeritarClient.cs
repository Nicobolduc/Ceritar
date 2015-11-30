

namespace Ceritar.CVS.Models.Module_Configuration
{
    internal class mod_CeC_CeritarClient
    {
        //Model attributes
        private string _strCompanyName;
        private bool _blnIsActive;
        private mod_CVA_ClientVersionApp _cCurrentVersion;

        //Working variables


#region "Properties"

        internal string CompanyName
        {
            get { return _strCompanyName; }
            set { _strCompanyName = value; }
        }

        internal bool IsActive
        {
            get { return _blnIsActive; }
            set { _blnIsActive = value; }
        }

        internal mod_CVA_ClientVersionApp CurrentVersion
        {
            get { return _cCurrentVersion; }
            set { _cCurrentVersion = value; }
        }

#endregion



    }
}
