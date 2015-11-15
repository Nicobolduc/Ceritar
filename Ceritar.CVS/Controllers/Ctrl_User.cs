using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Configuration;

namespace Ceritar.CVS.Controllers
{
    public class Ctrl_User
    {
        private Mod_User mcUser = null;


        #region "Properties"

        public short GetUserLangage
        {
            get {
                if (mcUser == null)
                {
                    mcUser = new Mod_User();
                }

                return mcUser.GetLanguage; 
            }
        }

        #endregion


    }
}
