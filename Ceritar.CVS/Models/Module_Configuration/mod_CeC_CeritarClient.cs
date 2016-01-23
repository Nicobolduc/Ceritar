using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers;
using System;

namespace Ceritar.CVS.Models.Module_Configuration
{
    /// <summary>
    /// Cette classe représente le modèle objet d'un client de Ceritar.
    /// CeC représente le préfixe des colonnes de la table "CerClient" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_CeC_CeritarClient
    {
        //Model attributes
        private int _intCeritarClient_NRI;
        private int _intCeritarClient_TS;
        private string _strCompanyName;
        private bool _blnIsActive;
        private List<int> _lstApplications; //TODO Remove, peut etre calculer a partir des versions installee


        //Working variables
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsSQL mcSQL;


#region "Properties"

        public int CeritarClient_NRI
        {
            get { return _intCeritarClient_NRI; }
            set { _intCeritarClient_NRI = value; }
        }

        public int CeritarClient_TS
        {
            get { return _intCeritarClient_TS; }
            set { _intCeritarClient_TS = value; }
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
