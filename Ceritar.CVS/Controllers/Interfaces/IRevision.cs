using System;
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
        string GetCeritarApplication_Name();
        int GetCeritarClient_NRI();
        string GetCreationDate();
        string GetCompiledBy();       
        ushort GetVersionNo();
        byte GetRevisionNo();         
        string GetLocation_Release();
        string GetLocation_Scripts();
        List<string> GetModificationsList();
    }
}
