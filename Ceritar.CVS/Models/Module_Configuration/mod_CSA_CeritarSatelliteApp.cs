using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.CVS.Controllers;

namespace Ceritar.CVS.Models.Module_Configuration
{
    /// <summary>
    /// Cette classe représente le modèle objet d'une application satellite de Ceritar.
    /// CSA représente le préfixe des colonnes de la table "CerSatApp" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_CSA_CeritarSatelliteApp
    {
        //Model attributes
        private int _intCeritarSatelliteApp_NRI;
        private int _intCeritarSatelliteApp_TS;
        private string _strName;
        private string _strExportFolderName;
        private int _intCeritarApp_NRI;
        private bool _blnExeIsFolder;
        private bool _blnExePerCustomer;

        //Messages
        private const int mintMSG_InvalidName = 32;

        //Working variables
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsTTSQL mcSQL;


#region "Properties"

        internal int CeritarSatelliteApp_NRI
        {
            get { return _intCeritarSatelliteApp_NRI; }
            set { _intCeritarSatelliteApp_NRI = value; }
        }

        internal int CeritarSatelliteApp_TS
        {
            get { return _intCeritarSatelliteApp_TS; }
            set { _intCeritarSatelliteApp_TS = value; }
        }

        internal string Name
        {
            get { return _strName; }
            set { _strName = value; }
        }

        internal string ExportFolderName
        {
            get { return _strExportFolderName; }
            set { _strExportFolderName = value; }
        }

        internal int CeritarApp_NRI
        {
            get { return _intCeritarApp_NRI; }
            set { _intCeritarApp_NRI = value; }
        }

        internal bool ExeIsFolder
        {
            get { return _blnExeIsFolder; }
            set { _blnExeIsFolder = value; }
        }

        /// <summary>
        /// Indique que chaque client possède un Release spécifique
        /// </summary>
        internal bool ExePerCustomer
        {
            get { return _blnExePerCustomer; }
            set { _blnExePerCustomer = value; }
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

                        if (string.IsNullOrEmpty(_strName))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_CeritarApplication.ErrorCode_CSA.NAME_MANDATORY);
                        }
                        else if (Path.GetInvalidFileNameChars().Where(x => _strName.Contains(x)).Count() > 0 || _strName == "con")
                        {
                            mcActionResults.SetInvalid(mintMSG_InvalidName, ctr_CeritarApplication.ErrorCode_CeA.SATELLITE_NAME_INVALID);
                        }
                        else if (string.IsNullOrEmpty(_strExportFolderName))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_CeritarApplication.ErrorCode_CSA.KIT_FOLDER_NAME_MANDATORY);
                        }
                        else if (Path.GetInvalidFileNameChars().Where(x => _strExportFolderName.Contains(x)).Count() > 0 || _strExportFolderName == "con")
                        {
                            mcActionResults.SetInvalid(mintMSG_InvalidName, ctr_CeritarApplication.ErrorCode_CeA.SATELLITE_NAME_INVALID);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsTTSQL.bln_CheckReferenceIntegrity("CerSatApp", "CSA_NRI", _intCeritarSatelliteApp_NRI))
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

                        if (!pfblnCeA_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("CerSatApp", out _intCeritarSatelliteApp_NRI))
                        { }
                        else
                        {
                            mcActionResults.SetNewItem_NRI = _intCeritarSatelliteApp_NRI;
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnCeA_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("CerSatApp", "CerSatApp.CSA_NRI = " + _intCeritarSatelliteApp_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("CerSatApp", "CerSatApp.CSA_NRI = " + _intCeritarSatelliteApp_NRI))
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
                else if (!mcSQL.bln_AddField("CSA_Name", _strName, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CSA_KitFolderName", _strExportFolderName, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeA_NRI", _intCeritarApp_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CSA_ExeIsFolder", _blnExeIsFolder, clsTTSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CSA_ExePerCustomer", _blnExePerCustomer, clsTTSQL.MySQL_FieldTypes.BIT_TYPE))
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
