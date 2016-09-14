using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers;
using System;
using System.IO;
using System.Linq;

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

        //Messages
        private const int mintMSG_InvalidName = 34;

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsTTSQL mcSQL;


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

        internal clsActionResults ActionResults
        {
            get { return mcActionResults; }
        }

        internal sclsConstants.DML_Mode DML_Action
        {
            get { return mintDML_Action; }
            set { mintDML_Action = value; }
        }

        internal clsTTSQL SetcSQL
        {
            set { mcSQL = value; }
        }

#endregion


        internal clsActionResults Validate()
        {
            try
            {
                mcActionResults.SetDefault();

                switch (mintDML_Action)
                {
                    case sclsConstants.DML_Mode.NO_MODE:
                        mcActionResults.SetValid();

                        break;

                    case sclsConstants.DML_Mode.INSERT_MODE:
                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (string.IsNullOrEmpty(_strCompanyName))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_CeritarClient.ErrorCode_CeC.NAME_MANDATORY);
                        }
                        else if (Path.GetInvalidFileNameChars().Where(x => _strCompanyName.Contains(x)).Count() > 0 || _strCompanyName == "con")
                        {
                            mcActionResults.SetInvalid(mintMSG_InvalidName, ctr_CeritarClient.ErrorCode_CeC.NAME_INVALID);
                        }
                        else if (!clsTTSQL.bln_ADOValid_TS("CerClient", "CeC_NRI", _intCeritarClient_NRI, "CeC_TS", _intCeritarClient_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsTTSQL.bln_CheckReferenceIntegrity("CerClient", "CeC_NRI", _intCeritarClient_NRI))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_REFERENCE_INTEGRITY, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;
                }
            }
            catch (System.Exception ex)
            {
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResults;
        }

        internal bool blnSave()
        {
            try
            {
                mcActionResults.SetDefault();

                switch (mintDML_Action)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:

                        if (!pfblnCeC_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("CerClient", out _intCeritarClient_NRI))
                        { }
                        else
                        {
                            mcActionResults.SetNewItem_NRI = _intCeritarClient_NRI;
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnCeC_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("CerClient", "CerClient.CeC_NRI = " + _intCeritarClient_NRI))
                        { }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("CerClient", "CerClient.CeC_NRI = " + _intCeritarClient_NRI))
                        { }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    default:
                        mcActionResults.SetValid();

                        break;
                }
            }
            catch (System.Exception ex)
            {
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_SAVE_MSG, clsActionResults.BaseErrorCode.ERROR_SAVE);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResults.IsValid;
        }

        private bool pfblnCeC_AddFields()
        {
            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("CeC_Name", _strCompanyName, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeC_IsActive", _blnIsActive, clsTTSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else
                {
                    mcActionResults.SetValid();
                }
            }
            catch (Exception ex)
            {
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_SAVE_MSG, clsActionResults.BaseErrorCode.ERROR_SAVE);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResults.IsValid;
        }
    }
}
