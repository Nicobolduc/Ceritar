using System;
using System.Collections.Generic;

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
        List<string> GetLstModules();
        Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode GetDML_Mode();
    }
}
