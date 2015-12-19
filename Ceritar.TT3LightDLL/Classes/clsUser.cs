using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceritar.TT3LightDLL.Classes
{
    /// <summary>
    /// Cette classe est une petite classe générique représentant l'utilisateur courant de l'application.
    /// </summary>
    public class clsUser
    {
        //Working variables
        private short _intLanguage = 1;
        private int _intUser_NRI = 101;


#region "Properties"

        public short GetUserLanguage
        {
            get { return _intLanguage; }
            set { _intLanguage = value; }
        }

        public int GetUser_NRI
        {
            get { return _intUser_NRI; }
            set { _intUser_NRI = value; }
        }

#endregion


    }
}
