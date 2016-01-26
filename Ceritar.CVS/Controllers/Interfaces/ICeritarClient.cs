using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceritar.CVS.Controllers.Interfaces
{
    /// <summary>
    /// Cette interface est utilisée par le controleur ctr_CeritarClient pour accéder aux informations
    /// d'une vue l'implémentant.
    /// </summary>
    public interface ICeritarClient
    {
        int GetCerClient_NRI();
        int GetCerClient_TS();
        bool GetIsActive();
        string GetName();
        List<int> GetLstCerApplication();
        Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode GetDML_Mode();
    }
}
