﻿using System;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Controllers.Interfaces
{
    /// <summary>
    /// Cette interface est utilisé par le controleur ctr_Version pour accéder aux informations
    /// d'une vue l'implémentant.
    /// </summary>
    public interface IVersion
    {
        sclsConstants.DML_Mode GetDML_Action();
        int GetVersion_NRI();
        int GetVersion_TS();
        string GetCreationDate();
        string GetCompiledBy();
        ushort GetVersionNo();
        int GetCeritarApplication_NRI();
        string GetCeritarApplication_Name();
        int GetTemplateSource_NRI();
        string GetLocation_APP_CHANGEMENT();
        string GetLocation_Release();
        string GetLocation_TTApp();
        bool GetIsDemo();
        List<structClientAppVersion> GetClientsList();
        List<structClientSatVersion> GetClientSatellitesList();
    }

    /// <summary>
    /// Cette structure représente les informations contenues dans une grille qui fait le lien entre un Client, une Application et une Version.
    /// </summary>
    public struct structClientAppVersion
    {
        public sclsConstants.DML_Mode Action;
        public int intClientAppVersion_NRI;
        public int intClientAppVersion_TS;
        public bool blnIsCurrentVersion;
        public bool blnInstalled;
        public string strLicense;
        public string strCeritarClient_Name;
        public string strLocationReportExe;
        public int intCeritarClient_NRI;
    }

    /// <summary>
    /// Cette structure représente les informations contenues dans une grille qui fait le lien entre un Client, une Application satellite et une Version.
    /// </summary>
    public struct structClientSatVersion
    {
        public sclsConstants.DML_Mode Action;
        public int intClientSatVersion_NRI;
        public int intCeritarClient_NRI;
        public int intCeritarAppSat_NRI;
        public string strCeritarClient_Name;
        public string strCeritarSatelliteApp_Name;
        public string strLocationSatelliteExe;      
    }
}
