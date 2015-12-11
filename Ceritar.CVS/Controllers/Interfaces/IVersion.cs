using System;
using System.Collections.Generic;

namespace Ceritar.CVS.Controllers.Interfaces
{
    public interface IVersion
    {
        int GetVersion_NRI();
        int GetVersion_TS();
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
