using System;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Controllers.Interfaces
{
    public interface IVersion
    {
        sclsConstants.DML_Mode GetDML_Action();
        int GetVersion_NRI();
        int GetVersion_TS();
        string GetCreationDate();
        string GetCompiledBy();
        ushort GetVersionNo();
        int GetCeritarApplication_NRI();
        int GetTemplateSource_NRI();
        List<int> GetClientUsingList();
        string GetLocation_APP_CHANGEMENT();
        string GetLocation_Release();
        string GetLocation_TTApp();
    }
}
