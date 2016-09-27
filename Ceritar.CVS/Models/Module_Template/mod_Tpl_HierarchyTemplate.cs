using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers;
using System;

namespace Ceritar.CVS.Models.Module_Template
{
    /// <summary>
    /// Cette classe représente le modèle objet d'un gabarit d'une hiérarchie.
    /// Tpl représente le préfixe des colonnes de la table "Template" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_Tpl_HierarchyTemplate
    {
        //Model attributes
        private int _intTemplate_NRI;
        private ushort _intTemplate_TS;
        private int _intTemplate_NRI_Ref;
        private string _strTemplateName;
        private TemplateType _templateType;
        private bool _blnByDefault;
        private int _intCeritarApplication_NRI;
        private mod_HiCo_HierarchyComponent _cRootSystem; //La racine représentant un dossier système invariable
        //private List<mod_HiCo_HierarchyComponent> _lstHierarchyComponents;

        //Messages
        private const int mintMSG_UniqueDefaultTemplate = 13;
        private const int mintMSG_VersionFolderTypeRules = 19;
        private const int mintMSG_RevisionFolderTypeRules = 36;
        private const int mintMSG_UniqueTemplateNameForApp = 31;

        public enum TemplateType
        {
            Version = 1,
            Revision = 2
        }

        //mod_IBase
        private clsActionResults mcActionResults = new clsActionResults();
        private sclsConstants.DML_Mode mintDML_Action;
        private clsTTSQL mcSQL;

        //Working variables
        private int mintReleaseFolderCount = 0;
        private int mintCaptionsAndMenusFolderCount = 0;
        private int mintScriptsFolderCount = 0;
        private int mintVersionNoFolderCount = 0;
        private int mintReportFolderCount = 0;
        private int mintMinMaxFolderType = 5; //Release, CaptionsAndMenus, Report (if not external), Scripts, Version No


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

        internal int Template_NRI_Ref
        {
            get { return _intTemplate_NRI_Ref; }
            set { _intTemplate_NRI_Ref = value; }
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
            get { return _cRootSystem; }
            set { _cRootSystem = value; }
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

        internal clsTTSQL SetcSQL
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

                        //On verifie si l'application des rapports est obligatoire
                        string strExternalReportAppName = clsTTSQL.str_ADOSingleLookUp("CeA_ExternalRPTAppName", "CerApp", "CeA_NRI = " + _intCeritarApplication_NRI);

                        mintMinMaxFolderType -= (int)(string.IsNullOrEmpty(strExternalReportAppName) ? 1 : 0);

                        if (string.IsNullOrEmpty(_strTemplateName))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_Tpl.NAME_MANDATORY);
                        }
                        else if (!string.IsNullOrEmpty(clsTTSQL.str_ADOSingleLookUp("Tpl_NRI", "Template", "Tpl_NRI <> " + _intTemplate_NRI + " AND Tpl_Name = " + clsTTApp.GetAppController.str_FixStringForSQL(_strTemplateName) + " AND CeA_NRI = " + _intCeritarApplication_NRI + " AND Template.TeT_NRI = " + (int)_templateType)))
                        {
                            mcActionResults.SetInvalid(mintMSG_UniqueTemplateNameForApp, ctr_Template.ErrorCode_Tpl.TEMPLATE_NAME_UNIQUE);
                        }
                        else if ((int)_templateType == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_Tpl.TEMPLATE_TYPE_MANDATORY);
                        }
                        else if (_intCeritarApplication_NRI == 0)
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_Tpl.CERITAR_APPLICATION_MANDATORY);
                        }
                        else if (_cRootSystem == null && _templateType == TemplateType.Version && (_cRootSystem.GetType() == typeof(mod_Folder) && ((mod_Folder)_cRootSystem).LstChildrensComponents.Count == 0 && _cRootSystem.DML_Action == sclsConstants.DML_Mode.UPDATE_MODE))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_Tpl.HIERARCHY_MANDATORY);
                        }
                        else if (!clsTTSQL.bln_ADOValid_TS("Template", "Tpl_NRI", _intTemplate_NRI, "Tpl_TS", _intTemplate_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else if (!pfblnValidateFolderTypes((mod_Folder)_cRootSystem) || 
                                 ((int)_templateType == (int)ctr_Template.TemplateType.VERSION &&
                                  (mintCaptionsAndMenusFolderCount + mintReportFolderCount + mintScriptsFolderCount + mintReleaseFolderCount + mintVersionNoFolderCount) != mintMinMaxFolderType)
                                )
                        {
                            mcActionResults.SetInvalid(mintMSG_VersionFolderTypeRules, ctr_Template.ErrorCode_Tpl.ONLY_NORMAL_AND_OTHER_FOLDERTYPE_MULTIPLE);
                        }
                        else
                        {
                            mintReportFolderCount = 0;
                            mintCaptionsAndMenusFolderCount = 0;
                            mintScriptsFolderCount = 0;
                            mintReleaseFolderCount = 0;
                            mintVersionNoFolderCount = 0;

                            if (!pfblnValidateFolderTypes((mod_Folder)_cRootSystem) ||
                                 ((int)_templateType == (int)ctr_Template.TemplateType.REVISION &&
                                  (mintScriptsFolderCount + mintReleaseFolderCount + mintVersionNoFolderCount) != 3)
                                )
                            {
                                mcActionResults.SetInvalid(mintMSG_RevisionFolderTypeRules, ctr_Template.ErrorCode_Tpl.ONLY_NORMAL_AND_OTHER_FOLDERTYPE_MULTIPLE);
                            }
                            else if (_blnByDefault)
                            {      
                                string strExistingDefaultTemplateName = clsTTSQL.str_ADOSingleLookUp("Tpl_Name", "Template", "Tpl_NRI <> " + _intTemplate_NRI + " AND Tpl_ByDefault = 1 AND CeA_NRI = " + _intCeritarApplication_NRI + " AND Template.TeT_NRI = " + (int)_templateType);

                                if (!string.IsNullOrEmpty(strExistingDefaultTemplateName))
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

                if (mcActionResults.IsValid && _cRootSystem != null)
                {
                    mcActionResults = ((mod_Folder)_cRootSystem).Validate();
                }
            }
            catch (System.Exception ex)
            {
                mcActionResults.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResults;
        }

        private bool pfblnValidateFolderTypes(mod_Folder vcRootFolderToSearchFrom)
        {
            bool blnValidReturn = true;

            try
            {
                foreach (mod_Folder cFolder in ((mod_Folder)vcRootFolderToSearchFrom).LstChildrensComponents)
                {
                    blnValidReturn = pfblnValidateFolderTypes(cFolder);

                    if (!blnValidReturn) break;
                }

                if (blnValidReturn && vcRootFolderToSearchFrom.DML_Action != sclsConstants.DML_Mode.DELETE_MODE)
                {
                    switch (_templateType)
                    {
                        case TemplateType.Version:

                            switch (vcRootFolderToSearchFrom.Type)
                            {
                                case ctr_Template.FolderType.Release:

                                    mintReleaseFolderCount++;
                                    break;

                                case ctr_Template.FolderType.External_Report:

                                    mintReportFolderCount++;
                                    break;

                                case ctr_Template.FolderType.Scripts:

                                    mintScriptsFolderCount++;
                                    break;

                                case ctr_Template.FolderType.CaptionsAndMenus:

                                    mintCaptionsAndMenusFolderCount++;
                                    break;

                                case ctr_Template.FolderType.Version_Number:

                                    mintVersionNoFolderCount++;
                                    break;
                            }

                            if (mintReleaseFolderCount > 1 || mintReportFolderCount > 1 || mintScriptsFolderCount > 1 || mintCaptionsAndMenusFolderCount > 1 || mintVersionNoFolderCount > 1)
                            {
                                blnValidReturn = false;
                            }

                        break;

                        case TemplateType.Revision:

                            switch (vcRootFolderToSearchFrom.Type)
                            {
                                case ctr_Template.FolderType.Release:

                                    mintReleaseFolderCount++;
                                    break;

                                case ctr_Template.FolderType.Scripts:

                                    mintScriptsFolderCount++;
                                    break;

                                case ctr_Template.FolderType.Version_Number:

                                    mintVersionNoFolderCount++;
                                    break;
                            }

                            if (mintReleaseFolderCount > 1 || mintScriptsFolderCount > 1 || mintVersionNoFolderCount > 1)
                            {
                                blnValidReturn = false;
                            }

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
                        else
                        {
                            mcActionResults.SetNewItem_NRI = _intTemplate_NRI;

                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (!pfblnTpl_AddFields())
                        { }
                        else if (!mcSQL.bln_ADOUpdate("Template", "Template.Tpl_NRI = " + _intTemplate_NRI))
                        { }
                        else
                        {
                            blnValidReturn = true;
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!mcSQL.bln_ADODelete("Template", "Template.Tpl_NRI = " + _intTemplate_NRI))
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

        private bool pfblnTpl_AddFields()
        {
            bool blnValidReturn = false;

            try
            {
                if (!mcSQL.bln_RefreshFields())
                { }
                else if (!mcSQL.bln_AddField("Tpl_Name", _strTemplateName, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Tpl_ByDefault", _blnByDefault, clsTTSQL.MySQL_FieldTypes.BIT_TYPE))
                { }
                else if (!mcSQL.bln_AddField("TeT_NRI", (int)_templateType, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("Tpl_NRI_Ref", _intTemplate_NRI_Ref, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
                { }
                else if (!mcSQL.bln_AddField("CeA_NRI", _intCeritarApplication_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
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
