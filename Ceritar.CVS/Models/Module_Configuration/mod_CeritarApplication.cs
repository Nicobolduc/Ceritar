using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_Configuration
{
    internal class mod_CeritarApplication
    {
        //Model attributes
        private string _strName;
        private string _strDescription;
        private AppDomain _domaine;
        private List<string> _lstModules;


        public enum AppDomain
        {
            Tranport = 1,
            Entreposage = 2,
            Distribution = 3,
            SGSP = 4,
            Web = 5,   
            Interne = 6
        }

        //Working variables



#region "Properties"

        internal string Name
        {
            get { return _strName; }
            set { _strName = value; }
        }

        internal string Description
        {
            get { return _strDescription; }
            set { _strDescription = value; }
        }

        internal AppDomain Domaine
        {
            get { return _domaine; }
            set { _domaine = value; }
        }

        internal List<string> LstModules
        {
            get { return _lstModules; }
            set { _lstModules = value; }
        }

#endregion



    }
}
