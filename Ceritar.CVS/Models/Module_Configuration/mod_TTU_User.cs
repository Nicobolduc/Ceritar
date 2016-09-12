using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers;
using System;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    /// <summary>
    /// Cette classe représente le modèle objet d'un usager de l'application.
    /// TTU représente le préfixe des colonnes de la table "TTUser" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_TTU_User
    {
        //Model attributes
        private int _intUser_NRI;
        private int _intUser_TS;
        private int? _intCeritarApp_NRI_Default = null;
        private bool? _blnIsActive = null;
        private string _strUserCode = null;
        private string _strPassword = null;
        private string _strFirstname = null;
        private string _strLastname = null;
        private string _strEmail = null;
        private mod_TTG_UerGroup _cUserGroup = null; //TODO Unused

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsSQL mcSQL;

        //Working variables

        //Messages
        private int mintMSG_InvalidEmailFormat = 41;


#region "Properties"

        internal int User_NRI
        {
            get { return _intUser_NRI; }
            set { _intUser_NRI = value; }
        }

        internal int User_TS
        {
            get { return _intUser_TS; }
            set { _intUser_TS = value; }
        }

        internal int? CeritarApp_NRI_Default
        {
            get { return _intCeritarApp_NRI_Default; }
            set { _intCeritarApp_NRI_Default = value; }
        }

        internal bool IsActive
        {
            get { return _blnIsActive.Value; }
            set { _blnIsActive = value; }
        }

        internal string UserCode
        {
            get { return _strUserCode; }
            set { _strUserCode = value; }
        }

        internal string Password
        {
            get { return _strPassword; }
            set { _strPassword = value; }
        }

        internal string Firstname
        {
            get { return _strFirstname; }
            set { _strFirstname = value; }
        }

        internal string Lastname
        {
            get { return _strLastname; }
            set { _strLastname = value; }
        }

        internal string Email
        {
            get { return _strEmail; }
            set { _strEmail = value; }
        }

        internal mod_TTG_UerGroup UserGroup
        {
            get { return _cUserGroup; }
            set { _cUserGroup = value; }
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

                        if (string.IsNullOrEmpty(_strUserCode))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_User.ErrorCode_TTU.CODE_MANDATORY);
                        }
                        else if (!string.IsNullOrEmpty(clsSQL.str_ADOSingleLookUp("TTU_NRI", "TTUser", "TTU_Code = " + clsApp.GetAppController.str_FixStringForSQL(_strUserCode))))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.UNIQUE_ATTRIBUTE, ctr_User.ErrorCode_TTU.CODE_UNIQUE);
                        }
                        else if (string.IsNullOrEmpty(_strFirstname))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_User.ErrorCode_TTU.FIRST_NAME_MANDATORY);
                        }
                        else if (string.IsNullOrEmpty(_strLastname))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_User.ErrorCode_TTU.LAST_NAME_MANDATORY);
                        }
                        else if (!string.IsNullOrEmpty(_strEmail) && !_strEmail.Contains("@"))
                        {
                            mcActionResults.SetInvalid(mintMSG_InvalidEmailFormat, ctr_User.ErrorCode_TTU.EMAIL_INVALID);
                        }
                        else if (!clsSQL.bln_ADOValid_TS("TTUser", "TTU_NRI", _intUser_NRI, "TTU_TS", _intUser_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsSQL.bln_CheckReferenceIntegrity("TTUser", "TTU_NRI", _intUser_NRI))
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
            try
            {
                mcActionResults.SetDefault();

                switch (mintDML_Action)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:

                        if (!pfblnTTU_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("TTUser", out _intUser_NRI))
                        { }
                        else
                        {
                            mcActionResults.SetNewItem_NRI = _intUser_NRI;
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnTTU_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("TTUser", "TTUser.TTU_NRI = " + _intUser_NRI))
                        { }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("TTUser", "TTUser.TTU_NRI = " + _intUser_NRI))
                        { }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    default:
                        mcActionResults.SetValid();

                        break;
                }
            }
            catch (System.Exception ex)
            {
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_SAVE_MSG, clsActionResults.BaseErrorCode.ERROR_SAVE);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResults.IsValid;
        }

        private bool pfblnTTU_AddFields()
        {
            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (_strUserCode != null && !mcSQL.bln_AddField("TTU_Code", _strUserCode, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (_strFirstname != null && !mcSQL.bln_AddField("TTU_FirstName", _strFirstname, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (_strLastname != null && !mcSQL.bln_AddField("TTU_LastName", _strLastname, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (_strPassword != null && !mcSQL.bln_AddField("TTU_Password", _strPassword, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (_strEmail != null && !mcSQL.bln_AddField("TTU_Email", _strEmail, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (_blnIsActive != null && !mcSQL.bln_AddField("TTU_Active", _blnIsActive, clsSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (_intCeritarApp_NRI_Default != null && !mcSQL.bln_AddField("CeA_NRI_Default", _intCeritarApp_NRI_Default, clsSQL.MySQL_FieldTypes.INT_TYPE))
                { }
                else
                {
                    mcActionResults.SetValid();
                }
            }
            catch (Exception ex)
            {
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_SAVE_MSG, clsActionResults.BaseErrorCode.ERROR_SAVE);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResults.IsValid;
        }

    }
}
