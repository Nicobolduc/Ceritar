using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using System;

namespace Ceritar.CVS.Models.Module_Configuration
{
    internal class mod_CeritarApplication : mod_IBase
    {
        //Model attributes
        private int _intCeritarApplication_NRI;
        private string _strName;
        private string _strDescription;
        private int _domain_NRI;
        private List<string> _lstModules;

        public enum AppDomain
        {
            Tranport = 1,
            Entreposage = 2,
            Distribution = 3,
            SGSP = 4,
            Web = 5,   
            Interne = 6
        }

        public enum ErrorCode
        {
            NAME_MANDATORY = 1,
            DESCRIPTION_MANDATORY = 2,
            DOMAIN_MANDATORY = 3,
            MODULES_LIST_MANDATORY = 4
        }

        //Working variables
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintAction;
        private clsSQL mcSQL;


#region "Properties"

        public int CeritarApplication_NRI
        {
            get { return _intCeritarApplication_NRI; }
            set { _intCeritarApplication_NRI = value; }
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

        internal int Domaine_NRI
        {
            get { return _domain_NRI; }
            set { _domain_NRI = value; }
        }

        internal List<string> LstModules
        {
            get { return _lstModules; }
            set { _lstModules = value; }
        }

        clsActionResults mod_IBase.ActionResults
        {
            get
            {
                return mcActionResults;
            }
        }

        internal sclsConstants.DML_Mode Action
        {
            set { mintAction = value; }
        }

#endregion


        internal clsActionResults Validate()
        {
            try
            {
                switch (mintAction)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:
                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (string.IsNullOrEmpty(_strName))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ErrorCode.NAME_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strDescription))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ErrorCode.DESCRIPTION_MANDATORY);
                        }
                        else if (_lstModules == null || _lstModules.Count == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ErrorCode.MODULES_LIST_MANDATORY);
                        }
                        else if (_domain_NRI == null)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ErrorCode.DOMAIN_MANDATORY);
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

        internal clsActionResults Save()
        {
            bool blnValidReturn = false;

            try
            {
                mcSQL = new clsSQL();
                mcSQL.bln_BeginTransaction();

                switch (mintAction)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:

                        if (! pfblnCeA_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("CerApp", out _intCeritarApplication_NRI))
                        { }
                        else if (!pfblnListModules_Save()) 
                        { } 
                        else {
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
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("AppModule", "AppModule.CeA_NRI = " + _intCeritarApplication_NRI))
                        { }
                        else if (!mcSQL.bln_ADODelete("CerApp", "CerApp.CeA_NRI = " + _intCeritarApplication_NRI))
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

                mcSQL.bln_EndTransaction(blnValidReturn);
                mcSQL = null;
            }

            return mcActionResults;
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
                else if (!mcSQL.bln_AddField("ApD_NRI", _domain_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
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
            }

            return blnValidReturn;
        }

        private bool pfblnListModules_Save()
        {
            bool blnValidReturn = false;
            int intDML_OutParam = 0;

            try
            {
                for (int intIndex = 0; intIndex < _lstModules.Count; intIndex++)
                {
                    blnValidReturn = false;

                    if (!mcSQL.bln_RefreshFields())
                    { }
                    else if (!mcSQL.bln_AddField("ApM_Desc", _strDescription, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                    { }
                    else if (!mcSQL.bln_AddField("CeA_NRI", _intCeritarApplication_NRI, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
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
            }

            return blnValidReturn;
        }
    }
}
