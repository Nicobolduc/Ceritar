﻿using System;
using System.Collections.Generic;
using Ceritar.CVS.Controllers;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.CVS.Models.Module_Template;
using Ceritar.CVS.Models.Module_Configuration;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    internal class mod_Ver_Version
    {
        //Model attributes
        private int _intVersion_NRI;
        private int _intVersion_TS;
        private string _strCompiledBy;
        private ushort _intVersionNo;
        private string _strCreationDate;
        private string _strLocation_APP_CHANGEMENT;
        private string _strLocation_Release;
        private string _strLocation_TTApp;      
        private mod_TTU_User _cCreatedByUser;
        private List<mod_Rev_Revision> _lstRevisions; //TODO unused
        private mod_CeA_CeritarApplication _cApplication;
        private mod_Tpl_HierarchyTemplate _cTemplateSource;
        private List<int> _lstClientsUsing;

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsSQL mcSQL;

        //Working variables


#region "Properties"

        internal int Version_NRI
        {
            get { return _intVersion_NRI; }
            set { _intVersion_NRI = value; }
        }

        internal int Version_TS
        {
            get { return _intVersion_TS; }
            set { _intVersion_TS = value; }
        }

        internal string CompiledBy
        {
            get { return _strCompiledBy; }
            set { _strCompiledBy = value; }
        }

        internal string Location_APP_CHANGEMENT
        {
            get { return _strLocation_APP_CHANGEMENT; }
            set { _strLocation_APP_CHANGEMENT = value; }
        }

        internal string Location_Release
        {
            get { return _strLocation_Release; }
            set { _strLocation_Release = value; }
        }

        internal string Location_TTApp
        {
            get { return _strLocation_TTApp; }
            set { _strLocation_TTApp = value; }
        }

        internal ushort VersionNo
        {
            get { return _intVersionNo; }
            set { _intVersionNo = value; }
        }

        internal string GetCreationDate
        {
            get { return _strCreationDate; }
        }

        internal mod_TTU_User GetCreatedByUser
        {
            get { return _cCreatedByUser; }
        }

        internal List<mod_Rev_Revision> LstRevisions
        {
            get { return _lstRevisions; }
            set { _lstRevisions = value; }
        }

        internal mod_CeA_CeritarApplication Application
        {
            get { return _cApplication; }
            set { _cApplication = value; }
        }

        internal mod_Tpl_HierarchyTemplate TemplateSource
        {
            get { return _cTemplateSource; }
            set { _cTemplateSource = value; }
        }

        internal List<int> LstClientsUsing
        {
            get { return (_lstClientsUsing == null ? new List<int>() : _lstClientsUsing); }
            set { _lstClientsUsing = value; }
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
                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (string.IsNullOrEmpty(_strCompiledBy))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.COMPILED_BY_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strLocation_APP_CHANGEMENT))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.APP_CHANGEMENT_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strLocation_Release))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.RELEASE_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strLocation_TTApp))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.TTAPP_MANDATORY);
                        }
                        else if (_lstClientsUsing == null || _lstClientsUsing.Count == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.CLIENTS_LIST_MANDATORY);
                        }
                        else if (_intVersionNo <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.VERSION_NO_MANDATORY);
                        }
                        else if (_cApplication == null || _cApplication.CeritarApplication_NRI == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.CERITAR_APP_MANDATORY);
                        }
                        else if (_cTemplateSource == null || _cTemplateSource.Template_NRI == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.TEMPLATE_MANDATORY);
                        }
                        else if (!clsSQL.bln_ADOValid_TS("Version", "Ver_NRI", _intVersion_NRI, "Ver_TS", _intVersion_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsSQL.bln_CheckReferenceIntegrity("Ver_NRI", "Ver_NRI", _intVersion_NRI))
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

                        if (!pfblnVer_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("Version", out _intVersion_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnVer_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("Version", "Version.Ver_NRI = " + _intVersion_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("Version", "Version.Ver_NRI = " + _intVersion_NRI))
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

        private bool pfblnVer_AddFields()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("Ver_CompiledBy", _strCompiledBy, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Ver_No", _intVersionNo, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Ver_DtCreation", System.DateTime.Now, clsSQL.MySQL_FieldTypes.DATETIME_TYPE))
                { }
                else if (!mcSQL.bln_AddField("TTU_NRI", clsApp.GetAppController.cUser.GetUser_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeA_NRI", _cApplication.CeritarApplication_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE ))
                { }
                else if (!mcSQL.bln_AddField("Tpl_NRI", _cTemplateSource.Template_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
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
