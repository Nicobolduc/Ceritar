using System;
using System.Collections.Generic;

namespace Ceritar.CVS.Controllers.Interfaces
{
    public interface ICeritarApp
    {
        int GetCerApp_NRI();
        int GetDomain_NRI();
        string GetName();
        string GetDescription();
        List<string> GetLstModules();
        Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode GetDML_Mode();
    }
}
