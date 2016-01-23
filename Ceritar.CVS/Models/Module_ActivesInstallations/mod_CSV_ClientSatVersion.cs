using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Configuration;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    /// <summary>
    /// Cette classe représente le modèle objet d'une application satellite de Ceritar pour un client donné.
    /// CSV représente le préfixe des colonnes de la table "ClientSatVersion" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_CSV_ClientSatVersion
    {
        //Model attributes
        private int _intClientSatVersion_NRI;
        private int _intVersion_NRI;
        private mod_CSA_CeritarSatelliteApp _cCeritarSatelliteApp;
        private mod_CeC_CeritarClient _cCeritarClient;
        private string _strLocation_Exe;

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsSQL mcSQL;


#region "Properties"

        internal int ClientSatVersion_NRI
        {
            get { return _intClientSatVersion_NRI; }
            set { _intClientSatVersion_NRI = value; }
        }

        internal int Version_NRI
        {
            get { return _intVersion_NRI; }
            set { _intVersion_NRI = value; }
        }

        internal mod_CeC_CeritarClient CeritarClient
        {
            get { return _cCeritarClient; }
            set { _cCeritarClient = value; }
        }

        internal mod_CSA_CeritarSatelliteApp CeritarSatelliteApp
        {
            get { return _cCeritarSatelliteApp; }
            set { _cCeritarSatelliteApp = value; }
        }

        internal string Location_Exe
        {
            get { return _strLocation_Exe; }
            set { _strLocation_Exe = value; }
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

        internal clsActionResults ActionResults
        {
            get { return mcActionResults; }
        }

#endregion


        internal clsActionResults Validate()
        {
            try
            {
                mcActionResults.SetValid();
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

                        if (!pfblnCSV_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("ClientSatVersion", out _intClientSatVersion_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnCSV_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("ClientSatVersion", "ClientSatVersion.CSV_NRI = " + _intClientSatVersion_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("ClientSatVersion", "ClientSatVersion.CSV_NRI = " + _intClientSatVersion_NRI))
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

        private bool pfblnCSV_AddFields()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("Ver_NRI", _intVersion_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeC_NRI", _cCeritarClient.CeritarClient_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CSA_NRI", _cCeritarSatelliteApp.CeritarSatelliteApp_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CSV_Exe_Location", _strLocation_Exe, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
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
