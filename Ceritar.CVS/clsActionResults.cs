using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceritar.CVS
{
    public class clsActionResults
    {
        private bool mblnValid;
        private int mintMessage_NRI;
        private object mErrorCode;
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
            mintMessage_NRI = mintMSG_NotUsedError;
        }


#region "Properties"

        public bool IsValid
        {
            get { return mblnValid; }
            set { mblnValid = value; }
        }

        public int GetMessage_NRI
        {
            get { return mintMessage_NRI; }
        }

        public object GetErrorCode
        {
            get { return mErrorCode; }
        }

        public string[] GetLstParams
        {
            get { return lstParams; }
        }

#endregion

        internal void Reinitialiser()
        {
            mblnValid = false;
            mErrorCode = BaseErrorCode.UNHANDLED_ERROR;
            mintMessage_NRI = -1;
            lstParams = null;
        }

        internal void SetValid()
        {
            mblnValid = true;
            mErrorCode = BaseErrorCode.NO_ERROR;
            mintMessage_NRI = 0;
            lstParams = null;
        }

        internal void SetInvalid(object vintMessage_NRI, object vintErrorCode, params string[] vstrParams)
        {
            mblnValid = false;
            mErrorCode = vintErrorCode;
            mintMessage_NRI = (int)vintMessage_NRI;
            lstParams = vstrParams;
        }

        internal void SetDefault()
        {
            mblnValid = false;
            mErrorCode = BaseErrorCode.NO_ERROR;
            mintMessage_NRI = mintMSG_NotUsedError;
            lstParams = null;
        }
    }
}
