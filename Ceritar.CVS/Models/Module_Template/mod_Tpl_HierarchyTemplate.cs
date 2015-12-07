using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers;
using System;

namespace Ceritar.CVS.Models.Module_Template
{
    internal class mod_Tpl_HierarchyTemplate
    {
        //Model attributes
        private int _intTemplate_NRI;
        private ushort _intTemplate_TS;
        private string _strTemplateName;
        private TemplateType _templateType;
        private bool _blnByDefault;
        private int _intCeritarApplication_NRI;
        private mod_HiCo_HierarchyComponent _cRacineSystem;
        //private List<mod_HiCo_HierarchyComponent> _lstHierarchyComponents;

        //Messages
        private const int mintMSG_UniqueDefaultTemplate = 13;

        public enum TemplateType
        {
            Version = 1,
            Revision = 2
        }

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsSQL mcSQL;


#region "Properties"

        internal int Template_NRI
        {
            get { return _intTemplate_NRI; }
            set { _intTemplate_NRI = value; }
        }

        internal ushort Template_TS
        {
            get { return _intTemplate_TS; }
            set { _intTemplate_TS = value; }
        }

        internal string TemplateName
        {
            get { return _strTemplateName; }
            set { _strTemplateName = value; }
        }

        internal TemplateType TemplatType
        {
            get { return _templateType; }
            set { _templateType = value; }
        }

        internal bool IsByDefault
        {
            get { return _blnByDefault; }
            set { _blnByDefault = value; }
        }

        /// <summary>
        /// Représente la racine système du composant. Cette attribut n'est jamais sauvegardé.
        /// </summary>
        internal mod_HiCo_HierarchyComponent RacineSystem
        {
            get { return _cRacineSystem; }
            set { _cRacineSystem = value; }
        }
        //List<mod_HiCo_HierarchyComponent> LstHierarchyComponents
        //{
        //    get { return _lstHierarchyComponents; }
        //    set { _lstHierarchyComponents = value; }
        //}

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

        internal int CeritarApplication_NRI
        {
            get { return _intCeritarApplication_NRI; }
            set { _intCeritarApplication_NRI = value; }
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

                        if (string.IsNullOrEmpty(_strTemplateName))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_Tpl.NAME_MANDATORY);
                        }
                        else if ((int)_templateType == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_Tpl.TEMPLATE_TYPE_MANDATORY);
                        }
                        else if (_intCeritarApplication_NRI == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_Tpl.CERITAR_APPLICATION_MANDATORY);
                        }
                        else if (_cRacineSystem == null || (_cRacineSystem.GetType() == typeof(mod_Folder) && ((mod_Folder)_cRacineSystem).LstChildrensComponents.Count == 0 && _cRacineSystem.DML_Action == sclsConstants.DML_Mode.UPDATE_MODE))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_Tpl.HIERARCHY_MANDATORY);
                        }
                        else if (!clsSQL.bln_ADOValid_TS("Template", "Tpl_NRI", _intTemplate_NRI, "Tpl_TS", _intTemplate_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else
                        {
                            if (_blnByDefault)
                            {      
                                string strDeExistingDefaultTemplateName = clsSQL.str_ADOSingleLookUp("Tpl_Name", "Template", "Tpl_NRI <> " + _intTemplate_NRI + " AND Tpl_ByDefault = 1 AND CeA_NRI = " + _intCeritarApplication_NRI);

                                if (!string.IsNullOrEmpty(strDeExistingDefaultTemplateName))
                                {
                                    mcActionResults.SetInvalid(mintMSG_UniqueDefaultTemplate, ctr_Template.ErrorCode_Tpl.UNIQUE_DEFAULT_TEMPLATE);
                                }
                                else
                                {
                                    mcActionResults.SetValid();
                                }
                            }
                            else
                            {
                                mcActionResults.SetValid();
                            }
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsSQL.bln_CheckReferenceIntegrity("Template", "Tpl_NRI", _intTemplate_NRI))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_REFERENCE_INTEGRITY, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;
                }

                if (mcActionResults.IsValid)
                {
                    mcActionResults = ((mod_Folder)_cRacineSystem).Validate();
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

                        if (!pfblnTpl_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("Template", out _intTemplate_NRI))
                        { }
                        //else if (!pfblnLists_Save())
                        //{ }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnTpl_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("Template", "Template.Tpl_NRI = " + _intTemplate_NRI))
                        { }
                        //else if (!pfblnLists_Save())
                        //{ }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        //if (!pfblnLists_Save())
                        //{ }
                        if (!mcSQL.bln_ADODelete("Template", "Template.Tpl_NRI = " + _intTemplate_NRI))
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

        private bool pfblnTpl_AddFields()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("Tpl_Name", _strTemplateName, clsSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Tpl_ByDefault", _blnByDefault, clsSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("TeT_NRI", (int)_templateType, clsSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeA_NRI", _intCeritarApplication_NRI, clsSQL.MySQL_FieldTypes.NRI_TYPE))
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

        //private bool pfblnLists_Save()
        //{
        //    bool blnValidReturn = false;

        //    try
        //    {
        //        _cRacine.Template_NRI = _intTemplate_NRI;
        //        _cRacine.SetcSQL = mcSQL;

        //        blnValidReturn = _cRacine.blnSave();

        //        mcActionResults = _cRacine.ActionResults;

        //        //foreach (mod_HiCo_HierarchyComponent cHiCo in _lstHierarchyComponents)
        //        //{
        //        //    cHiCo.SetcSQL = mcSQL;
        //        //    cHiCo.Save();

        //        //    mcActionResults = cHiCo.ActionResults;

        //        //    blnValidReturn = mcActionResults.IsValid;

        //        //    if (!blnValidReturn) break;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        blnValidReturn = false;
        //        sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
        //    }
        //    finally
        //    {
        //        if (!blnValidReturn & mcActionResults.IsValid)
        //        {
        //            mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_SAVE_MSG, clsActionResults.BaseErrorCode.ERROR_SAVE);
        //        }
        //        else if (blnValidReturn & !mcActionResults.IsValid)
        //        {
        //            blnValidReturn = false;
        //        }
        //    }

        //    return blnValidReturn;
        //}
    }
}
