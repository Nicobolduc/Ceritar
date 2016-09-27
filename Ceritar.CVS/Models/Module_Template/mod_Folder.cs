using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers;
using System;
using System.IO;
using System.Linq;

namespace Ceritar.CVS.Models.Module_Template
{
    /// <summary>
    /// Cette classe représente le modèle objet d'un dossier dans une hiérarchie.
    /// </summary>
    internal class mod_Folder : mod_HiCo_HierarchyComponent
    {
        //Model attributes
        private ctr_Template.FolderType _folderType;
        private List<mod_HiCo_HierarchyComponent> _lstChildrensComponents;
        private ushort _intNodeLevel;

        //Messages
        private const int mintMSG_InvalidName = 32;
        private const int mintMSG_InvalidNodeLevel = 33;
        private const int mintMSG_UniqueFolderName = 53;

        //Working variables


#region "Properties"

        internal ctr_Template.FolderType Type
        {
            get { return _folderType; }
            set { _folderType = value; }
        }

        internal List<mod_HiCo_HierarchyComponent> LstChildrensComponents
        {
            get 
            {
                if (_lstChildrensComponents == null)
                {
                    _lstChildrensComponents = new List<mod_HiCo_HierarchyComponent>();
                }

                return _lstChildrensComponents; 
            }
            set { _lstChildrensComponents = value; }
        }

        internal ushort NodeLevel
        {
            get { return _intNodeLevel; }
            set { _intNodeLevel = value; }
        }

#endregion


        internal new clsActionResults Validate()
        {
            try
            {
                mcActionResults = base.Validate();

                if (mcActionResults.IsValid)
                {
                    mcActionResults.SetDefault();

                    switch (mintDML_Action)
                    {
                        case sclsConstants.DML_Mode.NO_MODE:
                            mcActionResults.SetValid();

                            break;
                            
                        case sclsConstants.DML_Mode.INSERT_MODE:
                        case sclsConstants.DML_Mode.UPDATE_MODE:

                            if (_folderType == 0)
                            {
                                mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_HiCo.FOLDER_TYPE_MANDATORY);
                            }
                            else if (Path.GetInvalidFileNameChars().Where(x => this._strNameOnDisk.Contains(x)).Count() > 0 || this._strNameOnDisk == "con")
                            {
                                mcActionResults.SetInvalid(mintMSG_InvalidName, ctr_Template.ErrorCode_HiCo.NAME_ON_DISK_INVALID);
                            }
                            else if (!string.IsNullOrEmpty(clsTTSQL.str_ADOSingleLookUp("HiCo_NRI", "HierarchyComp", " HierarchyComp.Tpl_NRI = " + _intTemplate_NRI + " AND HierarchyComp.HiCo_Name = " + clsTTApp.GetAppController.str_FixStringForSQL(_strNameOnDisk))))
                            {
                                mcActionResults.SetInvalid(mintMSG_UniqueFolderName, ctr_Template.ErrorCode_HiCo.NAME_ON_DISK_UNIQUE);
                            }
                            else if (!clsTTSQL.bln_ADOValid_TS("HierarchyComp", "HiCo_NRI", _intHierarchyComponent_NRI, "HiCo_TS", _intHierarchyComponent_TS))
                            {
                                mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                            }
                            //else if (LstChildrensComponents.Count > 0 && LstChildrensComponents[0].GetType() == typeof(mod_Folder) && _intNodeLevel + 1 < ((mod_Folder)LstChildrensComponents[0]).NodeLevel)
                            //{
                            //    mcActionResults.SetInvalid(mintMSG_InvalidNodeLevel, ctr_Template.ErrorCode_HiCo.INVALID_NODE_LEVEL);
                            //}
                            else
                            {
                                mcActionResults.SetValid();
                            }

                            break;

                        case sclsConstants.DML_Mode.DELETE_MODE:

                            if (!clsTTSQL.bln_CheckReferenceIntegrity("Template", "Tpl_NRI", _intTemplate_NRI))
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
                        pfblnChildrens_Validate();
                    }
                }         
            }
            catch (System.Exception ex)
            {
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResults;
        }

        private bool pfblnChildrens_Validate()
        {
            bool blnValidReturn = true;
            int intRowIndex = 3;

            try
            {
                foreach (mod_Folder cHiCo in LstChildrensComponents)
                {
                    if (LstChildrensComponents.Count > 0 && LstChildrensComponents[0].GetType() == typeof(mod_Folder) && _intNodeLevel + 1 < ((mod_Folder)LstChildrensComponents[0]).NodeLevel)
                    {
                        mcActionResults.SetInvalid(mintMSG_InvalidNodeLevel, ctr_Template.ErrorCode_HiCo.INVALID_NODE_LEVEL);
                    }
                    else
                    {
                        mcActionResults = cHiCo.Validate();
                    }

                    if (!blnValidReturn || !mcActionResults.IsValid)
                    {
                        mcActionResults.RowInError = mcActionResults.RowInError <= intRowIndex ? intRowIndex : mcActionResults.RowInError;

                        break;
                    }

                    intRowIndex++;
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

        internal override bool  blnSave()
        {
            bool blnValidReturn = false;

            try
            {
                mcActionResults.SetValid();

                switch (mintDML_Action)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:

                        if (!pfblnFoT_AddFields())
                        { }
                        else if (!base.pfblnHiCo_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOInsert("HierarchyComp", out _intHierarchyComponent_NRI))
                        { }
                        else if (!pfblnChildrens_Save())
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnFoT_AddFields())
                        { }
                        else if (!base.pfblnHiCo_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("HierarchyComp", "HierarchyComp.HiCo_NRI = " + _intHierarchyComponent_NRI))
                        { }
                        else if (!pfblnChildrens_Save())
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!pfblnChildrens_Save())
                        { }
                        else if (!mcSQL.bln_ADODelete("HierarchyComp", "HierarchyComp.HiCo_NRI = " + _intHierarchyComponent_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    default:

                        blnValidReturn = pfblnChildrens_Save();

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

        private bool pfblnFoT_AddFields()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("FoT_NRI", (int)_folderType, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("HiCo_IsFolder", 1, clsTTSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("HiCo_NodeLevel", _intNodeLevel, clsTTSQL.MySQL_FieldTypes.INT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Tpl_NRI", _intTemplate_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
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

        private bool pfblnChildrens_Save()
        {
            bool blnValidReturn = true;

            try
            {
                foreach (mod_HiCo_HierarchyComponent cHiCo in LstChildrensComponents)
                {
                    cHiCo.SetcSQL = mcSQL;
                    cHiCo.Template_NRI = _intTemplate_NRI;

                    blnValidReturn = cHiCo.blnSave();

                    mcActionResults = cHiCo.ActionResults;

                    if (!blnValidReturn || !mcActionResults.IsValid) break;
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