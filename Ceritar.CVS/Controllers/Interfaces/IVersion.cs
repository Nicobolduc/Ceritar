using System;
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
        ushort GetVersionNo();
        int GetCeritarApplication_NRI();
        int GetCeritarApplication_NRI_Master();
        int GetTemplateSource_NRI();
        int GetCreatedByUser_NRI();
        string GetCreationDate();
        string GetCompiledBy();
        string GetCeritarApplication_Name();
        string GetLocation_APP_CHANGEMENT();
        string GetLocation_Release();
        string GetLocation_TTApp();
        string GetLocation_VariousFile();
        string GetLocation_VariousFolder();
        string GetLatestVersionNo();
        string GetDescription();
        string GetExternalRPTAppName();
        bool GetIsDemo();
        bool GetIsBaseKit();
        bool GetIncludeScriptsOnRefresh();
        
        structClientAppVersion GetSelectedClient();
        List<structClientAppVersion> GetClientsList();
        List<structClientSatVersion> GetClientSatellitesList(int vintCeritarClient_NRI);
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
        public string strDateInstalled;
        public string strLicense;
        public string strCeritarClient_Name;
        public string strLocationReportExe;
        public string strLocationScriptsRoot;
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
        public int intCeritarSatelliteApp_NRI;
        public string strCeritarClient_Name;
        public string strCeritarSatelliteApp_Name;
        public string strLocationSatelliteExe;
        public bool blnExeIsFolder;
        public bool blnExePerCustomer;
        public string strKitFolderName;
    }
}
