using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceritar.TT3LightDLL.Classes
{
    public class clsUser
    {
        //Working variables
        private short _intLanguage = 1;




#region "Properties"

        public short GetUserLanguage
        {
            get { return _intLanguage; }
            set { _intLanguage = value; }
        }

#endregion


    }
}
