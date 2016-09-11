using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceritar.CVS.Controllers.Interfaces
{
    /// <summary>
    /// Cette interface est utilisée par le controleur ctr_User pour accéder aux informations
    /// d'une vue l'implémentant.
    /// </summary>
    public interface IUser
    {
        int GetUser_NRI();
        int GetUser_TS();
        int GetCeritarApp_NRI_Default();
        bool IsActive();
        string GetCode();
        string GetFirstName();
        string GetLastName();
        string GetPassword();
        string GetEMail();
        Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode GetDML_Mode();
    }
}
