using System;
using System.IO;
using System.Collections.Generic;
using Ceritar.CVS.Controllers;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.CVS.Models.Module_Template;
using Ceritar.CVS.Models.Module_Configuration;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    /// <summary>
    /// Cette classe représente le modèle objet d'une version d'application.
    /// Ver représente le préfixe des colonnes de la table "Version" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_Ver_Version
    {
        //Model attributes
        private int _intVersion_NRI;
        private int _intVersion_TS;
        private string  _strCompiledBy;
        private ushort _intVersionNo;
        private string _strCreationDate;
        private string _strLocation_APP_CHANGEMENT;
        private string _strLocation_Release;
        private string _strLocation_CaptionsAndMenus;
        private bool _blnIsDemo;
        private mod_TTU_User _cCreatedByUser;
        private List<mod_Rev_Revision> _lstRevisions; //TODO unused
        private mod_CeA_CeritarApplication _cCerApplication;
        private mod_Tpl_HierarchyTemplate _cTemplateSource;
        private List<mod_CAV_ClientAppVersion> _lstClientsUsing;
        private List<mod_CSV_ClientSatVersion> _lstClientSatelliteApps;

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsSQL mcSQL;

        //Messages
        private const int mintMSG_VersionNo_UniqueAndBiggerPrevious = 18;
        private const int mintMSG_CantDeleteVersionIfUsed = 22;
        private const int mintMSG_CantInstallDemoInProd = 24;
        private const int mintMSG_SCRIPS_NOT_FOUND = 29;

        //Working variables
        private bool mblnIncludeScriptsOnRefresh = false;


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

        internal string Location_CaptionsAndMenus
        {
            get { return _strLocation_CaptionsAndMenus; }
            set { _strLocation_CaptionsAndMenus = value; }
        }

        internal ushort VersionNo
        {
            get { return _intVersionNo; }
            set { _intVersionNo = value; }
        }

        internal string CreationDate
        {
            get { return _strCreationDate; }
            set { _strCreationDate = value; }
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

        internal mod_CeA_CeritarApplication CerApplication
        {
            get { return _cCerApplication; }
            set { _cCerApplication = value; }
        }

        internal mod_Tpl_HierarchyTemplate TemplateSource
        {
            get { return _cTemplateSource; }
            set { _cTemplateSource = value; }
        }

        internal List<mod_CAV_ClientAppVersion> LstClientsUsing
        {
            get 
            { 
                if (_lstClientsUsing == null){

                    _lstClientsUsing = new List<mod_CAV_ClientAppVersion>();
                }

                return _lstClientsUsing; 
            }
            set { _lstClientsUsing = value; }
        }

        internal List<mod_CSV_ClientSatVersion> LstClientSatelliteApps
        {
            get 
            {
                if (_lstClientSatelliteApps == null)
                {

                    _lstClientSatelliteApps = new List<mod_CSV_ClientSatVersion>();
                }

                return _lstClientSatelliteApps;
            }
            set { _lstClientSatelliteApps = value; }
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

        internal bool IsDemo
        {
            get { return _blnIsDemo; }
            set { _blnIsDemo = value; }
        }

        internal bool IncludeScriptsOnRefresh
        {
            get { return mblnIncludeScriptsOnRefresh; }
            set { mblnIncludeScriptsOnRefresh = value; }
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

                        if (string.IsNullOrEmpty(_strCompiledBy))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.COMPILED_BY_MANDATORY);
                        }
                        else if (_lstClientsUsing == null || _lstClientsUsing.Count == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.CLIENTS_LIST_MANDATORY);
                        }
                        else if (_intVersionNo <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.VERSION_NO_MANDATORY);
                        }
                        else if (_cCerApplication == null || _cCerApplication.CeritarApplication_NRI == 0)
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
                            //TODO: Il faut valider que le numero n'est pas plus petit pour un client donné ou les clients inclus
                        //else if (!string.IsNullOrEmpty(clsSQL.str_ADOSingleLookUp("Ver_NRI", "Version", "CeA_NRI = " + _cCerApplication.CeritarApplication_NRI +  " AND Ver_No >= " + clsApp.GetAppController.str_FixStringForSQL(_intVersionNo.ToString()))))
                        //{
                        //    mcActionResults.SetInvalid(mintMSG_VersionNo_UniqueAndBiggerPrevious, ctr_Version.ErrorCode_Ver.VERSION_NO_UNIQUE_AND_BIGGER_PREVIOUS);
                        //}
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (string.IsNullOrEmpty(_strCompiledBy))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.COMPILED_BY_MANDATORY);
                        }
                        else if (_lstClientsUsing == null || _lstClientsUsing.Count == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.CLIENTS_LIST_MANDATORY);
                        }
                        else if (_intVersionNo <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.VERSION_NO_MANDATORY);
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

                        if (!string.IsNullOrEmpty(clsSQL.str_ADOSingleLookUp("CAV_NRI", "ClientAppVersion", "(CAV_Installed = 1 OR CAV_IsCurrentVersion = 1) AND CeA_NRI = " + _cCerApplication.CeritarApplication_NRI + " AND Ver_NRI = " + _intVersion_NRI)))
                        {
                            mcActionResults.SetInvalid(mintMSG_CantDeleteVersionIfUsed, ctr_Version.ErrorCode_Ver.CANT_DELETE_USED_VERSION);
                        }
                        else if (!clsSQL.bln_CheckReferenceIntegrity("Ver_NRI", "Ver_NRI", _intVersion_NRI))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_REFERENCE_INTEGRITY, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;
                }

                pfblnListClients_Validate();
            }
            catch (System.Exception ex)
            {
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResults;
        }

        internal bool blnValidateHierarchyBuildFiles()
        {
            bool blnValidReturn = false;

            try
            {
                switch (mintDML_Action)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:

                        if (_blnIsDemo)
                        {
                            blnValidReturn = true;
                        }
                        else if (string.IsNullOrEmpty(_strLocation_APP_CHANGEMENT) || !File.Exists(_strLocation_APP_CHANGEMENT))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.APP_CHANGEMENT_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strLocation_CaptionsAndMenus) || !File.Exists(_strLocation_CaptionsAndMenus))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.TTAPP_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strLocation_Release) || !Directory.Exists(_strLocation_Release))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.RELEASE_MANDATORY);
                        }
                        else
                        {
                            if (LstClientsUsing != null)
                            {
                                blnValidReturn = true;

                                foreach (mod_CAV_ClientAppVersion client in LstClientsUsing)
                                {
                                    if (!string.IsNullOrEmpty(_cCerApplication.ExternalReportAppName) && (string.IsNullOrEmpty(client.LocationReportExe) || !File.Exists(client.LocationReportExe)))
                                    {
                                        mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.REPORT_MANDATORY);
                                        blnValidReturn = false;
                                    }

                                    if (string.IsNullOrEmpty(client.LocationScriptsRoot) || !Directory.Exists(client.LocationScriptsRoot))
                                    {
                                        mcActionResults.SetInvalid(mintMSG_SCRIPS_NOT_FOUND, ctr_Version.ErrorCode_Ver.SCRIPTS_MANDATORY, client.CeritarClient.CompanyName);
                                        blnValidReturn = false;
                                    }

                                    if (!blnValidReturn) break;
                                }
                            }
                            else
                            {
                                blnValidReturn = true;
                            }
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!string.IsNullOrEmpty(_strLocation_APP_CHANGEMENT) && !File.Exists(_strLocation_APP_CHANGEMENT))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.APP_CHANGEMENT_MANDATORY);
                        }
                        else if (!string.IsNullOrEmpty(_strLocation_CaptionsAndMenus) && !File.Exists(_strLocation_CaptionsAndMenus))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.TTAPP_MANDATORY);
                        }
                        else if (!string.IsNullOrEmpty(_strLocation_Release) && !Directory.Exists(_strLocation_Release))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.RELEASE_MANDATORY);
                        } 
                        else
                        {
                            if (LstClientsUsing != null)
                            {
                                blnValidReturn = true;

                                for (int intIndex = 0; intIndex < LstClientsUsing.Count; intIndex++)
                                {
                                    if (!string.IsNullOrEmpty(LstClientsUsing[intIndex].LocationReportExe) && !File.Exists(LstClientsUsing[intIndex].LocationReportExe))
                                    {
                                        mcActionResults.RowInError = intIndex + 1;
                                        mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Version.ErrorCode_Ver.REPORT_MANDATORY);
                                        blnValidReturn = false;
                                    }

                                    if (blnValidReturn && (string.IsNullOrEmpty(LstClientsUsing[intIndex].LocationScriptsRoot) || !Directory.Exists(LstClientsUsing[intIndex].LocationScriptsRoot)))
                                    {
                                        //mcActionResults.SetInvalid(mintMSG_SCRIPS_NOT_FOUND, ctr_Version.ErrorCode_Ver.SCRIPTS_MANDATORY, LstClientsUsing[intIndex].CeritarClient.CompanyName);
                                        //blnValidReturn = false;
                                        //TODO: A revoir
                                    }

                                    if (!blnValidReturn) break;
                                }
                            }
                            else
                            {
                                blnValidReturn = true;
                            }
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
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            if (blnValidReturn) mcActionResults.SetValid();

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

                        if (!pfblnVer_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("Version", out _intVersion_NRI))
                        { }
                        else if (!pfblnListClients_Save())
                        { }
                        else if (!pfblnListClientSatelliteApps_Save())
                        { }
                        else
                        {
                            mcActionResults.SetNewItem_NRI = _intVersion_NRI;

                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnVer_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("Version", "Version.Ver_NRI = " + _intVersion_NRI))
                        { }
                        else if (!pfblnListClients_Save())
                        { }
                        else if (!pfblnListClientSatelliteApps_Save())
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!pfblnListClients_Save())
                        { }
                        else if (!pfblnListClientSatelliteApps_Save())
                        { }
                        else if (!mcSQL.bln_ADODelete("Version", "Version.Ver_NRI = " + _intVersion_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    default:

                        if (!pfblnListClients_Save())
                        { }
                        else if (!pfblnListClientSatelliteApps_Save())
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
                else if (!mcSQL.bln_AddField("Ver_DtCreation", _strCreationDate, clsSQL.MySQL_FieldTypes.DATETIME_TYPE)) //TODO
                { }
                else if (!mcSQL.bln_AddField("TTU_NRI", clsApp.GetAppController.cUser.GetUser_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeA_NRI", _cCerApplication.CeritarApplication_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE ))
                { }
                else if (!mcSQL.bln_AddField("Tpl_NRI", _cTemplateSource.Template_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Ver_CaptionsAndMenus_Location", _strLocation_CaptionsAndMenus, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Ver_IsDemo", _blnIsDemo, clsSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else
                {
                    if (DML_Action == sclsConstants.DML_Mode.INSERT_MODE)
                    {
                        mcSQL.bln_AddField("Ver_Release_Location", _strLocation_Release, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE);
                        mcSQL.bln_AddField("Ver_AppChange_Location", _strLocation_APP_CHANGEMENT, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE);
                    }

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

        private bool pfblnListClients_Validate()
        {
            bool blnValidReturn = false;

            try
            {
                if (mcActionResults.IsValid)
                {
                    for (int intIndex = 0; intIndex < LstClientsUsing.Count; intIndex++)
                    {
                        mcActionResults = LstClientsUsing[intIndex].Validate();

                        if (!mcActionResults.IsValid)
                        {
                            mcActionResults.RowInError = intIndex + 1;

                            break;
                        }
                        else if (_blnIsDemo && (LstClientsUsing[intIndex].Installed | LstClientsUsing[intIndex].IsCurrentVersion))
                        {
                            mcActionResults.SetInvalid(mintMSG_CantInstallDemoInProd, ctr_Version.ErrorCode_Ver.DEMO_CANT_BE_INSTALLED);

                            break;
                        }
                    }

                    blnValidReturn = mcActionResults.IsValid;
                }
                else
                {
                    //Do nothing
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

        private bool pfblnListClients_Save()
        {
            bool blnValidReturn = false;

            try
            {
                for (int intIndex = 0; intIndex < _lstClientsUsing.Count; intIndex++)
                {
                    _lstClientsUsing[intIndex].SetcSQL = mcSQL;
                    _lstClientsUsing[intIndex].Version_NRI = _intVersion_NRI;
                    _lstClientsUsing[intIndex].DML_Action = (mintDML_Action == sclsConstants.DML_Mode.DELETE_MODE ? sclsConstants.DML_Mode.DELETE_MODE : _lstClientsUsing[intIndex].DML_Action);

                    blnValidReturn = _lstClientsUsing[intIndex].blnSave();

                    mcActionResults = _lstClientsUsing[intIndex].ActionResults;

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

        private bool pfblnListClientSatelliteApps_Save()
        {
            bool blnValidReturn = false;

            try
            {
                if (mintDML_Action != sclsConstants.DML_Mode.DELETE_MODE)
                {
                    for (int intIndex = 0; intIndex < _lstClientSatelliteApps.Count; intIndex++)
                    {
                        _lstClientSatelliteApps[intIndex].SetcSQL = mcSQL;
                        _lstClientSatelliteApps[intIndex].Version_NRI = _intVersion_NRI;

                        blnValidReturn = _lstClientSatelliteApps[intIndex].blnSave();

                        mcActionResults = _lstClientSatelliteApps[intIndex].ActionResults;

                        if (!blnValidReturn) break;
                    }
                }
                else
                {
                    blnValidReturn = mcSQL.bln_ADODelete("ClientSatVersion", "ClientSatVersion.Ver_NRI = " + _intVersion_NRI);
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

        internal bool blnLocationsUpdate()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("Ver_AppChange_Location", _strLocation_APP_CHANGEMENT, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Ver_Release_Location", _strLocation_Release, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Ver_CaptionsAndMenus_Location", _strLocation_CaptionsAndMenus, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_ADOUpdate("Version", "Version.Ver_NRI = " + _intVersion_NRI))
                { }
                else
                {
                    blnValidReturn = true;

                    foreach (mod_CAV_ClientAppVersion cCAV in LstClientsUsing)
                    {
                        cCAV.SetcSQL = mcSQL;

                        blnValidReturn = cCAV.blnLocationUpdate();

                        if (!blnValidReturn) break;
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
    }
}
