﻿using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Controllers;
using System;

namespace Ceritar.CVS.Models.Module_Template
{
    /// <summary>
    /// Cette classe représente le modèle objet générique d'un composant d'une hiérarchie.
    /// HiCo représente le préfixe des colonnes de la table "HierarchyComp" correspondant au modèle dans la base de données. Les types mod_Folder et mod_Script sont enregistrés dans cette table.
    /// </summary>
    internal abstract class mod_HiCo_HierarchyComponent
    {
        //Model attributes
        protected int _intHierarchyComponent_NRI;
        protected int _intHierarchyComponent_TS;
        protected string _strNameOnDisk;
        protected int _intTemplate_NRI;
        protected mod_HiCo_HierarchyComponent _cParentComponent;

        //mod_IBase
        protected clsActionResults mcActionResults = new clsActionResults();
        protected sclsConstants.DML_Mode mintDML_Action;
        protected clsTTSQL mcSQL;

        //Working variables


#region "Properties"

        internal int HierarchyComponent_NRI
        {
            get { return _intHierarchyComponent_NRI; }
            set { _intHierarchyComponent_NRI = value; }
        }

        internal int HierarchyComponent_TS
        {
            get { return _intHierarchyComponent_TS; }
            set { _intHierarchyComponent_TS = value; }
        }

        internal string NameOnDisk
        {
            get { return _strNameOnDisk; }
            set { _strNameOnDisk = value; }
        }

        internal int Template_NRI
        {
            get { return _intTemplate_NRI; }
            set { _intTemplate_NRI = value; }
        }

        internal mod_HiCo_HierarchyComponent ParentComponent
        {
            get { return _cParentComponent; }
            set { _cParentComponent = value; }
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


        protected clsActionResults Validate()
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

                        if (string.IsNullOrEmpty(_strNameOnDisk))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.MANDATORY_VALUE, ctr_Template.ErrorCode_HiCo.NAME_ON_DISK_MANDATORY);
                        }
                        else if (!clsTTSQL.bln_ADOValid_TS("HierarchyComp", "HiCo_NRI", _intHierarchyComponent_NRI, "HiCo_TS", _intHierarchyComponent_TS))
                        {
                            mcActionResults.SetInvalid(sclsConstants.Validation_Message.INVALID_TIMESTAMP, clsActionResults.BaseErrorCode.INVALID_TIMESTAMP);
                        }
                        else
                        {
                            mcActionResults.SetValid();
                        }

                        break;

                    case sclsConstants.DML_Mode.DELETE_MODE:

                        if (!clsTTSQL.bln_CheckReferenceIntegrity("HierarchyComp", "HiCo_NRI", _intTemplate_NRI))
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

        internal abstract bool blnSave();
        //protected bool blnSave()
        //{
        //    bool blnValidReturn = false;

        //    try
        //    {
        //        mcActionResults.SetValid();

        //        switch (mintDML_Action)
        //        {
        //            case sclsConstants.DML_Mode.INSERT_MODE:

        //                if (!pfblnHiCo_AddFields())
        //                { }
        //                else if (!mcSQL.bln_ADOInsert("HierarchyComp", out _intTemplate_NRI))
        //                { }
        //                else
        //                {
        //                    blnValidReturn = true;
        //                }

        //                break;

        //            case sclsConstants.DML_Mode.UPDATE_MODE:

        //                if (!pfblnHiCo_AddFields())
        //                { }
        //                else if (!mcSQL.bln_ADOUpdate("HierarchyComp", "HierarchyComp.HiCo_NRI = " + _intTemplate_NRI))
        //                { }
        //                else
        //                {
        //                    blnValidReturn = true;
        //                }

        //                break;

        //            case sclsConstants.DML_Mode.DELETE_MODE:

        //                if (!mcSQL.bln_ADODelete("HierarchyComp", "HierarchyComp.HiCo_NRI = " + _intTemplate_NRI))
        //                { }
        //                else
        //                {
        //                    blnValidReturn = true;
        //                }

        //                break;
        //        }
        //    }
        //    catch (System.Exception ex)
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

        protected bool pfblnHiCo_AddFields()
        {
            bool blnValidReturn = false;
            
            try
            {
                if (!mcSQL.bln_AddField("HiCo_Name", _strNameOnDisk, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!mcSQL.bln_AddField("HiCo_Parent_NRI", _cParentComponent._intHierarchyComponent_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
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
