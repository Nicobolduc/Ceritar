using System;
using System.Collections.Generic;
using System.Management;

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

        public short UserLanguage
        {
            get { return _intLanguage; }
            set { _intLanguage = value; }
        }

        public int User_NRI
        {
            get { return _intUser_NRI; }
            set { _intUser_NRI = value; }
        }

#endregion


        public string str_GetUserComputerID()
        {
            string strCPU_ID = string.Empty;
            string strDrive = "C";
            string strVolumeSerial;
            string strUnique_ID;

            ManagementClass cManClass = new ManagementClass("win32_processor");
            ManagementObjectCollection lstManObject = cManClass.GetInstances();
            ManagementObject localDisk_ID;

            foreach (ManagementObject cManObject in lstManObject)
            {
                strCPU_ID = cManObject.Properties["processorID"].Value.ToString();

                break;
            }
            
            localDisk_ID = new ManagementObject(@"win32_logicaldisk.deviceid=""" + strDrive + @":""");
            localDisk_ID.Get();
            strVolumeSerial = localDisk_ID["VolumeSerialNumber"].ToString();

            strUnique_ID = strCPU_ID + strVolumeSerial;

            return strUnique_ID;
        }
    }
}
