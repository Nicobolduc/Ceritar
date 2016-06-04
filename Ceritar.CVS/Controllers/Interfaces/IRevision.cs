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
        int GetCeritarClient_NRI();
        string GetCreationDate();
        string GetCompiledBy();
        string GetCreatedBy();
        ushort GetVersionNo();
        byte GetRevisionNo();         
        string GetLocation_Release();
        string GetLocation_Scripts();
        bool GetExeIsExternalReport();
        bool GetExeWithExternalReport();
        List<string> GetModificationsList();
        List<structSatRevision> GetRevisionSatelliteList();
    }

    /// <summary>
    /// Cette structure représente les informations contenues dans une grille qui permet la sélection des Exe pour des applications satellites d'une révision.
    /// </summary>
    public struct structSatRevision
    {
        public sclsConstants.DML_Mode Action;
        public int intSatRevision_NRI;
        public int intCeritarSatelliteApp_NRI;
        public string strCeritarSatelliteApp_Name;
        public string strLocationSatelliteExe;
        public string strExportFolderName;
        public bool blnExeIsFolder;
    }
}
