using System;
using System.Linq;
using System.Text;
using Ceritar.Logirack_CVS.Forms;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.Logirack_CVS.Static_Classes
{
    internal class sclsMain
    {
        //Messages
        private const int mintMSG_MustBeIdentified = 42;

        public static bool fbln_CreateNewUser(string vstrUser_Code = "")
        {
            bool blnValidReturn = false;
            int intUser_NRI = 0;

            try
            {
                frmUser frmUser = new frmUser();
                frmUser.mstrUser_Code = vstrUser_Code;

                ((TT3LightDLL.Controls.IFormController)frmUser).GetFormController().ShowForm(clsTTApp.GetAppController.GetMDI, sclsConstants.DML_Mode.INSERT_MODE, ref intUser_NRI, true, true);

                if (intUser_NRI <= 0)
                {
                    clsTTApp.GetAppController.ShowMessage(mintMSG_MustBeIdentified);

                    blnValidReturn = false;
                }
                else
                {
                    blnValidReturn = true;
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }
    }
}
