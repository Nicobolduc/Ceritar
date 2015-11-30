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
        private int _templateType;
        private bool _blnByDefault;
        private List<mod_HiCo_HierarchyComponent> _lstHierarchyComponents;

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

        internal int TemplatType
        {
            get { return _templateType; }
            set { _templateType = value; }
        }

        internal bool IsByDefault
        {
            get { return _blnByDefault; }
            set { _blnByDefault = value; }
        }

        List<mod_HiCo_HierarchyComponent> LstHierarchyComponents
        {
            get { return _lstHierarchyComponents; }
            set { _lstHierarchyComponents = value; }
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
                switch (mintDML_Action)
                {
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
                        else if (_lstHierarchyComponents == null || _lstHierarchyComponents.Count == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_Tpl.HIERARCHY_MANDATORY);
                        }
                        else if (!clsSQL.bln_ADOValid_TS("Template", "Tpl_NRI", _intTemplate_NRI, "Tpl_TS", _intTemplate_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else
                        {
                            mcActionResults.SetValid();
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
            }
            catch (System.Exception ex)
            {
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResults;
        }

        internal bool Save()
        {
            bool blnValidReturn = false;

            try
            {
                mcActionResults.SetValid();

                mcSQL = new clsSQL();
                mcSQL.bln_BeginTransaction();

                switch (mintDML_Action)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:

                        if (!pfblnTpl_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("Template", out _intTemplate_NRI))
                        { }
                        else if (!pfblnLists_Save())
                        { }
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
                        else if (!pfblnLists_Save())
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!pfblnLists_Save())
                        { }
                        else if (!mcSQL.bln_ADODelete("Template", "Template.Tpl_NRI = " + _intTemplate_NRI))
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

                mcSQL.bln_EndTransaction(blnValidReturn);
                mcSQL = null;
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

        private bool pfblnLists_Save()
        {
            bool blnValidReturn = false;

            try
            {
                foreach (mod_HiCo_HierarchyComponent cHiCo in _lstHierarchyComponents)
                {
                    cHiCo.SetcSQL = mcSQL;
                    cHiCo.Save();

                    mcActionResults = cHiCo.ActionResults;

                    blnValidReturn = mcActionResults.IsValid;

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
    }
}
