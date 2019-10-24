using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Configuration;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    /// <summary>
    /// CAR représente le préfixe des colonnes de la table "ClientAppRevision" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_CAR_ClientAppRevision
    {
        //Model attributes
        private int _intClientAppRevision_NRI;
        private int _intClientAppRevision_TS;
        private mod_CeC_CeritarClient _cCeritarClient;

        //Others
        private mod_Rev_Revision _cRevisionParent = null;

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsTTSQL mcSQL;


        #region "Properties"

        internal int ClientAppRevision_NRI
        {
            get { return _intClientAppRevision_NRI; }
            set { _intClientAppRevision_NRI = value; }
        }

        internal int ClientAppRevision_TS
        {
            get { return _intClientAppRevision_TS; }
            set { _intClientAppRevision_TS = value; }
        }

        internal mod_CeC_CeritarClient CeritarClient
        {
            get { return _cCeritarClient; }
            set { _cCeritarClient = value; }
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

        internal mod_Rev_Revision RevisionParent
        {
            get { return _cRevisionParent; }
            set { _cRevisionParent = value; }
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

                        if (_cCeritarClient.CeritarClient_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, Ceritar.CVS.Controllers.ctr_Revision.ErrorCode_Rev.CLIENT_NAME_MANDATORY);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (_cCeritarClient.CeritarClient_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, Ceritar.CVS.Controllers.ctr_Revision.ErrorCode_Rev.CLIENT_NAME_MANDATORY);
                        }
                        else if (_cRevisionParent == null || _cRevisionParent.Revision_NRI <= 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION);
                        }
                        else if (!clsTTSQL.bln_ADOValid_TS("ClientAppRevision", "CAR_NRI", _intClientAppRevision_NRI, "CAR_TS", _intClientAppRevision_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsTTSQL.bln_CheckReferenceIntegrity("ClientAppRevision", "CAR_NRI", _intClientAppRevision_NRI))
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

                        if (!pfblnCAR_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("ClientAppRevision", out _intClientAppRevision_NRI))
                        { }
                        else
                        {
                            mcActionResults.SetNewItem_NRI = _intClientAppRevision_NRI;
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnCAR_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("ClientAppRevision", "ClientAppRevision.CAR_NRI = " + _intClientAppRevision_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("ClientAppRevision", "ClientAppRevision.CAR_NRI = " + _intClientAppRevision_NRI))
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

        private bool pfblnCAR_AddFields()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("CeC_NRI", _cCeritarClient.CeritarClient_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Rev_NRI", _cRevisionParent.Revision_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
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
