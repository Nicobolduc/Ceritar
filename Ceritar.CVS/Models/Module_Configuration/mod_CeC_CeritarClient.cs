using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers;
using System;

namespace Ceritar.CVS.Models.Module_Configuration
{
    internal class mod_CeC_CeritarClient
    {
        //Model attributes
        private int _intCeritarApplication_NRI;
        private int _intCeritarApplication_TS;
        private string _strCompanyName;
        private bool _blnIsActive;
        private List<int> _lstApplications; //TODO Remove, peut etre calculer a partir des versions installee


        //Working variables
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsSQL mcSQL;


#region "Properties"

        public int CeritarApplication_NRI
        {
            get { return _intCeritarApplication_NRI; }
            set { _intCeritarApplication_NRI = value; }
        }

        public int CeritarApplication_TS
        {
            get { return _intCeritarApplication_TS; }
            set { _intCeritarApplication_TS = value; }
        }

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

        internal List<int> LstApplications
        {
            get { return _lstApplications; }
            set { _lstApplications = value; }
        }

        internal clsActionResults ActionResults
        {
            get { return mcActionResults; }
        }

        internal sclsConstants.DML_Mode DML_Action
        {
            get { return mintDML_Action; }
            set { mintDML_Action = value; }
        }

        internal clsSQL SetcSQL
        {
            set { mcSQL = value; }
        }


#endregion



    }
}
