using System;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    class mod_CAV_ClientAppVersion
    {
        //Model attributes
        private int _intClientAppVersion_NRI;
        private int _intClientAppVersion_TS;
        private bool _blnIsCurrentVersion;
        private bool _blnInstalled;
        private string _strLicense;
        private int _intCeritarClient_NRI;
        private int _intCeritarApplication_NRI;
        private int _intVersion_NRI;

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsSQL mcSQL;
        
        //Working variables


#region "Properties"

        internal int ClientAppVersion_NRI
        {
            get { return _intClientAppVersion_NRI; }
            set { _intClientAppVersion_NRI = value; }
        }

        internal int ClientAppVersion_TS
        {
            get { return _intClientAppVersion_TS; }
            set { _intClientAppVersion_TS = value; }
        }

        internal bool IsCurrentVersion
        {
            get { return _blnIsCurrentVersion; }
            set { _blnIsCurrentVersion = value; }
        }

        internal bool Installed
        {
            get { return _blnInstalled; }
            set { _blnInstalled = value; }
        }

        internal string License
        {
            get { return _strLicense; }
            set { _strLicense = value; }
        }

        internal int CeritarClient_NRI
        {
            get { return _intCeritarClient_NRI; }
            set { _intCeritarClient_NRI = value; }
        }

        internal int CeritarApplication_NRI
        {
            get { return _intCeritarApplication_NRI; }
            set { _intCeritarApplication_NRI = value; }
        }

        internal int Version_NRI
        {
            get { return _intVersion_NRI; }
            set { _intVersion_NRI = value; }
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

                        if (_intCeritarClient_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, Ceritar.CVS.Controllers.ctr_Version.ErrorCode_Ver.CLIENT_NAME_MANDATORY);
                        }
                        else if (_intCeritarApplication_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (_intCeritarClient_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, Ceritar.CVS.Controllers.ctr_Version.ErrorCode_Ver.CLIENT_NAME_MANDATORY);
                        }
                        else if (_intCeritarApplication_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION);
                        }
                        else if (_intVersion_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION);
                        }
                        else if (!clsSQL.bln_ADOValid_TS("ClientAppVersion", "CAV_NRI", _intClientAppVersion_NRI, "CAV_TS", _intClientAppVersion_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsSQL.bln_CheckReferenceIntegrity("ClientAppVersion", "CAV_NRI", _intClientAppVersion_NRI))
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
            bool blnValidReturn = false;

            try
            {
                mcActionResults.SetValid();

                switch (mintDML_Action)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:

                        if (!pfblnCAV_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("ClientAppVersion", out _intClientAppVersion_NRI))
                        { }
                        else
                        {
                            mcActionResults.SetNewItem_NRI = _intClientAppVersion_NRI;
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnCAV_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("ClientAppVersion", "ClientAppVersion.CAV_NRI = " + _intClientAppVersion_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("ClientAppVersion", "ClientAppVersion.CAV_NRI = " + _intClientAppVersion_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    default:
                        blnValidReturn = true;

                        break;
                }
            }
            catch (System.Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (!blnValidReturn & mcActionResults.IsValid)
                {
                    mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_SAVE_MSG, clsActionResults.BaseErrorCode.ERROR_SAVE);
                }
                else if (blnValidReturn & !mcActionResults.IsValid)
                {
                    blnValidReturn = false;
                }
            }

            return blnValidReturn;
        }

        private bool pfblnCAV_AddFields()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("CAV_IsCurrentVersion", _blnIsCurrentVersion, clsSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CAV_Installed", _blnInstalled, clsSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeC_NRI", _intCeritarClient_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeA_NRI", _intCeritarApplication_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Ver_NRI", _intVersion_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CAV_License", _strLicense, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else
                {
                    blnValidReturn = true;
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (!blnValidReturn & mcActionResults.IsValid)
                {
                    mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_SAVE_MSG, clsActionResults.BaseErrorCode.ERROR_SAVE);
                }
                else if (blnValidReturn & !mcActionResults.IsValid)
                {
                    blnValidReturn = false;
                }
            }

            return blnValidReturn;
        }
    }
}
