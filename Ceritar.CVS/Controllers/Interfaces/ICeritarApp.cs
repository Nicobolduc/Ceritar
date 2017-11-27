using System;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Controllers.Interfaces
{
    /// <summary>
    /// Cette interface est utilisée par le controleur ctr_CeritarApplication pour accéder aux informations
    /// d'une vue l'implémentant.
    /// </summary>
    public interface ICeritarApp
    {
        int GetCerApp_NRI();
        int GetCerApp_TS();
        int GetDomain_NRI();
        string GetName();
        string GetDescription();
        string GetExternalReportAppName();
        bool IsGeneratingRevisionNoScript();
        List<string> GetLstModules();
        List<structCeritarSatelliteApp> GetLstAppSatellites();
        Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode GetDML_Mode();
    }

    /// <summary>
    /// Cette structure représente les informations contenues dans une grille qui contient la liste des applications satellites d'une application de Ceritar.
    /// </summary>
    public struct structCeritarSatelliteApp
    {
        public sclsConstants.DML_Mode Action;
        public int intCeritarSatelliteApp_NRI;
        public int intCeritarSatelliteApp_TS;
        public bool blnExeIsFolder;
        public string strSatelliteApp_Name;
        public string strKitExport_FolderName;
    }
}
