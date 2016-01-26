using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers;
using System;

namespace Ceritar.CVS.Models.Module_Configuration
{
    /// <summary>
    /// Cette classe représente le modèle objet d'une application de Ceritar.
    /// CeA représente le préfixe des colonnes de la table "CerApp" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_CeA_CeritarApplication
    {
        //Model attributes
        private int _intCeritarApplication_NRI;
        private int _intCeritarApplication_TS;
        private string _strName;
        private string _strDescription;
        private AppDomain _domain_NRI;
        private List<string> _lstModules;
        private List<mod_CSA_CeritarSatelliteApp> _lstSatelliteApps;

        public enum AppDomain
        {
            Tranport = 1,
            Entreposage = 2,
            Distribution = 3,
            SGSP = 4,
            Web = 5,   
            Interne = 6
        }

        //mod_IBase
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

        internal string Name
        {
            get { return _strName; }
            set { _strName = value; }
        }

        internal string Description
        {
            get { return _strDescription; }
            set { _strDescription = value; }
        }

        internal AppDomain Domaine_NRI
        {
            get { return _domain_NRI; }
            set { _domain_NRI = value; }
        }

        internal List<string> LstModules
        {
            get { return _lstModules; }
            set { _lstModules = value; }
        }

        internal List<mod_CSA_CeritarSatelliteApp> LstCeritarSatelliteApps
        {
            get 
            {
                if (_lstSatelliteApps == null) _lstSatelliteApps = new List<mod_CSA_CeritarSatelliteApp>();

                return _lstSatelliteApps; 
            }
            set { _lstSatelliteApps = value; }
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

                        if (string.IsNullOrEmpty(_strName))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_CeritarApplication.ErrorCode_CeA.NAME_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strDescription))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_CeritarApplication.ErrorCode_CeA.DESCRIPTION_MANDATORY);
                        }
                        else if (_lstModules == null || _lstModules.Count == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_CeritarApplication.ErrorCode_CeA.MODULES_LIST_MANDATORY);
                        }
                        else if (_domain_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_CeritarApplication.ErrorCode_CeA.DOMAIN_MANDATORY);
                        }
                        else if(!clsSQL.bln_ADOValid_TS("CerApp", "CeA_NRI", _intCeritarApplication_NRI, "CeA_TS", _intCeritarApplication_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else if (!pfblnListModules_Validate())
                        {
                            //Invalid
                        }
                        else if (!pfblnListAppSatellites_Validate())
                        {
                            //Invalid
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsSQL.bln_CheckReferenceIntegrity("CerApp", "CeA_NRI", _intCeritarApplication_NRI))
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

                        if (! pfblnCeA_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("CerApp", out _intCeritarApplication_NRI))
                        { }
                        else if (!pfblnListModules_Save()) 
                        { }
                        else if (!pfblnListSatelliteApps_Save())
                        { } 
                        else 
                        {
                            mcActionResults.SetNewItem_NRI = _intCeritarApplication_NRI;

                            blnValidReturn = true;
                        }
 
                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnCeA_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("CerApp", "CerApp.CeA_NRI = " + _intCeritarApplication_NRI))
                        { }
                        else if (!pfblnListModules_Save())
                        { }
                        else if (!pfblnListSatelliteApps_Save())
                        { } 
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("AppModule", "AppModule.CeA_NRI = " + _intCeritarApplication_NRI))
                        { }
                        if (!mcSQL.bln_ADODelete("CerSatApp", "CerSatApp.CeA_NRI = " + _intCeritarApplication_NRI))
                        { }
                        else if (!mcSQL.bln_ADODelete("CerApp", "CerApp.CeA_NRI = " + _intCeritarApplication_NRI))
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

        private bool pfblnCeA_AddFields()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("CeA_Name", _strName, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeA_Desc", _strDescription, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("ApD_NRI", (int)_domain_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else {
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

        private bool pfblnListModules_Save()
        {
            bool blnValidReturn = false;
            int intDML_OutParam = 0;

            try
            {
                blnValidReturn = mcSQL.bln_ADODelete("AppModule", "CeA_NRI = " + _intCeritarApplication_NRI);
                
                for (int intIndex = 0; intIndex < _lstModules.Count & blnValidReturn; intIndex++)
                {
                    blnValidReturn = false;

                    if (!mcSQL.bln_RefreshFields())
                    { }
                    else if (!mcSQL.bln_AddField("ApM_Desc", _lstModules[intIndex], clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                    { }
                    else if (!mcSQL.bln_AddField("CeA_NRI", _intCeritarApplication_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                    { }
                    else if (!mcSQL.bln_ADOInsert("AppModule", out intDML_OutParam))
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

        private bool pfblnListSatelliteApps_Save()
        {
            bool blnValidReturn = true;

            try
            {
                for (int intIndex = 0; intIndex < _lstSatelliteApps.Count; intIndex++)
                {
                    _lstSatelliteApps[intIndex].SetcSQL = mcSQL;

                    blnValidReturn = _lstSatelliteApps[intIndex].blnSave();

                    if (!blnValidReturn)
                    {
                        mcActionResults = _lstSatelliteApps[intIndex].ActionResults;

                        break;
                    }
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

        private bool pfblnListModules_Validate()
        {
            bool blnValidReturn = true;

            try
            {
                for (int intIndex = 0; intIndex < _lstModules.Count & blnValidReturn; intIndex++)
                {
                    if (string.IsNullOrEmpty(_lstModules[intIndex]))
                    {
                        mcActionResults.RowInError = intIndex + 1;
                        mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_CeritarApplication.ErrorCode_CeA.MODULES_LIST_MANDATORY);
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

        private bool pfblnListAppSatellites_Validate()
        {
            bool blnValidReturn = true;

            try
            {
                for (int intIndex = 0; intIndex < _lstSatelliteApps.Count; intIndex++)
                {
                    mcActionResults = _lstSatelliteApps[intIndex].Validate();

                    if (!mcActionResults.IsValid)
                    {
                        mcActionResults.RowInError = intIndex + 1;
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
    }
}
