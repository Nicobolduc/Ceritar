using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Configuration;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    /// <summary>
    /// Cette classe représente le modèle objet pour une application satellite d'une révision.
    /// SRe représente le préfixe des colonnes de la table "SatRevision" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_SRe_SatelliteRevision
    {
        //Model attributes
        private int _intSatRevision_NRI;
        private mod_Rev_Revision _cRevision;
        private mod_CSA_CeritarSatelliteApp _cCeritarSatelliteApp;
        private string _strLocation_Exe;

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsTTSQL mcSQL;


#region "Properties"

        internal int SatRevision_NRI
        {
            get { return _intSatRevision_NRI; }
            set { _intSatRevision_NRI = value; }
        }

        internal mod_Rev_Revision Revision
        {
            get { return _cRevision; }
            set { _cRevision = value; }
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

        internal clsTTSQL SetcSQL
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
                mcActionResults.SetDefault();

                switch (mintDML_Action)
                {
                    case sclsConstants.DML_Mode.NO_MODE:

                        mcActionResults.SetValid();

                        break;

                    case sclsConstants.DML_Mode.INSERT_MODE:

                        mcActionResults.SetValid();

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        mcActionResults.SetValid();

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE: //TODO

                        if (!clsTTSQL.bln_CheckReferenceIntegrity("SRe_NRI", "SRe_NRI", _intSatRevision_NRI))
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

                        if (!pfblnSRe_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("SatRevision", out _intSatRevision_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnSRe_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("SatRevision", "SatRevision.SRe_NRI = " + _intSatRevision_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("SatRevision", "SatRevision.SRe_NRI = " + _intSatRevision_NRI))
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

        private bool pfblnSRe_AddFields()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("Rev_NRI", _cRevision.Revision_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CSA_NRI", _cCeritarSatelliteApp.CeritarSatelliteApp_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("SRe_Exe_Location", _strLocation_Exe, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
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
