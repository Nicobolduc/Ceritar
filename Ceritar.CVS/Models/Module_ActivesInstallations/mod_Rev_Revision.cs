﻿using System;
using Ceritar.CVS.Models.Module_Template;
using Ceritar.CVS.Models.Module_ActivesInstallations;
using Ceritar.CVS.Models.Module_Configuration;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.CVS.Controllers;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    /// <summary>
    /// Cette classe représente le modèle objet d'une révision de version.
    /// Rev représente le préfixe des colonnes de la table "Revision" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_Rev_Revision
    {
        //Model attributes
        private int _intRevision_NRI;
        private int _intRevision_TS;
        private byte _intRevision_Number;
        private mod_Tpl_HierarchyTemplate _cTemplateSource;
        private mod_Ver_Version _cVersion;
        private mod_TTU_User _cCreatedByUser;
        private mod_CeC_CeritarClient _cCeritarClient;
        private List<mod_SRe_SatelliteRevision> _lstSatelliteRevisions;
        private List<string> _lstModifications;
        private string _strLocation_Release;
        private string _strLocation_Scripts;
        private string _strCreationDate;
        private string _strCreatedBy;
        private string _strNote;
        private bool _blnExeIsExternalReport;
        private bool _blnExeWithExternalReport;
        private bool _blnPreparationMode;

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsTTSQL mcSQL;

        //Messages
        private const int mintMSG_CantDeleteRevisionIfNotLast = 30;
        //Working variables


#region "Properties"

        internal int Revision_NRI
        {
            get { return _intRevision_NRI; }
            set { _intRevision_NRI = value; }
        }

        internal int Revision_TS
        {
            get { return _intRevision_TS; }
            set { _intRevision_TS = value; }
        }

        internal byte Revision_Number
        {
            get { return _intRevision_Number; }
            set { _intRevision_Number = value; }
        }

        internal mod_Tpl_HierarchyTemplate TemplateSource
        {
            get { return _cTemplateSource; }
            set { _cTemplateSource = value; }
        }

        internal mod_Ver_Version Version
        {
            get { return _cVersion; }
            set { _cVersion = value; }
        }

        internal mod_TTU_User CreatedByUser
        {
            get { return _cCreatedByUser; }
            set { _cCreatedByUser = value; }
        }

        internal string CreatedBy
        {
            get { return _strCreatedBy; }
            set { _strCreatedBy = value; }
        }

        internal string Note
        {
            get { return _strNote; }
            set { _strNote = value; }
        }

        internal mod_CeC_CeritarClient CeritarClient
        {
            get { return _cCeritarClient; }
            set { _cCeritarClient = value; }
        }

        internal List<mod_SRe_SatelliteRevision> LstSatelliteRevisions
        {
            get
            {
                if (_lstSatelliteRevisions == null)
                {

                    _lstSatelliteRevisions = new List<mod_SRe_SatelliteRevision>();
                }

                return _lstSatelliteRevisions;
            }
            set { _lstSatelliteRevisions = value; }
        }

        internal List<string> LstModifications
        {
            get { return _lstModifications; }
            set { _lstModifications = value; }
        }

        internal string Path_Release
        {
            get { return _strLocation_Release; }
            set { _strLocation_Release = value; }
        }

        internal string Path_Scripts
        {
            get { return _strLocation_Scripts; }
            set { _strLocation_Scripts = value; }
        }

        internal string CreationDate
        {
            get { return _strCreationDate; }
            set { _strCreationDate = value; }
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

        internal bool ExeIsExternalReport
        {
            get { return _blnExeIsExternalReport; }
            set { _blnExeIsExternalReport = value; }
        }

        internal bool ExeWithExternalReport
        {
            get { return _blnExeWithExternalReport; }
            set { _blnExeWithExternalReport = value; }
        }

        internal bool PreparationMode
        {
            get { return _blnPreparationMode; }
            set { _blnPreparationMode = value; }
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

                        if (_lstModifications == null || _lstModifications.Count == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.MODIFICATION_LIST_MANDATORY);
                        }
                        else if (_intRevision_Number <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.REVISION_NUMBER_MANDATORY);
                        }
                        else if (_cTemplateSource == null || _cTemplateSource.Template_NRI == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.TEMPLATE_MANDATORY);
                        }
                        else if (_cVersion == null || _cVersion.Version_NRI == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.VERSION_MANDATORY);
                        }
                        else if (_cCeritarClient == null || _cCeritarClient.CeritarClient_NRI == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.CERITAR_CLIENT_MANDATORY);
                        }
                        else if ((_blnExeIsExternalReport || _blnExeWithExternalReport) && string.IsNullOrEmpty(_strLocation_Release))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.REPORT_EXE_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strLocation_Release) & string.IsNullOrEmpty(_strLocation_Scripts) & LstSatelliteRevisions.Count == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.EXE_OR_SCRIPT_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strCreatedBy) & string.IsNullOrEmpty(_strCreatedBy))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.CREATED_BY_MANDATORY);
                        }
                        else if (!clsTTSQL.bln_ADOValid_TS("Revision", "Rev_NRI", _intRevision_NRI, "Rev_TS", _intRevision_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (_lstModifications == null || _lstModifications.Count == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.MODIFICATION_LIST_MANDATORY);
                        }
                        else if (_cCeritarClient == null || _cCeritarClient.CeritarClient_NRI == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.CERITAR_CLIENT_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strLocation_Release) & string.IsNullOrEmpty(_strLocation_Scripts) & LstSatelliteRevisions.Count == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.EXE_OR_SCRIPT_MANDATORY);
                        }
                        else if ((_blnExeIsExternalReport || _blnExeWithExternalReport) && string.IsNullOrEmpty(_strLocation_Release))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.REPORT_EXE_MANDATORY);
                        }
                        else if (!clsTTSQL.bln_ADOValid_TS("Revision", "Rev_NRI", _intRevision_NRI, "Rev_TS", _intRevision_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsTTSQL.bln_CheckReferenceIntegrity("Revision", "Rev_NRI", _intRevision_NRI))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_REFERENCE_INTEGRITY, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                        }
                        else if (!string.IsNullOrEmpty(clsTTSQL.str_ADOSingleLookUp("Rev_NRI", "Revision", "Rev_No > " + _intRevision_Number + " AND Revision.Ver_NRI = " + _cVersion.Version_NRI)))
                        {
                            mcActionResults.SetInvalid(mintMSG_CantDeleteRevisionIfNotLast, ctr_Revision.ErrorCode_Rev.CANT_DELETE_NOT_LAST_REVISION);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;
                }

                if (mcActionResults.IsValid)
                {
                    pfblnListModifications_Validate();
                }
            }
            catch (System.Exception ex)
            {
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResults;
        }

        private bool pfblnListModifications_Validate()
        {
            bool blnValidReturn = true;

            try
            {
                for (int intIndex = 0; intIndex < _lstModifications.Count & blnValidReturn; intIndex++)
                {
                    if (string.IsNullOrEmpty(_lstModifications[intIndex]))
                    {
                        mcActionResults.RowInError = intIndex + 1;
                        mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Revision.ErrorCode_Rev.MODIFICATION_LIST_MANDATORY);
                        blnValidReturn = false;

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
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

                        if (!pfblnRev_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("Revision", out _intRevision_NRI))
                        { }
                        else if (!pfblnListModifications_Save())
                        { }
                        else if (!pfblnListSatelliteRevisions_Save())
                        { }
                        else
                        {
                            mcActionResults.SetNewItem_NRI = _intRevision_NRI;

                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnRev_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("Revision", "Revision.Rev_NRI = " + _intRevision_NRI))
                        { }
                        else if (!pfblnListModifications_Save())
                        { }
                        else if (!pfblnListSatelliteRevisions_Save())
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("RevModifs", "RevModifs.Rev_NRI = " + _intRevision_NRI))
                        { }
                        else if (!pfblnListSatelliteRevisions_Save())
                        { }
                        else if (!mcSQL.bln_ADODelete("Revision", "Revision.Rev_NRI = " + _intRevision_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

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

        private bool pfblnListModifications_Save()
        {
            bool blnValidReturn = false;
            int intDML_OutParam = 0;

            try
            {
                blnValidReturn = mcSQL.bln_ADODelete("RevModifs", "Rev_NRI = " + _intRevision_NRI);

                for (int intIndex = 0; intIndex < _lstModifications.Count & blnValidReturn; intIndex++)
                {
                    blnValidReturn = false;

                    if (!mcSQL.bln_RefreshFields())
                    { }
                    else if (!mcSQL.bln_AddField("RevM_ChangeDesc", _lstModifications[intIndex], clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                    { }
                    else if (!mcSQL.bln_AddField("Rev_NRI", _intRevision_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                    { }
                    else if (!mcSQL.bln_ADOInsert("RevModifs", out intDML_OutParam))
                    { }
                    else
                    {
                        blnValidReturn = true;
                    }

                    if (!blnValidReturn) break;
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

        private bool pfblnListSatelliteRevisions_Save()
        {
            bool blnValidReturn = true;

            try
            {
                if (mintDML_Action != sclsConstants.DML_Mode.DELETE_MODE)
                {
                    for (int intIndex = 0; intIndex < _lstSatelliteRevisions.Count; intIndex++)
                    {
                        blnValidReturn = false;

                        _lstSatelliteRevisions[intIndex].SetcSQL = mcSQL;
                        _lstSatelliteRevisions[intIndex].Revision = (_lstSatelliteRevisions[intIndex].Revision == null ? new mod_Rev_Revision() : _lstSatelliteRevisions[intIndex].Revision);
                        _lstSatelliteRevisions[intIndex].Revision.Revision_NRI = _intRevision_NRI;

                        blnValidReturn = _lstSatelliteRevisions[intIndex].blnSave();

                        mcActionResults = _lstSatelliteRevisions[intIndex].ActionResults;

                        if (!blnValidReturn) break;
                    }
                }
                else
                {
                    blnValidReturn = mcSQL.bln_ADODelete("SatRevision", "SatRevision.Rev_NRI = " + _intRevision_NRI);
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

        private bool pfblnRev_AddFields()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("Rev_No", _intRevision_Number, clsTTSQL.MySQL_FieldTypes.INT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Ver_NRI", _cVersion.Version_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Rev_DtCreation", _strCreationDate, clsTTSQL.MySQL_FieldTypes.DATETIME_TYPE)) 
                { }
                else if (!mcSQL.bln_AddField("TTU_NRI", clsTTApp.GetAppController.cUser.User_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeC_NRI", _cCeritarClient.CeritarClient_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Tpl_NRI", _cTemplateSource.Template_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Rev_Location_Exe", _strLocation_Release, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Rev_Location_Scripts", _strLocation_Scripts, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Rev_CreatedBy", _strCreatedBy, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Rev_ExeIsReport", _blnExeIsExternalReport, clsTTSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Rev_ExeWithReport", _blnExeWithExternalReport, clsTTSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Rev_PreparationMode", _blnPreparationMode, clsTTSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Rev_Note", _strNote, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
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

        internal bool blnUpdateFilesLocations()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("Rev_Location_Exe", _strLocation_Release, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Rev_Location_Scripts", _strLocation_Scripts, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_ADOUpdate("Revision", "Revision.Rev_NRI = " + _intRevision_NRI))
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
