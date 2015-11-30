using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_Configuration
{
    class mod_CVA_ClientVersionApp
    {
        //Model attributes
        private string _strVersionCourante;
        private string _strLicense;
        private List<mod_CeC_CeritarClient> _lstClient;
        private mod_CeA_CeritarApplication _cApplication;
        
        //Working variables


#region "Properties"

        internal string VersionCourante
        {
            get { return _strVersionCourante; }
            set { _strVersionCourante = value; }
        }

        internal string License
        {
            get { return _strLicense; }
            set { _strLicense = value; }
        }

        internal List<mod_CeC_CeritarClient> Client
        {
            get { return _lstClient; }
            set { _lstClient = value; }
        }

        internal mod_CeA_CeritarApplication Application
        {
            get { return _cApplication; }
            set { _cApplication = value; }
        }

#endregion


    }
}
