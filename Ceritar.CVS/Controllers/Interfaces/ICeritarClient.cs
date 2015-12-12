using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceritar.CVS.Controllers.Interfaces
{
    public interface ICeritarClient
    {
        int GetCerClient_NRI();
        int GetCerClient_TS();
        string GetName();
        List<int> GetLstCerApplication();
        Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode GetDML_Mode();
    }
}
