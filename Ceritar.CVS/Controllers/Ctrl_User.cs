using System;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_Configuration;

namespace Ceritar.CVS.Controllers
{
    public class ctrl_User
    {
        private mod_User mcUser = null;


        #region "Properties"

        public short GetUserLangage
        {
            get {
                if (mcUser == null)
                {
                    mcUser = new mod_User();
                }

                return mcUser.GetLanguage; 
            }
        }

        #endregion


    }
}
