﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceritar.CVS
{
    /// <summary>
    /// Cette classe retourne les résultats d'un traitement (validation, sauvegarde ou autre) effectué dans les modèles ou les controleurs.
    /// C'est une classe publique qui permet de communiquer de façoon générique avec n'importe quel objets externes à la DLL sans exposer les modèles..
    /// </summary>
    public class clsActionResults
    {
        private bool mblnValid;
        private List<int> mlstSuccessMessage_NRI;
        private int mintErrorMessage_NRI;
        private int mintNewItem_NRI;
        private object mErrorCode;
        private int mintRowInError;
        private string[] lstParams;

        //Messages
        private const ushort mintMSG_NotUsedError = 11;

        public enum BaseErrorCode
        {
            NO_ERROR = 0,
            UNHANDLED_ERROR = -1,
            UNHANDLED_VALIDATION = -2,
            UNHANDLED_EXCEPTION = -3,
            ERROR_SAVE = -4,
            INVALID_TIMESTAMP = -5
        }


        public clsActionResults()
        {
            mblnValid = false;  
            mErrorCode = BaseErrorCode.NO_ERROR;
            mintErrorMessage_NRI = mintMSG_NotUsedError;
            mintRowInError = 0;
            mlstSuccessMessage_NRI = new List<int>();
        }


#region "Properties"

        public bool IsValid
        {
            get { return mblnValid; }
            set { mblnValid = value; }
        }

        public int GetErrorMessage_NRI
        {
            get { return mintErrorMessage_NRI; }
        }

        public int SuccessMessage_NRI
        {
            get { return (mlstSuccessMessage_NRI.Count == 0 ? 0 : mlstSuccessMessage_NRI[0]); }
            set { if(!mlstSuccessMessage_NRI.Contains(value)) mlstSuccessMessage_NRI.Add(value);  }
        }

        public object GetErrorCode
        {
            get { return mErrorCode; }
        }

        public string[] GetLstParams
        {
            get { return lstParams; }
        }

        public int GetNewItem_NRI
        {
            get { return mintNewItem_NRI; }
        }

        internal int SetNewItem_NRI
        {
            set { mintNewItem_NRI = value; }
        }

        public int RowInError
        {
            get { return mintRowInError; }
            set { mintRowInError = value; }
        }

#endregion

        internal void Reinitialiser()
        {
            mblnValid = false;
            mErrorCode = BaseErrorCode.UNHANDLED_ERROR;
            mintErrorMessage_NRI = -1;
            mlstSuccessMessage_NRI = new List<int>();
            mintRowInError = 0;
            lstParams = null;
        }

        internal void SetValid(int vintSuccessMessage_NRI = 0)
        {
            mblnValid = true;
            mErrorCode = BaseErrorCode.NO_ERROR;
            mintErrorMessage_NRI = 0;
            mintRowInError = 0;
            lstParams = null;
        }

        internal void SetInvalid(object vintMessage_NRI, object vintErrorCode, params string[] vstrParams)
        {
            mblnValid = false;
            mErrorCode = vintErrorCode;
            mintErrorMessage_NRI = (int)vintMessage_NRI;
            mlstSuccessMessage_NRI.Clear();
            lstParams = vstrParams;
        }

        internal void SetDefault()
        {
            mblnValid = false;
            mErrorCode = BaseErrorCode.NO_ERROR;
            mintErrorMessage_NRI = mintMSG_NotUsedError;
            mintRowInError = 0;
            mlstSuccessMessage_NRI = new List<int>();
            lstParams = null;
        }
    }
}
