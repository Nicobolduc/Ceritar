using System;
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
        int GetRevision_NRI();
        int GetRevision_TS();
        string GetCreationDate();
        string GetCompiledBy();
        ushort GetVersionNo();
        ushort GetRevisionNo();
        int GetCeritarApplication_NRI();
        string GetCeritarApplication_Name();
        int GetTemplateSource_NRI();
        string GetLocation_Release();
        string GetLocation_Scripts();
    }
}
