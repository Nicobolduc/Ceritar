﻿using System;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Controllers.Interfaces
{
    /// <summary>
    /// Cette interface est utilisée par le controleur ctr_Revision pour accéder aux informations
    /// d'une vue l'implémentant.
    /// </summary>
    public interface IRevision
    {
        sclsConstants.DML_Mode GetDML_Action();
        int GetVersion_NRI();
        int GetRevision_NRI();
        int GetRevision_TS();
        int GetTemplateSource_NRI();
        int GetCeritarApplication_NRI();
        string GetCeritarApplication_Name();
        string GetCeritarClient_Name();
        string GetCreationDate();
        string GetCompiledBy();
        string GetCreatedBy();
        string GetNote();
        ushort GetVersionNo();
        sbyte GetRevisionNo();         
        string GetLocation_Release();
        string GetLocation_Scripts();
        string GetLocation_VariousFile();
        string GetLocation_VariousFolder();
        string GetRevisionsToInclude();
        bool GetExeIsExternalReport();
        bool GetExeWithExternalReport();
        bool GetIfScriptsAreToAppend();
        bool IsPreparationMode();
        bool IsPreviousRevisionScriptsIncluded();
        bool GetScriptsOnly();
        bool GetScriptsMerged();
        List<structRevModifs> GetModificationsList();
        List<structSatRevision> GetRevisionSatelliteList();
        structClientAppRevision GetSelectedClient();
        List<structClientAppRevision> GetClientsList();
    }

    /// <summary>
    /// Cette structure représente les informations contenues dans une grille qui fait le lien entre un Client et une Revision.
    /// </summary>
    public struct structClientAppRevision
    {
        public sclsConstants.DML_Mode Action;
        public int intClientAppRevision_NRI;
        public int intClientAppRevision_TS;
        public int intCeritarClient_NRI;
    }

    /// <summary>
    /// Cette structure représente les informations contenues dans une grille qui permet la sélection des Exe pour des applications satellites d'une révision.
    /// </summary>
    public struct structSatRevision
    {
        public sclsConstants.DML_Mode Action;
        public int intSatRevision_NRI;
        public int intClientSatVersion_NRI;
        public int intCeritarSatelliteApp_NRI;
        public string strCeritarSatelliteApp_Name;
        public string strLocationSatelliteExe;
        public string strExportFolderName;
        public bool blnExeIsFolder;
        public bool blnExePerCustomer;
        public int inCeritarClient_NRI_Specific;
    }

    /// <summary>
    /// Cette structure représente les informations contenues dans une grille contenant les modifications d'une revision.
    /// </summary>
    public struct structRevModifs
    {
        public sclsConstants.DML_Mode Action;
        public string strDescriptionModif;
        public int intCeritarClient_NRI;
        public DateTime dthrSent;
    }
}
