﻿using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Configuration;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    /// <summary>
    /// Cette classe représente le modèle objet de la version d'une application de Ceritar pour un client donné.
    /// CAV représente le préfixe des colonnes de la table "ClientAppVersion" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_CAV_ClientAppVersion
    {
        //Model attributes
        private int _intClientAppVersion_NRI;
        private int _intClientAppVersion_TS;
        private bool _blnIsCurrentVersion;
        private string _strDateInstalled;
        private string _strLicense;
        private string _strLocationReportExe;
        private string _strLocationScriptsRoot;
        private mod_CeC_CeritarClient _cCeritarClient;
        private int _intCeritarApplication_NRI;
        private int _intVersion_NRI;

        //Others
        private mod_Ver_Version _cVersionParent = null;

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsTTSQL mcSQL;
      

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

        internal string DateInstalled
        {
            get { return _strDateInstalled; }
            set { _strDateInstalled = value; }
        }

        internal string License
        {
            get { return _strLicense; }
            set { _strLicense = value; }
        }

        internal mod_CeC_CeritarClient CeritarClient
        {
            get { return _cCeritarClient; }
            set { _cCeritarClient = value; }
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

        internal clsTTSQL SetcSQL
        {
            set { mcSQL = value; }
        }

        internal string LocationReportExe
        {
            get { return _strLocationReportExe; }
            set { _strLocationReportExe = value; }
        }

        internal string LocationScriptsRoot
        {
            get { return _strLocationScriptsRoot; }
            set { _strLocationScriptsRoot = value; }
        }

        internal mod_Ver_Version VersionParent
        {
            get { return _cVersionParent; }
            set { _cVersionParent = value; }
        }

#endregion


        internal clsActionResults Validate()
        {
            try
            {
                string strExternalReportAppName = clsTTSQL.str_ADOSingleLookUp("CeA_ExternalRPTAppName", "CerApp", "CeA_NRI = " + _intCeritarApplication_NRI);

                mcActionResults.SetDefault();

                switch (mintDML_Action)
                {
                    case sclsConstants.DML_Mode.NO_MODE:

                        mcActionResults.SetValid();

                        break;

                    case sclsConstants.DML_Mode.INSERT_MODE:

                        

                        if (_cCeritarClient.CeritarClient_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, Ceritar.CVS.Controllers.ctr_Version.ErrorCode_Ver.CLIENT_NAME_MANDATORY);
                        }
                        else if (_intCeritarApplication_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION);
                        }
                        else if (string.IsNullOrEmpty(_strLocationReportExe) & !string.IsNullOrEmpty(strExternalReportAppName) & !_cVersionParent.IsDemo)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, Ceritar.CVS.Controllers.ctr_Version.ErrorCode_Ver.REPORT_MANDATORY);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (_cCeritarClient.CeritarClient_NRI <= 0)
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
                        else if (!clsTTSQL.bln_ADOValid_TS("ClientAppVersion", "CAV_NRI", _intClientAppVersion_NRI, "CAV_TS", _intClientAppVersion_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else if (string.IsNullOrEmpty(_strLocationReportExe) & !string.IsNullOrEmpty(strExternalReportAppName) & !_cVersionParent.IsDemo)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, Ceritar.CVS.Controllers.ctr_Version.ErrorCode_Ver.REPORT_MANDATORY);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsTTSQL.bln_CheckReferenceIntegrity("ClientAppVersion", "CAV_NRI", _intClientAppVersion_NRI))
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
                            if (_blnIsCurrentVersion)
                            {
                                blnValidReturn = mcSQL.bln_ADOExecute("UPDATE ClientAppVersion SET CAV_IsCurrentVersion = 0 WHERE ClientAppVersion.CeC_NRI = " + _cCeritarClient.CeritarClient_NRI + " AND ClientAppVersion.Ver_NRI <> " + Version_NRI);
                            }
                            else
                            {
                                blnValidReturn = true;
                            }
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
                else if (!mcSQL.bln_AddField("CAV_IsCurrentVersion", _blnIsCurrentVersion, clsTTSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CAV_DtInstalledProd", _strDateInstalled, clsTTSQL.MySQL_FieldTypes.DATETIME_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeC_NRI", _cCeritarClient.CeritarClient_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeA_NRI", _intCeritarApplication_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Ver_NRI", _intVersion_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CAV_License", _strLicense, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CAV_ReportExe_Location", _strLocationReportExe, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CAV_ScriptsRoot_Location", _strLocationScriptsRoot, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
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

        internal bool blnLocationUpdate()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("CAV_ReportExe_Location", _strLocationReportExe, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_ADOUpdate("ClientAppVersion", "ClientAppVersion.CAV_NRI = " + _intClientAppVersion_NRI))
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

            return blnValidReturn;
        }
    }
}
